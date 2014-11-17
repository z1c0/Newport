using System.Collections.Generic;
using System.Windows.Input;

namespace Newport
{
  public class TileMenuItemViewModel : ViewModelBase
  {

    public ICommand Command { get; set; }

    public IEnumerable<TileMenuItemViewModel> Items { get; set; }
  }
}