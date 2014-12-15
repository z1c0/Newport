using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Newport
{
  public class EnumList<T> : List<T>
  {
    public EnumList()
    {
#if UNIVERSAL
      AddRange(from f in typeof(T).GetRuntimeFields()
               where f.IsStatic
               select (T)f.GetValue(null));
#else
      AddRange(from f in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static)
               select (T)f.GetValue(null));
#endif
    }
  }
}