using System;
using Sputnik.LBinary;
using SputnikAsm.LDisassembler.LEnums;
using SputnikAsm.LUtils;

namespace SputnikAsm.LDisassembler
{
    public static class ADisassemblerCases2
    {
        #region Process
        public static Boolean Process(
            ADisassembler d,
            UBytePtr memory,
            ref UIntPtr offset,
            ref int prefixSize,
            ref UInt32 last,
            ref String description)
        {
            switch (memory[0])
            {
                case 0xf:
                    {
                        // simd extensions
                        switch (memory[1])
                        {
                            case 0x3a:
                                {
                                    switch (memory[2])
                                    {
                                        case 0:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Qwords element permutation";
                                                        d.LastDisassembleData.OpCode = "vpermq";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x1:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Permute double-precision floating-point elements";
                                                        d.LastDisassembleData.OpCode = "vpermpd";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x2:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Blend packed dwords";
                                                        d.LastDisassembleData.OpCode = "vblenddd";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x4:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Permute single-prevision floating-point values";
                                                        d.LastDisassembleData.OpCode = "vpermilps";
                                                        d.OpCodeFlags.SkipExtraReg = true;
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x5:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Permute double-prevision floating-point values";
                                                        d.LastDisassembleData.OpCode = "vpermilpd";
                                                        d.OpCodeFlags.SkipExtraReg = true;
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0x6:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Permute floating-point values";
                                                        d.LastDisassembleData.OpCode = "vperm2f128";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0x8:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Round scalar single precision floating-point values";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vroundps";
                                                    else
                                                        d.LastDisassembleData.OpCode = "roundps";
                                                    d.OpCodeFlags.SkipExtraReg = true;
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0x9:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Round packed double precision floating-point values";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vroundpd";
                                                    else
                                                        d.LastDisassembleData.OpCode = "roundpd";
                                                    d.OpCodeFlags.SkipExtraReg = true;
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0xa:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Round scalar single precision floating-point values";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vroundss";
                                                    else
                                                        d.LastDisassembleData.OpCode = "roundss";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0xb:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Round packed single precision floating-point values";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vroundsd";
                                                    else
                                                        d.LastDisassembleData.OpCode = "roundsd";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0xc:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Blend packed single precision floating-point values";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vblendps";
                                                    else
                                                        d.LastDisassembleData.OpCode = "blendps";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0xd:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Blend packed double precision floating-point values";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vblendpd";
                                                    else
                                                        d.LastDisassembleData.OpCode = "blendpd";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0xe:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Blend packed words";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpblendw";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pblendw";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0xf:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed align right";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpalignr";
                                                    else
                                                        d.LastDisassembleData.OpCode = "palignr";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    d.LastDisassembleData.OpCode = "palignr";
                                                    d.LastDisassembleData.Parameters = d.Mm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 3, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0x14:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Extract byte";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpextrb";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pextrb";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 3, 2, ref last, ATmrPos.Left) + d.Xmm(memory[3]) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0x15:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Extract word";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpextrw";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pextrw";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 3, 1, ref last, ATmrPos.Left) + d.Xmm(memory[3]) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0x16:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.RexW)
                                                    {
                                                        description = "Extract qword";
                                                        d.LastDisassembleData.OpCode = "pextrq";
                                                    }
                                                    else
                                                    {
                                                        description = "Extract dword";
                                                        d.LastDisassembleData.OpCode = "pextrd";
                                                    }
                                                    if (d.HasVex) d.LastDisassembleData.OpCode = 'v' + d.LastDisassembleData.OpCode;
                                                    d.OpCodeFlags.SkipExtraReg = true;
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 3, 2, ref last, ATmrPos.Left) + d.Xmm(memory[3]) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0x17:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Extract packed single precision floating-point value";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vextractps";
                                                    else
                                                        d.LastDisassembleData.OpCode = "extractps";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Left) + d.Xmm(memory[3]) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0x18:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Insert packed floating-point values";
                                                        d.LastDisassembleData.OpCode = "vinsertf128";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x19:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Extract packed floating-point values";
                                                        d.LastDisassembleData.OpCode = "vextractf128";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0x1d:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Convert single-precision FP value to 16-bit FP value";
                                                        d.LastDisassembleData.OpCode = "vcvtps2ph";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0x20:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Insert Byte";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpinsrb";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pinsrb";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0x21:
                                            {    //C4 E3 79 21 80 B8 00 00 00 20
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Insert Scalar Single-Precision Floating-Point Value";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vinsertps";
                                                    else
                                                        d.LastDisassembleData.OpCode = "insertps";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0x22:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.RexW)
                                                    {
                                                        description = "Insert qword";
                                                        d.LastDisassembleData.OpCode = "pinsrq";
                                                    }
                                                    else
                                                    {
                                                        description = "Insert dword";
                                                        d.LastDisassembleData.OpCode = "pinsrd";
                                                    }
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = 'v' + d.LastDisassembleData.OpCode;
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0x38:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Insert packed integer values";
                                                        d.LastDisassembleData.OpCode = "vinserti128";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x39:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Extract packed integer values";
                                                        d.LastDisassembleData.OpCode = "vextracti128";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x40:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Dot product of packed single precision floating-point values";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vdpps";
                                                    else
                                                        d.LastDisassembleData.OpCode = "dpps";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0x41:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Dot product of packed double precision floating-point values";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vdppd";
                                                    else
                                                        d.LastDisassembleData.OpCode = "dppd";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0x42:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Compute multiple packed sums of absolute difference";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vmpsadbw";
                                                    else
                                                        d.LastDisassembleData.OpCode = "mpsadbw";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0x44:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Carry-less multiplication quadword";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpclmulqdq";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pclmulqdq";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0x46:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Permute integer values";
                                                        d.LastDisassembleData.OpCode = "vperm2i128";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0x4a:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Variable Blend Packed Single Precision Floating-Point Values";
                                                        d.LastDisassembleData.OpCode = "vblendvps";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        if (d.OpCodeFlags.L)
                                                            d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + d.ColorReg + d.RegNrToStr(ARegisterType.RtYmm, ((int)memory[(int)last] >> 4) & 0xf) + d.EndColor;
                                                        else
                                                            d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + d.ColorReg + d.RegNrToStr(ARegisterType.RtXmm, ((int)memory[(int)last] >> 4) & 0xf) + d.EndColor;
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0x4b:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Variable Blend Packed Double Precision Floating-Point Values";
                                                        d.LastDisassembleData.OpCode = "vblendvpd";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        if (d.OpCodeFlags.L)
                                                            d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + d.ColorReg + d.RegNrToStr(ARegisterType.RtYmm, ((int)memory[(int)last] >> 4) & 0xf) + d.EndColor;
                                                        else
                                                            d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + d.ColorReg + d.RegNrToStr(ARegisterType.RtXmm, ((int)memory[(int)last] >> 4) & 0xf) + d.EndColor;
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0x60:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed compare explicit length string, return mask";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpcmpestrm";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pcmpestrm";
                                                    d.OpCodeFlags.SkipExtraReg = true;
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0x61:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed compare explicit length string, return index";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpcmpestri";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pcmpestri";
                                                    d.OpCodeFlags.SkipExtraReg = true;
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0x62:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed compare implicit length string, return mask";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpcmpistrm";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pcmpistrm";
                                                    d.OpCodeFlags.SkipExtraReg = true;
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*3a*/
                                        case 0x63:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed compare implicit length string, return index";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpcmpistri";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pcmpistri";
                                                    d.OpCodeFlags.SkipExtraReg = true;
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0xdf:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "AES round key generation assist";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vaeskeygenassist";
                                                    else
                                                        d.LastDisassembleData.OpCode = "aeskeygenassist";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    d.LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0xf0:
                                            {
                                                if (d.Prefix2.Contains(0xf2))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Rotate right logical without affecting flags";
                                                        d.LastDisassembleData.OpCode = "rorx";
                                                        d.OpCodeFlags.SkipExtraReg = true;
                                                        d.LastDisassembleData.Parameters = d.R32(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Right) + ',';
                                                        d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        default:
                                            {
                                                if (d.HasVex)
                                                {
                                                    d.LastDisassembleData.OpCode = "unknown avx 0F3A " + AStringUtils.IntToHex(memory[2], 2);
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 0x40:
                                {
                                    description = "move if overflow";
                                    d.LastDisassembleData.OpCode = "cmovo";
                                    if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x41:
                                {
                                    description = "move if not overflow";
                                    d.LastDisassembleData.OpCode = "cmovno";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x42:
                                {
                                    description = "move if below/ move if carry";
                                    d.LastDisassembleData.OpCode = "cmovb";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x43:
                                {
                                    description = "move if above or equal/ move if not carry";
                                    d.LastDisassembleData.OpCode = "cmovae";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x44:
                                {
                                    description = "move if equal/move if zero";
                                    d.LastDisassembleData.OpCode = "cmove";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x45:
                                {
                                    description = "move if not equal/move if not zero";
                                    d.LastDisassembleData.OpCode = "cmovne";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x46:
                                {
                                    description = "move if below or equal";
                                    d.LastDisassembleData.OpCode = "cmovbe";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x47:
                                {
                                    description = "move if above";
                                    d.LastDisassembleData.OpCode = "cmova";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x48:
                                {
                                    description = "move if sign";
                                    d.LastDisassembleData.OpCode = "cmovs";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x49:
                                {
                                    description = "move if not sign";
                                    d.LastDisassembleData.OpCode = "cmovns";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x4a:
                                {
                                    description = "move if parity even";
                                    d.LastDisassembleData.OpCode = "cmovpe";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x4b:
                                {
                                    description = "move if not parity/move if parity odd";
                                    d.LastDisassembleData.OpCode = "cmovnp";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x4c:
                                {
                                    description = "move if less";
                                    d.LastDisassembleData.OpCode = "cmovl";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x4d:
                                {
                                    description = "move if greater or equal";
                                    d.LastDisassembleData.OpCode = "cmovge";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x4e:
                                {
                                    description = "move if less or equal";
                                    d.LastDisassembleData.OpCode = "cmovle";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x4f:
                                {
                                    description = "move if greater";
                                    d.LastDisassembleData.OpCode = "cmovg";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x50:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovmskpd";
                                        else
                                            d.LastDisassembleData.OpCode = "movmskpd";
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "extract packed double-precision floating-point sign mask";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovmskps";
                                        else
                                            d.LastDisassembleData.OpCode = "movmskps";
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        description = "move mask to integer";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x51:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vsqrtsd";
                                        else
                                            d.LastDisassembleData.OpCode = "sqrtsd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "scalar double-fp square root";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vsqrtss";
                                        else
                                            d.LastDisassembleData.OpCode = "sqrtss";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "scalar single-fp square root";
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vsqrtpd";
                                        else
                                            d.LastDisassembleData.OpCode = "sqrtpd";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "packed double-fp square root";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vsqrtps";
                                        else
                                            d.LastDisassembleData.OpCode = "sqrtps";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "packed single-fp square root";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x52:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vrsqrtss";
                                        else
                                            d.LastDisassembleData.OpCode = "rsqrtss";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "packed single-fp square root reciprocal";
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vrsqrtps";
                                        else
                                            d.LastDisassembleData.OpCode = "rsqrtps";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "scalar single-fp square root reciprocal";
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x53:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vrcpss";
                                        else
                                            d.LastDisassembleData.OpCode = "rcpss";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "Compute Reciprocal of Scalar Single-Precision Floating-Point Values";
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vrcpps";
                                        else
                                            d.LastDisassembleData.OpCode = "rcpps";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "Compute Reciprocals of Packed Single-Precision Floating-Point Values";
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x54:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vandpd";
                                        else
                                            d.LastDisassembleData.OpCode = "andpd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "bit-wise logical and of xmm2/m128 and xmm1";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vandps";
                                        else
                                            d.LastDisassembleData.OpCode = "andps";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        description = "bit-wise logical and for single fp";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x55:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "bit-wise logical and not of packed double-precision fp values";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vandnpd";
                                        else
                                            d.LastDisassembleData.OpCode = "andnpd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "bit-wise logical and not for single-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vandnps";
                                        else
                                            d.LastDisassembleData.OpCode = "andnps";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x56:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "bit-wise logical or of double-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vorpd";
                                        else
                                            d.LastDisassembleData.OpCode = "orpd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "bit-wise logical or for single-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vorps";
                                        else
                                            d.LastDisassembleData.OpCode = "orps";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x57:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "bit-wise logical xor for double-fp data";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vxorpd";
                                        else
                                            d.LastDisassembleData.OpCode = "xorpd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "bit-wise logical xor for single-fp data";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vxorps";
                                        else
                                            d.LastDisassembleData.OpCode = "xorps";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x58:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        //delete the repne from the tempResult
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vaddsd";
                                        else
                                            d.LastDisassembleData.OpCode = "addsd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "add the lower sp fp number from xmm2/mem to xmm1.";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        //delete the repe from the tempResult
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vaddss";
                                        else
                                            d.LastDisassembleData.OpCode = "addss";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        description = "add the lower sp fp number from xmm2/mem to xmm1.";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.Prefix2.Contains(0x66))
                                        {
                                            if (d.HasVex)
                                                d.LastDisassembleData.OpCode = "vaddpd";
                                            else
                                                d.LastDisassembleData.OpCode = "addpd";
                                            d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                            description = "add packed double-precision floating-point values from xmm2/mem to xmm1";
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                        else
                                        {
                                            if (d.HasVex)
                                                d.LastDisassembleData.OpCode = "vaddps";
                                            else
                                                d.LastDisassembleData.OpCode = "addps";
                                            d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                            d.LastDisassembleData.DataSize = 4;
                                            description = "add packed sp fp numbers from xmm2/mem to xmm1";
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                }
                                break;
                            case 0x59:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmulsd";
                                        else
                                            d.LastDisassembleData.OpCode = "mulsd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "scalar double-fp multiply";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmulss";
                                        else
                                            d.LastDisassembleData.OpCode = "mulss";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        description = "scalar single-fp multiply";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmulpd";
                                        else
                                            d.LastDisassembleData.OpCode = "mulpd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "packed double-fp multiply";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmulps";
                                        else
                                            d.LastDisassembleData.OpCode = "mulps";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        description = "packed single-fp multiply";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x5a:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vcvtsd2ss";
                                        else
                                            d.LastDisassembleData.OpCode = "cvtsd2ss";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "convert scalar double-precision floating-point value to scalar single-precision floating-point value";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vcvtss2sd";
                                        else
                                            d.LastDisassembleData.OpCode = "cvtss2sd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        description = "convert scalar single-precision floating-point value to scalar double-precision floating-point value";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.Prefix2.Contains(0x66))
                                        {
                                            if (d.HasVex)
                                                d.LastDisassembleData.OpCode = "vcvtpd2ps";
                                            else
                                                d.LastDisassembleData.OpCode = "cvtpd2ps";
                                            d.OpCodeFlags.SkipExtraReg = true;
                                            d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                            description = "convert packed double precision fp values to packed single precision fp values";
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                        else
                                        {
                                            if (d.HasVex)
                                                d.LastDisassembleData.OpCode = "vcvtps2pd";
                                            else
                                                d.LastDisassembleData.OpCode = "cvtps2pd";
                                            d.OpCodeFlags.SkipExtraReg = true;
                                            d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                            d.LastDisassembleData.DataSize = 4;
                                            description = "convert packed single precision fp values to packed double precision fp values";
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                }
                                break;
                            case 0x5b:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.IsFloat = true;
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vcvtps2dq";
                                        else
                                            d.LastDisassembleData.OpCode = "cvtps2dq";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        description = "convert ps-precision fpoint values to packed dword's ";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vcvttps2dq";
                                        else
                                            d.LastDisassembleData.OpCode = "cvttps2dq";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "Convert with Truncation Packed Single-Precision FP Values to Packed Dword Integers";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vcvtdq2ps";
                                        else
                                            d.LastDisassembleData.OpCode = "cvtdq2ps";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "convert packed dword's to ps-precision fpoint values";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x5c:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vsubsd";
                                        else
                                            d.LastDisassembleData.OpCode = "subsd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "scalar double-fp subtract";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vsubss";
                                        else
                                            d.LastDisassembleData.OpCode = "subss";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        description = "scalar single-fp subtract";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vsubpd";
                                        else
                                            d.LastDisassembleData.OpCode = "subpd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "packed double-fp subtract";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vsubps";
                                        else
                                            d.LastDisassembleData.OpCode = "subps";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4; //4*4 actually
                                        description = "packed single-fp subtract";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x5d:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vminsd";
                                        else
                                            d.LastDisassembleData.OpCode = "minsd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "scalar single-fp minimum";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vminss";
                                        else
                                            d.LastDisassembleData.OpCode = "minss";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        description = "scalar single-fp minimum";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.Prefix2.Contains(0x66))
                                        {
                                            if (d.HasVex)
                                                d.LastDisassembleData.OpCode = "vminpd";
                                            else
                                                d.LastDisassembleData.OpCode = "minpd";
                                            d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                            description = "packed double-fp minimum";
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                        else
                                        {
                                            if (d.HasVex)
                                                d.LastDisassembleData.OpCode = "vminps";
                                            else
                                                d.LastDisassembleData.OpCode = "minps";
                                            d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                            description = "packed single-fp minimum";
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                }
                                break;
                            case 0x5e:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "divsd";
                                        else
                                            d.LastDisassembleData.OpCode = "divsd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "scalar double-precision-fp divide";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vdivss";
                                        else
                                            d.LastDisassembleData.OpCode = "divss";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        description = "scalar single-fp divide";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.Prefix2.Contains(0x66))
                                        {
                                            if (d.HasVex)
                                                d.LastDisassembleData.OpCode = "vdivpd";
                                            else
                                                d.LastDisassembleData.OpCode = "divpd";
                                            d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                            description = "packed double-precision fp divide";
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                        else
                                        {
                                            if (d.HasVex)
                                                d.LastDisassembleData.OpCode = "vdivps";
                                            else
                                                d.LastDisassembleData.OpCode = "divps";
                                            d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                            d.LastDisassembleData.DataSize = 4;
                                            description = "packed single-fp divide";
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                }
                                break;
                            case 0x5f:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        description = "scalar double-fp maximum";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmaxsd";
                                        else
                                            d.LastDisassembleData.OpCode = "maxsd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        description = "scalar single-fp maximum";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmaxss";
                                        else
                                            d.LastDisassembleData.OpCode = "maxss";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.Prefix2.Contains(0x66))
                                        {
                                            description = "packed double-fp maximum";
                                            if (d.HasVex)
                                                d.LastDisassembleData.OpCode = "vmaxpd";
                                            else
                                                d.LastDisassembleData.OpCode = "maxpd";
                                            d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                        else
                                        {
                                            description = "packed single-fp maximum";
                                            if (d.HasVex)
                                                d.LastDisassembleData.OpCode = "vmaxps";
                                            else
                                                d.LastDisassembleData.OpCode = "maxps";
                                            d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                            d.LastDisassembleData.DataSize = 4;
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                }
                                break;
                            case 0x60:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "unpack low packed data";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpunpcklbw";
                                        else
                                            d.LastDisassembleData.OpCode = "punpcklbw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unpack low packed data";
                                        d.LastDisassembleData.OpCode = "punpcklbw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x61:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "unpack low packed data";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "punpcklwd";
                                        else
                                            d.LastDisassembleData.OpCode = "punpcklwd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unpack low packed data";
                                        d.LastDisassembleData.OpCode = "punpcklwd";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x62:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "unpack low packed data";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpunpckldq";
                                        else
                                            d.LastDisassembleData.OpCode = "punpckldq";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unpack low packed data";
                                        d.LastDisassembleData.OpCode = "punpckldq";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x63:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "pack with signed saturation";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "packsswb";
                                        else
                                            d.LastDisassembleData.OpCode = "vpacksswb";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "pack with signed saturation";
                                        d.LastDisassembleData.OpCode = "packsswb";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x64:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed compare for greater than";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpcmpgtb";
                                        else
                                            d.LastDisassembleData.OpCode = "pcmpgtb";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed compare for greater than";
                                        d.LastDisassembleData.OpCode = "pcmpgtb";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x65:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed compare for greater than";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpcmpgtw";
                                        else
                                            d.LastDisassembleData.OpCode = "pcmpgtw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed compare for greater than";
                                        d.LastDisassembleData.OpCode = "pcmpgtw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x66:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed compare for greater than";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpcmpgtd";
                                        else
                                            d.LastDisassembleData.OpCode = "pcmpgtd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed compare for greater than";
                                        d.LastDisassembleData.OpCode = "pcmpgtd";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x67:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "pack with unsigned saturation";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpackuswb";
                                        else
                                            d.LastDisassembleData.OpCode = "packuswb";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "pack with unsigned saturation";
                                        d.LastDisassembleData.OpCode = "packuswb";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x68:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "unpack high packed data";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpunpckhbw";
                                        else
                                            d.LastDisassembleData.OpCode = "punpckhbw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unpack high packed data";
                                        d.LastDisassembleData.OpCode = "punpckhbw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x69:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "unpack high packed data";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpunpckhwd";
                                        else
                                            d.LastDisassembleData.OpCode = "punpckhwd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unpack high packed data";
                                        d.LastDisassembleData.OpCode = "punpckhwd";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x6a:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "unpack high packed data";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpunpckhdq";
                                        else
                                            d.LastDisassembleData.OpCode = "punpckhdq";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unpack high packed data";
                                        d.LastDisassembleData.OpCode = "punpckhdq";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x6b:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "pack with signed saturation";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "packssdw";
                                        else
                                            d.LastDisassembleData.OpCode = "packssdw";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "pack with signed saturation";
                                        d.LastDisassembleData.OpCode = "packssdw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x6c:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "unpack low packed data";
                                        d.LastDisassembleData.OpCode = "punpcklqdq";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x6d:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "unpack high packed data";
                                        d.LastDisassembleData.OpCode = "punpckhqdq";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x6e:
                                {
                                    //d.LastDisassembleData.isfloat:=true; //not sure
                                    if (d.RexW)
                                    {
                                        description = "move quadword";
                                        d.LastDisassembleData.OpCode = "movq";
                                    }
                                    else
                                    {
                                        description = "move doubleword";
                                        d.LastDisassembleData.OpCode = "movd";
                                    }
                                    if (d.HasVex)
                                        d.LastDisassembleData.OpCode = 'v' + d.LastDisassembleData.OpCode;
                                    d.OpCodeFlags.SkipExtraReg = true;
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x6f:
                                {
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        description = "move unaligned double quadword";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovdqu";
                                        else
                                            d.LastDisassembleData.OpCode = "movdqu";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "move aligned double quadword";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovdqa";
                                        else
                                            d.LastDisassembleData.OpCode = "movdqa";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "move 64 bits";
                                        d.LastDisassembleData.OpCode = "movq";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x70:
                                {
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        description = "shuffle packed low words";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpshuflw";
                                        else
                                            d.LastDisassembleData.OpCode = "pshuflw";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 2);
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        description = "shuffle packed high words";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpshufhw";
                                        else
                                            d.LastDisassembleData.OpCode = "pshufhw";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 2);
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed shuffle doubleword";
                                        d.LastDisassembleData.OpCode = "pshufd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 2);
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        description = "packed shuffle word";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpshufw";
                                        else
                                            d.LastDisassembleData.OpCode = "pshufw";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 2);
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;
                            case 0x71:
                                {
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[3];
                                    d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 3;
                                    d.LastDisassembleData.SeparatorCount += 1;
                                    switch (d.GetReg(memory[2]))
                                    {
                                        case 2:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "packed shift right logical";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpsrlw";
                                                    else
                                                        d.LastDisassembleData.OpCode = "psrlw";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + ',' + d.IntToHexSigned((UIntPtr)memory[3], 2);
                                                    offset += 3;
                                                }
                                                else
                                                {
                                                    description = "packed shift right logical";
                                                    d.LastDisassembleData.OpCode = "psrlw";
                                                    d.LastDisassembleData.Parameters = d.Mm(memory[2]) + ',' + d.IntToHexSigned((UIntPtr)memory[3], 2);
                                                    offset += 3;
                                                }
                                            }
                                            break;
                                        case 4:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "shift packed data right arithmetic";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpsraw";
                                                    else
                                                        d.LastDisassembleData.OpCode = "psraw";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last);
                                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 1);
                                                }
                                                else
                                                {
                                                    description = "packed shift left logical";
                                                    d.LastDisassembleData.OpCode = "psraw";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 3, ref last);
                                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 1);
                                                }
                                            }
                                            break;
                                        case 6:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "packed shift left logical";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpsllw";
                                                    else
                                                        d.LastDisassembleData.OpCode = "psllw";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last);
                                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 1);
                                                }
                                                else
                                                {
                                                    description = "packed shift left logical";
                                                    d.LastDisassembleData.OpCode = "psllw";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 3, ref last);
                                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 1);
                                                }
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 0x72:
                                {
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[3];
                                    d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 3;
                                    d.LastDisassembleData.SeparatorCount += 1;
                                    switch (d.GetReg(memory[2]))
                                    {
                                        case 2:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "packed shift right logical";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpsrld";
                                                    else
                                                        d.LastDisassembleData.OpCode = "psrld";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last);
                                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                                else
                                                {
                                                    description = "packed shift right logical";
                                                    d.LastDisassembleData.OpCode = "psrld";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 3, ref last);
                                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                            }
                                            break;
                                        case 4:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "packed shift right arithmetic";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpsrad";
                                                    else
                                                        d.LastDisassembleData.OpCode = "psrad";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last);
                                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                                else
                                                {
                                                    description = "packed shift right arithmetic";
                                                    d.LastDisassembleData.OpCode = "psrad";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 3, ref last);
                                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                            }
                                            break;
                                        case 6:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "packed shift left logical";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "pslld";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pslld";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last);
                                                    d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                                else
                                                {
                                                    description = "packed shift left logical";
                                                    d.LastDisassembleData.OpCode = "pslld";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 3, ref last);
                                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                            }
                                            break;
                                    }
                                }
                                break;
                        }
                        break;
                    }
                default:
                    return false;
            }
            return true;
        }
        #endregion
    }
}
