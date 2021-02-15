using System;
using System.Linq;
using Sputnik.LBinary;
using Sputnik.LMarshal;
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
        public static int CopyMemory(UBytePtr dest, int destStart, UBytePtr src, int size)
        {
            return CopyMemory(dest.ToIntPtr(), destStart, src.ToIntPtr(), 0, size);
        }
        public static int CopyMemory(UBytePtr dest, UBytePtr src, int size)
        {
            return CopyMemory(dest.ToIntPtr(), 0, src.ToIntPtr(), 0, size);
        }
        public static int CopyMemory(AByteArray dest, int destStart, AByteArray src, int size)
        {
            return CopyMemory(dest.Buffer, destStart, src.Buffer, 0, size);
        }
        public static int CopyMemory(AByteArray dest, AByteArray src, int size)
        {
            return CopyMemory(dest.Buffer, 0, src.Buffer, 0, size);
        }
        public static int CopyMemory<T>(T[] dest, T[] src, int size)
        {
            return CopyMemory(dest, 0, src, 0, size);
        }
        public static int CopyMemory<T>(T[] dest, int destStart, T[] src, int srcStart, int size)
        {
            var d = dest;
            var dS = destStart;
            var s = src;
            var sS = srcStart;
            var i = 0;
            while (size-- > 0 && i < d.Length && i < s.Length && dS + i < d.Length && sS + i < s.Length)
            {
                d[dS + i] = s[sS + i];
                i++;
            }
            return i;
        }
        public static unsafe int CopyMemory(Byte* dest, int destStart, Byte* src, int srcStart, int size)
        {
            var d = dest;
            var dS = destStart;
            var s = src;
            var sS = srcStart;
            var i = 0;
            while (size-- > 0)
            {
                d[dS + i] = s[sS + i];
                i++;
            }
            return i;
        }
        public static int CopyMemory(IntPtr dest, int destStart, IntPtr src, int srcStart, int size)
        {
            var d = dest;
            var dS = destStart;
            var s = src;
            var sS = srcStart;
            var i = 0;
            while (size-- > 0)
            {
                d.WriteByte(s.ReadByte(sS + i), dS + i);
                i++;
            }
            return i;
        }
        #endregion
    }
}
