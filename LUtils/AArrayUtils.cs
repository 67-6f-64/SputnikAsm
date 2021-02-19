using System;
using System.Linq;
using Sputnik.LBinary;
using Sputnik.LMarshal;
using Sputnik.LUtils;
using SputnikAsm.LCollections;

namespace SputnikAsm.LUtils
{
    public static class AArrayUtils
    {
        #region CopyMemory
        public static int CopyMemory(UBytePtr dest, int destStart, UBytePtr src, int size)
        {
            return UArrayUtils.CopyMemory(dest.ToIntPtr(), destStart, src.ToIntPtr(), 0, size);
        }
        public static int CopyMemory(UBytePtr dest, UBytePtr src, int size)
        {
            return UArrayUtils.CopyMemory(dest.ToIntPtr(), 0, src.ToIntPtr(), 0, size);
        }
        public static int CopyMemory(AByteArray dest, int destStart, AByteArray src, int size)
        {
            return UArrayUtils.CopyMemory(dest.Buffer, destStart, src.Buffer, 0, size);
        }
        public static int CopyMemory(AByteArray dest, AByteArray src, int size)
        {
            return UArrayUtils.CopyMemory(dest.Buffer, 0, src.Buffer, 0, size);
        }
        #endregion
    }
}
