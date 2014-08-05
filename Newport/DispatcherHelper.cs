using System;
using System.Windows;
using System.Windows.Threading;

namespace Newport
{
  public class DispatcherHelper
  {
    private readonly Dispatcher _dispatcher;

    public DispatcherHelper()
      : this(null)
    {
    }

    public DispatcherHelper(DependencyObject control)
    {
      _dispatcher = control == null ? Deployment.Current.Dispatcher : control.Dispatcher;
    }

    public void Invoke(Action a)
    {
      if (_dispatcher != null)
      {
        _dispatcher.BeginInvoke(a);
      }
    }

    public void Invoke(Delegate method, object parameter)
    {
      if (_dispatcher != null)
      {
        _dispatcher.BeginInvoke(method, parameter);
      }
    }
  }
}