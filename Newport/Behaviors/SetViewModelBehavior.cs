#if NETFX_CORE
using Windows.UI.Xaml;
using WinRtBehaviors;
#else
using System.Windows;
using System.Windows.Interactivity;
#endif

namespace Newport
{
  public class SetViewModelBehavior : Behavior<FrameworkElement>
  {
    public string Key { get; set; }

    public string Path { get; set; }

    protected override void OnAttached()
    {
      base.OnAttached();

      object data = ViewModelProvider.Default[Key];
      if (data != null)
      {
        if (Path != null)
        {
          data = new BindingEvaluator(data, Path).Evaluate();
        }
        AssociatedObject.DataContext = data;
      }
    }
  }
}