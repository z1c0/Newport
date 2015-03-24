using System.Collections.Generic;
using System.Linq;

namespace Newport.Demo.Universal.ViewModels
{
  [ExportedViewModel]
  public class UniformGridViewModel : ViewModelBase
  {
    public UniformGridViewModel()
    {
      Text = "Uniform Grid";
      Rows = Cols = 5;
      Items = Enumerable.Range(0, Rows * Cols).Select(_ => new SimpleViewModelBase()).ToList();
    }

    public int Cols { get; private set; }

    public int Rows { get; private set; }

    public List<SimpleViewModelBase> Items { get; private set; }
  }
}
