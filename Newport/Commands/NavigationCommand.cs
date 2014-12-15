﻿using System;
using System.Windows.Input;

namespace Newport
{
  public class NavigationCommand : ICommand
  {
    private readonly Type _type;

    public NavigationCommand()
    {
    }

    public NavigationCommand(string name)
    {
      Uri = new Uri(name, UriKind.RelativeOrAbsolute);
    }

    public NavigationCommand(Type t)
    {
      _type = t;
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
        NavigationAdapter.Navigate(null);//TODO
      }
      NavigationAdapter.Navigate(_type);//TODO
    }

    public Uri Uri { get; set; }
  }
}