using System;
using System.Windows.Input;

namespace Newport.Demo.Universal.ViewModels
{
  [ExportedViewModel]
  public class ParticlesViewModel : ViewModelBase
  {
    public ParticlesViewModel()
    {
      Text = "Particles";
      InitParticleCommand = new GenericActionCommand<Particle>(p =>
      {
        p.Position.X = RandomData.GetDouble(500);
        p.Position.Y = 0;
        p.Velocity.Y = Math.Abs(p.Velocity.Y);
      });
    }

    public ICommand InitParticleCommand { get; private set; }
  }
}
