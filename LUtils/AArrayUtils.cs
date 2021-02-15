using System;
using System.Linq;

namespace SputnikAsm.LUtils
{
    public class AArrayUtils
    {
        #region InArray
        public static Boolean InArray(Byte needle, params Byte[] haystack)
        {
            if (haystack == null || haystack.Length <= 0)
                return false;
            return haystack.Any(c => needle == c);
        }
        public static Boolean InArray(Char needle, params Char[] haystack)
        {
            if (haystack == null || haystack.Length <= 0)
                return false;
            return haystack.Any(c => needle == c);
        }
        #endregion
    }
}
