using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Newport
{
  [ContentProperty("Child")]
  public class BevelBorder : Control
  {
    public BevelBorder()
    {
      DefaultStyleKey = typeof(BevelBorder);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      var presenter = GetTemplateChild("ContentContainer") as ContentPresenter;
      presenter.Content = Child;
    }

    public UIElement Child { get; set; }
  }
}