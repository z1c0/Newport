using System;
using System.Collections.Generic;
using System.Windows;
#if UNIVERSAL
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Shapes;
using Windows.Foundation;
#else
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
#endif

namespace Newport
{
  public class ParticleControl : ContentControl
  {
    public event EventHandler Update;

    private readonly Canvas _particleHost;
    private readonly List<Particle> _particles;
    private readonly List<Particle> _deadList;
    private Brush[] _brushes;
    private readonly double _elapsed;
    private bool _areBrushesValid;

    public ParticleControl()
    {
      _particleHost = new Canvas() { UseLayoutRounding = false };
      Content = _particleHost;

      _particles = new List<Particle>();
      _deadList = new List<Particle>();
      _elapsed = 0.1;

      Speed = 20;
      OriginVariance = 5;
      ParticleSize = 12;
      ParticleSizeVariance = 5;
      MaxParticleCount = 100;
      OffsetX = 400;
      OffsetY = 200;
      Life = 10;
      LifeVariance = 10;
      StartColor = Color.FromArgb(0xFF, 0xFF, 0xFF, 0x00);
      EndColor = Color.FromArgb(0x00, 0xFF, 0x00, 0x00);

      CompositionTarget.Rendering += (o, e) => UpdateParticles();
    }

    private void GenerateBrushes(int count)
    {
      _brushes = new Brush[count];
      for (var i = 0; i < count; i++)
      {
        var progress = (1.0 / (double)count) * (double)i;

        if (this.Fuzziness > 0.01)
        {
          // use gradient brushes to create "fuzziness"
#if UNIVERSAL
          var r = new LinearGradientBrush();
#else
          var r = new RadialGradientBrush();
#endif
          var stop1 = new GradientStop();
          stop1.Color = InterpolateColor(this.StartColor, this.EndColor, progress);
          stop1.Offset = 1 - this.Fuzziness;

          var alphaFrom = Color.FromArgb(0x00, StartColor.R, StartColor.G, StartColor.B);
          var alphaTo = Color.FromArgb(0x00, EndColor.R, EndColor.G, EndColor.B);

          var stop2 = new GradientStop();
          stop2.Color = InterpolateColor(alphaFrom, alphaTo, progress);
          stop2.Offset = 1.0;

          r.GradientStops.Add(stop1);
          r.GradientStops.Add(stop2);
          _brushes[i] = r;
        }
        else
        {
          // just solid brushes

          var b = new SolidColorBrush(InterpolateColor(StartColor, EndColor, progress));
          _brushes[i] = b;
        }
      }

      _areBrushesValid = true;
    }

    private void UpdateParticles()
    {
      if (!_areBrushesValid)
      {
        GenerateBrushes(20);
      }
      if (IsRunning)
      {
        if (Update != null)
        {
          // TODO: Make command
          Update(this, EventArgs.Empty);
        }

        //
        // Update exsting particles
        //
        _deadList.Clear();

        foreach (var p in _particles)
        {
          // calculate the "life" of the particle
          p.Life -= p.Decay * _elapsed;

          if (p.Life <= 0.0)
          {
            _deadList.Add(p);
          }
          else
          {
            // update size
            p.Size = p.StartSize * (p.Life / p.StartLife);
            double scale = p.Size / p.StartSize;
            p.Ellipse.Width = p.Size;
            p.Ellipse.Height = p.Size;

            // update position
            p.Position.X = p.Position.X + (p.Velocity.X * _elapsed);
            p.Position.Y = p.Position.Y + (p.Velocity.Y * _elapsed);
            p.Position.Z = p.Position.Z + (p.Velocity.Z * _elapsed);
            var t = (p.Ellipse.RenderTransform as TranslateTransform);
            t.X = p.Position.X;
            t.Y = p.Position.Y;

            // update color/brush
            int colorIndex = (int)((double)_brushes.Length * scale);
            p.Ellipse.Fill = _brushes[colorIndex];
          }
        }
      }

      // create new particles (up to 10 or MaxParticleCount)

      for (int i = 0; i < 10 && this._particles.Count < this.MaxParticleCount; i++)

      //for (int i = 0; this.particles.Count < this.MaxParticleCount; i++)
      {
        // attempt to recycle ellipses if they are in the deadlist
        if (_deadList.Count - 1 >= i)
        {
          SpawnParticle(_deadList[i].Ellipse);
          _deadList[i].Ellipse = null;
        }
        else
        {
          SpawnParticle(null);
        }
      }

      foreach (var p in _deadList)
      {
        if (p.Ellipse != null)
        {
          _particleHost.Children.Remove(p.Ellipse);
        }
        _particles.Remove(p);
      }
    }

