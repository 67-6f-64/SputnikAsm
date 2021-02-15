using System;
using SputnikAsm.LCollections;

namespace SputnikAsm.LBinary
{
    /* VEX 3 byte form */
    /*   7                           0     7                           0    */
    /* +---+---+---+---+---+---+---+---+   +---+---+---+---+---+---+---+---+*/
    /* |~R |~X |~B | map_select        |   |W/E|    ~vvvv      | L |   pp  |*/
    /* +---+---+---+---+---+---+---+---+   +---+---+---+---+---+---+---+---+*/
    // A VEX instruction whose values for certain fields are VEX.~X == 1, VEX.~B == 1, VEX.W/E == 0 and map_select == b00001 may be encoded using the two byte VEX.
    // vexBytes[0] = 0xc4;
    // vexBytes[1] = (VEXr << 7) | (VEXx << 6) | (VEXb << 5) | (VEXmmmmm);
    // vexBytes[2] = (VEXwe << 7) | ((VEXvvvv & 0xf) << 3) | (VEXl << 2) | (VEXpp);
    public class AVex3Byte : AMultiByte
    {
        #region Constants
        private const int SIZE = 2;
        #endregion
        #region R
        public Byte R
        {
            get => (Byte)Get(7, 1, true);
            set
            {
                Set(7, 1, true, value);
                Apply();
            }
        }
        #endregion
        #region X
        public Byte X
        {
            get => (Byte)Get(6, 1, true);
            set
            {
                Set(6, 1, true, value);
                Apply();
            }
        }
        #endregion
        #region B
        public Byte B
        {
            get => (Byte)Get(5, 1, true);
            set
            {
                Set(5, 1, true, value);
                Apply();
            }
        }
        #endregion
        #region Mmmmm
        public Byte Mmmmm
        {
            get => (Byte)Get(0, 5, true);
            set
            {
                Set(0, 5, true, value);
                Apply();
            }
        }
        #endregion
        #region W
        public Byte W
        {
            get => (Byte)Get(15, 1, true);
            set
            {
                Set(15, 1, true, value);
                Apply();
            }
        }
        #endregion
        #region Vvvv
        public Byte Vvvv
        {
            get => (Byte)Get(11, 4, true);
            set
            {
                Set(11, 4, true, value);
                Apply();
            }
        }
        #endregion
        #region L
        public Byte L
        {
            get => (Byte)Get(10, 1, true);
            set
            {
                Set(10, 1, true, value);
                Apply();
            }
        }
        #endregion
        #region Pp
        public Byte Pp
        {
            get => (Byte)Get(8, 2, true);
            set
            {
                Set(8, 2, true, value);
                Apply();
            }
        }
        #endregion
        #region Constructor
        public AVex3Byte()
            : base(SIZE)
        {
        }
        public AVex3Byte(AByteArray bytes, int index)
            : base(SIZE, bytes, index)
        {
        }
        public AVex3Byte(IntPtr bytesPointer)
            : base(SIZE, bytesPointer, 0)
        {
        }
        public AVex3Byte(IntPtr bytesPointer, int index)
            : base(SIZE, bytesPointer, index)
        {
        }
        #endregion
    }
}
