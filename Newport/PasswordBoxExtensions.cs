using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Newport
{
  public static class PasswordBoxExtensions
  {
    #region UpdateBindingOnPasswordChange (Attached Property)

    public static readonly DependencyProperty UpdateBindingOnPasswordChangeProperty =
      DependencyProperty.RegisterAttached(
      "UpdateBindingOnPasswordChange",
      typeof(bool),
      typeof(PasswordBoxExtensions),
      new PropertyMetadata(false, OnUpdateBindingOnPasswordChangeChanged));

    private static void OnUpdateBindingOnPasswordChangeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      var passwordBox = (PasswordBox)sender;
      passwordBox.PasswordChanged += (_, __) =>
      {
        var binding = passwordBox.GetBindingExpression(PasswordBox.PasswordProperty);
        if (binding != null)
        {
          binding.UpdateSource();
        }
      };
    }

    public static void SetUpdateBindingOnPasswordChange(PasswordBox passwordBox, bool parameter)
    {
      passwordBox.SetValue(UpdateBindingOnPasswordChangeProperty, parameter);
    }

    public static bool GetUpdateBindingOnPasswordChange(PasswordBox passwordBox)
    {
      return (bool)passwordBox.GetValue(UpdateBindingOnPasswordChangeProperty);
    }

    #endregion UpdateBindingOnPasswordChange (Attached Property)
  }
}