using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
#if NETFX_CORE
using Windows.UI.Xaml;
using System.Reflection;
#endif

namespace Newport
{
  internal class ViewModelInformation
  {
    private ViewModelBase _instance;

    internal Type Type { get; set; }

    internal bool IsSingleton { get; set; }

    internal ViewModelBase GetInstance()
    {
      ViewModelBase viewmodel = null;
      if (IsSingleton)
      {
        if (_instance == null)
        {
          _instance = (ViewModelBase)Activator.CreateInstance(Type);
        }
        viewmodel = _instance;
      }
      else
      {
        viewmodel = (ViewModelBase)Activator.CreateInstance(Type);
      }
      return viewmodel;
    }
  }

  [AttributeUsage(AttributeTargets.Class)]
  public class ExportedViewModelAttribute : Attribute
  {
    public ExportedViewModelAttribute(string key)
    {
      Key = key;
    }

    public ExportedViewModelAttribute()
      : this(null)
    {
    }

    public string Key { get; set; }

    public bool IsSingleton { get; set; }
  }

  public class ViewModelProvider
  {
    private static Dictionary<string, ViewModelInformation> _viewModels;
    private static ViewModelProvider _default;

    static ViewModelProvider()
    {
      RegisterViewModels();
    }

    public static ViewModelProvider Default
    {
      get
      {
        if (_default == null)
        {
          _default = new ViewModelProvider();
        }
        return _default;
      }
    }

    public ViewModelBase this[string key]
    {
      get
      {
        ViewModelBase viewModel = null;
        if (_viewModels.ContainsKey(key))
        {
          viewModel = _viewModels[key].GetInstance();
        }
        return viewModel;
      }
    }

    public T Get<T>() where T : ViewModelBase, new()
    {
      T viewModel = null;
      if (ViewModelBase.IsDesignMode)
      {
        viewModel = new T();
      }
      else
      {
        foreach (var entry in _viewModels)
        {
          if (entry.Value.Type == typeof(T))
          {
            viewModel = (T)entry.Value.GetInstance();
            break;
          }
        }
      }
      return viewModel;
    }

    private static void RegisterViewModels()
    {
      _viewModels = new Dictionary<string, ViewModelInformation>();
#if NETFX_CORE
      var types = Application.Current.GetType().GetTypeInfo().Assembly.ExportedTypes;
      foreach (var t in types)
      {
        var attributes = t.GetTypeInfo().GetCustomAttributes<ExportedViewModelAttribute>();
        foreach (var a in attributes)
        {
          var key = a.Key ?? t.Name;
          _viewModels.Add(key, new ViewModelInformation()
          {
            Type = t,
            IsSingleton = a.IsSingleton
          });
        }
      }
#else
      var types = Application.Current.GetType().Assembly.GetTypes();
      foreach (var t in types)
      {
        var attributes = t.GetCustomAttributes(true);
        foreach (var a in attributes)
        {
          var exportedViewModelAttribute = a as ExportedViewModelAttribute;
          if (exportedViewModelAttribute != null)
          {
            var key = exportedViewModelAttribute.Key ?? t.Name;
            _viewModels.Add(key, new ViewModelInformation()
            {
              Type = t,
              IsSingleton = exportedViewModelAttribute.IsSingleton
            });
          }
        }
      }
#endif
    }
  }
}