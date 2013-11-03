using System;
using System.Windows;
#if NETFX_CORE
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Data;
#else
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Animation;
#endif

namespace Newport
{
#if NETFX_CORE
  [ContentProperty(Name = "Action")]
#else
  [ContentProperty("Action")]
#endif
  public class DataTrigger : DependencyObject
  {
    private BindingListener _bindingListener;

    public DataTrigger()
    {
      _bindingListener = new BindingListener();
      _bindingListener.PropertyChanged += (o, e) =>
      {
        if (Action != null)
        {
          if (_bindingListener.Result)
          {
            Action.Begin();
          }
          else
          {
            Action.Stop();
          }
        }
      };
    }

    public Storyboard Action { get; set; }

    public Binding Binding { get; set; }

    public string Value
    {
      get
      {
        return _bindingListener.Value;
      }
      set
      {
        _bindingListener.Value = value;
      }
    }

    internal void AttachToFrameworkElement(FrameworkElement frameworkElement)
    {
      _bindingListener.Setup(frameworkElement, Binding);
    }
  }
}