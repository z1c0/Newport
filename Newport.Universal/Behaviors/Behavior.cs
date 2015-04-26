using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.UI.Xaml;

namespace Newport
{
  public static class Interaction
  {
    private static readonly DependencyProperty BehaviorsProperty = DependencyProperty.RegisterAttached(
      "Behaviors", typeof(BehaviorCollection), typeof(Interaction), new PropertyMetadata(null, OnBehaviorsPropertyChanged));

    private static void OnBehaviorsPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      var oldValue = (BehaviorCollection)e.OldValue;
      var newValue = (BehaviorCollection)e.NewValue;
      if (oldValue != newValue)
      {
        if (oldValue != null && oldValue.AssociatedObject != null)
        {
          oldValue.Detach();
        }
        if (newValue != null && obj != null)
        {
          if (newValue.AssociatedObject != null)
          {
            throw new InvalidOperationException("AssociatedObject is already set");
          }
          newValue.Attach(obj);
        }
      }
    }

    public static BehaviorCollection GetBehaviors(DependencyObject d)
    {
      var value = (BehaviorCollection)d.GetValue(BehaviorsProperty);
      if (value == null)
      {
        value = new BehaviorCollection();
        d.SetValue(BehaviorsProperty, value);
      }
      return value;
    }
  }

  public class BehaviorCollection : ObservableCollection<Behavior>
  {
    public DependencyObject AssociatedObject { get; private set; }

    public BehaviorCollection()
    {
      if (!ViewModelBase.IsDesignMode)
      {
        ((INotifyCollectionChanged)this).CollectionChanged += (_, args) =>
        {
          switch (args.Action)
          {
            case NotifyCollectionChangedAction.Add:
              foreach (Behavior b in args.NewItems)
              {
                b.Attach(AssociatedObject);
              }
              break;

            case NotifyCollectionChangedAction.Remove:
              foreach (Behavior b in args.NewItems)
              {
                b.Detach();
              }
              break;
          }
        };
      }
    }

    internal void Detach()
    {
      AssociatedObject = null;
    }

    internal void Attach(DependencyObject obj)
    {
      AssociatedObject = obj;
    }
  }

  public class Behavior : DependencyObject
  {
    protected DependencyObject _associatedObject;

    internal void Detach()
    {
      OnDetaching();
      _associatedObject = null;
    }

    internal void Attach(DependencyObject obj)
    {
      _associatedObject = obj;
      OnAttached();
    }

    protected virtual void OnAttached()
    {
    }

    protected virtual void OnDetaching()
    {
    }
  }

  public class Behavior<T> : Behavior where T : DependencyObject
  {
    protected T AssociatedObject
    {
      get
      {
        return (T)_associatedObject;
      }
    }
  }
}
