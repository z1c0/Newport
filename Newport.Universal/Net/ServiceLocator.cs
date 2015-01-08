using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace Newport
{
  public class ServiceLocator
  {
    private const string SSDP_ADDR = "239.255.255.250";
    private readonly HttpClient _client;
    private readonly DatagramSocket _socket;

    public ServiceLocator()
    {
      _client = new HttpClient();
      _socket = new DatagramSocket();
      _socket.MessageReceived += HandleMessageReceived;
    }

    public async void Locate()
    {
      try
      {
        if (string.IsNullOrEmpty(_socket.Information.LocalPort))
        {
          await _socket.BindServiceNameAsync("");
          _socket.JoinMulticastGroup(new HostName(SSDP_ADDR));
        }
        var s = await _socket.GetOutputStreamAsync(new HostName(SSDP_ADDR), "1900");
        using (var dataWriter = new DataWriter(s))
        {
          var msg = 
            "M-SEARCH * HTTP/1.1\r\n" + 
            "HOST:239.255.255.250:1900\r\n" +
            "MAN:\"ssdp:discover\"\r\n" +
            "ST:urn:schemas-frontier-silicon-com:fs_reference:fsapi:1\r\n" + 
            "MX:3\r\n\r\n";
          var data = Encoding.UTF8.GetBytes(msg);
          dataWriter.WriteBytes(data);
          await dataWriter.StoreAsync();
        }
      }
      catch (Exception e)
      {
        Debug.WriteLine(e);
      }
    }

    private void HandleMessageReceived(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
    {
      try
      {
        var reader = args.GetDataReader();
        var data = new byte[reader.UnconsumedBufferLength];
        reader.ReadBytes(data);
        var s = Encoding.UTF8.GetString(data, 0, data.Length);
        ParseResponse(s);
      }
      catch (Exception e)
      {
        Debug.WriteLine(e);
      }
    }

    private void ParseResponse(string s)
    {
      var pairs =
        from parts in s.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
        where parts.Contains(':')
        let t = parts.Split(new[] { ':' }, 2, StringSplitOptions.None)
        select new Tuple<string, string>(t[0].Trim().ToUpperInvariant(), t[1].Trim());
      var location = pairs.FirstOrDefault(t => t.Item1 == "LOCATION");
      if (location != null)
      {
        Debug.WriteLine(location.Item2);
        if (ServiceFound != null)
        {
          ServiceFound(this, new Uri(location.Item2));
        }
      }
    }

    public event EventHandler<Uri> ServiceFound;
  }
}
