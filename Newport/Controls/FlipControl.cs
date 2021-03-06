﻿using System;
#if UNIVERSAL
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
#else
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
#endif

namespace Newport
{
  public class FlipControl : TemplatedControl
  {
    private Storyboard _sbFlip;
    private Storyboard _sbReverse;
    private ContentPresenter _contentPresenterFront;
    private ContentPresenter _contentPresenterBack;
    private UIElement _contentFront;
    private UIElement _contentBack;

    public FlipControl()
    {
      DefaultStyleKey = typeof(FlipControl);
    }

    protected override void OnFromTemplate()
    {
      var grid = (Grid)GetTemplateChild("Grid");
      _sbFlip = (Storyboard)grid.Resources["Storyboard_Flip"];
      _sbReverse = (Storyboard)grid.Resources["Storyboard_Reverse"];
      _sbFlip.Completed += (o, e) => IsFlipped = true;
      _sbReverse.Completed += (o, e) => IsFlipped = false;
      _contentPresenterFront = (ContentPresenter)GetTemplateChild("ContentFront");
      _contentPresenterFront.Content = _contentFront;
      _contentPresenterBack = (ContentPresenter)GetTemplateChild("ContentBack");
      _contentPresenterBack.Content = _contentBack;
    }

    public UIElement ContentFront
    {
      get
      {
        return _contentFront;
      }
      set
      {
        _contentFront = value;
        if (_contentPresenterFront != null)
        {
          _contentPresenterFront.Content = _contentFront;
        }
      }
    }

    public UIElement ContentBack
    {
      get
      {
        return _contentBack;
      }
      set
      {
        _contentBack = value;
        if (_contentPresenterBack != null)
        {
          _contentPresenterBack.Content = _contentBack;
        }
      }
    }

    public void Flip()
    {
      if (!IsFlipped)
      {
        if (_sbFlip != null)
        {
          _sbFlip.Begin();
        }
      }
      else
      {
        if (_sbReverse != null)
        {
          _sbReverse.Begin();
        }
      }
    }

    public bool IsFlipped { get; set; }
  }
}