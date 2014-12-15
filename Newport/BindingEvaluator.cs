#if UNIVERSAL
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
    private readonly Binding _binding;

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