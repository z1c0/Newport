using System;
using System.Linq;
using System.Collections.Generic;

namespace Newport
{
  public class WeakList<T> : IList<T> where T : class
  {
    private readonly List<WeakReference> _innerList = new List<WeakReference>();

    #region IList<T> Members

    public int IndexOf(T item)
    {
      return _innerList.Select(GetTarget).ToList().IndexOf(item);
    }

    public void Insert(int index, T item)
    {
      _innerList.Insert(index, new WeakReference(item));
    }

    public void RemoveAt(int index)
    {
      _innerList.RemoveAt(index);
    }

    public T this[int index]
    {
      get { return GetTarget(_innerList[index]); }
      set { _innerList[index] = new WeakReference(value); }
    }

    #endregion

    #region ICollection<T> Members

    public void Add(T item)
    {
      _innerList.Add(new WeakReference(item));
    }

    public void Clear()
    {
      _innerList.Clear();
    }

    public bool Contains(T item)
    {
      return _innerList.Any(wr => Equals(GetTarget(wr), item));
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      _innerList.Select(GetTarget).ToArray().CopyTo(array, arrayIndex);
    }

    public int Count
    {
      get { return _innerList.Count; }
    }

    public bool IsReadOnly
    {
      get { return false; }
    }

    public bool Remove(T item)
    {
      var index = IndexOf(item);
      if (index > -1)
      {
        RemoveAt(index);
        return true;
      }
      return false;
    }

    #endregion

    #region IEnumerable<T> Members

    public IEnumerator<T> GetEnumerator()
    {
      return _innerList.Select(GetTarget).GetEnumerator();
    }

    #endregion

    #region IEnumerable Members

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    #endregion

    public void Purge()
    {
      _innerList.Where(wr => !wr.IsAlive).ToList().ForEach(wr => _innerList.Remove(wr));
    }

    private static T GetTarget(WeakReference wr)
    {
      T t = null;
      if (wr.IsAlive)
      {
        t = wr.Target as T;
      }
      return t;
    }
  }
}