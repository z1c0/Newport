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

    public static NavigationService NavigationService
    {
      get
      {
        var page = new ControlFinder().FindChild<PhoneApplicationPage>(Application.Current.RootVisual);
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

    public static NavigationService TryGetNavigationService()
    {
      NavigationService service = null;
      try
      {
        service = NavigationService;
      }
      catch (Exception)
      {
      }
      return service;
    }
  }
}