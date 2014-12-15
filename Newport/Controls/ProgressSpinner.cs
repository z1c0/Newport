#if UNIVERSAL
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
#else
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
#endif

namespace Newport
{
  public class ProgressSpinner : Control
  {
    private Storyboard _storyBoard;
    private GradientStop _gradientStop1;
    private GradientStop _gradientStop2;
    private GradientStop _gradientStop3;

    public ProgressSpinner()
    {
      DefaultStyleKey = typeof(ProgressSpinner);
    }

#if UNIVERSAL
    protected override void OnApplyTemplate()
#else
    public override void OnApplyTemplate()
#endif
    {
      var viewBox = (Viewbox)GetTemplateChild("viewBox");
      _storyBoard = (Storyboard)viewBox.Resources["storyboard"];
      _gradientStop1 = (GradientStop)GetTemplateChild("stop1");
      _gradientStop2 = (GradientStop)GetTemplateChild("stop2");
      _gradientStop3 = (GradientStop)GetTemplateChild("stop3");

      AdjustGradient();
      StartStopAnimation(IsBusy);

      base.OnApplyTemplate();
    }

    private void StartStopAnimation(bool isBusy)
    {
      if (_storyBoard != null)
      {
        if (isBusy)
        {
          _storyBoard.Begin();
        }
        else
        {
          _storyBoard.Stop();
        }
      }
    }

    private void AdjustGradient()
    {
      if (_gradientStop1 != null)
      {
        var c = Color;
        var ch = Color.FromArgb(128, c.R, c.G, c.B);
        _gradientStop1.Color = ch;
        _gradientStop2.Color = ch;
        _gradientStop3.Color = c;
      }
    }

    public static readonly DependencyProperty IsBusyProperty =
      DependencyProperty.Register(
      "IsBusy",
      typeof(bool),
      typeof(ProgressSpinner),
      new PropertyMetadata(false, OnIsBusyChanged));

    public bool IsBusy
    {
      get { return (bool)GetValue(IsBusyProperty); }
      set { SetValue(IsBusyProperty, value); }
    }

    private static void OnIsBusyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      var spinner = (ProgressSpinner)sender;
      var isBusy = (bool)args.NewValue;
      spinner.Visibility = isBusy ? Visibility.Visible : Visibility.Collapsed;
      spinner.StartStopAnimation(isBusy);
    }

    public static readonly DependencyProperty ColorProperty =
      DependencyProperty.Register(
      "Color",
      typeof(Color),
      typeof(ProgressSpinner),
      new PropertyMetadata(Colors.White, OnColorChanged));

    public Color Color
    {
      get { return (Color)GetValue(ColorProperty); }
      set { SetValue(ColorProperty, value); }
    }

    private static void OnColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      ((ProgressSpinner)sender).AdjustGradient();
    }
  }
}
