using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Newport
{
  public class CountToVisibilityConverter : IValueConverter
  {
    public bool Invert { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var booleanValue = ((int)value > 0);
      if (Invert)
      {
        booleanValue = !booleanValue;
      }
      return booleanValue ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}