using System;

namespace SputnikAsm.LUtils
{
    public static class ABitUtils
    {
        #region GetBitCount
        public static int GetBitCount(UInt64 value)
        {
            var result = 0;
            while (value > 0)
            {
                if ((value % 2) == 1)
                    result += 1;
                value >>= 1;
            }
            return result;
        }
        #endregion
        #region GetBit
        public static int GetBit(int bitNr, UInt64 bt)
        {
            var result = (bt >> bitNr) & 1;
            return (int)result;
        }
        #endregion
        #region SetBit
        public static void SetBit(int bitNr, ref UInt64 bt, int state)
        {
            bt &= (UInt64)(~(1 << bitNr));
            bt |= (UInt32)(state << bitNr);
        }
        public static void SetBit(int bitNr, ref UInt32 bt, int state)
        {
            bt &= (UInt32)(~(1 << bitNr));
            bt |= (UInt32)(state << bitNr);
        }
        public static void SetBit(int bitNr, ref Byte bt, int state)
        {
            var d = (UInt32)bt;
            SetBit(bitNr, ref d, state);
            bt = (Byte)d;
        }
        #endregion
    }
}
