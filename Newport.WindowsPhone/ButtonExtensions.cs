using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Newport
{
  public static class ButtonExtensions
  {
    #region ClickCommand (Attached Property)

    public static readonly DependencyProperty ClickCommandProperty =
      DependencyProperty.RegisterAttached(
      "ClickCommand",
      typeof(ICommand),
      typeof(ButtonExtensions),
      new PropertyMetadata(null, OnClickCommandChanged));

    public static void SetClickCommand(Button button, ICommand command)
    {
      button.SetValue(ClickCommandProperty, command);
    }

    public static ICommand GetClickCommand(Button button)
    {
      return button.GetValue(ClickCommandProperty) as ICommand;
    }

    private static void OnClickCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      Button button = sender as Button;
      if (button != null)
      {
        button.Click += new RoutedEventHandler(HandleClick);
      }
    }

    private static void HandleClick(object sender, RoutedEventArgs e)
    {
      Button button = sender as Button;
      if (button != null)
      {
        ICommand command = GetClickCommand(button);
        object parameter = UIElementExtensions.GetCommandParameter(button);
        if ((command != null) && (command.CanExecute(parameter)))
        {
          command.Execute(parameter);
        }
      }
    }

    #endregion ClickCommand (Attached Property)
  }
}