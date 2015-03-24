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
  public class CountToVisibilityConverter : BaseConverter
  {
    public bool Invert { get; set; }

    protected override object OnConvert(object value)
    {
      var booleanValue = ((int)value > 0);
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