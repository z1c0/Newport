using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Newport
{
  public class BackNavigationBehavior : Behavior<Page>
  {
    private static Frame _frame;

    public string NavigationTarget { get; set; }

    protected override void OnAttached()
    {
      base.OnAttached();
      if (_frame == null)
      {
        _frame = ControlFinder.FindParent<Frame>(Window.Current.Content);
        _frame.Navigating += (f, e) =>
        {
          if (e.NavigationMode == NavigationMode.Back && ((Frame)f).SourcePageType == AssociatedObject.GetType())
          {
            e.Cancel = true;
            if (NavigationTarget != null)
            {
              var t = Type.GetType(NavigationTarget);
              // TODO: extension method -> Dispatcher.Run
              new DispatcherHelper().Invoke(() => _frame.Navigate(t));
            }
          }
        };
      }
    }
  }
}