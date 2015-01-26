namespace Newport
{
  public static class Get
  {
    static Get()
    {
      ApplicationSettingsManager = new ApplicationSettingsManager();
      ViewModelProvider = new ViewModelProvider();
      Navigator = new Navigator();
    }

    public static T ViewModel<T>() where T : ViewModelBase, new()
    {
      return ViewModelProvider.Get<T>();
    }

    public static T Settings<T>() where T : class, IApplicationSettings, new()
    {
      return ApplicationSettingsManager.GetSettings<T>();
    }

    public static ViewModelProvider ViewModelProvider { get; private set; }

    public static ApplicationSettingsManager ApplicationSettingsManager { get; private set; }


    public static Navigator Navigator { get; private set; }
  }
}