using System;
using System.Collections.Generic;
using System.Linq;
#if UNIVERSAL
using Windows.UI.Xaml.Media;
using Windows.UI;
#else
using System.Windows.Media;
#endif

namespace Newport
{
  public class ColorTheme
  {
    public SolidColorBrush Color1 { get; set; }

    public SolidColorBrush Color2 { get; set; }

    public string Name { get; set; }

    public override string ToString()
    {
      return Name;
    }

    public static ColorTheme Green { get { return FromColor(Colors.Green, "Green"); } }

    public static ColorTheme Blue { get { return FromColor(Colors.Blue, "Blue"); } }

    public static ColorTheme Red { get { return FromColor(Colors.Red, "Red"); } }

    public static ColorTheme Yellow { get { return FromColor(Colors.Yellow, "Yellow"); } }

    public static ColorTheme Orange { get { return FromColor(Colors.Orange, "Orange"); } }

    public static ColorTheme Purple { get { return FromColor(Colors.Purple, "Purple"); } }

    private static ColorTheme FromColor(Color color, string name)
    {
      return new ColorTheme
      {
        Color1 = new SolidColorBrush(color),
        Color2 = new SolidColorBrush(color),
        Name = name
      };
    }
  }

  public class ColorThemeViewModel : ViewModelBase
  {
    public event EventHandler ThemeSelected;

    private ColorTheme _selectedTheme;

    public ColorThemeViewModel()
    {
      Themes = new List<ColorTheme>
      {
        ColorTheme.Green,
        ColorTheme.Blue,
        ColorTheme.Yellow,
        ColorTheme.Orange,
        ColorTheme.Red,
        ColorTheme.Purple,
      };
    }

    public void InitTheme(string name)
    {
      SelectedTheme = GetColorTheme(name);
    }

    public List<ColorTheme> Themes { get; set; }

    public ColorTheme SelectedTheme
    {
      get
      {
        return _selectedTheme ?? Themes[0];
      }
      set
      {
        if (SetProperty(ref _selectedTheme, value, "SelectedTheme"))
        {
          if (ThemeSelected != null)
          {
            ThemeSelected(this, EventArgs.Empty);
          }
        }
      }
    }

    private ColorTheme GetColorTheme(string name)
    {
      var theme =
        (from t in Themes
          where t.Name == name
          select t).FirstOrDefault() ?? Themes[0];
      return theme;
    }
  }
}