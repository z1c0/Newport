using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
#if UNIVERSAL
using Windows.Storage;
#else
using System.Windows;
using System.ComponentModel;
using System.IO.IsolatedStorage;
#endif

namespace Newport
{
  internal static class SettingsStorageHelper
  {
    private const string SAVE_KEY = "SAVE_KEY";

    internal static void Save(object t)
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
            ApplicationData.Current.RoamingSettings.Values[SAVE_KEY] = xml;
#else
            IsolatedStorageSettings.ApplicationSettings[SAVE_KEY] = xml;
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

    internal static T Load<T>() where T : class
    {
      var t = default(T);
      try
      {
        object o;
#if UNIVERSAL
        if (ApplicationData.Current.RoamingSettings.Values.TryGetValue(SAVE_KEY, out o))
#else
        if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<object>(SAVE_KEY, out o))
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