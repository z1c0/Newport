using System;

namespace Newport
{
  public class BusyScope : IDisposable
  {
    private readonly ViewModelBase _viewModel;

    public BusyScope(ViewModelBase viewModel)
    {
      _viewModel = viewModel;
      _viewModel.IsBusy = true;

    }
    public void Dispose()
    {
      _viewModel.IsBusy = false;
    }
  }

  public class CountedBusyScope : IDisposable
  {
    private readonly BusyViewModel _viewModel;

    public CountedBusyScope(BusyViewModel viewModel)
    {
      _viewModel = viewModel;
      _viewModel.Count++;

    }
    public void Dispose()
    {
      _viewModel.Count--;
    }
  }
}
