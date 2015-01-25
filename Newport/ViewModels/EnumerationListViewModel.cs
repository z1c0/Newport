using System;
using System.Collections.Generic;
using System.Linq;

namespace Newport
{
  public class EnumerationListViewModel<T> : ViewModelBase where T : struct
  {
    public event EventHandler SelectedValueChanged;

    private T _selectedValue;

    public IEnumerable<EnumerationViewModel<T>> AvailableValues
    {
      get
      {
        return
          from d in new EnumList<T>()
          select new EnumerationViewModel<T>(this, d) { IsChecked = (d.Equals(SelectedValue)) };
      }
    }

    public T SelectedValue
    {
      get
      {
        return _selectedValue;
      }
      set
      {
        if (!_selectedValue.Equals(value))
        {
          OnPropertyChanged("SelectedValue");
          _selectedValue = value;
          if (SelectedValueChanged != null)
          {
            SelectedValueChanged(this, EventArgs.Empty);
          }
        }
      }
    }
  }
}