    private Color InterpolateColor(Color from, Color to, double progress)
    {
      byte[] finalBytes = { 0, 0, 0, 0 };
      byte[] fromBytes = { from.A, from.R, from.G, from.B };
      byte[] toBytes = { to.A, to.R, to.G, to.B };

      for (int i = 0; i < 4; i++)
      {
        byte fB = fromBytes[i];
        byte tB = toBytes[i];

        double dif = ((double)(fB - tB) * progress + (double)tB);
        dif = Math.Min(255, Math.Max(0, dif));

        finalBytes[i] = (byte)dif;
      }

      return Color.FromArgb(finalBytes[0], finalBytes[1], finalBytes[2], finalBytes[3]);
    }

    private void SpawnParticle(Ellipse e)
    {
      double x = RandomWithVariance(OffsetX, OriginVariance);
      double y = RandomWithVariance(OffsetY, OriginVariance);
      double z = 10 * (RandomData.GetDouble() * OriginVariance);
      double life = RandomWithVariance(this.Life, this.LifeVariance);
      double size = RandomWithVariance(this.ParticleSize, this.ParticleSizeVariance);

      var p = new Particle();
      p.Position = new Point3D(x, y, z);
      p.StartLife = life;
      p.Life = life;
      p.StartSize = size;
      p.Size = size;
      p.Decay = 1.0;

      TranslateTransform t;

      if (e != null)
      {
        //e.Fill = _brushes[0];
        e.Width = e.Height = size;
        p.Ellipse = e;
        t = e.RenderTransform as TranslateTransform;
      }
      else
      {
        p.Ellipse = new Ellipse();
        //p.Ellipse.Fill = brushes[0];
        p.Ellipse.Width = p.Ellipse.Height = size;
        _particleHost.Children.Add(p.Ellipse);

        t = new TranslateTransform();
        p.Ellipse.RenderTransform = t;
        p.Ellipse.RenderTransformOrigin = new Point(0.5, 0.5);
      }

      t.X = p.Position.X;
      t.Y = p.Position.Y;

      var velocityMultiplier = (RandomData.GetDouble() + 0.25) * Speed;
      var vX = (1.0 - (RandomData.GetDouble() * 2.0)) * velocityMultiplier;
      var vY = (1.0 - (RandomData.GetDouble() * 2.0)) * velocityMultiplier;

      // TODO: OnNewParticleCommand
      vY = Math.Abs(vY);

      p.Velocity = new Point3D(vX, vY, 0);

      _particles.Add(p);
    }

    private double RandomWithVariance(double midvalue, double variance)
    {
      double min = Math.Max(midvalue - (variance / 2), 0);
      double max = midvalue + (variance / 2);
      double value = min + ((max - min) * RandomData.GetDouble());
      return value;
    }

    #region IsRunning (DependencyProperty)

    public bool IsRunning
    {
      get { return (bool)GetValue(IsRunningProperty); }
      set { SetValue(IsRunningProperty, value); }
    }

    public static readonly DependencyProperty IsRunningProperty =
        DependencyProperty.Register("IsRunning", typeof(bool), typeof(ParticleControl), new PropertyMetadata(false, OnIsRunningChanged));

    private static void OnIsRunningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var particleControl = (ParticleControl)d;
      if ((bool)e.NewValue == false)
      {
        particleControl._particles.Clear();
        particleControl._deadList.Clear();
        particleControl._particleHost.Children.Clear();
      }
    }

    #endregion IsRunning (DependencyProperty)

    #region StartColor (DependencyProperty)

    public Color StartColor
    {
      get { return (Color)GetValue(StartColorProperty); }
      set { SetValue(StartColorProperty, value); }
    }

    public static readonly DependencyProperty StartColorProperty =
        DependencyProperty.Register("StartColor", typeof(Color), typeof(ParticleControl), new PropertyMetadata(new PropertyChangedCallback(OnStartColorChanged)));

