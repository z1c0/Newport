using System;
using System.Windows.Input;
#if UNIVERSAL
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
#else
using System.Windows;
using System.Windows.Media.Animation;
#endif

namespace Newport
{
  public class UIElementExtensions
  {
    #region CommandParameter (Attached Property)

    public static readonly DependencyProperty CommandParameterProperty =
      DependencyProperty.RegisterAttached(
      "CommandParameter",
      typeof(object),
      typeof(UIElementExtensions),
      new PropertyMetadata(null));

    public static void SetCommandParameter(UIElement e, object parameter)
    {
      e.SetValue(CommandParameterProperty, parameter);
    }

    public static object GetCommandParameter(UIElement e)
    {
      return e.GetValue(CommandParameterProperty);
    }

    #endregion CommandParameter (Attached Property)

    #region MouseLeftButtonDownCommand (Attached Property)

    public static readonly DependencyProperty MouseLeftButtonDownCommandProperty =
      DependencyProperty.RegisterAttached(
      "MouseLeftButtonDownCommand",
      typeof(ICommand),
      typeof(UIElementExtensions),
      new PropertyMetadata(null, OnMouseLeftButtonDownCommandChanged));

    public static void SetMouseLeftButtonDownCommand(UIElement e, ICommand command)
    {
      e.SetValue(MouseLeftButtonDownCommandProperty, command);
    }

    public static ICommand GetMouseLeftButtonDownCommand(UIElement e)
    {
      return e.GetValue(MouseLeftButtonDownCommandProperty) as ICommand;
    }

    private static void OnMouseLeftButtonDownCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
#if UNIVERSAL
      throw new NotImplementedException();
#else
      var element = (UIElement)sender;
      element.MouseLeftButtonDown += (o, e) => TriggerCommand(GetMouseLeftButtonDownCommand(element), element, e);
#endif
    }

    #endregion MouseLeftButtonDownCommand (Attached Property)

    #region IsVisible (Attached Property)

    public static readonly DependencyProperty IsVisibleProperty =
      DependencyProperty.RegisterAttached(
      "IsVisible",
      typeof(bool),
      typeof(UIElementExtensions),
      new PropertyMetadata(true, OnIsVisibleChanged));

    public static void SetIsVisible(UIElement e, bool value)
    {
      e.SetValue(IsVisibleProperty, value);
    }

    public static bool GetIsVisible(UIElement e)
    {
      return (bool)e.GetValue(IsVisibleProperty);
    }

    private static void OnIsVisibleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      var e = (UIElement)sender;
      var vis = GetIsVisible(e) ? Visibility.Visible : Visibility.Collapsed;
      e.Visibility = vis;
    }

    #endregion IsVisible (Attached Property)

    #region AppearAfter (Attached Property)

    public static readonly DependencyProperty AppearAfterProperty =
      DependencyProperty.RegisterAttached(
      "AppearAfter",
      typeof(int),
      typeof(UIElementExtensions),
      new PropertyMetadata(0, OnAppearAfterChanged));

    public static void SetAppearAfter(UIElement e, int value)
    {
      e.SetValue(AppearAfterProperty, value);
    }

    public static int GetAppearAfter(UIElement e)
    {
      return (int)e.GetValue(AppearAfterProperty);
    }

    private static void OnAppearAfterChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      if (!ViewModelBase.IsDesignMode)
      {
        var e = (UIElement)sender;
        e.Opacity = 0;
        var t = TimeSpan.FromMilliseconds(GetAppearAfter(e));
        var sb = new Storyboard
        {
          Duration = t
        };
        sb.Children.Add(new DoubleAnimation
        {
          Duration = t,
          To = 1.0,
        });
        Storyboard.SetTarget(sb, e);
#if UNIVERSAL
        Storyboard.SetTargetProperty(sb, "Opacity");
#else
        Storyboard.SetTargetProperty(sb, new PropertyPath("Opacity"));
#endif
        sb.Begin();
      }
    }

    #endregion AppearAfter (Attached Property)

    #region TapCommand (Attached Property)

    public static readonly DependencyProperty TapCommandProperty =
      DependencyProperty.RegisterAttached(
      "TapCommand",
      typeof(ICommand),
      typeof(UIElementExtensions),
      new PropertyMetadata(null, OnTapCommandChanged));

    public static void SetTapCommand(UIElement e, ICommand command)
    {
      e.SetValue(TapCommandProperty, command);
    }

    public static ICommand GetTapCommand(UIElement e)
    {
      return e.GetValue(TapCommandProperty) as ICommand;
    }

    private static void OnTapCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
#if UNIVERSAL
      throw new NotImplementedException();
#else
      var e = (UIElement)sender;
      if (e != null)
      {
        e.Tap += (o, a) => TriggerCommand(GetTapCommand(e), e, a);
      }
#endif
    }
    #endregion TapCommand (Attached Property)

// TODO Wrap EventArgs class
#if UNIVERSAL
    internal static void TriggerCommand(ICommand command, UIElement e, RoutedEventArgs eventArgs)
#else
    internal static void TriggerCommand(ICommand command, UIElement e, EventArgs eventArgs)
#endif
    {
      if (command != null)
      {
        var actionEventSource = command as ActionEventSource;
        if (actionEventSource != null)
        {
          actionEventSource.OriginalSender = e;
          actionEventSource.OriginalEventArgs = eventArgs;
        }
        var parameter = GetCommandParameter(e);
        if (command.CanExecute(parameter))
        {
          command.Execute(parameter);
        }
      }
    }
  }
}