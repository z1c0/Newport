using System;
#if UNIVERSAL
using Windows.UI.Xaml;
#else
using System.Windows;
using Microsoft.Phone.Shell;
#endif

namespace Newport
{
  public class ApplicationSettingsManager
  {
    public event EventHandler BeforeSave;

    private IApplicationSettings _settings;

    internal ApplicationSettingsManager()
    {
      if (!ViewModelBase.IsDesignMode)
      {
#if UNIVERSAL
        Application.Current.Suspending += (_, __) => PersistSettings();
#else
        var s = PhoneApplicationService.Current;
        s.Deactivated += (_, __) => PersistSettings();
        s.Closing += (_, __) => PersistSettings();
#endif
      }
    }

    public void PersistSettings()
    {
      if (_settings != null)
      {
        if (BeforeSave != null)
        {
          BeforeSave(this, EventArgs.Empty);
        }
        SettingsStorageHelper.Save(_settings);
      }
    }

    public T GetSettings<T>() where T : class, IApplicationSettings, new()
    {
      // We already have an instance of the settings object?
      if (_settings as T == null)
      {
        // Try to load from isolated storage.
        _settings = SettingsStorageHelper.Load<T>();
        if (_settings == null)
        {
          // Init with default values.
          _settings = new T();
          _settings.Init();
        }
      }
      return (T)_settings;
    }
  }
}