using System;

namespace SputnikAsm.LUtils
{
    public static class AMathUtils
    {
        #region InRangeX
        public static Boolean InRangeX(UIntPtr value, UIntPtr min, UIntPtr max)
        {
            var v = value.ToUInt64();
            var mn = min.ToUInt64();
            var mx = max.ToUInt64();
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
