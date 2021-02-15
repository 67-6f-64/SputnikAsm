using System;
using SputnikAsm.LAssembler.LEnums;

namespace SputnikAsm.LAssembler
{
    public class AOpCode
    {
        public String Mnemonic;
        public AExtraOpCode OpCode1, OpCode2;
        public AParam ParamType1, ParamType2, ParamType3;
        public Byte Bytes;
        public Byte Bt1, Bt2, Bt3;
        public Boolean Signed;
        public Boolean NorExw;
        public Boolean InvalidIn64Bit;
        public Boolean InvalidIn32Bit;
        // RexPrefixOffset: byte; //if specified specifies which byte should be used for the rexw (e.g f3 before rex )
        public AOpCode()
        {
            Mnemonic = "";
            OpCode1 = AExtraOpCode.eo_none;
            OpCode2 = AExtraOpCode.eo_none;
            ParamType1 = AParam.par_noparam;
            ParamType2 = AParam.par_noparam;
            ParamType3 = AParam.par_noparam;
            Bytes = 0;
            Bt1 = 0;
            Bt2 = 0;
            Bt3 = 0;
            Signed = false;
            NorExw = false;
            InvalidIn64Bit = false;
            InvalidIn32Bit = false;
        }
    }
}
