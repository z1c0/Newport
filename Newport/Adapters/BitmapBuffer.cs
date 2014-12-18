using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Newport
{
  public class BitmapBuffer
  {
    private readonly WriteableBitmap _bitmap;

    public BitmapBuffer(int width, int height)
    {
      _bitmap = new WriteableBitmap(width, height);
    }

    public void Render(UIElement e)
    {
      _bitmap.Render(e, new MatrixTransform());
    }

    public void Invalidate()
    {
      _bitmap.Invalidate();
    }

    public ImageSource ImageSource { get { return _bitmap; } }


    public Color GetPixel(int x, int y)
    {
      var i = y * _bitmap.PixelWidth + x;
      return _bitmap.Pixels[i].ToColor();
    }

    public void SetPixel(int x, int y, byte a, Color color)
    {
      const float preMultiplyFactor = 1 / 255f;
      var ai = a * preMultiplyFactor;
      var i = y * _bitmap.PixelWidth + x;
      _bitmap.Pixels[i] = (a << 24) | ((byte)(color.R * ai) << 16) | ((byte)(color.G * ai) << 8) | (byte)(color.B * ai);
    }

    public int PixelWidth
    {
      get { return _bitmap.PixelWidth; }
    }

    public int PixelHeight
    {
      get { return _bitmap.PixelHeight; }
    }
  }
}
