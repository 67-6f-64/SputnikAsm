using System;
using SputnikAsm.LCollections;

namespace SputnikAsm.LUtils
{
    public static class ACharUtils
    {
        #region Range
        public static ACharArray Range(Char first, Char second)
        {
            var ret = new ACharArray();
            if (first == second)
                return ret;
            var f = Math.Min(first, second);
            var e = Math.Max(first, second);
            for (var i = f; i <= e; i++)
                ret.Add((Char)i);
            return ret;
        }
        #endregion
        #region InRange
        public static Boolean InRange(Char needle, Char first, Char second)
        {
            if (first == second)
                return needle == first;
            var n = (UInt16)needle;
            var f = Math.Min(first, second);
            var e = Math.Max(first, second);
            for (var i = f; i <= e; i++)
            {
                if (n == i)
                    return true;
            }
            return false;
        }
        #endregion
    }
}
