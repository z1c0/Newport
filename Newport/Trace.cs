using System;
using System.Diagnostics;

namespace Newport
{
  public static class Trace
  {
    public static void WriteLine(Exception e)
    {
      Debug.WriteLine(e);
    }
  }
}