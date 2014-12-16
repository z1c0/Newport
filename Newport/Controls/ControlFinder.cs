#if UNIVERSAL
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
#else
using System.Windows;
using System.Windows.Media;
#endif

namespace Newport
{
  public static class ControlFinder
  {
    public static T FindParent<T>(DependencyObject o) where T : class
    {
      T t = null;
      while (o != null)
      {
        t = o as T;
        if (t != null)
        {
          break;
        }
        o = VisualTreeHelper.GetParent(o);
      }
      return t;
    }

    public static T FindChild<T>(DependencyObject o) where T : class
    {
      var t = o as T;
      if (t == null)
      {
        for (var i = 0; i < VisualTreeHelper.GetChildrenCount(o); i++)
        {
          t = FindChild<T>(VisualTreeHelper.GetChild(o, i));
          if (t != null)
          {
            break;
          }
        }
      }
      return t;
    }
  }
}