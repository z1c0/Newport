﻿using Microsoft.Phone.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Newport
{
  public class TileMenu : Control
  {
    private ItemsControl _itemsControl;
    private Stack<IEnumerable> _cascadeStack;

    public TileMenu()
    {
      DefaultStyleKey = typeof(TileMenu);
      _cascadeStack = new Stack<IEnumerable>();
    }

    public override void OnApplyTemplate()
    {
      _itemsControl = (ItemsControl)GetTemplateChild("itemsControl");
      if (TileItemTemplate == null)
      {
        TileItemTemplate = (DataTemplate)_itemsControl.Resources["DefaultTileItemTemplate"];
      }
      InsertItems(Items);
      var page = new ControlFinder().FindParent<PhoneApplicationPage>(this);
      page.BackKeyPress += (_, e) =>
      {
        if (IsOpen)
        {
          IsOpen = false;
          e.Cancel = true;
        }
      };
      base.OnApplyTemplate();
    }

    #region TileMenuBackground

    public static readonly DependencyProperty TileMenuBackgroundProperty = DependencyProperty.Register(
      "TileMenuBackground", typeof(Brush), typeof(TileMenu), new PropertyMetadata(null));

    public Brush TileMenuBackground
    {
      get { return (Brush)GetValue(TileMenuBackgroundProperty); }
      set { SetValue(TileMenuBackgroundProperty, value); }
    }

    #endregion TileMenuBackground

    #region TileMargin

    public static readonly DependencyProperty TileMarginProperty =
      DependencyProperty.Register("TileMargin", typeof(Thickness), typeof(TileMenu), new PropertyMetadata(new Thickness(12)));

    public Thickness TileMargin
    {
      get { return (Thickness)GetValue(TileMarginProperty); }
      set { SetValue(TileMarginProperty, value); }
    }

    #endregion TileMargin

    #region TileItemTemplate

    public static readonly DependencyProperty TileItemTemplateProperty =
      DependencyProperty.Register("TileItemTemplate", typeof(DataTemplate), typeof(TileMenu), new PropertyMetadata(null));

    public DataTemplate TileItemTemplate
    {
      get { return (DataTemplate)GetValue(TileItemTemplateProperty); }
      set { SetValue(TileItemTemplateProperty, value); }
    }

    #endregion TileItemTemplate

    #region Items

    public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
      "Items", typeof(IEnumerable), typeof(TileMenu), new PropertyMetadata(null, new PropertyChangedCallback(ItemsOpenPropertyChanged)));

    private static void ItemsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((TileMenu)d).InsertItems((IEnumerable)e.NewValue);
    }

    private void InsertItems(IEnumerable items)
    {
      if (_itemsControl != null)
      {
        _itemsControl.Items.Clear();
        if (items != null)
        {
          foreach (var i in items)
          {
            var tile = new Border
            {
              BorderThickness = new Thickness(0),
              Margin = TileMargin,
              Background = TileMenuBackground,
              Projection = new PlaneProjection
              {
                RotationY = 90,
              },
              Child = new ContentControl
              {
                ContentTemplate = TileItemTemplate,
                Content = i
              }
            };
            tile.Tap += (_, __) =>
            {
              var viewModel = i as TileMenuItemViewModel;
              if (viewModel != null)
              {
                if (viewModel.Items != null)
                {
                  _cascadeStack.Push(Items);
                  AnimateTiles(false, () =>
                  {
                    InsertItems(viewModel.Items);
                    AnimateTiles(true, null);
                  });
                }
                else if (viewModel.Command != null)
                {
                  viewModel.Command.Execute(null);
                  IsOpen = false;
                }
              }
              else
              {
                SelectedItem = i;
                IsOpen = false;
              }
            };
            _itemsControl.Items.Add(tile);
          }
        }
      }
    }

    public IEnumerable Items
    {
      get { return (IEnumerable)GetValue(ItemsProperty); }
      set { SetValue(ItemsProperty, value); }
    }

    #endregion Items

    #region SelectedItem

    public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
      "SelectedItem", typeof(object), typeof(TileMenu), new PropertyMetadata(null));

    public object SelectedItem
    {
      get { return GetValue(SelectedItemProperty); }
      set { SetValue(SelectedItemProperty, value); }
    }

    #endregion SelectedItem

    #region IsOpen

    public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
      "IsOpen", typeof(bool?), typeof(TileMenu), new PropertyMetadata(null, new PropertyChangedCallback(IsOpenPropertyChanged)));

    private static void IsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var tileMenu = (TileMenu)d;
      if (tileMenu._itemsControl != null)
      {
        if (tileMenu.IsOpen)
        {
          VisualStateManager.GoToState(tileMenu, "IsOpen", true);
          tileMenu.AnimateTiles(true, null);
        }
        else
        {
          tileMenu.AnimateTiles(false, () => VisualStateManager.GoToState(tileMenu, "IsClosed", true));
          // TODO
          if (tileMenu._cascadeStack.Count > 0)
          {
            tileMenu.InsertItems(tileMenu._cascadeStack.Pop());
          }
        }
      }
    }

    private void AnimateTiles(bool visible, Action action)
    {
      var borders = _itemsControl.Items.Cast<Border>();
      if (borders.Count() > 0)
      {
        double total = _cascadeStack.Count > 0 ? 250 : 500;
        var begin = TimeSpan.FromMilliseconds(200);
        var duration = TimeSpan.FromMilliseconds(total);
        var step = TimeSpan.FromMilliseconds(total / borders.Count());
        if (visible)
        {
          foreach (var b in borders)
          {
            var sb = new Storyboard();
            sb.Children.Add(new DoubleAnimation
            {
              BeginTime = begin,
              Duration = duration,
              To = 0,
              EasingFunction = new BackEase
              {
                EasingMode = EasingMode.EaseOut
              }
            });
            Storyboard.SetTarget(sb, b.Projection);
            Storyboard.SetTargetProperty(sb, new PropertyPath(PlaneProjection.RotationYProperty));
            sb.Begin();
            begin += step;
          }
        }
        else
        {
          foreach (var b in borders.Reverse())
          {
            var sb = new Storyboard();
            sb.Children.Add(new DoubleAnimation
            {
              Duration = duration,
              To = 90,
              EasingFunction = new BackEase
              {
                EasingMode = EasingMode.EaseOut
              }
            });
            sb.Completed += (_, __) => action();
            Storyboard.SetTarget(sb, b.Projection);
            Storyboard.SetTargetProperty(sb, new PropertyPath(PlaneProjection.RotationYProperty));
            sb.Begin();
            begin += step;
          }
        }
      }
    }

    public bool IsOpen
    {
      get { return ((bool?)GetValue(IsOpenProperty)).GetValueOrDefault(); }
      set { SetValue(IsOpenProperty, value); }
    }

    #endregion IsOpen
  }
}