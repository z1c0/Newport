using System;
#if UNIVERSAL
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#else
using System.Windows;
using System.Windows.Controls;
#endif


namespace Newport
{
  public abstract class TemplatedControl : Control
  {
#if UNIVERSAL
    protected override void OnApplyTemplate()
#else
    public override void OnApplyTemplate()
#endif
    {
      base.OnApplyTemplate();
      OnFromTemplate();
    }

    protected abstract void OnFromTemplate();

    protected T VerifyGetTemplateChild<T>(string name) where T : UIElement
    {
      var t = GetTemplateChild(name) as T;
      if (t == null)
      {
        throw new InvalidOperationException("Template child '" + name + "' not found");
      }
      return t;
    }
  }
}