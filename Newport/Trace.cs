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

    public static void WriteLine(string s)
    {
      Debug.WriteLine(string.Format("{0}: {1}", DateTime.Now, s));
    }
  }
}