namespace Newport
{
  public class MasterViewModel<T> : ViewModelBase
  {
    private T _current;

    public MasterViewModel()
    {
    }

    public MasterViewModel(string name)
    {
      Get.ViewModelProvider.RegisterInstance(name, this);
    }

    public T Current
    {
      get { return _current; }
      set { SetProperty(ref _current, value, "Current"); }
    }
  }
}
