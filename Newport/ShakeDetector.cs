using System;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;

namespace Newport
{
  public class ShakeDetector : IDisposable
  {
    private const double SHAKE_THRESHOLD = 0.7;

    private readonly DispatcherHelper _dispatcherHelper;
    private readonly Accelerometer _sensor;
    private Vector3 _lastReading;
    private int _shakeCount;
    private bool _shaking;
    private DateTime _lastShakeTime;

    private event EventHandler ShakeDetectedHandler;

    public ShakeDetector()
    {
      UseDispatcherForCallBack = true;
      PauseDuration = new TimeSpan(0);
      _dispatcherHelper = new DispatcherHelper();
      _sensor = new Accelerometer();
      if (_sensor.State == SensorState.NotSupported)
      {
        throw new NotSupportedException("Accelerometer not supported on this device");
      }
    }

    public bool UseDispatcherForCallBack { get; set; }

    public SensorState State
    {
      get { return _sensor.State; }
    }

    public TimeSpan PauseDuration { get; set; }

    #region IDisposable Members

    public void Dispose()
    {
      _sensor.Dispose();
    }

    #endregion IDisposable Members

    public event EventHandler ShakeDetected
    {
      add
      {
        ShakeDetectedHandler += value;
        _sensor.CurrentValueChanged += CurrentValueChanged;
      }
      remove
      {
        ShakeDetectedHandler -= value;
        _sensor.CurrentValueChanged -= CurrentValueChanged;
      }
    }

    public void Start()
    {
      _sensor.Start();
    }

    public void Stop()
    {
      _sensor.Stop();
    }

    private void CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
    {
      if (_sensor.State == SensorState.Ready)
      {
        var reading = e.SensorReading.Acceleration;
        try
        {
          if (!_shaking && CheckForShake(_lastReading, reading, SHAKE_THRESHOLD) && _shakeCount >= 1)
          {
            _shaking = true;
            _shakeCount = 0;
            OnShakeDetected();
          }
          else if (CheckForShake(_lastReading, reading, SHAKE_THRESHOLD))
          {
            _shakeCount++;
          }
          else if (!CheckForShake(_lastReading, reading, 0.2))
          {
            _shakeCount = 0;
            _shaking = false;
          }
          _lastReading = reading;
        }
        catch
        {
        }
      }
    }

    private void OnShakeDetected()
    {
      if (DateTime.Now - _lastShakeTime >= PauseDuration)
      {
        _lastShakeTime = DateTime.Now;
        if (ShakeDetectedHandler != null)
        {
          if (UseDispatcherForCallBack)
          {
            _dispatcherHelper.Invoke(() => ShakeDetectedHandler(this, EventArgs.Empty));
          }
          else
          {
            ShakeDetectedHandler(this, EventArgs.Empty);
          }
        }
      }
    }

    private bool CheckForShake(Vector3 last, Vector3 current, double threshold)
    {
      double deltaX = Math.Abs((last.X - current.X));
      double deltaY = Math.Abs((last.Y - current.Y));
      double deltaZ = Math.Abs((last.Z - current.Z));
      return (deltaX > threshold && deltaY > threshold) ||
              (deltaX > threshold && deltaZ > threshold) ||
              (deltaY > threshold && deltaZ > threshold);
    }
  }
}