using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace Newport
{
  public enum PageTransitionType
  {
    Turnstile,
  }

  public static class PhoneApplicationPageExtensions
  {
    #region ApplicationBarCommandButtons (Attached Property)

    public static readonly DependencyProperty ApplicationBarCommandButtonsProperty =
      DependencyProperty.RegisterAttached(
      "ApplicationBarCommandButtons",
      typeof(IList),
      typeof(PhoneApplicationPageExtensions),
      new PropertyMetadata(null));

    public static IList GetApplicationBarCommandButtons(PhoneApplicationPage page)
    {
      var buttons = page.GetValue(ApplicationBarCommandButtonsProperty) as IList;
      if (buttons == null)
      {
        buttons = new ApplicationBarCommandButtonList(page);
        page.SetValue(ApplicationBarCommandButtonsProperty, buttons);
      }
      return buttons;
    }

    #endregion ApplicationBarCommandButtons (Attached Property)

    #region BackKeyPressCommand (Attached Property)

    public static readonly DependencyProperty BackKeyPressCommandProperty =
      DependencyProperty.RegisterAttached(
      "BackKeyPressCommand",
      typeof(ICommand),
      typeof(PhoneApplicationPageExtensions),
      new PropertyMetadata(null, OnBackKeyPressCommandChanged));

    public static void SetBackKeyPressCommand(PhoneApplicationPage page, ICommand command)
    {
      page.SetValue(BackKeyPressCommandProperty, command);
    }

    public static ICommand GetBackKeyPressCommand(PhoneApplicationPage page)
    {
      return page.GetValue(BackKeyPressCommandProperty) as ICommand;
    }

    private static void OnBackKeyPressCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      var page = (PhoneApplicationPage)sender;
      page.BackKeyPress += (o, e) => UIElementExtensions.TriggerCommand(GetBackKeyPressCommand(page), page, e);
    }

    #endregion BackKeyPressCommand (Attached Property)
  }

  public class ApplicationBarCommandButtonList : IList
  {
    private readonly List<ApplicationBarBaseButton> _buttons;
    private readonly PhoneApplicationPage _page;

    public ApplicationBarCommandButtonList(PhoneApplicationPage page)
    {
      _page = page;
      _page.Loaded += new RoutedEventHandler(HandlePageLoaded);
      _buttons = new List<ApplicationBarBaseButton>();
    }

    private void HandlePageLoaded(object sender, RoutedEventArgs args)
    {
      if (_page.ApplicationBar == null)
      {
        _page.ApplicationBar = new ApplicationBar() { IsVisible = true };
      }
      foreach (var btn in _buttons)
      {
        btn.EvaluateBindings();
        var iconButton = new ApplicationBarIconButton()
        {
          IconUri = btn.IconUri,
          Text = btn.Text
        };
        if (btn.Command != null)
        {
          btn.Command.CanExecuteChanged += (_, __) => iconButton.IsEnabled = btn.Command.CanExecute(null);
        }
        iconButton.Click += (_, __) => btn.OnClick();
        _page.ApplicationBar.Buttons.Add(iconButton);
      }
      _page.Loaded -= HandlePageLoaded;
      CommandManager.InvalidateRequerySuggested();
    }

    public int Add(object value)
    {
      var btn = (ApplicationBarBaseButton)value;
      btn.Page = _page;
      _buttons.Add(btn);
      return Count;
    }

    public int Count
    {
      get { return _buttons.Count; }
    }

    public void Clear()
    {
      _buttons.Clear();
    }

    public bool Contains(object value)
    {
      throw new NotImplementedException();
    }

    public int IndexOf(object value)
    {
      throw new NotImplementedException();
    }

    public void Insert(int index, object value)
    {
      throw new NotImplementedException();
    }

    public bool IsFixedSize
    {
      get { return false; }
    }

    public bool IsReadOnly
    {
      get { throw new NotImplementedException(); }
    }

    public void Remove(object value)
    {
      throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
      throw new NotImplementedException();
    }

    public object this[int index]
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public void CopyTo(Array array, int index)
    {
      throw new NotImplementedException();
    }

    public bool IsSynchronized
    {
      get { throw new NotImplementedException(); }
    }

    public object SyncRoot
    {
      get { throw new NotImplementedException(); }
    }

    public IEnumerator GetEnumerator()
    {
      throw new NotImplementedException();
    }
  }

  public abstract class ApplicationBarBaseButton : DependencyObject
  {
    protected internal ICommand Command { get; protected set; }

    public string TextPath { get; set; }

    public string Text { get; set; }

    public Uri IconUri { get; set; }

    internal PhoneApplicationPage Page { get; set; }

    protected internal abstract void OnClick();

    protected internal virtual void EvaluateBindings()
    {
      if (!string.IsNullOrEmpty(TextPath))
      {
        Text = new BindingEvaluator(Page.DataContext, TextPath).Evaluate() as string;
      }
    }
  }

  [ContentProperty("Command")]
  public class ApplicationBarCommandButton : ApplicationBarBaseButton
  {
    public string CommandPath { get; set; }

    public object CommandParameter { get; set; }

    protected internal override void EvaluateBindings()
    {
      base.EvaluateBindings();
      Command = new BindingEvaluator(Page.DataContext, CommandPath).Evaluate() as ICommand;
    }

    internal protected override void OnClick()
    {
      if (Command != null)
      {
        Command.Execute(CommandParameter);
      }
    }
  }

  public class ApplicationBarNavigationButton : ApplicationBarBaseButton
  {
    public Uri NavigationUri { get; set; }

    public bool GoBack { get; set; }

    internal protected override void OnClick()
    {
      if (GoBack)
      {
        Get.Navigator.NavigationService.GoBack();
      }
      else
      {
        if (NavigationUri != null)
        {
          Get.Navigator.NavigationService.Navigate(NavigationUri);
        }
      }
    }
  }
}