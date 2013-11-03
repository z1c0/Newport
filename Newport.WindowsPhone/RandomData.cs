using System;
using System.Collections;
using System.Collections.Generic;
#if NETFX_CORE
using Windows.UI;
#else
using System.Windows.Media;
#endif

namespace Newport
{
  public class RandomData
  {
    private static Random _random;
    private ListRandomizer<string> _firstNames;
    private ListRandomizer<string> _surNames;

    static RandomData()
    {
      _random = new Random();
    }

    public RandomData()
    {
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

    public void RandomizeListItems(IList list)
    {
      for (int index = 0; index < list.Count; index++)
      {
        var swapIndex = _random.Next(list.Count);
        var swap = list[index];
        list[index] = list[swapIndex];
        list[swapIndex] = swap;
      }
    }

    public T GetRandomListItem<T>(List<T> list)
    {
      return list[GetInt(0, list.Count)];
    }

    public DateTime GetRandomDate(int minYear, int maxYear)
    {
      return new DateTime(
        _random.Next(minYear, maxYear + 1),
        _random.Next(1, 13),
        _random.Next(1, 29));
    }

    public DateTime GetRandomTime()
    {
      return new DateTime(
        DateTime.Now.Year,
        DateTime.Now.Month,
        DateTime.Now.Day,
        _random.Next(0, 24),
        _random.Next(0, 60),
        _random.Next(0, 60));
    }

    public bool GetBoolean()
    {
      return (GetInt(0, 100) < 50);
    }

    public double GetDouble()
    {
      return _random.NextDouble();
    }

    public double GetDouble(double minValue, double maxValue)
    {
      return minValue + (_random.NextDouble() * (maxValue - minValue));
    }

    public int GetInt()
    {
      return _random.Next();
    }

    public int GetInt(int maxValue)
    {
      return _random.Next(maxValue);
    }

    public int GetInt(int minValue, int maxValue)
    {
      return _random.Next(minValue, maxValue);
    }

    public string DummyText
    {
      get
      {
        return "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";
      }
    }

    public string GetRandomName()
    {
      return GetRandomFirstName() + " " + GetRandomSurName();
    }

    public string GetRandomFirstName()
    {
      return _firstNames.Next();
    }

    public string GetRandomSurName()
    {
      return _surNames.Next();
    }

    public Color GetRandomColor()
    {
      return Color.FromArgb(
        255,
        (byte)_random.Next(256),
        (byte)_random.Next(256),
        (byte)_random.Next(256));
    }

    public char GetRandomPrintableCharacter()
    {
      return (char)_random.Next(33, 126);
    }
  }
}