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
                new AOpCode { mnemonic = "AAA", opcode1 = AExtraOpCode.eo_none, opcode2 = AExtraOpCode.eo_none, paramtype1 = AParam.par_noparam, paramtype2 = AParam.par_noparam, paramtype3 = AParam.par_noparam, bytes = 1, bt1 = 0x37, bt2 = 0, bt3 = 0 }, //no param
                new AOpCode { mnemonic = "AAD", opcode1 = AExtraOpCode.eo_none, opcode2 = AExtraOpCode.eo_none, paramtype1 = AParam.par_noparam, paramtype2 = AParam.par_noparam, paramtype3 = AParam.par_noparam, bytes = 2, bt1 = 0xd5, bt2 = 0x0a, bt3 = 0 },
                new AOpCode { mnemonic = "AAD", opcode1 = AExtraOpCode.eo_ib, opcode2 = AExtraOpCode.eo_none, paramtype1 = AParam.par_imm8, paramtype2 = AParam.par_noparam, paramtype3 = AParam.par_noparam, bytes = 1, bt1 = 0xd5, bt2 = 0, bt3 = 0 },
                new AOpCode { mnemonic = "AAM", opcode1 = AExtraOpCode.eo_none, paramtype1 = AParam.par_noparam, bytes = 2, bt1 = 0xd4, bt2 = 0x0a },
                new AOpCode { mnemonic = "AAM", opcode1 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_imm8, bytes = 1, bt1 = 0xd4 },
                new AOpCode { mnemonic = "AAS", opcode1 = AExtraOpCode.eo_none, paramtype1 = AParam.par_noparam, bytes = 1, bt1 = 0x3F },
                new AOpCode { mnemonic = "ADC", opcode1 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_al, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x14 },
                new AOpCode { mnemonic = "ADC", opcode1 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_ax, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0x15 },
                new AOpCode { mnemonic = "ADC", opcode1 = AExtraOpCode.eo_id, paramtype1 = AParam.par_eax, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0x15 },
                new AOpCode { mnemonic = "ADC", opcode1 = AExtraOpCode.eo_reg2, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x80 },//verified
                new AOpCode { mnemonic = "ADC", opcode1 = AExtraOpCode.eo_reg2, opcode2 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0x81 },
                new AOpCode { mnemonic = "ADC", opcode1 = AExtraOpCode.eo_reg2, opcode2 = AExtraOpCode.eo_id, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0x81 },
                new AOpCode { mnemonic = "ADC", opcode1 = AExtraOpCode.eo_reg2, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0x83, signed = true },
                new AOpCode { mnemonic = "ADC", opcode1 = AExtraOpCode.eo_reg2, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x83, signed = true },
                new AOpCode { mnemonic = "ADC", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_r8, bytes = 1, bt1 = 0x10 },
                new AOpCode { mnemonic = "ADC", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, bytes = 2, bt1 = 0x66, bt2 = 0x11 },
                new AOpCode { mnemonic = "ADC", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 1, bt1 = 0x11 },
                new AOpCode { mnemonic = "ADC", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r8, paramtype2 = AParam.par_rm8, bytes = 1, bt1 = 0x12 },
                new AOpCode { mnemonic = "ADC", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0x13 },
                new AOpCode { mnemonic = "ADC", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 1, bt1 = 0x13 },

                new AOpCode { mnemonic = "ADD", opcode1 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_al, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x04 },
                new AOpCode { mnemonic = "ADD", opcode1 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_ax, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0x05 },
                new AOpCode { mnemonic = "ADD", opcode1 = AExtraOpCode.eo_id, paramtype1 = AParam.par_eax, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0x05 },
                new AOpCode { mnemonic = "ADD", opcode1 = AExtraOpCode.eo_reg0, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x80 },
                new AOpCode { mnemonic = "ADD", opcode1 = AExtraOpCode.eo_reg0, opcode2 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0x81 },
                new AOpCode { mnemonic = "ADD", opcode1 = AExtraOpCode.eo_reg0, opcode2 = AExtraOpCode.eo_id, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0x81 },
                new AOpCode { mnemonic = "ADD", opcode1 = AExtraOpCode.eo_reg0, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0x83, signed = true },
                new AOpCode { mnemonic = "ADD", opcode1 = AExtraOpCode.eo_reg0, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x83, signed = true },
                new AOpCode { mnemonic = "ADD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 1, bt1 = 0x01 },
                new AOpCode { mnemonic = "ADD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, bytes = 2, bt1 = 0x66, bt2 = 0x01 },
                new AOpCode { mnemonic = "ADD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_r8, bytes = 1, bt1 = 0x00 },
                new AOpCode { mnemonic = "ADD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 1, bt1 = 0x03 },
                new AOpCode { mnemonic = "ADD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0x03 },
                new AOpCode { mnemonic = "ADD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r8, paramtype2 = AParam.par_rm8, bytes = 1, bt1 = 0x02 },

                new AOpCode { mnemonic = "ADDPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x58 }, //should be xmm1,xmm2/m128 but is also handled in all the others, in fact all other modrm types have it, hmmmmm....
                new AOpCode { mnemonic = "ADDPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 2, bt1 = 0x0f, bt2 = 0x58 }, //I gues all reg,reg/mem can be handled like this. (oh well, i"m too lazy to change the code)
                new AOpCode { mnemonic = "ADDSD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m64, bytes = 3, bt1 = 0xf2, bt2 = 0x0f, bt3 = 0x58 },
                new AOpCode { mnemonic = "ADDSS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m32, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x58 },

                new AOpCode { mnemonic = "AND", opcode1 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_al, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x24 },
                new AOpCode { mnemonic = "AND", opcode1 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_ax, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0x25 },
                new AOpCode { mnemonic = "AND", opcode1 = AExtraOpCode.eo_id, paramtype1 = AParam.par_eax, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0x25 },
                new AOpCode { mnemonic = "AND", opcode1 = AExtraOpCode.eo_reg4, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x80 },
                new AOpCode { mnemonic = "AND", opcode1 = AExtraOpCode.eo_reg4, opcode2 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0x81 },
                new AOpCode { mnemonic = "AND", opcode1 = AExtraOpCode.eo_reg4, opcode2 = AExtraOpCode.eo_id, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0x81 },
                new AOpCode { mnemonic = "AND", opcode1 = AExtraOpCode.eo_reg4, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0x83, signed = true },
                new AOpCode { mnemonic = "AND", opcode1 = AExtraOpCode.eo_reg4, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x83, signed = true },
                new AOpCode { mnemonic = "AND", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_r8, bytes = 1, bt1 = 0x20 },
                new AOpCode { mnemonic = "AND", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, bytes = 2, bt1 = 0x66, bt2 = 0x21 },
                new AOpCode { mnemonic = "AND", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 1, bt1 = 0x21 },
                new AOpCode { mnemonic = "AND", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r8, paramtype2 = AParam.par_rm8, bytes = 1, bt1 = 0x22 },
                new AOpCode { mnemonic = "AND", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0x23 },
                new AOpCode { mnemonic = "AND", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 1, bt1 = 0x23 },

                new AOpCode { mnemonic = "ANDNPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xff },
                new AOpCode { mnemonic = "ANDNPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 2, bt1 = 0x0f, bt2 = 0x55 },

                new AOpCode { mnemonic = "ANDPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x54 },
                new AOpCode { mnemonic = "ANDPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 2, bt1 = 0x0f, bt2 = 0x54 },

                new AOpCode { mnemonic = "ARPL", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, bytes = 1, bt1 = 0x63 }, //textraopcode.eo_reg means I just need to find the reg and address
                new AOpCode { mnemonic = "BOUND", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0x62 },
                new AOpCode { mnemonic = "BOUND", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 1, bt1 = 0x62 },
                new AOpCode { mnemonic = "BSF", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xbc },
                new AOpCode { mnemonic = "BSF", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0xbc },
                new AOpCode { mnemonic = "BSR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xbd },
                new AOpCode { mnemonic = "BSR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0xbd },
                new AOpCode { mnemonic = "BSWAP", opcode1 = AExtraOpCode.eo_prd, paramtype1 = AParam.par_r32, bytes = 2, bt1 = 0x0f, bt2 = 0xc8 }, //textraopcode.eo_prd
                new AOpCode { mnemonic = "BSWAP", opcode1 = AExtraOpCode.eo_prw, paramtype1 = AParam.par_r16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xc8 }, //textraopcode.eo_prw

                new AOpCode { mnemonic = "BT", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xa3 },
                new AOpCode { mnemonic = "BT", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 2, bt1 = 0x0f, bt2 = 0xa3 },
                new AOpCode { mnemonic = "BT", opcode1 = AExtraOpCode.eo_reg4, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xba },
                new AOpCode { mnemonic = "BT", opcode1 = AExtraOpCode.eo_reg4, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0xba },

                new AOpCode { mnemonic = "BTC", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xbb },
                new AOpCode { mnemonic = "BTC", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 2, bt1 = 0x0f, bt2 = 0xbb },
                new AOpCode { mnemonic = "BTC", opcode1 = AExtraOpCode.eo_reg7, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xba },
                new AOpCode { mnemonic = "BTC", opcode1 = AExtraOpCode.eo_reg7, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0xba },

                new AOpCode { mnemonic = "BTR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xb3 },
                new AOpCode { mnemonic = "BTR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 2, bt1 = 0x0f, bt2 = 0xb3 },
                new AOpCode { mnemonic = "BTR", opcode1 = AExtraOpCode.eo_reg6, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xba },
                new AOpCode { mnemonic = "BTR", opcode1 = AExtraOpCode.eo_reg6, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0xba },

                new AOpCode { mnemonic = "BTS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xab },
                new AOpCode { mnemonic = "BTS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 2, bt1 = 0x0f, bt2 = 0xab },
                new AOpCode { mnemonic = "BTS", opcode1 = AExtraOpCode.eo_reg5, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xba },
                new AOpCode { mnemonic = "BTS", opcode1 = AExtraOpCode.eo_reg5, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0xba },
                
                //no 0x66 0xE8 because it makes the address it jumps to 16 bit
                new AOpCode { mnemonic = "CALL", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 1, bt1 = 0xe8 },
                //also no 0x66 0xff /2
                new AOpCode { mnemonic = "CALL", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_rm32, bytes = 1, bt1 = 0xff, norexw = true },
                new AOpCode { mnemonic = "CBW", opcode1 = AExtraOpCode.eo_none, paramtype1 = AParam.par_noparam, bytes = 2, bt1 = 0x66, bt2 = 0x98 },
                new AOpCode { mnemonic = "CDQ", bytes = 1, bt1 = 0x99 },
                new AOpCode { mnemonic = "CDQE", bytes = 2, bt1 = 0x48, bt2 = 0x98 },

                new AOpCode { mnemonic = "CLC", bytes = 1, bt1 = 0xf8 },
                new AOpCode { mnemonic = "CLD", bytes = 1, bt1 = 0xfc },
                new AOpCode { mnemonic = "CLFLUSH", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_m8, bytes = 2, bt1 = 0x0f, bt2 = 0xae },
                new AOpCode { mnemonic = "CLI", bytes = 1, bt1 = 0xfa },
                new AOpCode { mnemonic = "CLTS", bytes = 2, bt1 = 0x0f, bt2 = 0x06 },
                new AOpCode { mnemonic = "CMC", bytes = 1, bt1 = 0xf5 },
                new AOpCode { mnemonic = "CMOVA", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x47 },
                new AOpCode { mnemonic = "CMOVA", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x47 },
                new AOpCode { mnemonic = "CMOVAE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x43 },
                new AOpCode { mnemonic = "CMOVAE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x43 },
                new AOpCode { mnemonic = "CMOVB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x42 },
                new AOpCode { mnemonic = "CMOVB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x42 },
                new AOpCode { mnemonic = "CMOVBE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x46 },
                new AOpCode { mnemonic = "CMOVBE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x46 },
                new AOpCode { mnemonic = "CMOVC", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x42 },
                new AOpCode { mnemonic = "CMOVC", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x42 },
                new AOpCode { mnemonic = "CMOVE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x44 },
                new AOpCode { mnemonic = "CMOVE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x44 },
                new AOpCode { mnemonic = "CMOVG", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x4f },
                new AOpCode { mnemonic = "CMOVG", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x4f },
                new AOpCode { mnemonic = "CMOVGE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x4d },
                new AOpCode { mnemonic = "CMOVGE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x4d },
                new AOpCode { mnemonic = "CMOVL", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x4c },
                new AOpCode { mnemonic = "CMOVL", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x4c },
                new AOpCode { mnemonic = "CMOVLE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x4e },
                new AOpCode { mnemonic = "CMOVLE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x4e },
                new AOpCode { mnemonic = "CMOVNA", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x46 },
                new AOpCode { mnemonic = "CMOVNA", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x46 },
                new AOpCode { mnemonic = "CMOVNAE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x42 },
                new AOpCode { mnemonic = "CMOVNAE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x42 },
                new AOpCode { mnemonic = "CMOVNB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x43 },
                new AOpCode { mnemonic = "CMOVNB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x43 },
                new AOpCode { mnemonic = "CMOVNBE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x47 },
                new AOpCode { mnemonic = "CMOVNBE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x47 },
                new AOpCode { mnemonic = "CMOVNC", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x43 },
                new AOpCode { mnemonic = "CMOVNC", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x43 },
                new AOpCode { mnemonic = "CMOVNE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x45 },
                new AOpCode { mnemonic = "CMOVNE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x45 },
                new AOpCode { mnemonic = "CMOVNG", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x4e },
                new AOpCode { mnemonic = "CMOVNG", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x4e },
                new AOpCode { mnemonic = "CMOVNGE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x4c },
                new AOpCode { mnemonic = "CMOVNGE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x4c },
                new AOpCode { mnemonic = "CMOVNL", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x4d },
                new AOpCode { mnemonic = "CMOVNL", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x4d },
                new AOpCode { mnemonic = "CMOVNLE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x4f },
                new AOpCode { mnemonic = "CMOVNLE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x4f },
                new AOpCode { mnemonic = "CMOVNO", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x41 },
                new AOpCode { mnemonic = "CMOVNO", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x41 },
                new AOpCode { mnemonic = "CMOVNP", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x4b },
                new AOpCode { mnemonic = "CMOVNP", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x4b },
                new AOpCode { mnemonic = "CMOVNS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x49 },
                new AOpCode { mnemonic = "CMOVNS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x49 },
                new AOpCode { mnemonic = "CMOVNZ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x45 },
                new AOpCode { mnemonic = "CMOVNZ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x45 },
                new AOpCode { mnemonic = "CMOVO", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x40 },
                new AOpCode { mnemonic = "CMOVO", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x40 },
                new AOpCode { mnemonic = "CMOVP", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x4a },
                new AOpCode { mnemonic = "CMOVP", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x4a },
                new AOpCode { mnemonic = "CMOVPE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x4a },
                new AOpCode { mnemonic = "CMOVPE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x4a },
                new AOpCode { mnemonic = "CMOVPO", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x4b },
                new AOpCode { mnemonic = "CMOVPO", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x4b },
                new AOpCode { mnemonic = "CMOVS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x48 },
                new AOpCode { mnemonic = "CMOVS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x48 },
                new AOpCode { mnemonic = "CMOVZ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x44 },
                new AOpCode { mnemonic = "CMOVZ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x44 },

                new AOpCode { mnemonic = "CMP", opcode1 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_al, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x3C }, //2 bytes
                new AOpCode { mnemonic = "CMP", opcode1 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_ax, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0x3D }, //4 bytes
                new AOpCode { mnemonic = "CMP", opcode1 = AExtraOpCode.eo_id, paramtype1 = AParam.par_eax, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0x3D }, //5 bytes
                new AOpCode { mnemonic = "CMP", opcode1 = AExtraOpCode.eo_reg7, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x80 },
                new AOpCode { mnemonic = "CMP", opcode1 = AExtraOpCode.eo_reg7, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0x83, signed = true },
                new AOpCode { mnemonic = "CMP", opcode1 = AExtraOpCode.eo_reg7, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x83, signed = true },

                new AOpCode { mnemonic = "CMP", opcode1 = AExtraOpCode.eo_reg7, opcode2 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0x81 },
                new AOpCode { mnemonic = "CMP", opcode1 = AExtraOpCode.eo_reg7, opcode2 = AExtraOpCode.eo_id, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0x81 },
                new AOpCode { mnemonic = "CMP", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_r8, bytes = 1, bt1 = 0x38 },
                new AOpCode { mnemonic = "CMP", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, bytes = 2, bt1 = 0x66, bt2 = 0x39 },
                new AOpCode { mnemonic = "CMP", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 1, bt1 = 0x39 },
                new AOpCode { mnemonic = "CMP", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r8, paramtype2 = AParam.par_rm8, bytes = 1, bt1 = 0x3A },
                new AOpCode { mnemonic = "CMP", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0x3B },
                new AOpCode { mnemonic = "CMP", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 1, bt1 = 0x3B },

                new AOpCode { mnemonic = "CMPPD", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, paramtype3 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xc2 },
                new AOpCode { mnemonic = "CMPPS", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, paramtype3 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0xc2 },

                new AOpCode { mnemonic = "CMPSB", bytes = 1, bt1 = 0xa6 },
                new AOpCode { mnemonic = "CMPSD", bytes = 1, bt1 = 0xa7 },
                new AOpCode { mnemonic = "CMPSD", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m64, paramtype3 = AParam.par_imm8, bytes = 3, bt1 = 0xf2, bt2 = 0x0f, bt3 = 0xc2 },
                new AOpCode { mnemonic = "CMPSS", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m32, paramtype3 = AParam.par_imm8, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0xc2 },
                new AOpCode { mnemonic = "CMPSW", bytes = 2, bt1 = 0x66, bt2 = 0xa7 },
                new AOpCode { mnemonic = "CMPXCHG", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 2, bt1 = 0x0f, bt2 = 0xb0 },
                new AOpCode { mnemonic = "CMPXCHG", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, bytes = 2, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xb1 },
                new AOpCode { mnemonic = "CMPXCHG", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 2, bt1 = 0x0f, bt2 = 0xb1 },
                new AOpCode { mnemonic = "CMPXCHG8B", opcode1 = AExtraOpCode.eo_reg1, paramtype1 = AParam.par_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xc7 }, //no m64 as eo, seems it"s just a /1

                new AOpCode { mnemonic = "COMISD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m64, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x2f },
                new AOpCode { mnemonic = "COMISS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m32, bytes = 2, bt1 = 0x0f, bt2 = 0x2f },

                new AOpCode { mnemonic = "CPUID", bytes = 2, bt1 = 0x0f, bt2 = 0xa2 },
                new AOpCode { mnemonic = "CQO", bytes = 2, bt1 = 0x48, bt2 = 0x99 },
                new AOpCode { mnemonic = "CVTDQ2PD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m64, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0xe6 },  //just a gues, the documentation didn"t say anything about a /r, and the disassembler of delphi also doesn"t recognize it
                new AOpCode { mnemonic = "CVTDQ2PS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 2, bt1 = 0x0f, bt2 = 0x5b },
                new AOpCode { mnemonic = "CVTPD2DQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0xf2, bt2 = 0x0f, bt3 = 0xe6 },
                new AOpCode { mnemonic = "CVTPD2PI", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x2d },

                new AOpCode { mnemonic = "CVTPD2PS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x5a },
                new AOpCode { mnemonic = "CVTPI2PD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_mm_m64, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x2a },
                new AOpCode { mnemonic = "CVTPI2PS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x2a },
                new AOpCode { mnemonic = "CVTPS2DQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x5b },

                new AOpCode { mnemonic = "CVTPS2PD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x5a },
                new AOpCode { mnemonic = "CVTPS2PI", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_xmm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x2d },
                new AOpCode { mnemonic = "CVTSD2SI", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_xmm_m64, bytes = 3, bt1 = 0xf2, bt2 = 0x0f, bt3 = 0x2d },
                new AOpCode { mnemonic = "CVTSD2SS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m64, bytes = 3, bt1 = 0xf2, bt2 = 0x0f, bt3 = 0x5a },
                new AOpCode { mnemonic = "CVTSI2SD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_rm32, bytes = 3, bt1 = 0xf2, bt2 = 0x0f, bt3 = 0x2a },
                new AOpCode { mnemonic = "CVTSI2SS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_rm32, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x2a },

                new AOpCode { mnemonic = "CVTSS2SD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m32, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x5a },
                new AOpCode { mnemonic = "CVTSS2SI", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_xmm_m32, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x2d },

                new AOpCode { mnemonic = "CVTTPD2DQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xe6 },
                new AOpCode { mnemonic = "CVTTPD2PI", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x2c },

                new AOpCode { mnemonic = "CVTTPS2PI", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_xmm_m64, bytes = 3, bt1 = 0x0f, bt2 = 0x2c },
                new AOpCode { mnemonic = "CVTTSD2SI", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_xmm_m64, bytes = 3, bt1 = 0xf2, bt2 = 0x0f, bt3 = 0x2c },
                new AOpCode { mnemonic = "CVTTSS2SI", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_xmm_m64, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x2c },

                new AOpCode { mnemonic = "CWD", bytes = 1, bt1 = 0x99 },
                new AOpCode { mnemonic = "CWDE", opcode1 = AExtraOpCode.eo_none, paramtype1 = AParam.par_noparam, bytes = 1, bt1 = 0x98 },
                new AOpCode { mnemonic = "DAA", bytes = 1, bt1 = 0x27 },
                new AOpCode { mnemonic = "DAS", bytes = 1, bt1 = 0x2F },
                new AOpCode { mnemonic = "DEC", opcode1 = AExtraOpCode.eo_prw, paramtype1 = AParam.par_r16, bytes = 2, bt1 = 0x66, bt2 = 0x48, invalidin64bit = true },
                new AOpCode { mnemonic = "DEC", opcode1 = AExtraOpCode.eo_prd, paramtype1 = AParam.par_r32, bytes = 1, bt1 = 0x48, invalidin64bit = true },
                new AOpCode { mnemonic = "DEC", opcode1 = AExtraOpCode.eo_reg1, paramtype1 = AParam.par_rm8, bytes = 1, bt1 = 0xfe },
                new AOpCode { mnemonic = "DEC", opcode1 = AExtraOpCode.eo_reg1, paramtype1 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0xff },
                new AOpCode { mnemonic = "DEC", opcode1 = AExtraOpCode.eo_reg1, paramtype1 = AParam.par_rm32, bytes = 1, bt1 = 0xff },
                new AOpCode { mnemonic = "DIV", opcode1 = AExtraOpCode.eo_reg6, paramtype1 = AParam.par_rm8, bytes = 1, bt1 = 0xf6 },
                new AOpCode { mnemonic = "DIV", opcode1 = AExtraOpCode.eo_reg6, paramtype1 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0xf7 },
                new AOpCode { mnemonic = "DIV", opcode1 = AExtraOpCode.eo_reg6, paramtype1 = AParam.par_rm32, bytes = 1, bt1 = 0xf7 },
                new AOpCode { mnemonic = "DIVPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x5e },
                new AOpCode { mnemonic = "DIVPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 2, bt1 = 0x0f, bt2 = 0x5e },
                new AOpCode { mnemonic = "DIVSD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m64, bytes = 3, bt1 = 0xf2, bt2 = 0x0f, bt3 = 0x5e },
                new AOpCode { mnemonic = "DIVSS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m32, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x5e },
                new AOpCode { mnemonic = "EMMS", bytes = 2, bt1 = 0x0f, bt2 = 0x77 },
                new AOpCode { mnemonic = "ENTER", opcode1 = AExtraOpCode.eo_iw, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_imm16, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xc8 },
                new AOpCode { mnemonic = "F2XM1", bytes = 2, bt1 = 0xd9, bt2 = 0xf0 },
                new AOpCode { mnemonic = "FABS", bytes = 2, bt1 = 0xd9, bt2 = 0xe1 },
                new AOpCode { mnemonic = "FADD", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xd8 },
                new AOpCode { mnemonic = "FADD", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_m64, bytes = 1, bt1 = 0xdc },
                new AOpCode { mnemonic = "FADD", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xd8, bt2 = 0xc0 },
                new AOpCode { mnemonic = "FADD", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xd8, bt2 = 0xc0 },
                new AOpCode { mnemonic = "FADD", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, paramtype2 = AParam.par_st0, bytes = 2, bt1 = 0xdc, bt2 = 0xc0 },
                new AOpCode { mnemonic = "FADDP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, paramtype2 = AParam.par_st0, bytes = 2, bt1 = 0xde, bt2 = 0xc0 },
                new AOpCode { mnemonic = "FADDP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xde, bt2 = 0xc0 },
                new AOpCode { mnemonic = "FADDP", bytes = 2, bt1 = 0xde, bt2 = 0xc1 },

                new AOpCode { mnemonic = "FBLD", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_m80, bytes = 1, bt1 = 0xdf },
                new AOpCode { mnemonic = "FBSTP", opcode1 = AExtraOpCode.eo_reg6, paramtype1 = AParam.par_m80, bytes = 1, bt1 = 0xdf },
                new AOpCode { mnemonic = "FCHS", bytes = 2, bt1 = 0xD9, bt2 = 0xe0 },
                new AOpCode { mnemonic = "FCLEX", bytes = 3, bt1 = 0x9b, bt2 = 0xdb, bt3 = 0xe2 },
                new AOpCode { mnemonic = "FCMOVB", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xDA, bt2 = 0xc0 },
                new AOpCode { mnemonic = "FCMOVB", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xDA, bt2 = 0xc0 },
                new AOpCode { mnemonic = "FCMOVBE", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xDA, bt2 = 0xd0 },
                new AOpCode { mnemonic = "FCMOVBE", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xDA, bt2 = 0xd0 },
                new AOpCode { mnemonic = "FCMOVE", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xDA, bt2 = 0xc8 },
                new AOpCode { mnemonic = "FCMOVE", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xDA, bt2 = 0xc8 },
                new AOpCode { mnemonic = "FCMOVNB", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xDB, bt2 = 0xc0 },
                new AOpCode { mnemonic = "FCMOVNB", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xDB, bt2 = 0xc0 },
                new AOpCode { mnemonic = "FCMOVNBE", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xDB, bt2 = 0xd0 },
                new AOpCode { mnemonic = "FCMOVNBE", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xDB, bt2 = 0xd0 },
                new AOpCode { mnemonic = "FCMOVNE", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xDB, bt2 = 0xc8 },
                new AOpCode { mnemonic = "FCMOVNE", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xDB, bt2 = 0xc8 },
                new AOpCode { mnemonic = "FCMOVNU", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xDB, bt2 = 0xd8 },
                new AOpCode { mnemonic = "FCMOVNU", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xDB, bt2 = 0xd8 },
                new AOpCode { mnemonic = "FCMOVU", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xDA, bt2 = 0xd8 },
                new AOpCode { mnemonic = "FCMOVU", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xDA, bt2 = 0xd8 },
                new AOpCode { mnemonic = "FCOM", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xd8 },
                new AOpCode { mnemonic = "FCOM", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_m64, bytes = 1, bt1 = 0xdc },
                new AOpCode { mnemonic = "FCOM", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xd8, bt2 = 0xd0 },
                new AOpCode { mnemonic = "FCOM", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xd8, bt2 = 0xd0 },
                new AOpCode { mnemonic = "FCOM", bytes = 2, bt1 = 0xd8, bt2 = 0xd1 },
                new AOpCode { mnemonic = "FCOMP", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xd8 },
                new AOpCode { mnemonic = "FCOMP", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_m64, bytes = 1, bt1 = 0xdc },
                new AOpCode { mnemonic = "FCOMP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xd8, bt2 = 0xd8 },
                new AOpCode { mnemonic = "FCOMP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xd8, bt2 = 0xd8 },
                new AOpCode { mnemonic = "FCOMP", bytes = 2, bt1 = 0xd8, bt2 = 0xd9 },
                new AOpCode { mnemonic = "FCOMI", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xdb, bt2 = 0xf0 },
                new AOpCode { mnemonic = "FCOMI", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xdb, bt2 = 0xf0 },
                new AOpCode { mnemonic = "FCOMIP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xdf, bt2 = 0xf0 },
                new AOpCode { mnemonic = "FCOMIP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xdf, bt2 = 0xf0 },
                new AOpCode { mnemonic = "FCOMPP", bytes = 2, bt1 = 0xde, bt2 = 0xd9 },

                new AOpCode { mnemonic = "FCOMPP", bytes = 2, bt1 = 0xde, bt2 = 0xd9 },
                new AOpCode { mnemonic = "FCOS", bytes = 2, bt1 = 0xD9, bt2 = 0xff },

                new AOpCode { mnemonic = "FDECSTP", bytes = 2, bt1 = 0xd9, bt2 = 0xf6 },

                new AOpCode { mnemonic = "FDIV", opcode1 = AExtraOpCode.eo_reg6, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xd8 },
                new AOpCode { mnemonic = "FDIV", opcode1 = AExtraOpCode.eo_reg6, paramtype1 = AParam.par_m64, bytes = 1, bt1 = 0xdc },
                new AOpCode { mnemonic = "FDIV", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xd8, bt2 = 0xf0 },
                new AOpCode { mnemonic = "FDIV", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xd8, bt2 = 0xf0 },
                new AOpCode { mnemonic = "FDIV", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, paramtype2 = AParam.par_st0, bytes = 2, bt1 = 0xdc, bt2 = 0xf8 },
                new AOpCode { mnemonic = "FDIVP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, paramtype2 = AParam.par_st0, bytes = 2, bt1 = 0xde, bt2 = 0xf8 },
                new AOpCode { mnemonic = "FDIVP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xde, bt2 = 0xf8 },
                new AOpCode { mnemonic = "FDIVP", bytes = 2, bt1 = 0xde, bt2 = 0xf9 },
                new AOpCode { mnemonic = "FDIVR", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xd8 },
                new AOpCode { mnemonic = "FDIVR", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_m64, bytes = 1, bt1 = 0xdc },
                new AOpCode { mnemonic = "FDIVR", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xd8, bt2 = 0xf8 },
                new AOpCode { mnemonic = "FDIVR", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xd8, bt2 = 0xf8 },
                new AOpCode { mnemonic = "FDIVR", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, paramtype2 = AParam.par_st0, bytes = 2, bt1 = 0xdc, bt2 = 0xf0 },
                new AOpCode { mnemonic = "FDIVRP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, paramtype2 = AParam.par_st0, bytes = 2, bt1 = 0xde, bt2 = 0xf0 },
                new AOpCode { mnemonic = "FDIVRP", bytes = 2, bt1 = 0xde, bt2 = 0xf1 },
                new AOpCode { mnemonic = "FFREE", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xdd, bt2 = 0xc0 },

                new AOpCode { mnemonic = "FIADD", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xDA },
                new AOpCode { mnemonic = "FIADD", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_m16, bytes = 1, bt1 = 0xDE },

                new AOpCode { mnemonic = "FICOM", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xda },
                new AOpCode { mnemonic = "FICOM", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_m16, bytes = 1, bt1 = 0xde },
                new AOpCode { mnemonic = "FICOMP", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xda },
                new AOpCode { mnemonic = "FICOMP", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_m16, bytes = 1, bt1 = 0xde },

                new AOpCode { mnemonic = "FIDIV", opcode1 = AExtraOpCode.eo_reg6, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xda },
                new AOpCode { mnemonic = "FIDIV", opcode1 = AExtraOpCode.eo_reg6, paramtype1 = AParam.par_m16, bytes = 1, bt1 = 0xde },

                new AOpCode { mnemonic = "FIDIVR", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xda },
                new AOpCode { mnemonic = "FIDIVR", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_m16, bytes = 1, bt1 = 0xde },


                new AOpCode { mnemonic = "FILD", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_m16, bytes = 1, bt1 = 0xdf }, //(I would have chosen to put 32 first, but I gues delphi used the same documentation as I did, cause it choose 16 as default)
                new AOpCode { mnemonic = "FILD", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xdb },
                new AOpCode { mnemonic = "FILD", opcode1 = AExtraOpCode.eo_reg5, paramtype1 = AParam.par_m64, bytes = 1, bt1 = 0xdf },

                new AOpCode { mnemonic = "FIMUL", opcode1 = AExtraOpCode.eo_reg1, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xda },
                new AOpCode { mnemonic = "FIMUL", opcode1 = AExtraOpCode.eo_reg1, paramtype1 = AParam.par_m16, bytes = 1, bt1 = 0xde },

                new AOpCode { mnemonic = "FINCSTP", bytes = 2, bt1 = 0xd9, bt2 = 0xf7 },
                new AOpCode { mnemonic = "FINIT", bytes = 3, bt1 = 0x9b, bt2 = 0xdb, bt3 = 0xe3 },

                new AOpCode { mnemonic = "FIST", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xdb },
                new AOpCode { mnemonic = "FIST", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_m16, bytes = 1, bt1 = 0xdf },
                new AOpCode { mnemonic = "FISTP", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_m16, bytes = 1, bt1 = 0xdf },
                new AOpCode { mnemonic = "FISTP", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xdb },
                new AOpCode { mnemonic = "FISTP", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_m64, bytes = 1, bt1 = 0xdf },

                new AOpCode { mnemonic = "FISUB", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xda },
                new AOpCode { mnemonic = "FISUB", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_m16, bytes = 1, bt1 = 0xde },
                new AOpCode { mnemonic = "FISUBR", opcode1 = AExtraOpCode.eo_reg5, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xda },
                new AOpCode { mnemonic = "FISUBR", opcode1 = AExtraOpCode.eo_reg5, paramtype1 = AParam.par_m16, bytes = 1, bt1 = 0xde },

                new AOpCode { mnemonic = "FLD", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_m64, bytes = 1, bt1 = 0xdd },
                new AOpCode { mnemonic = "FLD", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xd9 },
                new AOpCode { mnemonic = "FLD", opcode1 = AExtraOpCode.eo_reg5, paramtype1 = AParam.par_m80, bytes = 1, bt1 = 0xdb },
                new AOpCode { mnemonic = "FLD", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xd9, bt2 = 0xc0 },

                new AOpCode { mnemonic = "FLD1", bytes = 2, bt1 = 0xd9, bt2 = 0xe8 },
                new AOpCode { mnemonic = "FLDCW", opcode1 = AExtraOpCode.eo_reg5, paramtype1 = AParam.par_m16, bytes = 1, bt1 = 0xd9 },
                new AOpCode { mnemonic = "FLDENV", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xd9 },
                new AOpCode { mnemonic = "FLDL2E", bytes = 2, bt1 = 0xd9, bt2 = 0xea },
                new AOpCode { mnemonic = "FLDL2T", bytes = 2, bt1 = 0xd9, bt2 = 0xe9 },
                new AOpCode { mnemonic = "FLDLG2", bytes = 2, bt1 = 0xd9, bt2 = 0xec },
                new AOpCode { mnemonic = "FLDLN2", bytes = 2, bt1 = 0xd9, bt2 = 0xed },
                new AOpCode { mnemonic = "FLDPI", bytes = 2, bt1 = 0xd9, bt2 = 0xeb },
                new AOpCode { mnemonic = "FLDZ", bytes = 2, bt1 = 0xd9, bt2 = 0xee },

                new AOpCode { mnemonic = "FMUL", opcode1 = AExtraOpCode.eo_reg1, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xd8 },
                new AOpCode { mnemonic = "FMUL", opcode1 = AExtraOpCode.eo_reg1, paramtype1 = AParam.par_m64, bytes = 1, bt1 = 0xdc },
                new AOpCode { mnemonic = "FMUL", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xd8, bt2 = 0xC8 },
                new AOpCode { mnemonic = "FMUL", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xd8, bt2 = 0xC8 },
                new AOpCode { mnemonic = "FMUL", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, paramtype2 = AParam.par_st0, bytes = 2, bt1 = 0xdc, bt2 = 0xC8 },
                new AOpCode { mnemonic = "FMULP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, paramtype2 = AParam.par_st0, bytes = 2, bt1 = 0xde, bt2 = 0xC8 },
                new AOpCode { mnemonic = "FMULP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xde, bt2 = 0xC8 },
                new AOpCode { mnemonic = "FMULP", bytes = 2, bt1 = 0xde, bt2 = 0xc9 },

                new AOpCode { mnemonic = "FNINIT", bytes = 2, bt1 = 0xdb, bt2 = 0xe3 },
                new AOpCode { mnemonic = "FNLEX", bytes = 2, bt1 = 0xDb, bt2 = 0xe2 },
                new AOpCode { mnemonic = "FNOP", bytes = 2, bt1 = 0xd9, bt2 = 0xd0 },
                new AOpCode { mnemonic = "FNSAVE", opcode1 = AExtraOpCode.eo_reg6, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xdd },

                new AOpCode { mnemonic = "FNSTCW", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_m16, bytes = 1, bt1 = 0xd9 },
                new AOpCode { mnemonic = "FNSTENV", opcode1 = AExtraOpCode.eo_reg6, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xd9 },

                new AOpCode { mnemonic = "FNSTSW", paramtype1 = AParam.par_ax, bytes = 2, bt1 = 0xdf, bt2 = 0xe0 },
                new AOpCode { mnemonic = "FNSTSW", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_m16, bytes = 1, bt1 = 0xdd },

                new AOpCode { mnemonic = "FPATAN", bytes = 2, bt1 = 0xd9, bt2 = 0xf3 },
                new AOpCode { mnemonic = "FPREM", bytes = 2, bt1 = 0xd9, bt2 = 0xf8 },
                new AOpCode { mnemonic = "FPREM1", bytes = 2, bt1 = 0xd9, bt2 = 0xf5 },
                new AOpCode { mnemonic = "FPTAN", bytes = 2, bt1 = 0xd9, bt2 = 0xf2 },
                new AOpCode { mnemonic = "FRNDINT", bytes = 2, bt1 = 0xd9, bt2 = 0xfc },
                new AOpCode { mnemonic = "FRSTOR", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xdd },

                new AOpCode { mnemonic = "FSAVE", opcode1 = AExtraOpCode.eo_reg6, paramtype1 = AParam.par_m32, bytes = 2, bt1 = 0x9b, bt2 = 0xdd },

                new AOpCode { mnemonic = "FSCALE", bytes = 2, bt1 = 0xd9, bt2 = 0xfd },
                new AOpCode { mnemonic = "FSIN", bytes = 2, bt1 = 0xd9, bt2 = 0xfe },
                new AOpCode { mnemonic = "FSINCOS", bytes = 2, bt1 = 0xd9, bt2 = 0xfb },
                new AOpCode { mnemonic = "FSQRT", bytes = 2, bt1 = 0xd9, bt2 = 0xfa },

                new AOpCode { mnemonic = "FST", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xd9 },
                new AOpCode { mnemonic = "FST", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_m64, bytes = 1, bt1 = 0xdd },
                new AOpCode { mnemonic = "FST", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xdd, bt2 = 0xd0 },
                new AOpCode { mnemonic = "FSTCW", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_m16, bytes = 2, bt1 = 0x9b, bt2 = 0xd9 },
                new AOpCode { mnemonic = "FSTENV", opcode1 = AExtraOpCode.eo_reg6, paramtype1 = AParam.par_m32, bytes = 2, bt1 = 0x9b, bt2 = 0xd9 },
                new AOpCode { mnemonic = "FSTP", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xd9 },
                new AOpCode { mnemonic = "FSTP", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_m64, bytes = 1, bt1 = 0xdd },
                new AOpCode { mnemonic = "FSTP", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_m80, bytes = 1, bt1 = 0xdb },
                new AOpCode { mnemonic = "FSTP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xdd, bt2 = 0xd8 },

                new AOpCode { mnemonic = "FSTSW", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_m16, bytes = 2, bt1 = 0x9b, bt2 = 0xdd },
                new AOpCode { mnemonic = "FSTSW", paramtype1 = AParam.par_ax, bytes = 3, bt1 = 0x9b, bt2 = 0xdf, bt3 = 0xe0 },

                new AOpCode { mnemonic = "FSUB", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xd8 },
                new AOpCode { mnemonic = "FSUB", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_m64, bytes = 1, bt1 = 0xdc },
                new AOpCode { mnemonic = "FSUB", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xd8, bt2 = 0xe0 },
                new AOpCode { mnemonic = "FSUB", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xdc, bt2 = 0xe8 },
                new AOpCode { mnemonic = "FSUB", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, paramtype2 = AParam.par_st0, bytes = 2, bt1 = 0xdc, bt2 = 0xe8 },
                new AOpCode { mnemonic = "FSUBP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, paramtype2 = AParam.par_st0, bytes = 2, bt1 = 0xde, bt2 = 0xe8 },
                new AOpCode { mnemonic = "FSUBP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xde, bt2 = 0xe8 },
                new AOpCode { mnemonic = "FSUBP", bytes = 2, bt1 = 0xde, bt2 = 0xe9 },
                new AOpCode { mnemonic = "FSUBR", opcode1 = AExtraOpCode.eo_reg5, paramtype1 = AParam.par_m32, bytes = 1, bt1 = 0xd8 },
                new AOpCode { mnemonic = "FSUBR", opcode1 = AExtraOpCode.eo_reg5, paramtype1 = AParam.par_m64, bytes = 1, bt1 = 0xdc },
                new AOpCode { mnemonic = "FSUBR", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xd8, bt2 = 0xe8 },
                new AOpCode { mnemonic = "FSUBR", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xd8, bt2 = 0xe8 },
                new AOpCode { mnemonic = "FSUBR", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, paramtype2 = AParam.par_st0, bytes = 2, bt1 = 0xdc, bt2 = 0xe0 },
                new AOpCode { mnemonic = "FSUBRP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, paramtype2 = AParam.par_st0, bytes = 2, bt1 = 0xde, bt2 = 0xe0 },
                new AOpCode { mnemonic = "FSUBRP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xde, bt2 = 0xe0 },
                new AOpCode { mnemonic = "FSUBRP", bytes = 2, bt1 = 0xde, bt2 = 0xe1 },
                new AOpCode { mnemonic = "FTST", bytes = 2, bt1 = 0xd9, bt2 = 0xe4 },

                new AOpCode { mnemonic = "FUCOM", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xdd, bt2 = 0xe0 },
                new AOpCode { mnemonic = "FUCOM", bytes = 2, bt1 = 0xdd, bt2 = 0xe1 },
                new AOpCode { mnemonic = "FUCOMI", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xdb, bt2 = 0xe8 },
                new AOpCode { mnemonic = "FUCOMI", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xdb, bt2 = 0xe8 },
                new AOpCode { mnemonic = "FUCOMIP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st0, paramtype2 = AParam.par_st, bytes = 2, bt1 = 0xdf, bt2 = 0xe8 },
                new AOpCode { mnemonic = "FUCOMIP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xdf, bt2 = 0xe8 },
                new AOpCode { mnemonic = "FUCOMP", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xdd, bt2 = 0xe8 },
                new AOpCode { mnemonic = "FUCOMP", bytes = 2, bt1 = 0xdd, bt2 = 0xe9 },
                new AOpCode { mnemonic = "FUCOMPP", bytes = 2, bt1 = 0xda, bt2 = 0xe9 },

                new AOpCode { mnemonic = "FWAIT", bytes = 1, bt1 = 0x9b },

                new AOpCode { mnemonic = "FXAM", bytes = 2, bt1 = 0xd9, bt2 = 0xe5 },
                new AOpCode { mnemonic = "FXCH", opcode1 = AExtraOpCode.eo_pi, paramtype1 = AParam.par_st, bytes = 2, bt1 = 0xd9, bt2 = 0xc8 },
                new AOpCode { mnemonic = "FXCH", bytes = 2, bt1 = 0xd9, bt2 = 0xc9 },
                new AOpCode { mnemonic = "FXRSTOR", opcode1 = AExtraOpCode.eo_reg1, paramtype1 = AParam.par_m32, bytes = 2, bt1 = 0x0f, bt2 = 0xae },
                new AOpCode { mnemonic = "FXSAVE", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_m32, bytes = 2, bt1 = 0x0f, bt2 = 0xae },
                new AOpCode { mnemonic = "FXTRACT", bytes = 2, bt1 = 0xd9, bt2 = 0xf4 },
                new AOpCode { mnemonic = "FYL2X", bytes = 2, bt1 = 0xd9, bt2 = 0xf1 },
                new AOpCode { mnemonic = "FYL2XPI", bytes = 2, bt1 = 0xd9, bt2 = 0xf9 },

                new AOpCode { mnemonic = "HLT", bytes = 1, bt1 = 0xf4 },

                new AOpCode { mnemonic = "IDIV", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_rm8, bytes = 1, bt1 = 0xf6 },
                new AOpCode { mnemonic = "IDIV", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0xf7 },
                new AOpCode { mnemonic = "IDIV", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_rm32, bytes = 1, bt1 = 0xf7 },


                new AOpCode { mnemonic = "IMUL", opcode1 = AExtraOpCode.eo_reg5, paramtype1 = AParam.par_rm8, bytes = 1, bt1 = 0xf6 },
                new AOpCode { mnemonic = "IMUL", opcode1 = AExtraOpCode.eo_reg5, paramtype1 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0xf7 },
                new AOpCode { mnemonic = "IMUL", opcode1 = AExtraOpCode.eo_reg5, paramtype1 = AParam.par_rm32, bytes = 1, bt1 = 0xf7 },

                new AOpCode { mnemonic = "IMUL", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xaf },
                new AOpCode { mnemonic = "IMUL", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0xaf },

                new AOpCode { mnemonic = "IMUL", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, paramtype3 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0x6b },
                new AOpCode { mnemonic = "IMUL", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, paramtype3 = AParam.par_imm8, bytes = 1, bt1 = 0x6b },

                new AOpCode { mnemonic = "IMUL", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0x6b },
                new AOpCode { mnemonic = "IMUL", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x6b },

                new AOpCode { mnemonic = "IMUL", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, paramtype3 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0x69 },
                new AOpCode { mnemonic = "IMUL", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_id, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, paramtype3 = AParam.par_imm32, bytes = 1, bt1 = 0x69 },

                new AOpCode { mnemonic = "IMUL", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0x69 },
                new AOpCode { mnemonic = "IMUL", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_id, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0x69 },

                new AOpCode { mnemonic = "IN", opcode1 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_al, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xe4 },
                new AOpCode { mnemonic = "IN", opcode1 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_ax, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0xe5 },
                new AOpCode { mnemonic = "IN", opcode1 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_eax, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xe5 },

                new AOpCode { mnemonic = "IN", paramtype1 = AParam.par_al, paramtype2 = AParam.par_dx, bytes = 1, bt1 = 0xec },
                new AOpCode { mnemonic = "IN", paramtype1 = AParam.par_ax, paramtype2 = AParam.par_dx, bytes = 2, bt1 = 0x66, bt2 = 0xed },
                new AOpCode { mnemonic = "IN", paramtype1 = AParam.par_eax, paramtype2 = AParam.par_dx, bytes = 1, bt1 = 0xed },

                new AOpCode { mnemonic = "INC", opcode1 = AExtraOpCode.eo_prw, paramtype1 = AParam.par_r16, bytes = 2, bt1 = 0x66, bt2 = 0x40, invalidin64bit = true },
                new AOpCode { mnemonic = "INC", opcode1 = AExtraOpCode.eo_prd, paramtype1 = AParam.par_r32, bytes = 1, bt1 = 0x40, invalidin64bit = true },
                new AOpCode { mnemonic = "INC", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_rm8, bytes = 1, bt1 = 0xfe },
                new AOpCode { mnemonic = "INC", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0xff },
                new AOpCode { mnemonic = "INC", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_rm32, bytes = 1, bt1 = 0xff },

                new AOpCode { mnemonic = "INSB", bytes = 1, bt1 = 0x6c },
                new AOpCode { mnemonic = "INSD", bytes = 1, bt1 = 0x6d },
                new AOpCode { mnemonic = "INSW", bytes = 2, bt1 = 0x66, bt2 = 0x6d },

                new AOpCode { mnemonic = "INT", paramtype1 = AParam.par_3, bytes = 1, bt1 = 0xcc },
                new AOpCode { mnemonic = "INT", opcode1 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_imm8, bytes = 1, bt1 = 0xcd },
                new AOpCode { mnemonic = "INTO", bytes = 1, bt1 = 0xce },

                new AOpCode { mnemonic = "INVD", bytes = 2, bt1 = 0x0f, bt2 = 0x08 },
                new AOpCode { mnemonic = "INVLPG", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_m32, bytes = 2, bt1 = 0x0f, bt2 = 0x01 },

                new AOpCode { mnemonic = "IRET", bytes = 2, bt1 = 0x66, bt2 = 0xcf },
                new AOpCode { mnemonic = "IRETD", bytes = 1, bt1 = 0xcf },
                new AOpCode { mnemonic = "IRETQ", bytes = 2, bt1 = 0x48, bt2 = 0xcf },

                new AOpCode { mnemonic = "JA", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x77 },
                new AOpCode { mnemonic = "JA", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x87 },
                new AOpCode { mnemonic = "JAE", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x73 },
                new AOpCode { mnemonic = "JAE", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x83 },
                new AOpCode { mnemonic = "JB", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x72 },
                new AOpCode { mnemonic = "JB", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x82 },
                new AOpCode { mnemonic = "JBE", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x76 },
                new AOpCode { mnemonic = "JBE", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x86 },
                new AOpCode { mnemonic = "JC", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x72 },
                new AOpCode { mnemonic = "JC", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x82 },

                new AOpCode { mnemonic = "JCXZ", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 2, bt1 = 0x66, bt2 = 0xe3 },
                new AOpCode { mnemonic = "JE", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x74 },
                new AOpCode { mnemonic = "JE", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x84 },
                new AOpCode { mnemonic = "JECXZ", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0xe3 },
                new AOpCode { mnemonic = "JG", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x7f },
                new AOpCode { mnemonic = "JG", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x8f },
                new AOpCode { mnemonic = "JGE", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x7d },
                new AOpCode { mnemonic = "JGE", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x8d },
                new AOpCode { mnemonic = "JL", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x7c },
                new AOpCode { mnemonic = "JL", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x8c },
                new AOpCode { mnemonic = "JLE", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x7e },
                new AOpCode { mnemonic = "JLE", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x8e },

                new AOpCode { mnemonic = "JMP", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0xeb },
                new AOpCode { mnemonic = "JMP", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 1, bt1 = 0xe9 },
                new AOpCode { mnemonic = "JMP", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_rm32, bytes = 1, bt1 = 0xff, norexw = true },

                new AOpCode { mnemonic = "JNA", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x76 },
                new AOpCode { mnemonic = "JNA", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x86 },
                new AOpCode { mnemonic = "JNAE", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x72 },
                new AOpCode { mnemonic = "JNAE", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x82 },
                new AOpCode { mnemonic = "JNB", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x73 },
                new AOpCode { mnemonic = "JNB", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x83 },
                new AOpCode { mnemonic = "JNBE", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x77 },
                new AOpCode { mnemonic = "JNBE", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x87 },
                new AOpCode { mnemonic = "JNC", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x73 },
                new AOpCode { mnemonic = "JNC", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x83 },
                new AOpCode { mnemonic = "JNE", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x75 },
                new AOpCode { mnemonic = "JNE", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x85 },
                new AOpCode { mnemonic = "JNG", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x7e },
                new AOpCode { mnemonic = "JNG", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x8e },
                new AOpCode { mnemonic = "JNGE", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x7c },
                new AOpCode { mnemonic = "JNGE", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x8c },
                new AOpCode { mnemonic = "JNL", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x7d },
                new AOpCode { mnemonic = "JNL", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x8d },

                new AOpCode { mnemonic = "JNLE", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x7f },
                new AOpCode { mnemonic = "JNLE", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x8f },
                new AOpCode { mnemonic = "JNO", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x71 },
                new AOpCode { mnemonic = "JNO", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x81 },
                new AOpCode { mnemonic = "JNP", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x7b },
                new AOpCode { mnemonic = "JNP", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x8b },
                new AOpCode { mnemonic = "JNS", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x79 },
                new AOpCode { mnemonic = "JNS", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x89 },
                new AOpCode { mnemonic = "JNZ", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x75 },
                new AOpCode { mnemonic = "JNZ", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x85 },
                new AOpCode { mnemonic = "JO", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x70 },
                new AOpCode { mnemonic = "JO", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x80 },
                new AOpCode { mnemonic = "JP", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x7a },
                new AOpCode { mnemonic = "JP", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x8a },
                new AOpCode { mnemonic = "JPE", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x7a },
                new AOpCode { mnemonic = "JPE", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x8a },
                new AOpCode { mnemonic = "JPO", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x7b },
                new AOpCode { mnemonic = "JPO", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x8b },
                new AOpCode { mnemonic = "JS", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x78 },
                new AOpCode { mnemonic = "JS", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x88 },
                new AOpCode { mnemonic = "JZ", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0x74 },
                new AOpCode { mnemonic = "JZ", opcode1 = AExtraOpCode.eo_cd, paramtype1 = AParam.par_rel32, bytes = 2, bt1 = 0x0f, bt2 = 0x84 },

                new AOpCode { mnemonic = "LAHF", bytes = 1, bt1 = 0x9f },
                new AOpCode { mnemonic = "LAR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x02 },
                new AOpCode { mnemonic = "LAR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x02 },

                new AOpCode { mnemonic = "LDMXCSR", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_m32, bytes = 2, bt1 = 0x0f, bt2 = 0xae },
                new AOpCode { mnemonic = "LDS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_m16, bytes = 2, bt1 = 0x66, bt2 = 0xc5 },
                new AOpCode { mnemonic = "LDS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_m32, bytes = 1, bt1 = 0xc5 },

                new AOpCode { mnemonic = "LEA", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_m16, bytes = 2, bt1 = 0x66, bt2 = 0x8d },
                new AOpCode { mnemonic = "LEA", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_m32, bytes = 1, bt1 = 0x8d },
                new AOpCode { mnemonic = "LEAVE", bytes = 1, bt1 = 0xc9 },

                new AOpCode { mnemonic = "LES", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0xc4 },
                new AOpCode { mnemonic = "LES", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 1, bt1 = 0xc4 },
                new AOpCode { mnemonic = "LFENCE", bytes = 3, bt1 = 0x0f, bt2 = 0xae, bt3 = 0xe8 },

                new AOpCode { mnemonic = "LFS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_m16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xb4 },
                new AOpCode { mnemonic = "LFS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_m32, bytes = 2, bt1 = 0x0f, bt2 = 0xb4 },

                new AOpCode { mnemonic = "LGDT", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_m16, bytes = 2, bt1 = 0x0f, bt2 = 0x01 },
                new AOpCode { mnemonic = "LGDT", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_m32, bytes = 2, bt1 = 0x0f, bt2 = 0x01 },

                new AOpCode { mnemonic = "LGS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_m16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xb5 },
                new AOpCode { mnemonic = "LGS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_m32, bytes = 2, bt1 = 0x0f, bt2 = 0xb5 },

                new AOpCode { mnemonic = "LIDT", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_m16, bytes = 2, bt1 = 0x0f, bt2 = 0x01 },
                new AOpCode { mnemonic = "LIDT", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_m32, bytes = 2, bt1 = 0x0f, bt2 = 0x01 },

                new AOpCode { mnemonic = "LLDT", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_rm16, bytes = 2, bt1 = 0x0f, bt2 = 0x00 },
                new AOpCode { mnemonic = "LMSW", opcode1 = AExtraOpCode.eo_reg6, paramtype1 = AParam.par_rm16, bytes = 2, bt1 = 0x0f, bt2 = 0x01 },

                new AOpCode { mnemonic = "LODSB", bytes = 1, bt1 = 0xac },
                new AOpCode { mnemonic = "LODSD", bytes = 1, bt1 = 0xad },
                new AOpCode { mnemonic = "LODSW", bytes = 2, bt1 = 0x66, bt2 = 0xad },

                new AOpCode { mnemonic = "LOOP", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0xe2 },
                new AOpCode { mnemonic = "LOOPE", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 2, bt1 = 0x66, bt2 = 0xe1 },
                new AOpCode { mnemonic = "LOOPNE", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 2, bt1 = 0x66, bt2 = 0xe0 },
                new AOpCode { mnemonic = "LOOPNZ", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0xe0 },
                new AOpCode { mnemonic = "LOOPZ", opcode1 = AExtraOpCode.eo_cb, paramtype1 = AParam.par_rel8, bytes = 1, bt1 = 0xe1 },

                new AOpCode { mnemonic = "LSL", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x03 },
                new AOpCode { mnemonic = "LSL", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x03 },

                new AOpCode { mnemonic = "LSS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_m16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xb2 },
                new AOpCode { mnemonic = "LSS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_m32, bytes = 2, bt1 = 0x0f, bt2 = 0xb2 },

                new AOpCode { mnemonic = "LTR", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_rm16, bytes = 2, bt1 = 0x0f, bt2 = 0x00 },

                new AOpCode { mnemonic = "MASKMOVDQU", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_mm, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xf7 },
                new AOpCode { mnemonic = "MASKMOVQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm, bytes = 2, bt1 = 0x0f, bt2 = 0xf7 },
                new AOpCode { mnemonic = "MAXPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x5f },
                new AOpCode { mnemonic = "MAXPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 2, bt1 = 0x0f, bt2 = 0x5f },
                new AOpCode { mnemonic = "MAXSD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m64, bytes = 3, bt1 = 0xf2, bt2 = 0x0f, bt3 = 0x5f },
                new AOpCode { mnemonic = "MAXSS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m32, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x5f },
                new AOpCode { mnemonic = "MFENCE", bytes = 3, bt1 = 0x0f, bt2 = 0xae, bt3 = 0xf0 },
                new AOpCode { mnemonic = "MINPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x5d },
                new AOpCode { mnemonic = "MINPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 2, bt1 = 0x0f, bt2 = 0x5d },
                new AOpCode { mnemonic = "MINSD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m64, bytes = 3, bt1 = 0xf2, bt2 = 0x0f, bt3 = 0x5d },
                new AOpCode { mnemonic = "MINSS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m32, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x5d },

                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_id, paramtype1 = AParam.par_al, paramtype2 = AParam.par_moffs8, bytes = 1, bt1 = 0xa0 },
                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_id, paramtype1 = AParam.par_ax, paramtype2 = AParam.par_moffs16, bytes = 2, bt1 = 0x66, bt2 = 0xa1 },
                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_id, paramtype1 = AParam.par_eax, paramtype2 = AParam.par_moffs32, bytes = 1, bt1 = 0xa1 },
                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_id, paramtype1 = AParam.par_moffs8, paramtype2 = AParam.par_al, bytes = 1, bt1 = 0xa2 },
                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_id, paramtype1 = AParam.par_moffs16, paramtype2 = AParam.par_ax, bytes = 2, bt1 = 0x66, bt2 = 0xa3 },
                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_id, paramtype1 = AParam.par_moffs32, paramtype2 = AParam.par_eax, bytes = 1, bt1 = 0xa3 },

                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_r8, bytes = 1, bt1 = 0x88 },
                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, bytes = 2, bt1 = 0x66, bt2 = 0x89 },
                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 1, bt1 = 0x8b }, //8b prefered over 89 in case of r32,r32
                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 1, bt1 = 0x89 },
                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r8, paramtype2 = AParam.par_rm8, bytes = 1, bt1 = 0x8a },
                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0x8b },

                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_sreg, bytes = 2, bt1 = 0x66, bt2 = 0x8c },
                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_sreg, paramtype2 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0x8e },



                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_prb, paramtype1 = AParam.par_r8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xb0 },
                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_prw, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0xb8 },
                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_prd, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0xb8 },

                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xc6 },
                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0xc7 },
                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0xc7 },

                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_cr, paramtype2 = AParam.par_r32, bytes = 2, bt1 = 0x0f, bt2 = 0x22 },
                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_cr, bytes = 2, bt1 = 0x0f, bt2 = 0x20 },

                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_dr, bytes = 2, bt1 = 0x0f, bt2 = 0x21 },
                new AOpCode { mnemonic = "MOV", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_dr, paramtype2 = AParam.par_r32, bytes = 2, bt1 = 0x0f, bt2 = 0x23 },

                new AOpCode { mnemonic = "MOVAPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x28 },

                new AOpCode { mnemonic = "MOVAPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 2, bt1 = 0x0f, bt2 = 0x28 },
                new AOpCode { mnemonic = "MOVAPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm_m128, paramtype2 = AParam.par_xmm, bytes = 2, bt1 = 0x0f, bt2 = 0x29 },

                new AOpCode { mnemonic = "MOVD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x6e },
                new AOpCode { mnemonic = "MOVD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_mm, bytes = 2, bt1 = 0x0f, bt2 = 0x7e },

                new AOpCode { mnemonic = "MOVD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_rm32, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x6e },
                new AOpCode { mnemonic = "MOVD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_xmm, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x7e },

                new AOpCode { mnemonic = "MOVDQ2Q", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_xmm, bytes = 3, bt1 = 0xf2, bt2 = 0x0f, bt3 = 0xd6 },
                new AOpCode { mnemonic = "MOVDQA", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x6f },
                new AOpCode { mnemonic = "MOVDQA", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm_m128, paramtype2 = AParam.par_xmm, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x7f },

                new AOpCode { mnemonic = "MOVDQU", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x6f },
                new AOpCode { mnemonic = "MOVDQU", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm_m128, paramtype2 = AParam.par_xmm, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x7f },

                new AOpCode { mnemonic = "MOVHLPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm, bytes = 2, bt1 = 0x0f, bt2 = 0x12 },

                new AOpCode { mnemonic = "MOVHPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_m64, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x16 },
                new AOpCode { mnemonic = "MOVHPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_m64, paramtype2 = AParam.par_xmm, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x17 },

                new AOpCode { mnemonic = "MOVHPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x16 },
                new AOpCode { mnemonic = "MOVHPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_m64, paramtype2 = AParam.par_xmm, bytes = 2, bt1 = 0x0f, bt2 = 0x17 },

                new AOpCode { mnemonic = "MOVLHPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm, bytes = 2, bt1 = 0x0f, bt3 = 0x16 },

                new AOpCode { mnemonic = "MOVLPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_m64, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x12 },
                new AOpCode { mnemonic = "MOVLPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_m64, paramtype2 = AParam.par_xmm, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x13 },

                new AOpCode { mnemonic = "MOVLPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x12 },
                new AOpCode { mnemonic = "MOVLPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_m64, paramtype2 = AParam.par_xmm, bytes = 2, bt1 = 0x0f, bt2 = 0x13 },

                new AOpCode { mnemonic = "MOVMSKPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_xmm, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x50 },
                new AOpCode { mnemonic = "MOVMSKPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_xmm, bytes = 2, bt1 = 0x0f, bt2 = 0x50 },
                new AOpCode { mnemonic = "MOVNTDQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_m128, paramtype2 = AParam.par_xmm, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xe7 },
                new AOpCode { mnemonic = "MOVNTI", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_m32, paramtype2 = AParam.par_r32, bytes = 2, bt1 = 0x0f, bt2 = 0xc3 },

                new AOpCode { mnemonic = "MOVNTPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_m128, paramtype2 = AParam.par_xmm, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x2b },
                new AOpCode { mnemonic = "MOVNTPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_m128, paramtype2 = AParam.par_xmm, bytes = 2, bt1 = 0x0f, bt2 = 0x2b },

                new AOpCode { mnemonic = "MOVNTQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_m64, paramtype2 = AParam.par_mm, bytes = 2, bt1 = 0x0f, bt2 = 0xe7 },


                new AOpCode { mnemonic = "MOVQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x6f },
                new AOpCode { mnemonic = "MOVQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm_m64, paramtype2 = AParam.par_mm, bytes = 2, bt1 = 0x0f, bt2 = 0x7f },

                new AOpCode { mnemonic = "MOVQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m64, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x7e },
                new AOpCode { mnemonic = "MOVQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm_m64, paramtype2 = AParam.par_xmm, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xd6 },

                new AOpCode { mnemonic = "MOVQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x6e },
                new AOpCode { mnemonic = "MOVQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_rm32, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x7e },

                new AOpCode { mnemonic = "MOVQ2DQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_mm, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xd6 },

                new AOpCode { mnemonic = "MOVSB", bytes = 1, bt1 = 0xa4 },
                new AOpCode { mnemonic = "MOVSD", bytes = 1, bt1 = 0xa5 },

                new AOpCode { mnemonic = "MOVSD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m64, bytes = 3, bt1 = 0xf2, bt2 = 0x0f, bt3 = 0x10 },
                new AOpCode { mnemonic = "MOVSD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm_m64, paramtype2 = AParam.par_xmm, bytes = 3, bt1 = 0xf2, bt2 = 0x0f, bt3 = 0x11 },

                new AOpCode { mnemonic = "MOVSS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m32, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x10 },
                new AOpCode { mnemonic = "MOVSS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_m32, paramtype2 = AParam.par_xmm, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x11 },
                new AOpCode { mnemonic = "MOVSW", bytes = 2, bt1 = 0x66, bt2 = 0xa5 },

                new AOpCode { mnemonic = "MOVSX", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xbe },
                new AOpCode { mnemonic = "MOVSX", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0xbe },
                new AOpCode { mnemonic = "MOVSX", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm16, bytes = 2, bt1 = 0x0f, bt2 = 0xbf },
                new AOpCode { mnemonic = "MOVSXD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 1, bt1 = 0x63 },   //actuall r64,rm32 but the usage of the 64-bit register turns it into a rex_w itself

                new AOpCode { mnemonic = "MOVUPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x10 },
                new AOpCode { mnemonic = "MOVUPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm_m128, paramtype2 = AParam.par_xmm, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x11 },

                new AOpCode { mnemonic = "MOVUPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 2, bt1 = 0x0f, bt2 = 0x10 },
                new AOpCode { mnemonic = "MOVUPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm_m128, paramtype2 = AParam.par_xmm, bytes = 2, bt1 = 0x0f, bt2 = 0x11 },

                new AOpCode { mnemonic = "MOVZX", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xb6 },
                new AOpCode { mnemonic = "MOVZX", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0xb6 },
                new AOpCode { mnemonic = "MOVZX", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm16, bytes = 2, bt1 = 0x0f, bt2 = 0xb7 },

                new AOpCode { mnemonic = "MUL", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_rm8, bytes = 1, bt1 = 0xf6 },
                new AOpCode { mnemonic = "MUL", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0xf7 },
                new AOpCode { mnemonic = "MUL", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_rm32, bytes = 1, bt1 = 0xf7 },

                new AOpCode { mnemonic = "MULPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x59 },
                new AOpCode { mnemonic = "MULPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 2, bt1 = 0x0f, bt2 = 0x59 },
                new AOpCode { mnemonic = "MULSD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m64, bytes = 3, bt1 = 0xf2, bt2 = 0x0f, bt3 = 0x59 },
                new AOpCode { mnemonic = "MULSS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m32, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x59 },

                new AOpCode { mnemonic = "NEG", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_rm8, bytes = 1, bt1 = 0xf6 },
                new AOpCode { mnemonic = "NEG", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0xf7 },
                new AOpCode { mnemonic = "NEG", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_rm32, bytes = 1, bt1 = 0xf7 },

                new AOpCode { mnemonic = "NOP", bytes = 1, bt1 = 0x90 },  //NOP nop Nop nOp noP NoP nOp NOp nOP

                new AOpCode { mnemonic = "NOT", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_rm8, bytes = 1, bt1 = 0xf6 },
                new AOpCode { mnemonic = "NOT", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0xf7 },
                new AOpCode { mnemonic = "NOT", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_rm32, bytes = 1, bt1 = 0xf7 },

                new AOpCode { mnemonic = "OR", opcode1 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_al, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x0c },
                new AOpCode { mnemonic = "OR", opcode1 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_ax, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0x0d },
                new AOpCode { mnemonic = "OR", opcode1 = AExtraOpCode.eo_id, paramtype1 = AParam.par_eax, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0x0d },
                new AOpCode { mnemonic = "OR", opcode1 = AExtraOpCode.eo_reg1, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x80 },
                new AOpCode { mnemonic = "OR", opcode1 = AExtraOpCode.eo_reg1, opcode2 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0x80 },
                new AOpCode { mnemonic = "OR", opcode1 = AExtraOpCode.eo_reg1, opcode2 = AExtraOpCode.eo_id, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0x81 },
                new AOpCode { mnemonic = "OR", opcode1 = AExtraOpCode.eo_reg1, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0x83, signed = true },
                new AOpCode { mnemonic = "OR", opcode1 = AExtraOpCode.eo_reg1, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x83, signed = true },

                new AOpCode { mnemonic = "OR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_r8, bytes = 1, bt1 = 0x08 },
                new AOpCode { mnemonic = "OR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, bytes = 2, bt1 = 0x66, bt2 = 0x09 },
                new AOpCode { mnemonic = "OR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 1, bt1 = 0x09 },
                new AOpCode { mnemonic = "OR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r8, paramtype2 = AParam.par_rm8, bytes = 1, bt1 = 0x0a },
                new AOpCode { mnemonic = "OR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0x0b },
                new AOpCode { mnemonic = "OR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 1, bt1 = 0x0b },

                new AOpCode { mnemonic = "ORPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x56 },
                new AOpCode { mnemonic = "ORPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 2, bt1 = 0x0f, bt2 = 0x56 },

                new AOpCode { mnemonic = "OUT", opcode1 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_imm8, paramtype2 = AParam.par_al, bytes = 1, bt1 = 0xe6 },
                new AOpCode { mnemonic = "OUT", opcode1 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_imm8, paramtype2 = AParam.par_ax, bytes = 2, bt1 = 0x66, bt2 = 0xe7 },
                new AOpCode { mnemonic = "OUT", opcode1 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_imm8, paramtype2 = AParam.par_eax, bytes = 1, bt1 = 0xe7 },

                new AOpCode { mnemonic = "OUT", paramtype1 = AParam.par_dx, paramtype2 = AParam.par_al, bytes = 1, bt1 = 0xee },
                new AOpCode { mnemonic = "OUT", paramtype1 = AParam.par_dx, paramtype2 = AParam.par_ax, bytes = 2, bt1 = 0x66, bt2 = 0xef },
                new AOpCode { mnemonic = "OUT", paramtype1 = AParam.par_dx, paramtype2 = AParam.par_eax, bytes = 1, bt1 = 0xef },

                new AOpCode { mnemonic = "OUTSB", bytes = 1, bt1 = 0x6e },
                new AOpCode { mnemonic = "OUTSD", bytes = 1, bt1 = 0x6f },
                new AOpCode { mnemonic = "OUTSW", bytes = 2, bt1 = 0x66, bt2 = 0x6f },

                new AOpCode { mnemonic = "PACKSSDW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x6b },
                new AOpCode { mnemonic = "PACKSSDW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x6b },

                new AOpCode { mnemonic = "PACKSSWB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x63 },
                new AOpCode { mnemonic = "PACKSSWB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x63 },

                new AOpCode { mnemonic = "PACKUSWB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x67 },
                new AOpCode { mnemonic = "PACKUSWB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x67 },

                new AOpCode { mnemonic = "PADDB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xfc },
                new AOpCode { mnemonic = "PADDB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xfc },

                new AOpCode { mnemonic = "PADDD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xfe },
                new AOpCode { mnemonic = "PADDD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xfe },

                new AOpCode { mnemonic = "PADDQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xd4 },
                new AOpCode { mnemonic = "PADDQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xd4 },

                new AOpCode { mnemonic = "PADDSB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xec },
                new AOpCode { mnemonic = "PADDSB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xec },

                new AOpCode { mnemonic = "PADDSW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xed },
                new AOpCode { mnemonic = "PADDSW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xed },

                new AOpCode { mnemonic = "PADDUSB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xdc },
                new AOpCode { mnemonic = "PADDUSB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xdc },

                new AOpCode { mnemonic = "PADDUSW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xdd },
                new AOpCode { mnemonic = "PADDUSW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xdd },

                new AOpCode { mnemonic = "PADDW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xfd },
                new AOpCode { mnemonic = "PADDW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xfd },

                new AOpCode { mnemonic = "PAND", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xdb },
                new AOpCode { mnemonic = "PAND", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xdb },

                new AOpCode { mnemonic = "PANDN", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xdf },
                new AOpCode { mnemonic = "PANDN", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xdf },

                new AOpCode { mnemonic = "PAUSE", bytes = 2, bt1 = 0xf3, bt2 = 0x90 },

                new AOpCode { mnemonic = "PAVGB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xe0 },
                new AOpCode { mnemonic = "PAVGB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xe0 },

                new AOpCode { mnemonic = "PAVGW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xe3 },
                new AOpCode { mnemonic = "PAVGW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xe3 },

                new AOpCode { mnemonic = "PCMPEQB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x74 },
                new AOpCode { mnemonic = "PCMPEQB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x74 },

                new AOpCode { mnemonic = "PCMPEQD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x76 },
                new AOpCode { mnemonic = "PCMPEQD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x76 },

                new AOpCode { mnemonic = "PCMPEQW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x75 },
                new AOpCode { mnemonic = "PCMPEQW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x75 },

                new AOpCode { mnemonic = "PCMPGTB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x64 },
                new AOpCode { mnemonic = "PCMPGTB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x64 },

                new AOpCode { mnemonic = "PCMPGTD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x66 },
                new AOpCode { mnemonic = "PCMPGTD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x66 },

                new AOpCode { mnemonic = "PCMPGTW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x65 },
                new AOpCode { mnemonic = "PCMPGTW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x65 },

                new AOpCode { mnemonic = "PCPPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 2, bt1 = 0x0f, bt2 = 0x53 },
                new AOpCode { mnemonic = "PCPSS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x53 },

                new AOpCode { mnemonic = "PEXTRW", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_mm, paramtype3 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0xc5 },
                new AOpCode { mnemonic = "PEXTRW", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_xmm, paramtype3 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xc5 },

                new AOpCode { mnemonic = "PINSRW", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_r32_m16, paramtype3 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0xc4 },
                new AOpCode { mnemonic = "PINSRW", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_r32_m16, paramtype3 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xc4 },

                new AOpCode { mnemonic = "PMADDWD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xf5 },
                new AOpCode { mnemonic = "PMADDWD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xf5 },

                new AOpCode { mnemonic = "PMAXSW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xee },
                new AOpCode { mnemonic = "PMAXSW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xee },

                new AOpCode { mnemonic = "PMAXUB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xde },
                new AOpCode { mnemonic = "PMAXUB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xde },

                new AOpCode { mnemonic = "PMINSW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xea },
                new AOpCode { mnemonic = "PMINSW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xea },

                new AOpCode { mnemonic = "PMINUB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xda },
                new AOpCode { mnemonic = "PMINUB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xda },

                new AOpCode { mnemonic = "PMOVMSKB", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_mm, paramtype3 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0xd7 },
                new AOpCode { mnemonic = "PMOVMSKB", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_xmm, paramtype3 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xd7 },

                new AOpCode { mnemonic = "PMULHUL", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xe4 },
                new AOpCode { mnemonic = "PMULHUL", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xe4 },

                new AOpCode { mnemonic = "PMULHW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xe5 },
                new AOpCode { mnemonic = "PMULHW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xe5 },

                new AOpCode { mnemonic = "PMULLW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xd5 },
                new AOpCode { mnemonic = "PMULLW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xd5 },

                new AOpCode { mnemonic = "PMULUDQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xf4 },
                new AOpCode { mnemonic = "PMULUDQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xf4 },

                new AOpCode { mnemonic = "POP", opcode1 = AExtraOpCode.eo_prd, paramtype1 = AParam.par_r32, bytes = 1, bt1 = 0x58, norexw = true },
                new AOpCode { mnemonic = "POP", opcode1 = AExtraOpCode.eo_prw, paramtype1 = AParam.par_r16, bytes = 2, bt1 = 0x66, bt2 = 0x58 },

                new AOpCode { mnemonic = "POP", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_rm32, bytes = 1, bt1 = 0x8f },
                new AOpCode { mnemonic = "POP", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0x8f },

                new AOpCode { mnemonic = "POP", paramtype1 = AParam.par_ds, bytes = 1, bt1 = 0x1f },
                new AOpCode { mnemonic = "POP", paramtype1 = AParam.par_es, bytes = 1, bt1 = 0x07 },
                new AOpCode { mnemonic = "POP", paramtype1 = AParam.par_ss, bytes = 1, bt1 = 0x17 },
                new AOpCode { mnemonic = "POP", paramtype1 = AParam.par_fs, bytes = 2, bt1 = 0x0f, bt2 = 0xa1 },
                new AOpCode { mnemonic = "POP", paramtype1 = AParam.par_gs, bytes = 2, bt1 = 0x0f, bt2 = 0xa9 },

                new AOpCode { mnemonic = "POPA", bytes = 2, bt1 = 0x66, bt2 = 0x61 },
                new AOpCode { mnemonic = "POPAD", bytes = 1, bt1 = 0x61 },
                new AOpCode { mnemonic = "POPALL", bytes = 1, bt1 = 0x61 },

                new AOpCode { mnemonic = "POPF", bytes = 2, bt1 = 0x66, bt2 = 0x9d },
                new AOpCode { mnemonic = "POPFD", bytes = 1, bt1 = 0x9d, invalidin64bit = true },
                new AOpCode { mnemonic = "POPFQ", bytes = 1, bt1 = 0x9d, invalidin32bit = true },

                new AOpCode { mnemonic = "POR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xeb },
                new AOpCode { mnemonic = "POR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xeb },

                new AOpCode { mnemonic = "PREFETCH0", opcode1 = AExtraOpCode.eo_reg1, paramtype1 = AParam.par_m8, bytes = 2, bt1 = 0x0f, bt2 = 0x18 },
                new AOpCode { mnemonic = "PREFETCH1", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_m8, bytes = 2, bt1 = 0x0f, bt2 = 0x18 },
                new AOpCode { mnemonic = "PREFETCH2", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_m8, bytes = 2, bt1 = 0x0f, bt2 = 0x18 },
                new AOpCode { mnemonic = "PREFETCHA", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_m8, bytes = 2, bt1 = 0x0f, bt2 = 0x18 },

                new AOpCode { mnemonic = "PSADBW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xf6 },
                new AOpCode { mnemonic = "PSADBW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xf6 },

                new AOpCode { mnemonic = "PSHUFD", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, paramtype3 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x70 },
                new AOpCode { mnemonic = "PSHUFHW", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, paramtype3 = AParam.par_imm8, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x70 },
                new AOpCode { mnemonic = "PSHUFLW", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, paramtype3 = AParam.par_imm8, bytes = 3, bt1 = 0xf2, bt2 = 0x0f, bt3 = 0x70 },
                new AOpCode { mnemonic = "PSHUFW", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, paramtype3 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0x70 },

                new AOpCode { mnemonic = "PSLLD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xf2 },
                new AOpCode { mnemonic = "PSLLD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xf2 },

                new AOpCode { mnemonic = "PSLLD", opcode1 = AExtraOpCode.eo_reg6, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0x72 },
                new AOpCode { mnemonic = "PSLLD", opcode1 = AExtraOpCode.eo_reg6, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x72 },

                new AOpCode { mnemonic = "PSLLDQ", opcode1 = AExtraOpCode.eo_reg7, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x73 },

                new AOpCode { mnemonic = "PSLLQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xf3 },
                new AOpCode { mnemonic = "PSLLQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xf3 },

                new AOpCode { mnemonic = "PSLLQ", opcode1 = AExtraOpCode.eo_reg6, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0x73 },
                new AOpCode { mnemonic = "PSLLQ", opcode1 = AExtraOpCode.eo_reg6, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x73 },


                new AOpCode { mnemonic = "PSLLW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xf1 },
                new AOpCode { mnemonic = "PSLLW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xf1 },

                new AOpCode { mnemonic = "PSLLW", opcode1 = AExtraOpCode.eo_reg6, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0x71 },
                new AOpCode { mnemonic = "PSLLW", opcode1 = AExtraOpCode.eo_reg6, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x71 },

                new AOpCode { mnemonic = "PSQRTPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 2, bt1 = 0x0f, bt2 = 0x52 },
                new AOpCode { mnemonic = "PSQRTSS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m32, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x52 },

                new AOpCode { mnemonic = "PSRAD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xe2 },
                new AOpCode { mnemonic = "PSRAD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xe2 },

                new AOpCode { mnemonic = "PSRAD", opcode1 = AExtraOpCode.eo_reg4, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0x72 },
                new AOpCode { mnemonic = "PSRAD", opcode1 = AExtraOpCode.eo_reg4, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x72 },

                new AOpCode { mnemonic = "PSRAW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xe1 },
                new AOpCode { mnemonic = "PSRAW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xe1 },

                new AOpCode { mnemonic = "PSRAW", opcode1 = AExtraOpCode.eo_reg4, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0x71 },
                new AOpCode { mnemonic = "PSRAW", opcode1 = AExtraOpCode.eo_reg4, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x71 },

                new AOpCode { mnemonic = "PSRLD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xd2 },
                new AOpCode { mnemonic = "PSRLD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xd2 },

                new AOpCode { mnemonic = "PSRLD", opcode1 = AExtraOpCode.eo_reg2, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0x72 },
                new AOpCode { mnemonic = "PSRLD", opcode1 = AExtraOpCode.eo_reg2, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x72 },
                new AOpCode { mnemonic = "PSRLDQ", opcode1 = AExtraOpCode.eo_reg3, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x73 },

                new AOpCode { mnemonic = "PSRLQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xd3 },
                new AOpCode { mnemonic = "PSRLQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xd3 },

                new AOpCode { mnemonic = "PSRLQ", opcode1 = AExtraOpCode.eo_reg2, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0x73 },
                new AOpCode { mnemonic = "PSRLQ", opcode1 = AExtraOpCode.eo_reg2, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x73 },

                new AOpCode { mnemonic = "PSRLW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xd1 },
                new AOpCode { mnemonic = "PSRLW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xd1 },

                new AOpCode { mnemonic = "PSRLW", opcode1 = AExtraOpCode.eo_reg2, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0x71 },
                new AOpCode { mnemonic = "PSRLW", opcode1 = AExtraOpCode.eo_reg2, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x71 },

                new AOpCode { mnemonic = "PSUBB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xf8 },
                new AOpCode { mnemonic = "PSUBB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xf8 },

                new AOpCode { mnemonic = "PSUBD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xfa },
                new AOpCode { mnemonic = "PSUBD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xfa },

                new AOpCode { mnemonic = "PSUBQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xfb },
                new AOpCode { mnemonic = "PSUBQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xfb },

                new AOpCode { mnemonic = "PSUBUSB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xd8 },
                new AOpCode { mnemonic = "PSUBUSB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xd8 },

                new AOpCode { mnemonic = "PSUBUSW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xd9 },
                new AOpCode { mnemonic = "PSUBUSW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xd9 },

                new AOpCode { mnemonic = "PSUBW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xf9 },
                new AOpCode { mnemonic = "PSUBW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xf9 },

                new AOpCode { mnemonic = "PSUSB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xe8 },
                new AOpCode { mnemonic = "PSUSB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xe8 },

                new AOpCode { mnemonic = "PSUSW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xe9 },
                new AOpCode { mnemonic = "PSUSW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xe9 },

                new AOpCode { mnemonic = "PUNPCKHBW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x68 },
                new AOpCode { mnemonic = "PUNPCKHBW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x68 },

                new AOpCode { mnemonic = "PUNPCKHDQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x6a },
                new AOpCode { mnemonic = "PUNPCKHDQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x6a },

                new AOpCode { mnemonic = "PUNPCKHQDQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x6d },

                new AOpCode { mnemonic = "PUNPCKHWD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x69 },
                new AOpCode { mnemonic = "PUNPCKHWD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x69 },

                new AOpCode { mnemonic = "PUNPCKLBW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x60 },
                new AOpCode { mnemonic = "PUNPCKLBW", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x60 },

                new AOpCode { mnemonic = "PUNPCKLDQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x62 },
                new AOpCode { mnemonic = "PUNPCKLDQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x62 },

                new AOpCode { mnemonic = "PUNPCKLQDQ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x6c },

                new AOpCode { mnemonic = "PUNPCKLWD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0x61 },
                new AOpCode { mnemonic = "PUNPCKLWD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x61 },


                new AOpCode { mnemonic = "PUSH", opcode1 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_imm8, bytes = 1, bt1 = 0x6a },
                new AOpCode { mnemonic = "PUSH", opcode1 = AExtraOpCode.eo_id, paramtype1 = AParam.par_imm32, bytes = 1, bt1 = 0x68 },
                //  new topcode(){ mnemonic="PUSH", opcode1=textraopcode.eo_iw, paramtype1=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x68 }, todo do we need this?

                new AOpCode { mnemonic = "PUSH", opcode1 = AExtraOpCode.eo_prd, paramtype1 = AParam.par_r32, bytes = 1, bt1 = 0x50, norexw = true },
                new AOpCode { mnemonic = "PUSH", opcode1 = AExtraOpCode.eo_prw, paramtype1 = AParam.par_r16, bytes = 2, bt1 = 0x66, bt2 = 0x50 },

                new AOpCode { mnemonic = "PUSH", opcode1 = AExtraOpCode.eo_reg6, paramtype1 = AParam.par_rm32, bytes = 1, bt1 = 0xff },
                new AOpCode { mnemonic = "PUSH", opcode1 = AExtraOpCode.eo_reg6, paramtype1 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0xff },

                new AOpCode { mnemonic = "PUSH", paramtype1 = AParam.par_cs, bytes = 1, bt1 = 0x0e },
                new AOpCode { mnemonic = "PUSH", paramtype1 = AParam.par_ss, bytes = 1, bt1 = 0x16 },
                new AOpCode { mnemonic = "PUSH", paramtype1 = AParam.par_ds, bytes = 1, bt1 = 0x1e },
                new AOpCode { mnemonic = "PUSH", paramtype1 = AParam.par_es, bytes = 1, bt1 = 0x06 },
                new AOpCode { mnemonic = "PUSH", paramtype1 = AParam.par_fs, bytes = 2, bt1 = 0x0f, bt2 = 0xa0 },
                new AOpCode { mnemonic = "PUSH", paramtype1 = AParam.par_gs, bytes = 2, bt1 = 0x0f, bt2 = 0xa8 },

                new AOpCode { mnemonic = "PUSHA", bytes = 2, bt1 = 0x66, bt2 = 0x60, invalidin64bit = true },
                new AOpCode { mnemonic = "PUSHAD", bytes = 1, bt1 = 0x60, invalidin64bit = true },
                new AOpCode { mnemonic = "PUSHALL", bytes = 1, bt1 = 0x60, invalidin64bit = true },
                new AOpCode { mnemonic = "PUSHF", bytes = 2, bt1 = 0x66, bt2 = 0x9c },
                new AOpCode { mnemonic = "PUSHFD", bytes = 1, bt1 = 0x9c, invalidin64bit = true },
                new AOpCode { mnemonic = "PUSHFQ", bytes = 1, bt1 = 0x9c, invalidin32bit = true },

                new AOpCode { mnemonic = "PXOR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_mm, paramtype2 = AParam.par_mm_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xef },
                new AOpCode { mnemonic = "PXOR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xef },

                new AOpCode { mnemonic = "RCL", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_1, bytes = 1, bt1 = 0xd0 },
                new AOpCode { mnemonic = "RCL", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_cl, bytes = 1, bt1 = 0xd2 },
                new AOpCode { mnemonic = "RCL", opcode1 = AExtraOpCode.eo_reg2, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xc0 },

                new AOpCode { mnemonic = "RCL", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_1, bytes = 2, bt1 = 0x66, bt2 = 0xd1 },
                new AOpCode { mnemonic = "RCL", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_cl, bytes = 2, bt1 = 0x66, bt2 = 0xd3 },
                new AOpCode { mnemonic = "RCL", opcode1 = AExtraOpCode.eo_reg2, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0xc1 },

                new AOpCode { mnemonic = "RCL", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_1, bytes = 1, bt1 = 0xd1 },
                new AOpCode { mnemonic = "RCL", opcode1 = AExtraOpCode.eo_reg2, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_cl, bytes = 1, bt1 = 0xd3 },
                new AOpCode { mnemonic = "RCL", opcode1 = AExtraOpCode.eo_reg2, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xc1 },

                new AOpCode { mnemonic = "RCR", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_1, bytes = 1, bt1 = 0xd1 },
                new AOpCode { mnemonic = "RCR", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_cl, bytes = 1, bt1 = 0xd3 },
                new AOpCode { mnemonic = "RCR", opcode1 = AExtraOpCode.eo_reg3, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xc1 },

                new AOpCode { mnemonic = "RCR", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_1, bytes = 2, bt1 = 0x66, bt2 = 0xd1 },
                new AOpCode { mnemonic = "RCR", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_cl, bytes = 2, bt1 = 0x66, bt2 = 0xd3 },
                new AOpCode { mnemonic = "RCR", opcode1 = AExtraOpCode.eo_reg3, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0xc1 },

                new AOpCode { mnemonic = "RCR", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_1, bytes = 1, bt1 = 0xd0 },
                new AOpCode { mnemonic = "RCR", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_cl, bytes = 1, bt1 = 0xd2 },
                new AOpCode { mnemonic = "RCR", opcode1 = AExtraOpCode.eo_reg3, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xc0 },

                new AOpCode { mnemonic = "RDMSR", bytes = 2, bt1 = 0x0f, bt2 = 0x32 },
                new AOpCode { mnemonic = "RDPMC", bytes = 2, bt1 = 0x0f, bt2 = 0x33 },
                new AOpCode { mnemonic = "RDTSC", bytes = 2, bt1 = 0x0f, bt2 = 0x31 },

                new AOpCode { mnemonic = "RET", bytes = 1, bt1 = 0xc3 },
                new AOpCode { mnemonic = "RET", bytes = 1, bt1 = 0xcb },
                new AOpCode { mnemonic = "RET", opcode1 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_imm16, bytes = 1, bt1 = 0xc2 },
                new AOpCode { mnemonic = "RETN", bytes = 1, bt1 = 0xc3 },
                new AOpCode { mnemonic = "RETN", opcode1 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_imm16, bytes = 1, bt1 = 0xc2 },
                
                new AOpCode { mnemonic = "ROL", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_1, bytes = 1, bt1 = 0xd1 },
                new AOpCode { mnemonic = "ROL", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_cl, bytes = 1, bt1 = 0xd3 },
                new AOpCode { mnemonic = "ROL", opcode1 = AExtraOpCode.eo_reg0, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xc1 },

                new AOpCode { mnemonic = "ROL", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_1, bytes = 2, bt1 = 0x66, bt2 = 0xd1 },
                new AOpCode { mnemonic = "ROL", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_cl, bytes = 2, bt1 = 0x66, bt2 = 0xd3 },
                new AOpCode { mnemonic = "ROL", opcode1 = AExtraOpCode.eo_reg0, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0xc1 },

                new AOpCode { mnemonic = "ROL", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_1, bytes = 1, bt1 = 0xd0 },
                new AOpCode { mnemonic = "ROL", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_cl, bytes = 1, bt1 = 0xd2 },
                new AOpCode { mnemonic = "ROL", opcode1 = AExtraOpCode.eo_reg0, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xc0 },

                new AOpCode { mnemonic = "ROR", opcode1 = AExtraOpCode.eo_reg1, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_1, bytes = 1, bt1 = 0xd1 },
                new AOpCode { mnemonic = "ROR", opcode1 = AExtraOpCode.eo_reg1, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_cl, bytes = 1, bt1 = 0xd3 },
                new AOpCode { mnemonic = "ROR", opcode1 = AExtraOpCode.eo_reg1, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xc1 },

                new AOpCode { mnemonic = "ROR", opcode1 = AExtraOpCode.eo_reg1, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_1, bytes = 2, bt1 = 0x66, bt2 = 0xd1 },
                new AOpCode { mnemonic = "ROR", opcode1 = AExtraOpCode.eo_reg1, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_cl, bytes = 2, bt1 = 0x66, bt2 = 0xd3 },
                new AOpCode { mnemonic = "ROR", opcode1 = AExtraOpCode.eo_reg1, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0xc1 },

                new AOpCode { mnemonic = "ROR", opcode1 = AExtraOpCode.eo_reg1, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_1, bytes = 1, bt1 = 0xd0 },
                new AOpCode { mnemonic = "ROR", opcode1 = AExtraOpCode.eo_reg1, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_cl, bytes = 1, bt1 = 0xd2 },
                new AOpCode { mnemonic = "ROR", opcode1 = AExtraOpCode.eo_reg1, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xc0 },

                new AOpCode { mnemonic = "RSM", bytes = 2, bt1 = 0x0f, bt2 = 0xaa },

                new AOpCode { mnemonic = "SAHF", bytes = 1, bt1 = 0x9e },

                new AOpCode { mnemonic = "SAL", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_1, bytes = 1, bt1 = 0xd1 },
                new AOpCode { mnemonic = "SAL", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_cl, bytes = 1, bt1 = 0xd3 },
                new AOpCode { mnemonic = "SAL", opcode1 = AExtraOpCode.eo_reg4, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xc1 },

                new AOpCode { mnemonic = "SAL", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_1, bytes = 2, bt1 = 0x66, bt2 = 0xd1 },
                new AOpCode { mnemonic = "SAL", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_cl, bytes = 2, bt1 = 0x66, bt2 = 0xd3 },
                new AOpCode { mnemonic = "SAL", opcode1 = AExtraOpCode.eo_reg4, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0xc1 },

                new AOpCode { mnemonic = "SAL", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_1, bytes = 1, bt1 = 0xd0 },
                new AOpCode { mnemonic = "SAL", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_cl, bytes = 1, bt1 = 0xd2 },
                new AOpCode { mnemonic = "SAL", opcode1 = AExtraOpCode.eo_reg4, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xc0 },

                new AOpCode { mnemonic = "SAR", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_1, bytes = 1, bt1 = 0xd1 },
                new AOpCode { mnemonic = "SAR", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_cl, bytes = 1, bt1 = 0xd3 },
                new AOpCode { mnemonic = "SAR", opcode1 = AExtraOpCode.eo_reg7, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xc1 },

                new AOpCode { mnemonic = "SAR", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_1, bytes = 2, bt1 = 0x66, bt2 = 0xd1 },
                new AOpCode { mnemonic = "SAR", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_cl, bytes = 2, bt1 = 0x66, bt2 = 0xd3 },
                new AOpCode { mnemonic = "SAR", opcode1 = AExtraOpCode.eo_reg7, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0xc1 },

                new AOpCode { mnemonic = "SAR", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_1, bytes = 1, bt1 = 0xd0 },
                new AOpCode { mnemonic = "SAR", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_cl, bytes = 1, bt1 = 0xd2 },
                new AOpCode { mnemonic = "SAR", opcode1 = AExtraOpCode.eo_reg7, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xc0 },

                new AOpCode { mnemonic = "SBB", opcode1 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_al, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x1c },
                new AOpCode { mnemonic = "SBB", opcode1 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_ax, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0x1d },
                new AOpCode { mnemonic = "SBB", opcode1 = AExtraOpCode.eo_id, paramtype1 = AParam.par_eax, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0x1d },
                new AOpCode { mnemonic = "SBB", opcode1 = AExtraOpCode.eo_reg3, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x80 },
                new AOpCode { mnemonic = "SBB", opcode1 = AExtraOpCode.eo_reg3, opcode2 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0x80 },
                new AOpCode { mnemonic = "SBB", opcode1 = AExtraOpCode.eo_reg3, opcode2 = AExtraOpCode.eo_id, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0x81 },
                new AOpCode { mnemonic = "SBB", opcode1 = AExtraOpCode.eo_reg3, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0x83, signed = true },
                new AOpCode { mnemonic = "SBB", opcode1 = AExtraOpCode.eo_reg3, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x83, signed = true },
                new AOpCode { mnemonic = "SBB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_r8, bytes = 1, bt1 = 0x18 },
                new AOpCode { mnemonic = "SBB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, bytes = 2, bt1 = 0x66, bt2 = 0x19 },
                new AOpCode { mnemonic = "SBB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 1, bt1 = 0x19 },
                new AOpCode { mnemonic = "SBB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r8, paramtype2 = AParam.par_rm8, bytes = 1, bt1 = 0x1a },
                new AOpCode { mnemonic = "SBB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0x1b },
                new AOpCode { mnemonic = "SBB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 1, bt1 = 0x1b },

                new AOpCode { mnemonic = "SCASB", bytes = 1, bt1 = 0xae },
                new AOpCode { mnemonic = "SCASD", bytes = 1, bt1 = 0xaf },
                new AOpCode { mnemonic = "SCASW", bytes = 2, bt1 = 0x66, bt2 = 0xaf },

                new AOpCode { mnemonic = "SETA", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x97 },
                new AOpCode { mnemonic = "SETAE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x93 },
                new AOpCode { mnemonic = "SETB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x92 },
                new AOpCode { mnemonic = "SETBE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x96 },
                new AOpCode { mnemonic = "SETC", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x92 },
                new AOpCode { mnemonic = "SETE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x94 },
                new AOpCode { mnemonic = "SETG", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x9f },
                new AOpCode { mnemonic = "SETGE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x9d },
                new AOpCode { mnemonic = "SETL", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x9c },
                new AOpCode { mnemonic = "SETLE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x9e },
                new AOpCode { mnemonic = "SETNA", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x96 },

                new AOpCode { mnemonic = "SETNAE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x92 },
                new AOpCode { mnemonic = "SETNB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x93 },
                new AOpCode { mnemonic = "SETNBE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x97 },
                new AOpCode { mnemonic = "SETNC", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x93 },
                new AOpCode { mnemonic = "SETNE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x95 },
                new AOpCode { mnemonic = "SETNG", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x9e },
                new AOpCode { mnemonic = "SETNGE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x9c },
                new AOpCode { mnemonic = "SETNL", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x9d },
                new AOpCode { mnemonic = "SETNLE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x9f },
                new AOpCode { mnemonic = "SETNO", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x91 },
                new AOpCode { mnemonic = "SETNP", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x9b },

                new AOpCode { mnemonic = "SETNS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x99 },
                new AOpCode { mnemonic = "SETNZ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x95 },
                new AOpCode { mnemonic = "SETO", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x90 },
                new AOpCode { mnemonic = "SETP", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x9a },
                new AOpCode { mnemonic = "SETPE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x9a },
                new AOpCode { mnemonic = "SETPO", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x9b },
                new AOpCode { mnemonic = "SETS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x98 },
                new AOpCode { mnemonic = "SETZ", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, bytes = 2, bt1 = 0x0f, bt2 = 0x94 },

                new AOpCode { mnemonic = "SFENCE", bytes = 3, bt1 = 0x0f, bt2 = 0xae, bt3 = 0xf8 },

                new AOpCode { mnemonic = "SGDT", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_m32, bytes = 2, bt1 = 0x0f, bt2 = 0x01 },

                new AOpCode { mnemonic = "SHL", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_1, bytes = 1, bt1 = 0xd1 },
                new AOpCode { mnemonic = "SHL", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_1, bytes = 2, bt1 = 0x66, bt2 = 0xd1 },
                new AOpCode { mnemonic = "SHL", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_1, bytes = 1, bt1 = 0xd0 },
                new AOpCode { mnemonic = "SHL", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_cl, bytes = 1, bt1 = 0xd2 },
                new AOpCode { mnemonic = "SHL", opcode1 = AExtraOpCode.eo_reg4, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xc0 },

                new AOpCode { mnemonic = "SHL", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_cl, bytes = 2, bt1 = 0x66, bt2 = 0xd3 },
                new AOpCode { mnemonic = "SHL", opcode1 = AExtraOpCode.eo_reg4, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0xc1 },

                new AOpCode { mnemonic = "SHL", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_cl, bytes = 1, bt1 = 0xd3 },
                new AOpCode { mnemonic = "SHL", opcode1 = AExtraOpCode.eo_reg4, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xc1 },

                new AOpCode { mnemonic = "SHLD", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, paramtype3 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xa4 },
                new AOpCode { mnemonic = "SHLD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, paramtype3 = AParam.par_cl, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xa5 },

                new AOpCode { mnemonic = "SHLD", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, paramtype3 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0xa4 },
                new AOpCode { mnemonic = "SHLD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, paramtype3 = AParam.par_cl, bytes = 2, bt1 = 0x0f, bt2 = 0xa5 },

                new AOpCode { mnemonic = "SHR", opcode1 = AExtraOpCode.eo_reg5, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_1, bytes = 1, bt1 = 0xd0 },
                new AOpCode { mnemonic = "SHR", opcode1 = AExtraOpCode.eo_reg5, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_cl, bytes = 1, bt1 = 0xd2 },
                new AOpCode { mnemonic = "SHR", opcode1 = AExtraOpCode.eo_reg5, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xc0 },

                new AOpCode { mnemonic = "SHR", opcode1 = AExtraOpCode.eo_reg5, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_1, bytes = 2, bt1 = 0x66, bt2 = 0xd1 },
                new AOpCode { mnemonic = "SHR", opcode1 = AExtraOpCode.eo_reg5, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_cl, bytes = 2, bt1 = 0x66, bt2 = 0xd3 },
                new AOpCode { mnemonic = "SHR", opcode1 = AExtraOpCode.eo_reg5, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0xc1 },

                new AOpCode { mnemonic = "SHR", opcode1 = AExtraOpCode.eo_reg5, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_1, bytes = 1, bt1 = 0xd1 },
                new AOpCode { mnemonic = "SHR", opcode1 = AExtraOpCode.eo_reg5, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_cl, bytes = 1, bt1 = 0xd3 },
                new AOpCode { mnemonic = "SHR", opcode1 = AExtraOpCode.eo_reg5, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xc1 },

                new AOpCode { mnemonic = "SHRD", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, paramtype3 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0xac },
                new AOpCode { mnemonic = "SHRD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, paramtype3 = AParam.par_cl, bytes = 2, bt1 = 0x0f, bt2 = 0xad },

                new AOpCode { mnemonic = "SHUFPD", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, paramtype3 = AParam.par_imm8, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xc6 },
                new AOpCode { mnemonic = "SHUFPS", opcode1 = AExtraOpCode.eo_reg, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, paramtype3 = AParam.par_imm8, bytes = 2, bt1 = 0x0f, bt2 = 0xc6 },

                new AOpCode { mnemonic = "SIDT", opcode1 = AExtraOpCode.eo_reg1, paramtype1 = AParam.par_m32, bytes = 2, bt1 = 0x0f, bt2 = 0x01 },
                new AOpCode { mnemonic = "SLDT", opcode1 = AExtraOpCode.eo_reg0, paramtype1 = AParam.par_rm16, bytes = 2, bt1 = 0x0f, bt2 = 0x00 },

                new AOpCode { mnemonic = "SMSW", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_rm16, bytes = 2, bt1 = 0x0f, bt2 = 0x01 },

                new AOpCode { mnemonic = "SQRTPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x51 },
                new AOpCode { mnemonic = "SQRTPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 2, bt1 = 0x0f, bt2 = 0x51 },
                new AOpCode { mnemonic = "SQRTSD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m64, bytes = 3, bt1 = 0xf2, bt2 = 0x0f, bt3 = 0x51 },
                new AOpCode { mnemonic = "SQRTSS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m32, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x51 },

                new AOpCode { mnemonic = "STC", bytes = 1, bt1 = 0xf9 },
                new AOpCode { mnemonic = "STD", bytes = 1, bt1 = 0xfd },
                new AOpCode { mnemonic = "STI", bytes = 1, bt1 = 0xfb },

                new AOpCode { mnemonic = "STMXCSR", opcode1 = AExtraOpCode.eo_reg3, paramtype1 = AParam.par_m32, bytes = 2, bt1 = 0x0f, bt2 = 0xae },

                new AOpCode { mnemonic = "STOSB", bytes = 1, bt1 = 0xaa },
                new AOpCode { mnemonic = "STOSD", bytes = 1, bt1 = 0xab },
                new AOpCode { mnemonic = "STOSW", bytes = 2, bt1 = 0x66, bt2 = 0xab },

                new AOpCode { mnemonic = "STR", opcode1 = AExtraOpCode.eo_reg1, paramtype1 = AParam.par_rm16, bytes = 2, bt1 = 0x0f, bt2 = 0x00 },

                new AOpCode { mnemonic = "SUB", opcode1 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_al, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x2c },
                new AOpCode { mnemonic = "SUB", opcode1 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_ax, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0x2d },
                new AOpCode { mnemonic = "SUB", opcode1 = AExtraOpCode.eo_id, paramtype1 = AParam.par_eax, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0x2d },
                new AOpCode { mnemonic = "SUB", opcode1 = AExtraOpCode.eo_reg5, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x80 },
                new AOpCode { mnemonic = "SUB", opcode1 = AExtraOpCode.eo_reg5, opcode2 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0x80 },
                new AOpCode { mnemonic = "SUB", opcode1 = AExtraOpCode.eo_reg5, opcode2 = AExtraOpCode.eo_id, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0x81 },
                new AOpCode { mnemonic = "SUB", opcode1 = AExtraOpCode.eo_reg5, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0x83, signed = true },
                new AOpCode { mnemonic = "SUB", opcode1 = AExtraOpCode.eo_reg5, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x83, signed = true },
                new AOpCode { mnemonic = "SUB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_r8, bytes = 1, bt1 = 0x28 },
                new AOpCode { mnemonic = "SUB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, bytes = 2, bt1 = 0x66, bt2 = 0x29 },
                new AOpCode { mnemonic = "SUB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 1, bt1 = 0x29 },
                new AOpCode { mnemonic = "SUB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r8, paramtype2 = AParam.par_rm8, bytes = 1, bt1 = 0x2a },
                new AOpCode { mnemonic = "SUB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0x2b },
                new AOpCode { mnemonic = "SUB", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 1, bt1 = 0x2b },

                new AOpCode { mnemonic = "SUBPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x5c },
                new AOpCode { mnemonic = "SUBPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 2, bt1 = 0x0f, bt2 = 0x5c },
                new AOpCode { mnemonic = "SUBSD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m64, bytes = 3, bt1 = 0xf2, bt2 = 0x0f, bt3 = 0x5c },
                new AOpCode { mnemonic = "SUBSS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m32, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0x5c },
                new AOpCode { mnemonic = "SWAPGS", bytes = 3, bt1 = 0x0f, bt2 = 0x01, bt3 = 0xf8 },

                new AOpCode { mnemonic = "SYSENTER", bytes = 2, bt1 = 0x0f, bt2 = 0x34 },
                new AOpCode { mnemonic = "SYSEXIT", bytes = 2, bt1 = 0x0f, bt2 = 0x35 },

                new AOpCode { mnemonic = "TEST", opcode1 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_al, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xa8 },
                new AOpCode { mnemonic = "TEST", opcode1 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_ax, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0xa9 },
                new AOpCode { mnemonic = "TEST", opcode1 = AExtraOpCode.eo_id, paramtype1 = AParam.par_eax, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0xa9 },

                new AOpCode { mnemonic = "TEST", opcode1 = AExtraOpCode.eo_reg0, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0xf6 },
                new AOpCode { mnemonic = "TEST", opcode1 = AExtraOpCode.eo_reg0, opcode2 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0xf7 },
                new AOpCode { mnemonic = "TEST", opcode1 = AExtraOpCode.eo_reg0, opcode2 = AExtraOpCode.eo_id, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0xf7 },

                new AOpCode { mnemonic = "TEST", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_r8, bytes = 1, bt1 = 0x84 },
                new AOpCode { mnemonic = "TEST", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, bytes = 2, bt1 = 0x66, bt2 = 0x85 },
                new AOpCode { mnemonic = "TEST", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 1, bt1 = 0x85 },

                new AOpCode { mnemonic = "UCOMISD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m64, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x2e },
                new AOpCode { mnemonic = "UCOMISS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m32, bytes = 2, bt1 = 0x0f, bt2 = 0x2e },

                new AOpCode { mnemonic = "UD2", bytes = 2, bt1 = 0x0f, bt2 = 0x0b },

                new AOpCode { mnemonic = "UNPCKHPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x15 },
                new AOpCode { mnemonic = "UNPCKHPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 2, bt1 = 0x0f, bt2 = 0x15 },

                new AOpCode { mnemonic = "UNPCKLPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x14 },
                new AOpCode { mnemonic = "UNPCKLPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 2, bt1 = 0x0f, bt2 = 0x14 },

                new AOpCode { mnemonic = "VERR", opcode1 = AExtraOpCode.eo_reg4, paramtype1 = AParam.par_rm16, bytes = 2, bt1 = 0x0f, bt2 = 0x00 },
                new AOpCode { mnemonic = "VERW", opcode1 = AExtraOpCode.eo_reg5, paramtype1 = AParam.par_rm16, bytes = 2, bt1 = 0x0f, bt2 = 0x00 },

                new AOpCode { mnemonic = "VMCALL", bytes = 3, bt1 = 0x0f, bt2 = 0x01, bt3 = 0xc1 },
                new AOpCode { mnemonic = "VMCLEAR", opcode1 = AExtraOpCode.eo_reg6, paramtype1 = AParam.par_m64, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xc7 },
                new AOpCode { mnemonic = "VMLAUNCH", bytes = 3, bt1 = 0x0f, bt2 = 0x01, bt3 = 0xc2 },
                new AOpCode { mnemonic = "VMPTRLD", opcode1 = AExtraOpCode.eo_reg6, paramtype1 = AParam.par_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xc7 },
                new AOpCode { mnemonic = "VMPTRST", opcode1 = AExtraOpCode.eo_reg7, paramtype1 = AParam.par_m64, bytes = 2, bt1 = 0x0f, bt2 = 0xc7 },
                new AOpCode { mnemonic = "VMREAD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 2, bt1 = 0x0f, bt2 = 0x78 },
                new AOpCode { mnemonic = "VMRESUME", bytes = 3, bt1 = 0x0f, bt2 = 0x01, bt3 = 0xc3 },
                new AOpCode { mnemonic = "VMWRITE", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 2, bt1 = 0x0f, bt2 = 0x79 },
                new AOpCode { mnemonic = "VMXOFF", bytes = 3, bt1 = 0x0f, bt2 = 0x01, bt3 = 0xc4 },
                new AOpCode { mnemonic = "VMXON", opcode1 = AExtraOpCode.eo_reg6, paramtype1 = AParam.par_m64, bytes = 3, bt1 = 0xf3, bt2 = 0x0f, bt3 = 0xc7 },

                new AOpCode { mnemonic = "WAIT", bytes = 1, bt1 = 0x9b },
                new AOpCode { mnemonic = "WBINVD", bytes = 2, bt1 = 0x0f, bt2 = 0x09 },
                new AOpCode { mnemonic = "WRMSR", bytes = 2, bt1 = 0x0f, bt2 = 0x30 },

                new AOpCode { mnemonic = "XADD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_r8, bytes = 2, bt1 = 0x0f, bt2 = 0xc0 },
                new AOpCode { mnemonic = "XADD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0xc1 },
                new AOpCode { mnemonic = "XADD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 2, bt1 = 0x0f, bt2 = 0xc1 },

                new AOpCode { mnemonic = "XCHG", opcode1 = AExtraOpCode.eo_prd, paramtype1 = AParam.par_eax, paramtype2 = AParam.par_r32, bytes = 1, bt1 = 0x90 },
                new AOpCode { mnemonic = "XCHG", opcode1 = AExtraOpCode.eo_prw, paramtype1 = AParam.par_ax, paramtype2 = AParam.par_r16, bytes = 2, bt1 = 0x66, bt2 = 0x90 },

                new AOpCode { mnemonic = "XCHG", opcode1 = AExtraOpCode.eo_prw, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_ax, bytes = 2, bt1 = 0x66, bt2 = 0x90 },

                new AOpCode { mnemonic = "XCHG", opcode1 = AExtraOpCode.eo_prd, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_eax, bytes = 1, bt1 = 0x90 },

                new AOpCode { mnemonic = "XCHG", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_r8, bytes = 1, bt1 = 0x86 },
                new AOpCode { mnemonic = "XCHG", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r8, paramtype2 = AParam.par_rm8, bytes = 1, bt1 = 0x86 },

                new AOpCode { mnemonic = "XCHG", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, bytes = 2, bt1 = 0x66, bt2 = 0x87 },
                new AOpCode { mnemonic = "XCHG", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0x87 },

                new AOpCode { mnemonic = "XCHG", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 1, bt1 = 0x87 },
                new AOpCode { mnemonic = "XCHG", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 1, bt1 = 0x87 },

                new AOpCode { mnemonic = "XLATB", bytes = 1, bt1 = 0xd7 },

                new AOpCode { mnemonic = "XOR", opcode1 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_al, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x34 },
                new AOpCode { mnemonic = "XOR", opcode1 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_ax, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0x35 },
                new AOpCode { mnemonic = "XOR", opcode1 = AExtraOpCode.eo_id, paramtype1 = AParam.par_eax, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0x35 },
                new AOpCode { mnemonic = "XOR", opcode1 = AExtraOpCode.eo_reg6, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x80 },
                new AOpCode { mnemonic = "XOR", opcode1 = AExtraOpCode.eo_reg6, opcode2 = AExtraOpCode.eo_id, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm32, bytes = 1, bt1 = 0x81 },
                new AOpCode { mnemonic = "XOR", opcode1 = AExtraOpCode.eo_reg6, opcode2 = AExtraOpCode.eo_iw, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm16, bytes = 2, bt1 = 0x66, bt2 = 0x81 },
                new AOpCode { mnemonic = "XOR", opcode1 = AExtraOpCode.eo_reg6, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_imm8, bytes = 2, bt1 = 0x66, bt2 = 0x83, signed = true },
                new AOpCode { mnemonic = "XOR", opcode1 = AExtraOpCode.eo_reg6, opcode2 = AExtraOpCode.eo_ib, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_imm8, bytes = 1, bt1 = 0x83, signed = true },
                new AOpCode { mnemonic = "XOR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm8, paramtype2 = AParam.par_r8, bytes = 1, bt1 = 0x30 },
                new AOpCode { mnemonic = "XOR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm16, paramtype2 = AParam.par_r16, bytes = 2, bt1 = 0x66, bt2 = 0x31 },
                new AOpCode { mnemonic = "XOR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_rm32, paramtype2 = AParam.par_r32, bytes = 1, bt1 = 0x31 },
                new AOpCode { mnemonic = "XOR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r8, paramtype2 = AParam.par_rm8, bytes = 1, bt1 = 0x32 },
                new AOpCode { mnemonic = "XOR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r16, paramtype2 = AParam.par_rm16, bytes = 2, bt1 = 0x66, bt2 = 0x33 },
                new AOpCode { mnemonic = "XOR", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_r32, paramtype2 = AParam.par_rm32, bytes = 1, bt1 = 0x33 },

                new AOpCode { mnemonic = "XORPD", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 3, bt1 = 0x66, bt2 = 0x0f, bt3 = 0x57 },
                new AOpCode { mnemonic = "XORPS", opcode1 = AExtraOpCode.eo_reg, paramtype1 = AParam.par_xmm, paramtype2 = AParam.par_xmm_m128, bytes = 2, bt1 = 0x0f, bt2 = 0x57, },
            };
        }
        #endregion
    }
}
