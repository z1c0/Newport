using System;
#if UNIVERSAL
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
#else
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
#endif

namespace Newport
{
  public class CircularFrame : TemplatedControl
  {
    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source",
      typeof(Uri), typeof(CircularFrame), new PropertyMetadata(null));

    public static readonly DependencyProperty FrameThicknessProperty = DependencyProperty.Register("FrameThickness",
      typeof(double), typeof(CircularFrame), new PropertyMetadata(0.0));

    public static readonly DependencyProperty FrameBrushProperty = DependencyProperty.Register("FrameBrush",
      typeof(Brush), typeof(CircularFrame), new PropertyMetadata(null));

    public CircularFrame()
    {
      DefaultStyleKey = typeof(CircularFrame);
    }


    public Uri Source
    {
      get
      {
        return (Uri)GetValue(SourceProperty);
      }
      set
      {
        SetValue(SourceProperty, value);
      }
    }

    public double FrameThickness
    {
      get
      {
        return (double)GetValue(FrameThicknessProperty);
      }
      set
      {
        SetValue(FrameThicknessProperty, value);
      }
    }

    public Brush FrameBrush
    {
      get
      {
        return (Brush)GetValue(FrameBrushProperty);
      }
      set
      {
        SetValue(FrameBrushProperty, value);
      }
    }

    protected override void OnFromTemplate()
    {
      var ellipse = VerifyGetTemplateChild<Ellipse>("Ellipse");
      ellipse.StrokeThickness = FrameThickness;
      ellipse.Stroke = FrameBrush;
      var imageBrush = VerifyGetTemplateChild<ImageBrush>("ImageBrush");
      imageBrush.ImageSource = new BitmapImage { UriSource = Source };
    }
  }
}