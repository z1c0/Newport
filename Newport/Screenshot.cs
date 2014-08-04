using Microsoft.Xna.Framework.Media;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Newport
{
  public class Screenshot
  {
    public void Save(FrameworkElement element = null, string title = null)
    {
      try
      {
        if (element == null)
        {
          element = Application.Current.RootVisual as FrameworkElement;
        }
        var bmp = new WriteableBitmap(element, null);
        Save(bmp, title);
      }
      catch
      {
        MessageBox.Show(
          "There was an error. Please disconnect your phone from the computer before saving.",
          "Cannot save",
          MessageBoxButton.OK);
      }
    }

    public void Save(WriteableBitmap bmp, string title = null, int width = 0, int height = 0)
    {
      try
      {
        if (string.IsNullOrEmpty(title))
        {
          title = Guid.NewGuid().ToString();
        }
        var ms = new MemoryStream();
        if (width == 0)
        {
          width = bmp.PixelWidth;
        }
        if (height == 0)
        {
          height = bmp.PixelHeight;
        }
        bmp.SaveJpeg(ms, width, height, 0, 100);
        ms.Seek(0, SeekOrigin.Begin);
        var lib = new MediaLibrary();
        var filePath = string.Format(title + ".jpg");
        lib.SavePicture(filePath, ms);
        MessageBox.Show("Saved in your media library.", "Done", MessageBoxButton.OK);
      }
      catch (Exception)
      {
        MessageBox.Show(
          "There was an error. Please disconnect your phone from the computer before saving.",
          "Cannot save",
          MessageBoxButton.OK);
      }
    }
  }
}