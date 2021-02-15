using System;
using Sputnik.LBinary;
using Sputnik.LMarshal;
using SputnikAsm.LDisassembler.LEnums;
using SputnikAsm.LUtils;

namespace SputnikAsm.LDisassembler
{
    public partial class ADisassembler
    {
        #region DisassembleProcess3
        private Boolean DisassembleProcess3(UBytePtr memory, ref UIntPtr offset, ref int prefixSize, ref UInt32 last, ref String description)
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
                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    LastDisassembleData.ParameterValue = (UIntPtr)memory[3];
                                    LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 3;
                                    LastDisassembleData.SeparatorCount += 1;

                                    switch (GetReg(memory[2]))
                                    {
                                        case 2:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "packed shift right logical";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpsrlq";
                                                    else
                                                        LastDisassembleData.OpCode = "psrlq";
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                                    LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                                else
                                                {
                                                    description = "packed shift right logical";
                                                    LastDisassembleData.OpCode = "psrlq";
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                                    LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                                LastDisassembleData.Parameters = LastDisassembleData.Parameters.Substring(1);
                                            }
                                            break;

                                        case 3:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "shift double quadword right logical";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpsrldq";
                                                    else
                                                        LastDisassembleData.OpCode = "psrldq";

                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                                    LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                                LastDisassembleData.Parameters = LastDisassembleData.Parameters.Substring(1);
                                            }
                                            break;

                                        case 6:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "packed shift left logical";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpsllq";
                                                    else
                                                        LastDisassembleData.OpCode = "psllq";
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                                    LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                                else
                                                {
                                                    description = "packed shift left logical";
                                                    LastDisassembleData.OpCode = "psllq";
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                                    LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                                LastDisassembleData.Parameters = LastDisassembleData.Parameters.Substring(1);
                                            }
                                            break;

                                        case 7:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "shift double quadword left logical";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpslldq";
                                                    else
                                                        LastDisassembleData.OpCode = "pslldq";
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                                    LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + IntToHexSigned((UIntPtr)memory[(int)last], 2);

