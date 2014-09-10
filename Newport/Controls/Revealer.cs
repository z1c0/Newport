using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System;

namespace Newport
{
  [ContentProperty("Content")]
  public class Revealer : Control
  {
    private byte[,] _alphaMap;
    private WriteableBitmap _bitmap;
    private ContentPresenter _contentPresenter;

    public Revealer()
    {
      DefaultStyleKey = typeof(Revealer);
      Radius = 40;
    }

    public object Content { get; set; }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      _contentPresenter = (ContentPresenter)GetTemplateChild("ContentPresenter");
      _contentPresenter.Content = Content;
      var image = (Image)GetTemplateChild("Image");
      //image.Tap += (_, args) => Foo(args.GetPosition(image));
      image.MouseMove += (sender, args) => Foo(args.GetPosition(image));
      _bitmap = new WriteableBitmap((int)Width, (int)Height);

      var rect = CreateARectangleWithRGBrush(_bitmap.PixelWidth, _bitmap.PixelHeight);
      _bitmap.Render(rect, new MatrixTransform());
      _bitmap.Invalidate();

      image.Source = _bitmap;

      var r = (int)Radius;
      _alphaMap = new byte[2 * r, 2 * r];
      for (var x = 0; x < r; x++)
      {
        for (var y = 0; y < r; y++)
        {
          byte alpha = 255;
          var dist = x * x + y * y;
          if (dist < r * r)
          {
            alpha = 0;
          }
          _alphaMap[r + x, r - 1 - y] = alpha;
          _alphaMap[r - 1 - x, r - 1 - y] = alpha;
          _alphaMap[r + x, r + y] = alpha;
          _alphaMap[r - 1 - x, r + y] = alpha;
        }
      }
    }

    private void Foo(Point point)
    {
      var left = (int)Math.Max(0, point.X - Radius);
      var top = (int)Math.Max(0, point.Y - Radius);
      var right = (int)Math.Min(_bitmap.PixelWidth, point.X + Radius);
      var bottom = (int)Math.Min(_bitmap.PixelHeight, point.Y + Radius);

      for (var y = top; y < bottom; y++)
      {
        for (var x = left; x < right; x++)
        {
          var a = _alphaMap[x - left, y - top];
          if (a != 255)
          {
            var i = y * _bitmap.PixelWidth + x;
            var c = ToColor(_bitmap.Pixels[i]);
            SetPixel(_bitmap, i, a, c);
          }
        }
      }
      _bitmap.Invalidate();
    }

    private const float PreMultiplyFactor = 1 / 255f;

    public static void SetPixel(WriteableBitmap bmp, int index, byte a, Color color)
    {
      float ai = a * PreMultiplyFactor;
      bmp.Pixels[index] = (a << 24) | ((byte)(color.R * ai) << 16) | ((byte)(color.G * ai) << 8) | (byte)(color.B * ai);
    }

    private Rectangle CreateARectangleWithRGBrush(double width, double height)

    {

      // Create a Rectangle

      Rectangle rectangle = new Rectangle();

      rectangle.Height = height;

      rectangle.Width = width;



      // Create a radial gradient brush with five stops 

      RadialGradientBrush fiveColorRGB = new RadialGradientBrush();

      fiveColorRGB.GradientOrigin = new Point(0.5, 0.5);

      fiveColorRGB.Center = new Point(0.5, 0.5);



      // Create and add Gradient stops

      GradientStop blueGS = new GradientStop();

      blueGS.Color = Colors.Blue;

      blueGS.Offset = 0.0;

      fiveColorRGB.GradientStops.Add(blueGS);



      GradientStop orangeGS = new GradientStop();

      orangeGS.Color = Colors.Orange;

      orangeGS.Offset = 0.25;

      fiveColorRGB.GradientStops.Add(orangeGS);



      GradientStop yellowGS = new GradientStop();

      yellowGS.Color = Colors.Yellow;

      yellowGS.Offset = 0.50;

      fiveColorRGB.GradientStops.Add(yellowGS);



      GradientStop greenGS = new GradientStop();

      greenGS.Color = Colors.Green;

      greenGS.Offset = 0.75;

      fiveColorRGB.GradientStops.Add(greenGS);



      GradientStop redGS = new GradientStop();

      redGS.Color = Colors.Red;

      redGS.Offset = 1.0;

      fiveColorRGB.GradientStops.Add(redGS);



      // Set Fill property of rectangle

      rectangle.Fill = fiveColorRGB;

      return rectangle;
    }

    public static Color ToColor(int argb)
    {
      return Color.FromArgb((byte)((argb & -16777216) >> 0x18),
                            (byte)((argb & 0xff0000) >> 0x10),
                            (byte)((argb & 0xff00) >> 8),
                            (byte)(argb & 0xff));
    }

    public double Radius { get; set; }
  }
}
