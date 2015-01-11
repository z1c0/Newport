using System;
using System.Windows.Interactivity;
using Microsoft.Phone.Controls;

namespace Newport
{
  public class BackNavigationBehavior : Behavior<PhoneApplicationPage>
  {
    public Uri NavigationUri { get; set; }

    protected override void OnAttached()
    {
      base.OnAttached();

      AssociatedObject.BackKeyPress += (o, e) =>
      {
        e.Cancel = true;
        if (NavigationUri != null)
        {
          Get.Navigator.Navigate(NavigationUri);
        }
      };
    }
  }
}