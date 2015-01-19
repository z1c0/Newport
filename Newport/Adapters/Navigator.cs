using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace Newport
{
  public class Navigator : BaseNavigator
  {
    public void Navigate(Uri uri)
    {
      NavigationService.Navigate(uri);
    }

    public override void Navigate(Type type, object dataContext = null)
    {
      Navigate(new Uri("/" + type.Name + ".xaml", UriKind.Relative));
    }

    public override void NavigateBack()
    {
      NavigationService.GoBack();
    }

    internal NavigationService NavigationService
    {
      get
      {
        var page = ControlFinder.FindChild<PhoneApplicationPage>(Application.Current.RootVisual);
        if (page == null)
        {
          throw new InvalidOperationException("No PhoneApplicationPage ");
        }
        if (page.NavigationService == null)
        {
          throw new InvalidOperationException("No NavigationService available");
        }
        return page.NavigationService;
      }
    }
  }
}