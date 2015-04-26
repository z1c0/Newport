using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace Newport
{
  public static class IsolatedStorageHelper
  {
    public static async void Save(string key, object t)
    {
      try
      {
        using (var ms = new MemoryStream())
        {
          var serializer = new XmlSerializer(t.GetType());
          serializer.Serialize(ms, t);
          ms.Position = 0;
          using (var reader = new StreamReader(ms))
          {
            var xml = reader.ReadToEnd();
            var folder = ApplicationData.Current.LocalFolder;
            var file = await folder.CreateFileAsync(key, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, xml);
          }
        }
      }
      catch (Exception e)
      {
        Trace.WriteLine(e);
      }
    }

    public static async Task<T> Load<T>(string key) where T : class
    {
      var t = default(T);
      try
      {
        var folder = ApplicationData.Current.LocalFolder;
        var file = await folder.CreateFileAsync(key, CreationCollisionOption.OpenIfExists);
        if (file != null)
        {
          var xml = await FileIO.ReadTextAsync(file);
          if (!string.IsNullOrEmpty(xml))
          {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
              var serializer = new XmlSerializer(typeof(T));
              t = (T)serializer.Deserialize(ms);
            }
          }
        }
      }
      catch (Exception e)
      {
        Trace.WriteLine(e);
      }
      return t;
    }
  }
}