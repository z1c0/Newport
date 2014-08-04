using System;

namespace Newport
{
  public class EventArgs<T> : EventArgs
  {
    public EventArgs(T result)
    {
      Result = result;
    }

    public T Result { get; private set; }

    public static implicit operator EventArgs<T>(T t)
    {
      return new EventArgs<T>(t);
    }
  }
}