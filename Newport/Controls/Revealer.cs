using System.Linq.Expressions;
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
    private Image _image;

    public Revealer()
    {
      DefaultStyleKey = typeof(Revealer);
      Radius = 40;
    }

    public static readonly DependencyProperty CoverBrushProperty = DependencyProperty.Register("CoverBrush",
      typeof(Brush), typeof(Revealer), new PropertyMetadata(new SolidColorBrush(Colors.Magenta)));

    public object Content { get; set; }

    public Brush CoverBrush
    {
      get
      {
        return (Brush)GetValue(CoverBrushProperty);
      }
      set
      {
        SetValue(CoverBrushProperty, value);
      }
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      _contentPresenter = (ContentPresenter)GetTemplateChild("ContentPresenter");
      _contentPresenter.Content = Content;
      _image = (Image)GetTemplateChild("Image");
      //_image.Tap += (_, args) => Foo(args.GetPosition(_image));
      _image.MouseMove += (sender, args) => Foo(args.GetPosition(_image));

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

    protected override Size ArrangeOverride(Size finalSize)
    {
      var w = (int)finalSize.Width;
      var h = (int)finalSize.Height;
      if (_bitmap == null || _bitmap.PixelWidth != w || _bitmap.PixelHeight != h)
      {
        _bitmap = new WriteableBitmap(w, h);
        var rect = new Rectangle
        {
          Width = w,
          Height = h,
          Fill = CoverBrush
        };
        _bitmap.Render(rect, new MatrixTransform());
        _bitmap.Invalidate();
        _image.Source = _bitmap;
      }
      return base.ArrangeOverride(finalSize);
    }

    private void Foo(Point point)
    {
      var mapOffsetX = 0;
      var left = (int)(point.X - Radius);
      if (left < 0)
      {
        mapOffsetX = Math.Abs(left);
        left = 0;
      }
      var mapOffsetY = 0;
      var top = (int)(point.Y - Radius);
      if (top < 0)
      {
        mapOffsetY = Math.Abs(top);
        top = 0;
      }
      var right = (int)Math.Min(_bitmap.PixelWidth, point.X + Radius);
      var bottom = (int)Math.Min(_bitmap.PixelHeight, point.Y + Radius);

      for (var y = top; y < bottom; y++)
      {
        for (var x = left; x < right; x++)
        {
          var a = _alphaMap[x - left + mapOffsetX, y - top + mapOffsetY];
          if (a != 255)
          {
            var i = y * _bitmap.PixelWidth + x;
            var c = _bitmap.Pixels[i].ToColor();
            SetPixel(_bitmap, i, a, c);
          }
        }
      }
      _bitmap.Invalidate();
    }

    private static void SetPixel(WriteableBitmap bmp, int index, byte a, Color color)
    {
      const float preMultiplyFactor = 1 / 255f;
      var ai = a * preMultiplyFactor;
      bmp.Pixels[index] = (a << 24) | ((byte)(color.R * ai) << 16) | ((byte)(color.G * ai) << 8) | (byte)(color.B * ai);
    }
    
    public double Radius { get; set; }
  }
}
