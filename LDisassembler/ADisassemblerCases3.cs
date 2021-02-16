using System;
using Sputnik.LBinary;
using SputnikAsm.LDisassembler.LEnums;
using SputnikAsm.LUtils;

namespace SputnikAsm.LDisassembler
{
    public static class ADisassemblerCases3
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
                            case 0x73:
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
                                                        d.LastDisassembleData.OpCode = "vpsrlq";
                                                    else
                                                        d.LastDisassembleData.OpCode = "psrlq";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                                    d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                                else
                                                {
                                                    description = "packed shift right logical";
                                                    d.LastDisassembleData.OpCode = "psrlq";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                                    d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                                d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters.Substring(1);
                                            }
                                            break;
                                        case 3:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "shift double quadword right logical";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpsrldq";
                                                    else
                                                        d.LastDisassembleData.OpCode = "psrldq";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                                    d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                                d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters.Substring(1);
                                            }
                                            break;
                                        case 6:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "packed shift left logical";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpsllq";
                                                    else
                                                        d.LastDisassembleData.OpCode = "psllq";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                                    d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                                else
                                                {
                                                    description = "packed shift left logical";
                                                    d.LastDisassembleData.OpCode = "psllq";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                                    d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                                d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters.Substring(1);
                                            }
                                            break;
                                        case 7:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "shift double quadword left logical";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpslldq";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pslldq";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                                    d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters.Substring(1);
                                                    offset += 3;
                                                }
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 0x74:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed compare for equal";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpcmpeqb";
                                        else
                                            d.LastDisassembleData.OpCode = "pcmpeqb";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed compare for equal";
                                        d.LastDisassembleData.OpCode = "pcmpeqb";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x75:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed compare for equal";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpcmpeqw";
                                        else
                                            d.LastDisassembleData.OpCode = "pcmpeqw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed compare for equal";
                                        d.LastDisassembleData.OpCode = "pcmpeqw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x76:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed compare for equal";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpcmpeqd";
                                        else
                                            d.LastDisassembleData.OpCode = "pcmpeqd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed compare for equal";
                                        d.LastDisassembleData.OpCode = "pcmpeqd";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x77:
                                {
                                    if (d.HasVex)
                                    {
                                        if (d.OpCodeFlags.L)
                                        {
                                            description = "Zero all YMM registers";
                                            d.LastDisassembleData.OpCode = "vzeroall";
                                            offset += 1;
                                        }
                                        else
                                        {
                                            description = "Zero upper bits of YMM registers";
                                            d.LastDisassembleData.OpCode = "vzeroupper";
                                            offset += 1;
                                        }
                                    }
                                    else
                                    {
                                        description = "empty mmx™ state";
                                        d.LastDisassembleData.OpCode = "emms";
                                        offset += 1;
                                    }
                                }
                                break;
                            case 0x78:
                                {
                                    description = "reads a specified vmcs field (32 bits)";
                                    d.LastDisassembleData.OpCode = "vmread";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last) + d.R32(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x79:
                                {
                                    description = "writes a specified vmcs field (32 bits)";
                                    d.LastDisassembleData.OpCode = "vmwrite";
                                    d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x7c:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vhaddpd";
                                        else
                                            d.LastDisassembleData.OpCode = "haddpd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "packed double-fp horizontal add";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vhaddps";
                                        else
                                            d.LastDisassembleData.OpCode = "haddps";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "packed single-fp horizontal add";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x7d:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vhsubpd";
                                        else
                                            d.LastDisassembleData.OpCode = "hsubpd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "packed double-fp horizontal subtract";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vhsubps";
                                        else
                                            d.LastDisassembleData.OpCode = "hsubps";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "packed single-fp horizontal subtract";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x7e:
                                {
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        description = "move quadword";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovq";
                                        else
                                            d.LastDisassembleData.OpCode = "movq";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        if (d.RexW)
                                        {
                                            description = "move 64 bits";
                                            d.LastDisassembleData.OpCode = "movq";
                                        }
                                        else
                                        {
                                            description = "move 32 bits";
                                            d.LastDisassembleData.OpCode = "movd";
                                        }
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = 'v' + d.LastDisassembleData.OpCode;
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last) + d.Xmm(memory[2]);  //r32/rm32,xmm
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.RexW)
                                        {
                                            description = "move 64 bits";
                                            d.LastDisassembleData.OpCode = "movq";
                                        }
                                        else
                                        {
                                            description = "move 32 bits";
                                            d.LastDisassembleData.OpCode = "movd";
                                        }
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last) + d.Mm(memory[2]); //r32/rm32,mm
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x7f:
                                {
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        description = "move unaligned double quadword";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovdqu";
                                        else
                                            d.LastDisassembleData.OpCode = "movdqu";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last) + d.Xmm(memory[2]);
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
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last) + d.Xmm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "move 64 bits";
                                        d.LastDisassembleData.OpCode = "movq";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 3, ref last) + d.Mm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x80:
                                {
                                    description = "jump near if overflow (OF=1)";
                                    d.LastDisassembleData.OpCode = "jo";
                                    d.LastDisassembleData.IsJump = true;
                                    d.LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_of) != 0;
                                    offset += 1 + 4;
                                    if (d.MarkIpRelativeInstructions)
                                    {
                                        d.LastDisassembleData.RipRelative = 2;
                                        d.RipRelative = true;
                                    }
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (d.Is64Bit)
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 2;
                                    d.LastDisassembleData.SeparatorCount += 1;
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                                }
                                break;
                            case 0x81:
                                {
                                    description = "jump near if not overflow (OF=0)";
                                    d.LastDisassembleData.OpCode = "jno";
                                    d.LastDisassembleData.IsJump = true;
                                    d.LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_of) == 0;
                                    offset += 1 + 4;
                                    if (d.MarkIpRelativeInstructions)
                                    {
                                        d.LastDisassembleData.RipRelative = 2;
                                        d.RipRelative = true;
                                    }
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (d.Is64Bit)
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 2;
                                    d.LastDisassembleData.SeparatorCount += 1;
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                                }
                                break;
                            case 0x82:
                                {
                                    description = "jump near if below/carry (CF=1)";
                                    d.LastDisassembleData.OpCode = "jb";
                                    d.LastDisassembleData.IsJump = true;
                                    d.LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_cf) != 0;
                                    offset += 1 + 4;
                                    if (d.MarkIpRelativeInstructions)
                                    {
                                        d.LastDisassembleData.RipRelative = 2;
                                        d.RipRelative = true;
                                    }
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (d.Is64Bit)
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 2;
                                    d.LastDisassembleData.SeparatorCount += 1;
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                                }
                                break;
                            case 0x83:
                                {
                                    description = "jump near if above or equal (CF=0)";
                                    d.LastDisassembleData.OpCode = "jae";
                                    d.LastDisassembleData.IsJump = true;
                                    d.LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_cf) == 0;
                                    offset += 1 + 4;
                                    if (d.MarkIpRelativeInstructions)
                                    {
                                        d.LastDisassembleData.RipRelative = 2;
                                        d.RipRelative = true;
                                    }
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (d.Is64Bit)
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 2;
                                    d.LastDisassembleData.SeparatorCount += 1;
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                                }
                                break;
                            case 0x84:
                                {
                                    description = "jump near if equal (ZF=1)";
                                    d.LastDisassembleData.OpCode = "je";
                                    d.LastDisassembleData.IsJump = true;
                                    d.LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_zf) != 0;
                                    offset += 1 + 4;
                                    if (d.MarkIpRelativeInstructions)
                                    {
                                        d.LastDisassembleData.RipRelative = 2;
                                        d.RipRelative = true;
                                    }
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (d.Is64Bit)
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 2;
                                    d.LastDisassembleData.SeparatorCount += 1;
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                                }
                                break;
                            case 0x85:
                                {
                                    description = "jump near if not equal (ZF=0)";
                                    d.LastDisassembleData.OpCode = "jne";
                                    d.LastDisassembleData.IsJump = true;
                                    d.LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_zf) == 0;
                                    offset += 1 + 4;
                                    if (d.MarkIpRelativeInstructions)
                                    {
                                        d.LastDisassembleData.RipRelative = 2;
                                        d.RipRelative = true;
                                    }
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (d.Is64Bit)
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 2;
                                    d.LastDisassembleData.SeparatorCount += 1;
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                                }
                                break;
                            case 0x86:
                                {
                                    description = "jump near if below or equal (CF=1 or ZF=1)";
                                    d.LastDisassembleData.OpCode = "jbe";
                                    d.LastDisassembleData.IsJump = true;
                                    d.LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & (eflags_cf | eflags_zf)) != 0;
                                    offset += 1 + 4;
                                    if (d.MarkIpRelativeInstructions)
                                    {
                                        d.LastDisassembleData.RipRelative = 2;
                                        d.RipRelative = true;
                                    }
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (d.Is64Bit)
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 2;
                                    d.LastDisassembleData.SeparatorCount += 1;
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                                }
                                break;
                            case 0x87:
                                {
                                    description = "jump near if above (CF=0 and ZF=0)";
                                    d.LastDisassembleData.OpCode = "ja";
                                    d.LastDisassembleData.IsJump = true;
                                    d.LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & (eflags_cf | eflags_zf)) == 0;
                                    offset += 1 + 4;
                                    if (d.MarkIpRelativeInstructions)
                                    {
                                        d.LastDisassembleData.RipRelative = 2;
                                        d.RipRelative = true;
                                    }
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (d.Is64Bit)
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 2;
                                    d.LastDisassembleData.SeparatorCount += 1;
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                                }
                                break;
                            case 0x88:
                                {
                                    description = "jump near if sign (SF=1)";
                                    d.LastDisassembleData.OpCode = "js";
                                    d.LastDisassembleData.IsJump = true;
                                    d.LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_sf) != 0;
                                    offset += 1 + 4;
                                    if (d.MarkIpRelativeInstructions)
                                    {
                                        d.LastDisassembleData.RipRelative = 2;
                                        d.RipRelative = true;
                                    }
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (d.Is64Bit)
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 2;
                                    d.LastDisassembleData.SeparatorCount += 1;
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                                }
                                break;
                            case 0x89:
                                {
                                    description = "jump near if not sign (SF=0)";
                                    d.LastDisassembleData.OpCode = "jns";
                                    d.LastDisassembleData.IsJump = true;
                                    d.LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_sf) == 0;
                                    offset += 1 + 4;
                                    if (d.MarkIpRelativeInstructions)
                                    {
                                        d.LastDisassembleData.RipRelative = 2;
                                        d.RipRelative = true;
                                    }
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (d.Is64Bit)
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 2;
                                    d.LastDisassembleData.SeparatorCount += 1;
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                                }
                                break;
                            case 0x8a:
                                {
                                    description = "jump near if parity (PF=1)";
                                    d.LastDisassembleData.OpCode = "jp";
                                    d.LastDisassembleData.IsJump = true;
                                    d.LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_pf) != 0;
                                    offset += 1 + 4;
                                    if (d.MarkIpRelativeInstructions)
                                    {
                                        d.LastDisassembleData.RipRelative = 2;
                                        d.RipRelative = true;
                                    }
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (d.Is64Bit)
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 2;
                                    d.LastDisassembleData.SeparatorCount += 1;
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                                }
                                break;
                            case 0x8b:
                                {
                                    description = "jump near if not parity (PF=0)";
                                    d.LastDisassembleData.OpCode = "jnp";
                                    d.LastDisassembleData.IsJump = true;
                                    d.LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_pf) == 0;
                                    offset += 1 + 4;
                                    if (d.MarkIpRelativeInstructions)
                                    {
                                        d.LastDisassembleData.RipRelative = 2;
                                        d.RipRelative = true;
                                    }
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (d.Is64Bit)
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 2;
                                    d.LastDisassembleData.SeparatorCount += 1;
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                                }
                                break;
                            case 0x8c:
                                {
                                    description = "jump near if less (SF~=OF)";
                                    d.LastDisassembleData.OpCode = "jl";
                                    d.LastDisassembleData.IsJump = true;
                                    d.LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_sf) != (context->eflags & eflags_of);
                                    offset += 1 + 4;
                                    if (d.MarkIpRelativeInstructions)
                                    {
                                        d.LastDisassembleData.RipRelative = 2;
                                        d.RipRelative = true;
                                    }
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (d.Is64Bit)
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 2;
                                    d.LastDisassembleData.SeparatorCount += 1;
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                                }
                                break;
                            case 0x8d:
                                {
                                    description = "jump near if not less (SF=OF)";
                                    d.LastDisassembleData.OpCode = "jnl";
                                    d.LastDisassembleData.IsJump = true;
                                    d.LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_sf) == (context->eflags & eflags_of);
                                    offset += 1 + 4;
                                    if (d.MarkIpRelativeInstructions)
                                    {
                                        d.LastDisassembleData.RipRelative = 2;
                                        d.RipRelative = true;
                                    }
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (d.Is64Bit)
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 2;
                                    d.LastDisassembleData.SeparatorCount += 1;
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                                }
                                break;
                            case 0x8e:
                                {
                                    description = "jump near if not greater (ZF=1 or SF~=OF)";
                                    d.LastDisassembleData.OpCode = "jng";
                                    d.LastDisassembleData.IsJump = true;
                                    d.LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //d.LastDisassembleData.willjumpaccordingtocontext = ((context->eflags & eflags_sf) != (context->eflags & eflags_of)) || ((context->eflags & eflags_zf) != 0);
                                    offset += 1 + 4;
                                    if (d.MarkIpRelativeInstructions)
                                    {
                                        d.LastDisassembleData.RipRelative = 2;
                                        d.RipRelative = true;
                                    }
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (d.Is64Bit)
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 2;
                                    d.LastDisassembleData.SeparatorCount += 1;
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                                }
                                break;
                            case 0x8f:
                                {
                                    description = "jump near if greater (ZF=0 and SF=OF)";
                                    d.LastDisassembleData.OpCode = "jg";
                                    d.LastDisassembleData.IsJump = true;
                                    d.LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //d.LastDisassembleData.willjumpaccordingtocontext = ((context->eflags & eflags_sf) == (context->eflags & eflags_of)) && ((context->eflags & eflags_zf) == 0);
                                    offset += 1 + 4;
                                    if (d.MarkIpRelativeInstructions)
                                    {
                                        d.LastDisassembleData.RipRelative = 2;
                                        d.RipRelative = true;
                                    }
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (d.Is64Bit)
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 2;
                                    d.LastDisassembleData.SeparatorCount += 1;
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                                }
                                break;
                            case 0x90:
                                {
                                    description = "set byte if overflow";
                                    d.LastDisassembleData.OpCode = "seto";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x91:
                                {
                                    description = "set byte if not overfloww";
                                    d.LastDisassembleData.OpCode = "setno";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x92:
                                {
                                    description = "set byte if below/carry";
                                    d.LastDisassembleData.OpCode = "setb";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x93:
                                {
                                    description = "set byte if above or equal";
                                    d.LastDisassembleData.OpCode = "setae";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x94:
                                {
                                    description = "set byte if equal";
                                    d.LastDisassembleData.OpCode = "sete";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x95:
                                {
                                    description = "set byte if not equal";
                                    d.LastDisassembleData.OpCode = "setne";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x96:
                                {
                                    description = "set byte if below or equal";
                                    d.LastDisassembleData.OpCode = "setbe";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x97:
                                {
                                    description = "set byte if above";
                                    d.LastDisassembleData.OpCode = "seta";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x98:
                                {
                                    description = "set byte if sign";
                                    d.LastDisassembleData.OpCode = "sets";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x99:
                                {
                                    description = "set byte if not sign";
                                    d.LastDisassembleData.OpCode = "setns";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x9a:
                                {
                                    description = "set byte if parity";
                                    d.LastDisassembleData.OpCode = "setp";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x9b:
                                {
                                    description = "set byte if not parity";
                                    d.LastDisassembleData.OpCode = "setnp";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x9c:
                                {
                                    description = "set byte if less";
                                    d.LastDisassembleData.OpCode = "setl";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x9d:
                                {
                                    description = "set byte if greater or equal";
                                    d.LastDisassembleData.OpCode = "setge";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x9e:
                                {
                                    description = "set byte if less or equal";
                                    d.LastDisassembleData.OpCode = "setle";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x9f:
                                {
                                    description = "set byte if greater";
                                    d.LastDisassembleData.OpCode = "setg";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xa0:
                                {
                                    description = "push word or doubleword onto the stack";
                                    d.LastDisassembleData.OpCode = "push";
                                    d.LastDisassembleData.Parameters = "fs";
                                    offset += 1;
                                }
                                break;
                            case 0xa1:
                                {
                                    description = "pop a value from the stack";
                                    d.LastDisassembleData.OpCode = "pop";
                                    d.LastDisassembleData.Parameters = "fs";
                                    offset += 1;
                                }
                                break;
                            case 0xa2:
                                {
                                    description = "cpu identification";
                                    d.LastDisassembleData.OpCode = "cpuid";
                                    offset += 1;
                                }
                                break;
                            case 0xa3:
                                {
                                    description = "bit test";
                                    d.LastDisassembleData.OpCode = "bt";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last) + d.R16(memory[2]);
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last) + d.R32(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xa4:
                                {
                                    description = "double precision shift left";
                                    d.LastDisassembleData.OpCode = "shld";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last) + d.R16(memory[2]);
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last) + d.R32(memory[2]);
                                    d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + AStringUtils.IntToHex(memory[(int)last], 2);
                                    last += 1;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xa5:
                                {
                                    description = "double precision shift left";
                                    d.LastDisassembleData.OpCode = "shld";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last) + d.R16(memory[2]) + ',' + d.ColorReg + "cl" + d.EndColor;
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last) + d.R32(memory[2]) + ',' + d.ColorReg + "cl" + d.EndColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xa8:
                                {
                                    description = "push word or doubleword onto the stack";
                                    d.LastDisassembleData.OpCode = "push";
                                    d.LastDisassembleData.Parameters = "gs";
                                    offset += 1;
                                }
                                break;
                            case 0xa9:
                                {
                                    description = "pop a value from the stack";
                                    d.LastDisassembleData.OpCode = "pop";
                                    d.LastDisassembleData.Parameters = "gs";
                                    offset += 1;
                                }
                                break;
                            case 0xaa:
                                {
                                    description = "resume from system management mode";
                                    d.LastDisassembleData.OpCode = "rsm";
                                    offset += 1;
                                }
                                break;
                            case 0xab:
                                {
                                    description = "bit test and set";
                                    d.LastDisassembleData.OpCode = "bts";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last) + d.R16(memory[2]);
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last) + d.R32(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xac:
                                {
                                    description = "double precision shift right";
                                    d.LastDisassembleData.OpCode = "shrd";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last) + d.R16(memory[2]);
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last) + d.R32(memory[2]);
                                    d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + AStringUtils.IntToHex(memory[(int)last], 2);
                                    last += 1;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xad:
                                {
                                    description = "double precision shift right";
                                    d.LastDisassembleData.OpCode = "shrd";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last) + d.R16(memory[2]) + ',' + d.ColorReg + "cl" + d.EndColor;
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last) + d.R32(memory[2]) + ',' + d.ColorReg + "cl" + d.EndColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xae:
                                {
                                    switch (memory[2])
                                    {
                                        case 0xf0:
                                            {
                                                description = "memory fence";
                                                d.LastDisassembleData.OpCode = "mfence";
                                                offset += 1;
                                            }
                                            break;
                                        case 0xf8:
                                            {
                                                description = "store fence";
                                                d.LastDisassembleData.OpCode = "sfence";
                                                offset += 1;
                                            }
                                            break;
                                        default:
                                            switch (d.GetReg(memory[2]))
                                            {
                                                case 0:
                                                    {
                                                        if (d.Prefix2.Contains(0xf3))
                                                        {
                                                            description = "read fs base address";
                                                            d.LastDisassembleData.OpCode = "rdfsbase";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "store fp and mmx state and streaming simd extension state";
                                                            d.LastDisassembleData.OpCode = "fxsave";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                    break;
                                                case 1:
                                                    {
                                                        if (d.Prefix2.Contains(0xf3))
                                                        {
                                                            description = "read gs base address";
                                                            d.LastDisassembleData.OpCode = "rdgsbase";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "restore fp and mmx state and streaming simd extension state";
                                                            d.LastDisassembleData.OpCode = "fxrstor";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                    break;
                                                case 2:
                                                    {
                                                        if (d.Prefix2.Contains(0xf3))
                                                        {
                                                            description = "write fs base address";
                                                            d.LastDisassembleData.OpCode = "wrfsbase";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "load streaming simd extension control/status";
                                                            if (d.HasVex)
                                                                d.LastDisassembleData.OpCode = "vldmxcsr";
                                                            else
                                                                d.LastDisassembleData.OpCode = "ldmxcsr";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                    break;
                                                case 3:
                                                    {
                                                        if (d.Prefix2.Contains(0xf3))
                                                        {
                                                            description = "write gs base address";
                                                            d.LastDisassembleData.OpCode = "wrgsbase";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "store streaming simd extension control/status";
                                                            if (d.HasVex)
                                                                d.LastDisassembleData.OpCode = "stmxcsr";
                                                            else
                                                                d.LastDisassembleData.OpCode = "stmxcsr";
                                                            d.OpCodeFlags.SkipExtraReg = true;
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                    break;
                                                case 4:
                                                    {
                                                        description = "save processor extended state";
                                                        if (d.RexW)
                                                            d.LastDisassembleData.OpCode = "xsave64";
                                                        else
                                                            d.LastDisassembleData.OpCode = "xsave";
                                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                    break;
                                                case 5:
                                                    {
                                                        if (d.GetMod(memory[2]) == 3)
                                                        {
                                                            description = "Load Fence";
                                                            d.LastDisassembleData.OpCode = "lfence";
                                                            offset += 2;
                                                        }
                                                        else
                                                        {
                                                            description = "restore processor extended state";
                                                            if (d.RexW)
                                                                d.LastDisassembleData.OpCode = "xrstor64";
                                                            else
                                                                d.LastDisassembleData.OpCode = "xrstor";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                    break;
                                                case 6:
                                                    {
                                                        description = "save processor extended status optimized";
                                                        if (d.RexW)
                                                            d.LastDisassembleData.OpCode = "xsaveopt64";
                                                        else
                                                            d.LastDisassembleData.OpCode = "xsaveopt";
                                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                    break;
                                                case 7:
                                                    {
                                                        ;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 0xaf:
                                {
                                    description = "signed multiply";
                                    d.LastDisassembleData.OpCode = "imul";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xb0:
                                {
                                    description = "compare and exchange";
                                    d.LastDisassembleData.OpCode = "cmpxchg";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last) + d.R8(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xb1:
                                {
                                    description = "compare and exchange";
                                    d.LastDisassembleData.OpCode = "cmpxchg";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last) + d.R16(memory[2]);
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last) + d.R32(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xb2:
                                {
                                    description = "load far pointer";
                                    d.LastDisassembleData.OpCode = "lss";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xb3:
                                {
                                    description = "bit test and reset";
                                    d.LastDisassembleData.OpCode = "btr";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last) + d.R16(memory[2]);
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last) + d.R32(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xb4:
                                {
                                    description = "load far pointer";
                                    d.LastDisassembleData.OpCode = "lfs";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xb5:
                                {
                                    description = "load far pointer";
                                    d.LastDisassembleData.OpCode = "lgs";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xb6:
                                {
                                    description = "Move with zero-extend";
                                    d.LastDisassembleData.OpCode = "movzx";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8, 0, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8, 0, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xb7:
                                {
                                    description = "Move with zero-extend";
                                    d.LastDisassembleData.OpCode = "movzx";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, 16, 0, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, 16, 0, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xb8:
                                {
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        description = "Return the Count of Number of Bits Set to 1";
                                        d.LastDisassembleData.OpCode = "popcnt";
                                        if (d.Prefix2.Contains(0x66))
                                            d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                        else
                                            d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xba:
                                {
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    switch (d.GetReg(memory[2]))
                                    {
                                        case 4:
                                            {
                                                //bt
                                                description = "bit test";
                                                d.LastDisassembleData.OpCode = "bt";
                                                if (d.Prefix2.Contains(0x66))
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last);
                                                else
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);     //notice the difference in the modrm 4th parameter
                                                d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                                d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 1);
                                            }
                                            break;
                                        case 5:
                                            {
                                                //bts
                                                description = "bit test and set";
                                                d.LastDisassembleData.OpCode = "bts";
                                                if (d.Prefix2.Contains(0x66))
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last);
                                                else
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);     //notice the difference in the modrm 4th parameter
                                                d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                                d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 1);
                                            }
                                            break;
                                        case 6:
                                            {
                                                //btr
                                                description = "bit test and reset";
                                                d.LastDisassembleData.OpCode = "btr";
                                                if (d.Prefix2.Contains(0x66))
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last);
                                                else
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);     //notice the difference in the modrm 4th parameter
                                                d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                                d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 1);
                                            }
                                            break;
                                        case 7:
                                            {
                                                //btc
                                                description = "bit test and complement";
                                                d.LastDisassembleData.OpCode = "btc";
                                                if (d.Prefix2.Contains(0x66))
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last);
                                                else
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);     //notice the difference in the modrm 4th parameter
                                                d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                                d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 1);
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 0xbb:
                                {
                                    description = "bit test and complement";
                                    d.LastDisassembleData.OpCode = "btc";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last) + d.R16(memory[2]);
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last) + d.R32(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xbc:
                                {
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        description = "count the number of trailing zero bits";
                                        d.LastDisassembleData.OpCode = "tzcnt";
                                        if (d.Prefix2.Contains(0x66))
                                            d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                        else
                                            d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        //bsf
                                        description = "bit scan forward";
                                        d.LastDisassembleData.OpCode = "bsf";
                                        if (d.Prefix2.Contains(0x66))
                                            d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                        else
                                            d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xbd:
                                {
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        description = "count the number of leading zero bits";
                                        d.LastDisassembleData.OpCode = "lzcnt";
                                        if (d.Prefix2.Contains(0x66))
                                            d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                        else
                                            d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        //bsf
                                        description = "bit scan reverse";
                                        d.LastDisassembleData.OpCode = "bsr";
                                        if (d.Prefix2.Contains(0x66))
                                            d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                        else
                                            d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xbe:
                                {
                                    description = "move with sign-extension";
                                    d.LastDisassembleData.OpCode = "movsx";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8, 0, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 2, ref last, 8, 0, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xbf:
                                {
                                    description = "move with sign-extension";
                                    d.LastDisassembleData.OpCode = "movsx";
                                    d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, 16, 0, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xc0:
                                {
                                    description = "exchange and add";
                                    d.LastDisassembleData.OpCode = "xadd";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last) + d.R8(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xc1:
                                {
                                    description = "exchange and add";
                                    d.LastDisassembleData.OpCode = "xadd";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last) + d.R16(memory[2]);
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last) + d.R32(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0xc2:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        description = "compare scalar dpuble-precision floating-point values";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vcmpsd";
                                        else
                                            d.LastDisassembleData.OpCode = "cmpsd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, 128, 0, ATmrPos.Right);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 2);
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        description = "packed single-fp compare";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vcmpss";
                                        else
                                            d.LastDisassembleData.OpCode = "cmpss";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, 128, 0, ATmrPos.Right);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 2);
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        if (d.Prefix2.Contains(0x66))
                                        {
                                            description = "compare packed double-precision floating-point values";
                                            if (d.HasVex)
                                                d.LastDisassembleData.OpCode = "vcmppd";
                                            else
                                                d.LastDisassembleData.OpCode = "cmppd";
                                            d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, 128, 0, ATmrPos.Right);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 2);
                                            offset = (UIntPtr)(offset.ToUInt64() + last);
                                        }
                                        else
                                        {
                                            description = "packed single-fp compare";
                                            if (d.HasVex)
                                                d.LastDisassembleData.OpCode = "vcmpps";
                                            else
                                                d.LastDisassembleData.OpCode = "cmpps";
                                            d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, 128, 0, ATmrPos.Right);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 2);
                                            d.LastDisassembleData.DataSize = 4;
                                            offset = (UIntPtr)(offset.ToUInt64() + last);
                                        }
                                    }
                                }
                                break;
                            case 0xc3:
                                {
                                    description = "store doubleword using non-temporal hint";
                                    d.LastDisassembleData.OpCode = "movnti";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last) + d.R32(memory[2]);
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                }
                                break;
                            case 0xc4:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "insert word";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpinsrw";
                                        else
                                            d.LastDisassembleData.OpCode = "pinsrw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 2);
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        description = "insert word";
                                        d.LastDisassembleData.OpCode = "pinsrw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 2);
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;
                            case 0xc5:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "extract word";
                                        d.LastDisassembleData.OpCode = "pextrw";
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 2);
                                        offset += 3;
                                    }
                                    else
                                    {
                                        description = "extract word";
                                        d.LastDisassembleData.OpCode = "pextrw";
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 2);
                                        offset += 3;
                                    }
                                }
                                break;
                            case 0xc6:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "shuffle double-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vshufpd";
                                        else
                                            d.LastDisassembleData.OpCode = "shufpd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 2);
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        description = "shuffle single-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vshufps";
                                        else
                                            d.LastDisassembleData.OpCode = "shufps";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 2);
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;
                            case 0xc7:
                                {
                                    switch (d.GetReg(memory[2]))
                                    {
                                        case 1:
                                            {
                                                description = "compare and exchange 8 bytes";
                                                d.LastDisassembleData.OpCode = "cmpxchg8b";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 3:
                                            {
                                                description = "restore processor extended status supervisor";
                                                if (d.RexW)
                                                    d.LastDisassembleData.OpCode = "xrstors64";
                                                else
                                                    d.LastDisassembleData.OpCode = "xrstors";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 4:
                                            {
                                                description = "save processor extended state with compaction";
                                                if (d.RexW)
                                                    d.LastDisassembleData.OpCode = "xsavec";
                                                else
                                                    d.LastDisassembleData.OpCode = "xsavec64";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 5:
                                            {
                                                description = "save processor extended state supervisor";
                                                if (d.RexW)
                                                    d.LastDisassembleData.OpCode = "xsaves";
                                                else
                                                    d.LastDisassembleData.OpCode = "xsaves64";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 6:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.GetMod(memory[2]) == 3)  //reg
                                                    {
                                                        description = "read random numer";
                                                        d.LastDisassembleData.OpCode = "rdrand";
                                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                    else
                                                    {
                                                        description = "copy vmcs data to vmcs region in memory";
                                                        d.LastDisassembleData.OpCode = "vmclear";
                                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                                else
                                                if (d.Prefix2.Contains(0xf3))
                                                {
                                                    description = "enter vmx root operation";
                                                    d.LastDisassembleData.OpCode = "vmxon";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    //check if it's a memory or register access
                                                    //if register it's rdrand else vmptrld
                                                    if (d.GetMod(memory[2]) == 3)  //reg
                                                    {
                                                        description = "read random numer";
                                                        d.LastDisassembleData.OpCode = "rdrand";
                                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                    else
                                                    {
                                                        description = "loads the current vmcs pointer from memory";
                                                        d.LastDisassembleData.OpCode = "vmptrld";
                                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        case 7:
                                            {
                                                if (d.GetMod(memory[2]) == 3)  //reg
                                                {
                                                    description = "read random SEED";
                                                    d.LastDisassembleData.OpCode = "rdseed";
                                                    if (d.Prefix2.Contains(0x66))
                                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last);
                                                    else
                                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    description = "stores the current vmcs pointer into memory";
                                                    d.LastDisassembleData.OpCode = "vmptrst";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 0xc8:
                            case 0xc9:
                            case 0xca:
                            case 0xcb:
                            case 0xcc:
                            case 0xcd:
                            case 0xce:
                            case 0xcf:
                                {
                                    //bswap
                                    description = "byte swap";
                                    d.LastDisassembleData.OpCode = "bswap";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.Rd16((Byte)(memory[1] - 0xc8));
                                    else
                                        d.LastDisassembleData.Parameters = d.Rd((Byte)(memory[1] - 0xc8));
                                    offset += 1;
                                }
                                break;
                            case 0xd0:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "Packed Double-FP Add/Subtract";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vaddsubpd";
                                        else
                                            d.LastDisassembleData.OpCode = "addsubpd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        description = "Packed Single-FP Add/Subtract";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vaddsubps";
                                        else
                                            d.LastDisassembleData.OpCode = "addsubps";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xd1:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed shift right logical";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpsrlw";
                                        else
                                            d.LastDisassembleData.OpCode = "psrlw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed shift right logical";
                                        d.LastDisassembleData.OpCode = "psrlw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xd2:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed shift right logical";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpsrld";
                                        else
                                            d.LastDisassembleData.OpCode = "psrld";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed shift right logical";
                                        d.LastDisassembleData.OpCode = "psrld";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xd3:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed shift right logical";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpsrlq";
                                        else
                                            d.LastDisassembleData.OpCode = "psrlq";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed shift right logical";
                                        d.LastDisassembleData.OpCode = "psrlq";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xd4:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "add packed quadword integers";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpaddq";
                                        else
                                            d.LastDisassembleData.OpCode = "paddq";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "add packed quadword integers";
                                        d.LastDisassembleData.OpCode = "paddq";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xd5:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed multiply low";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpmullw";
                                        else
                                            d.LastDisassembleData.OpCode = "pmullw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed multiply low";
                                        d.LastDisassembleData.OpCode = "pmullw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xd6:
                                {
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        description = "move low quadword from xmm to mmx technology register";
                                        d.LastDisassembleData.OpCode = "movdq2q";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        description = "move low quadword from xmm to mmx technology register";
                                        d.LastDisassembleData.OpCode = "movq2dq";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "move low quadword from xmm to mmx technology register";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovq";
                                        else
                                            d.LastDisassembleData.OpCode = "movq";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last) + d.Xmm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "move quadword from mmx technology to xmm register";
                                        d.LastDisassembleData.OpCode = "movq2dq";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last) + d.Mm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xd7:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "move byte mask to integer";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpmovmskb";
                                        else
                                            d.LastDisassembleData.OpCode = "pmovmskb";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "move byte mask to integer";
                                        d.LastDisassembleData.OpCode = "pmovmskb";
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xd8:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed subtract unsigned with saturation";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpsubusb";
                                        else
                                            d.LastDisassembleData.OpCode = "psubusb";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed subtract unsigned with saturation";
                                        d.LastDisassembleData.OpCode = "psubusb";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xd9:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed subtract unsigned with saturation";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpsubusw";
                                        else
                                            d.LastDisassembleData.OpCode = "psubusw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed subtract unsigned with saturation";
                                        d.LastDisassembleData.OpCode = "psubusw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xda:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed unsigned integer byte minimum";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpminub";
                                        else
                                            d.LastDisassembleData.OpCode = "pminub";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed unsigned integer byte minimum";
                                        d.LastDisassembleData.OpCode = "pminub";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xdb:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "logical and";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpand";
                                        else
                                            d.LastDisassembleData.OpCode = "pand";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "logical and";
                                        d.LastDisassembleData.OpCode = "pand";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xdc:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed add unsigned with saturation";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpaddusb";
                                        else
                                            d.LastDisassembleData.OpCode = "paddusb";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed add unsigned with saturation";
                                        d.LastDisassembleData.OpCode = "paddusb";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xdd:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed add unsigned with saturation";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpaddusw";
                                        else
                                            d.LastDisassembleData.OpCode = "paddusw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed add unsigned with saturation";
                                        d.LastDisassembleData.OpCode = "paddusw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xde:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed unsigned integer byte maximum";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpmaxub";
                                        else
                                            d.LastDisassembleData.OpCode = "pmaxub";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Left);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed unsigned integer byte maximum";
                                        d.LastDisassembleData.OpCode = "pmaxub";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xdf:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "logical and not";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpandn";
                                        else
                                            d.LastDisassembleData.OpCode = "pandn";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "logical and not";
                                        d.LastDisassembleData.OpCode = "pandn";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xe0:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed average";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpavgb";
                                        else
                                            d.LastDisassembleData.OpCode = "pavgb";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed average";
                                        d.LastDisassembleData.OpCode = "pavgb";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xe1:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed shift right arithmetic";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpsraw";
                                        else
                                            d.LastDisassembleData.OpCode = "psraw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed shift right arithmetic";
                                        d.LastDisassembleData.OpCode = "psraw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xe2:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed shift left logical";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpsrad";
                                        else
                                            d.LastDisassembleData.OpCode = "psrad";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed shift left logical";
                                        d.LastDisassembleData.OpCode = "psrad";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xe3:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed average";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpavgw";
                                        else
                                            d.LastDisassembleData.OpCode = "pavgw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed average";
                                        d.LastDisassembleData.OpCode = "pavgw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xe4:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed multiply high unsigned";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpmulhuw";
                                        else
                                            d.LastDisassembleData.OpCode = "pmulhuw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed multiply high unsigned";
                                        d.LastDisassembleData.OpCode = "pmulhuw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xe5:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed multiply high";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpmulhw";
                                        else
                                            d.LastDisassembleData.OpCode = "pmulhw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed multiply high";
                                        d.LastDisassembleData.OpCode = "pmulhw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xe6:
                                {
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        description = "convert two packed signed dwords from param2 to two packed dp-floating point values in param1";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vcvtpd2dq";
                                        else
                                            d.LastDisassembleData.OpCode = "cvtpd2dq";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        description = "convert two packed signed dwords from param2 to two packed dp-floating point values in param1";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vcvtdq2pd";
                                        else
                                            d.LastDisassembleData.OpCode = "cvtdq2pd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + ',';
                                        d.OpCodeFlags.L = false;
                                        d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.Prefix2.Contains(0x66))
                                        {
                                            description = "convert with truncation packed double-precision floating-point values to packed doubleword integers";
                                            if (d.HasVex)
                                                d.LastDisassembleData.OpCode = "vcvttpd2dq";
                                            else
                                                d.LastDisassembleData.OpCode = "cvttpd2dq";
                                            d.OpCodeFlags.SkipExtraReg = true;
                                            d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                }
                                break;
                            case 0xe7:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "movntdq";
                                        else
                                            d.LastDisassembleData.OpCode = "vmovntdq";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last) + d.Xmm(memory[2]);
                                        description = "move double quadword using non-temporal hint";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "movntq";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 3, ref last) + d.Mm(memory[2]);
                                        description = "move 64 bits non temporal";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xe8:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed subtract with saturation";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpsubsb";
                                        else
                                            d.LastDisassembleData.OpCode = "psubsb";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed subtract with saturation";
                                        d.LastDisassembleData.OpCode = "psubsb";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xe9:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed subtract with saturation";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpsubsw";
                                        else
                                            d.LastDisassembleData.OpCode = "psubsw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed subtract with saturation";
                                        d.LastDisassembleData.OpCode = "psubsw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xea:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed signed integer word minimum";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpminsw";
                                        else
                                            d.LastDisassembleData.OpCode = "pminsw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed signed integer word minimum";
                                        d.LastDisassembleData.OpCode = "pminsw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xeb:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "bitwise logical or";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpor";
                                        else
                                            d.LastDisassembleData.OpCode = "por";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "bitwise logical or";
                                        d.LastDisassembleData.OpCode = "por";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xec:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed add with saturation";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpaddsb";
                                        else
                                            d.LastDisassembleData.OpCode = "paddsb";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed add with saturation";
                                        d.LastDisassembleData.OpCode = "paddsb";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xed:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed add with saturation";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpaddsw";
                                        else
                                            d.LastDisassembleData.OpCode = "paddsw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed add with saturation";
                                        d.LastDisassembleData.OpCode = "paddsw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xee:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed signed integer word maximum";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpmaxsw";
                                        else
                                            d.LastDisassembleData.OpCode = "pmaxsw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed signed integer word maximum";
                                        d.LastDisassembleData.OpCode = "pmaxsw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xef:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "logical exclusive or";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpxor";
                                        else
                                            d.LastDisassembleData.OpCode = "pxor";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "logical exclusive or";
                                        d.LastDisassembleData.OpCode = "pxor";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xf0:
                                {
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        description = "load unaligned integer 128 bits";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vlddqu";
                                        else
                                            d.LastDisassembleData.OpCode = "lddqu";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                        offset += 1;
                                }
                                break;
                            case 0xf1:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed shift left logical";
                                        d.LastDisassembleData.OpCode = "psllw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed shift left logical";
                                        d.LastDisassembleData.OpCode = "psllw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xf2:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed shift left logical";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpslld";
                                        else
                                            d.LastDisassembleData.OpCode = "pslld";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed shift left logical";
                                        d.LastDisassembleData.OpCode = "pslld";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xf3:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed shift left logical";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpsllq";
                                        else
                                            d.LastDisassembleData.OpCode = "psllq";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed shift left logical";
                                        d.LastDisassembleData.OpCode = "psllq";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xf4:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "multiply packed unsigned doubleword integers";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "pmuludq";
                                        else
                                            d.LastDisassembleData.OpCode = "vpmuludq";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "multiply packed unsigned doubleword integers";
                                        d.LastDisassembleData.OpCode = "pmuludq";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xf5:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed multiply and add";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpmaddwd";
                                        else
                                            d.LastDisassembleData.OpCode = "pmaddwd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed multiply and add";
                                        d.LastDisassembleData.OpCode = "pmaddwd";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xf6:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed sum of absolute differences";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpsadbw";
                                        else
                                            d.LastDisassembleData.OpCode = "psadbw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed sum of absolute differences";
                                        d.LastDisassembleData.OpCode = "psadbw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xf7:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "store selected bytes of double quadword";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmaskmovdqu";
                                        else
                                            d.LastDisassembleData.OpCode = "maskmovdqu";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "byte mask write";
                                        d.LastDisassembleData.OpCode = "maskmovq";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xf8:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed subtract";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpsubb";
                                        else
                                            d.LastDisassembleData.OpCode = "psubb";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed subtract";
                                        d.LastDisassembleData.OpCode = "psubb";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xf9:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed subtract";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpsubw";
                                        else
                                            d.LastDisassembleData.OpCode = "psubw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed subtract";
                                        d.LastDisassembleData.OpCode = "psubw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xfa:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed subtract";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpsubd";
                                        else
                                            d.LastDisassembleData.OpCode = "psubd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed subtract";
                                        d.LastDisassembleData.OpCode = "psubd";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xfb:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed subtract";
                                        d.LastDisassembleData.OpCode = "psubq";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed subtract";
                                        d.LastDisassembleData.OpCode = "psubq";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xfc:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed add";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpaddb";
                                        else
                                            d.LastDisassembleData.OpCode = "paddb";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed add";
                                        d.LastDisassembleData.OpCode = "paddb";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xfd:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed add";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpaddw";
                                        else
                                            d.LastDisassembleData.OpCode = "paddw";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed add";
                                        d.LastDisassembleData.OpCode = "paddw";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0xfe:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "packed add";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vpaddd";
                                        else
                                            d.LastDisassembleData.OpCode = "paddd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed add";
                                        d.LastDisassembleData.OpCode = "paddd";
                                        d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
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
