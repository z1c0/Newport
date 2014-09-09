using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Newport
{
  public static class TextBoxExtensions
  {
    #region SelectOnFocus (Attached Property)

    public static readonly DependencyProperty SelectOnFocusProperty =
      DependencyProperty.RegisterAttached(
      "SelectOnFocus",
      typeof(bool),
      typeof(TextBoxExtensions),
      new PropertyMetadata(false, SelectOnFocusPropertyChanged));

    private static void SelectOnFocusPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      var textBox = (TextBox)sender;
      textBox.GotFocus += (o, e) =>
      {
        textBox.SelectAll();
      };
    }

    public static void SetSelectOnFocus(TextBox textBox, bool parameter)
    {
      textBox.SetValue(SelectOnFocusProperty, parameter);
    }

    public static bool GetSelectOnFocus(TextBox textBox)
    {
      return (bool)textBox.GetValue(SelectOnFocusProperty);
    }

    #endregion SelectOnFocus (Attached Property)

    #region UpdateBindingOnTextChange (Attached Property)

    public static readonly DependencyProperty UpdateBindingOnTextChangeProperty =
      DependencyProperty.RegisterAttached(
      "UpdateBindingOnTextChange",
      typeof(bool),
      typeof(TextBoxExtensions),
      new PropertyMetadata(false, OnUpdateBindingOnTextChangeChanged));

    private static void OnUpdateBindingOnTextChangeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      var textBox = (TextBox)sender;
      textBox.TextChanged += (_, __) =>
      {
        var binding = textBox.GetBindingExpression(TextBox.TextProperty);
        if (binding != null)
        {
          binding.UpdateSource();
        }
      };
    }

    public static void SetUpdateBindingOnTextChange(TextBox textBox, bool parameter)
    {
      textBox.SetValue(UpdateBindingOnTextChangeProperty, parameter);
    }

    public static bool GetUpdateBindingOnTextChange(TextBox textBox)
    {
      return (bool)textBox.GetValue(UpdateBindingOnTextChangeProperty);
    }

    #endregion UpdateBindingOnTextChange (Attached Property)

    #region TextChangedCommand (Attached Property)

    public static readonly DependencyProperty TextChangedCommandProperty =
      DependencyProperty.RegisterAttached(
      "TextChangedCommand",
      typeof(ICommand),
      typeof(TextBoxExtensions),
      new PropertyMetadata(null, OnTextChangedCommandCommandChanged));

    public static void SetTextChangedCommand(TextBox t, ICommand command)
    {
      t.SetValue(TextChangedCommandProperty, command);
    }

    public static ICommand GetTextChangedCommand(TextBox t)
    {
      return t.GetValue(TextChangedCommandProperty) as ICommand;
    }

    private static void OnTextChangedCommandCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      var element = (TextBox)sender;
      element.TextChanged += (o, e) => UIElementExtensions.TriggerCommand(GetTextChangedCommand(element), element, e);
    }

    #endregion TextChangedCommand (Attached Property)
  }
}