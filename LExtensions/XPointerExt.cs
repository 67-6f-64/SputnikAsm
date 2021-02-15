using System;

namespace SputnikAsm.LExtensions
{
    public static class XPointerExt
    {
        #region ToIntPtr
        public static IntPtr ToIntPtr(this UIntPtr p)
        {
            return (IntPtr)p.ToUInt64();
        }
        #endregion
        #region ToUIntPtr
        public static UIntPtr ToUIntPtr(this IntPtr p)
        {
            return (UIntPtr)p.ToInt64();
        }
        #endregion
    }
}
