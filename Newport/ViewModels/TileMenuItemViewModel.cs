using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Newport
{
  public class TileMenuItemViewModel : ViewModelBase
  {
    public string Text { get; set; }

    public ICommand Command { get; set; }

    public IEnumerable<TileMenuItemViewModel> Items { get; set; }
  }
}