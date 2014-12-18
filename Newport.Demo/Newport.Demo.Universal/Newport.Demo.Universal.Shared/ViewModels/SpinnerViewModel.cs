namespace Newport.Demo.Universal.ViewModels
{
  [ExportedViewModel]
  public class SpinnerViewModel : ViewModelBase
  {
    public SpinnerViewModel()
    {
      Text = "Spinner";
      IsBusy = true;
    }
  }
}
