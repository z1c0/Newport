using System;
using System.Windows.Input;

namespace Newport
{
  public class NavigationCommand : ICommand
  {
    public NavigationCommand()
    {
    }

    public NavigationCommand(string name)
    {
      Uri = new Uri(name, UriKind.RelativeOrAbsolute);
    }

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