using System;
using System.Windows;
using System.Windows.Input;

namespace Newport
{
  public static class CommandHelper
  {
    public static Point GetPosition(ActionEventSource actionEventSource)
    {
      var gestureEventArgs = actionEventSource.OriginalEventArgs as GestureEventArgs;
      if (gestureEventArgs != null)
      {
        return gestureEventArgs.GetPosition(actionEventSource.OriginalSender);
      }
      throw new NotImplementedException("TODO");
    }
  }
}