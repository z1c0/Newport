#if UNIVERSAL
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#endif

namespace Newport
{
  public class WrapPanel : Panel
  {
    public WrapPanel()
    {
      Orientation = Orientation.Horizontal;
    }

    #region Orientation
    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(WrapPanel), null);

    public Orientation Orientation
    {
      get { return (Orientation)GetValue(OrientationProperty); }
      set { SetValue(OrientationProperty, value); }
    }
    #endregion

    protected override Size MeasureOverride(Size availableSize)
    {
      foreach (var child in Children)
      {
        child.Measure(new Size(availableSize.Width, availableSize.Height));
      }
      return base.MeasureOverride(availableSize);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      var point = new Point(0, 0);
      var i = 0;
      if (Orientation == Orientation.Horizontal)
      {
        var largestHeight = 0.0;

        foreach (var child in Children)
        {
          child.Arrange(new Rect(point, new Point(point.X + child.DesiredSize.Width, point.Y + child.DesiredSize.Height)));
          if (child.DesiredSize.Height > largestHeight)
          {
            largestHeight = child.DesiredSize.Height;
          }
          point.X = point.X + child.DesiredSize.Width;
          if ((i + 1) < Children.Count)
          {
            if ((point.X + Children[i + 1].DesiredSize.Width) > finalSize.Width)
            {
              point.X = 0;
              point.Y = point.Y + largestHeight;
              largestHeight = 0.0;
            }
          }
          i++;
        }
      }
      else
      {
        var largestWidth = 0.0;
        foreach (var child in Children)
        {
          child.Arrange(new Rect(point, new Point(point.X + child.DesiredSize.Width, point.Y + child.DesiredSize.Height)));
          if (child.DesiredSize.Width > largestWidth)
          {
            largestWidth = child.DesiredSize.Width;
          }
          point.Y = point.Y + child.DesiredSize.Height;
          if ((i + 1) < Children.Count)
          {
            if ((point.Y + Children[i + 1].DesiredSize.Height) > finalSize.Height)
            {
              point.Y = 0;
              point.X = point.X + largestWidth;
              largestWidth = 0.0;
            }
          }
          i++;
        }
      }
      return base.ArrangeOverride(finalSize);
    }
  }
}
