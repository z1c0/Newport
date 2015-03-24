using System.Collections.Generic;
using System.Linq;

namespace Newport.Demo.Universal.ViewModels
{
  public class CellViewModel : SimpleViewModelBase
  {
    private bool _isChecked;

    public CellViewModel()
    {
      TapCommand = new ActionCommand(_ => IsChecked = true);
    }

    public bool IsChecked
    {
      get
      {
        return _isChecked;
      }
      set
      {
        SetProperty(ref _isChecked, value);
      }
    }

    public ActionCommand TapCommand { get; private set; }
  }

  [ExportedViewModel]
  public class UniformGridViewModel : ViewModelBase
  {
    public UniformGridViewModel()
    {
      Text = "Uniform Grid";
      Rows = Cols = 8;
      Items = Enumerable.Range(0, Rows * Cols).Select(_ => new CellViewModel()).ToList();
    }

    public int Cols { get; private set; }

    public int Rows { get; private set; }

    public List<CellViewModel> Items { get; private set; }
  }
}
