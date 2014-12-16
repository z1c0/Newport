using System;
using System.Windows;
using System.Windows.Input;
#if UNIVERSAL
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.Foundation;
#else
using Microsoft.Phone.Controls;
using System.Windows.Media;
#endif

namespace Newport
{
  public static class FrameworkElementExtensions
  {
    public static object TryFindResource(this FrameworkElement element, object resourceKey)
    {
      var currentElement = element;
      while (currentElement != null)
      {
        var resource = currentElement.Resources[resourceKey];
        if (resource != null)
        {
          return resource;
        }

        currentElement = currentElement.Parent as FrameworkElement;
      }
      return Application.Current.Resources[resourceKey];
    }

    #region DataTriggers (Attached Property)

    public static readonly DependencyProperty DataTriggerProperty =
      DependencyProperty.RegisterAttached(
      "DataTrigger",
      typeof(DataTrigger),
      typeof(FrameworkElementExtensions),
      new PropertyMetadata(null, OnDataTriggerChanged));

    public static DataTrigger GetDataTrigger(FrameworkElement e)
    {
      return (DataTrigger)e.GetValue(DataTriggerProperty);
    }

    public static void SetDataTrigger(FrameworkElement e, DataTrigger trigger)
    {
      e.SetValue(DataTriggerProperty, trigger);
    }

    private static void OnDataTriggerChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      ((DataTrigger)args.NewValue).AttachToFrameworkElement((FrameworkElement)sender);
    }

    #endregion DataTriggers (Attached Property)

    #region LoadedCommand (Attached Property)

    public static readonly DependencyProperty LoadedCommandProperty =
      DependencyProperty.RegisterAttached(
      "LoadedCommand",
      typeof(ICommand),
      typeof(FrameworkElementExtensions),
      new PropertyMetadata(null, OnLoadedCommandCommandChanged));

    public static void SetLoadedCommand(FrameworkElement e, ICommand command)
    {
      e.SetValue(LoadedCommandProperty, command);
    }

    public static ICommand GetLoadedCommand(FrameworkElement e)
    {
      return e.GetValue(LoadedCommandProperty) as ICommand;
    }

    private static void OnLoadedCommandCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      var element = (FrameworkElement)sender;
      element.Loaded += (o, e) => UIElementExtensions.TriggerCommand(GetLoadedCommand(element), element, e);
    }

    #endregion LoadedCommand (Attached Property)

    #region UnloadedCommand (Attached Property)

    public static readonly DependencyProperty UnloadedCommandProperty =
      DependencyProperty.RegisterAttached(
      "UnloadedCommand",
      typeof(ICommand),
      typeof(FrameworkElementExtensions),
      new PropertyMetadata(null, OnUnloadedCommandCommandChanged));

    public static void SetUnloadedCommand(FrameworkElement e, ICommand command)
    {
      e.SetValue(UnloadedCommandProperty, command);
    }

    public static ICommand GetUnloadedCommand(FrameworkElement e)
    {
      return e.GetValue(UnloadedCommandProperty) as ICommand;
    }

    private static void OnUnloadedCommandCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      var element = (FrameworkElement)sender;
      element.Unloaded += (o, e) => UIElementExtensions.TriggerCommand(GetUnloadedCommand(element), element, e);
    }

    #endregion UnloadedCommand (Attached Property)

    #region InvalidateRequerySuggestedAfterTouch (Attached Property)

    public static readonly DependencyProperty InvalidateRequerySuggestedAfterTouchProperty = DependencyProperty.RegisterAttached(
      "InvalidateRequerySuggestedAfterTouch", typeof(bool), typeof(FrameworkElementExtensions), new PropertyMetadata(false, OnInvalidateRequerySuggestedAfterTouchPropertyChanged));

    public static void SetInvalidateRequerySuggestedAfterTouch(FrameworkElement e, bool value)
    {
      e.SetValue(InvalidateRequerySuggestedAfterTouchProperty, value);
    }

    public static bool GetInvalidateRequerySuggestedAfterTouch(FrameworkElement e)
    {
      return (bool)e.GetValue(InvalidateRequerySuggestedAfterTouchProperty);
    }

    private static void OnInvalidateRequerySuggestedAfterTouchPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
#if UNIVERSAL
      throw new NotImplementedException();
#else
      var fe = (FrameworkElement)d;
      MouseEventHandler handler = (_, __) => CommandManager.InvalidateRequerySuggested();
      if (GetInvalidateRequerySuggestedAfterTouch(fe))
      {
        fe.MouseMove += handler;
      }
      else
      {
        fe.MouseMove -= handler;
      }
#endif
    }

    #endregion InvalidateRequerySuggestedAfterTouch (Attached Property)

    #region ClipToBounds (Attached Property)

    public static readonly DependencyProperty ClipToBoundsProperty = DependencyProperty.RegisterAttached(
      "ClipToBounds", typeof(bool), typeof(FrameworkElementExtensions), new PropertyMetadata(false, OnToBoundsPropertyChanged));

    public static void SetClipToBounds(FrameworkElement e, bool value)
    {
      e.SetValue(ClipToBoundsProperty, value);
    }

    public static bool GetClipToBounds(FrameworkElement e)
    {
      return (bool)e.GetValue(ClipToBoundsProperty);
    }

    private static void OnToBoundsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var fe = (FrameworkElement)d;
      ClipToBounds(fe);
      fe.Loaded += (_, __) => ClipToBounds(fe);
      fe.SizeChanged += (_, __) => ClipToBounds(fe);
    }

    private static void ClipToBounds(FrameworkElement fe)
    {
      if (GetClipToBounds(fe))
      {
        fe.Clip = new RectangleGeometry
        {
          Rect = new Rect(0, 0, fe.ActualWidth, fe.ActualHeight)
        };
      }
      else
      {
        fe.Clip = null;
      }
    }

    #endregion ClipToBounds (Attached Property)

    #region SwapDimensionsOnRotation (Attached Property)

    public static readonly DependencyProperty SwapDimensionsOnRotationProperty =
      DependencyProperty.RegisterAttached(
      "SwapDimensionsOnRotation",
      typeof(bool),
      typeof(FrameworkElementExtensions),
      new PropertyMetadata(false, OnSwapDimensionsOnRotationChanged));

    public static void SetSwapDimensionsOnRotation(FrameworkElement e, bool b)
    {
      e.SetValue(SwapDimensionsOnRotationProperty, b);
    }

    public static bool GetSwapDimensionsOnRotation(FrameworkElement e)
    {
      return (bool)e.GetValue(SwapDimensionsOnRotationProperty);
    }

    private static void OnSwapDimensionsOnRotationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      ((FrameworkElement)sender).Loaded += HandleElementLoaded;
    }

    private static void HandleElementLoaded(object sender, RoutedEventArgs e)
    {
#if UNIVERSAL
      throw new NotImplementedException();
#else
      var element = (FrameworkElement)sender;
      element.Loaded -= HandleElementLoaded;
      if (GetSwapDimensionsOnRotation(element))
      {
        var page = ControlFinder.FindParent<PhoneApplicationPage>(element);
        page.OrientationChanged += (_, __) =>
        {
          var tmp = element.Width;
          element.Width = element.Height;
          element.Height = tmp;
        };
      }
#endif
    }

    #endregion SwapDimensionsOnRotation (Attached Property)
  }
}