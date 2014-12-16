using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Newport
{
  public static class NavigationAdapter
  {
    public static void Navigate(Type type)
    {
      if (type != null)
      {
        var frame = ControlFinder.FindParent<Frame>(Window.Current.Content);
        if (frame != null)
        {
          frame.Navigate(type);
        }
      }
    }
  }
}
