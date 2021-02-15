using System;
using SputnikAsm.LAssembler.LEnums;

namespace SputnikAsm.LAssembler
{
    public class AOpCode
    {
        public string mnemonic;
        public AExtraOpCode opcode1, opcode2;
        public AParam paramtype1, paramtype2, paramtype3;
        public byte bytes;
        public byte bt1, bt2, bt3;
        public Boolean signed;
        public Boolean norexw;
        public Boolean invalidin64bit;
        public Boolean invalidin32bit;
        // RexPrefixOffset: byte; //if specified specifies which byte should be used for the rexw (e.g f3 before rex )
        public AOpCode()
        {
            mnemonic = "";
            opcode1 = AExtraOpCode.eo_none;
            opcode2 = AExtraOpCode.eo_none;
            paramtype1 = AParam.par_noparam;
            paramtype2 = AParam.par_noparam;
            paramtype3 = AParam.par_noparam;
            bytes = 0;
            bt1 = 0;
            bt2 = 0;
            bt3 = 0;
            signed = false;
            norexw = false;
            invalidin64bit = false;
            invalidin32bit = false;
        }
    }
}
