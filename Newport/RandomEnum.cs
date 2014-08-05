namespace Newport
{
  public class RandomEnum<T> : ListRandomizer<T>
  {
    public RandomEnum() :
      base(new EnumList<T>())
    {
    }
  }
}