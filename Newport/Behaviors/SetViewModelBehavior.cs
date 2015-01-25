using System;
#if UNIVERSAL
using Windows.UI.Xaml;
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
      if (!ViewModelBase.IsDesignMode)
      {
        if (Key == null)
        {
          Key = InferViewModelKeyName();
        }
        object data = Get.ViewModelProvider[Key];
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

    private string InferViewModelKeyName()
    {
      string keyName = null;
      var className = AssociatedObject.GetType().Name;
      var suffices = new [] { "Page", "View", "Control" };
      foreach (var s in suffices)
      {
        if (className.EndsWith(s))
        {
          keyName = className.Remove(className.Length - s.Length) + "ViewModel";
          break;
        }
      }
      return keyName;
    }
  }
}