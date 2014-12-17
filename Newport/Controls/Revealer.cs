using System;
#if UNIVERSAL
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
#endif

namespace Newport
{
#if UNIVERSAL
  [ContentProperty(Name = "Content")]
#else
  [ContentProperty("Content")]
#endif
  public class Revealer : TemplatedControl
  {
    private byte[,] _alphaMap;
    private BitmapBuffer _bitmap;
    private ContentPresenter _contentPresenter;
    private Image _image;
    private Rectangle _rectangle;

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

    protected override void OnFromTemplate()
    {
      _contentPresenter = VerifyGetTemplateChild<ContentPresenter>("ContentPresenter");
      _contentPresenter.Content = Content;
      _image = VerifyGetTemplateChild<Image>("Image");
      _rectangle = VerifyGetTemplateChild<Rectangle>("Rectangle");
#if UNIVERSAL
      _rectangle.PointerMoved += (sender, args) => Reveal(args.GetCurrentPoint(_rectangle).Position);
#else
      _rectangle.MouseMove += (sender, args) => Reveal(args.GetPosition(_rectangle));
#endif

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
      if (_bitmap == null || _bitmap.Width != w || _bitmap.Height != h)
      {
        _bitmap = new BitmapBuffer(w, h);
        _rectangle.Width = w;
        _rectangle.Height = h;
        _rectangle.Fill = CoverBrush;
#if UNIVERSAL
        _bitmap.Render(_rectangle);
#else
        var r = new Rectangle()
        {
          Width = w,
          Height = h,
          Fill = CoverBrush
        };
        _bitmap.Render(r);
#endif
        _bitmap.Invalidate();
        _image.Width = w;
        _image.Height = h;
        _image.Source = _bitmap.ImageSource;
      }
      return base.ArrangeOverride(finalSize);
    }

    private void Reveal(Point point)
    {
      _rectangle.Opacity = 0.0;

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
      var right = (int)Math.Min(_bitmap.Width, point.X + Radius);
      var bottom = (int)Math.Min(_bitmap.Height, point.Y + Radius);

      for (var y = top; y < bottom; y++)
      {
        for (var x = left; x < right; x++)
        {
          var a = _alphaMap[x - left + mapOffsetX, y - top + mapOffsetY];
          if (a != 255)
          {
            var c = _bitmap.GetPixel(x, y);
            _bitmap.SetPixel(x, y, a, c);
          }
        }
      }
      _bitmap.Invalidate();
    }

    public double Radius { get; set; }
  }
}
