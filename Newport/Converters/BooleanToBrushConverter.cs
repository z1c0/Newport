using System;
#if UNIVERSAL
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
#else
using System.Windows;
using System.Windows.Media;
#endif

namespace Newport
{
  public class BooleanToBrushConverter : BaseConverter
  {
    protected override object OnConvert(object value)
    {
      if (value is bool && (bool)value)
      {
        return TrueBrush;
      }
      return FalseBrush;
    }

    protected override object OnConvertBack(object value)
    {
      throw new NotImplementedException();
    }

    public Brush FalseBrush
    {
      get { return (Brush)GetValue(FalseBrushProperty); }
      set { SetValue(FalseBrushProperty, value); }
    }

    public static readonly DependencyProperty FalseBrushProperty =
      DependencyProperty.Register("FalseBrush", typeof(Brush), typeof(BooleanToBrushConverter), new PropertyMetadata(null));

    public Brush TrueBrush
    {
      get { return (Brush)GetValue(TrueBrushProperty); }
      set { SetValue(TrueBrushProperty, value); }
    }

    public static readonly DependencyProperty TrueBrushProperty =
      DependencyProperty.Register("TrueBrush", typeof(Brush), typeof(BooleanToBrushConverter), new PropertyMetadata(null));
  }
}