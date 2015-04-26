using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
#if UNIVERSAL
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;
#else
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Media;
using System.IO.IsolatedStorage;
#endif

namespace Newport
{
  public static class GeneralExtensions
  {
    public static T Random<T>(this List<T> list)
    {
      return list[RandomData.GetInt(list.Count)];
    }

    public static T Random<T>(this T[] array)
    {
      return array[RandomData.GetInt(array.Length)];
    }

    public static void Shuffle<T>(this List<T> list)
    {
      for (var i = 0; i < list.Count; i++)
      {
        var j = RandomData.GetInt(i, list.Count);
        var tmp = list[i];
        list[i] = list[j];
        list[j] = tmp;
      }
    }

    public static void Times(this int count, Action<int> action)
    {
      for (var i = 0; i < count; i++)
      {
        action(i);
      }
    }

    public static void AddOrReplace<K, V>(this Dictionary<K, V> dict, K key, V value)
    {
      if (dict.ContainsKey(key))
      {
        dict[key] = value;
      }
      else
      {
        dict.Add(key, value);
      }
    }

    public static void AddUnique<T>(this IList<T> list, T item)
    {
      if (!list.Contains(item))
      {
        list.Add(item);
      }
    }

    public static T RemoveLast<T>(this List<T> list)
    {
      var t = list[list.Count - 1];
      list.RemoveAt(list.Count - 1);
      return t;
    }

    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
    {
      foreach (var item in items)
      {
        action(item);
      }
    }

    public static void ForEach<T>(this IEnumerable<T> items, Action<int, T> action)
    {
      var i = 0;
      foreach (var item in items)
      {
        action(i++, item);
      }
    }

    public static T GetRandomElement<T>(this IEnumerable<T> items)
    {
      return items.ElementAt(RandomData.GetInt(items.Count()));
    }

    public static void ClearAndAddRange<T>(this ObservableCollection<T> items, IEnumerable<T> range)
    {
      items.Clear();
      range.ForEach(items.Add);
    }

    public static void AddRange<T>(this ObservableCollection<T> items, IEnumerable<T> range)
    {
      range.ForEach(items.Add);
    }

    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items)
    {
      var observableCollection = new ObservableCollection<T>();
      items.ForEach(observableCollection.Add);
      return observableCollection;
    }
  }
}