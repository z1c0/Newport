using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Newport
{
  public static class PageExtensions
  {
    public static readonly DependencyProperty SetDataContextAfterNavigationProperty =
      DependencyProperty.RegisterAttached(
      "SetDataContextAfterNavigation",
      typeof(bool),
      typeof(PageExtensions),
      new PropertyMetadata(false, SetDataContextAfterNavigationChanged));

    public static void SetSetDataContextAfterNavigation(Page page, bool value)
    {
      page.SetValue(SetDataContextAfterNavigationProperty, value);
    }

    public static bool GetSetDataContextAfterNavigation(Page page)
    {
      return (bool)page.GetValue(SetDataContextAfterNavigationProperty);
    }

    private static void SetDataContextAfterNavigationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      var page = (Page)sender;
      page.Loaded += (_, __) =>
      {
        if (GetSetDataContextAfterNavigation(page))
        {
          page.DataContext = Get.Navigator.GetDataContextFor(page.GetType());
        }
      };
    }
  }
}
