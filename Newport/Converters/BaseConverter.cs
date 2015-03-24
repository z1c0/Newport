using System;
using System.Globalization;
#if UNIVERSAL
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;
#else
using System.Windows;
using System.Windows.Data;
#endif

namespace Newport
{
  public abstract class BaseConverter : DependencyObject, IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return OnConvert(value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return OnConvertBack(value);
    }

    public object Convert(object value, Type targetType, object parameter, string culture)
    {
      return OnConvert(value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string culture)
    {
      return OnConvertBack(value);
    }

    protected abstract object OnConvert(object value);

    protected abstract object OnConvertBack(object value);
  }
}