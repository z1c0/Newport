using System;
using Microsoft.Phone.Shell;

namespace Newport
{
  public class ApplicationSettingsService : PhoneApplicationService
  {
    public event EventHandler BeforeSave;

    private static ApplicationSettingsService _default;
    private IApplicationSettings _settings;
    private IsolatedStorageHelper _isolatedStorage;

    public ApplicationSettingsService()
    {
      _isolatedStorage = new IsolatedStorageHelper();
      if (_default == null)
      {
        _default = this;
      }
      Activated += new EventHandler<ActivatedEventArgs>(HandleActivated);
      Closing += new EventHandler<ClosingEventArgs>(HandleClosing);
      Deactivated += new EventHandler<DeactivatedEventArgs>(HandleDeactivated);
      Launching += new EventHandler<LaunchingEventArgs>(HandleLaunching);
    }

    private void HandleLaunching(object sender, LaunchingEventArgs e)
    {
    }

    private void HandleActivated(object sender, ActivatedEventArgs e)
    {
    }

    private void HandleDeactivated(object sender, DeactivatedEventArgs e)
    {
      PersistSettings();
    }

    private void HandleClosing(object sender, ClosingEventArgs e)
    {
      PersistSettings();
    }

    public void PersistSettings()
    {
      if (_settings != null)
      {
        if (BeforeSave != null)
        {
          BeforeSave(this, EventArgs.Empty);
        }
        _isolatedStorage.Save(_settings);
      }
    }

    public T GetSettings<T>() where T : class, IApplicationSettings, new()
    {
      // We already have an instance of the settings object?
      if (_settings as T == null)
      {
        // Try to load from isolated storage.
        _settings = _isolatedStorage.Load<T>();
        if (_settings == null)
        {
          // Init with default values.
          _settings = new T();
          _settings.Init();
        }
      }
      return (T)_settings;
    }

    public static ApplicationSettingsService Default
    {
      get
      {
        if (_default == null)
        {
          _default = new ApplicationSettingsService();
        }
        return _default;
      }
    }
  }
}