using System;
using System.Windows.Input;
#if UNIVERSAL
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace Newport
{
  internal interface ITriggerable
  {
    void Trigger();
  }

  public class ActionEventSource
  {
    // TODO Wrap EVent class
#if UNIVERSAL
    public RoutedEventArgs OriginalEventArgs { get; set; }
#else
    public EventArgs OriginalEventArgs { get; set; }
#endif

    public UIElement OriginalSender { get; set; }
  }

  public class GenericActionCommand<T> : ActionEventSource, ITriggerable, ICommand
  {
    public GenericActionCommand()
    {
      CommandManager.Commands.Add(this);
    }

    public GenericActionCommand(Action<T> action)
    {
      CommandManager.Commands.Add(this);
      Action = action;
    }

    public GenericActionCommand(Action<T> action, Func<T, bool> isEnabled)
    {
      CommandManager.Commands.Add(this);
      Action = action;
      IsEnabled = isEnabled;
    }

    public event EventHandler CanExecuteChanged;


    public bool CanExecute(object parameter)
    {
      return (IsEnabled == null) || IsEnabled((T)parameter);
    }

    public void Execute()
    {
      Execute(default(T));
    }

    public void Execute(object parameter)
    {
      if (Action != null)
      {
        Action((T)parameter);
        CommandManager.InvalidateRequerySuggested();
      }
    }

    public Action<T> Action { get; set; }

    public Func<T, bool> IsEnabled { get; set; }

    public void Trigger()
    {
      var handler = CanExecuteChanged;
      if (handler != null)
      {
        handler(this, EventArgs.Empty);
      }
    }
  }

  public class ActionCommand : GenericActionCommand<object>
  {
    public ActionCommand()
    {
    }

    public ActionCommand(Action<object> action)
      : base(action)
    {
    }

    public ActionCommand(Action<object> action, Func<object, bool> isEnabled)
      : base(action, isEnabled)
    {
    }
  }
}