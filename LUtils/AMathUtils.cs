using System;

namespace SputnikAsm.LUtils
{
    public static class AMathUtils
    {
        #region InRange
        public static Boolean InRange(Byte value, Byte min, Byte max)
        {
            return value >= min && value <= max;
        }
        public static Boolean InRange(UInt32 value, UInt32 min, UInt32 max)
        {
            return value >= min && value <= max;
        }
        public static Boolean InRange(UInt64 value, UInt64 min, UInt64 max)
        {
            return value >= min && value <= max;
        }
        public static Boolean InRange(Single value, Single min, Single max)
        {
            return value >= min && value <= max;
        }
        public static Boolean InRange(Double value, Double min, Double max)
        {
            return value >= min && value <= max;
        }
        public static Boolean InRange(UIntPtr value, UIntPtr min, UIntPtr max)
        {
            var v = value.ToUInt64();
            var mn = min.ToUInt64();
            var mx = max.ToUInt64();
            return v >= mn && v <= mx;
        }
        #endregion
    }
}
