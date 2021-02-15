using System;
using SputnikAsm.LCollections;

namespace SputnikAsm.LBinary
{
    /* VEX 2 byte form */
    /*  7                           0    */
    /* +---+---+---+---+---+---+---+---+ */
    /* |~R |     ~vvvv     | L |   pp  | */
    /* +---+---+---+---+---+---+---+---+ */
    /*
	~R			1 bit	This 1 - bit value is an 'inverted' extension to the MODRM.reg field.The inverse of REX.R.See Registers.
	~X			1 bit	This 1 - bit value is an 'inverted' extension to the SIB.index field.The inverse of REX.X.See 64 - bit addressing.
	~B			1 bit	This 1 - bit value is an 'inverted' extension to the MODRM.rm field or the SIB.base field.The inverse of REX.B.See 64 - bit addressing.
	map_select	5 bits	Specifies the opcode map to use.
	W / E		1 bit	For integer instructions : when 1, a 64 - bit operand size is used; otherwise, when 0, the default operand size is used(equivalent with REX.W).For non - integer instructions, this bit is a general opcode extension bit.
	~vvvv		4 bits	An additional operand for the instruction.The value of the XMM or YMM register (see Registers) is 'inverted'.
	L			1 bit	When 0, a 128 - bit vector lengh is used.Otherwise, when 1, a 256 - bit vector length is used.
	pp			2 bits	Specifies an implied mandatory prefix for the opcode.
			  Value	Implied mandatory prefix
			  b00		none
			  b01		0x66
			  b10		0xF3
			  b11		0xF2
	*/
    // vexBytes[0] = 0xc5;
    // vexBytes[1] = (VEXpp) | (VEXl << 2) | ((VEXvvvv & 0xf) << 3) | (VEXr << 7);
    public class AVex2Byte : AMultiByte
    {
        #region Constants
        private const int SIZE = 2;
        #endregion
        #region Pp
        public Byte Pp
        {
            get => (Byte)Get(0, 2, true);
            set
            {
                Set(0, 2, true, value);
                Apply();
            }
        }
        #endregion
        #region L
        public Byte L
        {
            get => (Byte)Get(2, 1, true);
            set
            {
                Set(2, 1, true, value);
                Apply();
            }
        }
        #endregion
        #region Vvvv
        public Byte Vvvv
        {
            get => (Byte)Get(3, 4, true);
            set
            {
                Set(3, 4, true, value);
                Apply();
            }
        }
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
        #region Constructor
        public AVex2Byte()
            : base(SIZE)
        {
        }
        public AVex2Byte(AByteArray bytes, int index)
            : base(SIZE, bytes, index)
        {
        }
        public AVex2Byte(IntPtr bytesPointer)
            : base(SIZE, bytesPointer, 0)
        {
        }
        public AVex2Byte(IntPtr bytesPointer, int index)
            : base(SIZE, bytesPointer, index)
        {
        }
        #endregion
    }
}
