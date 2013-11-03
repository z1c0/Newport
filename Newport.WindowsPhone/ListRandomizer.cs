using System;
using System.Collections.Generic;

namespace Newport
{
  public class ListRandomizer<T>
  {
    private readonly Random _random;
    private readonly List<T> _list;

    public ListRandomizer(List<T> list)
    {
      if (list == null)
      {
        throw new ArgumentNullException("list");
      }
      ExcludeResults = true;
      ExcludedValues = new List<T>();
      _random = new Random(Environment.TickCount);
      _list = list;
    }

    public bool ExcludeResults { get; set; }

    public List<T> ExcludedValues { get; private set; }

    public T Next()
    {
      if (_list.Count == ExcludedValues.Count)
      {
        throw new InvalidOperationException("All values are excluded");
      }
      T t;
      do
      {
        t = _list[_random.Next(_list.Count)];
      }
      while (ExcludedValues.Contains(t));
      if (ExcludeResults)
      {
        ExcludedValues.Add(t);
      }
      return t;
    }

    public List<T> NextRange(int count)
    {
      var range = new List<T>();
      count.Times(a => range.Add(Next()));
      return range;
    }
  }
}