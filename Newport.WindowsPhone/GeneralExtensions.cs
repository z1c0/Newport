using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
#if NETFX_CORE
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;
#else
using System.Windows.Media;
using System.Windows.Media.Imaging;
#endif

namespace Newport
{
  public static class GeneralExtensions
  {
    public static T Random<T>(this List<T> list)
    {
      return list[new Random().Next(list.Count)];
    }

    public static T Random<T>(this T[] array)
    {
      return array[new Random().Next(array.Length)];
    }

    public static void Times(this int count, Action<int> action)
    {
      for (var i = 0; i < count; i++)
      {
        action(i);
      }
    }

    public static void AppendUnique<T>(this List<T> list, T item)
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

    public static T GetRandomElement<T>(this IEnumerable<T> items)
    {
      return items.ElementAt(new RandomData().GetInt(items.Count()));
    }

    public static void AddRange<T>(this ObservableCollection<T> items, IEnumerable<T> range)
    {
      range.ForEach(a => items.Add(a));
    }

    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items)
    {
      var observableCollection = new ObservableCollection<T>();
      items.ForEach(a => observableCollection.Add(a));
      return observableCollection;
    }

    public static int ToARGB32(this Color color)
    {
      return ((color.R << 16) | (color.G << 8) | (color.B << 0) | (color.A << 24));
    }

    public static Color ToColor(this int argb32)
    {
      byte b = (byte)(argb32 & 0xFF);
      argb32 >>= 8;
      byte g = (byte)(argb32 & 0xFF);
      argb32 >>= 8;
      byte r = (byte)(argb32 & 0xFF);
      argb32 >>= 8;
      byte a = (byte)(argb32 & 0xFF);
      return Color.FromArgb(a, r, g, b);
    }

    public static Color GrayScale(this Color color)
    {
      // Lightness
      //var v = (byte)((Math.Max(color.R, Math.Max(color.G, color.B)) + Math.Min(color.R, Math.Min(color.G, color.B))) / 2);
      // Average
      //var v = (byte)((color.R + color.G + color.B) / 3);
      // Luminosity
      var v = (byte)(0.21 * color.R + 0.71 * color.G + 0.07 * color.B);
      return Color.FromArgb(255, v, v, v);
    }

    public static Color Sepia(this Color color)
    {
      var r = (byte)Math.Min(255, ((color.R * .393) + (color.G * .769) + (color.B * .189)));
      var g = (byte)Math.Min(255, ((color.R * .349) + (color.G * .686) + (color.B * .168)));
      var b = (byte)Math.Min(255, ((color.R * .272) + (color.G * .534) + (color.B * .131)));
      return Color.FromArgb(255, r, g, b);
    }
  }
}