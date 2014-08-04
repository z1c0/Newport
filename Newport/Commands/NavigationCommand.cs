using System;
using System.Windows.Input;

namespace Newport
{
  public class NavigationCommand : ICommand
  {
    public event EventHandler CanExecuteChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      if (Uri != null)
      {
        NavigationAdapter.NavigationService.Navigate(Uri);
      }
    }

    public Uri Uri { get; set; }
  }
}