using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Newport
{
  public class FastObservableCollection<T> : ObservableCollection<T>
  {
    private bool _suppressOnCollectionChanged;

    public FastObservableCollection()
    {
    }

    public FastObservableCollection(IEnumerable<T> items)
    {
      AddRange(items);
    }

    public void Reset(IEnumerable<T> items)
    {
      AddRange(items, true);
    }

    public void AddRange(IEnumerable<T> items)
    {
      AddRange(items, false);
    }

    private void AddRange(IEnumerable<T> items, bool clearFirst)
    {
      if (null == items)
      {
        throw new ArgumentNullException("items");
      }
      var list = items.ToList();
      if (list.Count > 0)
      {
        try
        {
          _suppressOnCollectionChanged = true;
          if (clearFirst)
          {
            Clear();
          }
          list.ForEach(Add);
        }
        finally
        {
          _suppressOnCollectionChanged = false;
          OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
      }
    }

    public void RemoveRange(IEnumerable<T> items)
    {
      if (null == items)
      {
        throw new ArgumentNullException("items");
      }
      var list = items.ToList();
      if (list.Count > 0)
      {
        try
        {
          _suppressOnCollectionChanged = true;
          list.ForEach(i => Remove(i));
        }
        finally
        {
          _suppressOnCollectionChanged = false;
          OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
      }
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      if (!_suppressOnCollectionChanged)
      {
        base.OnCollectionChanged(e);
      }
    }
  }
}
