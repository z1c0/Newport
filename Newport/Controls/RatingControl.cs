using System;
#if UNIVERSAL
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
#else
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
#endif

namespace Newport
{
  public class RatingControl : TemplatedControl
  {
    private StackPanel _itemsPanel;
    private bool _isInitialized;

    public RatingControl()
    {
      DefaultStyleKey = typeof(RatingControl);
    }

    protected override void OnFromTemplate()
    {
      _itemsPanel = VerifyGetTemplateChild<StackPanel>("itemsPanel");
      if (ItemTemplate == null)
      {
        ItemTemplate = (DataTemplate)_itemsPanel.Resources["DefaultItemTemplate"];
      }
      _isInitialized = true;
      CreateItems();
    }

    private void CreateItems()
    {
      if (_isInitialized)
      {
        _itemsPanel.Children.Clear();
        Maximum.Times(i =>
        {
          var c = new ContentControl
          {
            ContentTemplate = ItemTemplate
          };
#if UNIVERSAL
            c.Tapped += (_, __) =>
#else
            c.Tap += (_, __) =>
#endif
          {
            Value = i + 1;
            UpdateItems();
          };
          _itemsPanel.Children.Add(c);
        });
        UpdateItems();
      }
    }

    private void UpdateItems()
    {
      if (_isInitialized)
      {
        for (var i = 0; i < _itemsPanel.Children.Count; i++)
        {
          var e = _itemsPanel.Children[i];
          e.Opacity = (i < Value) ? 1.0 : 0.3;
        }
      }
    }

    public static readonly DependencyProperty MaximumProperty =
      DependencyProperty.Register(
      "Maximum",
      typeof(int),
      typeof(RatingControl),
      new PropertyMetadata(5, OnMaximumChanged));

    public int Maximum
    {
      get { return (int)GetValue(MaximumProperty); }
      set { SetValue(MaximumProperty, value); }
    }

    private static void OnMaximumChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      ((RatingControl)sender).CreateItems();
    }

    public static readonly DependencyProperty ValueProperty =
      DependencyProperty.Register(
      "Value",
      typeof(int),
      typeof(RatingControl),
      new PropertyMetadata(0, OnValueChanged));

    public int Value
    {
      get { return (int)GetValue(ValueProperty); }
      set { SetValue(ValueProperty, value); }
    }

    private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      ((RatingControl)sender).UpdateItems();
    }

    public static readonly DependencyProperty ItemTemplateProperty =
      DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(RatingControl), new PropertyMetadata(null));

    public DataTemplate ItemTemplate
    {
      get { return (DataTemplate)GetValue(ItemTemplateProperty); }
      set { SetValue(ItemTemplateProperty, value); }
    }
  }
}
