using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Newport
{
  public static class VisualTreeEnumeration
  {
    public static IEnumerable<DependencyObject> Descendants(this DependencyObject root, IEnumerable<Type> excludedTypes = null)
    {
      excludedTypes = excludedTypes ?? new List<Type>();
      var count = VisualTreeHelper.GetChildrenCount(root);
      for (var i = 0; i < count; i++)
      {
        var child = VisualTreeHelper.GetChild(root, i);
        if (!excludedTypes.Contains(child.GetType()))
        {
          yield return child;
          foreach (var descendent in Descendants(child, excludedTypes))
          {
            yield return descendent;
          }
        }
      }
    }
  }
}