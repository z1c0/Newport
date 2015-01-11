using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Newport
{
  public class Navigator : BaseNavigator
  {
    public override void Navigate(Type type, object dataContext = null)
    {
      if (type != null)
      {
        var frame = ControlFinder.FindParent<Frame>(Window.Current.Content);
        if (frame != null)
        {
          base.Navigate(type, dataContext);
          frame.Navigate(type);
        }
      }
    }
  }
}
