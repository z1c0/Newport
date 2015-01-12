using System;
using System.Linq;
using System.Windows.Input;

namespace Newport
{
  public static class CommandManager
  {
    private static readonly WeakList<ITriggerable> _commands = new WeakList<ITriggerable>();

    public static void InvalidateRequerySuggested()
    {
      Commands.Where(c => c != null).ForEach(c => c.Trigger());
      Commands.Purge();
    }

    internal static WeakList<ITriggerable> Commands  {get { return _commands; } }
  }
}