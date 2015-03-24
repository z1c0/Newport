using System;

namespace Newport
{
  public class FormatConverter : BaseConverter
  {
    public FormatConverter()
    {
      Format = "{0}";
    }

    public string Format { get; set; }

    protected override object OnConvert(object value)
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

    protected override object OnConvertBack(object value)
    {
      return value;
    }
  }
}