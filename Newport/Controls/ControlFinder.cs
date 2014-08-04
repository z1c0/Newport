﻿using System.Windows;
using System.Windows.Media;

namespace Newport
{
  public class ControlFinder
  {
    public T FindParent<T>(DependencyObject o) where T : class
    {
      T t = null;
      while (o != null)
      {
        t = o as T;
        if (t != null)
        {
          break;
        }
        o = VisualTreeHelper.GetParent(o);
      }
      return t;
    }

    public T FindChild<T>(DependencyObject o) where T : class
    {
      var t = o as T;
      if (t == null)
      {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(o); i++)
        {
          t = FindChild<T>(VisualTreeHelper.GetChild(o, i));
          if (t != null)
          {
            break;
          }
        }
      }
      return t;
    }
  }
}