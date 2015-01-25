using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Newport
{
  public class ObservableGroup<T> : ObservableCollection<T>
  {
    public ObservableGroup(string key)
    {
      Key = key;
    }

    public string Key { get; set; }

    public bool HasItems { get { return Count > 0; } }
  }

  public class GroupedObservableList<T> : IEnumerable<ObservableGroup<T>>
  {
    private readonly ObservableCollection<ObservableGroup<T>> _groupList;
    private readonly Dictionary<string, ObservableGroup<T>> _groups;

    public GroupedObservableList()
    {
      _groupList = new ObservableCollection<ObservableGroup<T>>();
      _groups = new Dictionary<string, ObservableGroup<T>>();
      foreach (char c in "#abcdefghijklmnopqrstuvwxyz")
      {
        var group = new ObservableGroup<T>(c.ToString());
        _groupList.Add(group);
        _groups[c.ToString()] = group;
      }
    }

    public void Add(T t)
    {
      _groups[GetKey(t)].Add(t);
    }

    public void AddRange(IEnumerable<T> items)
    {
      foreach (var item in items)
      {
        Add(item);
      }
    }

    public void Clear()
    {
      _groups.ForEach(a => a.Value.Clear());
    }

    public void Remove(T t)
    {
      foreach (var e in _groups)
      {
        if (e.Value.Contains(t))
        {
          e.Value.Remove(t);
        }
      }
    }

    private string GetKey(object o)
    {
      var s = o.ToString();
      char key = !string.IsNullOrEmpty(s) ? char.ToLower(s[0]) : '#';
      if ((key < 'a') || (key > 'z'))
      {
        key = '#';
      }
      return key.ToString();
    }

    public List<T> ToFlatList()
    {
      var list = new List<T>();
      foreach (var e in _groups)
      {
        list.AddRange(e.Value);
      }
      return list;
    }

    #region IEnumerable

    public IEnumerator<ObservableGroup<T>> GetEnumerator()
    {
      return _groupList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    #endregion IEnumerable
  }
}