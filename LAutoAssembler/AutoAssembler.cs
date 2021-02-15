using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sputnik.LGenerics;
using Sputnik.LUtils;

namespace Tack.LAutoAssembler
{
    public class AutoAssembler
    {
        #region dumy
        public class TSymhandler
        {
            public TProcess process= new TProcess();
            public IntPtr getaddressfromname(String name, Boolean waitforsymbols, out Boolean haserror)
            {
                if (name == "shinobi.exe")
                {
                    haserror = false;
                    return (IntPtr) 0x400300;
                }
                if (name == "cat")
                {
                    haserror = false;
                    return (IntPtr)0x777;
                }
                haserror = true;
                return IntPtr.Zero;
            }
        }
        public class TProcess
        {
            public Boolean is64bit = true;
        }
        #endregion
        #region opcodes
        public topcode[] opcodes = new[]
        {
            new topcode { mnemonic="AAA", opcode1=textraopcode.eo_none, opcode2=textraopcode.eo_none, paramtype1=tparam.par_noparam, paramtype2=tparam.par_noparam, paramtype3=tparam.par_noparam, bytes=1, bt1=0x37, bt2=0, bt3=0 }, //no param
            new topcode { mnemonic="AAD", opcode1=textraopcode.eo_none, opcode2=textraopcode.eo_none, paramtype1=tparam.par_noparam, paramtype2=tparam.par_noparam, paramtype3=tparam.par_noparam, bytes=2, bt1=0xd5, bt2=0x0a, bt3=0 },
            new topcode { mnemonic="AAD", opcode1=textraopcode.eo_ib, opcode2=textraopcode.eo_none, paramtype1=tparam.par_imm8, paramtype2=tparam.par_noparam, paramtype3=tparam.par_noparam, bytes=1, bt1=0xd5, bt2=0, bt3=0 },
            new topcode { mnemonic="AAM", opcode1=textraopcode.eo_none, paramtype1=tparam.par_noparam, bytes=2, bt1=0xd4, bt2=0x0a },
            new topcode { mnemonic="AAM", opcode1=textraopcode.eo_ib, paramtype1=tparam.par_imm8, bytes=1, bt1=0xd4 },
            new topcode { mnemonic="AAS", opcode1=textraopcode.eo_none, paramtype1=tparam.par_noparam, bytes=1, bt1=0x3F },
            new topcode { mnemonic="ADC", opcode1=textraopcode.eo_ib, paramtype1=tparam.par_al, paramtype2=tparam.par_imm8, bytes=1, bt1=0x14 },
            new topcode { mnemonic="ADC", opcode1=textraopcode.eo_iw, paramtype1=tparam.par_ax, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x15 },
            new topcode { mnemonic="ADC", opcode1=textraopcode.eo_id, paramtype1=tparam.par_eax, paramtype2=tparam.par_imm32, bytes=1, bt1=0x15 },
            new topcode { mnemonic="ADC", opcode1=textraopcode.eo_reg2, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm8, paramtype2=tparam.par_imm8, bytes=1, bt1=0x80 },//verified
            new topcode { mnemonic="ADC", opcode1=textraopcode.eo_reg2, opcode2=textraopcode.eo_iw, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x81 },
            new topcode { mnemonic="ADC", opcode1=textraopcode.eo_reg2, opcode2=textraopcode.eo_id, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm32, bytes=1, bt1=0x81 },
            new topcode { mnemonic="ADC", opcode1=textraopcode.eo_reg2, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0x83,  signed= true },
            new topcode { mnemonic="ADC", opcode1=textraopcode.eo_reg2, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=1, bt1=0x83,  signed= true },
            new topcode { mnemonic="ADC", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, paramtype2=tparam.par_r8, bytes=1, bt1=0x10 },
            new topcode { mnemonic="ADC", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, bytes=2, bt1=0x66, bt2=0x11 },
            new topcode { mnemonic="ADC", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=1, bt1=0x11 },
            new topcode { mnemonic="ADC", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r8, paramtype2=tparam.par_rm8, bytes=1, bt1=0x12 },
            new topcode { mnemonic="ADC", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0x13 },
            new topcode { mnemonic="ADC", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=1, bt1=0x13 },

            new topcode { mnemonic="ADD", opcode1=textraopcode.eo_ib, paramtype1=tparam.par_al, paramtype2=tparam.par_imm8, bytes=1, bt1=0x04 },
            new topcode { mnemonic="ADD", opcode1=textraopcode.eo_iw, paramtype1=tparam.par_ax, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x05 },
            new topcode { mnemonic="ADD", opcode1=textraopcode.eo_id, paramtype1=tparam.par_eax, paramtype2=tparam.par_imm32, bytes=1, bt1=0x05 },
            new topcode { mnemonic="ADD", opcode1=textraopcode.eo_reg0, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm8, paramtype2=tparam.par_imm8, bytes=1, bt1=0x80 },
            new topcode { mnemonic="ADD", opcode1=textraopcode.eo_reg0, opcode2=textraopcode.eo_iw, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x81 },
            new topcode { mnemonic="ADD", opcode1=textraopcode.eo_reg0, opcode2=textraopcode.eo_id, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm32, bytes=1, bt1=0x81 },
            new topcode { mnemonic="ADD", opcode1=textraopcode.eo_reg0, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0x83,  signed= true },
            new topcode { mnemonic="ADD", opcode1=textraopcode.eo_reg0, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=1, bt1=0x83,  signed= true },
            new topcode { mnemonic="ADD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=1, bt1=0x01 },
            new topcode { mnemonic="ADD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, bytes=2, bt1=0x66, bt2=0x01 },
            new topcode { mnemonic="ADD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, paramtype2=tparam.par_r8, bytes=1, bt1=0x00 },
            new topcode { mnemonic="ADD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=1, bt1=0x03 },
            new topcode { mnemonic="ADD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0x03 },
            new topcode { mnemonic="ADD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r8, paramtype2=tparam.par_rm8, bytes=1, bt1=0x02 },

            new topcode { mnemonic="ADDPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x58 }, //should be xmm1,xmm2/m128 but is also handled in all the others, in fact all other modrm types have it, hmmmmm....
            new topcode { mnemonic="ADDPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=2, bt1=0x0f, bt2=0x58 }, //I gues all reg,reg/mem can be handled like this. (oh well, i"m too lazy to change the code)
            new topcode { mnemonic="ADDSD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m64, bytes=3, bt1=0xf2, bt2=0x0f, bt3=0x58 },
            new topcode { mnemonic="ADDSS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m32, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x58 },

            new topcode { mnemonic="AND", opcode1=textraopcode.eo_ib, paramtype1=tparam.par_al, paramtype2=tparam.par_imm8, bytes=1, bt1=0x24 },
            new topcode { mnemonic="AND", opcode1=textraopcode.eo_iw, paramtype1=tparam.par_ax, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x25 },
            new topcode { mnemonic="AND", opcode1=textraopcode.eo_id, paramtype1=tparam.par_eax, paramtype2=tparam.par_imm32, bytes=1, bt1=0x25 },
            new topcode { mnemonic="AND", opcode1=textraopcode.eo_reg4, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm8, paramtype2=tparam.par_imm8, bytes=1, bt1=0x80 },
            new topcode { mnemonic="AND", opcode1=textraopcode.eo_reg4, opcode2=textraopcode.eo_iw, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x81 },
            new topcode { mnemonic="AND", opcode1=textraopcode.eo_reg4, opcode2=textraopcode.eo_id, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm32, bytes=1, bt1=0x81 },
            new topcode { mnemonic="AND", opcode1=textraopcode.eo_reg4, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0x83,  signed= true },
            new topcode { mnemonic="AND", opcode1=textraopcode.eo_reg4, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=1, bt1=0x83,  signed= true },
            new topcode { mnemonic="AND", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, paramtype2=tparam.par_r8, bytes=1, bt1=0x20 },
            new topcode { mnemonic="AND", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, bytes=2, bt1=0x66, bt2=0x21 },
            new topcode { mnemonic="AND", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=1, bt1=0x21 },
            new topcode { mnemonic="AND", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r8, paramtype2=tparam.par_rm8, bytes=1, bt1=0x22 },
            new topcode { mnemonic="AND", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0x23 },
            new topcode { mnemonic="AND", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=1, bt1=0x23 },

            new topcode { mnemonic="ANDNPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xff },
            new topcode { mnemonic="ANDNPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=2, bt1=0x0f, bt2=0x55 },

            new topcode { mnemonic="ANDPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x54 },
            new topcode { mnemonic="ANDPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=2, bt1=0x0f, bt2=0x54 },

            new topcode { mnemonic="ARPL", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, bytes=1, bt1=0x63 }, //textraopcode.eo_reg means I just need to find the reg and address
            new topcode { mnemonic="BOUND", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0x62 },
            new topcode { mnemonic="BOUND", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=1, bt1=0x62 },
            new topcode { mnemonic="BSF", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xbc },
            new topcode { mnemonic="BSF", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0xbc },
            new topcode { mnemonic="BSR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xbd },
            new topcode { mnemonic="BSR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0xbd },
            new topcode { mnemonic="BSWAP", opcode1=textraopcode.eo_prd, paramtype1=tparam.par_r32, bytes=2, bt1=0x0f, bt2=0xc8 }, //textraopcode.eo_prd
            new topcode { mnemonic="BSWAP", opcode1=textraopcode.eo_prw, paramtype1=tparam.par_r16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xc8 }, //textraopcode.eo_prw

            new topcode { mnemonic="BT", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xa3 },
            new topcode { mnemonic="BT", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=2, bt1=0x0f, bt2=0xa3 },
            new topcode { mnemonic="BT", opcode1=textraopcode.eo_reg4, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xba },
            new topcode { mnemonic="BT", opcode1=textraopcode.eo_reg4, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0xba },

            new topcode { mnemonic="BTC", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xbb },
            new topcode { mnemonic="BTC", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=2, bt1=0x0f, bt2=0xbb },
            new topcode { mnemonic="BTC", opcode1=textraopcode.eo_reg7, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xba },
            new topcode { mnemonic="BTC", opcode1=textraopcode.eo_reg7, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0xba },

            new topcode { mnemonic="BTR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xb3 },
            new topcode { mnemonic="BTR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=2, bt1=0x0f, bt2=0xb3 },
            new topcode { mnemonic="BTR", opcode1=textraopcode.eo_reg6, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xba },
            new topcode { mnemonic="BTR", opcode1=textraopcode.eo_reg6, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0xba },

            new topcode { mnemonic="BTS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xab },
            new topcode { mnemonic="BTS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=2, bt1=0x0f, bt2=0xab },
            new topcode { mnemonic="BTS", opcode1=textraopcode.eo_reg5, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xba },
            new topcode { mnemonic="BTS", opcode1=textraopcode.eo_reg5, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0xba },
            //no 0x66 0xE8 because it makes the address it jumps to 16 bit
            new topcode { mnemonic="CALL", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=1, bt1=0xe8 },
            //also no 0x66 0xff /2
            new topcode { mnemonic="CALL", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_rm32, bytes=1, bt1=0xff, norexw=true },
            new topcode { mnemonic="CBW", opcode1=textraopcode.eo_none, paramtype1=tparam.par_noparam, bytes=2, bt1=0x66, bt2=0x98 },
            new topcode { mnemonic="CDQ", bytes=1, bt1=0x99 },
            new topcode { mnemonic="CDQE", bytes=2, bt1=0x48, bt2=0x98 },

            new topcode { mnemonic="CLC", bytes=1, bt1=0xf8 },
            new topcode { mnemonic="CLD", bytes=1, bt1=0xfc },
            new topcode { mnemonic="CLFLUSH", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_m8, bytes=2, bt1=0x0f, bt2=0xae },
            new topcode { mnemonic="CLI", bytes=1, bt1=0xfa },
            new topcode { mnemonic="CLTS", bytes=2, bt1=0x0f, bt2=0x06 },
            new topcode { mnemonic="CMC", bytes=1, bt1=0xf5 },
            new topcode { mnemonic="CMOVA", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x47 },
            new topcode { mnemonic="CMOVA", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x47 },
            new topcode { mnemonic="CMOVAE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x43 },
            new topcode { mnemonic="CMOVAE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x43 },
            new topcode { mnemonic="CMOVB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x42 },
            new topcode { mnemonic="CMOVB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x42 },
            new topcode { mnemonic="CMOVBE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x46 },
            new topcode { mnemonic="CMOVBE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x46 },
            new topcode { mnemonic="CMOVC", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x42 },
            new topcode { mnemonic="CMOVC", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x42 },
            new topcode { mnemonic="CMOVE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x44 },
            new topcode { mnemonic="CMOVE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x44 },
            new topcode { mnemonic="CMOVG", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x4f },
            new topcode { mnemonic="CMOVG", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x4f },
            new topcode { mnemonic="CMOVGE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x4d },
            new topcode { mnemonic="CMOVGE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x4d },
            new topcode { mnemonic="CMOVL", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x4c },
            new topcode { mnemonic="CMOVL", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x4c },
            new topcode { mnemonic="CMOVLE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x4e },
            new topcode { mnemonic="CMOVLE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x4e },
            new topcode { mnemonic="CMOVNA", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x46 },
            new topcode { mnemonic="CMOVNA", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x46 },
            new topcode { mnemonic="CMOVNAE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x42 },
            new topcode { mnemonic="CMOVNAE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x42 },
            new topcode { mnemonic="CMOVNB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x43 },
            new topcode { mnemonic="CMOVNB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x43 },
            new topcode { mnemonic="CMOVNBE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x47 },
            new topcode { mnemonic="CMOVNBE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x47 },
            new topcode { mnemonic="CMOVNC", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x43 },
            new topcode { mnemonic="CMOVNC", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x43 },
            new topcode { mnemonic="CMOVNE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x45 },
            new topcode { mnemonic="CMOVNE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x45 },
            new topcode { mnemonic="CMOVNG", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x4e },
            new topcode { mnemonic="CMOVNG", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x4e },
            new topcode { mnemonic="CMOVNGE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x4c },
            new topcode { mnemonic="CMOVNGE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x4c },
            new topcode { mnemonic="CMOVNL", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x4d },
            new topcode { mnemonic="CMOVNL", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x4d },
            new topcode { mnemonic="CMOVNLE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x4f },
            new topcode { mnemonic="CMOVNLE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x4f },
            new topcode { mnemonic="CMOVNO", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x41 },
            new topcode { mnemonic="CMOVNO", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x41 },
            new topcode { mnemonic="CMOVNP", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x4b },
            new topcode { mnemonic="CMOVNP", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x4b },
            new topcode { mnemonic="CMOVNS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x49 },
            new topcode { mnemonic="CMOVNS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x49 },
            new topcode { mnemonic="CMOVNZ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x45 },
            new topcode { mnemonic="CMOVNZ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x45 },
            new topcode { mnemonic="CMOVO", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x40 },
            new topcode { mnemonic="CMOVO", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x40 },
            new topcode { mnemonic="CMOVP", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x4a },
            new topcode { mnemonic="CMOVP", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x4a },
            new topcode { mnemonic="CMOVPE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x4a },
            new topcode { mnemonic="CMOVPE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x4a },
            new topcode { mnemonic="CMOVPO", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x4b },
            new topcode { mnemonic="CMOVPO", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x4b },
            new topcode { mnemonic="CMOVS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x48 },
            new topcode { mnemonic="CMOVS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x48 },
            new topcode { mnemonic="CMOVZ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x44 },
            new topcode { mnemonic="CMOVZ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x44 },



            new topcode { mnemonic="CMP", opcode1=textraopcode.eo_ib, paramtype1=tparam.par_al, paramtype2=tparam.par_imm8, bytes=1, bt1=0x3C }, //2 bytes
            new topcode { mnemonic="CMP", opcode1=textraopcode.eo_iw, paramtype1=tparam.par_ax, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x3D }, //4 bytes
            new topcode { mnemonic="CMP", opcode1=textraopcode.eo_id, paramtype1=tparam.par_eax, paramtype2=tparam.par_imm32, bytes=1, bt1=0x3D }, //5 bytes
            new topcode { mnemonic="CMP", opcode1=textraopcode.eo_reg7, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm8, paramtype2=tparam.par_imm8, bytes=1, bt1=0x80 },
            new topcode { mnemonic="CMP", opcode1=textraopcode.eo_reg7, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0x83,  signed= true },
            new topcode { mnemonic="CMP", opcode1=textraopcode.eo_reg7, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=1, bt1=0x83,  signed= true },


            new topcode { mnemonic="CMP", opcode1=textraopcode.eo_reg7, opcode2=textraopcode.eo_iw, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x81 },
            new topcode { mnemonic="CMP", opcode1=textraopcode.eo_reg7, opcode2=textraopcode.eo_id, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm32, bytes=1, bt1=0x81 },
            new topcode { mnemonic="CMP", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, paramtype2=tparam.par_r8, bytes=1, bt1=0x38 },
            new topcode { mnemonic="CMP", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, bytes=2, bt1=0x66, bt2=0x39 },
            new topcode { mnemonic="CMP", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=1, bt1=0x39 },
            new topcode { mnemonic="CMP", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r8, paramtype2=tparam.par_rm8, bytes=1, bt1=0x3A },
            new topcode { mnemonic="CMP", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0x3B },
            new topcode { mnemonic="CMP", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=1, bt1=0x3B },

            new topcode { mnemonic="CMPPD", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, paramtype3=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xc2 },
            new topcode { mnemonic="CMPPS", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, paramtype3=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0xc2 },

            new topcode { mnemonic="CMPSB", bytes=1, bt1=0xa6 },
            new topcode { mnemonic="CMPSD", bytes=1, bt1=0xa7 },
            new topcode { mnemonic="CMPSD", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m64, paramtype3=tparam.par_imm8, bytes=3, bt1=0xf2, bt2=0x0f, bt3=0xc2 },
            new topcode { mnemonic="CMPSS", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m32, paramtype3=tparam.par_imm8, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0xc2 },
            new topcode { mnemonic="CMPSW", bytes=2, bt1=0x66, bt2=0xa7 },
            new topcode { mnemonic="CMPXCHG", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=2, bt1=0x0f, bt2=0xb0 },
            new topcode { mnemonic="CMPXCHG", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, bytes=2, bt1=0x66, bt2=0x0f, bt3=0xb1 },
            new topcode { mnemonic="CMPXCHG", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=2, bt1=0x0f, bt2=0xb1 },
            new topcode { mnemonic="CMPXCHG8B", opcode1=textraopcode.eo_reg1, paramtype1=tparam.par_m64, bytes=2, bt1=0x0f, bt2=0xc7 }, //no m64 as eo, seems it"s just a /1

            new topcode { mnemonic="COMISD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m64, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x2f },
            new topcode { mnemonic="COMISS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m32, bytes=2, bt1=0x0f, bt2=0x2f },

            new topcode { mnemonic="CPUID", bytes=2, bt1=0x0f, bt2=0xa2 },
            new topcode { mnemonic="CQO", bytes=2, bt1=0x48, bt2=0x99 },
            new topcode { mnemonic="CVTDQ2PD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m64, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0xe6 },  //just a gues, the documentation didn"t say anything about a /r, and the disassembler of delphi also doesn"t recognize it
            new topcode { mnemonic="CVTDQ2PS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=2, bt1=0x0f, bt2=0x5b },
            new topcode { mnemonic="CVTPD2DQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0xf2, bt2=0x0f, bt3=0xe6 },
            new topcode { mnemonic="CVTPD2PI", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x2d },

            new topcode { mnemonic="CVTPD2PS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x5a },
            new topcode { mnemonic="CVTPI2PD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_mm_m64, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x2a },
            new topcode { mnemonic="CVTPI2PS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0x2a },
            new topcode { mnemonic="CVTPS2DQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x5b },

            new topcode { mnemonic="CVTPS2PD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m64, bytes=2, bt1=0x0f, bt2=0x5a },
            new topcode { mnemonic="CVTPS2PI", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_xmm_m64, bytes=2, bt1=0x0f, bt2=0x2d },
            new topcode { mnemonic="CVTSD2SI", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_xmm_m64, bytes=3, bt1=0xf2, bt2=0x0f, bt3=0x2d },
            new topcode { mnemonic="CVTSD2SS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m64, bytes=3, bt1=0xf2, bt2=0x0f, bt3=0x5a },
            new topcode { mnemonic="CVTSI2SD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_rm32, bytes=3, bt1=0xf2, bt2=0x0f, bt3=0x2a },
            new topcode { mnemonic="CVTSI2SS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_rm32, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x2a },

            new topcode { mnemonic="CVTSS2SD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m32, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x5a },
            new topcode { mnemonic="CVTSS2SI", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_xmm_m32, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x2d },

            new topcode { mnemonic="CVTTPD2DQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xe6 },
            new topcode { mnemonic="CVTTPD2PI", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x2c },

            new topcode { mnemonic="CVTTPS2PI", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_xmm_m64, bytes=3, bt1=0x0f, bt2=0x2c },
            new topcode { mnemonic="CVTTSD2SI", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_xmm_m64, bytes=3, bt1=0xf2, bt2=0x0f, bt3=0x2c },
            new topcode { mnemonic="CVTTSS2SI", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_xmm_m64, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x2c },

            new topcode { mnemonic="CWD", bytes=1, bt1=0x99 },
            new topcode { mnemonic="CWDE", opcode1=textraopcode.eo_none, paramtype1=tparam.par_noparam, bytes=1, bt1=0x98 },
            new topcode { mnemonic="DAA", bytes=1, bt1=0x27 },
            new topcode { mnemonic="DAS", bytes=1, bt1=0x2F },
            new topcode { mnemonic="DEC", opcode1=textraopcode.eo_prw, paramtype1=tparam.par_r16, bytes=2, bt1=0x66, bt2=0x48, invalidin64bit=true },
            new topcode { mnemonic="DEC", opcode1=textraopcode.eo_prd, paramtype1=tparam.par_r32, bytes=1, bt1=0x48, invalidin64bit=true },
            new topcode { mnemonic="DEC", opcode1=textraopcode.eo_reg1, paramtype1=tparam.par_rm8, bytes=1, bt1=0xfe },
            new topcode { mnemonic="DEC", opcode1=textraopcode.eo_reg1, paramtype1=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0xff },
            new topcode { mnemonic="DEC", opcode1=textraopcode.eo_reg1, paramtype1=tparam.par_rm32, bytes=1, bt1=0xff },
            new topcode { mnemonic="DIV", opcode1=textraopcode.eo_reg6, paramtype1=tparam.par_rm8, bytes=1, bt1=0xf6 },
            new topcode { mnemonic="DIV", opcode1=textraopcode.eo_reg6, paramtype1=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0xf7 },
            new topcode { mnemonic="DIV", opcode1=textraopcode.eo_reg6, paramtype1=tparam.par_rm32, bytes=1, bt1=0xf7 },
            new topcode { mnemonic="DIVPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x5e },
            new topcode { mnemonic="DIVPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=2, bt1=0x0f, bt2=0x5e },
            new topcode { mnemonic="DIVSD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m64, bytes=3, bt1=0xf2, bt2=0x0f, bt3=0x5e },
            new topcode { mnemonic="DIVSS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m32, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x5e },
            new topcode { mnemonic="EMMS", bytes=2, bt1=0x0f, bt2=0x77 },
            new topcode { mnemonic="ENTER", opcode1=textraopcode.eo_iw, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_imm16, paramtype2=tparam.par_imm8, bytes=1, bt1=0xc8 },
            new topcode { mnemonic="F2XM1", bytes=2, bt1=0xd9, bt2=0xf0 },
            new topcode { mnemonic="FABS", bytes=2, bt1=0xd9, bt2=0xe1 },
            new topcode { mnemonic="FADD", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_m32, bytes=1, bt1=0xd8 },
            new topcode { mnemonic="FADD", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_m64, bytes=1, bt1=0xdc },
            new topcode { mnemonic="FADD", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xd8, bt2=0xc0 },
            new topcode { mnemonic="FADD", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xd8, bt2=0xc0 },
            new topcode { mnemonic="FADD", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, paramtype2=tparam.par_st0, bytes=2, bt1=0xdc, bt2=0xc0 },
            new topcode { mnemonic="FADDP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, paramtype2=tparam.par_st0, bytes=2, bt1=0xde, bt2=0xc0 },
            new topcode { mnemonic="FADDP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xde, bt2=0xc0 },
            new topcode { mnemonic="FADDP", bytes=2, bt1=0xde, bt2=0xc1 },

            new topcode { mnemonic="FBLD", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_m80, bytes=1, bt1=0xdf },
            new topcode { mnemonic="FBSTP", opcode1=textraopcode.eo_reg6, paramtype1=tparam.par_m80, bytes=1, bt1=0xdf },
            new topcode { mnemonic="FCHS", bytes=2, bt1=0xD9, bt2=0xe0 },
            new topcode { mnemonic="FCLEX", bytes=3, bt1=0x9b, bt2=0xdb, bt3=0xe2 },
            new topcode { mnemonic="FCMOVB", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xDA, bt2=0xc0 },
            new topcode { mnemonic="FCMOVB", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xDA, bt2=0xc0 },
            new topcode { mnemonic="FCMOVBE", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xDA, bt2=0xd0 },
            new topcode { mnemonic="FCMOVBE", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xDA, bt2=0xd0 },
            new topcode { mnemonic="FCMOVE", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xDA, bt2=0xc8 },
            new topcode { mnemonic="FCMOVE", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xDA, bt2=0xc8 },
            new topcode { mnemonic="FCMOVNB", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xDB, bt2=0xc0 },
            new topcode { mnemonic="FCMOVNB", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xDB, bt2=0xc0 },
            new topcode { mnemonic="FCMOVNBE", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xDB, bt2=0xd0 },
            new topcode { mnemonic="FCMOVNBE", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xDB, bt2=0xd0 },
            new topcode { mnemonic="FCMOVNE", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xDB, bt2=0xc8 },
            new topcode { mnemonic="FCMOVNE", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xDB, bt2=0xc8 },
            new topcode { mnemonic="FCMOVNU", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xDB, bt2=0xd8 },
            new topcode { mnemonic="FCMOVNU", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xDB, bt2=0xd8 },
            new topcode { mnemonic="FCMOVU", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xDA, bt2=0xd8 },
            new topcode { mnemonic="FCMOVU", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xDA, bt2=0xd8 },
            new topcode { mnemonic="FCOM", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_m32, bytes=1, bt1=0xd8 },
            new topcode { mnemonic="FCOM", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_m64, bytes=1, bt1=0xdc },
            new topcode { mnemonic="FCOM", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xd8, bt2=0xd0 },
            new topcode { mnemonic="FCOM", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xd8, bt2=0xd0 },
            new topcode { mnemonic="FCOM", bytes=2, bt1=0xd8, bt2=0xd1 },
            new topcode { mnemonic="FCOMP", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_m32, bytes=1, bt1=0xd8 },
            new topcode { mnemonic="FCOMP", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_m64, bytes=1, bt1=0xdc },
            new topcode { mnemonic="FCOMP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xd8, bt2=0xd8 },
            new topcode { mnemonic="FCOMP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xd8, bt2=0xd8 },
            new topcode { mnemonic="FCOMP", bytes=2, bt1=0xd8, bt2=0xd9 },
            new topcode { mnemonic="FCOMI", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xdb, bt2=0xf0 },
            new topcode { mnemonic="FCOMI", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xdb, bt2=0xf0 },
            new topcode { mnemonic="FCOMIP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xdf, bt2=0xf0 },
            new topcode { mnemonic="FCOMIP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xdf, bt2=0xf0 },
            new topcode { mnemonic="FCOMPP", bytes=2, bt1=0xde, bt2=0xd9 },

            new topcode { mnemonic="FCOMPP", bytes=2, bt1=0xde, bt2=0xd9 },
            new topcode { mnemonic="FCOS", bytes=2, bt1=0xD9, bt2=0xff },

            new topcode { mnemonic="FDECSTP", bytes=2, bt1=0xd9, bt2=0xf6 },

            new topcode { mnemonic="FDIV", opcode1=textraopcode.eo_reg6, paramtype1=tparam.par_m32, bytes=1, bt1=0xd8 },
            new topcode { mnemonic="FDIV", opcode1=textraopcode.eo_reg6, paramtype1=tparam.par_m64, bytes=1, bt1=0xdc },
            new topcode { mnemonic="FDIV", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xd8, bt2=0xf0 },
            new topcode { mnemonic="FDIV", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xd8, bt2=0xf0 },
            new topcode { mnemonic="FDIV", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, paramtype2=tparam.par_st0, bytes=2, bt1=0xdc, bt2=0xf8 },
            new topcode { mnemonic="FDIVP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, paramtype2=tparam.par_st0, bytes=2, bt1=0xde, bt2=0xf8 },
            new topcode { mnemonic="FDIVP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xde, bt2=0xf8 },
            new topcode { mnemonic="FDIVP", bytes=2, bt1=0xde, bt2=0xf9 },
            new topcode { mnemonic="FDIVR", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_m32, bytes=1, bt1=0xd8 },
            new topcode { mnemonic="FDIVR", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_m64, bytes=1, bt1=0xdc },
            new topcode { mnemonic="FDIVR", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xd8, bt2=0xf8 },
            new topcode { mnemonic="FDIVR", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xd8, bt2=0xf8 },
            new topcode { mnemonic="FDIVR", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, paramtype2=tparam.par_st0, bytes=2, bt1=0xdc, bt2=0xf0 },
            new topcode { mnemonic="FDIVRP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, paramtype2=tparam.par_st0, bytes=2, bt1=0xde, bt2=0xf0 },
            new topcode { mnemonic="FDIVRP", bytes=2, bt1=0xde, bt2=0xf1 },
            new topcode { mnemonic="FFREE", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xdd, bt2=0xc0 },

            new topcode { mnemonic="FIADD", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_m32, bytes=1, bt1=0xDA },
            new topcode { mnemonic="FIADD", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_m16, bytes=1, bt1=0xDE },

            new topcode { mnemonic="FICOM", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_m32, bytes=1, bt1=0xda },
            new topcode { mnemonic="FICOM", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_m16, bytes=1, bt1=0xde },
            new topcode { mnemonic="FICOMP", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_m32, bytes=1, bt1=0xda },
            new topcode { mnemonic="FICOMP", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_m16, bytes=1, bt1=0xde },

            new topcode { mnemonic="FIDIV", opcode1=textraopcode.eo_reg6, paramtype1=tparam.par_m32, bytes=1, bt1=0xda },
            new topcode { mnemonic="FIDIV", opcode1=textraopcode.eo_reg6, paramtype1=tparam.par_m16, bytes=1, bt1=0xde },

            new topcode { mnemonic="FIDIVR", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_m32, bytes=1, bt1=0xda },
            new topcode { mnemonic="FIDIVR", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_m16, bytes=1, bt1=0xde },


            new topcode { mnemonic="FILD", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_m16, bytes=1, bt1=0xdf }, //(I would have chosen to put 32 first, but I gues delphi used the same documentation as I did, cause it choose 16 as default)
            new topcode { mnemonic="FILD", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_m32, bytes=1, bt1=0xdb },
            new topcode { mnemonic="FILD", opcode1=textraopcode.eo_reg5, paramtype1=tparam.par_m64, bytes=1, bt1=0xdf },

            new topcode { mnemonic="FIMUL", opcode1=textraopcode.eo_reg1, paramtype1=tparam.par_m32, bytes=1, bt1=0xda },
            new topcode { mnemonic="FIMUL", opcode1=textraopcode.eo_reg1, paramtype1=tparam.par_m16, bytes=1, bt1=0xde },

            new topcode { mnemonic="FINCSTP", bytes=2, bt1=0xd9, bt2=0xf7 },
            new topcode { mnemonic="FINIT", bytes=3, bt1=0x9b, bt2=0xdb, bt3=0xe3 },

            new topcode { mnemonic="FIST", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_m32, bytes=1, bt1=0xdb },
            new topcode { mnemonic="FIST", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_m16, bytes=1, bt1=0xdf },
            new topcode { mnemonic="FISTP", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_m16, bytes=1, bt1=0xdf },
            new topcode { mnemonic="FISTP", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_m32, bytes=1, bt1=0xdb },
            new topcode { mnemonic="FISTP", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_m64, bytes=1, bt1=0xdf },

            new topcode { mnemonic="FISUB", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_m32, bytes=1, bt1=0xda },
            new topcode { mnemonic="FISUB", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_m16, bytes=1, bt1=0xde },
            new topcode { mnemonic="FISUBR", opcode1=textraopcode.eo_reg5, paramtype1=tparam.par_m32, bytes=1, bt1=0xda },
            new topcode { mnemonic="FISUBR", opcode1=textraopcode.eo_reg5, paramtype1=tparam.par_m16, bytes=1, bt1=0xde },

            new topcode { mnemonic="FLD", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_m64, bytes=1, bt1=0xdd },
            new topcode { mnemonic="FLD", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_m32, bytes=1, bt1=0xd9 },
            new topcode { mnemonic="FLD", opcode1=textraopcode.eo_reg5, paramtype1=tparam.par_m80, bytes=1, bt1=0xdb },
            new topcode { mnemonic="FLD", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xd9, bt2=0xc0 },

            new topcode { mnemonic="FLD1", bytes=2, bt1=0xd9, bt2=0xe8 },
            new topcode { mnemonic="FLDCW", opcode1=textraopcode.eo_reg5, paramtype1=tparam.par_m16, bytes=1, bt1=0xd9 },
            new topcode { mnemonic="FLDENV", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_m32, bytes=1, bt1=0xd9 },
            new topcode { mnemonic="FLDL2E", bytes=2, bt1=0xd9, bt2=0xea },
            new topcode { mnemonic="FLDL2T", bytes=2, bt1=0xd9, bt2=0xe9 },
            new topcode { mnemonic="FLDLG2", bytes=2, bt1=0xd9, bt2=0xec },
            new topcode { mnemonic="FLDLN2", bytes=2, bt1=0xd9, bt2=0xed },
            new topcode { mnemonic="FLDPI", bytes=2, bt1=0xd9, bt2=0xeb },
            new topcode { mnemonic="FLDZ", bytes=2, bt1=0xd9, bt2=0xee },

            new topcode { mnemonic="FMUL", opcode1=textraopcode.eo_reg1, paramtype1=tparam.par_m32, bytes=1, bt1=0xd8 },
            new topcode { mnemonic="FMUL", opcode1=textraopcode.eo_reg1, paramtype1=tparam.par_m64, bytes=1, bt1=0xdc },
            new topcode { mnemonic="FMUL", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xd8, bt2=0xC8 },
            new topcode { mnemonic="FMUL", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xd8, bt2=0xC8 },
            new topcode { mnemonic="FMUL", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, paramtype2=tparam.par_st0, bytes=2, bt1=0xdc, bt2=0xC8 },
            new topcode { mnemonic="FMULP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, paramtype2=tparam.par_st0, bytes=2, bt1=0xde, bt2=0xC8 },
            new topcode { mnemonic="FMULP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xde, bt2=0xC8 },
            new topcode { mnemonic="FMULP", bytes=2, bt1=0xde, bt2=0xc9 },


            new topcode { mnemonic="FNINIT", bytes=2, bt1=0xdb, bt2=0xe3 },
            new topcode { mnemonic="FNLEX", bytes=2, bt1=0xDb, bt2=0xe2 },
            new topcode { mnemonic="FNOP", bytes=2, bt1=0xd9, bt2=0xd0 },
            new topcode { mnemonic="FNSAVE", opcode1=textraopcode.eo_reg6, paramtype1=tparam.par_m32, bytes=1, bt1=0xdd },

            new topcode { mnemonic="FNSTCW", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_m16, bytes=1, bt1=0xd9 },
            new topcode { mnemonic="FNSTENV", opcode1=textraopcode.eo_reg6, paramtype1=tparam.par_m32, bytes=1, bt1=0xd9 },

            new topcode { mnemonic="FNSTSW", paramtype1=tparam.par_ax, bytes=2, bt1=0xdf, bt2=0xe0 },
            new topcode { mnemonic="FNSTSW", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_m16, bytes=1, bt1=0xdd },


            new topcode { mnemonic="FPATAN", bytes=2, bt1=0xd9, bt2=0xf3 },
            new topcode { mnemonic="FPREM", bytes=2, bt1=0xd9, bt2=0xf8 },
            new topcode { mnemonic="FPREM1", bytes=2, bt1=0xd9, bt2=0xf5 },
            new topcode { mnemonic="FPTAN", bytes=2, bt1=0xd9, bt2=0xf2 },
            new topcode { mnemonic="FRNDINT", bytes=2, bt1=0xd9, bt2=0xfc },
            new topcode { mnemonic="FRSTOR", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_m32, bytes=1, bt1=0xdd },

            new topcode { mnemonic="FSAVE", opcode1=textraopcode.eo_reg6, paramtype1=tparam.par_m32, bytes=2, bt1=0x9b, bt2=0xdd },

            new topcode { mnemonic="FSCALE", bytes=2, bt1=0xd9, bt2=0xfd },
            new topcode { mnemonic="FSIN", bytes=2, bt1=0xd9, bt2=0xfe },
            new topcode { mnemonic="FSINCOS", bytes=2, bt1=0xd9, bt2=0xfb },
            new topcode { mnemonic="FSQRT", bytes=2, bt1=0xd9, bt2=0xfa },

            new topcode { mnemonic="FST", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_m32, bytes=1, bt1=0xd9 },
            new topcode { mnemonic="FST", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_m64, bytes=1, bt1=0xdd },
            new topcode { mnemonic="FST", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xdd, bt2=0xd0 },
            new topcode { mnemonic="FSTCW", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_m16, bytes=2, bt1=0x9b, bt2=0xd9 },
            new topcode { mnemonic="FSTENV", opcode1=textraopcode.eo_reg6, paramtype1=tparam.par_m32, bytes=2, bt1=0x9b, bt2=0xd9 },
            new topcode { mnemonic="FSTP", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_m32, bytes=1, bt1=0xd9 },
            new topcode { mnemonic="FSTP", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_m64, bytes=1, bt1=0xdd },
            new topcode { mnemonic="FSTP", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_m80, bytes=1, bt1=0xdb },
            new topcode { mnemonic="FSTP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xdd, bt2=0xd8 },


            new topcode { mnemonic="FSTSW", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_m16, bytes=2, bt1=0x9b, bt2=0xdd },
            new topcode { mnemonic="FSTSW", paramtype1=tparam.par_ax, bytes=3, bt1=0x9b, bt2=0xdf, bt3=0xe0 },


            new topcode { mnemonic="FSUB", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_m32, bytes=1, bt1=0xd8 },
            new topcode { mnemonic="FSUB", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_m64, bytes=1, bt1=0xdc },
            new topcode { mnemonic="FSUB", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xd8, bt2=0xe0 },
            new topcode { mnemonic="FSUB", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xdc, bt2=0xe8 },
            new topcode { mnemonic="FSUB", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, paramtype2=tparam.par_st0, bytes=2, bt1=0xdc, bt2=0xe8 },
            new topcode { mnemonic="FSUBP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, paramtype2=tparam.par_st0, bytes=2, bt1=0xde, bt2=0xe8 },
            new topcode { mnemonic="FSUBP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xde, bt2=0xe8 },
            new topcode { mnemonic="FSUBP", bytes=2, bt1=0xde, bt2=0xe9 },
            new topcode { mnemonic="FSUBR", opcode1=textraopcode.eo_reg5, paramtype1=tparam.par_m32, bytes=1, bt1=0xd8 },
            new topcode { mnemonic="FSUBR", opcode1=textraopcode.eo_reg5, paramtype1=tparam.par_m64, bytes=1, bt1=0xdc },
            new topcode { mnemonic="FSUBR", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xd8, bt2=0xe8 },
            new topcode { mnemonic="FSUBR", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xd8, bt2=0xe8 },
            new topcode { mnemonic="FSUBR", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, paramtype2=tparam.par_st0, bytes=2, bt1=0xdc, bt2=0xe0 },
            new topcode { mnemonic="FSUBRP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, paramtype2=tparam.par_st0, bytes=2, bt1=0xde, bt2=0xe0 },
            new topcode { mnemonic="FSUBRP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xde, bt2=0xe0 },
            new topcode { mnemonic="FSUBRP", bytes=2, bt1=0xde, bt2=0xe1 },
            new topcode { mnemonic="FTST", bytes=2, bt1=0xd9, bt2=0xe4 },

            new topcode { mnemonic="FUCOM", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xdd, bt2=0xe0 },
            new topcode { mnemonic="FUCOM", bytes=2, bt1=0xdd, bt2=0xe1 },
            new topcode { mnemonic="FUCOMI", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xdb, bt2=0xe8 },
            new topcode { mnemonic="FUCOMI", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xdb, bt2=0xe8 },
            new topcode { mnemonic="FUCOMIP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st0, paramtype2=tparam.par_st, bytes=2, bt1=0xdf, bt2=0xe8 },
            new topcode { mnemonic="FUCOMIP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xdf, bt2=0xe8 },
            new topcode { mnemonic="FUCOMP", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xdd, bt2=0xe8 },
            new topcode { mnemonic="FUCOMP", bytes=2, bt1=0xdd, bt2=0xe9 },
            new topcode { mnemonic="FUCOMPP", bytes=2, bt1=0xda, bt2=0xe9 },

            new topcode { mnemonic="FWAIT", bytes=1, bt1=0x9b },

            new topcode { mnemonic="FXAM", bytes=2, bt1=0xd9, bt2=0xe5 },
            new topcode { mnemonic="FXCH", opcode1=textraopcode.eo_pi, paramtype1=tparam.par_st, bytes=2, bt1=0xd9, bt2=0xc8 },
            new topcode { mnemonic="FXCH", bytes=2, bt1=0xd9, bt2=0xc9 },
            new topcode { mnemonic="FXRSTOR", opcode1=textraopcode.eo_reg1, paramtype1=tparam.par_m32, bytes=2, bt1=0x0f, bt2=0xae },
            new topcode { mnemonic="FXSAVE", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_m32, bytes=2, bt1=0x0f, bt2=0xae },
            new topcode { mnemonic="FXTRACT", bytes=2, bt1=0xd9, bt2=0xf4 },
            new topcode { mnemonic="FYL2X", bytes=2, bt1=0xd9, bt2=0xf1 },
            new topcode { mnemonic="FYL2XPI", bytes=2, bt1=0xd9, bt2=0xf9 },

            new topcode { mnemonic="HLT", bytes=1, bt1=0xf4 },

            new topcode { mnemonic="IDIV", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_rm8, bytes=1, bt1=0xf6 },
            new topcode { mnemonic="IDIV", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0xf7 },
            new topcode { mnemonic="IDIV", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_rm32, bytes=1, bt1=0xf7 },


            new topcode { mnemonic="IMUL", opcode1=textraopcode.eo_reg5, paramtype1=tparam.par_rm8, bytes=1, bt1=0xf6 },
            new topcode { mnemonic="IMUL", opcode1=textraopcode.eo_reg5, paramtype1=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0xf7 },
            new topcode { mnemonic="IMUL", opcode1=textraopcode.eo_reg5, paramtype1=tparam.par_rm32, bytes=1, bt1=0xf7 },

            new topcode { mnemonic="IMUL", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xaf },
            new topcode { mnemonic="IMUL", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0xaf },

            new topcode { mnemonic="IMUL", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, paramtype3=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0x6b },
            new topcode { mnemonic="IMUL", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, paramtype3=tparam.par_imm8, bytes=1, bt1=0x6b },

            new topcode { mnemonic="IMUL", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_r16, paramtype2=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0x6b },
            new topcode { mnemonic="IMUL", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_r32, paramtype2=tparam.par_imm8, bytes=1, bt1=0x6b },

            new topcode { mnemonic="IMUL", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_iw, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, paramtype3=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x69 },
            new topcode { mnemonic="IMUL", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_id, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, paramtype3=tparam.par_imm32, bytes=1, bt1=0x69 },

            new topcode { mnemonic="IMUL", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_iw, paramtype1=tparam.par_r16, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x69 },
            new topcode { mnemonic="IMUL", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_id, paramtype1=tparam.par_r32, paramtype2=tparam.par_imm32, bytes=1, bt1=0x69 },

            new topcode { mnemonic="IN", opcode1=textraopcode.eo_ib, paramtype1=tparam.par_al, paramtype2=tparam.par_imm8, bytes=1, bt1=0xe4 },
            new topcode { mnemonic="IN", opcode1=textraopcode.eo_ib, paramtype1=tparam.par_ax, paramtype2=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0xe5 },
            new topcode { mnemonic="IN", opcode1=textraopcode.eo_ib, paramtype1=tparam.par_eax, paramtype2=tparam.par_imm8, bytes=1, bt1=0xe5 },

            new topcode { mnemonic="IN", paramtype1=tparam.par_al, paramtype2=tparam.par_dx, bytes=1, bt1=0xec },
            new topcode { mnemonic="IN", paramtype1=tparam.par_ax, paramtype2=tparam.par_dx, bytes=2, bt1=0x66, bt2=0xed },
            new topcode { mnemonic="IN", paramtype1=tparam.par_eax, paramtype2=tparam.par_dx, bytes=1, bt1=0xed },

            new topcode { mnemonic="INC", opcode1=textraopcode.eo_prw, paramtype1=tparam.par_r16, bytes=2, bt1=0x66, bt2=0x40, invalidin64bit=true },
            new topcode { mnemonic="INC", opcode1=textraopcode.eo_prd, paramtype1=tparam.par_r32, bytes=1, bt1=0x40, invalidin64bit=true },
            new topcode { mnemonic="INC", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_rm8, bytes=1, bt1=0xfe },
            new topcode { mnemonic="INC", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0xff },
            new topcode { mnemonic="INC", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_rm32, bytes=1, bt1=0xff },

            new topcode { mnemonic="INSB", bytes=1, bt1=0x6c },
            new topcode { mnemonic="INSD", bytes=1, bt1=0x6d },
            new topcode { mnemonic="INSW", bytes=2, bt1=0x66, bt2=0x6d },

            new topcode { mnemonic="INT", paramtype1=tparam.par_3, bytes=1, bt1=0xcc },
            new topcode { mnemonic="INT", opcode1=textraopcode.eo_ib, paramtype1=tparam.par_imm8, bytes=1, bt1=0xcd },
            new topcode { mnemonic="INTO", bytes=1, bt1=0xce },

            new topcode { mnemonic="INVD", bytes=2, bt1=0x0f, bt2=0x08 },
            new topcode { mnemonic="INVLPG", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_m32, bytes=2, bt1=0x0f, bt2=0x01 },

            new topcode { mnemonic="IRET", bytes=2, bt1=0x66, bt2=0xcf },
            new topcode { mnemonic="IRETD", bytes=1, bt1=0xcf },
            new topcode { mnemonic="IRETQ", bytes=2, bt1=0x48, bt2=0xcf },

            new topcode { mnemonic="JA", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x77 },
            new topcode { mnemonic="JA", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x87 },
            new topcode { mnemonic="JAE", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x73 },
            new topcode { mnemonic="JAE", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x83 },
            new topcode { mnemonic="JB", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x72 },
            new topcode { mnemonic="JB", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x82 },
            new topcode { mnemonic="JBE", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x76 },
            new topcode { mnemonic="JBE", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x86 },
            new topcode { mnemonic="JC", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x72 },
            new topcode { mnemonic="JC", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x82 },

            new topcode { mnemonic="JCXZ", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=2, bt1=0x66, bt2=0xe3 },
            new topcode { mnemonic="JE", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x74 },
            new topcode { mnemonic="JE", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x84 },
            new topcode { mnemonic="JECXZ", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0xe3 },
            new topcode { mnemonic="JG", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x7f },
            new topcode { mnemonic="JG", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x8f },
            new topcode { mnemonic="JGE", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x7d },
            new topcode { mnemonic="JGE", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x8d },
            new topcode { mnemonic="JL", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x7c },
            new topcode { mnemonic="JL", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x8c },
            new topcode { mnemonic="JLE", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x7e },
            new topcode { mnemonic="JLE", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x8e },

            new topcode { mnemonic="JMP", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0xeb },
            new topcode { mnemonic="JMP", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=1, bt1=0xe9 },
            new topcode { mnemonic="JMP", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_rm32, bytes=1, bt1=0xff, norexw=true },



            new topcode { mnemonic="JNA", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x76 },
            new topcode { mnemonic="JNA", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x86 },
            new topcode { mnemonic="JNAE", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x72 },
            new topcode { mnemonic="JNAE", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x82 },
            new topcode { mnemonic="JNB", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x73 },
            new topcode { mnemonic="JNB", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x83 },
            new topcode { mnemonic="JNBE", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x77 },
            new topcode { mnemonic="JNBE", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x87 },
            new topcode { mnemonic="JNC", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x73 },
            new topcode { mnemonic="JNC", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x83 },
            new topcode { mnemonic="JNE", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x75 },
            new topcode { mnemonic="JNE", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x85 },
            new topcode { mnemonic="JNG", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x7e },
            new topcode { mnemonic="JNG", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x8e },
            new topcode { mnemonic="JNGE", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x7c },
            new topcode { mnemonic="JNGE", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x8c },
            new topcode { mnemonic="JNL", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x7d },
            new topcode { mnemonic="JNL", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x8d },

            new topcode { mnemonic="JNLE", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x7f },
            new topcode { mnemonic="JNLE", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x8f },
            new topcode { mnemonic="JNO", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x71 },
            new topcode { mnemonic="JNO", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x81 },
            new topcode { mnemonic="JNP", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x7b },
            new topcode { mnemonic="JNP", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x8b },
            new topcode { mnemonic="JNS", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x79 },
            new topcode { mnemonic="JNS", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x89 },
            new topcode { mnemonic="JNZ", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x75 },
            new topcode { mnemonic="JNZ", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x85 },
            new topcode { mnemonic="JO", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x70 },
            new topcode { mnemonic="JO", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x80 },
            new topcode { mnemonic="JP", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x7a },
            new topcode { mnemonic="JP", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x8a },
            new topcode { mnemonic="JPE", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x7a },
            new topcode { mnemonic="JPE", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x8a },
            new topcode { mnemonic="JPO", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x7b },
            new topcode { mnemonic="JPO", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x8b },
            new topcode { mnemonic="JS", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x78 },
            new topcode { mnemonic="JS", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x88 },
            new topcode { mnemonic="JZ", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0x74 },
            new topcode { mnemonic="JZ", opcode1=textraopcode.eo_cd, paramtype1=tparam.par_rel32, bytes=2, bt1=0x0f, bt2=0x84 },

            new topcode { mnemonic="LAHF", bytes=1, bt1=0x9f },
            new topcode { mnemonic="LAR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x02 },
            new topcode { mnemonic="LAR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x02 },

            new topcode { mnemonic="LDMXCSR", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_m32, bytes=2, bt1=0x0f, bt2=0xae },
            new topcode { mnemonic="LDS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_m16, bytes=2, bt1=0x66, bt2=0xc5 },
            new topcode { mnemonic="LDS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_m32, bytes=1, bt1=0xc5 },

            new topcode { mnemonic="LEA", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_m16, bytes=2, bt1=0x66, bt2=0x8d },
            new topcode { mnemonic="LEA", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_m32, bytes=1, bt1=0x8d },
            new topcode { mnemonic="LEAVE", bytes=1, bt1=0xc9 },

            new topcode { mnemonic="LES", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0xc4 },
            new topcode { mnemonic="LES", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=1, bt1=0xc4 },
            new topcode { mnemonic="LFENCE", bytes=3, bt1=0x0f, bt2=0xae, bt3=0xe8 },

            new topcode { mnemonic="LFS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_m16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xb4 },
            new topcode { mnemonic="LFS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_m32, bytes=2, bt1=0x0f, bt2=0xb4 },

            new topcode { mnemonic="LGDT", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_m16, bytes=2, bt1=0x0f, bt2=0x01 },
            new topcode { mnemonic="LGDT", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_m32, bytes=2, bt1=0x0f, bt2=0x01 },

            new topcode { mnemonic="LGS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_m16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xb5 },
            new topcode { mnemonic="LGS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_m32, bytes=2, bt1=0x0f, bt2=0xb5 },

            new topcode { mnemonic="LIDT", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_m16, bytes=2, bt1=0x0f, bt2=0x01 },
            new topcode { mnemonic="LIDT", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_m32, bytes=2, bt1=0x0f, bt2=0x01 },

            new topcode { mnemonic="LLDT", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_rm16, bytes=2, bt1=0x0f, bt2=0x00 },
            new topcode { mnemonic="LMSW", opcode1=textraopcode.eo_reg6, paramtype1=tparam.par_rm16, bytes=2, bt1=0x0f, bt2=0x01 },

            new topcode { mnemonic="LODSB", bytes=1, bt1=0xac },
            new topcode { mnemonic="LODSD", bytes=1, bt1=0xad },
            new topcode { mnemonic="LODSW", bytes=2, bt1=0x66, bt2=0xad },

            new topcode { mnemonic="LOOP", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0xe2 },
            new topcode { mnemonic="LOOPE", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=2, bt1=0x66, bt2=0xe1 },
            new topcode { mnemonic="LOOPNE", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=2, bt1=0x66, bt2=0xe0 },
            new topcode { mnemonic="LOOPNZ", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0xe0 },
            new topcode { mnemonic="LOOPZ", opcode1=textraopcode.eo_cb, paramtype1=tparam.par_rel8, bytes=1, bt1=0xe1 },

            new topcode { mnemonic="LSL", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x03 },
            new topcode { mnemonic="LSL", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x03 },

            new topcode { mnemonic="LSS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_m16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xb2 },
            new topcode { mnemonic="LSS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_m32, bytes=2, bt1=0x0f, bt2=0xb2 },

            new topcode { mnemonic="LTR", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_rm16, bytes=2, bt1=0x0f, bt2=0x00 },

            new topcode { mnemonic="MASKMOVDQU", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_mm, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xf7 },
            new topcode { mnemonic="MASKMOVQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm, bytes=2, bt1=0x0f, bt2=0xf7 },
            new topcode { mnemonic="MAXPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x5f },
            new topcode { mnemonic="MAXPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=2, bt1=0x0f, bt2=0x5f },
            new topcode { mnemonic="MAXSD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m64, bytes=3, bt1=0xf2, bt2=0x0f, bt3=0x5f },
            new topcode { mnemonic="MAXSS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m32, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x5f },
            new topcode { mnemonic="MFENCE", bytes=3, bt1=0x0f, bt2=0xae, bt3=0xf0 },
            new topcode { mnemonic="MINPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x5d },
            new topcode { mnemonic="MINPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=2, bt1=0x0f, bt2=0x5d },
            new topcode { mnemonic="MINSD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m64, bytes=3, bt1=0xf2, bt2=0x0f, bt3=0x5d },
            new topcode { mnemonic="MINSS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m32, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x5d },

            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_id, paramtype1=tparam.par_al, paramtype2=tparam.par_moffs8, bytes=1, bt1=0xa0 },
            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_id, paramtype1=tparam.par_ax, paramtype2=tparam.par_moffs16, bytes=2, bt1=0x66, bt2=0xa1 },
            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_id, paramtype1=tparam.par_eax, paramtype2=tparam.par_moffs32, bytes=1, bt1=0xa1 },
            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_id, paramtype1=tparam.par_moffs8, paramtype2=tparam.par_al, bytes=1, bt1=0xa2 },
            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_id, paramtype1=tparam.par_moffs16, paramtype2=tparam.par_ax, bytes=2, bt1=0x66, bt2=0xa3 },
            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_id, paramtype1=tparam.par_moffs32, paramtype2=tparam.par_eax, bytes=1, bt1=0xa3 },

            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, paramtype2=tparam.par_r8, bytes=1, bt1=0x88 },
            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, bytes=2, bt1=0x66, bt2=0x89 },
            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=1, bt1=0x8b }, //8b prefered over 89 in case of r32,r32
            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=1, bt1=0x89 },
            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r8, paramtype2=tparam.par_rm8, bytes=1, bt1=0x8a },
            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0x8b },

            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_sreg, bytes=2, bt1=0x66, bt2=0x8c },
            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_sreg, paramtype2=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0x8e },



            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_prb, paramtype1=tparam.par_r8, paramtype2=tparam.par_imm8, bytes=1, bt1=0xb0 },
            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_prw, paramtype1=tparam.par_r16, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0xb8 },
            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_prd, paramtype1=tparam.par_r32, paramtype2=tparam.par_imm32, bytes=1, bt1=0xb8 },

            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_rm8, paramtype2=tparam.par_imm8, bytes=1, bt1=0xc6 },
            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0xc7 },
            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm32, bytes=1, bt1=0xc7 },

            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_cr, paramtype2=tparam.par_r32, bytes=2, bt1=0x0f, bt2=0x22 },
            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_cr, bytes=2, bt1=0x0f, bt2=0x20 },

            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_dr, bytes=2, bt1=0x0f, bt2=0x21 },
            new topcode { mnemonic="MOV", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_dr, paramtype2=tparam.par_r32, bytes=2, bt1=0x0f, bt2=0x23 },

            new topcode { mnemonic="MOVAPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x28 },

            new topcode { mnemonic="MOVAPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=2, bt1=0x0f, bt2=0x28 },
            new topcode { mnemonic="MOVAPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm_m128, paramtype2=tparam.par_xmm, bytes=2, bt1=0x0f, bt2=0x29 },

            new topcode { mnemonic="MOVD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x6e },
            new topcode { mnemonic="MOVD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_mm, bytes=2, bt1=0x0f, bt2=0x7e },

            new topcode { mnemonic="MOVD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_rm32, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x6e },
            new topcode { mnemonic="MOVD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_xmm, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x7e },

            new topcode { mnemonic="MOVDQ2Q", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_xmm, bytes=3, bt1=0xf2, bt2=0x0f, bt3=0xd6 },
            new topcode { mnemonic="MOVDQA", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x6f },
            new topcode { mnemonic="MOVDQA", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm_m128, paramtype2=tparam.par_xmm, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x7f },

            new topcode { mnemonic="MOVDQU", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x6f },
            new topcode { mnemonic="MOVDQU", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm_m128, paramtype2=tparam.par_xmm, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x7f },

            new topcode { mnemonic="MOVHLPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm, bytes=2, bt1=0x0f, bt2=0x12 },

            new topcode { mnemonic="MOVHPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_m64, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x16 },
            new topcode { mnemonic="MOVHPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_m64, paramtype2=tparam.par_xmm, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x17 },

            new topcode { mnemonic="MOVHPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_m64, bytes=2, bt1=0x0f, bt2=0x16 },
            new topcode { mnemonic="MOVHPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_m64, paramtype2=tparam.par_xmm, bytes=2, bt1=0x0f, bt2=0x17 },

            new topcode { mnemonic="MOVLHPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm, bytes=2, bt1=0x0f, bt3=0x16 },

            new topcode { mnemonic="MOVLPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_m64, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x12 },
            new topcode { mnemonic="MOVLPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_m64, paramtype2=tparam.par_xmm, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x13 },

            new topcode { mnemonic="MOVLPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_m64, bytes=2, bt1=0x0f, bt2=0x12 },
            new topcode { mnemonic="MOVLPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_m64, paramtype2=tparam.par_xmm, bytes=2, bt1=0x0f, bt2=0x13 },

            new topcode { mnemonic="MOVMSKPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_xmm, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x50 },
            new topcode { mnemonic="MOVMSKPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_xmm, bytes=2, bt1=0x0f, bt2=0x50 },
            new topcode { mnemonic="MOVNTDQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_m128, paramtype2=tparam.par_xmm, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xe7 },
            new topcode { mnemonic="MOVNTI", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_m32, paramtype2=tparam.par_r32, bytes=2, bt1=0x0f, bt2=0xc3 },

            new topcode { mnemonic="MOVNTPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_m128, paramtype2=tparam.par_xmm, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x2b },
            new topcode { mnemonic="MOVNTPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_m128, paramtype2=tparam.par_xmm, bytes=2, bt1=0x0f, bt2=0x2b },

            new topcode { mnemonic="MOVNTQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_m64, paramtype2=tparam.par_mm, bytes=2, bt1=0x0f, bt2=0xe7 },


            new topcode { mnemonic="MOVQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0x6f },
            new topcode { mnemonic="MOVQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm_m64, paramtype2=tparam.par_mm, bytes=2, bt1=0x0f, bt2=0x7f },

            new topcode { mnemonic="MOVQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m64, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x7e },
            new topcode { mnemonic="MOVQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm_m64, paramtype2=tparam.par_xmm, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xd6 },

            new topcode { mnemonic="MOVQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x6e },
            new topcode { mnemonic="MOVQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_rm32, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x7e },



            new topcode { mnemonic="MOVQ2DQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_mm, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xd6 },

            new topcode { mnemonic="MOVSB", bytes=1, bt1=0xa4 },
            new topcode { mnemonic="MOVSD", bytes=1, bt1=0xa5 },

            new topcode { mnemonic="MOVSD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m64, bytes=3, bt1=0xf2, bt2=0x0f, bt3=0x10 },
            new topcode { mnemonic="MOVSD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm_m64, paramtype2=tparam.par_xmm, bytes=3, bt1=0xf2, bt2=0x0f, bt3=0x11 },

            new topcode { mnemonic="MOVSS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m32, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x10 },
            new topcode { mnemonic="MOVSS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_m32, paramtype2=tparam.par_xmm, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x11 },
            new topcode { mnemonic="MOVSW", bytes=2, bt1=0x66, bt2=0xa5 },

            new topcode { mnemonic="MOVSX", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xbe },
            new topcode { mnemonic="MOVSX", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0xbe },
            new topcode { mnemonic="MOVSX", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm16, bytes=2, bt1=0x0f, bt2=0xbf },
            new topcode { mnemonic="MOVSXD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=1, bt1=0x63 },   //actuall r64,rm32 but the usage of the 64-bit register turns it into a rex_w itself

            new topcode { mnemonic="MOVUPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x10 },
            new topcode { mnemonic="MOVUPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm_m128, paramtype2=tparam.par_xmm, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x11 },

            new topcode { mnemonic="MOVUPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=2, bt1=0x0f, bt2=0x10 },
            new topcode { mnemonic="MOVUPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm_m128, paramtype2=tparam.par_xmm, bytes=2, bt1=0x0f, bt2=0x11 },

            new topcode { mnemonic="MOVZX", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xb6 },
            new topcode { mnemonic="MOVZX", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0xb6 },
            new topcode { mnemonic="MOVZX", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm16, bytes=2, bt1=0x0f, bt2=0xb7 },

            new topcode { mnemonic="MUL", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_rm8, bytes=1, bt1=0xf6 },
            new topcode { mnemonic="MUL", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0xf7 },
            new topcode { mnemonic="MUL", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_rm32, bytes=1, bt1=0xf7 },

            new topcode { mnemonic="MULPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x59 },
            new topcode { mnemonic="MULPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=2, bt1=0x0f, bt2=0x59 },
            new topcode { mnemonic="MULSD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m64, bytes=3, bt1=0xf2, bt2=0x0f, bt3=0x59 },
            new topcode { mnemonic="MULSS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m32, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x59 },

            new topcode { mnemonic="NEG", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_rm8, bytes=1, bt1=0xf6 },
            new topcode { mnemonic="NEG", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0xf7 },
            new topcode { mnemonic="NEG", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_rm32, bytes=1, bt1=0xf7 },

            new topcode { mnemonic="NOP", bytes=1, bt1=0x90 },  //NOP nop Nop nOp noP NoP nOp NOp nOP

            new topcode { mnemonic="NOT", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_rm8, bytes=1, bt1=0xf6 },
            new topcode { mnemonic="NOT", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0xf7 },
            new topcode { mnemonic="NOT", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_rm32, bytes=1, bt1=0xf7 },

            new topcode { mnemonic="OR", opcode1=textraopcode.eo_ib, paramtype1=tparam.par_al, paramtype2=tparam.par_imm8, bytes=1, bt1=0x0c },
            new topcode { mnemonic="OR", opcode1=textraopcode.eo_iw, paramtype1=tparam.par_ax, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x0d },
            new topcode { mnemonic="OR", opcode1=textraopcode.eo_id, paramtype1=tparam.par_eax, paramtype2=tparam.par_imm32, bytes=1, bt1=0x0d },
            new topcode { mnemonic="OR", opcode1=textraopcode.eo_reg1, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm8, paramtype2=tparam.par_imm8, bytes=1, bt1=0x80 },
            new topcode { mnemonic="OR", opcode1=textraopcode.eo_reg1, opcode2=textraopcode.eo_iw, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x80 },
            new topcode { mnemonic="OR", opcode1=textraopcode.eo_reg1, opcode2=textraopcode.eo_id, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm32, bytes=1, bt1=0x81 },
            new topcode { mnemonic="OR", opcode1=textraopcode.eo_reg1, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0x83,  signed= true },
            new topcode { mnemonic="OR", opcode1=textraopcode.eo_reg1, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=1, bt1=0x83,  signed= true },

            new topcode { mnemonic="OR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, paramtype2=tparam.par_r8, bytes=1, bt1=0x08 },
            new topcode { mnemonic="OR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, bytes=2, bt1=0x66, bt2=0x09 },
            new topcode { mnemonic="OR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=1, bt1=0x09 },
            new topcode { mnemonic="OR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r8, paramtype2=tparam.par_rm8, bytes=1, bt1=0x0a },
            new topcode { mnemonic="OR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0x0b },
            new topcode { mnemonic="OR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=1, bt1=0x0b },

            new topcode { mnemonic="ORPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x56 },
            new topcode { mnemonic="ORPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=2, bt1=0x0f, bt2=0x56 },

            new topcode { mnemonic="OUT", opcode1=textraopcode.eo_ib, paramtype1=tparam.par_imm8, paramtype2=tparam.par_al, bytes=1, bt1=0xe6 },
            new topcode { mnemonic="OUT", opcode1=textraopcode.eo_ib, paramtype1=tparam.par_imm8, paramtype2=tparam.par_ax, bytes=2, bt1=0x66, bt2=0xe7 },
            new topcode { mnemonic="OUT", opcode1=textraopcode.eo_ib, paramtype1=tparam.par_imm8, paramtype2=tparam.par_eax, bytes=1, bt1=0xe7 },

            new topcode { mnemonic="OUT", paramtype1=tparam.par_dx, paramtype2=tparam.par_al, bytes=1, bt1=0xee },
            new topcode { mnemonic="OUT", paramtype1=tparam.par_dx, paramtype2=tparam.par_ax, bytes=2, bt1=0x66, bt2=0xef },
            new topcode { mnemonic="OUT", paramtype1=tparam.par_dx, paramtype2=tparam.par_eax, bytes=1, bt1=0xef },

            new topcode { mnemonic="OUTSB", bytes=1, bt1=0x6e },
            new topcode { mnemonic="OUTSD", bytes=1, bt1=0x6f },
            new topcode { mnemonic="OUTSW", bytes=2, bt1=0x66, bt2=0x6f },

            new topcode { mnemonic="PACKSSDW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0x6b },
            new topcode { mnemonic="PACKSSDW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x6b },

            new topcode { mnemonic="PACKSSWB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0x63 },
            new topcode { mnemonic="PACKSSWB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x63 },


            new topcode { mnemonic="PACKUSWB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0x67 },
            new topcode { mnemonic="PACKUSWB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x67 },

            new topcode { mnemonic="PADDB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xfc },
            new topcode { mnemonic="PADDB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xfc },

            new topcode { mnemonic="PADDD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xfe },
            new topcode { mnemonic="PADDD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xfe },

            new topcode { mnemonic="PADDQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xd4 },
            new topcode { mnemonic="PADDQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xd4 },

            new topcode { mnemonic="PADDSB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xec },
            new topcode { mnemonic="PADDSB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xec },

            new topcode { mnemonic="PADDSW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xed },
            new topcode { mnemonic="PADDSW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xed },

            new topcode { mnemonic="PADDUSB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xdc },
            new topcode { mnemonic="PADDUSB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xdc },

            new topcode { mnemonic="PADDUSW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xdd },
            new topcode { mnemonic="PADDUSW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xdd },

            new topcode { mnemonic="PADDW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xfd },
            new topcode { mnemonic="PADDW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xfd },


            new topcode { mnemonic="PAND", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xdb },
            new topcode { mnemonic="PAND", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xdb },

            new topcode { mnemonic="PANDN", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xdf },
            new topcode { mnemonic="PANDN", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xdf },

            new topcode { mnemonic="PAUSE", bytes=2, bt1=0xf3, bt2=0x90 },

            new topcode { mnemonic="PAVGB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xe0 },
            new topcode { mnemonic="PAVGB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xe0 },

            new topcode { mnemonic="PAVGW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xe3 },
            new topcode { mnemonic="PAVGW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xe3 },

            new topcode { mnemonic="PCMPEQB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0x74 },
            new topcode { mnemonic="PCMPEQB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x74 },

            new topcode { mnemonic="PCMPEQD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0x76 },
            new topcode { mnemonic="PCMPEQD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x76 },

            new topcode { mnemonic="PCMPEQW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0x75 },
            new topcode { mnemonic="PCMPEQW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x75 },

            new topcode { mnemonic="PCMPGTB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0x64 },
            new topcode { mnemonic="PCMPGTB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x64 },

            new topcode { mnemonic="PCMPGTD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0x66 },
            new topcode { mnemonic="PCMPGTD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x66 },

            new topcode { mnemonic="PCMPGTW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0x65 },
            new topcode { mnemonic="PCMPGTW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x65 },

            new topcode { mnemonic="PCPPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=2, bt1=0x0f, bt2=0x53 },
            new topcode { mnemonic="PCPSS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x53 },

            new topcode { mnemonic="PEXTRW", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_r32, paramtype2=tparam.par_mm, paramtype3=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0xc5 },
            new topcode { mnemonic="PEXTRW", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_r32, paramtype2=tparam.par_xmm, paramtype3=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xc5 },

            new topcode { mnemonic="PINSRW", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_mm, paramtype2=tparam.par_r32_m16, paramtype3=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0xc4 },
            new topcode { mnemonic="PINSRW", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_r32_m16, paramtype3=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xc4 },

            new topcode { mnemonic="PMADDWD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xf5 },
            new topcode { mnemonic="PMADDWD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xf5 },

            new topcode { mnemonic="PMAXSW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xee },
            new topcode { mnemonic="PMAXSW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xee },

            new topcode { mnemonic="PMAXUB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xde },
            new topcode { mnemonic="PMAXUB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xde },

            new topcode { mnemonic="PMINSW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xea },
            new topcode { mnemonic="PMINSW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xea },

            new topcode { mnemonic="PMINUB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xda },
            new topcode { mnemonic="PMINUB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xda },

            new topcode { mnemonic="PMOVMSKB", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_r32, paramtype2=tparam.par_mm, paramtype3=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0xd7 },
            new topcode { mnemonic="PMOVMSKB", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_r32, paramtype2=tparam.par_xmm, paramtype3=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xd7 },

            new topcode { mnemonic="PMULHUL", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xe4 },
            new topcode { mnemonic="PMULHUL", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xe4 },

            new topcode { mnemonic="PMULHW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xe5 },
            new topcode { mnemonic="PMULHW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xe5 },

            new topcode { mnemonic="PMULLW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xd5 },
            new topcode { mnemonic="PMULLW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xd5 },

            new topcode { mnemonic="PMULUDQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xf4 },
            new topcode { mnemonic="PMULUDQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xf4 },

            new topcode { mnemonic="POP", opcode1=textraopcode.eo_prd, paramtype1=tparam.par_r32, bytes=1, bt1=0x58,  norexw= true },
            new topcode { mnemonic="POP", opcode1=textraopcode.eo_prw, paramtype1=tparam.par_r16, bytes=2, bt1=0x66, bt2=0x58 },

            new topcode { mnemonic="POP", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_rm32, bytes=1, bt1=0x8f },
            new topcode { mnemonic="POP", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0x8f },

            new topcode { mnemonic="POP", paramtype1=tparam.par_ds, bytes=1, bt1=0x1f },
            new topcode { mnemonic="POP", paramtype1=tparam.par_es, bytes=1, bt1=0x07 },
            new topcode { mnemonic="POP", paramtype1=tparam.par_ss, bytes=1, bt1=0x17 },
            new topcode { mnemonic="POP", paramtype1=tparam.par_fs, bytes=2, bt1=0x0f, bt2=0xa1 },
            new topcode { mnemonic="POP", paramtype1=tparam.par_gs, bytes=2, bt1=0x0f, bt2=0xa9 },

            new topcode { mnemonic="POPA", bytes=2, bt1=0x66, bt2=0x61 },
            new topcode { mnemonic="POPAD", bytes=1, bt1=0x61 },
            new topcode { mnemonic="POPALL", bytes=1, bt1=0x61 },

            new topcode { mnemonic="POPF", bytes=2, bt1=0x66, bt2=0x9d },
            new topcode { mnemonic="POPFD", bytes=1, bt1=0x9d,  invalidin64bit= true },
            new topcode { mnemonic="POPFQ", bytes=1, bt1=0x9d,  invalidin32bit= true },

            new topcode { mnemonic="POR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xeb },
            new topcode { mnemonic="POR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xeb },

            new topcode { mnemonic="PREFETCH0", opcode1=textraopcode.eo_reg1, paramtype1=tparam.par_m8, bytes=2, bt1=0x0f, bt2=0x18 },
            new topcode { mnemonic="PREFETCH1", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_m8, bytes=2, bt1=0x0f, bt2=0x18 },
            new topcode { mnemonic="PREFETCH2", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_m8, bytes=2, bt1=0x0f, bt2=0x18 },
            new topcode { mnemonic="PREFETCHA", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_m8, bytes=2, bt1=0x0f, bt2=0x18 },

            new topcode { mnemonic="PSADBW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xf6 },
            new topcode { mnemonic="PSADBW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xf6 },

            new topcode { mnemonic="PSHUFD", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, paramtype3=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x70 },
            new topcode { mnemonic="PSHUFHW", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, paramtype3=tparam.par_imm8, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x70 },
            new topcode { mnemonic="PSHUFLW", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, paramtype3=tparam.par_imm8, bytes=3, bt1=0xf2, bt2=0x0f, bt3=0x70 },
            new topcode { mnemonic="PSHUFW", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, paramtype3=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0x70 },


            new topcode { mnemonic="PSLLD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xf2 },
            new topcode { mnemonic="PSLLD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xf2 },

            new topcode { mnemonic="PSLLD", opcode1=textraopcode.eo_reg6, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_mm, paramtype2=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0x72 },
            new topcode { mnemonic="PSLLD", opcode1=textraopcode.eo_reg6, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x72 },

            new topcode { mnemonic="PSLLDQ", opcode1=textraopcode.eo_reg7, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x73 },

            new topcode { mnemonic="PSLLQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xf3 },
            new topcode { mnemonic="PSLLQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xf3 },

            new topcode { mnemonic="PSLLQ", opcode1=textraopcode.eo_reg6, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_mm, paramtype2=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0x73 },
            new topcode { mnemonic="PSLLQ", opcode1=textraopcode.eo_reg6, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x73 },


            new topcode { mnemonic="PSLLW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xf1 },
            new topcode { mnemonic="PSLLW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xf1 },

            new topcode { mnemonic="PSLLW", opcode1=textraopcode.eo_reg6, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_mm, paramtype2=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0x71 },
            new topcode { mnemonic="PSLLW", opcode1=textraopcode.eo_reg6, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x71 },

            new topcode { mnemonic="PSQRTPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=2, bt1=0x0f, bt2=0x52 },
            new topcode { mnemonic="PSQRTSS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m32, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x52 },


            new topcode { mnemonic="PSRAD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xe2 },
            new topcode { mnemonic="PSRAD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xe2 },

            new topcode { mnemonic="PSRAD", opcode1=textraopcode.eo_reg4, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_mm, paramtype2=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0x72 },
            new topcode { mnemonic="PSRAD", opcode1=textraopcode.eo_reg4, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x72 },

            new topcode { mnemonic="PSRAW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xe1 },
            new topcode { mnemonic="PSRAW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xe1 },

            new topcode { mnemonic="PSRAW", opcode1=textraopcode.eo_reg4, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_mm, paramtype2=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0x71 },
            new topcode { mnemonic="PSRAW", opcode1=textraopcode.eo_reg4, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x71 },



            new topcode { mnemonic="PSRLD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xd2 },
            new topcode { mnemonic="PSRLD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xd2 },

            new topcode { mnemonic="PSRLD", opcode1=textraopcode.eo_reg2, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_mm, paramtype2=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0x72 },
            new topcode { mnemonic="PSRLD", opcode1=textraopcode.eo_reg2, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x72 },
            new topcode { mnemonic="PSRLDQ", opcode1=textraopcode.eo_reg3, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x73 },

            new topcode { mnemonic="PSRLQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xd3 },
            new topcode { mnemonic="PSRLQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xd3 },

            new topcode { mnemonic="PSRLQ", opcode1=textraopcode.eo_reg2, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_mm, paramtype2=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0x73 },
            new topcode { mnemonic="PSRLQ", opcode1=textraopcode.eo_reg2, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x73 },

            new topcode { mnemonic="PSRLW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xd1 },
            new topcode { mnemonic="PSRLW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xd1 },

            new topcode { mnemonic="PSRLW", opcode1=textraopcode.eo_reg2, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_mm, paramtype2=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0x71 },
            new topcode { mnemonic="PSRLW", opcode1=textraopcode.eo_reg2, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x71 },



            new topcode { mnemonic="PSUBB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xf8 },
            new topcode { mnemonic="PSUBB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xf8 },

            new topcode { mnemonic="PSUBD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xfa },
            new topcode { mnemonic="PSUBD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xfa },

            new topcode { mnemonic="PSUBQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xfb },
            new topcode { mnemonic="PSUBQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xfb },

            new topcode { mnemonic="PSUBUSB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xd8 },
            new topcode { mnemonic="PSUBUSB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xd8 },

            new topcode { mnemonic="PSUBUSW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xd9 },
            new topcode { mnemonic="PSUBUSW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xd9 },


            new topcode { mnemonic="PSUBW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xf9 },
            new topcode { mnemonic="PSUBW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xf9 },


            new topcode { mnemonic="PSUSB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xe8 },
            new topcode { mnemonic="PSUSB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xe8 },

            new topcode { mnemonic="PSUSW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xe9 },
            new topcode { mnemonic="PSUSW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xe9 },


            new topcode { mnemonic="PUNPCKHBW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0x68 },
            new topcode { mnemonic="PUNPCKHBW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x68 },

            new topcode { mnemonic="PUNPCKHDQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0x6a },
            new topcode { mnemonic="PUNPCKHDQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x6a },

            new topcode { mnemonic="PUNPCKHQDQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x6d },

            new topcode { mnemonic="PUNPCKHWD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0x69 },
            new topcode { mnemonic="PUNPCKHWD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x69 },

            new topcode { mnemonic="PUNPCKLBW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0x60 },
            new topcode { mnemonic="PUNPCKLBW", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x60 },

            new topcode { mnemonic="PUNPCKLDQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0x62 },
            new topcode { mnemonic="PUNPCKLDQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x62 },

            new topcode { mnemonic="PUNPCKLQDQ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x6c },

            new topcode { mnemonic="PUNPCKLWD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0x61 },
            new topcode { mnemonic="PUNPCKLWD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x61 },


            new topcode { mnemonic="PUSH", opcode1=textraopcode.eo_ib, paramtype1=tparam.par_imm8, bytes=1, bt1=0x6a },
            new topcode { mnemonic="PUSH", opcode1=textraopcode.eo_id, paramtype1=tparam.par_imm32, bytes=1, bt1=0x68 },
            //  new topcode(){ mnemonic="PUSH", opcode1=textraopcode.eo_iw, paramtype1=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x68 },


            new topcode { mnemonic="PUSH", opcode1=textraopcode.eo_prd, paramtype1=tparam.par_r32, bytes=1, bt1=0x50, norexw= true },
            new topcode { mnemonic="PUSH", opcode1=textraopcode.eo_prw, paramtype1=tparam.par_r16, bytes=2, bt1=0x66, bt2=0x50 },

            new topcode { mnemonic="PUSH", opcode1=textraopcode.eo_reg6, paramtype1=tparam.par_rm32, bytes=1, bt1=0xff },
            new topcode { mnemonic="PUSH", opcode1=textraopcode.eo_reg6, paramtype1=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0xff },


            new topcode { mnemonic="PUSH", paramtype1=tparam.par_cs, bytes=1, bt1=0x0e },
            new topcode { mnemonic="PUSH", paramtype1=tparam.par_ss, bytes=1, bt1=0x16 },
            new topcode { mnemonic="PUSH", paramtype1=tparam.par_ds, bytes=1, bt1=0x1e },
            new topcode { mnemonic="PUSH", paramtype1=tparam.par_es, bytes=1, bt1=0x06 },
            new topcode { mnemonic="PUSH", paramtype1=tparam.par_fs, bytes=2, bt1=0x0f, bt2=0xa0 },
            new topcode { mnemonic="PUSH", paramtype1=tparam.par_gs, bytes=2, bt1=0x0f, bt2=0xa8 },

            new topcode { mnemonic="PUSHA", bytes=2, bt1=0x66, bt2=0x60, invalidin64bit= true },
            new topcode { mnemonic="PUSHAD", bytes=1, bt1=0x60, invalidin64bit= true },
            new topcode { mnemonic="PUSHALL", bytes=1, bt1=0x60, invalidin64bit= true },
            new topcode { mnemonic="PUSHF", bytes=2, bt1=0x66, bt2=0x9c },
            new topcode { mnemonic="PUSHFD", bytes=1, bt1=0x9c, invalidin64bit= true },
            new topcode { mnemonic="PUSHFQ", bytes=1, bt1=0x9c, invalidin32bit= true },

            new topcode { mnemonic="PXOR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_mm, paramtype2=tparam.par_mm_m64, bytes=2, bt1=0x0f, bt2=0xef },
            new topcode { mnemonic="PXOR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xef },

            new topcode { mnemonic="RCL", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_rm8, paramtype2=tparam.par_1, bytes=1, bt1=0xd0 },
            new topcode { mnemonic="RCL", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_rm8, paramtype2=tparam.par_cl, bytes=1, bt1=0xd2 },
            new topcode { mnemonic="RCL", opcode1=textraopcode.eo_reg2, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm8, paramtype2=tparam.par_imm8, bytes=1, bt1=0xc0 },

            new topcode { mnemonic="RCL", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_rm16, paramtype2=tparam.par_1, bytes=2, bt1=0x66, bt2=0xd1 },
            new topcode { mnemonic="RCL", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_rm16, paramtype2=tparam.par_cl, bytes=2, bt1=0x66, bt2=0xd3 },
            new topcode { mnemonic="RCL", opcode1=textraopcode.eo_reg2, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0xc1 },

            new topcode { mnemonic="RCL", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_rm32, paramtype2=tparam.par_1, bytes=1, bt1=0xd1 },
            new topcode { mnemonic="RCL", opcode1=textraopcode.eo_reg2, paramtype1=tparam.par_rm32, paramtype2=tparam.par_cl, bytes=1, bt1=0xd3 },
            new topcode { mnemonic="RCL", opcode1=textraopcode.eo_reg2, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=1, bt1=0xc1 },


            new topcode { mnemonic="RCR", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_rm32, paramtype2=tparam.par_1, bytes=1, bt1=0xd1 },
            new topcode { mnemonic="RCR", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_rm32, paramtype2=tparam.par_cl, bytes=1, bt1=0xd3 },
            new topcode { mnemonic="RCR", opcode1=textraopcode.eo_reg3, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=1, bt1=0xc1 },

            new topcode { mnemonic="RCR", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_rm16, paramtype2=tparam.par_1, bytes=2, bt1=0x66, bt2=0xd1 },
            new topcode { mnemonic="RCR", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_rm16, paramtype2=tparam.par_cl, bytes=2, bt1=0x66, bt2=0xd3 },
            new topcode { mnemonic="RCR", opcode1=textraopcode.eo_reg3, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0xc1 },

            new topcode { mnemonic="RCR", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_rm8, paramtype2=tparam.par_1, bytes=1, bt1=0xd0 },
            new topcode { mnemonic="RCR", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_rm8, paramtype2=tparam.par_cl, bytes=1, bt1=0xd2 },
            new topcode { mnemonic="RCR", opcode1=textraopcode.eo_reg3, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm8, paramtype2=tparam.par_imm8, bytes=1, bt1=0xc0 },




            new topcode { mnemonic="RDMSR", bytes=2, bt1=0x0f, bt2=0x32 },
            new topcode { mnemonic="RDPMC", bytes=2, bt1=0x0f, bt2=0x33 },
            new topcode { mnemonic="RDTSC", bytes=2, bt1=0x0f, bt2=0x31 },

            new topcode { mnemonic="RET", bytes=1, bt1=0xc3 },
            new topcode { mnemonic="RET", bytes=1, bt1=0xcb },
            new topcode { mnemonic="RET", opcode1=textraopcode.eo_iw, paramtype1=tparam.par_imm16, bytes=1, bt1=0xc2 },
            new topcode { mnemonic="RETN", bytes=1, bt1=0xc3 },
            new topcode { mnemonic="RETN", opcode1=textraopcode.eo_iw, paramtype1=tparam.par_imm16, bytes=1, bt1=0xc2 },




            new topcode { mnemonic="ROL", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_rm32, paramtype2=tparam.par_1, bytes=1, bt1=0xd1 },
            new topcode { mnemonic="ROL", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_rm32, paramtype2=tparam.par_cl, bytes=1, bt1=0xd3 },
            new topcode { mnemonic="ROL", opcode1=textraopcode.eo_reg0, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=1, bt1=0xc1 },

            new topcode { mnemonic="ROL", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_rm16, paramtype2=tparam.par_1, bytes=2, bt1=0x66, bt2=0xd1 },
            new topcode { mnemonic="ROL", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_rm16, paramtype2=tparam.par_cl, bytes=2, bt1=0x66, bt2=0xd3 },
            new topcode { mnemonic="ROL", opcode1=textraopcode.eo_reg0, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0xc1 },

            new topcode { mnemonic="ROL", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_rm8, paramtype2=tparam.par_1, bytes=1, bt1=0xd0 },
            new topcode { mnemonic="ROL", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_rm8, paramtype2=tparam.par_cl, bytes=1, bt1=0xd2 },
            new topcode { mnemonic="ROL", opcode1=textraopcode.eo_reg0, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm8, paramtype2=tparam.par_imm8, bytes=1, bt1=0xc0 },

            new topcode { mnemonic="ROR", opcode1=textraopcode.eo_reg1, paramtype1=tparam.par_rm32, paramtype2=tparam.par_1, bytes=1, bt1=0xd1 },
            new topcode { mnemonic="ROR", opcode1=textraopcode.eo_reg1, paramtype1=tparam.par_rm32, paramtype2=tparam.par_cl, bytes=1, bt1=0xd3 },
            new topcode { mnemonic="ROR", opcode1=textraopcode.eo_reg1, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=1, bt1=0xc1 },

            new topcode { mnemonic="ROR", opcode1=textraopcode.eo_reg1, paramtype1=tparam.par_rm16, paramtype2=tparam.par_1, bytes=2, bt1=0x66, bt2=0xd1 },
            new topcode { mnemonic="ROR", opcode1=textraopcode.eo_reg1, paramtype1=tparam.par_rm16, paramtype2=tparam.par_cl, bytes=2, bt1=0x66, bt2=0xd3 },
            new topcode { mnemonic="ROR", opcode1=textraopcode.eo_reg1, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0xc1 },

            new topcode { mnemonic="ROR", opcode1=textraopcode.eo_reg1, paramtype1=tparam.par_rm8, paramtype2=tparam.par_1, bytes=1, bt1=0xd0 },
            new topcode { mnemonic="ROR", opcode1=textraopcode.eo_reg1, paramtype1=tparam.par_rm8, paramtype2=tparam.par_cl, bytes=1, bt1=0xd2 },
            new topcode { mnemonic="ROR", opcode1=textraopcode.eo_reg1, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm8, paramtype2=tparam.par_imm8, bytes=1, bt1=0xc0 },



            new topcode { mnemonic="RSM", bytes=2, bt1=0x0f, bt2=0xaa },


            new topcode { mnemonic="SAHF", bytes=1, bt1=0x9e },

            new topcode { mnemonic="SAL", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_rm32, paramtype2=tparam.par_1, bytes=1, bt1=0xd1 },
            new topcode { mnemonic="SAL", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_rm32, paramtype2=tparam.par_cl, bytes=1, bt1=0xd3 },
            new topcode { mnemonic="SAL", opcode1=textraopcode.eo_reg4, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=1, bt1=0xc1 },

            new topcode { mnemonic="SAL", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_rm16, paramtype2=tparam.par_1, bytes=2, bt1=0x66, bt2=0xd1 },
            new topcode { mnemonic="SAL", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_rm16, paramtype2=tparam.par_cl, bytes=2, bt1=0x66, bt2=0xd3 },
            new topcode { mnemonic="SAL", opcode1=textraopcode.eo_reg4, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0xc1 },

            new topcode { mnemonic="SAL", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_rm8, paramtype2=tparam.par_1, bytes=1, bt1=0xd0 },
            new topcode { mnemonic="SAL", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_rm8, paramtype2=tparam.par_cl, bytes=1, bt1=0xd2 },
            new topcode { mnemonic="SAL", opcode1=textraopcode.eo_reg4, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm8, paramtype2=tparam.par_imm8, bytes=1, bt1=0xc0 },

            new topcode { mnemonic="SAR", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_rm32, paramtype2=tparam.par_1, bytes=1, bt1=0xd1 },
            new topcode { mnemonic="SAR", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_rm32, paramtype2=tparam.par_cl, bytes=1, bt1=0xd3 },
            new topcode { mnemonic="SAR", opcode1=textraopcode.eo_reg7, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=1, bt1=0xc1 },

            new topcode { mnemonic="SAR", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_rm16, paramtype2=tparam.par_1, bytes=2, bt1=0x66, bt2=0xd1 },
            new topcode { mnemonic="SAR", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_rm16, paramtype2=tparam.par_cl, bytes=2, bt1=0x66, bt2=0xd3 },
            new topcode { mnemonic="SAR", opcode1=textraopcode.eo_reg7, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0xc1 },

            new topcode { mnemonic="SAR", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_rm8, paramtype2=tparam.par_1, bytes=1, bt1=0xd0 },
            new topcode { mnemonic="SAR", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_rm8, paramtype2=tparam.par_cl, bytes=1, bt1=0xd2 },
            new topcode { mnemonic="SAR", opcode1=textraopcode.eo_reg7, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm8, paramtype2=tparam.par_imm8, bytes=1, bt1=0xc0 },

            new topcode { mnemonic="SBB", opcode1=textraopcode.eo_ib, paramtype1=tparam.par_al, paramtype2=tparam.par_imm8, bytes=1, bt1=0x1c },
            new topcode { mnemonic="SBB", opcode1=textraopcode.eo_iw, paramtype1=tparam.par_ax, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x1d },
            new topcode { mnemonic="SBB", opcode1=textraopcode.eo_id, paramtype1=tparam.par_eax, paramtype2=tparam.par_imm32, bytes=1, bt1=0x1d },
            new topcode { mnemonic="SBB", opcode1=textraopcode.eo_reg3, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm8, paramtype2=tparam.par_imm8, bytes=1, bt1=0x80 },
            new topcode { mnemonic="SBB", opcode1=textraopcode.eo_reg3, opcode2=textraopcode.eo_iw, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x80 },
            new topcode { mnemonic="SBB", opcode1=textraopcode.eo_reg3, opcode2=textraopcode.eo_id, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm32, bytes=1, bt1=0x81 },
            new topcode { mnemonic="SBB", opcode1=textraopcode.eo_reg3, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0x83,  signed= true },
            new topcode { mnemonic="SBB", opcode1=textraopcode.eo_reg3, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=1, bt1=0x83,  signed= true },
            new topcode { mnemonic="SBB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, paramtype2=tparam.par_r8, bytes=1, bt1=0x18 },
            new topcode { mnemonic="SBB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, bytes=2, bt1=0x66, bt2=0x19 },
            new topcode { mnemonic="SBB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=1, bt1=0x19 },
            new topcode { mnemonic="SBB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r8, paramtype2=tparam.par_rm8, bytes=1, bt1=0x1a },
            new topcode { mnemonic="SBB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0x1b },
            new topcode { mnemonic="SBB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=1, bt1=0x1b },

            new topcode { mnemonic="SCASB", bytes=1, bt1=0xae },
            new topcode { mnemonic="SCASD", bytes=1, bt1=0xaf },
            new topcode { mnemonic="SCASW", bytes=2, bt1=0x66, bt2=0xaf },


            new topcode { mnemonic="SETA", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x97 },
            new topcode { mnemonic="SETAE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x93 },
            new topcode { mnemonic="SETB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x92 },
            new topcode { mnemonic="SETBE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x96 },
            new topcode { mnemonic="SETC", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x92 },
            new topcode { mnemonic="SETE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x94 },
            new topcode { mnemonic="SETG", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x9f },
            new topcode { mnemonic="SETGE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x9d },
            new topcode { mnemonic="SETL", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x9c },
            new topcode { mnemonic="SETLE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x9e },
            new topcode { mnemonic="SETNA", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x96 },

            new topcode { mnemonic="SETNAE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x92 },
            new topcode { mnemonic="SETNB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x93 },
            new topcode { mnemonic="SETNBE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x97 },
            new topcode { mnemonic="SETNC", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x93 },
            new topcode { mnemonic="SETNE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x95 },
            new topcode { mnemonic="SETNG", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x9e },
            new topcode { mnemonic="SETNGE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x9c },
            new topcode { mnemonic="SETNL", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x9d },
            new topcode { mnemonic="SETNLE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x9f },
            new topcode { mnemonic="SETNO", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x91 },
            new topcode { mnemonic="SETNP", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x9b },

            new topcode { mnemonic="SETNS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x99 },
            new topcode { mnemonic="SETNZ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x95 },
            new topcode { mnemonic="SETO", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x90 },
            new topcode { mnemonic="SETP", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x9a },
            new topcode { mnemonic="SETPE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x9a },
            new topcode { mnemonic="SETPO", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x9b },
            new topcode { mnemonic="SETS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x98 },
            new topcode { mnemonic="SETZ", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, bytes=2, bt1=0x0f, bt2=0x94 },

            new topcode { mnemonic="SFENCE", bytes=3, bt1=0x0f, bt2=0xae, bt3=0xf8 },

            new topcode { mnemonic="SGDT", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_m32, bytes=2, bt1=0x0f, bt2=0x01 },

            new topcode { mnemonic="SHL", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_rm32, paramtype2=tparam.par_1, bytes=1, bt1=0xd1 },
            new topcode { mnemonic="SHL", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_rm16, paramtype2=tparam.par_1, bytes=2, bt1=0x66, bt2=0xd1 },
            new topcode { mnemonic="SHL", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_rm8, paramtype2=tparam.par_1, bytes=1, bt1=0xd0 },
            new topcode { mnemonic="SHL", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_rm8, paramtype2=tparam.par_cl, bytes=1, bt1=0xd2 },
            new topcode { mnemonic="SHL", opcode1=textraopcode.eo_reg4, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm8, paramtype2=tparam.par_imm8, bytes=1, bt1=0xc0 },

            new topcode { mnemonic="SHL", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_rm16, paramtype2=tparam.par_cl, bytes=2, bt1=0x66, bt2=0xd3 },
            new topcode { mnemonic="SHL", opcode1=textraopcode.eo_reg4, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0xc1 },

            new topcode { mnemonic="SHL", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_rm32, paramtype2=tparam.par_cl, bytes=1, bt1=0xd3 },
            new topcode { mnemonic="SHL", opcode1=textraopcode.eo_reg4, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=1, bt1=0xc1 },



            new topcode { mnemonic="SHLD", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, paramtype3=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xa4 },
            new topcode { mnemonic="SHLD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, paramtype3=tparam.par_cl, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xa5 },

            new topcode { mnemonic="SHLD", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, paramtype3=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0xa4 },
            new topcode { mnemonic="SHLD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, paramtype3=tparam.par_cl, bytes=2, bt1=0x0f, bt2=0xa5 },


            new topcode { mnemonic="SHR", opcode1=textraopcode.eo_reg5, paramtype1=tparam.par_rm8, paramtype2=tparam.par_1, bytes=1, bt1=0xd0 },
            new topcode { mnemonic="SHR", opcode1=textraopcode.eo_reg5, paramtype1=tparam.par_rm8, paramtype2=tparam.par_cl, bytes=1, bt1=0xd2 },
            new topcode { mnemonic="SHR", opcode1=textraopcode.eo_reg5, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm8, paramtype2=tparam.par_imm8, bytes=1, bt1=0xc0 },

            new topcode { mnemonic="SHR", opcode1=textraopcode.eo_reg5, paramtype1=tparam.par_rm16, paramtype2=tparam.par_1, bytes=2, bt1=0x66, bt2=0xd1 },
            new topcode { mnemonic="SHR", opcode1=textraopcode.eo_reg5, paramtype1=tparam.par_rm16, paramtype2=tparam.par_cl, bytes=2, bt1=0x66, bt2=0xd3 },
            new topcode { mnemonic="SHR", opcode1=textraopcode.eo_reg5, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0xc1 },

            new topcode { mnemonic="SHR", opcode1=textraopcode.eo_reg5, paramtype1=tparam.par_rm32, paramtype2=tparam.par_1, bytes=1, bt1=0xd1 },
            new topcode { mnemonic="SHR", opcode1=textraopcode.eo_reg5, paramtype1=tparam.par_rm32, paramtype2=tparam.par_cl, bytes=1, bt1=0xd3 },
            new topcode { mnemonic="SHR", opcode1=textraopcode.eo_reg5, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=1, bt1=0xc1 },

            new topcode { mnemonic="SHRD", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, paramtype3=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0xac },
            new topcode { mnemonic="SHRD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, paramtype3=tparam.par_cl, bytes=2, bt1=0x0f, bt2=0xad },

            new topcode { mnemonic="SHUFPD", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, paramtype3=tparam.par_imm8, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xc6 },
            new topcode { mnemonic="SHUFPS", opcode1=textraopcode.eo_reg, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, paramtype3=tparam.par_imm8, bytes=2, bt1=0x0f, bt2=0xc6 },

            new topcode { mnemonic="SIDT", opcode1=textraopcode.eo_reg1, paramtype1=tparam.par_m32, bytes=2, bt1=0x0f, bt2=0x01 },
            new topcode { mnemonic="SLDT", opcode1=textraopcode.eo_reg0, paramtype1=tparam.par_rm16, bytes=2, bt1=0x0f, bt2=0x00 },

            new topcode { mnemonic="SMSW", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_rm16, bytes=2, bt1=0x0f, bt2=0x01 },

            new topcode { mnemonic="SQRTPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x51 },
            new topcode { mnemonic="SQRTPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=2, bt1=0x0f, bt2=0x51 },
            new topcode { mnemonic="SQRTSD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m64, bytes=3, bt1=0xf2, bt2=0x0f, bt3=0x51 },
            new topcode { mnemonic="SQRTSS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m32, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x51 },

            new topcode { mnemonic="STC", bytes=1, bt1=0xf9 },
            new topcode { mnemonic="STD", bytes=1, bt1=0xfd },
            new topcode { mnemonic="STI", bytes=1, bt1=0xfb },

            new topcode { mnemonic="STMXCSR", opcode1=textraopcode.eo_reg3, paramtype1=tparam.par_m32, bytes=2, bt1=0x0f, bt2=0xae },

            new topcode { mnemonic="STOSB", bytes=1, bt1=0xaa },
            new topcode { mnemonic="STOSD", bytes=1, bt1=0xab },
            new topcode { mnemonic="STOSW", bytes=2, bt1=0x66, bt2=0xab },

            new topcode { mnemonic="STR", opcode1=textraopcode.eo_reg1, paramtype1=tparam.par_rm16, bytes=2, bt1=0x0f, bt2=0x00 },


            new topcode { mnemonic="SUB", opcode1=textraopcode.eo_ib, paramtype1=tparam.par_al, paramtype2=tparam.par_imm8, bytes=1, bt1=0x2c },
            new topcode { mnemonic="SUB", opcode1=textraopcode.eo_iw, paramtype1=tparam.par_ax, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x2d },
            new topcode { mnemonic="SUB", opcode1=textraopcode.eo_id, paramtype1=tparam.par_eax, paramtype2=tparam.par_imm32, bytes=1, bt1=0x2d },
            new topcode { mnemonic="SUB", opcode1=textraopcode.eo_reg5, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm8, paramtype2=tparam.par_imm8, bytes=1, bt1=0x80 },
            new topcode { mnemonic="SUB", opcode1=textraopcode.eo_reg5, opcode2=textraopcode.eo_iw, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x80 },
            new topcode { mnemonic="SUB", opcode1=textraopcode.eo_reg5, opcode2=textraopcode.eo_id, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm32, bytes=1, bt1=0x81 },
            new topcode { mnemonic="SUB", opcode1=textraopcode.eo_reg5, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0x83,  signed= true },
            new topcode { mnemonic="SUB", opcode1=textraopcode.eo_reg5, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=1, bt1=0x83,  signed= true },
            new topcode { mnemonic="SUB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, paramtype2=tparam.par_r8, bytes=1, bt1=0x28 },
            new topcode { mnemonic="SUB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, bytes=2, bt1=0x66, bt2=0x29 },
            new topcode { mnemonic="SUB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=1, bt1=0x29 },
            new topcode { mnemonic="SUB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r8, paramtype2=tparam.par_rm8, bytes=1, bt1=0x2a },
            new topcode { mnemonic="SUB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0x2b },
            new topcode { mnemonic="SUB", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=1, bt1=0x2b },

            new topcode { mnemonic="SUBPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x5c },
            new topcode { mnemonic="SUBPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=2, bt1=0x0f, bt2=0x5c },
            new topcode { mnemonic="SUBSD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m64, bytes=3, bt1=0xf2, bt2=0x0f, bt3=0x5c },
            new topcode { mnemonic="SUBSS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m32, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0x5c },
            new topcode { mnemonic="SWAPGS", bytes=3, bt1=0x0f, bt2=0x01, bt3=0xf8 },

            new topcode { mnemonic="SYSENTER", bytes=2, bt1=0x0f, bt2=0x34 },
            new topcode { mnemonic="SYSEXIT", bytes=2, bt1=0x0f, bt2=0x35 },


            new topcode { mnemonic="TEST", opcode1=textraopcode.eo_ib, paramtype1=tparam.par_al, paramtype2=tparam.par_imm8, bytes=1, bt1=0xa8 },
            new topcode { mnemonic="TEST", opcode1=textraopcode.eo_iw, paramtype1=tparam.par_ax, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0xa9 },
            new topcode { mnemonic="TEST", opcode1=textraopcode.eo_id, paramtype1=tparam.par_eax, paramtype2=tparam.par_imm32, bytes=1, bt1=0xa9 },

            new topcode { mnemonic="TEST", opcode1=textraopcode.eo_reg0, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm8, paramtype2=tparam.par_imm8, bytes=1, bt1=0xf6 },
            new topcode { mnemonic="TEST", opcode1=textraopcode.eo_reg0, opcode2=textraopcode.eo_iw, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0xf7 },
            new topcode { mnemonic="TEST", opcode1=textraopcode.eo_reg0, opcode2=textraopcode.eo_id, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm32, bytes=1, bt1=0xf7 },

            new topcode { mnemonic="TEST", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, paramtype2=tparam.par_r8, bytes=1, bt1=0x84 },
            new topcode { mnemonic="TEST", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, bytes=2, bt1=0x66, bt2=0x85 },
            new topcode { mnemonic="TEST", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=1, bt1=0x85 },


            new topcode { mnemonic="UCOMISD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m64, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x2e },
            new topcode { mnemonic="UCOMISS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m32, bytes=2, bt1=0x0f, bt2=0x2e },

            new topcode { mnemonic="UD2", bytes=2, bt1=0x0f, bt2=0x0b },

            new topcode { mnemonic="UNPCKHPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x15 },
            new topcode { mnemonic="UNPCKHPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=2, bt1=0x0f, bt2=0x15 },

            new topcode { mnemonic="UNPCKLPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x14 },
            new topcode { mnemonic="UNPCKLPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=2, bt1=0x0f, bt2=0x14 },

            new topcode { mnemonic="VERR", opcode1=textraopcode.eo_reg4, paramtype1=tparam.par_rm16, bytes=2, bt1=0x0f, bt2=0x00 },
            new topcode { mnemonic="VERW", opcode1=textraopcode.eo_reg5, paramtype1=tparam.par_rm16, bytes=2, bt1=0x0f, bt2=0x00 },

            new topcode { mnemonic="VMCALL", bytes=3, bt1=0x0f, bt2=0x01, bt3=0xc1 },
            new topcode { mnemonic="VMCLEAR", opcode1=textraopcode.eo_reg6, paramtype1=tparam.par_m64, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xc7 },
            new topcode { mnemonic="VMLAUNCH", bytes=3, bt1=0x0f, bt2=0x01, bt3=0xc2 },
            new topcode { mnemonic="VMPTRLD", opcode1=textraopcode.eo_reg6, paramtype1=tparam.par_m64, bytes=2, bt1=0x0f, bt2=0xc7 },
            new topcode { mnemonic="VMPTRST", opcode1=textraopcode.eo_reg7, paramtype1=tparam.par_m64, bytes=2, bt1=0x0f, bt2=0xc7 },
            new topcode { mnemonic="VMREAD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=2, bt1=0x0f, bt2=0x78 },
            new topcode { mnemonic="VMRESUME", bytes=3, bt1=0x0f, bt2=0x01, bt3=0xc3 },
            new topcode { mnemonic="VMWRITE", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=2, bt1=0x0f, bt2=0x79 },
            new topcode { mnemonic="VMXOFF", bytes=3, bt1=0x0f, bt2=0x01, bt3=0xc4 },
            new topcode { mnemonic="VMXON", opcode1=textraopcode.eo_reg6, paramtype1=tparam.par_m64, bytes=3, bt1=0xf3, bt2=0x0f, bt3=0xc7 },





            new topcode { mnemonic="WAIT", bytes=1, bt1=0x9b },
            new topcode { mnemonic="WBINVD", bytes=2, bt1=0x0f, bt2=0x09 },
            new topcode { mnemonic="WRMSR", bytes=2, bt1=0x0f, bt2=0x30 },

            new topcode { mnemonic="XADD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, paramtype2=tparam.par_r8, bytes=2, bt1=0x0f, bt2=0xc0 },
            new topcode { mnemonic="XADD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, bytes=3, bt1=0x66, bt2=0x0f, bt3=0xc1 },
            new topcode { mnemonic="XADD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=2, bt1=0x0f, bt2=0xc1 },

            new topcode { mnemonic="XCHG", opcode1=textraopcode.eo_prd, paramtype1=tparam.par_eax, paramtype2=tparam.par_r32, bytes=1, bt1=0x90 },
            new topcode { mnemonic="XCHG", opcode1=textraopcode.eo_prw, paramtype1=tparam.par_ax, paramtype2=tparam.par_r16, bytes=2, bt1=0x66, bt2=0x90 },

            new topcode { mnemonic="XCHG", opcode1=textraopcode.eo_prw, paramtype1=tparam.par_r16, paramtype2=tparam.par_ax, bytes=2, bt1=0x66, bt2=0x90 },

            new topcode { mnemonic="XCHG", opcode1=textraopcode.eo_prd, paramtype1=tparam.par_r32, paramtype2=tparam.par_eax, bytes=1, bt1=0x90 },

            new topcode { mnemonic="XCHG", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, paramtype2=tparam.par_r8, bytes=1, bt1=0x86 },
            new topcode { mnemonic="XCHG", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r8, paramtype2=tparam.par_rm8, bytes=1, bt1=0x86 },

            new topcode { mnemonic="XCHG", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, bytes=2, bt1=0x66, bt2=0x87 },
            new topcode { mnemonic="XCHG", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0x87 },

            new topcode { mnemonic="XCHG", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=1, bt1=0x87 },
            new topcode { mnemonic="XCHG", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=1, bt1=0x87 },

            new topcode { mnemonic="XLATB", bytes=1, bt1=0xd7 },

            new topcode { mnemonic="XOR", opcode1=textraopcode.eo_ib, paramtype1=tparam.par_al, paramtype2=tparam.par_imm8, bytes=1, bt1=0x34 },
            new topcode { mnemonic="XOR", opcode1=textraopcode.eo_iw, paramtype1=tparam.par_ax, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x35 },
            new topcode { mnemonic="XOR", opcode1=textraopcode.eo_id, paramtype1=tparam.par_eax, paramtype2=tparam.par_imm32, bytes=1, bt1=0x35 },
            new topcode { mnemonic="XOR", opcode1=textraopcode.eo_reg6, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm8, paramtype2=tparam.par_imm8, bytes=1, bt1=0x80 },
            new topcode { mnemonic="XOR", opcode1=textraopcode.eo_reg6, opcode2=textraopcode.eo_id, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm32, bytes=1, bt1=0x81 },
            new topcode { mnemonic="XOR", opcode1=textraopcode.eo_reg6, opcode2=textraopcode.eo_iw, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm16, bytes=2, bt1=0x66, bt2=0x81 },
            new topcode { mnemonic="XOR", opcode1=textraopcode.eo_reg6, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm16, paramtype2=tparam.par_imm8, bytes=2, bt1=0x66, bt2=0x83,  signed= true },
            new topcode { mnemonic="XOR", opcode1=textraopcode.eo_reg6, opcode2=textraopcode.eo_ib, paramtype1=tparam.par_rm32, paramtype2=tparam.par_imm8, bytes=1, bt1=0x83,  signed= true },
            new topcode { mnemonic="XOR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm8, paramtype2=tparam.par_r8, bytes=1, bt1=0x30 },
            new topcode { mnemonic="XOR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm16, paramtype2=tparam.par_r16, bytes=2, bt1=0x66, bt2=0x31 },
            new topcode { mnemonic="XOR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_rm32, paramtype2=tparam.par_r32, bytes=1, bt1=0x31 },
            new topcode { mnemonic="XOR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r8, paramtype2=tparam.par_rm8, bytes=1, bt1=0x32 },
            new topcode { mnemonic="XOR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r16, paramtype2=tparam.par_rm16, bytes=2, bt1=0x66, bt2=0x33 },
            new topcode { mnemonic="XOR", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_r32, paramtype2=tparam.par_rm32, bytes=1, bt1=0x33 },

            new topcode { mnemonic="XORPD", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=3, bt1=0x66, bt2=0x0f, bt3=0x57 },
            new topcode { mnemonic="XORPS", opcode1=textraopcode.eo_reg, paramtype1=tparam.par_xmm, paramtype2=tparam.par_xmm_m128, bytes=2, bt1=0x0f, bt2=0x57,  },
        };
        #endregion
        #region Variables
        public int parameter1, parameter2, parameter3;
        public int opcodenr;
        public tindexarray assemblerindex;
        public TSymhandler symhandler = new TSymhandler();
        public TSingleLineAssembler singlelineassembler;
        #endregion
        #region Properties
        public int opcodecount => opcodes.Length;
        #endregion
        #region Constructor
        public AutoAssembler()
        {
            parameter1 = 0;
            parameter2 = 0;
            parameter3 = 0;
            opcodenr = 0;
            assemblerindex = new tindexarray(25);
            var lastentry = 0;
            tindex lastindex = null;
            for (var i = 0; i < assemblerindex.Length; i++)
            {
                assemblerindex[i].startentry = -1;
                assemblerindex[i].nextentry = -1;
                assemblerindex[i].subindex = null;
                for (var j = lastentry; j < opcodecount; j++)
                {
                    if (opcodes[j].mnemonic[0] == 'A' + i)
                    {
                        Console.WriteLine(opcodes[j].mnemonic);
                        //found the first entry with this as first character
                        if (lastindex != null)
                            lastindex.nextentry = j;
                        lastindex = assemblerindex[i];
                        assemblerindex[i].startentry = j;
                        assemblerindex[i].subindex = null; //default initialization
                        lastentry = j;
                        break; // todo check if should be continue
                    }
                    if (opcodes[j].mnemonic[0] > 'A' + i)
                        break; // passed it // todo check if should be continue
                }
            }
            if (assemblerindex.Last.startentry != -1)
                assemblerindex.Last.nextentry = opcodecount;
            //fill in the subindexes
            for (var i = 0; i < assemblerindex.Length; i++)
            {
                if (assemblerindex[i].startentry == -1)
                    continue;
                //initialize subindex
                assemblerindex[i].subindex = new tindexarray(25);
                for (var j = 0; j < assemblerindex.Length; j++)
                {
                    assemblerindex[i].subindex[j].startentry = -1;
                    assemblerindex[i].subindex[j].nextentry = -1;
                    assemblerindex[i].subindex[j].subindex = null;
                }
                lastindex = null;
                if (assemblerindex[i].nextentry == -1)  //last one in the list didn't get a assignment
                    assemblerindex[i].nextentry = opcodecount + 1;
                for (var j = 0; j < assemblerindex.Length; j++)
                {
                    for (var k = assemblerindex[i].startentry; k < assemblerindex[i].nextentry - 1; k++)
                    {
                        if (opcodes[k].mnemonic[0] == 'A' + j)
                        {
                            if (lastindex != null)
                                lastindex.nextentry = k;
                            lastindex = assemblerindex[i].subindex[j];
                            assemblerindex[i].subindex[j].startentry = k;
                            break; // todo check if should be continue
                        }
                    }
                }
            }
            singlelineassembler = new TSingleLineAssembler(this);
        }
        #endregion
        #region getopcodesindex
        /*
        will return the first entry in the opcodes list for this opcode
        If not found, -1
        */
        public int getopcodesindex(string opcode)
        {
            int i;
            int index1, index2;
            tindex bestindex;
            int minindex, maxindex;
            opcode = opcode.ToUpper();
            var result = -1;
            if (opcode.Length <= 0)
                return result;
            index1 = opcode[0] - 'A';
            if ((index1 < 0) || (index1 >= 25))
                return result; //not alphabetical
            bestindex = assemblerindex[index1];
            if (bestindex.startentry == -1)
                return result;
            if ((assemblerindex[index1].subindex != null) && (opcode.Length > 1))
            {
                index2 = opcode[0] - 'A';
                if ((index2 < 0) || (index2 >= 25))
                    return result; //not alphabetical
                bestindex = assemblerindex[index1].subindex[index2];
                if (bestindex.startentry == -1)
                    return result; //no subitem2
            }
            minindex = bestindex.startentry;
            maxindex = bestindex.nextentry;
            if (maxindex == -1)
                if (assemblerindex[index1].nextentry != -1)
                    maxindex = assemblerindex[index1].nextentry;
                else
                    maxindex = opcodecount;
            if (maxindex > opcodecount)
                maxindex = opcodecount;
            //now scan from minindex to maxindex for opcode
            for (i = minindex; i <= maxindex; i++)
            {
                if (opcodes[i].mnemonic == opcode)
                {
                    result = i; //found it
                    return result;
                }
                if (opcodes[i].mnemonic[0] != opcode[0])
                    return result;
            }
            //still here, not found, -1
            return -1;
        }
        #endregion
        #region ismemorylocationdefault
        public Boolean ismemorylocationdefault(string parameter)
        {
            return parameter[0] == '[' && parameter[parameter.Length - 1] == ']';
        }
        #endregion
        #region add
        public void add(tassemblerbytes bytes, params Byte[] a)
        {
            bytes.EnsureCapacity(bytes.Length + a.Length);
            for (var i = 0; i < a.Length; i++)
                bytes[bytes.Length - a.Length + i] = a[i];
        }
        public void add(tassemblerbytes bytes, Byte a)
        {
            add(bytes, new[] {a});
        }
        #endregion
        #region addword
        public void addword(tassemblerbytes bytes, UInt16 a)
        {
            add(bytes, (Byte)a);
            add(bytes, (Byte)(a >> 8));
        }
        #endregion
        #region adddword
        public void adddword(tassemblerbytes bytes, UInt32 a)
        {
            add(bytes, (Byte)a);
            add(bytes, (Byte)(a >> 8));
            add(bytes, (Byte)(a >> 16));
            add(bytes, (Byte)(a >> 24));
        }
        #endregion
        #region addqword
        public void addqword(tassemblerbytes bytes, UInt64 a)
        {
            add(bytes, (Byte)a);
            add(bytes, (Byte)(a >> 8));
            add(bytes, (Byte)(a >> 16));
            add(bytes, (Byte)(a >> 24));
            add(bytes, (Byte)(a >> 32));
            add(bytes, (Byte)(a >> 40));
            add(bytes, (Byte)(a >> 48));
            add(bytes, (Byte)(a >> 56));
        }
        #endregion
        #region addstring
        public void addstring(tassemblerbytes bytes, String s)
        {
            var j = bytes.Length;
            bytes.EnsureCapacity(bytes.Length + s.Length - 2); //not the quotes;
            for (var i = 1; i < s.Length - 1; i++, j++)
                bytes[j] = (Byte)s[i];
        }
        #endregion
        #region valuetotype
        public int valuetotype(UInt32 value)
        {
            int result = 32;
            if (value <= 0xffff) 
            {
                result = 16;
                if (value >= 0x8000)
                    result = 32;
            }
            if (value <= 0xff) 
            {
                result = 8;
                if (value >= 0x80)
                    result = 16;
            }
            if (result == 32)
            {
                if ((int)(value) < 0)
                {
                    if ((int)(value) >= -128)
                        result = 8;
                    else if ((int)(value) >= -32768)
                        result = 16;
                }
            }
            return result;
        }
        #endregion
        #region signedvaluetotype
        public int signedvaluetotype(int value)
        {
            var result = 8;
            if ((value < -128) || (value > 127))
                result = 16;
            if ((value < -32768) || (value > 32767))
                result = 32;
            return result;
        }
        #endregion
        #region stringvaluetotype
        public int stringvaluetotype(string value)
        {
            //this function converts a string to a valuetype depending on how it is written
            var result = 0;
            TUtils.val(value, out UInt32 x, out var err);
            if (err > 0)
                return 0;
            if (value.Length == 17)
                result = 64;
            else if (value.Length == 9)
                result = 32;
            else if (value.Length == 5)
            {
                result = 16;
                if (x > 65535)
                    result = 32;
            }
            else if (value.Length == 3)
            {
                result = 8;
                if (x > 255)
                    result = 16;
            }
            if (result == 0)
                result = valuetotype(x); //not a specific ammount of characters given
            return result;
        }
        #endregion
        #region getreg
        public int getreg(string reg)
        {
            return getreg(reg, true);
        }
        public int getreg(string reg, Boolean exceptonerror)
        {
            var result = -1;
            if ((reg == "RAX") || (reg == "EAX") || (reg == "AX") || (reg == "AL") || (reg == "MM0") || (reg == "XMM0") || (reg == "ST(0)") || (reg == "ST") || (reg == "ES") || (reg == "CR0") || (reg == "DR0"))
                result = 0;
            if ((reg == "RCX") || (reg == "ECX") || (reg == "CX") || (reg == "CL") || (reg == "MM1") || (reg == "XMM1") || (reg == "ST(1)") || (reg == "CS") || (reg == "CR1") || (reg == "DR1"))
                result = 1;
            if ((reg == "RDX") || (reg == "EDX") || (reg == "DX") || (reg == "DL") || (reg == "MM2") || (reg == "XMM2") || (reg == "ST(2)") || (reg == "SS") || (reg == "CR2") || (reg == "DR2"))
                result = 2;
            if ((reg == "RBX") || (reg == "EBX") || (reg == "BX") || (reg == "BL") || (reg == "MM3") || (reg == "XMM3") || (reg == "ST(3)") || (reg == "DS") || (reg == "CR3") || (reg == "DR3"))
                result = 3;
            if ((reg == "SPL") || (reg == "RSP") || (reg == "ESP") || (reg == "SP") || (reg == "AH") || (reg == "MM4") || (reg == "XMM4") || (reg == "ST(4)") || (reg == "FS") || (reg == "CR4") || (reg == "DR4"))
                result = 4;
            if ((reg == "BPL") || (reg == "RBP") || (reg == "EBP") || (reg == "BP") || (reg == "CH") || (reg == "MM5") || (reg == "XMM5") || (reg == "ST(5)") || (reg == "GS") || (reg == "CR5") || (reg == "DR5"))
                result = 5;
            if ((reg == "SIL") || (reg == "RSI") || (reg == "ESI") || (reg == "SI") || (reg == "DH") || (reg == "MM6") || (reg == "XMM6") || (reg == "ST(6)") || (reg == "HS") || (reg == "CR6") || (reg == "DR6"))
                result = 6;
            if ((reg == "DIL") || (reg == "RDI") || (reg == "EDI") || (reg == "DI") || (reg == "BH") || (reg == "MM7") || (reg == "XMM7") || (reg == "ST(7)") || (reg == "IS") || (reg == "CR7") || (reg == "DR7"))
                result = 7;
            if (reg == "R8")
                result = 8;
            if (reg == "R9")
                result = 9;
            if (reg == "R10")
                result = 10;
            if (reg == "R11")
                result = 11;
            if (reg == "R12")
                result = 12;
            if (reg == "R13")
                result = 13;
            if (reg == "R14")
                result = 14;
            if (reg == "R15")
                result = 15;
            if (result == -1 && exceptonerror)
                throw new Exception("Invalid register");
            return result;
        }
        #endregion
        #region tokentoregisterbit
        public ttokentype tokentoregisterbit(string token)
        {
            var result = ttokentype.ttregister32bit;
            if (token == "AL") result = ttokentype.ttregister8bit;
            else
            if (token == "CL") result = ttokentype.ttregister8bit;
            else
            if (token == "DL") result = ttokentype.ttregister8bit;
            else
            if (token == "BL") result = ttokentype.ttregister8bit;
            else
            if (token == "AH") result = ttokentype.ttregister8bit;
            else
            if (token == "CH") result = ttokentype.ttregister8bit;
            else
            if (token == "DH") result = ttokentype.ttregister8bit;
            else if (token == "BH") result = ttokentype.ttregister8bit;
            else
            if (token == "AX") result = ttokentype.ttregister16bit;
            else
            if (token == "CX") result = ttokentype.ttregister16bit;
            else
            if (token == "DX") result = ttokentype.ttregister16bit;
            else
            if (token == "BX") result = ttokentype.ttregister16bit;
            else
            if (token == "SP") result = ttokentype.ttregister16bit;
            else
            if (token == "BP") result = ttokentype.ttregister16bit;
            else
            if (token == "SI") result = ttokentype.ttregister16bit;
            else
            if (token == "DI") result = ttokentype.ttregister16bit;
            else

            if (token == "EAX") result = ttokentype.ttregister32bit;
            else
            if (token == "ECX") result = ttokentype.ttregister32bit;
            else
            if (token == "EDX") result = ttokentype.ttregister32bit;
            else
            if (token == "EBX") result = ttokentype.ttregister32bit;
            else
            if (token == "ESP") result = ttokentype.ttregister32bit;
            else
            if (token == "EBP") result = ttokentype.ttregister32bit;
            else
            if (token == "ESI") result = ttokentype.ttregister32bit;
            else
            if (token == "EDI") result = ttokentype.ttregister32bit;
            else

            if (token == "MM0") result = ttokentype.ttregistermm;
            else
            if (token == "MM1") result = ttokentype.ttregistermm;
            else
            if (token == "MM2") result = ttokentype.ttregistermm;
            else
            if (token == "MM3") result = ttokentype.ttregistermm;
            else
            if (token == "MM4") result = ttokentype.ttregistermm;
            else
            if (token == "MM5") result = ttokentype.ttregistermm;
            else
            if (token == "MM6") result = ttokentype.ttregistermm;
            else
            if (token == "MM7") result = ttokentype.ttregistermm;
            else

            if (token == "XMM0") result = ttokentype.ttregisterxmm;
            else
            if (token == "XMM1") result = ttokentype.ttregisterxmm;
            else
            if (token == "XMM2") result = ttokentype.ttregisterxmm;
            else
            if (token == "XMM3") result = ttokentype.ttregisterxmm;
            else
            if (token == "XMM4") result = ttokentype.ttregisterxmm;
            else
            if (token == "XMM5") result = ttokentype.ttregisterxmm;
            else
            if (token == "XMM6") result = ttokentype.ttregisterxmm;
            else
            if (token == "XMM7") result = ttokentype.ttregisterxmm;
            else


            if (token == "ST") result = ttokentype.ttregisterst;
            else
            if (token == "ST(0)") result = ttokentype.ttregisterst;
            else
            if (token == "ST(1)") result = ttokentype.ttregisterst;
            else
            if (token == "ST(2)") result = ttokentype.ttregisterst;
            else
            if (token == "ST(3)") result = ttokentype.ttregisterst;
            else
            if (token == "ST(4)") result = ttokentype.ttregisterst;
            else
            if (token == "ST(5)") result = ttokentype.ttregisterst;
            else
            if (token == "ST(6)") result = ttokentype.ttregisterst;
            else
            if (token == "ST(7)") result = ttokentype.ttregisterst;
            else

            if (token == "ES") result = ttokentype.ttregistersreg;
            else
            if (token == "CS") result = ttokentype.ttregistersreg;
            else
            if (token == "SS") result = ttokentype.ttregistersreg;
            else
            if (token == "DS") result = ttokentype.ttregistersreg;
            else
            if (token == "FS") result = ttokentype.ttregistersreg;
            else
            if (token == "GS") result = ttokentype.ttregistersreg;
            else
            if (token == "HS") result = ttokentype.ttregistersreg;
            else
            if (token == "IS") result = ttokentype.ttregistersreg;
            else

            if (token == "CR0") result = ttokentype.ttregistercr;
            else
            if (token == "CR1") result = ttokentype.ttregistercr;
            else
            if (token == "CR2") result = ttokentype.ttregistercr;
            else
            if (token == "CR3") result = ttokentype.ttregistercr;
            else
            if (token == "CR4") result = ttokentype.ttregistercr;
            else
            if (token == "CR5") result = ttokentype.ttregistercr;
            else
            if (token == "CR6") result = ttokentype.ttregistercr;
            else
            if (token == "CR7") result = ttokentype.ttregistercr;
            else


            if (token == "DR0") result = ttokentype.ttregisterdr;
            else
            if (token == "DR1") result = ttokentype.ttregisterdr;
            else
            if (token == "DR2") result = ttokentype.ttregisterdr;
            else
            if (token == "DR3") result = ttokentype.ttregisterdr;
            else
            if (token == "DR4") result = ttokentype.ttregisterdr;
            else
            if (token == "DR5") result = ttokentype.ttregisterdr;
            else
            if (token == "DR6") result = ttokentype.ttregisterdr;
            else
            if (token == "DR7") result = ttokentype.ttregisterdr;
            else
            if (symhandler.process.is64bit)
            {
                if (token == "RAX") result = ttokentype.ttregister64bit;
                else
                if (token == "RCX") result = ttokentype.ttregister64bit;
                else
                if (token == "RDX") result = ttokentype.ttregister64bit;
                else
                if (token == "RBX") result = ttokentype.ttregister64bit;
                else
                if (token == "RSP") result = ttokentype.ttregister64bit;
                else
                if (token == "RBP") result = ttokentype.ttregister64bit;
                else
                if (token == "RSI") result = ttokentype.ttregister64bit;
                else
                if (token == "RDI") result = ttokentype.ttregister64bit;
                else
                if (token == "R8") result = ttokentype.ttregister64bit;
                else
                if (token == "R9") result = ttokentype.ttregister64bit;
                else
                if (token == "R10") result = ttokentype.ttregister64bit;
                else
                if (token == "R11") result = ttokentype.ttregister64bit;
                else
                if (token == "R12") result = ttokentype.ttregister64bit;
                else
                if (token == "R13") result = ttokentype.ttregister64bit;
                else
                if (token == "R14") result = ttokentype.ttregister64bit;
                else
                if (token == "R15") result = ttokentype.ttregister64bit;
                else

                if (token == "SPL") result = ttokentype.ttregister8bitwithprefix;
                else
                if (token == "BPL") result = ttokentype.ttregister8bitwithprefix;
                else
                if (token == "SIL") result = ttokentype.ttregister8bitwithprefix;
                else
                if (token == "DIL") result = ttokentype.ttregister8bitwithprefix;
                else


                if (token == "R8L") result = ttokentype.ttregister8bit;
                else
                if (token == "R9L") result = ttokentype.ttregister8bit;
                else
                if (token == "R10L") result = ttokentype.ttregister8bit;
                else
                if (token == "R11L") result = ttokentype.ttregister8bit;
                else
                if (token == "R12L") result = ttokentype.ttregister8bit;
                else
                if (token == "R13L") result = ttokentype.ttregister8bit;
                else
                if (token == "R14L") result = ttokentype.ttregister8bit;
                else
                if (token == "R15L") result = ttokentype.ttregister8bit;
                else

                if (token == "R8W") result = ttokentype.ttregister16bit;
                else
                if (token == "R9W") result = ttokentype.ttregister16bit;
                else
                if (token == "R10W") result = ttokentype.ttregister16bit;
                else
                if (token == "R11W") result = ttokentype.ttregister16bit;
                else
                if (token == "R12W") result = ttokentype.ttregister16bit;
                else
                if (token == "R13W") result = ttokentype.ttregister16bit;
                else
                if (token == "R14W") result = ttokentype.ttregister16bit;
                else
                if (token == "R15W") result = ttokentype.ttregister16bit;
                else

                if (token == "R8D") result = ttokentype.ttregister32bit;
                else
                if (token == "R9D") result = ttokentype.ttregister32bit;
                else
                if (token == "R10D") result = ttokentype.ttregister32bit;
                else
                if (token == "R11D") result = ttokentype.ttregister32bit;
                else
                if (token == "R12D") result = ttokentype.ttregister32bit;
                else
                if (token == "R13D") result = ttokentype.ttregister32bit;
                else
                if (token == "R14D") result = ttokentype.ttregister32bit;
                else
                if (token == "R15D") result = ttokentype.ttregister32bit;
                else

                if (token == "XMM8") result = ttokentype.ttregisterxmm;
                else
                if (token == "XMM9") result = ttokentype.ttregisterxmm;
                else
                if (token == "XMM10") result = ttokentype.ttregisterxmm;
                else
                if (token == "XMM11") result = ttokentype.ttregisterxmm;
                else
                if (token == "XMM12") result = ttokentype.ttregisterxmm;
                else
                if (token == "XMM13") result = ttokentype.ttregisterxmm;
                else
                if (token == "XMM14") result = ttokentype.ttregisterxmm;
                else
                if (token == "XMM15") result = ttokentype.ttregisterxmm;
                else
                if (token == "CR8") result = ttokentype.ttregistercr;
                else
                if (token == "CR9") result = ttokentype.ttregistercr;
                else
                if (token == "CR10") result = ttokentype.ttregistercr;
                else
                if (token == "CR11") result = ttokentype.ttregistercr;
                else
                if (token == "CR12") result = ttokentype.ttregistercr;
                else
                if (token == "CR13") result = ttokentype.ttregistercr;
                else
                if (token == "CR14") result = ttokentype.ttregistercr;
                else
                if (token == "CR15") result = ttokentype.ttregistercr;
            }
            return result;
        }
        #endregion
        #region isrm8
        public Boolean isrm8(ttokentype parametertype)
        {
            var result = (parametertype == ttokentype.ttmemorylocation8) || (parametertype == ttokentype.ttregister8bit);
            return result;
        }
        #endregion
        #region isrm16
        public Boolean isrm16(ttokentype parametertype)
        {
            var result = (parametertype == ttokentype.ttmemorylocation16) || (parametertype == ttokentype.ttregister16bit);
            return result;
        }
        #endregion
        #region isrm32
        public Boolean isrm32(ttokentype parametertype)
        {
            var result = (parametertype == ttokentype.ttmemorylocation32) || (parametertype == ttokentype.ttregister32bit);
            return result;
        }
        #endregion
        #region ismm_m32
        public Boolean ismm_m32(ttokentype parametertype)
        {
            var result = (parametertype == ttokentype.ttregistermm) || (parametertype == ttokentype.ttmemorylocation32);
            return result;
        }
        #endregion
        #region ismm_m64
        public Boolean ismm_m64(ttokentype parametertype)
        {
            var result = (parametertype == ttokentype.ttregistermm) || (parametertype == ttokentype.ttmemorylocation64);
            return result;
        }
        #endregion
        #region isxmm_m32
        public Boolean isxmm_m32(ttokentype parametertype)
        {
            var result = (parametertype == ttokentype.ttregisterxmm) || (parametertype == ttokentype.ttmemorylocation32);
            return result;
        }
        #endregion
        #region isxmm_m64
        public Boolean isxmm_m64(ttokentype parametertype)
        {
            var result = (parametertype == ttokentype.ttregisterxmm) || (parametertype == ttokentype.ttmemorylocation64);
            return result;
        }
        #endregion
        #region isxmm_m128
        public Boolean isxmm_m128(ttokentype parametertype)
        {
            var result = (parametertype == ttokentype.ttregisterxmm) || (parametertype == ttokentype.ttmemorylocation128);
            return result;
        }
        #endregion
        #region eotoreg
        public int eotoreg(textraopcode eo)
        {
            var result = -1;
            switch (eo)
            {
                case textraopcode.eo_reg0: result = 0; break;
                case textraopcode.eo_reg1: result = 1; break;
                case textraopcode.eo_reg2: result = 2; break;
                case textraopcode.eo_reg3: result = 3; break;
                case textraopcode.eo_reg4: result = 4; break;
                case textraopcode.eo_reg5: result = 5; break;
                case textraopcode.eo_reg6: result = 6; break;
                case textraopcode.eo_reg7: result = 7; break;
            }
            return result;
        }
        #endregion
        #region setmod
        public void setmod(byte[] modrm, int index, byte i)
        {
            var tmp = modrm[index];
            setmod(ref tmp, i);
            modrm[index] = tmp;
        }
        public void setmod(tassemblerbytes modrm, int index, byte i)
        {
            var tmp = modrm[index];
            setmod(ref tmp, i);
            modrm[index] = tmp;
        }
        public void setmod(ref byte modrm, byte i)
        {
            modrm = (Byte)((modrm & 0x3f) | (i << 6));
        }
        #endregion
        #region getmod
        public byte getmod(byte modrm)
        {
            return (Byte)(modrm >> 6);
        }
        #endregion
        #region setsibscale
        public void setsibscale(byte[] sib, int index, byte i)
        {
            var tmp = sib[index];
            setsibscale(ref tmp, i);
            sib[index] = tmp;
        }
        public void setsibscale(tassemblerbytes sib, int index, byte i)
        {
            var tmp = sib[index];
            setsibscale(ref tmp, i);
            sib[index] = tmp;
        }
        public byte setsibscale(ref byte sib, byte i)
        {
            return (Byte)((sib & 0x3f) | (i << 6));
        }
        #endregion
        #region gettokentype
        public ttokentype gettokentype(ref string token, string token2)
        /*i,*/
        {
            var result = ttokentype.ttinvalidtoken;
            if (token.Length == 0)
                return result;
            result = tokentoregisterbit(token);
            //filter these 2 words
            token = UStringUtils.Replace(token, "LONG ", "", true);
            token = UStringUtils.Replace(token, "SHORT ", "", true);
            token = UStringUtils.Replace(token, "FAR ", "", true);
            var temp = TUtils.converthexstrtorealstr(token);
            TUtils.val(temp, out UInt64 _, out var err);
            if (err == 0)
            {
                result = ttokentype.ttvalue;
                token = temp;
            }
            if (TUtils.pos("[", token) > 0)
            {
                if (TUtils.pos("DQWORD ", token) != -1)
                    result = ttokentype.ttmemorylocation128;
                else if (TUtils.pos("TBYTE ", token) != -1)
                    result = ttokentype.ttmemorylocation80;
                else if (TUtils.pos("TWORD ", token) != -1)
                    result = ttokentype.ttmemorylocation80;
                else if (TUtils.pos("QWORD ", token) != -1)
                    result = ttokentype.ttmemorylocation64;
                else if (TUtils.pos("DWORD ", token) != -1)
                    result = ttokentype.ttmemorylocation32;
                else if (TUtils.pos("WORD ", token) != -1)
                    result = ttokentype.ttmemorylocation16;
                else if (TUtils.pos("BYTE ", token) != -1)
                    result = ttokentype.ttmemorylocation8;
                else
                    result = ttokentype.ttmemorylocation;
            }
            if (result == ttokentype.ttmemorylocation)
            {
                if (token2 == "")
                {
                    result = ttokentype.ttmemorylocation32;
                    return result;
                }
                //I need the helper param to figure it out
                switch (tokentoregisterbit(token2))
                {
                    case ttokentype.ttregister8bit:
                    case ttokentype.ttregister8bitwithprefix:
                        result = ttokentype.ttmemorylocation8;
                        break;
                    case ttokentype.ttregistersreg:
                    case ttokentype.ttregister16bit:
                        result = ttokentype.ttmemorylocation16;
                        break;
                    case ttokentype.ttregister64bit:
                        result = ttokentype.ttmemorylocation64;
                        break;
                    default:
                        result = ttokentype.ttmemorylocation32;
                        break;
                }
            }
            return result;
        }
        #endregion
        #region tokenize
        public Boolean tokenize(String opcode, ttokens tokens)
        {
            int i, j, last;
            Boolean quoted;
            char quotechar;
            string t;
            Boolean ispartial;
            quotechar = '\0';
            tokens.SetLength(0);
            if (opcode.Length > 0)
                opcode = opcode.TrimEnd(' ', ',');
            last = 0;
            quoted = false;
            for (i = 0; i <= opcode.Length; i++)
            {
                //check if this is a quote char
                if (i < opcode.Length && ((opcode[i] == '\'') || (opcode[i] == '"')))
                {
                    if (quoted)  //check if it's the end quote
                    {
                        if (opcode[i] == quotechar)
                            quoted = false;
                    }
                    else
                    {
                        quoted = true;
                        quotechar = opcode[i];
                    }
                }
                //check if we encounter a token seperator. (space or , )
                //but only check when it's not inside a quoted string
                if ((i == opcode.Length) || ((!quoted) && ((opcode[i] == ' ') || (opcode[i] == ','))))
                {
                    tokens.SetLength(tokens.Length + 1);
                    if (i == opcode.Length)
                        j = i - last + 1;
                    else
                        j = i - last;
                    tokens.Last = TUtils.copy(opcode, last, j);
                    if ((j > 0) && (tokens.Last[0] != '$') && ((j < 7) || (TUtils.pos("KERNEL_", tokens.Last.ToUpper()) == 0)))  //only uppercase if it's not kernel_
                    {
                        //don't uppercase empty strings, kernel_ strings or strings starting with $
                        if (tokens.Last.Length > 2)
                        {
                            if (!TUtils.inarray(tokens.Last[0], '\'', '"'))  //if not a quoted string then make it uppercase
                                tokens.Last = tokens.Last.ToUpper();
                        }
                        else
                            tokens.Last = tokens.Last.ToUpper();
                    }
                    //6.1: Optimized this lookup. Instead of a 18 compares a full string lookup on each token it now only compares up to 4 times
                    t = tokens.Last;
                    ispartial = false;
                    if (t.Length >= 3)  //3 characters is good enough to get the general idea, then do a string compare to verify
                    {
                        switch (t[0])
                        {
                            case 'B': //BYTE, BYTE PTR
                                {
                                    if ((t[1] == 'Y') && (t[2] == 'T'))  //could be BYTE
                                        ispartial = (t == "BYTE") || (t == "BYTE PTR");
                                }
                                break;
                            case 'D': //DQWORD, DWORD, DQWORD PTR, DWORD PTR
                                {
                                    switch (t[1])
                                    {
                                        case 'Q': //DQWORD or DQWORD PTR
                                            {
                                                if (t[2] == 'W')
                                                    ispartial = (t == "DQWORD") || (t == "DQWORD PTR");
                                            }
                                            break;

                                        case 'W': //DWORD or DWORD PTR
                                            {
                                                if (t[2] == 'O')
                                                    ispartial = (t == "DWORD") || (t == "DWORD PTR");
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 'F': //FAR
                                {
                                    if ((t[1] == 'A') && (t[2] == 'R'))
                                        ispartial = (t == "FAR");
                                }
                                break;
                            case 'L': //LONG
                                {
                                    if ((t[1] == 'O') && (t[2] == 'N'))
                                        ispartial = (t == "LONG");
                                }
                                break;
                            case 'Q': //QWORD, QWORD PTR
                                {
                                    if ((t[1] == 'W') && (t[2] == 'O'))  //could be QWORD
                                        ispartial = (t == "QWORD") || (t == "QWORD PTR");
                                }
                                break;
                            case 'S': //SHORT
                                {
                                    if ((t[1] == 'H') && (t[2] == 'O'))
                                        ispartial = (t == "SHORT");
                                }
                                break;
                            case 'T': //TBYTE, TWORD, TBYTE PTR, TWORD PTR,
                                {
                                    switch (t[1])
                                    {
                                        case 'B': //TBYTE or TBYTE PTR
                                            {
                                                if (t[2] == 'Y')
                                                    ispartial = (t == "TBYTE") || (t == "TBYTE PTR");
                                            }
                                            break;

                                        case 'W': //TWORD or TWORD PTR
                                            {
                                                if (t[2] == 'O')
                                                    ispartial = (t == "TWORD") || (t == "TWORD PTR");
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 'W': //WORD, WORD PTR
                                {
                                    if ((t[1] == 'O') && (t[3] == 'R'))  //could be WORD
                                        ispartial = (t == "WORD") || (t == "WORD PTR");
                                }
                                break;
                        }
                    }
                    if (ispartial)
                        tokens.SetLength(tokens.Length - 1);
                    else
                    {
                        last = i + 1;
                        if (tokens.Length > 1)
                        {
                            var lastElem = tokens.Last;
                            rewrite(ref lastElem); //Rewrite
                            tokens.Last = lastElem;
                        }
                    }
                }
            }
            i = 0;
            while (i < tokens.Length)
            {
                if ((tokens[i] == "") || (tokens[i] == " ") || (tokens[i] == ","))
                {
                    for (j = i; j < tokens.Length - 1; j++)
                        tokens[j] = tokens[j + 1];
                    tokens.SetLength(tokens.Length - 1);
                    continue;
                }
                i++;
            }
            return true;
        }
        #endregion
        #region rewrite
        public Boolean rewrite(ref string token)
        {
            if (token.Length == 0)
                return false; //empty string
            var tokens = new ttokens();
            var quotechar = '\0';
            tokens.SetLength(0);
            String temp;
            /* 5.4: special pointer notation case */
            if (token.Length > 4 && token.StartsWith("[[") && token.EndsWith("]]"))
            {
                //looks like a pointer in a address specifier (idiot user detected...)
                Console.WriteLine(TUtils.copy(token, 2, token.Length - 4));
                temp = "[" + TUtils.inttohex(symhandler.getaddressfromname(TUtils.copy(token, 2, token.Length - 4), false, out var haserror), 8) + ']';
                if (!haserror)
                    token = temp;
                else
                    throw new Exception("Invalid");
            }
            /* 5.4 ^^^ */
            temp = "";
            var i = 0;
            var inquote = false;
            while (i < token.Length)
            {
                if (TUtils.inarray(token[i], '\'', '"'))
                {
                    if (inquote)
                    {
                        if (token[i] == quotechar)
                            inquote = false;
                    }
                    else
                    {
                        //start of a quote
                        quotechar = token[i];
                        inquote = true;
                    }
                }
                if (!inquote)
                {
                    if (TUtils.inarray(token[i], '[', ']', '+', '-', '*'))
                    {
                        if (temp != "")
                        {
                            tokens.SetLength(tokens.Length + 1);
                            tokens.Last = temp;
                            temp = "";
                        }
                        tokens.SetLength(tokens.Length + 1);
                        tokens[tokens.Length - 1] = token[i].ToString();
                        i++;
                        continue;
                    }
                }
                temp += token[i];
                i++;
            }
            if (temp != "")
            {
                tokens.SetLength(tokens.Length + 1);
                tokens[tokens.Length - 1] = temp;
                temp = "";
            }
            for (i = 0; i < tokens.Length; i++)
            {
                if (tokens[i].Length > 1 && !TUtils.inarray(tokens[i][0], '[', ']', '+', '-', '*'))  //3/16/2011: 11:15 (replaced or with and)
                {
                    TUtils.val("0x" + tokens[i], out Int64 _, out var err);
                    if ((err != 0) && (getreg(tokens[i], false) == -1))     //not a hexadecimal value and not a register
                    {
                        temp = TUtils.inttohex(symhandler.getaddressfromname(tokens[i], false, out var haserror), 8);
                        if (!haserror)
                            tokens[i] = temp; //can be rewritten as a hexadecimal
                        else
                        {
                            if (i < tokens.Length - 1)
                            {
                                //perhaps it can be concatenated with the next one
                                if ((tokens[i + 1].Length > 0) && (!(TUtils.inarray(tokens[i + 1][0], '\'', '"', '[', ']', '(', ')'))))  //not an invalid token char
                                {
                                    tokens[i + 1] = tokens[i] + tokens[i + 1];
                                    tokens[i] = "";
                                }
                            }
                        }
                    }
                }
            }
            //do some calculations
            //check multiply first
            for (i = 1; i <= tokens.Length - 2; i++)
            {
                if (tokens[i] == "*")
                {
                    TUtils.val("0x" + tokens[i - 1], out Int64 a, out var err);
                    TUtils.val("0x" + tokens[i + 1], out Int64 b, out var err2);
                    if ((err == 0) && (err2 == 0))
                    {
                        a *= b;
                        tokens[i - 1] = TUtils.inttohex(a, 8);
                        tokens.Remove(i, 2);
                        i -= 2;
                    }
                }
            }
            for (i = 1; i <= tokens.Length - 2; i++)
            {
                //get the value of the token before and after this token
                TUtils.val("0x" + tokens[i - 1], out Int64 a, out var err);
                TUtils.val("0x" + tokens[i + 1], out Int64 b, out var err2);
                //if no error, check if this token is a mathemetical value
                if ((err == 0) && (err2 == 0))
                {
                    switch (tokens[i][0])
                    {
                        case '+':
                            {
                                a += b;
                                tokens[i - 1] = TUtils.inttohex(a, 8);
                                tokens.Remove(i, 2);
                                i -= 2;
                            }
                            break;

                        case '-':
                            {
                                a -= b;
                                tokens[i - 1] = TUtils.inttohex(a, 8);
                                tokens.Remove(i, 2);
                                i -= 2;
                            }
                            break;
                    }
                }
                else
                {
                    if ((err2 == 0) && (tokens[i] != "") && (tokens[i][0] == '-') && (tokens[i - 1] != "#"))  //before is not a valid value, but after it is. and this is a -  (so -value) (don't mess with #-10000)
                    {
                        tokens[i] = "+";
                        tokens[i + 1] = TUtils.inttohex(-b, 8);
                    }
                }
            }
            token = "";
            for (i = 0; i <= tokens.Length - 1; i++)
                token += tokens[i];
            tokens.SetLength(0);
            return true;
        }
        #endregion
        #region assmble
        public Boolean assemble(String opcode, UInt64 address, tassemblerbytes bytes, tassemblerpreference assemblerPreference = tassemblerpreference.apnone, Boolean skiprangecheck = false)
        {
            var result = singlelineassembler.assemble(opcode, address, bytes, assemblerPreference, skiprangecheck);
            return result;
        }
        #endregion
    }
}
