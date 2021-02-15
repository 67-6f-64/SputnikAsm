using SputnikAsm.LAssembler.LEnums;

namespace SputnikAsm.LAssembler
{
    public static class AOpCodes
    {
        #region GetOpCodes
        public static AOpCode[] GetOpCodes()
        {
            return new[]
            {
                new AOpCode { Mnemonic = "AAA", OpCode1 = AExtraOpCode.eo_none, OpCode2 = AExtraOpCode.eo_none, ParamType1 = AParam.par_noparam, ParamType2 = AParam.par_noparam, ParamType3 = AParam.par_noparam, Bytes = 1, Bt1 = 0x37, Bt2 = 0, Bt3 = 0 }, //no param
                new AOpCode { Mnemonic = "AAD", OpCode1 = AExtraOpCode.eo_none, OpCode2 = AExtraOpCode.eo_none, ParamType1 = AParam.par_noparam, ParamType2 = AParam.par_noparam, ParamType3 = AParam.par_noparam, Bytes = 2, Bt1 = 0xd5, Bt2 = 0x0a, Bt3 = 0 },
                new AOpCode { Mnemonic = "AAD", OpCode1 = AExtraOpCode.eo_ib, OpCode2 = AExtraOpCode.eo_none, ParamType1 = AParam.par_imm8, ParamType2 = AParam.par_noparam, ParamType3 = AParam.par_noparam, Bytes = 1, Bt1 = 0xd5, Bt2 = 0, Bt3 = 0 },
                new AOpCode { Mnemonic = "AAM", OpCode1 = AExtraOpCode.eo_none, ParamType1 = AParam.par_noparam, Bytes = 2, Bt1 = 0xd4, Bt2 = 0x0a },
                new AOpCode { Mnemonic = "AAM", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_imm8, Bytes = 1, Bt1 = 0xd4 },
                new AOpCode { Mnemonic = "AAS", OpCode1 = AExtraOpCode.eo_none, ParamType1 = AParam.par_noparam, Bytes = 1, Bt1 = 0x3F, InvalidIn64Bit = true },
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
                new AOpCode { Mnemonic = "ADCX", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0xf6 },

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

                new AOpCode { Mnemonic = "ADDSUBPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xd0 },
                new AOpCode { Mnemonic = "ADDSUBPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0xd0 },
                new AOpCode { Mnemonic = "ADOX", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 4, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0xf6 },
                new AOpCode { Mnemonic = "AESDEC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0xde },
                new AOpCode { Mnemonic = "AESDECLAST", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0xdf },
                new AOpCode { Mnemonic = "AESENC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0xdc },
                new AOpCode { Mnemonic = "AESENCLAST", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0xdd },
                new AOpCode { Mnemonic = "AESIMC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0xdb },
                new AOpCode { Mnemonic = "AESKEYGENASSIST", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0xdf },


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

                new AOpCode { Mnemonic = "ANDN", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_r32, ParamType3 = AParam.par_m32, Bytes = 1, Bt1 = 0xf2, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "ANDNPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xff },
                new AOpCode { Mnemonic = "ANDNPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x55 },

                new AOpCode { Mnemonic = "ANDPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x54 },
                new AOpCode { Mnemonic = "ANDPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x54 },

                new AOpCode { Mnemonic = "ARPL", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 1, Bt1 = 0x63 }, //eo_reg means I just need to find the reg and address

                new AOpCode { Mnemonic = "BEXTR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, ParamType3 = AParam.par_r32, Bytes = 1, Bt1 = 0xf7, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },
                new AOpCode { Mnemonic = "BLENDPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x0d },
                new AOpCode { Mnemonic = "BLENDPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x0c },
                new AOpCode { Mnemonic = "BLENDVPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_noparam, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x15 },
                new AOpCode { Mnemonic = "BLENDVPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_noparam, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x14 },
                new AOpCode { Mnemonic = "BLSI", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0xf3, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "BLSMSK", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0xf3, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "BLSR", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0xf3, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "BOUND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x62 },
                new AOpCode { Mnemonic = "BOUND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0x62 },
                new AOpCode { Mnemonic = "BSF", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xbc },
                new AOpCode { Mnemonic = "BSF", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xbc },
                new AOpCode { Mnemonic = "BSR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xbd },
                new AOpCode { Mnemonic = "BSR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xbd },
                new AOpCode { Mnemonic = "BSWAP", OpCode1 = AExtraOpCode.eo_prd, ParamType1 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc8 }, //eo_prd
                new AOpCode { Mnemonic = "BSWAP", OpCode1 = AExtraOpCode.eo_prw, ParamType1 = AParam.par_r16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xc8 }, //eo_prw

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
                new AOpCode { Mnemonic = "BZHI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, ParamType3 = AParam.par_r32, Bytes = 1, Bt1 = 0xf5, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },
                //no 0x66 0xE8 because it makes the address it jumps to 16 bit
                new AOpCode { Mnemonic = "CALL", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 1, Bt1 = 0xe8 },
                //also no 0x66 0xff /2
                new AOpCode { Mnemonic = "CALL", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0xff, W0 = true },
                new AOpCode { Mnemonic = "CBW", OpCode1 = AExtraOpCode.eo_none, ParamType1 = AParam.par_noparam, Bytes = 2, Bt1 = 0x66, Bt2 = 0x98 },
                new AOpCode { Mnemonic = "CDQ", Bytes = 1, Bt1 = 0x99 },
                new AOpCode { Mnemonic = "CDQE", Bytes = 2, Bt1 = 0x48, Bt2 = 0x98 },
                new AOpCode { Mnemonic = "CLAC", Bytes = 3, Bt1 = 0x0f, Bt2 = 0x01, Bt3 = 0xca },
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



                new AOpCode { Mnemonic = "CMP", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_al, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x3C }, //2 Bytes
                new AOpCode { Mnemonic = "CMP", OpCode1 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_ax, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x3D }, //4 Bytes
                new AOpCode { Mnemonic = "CMP", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_eax, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x3D }, //5 Bytes
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
                new AOpCode { Mnemonic = "CMPXCHG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_r8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xb0 },
                new AOpCode { Mnemonic = "CMPXCHG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xb1 },
                new AOpCode { Mnemonic = "CMPXCHG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xb1 },
                new AOpCode { Mnemonic = "CMPXCHG8B", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc7 }, //no m64 as eo, seems it"s just a /1

                new AOpCode { Mnemonic = "COMISD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x2f },
                new AOpCode { Mnemonic = "COMISS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x2f },

                new AOpCode { Mnemonic = "CPUID", Bytes = 2, Bt1 = 0x0f, Bt2 = 0xa2 },
                new AOpCode { Mnemonic = "CQO", Bytes = 2, Bt1 = 0x48, Bt2 = 0x99 },

                new AOpCode { Mnemonic = "CRC32", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm8, Bytes = 4, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0xf0 },
                new AOpCode { Mnemonic = "CRC32", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 4, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0xf1 },


                new AOpCode { Mnemonic = "CVTDQ2PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0xe6 }, //just a gues, the documentation didn"t say anything about a /r, and the disassembler of delphi also doesn"t recognize it
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
                new AOpCode { Mnemonic = "DPPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x41 },
                new AOpCode { Mnemonic = "DPPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x40 },
                new AOpCode { Mnemonic = "EMMS", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x77 },
                new AOpCode { Mnemonic = "ENTER", OpCode1 = AExtraOpCode.eo_iw, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_imm16, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc8 },
                new AOpCode { Mnemonic = "EXTRACTPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x17 },
                new AOpCode { Mnemonic = "F2XM1", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xf0 },
                new AOpCode { Mnemonic = "FABS", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xe1 },
                new AOpCode { Mnemonic = "FADD", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd8, W0 = true },
                new AOpCode { Mnemonic = "FADD", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdc, W0 = true },
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
                new AOpCode { Mnemonic = "FCOM", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd8, W0 = true },
                new AOpCode { Mnemonic = "FCOM", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdc, W0 = true },
                new AOpCode { Mnemonic = "FCOM", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xd0 },
                new AOpCode { Mnemonic = "FCOM", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xd0 },
                new AOpCode { Mnemonic = "FCOM", Bytes = 2, Bt1 = 0xd8, Bt2 = 0xd1 },
                new AOpCode { Mnemonic = "FCOMI", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xdb, Bt2 = 0xf0 },
                new AOpCode { Mnemonic = "FCOMI", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xdb, Bt2 = 0xf0 },
                new AOpCode { Mnemonic = "FCOMIP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xdf, Bt2 = 0xf0 },
                new AOpCode { Mnemonic = "FCOMIP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xdf, Bt2 = 0xf0 },
                new AOpCode { Mnemonic = "FCOMP", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd8, W0 = true },
                new AOpCode { Mnemonic = "FCOMP", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdc, W0 = true },
                new AOpCode { Mnemonic = "FCOMP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xd8 },
                new AOpCode { Mnemonic = "FCOMP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xd8 },
                new AOpCode { Mnemonic = "FCOMP", Bytes = 2, Bt1 = 0xd8, Bt2 = 0xd9 },

                new AOpCode { Mnemonic = "FCOMPP", Bytes = 2, Bt1 = 0xde, Bt2 = 0xd9 },
                new AOpCode { Mnemonic = "FCOS", Bytes = 2, Bt1 = 0xD9, Bt2 = 0xff },

                new AOpCode { Mnemonic = "FDECSTP", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xf6 },

                new AOpCode { Mnemonic = "FDIV", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd8, W0 = true },
                new AOpCode { Mnemonic = "FDIV", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdc, W0 = true },
                new AOpCode { Mnemonic = "FDIV", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xf0 },
                new AOpCode { Mnemonic = "FDIV", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xf0 },
                new AOpCode { Mnemonic = "FDIV", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xdc, Bt2 = 0xf8 },
                new AOpCode { Mnemonic = "FDIVP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xde, Bt2 = 0xf8 },
                new AOpCode { Mnemonic = "FDIVP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xde, Bt2 = 0xf8 },
                new AOpCode { Mnemonic = "FDIVP", Bytes = 2, Bt1 = 0xde, Bt2 = 0xf9 },
                new AOpCode { Mnemonic = "FDIVR", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd8, W0 = true },
                new AOpCode { Mnemonic = "FDIVR", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdc, W0 = true },
                new AOpCode { Mnemonic = "FDIVR", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xf8 },
                new AOpCode { Mnemonic = "FDIVR", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xf8 },
                new AOpCode { Mnemonic = "FDIVR", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xdc, Bt2 = 0xf0 },
                new AOpCode { Mnemonic = "FDIVRP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xde, Bt2 = 0xf0 },
                new AOpCode { Mnemonic = "FDIVRP", Bytes = 2, Bt1 = 0xde, Bt2 = 0xf1 },
                new AOpCode { Mnemonic = "FFREE", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xdd, Bt2 = 0xc0 },
                new AOpCode { Mnemonic = "FFREEP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xdf, Bt2 = 0xc0 },

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


                new AOpCode { Mnemonic = "FILD", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xdb, W0 = true }, //screw this, going for a default of m32
                new AOpCode { Mnemonic = "FILD", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xdf, W0 = true },
                new AOpCode { Mnemonic = "FILD", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdf, W0 = true },

                new AOpCode { Mnemonic = "FIMUL", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xda, W0 = true },
                new AOpCode { Mnemonic = "FIMUL", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xde, W0 = true },

                new AOpCode { Mnemonic = "FINCSTP", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xf7 },
                new AOpCode { Mnemonic = "FINIT", Bytes = 3, Bt1 = 0x9b, Bt2 = 0xdb, Bt3 = 0xe3 },

                new AOpCode { Mnemonic = "FIST", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xdb, W0 = true },
                new AOpCode { Mnemonic = "FIST", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xdf, W0 = true },

                new AOpCode { Mnemonic = "FISTP", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xdb, W0 = true },
                new AOpCode { Mnemonic = "FISTP", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xdf, W0 = true },
                new AOpCode { Mnemonic = "FISTP", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdf, W0 = true },

                new AOpCode { Mnemonic = "FISTTP", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xdb, W0 = true },
                new AOpCode { Mnemonic = "FISTTP", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xdf, W0 = true },
                new AOpCode { Mnemonic = "FISTTP", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdd, W0 = true },

                new AOpCode { Mnemonic = "FISUB", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xda, W0 = true },
                new AOpCode { Mnemonic = "FISUB", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xde, W0 = true },
                new AOpCode { Mnemonic = "FISUBR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xda, W0 = true },
                new AOpCode { Mnemonic = "FISUBR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xde, W0 = true },

                new AOpCode { Mnemonic = "FLD", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd9, W0 = true },
                new AOpCode { Mnemonic = "FLD", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdd, W0 = true },
                new AOpCode { Mnemonic = "FLD", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_m80, Bytes = 1, Bt1 = 0xdb, W0 = true },
                new AOpCode { Mnemonic = "FLD", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xd9, Bt2 = 0xc0, W0 = true },

                new AOpCode { Mnemonic = "FLD1", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xe8 },
                new AOpCode { Mnemonic = "FLDCW", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_m16, Bytes = 1, Bt1 = 0xd9 },
                new AOpCode { Mnemonic = "FLDENV", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd9 },
                new AOpCode { Mnemonic = "FLDL2E", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xea },
                new AOpCode { Mnemonic = "FLDL2T", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xe9 },
                new AOpCode { Mnemonic = "FLDLG2", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xec },
                new AOpCode { Mnemonic = "FLDLN2", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xed },
                new AOpCode { Mnemonic = "FLDPI", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xeb },
                new AOpCode { Mnemonic = "FLDZ", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xee },

                new AOpCode { Mnemonic = "FMUL", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd8, W0 = true },
                new AOpCode { Mnemonic = "FMUL", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdc, W0 = true },
                new AOpCode { Mnemonic = "FMUL", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xC8 },
                new AOpCode { Mnemonic = "FMUL", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xC8 },
                new AOpCode { Mnemonic = "FMUL", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xdc, Bt2 = 0xC8 },
                new AOpCode { Mnemonic = "FMULP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xde, Bt2 = 0xC8 },
                new AOpCode { Mnemonic = "FMULP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xde, Bt2 = 0xC8 },
                new AOpCode { Mnemonic = "FMULP", Bytes = 2, Bt1 = 0xde, Bt2 = 0xc9 },

                new AOpCode { Mnemonic = "FNCLEX", Bytes = 2, Bt1 = 0xDb, Bt2 = 0xe2 },
                new AOpCode { Mnemonic = "FNINIT", Bytes = 2, Bt1 = 0xdb, Bt2 = 0xe3 },
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

                new AOpCode { Mnemonic = "FST", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd9, W0 = true },
                new AOpCode { Mnemonic = "FST", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdd, W0 = true },
                new AOpCode { Mnemonic = "FST", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xdd, Bt2 = 0xd0, W0 = true },
                new AOpCode { Mnemonic = "FSTCW", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m16, Bytes = 2, Bt1 = 0x9b, Bt2 = 0xd9 },
                new AOpCode { Mnemonic = "FSTENV", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x9b, Bt2 = 0xd9 },
                new AOpCode { Mnemonic = "FSTP", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd9, W0 = true },
                new AOpCode { Mnemonic = "FSTP", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdd, W0 = true },
                new AOpCode { Mnemonic = "FSTP", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m80, Bytes = 1, Bt1 = 0xdb, W0 = true },
                new AOpCode { Mnemonic = "FSTP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xdd, Bt2 = 0xd8, W0 = true },


                new AOpCode { Mnemonic = "FSTSW", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m16, Bytes = 2, Bt1 = 0x9b, Bt2 = 0xdd },
                new AOpCode { Mnemonic = "FSTSW", ParamType1 = AParam.par_ax, Bytes = 3, Bt1 = 0x9b, Bt2 = 0xdf, Bt3 = 0xe0 },


                new AOpCode { Mnemonic = "FSUB", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd8, W0 = true },
                new AOpCode { Mnemonic = "FSUB", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdc, W0 = true },
                new AOpCode { Mnemonic = "FSUB", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xe0, W0 = true },
                new AOpCode { Mnemonic = "FSUB", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xdc, Bt2 = 0xe8, W0 = true },
                new AOpCode { Mnemonic = "FSUB", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xdc, Bt2 = 0xe8, W0 = true },
                new AOpCode { Mnemonic = "FSUBP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xde, Bt2 = 0xe8, W0 = true },
                new AOpCode { Mnemonic = "FSUBP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xde, Bt2 = 0xe8, W0 = true },
                new AOpCode { Mnemonic = "FSUBP", Bytes = 2, Bt1 = 0xde, Bt2 = 0xe9, W0 = true },
                new AOpCode { Mnemonic = "FSUBR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xd8, W0 = true },
                new AOpCode { Mnemonic = "FSUBR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_m64, Bytes = 1, Bt1 = 0xdc, W0 = true },
                new AOpCode { Mnemonic = "FSUBR", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st0, ParamType2 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xe8, W0 = true },
                new AOpCode { Mnemonic = "FSUBR", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xd8, Bt2 = 0xe8, W0 = true },
                new AOpCode { Mnemonic = "FSUBR", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xdc, Bt2 = 0xe0, W0 = true },
                new AOpCode { Mnemonic = "FSUBRP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, ParamType2 = AParam.par_st0, Bytes = 2, Bt1 = 0xde, Bt2 = 0xe0, W0 = true },
                new AOpCode { Mnemonic = "FSUBRP", OpCode1 = AExtraOpCode.eo_pi, ParamType1 = AParam.par_st, Bytes = 2, Bt1 = 0xde, Bt2 = 0xe0, W0 = true },
                new AOpCode { Mnemonic = "FSUBRP", Bytes = 2, Bt1 = 0xde, Bt2 = 0xe1, W0 = true },
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
                new AOpCode { Mnemonic = "FYL2XP1", Bytes = 2, Bt1 = 0xd9, Bt2 = 0xf9 },


                new AOpCode { Mnemonic = "HADDPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x7c },
                new AOpCode { Mnemonic = "HADDPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x7c },
                new AOpCode { Mnemonic = "HLT", Bytes = 1, Bt1 = 0xf4 },
                new AOpCode { Mnemonic = "HSUBPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x7d },
                new AOpCode { Mnemonic = "HSUBPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x7d },

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

                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x69 },
                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_id, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x69 },

                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0x6b },
                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x6b },

                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, ParamType3 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x69 },
                new AOpCode { Mnemonic = "IMUL", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_id, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, ParamType3 = AParam.par_imm32, Bytes = 1, Bt1 = 0x69 },



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
                new AOpCode { Mnemonic = "INSERTPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x21 },

                new AOpCode { Mnemonic = "INSW", Bytes = 2, Bt1 = 0x66, Bt2 = 0x6d },

                new AOpCode { Mnemonic = "INT", ParamType1 = AParam.par_3, Bytes = 1, Bt1 = 0xcc },
                new AOpCode { Mnemonic = "INT", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_imm8, Bytes = 1, Bt1 = 0xcd },
                new AOpCode { Mnemonic = "INTO", Bytes = 1, Bt1 = 0xce },

                new AOpCode { Mnemonic = "INVD", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x08 },
                new AOpCode { Mnemonic = "INVLPG", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x01 },
                new AOpCode { Mnemonic = "INVPCID", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x82 },


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
                new AOpCode { Mnemonic = "JMP", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0xff, W0 = true },



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

                new AOpCode { Mnemonic = "LDDQU", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_m128, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0xf0 },
                new AOpCode { Mnemonic = "LDMXCSR", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xae },
                new AOpCode { Mnemonic = "LDS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_m16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xc5 },
                new AOpCode { Mnemonic = "LDS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_m32, Bytes = 1, Bt1 = 0xc5 },

                new AOpCode { Mnemonic = "LEA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_m16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x8d, CanDoAddressSwitch = true },
                new AOpCode { Mnemonic = "LEA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_m32, Bytes = 1, Bt1 = 0x8d, CanDoAddressSwitch = true },
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
                new AOpCode { Mnemonic = "LODSQ", Bytes = 2, Bt1 = 0x48, Bt2 = 0xad },
                new AOpCode { Mnemonic = "LODSW", Bytes = 2, Bt1 = 0x66, Bt2 = 0xad },

                new AOpCode { Mnemonic = "LOOP", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0xe2 },
                new AOpCode { Mnemonic = "LOOPE", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0xe1 },
                new AOpCode { Mnemonic = "LOOPNE", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0xe0 },
                new AOpCode { Mnemonic = "LOOPNZ", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0xe0 },
                new AOpCode { Mnemonic = "LOOPZ", OpCode1 = AExtraOpCode.eo_cb, ParamType1 = AParam.par_rel8, Bytes = 1, Bt1 = 0xe1 },

                new AOpCode { Mnemonic = "LSL", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x03 },
                new AOpCode { Mnemonic = "LSL", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x03 },

                new AOpCode { Mnemonic = "LSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_m16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xb2 },
                new AOpCode { Mnemonic = "LSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xb2 },

                new AOpCode { Mnemonic = "LTR", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x00 },

                new AOpCode { Mnemonic = "LZCNT", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 4, Bt1 = 0x66, Bt2 = 0xF3, Bt3 = 0x0f, Bt4 = 0xbd },
                new AOpCode { Mnemonic = "LZCNT", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 3, Bt1 = 0xF3, Bt2 = 0x0f, Bt3 = 0xbd },

                new AOpCode { Mnemonic = "MASKMOVDQU", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xf7 },
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

                new AOpCode { Mnemonic = "MONITOR", Bytes = 3, Bt1 = 0x0f, Bt2 = 0x01, Bt3 = 0xc8 },

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

                new AOpCode { Mnemonic = "MOVBE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0xf0 },
                new AOpCode { Mnemonic = "MOVBE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 3, Bt1 = 0x0f, Bt2 = 0x38, Bt3 = 0xf0 },

                new AOpCode { Mnemonic = "MOVBE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0xf1 },
                new AOpCode { Mnemonic = "MOVBE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 3, Bt1 = 0x0f, Bt2 = 0x38, Bt3 = 0xf1 },

                new AOpCode { Mnemonic = "MOVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x6e },
                new AOpCode { Mnemonic = "MOVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_mm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x7e },

                new AOpCode { Mnemonic = "MOVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_rm32, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x6e },
                new AOpCode { Mnemonic = "MOVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x7e },

                new AOpCode { Mnemonic = "MOVDQ2Q", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0xd6 },
                new AOpCode { Mnemonic = "MOVDQA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x6f },
                new AOpCode { Mnemonic = "MOVDQA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m128, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x7f },

                new AOpCode { Mnemonic = "MOVDQU", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x6f },
                new AOpCode { Mnemonic = "MOVDQU", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m128, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x7f },

                new AOpCode { Mnemonic = "MOVDDUP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x12 },

                new AOpCode { Mnemonic = "MOVHLPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x12 },

                new AOpCode { Mnemonic = "MOVHPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_m64, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x16 },
                new AOpCode { Mnemonic = "MOVHPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m64, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x17 },

                new AOpCode { Mnemonic = "MOVHPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x16 },
                new AOpCode { Mnemonic = "MOVHPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m64, ParamType2 = AParam.par_xmm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x17 },

                new AOpCode { Mnemonic = "MOVLHPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x16 },

                new AOpCode { Mnemonic = "MOVLPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_m64, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x12 },
                new AOpCode { Mnemonic = "MOVLPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m64, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x13 },

                new AOpCode { Mnemonic = "MOVLPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x12 },
                new AOpCode { Mnemonic = "MOVLPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m64, ParamType2 = AParam.par_xmm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x13 },

                new AOpCode { Mnemonic = "MOVMSKPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x50 },
                new AOpCode { Mnemonic = "MOVMSKPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x50 },
                new AOpCode { Mnemonic = "MOVNTDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m128, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xe7 },
                new AOpCode { Mnemonic = "MOVNTDQA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x2a },
                new AOpCode { Mnemonic = "MOVNTI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m32, ParamType2 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc3 },

                new AOpCode { Mnemonic = "MOVNTPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m128, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x2b },
                new AOpCode { Mnemonic = "MOVNTPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m128, ParamType2 = AParam.par_xmm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x2b },

                new AOpCode { Mnemonic = "MOVNTQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m64, ParamType2 = AParam.par_mm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xe7 },


                new AOpCode { Mnemonic = "MOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x6f },
                new AOpCode { Mnemonic = "MOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm_m64, ParamType2 = AParam.par_mm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x7f },

                new AOpCode { Mnemonic = "MOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x7e },
                new AOpCode { Mnemonic = "MOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m64, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xd6 },

                new AOpCode { Mnemonic = "MOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x6e },
                new AOpCode { Mnemonic = "MOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_rm32, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x6e },
                new AOpCode { Mnemonic = "MOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x7e },



                new AOpCode { Mnemonic = "MOVQ2DQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_mm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xd6 },

                new AOpCode { Mnemonic = "MOVSB", Bytes = 1, Bt1 = 0xa4 },
                new AOpCode { Mnemonic = "MOVSD", Bytes = 1, Bt1 = 0xa5 },

                new AOpCode { Mnemonic = "MOVSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x10 },
                new AOpCode { Mnemonic = "MOVSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m64, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x11 },

                new AOpCode { Mnemonic = "MOVSHDUP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x16 },
                new AOpCode { Mnemonic = "MOVSLDUP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x12 },


                new AOpCode { Mnemonic = "MOVSQ", Bytes = 1, Bt1 = 0xa5, W1 = true, InvalidIn32Bit = true },

                new AOpCode { Mnemonic = "MOVSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x10 },
                new AOpCode { Mnemonic = "MOVSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m32, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x11 },
                new AOpCode { Mnemonic = "MOVSW", Bytes = 2, Bt1 = 0x66, Bt2 = 0xa5 },

                new AOpCode { Mnemonic = "MOVSX", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xbe },
                new AOpCode { Mnemonic = "MOVSX", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xbe },
                new AOpCode { Mnemonic = "MOVSX", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm16, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xbf },
                new AOpCode { Mnemonic = "MOVSXD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0x63 }, //actuall r64,rm32 but the usage of the 64-bit register turns it into a rex_w itself


                new AOpCode { Mnemonic = "MOVUPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x10 },
                new AOpCode { Mnemonic = "MOVUPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m128, ParamType2 = AParam.par_xmm, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x11 },

                new AOpCode { Mnemonic = "MOVUPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x10 },
                new AOpCode { Mnemonic = "MOVUPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m128, ParamType2 = AParam.par_xmm, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x11 },

                new AOpCode { Mnemonic = "MOVZX", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xb6 },
                new AOpCode { Mnemonic = "MOVZX", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xb6 },
                new AOpCode { Mnemonic = "MOVZX", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm16, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xb7 },

                new AOpCode { Mnemonic = "MPSADBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x42 },

                new AOpCode { Mnemonic = "MUL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm8, Bytes = 1, Bt1 = 0xf6 },
                new AOpCode { Mnemonic = "MUL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xf7 },
                new AOpCode { Mnemonic = "MUL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0xf7 },

                new AOpCode { Mnemonic = "MULPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x59 },
                new AOpCode { Mnemonic = "MULPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x59 },
                new AOpCode { Mnemonic = "MULSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x59 },
                new AOpCode { Mnemonic = "MULSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x59 },

                new AOpCode { Mnemonic = "MULX", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_r32, ParamType3 = AParam.par_rm32, Bytes = 1, Bt1 = 0xf6, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "MWAIT", Bytes = 3, Bt1 = 0x0f, Bt2 = 0x01, Bt3 = 0xc9 },


                new AOpCode { Mnemonic = "NEG", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm8, Bytes = 1, Bt1 = 0xf6 },
                new AOpCode { Mnemonic = "NEG", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xf7 },
                new AOpCode { Mnemonic = "NEG", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0xf7 },

                new AOpCode { Mnemonic = "NOP", Bytes = 1, Bt1 = 0x90 }, //NOP nop Nop nOp noP NoP nOp NOp nOP
                new AOpCode { Mnemonic = "NOP", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_rm16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x1f },
                new AOpCode { Mnemonic = "NOP", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x1f },

                new AOpCode { Mnemonic = "NOT", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_rm8, Bytes = 1, Bt1 = 0xf6 },
                new AOpCode { Mnemonic = "NOT", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0xf7 },
                new AOpCode { Mnemonic = "NOT", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0xf7 },

                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_al, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x0c },
                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_ax, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x0d },
                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_eax, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x0d },
                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_reg1, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x80 },
                new AOpCode { Mnemonic = "OR", OpCode1 = AExtraOpCode.eo_reg1, OpCode2 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x81 },
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

                new AOpCode { Mnemonic = "PABSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 3, Bt1 = 0x0f, Bt2 = 0x38, Bt3 = 0x1c },
                new AOpCode { Mnemonic = "PABSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x1c },
                new AOpCode { Mnemonic = "PABSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 3, Bt1 = 0x0f, Bt2 = 0x38, Bt3 = 0x1e },
                new AOpCode { Mnemonic = "PABSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x1e },
                new AOpCode { Mnemonic = "PABSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 3, Bt1 = 0x0f, Bt2 = 0x38, Bt3 = 0x1d },
                new AOpCode { Mnemonic = "PABSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x1d },



                new AOpCode { Mnemonic = "PACKSSDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x6b },
                new AOpCode { Mnemonic = "PACKSSDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x6b },

                new AOpCode { Mnemonic = "PACKSSWB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x63 },
                new AOpCode { Mnemonic = "PACKSSWB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x63 },

                new AOpCode { Mnemonic = "PACKUSDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x2b },

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

                new AOpCode { Mnemonic = "PALIGNR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, ParamType3 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xfd },
                new AOpCode { Mnemonic = "PALIGNR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xfd },


                new AOpCode { Mnemonic = "PAND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xdb },
                new AOpCode { Mnemonic = "PAND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xdb },

                new AOpCode { Mnemonic = "PANDN", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xdf },
                new AOpCode { Mnemonic = "PANDN", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xdf },

                new AOpCode { Mnemonic = "PAUSE", Bytes = 2, Bt1 = 0xf3, Bt2 = 0x90 },

                new AOpCode { Mnemonic = "PAVGB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xe0 },
                new AOpCode { Mnemonic = "PAVGB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xe0 },

                new AOpCode { Mnemonic = "PAVGW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xe3 },
                new AOpCode { Mnemonic = "PAVGW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xe3 },

                new AOpCode { Mnemonic = "PBLENDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x0e },


                new AOpCode { Mnemonic = "PCMPEQB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x74 },
                new AOpCode { Mnemonic = "PCMPEQB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x74 },

                new AOpCode { Mnemonic = "PCMPEQD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x76 },
                new AOpCode { Mnemonic = "PCMPEQD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x76 },

                new AOpCode { Mnemonic = "PCMPEQQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x29 },

                new AOpCode { Mnemonic = "PCMPEQW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x75 },
                new AOpCode { Mnemonic = "PCMPEQW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x75 },

                new AOpCode { Mnemonic = "PCMPESTRI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x61 },
                new AOpCode { Mnemonic = "PCMPESTRM", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x60 },


                new AOpCode { Mnemonic = "PCMPGTB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x64 },
                new AOpCode { Mnemonic = "PCMPGTB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x64 },

                new AOpCode { Mnemonic = "PCMPGTD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x66 },
                new AOpCode { Mnemonic = "PCMPGTD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x66 },

                new AOpCode { Mnemonic = "PCMPGTW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x65 },
                new AOpCode { Mnemonic = "PCMPGTW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x65 },

                new AOpCode { Mnemonic = "PCMPISTRI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x63 },
                new AOpCode { Mnemonic = "PCMPISTRM", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x62 },

                new AOpCode { Mnemonic = "PCMULQDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x44 },

                new AOpCode { Mnemonic = "PCPPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x53 },
                new AOpCode { Mnemonic = "PCPSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x53 },



                new AOpCode { Mnemonic = "PDEP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, ParamType3 = AParam.par_r32, Bytes = 1, Bt1 = 0xf5, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },
                new AOpCode { Mnemonic = "PEXT", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, ParamType3 = AParam.par_r32, Bytes = 1, Bt1 = 0xf5, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },

                new AOpCode { Mnemonic = "PEXTRB", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x14 },
                new AOpCode { Mnemonic = "PEXTRD", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x16 },
                new AOpCode { Mnemonic = "PEXTRQ", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x16, W1 = true },
                new AOpCode { Mnemonic = "PEXTRW", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_mm, ParamType3 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc5 },
                new AOpCode { Mnemonic = "PEXTRW", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xc5 },
                new AOpCode { Mnemonic = "PEXTRW", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x15 },

                new AOpCode { Mnemonic = "PHADDD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 3, Bt1 = 0x0f, Bt2 = 0x38, Bt3 = 0x01 },
                new AOpCode { Mnemonic = "PHADDD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x01 },
                new AOpCode { Mnemonic = "PHADDSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 3, Bt1 = 0x0f, Bt2 = 0x38, Bt3 = 0x03 },
                new AOpCode { Mnemonic = "PHADDSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x03 },

                new AOpCode { Mnemonic = "PHADDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 3, Bt1 = 0x0f, Bt2 = 0x38, Bt3 = 0x02 },
                new AOpCode { Mnemonic = "PHADDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x02 },

                new AOpCode { Mnemonic = "PHMINPOSUW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x41 },

                new AOpCode { Mnemonic = "PHSUBD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 3, Bt1 = 0x0f, Bt2 = 0x38, Bt3 = 0x06 },
                new AOpCode { Mnemonic = "PHSUBD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x06 },
                new AOpCode { Mnemonic = "PHSUBSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 3, Bt1 = 0x0f, Bt2 = 0x38, Bt3 = 0x07 },
                new AOpCode { Mnemonic = "PHSUBSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x07 },
                new AOpCode { Mnemonic = "PHSUBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 3, Bt1 = 0x0f, Bt2 = 0x38, Bt3 = 0x05 },
                new AOpCode { Mnemonic = "PHSUBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x05 },


                new AOpCode { Mnemonic = "PINSRB", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_r32_m8, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x20 },
                new AOpCode { Mnemonic = "PINSRD", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_rm32, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x22 },
                new AOpCode { Mnemonic = "PINSRQ", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_rm32, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x22, W1 = true },
                new AOpCode { Mnemonic = "PINSRW", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_r32_m16, ParamType3 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc4 },
                new AOpCode { Mnemonic = "PINSRW", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_r32_m16, ParamType3 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xc4 },

                new AOpCode { Mnemonic = "PMADDUBSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 3, Bt1 = 0x0f, Bt2 = 0x38, Bt3 = 0x04 },
                new AOpCode { Mnemonic = "PMADDUBSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x04 },
                new AOpCode { Mnemonic = "PMADDWD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xf5 },
                new AOpCode { Mnemonic = "PMADDWD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xf5 },


                new AOpCode { Mnemonic = "PMAXSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x3c },
                new AOpCode { Mnemonic = "PMAXSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x3d },


                new AOpCode { Mnemonic = "PMAXSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xee },
                new AOpCode { Mnemonic = "PMAXSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xee },

                new AOpCode { Mnemonic = "PMAXUB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xde },
                new AOpCode { Mnemonic = "PMAXUB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xde },

                new AOpCode { Mnemonic = "PMAXUD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x3f },
                new AOpCode { Mnemonic = "PMAXUW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x3e },

                new AOpCode { Mnemonic = "PMINSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x38 },
                new AOpCode { Mnemonic = "PMINSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x39 },

                new AOpCode { Mnemonic = "PMINSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xea },
                new AOpCode { Mnemonic = "PMINSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xea },

                new AOpCode { Mnemonic = "PMINUB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xda },
                new AOpCode { Mnemonic = "PMINUB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xda },
                new AOpCode { Mnemonic = "PMINUD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x3b },
                new AOpCode { Mnemonic = "PMINUW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x3a },


                new AOpCode { Mnemonic = "PMOVMSKB", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_mm, ParamType3 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xd7 },
                new AOpCode { Mnemonic = "PMOVMSKB", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xd7 },

                new AOpCode { Mnemonic = "PMOVSXBD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x21 },
                new AOpCode { Mnemonic = "PMOVSXBQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m16, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x22 },
                new AOpCode { Mnemonic = "PMOVSXBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x20 },
                new AOpCode { Mnemonic = "PMOVSXDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x25 },
                new AOpCode { Mnemonic = "PMOVSXWD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x23 },
                new AOpCode { Mnemonic = "PMOVSXWQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x24 },

                new AOpCode { Mnemonic = "PMOVZXBD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x31 },
                new AOpCode { Mnemonic = "PMOVZXBQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m16, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x32 },
                new AOpCode { Mnemonic = "PMOVZXBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x30 },
                new AOpCode { Mnemonic = "PMOVZXDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x35 },
                new AOpCode { Mnemonic = "PMOVZXWD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x33 },
                new AOpCode { Mnemonic = "PMOVZXWQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x34 },
                new AOpCode { Mnemonic = "PMULDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x28 },


                new AOpCode { Mnemonic = "PMULHRSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 3, Bt1 = 0x0f, Bt2 = 0x38, Bt3 = 0x0b },
                new AOpCode { Mnemonic = "PMULHRSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x0b },

                new AOpCode { Mnemonic = "PMULHUW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xe4 },
                new AOpCode { Mnemonic = "PMULHUW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xe4 },

                new AOpCode { Mnemonic = "PMULHW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xe5 },
                new AOpCode { Mnemonic = "PMULHW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xe5 },

                new AOpCode { Mnemonic = "PMULLD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x40 },

                new AOpCode { Mnemonic = "PMULLW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xd5 },
                new AOpCode { Mnemonic = "PMULLW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xd5 },

                new AOpCode { Mnemonic = "PMULUDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xf4 },
                new AOpCode { Mnemonic = "PMULUDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xf4 },

                new AOpCode { Mnemonic = "POP", OpCode1 = AExtraOpCode.eo_prd, ParamType1 = AParam.par_r32, Bytes = 1, Bt1 = 0x58, W0 = true },
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

                new AOpCode { Mnemonic = "POPCNT", ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_rm16, Bytes = 4, Bt1 = 0x66, Bt2 = 0xf3, Bt3 = 0x0f, Bt4 = 0xb8 },
                new AOpCode { Mnemonic = "POPCNT", ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_rm32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0xb8 },


                new AOpCode { Mnemonic = "POPF", Bytes = 2, Bt1 = 0x66, Bt2 = 0x9d },
                new AOpCode { Mnemonic = "POPFD", Bytes = 1, Bt1 = 0x9d, InvalidIn64Bit = true },
                new AOpCode { Mnemonic = "POPFQ", Bytes = 1, Bt1 = 0x9d, InvalidIn32Bit = true },

                new AOpCode { Mnemonic = "POR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xeb },
                new AOpCode { Mnemonic = "POR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xeb },

                new AOpCode { Mnemonic = "PREFETCH0", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_m8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x18 },
                new AOpCode { Mnemonic = "PREFETCH1", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x18 },
                new AOpCode { Mnemonic = "PREFETCH2", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x18 },
                new AOpCode { Mnemonic = "PREFETCHA", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x18 },

                new AOpCode { Mnemonic = "PREFETCHW", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_m8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x0d },
                new AOpCode { Mnemonic = "PREFETCHWT1", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x0d },

                new AOpCode { Mnemonic = "PSADBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xf6 },
                new AOpCode { Mnemonic = "PSADBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xf6 },

                new AOpCode { Mnemonic = "PSHUFB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 3, Bt1 = 0x0f, Bt2 = 0x38, Bt3 = 0x00 },
                new AOpCode { Mnemonic = "PSHUFB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x00 },
                new AOpCode { Mnemonic = "PSHUFD", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x70 },
                new AOpCode { Mnemonic = "PSHUFHW", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x70 },
                new AOpCode { Mnemonic = "PSHUFLW", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 3, Bt1 = 0xf2, Bt2 = 0x0f, Bt3 = 0x70 },
                new AOpCode { Mnemonic = "PSHUFW", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, ParamType3 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x70 },

                new AOpCode { Mnemonic = "PSIGNB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 3, Bt1 = 0x0f, Bt2 = 0x38, Bt3 = 0x08 },
                new AOpCode { Mnemonic = "PSIGNB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x08 },
                new AOpCode { Mnemonic = "PSIGND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 3, Bt1 = 0x0f, Bt2 = 0x38, Bt3 = 0x0a },
                new AOpCode { Mnemonic = "PSIGND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x0a },
                new AOpCode { Mnemonic = "PSIGNW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 3, Bt1 = 0x0f, Bt2 = 0x38, Bt3 = 0x09 },
                new AOpCode { Mnemonic = "PSIGNW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x09 },


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




                new AOpCode { Mnemonic = "PSUBSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xe8 },
                new AOpCode { Mnemonic = "PSUBSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xe8 },

                new AOpCode { Mnemonic = "PSUBSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_mm, ParamType2 = AParam.par_mm_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xe9 },
                new AOpCode { Mnemonic = "PSUBSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xe9 },


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

                new AOpCode { Mnemonic = "PTEST", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x38, Bt4 = 0x17 },


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
                // new AOpCode { Mnemonic = "PUSH", OpCode1 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x68 },


                new AOpCode { Mnemonic = "PUSH", OpCode1 = AExtraOpCode.eo_prd, ParamType1 = AParam.par_r32, Bytes = 1, Bt1 = 0x50, W0 = true },
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

                new AOpCode { Mnemonic = "RCPPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x53 },
                new AOpCode { Mnemonic = "RCPSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x53 },


                new AOpCode { Mnemonic = "RCR", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd1 },
                new AOpCode { Mnemonic = "RCR", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd3 },
                new AOpCode { Mnemonic = "RCR", OpCode1 = AExtraOpCode.eo_reg3, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc1 },

                new AOpCode { Mnemonic = "RCR", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_1, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd1 },
                new AOpCode { Mnemonic = "RCR", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_cl, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd3 },
                new AOpCode { Mnemonic = "RCR", OpCode1 = AExtraOpCode.eo_reg3, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0xc1 },

                new AOpCode { Mnemonic = "RCR", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd0 },
                new AOpCode { Mnemonic = "RCR", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd2 },
                new AOpCode { Mnemonic = "RCR", OpCode1 = AExtraOpCode.eo_reg3, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc0 },

                new AOpCode { Mnemonic = "RDFSBASE", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_r32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0xae },
                new AOpCode { Mnemonic = "RDGSBASE", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_r32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0xae },


                new AOpCode { Mnemonic = "RDMSR", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x32 },
                new AOpCode { Mnemonic = "RDPMC", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x33 },

                new AOpCode { Mnemonic = "RDRAND", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_r16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xc7 },
                new AOpCode { Mnemonic = "RDRAND", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc7 },

                new AOpCode { Mnemonic = "RDSEED", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_r16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xc7 },
                new AOpCode { Mnemonic = "RDSEED", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc7 },

                new AOpCode { Mnemonic = "RDTSC", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x31 },
                new AOpCode { Mnemonic = "RDTSCP", Bytes = 3, Bt1 = 0x0f, Bt2 = 0x01, Bt3 = 0xf9 },

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

                new AOpCode { Mnemonic = "RORX", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0xf0, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },

                new AOpCode { Mnemonic = "ROUNDPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x09 },
                new AOpCode { Mnemonic = "ROUNDPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x08 },
                new AOpCode { Mnemonic = "ROUNDSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x0b },
                new AOpCode { Mnemonic = "ROUNDSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 4, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x3a, Bt4 = 0x0a },


                new AOpCode { Mnemonic = "RSM", Bytes = 2, Bt1 = 0x0f, Bt2 = 0xaa },


                new AOpCode { Mnemonic = "RSQRTPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x52 },
                new AOpCode { Mnemonic = "RSQRTSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0x52 },



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

                new AOpCode { Mnemonic = "SARX", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0xf7, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },

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
                new AOpCode { Mnemonic = "SCASQ", Bytes = 2, Bt1 = 0xaf, W1 = true },
                new AOpCode { Mnemonic = "SCASW", Bytes = 2, Bt1 = 0x66, Bt2 = 0xaf },


                new AOpCode { Mnemonic = "SETA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x97, DefaultType = true },
                new AOpCode { Mnemonic = "SETAE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x93, DefaultType = true },
                new AOpCode { Mnemonic = "SETB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x92, DefaultType = true },
                new AOpCode { Mnemonic = "SETBE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x96, DefaultType = true },
                new AOpCode { Mnemonic = "SETC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x92, DefaultType = true },
                new AOpCode { Mnemonic = "SETE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x94, DefaultType = true },
                new AOpCode { Mnemonic = "SETG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9f, DefaultType = true },
                new AOpCode { Mnemonic = "SETGE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9d, DefaultType = true },
                new AOpCode { Mnemonic = "SETL", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9c, DefaultType = true },
                new AOpCode { Mnemonic = "SETLE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9e, DefaultType = true },
                new AOpCode { Mnemonic = "SETNA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x96, DefaultType = true },

                new AOpCode { Mnemonic = "SETNAE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x92, DefaultType = true },
                new AOpCode { Mnemonic = "SETNB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x93, DefaultType = true },
                new AOpCode { Mnemonic = "SETNBE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x97, DefaultType = true },
                new AOpCode { Mnemonic = "SETNC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x93, DefaultType = true },
                new AOpCode { Mnemonic = "SETNE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x95, DefaultType = true },
                new AOpCode { Mnemonic = "SETNG", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9e, DefaultType = true },
                new AOpCode { Mnemonic = "SETNGE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9c, DefaultType = true },
                new AOpCode { Mnemonic = "SETNL", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9d, DefaultType = true },
                new AOpCode { Mnemonic = "SETNLE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9f, DefaultType = true },
                new AOpCode { Mnemonic = "SETNO", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x91, DefaultType = true },
                new AOpCode { Mnemonic = "SETNP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9b, DefaultType = true },

                new AOpCode { Mnemonic = "SETNS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x99, DefaultType = true },
                new AOpCode { Mnemonic = "SETNZ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x95, DefaultType = true },
                new AOpCode { Mnemonic = "SETO", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x90, DefaultType = true },
                new AOpCode { Mnemonic = "SETP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9a, DefaultType = true },
                new AOpCode { Mnemonic = "SETPE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9a, DefaultType = true },
                new AOpCode { Mnemonic = "SETPO", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x9b, DefaultType = true },
                new AOpCode { Mnemonic = "SETS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x98, DefaultType = true },
                new AOpCode { Mnemonic = "SETZ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x94, DefaultType = true },

                new AOpCode { Mnemonic = "SFENCE", Bytes = 3, Bt1 = 0x0f, Bt2 = 0xae, Bt3 = 0xf8 },

                new AOpCode { Mnemonic = "SGDT", OpCode1 = AExtraOpCode.eo_reg0, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x01 },

                new AOpCode { Mnemonic = "SHL", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0xd1 },
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

                new AOpCode { Mnemonic = "SHLX", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0xf7, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },



                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd0 },
                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd2 },
                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc0 },

                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_1, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd1 },
                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_cl, Bytes = 2, Bt1 = 0x66, Bt2 = 0xd3 },
                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm8, Bytes = 2, Bt1 = 0x66, Bt2 = 0xc1 },

                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_rm32, Bytes = 1, Bt1 = 0xd1 },
                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_1, Bytes = 1, Bt1 = 0xd1 },
                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_cl, Bytes = 1, Bt1 = 0xd3 },
                new AOpCode { Mnemonic = "SHR", OpCode1 = AExtraOpCode.eo_reg5, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc1 },

                new AOpCode { Mnemonic = "SHRD", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, ParamType3 = AParam.par_imm8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xac },
                new AOpCode { Mnemonic = "SHRD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, ParamType3 = AParam.par_cl, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xad },

                new AOpCode { Mnemonic = "SHRX", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0xf7, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },


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
                new AOpCode { Mnemonic = "STOSQ", Bytes = 1, Bt1 = 0xab, W1 = true },
                new AOpCode { Mnemonic = "STOSW", Bytes = 2, Bt1 = 0x66, Bt2 = 0xab },

                new AOpCode { Mnemonic = "STR", OpCode1 = AExtraOpCode.eo_reg1, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x00 },


                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_al, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x2c },
                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_ax, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x2d },
                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_id, ParamType1 = AParam.par_eax, ParamType2 = AParam.par_imm32, Bytes = 1, Bt1 = 0x2d },
                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_reg5, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_imm8, Bytes = 1, Bt1 = 0x80 },
                new AOpCode { Mnemonic = "SUB", OpCode1 = AExtraOpCode.eo_reg5, OpCode2 = AExtraOpCode.eo_iw, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_imm16, Bytes = 2, Bt1 = 0x66, Bt2 = 0x81 },
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

                new AOpCode { Mnemonic = "SYSCALL", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x05, InvalidIn32Bit = true },
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

                new AOpCode { Mnemonic = "TZCNT", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r16, ParamType2 = AParam.par_rm16, Bytes = 4, Bt1 = 0x66, Bt2 = 0xF3, Bt3 = 0x0f, Bt4 = 0xbc },
                new AOpCode { Mnemonic = "TZCNT", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 3, Bt1 = 0xF3, Bt2 = 0x0f, Bt3 = 0xbc },


                new AOpCode { Mnemonic = "UCOMISD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x2e },
                new AOpCode { Mnemonic = "UCOMISS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x2e },

                new AOpCode { Mnemonic = "UD2", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x0b },

                new AOpCode { Mnemonic = "UNPCKHPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x15 },
                new AOpCode { Mnemonic = "UNPCKHPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x15 },

                new AOpCode { Mnemonic = "UNPCKLPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0x14 },
                new AOpCode { Mnemonic = "UNPCKLPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x14 },

                new AOpCode { Mnemonic = "VADDPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x58, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VADDPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x58, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VADDPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x58, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VADDPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x58, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VADDSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x58, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VADDSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x58, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VADDSUBPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xd0, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VADDSUBPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xd0, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VADDSUBPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xd0, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VADDSUBPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xd0, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VAEKEYGENASSIST", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0xdf, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 0 },
                new AOpCode { Mnemonic = "VAESDEC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xde, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VAESDECLAST", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xdf, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VAESENC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xdc, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VAESENCLAST", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xdd, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VAESIMC", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xdb, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VANDNPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x55, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VANDNPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x55, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VANDNPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x55, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VANDNPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x55, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VANDPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x54, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VANDPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x54, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VANDPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x54, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VANDPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x54, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VBLENDPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x0d, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VBLENDPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x0d, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VBLENDPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x0c, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VBLENDPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x0c, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VBLENDVPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_xmm, Bytes = 1, Bt1 = 0x4b, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VBLENDVPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, ParamType4 = AParam.par_ymm, Bytes = 1, Bt1 = 0x4b, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VBLENDVPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_xmm, Bytes = 1, Bt1 = 0x4a, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VBLENDVPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, ParamType4 = AParam.par_ymm, Bytes = 1, Bt1 = 0x4a, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VBROADCASTF128", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_m128, Bytes = 1, Bt1 = 0x1a, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VBROADCASTSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x19, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VBROADCASTSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x18, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VBROADCASTSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x18, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },



                new AOpCode { Mnemonic = "VCMPPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc2, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VCMPPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc2, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VCMPPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc2, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VCMPPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc2, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VCMPSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc2, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VCMPSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc2, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VCOMISD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x2f, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VCOMISS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x2f, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VCVTDQ2PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0xe6, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VCVTDQ2PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xe6, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VCVTDQ2PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x5b, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VCVTDQ2PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x5b, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VCVTPD2DQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xe6, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VCVTPD2DQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xe6, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VCVTPD2PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x5a, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VCVTPD2PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x5a, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VCVTPH2PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x13, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VCVTPH2PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x13, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },


                new AOpCode { Mnemonic = "VCVTPS2DQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x5b, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VCVTPS2DQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x5b, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VCVTPS2PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x5a, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VCVTPS2PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x5a, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VCVTPS2PH", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m128, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x1d, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },
                new AOpCode { Mnemonic = "VCVTPS2PH", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m64, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x1d, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },


                new AOpCode { Mnemonic = "VCVTSD2SI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x2d, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VCVTSD2SS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x5a, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VCVTSI2SD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_rm32, Bytes = 1, Bt1 = 0x2a, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VCVTSI2SS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_rm32, Bytes = 1, Bt1 = 0x2a, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VCVTSS2SD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x5a, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VCVTSS2SI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x2d, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VCVTTPD2DQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xe6, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VCVTTPD2DQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xe6, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VCVTTPS2DQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x5b, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VCVTTPS2DQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x5b, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VCVTTSD2SI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x2c, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VCVTTSS2SI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x2c, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VDIVPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x5e, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VDIVPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x5e, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VDIVPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x5e, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VDIVPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x5e, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VDIVSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x5e, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VDIVSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x5e, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VDPPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x41, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VDPPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x40, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VDPPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x40, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "VERR", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x00 },
                new AOpCode { Mnemonic = "VERW", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_rm16, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x00 },

                new AOpCode { Mnemonic = "VEXTRACTF128", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m128, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x19, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },
                new AOpCode { Mnemonic = "VEXTRACTI128", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m128, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x39, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },
                new AOpCode { Mnemonic = "VEXTRACTPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x17, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "VFMADD132PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x98, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADD132PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x98, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADD132PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x98, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADD132PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x98, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADD132SD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x99, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADD132SS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x99, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADD213PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xA8, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADD213PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xA8, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADD213PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xA8, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADD213PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xA8, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADD213SD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0xA9, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADD213SS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0xA9, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADD231PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xB8, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADD231PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xB8, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADD231PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xB8, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADD231PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xB8, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADD231SD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0xB9, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADD231SS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0xB9, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "VFMADDSUB132PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x96, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADDSUB132PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x96, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADDSUB132PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x96, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADDSUB132PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x96, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADDSUB213PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xA6, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADDSUB213PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xA6, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADDSUB213PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xA6, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADDSUB213PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xA6, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADDSUB231PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xB6, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADDSUB231PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xB6, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADDSUB231PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xB6, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMADDSUB231PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xB6, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VFMSUB132PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x9A, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUB132PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x9A, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUB132PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x9A, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUB132PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x9A, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUB132SD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x9B, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUB132SS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x9B, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUB213PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xAA, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUB213PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xAA, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUB213PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xAA, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUB213PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xAA, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUB213SD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0xAB, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUB213SS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0xAB, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUB231PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xBA, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUB231PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xBA, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUB231PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xBA, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUB231PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xBA, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUB231SD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0xBB, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUB231SS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0xBB, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VFMSUBADD132PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x97, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUBADD132PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x97, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUBADD132PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x97, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUBADD132PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x97, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUBADD213PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xA7, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUBADD213PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xA7, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUBADD213PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xA7, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUBADD213PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xA7, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUBADD231PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xB7, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUBADD231PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xB7, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUBADD231PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xB7, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFMSUBADD231PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xB7, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "VFNMADD132PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x9C, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADD132PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x9C, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADD132PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x9C, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADD132PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x9C, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADD132SD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x9D, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADD132SS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x9D, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADD213PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xAC, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADD213PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xAC, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADD213PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xAC, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADD213PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xAC, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADD213SD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0xAD, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADD213SS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0xAD, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADD231PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xBC, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADD231PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xBC, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADD231PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xBC, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADD231PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xBC, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADD231SD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0xBD, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADD231SS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0xBD, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "VFNMADDSUB132PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x96, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADDSUB132PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x96, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADDSUB132PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x96, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADDSUB132PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x96, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADDSUB213PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xA6, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADDSUB213PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xA6, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADDSUB213PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xA6, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADDSUB213PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xA6, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADDSUB231PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xB6, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADDSUB231PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xB6, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADDSUB231PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xB6, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMADDSUB231PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xB6, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VFNMSUB132PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x9E, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUB132PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x9E, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUB132PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x9E, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUB132PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x9E, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUB132SD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x9F, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUB132SS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x9F, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUB213PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xAE, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUB213PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xAE, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUB213PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xAE, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUB213PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xAE, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUB213SD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0xAF, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUB213SS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0xAF, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUB231PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xBE, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUB231PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xBE, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUB231PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xBE, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUB231PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xBE, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUB231SD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0xBF, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUB231SS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0xBF, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VFNMSUBADD132PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x97, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUBADD132PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x97, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUBADD132PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x97, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUBADD132PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x97, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUBADD213PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xA7, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUBADD213PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xA7, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUBADD213PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xA7, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUBADD213PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xA7, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUBADD231PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xB7, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUBADD231PD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xB7, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUBADD231PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xB7, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VFNMSUBADD231PS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xB7, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },


                //todo = add modrm support for vm*
                //new AOpCode { Mnemonic = "VGATHERDPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_vm32x, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x92, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },
                //new AOpCode { Mnemonic = "VGATHERQPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_vm64x, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x93, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },
                //new AOpCode { Mnemonic = "VGATHERDPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_vm32x, ParamType3 = AParam.par_ymm, Bytes = 1, Bt1 = 0x92, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },
                //new AOpCode { Mnemonic = "VGATHERQPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_vm64y, ParamType3 = AParam.par_ymm, Bytes = 1, Bt1 = 0x93, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },
                //new AOpCode { Mnemonic = "VGATHERDPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_vm32x, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x92, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },
                //new AOpCode { Mnemonic = "VGATHERQPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_vm64x, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x93, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },
                //new AOpCode { Mnemonic = "VGATHERDPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_vm32x, ParamType3 = AParam.par_ymm, Bytes = 1, Bt1 = 0x92, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },
                //new AOpCode { Mnemonic = "VGATHERQPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_vm64y, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x93, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },
                //new AOpCode { Mnemonic = "VPGATHERDD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_vm32x, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x90, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },
                //new AOpCode { Mnemonic = "VPGATHERQD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_vm64x, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x91, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },
                //new AOpCode { Mnemonic = "VPGATHERDD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_vm32x, ParamType3 = AParam.par_ymm, Bytes = 1, Bt1 = 0x90, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },
                //new AOpCode { Mnemonic = "VPGATHERQD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_vm64y, ParamType3 = AParam.par_ymm, Bytes = 1, Bt1 = 0x91, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },
                //new AOpCode { Mnemonic = "VPGATHERDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_vm32x, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x90, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },
                //new AOpCode { Mnemonic = "VPGATHERQQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_vm64x, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x91, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },
                //new AOpCode { Mnemonic = "VPGATHERDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_vm32x, ParamType3 = AParam.par_ymm, Bytes = 1, Bt1 = 0x90, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },
                //new AOpCode { Mnemonic = "VPGATHERQQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_vm64y, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x91, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 3 },


                new AOpCode { Mnemonic = "VHADDPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x7c, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VHADDPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x7c, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VHADDPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x7c, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VHADDPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x7c, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VHSUBPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x7d, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VHSUBPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x7d, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VHSUBPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x7d, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VHSUBPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x7d, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VINSERTF128", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x18, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },
                new AOpCode { Mnemonic = "VINSERTI128", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x38, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },
                new AOpCode { Mnemonic = "VINSERTPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m32, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x21, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VLDDQU", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_m128, Bytes = 1, Bt1 = 0xf0, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VLDDQU", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_m256, Bytes = 1, Bt1 = 0xf0, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VLDMXCSR", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xae, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMASKMOVDQU", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0xf7, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMASKMOVPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_m128, Bytes = 1, Bt1 = 0x2d, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMASKMOVPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_m256, Bytes = 1, Bt1 = 0x2d, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMASKMOVPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m128, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x2f, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMASKMOVPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m256, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm, Bytes = 1, Bt1 = 0x2f, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMASKMOVPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_m128, Bytes = 1, Bt1 = 0x2c, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMASKMOVPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_m256, Bytes = 1, Bt1 = 0x2c, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMASKMOVPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m128, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x2e, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMASKMOVPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m256, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm, Bytes = 1, Bt1 = 0x2e, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "VMAXPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x5f, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMAXPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x5f, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMAXPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x5f, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMAXPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x5f, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMAXSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x5f, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMAXSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x5f, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VMCALL", Bytes = 3, Bt1 = 0x0f, Bt2 = 0x01, Bt3 = 0xc1 },
                new AOpCode { Mnemonic = "VMCLEAR", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m64, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xc7 },

                new AOpCode { Mnemonic = "VMIXPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x5d, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMIXPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x5d, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMIXPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x5d, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMIXPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x5d, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMIXSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x5d, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMIXSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x5d, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "VMLAUNCH", Bytes = 3, Bt1 = 0x0f, Bt2 = 0x01, Bt3 = 0xc2 },

                new AOpCode { Mnemonic = "VMOVAPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x28, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVAPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x28, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVAPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m128, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0x29, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVAPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm_m256, ParamType2 = AParam.par_ymm, Bytes = 1, Bt1 = 0x29, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMOVAPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x28, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVAPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x28, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVAPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m128, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0x29, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVAPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm_m256, ParamType2 = AParam.par_ymm, Bytes = 1, Bt1 = 0x29, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMOVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0x6e, HasVex = true, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0x7e, HasVex = true, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMOVDDUP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x12, HasVex = true, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVDDUP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x12, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMOVDQA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x6f, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVDQA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x6f, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVDQA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m128, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0x7f, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVDQA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm_m256, ParamType2 = AParam.par_ymm, Bytes = 1, Bt1 = 0x7f, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMOVDQU", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x6f, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVDQU", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x6f, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVDQU", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m128, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0x7f, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVDQU", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm_m256, ParamType2 = AParam.par_ymm, Bytes = 1, Bt1 = 0x7f, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },


                new AOpCode { Mnemonic = "VMOVHLPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x12, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMOVHPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_m64, Bytes = 1, Bt1 = 0x16, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMOVHPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m64, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0x17, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVHPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_m64, Bytes = 1, Bt1 = 0x16, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMOVHPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m64, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0x17, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVLHPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x16, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VMOVLPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_m64, Bytes = 1, Bt1 = 0x12, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMOVLPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m64, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0x13, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVLPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_m64, Bytes = 1, Bt1 = 0x12, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMOVLPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m64, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0x13, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMOVMSKPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0x50, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVMSKPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_ymm, Bytes = 1, Bt1 = 0x50, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMOVMSKPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0x50, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVMSKPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_ymm, Bytes = 1, Bt1 = 0x50, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMOVNTDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m128, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0xe7, HasVex = true, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVNTDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m256, ParamType2 = AParam.par_ymm, Bytes = 1, Bt1 = 0xe7, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMOVNTDQA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_m128, Bytes = 1, Bt1 = 0x2a, HasVex = true, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VMOVNTDQA", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_m256, Bytes = 1, Bt1 = 0x2a, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },

                new AOpCode { Mnemonic = "VMOVNTPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m128, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0x2b, HasVex = true, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVNTPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m256, ParamType2 = AParam.par_ymm, Bytes = 1, Bt1 = 0x2b, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMOVNTPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m128, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0x2b, HasVex = true, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVNTPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m256, ParamType2 = AParam.par_ymm, Bytes = 1, Bt1 = 0x2b, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },



                new AOpCode { Mnemonic = "VMOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x7e, HasVex = true, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m64, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0xd6, HasVex = true, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_rm32, Bytes = 1, Bt1 = 0x6e, HasVex = true, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0x7e, HasVex = true, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMOVSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x10, HasVex = true, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMOVSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_m64, Bytes = 1, Bt1 = 0x10, HasVex = true, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMOVSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x11, HasVex = true, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMOVSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m64, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0x11, HasVex = true, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMOVSHDUP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x16, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVSHDUP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x16, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMOVSLDUP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x12, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVSLDUP", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x12, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMOVSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x10, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMOVSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_m32, Bytes = 1, Bt1 = 0x10, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x11, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMOVSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m32, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0x11, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMOVUPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x10, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVUPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x10, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVUPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m128, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0x11, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVUPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm_m256, ParamType2 = AParam.par_ymm, Bytes = 1, Bt1 = 0x11, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMOVUPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x10, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVUPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x10, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVUPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm_m128, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0x11, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VMOVUPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm_m256, ParamType2 = AParam.par_ymm, Bytes = 1, Bt1 = 0x11, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VMPSADBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x42, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMPSADBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x42, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VMPTRLD", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc7 },
                new AOpCode { Mnemonic = "VMPTRST", OpCode1 = AExtraOpCode.eo_reg7, ParamType1 = AParam.par_m64, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc7 },


                new AOpCode { Mnemonic = "VMREAD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x78, W0 = true },
                new AOpCode { Mnemonic = "VMRESUME", Bytes = 3, Bt1 = 0x0f, Bt2 = 0x01, Bt3 = 0xc3 },

                new AOpCode { Mnemonic = "VMULPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x59, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMULPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x59, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMULPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x59, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMULPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x59, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMULSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x59, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMULSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x59, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMULSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x59, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VMULSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x59, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },



                new AOpCode { Mnemonic = "VMWRITE", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_rm32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x79, W0 = true },
                new AOpCode { Mnemonic = "VMXOFF", Bytes = 3, Bt1 = 0x0f, Bt2 = 0x01, Bt3 = 0xc4 },
                new AOpCode { Mnemonic = "VMXON", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m64, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0xc7 },

                new AOpCode { Mnemonic = "VORPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x56, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VORPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x56, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VORPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x56, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VORPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x56, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPABSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x1c, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPABSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x1c, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPABSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x1e, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPABSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x1e, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPABSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x1d, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPABSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x1d, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },

                new AOpCode { Mnemonic = "VPACKSSDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x6b, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VPACKSSDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x6b, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VPACKSSWB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x63, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VPACKSSWB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x63, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VPACKUSDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x2b, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPACKUSDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x2b, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPACKUSWB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x67, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPACKUSWB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x67, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPADDB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xfc, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPADDB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xfc, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPADDD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xfe, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPADDD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xfe, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPADDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xd4, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPADDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xd4, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPADDSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xec, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPADDSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xec, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPADDSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xed, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPADDSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xed, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPADDUSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xdc, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPADDUSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xdc, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPADDUSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xdd, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPADDUSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xdd, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "VPADDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xfd, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPADDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xfd, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },




                new AOpCode { Mnemonic = "VPALIGNR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x0f, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPALIGNR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x0f, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPAND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xdb, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPAND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xdb, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPANDN", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xdf, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPANDN", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xdf, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPAVGB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xe0, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPAVGB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xe0, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPAVGW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xe3, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPAVGW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xe3, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPBLEND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x02, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPBLEND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x02, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPBLENDVB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_xmm, Bytes = 1, Bt1 = 0x10, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPBLENDVB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, ParamType4 = AParam.par_ymm, Bytes = 1, Bt1 = 0x10, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPBLENDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x0e, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPBLENDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x0e, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPBROADCASTB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m8, Bytes = 1, Bt1 = 0x78, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPBROADCASTB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m8, Bytes = 1, Bt1 = 0x78, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPBROADCASTD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x58, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPBROADCASTD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x58, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPBROADCASTI128", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_m128, Bytes = 1, Bt1 = 0x50, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPBROADCASTQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x59, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPBROADCASTQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x59, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPBROADCASTW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m16, Bytes = 1, Bt1 = 0x79, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPBROADCASTW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m16, Bytes = 1, Bt1 = 0x79, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },


                new AOpCode { Mnemonic = "VPCLMULQDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x44, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPCMPEQB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x74, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPCMPEQB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x74, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPCMPEQD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x76, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPCMPEQD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x76, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPCMPEQQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x29, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPCMPEQQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x29, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPCMPEQW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x75, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPCMPEQW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x75, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPCMPESTRI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x61, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPCMPESTRM", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x60, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "VPCMPGTB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x64, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPCMPGTB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x64, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPCMPGTD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x66, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPCMPGTD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x66, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPCMPGTQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x37, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPCMPGTQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x37, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPCMPGTW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x65, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPCMPGTW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x65, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPCMPISTRI", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x63, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },
                new AOpCode { Mnemonic = "VPCMPISTRM", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x62, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },

                new AOpCode { Mnemonic = "VPERM2F128", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x06, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPERM2I128", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x46, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPERMD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x36, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPERMILPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m128, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x0d, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPERMILPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m256, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm, Bytes = 1, Bt1 = 0x0d, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPERMILPD", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_m128, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x05, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },
                new AOpCode { Mnemonic = "VPERMILPD", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_m256, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x05, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },
                new AOpCode { Mnemonic = "VPERMILPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m128, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x0c, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPERMILPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m256, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm, Bytes = 1, Bt1 = 0x0c, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPERMILPS", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_m128, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x04, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },
                new AOpCode { Mnemonic = "VPERMILPS", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_m256, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x04, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },
                new AOpCode { Mnemonic = "VPERMPD", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x01, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },
                new AOpCode { Mnemonic = "VPERMPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x16, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPERMQ", OpCode1 = AExtraOpCode.eo_reg, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x00, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },




                new AOpCode { Mnemonic = "VPEXTRB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x14, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },
                new AOpCode { Mnemonic = "VPEXTRD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x16, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },
                new AOpCode { Mnemonic = "VPEXTRQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x16, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },
                new AOpCode { Mnemonic = "VPEXTRW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc5, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VPEXTRW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x15, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },

                new AOpCode { Mnemonic = "VPHADDD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x02, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPHADDD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x02, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPHADDSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x03, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPHADDSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x03, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPHADDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x01, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPHADDW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x01, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPHMINPOSUW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x41, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },


                new AOpCode { Mnemonic = "VPHSUBD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x06, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPHSUBD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x06, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPHSUBSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x07, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPHSUBSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x07, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPHSUBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x05, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPHSUBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x05, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "VPINSRB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_r32_m8, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x20, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },
                new AOpCode { Mnemonic = "VPINSRD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_rm32, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x22, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },
                new AOpCode { Mnemonic = "VPINSRW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_r32_m16, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc4, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VPMADDUBSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x04, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMADDUBSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x04, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPMADDWD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xf5, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMADDWD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xf5, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "VPMASKMOVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_m128, Bytes = 1, Bt1 = 0x8c, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMASKMOVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_m256, Bytes = 1, Bt1 = 0x8c, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMASKMOVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m128, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x8e, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMASKMOVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m256, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm, Bytes = 1, Bt1 = 0x8e, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPMASKMOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_m128, Bytes = 1, Bt1 = 0x8c, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMASKMOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_m256, Bytes = 1, Bt1 = 0x8c, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMASKMOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m128, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm, Bytes = 1, Bt1 = 0x8e, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMASKMOVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_m256, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm, Bytes = 1, Bt1 = 0x8e, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPMAXSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x3c, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMAXSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x3c, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMAXSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x3d, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMAXSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x3d, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMAXSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xee, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMAXSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xee, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMAXUB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xde, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMAXUB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xde, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMAXUD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x3f, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMAXUD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x3f, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMAXUW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x3e, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMAXUW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x3e, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPMINSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x38, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMINSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x38, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMINSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x39, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMINSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x39, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMINSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xea, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMINSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xea, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMINUB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xda, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMINUB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xda, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMINUD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x3b, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMINUD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x3b, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMINUW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x3a, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMINUW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x3a, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPMOVMSKB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_xmm, Bytes = 1, Bt1 = 0xd7, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VPMOVMSKB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_r32, ParamType2 = AParam.par_ymm, Bytes = 1, Bt1 = 0xd7, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VPMOVSXBD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x21, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVSXBD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x21, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVSXBQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m16, Bytes = 1, Bt1 = 0x22, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVSXBQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m16, Bytes = 1, Bt1 = 0x22, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVSXBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x20, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVSXBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x20, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVSXDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x25, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVSXDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x25, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVSXWD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x23, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVSXWD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x23, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVSXWQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x24, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVSXWQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x24, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },

                new AOpCode { Mnemonic = "VPMOVZXBD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x31, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVZXBD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x31, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVZXBQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m16, Bytes = 1, Bt1 = 0x32, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVZXBQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m16, Bytes = 1, Bt1 = 0x32, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVZXBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x30, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVZXBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x30, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVZXDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x35, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVZXDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x35, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVZXWD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x33, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVZXWD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x33, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVZXWQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x34, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPMOVZXWQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x34, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },

                new AOpCode { Mnemonic = "VPMULDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x28, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMULDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x28, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPMULHRSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x0b, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMULHRSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x0b, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMULHUW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xe4, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMULHUW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xe4, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMULHW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xe5, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMULHW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xe5, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPMULLD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x40, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMULLD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x40, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMULLW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xd5, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMULLW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xd5, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMULUDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xf4, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPMULUDQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xf4, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPOR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xeb, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPOR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xeb, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPSADBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xf6, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSADBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xf6, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSHUFB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x00, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSHUFB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x00, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSHUFD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x70, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VPSHUFD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x70, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VPSHUFHW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x70, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VPSHUFHW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x70, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VPSHUFLW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x70, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VPSHUFLW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x70, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VPSIGNB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x08, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSIGNB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x08, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSIGND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x0a, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSIGND", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x0a, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSIGNW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x09, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSIGNW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x09, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "VPSLLD", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x72, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "VPSLLD", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x72, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "VPSLLD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xf2, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSLLD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xf2, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSLLDQ", OpCode1 = AExtraOpCode.eo_reg7, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x73, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "VPSLLDQ", OpCode1 = AExtraOpCode.eo_reg7, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x73, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },

                new AOpCode { Mnemonic = "VPSLLQ", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x73, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "VPSLLQ", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x73, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "VPSLLQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xf3, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSLLQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xf3, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "VPSLLVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_m128, Bytes = 1, Bt1 = 0x8c, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSLLVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_m256, Bytes = 1, Bt1 = 0x8c, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSLLVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_m128, Bytes = 1, Bt1 = 0x8c, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSLLVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_m256, Bytes = 1, Bt1 = 0x8c, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "VPSLLW", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x71, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "VPSLLW", OpCode1 = AExtraOpCode.eo_reg6, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x71, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "VPSLLW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xf1, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSLLW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xf1, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },



                new AOpCode { Mnemonic = "VPSRAD", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x72, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "VPSRAD", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x72, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "VPSRAD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xe2, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSRAD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xe2, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPSRAVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_m128, Bytes = 1, Bt1 = 0x46, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSRAVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_m256, Bytes = 1, Bt1 = 0x46, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPSRAW", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x71, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "VPSRAW", OpCode1 = AExtraOpCode.eo_reg4, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x71, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "VPSRAW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xe1, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSRAW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xe1, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPSRLD", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x72, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "VPSRLD", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x72, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "VPSRLD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xd2, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSRLD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xd2, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSRLDQ", OpCode1 = AExtraOpCode.eo_reg3, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x73, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "VPSRLDQ", OpCode1 = AExtraOpCode.eo_reg3, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x73, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },

                new AOpCode { Mnemonic = "VPSRLQ", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x73, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "VPSRLQ", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x73, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "VPSRLQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xd3, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSRLQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xd3, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPSRLVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_m128, Bytes = 1, Bt1 = 0x45, W0 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSRLVD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_m256, Bytes = 1, Bt1 = 0x45, W0 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSRLVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_m128, Bytes = 1, Bt1 = 0x45, W1 = true, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSRLVQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_m256, Bytes = 1, Bt1 = 0x45, W1 = true, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPSRLW", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x71, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "VPSRLW", OpCode1 = AExtraOpCode.eo_reg2, OpCode2 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x71, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 1 },
                new AOpCode { Mnemonic = "VPSRLW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xd1, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSRLW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xd1, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "VPSUBB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xf8, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSUBB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xf8, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSUBD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xfa, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSUBD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xfa, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSUBQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xfb, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSUBQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xfb, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPSUBSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xe8, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSUBSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xe8, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSUBSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xe9, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSUBSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xe9, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPSUBUSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xd8, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSUBUSB", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xd8, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSUBUSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xd9, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSUBUSW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xd9, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPSUBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xf9, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPSUBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xf9, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "VPTEST", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x17, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VPTEST", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x17, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },


                new AOpCode { Mnemonic = "VPUNPCKHBD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x69, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPUNPCKHBD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x69, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPUNPCKHBQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x6a, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPUNPCKHBQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x6a, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPUNPCKHBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x68, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPUNPCKHBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x68, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPUNPCKLBD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x61, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPUNPCKLBD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x61, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPUNPCKLBQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x62, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPUNPCKLBQ", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x62, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPUNPCKLBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x60, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPUNPCKLBW", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x60, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VPXOR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0xef, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VPXOR", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0xef, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VRCPPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x53, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VRCPPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x53, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },

                new AOpCode { Mnemonic = "VRCPSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x53, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },

                new AOpCode { Mnemonic = "VROUNDPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x09, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },
                new AOpCode { Mnemonic = "VROUNDPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x08, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a },
                new AOpCode { Mnemonic = "VROUNDSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x0b, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VROUNDSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x0a, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_3a, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VRSQRTPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, ParamType3 = AParam.par_imm8, Bytes = 1, Bt1 = 0x52, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VRSQRTSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0x52, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VSHUFPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc6, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VSHUFPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc6, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VSHUFPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc6, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VSHUFPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, ParamType4 = AParam.par_imm8, Bytes = 1, Bt1 = 0xc6, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VSQRTPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x51, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VSQRTPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x51, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VSQRTPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x51, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VSQRTPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x51, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VSQRTSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x51, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VSQRTSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x51, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VSQRTSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x51, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VSQRTSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x51, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "VSTMXCSR", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m32, Bytes = 1, Bt1 = 0xae, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },


                new AOpCode { Mnemonic = "VSUBPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x5c, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VSUBPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x5c, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VSUBPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x5c, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VSUBPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x5c, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VSUBSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x5c, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VSUBSD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x5c, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f2, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VSUBSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x5c, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VSUBSS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x5c, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_f3, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VTESTPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x0e, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VTESTPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x0e, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VTESTPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x0f, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },
                new AOpCode { Mnemonic = "VTESTPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x0f, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f_38 },

                new AOpCode { Mnemonic = "VUCOMISD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m64, Bytes = 1, Bt1 = 0x2e, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VUCOMISS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m32, Bytes = 1, Bt1 = 0x2e, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "VUNPCKHPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x15, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VUNPCKHPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x15, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VUNPCKHPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x15, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VUNPCKHPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x15, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },

                new AOpCode { Mnemonic = "VUNPCKLPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x14, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VUNPCKLPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x14, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VUNPCKLPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x14, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VUNPCKLPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x14, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },



                new AOpCode { Mnemonic = "VXORPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x57, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VXORPD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x57, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_66, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VXORPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm, ParamType3 = AParam.par_xmm_m128, Bytes = 1, Bt1 = 0x57, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },
                new AOpCode { Mnemonic = "VXORPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_ymm, ParamType2 = AParam.par_ymm, ParamType3 = AParam.par_ymm_m256, Bytes = 1, Bt1 = 0x57, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f, VexExtraParam = 2 },


                new AOpCode { Mnemonic = "VZEROALL", Bytes = 1, Bt1 = 0x77, HasVex = true, VexL = 1, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },
                new AOpCode { Mnemonic = "VZEROUPPER", Bytes = 1, Bt1 = 0x77, HasVex = true, VexL = 0, VexOpCodeExtension = AVexOpCodeExtension.oe_none, VexLeadingOpCode = AVexLeadingOpCode.lo_0f },

                new AOpCode { Mnemonic = "WAIT", Bytes = 1, Bt1 = 0x9b },
                new AOpCode { Mnemonic = "WBINVD", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x09 },
                new AOpCode { Mnemonic = "WRFSBASE", OpCode1 = AExtraOpCode.eo_reg2, ParamType1 = AParam.par_r32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0xae },
                new AOpCode { Mnemonic = "WRGSBASE", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_r32, Bytes = 3, Bt1 = 0xf3, Bt2 = 0x0f, Bt3 = 0xae },
                new AOpCode { Mnemonic = "WRMSR", Bytes = 2, Bt1 = 0x0f, Bt2 = 0x30 },

                new AOpCode { Mnemonic = "XABORT", OpCode1 = AExtraOpCode.eo_ib, ParamType1 = AParam.par_imm8, Bytes = 2, Bt1 = 0xc6, Bt2 = 0xf8 },

                new AOpCode { Mnemonic = "XADD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm8, ParamType2 = AParam.par_r8, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc0 },
                new AOpCode { Mnemonic = "XADD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm16, ParamType2 = AParam.par_r16, Bytes = 3, Bt1 = 0x66, Bt2 = 0x0f, Bt3 = 0xc1 },
                new AOpCode { Mnemonic = "XADD", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_rm32, ParamType2 = AParam.par_r32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc1 },

                new AOpCode { Mnemonic = "XBEGIN", OpCode1 = AExtraOpCode.eo_cd, ParamType1 = AParam.par_rel32, Bytes = 1, Bt1 = 0xe8 },


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

                new AOpCode { Mnemonic = "XEND", Bytes = 3, Bt1 = 0x0f, Bt2 = 0x01, Bt3 = 0xd5 },
                new AOpCode { Mnemonic = "XGETBV", Bytes = 3, Bt1 = 0x0f, Bt2 = 0x01, Bt3 = 0xd0 },
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
                new AOpCode { Mnemonic = "XORPS", OpCode1 = AExtraOpCode.eo_reg, ParamType1 = AParam.par_xmm, ParamType2 = AParam.par_xmm_m128, Bytes = 2, Bt1 = 0x0f, Bt2 = 0x57 },




                new AOpCode { Mnemonic = "XRSTOR", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xae, W0 = true },
                new AOpCode { Mnemonic = "XRSTOR64", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt3 = 0xae, W1 = true },
                new AOpCode { Mnemonic = "XRSTORS", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc7, W0 = true },
                new AOpCode { Mnemonic = "XRSTORS64", OpCode1 = AExtraOpCode.eo_reg3, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt3 = 0xc7, W1 = true },

                new AOpCode { Mnemonic = "XSAVE", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xae, W0 = true },
                new AOpCode { Mnemonic = "XSAVE64", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt3 = 0xae, W1 = true },

                new AOpCode { Mnemonic = "XSAVEC", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc7, W0 = true },
                new AOpCode { Mnemonic = "XSAVEC64", OpCode1 = AExtraOpCode.eo_reg4, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt3 = 0xc7, W1 = true },

                new AOpCode { Mnemonic = "XSAVEOPT", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xae, W0 = true },
                new AOpCode { Mnemonic = "XSAVEOPT64", OpCode1 = AExtraOpCode.eo_reg6, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt3 = 0xae, W1 = true },

                new AOpCode { Mnemonic = "XSAVES", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt2 = 0xc7, W0 = true },
                new AOpCode { Mnemonic = "XSAVES64", OpCode1 = AExtraOpCode.eo_reg5, ParamType1 = AParam.par_m32, Bytes = 2, Bt1 = 0x0f, Bt3 = 0xc7, W1 = true },

                new AOpCode { Mnemonic = "XSETBV", Bytes = 3, Bt1 = 0x0f, Bt2 = 0x01, Bt3 = 0xd1 },
                new AOpCode { Mnemonic = "XTEST", Bytes = 3, Bt1 = 0x0f, Bt2 = 0x01, Bt3 = 0xd6 },
            };
        }
        #endregion
    }
}
