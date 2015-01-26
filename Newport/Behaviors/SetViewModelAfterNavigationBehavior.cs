using System;
#if UNIVERSAL
using Windows.UI.Xaml.Controls;
#else
using System.Windows.Interactivity;
using Microsoft.Phone.Controls;
#endif

namespace Newport
{
#if UNIVERSAL
  public class SetViewModelAfterNavigationBehavior : Behavior<Page>
#else
  public class SetViewModelAfterNavigationBehavior : Behavior<PhoneApplicationPage>
#endif
  {
    protected override void OnAttached()
    {
      base.OnAttached();
      if (!ViewModelBase.IsDesignMode)
      {
        AssociatedObject.Loaded += (_, __) =>
        {
          AssociatedObject.DataContext = Get.Navigator.GetDataContextFor(AssociatedObject.GetType());
        };
      }
    }
  }
}