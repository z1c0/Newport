using System.Windows;
using System.Windows.Controls;

namespace Newport
{
  public class ContentControlEx : ContentControl
  {
    public static readonly DependencyProperty ContentTemplateSelectorProperty;

    static ContentControlEx()
    {
      ContentTemplateSelectorProperty = DependencyProperty.Register(
        "ContentTemplateSelector",
        typeof(DataTemplateSelector),
        typeof(ContentControlEx),
        null);
    }

    public DataTemplateSelector ContentTemplateSelector
    {
      get
      {
        return GetValue(ContentTemplateSelectorProperty) as DataTemplateSelector;
      }
      set
      {
        SetValue(ContentTemplateSelectorProperty, value);
      }
    }

    protected override void OnContentChanged(object oldContent, object newContent)
    {
      DataTemplateSelector selector = this.ContentTemplateSelector;
      if (selector != null)
      {
        this.ContentTemplate = selector.SelectTemplate(newContent, this);
      }
      base.OnContentChanged(oldContent, newContent);
    }
  }
}