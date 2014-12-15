using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Newport
{
  public static class WriteableBitmapExtensions
  {
    public static void Render(this WriteableBitmap bmp, UIElement e, Transform transform)
    {
      var rtb = new RenderTargetBitmap();
      rtb.RenderAsync(e);
      //bmp.Source = renderTargetBitmap;

    }
  }
}
