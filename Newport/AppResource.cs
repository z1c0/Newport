using System.Diagnostics;
using System.Windows;

namespace Newport
{
  public static class AppResource
  {
    public static T Get<T>(string name)
    {
      var o = Application.Current.Resources[name];
      Debug.Assert(o != null);
      return (T)o;
    }
  }
}
