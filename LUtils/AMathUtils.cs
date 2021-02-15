using System;

namespace SputnikAsm.LUtils
{
    public static class AMathUtils
    {
        #region InRangeX
        public static Boolean InRangeX(IntPtr value, IntPtr min, IntPtr max)
        {
            var v = value.ToInt64();
            var mn = min.ToInt64();
            var mx = max.ToInt64();
            return v >= mn && v <= mx;
        }
        #endregion
        #region InRangeQ
        public static Boolean InRangeQ(UInt64 value, UInt64 min, UInt64 max)
        {
            return value >= min && value <= max;
        }
        #endregion
    }
}
