using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Windows.Storage;
#if UNIVERSAL
#else
using System.Windows;
using System.ComponentModel;
using System.IO.IsolatedStorage;
#endif

namespace Newport
{
  public class IsolatedStorageHelper
  {
    public void Save(string key, object t)
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
#if UNIVERSAL
            ApplicationData.Current.RoamingSettings.Values[key] = xml;
#else
            IsolatedStorageSettings.ApplicationSettings[key] = xml;
            IsolatedStorageSettings.ApplicationSettings.Save();
#endif
          }
        }
      }
      catch (Exception e)
      {
        Trace.WriteLine(e);
      }
    }

    public T Load<T>(string key) where T : class
    {
      var t = default(T);
      try
      {
        object o;
#if UNIVERSAL
        if (ApplicationData.Current.RoamingSettings.Values.TryGetValue(key, out o))
#else
        if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<object>(key, out o))
#endif
        {
          var xml = o as string;
          using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
          {
            var serializer = new XmlSerializer(typeof(T));
            t = (T)serializer.Deserialize(ms);
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