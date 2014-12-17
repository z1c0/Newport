using System.Collections.Generic;

namespace Newport.Demo.Universal.ViewModels
{
  [ExportedViewModel]
  public class MainViewModel : ViewModelBase
  {
    private bool? _showMenu;
    
    public MainViewModel()
    {
      Text = "Hello Newport!";
      ShowMenu = true;
      ShowMenuCommand = new ActionCommand(_ => ShowMenu = true);
      MenuItems = new List<TileMenuItemViewModel>
      {
        new TileMenuItemViewModel
        {
          Text = "Content Revealer",
          Command = new NavigationCommand(typeof(RevealerPage))
        }, 
        new TileMenuItemViewModel
        {
          Text = "Particles" ,
          Command = new NavigationCommand(typeof(ParticlesPage))
        },
        new TileMenuItemViewModel { Text = "Spinner" },
        new TileMenuItemViewModel
        {
          Text = "Rating",
          Command = new NavigationCommand(typeof(RatingPage)),
        },
        new TileMenuItemViewModel { Text = "FancyBackground" },
      };
    }

    public ActionCommand ShowMenuCommand { get; private set; }

    public bool? ShowMenu
    {
      get { return _showMenu; }
      set { SetProperty(ref _showMenu, value); }
    }

    public IEnumerable<TileMenuItemViewModel> MenuItems { get; private set; }
  }
}
