using Microsoft.Xna.Framework.Media;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;

namespace Newport
{
  public static class MediaExtensions
  {
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
        wb.SaveJpeg(fileStream, wb.PixelWidth, wb.PixelHeight, 0, 100);
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