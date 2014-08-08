using System;
using System.ComponentModel;
#if NETFX_CORE
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

    public ViewModelBase()
    {
#if NETFX_CORE
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

    public bool IsEmulator
    {
      get
      {
#if NETFX_CORE
        throw new NotImplementedException();
#else
        return Microsoft.Devices.Environment.DeviceType == DeviceType.Emulator;
#endif
      }
    }

    public bool IsTrial
    {
      get
      {
#if NETFX_CORE
        return IsDebug ? true : _licenseInformation.IsTrial;
#else
        return IsDebug || _licenseInformation.IsTrial();
#endif
      }
    }

    public static bool IsDesignMode
    {
      get
      {
#if NETFX_CORE
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