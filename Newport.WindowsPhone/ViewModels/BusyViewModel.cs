namespace Newport
{
  public class BusyViewModel : ViewModelBase
  {
    private int _count;

    public int Count
    {
      get
      {
        return _count;
      }
      set
      {
        _count = value;
        IsBusy = (_count > 0);
      }
    }
  }
}
