using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Newport
{
  public class SoundPlayer : SoundPlayerBase
  {
    private Dictionary<string, SoundEffectInstance> _dict;

    public SoundPlayer(Func<bool> funcIsSoundOn)
      : base(funcIsSoundOn)
    {
      _dict = new Dictionary<string, SoundEffectInstance>();
    }

    public SoundHandle Register(string resource, bool isLoop = false)
    {
      return Register(Guid.NewGuid().ToString(), resource, isLoop);
    }

    public SoundHandle Register(string key, string resource, bool isLoop = false)
    {
      SoundHandle soundHandle = null;
      if (!DesignerProperties.IsInDesignTool)
      {
        var stream = TitleContainer.OpenStream(resource);
        FrameworkDispatcher.Update();
        var s = SoundEffect.FromStream(stream).CreateInstance();
        s.IsLooped = isLoop;
        _dict[key] = s;
        soundHandle = new SoundHandle(this, key);
      }
      return soundHandle;
    }

    public void Play(string key)
    {
      if (IsSoundOn)
      {
        if (_dict.ContainsKey(key))
        {
          _dict[key].Play();
        }
      }
    }

    public void Stop(string key)
    {
      if (_dict.ContainsKey(key))
      {
        _dict[key].Stop();
      }
    }

    public void PlayRandomSong()
    {
      var ml = new MediaLibrary();
      var count = ml.Songs.Count;
      if (count > 0)
      {
        if (MediaPlayer.State == MediaState.Playing)
        {
          MediaPlayer.Stop();
        }
        MediaPlayer.Play(ml.Songs[new Random().Next(count)]);
      }
    }
  }
}