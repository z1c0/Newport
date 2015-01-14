using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace Newport
{
  public class ServiceInformation
  {
    public override string ToString()
    {
      return string.Format("{0}: {1} at {2}", Identifier, Location, HostName);
    }

    public Uri Location { get; internal set; }

    public string Identifier { get; internal set; }
    
    public HostName HostName { get; internal set; }
  }

  public class ServiceLocator
  {
    private const string SSDP_ADDR = "239.255.255.250";

    public Task<IEnumerable<ServiceInformation>> LocateAll()
    {
      return Locate("ssdp:all");

    }

    public async Task<IEnumerable<ServiceInformation>> Locate(string service)
    {
      var results = new List<ServiceInformation>();
      try
      {
        var socket = new DatagramSocket();
        TypedEventHandler<DatagramSocket, DatagramSocketMessageReceivedEventArgs> handler = (_, a) => HandleMessageReceived(results, a);
        socket.MessageReceived += handler;
        if (string.IsNullOrEmpty(socket.Information.LocalPort))
        {
          await socket.BindServiceNameAsync("");
          socket.JoinMulticastGroup(new HostName(SSDP_ADDR));
        }
        var s = await socket.GetOutputStreamAsync(new HostName(SSDP_ADDR), "1900");
        using (var dataWriter = new DataWriter(s))
        {
          var msg = string.Format( 
            "M-SEARCH * HTTP/1.1\r\n" + 
            "HOST:239.255.255.250:1900\r\n" +
            "MAN:\"ssdp:discover\"\r\n" +
            "ST:{0}\r\n" + 
            "MX:3\r\n\r\n", service);
          var data = Encoding.UTF8.GetBytes(msg);
          dataWriter.WriteBytes(data);
          await dataWriter.StoreAsync();
          await Task.Delay(TimeSpan.FromMilliseconds(100));
          socket.MessageReceived -= handler;
        }
      }
      catch (Exception e)
      {
        Trace.WriteLine(e);
      }
      return results;
    }

    private static void HandleMessageReceived(ICollection<ServiceInformation> results, DatagramSocketMessageReceivedEventArgs args)
    {
      try
      {
        var reader = args.GetDataReader();
        var data = new byte[reader.UnconsumedBufferLength];
        reader.ReadBytes(data);
        var s = Encoding.UTF8.GetString(data, 0, data.Length);
        var si = ParseResponse(s);
        si.HostName = args.RemoteAddress;
        results.Add(si);
      }
      catch (Exception e)
      {
        Debug.WriteLine(e);
      }
    }

    private static ServiceInformation ParseResponse(string s)
    {
      var pairs =
        (from parts in s.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries)
         where parts.Contains(':')
         let t = parts.Split(new[] {':'}, 2, StringSplitOptions.None)
         select new Tuple<string, string>(t[0].Trim().ToUpperInvariant(), t[1].Trim())).ToList();
      return new ServiceInformation
      {
        Location = new Uri(pairs.FirstOrDefault(t => t.Item1 == "LOCATION").Item2),
        Identifier = pairs.FirstOrDefault(t => t.Item1 == "ST").Item2,
      };
    }
  }
}
