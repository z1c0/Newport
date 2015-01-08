namespace Newport
{
  public static class Get
  {
    static Get()
    {
      ApplicationSettingsManager = new ApplicationSettingsManager();
      ViewModelProvider = new ViewModelProvider();
    }

    public static ApplicationSettingsManager ApplicationSettingsManager { get; private set; }

    public static ViewModelProvider ViewModelProvider { get; private set; }
  }
}