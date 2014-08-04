#if NETFX_CORE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#else
using System.Windows;
using System.Windows.Data;
#endif

namespace Newport
{
  public class BindingEvaluator : FrameworkElement
  {
    private Binding _binding;

    private static readonly DependencyProperty DummyProperty = DependencyProperty.Register(
      "Dummy",
      typeof(object),
      typeof(BindingEvaluator),
      new PropertyMetadata(null));

    public BindingEvaluator(Binding binding)
    {
      _binding = binding;
    }

    public BindingEvaluator(object container, string expression) :
      //this(new Binding(expression) { Source = container })
      this(new Binding() { Source = container, Path = new PropertyPath(expression) })
    {
    }

    public object Evaluate()
    {
      BindingOperations.SetBinding(this, DummyProperty, _binding);
      return GetValue(DummyProperty);
    }
  }
}