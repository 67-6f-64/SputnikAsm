using SputnikAsm.LAssembler.LEnums;

namespace SputnikAsm.LAssembler
{
    public static class AOpCodes
    {
        #region GetOpCodes
        public static AOpCode[] GetOpCodes()
        {
            return new []
            {
                new AOpCode { Mnemonic = "AAA", OpCode1 = AExtraOpCode.eo_none, OpCode2 = AExtraOpCode.eo_none, ParamType1 = AParam.par_noparam, ParamType2 = AParam.par_noparam, ParamType3 = AParam.par_noparam, Bytes = 1, Bt1 = 0x37, Bt2 = 0, Bt3 = 0 }, //no param
                new AOpCode { Mnemonic = "AAD", OpCode1 = AExtraOpCode.eo_none, OpCode2 = AExtraOpCode.eo_none, ParamType1 = AParam.par_noparam, ParamType2 = AParam.par_noparam, ParamType3 = AParam.par_noparam, Bytes = 2, Bt1 = 0xd5, Bt2 = 0x0a, Bt3 = 0 },
                new AOpCode { Mnemonic = "AAD", OpCode1 = AExtraOpCode.eo_ib, OpCode2 = AExtraOpCode.eo_none, ParamType1 = AParam.par_imm8, ParamType2 = AParam.par_noparam, ParamType3 = AParam.par_noparam, Bytes = 1, Bt1 = 0xd5, Bt2 = 0, Bt3 = 0 },
                new AOpCode { Mnemonic = "AAM", OpCode1 = AExtraOpCode.eo_none, ParamType1 = AParam.par_noparam, Bytes = 2, Bt1 = 0xd4, Bt2 = 0x0a },
                new AOpCode { Mnemonic = "AAM", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_imm8, Bytes = 1, Bt1 = 0xd4 },
                new AOpCode { Mnemonic = "AAS", OpCode1 = AExtraOpCode.eo_none, ParamType1 = AParam.par_noparam, Bytes = 1, Bt1 = 0x3F },
                new AOpCode { Mnemonic = "ADC", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_al, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x14 },
                new AOpCode { Mnemonic = "ADC", OpCode1 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_ax, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x15 },
                new AOpCode { Mnemonic = "ADC", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_eax, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x15 },
                new AOpCode { Mnemonic = "ADC", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x80 },//verified
                new AOpCode { Mnemonic = "ADC", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x81 },
                new AOpCode { Mnemonic = "ADC", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_id, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x81 },
                new AOpCode { Mnemonic = "ADC", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0x83, Signed = true },
                new AOpCode { Mnemonic = "ADC", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x83, Signed = true },
                new AOpCode { Mnemonic = "ADC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_r8, Bytes = 1, Bt1 = 0x10 },
                new AOpCode { Mnemonic = "ADC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x11 },
                new AOpCode { Mnemonic = "ADC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 1, Bt1 = 0x11 },
                new AOpCode { Mnemonic = "ADC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r8, ParamType2 = AParam.par_rm8, Bytes = 1, Bt1 = 0x12 },
                new AOpCode { Mnemonic = "ADC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x13 },
                new AOpCode { Mnemonic = "ADC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0x13 },

                new AOpCode { Mnemonic = "ADD", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_al, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x04 },
                new AOpCode { Mnemonic = "ADD", OpCode1 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_ax, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x05 },
                new AOpCode { Mnemonic = "ADD", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_eax, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x05 },
                new AOpCode { Mnemonic = "ADD", OpCode1 = AExtraOpCode.eo_reg0, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x80 },
                new AOpCode { Mnemonic = "ADD", OpCode1 = AExtraOpCode.eo_reg0, OpCode2 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x81 },
                new AOpCode { Mnemonic = "ADD", OpCode1 = AExtraOpCode.eo_reg0, OpCode2 = AExtraOpCode.eo_id, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x81 },
                new AOpCode { Mnemonic = "ADD", OpCode1 = AExtraOpCode.eo_reg0, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0x83, Signed = true },
                new AOpCode { Mnemonic = "ADD", OpCode1 = AExtraOpCode.eo_reg0, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x83, Signed = true },
                new AOpCode { Mnemonic = "ADD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 1, Bt1 = 0x01 },
                new AOpCode { Mnemonic = "ADD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x01 },
                new AOpCode { Mnemonic = "ADD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_r8, Bytes = 1, Bt1 = 0x00 },
                new AOpCode { Mnemonic = "ADD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0x03 },
                new AOpCode { Mnemonic = "ADD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x03 },
                new AOpCode { Mnemonic = "ADD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r8, ParamType2 = AParam.par_rm8, Bytes = 1, Bt1 = 0x02 },

                new AOpCode { Mnemonic = "ADDPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x58 }, //should be xmm1,xmm2/m128 but is also handled in all the others, in fact all other modrm types have it, hmmmmm....
                new AOpCode { Mnemonic = "ADDPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x58 }, //I gues all reg,reg/mem can be handled like this. (oh well, i"m too lazy to change the code)
                new AOpCode { Mnemonic = "ADDSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x58 },
                new AOpCode { Mnemonic = "ADDSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x58 },

                new AOpCode { Mnemonic = "AND", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_al, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x24 },
                new AOpCode { Mnemonic = "AND", OpCode1 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_ax, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x25 },
                new AOpCode { Mnemonic = "AND", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_eax, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x25 },
                new AOpCode { Mnemonic = "AND", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x80 },
                new AOpCode { Mnemonic = "AND", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x81 },
                new AOpCode { Mnemonic = "AND", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_id, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x81 },
                new AOpCode { Mnemonic = "AND", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0x83, Signed = true },
                new AOpCode { Mnemonic = "AND", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x83, Signed = true },
                new AOpCode { Mnemonic = "AND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_r8, Bytes = 1, Bt1 = 0x20 },
                new AOpCode { Mnemonic = "AND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x21 },
                new AOpCode { Mnemonic = "AND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 1, Bt1 = 0x21 },
                new AOpCode { Mnemonic = "AND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r8, ParamType2 = AParam.par_rm8, Bytes = 1, Bt1 = 0x22 },
                new AOpCode { Mnemonic = "AND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x23 },
                new AOpCode { Mnemonic = "AND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0x23 },

                new AOpCode { Mnemonic = "ANDNPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xff },
                new AOpCode { Mnemonic = "ANDNPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x55 },

                new AOpCode { Mnemonic = "ANDPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x54 },
                new AOpCode { Mnemonic = "ANDPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x54 },

                new AOpCode { Mnemonic = "ARPL", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 1, Bt1 = 0x63 }, //textraopcode.eo_reg means I just need to find the reg and address
                new AOpCode { Mnemonic = "BOUND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x62 },
                new AOpCode { Mnemonic = "BOUND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0x62 },
                new AOpCode { Mnemonic = "BSF", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xbc },
                new AOpCode { Mnemonic = "BSF", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xbc },
                new AOpCode { Mnemonic = "BSR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xbd },
                new AOpCode { Mnemonic = "BSR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xbd },
                new AOpCode { Mnemonic = "BSWAP", OpCode1 = AExtraOpCode.eo_prd, ParamType1 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc8 }, //textraopcode.eo_prd
                new AOpCode { Mnemonic = "BSWAP", OpCode1 = AExtraOpCode.eo_prw, ParamType1 = AParam.par_r16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xc8 }, //textraopcode.eo_prw

                new AOpCode { Mnemonic = "BT", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xa3 },
                new AOpCode { Mnemonic = "BT", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xa3 },
                new AOpCode { Mnemonic = "BT", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xba },
                new AOpCode { Mnemonic = "BT", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xba },

                new AOpCode { Mnemonic = "BTC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xbb },
                new AOpCode { Mnemonic = "BTC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xbb },
                new AOpCode { Mnemonic = "BTC", OpCode1 = AExtraOpCode.eo_reg7, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xba },
                new AOpCode { Mnemonic = "BTC", OpCode1 = AExtraOpCode.eo_reg7, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xba },

                new AOpCode { Mnemonic = "BTR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xb3 },
                new AOpCode { Mnemonic = "BTR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xb3 },
                new AOpCode { Mnemonic = "BTR", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xba },
                new AOpCode { Mnemonic = "BTR", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xba },

                new AOpCode { Mnemonic = "BTS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xab },
                new AOpCode { Mnemonic = "BTS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xab },
                new AOpCode { Mnemonic = "BTS", OpCode1 = AExtraOpCode.eo_reg5, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xba },
                new AOpCode { Mnemonic = "BTS", OpCode1 = AExtraOpCode.eo_reg5, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xba },
                
                //no 0x66 0xE8 because it makes the address it jumps to 16 bit
                new AOpCode { Mnemonic = "CALL", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 1, Bt1 = 0xe8 },
                //also no 0x66 0xff /2
                new AOpCode { Mnemonic = "CALL", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0xff, NorExw = true },
                new AOpCode { Mnemonic = "CBW", OpCode1 = AExtraOpCode.eo_none, ParamType1 = AParam.par_noparam, Bytes = 2, Bt1 = 0x66, Bt2 = 0x98 },
                new AOpCode { Mnemonic = "CDQ", Bytes = 1, Bt1 = 0x99 },
                new AOpCode { Mnemonic = "CDQE", Bytes = 2, Bt1 = 0x48, Bt2 = 0x98 },

                new AOpCode { Mnemonic = "CLC", Bytes = 1, Bt1 = 0xf8 },
                new AOpCode { Mnemonic = "CLD", Bytes = 1, Bt1 = 0xfc },
                new AOpCode { Mnemonic = "CLFLUSH", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xae },
                new AOpCode { Mnemonic = "CLI", Bytes = 1, Bt1 = 0xfa },
                new AOpCode { Mnemonic = "CLTS", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x06 },
                new AOpCode { Mnemonic = "CMC", Bytes = 1, Bt1 = 0xf5 },
                new AOpCode { Mnemonic = "CMOVA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x47 },
                new AOpCode { Mnemonic = "CMOVA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x47 },
                new AOpCode { Mnemonic = "CMOVAE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x43 },
                new AOpCode { Mnemonic = "CMOVAE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x43 },
                new AOpCode { Mnemonic = "CMOVB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x42 },
                new AOpCode { Mnemonic = "CMOVB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x42 },
                new AOpCode { Mnemonic = "CMOVBE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x46 },
                new AOpCode { Mnemonic = "CMOVBE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x46 },
                new AOpCode { Mnemonic = "CMOVC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x42 },
                new AOpCode { Mnemonic = "CMOVC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x42 },
                new AOpCode { Mnemonic = "CMOVE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x44 },
                new AOpCode { Mnemonic = "CMOVE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x44 },
                new AOpCode { Mnemonic = "CMOVG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x4f },
                new AOpCode { Mnemonic = "CMOVG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x4f },
                new AOpCode { Mnemonic = "CMOVGE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x4d },
                new AOpCode { Mnemonic = "CMOVGE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x4d },
                new AOpCode { Mnemonic = "CMOVL", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x4c },
                new AOpCode { Mnemonic = "CMOVL", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x4c },
                new AOpCode { Mnemonic = "CMOVLE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x4e },
                new AOpCode { Mnemonic = "CMOVLE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x4e },
                new AOpCode { Mnemonic = "CMOVNA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x46 },
                new AOpCode { Mnemonic = "CMOVNA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x46 },
                new AOpCode { Mnemonic = "CMOVNAE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x42 },
                new AOpCode { Mnemonic = "CMOVNAE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x42 },
                new AOpCode { Mnemonic = "CMOVNB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x43 },
                new AOpCode { Mnemonic = "CMOVNB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x43 },
                new AOpCode { Mnemonic = "CMOVNBE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x47 },
                new AOpCode { Mnemonic = "CMOVNBE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x47 },
                new AOpCode { Mnemonic = "CMOVNC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x43 },
                new AOpCode { Mnemonic = "CMOVNC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x43 },
                new AOpCode { Mnemonic = "CMOVNE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x45 },
                new AOpCode { Mnemonic = "CMOVNE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x45 },
                new AOpCode { Mnemonic = "CMOVNG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x4e },
                new AOpCode { Mnemonic = "CMOVNG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x4e },
                new AOpCode { Mnemonic = "CMOVNGE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x4c },
                new AOpCode { Mnemonic = "CMOVNGE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x4c },
                new AOpCode { Mnemonic = "CMOVNL", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x4d },
                new AOpCode { Mnemonic = "CMOVNL", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x4d },
                new AOpCode { Mnemonic = "CMOVNLE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x4f },
                new AOpCode { Mnemonic = "CMOVNLE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x4f },
                new AOpCode { Mnemonic = "CMOVNO", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x41 },
                new AOpCode { Mnemonic = "CMOVNO", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x41 },
                new AOpCode { Mnemonic = "CMOVNP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x4b },
                new AOpCode { Mnemonic = "CMOVNP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x4b },
                new AOpCode { Mnemonic = "CMOVNS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x49 },
                new AOpCode { Mnemonic = "CMOVNS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x49 },
                new AOpCode { Mnemonic = "CMOVNZ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x45 },
                new AOpCode { Mnemonic = "CMOVNZ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x45 },
                new AOpCode { Mnemonic = "CMOVO", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x40 },
                new AOpCode { Mnemonic = "CMOVO", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x40 },
                new AOpCode { Mnemonic = "CMOVP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x4a },
                new AOpCode { Mnemonic = "CMOVP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x4a },
                new AOpCode { Mnemonic = "CMOVPE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x4a },
                new AOpCode { Mnemonic = "CMOVPE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x4a },
                new AOpCode { Mnemonic = "CMOVPO", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x4b },
                new AOpCode { Mnemonic = "CMOVPO", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x4b },
                new AOpCode { Mnemonic = "CMOVS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x48 },
                new AOpCode { Mnemonic = "CMOVS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x48 },
                new AOpCode { Mnemonic = "CMOVZ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x44 },
                new AOpCode { Mnemonic = "CMOVZ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x44 },

                new AOpCode { Mnemonic = "CMP", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_al, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x3C }, //2 bytes
                new AOpCode { Mnemonic = "CMP", OpCode1 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_ax, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x3D }, //4 bytes
                new AOpCode { Mnemonic = "CMP", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_eax, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x3D }, //5 bytes
                new AOpCode { Mnemonic = "CMP", OpCode1 = AExtraOpCode.eo_reg7, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x80 },
                new AOpCode { Mnemonic = "CMP", OpCode1 = AExtraOpCode.eo_reg7, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0x83, Signed = true },
                new AOpCode { Mnemonic = "CMP", OpCode1 = AExtraOpCode.eo_reg7, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x83, Signed = true },

                new AOpCode { Mnemonic = "CMP", OpCode1 = AExtraOpCode.eo_reg7, OpCode2 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x81 },
                new AOpCode { Mnemonic = "CMP", OpCode1 = AExtraOpCode.eo_reg7, OpCode2 = AExtraOpCode.eo_id, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x81 },
                new AOpCode { Mnemonic = "CMP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_r8, Bytes = 1, Bt1 = 0x38 },
                new AOpCode { Mnemonic = "CMP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x39 },
                new AOpCode { Mnemonic = "CMP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 1, Bt1 = 0x39 },
                new AOpCode { Mnemonic = "CMP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r8, ParamType2 = AParam.par_rm8, Bytes = 1, Bt1 = 0x3A },
                new AOpCode { Mnemonic = "CMP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x3B },
                new AOpCode { Mnemonic = "CMP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0x3B },

                new AOpCode { Mnemonic = "CMPPD", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xc2 },
                new AOpCode { Mnemonic = "CMPPS", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc2 },

                new AOpCode { Mnemonic = "CMPSB", Bytes = 1, Bt1 = 0xa6 },
                new AOpCode { Mnemonic = "CMPSD", Bytes = 1, Bt1 = 0xa7 },
                new AOpCode { Mnemonic = "CMPSD", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, ParamType3 = AParam.par_imm8, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0xc2 },
                new AOpCode { Mnemonic = "CMPSS", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, ParamType3 = AParam.par_imm8, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0xc2 },
                new AOpCode { Mnemonic = "CMPSW", Bytes = 2, Bt1 = 0x66, Bt2 = 0xa7 },
                new AOpCode { Mnemonic = "CMPXCHG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xb0 },
                new AOpCode { Mnemonic = "CMPXCHG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xb1 },
                new AOpCode { Mnemonic = "CMPXCHG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xb1 },
                new AOpCode { Mnemonic = "CMPXCHG8B", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc7 }, //no m64 as eo, seems it"s just a /1

                new AOpCode { Mnemonic = "COMISD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x2f },
                new AOpCode { Mnemonic = "COMISS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x2f },

                new AOpCode { Mnemonic = "CPUID", Bytes = 2, Bt1 = 0x0f, Bt2 = 0xa2 },
                new AOpCode { Mnemonic = "CQO", Bytes = 2, Bt1 = 0x48, Bt2 = 0x99 },
                new AOpCode { Mnemonic = "CVTDQ2PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0xe6 },  //just a gues, the documentation didn"t say anything about a /r, and the disassembler of delphi also doesn"t recognize it
                new AOpCode { Mnemonic = "CVTDQ2PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x5b },
                new AOpCode { Mnemonic = "CVTPD2DQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0xe6 },
                new AOpCode { Mnemonic = "CVTPD2PI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x2d },

                new AOpCode { Mnemonic = "CVTPD2PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x5a },
                new AOpCode { Mnemonic = "CVTPI2PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_mm_m64, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x2a },
                new AOpCode { Mnemonic = "CVTPI2PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x2a },
                new AOpCode { Mnemonic = "CVTPS2DQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x5b },

                new AOpCode { Mnemonic = "CVTPS2PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x5a },
                new AOpCode { Mnemonic = "CVTPS2PI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_xmm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x2d },
                new AOpCode { Mnemonic = "CVTSD2SI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x2d },
                new AOpCode { Mnemonic = "CVTSD2SS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x5a },
                new AOpCode { Mnemonic = "CVTSI2SD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_rm32, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x2a },
                new AOpCode { Mnemonic = "CVTSI2SS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_rm32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x2a },

                new AOpCode { Mnemonic = "CVTSS2SD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x5a },
                new AOpCode { Mnemonic = "CVTSS2SI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm_m32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x2d },

                new AOpCode { Mnemonic = "CVTTPD2DQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xe6 },
                new AOpCode { Mnemonic = "CVTTPD2PI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x2c },

                new AOpCode { Mnemonic = "CVTTPS2PI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0x0f, Bt2 = 0x2c },
                new AOpCode { Mnemonic = "CVTTSD2SI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x2c },
                new AOpCode { Mnemonic = "CVTTSS2SI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x2c },

                new AOpCode { Mnemonic = "CWD", Bytes = 1, Bt1 = 0x99 },
                new AOpCode { Mnemonic = "CWDE", OpCode1 = AExtraOpCode.eo_none, ParamType1 = AParam.par_noparam, Bytes = 1, Bt1 = 0x98 },
                new AOpCode { Mnemonic = "DAA", Bytes = 1, Bt1 = 0x27 },
                new AOpCode { Mnemonic = "DAS", Bytes = 1, Bt1 = 0x2F },
                new AOpCode { Mnemonic = "DEC", OpCode1 = AExtraOpCode.eo_prw, ParamType1 = AParam.par_r16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x48, InvalidIn64Bit = true },
                new AOpCode { Mnemonic = "DEC", OpCode1 = AExtraOpCode.eo_prd, ParamType1 = AParam.par_r32, Bytes = 1, Bt1 = 0x48, InvalidIn64Bit = true },
                new AOpCode { Mnemonic = "DEC", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_rm8, Bytes = 1, Bt1 = 0xfe },
                new AOpCode { Mnemonic = "DEC", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xff },
                new AOpCode { Mnemonic = "DEC", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0xff },
                new AOpCode { Mnemonic = "DIV", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_rm8, Bytes = 1, Bt1 = 0xf6 },
                new AOpCode { Mnemonic = "DIV", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xf7 },
                new AOpCode { Mnemonic = "DIV", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0xf7 },
                new AOpCode { Mnemonic = "DIVPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x5e },
                new AOpCode { Mnemonic = "DIVPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x5e },
                new AOpCode { Mnemonic = "DIVSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x5e },
                new AOpCode { Mnemonic = "DIVSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x5e },
                new AOpCode { Mnemonic = "EMMS", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x77 },
                new AOpCode { Mnemonic = "ENTER", OpCode1 = AExtraOpCode.eo_iw, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_imm16, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc8 },
                new AOpCode { Mnemonic = "F2XM1", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xf0 },
                new AOpCode { Mnemonic = "FABS", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xe1 },
                new AOpCode { Mnemonic = "FADD", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd8 },
                new AOpCode { Mnemonic = "FADD", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdc },
                new AOpCode { Mnemonic = "FADD", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xc0 },
                new AOpCode { Mnemonic = "FADD", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xc0 },
                new AOpCode { Mnemonic = "FADD", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xdc, Bt2 = 0xc0 },
                new AOpCode { Mnemonic = "FADDP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xde, Bt2 = 0xc0 },
                new AOpCode { Mnemonic = "FADDP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xde, Bt2 = 0xc0 },
                new AOpCode { Mnemonic = "FADDP", Bytes = 2, Bt1 = 0xde, Bt2 = 0xc1 },

                new AOpCode { Mnemonic = "FBLD", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_m80, Bytes = 1, Bt1 = 0xdf },
                new AOpCode { Mnemonic = "FBSTP", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m80, Bytes = 1, Bt1 = 0xdf },
                new AOpCode { Mnemonic = "FCHS", Bytes = 2, Bt1 = 0xD9, Bt2 = 0xe0 },
                new AOpCode { Mnemonic = "FCLEX", Bytes = 3, Bt1 = 0x9b, Bt2 = 0xdb, Bt3 = 0xe2 },
                new AOpCode { Mnemonic = "FCMOVB", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xDA, Bt2 = 0xc0 },
                new AOpCode { Mnemonic = "FCMOVB", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xDA, Bt2 = 0xc0 },
                new AOpCode { Mnemonic = "FCMOVBE", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xDA, Bt2 = 0xd0 },
                new AOpCode { Mnemonic = "FCMOVBE", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xDA, Bt2 = 0xd0 },
                new AOpCode { Mnemonic = "FCMOVE", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xDA, Bt2 = 0xc8 },
                new AOpCode { Mnemonic = "FCMOVE", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xDA, Bt2 = 0xc8 },
                new AOpCode { Mnemonic = "FCMOVNB", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xDB, Bt2 = 0xc0 },
                new AOpCode { Mnemonic = "FCMOVNB", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xDB, Bt2 = 0xc0 },
                new AOpCode { Mnemonic = "FCMOVNBE", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xDB, Bt2 = 0xd0 },
                new AOpCode { Mnemonic = "FCMOVNBE", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xDB, Bt2 = 0xd0 },
                new AOpCode { Mnemonic = "FCMOVNE", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xDB, Bt2 = 0xc8 },
                new AOpCode { Mnemonic = "FCMOVNE", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xDB, Bt2 = 0xc8 },
                new AOpCode { Mnemonic = "FCMOVNU", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xDB, Bt2 = 0xd8 },
                new AOpCode { Mnemonic = "FCMOVNU", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xDB, Bt2 = 0xd8 },
                new AOpCode { Mnemonic = "FCMOVU", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xDA, Bt2 = 0xd8 },
                new AOpCode { Mnemonic = "FCMOVU", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xDA, Bt2 = 0xd8 },
                new AOpCode { Mnemonic = "FCOM", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd8 },
                new AOpCode { Mnemonic = "FCOM", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdc },
                new AOpCode { Mnemonic = "FCOM", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xd0 },
                new AOpCode { Mnemonic = "FCOM", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xd0 },
                new AOpCode { Mnemonic = "FCOM", Bytes = 2, Bt1 = 0xd8, Bt2 = 0xd1 },
                new AOpCode { Mnemonic = "FCOMP", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd8 },
                new AOpCode { Mnemonic = "FCOMP", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdc },
                new AOpCode { Mnemonic = "FCOMP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xd8 },
                new AOpCode { Mnemonic = "FCOMP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xd8 },
                new AOpCode { Mnemonic = "FCOMP", Bytes = 2, Bt1 = 0xd8, Bt2 = 0xd9 },
                new AOpCode { Mnemonic = "FCOMI", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xdb, Bt2 = 0xf0 },
                new AOpCode { Mnemonic = "FCOMI", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xdb, Bt2 = 0xf0 },
                new AOpCode { Mnemonic = "FCOMIP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xdf, Bt2 = 0xf0 },
                new AOpCode { Mnemonic = "FCOMIP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xdf, Bt2 = 0xf0 },
                new AOpCode { Mnemonic = "FCOMPP", Bytes = 2, Bt1 = 0xde, Bt2 = 0xd9 },

                new AOpCode { Mnemonic = "FCOMPP", Bytes = 2, Bt1 = 0xde, Bt2 = 0xd9 },
                new AOpCode { Mnemonic = "FCOS", Bytes = 2, Bt1 = 0xD9, Bt2 = 0xff },

                new AOpCode { Mnemonic = "FDECSTP", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xf6 },

                new AOpCode { Mnemonic = "FDIV", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd8 },
                new AOpCode { Mnemonic = "FDIV", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdc },
                new AOpCode { Mnemonic = "FDIV", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xf0 },
                new AOpCode { Mnemonic = "FDIV", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xf0 },
                new AOpCode { Mnemonic = "FDIV", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xdc, Bt2 = 0xf8 },
                new AOpCode { Mnemonic = "FDIVP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xde, Bt2 = 0xf8 },
                new AOpCode { Mnemonic = "FDIVP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xde, Bt2 = 0xf8 },
                new AOpCode { Mnemonic = "FDIVP", Bytes = 2, Bt1 = 0xde, Bt2 = 0xf9 },
                new AOpCode { Mnemonic = "FDIVR", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd8 },
                new AOpCode { Mnemonic = "FDIVR", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdc },
                new AOpCode { Mnemonic = "FDIVR", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xf8 },
                new AOpCode { Mnemonic = "FDIVR", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xf8 },
                new AOpCode { Mnemonic = "FDIVR", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xdc, Bt2 = 0xf0 },
                new AOpCode { Mnemonic = "FDIVRP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xde, Bt2 = 0xf0 },
                new AOpCode { Mnemonic = "FDIVRP", Bytes = 2, Bt1 = 0xde, Bt2 = 0xf1 },
                new AOpCode { Mnemonic = "FFREE", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xdd, Bt2 = 0xc0 },

                new AOpCode { Mnemonic = "FIADD", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xDA },
                new AOpCode { Mnemonic = "FIADD", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xDE },

                new AOpCode { Mnemonic = "FICOM", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xda },
                new AOpCode { Mnemonic = "FICOM", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xde },
                new AOpCode { Mnemonic = "FICOMP", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xda },
                new AOpCode { Mnemonic = "FICOMP", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xde },

                new AOpCode { Mnemonic = "FIDIV", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xda },
                new AOpCode { Mnemonic = "FIDIV", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xde },

                new AOpCode { Mnemonic = "FIDIVR", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xda },
                new AOpCode { Mnemonic = "FIDIVR", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xde },


                new AOpCode { Mnemonic = "FILD", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xdf }, //(I would have chosen to put 32 first, but I gues delphi used the same documentation as I did, cause it choose 16 as default)
                new AOpCode { Mnemonic = "FILD", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xdb },
                new AOpCode { Mnemonic = "FILD", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdf },

                new AOpCode { Mnemonic = "FIMUL", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xda },
                new AOpCode { Mnemonic = "FIMUL", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xde },

                new AOpCode { Mnemonic = "FINCSTP", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xf7 },
                new AOpCode { Mnemonic = "FINIT", Bytes = 3, Bt1 = 0x9b, Bt2 = 0xdb, Bt3 = 0xe3 },

                new AOpCode { Mnemonic = "FIST", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xdb },
                new AOpCode { Mnemonic = "FIST", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xdf },
                new AOpCode { Mnemonic = "FISTP", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xdf },
                new AOpCode { Mnemonic = "FISTP", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xdb },
                new AOpCode { Mnemonic = "FISTP", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdf },

                new AOpCode { Mnemonic = "FISUB", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xda },
                new AOpCode { Mnemonic = "FISUB", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xde },
                new AOpCode { Mnemonic = "FISUBR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xda },
                new AOpCode { Mnemonic = "FISUBR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xde },

                new AOpCode { Mnemonic = "FLD", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdd },
                new AOpCode { Mnemonic = "FLD", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd9 },
                new AOpCode { Mnemonic = "FLD", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_m80, Bytes = 1, Bt1 = 0xdb },
                new AOpCode { Mnemonic = "FLD", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xd9, Bt2 = 0xc0 },

                new AOpCode { Mnemonic = "FLD1", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xe8 },
                new AOpCode { Mnemonic = "FLDCW", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xd9 },
                new AOpCode { Mnemonic = "FLDENV", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd9 },
                new AOpCode { Mnemonic = "FLDL2E", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xea },
                new AOpCode { Mnemonic = "FLDL2T", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xe9 },
                new AOpCode { Mnemonic = "FLDLG2", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xec },
                new AOpCode { Mnemonic = "FLDLN2", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xed },
                new AOpCode { Mnemonic = "FLDPI", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xeb },
                new AOpCode { Mnemonic = "FLDZ", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xee },

                new AOpCode { Mnemonic = "FMUL", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd8 },
                new AOpCode { Mnemonic = "FMUL", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdc },
                new AOpCode { Mnemonic = "FMUL", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xC8 },
                new AOpCode { Mnemonic = "FMUL", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xC8 },
                new AOpCode { Mnemonic = "FMUL", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xdc, Bt2 = 0xC8 },
                new AOpCode { Mnemonic = "FMULP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xde, Bt2 = 0xC8 },
                new AOpCode { Mnemonic = "FMULP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xde, Bt2 = 0xC8 },
                new AOpCode { Mnemonic = "FMULP", Bytes = 2, Bt1 = 0xde, Bt2 = 0xc9 },

                new AOpCode { Mnemonic = "FNINIT", Bytes = 2, Bt1 = 0xdb, Bt2 = 0xe3 },
                new AOpCode { Mnemonic = "FNLEX", Bytes = 2, Bt1 = 0xDb, Bt2 = 0xe2 },
                new AOpCode { Mnemonic = "FNOP", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xd0 },
                new AOpCode { Mnemonic = "FNSAVE", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xdd },

                new AOpCode { Mnemonic = "FNSTCW", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xd9 },
                new AOpCode { Mnemonic = "FNSTENV", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd9 },

                new AOpCode { Mnemonic = "FNSTSW", ParamType1 = AParam.par_ax, Bytes = 2, Bt1 = 0xdf, Bt2 = 0xe0 },
                new AOpCode { Mnemonic = "FNSTSW", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xdd },

                new AOpCode { Mnemonic = "FPATAN", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xf3 },
                new AOpCode { Mnemonic = "FPREM", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xf8 },
                new AOpCode { Mnemonic = "FPREM1", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xf5 },
                new AOpCode { Mnemonic = "FPTAN", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xf2 },
                new AOpCode { Mnemonic = "FRNDINT", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xfc },
                new AOpCode { Mnemonic = "FRSTOR", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xdd },

                new AOpCode { Mnemonic = "FSAVE", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x9b, Bt2 = 0xdd },

                new AOpCode { Mnemonic = "FSCALE", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xfd },
                new AOpCode { Mnemonic = "FSIN", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xfe },
                new AOpCode { Mnemonic = "FSINCOS", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xfb },
                new AOpCode { Mnemonic = "FSQRT", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xfa },

                new AOpCode { Mnemonic = "FST", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd9 },
                new AOpCode { Mnemonic = "FST", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdd },
                new AOpCode { Mnemonic = "FST", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xdd, Bt2 = 0xd0 },
                new AOpCode { Mnemonic = "FSTCW", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m16, Bytes = 2, Bt1 = 0x9b, Bt2 = 0xd9 },
                new AOpCode { Mnemonic = "FSTENV", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x9b, Bt2 = 0xd9 },
                new AOpCode { Mnemonic = "FSTP", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd9 },
                new AOpCode { Mnemonic = "FSTP", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdd },
                new AOpCode { Mnemonic = "FSTP", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m80, Bytes = 1, Bt1 = 0xdb },
                new AOpCode { Mnemonic = "FSTP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xdd, Bt2 = 0xd8 },

                new AOpCode { Mnemonic = "FSTSW", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m16, Bytes = 2, Bt1 = 0x9b, Bt2 = 0xdd },
                new AOpCode { Mnemonic = "FSTSW", ParamType1 = AParam.par_ax, Bytes = 3, Bt1 = 0x9b, Bt2 = 0xdf, Bt3 = 0xe0 },

                new AOpCode { Mnemonic = "FSUB", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd8 },
                new AOpCode { Mnemonic = "FSUB", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdc },
                new AOpCode { Mnemonic = "FSUB", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xe0 },
                new AOpCode { Mnemonic = "FSUB", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xdc, Bt2 = 0xe8 },
                new AOpCode { Mnemonic = "FSUB", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xdc, Bt2 = 0xe8 },
                new AOpCode { Mnemonic = "FSUBP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xde, Bt2 = 0xe8 },
                new AOpCode { Mnemonic = "FSUBP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xde, Bt2 = 0xe8 },
                new AOpCode { Mnemonic = "FSUBP", Bytes = 2, Bt1 = 0xde, Bt2 = 0xe9 },
                new AOpCode { Mnemonic = "FSUBR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd8 },
                new AOpCode { Mnemonic = "FSUBR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdc },
                new AOpCode { Mnemonic = "FSUBR", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xe8 },
                new AOpCode { Mnemonic = "FSUBR", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xe8 },
                new AOpCode { Mnemonic = "FSUBR", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xdc, Bt2 = 0xe0 },
                new AOpCode { Mnemonic = "FSUBRP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xde, Bt2 = 0xe0 },
                new AOpCode { Mnemonic = "FSUBRP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xde, Bt2 = 0xe0 },
                new AOpCode { Mnemonic = "FSUBRP", Bytes = 2, Bt1 = 0xde, Bt2 = 0xe1 },
                new AOpCode { Mnemonic = "FTST", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xe4 },

                new AOpCode { Mnemonic = "FUCOM", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xdd, Bt2 = 0xe0 },
                new AOpCode { Mnemonic = "FUCOM", Bytes = 2, Bt1 = 0xdd, Bt2 = 0xe1 },
                new AOpCode { Mnemonic = "FUCOMI", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xdb, Bt2 = 0xe8 },
                new AOpCode { Mnemonic = "FUCOMI", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xdb, Bt2 = 0xe8 },
                new AOpCode { Mnemonic = "FUCOMIP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xdf, Bt2 = 0xe8 },
                new AOpCode { Mnemonic = "FUCOMIP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xdf, Bt2 = 0xe8 },
                new AOpCode { Mnemonic = "FUCOMP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xdd, Bt2 = 0xe8 },
                new AOpCode { Mnemonic = "FUCOMP", Bytes = 2, Bt1 = 0xdd, Bt2 = 0xe9 },
                new AOpCode { Mnemonic = "FUCOMPP", Bytes = 2, Bt1 = 0xda, Bt2 = 0xe9 },

                new AOpCode { Mnemonic = "FWAIT", Bytes = 1, Bt1 = 0x9b },

                new AOpCode { Mnemonic = "FXAM", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xe5 },
                new AOpCode { Mnemonic = "FXCH", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xd9, Bt2 = 0xc8 },
                new AOpCode { Mnemonic = "FXCH", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xc9 },
                new AOpCode { Mnemonic = "FXRSTOR", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xae },
                new AOpCode { Mnemonic = "FXSAVE", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xae },
                new AOpCode { Mnemonic = "FXTRACT", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xf4 },
                new AOpCode { Mnemonic = "FYL2X", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xf1 },
                new AOpCode { Mnemonic = "FYL2XPI", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xf9 },

                new AOpCode { Mnemonic = "HLT", Bytes = 1, Bt1 = 0xf4 },

                new AOpCode { Mnemonic = "IDIV", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_rm8, Bytes = 1, Bt1 = 0xf6 },
                new AOpCode { Mnemonic = "IDIV", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xf7 },
                new AOpCode { Mnemonic = "IDIV", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0xf7 },


                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_rm8, Bytes = 1, Bt1 = 0xf6 },
                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xf7 },
                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0xf7 },

                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xaf },
                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xaf },

                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, ParamType3 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0x6b },
                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x6b },

                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0x6b },
                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x6b },

                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, ParamType3 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x69 },
                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_id, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, ParamType3 = AParam.par_imm32, Bytes = 1, Bt1 = 0x69 },

                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x69 },
                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_id, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x69 },

                new AOpCode { Mnemonic = "IN", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_al, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xe4 },
                new AOpCode { Mnemonic = "IN", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_ax, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0xe5 },
                new AOpCode { Mnemonic = "IN", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_eax, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xe5 },

                new AOpCode { Mnemonic = "IN", ParamType1 = AParam.par_al, ParamType2 = AParam.par_dx, Bytes = 1, Bt1 = 0xec },
                new AOpCode { Mnemonic = "IN", ParamType1 = AParam.par_ax, ParamType2 = AParam.par_dx, Bytes = 2, Bt1 = 0x66, Bt2 = 0xed },
                new AOpCode { Mnemonic = "IN", ParamType1 = AParam.par_eax, ParamType2 = AParam.par_dx, Bytes = 1, Bt1 = 0xed },

                new AOpCode { Mnemonic = "INC", OpCode1 = AExtraOpCode.eo_prw, ParamType1 = AParam.par_r16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x40, InvalidIn64Bit = true },
                new AOpCode { Mnemonic = "INC", OpCode1 = AExtraOpCode.eo_prd, ParamType1 = AParam.par_r32, Bytes = 1, Bt1 = 0x40, InvalidIn64Bit = true },
                new AOpCode { Mnemonic = "INC", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_rm8, Bytes = 1, Bt1 = 0xfe },
                new AOpCode { Mnemonic = "INC", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xff },
                new AOpCode { Mnemonic = "INC", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0xff },

                new AOpCode { Mnemonic = "INSB", Bytes = 1, Bt1 = 0x6c },
                new AOpCode { Mnemonic = "INSD", Bytes = 1, Bt1 = 0x6d },
                new AOpCode { Mnemonic = "INSW", Bytes = 2, Bt1 = 0x66, Bt2 = 0x6d },

                new AOpCode { Mnemonic = "INT", ParamType1 = AParam.par_3, Bytes = 1, Bt1 = 0xcc },
                new AOpCode { Mnemonic = "INT", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_imm8, Bytes = 1, Bt1 = 0xcd },
                new AOpCode { Mnemonic = "INTO", Bytes = 1, Bt1 = 0xce },

                new AOpCode { Mnemonic = "INVD", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x08 },
                new AOpCode { Mnemonic = "INVLPG", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x01 },

                new AOpCode { Mnemonic = "IRET", Bytes = 2, Bt1 = 0x66, Bt2 = 0xcf },
                new AOpCode { Mnemonic = "IRETD", Bytes = 1, Bt1 = 0xcf },
                new AOpCode { Mnemonic = "IRETQ", Bytes = 2, Bt1 = 0x48, Bt2 = 0xcf },

                new AOpCode { Mnemonic = "JA", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x77 },
                new AOpCode { Mnemonic = "JA", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x87 },
                new AOpCode { Mnemonic = "JAE", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x73 },
                new AOpCode { Mnemonic = "JAE", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x83 },
                new AOpCode { Mnemonic = "JB", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x72 },
                new AOpCode { Mnemonic = "JB", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x82 },
                new AOpCode { Mnemonic = "JBE", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x76 },
                new AOpCode { Mnemonic = "JBE", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x86 },
                new AOpCode { Mnemonic = "JC", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x72 },
                new AOpCode { Mnemonic = "JC", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x82 },

                new AOpCode { Mnemonic = "JCXZ", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 2, Bt1 = 0x66, Bt2 = 0xe3 },
                new AOpCode { Mnemonic = "JE", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x74 },
                new AOpCode { Mnemonic = "JE", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x84 },
                new AOpCode { Mnemonic = "JECXZ", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0xe3 },
                new AOpCode { Mnemonic = "JG", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x7f },
                new AOpCode { Mnemonic = "JG", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x8f },
                new AOpCode { Mnemonic = "JGE", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x7d },
                new AOpCode { Mnemonic = "JGE", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x8d },
                new AOpCode { Mnemonic = "JL", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x7c },
                new AOpCode { Mnemonic = "JL", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x8c },
                new AOpCode { Mnemonic = "JLE", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x7e },
                new AOpCode { Mnemonic = "JLE", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x8e },

                new AOpCode { Mnemonic = "JMP", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0xeb },
                new AOpCode { Mnemonic = "JMP", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 1, Bt1 = 0xe9 },
                new AOpCode { Mnemonic = "JMP", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0xff, NorExw = true },

                new AOpCode { Mnemonic = "JNA", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x76 },
                new AOpCode { Mnemonic = "JNA", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x86 },
                new AOpCode { Mnemonic = "JNAE", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x72 },
                new AOpCode { Mnemonic = "JNAE", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x82 },
                new AOpCode { Mnemonic = "JNB", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x73 },
                new AOpCode { Mnemonic = "JNB", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x83 },
                new AOpCode { Mnemonic = "JNBE", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x77 },
                new AOpCode { Mnemonic = "JNBE", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x87 },
                new AOpCode { Mnemonic = "JNC", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x73 },
                new AOpCode { Mnemonic = "JNC", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x83 },
                new AOpCode { Mnemonic = "JNE", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x75 },
                new AOpCode { Mnemonic = "JNE", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x85 },
                new AOpCode { Mnemonic = "JNG", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x7e },
                new AOpCode { Mnemonic = "JNG", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x8e },
                new AOpCode { Mnemonic = "JNGE", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x7c },
                new AOpCode { Mnemonic = "JNGE", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x8c },
                new AOpCode { Mnemonic = "JNL", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x7d },
                new AOpCode { Mnemonic = "JNL", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x8d },

                new AOpCode { Mnemonic = "JNLE", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x7f },
                new AOpCode { Mnemonic = "JNLE", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x8f },
                new AOpCode { Mnemonic = "JNO", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x71 },
                new AOpCode { Mnemonic = "JNO", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x81 },
                new AOpCode { Mnemonic = "JNP", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x7b },
                new AOpCode { Mnemonic = "JNP", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x8b },
                new AOpCode { Mnemonic = "JNS", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x79 },
                new AOpCode { Mnemonic = "JNS", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x89 },
                new AOpCode { Mnemonic = "JNZ", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x75 },
                new AOpCode { Mnemonic = "JNZ", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x85 },
                new AOpCode { Mnemonic = "JO", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x70 },
                new AOpCode { Mnemonic = "JO", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x80 },
                new AOpCode { Mnemonic = "JP", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x7a },
                new AOpCode { Mnemonic = "JP", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x8a },
                new AOpCode { Mnemonic = "JPE", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x7a },
                new AOpCode { Mnemonic = "JPE", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x8a },
                new AOpCode { Mnemonic = "JPO", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x7b },
                new AOpCode { Mnemonic = "JPO", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x8b },
                new AOpCode { Mnemonic = "JS", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x78 },
                new AOpCode { Mnemonic = "JS", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x88 },
                new AOpCode { Mnemonic = "JZ", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0x74 },
                new AOpCode { Mnemonic = "JZ", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x84 },

                new AOpCode { Mnemonic = "LAHF", Bytes = 1, Bt1 = 0x9f },
                new AOpCode { Mnemonic = "LAR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x02 },
                new AOpCode { Mnemonic = "LAR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x02 },

                new AOpCode { Mnemonic = "LDMXCSR", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xae },
                new AOpCode { Mnemonic = "LDS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_m16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xc5 },
                new AOpCode { Mnemonic = "LDS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_m32, Bytes = 1, Bt1 = 0xc5 },

                new AOpCode { Mnemonic = "LEA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_m16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x8d },
                new AOpCode { Mnemonic = "LEA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_m32, Bytes = 1, Bt1 = 0x8d },
                new AOpCode { Mnemonic = "LEAVE", Bytes = 1, Bt1 = 0xc9 },

                new AOpCode { Mnemonic = "LES", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xc4 },
                new AOpCode { Mnemonic = "LES", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0xc4 },
                new AOpCode { Mnemonic = "LFENCE", Bytes = 3, Bt1 = 0x0f, Bt2 = 0xae, Bt3 = 0xe8 },

                new AOpCode { Mnemonic = "LFS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_m16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xb4 },
                new AOpCode { Mnemonic = "LFS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xb4 },

                new AOpCode { Mnemonic = "LGDT", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m16, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x01 },
                new AOpCode { Mnemonic = "LGDT", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x01 },

                new AOpCode { Mnemonic = "LGS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_m16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xb5 },
                new AOpCode { Mnemonic = "LGS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xb5 },

                new AOpCode { Mnemonic = "LIDT", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m16, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x01 },
                new AOpCode { Mnemonic = "LIDT", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x01 },

                new AOpCode { Mnemonic = "LLDT", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x00 },
                new AOpCode { Mnemonic = "LMSW", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x01 },

                new AOpCode { Mnemonic = "LODSB", Bytes = 1, Bt1 = 0xac },
                new AOpCode { Mnemonic = "LODSD", Bytes = 1, Bt1 = 0xad },
                new AOpCode { Mnemonic = "LODSW", Bytes = 2, Bt1 = 0x66, Bt2 = 0xad },

                new AOpCode { Mnemonic = "LOOP", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0xe2 },
                new AOpCode { Mnemonic = "LOOPE", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 2, Bt1 = 0x66, Bt2 = 0xe1 },
                new AOpCode { Mnemonic = "LOOPNE", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 2, Bt1 = 0x66, Bt2 = 0xe0 },
                new AOpCode { Mnemonic = "LOOPNZ", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0xe0 },
                new AOpCode { Mnemonic = "LOOPZ", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0xe1 },

                new AOpCode { Mnemonic = "LSL", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x03 },
                new AOpCode { Mnemonic = "LSL", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x03 },

                new AOpCode { Mnemonic = "LSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_m16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xb2 },
                new AOpCode { Mnemonic = "LSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xb2 },

                new AOpCode { Mnemonic = "LTR", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x00 },

                new AOpCode { Mnemonic = "MASKMOVDQU", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_mm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xf7 },
                new AOpCode { Mnemonic = "MASKMOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xf7 },
                new AOpCode { Mnemonic = "MAXPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x5f },
                new AOpCode { Mnemonic = "MAXPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x5f },
                new AOpCode { Mnemonic = "MAXSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x5f },
                new AOpCode { Mnemonic = "MAXSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x5f },
                new AOpCode { Mnemonic = "MFENCE", Bytes = 3, Bt1 = 0x0f, Bt2 = 0xae, Bt3 = 0xf0 },
                new AOpCode { Mnemonic = "MINPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x5d },
                new AOpCode { Mnemonic = "MINPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x5d },
                new AOpCode { Mnemonic = "MINSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x5d },
                new AOpCode { Mnemonic = "MINSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x5d },

                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_al, ParamType2 = AParam.par_moffs8, Bytes = 1, Bt1 = 0xa0 },
                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_ax, ParamType2 = AParam.par_moffs16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xa1 },
                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_eax, ParamType2 = AParam.par_moffs32, Bytes = 1, Bt1 = 0xa1 },
                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_moffs8, ParamType2 = AParam.par_al, Bytes = 1, Bt1 = 0xa2 },
                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_moffs16, ParamType2 = AParam.par_ax, Bytes = 2, Bt1 = 0x66, Bt2 = 0xa3 },
                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_moffs32, ParamType2 = AParam.par_eax, Bytes = 1, Bt1 = 0xa3 },

                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_r8, Bytes = 1, Bt1 = 0x88 },
                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x89 },
                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0x8b }, //8b prefered over 89 in case of r32,r32
                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 1, Bt1 = 0x89 },
                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r8, ParamType2 = AParam.par_rm8, Bytes = 1, Bt1 = 0x8a },
                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x8b },

                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_sreg, Bytes = 2, Bt1 = 0x66, Bt2 = 0x8c },
                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_sreg, ParamType2 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x8e },



                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_prb, ParamType1 = AParam.par_r8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xb0 },
                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_prw, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xb8 },
                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_prd, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0xb8 },

                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc6 },
                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xc7 },
                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0xc7 },

                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_cr, ParamType2 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x22 },
                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_cr, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x20 },

                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_dr, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x21 },
                new AOpCode { Mnemonic = "MOV", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_dr, ParamType2 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x23 },

                new AOpCode { Mnemonic = "MOVAPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x28 },

                new AOpCode { Mnemonic = "MOVAPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x28 },
                new AOpCode { Mnemonic = "MOVAPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m128, ParamType2 = AParam.par_xmm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x29 },

                new AOpCode { Mnemonic = "MOVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x6e },
                new AOpCode { Mnemonic = "MOVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_mm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x7e },

                new AOpCode { Mnemonic = "MOVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_rm32, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x6e },
                new AOpCode { Mnemonic = "MOVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x7e },

                new AOpCode { Mnemonic = "MOVDQ2Q", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0xd6 },
                new AOpCode { Mnemonic = "MOVDQA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x6f },
                new AOpCode { Mnemonic = "MOVDQA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m128, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x7f },

                new AOpCode { Mnemonic = "MOVDQU", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x6f },
                new AOpCode { Mnemonic = "MOVDQU", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m128, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x7f },

                new AOpCode { Mnemonic = "MOVHLPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x12 },

                new AOpCode { Mnemonic = "MOVHPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_m64, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x16 },
                new AOpCode { Mnemonic = "MOVHPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m64, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x17 },

                new AOpCode { Mnemonic = "MOVHPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x16 },
                new AOpCode { Mnemonic = "MOVHPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m64, ParamType2 = AParam.par_xmm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x17 },

                new AOpCode { Mnemonic = "MOVLHPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, Bytes = 2, Bt1 = 0x0f, Bt3 = 0x16 },

                new AOpCode { Mnemonic = "MOVLPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_m64, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x12 },
                new AOpCode { Mnemonic = "MOVLPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m64, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x13 },

                new AOpCode { Mnemonic = "MOVLPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x12 },
                new AOpCode { Mnemonic = "MOVLPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m64, ParamType2 = AParam.par_xmm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x13 },

                new AOpCode { Mnemonic = "MOVMSKPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x50 },
                new AOpCode { Mnemonic = "MOVMSKPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x50 },
                new AOpCode { Mnemonic = "MOVNTDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m128, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xe7 },
                new AOpCode { Mnemonic = "MOVNTI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m32, ParamType2 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc3 },

                new AOpCode { Mnemonic = "MOVNTPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m128, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x2b },
                new AOpCode { Mnemonic = "MOVNTPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m128, ParamType2 = AParam.par_xmm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x2b },

                new AOpCode { Mnemonic = "MOVNTQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m64, ParamType2 = AParam.par_mm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xe7 },


                new AOpCode { Mnemonic = "MOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x6f },
                new AOpCode { Mnemonic = "MOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm_m64, ParamType2 = AParam.par_mm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x7f },

                new AOpCode { Mnemonic = "MOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x7e },
                new AOpCode { Mnemonic = "MOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m64, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xd6 },

                new AOpCode { Mnemonic = "MOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x6e },
                new AOpCode { Mnemonic = "MOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_rm32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x7e },

                new AOpCode { Mnemonic = "MOVQ2DQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_mm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xd6 },

                new AOpCode { Mnemonic = "MOVSB", Bytes = 1, Bt1 = 0xa4 },
                new AOpCode { Mnemonic = "MOVSD", Bytes = 1, Bt1 = 0xa5 },

                new AOpCode { Mnemonic = "MOVSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x10 },
                new AOpCode { Mnemonic = "MOVSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m64, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x11 },

                new AOpCode { Mnemonic = "MOVSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x10 },
                new AOpCode { Mnemonic = "MOVSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m32, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x11 },
                new AOpCode { Mnemonic = "MOVSW", Bytes = 2, Bt1 = 0x66, Bt2 = 0xa5 },

                new AOpCode { Mnemonic = "MOVSX", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xbe },
                new AOpCode { Mnemonic = "MOVSX", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xbe },
                new AOpCode { Mnemonic = "MOVSX", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm16, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xbf },
                new AOpCode { Mnemonic = "MOVSXD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0x63 },   //actuall r64,rm32 but the usage of the 64-bit register turns it into a rex_w itself

                new AOpCode { Mnemonic = "MOVUPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x10 },
                new AOpCode { Mnemonic = "MOVUPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m128, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x11 },

                new AOpCode { Mnemonic = "MOVUPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x10 },
                new AOpCode { Mnemonic = "MOVUPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m128, ParamType2 = AParam.par_xmm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x11 },

                new AOpCode { Mnemonic = "MOVZX", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xb6 },
                new AOpCode { Mnemonic = "MOVZX", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xb6 },
                new AOpCode { Mnemonic = "MOVZX", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm16, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xb7 },

                new AOpCode { Mnemonic = "MUL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm8, Bytes = 1, Bt1 = 0xf6 },
                new AOpCode { Mnemonic = "MUL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xf7 },
                new AOpCode { Mnemonic = "MUL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0xf7 },

                new AOpCode { Mnemonic = "MULPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x59 },
                new AOpCode { Mnemonic = "MULPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x59 },
                new AOpCode { Mnemonic = "MULSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x59 },
                new AOpCode { Mnemonic = "MULSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x59 },

                new AOpCode { Mnemonic = "NEG", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm8, Bytes = 1, Bt1 = 0xf6 },
                new AOpCode { Mnemonic = "NEG", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xf7 },
                new AOpCode { Mnemonic = "NEG", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0xf7 },

                new AOpCode { Mnemonic = "NOP", Bytes = 1, Bt1 = 0x90 },  //NOP nop Nop nOp noP NoP nOp NOp nOP

                new AOpCode { Mnemonic = "NOT", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_rm8, Bytes = 1, Bt1 = 0xf6 },
                new AOpCode { Mnemonic = "NOT", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xf7 },
                new AOpCode { Mnemonic = "NOT", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0xf7 },

                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_al, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x0c },
                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_ax, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x0d },
                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_eax, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x0d },
                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_reg1, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x80 },
                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_reg1, OpCode2 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x80 },
                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_reg1, OpCode2 = AExtraOpCode.eo_id, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x81 },
                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_reg1, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0x83, Signed = true },
                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_reg1, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x83, Signed = true },

                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_r8, Bytes = 1, Bt1 = 0x08 },
                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x09 },
                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 1, Bt1 = 0x09 },
                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r8, ParamType2 = AParam.par_rm8, Bytes = 1, Bt1 = 0x0a },
                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x0b },
                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0x0b },

                new AOpCode { Mnemonic = "ORPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x56 },
                new AOpCode { Mnemonic = "ORPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x56 },

                new AOpCode { Mnemonic = "OUT", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_imm8, ParamType2 = AParam.par_al, Bytes = 1, Bt1 = 0xe6 },
                new AOpCode { Mnemonic = "OUT", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_imm8, ParamType2 = AParam.par_ax, Bytes = 2, Bt1 = 0x66, Bt2 = 0xe7 },
                new AOpCode { Mnemonic = "OUT", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_imm8, ParamType2 = AParam.par_eax, Bytes = 1, Bt1 = 0xe7 },

                new AOpCode { Mnemonic = "OUT", ParamType1 = AParam.par_dx, ParamType2 = AParam.par_al, Bytes = 1, Bt1 = 0xee },
                new AOpCode { Mnemonic = "OUT", ParamType1 = AParam.par_dx, ParamType2 = AParam.par_ax, Bytes = 2, Bt1 = 0x66, Bt2 = 0xef },
                new AOpCode { Mnemonic = "OUT", ParamType1 = AParam.par_dx, ParamType2 = AParam.par_eax, Bytes = 1, Bt1 = 0xef },

                new AOpCode { Mnemonic = "OUTSB", Bytes = 1, Bt1 = 0x6e },
                new AOpCode { Mnemonic = "OUTSD", Bytes = 1, Bt1 = 0x6f },
                new AOpCode { Mnemonic = "OUTSW", Bytes = 2, Bt1 = 0x66, Bt2 = 0x6f },

                new AOpCode { Mnemonic = "PACKSSDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x6b },
                new AOpCode { Mnemonic = "PACKSSDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x6b },

                new AOpCode { Mnemonic = "PACKSSWB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x63 },
                new AOpCode { Mnemonic = "PACKSSWB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x63 },

                new AOpCode { Mnemonic = "PACKUSWB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x67 },
                new AOpCode { Mnemonic = "PACKUSWB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x67 },

                new AOpCode { Mnemonic = "PADDB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xfc },
                new AOpCode { Mnemonic = "PADDB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xfc },

                new AOpCode { Mnemonic = "PADDD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xfe },
                new AOpCode { Mnemonic = "PADDD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xfe },

                new AOpCode { Mnemonic = "PADDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xd4 },
                new AOpCode { Mnemonic = "PADDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xd4 },

                new AOpCode { Mnemonic = "PADDSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xec },
                new AOpCode { Mnemonic = "PADDSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xec },

                new AOpCode { Mnemonic = "PADDSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xed },
                new AOpCode { Mnemonic = "PADDSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xed },

                new AOpCode { Mnemonic = "PADDUSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xdc },
                new AOpCode { Mnemonic = "PADDUSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xdc },

                new AOpCode { Mnemonic = "PADDUSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xdd },
                new AOpCode { Mnemonic = "PADDUSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xdd },

                new AOpCode { Mnemonic = "PADDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xfd },
                new AOpCode { Mnemonic = "PADDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xfd },

                new AOpCode { Mnemonic = "PAND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xdb },
                new AOpCode { Mnemonic = "PAND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xdb },

                new AOpCode { Mnemonic = "PANDN", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xdf },
                new AOpCode { Mnemonic = "PANDN", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xdf },

                new AOpCode { Mnemonic = "PAUSE", Bytes = 2, Bt1 = 0xf3, Bt2 = 0x90 },

                new AOpCode { Mnemonic = "PAVGB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xe0 },
                new AOpCode { Mnemonic = "PAVGB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xe0 },

                new AOpCode { Mnemonic = "PAVGW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xe3 },
                new AOpCode { Mnemonic = "PAVGW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xe3 },

                new AOpCode { Mnemonic = "PCMPEQB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x74 },
                new AOpCode { Mnemonic = "PCMPEQB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x74 },

                new AOpCode { Mnemonic = "PCMPEQD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x76 },
                new AOpCode { Mnemonic = "PCMPEQD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x76 },

                new AOpCode { Mnemonic = "PCMPEQW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x75 },
                new AOpCode { Mnemonic = "PCMPEQW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x75 },

                new AOpCode { Mnemonic = "PCMPGTB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x64 },
                new AOpCode { Mnemonic = "PCMPGTB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x64 },

                new AOpCode { Mnemonic = "PCMPGTD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x66 },
                new AOpCode { Mnemonic = "PCMPGTD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x66 },

                new AOpCode { Mnemonic = "PCMPGTW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x65 },
                new AOpCode { Mnemonic = "PCMPGTW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x65 },

                new AOpCode { Mnemonic = "PCPPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x53 },
                new AOpCode { Mnemonic = "PCPSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x53 },

                new AOpCode { Mnemonic = "PEXTRW", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_mm, ParamType3 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc5 },
                new AOpCode { Mnemonic = "PEXTRW", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xc5 },

                new AOpCode { Mnemonic = "PINSRW", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_r32_m16, ParamType3 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc4 },
                new AOpCode { Mnemonic = "PINSRW", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_r32_m16, ParamType3 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xc4 },

                new AOpCode { Mnemonic = "PMADDWD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xf5 },
                new AOpCode { Mnemonic = "PMADDWD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xf5 },

                new AOpCode { Mnemonic = "PMAXSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xee },
                new AOpCode { Mnemonic = "PMAXSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xee },

                new AOpCode { Mnemonic = "PMAXUB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xde },
                new AOpCode { Mnemonic = "PMAXUB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xde },

                new AOpCode { Mnemonic = "PMINSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xea },
                new AOpCode { Mnemonic = "PMINSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xea },

                new AOpCode { Mnemonic = "PMINUB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xda },
                new AOpCode { Mnemonic = "PMINUB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xda },

                new AOpCode { Mnemonic = "PMOVMSKB", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_mm, ParamType3 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xd7 },
                new AOpCode { Mnemonic = "PMOVMSKB", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xd7 },

                new AOpCode { Mnemonic = "PMULHUL", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xe4 },
                new AOpCode { Mnemonic = "PMULHUL", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xe4 },

                new AOpCode { Mnemonic = "PMULHW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xe5 },
                new AOpCode { Mnemonic = "PMULHW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xe5 },

                new AOpCode { Mnemonic = "PMULLW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xd5 },
                new AOpCode { Mnemonic = "PMULLW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xd5 },

                new AOpCode { Mnemonic = "PMULUDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xf4 },
                new AOpCode { Mnemonic = "PMULUDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xf4 },

                new AOpCode { Mnemonic = "POP", OpCode1 = AExtraOpCode.eo_prd, ParamType1 = AParam.par_r32, Bytes = 1, Bt1 = 0x58, NorExw = true },
                new AOpCode { Mnemonic = "POP", OpCode1 = AExtraOpCode.eo_prw, ParamType1 = AParam.par_r16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x58 },

                new AOpCode { Mnemonic = "POP", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0x8f },
                new AOpCode { Mnemonic = "POP", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x8f },

                new AOpCode { Mnemonic = "POP", ParamType1 = AParam.par_ds, Bytes = 1, Bt1 = 0x1f },
                new AOpCode { Mnemonic = "POP", ParamType1 = AParam.par_es, Bytes = 1, Bt1 = 0x07 },
                new AOpCode { Mnemonic = "POP", ParamType1 = AParam.par_ss, Bytes = 1, Bt1 = 0x17 },
                new AOpCode { Mnemonic = "POP", ParamType1 = AParam.par_fs, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xa1 },
                new AOpCode { Mnemonic = "POP", ParamType1 = AParam.par_gs, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xa9 },

                new AOpCode { Mnemonic = "POPA", Bytes = 2, Bt1 = 0x66, Bt2 = 0x61 },
                new AOpCode { Mnemonic = "POPAD", Bytes = 1, Bt1 = 0x61 },
                new AOpCode { Mnemonic = "POPALL", Bytes = 1, Bt1 = 0x61 },

                new AOpCode { Mnemonic = "POPF", Bytes = 2, Bt1 = 0x66, Bt2 = 0x9d },
                new AOpCode { Mnemonic = "POPFD", Bytes = 1, Bt1 = 0x9d, InvalidIn64Bit = true },
                new AOpCode { Mnemonic = "POPFQ", Bytes = 1, Bt1 = 0x9d, InvalidIn32Bit = true },

                new AOpCode { Mnemonic = "POR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xeb },
                new AOpCode { Mnemonic = "POR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xeb },

                new AOpCode { Mnemonic = "PREFETCH0", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_m8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x18 },
                new AOpCode { Mnemonic = "PREFETCH1", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x18 },
                new AOpCode { Mnemonic = "PREFETCH2", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x18 },
                new AOpCode { Mnemonic = "PREFETCHA", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x18 },

                new AOpCode { Mnemonic = "PSADBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xf6 },
                new AOpCode { Mnemonic = "PSADBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xf6 },

                new AOpCode { Mnemonic = "PSHUFD", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x70 },
                new AOpCode { Mnemonic = "PSHUFHW", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x70 },
                new AOpCode { Mnemonic = "PSHUFLW", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x70 },
                new AOpCode { Mnemonic = "PSHUFW", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, ParamType3 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x70 },

                new AOpCode { Mnemonic = "PSLLD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xf2 },
                new AOpCode { Mnemonic = "PSLLD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xf2 },

                new AOpCode { Mnemonic = "PSLLD", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x72 },
                new AOpCode { Mnemonic = "PSLLD", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x72 },

                new AOpCode { Mnemonic = "PSLLDQ", OpCode1 = AExtraOpCode.eo_reg7, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x73 },

                new AOpCode { Mnemonic = "PSLLQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xf3 },
                new AOpCode { Mnemonic = "PSLLQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xf3 },

                new AOpCode { Mnemonic = "PSLLQ", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x73 },
                new AOpCode { Mnemonic = "PSLLQ", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x73 },


                new AOpCode { Mnemonic = "PSLLW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xf1 },
                new AOpCode { Mnemonic = "PSLLW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xf1 },

                new AOpCode { Mnemonic = "PSLLW", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x71 },
                new AOpCode { Mnemonic = "PSLLW", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x71 },

                new AOpCode { Mnemonic = "PSQRTPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x52 },
                new AOpCode { Mnemonic = "PSQRTSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x52 },

                new AOpCode { Mnemonic = "PSRAD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xe2 },
                new AOpCode { Mnemonic = "PSRAD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xe2 },

                new AOpCode { Mnemonic = "PSRAD", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x72 },
                new AOpCode { Mnemonic = "PSRAD", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x72 },

                new AOpCode { Mnemonic = "PSRAW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xe1 },
                new AOpCode { Mnemonic = "PSRAW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xe1 },

                new AOpCode { Mnemonic = "PSRAW", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x71 },
                new AOpCode { Mnemonic = "PSRAW", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x71 },

                new AOpCode { Mnemonic = "PSRLD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xd2 },
                new AOpCode { Mnemonic = "PSRLD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xd2 },

                new AOpCode { Mnemonic = "PSRLD", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x72 },
                new AOpCode { Mnemonic = "PSRLD", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x72 },
                new AOpCode { Mnemonic = "PSRLDQ", OpCode1 = AExtraOpCode.eo_reg3, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x73 },

                new AOpCode { Mnemonic = "PSRLQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xd3 },
                new AOpCode { Mnemonic = "PSRLQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xd3 },

                new AOpCode { Mnemonic = "PSRLQ", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x73 },
                new AOpCode { Mnemonic = "PSRLQ", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x73 },

                new AOpCode { Mnemonic = "PSRLW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xd1 },
                new AOpCode { Mnemonic = "PSRLW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xd1 },

                new AOpCode { Mnemonic = "PSRLW", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x71 },
                new AOpCode { Mnemonic = "PSRLW", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x71 },

                new AOpCode { Mnemonic = "PSUBB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xf8 },
                new AOpCode { Mnemonic = "PSUBB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xf8 },

                new AOpCode { Mnemonic = "PSUBD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xfa },
                new AOpCode { Mnemonic = "PSUBD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xfa },

                new AOpCode { Mnemonic = "PSUBQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xfb },
                new AOpCode { Mnemonic = "PSUBQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xfb },

                new AOpCode { Mnemonic = "PSUBUSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xd8 },
                new AOpCode { Mnemonic = "PSUBUSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xd8 },

                new AOpCode { Mnemonic = "PSUBUSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xd9 },
                new AOpCode { Mnemonic = "PSUBUSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xd9 },

                new AOpCode { Mnemonic = "PSUBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xf9 },
                new AOpCode { Mnemonic = "PSUBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xf9 },

                new AOpCode { Mnemonic = "PSUSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xe8 },
                new AOpCode { Mnemonic = "PSUSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xe8 },

                new AOpCode { Mnemonic = "PSUSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xe9 },
                new AOpCode { Mnemonic = "PSUSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xe9 },

                new AOpCode { Mnemonic = "PUNPCKHBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x68 },
                new AOpCode { Mnemonic = "PUNPCKHBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x68 },

                new AOpCode { Mnemonic = "PUNPCKHDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x6a },
                new AOpCode { Mnemonic = "PUNPCKHDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x6a },

                new AOpCode { Mnemonic = "PUNPCKHQDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x6d },

                new AOpCode { Mnemonic = "PUNPCKHWD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x69 },
                new AOpCode { Mnemonic = "PUNPCKHWD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x69 },

                new AOpCode { Mnemonic = "PUNPCKLBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x60 },
                new AOpCode { Mnemonic = "PUNPCKLBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x60 },

                new AOpCode { Mnemonic = "PUNPCKLDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x62 },
                new AOpCode { Mnemonic = "PUNPCKLDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x62 },

                new AOpCode { Mnemonic = "PUNPCKLQDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x6c },

                new AOpCode { Mnemonic = "PUNPCKLWD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x61 },
                new AOpCode { Mnemonic = "PUNPCKLWD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x61 },


                new AOpCode { Mnemonic = "PUSH", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_imm8, Bytes = 1, Bt1 = 0x6a },
                new AOpCode { Mnemonic = "PUSH", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_imm32, Bytes = 1, Bt1 = 0x68 },
                //  new topcode(){ mnemonic="PUSH", opcode1=textraopcode.eo_iw, paramtype1=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x68 }, todo do we need this?

                new AOpCode { Mnemonic = "PUSH", OpCode1 = AExtraOpCode.eo_prd, ParamType1 = AParam.par_r32, Bytes = 1, Bt1 = 0x50, NorExw = true },
                new AOpCode { Mnemonic = "PUSH", OpCode1 = AExtraOpCode.eo_prw, ParamType1 = AParam.par_r16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x50 },

                new AOpCode { Mnemonic = "PUSH", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0xff },
                new AOpCode { Mnemonic = "PUSH", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xff },

                new AOpCode { Mnemonic = "PUSH", ParamType1 = AParam.par_cs, Bytes = 1, Bt1 = 0x0e },
                new AOpCode { Mnemonic = "PUSH", ParamType1 = AParam.par_ss, Bytes = 1, Bt1 = 0x16 },
                new AOpCode { Mnemonic = "PUSH", ParamType1 = AParam.par_ds, Bytes = 1, Bt1 = 0x1e },
                new AOpCode { Mnemonic = "PUSH", ParamType1 = AParam.par_es, Bytes = 1, Bt1 = 0x06 },
                new AOpCode { Mnemonic = "PUSH", ParamType1 = AParam.par_fs, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xa0 },
                new AOpCode { Mnemonic = "PUSH", ParamType1 = AParam.par_gs, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xa8 },

                new AOpCode { Mnemonic = "PUSHA", Bytes = 2, Bt1 = 0x66, Bt2 = 0x60, InvalidIn64Bit = true },
                new AOpCode { Mnemonic = "PUSHAD", Bytes = 1, Bt1 = 0x60, InvalidIn64Bit = true },
                new AOpCode { Mnemonic = "PUSHALL", Bytes = 1, Bt1 = 0x60, InvalidIn64Bit = true },
                new AOpCode { Mnemonic = "PUSHF", Bytes = 2, Bt1 = 0x66, Bt2 = 0x9c },
                new AOpCode { Mnemonic = "PUSHFD", Bytes = 1, Bt1 = 0x9c, InvalidIn64Bit = true },
                new AOpCode { Mnemonic = "PUSHFQ", Bytes = 1, Bt1 = 0x9c, InvalidIn32Bit = true },

                new AOpCode { Mnemonic = "PXOR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xef },
                new AOpCode { Mnemonic = "PXOR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xef },

                new AOpCode { Mnemonic = "RCL", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd0 },
                new AOpCode { Mnemonic = "RCL", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd2 },
                new AOpCode { Mnemonic = "RCL", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc0 },

                new AOpCode { Mnemonic = "RCL", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_1, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd1 },
                new AOpCode { Mnemonic = "RCL", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_cl, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd3 },
                new AOpCode { Mnemonic = "RCL", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0xc1 },

                new AOpCode { Mnemonic = "RCL", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd1 },
                new AOpCode { Mnemonic = "RCL", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd3 },
                new AOpCode { Mnemonic = "RCL", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc1 },

                new AOpCode { Mnemonic = "RCR", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd1 },
                new AOpCode { Mnemonic = "RCR", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd3 },
                new AOpCode { Mnemonic = "RCR", OpCode1 = AExtraOpCode.eo_reg3, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc1 },

                new AOpCode { Mnemonic = "RCR", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_1, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd1 },
                new AOpCode { Mnemonic = "RCR", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_cl, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd3 },
                new AOpCode { Mnemonic = "RCR", OpCode1 = AExtraOpCode.eo_reg3, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0xc1 },

                new AOpCode { Mnemonic = "RCR", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd0 },
                new AOpCode { Mnemonic = "RCR", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd2 },
                new AOpCode { Mnemonic = "RCR", OpCode1 = AExtraOpCode.eo_reg3, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc0 },

                new AOpCode { Mnemonic = "RDMSR", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x32 },
                new AOpCode { Mnemonic = "RDPMC", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x33 },
                new AOpCode { Mnemonic = "RDTSC", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x31 },

                new AOpCode { Mnemonic = "RET", Bytes = 1, Bt1 = 0xc3 },
                new AOpCode { Mnemonic = "RET", Bytes = 1, Bt1 = 0xcb },
                new AOpCode { Mnemonic = "RET", OpCode1 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_imm16, Bytes = 1, Bt1 = 0xc2 },
                new AOpCode { Mnemonic = "RETN", Bytes = 1, Bt1 = 0xc3 },
                new AOpCode { Mnemonic = "RETN", OpCode1 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_imm16, Bytes = 1, Bt1 = 0xc2 },
                
                new AOpCode { Mnemonic = "ROL", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd1 },
                new AOpCode { Mnemonic = "ROL", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd3 },
                new AOpCode { Mnemonic = "ROL", OpCode1 = AExtraOpCode.eo_reg0, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc1 },

                new AOpCode { Mnemonic = "ROL", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_1, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd1 },
                new AOpCode { Mnemonic = "ROL", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_cl, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd3 },
                new AOpCode { Mnemonic = "ROL", OpCode1 = AExtraOpCode.eo_reg0, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0xc1 },

                new AOpCode { Mnemonic = "ROL", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd0 },
                new AOpCode { Mnemonic = "ROL", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd2 },
                new AOpCode { Mnemonic = "ROL", OpCode1 = AExtraOpCode.eo_reg0, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc0 },

                new AOpCode { Mnemonic = "ROR", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd1 },
                new AOpCode { Mnemonic = "ROR", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd3 },
                new AOpCode { Mnemonic = "ROR", OpCode1 = AExtraOpCode.eo_reg1, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc1 },

                new AOpCode { Mnemonic = "ROR", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_1, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd1 },
                new AOpCode { Mnemonic = "ROR", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_cl, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd3 },
                new AOpCode { Mnemonic = "ROR", OpCode1 = AExtraOpCode.eo_reg1, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0xc1 },

                new AOpCode { Mnemonic = "ROR", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd0 },
                new AOpCode { Mnemonic = "ROR", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd2 },
                new AOpCode { Mnemonic = "ROR", OpCode1 = AExtraOpCode.eo_reg1, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc0 },

                new AOpCode { Mnemonic = "RSM", Bytes = 2, Bt1 = 0x0f, Bt2 = 0xaa },

                new AOpCode { Mnemonic = "SAHF", Bytes = 1, Bt1 = 0x9e },

                new AOpCode { Mnemonic = "SAL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd1 },
                new AOpCode { Mnemonic = "SAL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd3 },
                new AOpCode { Mnemonic = "SAL", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc1 },

                new AOpCode { Mnemonic = "SAL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_1, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd1 },
                new AOpCode { Mnemonic = "SAL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_cl, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd3 },
                new AOpCode { Mnemonic = "SAL", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0xc1 },

                new AOpCode { Mnemonic = "SAL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd0 },
                new AOpCode { Mnemonic = "SAL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd2 },
                new AOpCode { Mnemonic = "SAL", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc0 },

                new AOpCode { Mnemonic = "SAR", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd1 },
                new AOpCode { Mnemonic = "SAR", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd3 },
                new AOpCode { Mnemonic = "SAR", OpCode1 = AExtraOpCode.eo_reg7, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc1 },

                new AOpCode { Mnemonic = "SAR", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_1, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd1 },
                new AOpCode { Mnemonic = "SAR", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_cl, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd3 },
                new AOpCode { Mnemonic = "SAR", OpCode1 = AExtraOpCode.eo_reg7, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0xc1 },

                new AOpCode { Mnemonic = "SAR", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd0 },
                new AOpCode { Mnemonic = "SAR", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd2 },
                new AOpCode { Mnemonic = "SAR", OpCode1 = AExtraOpCode.eo_reg7, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc0 },

                new AOpCode { Mnemonic = "SBB", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_al, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x1c },
                new AOpCode { Mnemonic = "SBB", OpCode1 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_ax, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x1d },
                new AOpCode { Mnemonic = "SBB", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_eax, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x1d },
                new AOpCode { Mnemonic = "SBB", OpCode1 = AExtraOpCode.eo_reg3, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x80 },
                new AOpCode { Mnemonic = "SBB", OpCode1 = AExtraOpCode.eo_reg3, OpCode2 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x80 },
                new AOpCode { Mnemonic = "SBB", OpCode1 = AExtraOpCode.eo_reg3, OpCode2 = AExtraOpCode.eo_id, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x81 },
                new AOpCode { Mnemonic = "SBB", OpCode1 = AExtraOpCode.eo_reg3, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0x83, Signed = true },
                new AOpCode { Mnemonic = "SBB", OpCode1 = AExtraOpCode.eo_reg3, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x83, Signed = true },
                new AOpCode { Mnemonic = "SBB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_r8, Bytes = 1, Bt1 = 0x18 },
                new AOpCode { Mnemonic = "SBB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x19 },
                new AOpCode { Mnemonic = "SBB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 1, Bt1 = 0x19 },
                new AOpCode { Mnemonic = "SBB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r8, ParamType2 = AParam.par_rm8, Bytes = 1, Bt1 = 0x1a },
                new AOpCode { Mnemonic = "SBB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x1b },
                new AOpCode { Mnemonic = "SBB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0x1b },

                new AOpCode { Mnemonic = "SCASB", Bytes = 1, Bt1 = 0xae },
                new AOpCode { Mnemonic = "SCASD", Bytes = 1, Bt1 = 0xaf },
                new AOpCode { Mnemonic = "SCASW", Bytes = 2, Bt1 = 0x66, Bt2 = 0xaf },

                new AOpCode { Mnemonic = "SETA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x97 },
                new AOpCode { Mnemonic = "SETAE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x93 },
                new AOpCode { Mnemonic = "SETB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x92 },
                new AOpCode { Mnemonic = "SETBE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x96 },
                new AOpCode { Mnemonic = "SETC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x92 },
                new AOpCode { Mnemonic = "SETE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x94 },
                new AOpCode { Mnemonic = "SETG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9f },
                new AOpCode { Mnemonic = "SETGE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9d },
                new AOpCode { Mnemonic = "SETL", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9c },
                new AOpCode { Mnemonic = "SETLE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9e },
                new AOpCode { Mnemonic = "SETNA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x96 },

                new AOpCode { Mnemonic = "SETNAE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x92 },
                new AOpCode { Mnemonic = "SETNB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x93 },
                new AOpCode { Mnemonic = "SETNBE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x97 },
                new AOpCode { Mnemonic = "SETNC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x93 },
                new AOpCode { Mnemonic = "SETNE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x95 },
                new AOpCode { Mnemonic = "SETNG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9e },
                new AOpCode { Mnemonic = "SETNGE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9c },
                new AOpCode { Mnemonic = "SETNL", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9d },
                new AOpCode { Mnemonic = "SETNLE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9f },
                new AOpCode { Mnemonic = "SETNO", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x91 },
                new AOpCode { Mnemonic = "SETNP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9b },

                new AOpCode { Mnemonic = "SETNS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x99 },
                new AOpCode { Mnemonic = "SETNZ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x95 },
                new AOpCode { Mnemonic = "SETO", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x90 },
                new AOpCode { Mnemonic = "SETP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9a },
                new AOpCode { Mnemonic = "SETPE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9a },
                new AOpCode { Mnemonic = "SETPO", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9b },
                new AOpCode { Mnemonic = "SETS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x98 },
                new AOpCode { Mnemonic = "SETZ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x94 },

                new AOpCode { Mnemonic = "SFENCE", Bytes = 3, Bt1 = 0x0f, Bt2 = 0xae, Bt3 = 0xf8 },

                new AOpCode { Mnemonic = "SGDT", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x01 },

                new AOpCode { Mnemonic = "SHL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd1 },
                new AOpCode { Mnemonic = "SHL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_1, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd1 },
                new AOpCode { Mnemonic = "SHL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd0 },
                new AOpCode { Mnemonic = "SHL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd2 },
                new AOpCode { Mnemonic = "SHL", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc0 },

                new AOpCode { Mnemonic = "SHL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_cl, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd3 },
                new AOpCode { Mnemonic = "SHL", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0xc1 },

                new AOpCode { Mnemonic = "SHL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd3 },
                new AOpCode { Mnemonic = "SHL", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc1 },

                new AOpCode { Mnemonic = "SHLD", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, ParamType3 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xa4 },
                new AOpCode { Mnemonic = "SHLD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, ParamType3 = AParam.par_cl, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xa5 },

                new AOpCode { Mnemonic = "SHLD", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, ParamType3 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xa4 },
                new AOpCode { Mnemonic = "SHLD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, ParamType3 = AParam.par_cl, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xa5 },

                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd0 },
                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd2 },
                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc0 },

                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_1, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd1 },
                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_cl, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd3 },
                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0xc1 },

                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd1 },
                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd3 },
                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc1 },

                new AOpCode { Mnemonic = "SHRD", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, ParamType3 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xac },
                new AOpCode { Mnemonic = "SHRD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, ParamType3 = AParam.par_cl, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xad },

                new AOpCode { Mnemonic = "SHUFPD", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xc6 },
                new AOpCode { Mnemonic = "SHUFPS", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc6 },

                new AOpCode { Mnemonic = "SIDT", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x01 },
                new AOpCode { Mnemonic = "SLDT", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x00 },

                new AOpCode { Mnemonic = "SMSW", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x01 },

                new AOpCode { Mnemonic = "SQRTPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x51 },
                new AOpCode { Mnemonic = "SQRTPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x51 },
                new AOpCode { Mnemonic = "SQRTSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x51 },
                new AOpCode { Mnemonic = "SQRTSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x51 },

                new AOpCode { Mnemonic = "STC", Bytes = 1, Bt1 = 0xf9 },
                new AOpCode { Mnemonic = "STD", Bytes = 1, Bt1 = 0xfd },
                new AOpCode { Mnemonic = "STI", Bytes = 1, Bt1 = 0xfb },

                new AOpCode { Mnemonic = "STMXCSR", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xae },

                new AOpCode { Mnemonic = "STOSB", Bytes = 1, Bt1 = 0xaa },
                new AOpCode { Mnemonic = "STOSD", Bytes = 1, Bt1 = 0xab },
                new AOpCode { Mnemonic = "STOSW", Bytes = 2, Bt1 = 0x66, Bt2 = 0xab },

                new AOpCode { Mnemonic = "STR", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x00 },

                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_al, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x2c },
                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_ax, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x2d },
                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_eax, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x2d },
                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_reg5, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x80 },
                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_reg5, OpCode2 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x80 },
                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_reg5, OpCode2 = AExtraOpCode.eo_id, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x81 },
                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_reg5, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0x83, Signed = true },
                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_reg5, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x83, Signed = true },
                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_r8, Bytes = 1, Bt1 = 0x28 },
                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x29 },
                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 1, Bt1 = 0x29 },
                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r8, ParamType2 = AParam.par_rm8, Bytes = 1, Bt1 = 0x2a },
                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x2b },
                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0x2b },

                new AOpCode { Mnemonic = "SUBPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x5c },
                new AOpCode { Mnemonic = "SUBPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x5c },
                new AOpCode { Mnemonic = "SUBSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x5c },
                new AOpCode { Mnemonic = "SUBSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x5c },
                new AOpCode { Mnemonic = "SWAPGS", Bytes = 3, Bt1 = 0x0f, Bt2 = 0x01, Bt3 = 0xf8 },

                new AOpCode { Mnemonic = "SYSENTER", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x34 },
                new AOpCode { Mnemonic = "SYSEXIT", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x35 },

                new AOpCode { Mnemonic = "TEST", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_al, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xa8 },
                new AOpCode { Mnemonic = "TEST", OpCode1 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_ax, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xa9 },
                new AOpCode { Mnemonic = "TEST", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_eax, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0xa9 },

                new AOpCode { Mnemonic = "TEST", OpCode1 = AExtraOpCode.eo_reg0, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xf6 },
                new AOpCode { Mnemonic = "TEST", OpCode1 = AExtraOpCode.eo_reg0, OpCode2 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xf7 },
                new AOpCode { Mnemonic = "TEST", OpCode1 = AExtraOpCode.eo_reg0, OpCode2 = AExtraOpCode.eo_id, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0xf7 },

                new AOpCode { Mnemonic = "TEST", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_r8, Bytes = 1, Bt1 = 0x84 },
                new AOpCode { Mnemonic = "TEST", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x85 },
                new AOpCode { Mnemonic = "TEST", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 1, Bt1 = 0x85 },

                new AOpCode { Mnemonic = "UCOMISD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x2e },
                new AOpCode { Mnemonic = "UCOMISS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x2e },

                new AOpCode { Mnemonic = "UD2", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x0b },

                new AOpCode { Mnemonic = "UNPCKHPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x15 },
                new AOpCode { Mnemonic = "UNPCKHPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x15 },

                new AOpCode { Mnemonic = "UNPCKLPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x14 },
                new AOpCode { Mnemonic = "UNPCKLPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x14 },

                new AOpCode { Mnemonic = "VERR", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x00 },
                new AOpCode { Mnemonic = "VERW", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x00 },

                new AOpCode { Mnemonic = "VMCALL", Bytes = 3, Bt1 = 0x0f, Bt2 = 0x01, Bt3 = 0xc1 },
                new AOpCode { Mnemonic = "VMCLEAR", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m64, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xc7 },
                new AOpCode { Mnemonic = "VMLAUNCH", Bytes = 3, Bt1 = 0x0f, Bt2 = 0x01, Bt3 = 0xc2 },
                new AOpCode { Mnemonic = "VMPTRLD", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc7 },
                new AOpCode { Mnemonic = "VMPTRST", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc7 },
                new AOpCode { Mnemonic = "VMREAD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x78 },
                new AOpCode { Mnemonic = "VMRESUME", Bytes = 3, Bt1 = 0x0f, Bt2 = 0x01, Bt3 = 0xc3 },
                new AOpCode { Mnemonic = "VMWRITE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x79 },
                new AOpCode { Mnemonic = "VMXOFF", Bytes = 3, Bt1 = 0x0f, Bt2 = 0x01, Bt3 = 0xc4 },
                new AOpCode { Mnemonic = "VMXON", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m64, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0xc7 },

                new AOpCode { Mnemonic = "WAIT", Bytes = 1, Bt1 = 0x9b },
                new AOpCode { Mnemonic = "WBINVD", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x09 },
                new AOpCode { Mnemonic = "WRMSR", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x30 },

                new AOpCode { Mnemonic = "XADD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_r8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc0 },
                new AOpCode { Mnemonic = "XADD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xc1 },
                new AOpCode { Mnemonic = "XADD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc1 },

                new AOpCode { Mnemonic = "XCHG", OpCode1 = AExtraOpCode.eo_prd, ParamType1 = AParam.par_eax, ParamType2 = AParam.par_r32, Bytes = 1, Bt1 = 0x90 },
                new AOpCode { Mnemonic = "XCHG", OpCode1 = AExtraOpCode.eo_prw, ParamType1 = AParam.par_ax, ParamType2 = AParam.par_r16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x90 },

                new AOpCode { Mnemonic = "XCHG", OpCode1 = AExtraOpCode.eo_prw, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_ax, Bytes = 2, Bt1 = 0x66, Bt2 = 0x90 },

                new AOpCode { Mnemonic = "XCHG", OpCode1 = AExtraOpCode.eo_prd, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_eax, Bytes = 1, Bt1 = 0x90 },

                new AOpCode { Mnemonic = "XCHG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_r8, Bytes = 1, Bt1 = 0x86 },
                new AOpCode { Mnemonic = "XCHG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r8, ParamType2 = AParam.par_rm8, Bytes = 1, Bt1 = 0x86 },

                new AOpCode { Mnemonic = "XCHG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x87 },
                new AOpCode { Mnemonic = "XCHG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x87 },

                new AOpCode { Mnemonic = "XCHG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 1, Bt1 = 0x87 },
                new AOpCode { Mnemonic = "XCHG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0x87 },

                new AOpCode { Mnemonic = "XLATB", Bytes = 1, Bt1 = 0xd7 },

                new AOpCode { Mnemonic = "XOR", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_al, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x34 },
                new AOpCode { Mnemonic = "XOR", OpCode1 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_ax, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x35 },
                new AOpCode { Mnemonic = "XOR", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_eax, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x35 },
                new AOpCode { Mnemonic = "XOR", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x80 },
                new AOpCode { Mnemonic = "XOR", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_id, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x81 },
                new AOpCode { Mnemonic = "XOR", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x81 },
                new AOpCode { Mnemonic = "XOR", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0x83, Signed = true },
                new AOpCode { Mnemonic = "XOR", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x83, Signed = true },
                new AOpCode { Mnemonic = "XOR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_r8, Bytes = 1, Bt1 = 0x30 },
                new AOpCode { Mnemonic = "XOR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x31 },
                new AOpCode { Mnemonic = "XOR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 1, Bt1 = 0x31 },
                new AOpCode { Mnemonic = "XOR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r8, ParamType2 = AParam.par_rm8, Bytes = 1, Bt1 = 0x32 },
                new AOpCode { Mnemonic = "XOR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x33 },
                new AOpCode { Mnemonic = "XOR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0x33 },

                new AOpCode { Mnemonic = "XORPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x57 },
                new AOpCode { Mnemonic = "XORPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x57, },
            };
        }
        #endregion
    }
}
