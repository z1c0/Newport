using System;
#if UNIVERSAL
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
#else
using System.Windows;
using System.Windows.Markup;
using System.Windows.Controls;
using System.Windows.Media;
#endif

namespace Newport
{
#if UNIVERSAL
  [ContentProperty(Name = "Child")]
#else
  [ContentProperty("Child")]
#endif
  public class CircularFrame : TemplatedControl
  {
    private EllipseGeometry _ellipseGeometry;

    public CircularFrame()
    {
      DefaultStyleKey = typeof(CircularFrame);
    }

    protected override void OnFromTemplate()
    {
      var presenter = VerifyGetTemplateChild<ContentPresenter>("ContentPresenter");
      presenter.Content = Child;
      _ellipseGeometry = VerifyGetTemplateChild<EllipseGeometry>("EllipseGeometry");
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      _ellipseGeometry.RadiusX = finalSize.Width / 2;
      _ellipseGeometry.RadiusY = finalSize.Height / 2;
      _ellipseGeometry.Center = new Point(finalSize.Width / 2, finalSize.Height / 2);
      return base.ArrangeOverride(finalSize);
    }

    public UIElement Child { get; set; }
  }
}