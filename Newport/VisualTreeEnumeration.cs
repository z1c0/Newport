using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Newport
{
  public static class VisualTreeEnumeration
  {
    public static IEnumerable<DependencyObject> Descendents(this DependencyObject root, int depth)
    {
      var count = VisualTreeHelper.GetChildrenCount(root);
      for (var i = 0; i < count; i++)
      {
        var child = VisualTreeHelper.GetChild(root, i);
        yield return child;
        if (depth > 0)
        {
          foreach (var descendent in Descendents(child, --depth))
          {
            yield return descendent;
          }
        }
      }
    }

    public static IEnumerable<DependencyObject> Descendents(this DependencyObject root)
    {
      return Descendents(root, Int32.MaxValue);
    }
  }
}