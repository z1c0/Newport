using System.ComponentModel;
#if UNIVERSAL
using System;
using Windows.UI.Xaml;
using Windows.ApplicationModel.Store;
using Windows.ApplicationModel;
using System.Runtime.CompilerServices;
#else
using Microsoft.Devices;
using Microsoft.Phone.Marketplace;
using System.Windows;
using System.Runtime.CompilerServices;
#endif

namespace Newport
{
  public class ViewModelBase : DependencyObject, INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    private readonly LicenseInformation _licenseInformation;
    private bool _isBusy;
    private string _text;

    public ViewModelBase()
    {
#if UNIVERSAL
      _licenseInformation = CurrentApp.LicenseInformation;
#else
      _licenseInformation = new LicenseInformation();
#endif
    }

    public bool IsDebug
    {
      get
      {
#if DEBUG
        return true;
#else
        return false;
#endif
      }
    }

    public bool IsTrial
    {
      get
      {
#if UNIVERSAL
        return IsDebug || _licenseInformation.IsTrial;
#else
        return IsDebug || _licenseInformation.IsTrial();
#endif
      }
    }

    public static bool IsDesignMode
    {
      get
      {
#if UNIVERSAL
        return DesignMode.DesignModeEnabled;
#else
        return DesignerProperties.IsInDesignTool;
#endif
      }
    }

    public bool IsBusy
    {
      get
      {
        return _isBusy;
      }
      set
      {
        if (_isBusy != value)
        {
          _isBusy = value;
          OnPropertyChanged("IsBusy");
          CommandManager.InvalidateRequerySuggested();
        }
      }
    }
    public string Text
    {
      get
      {
        return _text;
      }
      set
      {
        SetProperty(ref _text, value, "Text");
      }
    }

    public BusyScope BusyScope()
    {
      return new BusyScope(this);
    }

    public void AllPropertiesChanged()
    {
      OnPropertyChangedHelper(null);
    }

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

    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
      var changed = false;
      if (!Equals(storage, value))
      {
        storage = value;
        OnPropertyChangedHelper(propertyName);
        changed = true;
      }
      return changed;
    }
  }
}