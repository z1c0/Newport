using System;
#if UNIVERSAL
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
#else
using System.Windows;
using System.Windows.Markup;
using System.Windows.Controls;
#endif

namespace Newport
{
#if UNIVERSAL
  [ContentProperty(Name = "Child")]
#else
  [ContentProperty("Child")]
#endif
  public class BevelBorder : TemplatedControl
  {
    public BevelBorder()
    {
      DefaultStyleKey = typeof(BevelBorder);
    }

    protected override void OnFromTemplate()
    {
      var presenter = VerifyGetTemplateChild<ContentPresenter>("ContentContainer");
      presenter.Content = Child;
    }

    public UIElement Child { get; set; }
  }
}