using System.Collections.Generic;

namespace Newport.Demo.WindowsPhone.ViewModels
{
  [ExportedViewModel]
  public class MainViewModel : ViewModelBase
  {
    private bool _showMenu;

    public MainViewModel()
    {
      Text = "Hello Newport!";
      ShowMenu = true;
      MenuItems = new List<TileMenuItemViewModel>
      {
        new TileMenuItemViewModel { Text = "Content Revealer", Command = new NavigationCommand("/RevealerPage.xaml")}, // TODO: typeof(..page)?
        new TileMenuItemViewModel { Text = "Elmo" },
        new TileMenuItemViewModel { Text = "Kermit" },
        new TileMenuItemViewModel { Text = "Fozzy" },
        new TileMenuItemViewModel { Text = "Bert" },
      };
    }

    public bool ShowMenu
    {
      get { return _showMenu; }
      set { SetProperty(ref _showMenu, value); }
    }

    public IEnumerable<TileMenuItemViewModel> MenuItems { get; private set; }
  }
}
