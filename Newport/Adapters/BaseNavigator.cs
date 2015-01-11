using System;
using System.Collections.Generic;

namespace Newport
{
  public abstract class BaseNavigator
  {
    private readonly Dictionary<Type, WeakReference> _dataContextDict;

    internal BaseNavigator()
    {
      _dataContextDict = new Dictionary<Type, WeakReference>();
    }

    internal object GetDataContextFor(Type type)
    {
      WeakReference wr;
      _dataContextDict.TryGetValue(type, out wr);
      return (wr != null && wr.IsAlive) ? wr.Target : null;
    }

    public virtual void Navigate(Type type, object dataContext = null)
    {
      _dataContextDict.AddOrReplace(type, new WeakReference(dataContext));
    }
  }
}