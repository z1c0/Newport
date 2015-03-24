namespace Newport
{
  public class BooleanInvertConverter : BaseConverter
  {
    protected override object OnConvert(object value)
    {
      return !(bool)value;
    }

    protected override object OnConvertBack(object value)
    {
      return !(bool)value;
    }
  }
}