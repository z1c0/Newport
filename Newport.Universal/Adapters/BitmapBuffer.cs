using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Newport
{
  public class BitmapBuffer
  {
    private readonly WriteableBitmap _writeableBitmap;

    public BitmapBuffer(int width, int height)
    {
      _writeableBitmap = new WriteableBitmap(width, height);
    }

    public async void Render(UIElement e)
    {
      var rtb = new RenderTargetBitmap();
      await rtb.RenderAsync(e, Width, Height);
      var p = await rtb.GetPixelsAsync();
      p.CopyTo(_writeableBitmap.PixelBuffer);
    }

    public void Invalidate()
    {
      _writeableBitmap.Invalidate();
    }

    public int Width
    {
      get { return _writeableBitmap.PixelWidth; }
    }

    public int Height
    {
      get { return _writeableBitmap.PixelHeight; }
    }

    public ImageSource ImageSource { get { return _writeableBitmap; } }

    public Color GetPixel(int x, int y)
    {
      var i = y * _writeableBitmap.PixelWidth + x;
      var s = _writeableBitmap.PixelBuffer.AsStream();
      s.Seek(4 * i, SeekOrigin.Begin);
      var b = (byte)s.ReadByte();
      var g = (byte)s.ReadByte();
      var r = (byte)s.ReadByte();
      var a = (byte)s.ReadByte();
      return Color.FromArgb(a, r, g, b);
    }

    public void SetPixel(int x, int y, byte a, Color color)
    {
      const float preMultiplyFactor = 1 / 255f;
      var ai = a * preMultiplyFactor;
      var i = y * _writeableBitmap.PixelWidth + x;
      var s = _writeableBitmap.PixelBuffer.AsStream();
      s.Seek(4 * i, SeekOrigin.Begin);
      s.WriteByte(color.B);
      s.WriteByte(color.B);
      s.WriteByte(color.B);
      s.WriteByte(a);
    }
  }
}
