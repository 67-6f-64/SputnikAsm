using System;

namespace Tack.LAutoAssembler
{
    public class topcode
    {
        public string mnemonic;
        public textraopcode opcode1, opcode2;
        public tparam paramtype1, paramtype2, paramtype3;
        public byte bytes;
        public byte bt1, bt2, bt3;
        public Boolean signed;
        public Boolean norexw;
        public Boolean invalidin64bit;
        public Boolean invalidin32bit;
        // RexPrefixOffset: byte; //if specified specifies which byte should be used for the rexw (e.g f3 before rex )
        public topcode()
        {
            mnemonic = "";
            opcode1 = textraopcode.eo_none;
            opcode2 = textraopcode.eo_none;
            paramtype1 = tparam.par_noparam;
            paramtype2 = tparam.par_noparam;
            paramtype3 = tparam.par_noparam;
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
