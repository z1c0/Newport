﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newport
{
  public static class CommandManager
  {
    public static event EventHandler RequerySuggested;

    public static void InvalidateRequerySuggested()
    {
      var handler = RequerySuggested;
      if (handler != null)
      {
        handler(null, EventArgs.Empty);
      }
    }
  }
}