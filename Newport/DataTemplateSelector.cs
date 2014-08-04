using System.Collections.Generic;
using System.Windows;
using System.Windows.Markup;

namespace Newport
{
  [ContentProperty("Templates")]
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