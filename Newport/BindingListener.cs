using System;
using System.Globalization;
#if UNIVERSAL
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#else
using System.Windows;
using System.Windows.Data;
#endif

namespace Newport
{
  public class BindingListener : DependencyObject
  {
    public EventHandler PropertyChanged;

    #region BindingListener (Attached Property)

    private static readonly DependencyProperty BindingListenerProperty = DependencyProperty.RegisterAttached(
      "BindingListener", typeof(BindingListener), typeof(BindingListener), new PropertyMetadata(null));

    private static BindingListener GetBindingListener(FrameworkElement element)
    {
      return (BindingListener)element.GetValue(BindingListenerProperty);
    }

    private static void SetBindingListener(FrameworkElement element, BindingListener value)
    {
      element.SetValue(BindingListenerProperty, value);
    }

    #endregion BindingListener (Attached Property)

    #region Property (Attached Property)

    private static readonly DependencyProperty PropertyProperty = DependencyProperty.RegisterAttached(
      "Property", typeof(object), typeof(BindingListener), new PropertyMetadata(null, new PropertyChangedCallback(OnPropertyChanged)));

    private static void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
      var element = (FrameworkElement)sender;
      var bindingListener = GetBindingListener(element);
      if (bindingListener.PropertyChanged != null)
      {
        bindingListener.Evaluate(e.NewValue);
        bindingListener.PropertyChanged(bindingListener, EventArgs.Empty);
      }
    }

    private void Evaluate(object value)
    {
      Result = false;
      if (value != null)
      {
        Result = ConvertValue(value.GetType()).Equals(value);
        //foreach (Setter setter in Setters)
        //{
        //  if (set)
        //  {
        //    object oldValue = _element.GetValue(setter.Property);
        //    if (_oldValues.ContainsKey(setter.Property))
        //    {
        //      _oldValues[setter.Property] = oldValue;
        //    }
        //    else
        //    {
        //      _oldValues.Add(setter.Property, oldValue);
        //    }
        //    _element.SetValue(setter.Property, setter.Value);
        //  }
        //  else
        //  {
        //    if (_oldValues.ContainsKey(setter.Property))
        //    {
        //      _element.SetValue(setter.Property, _oldValues[setter.Property]);
        //    }
        //  }
        //}
      }
    }

    protected virtual object ConvertValue(Type t)
    {
      object o = null;
      try
      {
        if (t == typeof(bool))
        {
          o = ConvertValueToBool();
        }
        else if (t == typeof(int))
        {
          o = ConvertValueToInt();
        }
        else if (t == typeof(double))
        {
          o = ConvertValueToDouble();
        }
      }
      catch (Exception e)
      {
        string msg = string.Format("Cannot convert '{0}' to type '{1}'", Value, t);
        throw new InvalidOperationException(msg, e);
      }
      if (o == null)
      {
        throw new NotImplementedException();
      }
      return o;
    }

    private double ConvertValueToDouble()
    {
      return Double.Parse(Value, NumberFormatInfo.InvariantInfo);
    }

    private int ConvertValueToInt()
    {
      return Int32.Parse(Value);
    }

    private bool ConvertValueToBool()
    {
      bool b;
      if (Value == "True")
      {
        b = true;
      }
      else if (Value == "False")
      {
        b = false;
      }
      else
      {
        throw new InvalidOperationException();
      }
      return b;
    }

    #endregion Property (Attached Property)

    public string Value { get; set; }

    public bool Result { get; private set; }

    public void Setup(FrameworkElement element, Binding binding)
    {
      SetBindingListener(element, this);
      element.SetBinding(BindingListener.PropertyProperty, binding);
    }
  }
}