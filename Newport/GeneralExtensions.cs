using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
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
      return items.ElementAt(RandomData.GetInt(items.Count()));
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

    public static void SaveToMediaLibrary(this BitmapImage bmi, string fileName)
    {
      new WriteableBitmap(bmi).SaveToMediaLibrary(fileName);
    }

    public static void SaveToMediaLibrary(this WriteableBitmap wb, string fileName)
    {
      var store = IsolatedStorageFile.GetUserStoreForApplication();
      // If a file with this name already exists, delete it.
      var tempName = Guid.NewGuid().ToString();
      using (var fileStream = store.CreateFile(tempName))
      {
        // Save the WriteableBitmap into isolated storage as JPEG.
        Extensions.SaveJpeg(wb, fileStream, wb.PixelWidth, wb.PixelHeight, 0, 100);
      }
      using (var fileStream = store.OpenFile(tempName, FileMode.Open, FileAccess.Read))
      {
        // Now, add the JPEG image to the photos library.
        var library = new MediaLibrary();
        var pic = library.SavePicture(fileName, fileStream);
      }
    }
  }
}