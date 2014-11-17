using System;
using System.Collections;
using System.Collections.Generic;
#if UNIVERSAL
using Windows.UI;
#else
using System.Windows.Media;
#endif

namespace Newport
{
  public static class RandomData
  {
    private static readonly Random _random;
    private static readonly ListRandomizer<string> _firstNames;
    private static readonly ListRandomizer<string> _surNames;

    static RandomData()
    {
      _random = new Random();
      _firstNames = new ListRandomizer<string>(new List<string>
      {
        "Wolfgang",
        "Stefanie",
        "Anneliese",
        "Franz",
        "Judith",
        "Anneliese",
        "Eva",
        "Hans",
        "Mario",
        "Bill",
        "Dexter"
      }) { ExcludeResults = false };
      _firstNames.ExcludeResults = false;
      _surNames = new ListRandomizer<string>(new List<string>
      {
        "Ziegler",
        "Maureder",
        "Gates",
        "Morgan"
      }) { ExcludeResults = false };
    }

    public static void RandomizeListItems(IList list)
    {
      for (var index = 0; index < list.Count; index++)
      {
        var swapIndex = _random.Next(list.Count);
        var swap = list[index];
        list[index] = list[swapIndex];
        list[swapIndex] = swap;
      }
    }

    public static T GetRandomListItem<T>(List<T> list)
    {
      return list[GetInt(0, list.Count)];
    }

    public static DateTime GetRandomDate(int minYear, int maxYear)
    {
      return new DateTime(
        _random.Next(minYear, maxYear + 1),
        _random.Next(1, 13),
        _random.Next(1, 29));
    }

    public static DateTime GetRandomTime()
    {
      return new DateTime(
        DateTime.Now.Year,
        DateTime.Now.Month,
        DateTime.Now.Day,
        _random.Next(0, 24),
        _random.Next(0, 60),
        _random.Next(0, 60));
    }

    public static bool GetBoolean()
    {
      return (GetInt(0, 100) < 50);
    }

    public static double GetDouble()
    {
      return _random.NextDouble();
    }

    public static double GetDouble(double maxValue)
    {
      return GetDouble(0, maxValue);
    }

    public static double GetDouble(double minValue, double maxValue)
    {
      return minValue + (_random.NextDouble() * (maxValue - minValue));
    }

    public static int GetInt()
    {
      return _random.Next();
    }

    public static int GetInt(int maxValue)
    {
      return _random.Next(maxValue);
    }

    public static int GetInt(int minValue, int maxValue)
    {
      return _random.Next(minValue, maxValue);
    }

    public static string DummyText
    {
      get
      {
        return "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";
      }
    }

    public static string GetRandomName()
    {
      return GetRandomFirstName() + " " + GetRandomSurName();
    }

    public static string GetRandomFirstName()
    {
      return _firstNames.Next();
    }

    public static string GetRandomSurName()
    {
      return _surNames.Next();
    }

    public static Color GetRandomColor()
    {
      return Color.FromArgb(
        255,
        (byte)_random.Next(256),
        (byte)_random.Next(256),
        (byte)_random.Next(256));
    }

    public static char GetRandomPrintableCharacter()
    {
      return (char)_random.Next(33, 126);
    }
  }
}