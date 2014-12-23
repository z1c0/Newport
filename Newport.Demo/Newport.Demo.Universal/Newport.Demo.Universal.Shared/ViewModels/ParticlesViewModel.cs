using System;
using Windows.UI.Xaml;

namespace Newport.Demo.Universal.ViewModels
{
  [ExportedViewModel]
  public class ParticlesViewModel : ViewModelBase
  {
    private double _offsetX;
    private double _offsetY;

    public ParticlesViewModel()
    {
      Text = "Particles";

      OffsetY = 10;

      var timer = new DispatcherTimer();
      timer.Interval = TimeSpan.FromMilliseconds(100);
      timer.Tick += Timer_Tick;
      timer.Start();
    }

    private void Timer_Tick(object sender, object e)
    {
      OffsetX = RandomData.GetDouble(500);
    }

    public double OffsetX
    {
      get
      {
        return _offsetX;
      }
      set
      {
        SetProperty(ref _offsetX, value);
      }
    }

    public double OffsetY
    {
      get
      {
        return _offsetY;
      }
      set
      {
        SetProperty(ref _offsetY, value);
      }
    }
  }
}
