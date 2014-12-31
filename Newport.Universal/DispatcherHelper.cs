using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Newport
{
  public class DispatcherHelper
  {
    private readonly CoreDispatcher _dispatcher;

    public DispatcherHelper()
      : this(null)
    {
    }

    public DispatcherHelper(DependencyObject control)
    {
      _dispatcher = control == null ? CoreApplication.MainView.CoreWindow.Dispatcher : control.Dispatcher;
    }

    public async void Invoke(Action a)
    {
      if (_dispatcher != null)
      {
        await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => a());
      }
    }
  }
}