    private static void OnStartColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((ParticleControl)d).OnStartColorChanged(e);
    }

    protected virtual void OnStartColorChanged(DependencyPropertyChangedEventArgs e)
    {
      this._areBrushesValid = false;
    }

    #endregion StartColor (DependencyProperty)

    #region EndColor (DependencyProperty)

    /// <summary>
    /// A description of the property.
    /// </summary>
    public Color EndColor
    {
      get { return (Color)GetValue(EndColorProperty); }
      set { SetValue(EndColorProperty, value); }
    }

    public static readonly DependencyProperty EndColorProperty =
        DependencyProperty.Register("EndColor", typeof(Color), typeof(ParticleControl), new PropertyMetadata(new PropertyChangedCallback(OnEndColorChanged)));

    private static void OnEndColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((ParticleControl)d).OnEndColorChanged(e);
    }

    protected virtual void OnEndColorChanged(DependencyPropertyChangedEventArgs e)
    {
      this._areBrushesValid = false;
    }

    #endregion EndColor (DependencyProperty)

    #region MaxParticleCount (DependencyProperty)

    public int MaxParticleCount
    {
      get { return (int)GetValue(MaxParticleCountProperty); }
      set { SetValue(MaxParticleCountProperty, value); }
    }

    public static readonly DependencyProperty MaxParticleCountProperty =
        DependencyProperty.Register("MaxParticleCount", typeof(int), typeof(ParticleControl), null);

    #endregion MaxParticleCount (DependencyProperty)

    #region ParticleSize (DependencyProperty)

    public double ParticleSize
    {
      get { return (double)GetValue(ParticleSizeProperty); }
      set { SetValue(ParticleSizeProperty, value); }
    }

    public static readonly DependencyProperty ParticleSizeProperty =
        DependencyProperty.Register("ParticleSize", typeof(double), typeof(ParticleControl), null);

    #endregion ParticleSize (DependencyProperty)

    #region ParticleSizeVariance (DependencyProperty)

    public double ParticleSizeVariance
    {
      get { return (double)GetValue(ParticleSizeVarianceProperty); }
      set { SetValue(ParticleSizeVarianceProperty, value); }
    }

    public static readonly DependencyProperty ParticleSizeVarianceProperty =
        DependencyProperty.Register("ParticleSizeVariance", typeof(double), typeof(ParticleControl), null);

    #endregion ParticleSizeVariance (DependencyProperty)

    #region OffsetX (DependencyProperty)

    /// <summary>
    /// A description of the property.
    /// </summary>
    public double OffsetX
    {
      get { return (double)GetValue(OffsetXProperty); }
      set { SetValue(OffsetXProperty, value); }
    }

    public static readonly DependencyProperty OffsetXProperty =
        DependencyProperty.Register("OffsetX", typeof(double), typeof(ParticleControl), null);

    #endregion OffsetX (DependencyProperty)

    #region OffsetY (DependencyProperty)

    /// <summary>
    /// A description of the property.
    /// </summary>
    public double OffsetY
    {
      get { return (double)GetValue(OffsetYProperty); }
      set { SetValue(OffsetYProperty, value); }
    }

    public static readonly DependencyProperty OffsetYProperty =
        DependencyProperty.Register("OffsetY", typeof(double), typeof(ParticleControl), null);

    #endregion OffsetY (DependencyProperty)

    #region OriginVariance (DependencyProperty)

    /// <summary>
    /// A description of the property.
    /// </summary>
    public double OriginVariance
    {
      get { return (double)GetValue(OriginVarianceProperty); }
      set { SetValue(OriginVarianceProperty, value); }
    }

    public static readonly DependencyProperty OriginVarianceProperty =
        DependencyProperty.Register("OriginVariance", typeof(double), typeof(ParticleControl), null);

    #endregion OriginVariance (DependencyProperty)

    #region Speed (DependencyProperty)

    public double Speed
    {
      get { return (double)GetValue(SpeedProperty); }
      set { SetValue(SpeedProperty, value); }
    }

    public static readonly DependencyProperty SpeedProperty =
        DependencyProperty.Register("Speed", typeof(double), typeof(ParticleControl), null);

    #endregion Speed (DependencyProperty)

    #region Life (DependencyProperty)

    /// <summary>
    /// A description of the property.
    /// </summary>
    public double Life
    {
      get { return (double)GetValue(LifeProperty); }
      set { SetValue(LifeProperty, value); }
    }

    public static readonly DependencyProperty LifeProperty =
        DependencyProperty.Register("Life", typeof(double), typeof(ParticleControl), null);

    #endregion Life (DependencyProperty)

    #region LifeVariance (DependencyProperty)

    public double LifeVariance
    {
      get { return (double)GetValue(LifeVarianceProperty); }
      set { SetValue(LifeVarianceProperty, value); }
    }

    public static readonly DependencyProperty LifeVarianceProperty =
        DependencyProperty.Register("LifeVariance", typeof(double), typeof(ParticleControl), null);

    #endregion LifeVariance (DependencyProperty)

    #region Fuzziness (DependencyProperty)

    /// <summary>
    /// A number between 0 and 1 that indicates how fuzzy the particles will appear.
    /// </summary>
    public double Fuzziness
    {
      get { return (double)GetValue(FuzzinessProperty); }
      set { SetValue(FuzzinessProperty, value); }
    }

    public static readonly DependencyProperty FuzzinessProperty =
        DependencyProperty.Register("Fuzziness", typeof(double), typeof(ParticleControl), new PropertyMetadata(1.0, OnFuzzinessChanged));

    private static void OnFuzzinessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((ParticleControl)d).OnFuzzinessChanged(e);
    }

    protected virtual void OnFuzzinessChanged(DependencyPropertyChangedEventArgs e)
    {
      _areBrushesValid = false;
    }

    #endregion Fuzziness (DependencyProperty)

    private class Particle
    {
      public Point3D Position;
      public Point3D Velocity;
      public double StartLife;
      public double Life;
      public double Decay;
      public double StartSize;
      public double Size;
      public Ellipse Ellipse;
    }
  }

  internal class Point3D
  {
    public Point3D(double x, double y, double z)
    {
      X = x;
      Y = y;
      Z = z;
    }

    public double X, Y, Z;
  }
}