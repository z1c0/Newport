using System;
#if UNIVERSAL
using Windows.UI.Xaml.Controls;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
#endif

namespace Newport
{
  public class FancyBackground : TemplatedControl
  {
    private Canvas _canvas;
    private bool _isInitialized;

    public FancyBackground()
    {
      DefaultStyleKey = typeof(FancyBackground);
      Loaded += (_, __) => CreateItems();
    }

    protected override void OnFromTemplate()
    {
      _canvas = VerifyGetTemplateChild<Canvas>("canvas");
      _isInitialized = true;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      _canvas.Clip = new RectangleGeometry
      {
        Rect = new Rect(0, 0, finalSize.Width, finalSize.Height)
      };
      return base.ArrangeOverride(finalSize);
    }

    private void CreateItems()
    {
      if (_isInitialized)
      {
        _canvas.Children.Clear();
        const int steps = 10;
        steps.Times(i =>
        {
          var div = ActualWidth / 2;
          CreateTriangle(div * 0, div * 1, i, steps);
          CreateTriangle(div * 1, div * 2, i, steps);
        });
      }
    }

    private void CreateTriangle(double w0, double w1, int i, int steps)
    {
      var stepX = (ActualHeight * 0.1) * (steps / (steps + i));
      var stepY = ActualHeight / steps;
      var xM = RandomData.GetDouble(w0, w1);
      var yM = ActualHeight - i * stepY - RandomData.GetDouble(1.5 * stepY);
      var xTL = xM - (yM + RandomData.GetDouble(-stepX, stepX));
      var xTR = xM + (yM + RandomData.GetDouble(-stepX, stepX));
      var yT = 0;
      var p = new Path();
      p.Data = new PathGeometry
      {
        Figures = new PathFigureCollection()
        {
          new PathFigure
          {
            IsClosed = true,
            StartPoint = new Point(xM, yM),
            Segments = new PathSegmentCollection()
            {
              new LineSegment { Point = new Point(xTL, yT) },
              new LineSegment { Point = new Point(xTR, yT) },
            }
          },
        }
      };
      var l = 1 - (i + 1.0) / steps; // lightness percentage
      l = ExponentialEase(l, 0.40);
      var col = Color.Lerp(Colors.White, l);
      p.Fill = new SolidColorBrush(col);
      p.Stroke = new SolidColorBrush(Color);
      p.StrokeThickness = LineThickness;
      _canvas.Children.Add(p);
    }

    private static double ExponentialEase(double x, double a)
    {
      const double epsilon = 0.00001;
      const double minA = 0.0 + epsilon;
      const double maxA = 1.0 - epsilon;
      a = Math.Max(minA, Math.Min(maxA, a));
      var y = 0.0;
      if (x <= 0.5)
      {
        y = (Math.Pow(2.0 * x, 1 - a)) / 2.0;
      }
      else
      {
        y = 1.0 - (Math.Pow(2.0 * (1.0 - x), 1 - a)) / 2.0;
      }
      return y;
    }

    public static readonly DependencyProperty ColorProperty =
      DependencyProperty.Register(
      "Color",
      typeof(Color),
      typeof(FancyBackground),
      new PropertyMetadata(Colors.Green, OnColorChanged));

    public Color Color
    {
      get { return (Color)GetValue(ColorProperty); }
      set { SetValue(ColorProperty, value); }
    }

    private static void OnColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      ((FancyBackground)sender).CreateItems();
    }

    public static readonly DependencyProperty LineThicknessProperty =
    DependencyProperty.Register(
    "LineThickness",
    typeof(double),
    typeof(FancyBackground),
    new PropertyMetadata(0.0, OnLineThicknessChanged));

    public double LineThickness
    {
      get { return (double)GetValue(LineThicknessProperty); }
      set { SetValue(LineThicknessProperty, value); }
    }

    private static void OnLineThicknessChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      ((FancyBackground)sender).CreateItems();
    }
  }
}
