using System.Collections.Generic;
#if UNIVERSAL
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
#else
using System.Windows;
using System.Windows.Markup;
#endif

namespace Newport
{
#if UNIVERSAL
  [ContentProperty(Name = "Templates")]
#else
  [ContentProperty("Templates")]
#endif
  public abstract class DataTemplateSelector
  {
    public DataTemplateSelector()
    {
      Templates = new List<DataTemplate>();
    }

    public List<DataTemplate> Templates { get; set; }

    public abstract DataTemplate SelectTemplate(object item, DependencyObject element);
  }
}