using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Newport
{
  public class BackNavigationBehavior : Behavior<Frame>
  {
    public Uri NavigationUri { get; set; }

    protected override void OnAttached()
    {
      base.OnAttached();

      AssociatedObject.Navigating += (_, e) =>
      {
        e.Cancel = true;
        if (e.NavigationMode == NavigationMode.Back && NavigationUri != null)
        {
          //NavigationAdapter.NavigationService.Navigate(NavigationUri);
        }
      };
    }
  }
}