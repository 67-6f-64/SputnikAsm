using System;
using System.Linq;
using SputnikAsm.LCollections;

namespace SputnikAsm.LUtils
{
    public static class AArrayUtils
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
        #region CopyMemory
        public static int CopyMemory(AByteArray dest, int destStart, AByteArray src, int size)
        {
            return CopyMemory(dest.Raw, destStart, src.Raw, 0, size);
        }
        public static int CopyMemory(AByteArray dest, AByteArray src, int size)
        {
            return CopyMemory(dest.Raw, 0, src.Raw, 0, size);
        }
        public static int CopyMemory(Byte[] dest, Byte[] src, int size)
        {
            return CopyMemory(dest, 0, src, 0, size);
        }
        public static int CopyMemory(Byte[] dest, int destStart, Byte[] src, int srcStart, int size)
        {
            var d = dest;
            var s = src;
            var i = 0;
            while (size-- > 0 && i < dest.Length && i < src.Length && destStart + i < dest.Length && srcStart + i < src.Length)
            {
                d[destStart + i] = s[srcStart + i];
                i++;
            }
            return i;
        }
        #endregion
    }
}
