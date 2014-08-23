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
      var button = (Button)sender;
      button.Click += (o, e) => UIElementExtensions.TriggerCommand(GetClickCommand(button), button, e);
    }

    #endregion ClickCommand (Attached Property)
  }
}