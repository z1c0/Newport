using System;

namespace Newport
{
  public class EnumDescriptionAttribute : Attribute
  {
    public EnumDescriptionAttribute(string text)
    {
      Text = text;
    }

    public string Text { get; private set; }
  }

  public class EnumerationViewModel<T> : ViewModelBase where T : struct
  {
    private readonly EnumerationListViewModel<T> _parent;
    private T _value;
    private bool _isChecked;

    internal EnumerationViewModel(EnumerationListViewModel<T> parent, T value)
    {
      _parent = parent;
      _value = value;
    }

    public string ShortText
    {
      get
      {
        return _value.ToString();
      }
    }

    public string FullText
    {
      get
      {
        return ToString();
      }
    }

    public override string ToString()
    {
      var s = _value.ToString();
#if UNIVERSAL
      // TODO
#else
      var memberInfo = typeof(T).GetMember(s);
      if ((memberInfo != null) && (memberInfo.Length > 0))
      {
        var attrs = memberInfo[0].GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
        if ((attrs != null) && (attrs.Length > 0))
        {
          s = ((EnumDescriptionAttribute)attrs[0]).Text;
        }
      }
#endif
      return s;
    }

    public bool IsChecked
    {
      get
      {
        return _isChecked;
      }
      set
      {
        _isChecked = value;
        if (_isChecked)
        {
          _parent.SelectedValue = _value;
        }
        SetProperty(ref _isChecked, value, "IsChecked");
      }
    }
  }
}