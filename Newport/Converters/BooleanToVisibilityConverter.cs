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
  public class BooleanToVisibilityConverter : BaseConverter
  {
    public bool Invert { get; set; }

    protected override object OnConvert(object value)
    {
      var booleanValue = (bool)value;
      if (Invert)
      {
        booleanValue = !booleanValue;
      }
      return booleanValue ? Visibility.Visible : Visibility.Collapsed;
    }

    protected override object OnConvertBack(object value)
    {
      throw new NotImplementedException();
    }
  }
}