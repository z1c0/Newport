using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Windows;
using System.Xml.Serialization;

namespace Newport
{
  public class IsolatedStorageHelper
  {
    private const string SAVE_KEY = "SAVE_KEY";

    public void Save(object t)
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
            IsolatedStorageSettings.ApplicationSettings[SAVE_KEY] = xml;
            IsolatedStorageSettings.ApplicationSettings.Save();
          }
        }
      }
      catch (Exception e)
      {
        Error(e);
      }
    }

    [Conditional("DEBUG")]
    private void Error(Exception e)
    {
      if (!DesignerProperties.IsInDesignTool)
      {
        MessageBox.Show(e.ToString());
      }
    }

    public T Load<T>() where T : class
    {
      var t = default(T);
      try
      {
        string xml = null;
        if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<string>(SAVE_KEY, out xml))
        {
          using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
          {
            var serializer = new XmlSerializer(typeof(T));
            t = (T)serializer.Deserialize(ms);
          }
        }
      }
      catch (Exception e)
      {
        Error(e);
      }
      return t;
    }
  }
}