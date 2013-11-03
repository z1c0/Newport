using System;
using System.Globalization;
using System.Windows.Data;

namespace Newport
{
  public class FormatConverter : IValueConverter
  {
    public FormatConverter()
    {
      Format = "{0}";
    }

    public string Format { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      string s = null;
      if (value is double)
      {
        var d = (double)value;
        s = string.Format(Format, d);
      }
      else if (value is int)
      {
        var i = (int)value;
        s = string.Format(Format, i);
      }
      else if (value is DateTime)
      {
        var dt = (DateTime)value;
        s = string.Format(Format, dt);
      }
      return s;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return value;
    }
  }
}