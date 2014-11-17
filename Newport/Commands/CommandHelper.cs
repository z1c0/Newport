using System;
using Windows.UI.Core;
#if UNIVERSAL
using Windows.Foundation;
#else
using System.Windows;
using System.Windows.Input;
#endif

namespace Newport
{
  public static class CommandHelper
  {
    public static Point GetPosition(ActionEventSource actionEventSource)
    {
      //TappedRoutedEventArgs 
      var gestureEventArgs = actionEventSource.OriginalEventArgs as GestureEventArgs;
      if (gestureEventArgs != null)
      {
        return gestureEventArgs.GetPosition(actionEventSource.OriginalSender);
      }
      throw new NotImplementedException("TODO");
    }
  }
}