#if UNIVERSAL
using Windows.UI.Xaml.Controls;
using Windows.Foundation;
using Windows.UI.Xaml;
#else
using System.Windows;
using System.Windows.Controls;
#endif

namespace Newport
{
  public class UniformGrid : Panel
  {
    #region ColumnCount (Dependency Property)

    public static readonly DependencyProperty ColumnCountProperty =
      DependencyProperty.Register(
      "ColumnCount",
      typeof(int),
      typeof(UniformGrid),
      new PropertyMetadata(0, OnColumnCountChanged));

    public int ColumnCount
    {
      get { return (int)GetValue(ColumnCountProperty); }
      set { SetValue(ColumnCountProperty, value); }
    }

    private static void OnColumnCountChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      ((UniformGrid)sender).UpdateLayout();
    }

    #endregion ColumnCount (Dependency Property)

    #region RowCount (Dependency Property)

    public static readonly DependencyProperty RowCountProperty =
      DependencyProperty.RegisterAttached(
      "RowCount",
      typeof(int),
      typeof(UniformGrid),
      new PropertyMetadata(0, OnRowCountChanged));

    public int RowCount
    {
      get { return (int)GetValue(RowCountProperty); }
      set { SetValue(RowCountProperty, value); }
    }

    private static void OnRowCountChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      ((UniformGrid)sender).UpdateLayout();
    }

    #endregion RowCount (Dependency Property)

    protected override Size MeasureOverride(Size constraint)
    {
      if (ColumnCount > 0 && RowCount > 0)
      {
        var childConstraint = new Size(constraint.Width / ColumnCount, constraint.Height / RowCount);
        var maxChildDesiredWidth = 0.0;
        var maxChildDesiredHeight = 0.0;
        foreach (var child in Children)
        {
          child.Measure(childConstraint);
          var childDesiredSize = child.DesiredSize;
          if (maxChildDesiredWidth < childDesiredSize.Width)
          {
            maxChildDesiredWidth = childDesiredSize.Width;
          }

          if (maxChildDesiredHeight < childDesiredSize.Height)
          {
            maxChildDesiredHeight = childDesiredSize.Height;
          }
        }
        constraint = new Size((maxChildDesiredWidth * ColumnCount), (maxChildDesiredHeight * RowCount));
      }
      else
      {
        constraint = new Size(0, 0);
      }
      return constraint;
    }

    protected override Size ArrangeOverride(Size arrangeSize)
    {
      if ((ColumnCount > 0) && (RowCount > 0))
      {
        var childBounds = new Rect(0, 0, arrangeSize.Width / ColumnCount, arrangeSize.Height / RowCount);
        var xStep = childBounds.Width;
        var xBound = arrangeSize.Width - 1.0;

        foreach (var child in Children)
        {
          child.Arrange(childBounds);
          if (child.Visibility != Visibility.Collapsed)
          {
            childBounds.X += xStep;
            if (childBounds.X >= xBound)
            {
              childBounds.Y += childBounds.Height;
              childBounds.X = 0;
            }
          }
        }
      }
      return arrangeSize;
    }
  }
}