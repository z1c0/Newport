using System;
using System.Globalization;
using System.Windows;
#if NETFX_CORE
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;
#else
using System.Windows.Data;
#endif

namespace Newport
{
  public class BooleanToVisibilityConverter : IValueConverter
  {
    public bool Invert { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return Convert(value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return Convert(value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      throw new NotImplementedException();
    }

    private object Convert(object value)
    {
      var booleanValue = (bool)value;
      if (Invert)
      {
        booleanValue = !booleanValue;
      }
      return booleanValue ? Visibility.Visible : Visibility.Collapsed;
    }
  }
}