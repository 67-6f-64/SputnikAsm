using System;
using SputnikAsm.LAssembler.LEnums;

namespace SputnikAsm.LAssembler
{
    public class AOpCode
    {
        public String Mnemonic;
        public AExtraOpCode OpCode1, OpCode2;
        public AParam ParamType1, ParamType2, ParamType3, ParamType4;
        public Byte Bytes;
        public Byte Bt1, Bt2, Bt3, Bt4;
        public Boolean Signed;
        public Boolean W0;
        public Boolean W1;
        public Boolean InvalidIn64Bit;
        public Boolean InvalidIn32Bit;
        public Boolean CanDoAddressSwitch; //does it support the 0x67 address switch (e.g lea)
        public Boolean DefaultType;
        public Boolean HasVex;
        public Byte VexL;
        public AVexOpCodeExtension VexOpCodeExtension;
        public AVexLeadingOpCode VexLeadingOpCode;
        public int VexExtraParam;
        // RexPrefixOffset: byte; //if specified specifies which byte should be used for the rexw (e.g f3 before rex )
        //paramencoding: TParamEncoding;
        public AOpCode()
        {
            Mnemonic = "";
            OpCode1 = AExtraOpCode.None;
            OpCode2 = AExtraOpCode.None;
            ParamType1 = AParam.None;
            ParamType2 = AParam.None;
            ParamType3 = AParam.None;
            ParamType4 = AParam.None;
            Bytes = 0;
            Bt1 = 0;
            Bt2 = 0;
            Bt3 = 0;
            Bt4 = 0;
            Signed = false;
            W0 = false;
            W1 = false;
            InvalidIn64Bit = false;
            InvalidIn32Bit = false;
            CanDoAddressSwitch = false;
            DefaultType = false;
            HasVex = false;
            VexL = 0x00;
            VexOpCodeExtension = AVexOpCodeExtension.None;
            VexLeadingOpCode = AVexLeadingOpCode.None;
            VexExtraParam = 0;
        }
    }
}
