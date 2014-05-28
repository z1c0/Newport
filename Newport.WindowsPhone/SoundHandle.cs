using System;

namespace Newport
{
  public class SoundHandle
  {
    private SoundPlayer _soundPlayer;
    private string _key;

    internal SoundHandle(SoundPlayer soundPlayer, string key)
    {
      _soundPlayer = soundPlayer;
      _key = key;
    }

    public void Play()
    {
      _soundPlayer.Play(_key);
    }

    public void Stop()
    {
      _soundPlayer.Stop(_key);
    }
  }

  public abstract class SoundPlayerBase
  {
    private Func<bool> _funcIsSoundOn;

    internal SoundPlayerBase(Func<bool> funcIsSoundOn)
    {
      _funcIsSoundOn = funcIsSoundOn;
    }

    public virtual bool IsSoundOn
    {
      get
      {
        bool isSoundOn = true;
        if (_funcIsSoundOn != null)
        {
          isSoundOn = _funcIsSoundOn();
        }
        return isSoundOn;
      }
    }
  }
}