                                                    LastDisassembleData.Parameters = LastDisassembleData.Parameters.Substring(1);
                                                    offset += 3;
                                                }
                                            }
                                            break;
                                    }
                                }
                                break;



                            case 0x74:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed compare for equal";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpcmpeqb";
                                        else
                                            LastDisassembleData.OpCode = "pcmpeqb";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed compare for equal";
                                        LastDisassembleData.OpCode = "pcmpeqb";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x75:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed compare for equal";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpcmpeqw";
                                        else
                                            LastDisassembleData.OpCode = "pcmpeqw";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed compare for equal";
                                        LastDisassembleData.OpCode = "pcmpeqw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x76:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed compare for equal";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpcmpeqd";
                                        else
                                            LastDisassembleData.OpCode = "pcmpeqd";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed compare for equal";
                                        LastDisassembleData.OpCode = "pcmpeqd";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;


                            case 0x77:
                                {
                                    if (_hasVex)
                                    {
                                        if (_opCodeFlags.L)
                                        {
                                            description = "Zero all YMM registers";
                                            LastDisassembleData.OpCode = "vzeroall";
                                            offset += 1;
                                        }
                                        else
                                        {
                                            description = "Zero upper bits of YMM registers";
                                            LastDisassembleData.OpCode = "vzeroupper";
                                            offset += 1;
                                        }
                                    }
                                    else
                                    {
                                        description = "empty mmx™ state";
                                        LastDisassembleData.OpCode = "emms";
                                        offset += 1;
                                    }
                                }
                                break;

                            case 0x78:
                                {
                                    description = "reads a specified vmcs field (32 bits)";
                                    LastDisassembleData.OpCode = "vmread";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last) + R32(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x79:
                                {
                                    description = "writes a specified vmcs field (32 bits)";
                                    LastDisassembleData.OpCode = "vmwrite";
                                    LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x7c:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vhaddpd";
                                        else
                                            LastDisassembleData.OpCode = "haddpd";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "packed double-fp horizontal add";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf2))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vhaddps";
                                        else
                                            LastDisassembleData.OpCode = "haddps";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "packed single-fp horizontal add";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x7d:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vhsubpd";
                                        else
                                            LastDisassembleData.OpCode = "hsubpd";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "packed double-fp horizontal subtract";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf2))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vhsubps";
                                        else
                                            LastDisassembleData.OpCode = "hsubps";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "packed single-fp horizontal subtract";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x7e:
                                {

                                    if (_prefix2.Contains(0xf3))
                                    {

                                        description = "move quadword";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovq";
                                        else
                                            LastDisassembleData.OpCode = "movq";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0x66))
                                    {
                                        if (RexW)
                                        {
                                            description = "move 64 bits";
                                            LastDisassembleData.OpCode = "movq";
                                        }
                                        else
                                        {
                                            description = "move 32 bits";
                                            LastDisassembleData.OpCode = "movd";
                                        }

                                        if (_hasVex)
                                            LastDisassembleData.OpCode = 'v' + LastDisassembleData.OpCode;

                                        _opCodeFlags.SkipExtraReg = true;

                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last) + Xmm(memory[2]);  //r32/rm32,xmm
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (RexW)
                                        {
                                            description = "move 64 bits";
                                            LastDisassembleData.OpCode = "movq";
                                        }
                                        else
                                        {
                                            description = "move 32 bits";
                                            LastDisassembleData.OpCode = "movd";
                                        }


                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last) + Mm(memory[2]); //r32/rm32,mm
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x7f:
                                {
                                    if (_prefix2.Contains(0xf3))
                                    {

                                        description = "move unaligned double quadword";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovdqu";
                                        else
                                            LastDisassembleData.OpCode = "movdqu";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last) + Xmm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "move aligned double quadword";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovdqa";
                                        else
                                            LastDisassembleData.OpCode = "movdqa";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last) + Xmm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "move 64 bits";
                                        LastDisassembleData.OpCode = "movq";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 3, ref last) + Mm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x80:
                                {
                                    description = "jump near if overflow (OF=1)";
                                    LastDisassembleData.OpCode = "jo";
                                    LastDisassembleData.IsJump = true;
                                    LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_of) != 0;

                                    offset += 1 + 4;
                                    if (MarkIpRelativeInstructions)
                                    {
                                        LastDisassembleData.RipRelative = 2;
                                        _ripRelative = true;
                                    }

                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (Is64Bit)
                                        LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));

                                    LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 2;
                                    LastDisassembleData.SeparatorCount += 1;



                                    LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                                }
                                break;

                            case 0x81:
                                {
                                    description = "jump near if not overflow (OF=0)";
                                    LastDisassembleData.OpCode = "jno";
                                    LastDisassembleData.IsJump = true;
                                    LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_of) == 0;

                                    offset += 1 + 4;
                                    if (MarkIpRelativeInstructions)
                                    {
                                        LastDisassembleData.RipRelative = 2;
                                        _ripRelative = true;
                                    }

                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (Is64Bit)
                                        LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));

                                    LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 2;
                                    LastDisassembleData.SeparatorCount += 1;



                                    LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                                }
                                break;

                            case 0x82:
                                {
                                    description = "jump near if below/carry (CF=1)";

                                    LastDisassembleData.OpCode = "jb";
                                    LastDisassembleData.IsJump = true;
                                    LastDisassembleData.IsConditionalJump = true;

                                    //if (context != nil)
                                    //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_cf) != 0;

                                    offset += 1 + 4;
                                    if (MarkIpRelativeInstructions)
                                    {
                                        LastDisassembleData.RipRelative = 2;
                                        _ripRelative = true;
                                    }

                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (Is64Bit)
                                        LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));

                                    LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 2;
                                    LastDisassembleData.SeparatorCount += 1;



                                    LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                                }
                                break;

                            case 0x83:
                                {
                                    description = "jump near if above or equal (CF=0)";
                                    LastDisassembleData.OpCode = "jae";
                                    LastDisassembleData.IsJump = true;
                                    LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_cf) == 0;

                                    offset += 1 + 4;
                                    if (MarkIpRelativeInstructions)
                                    {
                                        LastDisassembleData.RipRelative = 2;
                                        _ripRelative = true;
                                    }

                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (Is64Bit)
                                        LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));

                                    LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 2;
                                    LastDisassembleData.SeparatorCount += 1;



                                    LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                                }
                                break;

                            case 0x84:
                                {
                                    description = "jump near if equal (ZF=1)";

                                    LastDisassembleData.OpCode = "je";
                                    LastDisassembleData.IsJump = true;
                                    LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_zf) != 0;

                                    offset += 1 + 4;
                                    if (MarkIpRelativeInstructions)
                                    {
                                        LastDisassembleData.RipRelative = 2;
                                        _ripRelative = true;
                                    }

                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (Is64Bit)
                                        LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));

                                    LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 2;
                                    LastDisassembleData.SeparatorCount += 1;



                                    LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                                }
                                break;


                            case 0x85:
                                {
                                    description = "jump near if not equal (ZF=0)";
                                    LastDisassembleData.OpCode = "jne";
                                    LastDisassembleData.IsJump = true;
                                    LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_zf) == 0;

                                    offset += 1 + 4;
                                    if (MarkIpRelativeInstructions)
                                    {
                                        LastDisassembleData.RipRelative = 2;
                                        _ripRelative = true;
                                    }

                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (Is64Bit)
                                        LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));

                                    LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 2;
                                    LastDisassembleData.SeparatorCount += 1;



                                    LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                                }
                                break;

                            case 0x86:
                                {
                                    description = "jump near if below or equal (CF=1 or ZF=1)";
                                    LastDisassembleData.OpCode = "jbe";
                                    LastDisassembleData.IsJump = true;
                                    LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & (eflags_cf | eflags_zf)) != 0;

                                    offset += 1 + 4;
                                    if (MarkIpRelativeInstructions)
                                    {
                                        LastDisassembleData.RipRelative = 2;
                                        _ripRelative = true;
                                    }


                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (Is64Bit)
                                        LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));

                                    LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 2;
                                    LastDisassembleData.SeparatorCount += 1;



                                    LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                                }
                                break;

                            case 0x87:
                                {
                                    description = "jump near if above (CF=0 and ZF=0)";
                                    LastDisassembleData.OpCode = "ja";
                                    LastDisassembleData.IsJump = true;
                                    LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & (eflags_cf | eflags_zf)) == 0;
                                    offset += 1 + 4;
                                    if (MarkIpRelativeInstructions)
                                    {
                                        LastDisassembleData.RipRelative = 2;
                                        _ripRelative = true;
                                    }

                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (Is64Bit)
                                        LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));

                                    LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 2;
                                    LastDisassembleData.SeparatorCount += 1;



                                    LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                                }
                                break;

                            case 0x88:
                                {
                                    description = "jump near if sign (SF=1)";
                                    LastDisassembleData.OpCode = "js";
                                    LastDisassembleData.IsJump = true;
                                    LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_sf) != 0;

                                    offset += 1 + 4;
                                    if (MarkIpRelativeInstructions)
                                    {
                                        LastDisassembleData.RipRelative = 2;
                                        _ripRelative = true;
                                    }

                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (Is64Bit)
                                        LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));

                                    LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 2;
                                    LastDisassembleData.SeparatorCount += 1;



                                    LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                                }
                                break;

                            case 0x89:
                                {
                                    description = "jump near if not sign (SF=0)";
                                    LastDisassembleData.OpCode = "jns";
                                    LastDisassembleData.IsJump = true;
                                    LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_sf) == 0;

                                    offset += 1 + 4;
                                    if (MarkIpRelativeInstructions)
                                    {
                                        LastDisassembleData.RipRelative = 2;
                                        _ripRelative = true;
                                    }

                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (Is64Bit)
                                        LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));

                                    LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 2;
                                    LastDisassembleData.SeparatorCount += 1;



                                    LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                                }
                                break;

                            case 0x8a:
                                {
                                    description = "jump near if parity (PF=1)";
                                    LastDisassembleData.OpCode = "jp";
                                    LastDisassembleData.IsJump = true;
                                    LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_pf) != 0;

                                    offset += 1 + 4;
                                    if (MarkIpRelativeInstructions)
                                    {
                                        LastDisassembleData.RipRelative = 2;
                                        _ripRelative = true;
                                    }

                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (Is64Bit)
                                        LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));

                                    LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 2;
                                    LastDisassembleData.SeparatorCount += 1;



                                    LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                                }
                                break;

                            case 0x8b:
                                {
                                    description = "jump near if not parity (PF=0)";
                                    LastDisassembleData.OpCode = "jnp";
                                    LastDisassembleData.IsJump = true;
                                    LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_pf) == 0;

                                    offset += 1 + 4;
                                    if (MarkIpRelativeInstructions)
                                    {
                                        LastDisassembleData.RipRelative = 2;
                                        _ripRelative = true;
                                    }

                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (Is64Bit)
                                        LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));

                                    LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 2;
                                    LastDisassembleData.SeparatorCount += 1;



                                    LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                                }
                                break;

                            case 0x8c:
                                {
                                    description = "jump near if less (SF~=OF)";
                                    LastDisassembleData.OpCode = "jl";
                                    LastDisassembleData.IsJump = true;
                                    LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_sf) != (context->eflags & eflags_of);

                                    offset += 1 + 4;
                                    if (MarkIpRelativeInstructions)
                                    {
                                        LastDisassembleData.RipRelative = 2;
                                        _ripRelative = true;
                                    }

                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (Is64Bit)
                                        LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));

                                    LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 2;
                                    LastDisassembleData.SeparatorCount += 1;



                                    LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                                }
                                break;

                            case 0x8d:
                                {
                                    description = "jump near if not less (SF=OF)";
                                    LastDisassembleData.OpCode = "jnl";
                                    LastDisassembleData.IsJump = true;
                                    LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_sf) == (context->eflags & eflags_of);

                                    offset += 1 + 4;
                                    if (MarkIpRelativeInstructions)
                                    {
                                        LastDisassembleData.RipRelative = 2;
                                        _ripRelative = true;
                                    }

                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (Is64Bit)
                                        LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));

                                    LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 2;
                                    LastDisassembleData.SeparatorCount += 1;



                                    LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                                }
                                break;

                            case 0x8e:
                                {
                                    description = "jump near if not greater (ZF=1 or SF~=OF)";
                                    LastDisassembleData.OpCode = "jng";
                                    LastDisassembleData.IsJump = true;
                                    LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //lastdisassembledata.willjumpaccordingtocontext = ((context->eflags & eflags_sf) != (context->eflags & eflags_of)) || ((context->eflags & eflags_zf) != 0);


                                    offset += 1 + 4;
                                    if (MarkIpRelativeInstructions)
                                    {
                                        LastDisassembleData.RipRelative = 2;
                                        _ripRelative = true;
                                    }

                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (Is64Bit)
                                        LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));

                                    LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 2;
                                    LastDisassembleData.SeparatorCount += 1;



                                    LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                                }
                                break;

                            case 0x8f:
                                {
                                    description = "jump near if greater (ZF=0 and SF=OF)";
                                    LastDisassembleData.OpCode = "jg";
                                    LastDisassembleData.IsJump = true;
                                    LastDisassembleData.IsConditionalJump = true;
                                    //if (context != nil)
                                    //lastdisassembledata.willjumpaccordingtocontext = ((context->eflags & eflags_sf) == (context->eflags & eflags_of)) && ((context->eflags & eflags_zf) == 0);


                                    offset += 1 + 4;
                                    if (MarkIpRelativeInstructions)
                                    {
                                        LastDisassembleData.RipRelative = 2;
                                        _ripRelative = true;
                                    }

                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                    if (Is64Bit)
                                        LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                                    else
                                        LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));

                                    LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 2;
                                    LastDisassembleData.SeparatorCount += 1;



                                    LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                                }
                                break;

                            case 0x90:
                                {
                                    description = "set byte if overflow";
                                    LastDisassembleData.OpCode = "seto";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x91:
                                {
                                    description = "set byte if not overfloww";
                                    LastDisassembleData.OpCode = "setno";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x92:
                                {
                                    description = "set byte if below/carry";
                                    LastDisassembleData.OpCode = "setb";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x93:
                                {
                                    description = "set byte if above or equal";
                                    LastDisassembleData.OpCode = "setae";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x94:
                                {
                                    description = "set byte if equal";
                                    LastDisassembleData.OpCode = "sete";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x95:
                                {
                                    description = "set byte if not equal";
                                    LastDisassembleData.OpCode = "setne";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x96:
                                {
                                    description = "set byte if below or equal";
                                    LastDisassembleData.OpCode = "setbe";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x97:
                                {
                                    description = "set byte if above";
                                    LastDisassembleData.OpCode = "seta";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x98:
                                {
                                    description = "set byte if sign";
                                    LastDisassembleData.OpCode = "sets";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x99:
                                {
                                    description = "set byte if not sign";
                                    LastDisassembleData.OpCode = "setns";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x9a:
                                {
                                    description = "set byte if parity";
                                    LastDisassembleData.OpCode = "setp";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x9b:
                                {
                                    description = "set byte if not parity";
                                    LastDisassembleData.OpCode = "setnp";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x9c:
                                {
                                    description = "set byte if less";
                                    LastDisassembleData.OpCode = "setl";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x9d:
                                {
                                    description = "set byte if greater or equal";
                                    LastDisassembleData.OpCode = "setge";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());

                                }
                                break;

                            case 0x9e:
                                {
                                    description = "set byte if less or equal";
                                    LastDisassembleData.OpCode = "setle";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());

                                }
                                break;

                            case 0x9f:
                                {
                                    description = "set byte if greater";
                                    LastDisassembleData.OpCode = "setg";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());


                                }
                                break;

                            case 0xa0:
                                {
                                    description = "push word or doubleword onto the stack";
                                    LastDisassembleData.OpCode = "push";
                                    LastDisassembleData.Parameters = "fs";
                                    offset += 1;
                                }
                                break;

                            case 0xa1:
                                {
                                    description = "pop a value from the stack";
                                    LastDisassembleData.OpCode = "pop";
                                    LastDisassembleData.Parameters = "fs";
                                    offset += 1;
                                }
                                break;


                            case 0xa2:
                                {
                                    description = "cpu identification";
                                    LastDisassembleData.OpCode = "cpuid";
                                    offset += 1;
                                }
                                break;

                            case 0xa3:
                                {
                                    description = "bit test";
                                    LastDisassembleData.OpCode = "bt";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last) + R16(memory[2]);
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last) + R32(memory[2]);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0xa4:
                                {
                                    description = "double precision shift left";
                                    LastDisassembleData.OpCode = "shld";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last) + R16(memory[2]);
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last) + R32(memory[2]);

                                    LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + AStringUtils.IntToHex(memory[(int)last], 2);
                                    last += 1;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());

                                }
                                break;

                            case 0xa5:
                                {
                                    description = "double precision shift left";
                                    LastDisassembleData.OpCode = "shld";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last) + R16(memory[2]) + ',' + _colorReg + "cl" + _endColor;
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last) + R32(memory[2]) + ',' + _colorReg + "cl" + _endColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());

                                }
                                break;

                            case 0xa8:
                                {
                                    description = "push word or doubleword onto the stack";
                                    LastDisassembleData.OpCode = "push";
                                    LastDisassembleData.Parameters = "gs";
                                    offset += 1;
                                }
                                break;

                            case 0xa9:
                                {
                                    description = "pop a value from the stack";
                                    LastDisassembleData.OpCode = "pop";
                                    LastDisassembleData.Parameters = "gs";
                                    offset += 1;
                                }
                                break;

                            case 0xaa:
                                {
                                    description = "resume from system management mode";
                                    LastDisassembleData.OpCode = "rsm";
                                    offset += 1;
                                }
                                break;

                            case 0xab:
                                {
                                    description = "bit test and set";
                                    LastDisassembleData.OpCode = "bts";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last) + R16(memory[2]);
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last) + R32(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());

                                }
                                break;

                            case 0xac:
                                {
                                    description = "double precision shift right";
                                    LastDisassembleData.OpCode = "shrd";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last) + R16(memory[2]);
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last) + R32(memory[2]);

                                    LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + AStringUtils.IntToHex(memory[(int)last], 2);
                                    last += 1;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0xad:
                                {
                                    description = "double precision shift right";
                                    LastDisassembleData.OpCode = "shrd";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last) + R16(memory[2]) + ',' + _colorReg + "cl" + _endColor;
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last) + R32(memory[2]) + ',' + _colorReg + "cl" + _endColor;
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
                                                LastDisassembleData.OpCode = "mfence";
                                                offset += 1;
                                            }
                                            break;

                                        case 0xf8:
                                            {
                                                description = "store fence";
                                                LastDisassembleData.OpCode = "sfence";
                                                offset += 1;
                                            }
                                            break;

                                        default:
                                            switch (GetReg(memory[2]))
                                            {
                                                case 0:
                                                    {
                                                        if (_prefix2.Contains(0xf3))
                                                        {
                                                            description = "read fs base address";
                                                            LastDisassembleData.OpCode = "rdfsbase";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "store fp and mmx state and streaming simd extension state";
                                                            LastDisassembleData.OpCode = "fxsave";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                    break;

                                                case 1:
                                                    {
                                                        if (_prefix2.Contains(0xf3))
                                                        {
                                                            description = "read gs base address";
                                                            LastDisassembleData.OpCode = "rdgsbase";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "restore fp and mmx state and streaming simd extension state";
                                                            LastDisassembleData.OpCode = "fxrstor";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                    break;

                                                case 2:
                                                    {
                                                        if (_prefix2.Contains(0xf3))
                                                        {
                                                            description = "write fs base address";
                                                            LastDisassembleData.OpCode = "wrfsbase";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "load streaming simd extension control/status";
                                                            if (_hasVex)
                                                                LastDisassembleData.OpCode = "vldmxcsr";
                                                            else
                                                                LastDisassembleData.OpCode = "ldmxcsr";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                    break;

                                                case 3:
                                                    {
                                                        if (_prefix2.Contains(0xf3))
                                                        {
                                                            description = "write gs base address";
                                                            LastDisassembleData.OpCode = "wrgsbase";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "store streaming simd extension control/status";
                                                            if (_hasVex)
                                                                LastDisassembleData.OpCode = "stmxcsr";
                                                            else
                                                                LastDisassembleData.OpCode = "stmxcsr";

                                                            _opCodeFlags.SkipExtraReg = true;
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                    break;

                                                case 4:
                                                    {
                                                        description = "save processor extended state";
                                                        if (RexW)
                                                            LastDisassembleData.OpCode = "xsave64";
                                                        else
                                                            LastDisassembleData.OpCode = "xsave";
                                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                    break;

                                                case 5:
                                                    {
                                                        if (GetMod(memory[2]) == 3)
                                                        {
                                                            description = "Load Fence";
                                                            LastDisassembleData.OpCode = "lfence";
                                                            offset += 2;
                                                        }
                                                        else
                                                        {
                                                            description = "restore processor extended state";
                                                            if (RexW)
                                                                LastDisassembleData.OpCode = "xrstor64";
                                                            else
                                                                LastDisassembleData.OpCode = "xrstor";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                    break;

                                                case 6:
                                                    {
                                                        description = "save processor extended status optimized";
                                                        if (RexW)
                                                            LastDisassembleData.OpCode = "xsaveopt64";
                                                        else
                                                            LastDisassembleData.OpCode = "xsaveopt";
                                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
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
                                    LastDisassembleData.OpCode = "imul";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0xb0:
                                {
                                    description = "compare and exchange";
                                    LastDisassembleData.OpCode = "cmpxchg";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last) + R8(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0xb1:
                                {
                                    description = "compare and exchange";
                                    LastDisassembleData.OpCode = "cmpxchg";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last) + R16(memory[2]);
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last) + R32(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0xb2:
                                {
                                    description = "load far pointer";
                                    LastDisassembleData.OpCode = "lss";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0xb3:
                                {
                                    description = "bit test and reset";
                                    LastDisassembleData.OpCode = "btr";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last) + R16(memory[2]);
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last) + R32(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());

                                }
                                break;

                            case 0xb4:
                                {
                                    description = "load far pointer";
                                    LastDisassembleData.OpCode = "lfs";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0xb5:
                                {
                                    description = "load far pointer";
                                    LastDisassembleData.OpCode = "lgs";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0xb6:
                                {
                                    description = "Move with zero-extend";
                                    LastDisassembleData.OpCode = "movzx";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 2, ref last, 8, 0, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 2, ref last, 8, 0, ATmrPos.Right);


                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0xb7:
                                {
                                    description = "Move with zero-extend";
                                    LastDisassembleData.OpCode = "movzx";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, 16, 0, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, 16, 0, ATmrPos.Right);


                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0xb8:
                                {
                                    if (_prefix2.Contains(0xf3))
                                    {
                                        description = "Return the Count of Number of Bits Set to 1";
                                        LastDisassembleData.OpCode = "popcnt";
                                        if (_prefix2.Contains(0x66))
                                            LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                        else
                                            LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;


                            case 0xba:
                                {
                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;


                                    switch (GetReg(memory[2]))
                                    {
                                        case 4:
                                            {
                                                //bt
                                                description = "bit test";
                                                LastDisassembleData.OpCode = "bt";
                                                if (_prefix2.Contains(0x66))
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last);
                                                else
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);     //notice the difference in the modrm 4th parameter

                                                LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                                LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);

                                                offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 1);
                                            }
                                            break;

                                        case 5:
                                            {
                                                //bts
                                                description = "bit test and set";
                                                LastDisassembleData.OpCode = "bts";
                                                if (_prefix2.Contains(0x66))
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last);
                                                else
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);     //notice the difference in the modrm 4th parameter

                                                LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                                LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 1);
                                            }
                                            break;

                                        case 6:
                                            {
                                                //btr
                                                description = "bit test and reset";
                                                LastDisassembleData.OpCode = "btr";
                                                if (_prefix2.Contains(0x66))
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last);
                                                else
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);     //notice the difference in the modrm 4th parameter

                                                LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                                LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);

                                                offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 1);
                                            }
                                            break;

                                        case 7:
                                            {
                                                //btc
                                                description = "bit test and complement";
                                                LastDisassembleData.OpCode = "btc";
                                                if (_prefix2.Contains(0x66))
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last);
                                                else
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);     //notice the difference in the modrm 4th parameter

                                                LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                                LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);

                                                offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 1);
                                            }
                                            break;

                                    }

                                }
                                break;

                            case 0xbb:
                                {
                                    description = "bit test and complement";
                                    LastDisassembleData.OpCode = "btc";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last) + R16(memory[2]);
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last) + R32(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());

                                }
                                break;


                            case 0xbc:
                                {
                                    if (_prefix2.Contains(0xf3))
                                    {
                                        description = "count the number of trailing zero bits";
                                        LastDisassembleData.OpCode = "tzcnt";
                                        if (_prefix2.Contains(0x66))
                                            LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                        else
                                            LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        //bsf
                                        description = "bit scan forward";
                                        LastDisassembleData.OpCode = "bsf";
                                        if (_prefix2.Contains(0x66))
                                            LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                        else
                                            LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);


                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xbd:
                                {
                                    if (_prefix2.Contains(0xf3))
                                    {
                                        description = "count the number of leading zero bits";
                                        LastDisassembleData.OpCode = "lzcnt";
                                        if (_prefix2.Contains(0x66))
                                            LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                        else
                                            LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        //bsf
                                        description = "bit scan reverse";
                                        LastDisassembleData.OpCode = "bsr";
                                        if (_prefix2.Contains(0x66))
                                            LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                        else
                                            LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);


                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xbe:
                                {
                                    description = "move with sign-extension";
                                    LastDisassembleData.OpCode = "movsx";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 2, ref last, 8, 0, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 2, ref last, 8, 0, ATmrPos.Right);



                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0xbf:
                                {
                                    description = "move with sign-extension";
                                    LastDisassembleData.OpCode = "movsx";
                                    LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, 16, 0, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0xc0:
                                {
                                    description = "exchange and add";
                                    LastDisassembleData.OpCode = "xadd";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last) + R8(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0xc1:
                                {
                                    description = "exchange and add";
                                    LastDisassembleData.OpCode = "xadd";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last) + R16(memory[2]);
                                    else

                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last) + R32(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0xc2:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0xf2))
                                    {

                                        description = "compare scalar dpuble-precision floating-point values";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vcmpsd";
                                        else
                                            LastDisassembleData.OpCode = "cmpsd";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, 128, 0, ATmrPos.Right);

                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + IntToHexSigned(LastDisassembleData.ParameterValue, 2);
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {
                                        description = "packed single-fp compare";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vcmpss";
                                        else
                                            LastDisassembleData.OpCode = "cmpss";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, 128, 0, ATmrPos.Right);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + IntToHexSigned(LastDisassembleData.ParameterValue, 2);
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        if (_prefix2.Contains(0x66))
                                        {
                                            description = "compare packed double-precision floating-point values";
                                            if (_hasVex)
                                                LastDisassembleData.OpCode = "vcmppd";
                                            else
                                                LastDisassembleData.OpCode = "cmppd";
                                            LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, 128, 0, ATmrPos.Right);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + IntToHexSigned(LastDisassembleData.ParameterValue, 2);
                                            offset = (UIntPtr)(offset.ToUInt64() + last);
                                        }
                                        else
                                        {
                                            description = "packed single-fp compare";
                                            if (_hasVex)
                                                LastDisassembleData.OpCode = "vcmpps";
                                            else
                                                LastDisassembleData.OpCode = "cmpps";
                                            LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, 128, 0, ATmrPos.Right);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + IntToHexSigned(LastDisassembleData.ParameterValue, 2);
                                            LastDisassembleData.DataSize = 4;
                                            offset = (UIntPtr)(offset.ToUInt64() + last);
                                        }
                                    }
                                }
                                break;

                            case 0xc3:
                                {
                                    description = "store doubleword using non-temporal hint";
                                    LastDisassembleData.OpCode = "movnti";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last) + R32(memory[2]);
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                }
                                break;

                            case 0xc4:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "insert word";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpinsrw";
                                        else
                                            LastDisassembleData.OpCode = "pinsrw";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned(LastDisassembleData.ParameterValue, 2);
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        description = "insert word";
                                        LastDisassembleData.OpCode = "pinsrw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned(LastDisassembleData.ParameterValue, 2);
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;

                            case 0xc5:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "extract word";
                                        LastDisassembleData.OpCode = "pextrw";
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned(LastDisassembleData.ParameterValue, 2);
                                        offset += 3;
                                    }
                                    else
                                    {
                                        description = "extract word";
                                        LastDisassembleData.OpCode = "pextrw";
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned(LastDisassembleData.ParameterValue, 2);
                                        offset += 3;
                                    }
                                }
                                break;

                            case 0xc6:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "shuffle double-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vshufpd";
                                        else
                                            LastDisassembleData.OpCode = "shufpd";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + IntToHexSigned(LastDisassembleData.ParameterValue, 2);
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        description = "shuffle single-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vshufps";
                                        else
                                            LastDisassembleData.OpCode = "shufps";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + IntToHexSigned(LastDisassembleData.ParameterValue, 2);
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;

                            case 0xc7:
                                {
                                    switch (GetReg(memory[2]))
                                    {
                                        case 1:
                                            {
                                                description = "compare and exchange 8 bytes";
                                                LastDisassembleData.OpCode = "cmpxchg8b";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 3:
                                            {
                                                description = "restore processor extended status supervisor";
                                                if (RexW)
                                                    LastDisassembleData.OpCode = "xrstors64";
                                                else
                                                    LastDisassembleData.OpCode = "xrstors";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 4:
                                            {
                                                description = "save processor extended state with compaction";
                                                if (RexW)
                                                    LastDisassembleData.OpCode = "xsavec";
                                                else
                                                    LastDisassembleData.OpCode = "xsavec64";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 5:
                                            {
                                                description = "save processor extended state supervisor";
                                                if (RexW)
                                                    LastDisassembleData.OpCode = "xsaves";
                                                else
                                                    LastDisassembleData.OpCode = "xsaves64";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;


                                        case 6:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (GetMod(memory[2]) == 3)  //reg
                                                    {
                                                        description = "read random numer";
                                                        LastDisassembleData.OpCode = "rdrand";
                                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                    else
                                                    {
                                                        description = "copy vmcs data to vmcs region in memory";
                                                        LastDisassembleData.OpCode = "vmclear";
                                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);

                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                                else
                                                if (_prefix2.Contains(0xf3))
                                                {
                                                    description = "enter vmx root operation";
                                                    LastDisassembleData.OpCode = "vmxon";
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);

                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    //check if it's a memory or register access
                                                    //if register it's rdrand else vmptrld
                                                    if (GetMod(memory[2]) == 3)  //reg
                                                    {
                                                        description = "read random numer";
                                                        LastDisassembleData.OpCode = "rdrand";
                                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                    else
                                                    {
                                                        description = "loads the current vmcs pointer from memory";
                                                        LastDisassembleData.OpCode = "vmptrld";
                                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);

                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }


                                                }
                                            }
                                            break;

                                        case 7:
                                            {
                                                if (GetMod(memory[2]) == 3)  //reg
                                                {
                                                    description = "read random SEED";
                                                    LastDisassembleData.OpCode = "rdseed";
                                                    if (_prefix2.Contains(0x66))
                                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last);
                                                    else
                                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);

                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    description = "stores the current vmcs pointer into memory";
                                                    LastDisassembleData.OpCode = "vmptrst";
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);

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
                                    LastDisassembleData.OpCode = "bswap";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = Rd16((Byte)(memory[1] - 0xc8));
                                    else
                                        LastDisassembleData.Parameters = Rd((Byte)(memory[1] - 0xc8));

                                    offset += 1;
                                }
                                break;

                            case 0xd0:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "Packed Double-FP Add/Subtract";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vaddsubpd";
                                        else
                                            LastDisassembleData.OpCode = "addsubpd";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf2))
                                    {
                                        description = "Packed Single-FP Add/Subtract";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vaddsubps";
                                        else
                                            LastDisassembleData.OpCode = "addsubps";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xd1:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed shift right logical";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpsrlw";
                                        else
                                            LastDisassembleData.OpCode = "psrlw";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed shift right logical";
                                        LastDisassembleData.OpCode = "psrlw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xd2:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed shift right logical";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpsrld";
                                        else
                                            LastDisassembleData.OpCode = "psrld";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed shift right logical";
                                        LastDisassembleData.OpCode = "psrld";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xd3:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed shift right logical";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpsrlq";
                                        else
                                            LastDisassembleData.OpCode = "psrlq";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed shift right logical";
                                        LastDisassembleData.OpCode = "psrlq";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xd4:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "add packed quadword integers";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpaddq";
                                        else
                                            LastDisassembleData.OpCode = "paddq";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "add packed quadword integers";
                                        LastDisassembleData.OpCode = "paddq";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;


                            case 0xd5:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed multiply low";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpmullw";
                                        else
                                            LastDisassembleData.OpCode = "pmullw";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed multiply low";
                                        LastDisassembleData.OpCode = "pmullw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xd6:
                                {
                                    if (_prefix2.Contains(0xf2))
                                    {

                                        description = "move low quadword from xmm to mmx technology register";
                                        LastDisassembleData.OpCode = "movdq2q";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {

                                        description = "move low quadword from xmm to mmx technology register";
                                        LastDisassembleData.OpCode = "movq2dq";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "move low quadword from xmm to mmx technology register";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovq";
                                        else
                                            LastDisassembleData.OpCode = "movq";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last) + Xmm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "move quadword from mmx technology to xmm register";
                                        LastDisassembleData.OpCode = "movq2dq";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last) + Mm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }

                                }
                                break;


                            case 0xd7:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "move byte mask to integer";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpmovmskb";
                                        else
                                            LastDisassembleData.OpCode = "pmovmskb";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "move byte mask to integer";
                                        LastDisassembleData.OpCode = "pmovmskb";
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xd8:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed subtract unsigned with saturation";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpsubusb";
                                        else
                                            LastDisassembleData.OpCode = "psubusb";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed subtract unsigned with saturation";
                                        LastDisassembleData.OpCode = "psubusb";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xd9:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed subtract unsigned with saturation";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpsubusw";
                                        else
                                            LastDisassembleData.OpCode = "psubusw";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed subtract unsigned with saturation";
                                        LastDisassembleData.OpCode = "psubusw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xda:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed unsigned integer byte minimum";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpminub";
                                        else
                                            LastDisassembleData.OpCode = "pminub";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed unsigned integer byte minimum";
                                        LastDisassembleData.OpCode = "pminub";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xdb:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "logical and";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpand";
                                        else
                                            LastDisassembleData.OpCode = "pand";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "logical and";
                                        LastDisassembleData.OpCode = "pand";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xdc:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed add unsigned with saturation";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpaddusb";
                                        else
                                            LastDisassembleData.OpCode = "paddusb";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed add unsigned with saturation";
                                        LastDisassembleData.OpCode = "paddusb";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xdd:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed add unsigned with saturation";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpaddusw";
                                        else
                                            LastDisassembleData.OpCode = "paddusw";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed add unsigned with saturation";
                                        LastDisassembleData.OpCode = "paddusw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xde:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed unsigned integer byte maximum";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpmaxub";
                                        else
                                            LastDisassembleData.OpCode = "pmaxub";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Left);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed unsigned integer byte maximum";
                                        LastDisassembleData.OpCode = "pmaxub";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xdf:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "logical and not";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpandn";
                                        else
                                            LastDisassembleData.OpCode = "pandn";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "logical and not";
                                        LastDisassembleData.OpCode = "pandn";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xe0:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed average";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpavgb";
                                        else
                                            LastDisassembleData.OpCode = "pavgb";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed average";
                                        LastDisassembleData.OpCode = "pavgb";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xe1:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed shift right arithmetic";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpsraw";
                                        else
                                            LastDisassembleData.OpCode = "psraw";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed shift right arithmetic";
                                        LastDisassembleData.OpCode = "psraw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xe2:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed shift left logical";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpsrad";
                                        else
                                            LastDisassembleData.OpCode = "psrad";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed shift left logical";
                                        LastDisassembleData.OpCode = "psrad";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xe3:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed average";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpavgw";
                                        else
                                            LastDisassembleData.OpCode = "pavgw";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed average";
                                        LastDisassembleData.OpCode = "pavgw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xe4:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed multiply high unsigned";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpmulhuw";
                                        else
                                            LastDisassembleData.OpCode = "pmulhuw";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed multiply high unsigned";
                                        LastDisassembleData.OpCode = "pmulhuw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xe5:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed multiply high";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpmulhw";
                                        else
                                            LastDisassembleData.OpCode = "pmulhw";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed multiply high";
                                        LastDisassembleData.OpCode = "pmulhw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xe6:
                                {
                                    if (_prefix2.Contains(0xf2))
                                    {

                                        description = "convert two packed signed dwords from param2 to two packed dp-floating point values in param1";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vcvtpd2dq";
                                        else
                                            LastDisassembleData.OpCode = "cvtpd2dq";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {

                                        description = "convert two packed signed dwords from param2 to two packed dp-floating point values in param1";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vcvtdq2pd";
                                        else
                                            LastDisassembleData.OpCode = "cvtdq2pd";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ',';
                                        _opCodeFlags.L = false;
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (_prefix2.Contains(0x66))
                                        {
                                            description = "convert with truncation packed double-precision floating-point values to packed doubleword integers";
                                            if (_hasVex)
                                                LastDisassembleData.OpCode = "vcvttpd2dq";
                                            else
                                                LastDisassembleData.OpCode = "cvttpd2dq";

                                            _opCodeFlags.SkipExtraReg = true;
                                            LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                }
                                break;

                            case 0xe7:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "movntdq";
                                        else
                                            LastDisassembleData.OpCode = "vmovntdq";

                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last) + Xmm(memory[2]);
                                        description = "move double quadword using non-temporal hint";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "movntq";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 3, ref last) + Mm(memory[2]);
                                        description = "move 64 bits non temporal";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xe8:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed subtract with saturation";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpsubsb";
                                        else
                                            LastDisassembleData.OpCode = "psubsb";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed subtract with saturation";
                                        LastDisassembleData.OpCode = "psubsb";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xe9:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed subtract with saturation";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpsubsw";
                                        else
                                            LastDisassembleData.OpCode = "psubsw";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed subtract with saturation";
                                        LastDisassembleData.OpCode = "psubsw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xea:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed signed integer word minimum";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpminsw";
                                        else
                                            LastDisassembleData.OpCode = "pminsw";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed signed integer word minimum";
                                        LastDisassembleData.OpCode = "pminsw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xeb:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "bitwise logical or";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpor";
                                        else
                                            LastDisassembleData.OpCode = "por";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "bitwise logical or";
                                        LastDisassembleData.OpCode = "por";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xec:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed add with saturation";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpaddsb";
                                        else
                                            LastDisassembleData.OpCode = "paddsb";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed add with saturation";
                                        LastDisassembleData.OpCode = "paddsb";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xed:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed add with saturation";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpaddsw";
                                        else
                                            LastDisassembleData.OpCode = "paddsw";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed add with saturation";
                                        LastDisassembleData.OpCode = "paddsw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xee:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed signed integer word maximum";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpmaxsw";
                                        else
                                            LastDisassembleData.OpCode = "pmaxsw";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed signed integer word maximum";
                                        LastDisassembleData.OpCode = "pmaxsw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xef:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "logical exclusive or";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpxor";
                                        else
                                            LastDisassembleData.OpCode = "pxor";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "logical exclusive or";
                                        LastDisassembleData.OpCode = "pxor";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xf0:
                                {
                                    if (_prefix2.Contains(0xf2))
                                    {
                                        description = "load unaligned integer 128 bits";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vlddqu";
                                        else
                                            LastDisassembleData.OpCode = "lddqu";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                        offset += 1;
                                }
                                break;


                            case 0xf1:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed shift left logical";
                                        LastDisassembleData.OpCode = "psllw";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed shift left logical";
                                        LastDisassembleData.OpCode = "psllw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xf2:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed shift left logical";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpslld";
                                        else
                                            LastDisassembleData.OpCode = "pslld";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed shift left logical";
                                        LastDisassembleData.OpCode = "pslld";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xf3:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed shift left logical";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpsllq";
                                        else
                                            LastDisassembleData.OpCode = "psllq";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed shift left logical";
                                        LastDisassembleData.OpCode = "psllq";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xf4:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "multiply packed unsigned doubleword integers";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "pmuludq";
                                        else
                                            LastDisassembleData.OpCode = "vpmuludq";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "multiply packed unsigned doubleword integers";
                                        LastDisassembleData.OpCode = "pmuludq";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;


                            case 0xf5:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed multiply and add";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpmaddwd";
                                        else
                                            LastDisassembleData.OpCode = "pmaddwd";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed multiply and add";
                                        LastDisassembleData.OpCode = "pmaddwd";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xf6:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed sum of absolute differences";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpsadbw";
                                        else
                                            LastDisassembleData.OpCode = "psadbw";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed sum of absolute differences";
                                        LastDisassembleData.OpCode = "psadbw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xf7:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "store selected bytes of double quadword";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmaskmovdqu";
                                        else
                                            LastDisassembleData.OpCode = "maskmovdqu";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "byte mask write";
                                        LastDisassembleData.OpCode = "maskmovq";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xf8:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed subtract";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpsubb";
                                        else
                                            LastDisassembleData.OpCode = "psubb";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed subtract";
                                        LastDisassembleData.OpCode = "psubb";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xf9:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed subtract";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpsubw";
                                        else
                                            LastDisassembleData.OpCode = "psubw";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed subtract";
                                        LastDisassembleData.OpCode = "psubw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xfa:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed subtract";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpsubd";
                                        else
                                            LastDisassembleData.OpCode = "psubd";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed subtract";
                                        LastDisassembleData.OpCode = "psubd";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xfb:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed subtract";
                                        LastDisassembleData.OpCode = "psubq";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed subtract";
                                        LastDisassembleData.OpCode = "psubq";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xfc:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed add";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpaddb";
                                        else
                                            LastDisassembleData.OpCode = "paddb";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed add";
                                        LastDisassembleData.OpCode = "paddb";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xfd:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed add";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpaddw";
                                        else
                                            LastDisassembleData.OpCode = "paddw";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed add";
                                        LastDisassembleData.OpCode = "paddw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0xfe:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed add";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpaddd";
                                        else
                                            LastDisassembleData.OpCode = "paddd";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed add";
                                        LastDisassembleData.OpCode = "paddd";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

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
