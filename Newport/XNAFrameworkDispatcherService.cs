using System;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Xna.Framework;

namespace Newport
{
  public class XNAFrameworkDispatcherService : IApplicationService
  {
    private readonly DispatcherTimer _frameworkDispatcherTimer;

    public XNAFrameworkDispatcherService()
    {
      _frameworkDispatcherTimer = new DispatcherTimer { Interval = TimeSpan.FromTicks(333333) };
      _frameworkDispatcherTimer.Tick += (_, __) => FrameworkDispatcher.Update();
      FrameworkDispatcher.Update();
    }

    public void StartService(ApplicationServiceContext context)
    {
      _frameworkDispatcherTimer.Start();
    }

    public void StopService()
    {
      _frameworkDispatcherTimer.Stop();
    }
  }
}