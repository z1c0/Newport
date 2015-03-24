using System;
#if UNIVERSAL
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace Newport
{
  public class IsNullToVisibilityConverter : BaseConverter
  {
    protected override object OnConvert(object value)
    {
      return (value != null) ? Visibility.Visible : Visibility.Collapsed;
    }

    protected override object OnConvertBack(object value)
    {
      throw new NotImplementedException();
    }
  }
}