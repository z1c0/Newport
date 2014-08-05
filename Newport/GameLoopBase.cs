using System;
#if NETFX_CORE
using Windows.UI.Xaml.Media;
#else
using System.Windows.Media;
#endif

namespace Newport
{
  public class GameLoopBase
  {
    public delegate void UpdateHandler(object sender, TimeSpan elapsed);

    public event UpdateHandler Update;

    private DateTime _lastTick;
    private bool _isStarted;

    public GameLoopBase()
    {
      Frames = 25;
    }

    public int Frames { get; set; }

    public virtual void OnTick(TimeSpan elapsed)
    {
      if ((_isStarted) && (Update != null))
      {
        Update(this, elapsed);
      }
    }

    public virtual void Start()
    {
      if (!_isStarted)
      {
        CompositionTarget.Rendering += HandleCompositionTargetRendering;
        _lastTick = DateTime.Now;
        _isStarted = true;
      }
    }

    public virtual void Stop()
    {
      CompositionTarget.Rendering -= HandleCompositionTargetRendering;
      _isStarted = false;
    }

    private void HandleCompositionTargetRendering(object sender, object e)
    {
      var now = DateTime.Now;
      var elapsed = now - _lastTick;
      if (elapsed.Milliseconds >= 1000 / Frames)
      {
        _lastTick = now;
        OnTick(elapsed);
      }
    }
  }
}