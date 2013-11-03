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
#endif

namespace Newport
{
#if !NETFX_CORE
  class CallerMemberNameAttribute : Attribute
  {
  }
#endif

  public class ViewModelBase : DependencyObject, INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    private LicenseInformation _licenseInformation;
    private bool _isBusy;

    public ViewModelBase()
    {
#if NETFX_CORE
      _licenseInformation = CurrentApp.LicenseInformation;
#else
      _licenseInformation = new LicenseInformation();
#endif
      Random = new RandomData();
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

    public RandomData Random { get; private set; }

    public bool IsTrial
    {
      get
      {
#if NETFX_CORE
        return IsDebug ? true : _licenseInformation.IsTrial;
#else
        return IsDebug ? true : _licenseInformation.IsTrial();
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
        _isBusy = value;
        OnPropertyChanged("IsBusy");
        CommandManager.InvalidateRequerySuggested();
      }
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      var handler = PropertyChanged;
      if (handler != null)
      {
        handler(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    protected void SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
    {
      if (!object.Equals(storage, value))
      {
        storage = value;
        OnPropertyChanged(propertyName);
      }
    }
  }
}