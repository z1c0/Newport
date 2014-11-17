using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Newport
{
  public class SimpleViewModelBase : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;


    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      OnPropertyChangedHelper(propertyName);
    }


    private void OnPropertyChangedHelper(string propertyName)
    {
      var handler = PropertyChanged;
      if (handler != null)
      {
        handler(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    protected void SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
      if (!Equals(storage, value))
      {
        storage = value;
        OnPropertyChangedHelper(propertyName);
      }
    }
  }
}