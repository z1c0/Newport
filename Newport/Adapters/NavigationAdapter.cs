using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace Newport
{
  public static class NavigationAdapter
  {
    static NavigationAdapter()
    {
      State = new Dictionary<string, object>();
    }

    public static Dictionary<string, object> State { get; private set; }

    public static void Navigate(Type t)
    {
      Navigate(new Uri("/" + t.Name + ".xaml", UriKind.Relative));
    }

    public static void Navigate(Uri uri)
    {
      NavigationService.Navigate(uri);
    }

    internal static NavigationService NavigationService
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