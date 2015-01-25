using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace Newport.Demo.Universal.ViewModels
{
  [ExportedViewModel]
  public class SpinnerViewModel : ViewModelBase
  {
    public SpinnerViewModel()
    {
      Text = "Spinner";
      StartBusyCommand = new ActionCommand(async _ =>
      {
        using (BusyScope())
        {
          await Task.Delay(2000);
        }
      }, _ => !IsBusy);
      Colors = new ColorThemeViewModel();
    }

    public ColorThemeViewModel Colors { get; private set; }

    public ActionCommand StartBusyCommand { get; private set; }
  }
}
