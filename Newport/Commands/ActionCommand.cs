using System;
using System.Windows;
using System.Windows.Input;

namespace Newport
{
  public class ActionEventSource
  {

    public EventArgs OriginalEventArgs { get; set; }

    public UIElement OriginalSender { get; set; }
  }

  public class GenericActionCommand<T> : ActionEventSource, ICommand
  {
    public GenericActionCommand()
    {
    }

    public GenericActionCommand(Action<T> action)
    {
      Action = action;
    }

    public GenericActionCommand(Action<T> action, Func<T, bool> isEnabled)
    {
      Action = action;
      IsEnabled = isEnabled;
    }

    public event EventHandler CanExecuteChanged
    {
      add
      {
        CommandManager.RequerySuggested += value;
      }
      remove
      {
        CommandManager.RequerySuggested -= value;
      }
    }

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
      }
      CommandManager.InvalidateRequerySuggested();
    }

    public Action<T> Action { get; set; }

    public Func<T, bool> IsEnabled { get; set; }
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