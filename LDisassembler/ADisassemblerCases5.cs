using System;
using Sputnik.LBinary;
using Sputnik.LMarshal;
using SputnikAsm.LDisassembler.LEnums;
using SputnikAsm.LUtils;

namespace SputnikAsm.LDisassembler
{
    public static class ADisassemblerCases5
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
                case 0xc1:
                    {
                        switch (d.GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "rol";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "rotate 16 bits left " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "rol";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "rotate 32 bits left " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;
                            case 1:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "ror";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "rotate 16 bits right " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "ror";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "rotate 32 bits right " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;
                            case 2:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "rcl";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "rotate 17 bits left " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "rcl";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "rotate 33 bits left " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;
                            case 3:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "rcr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "rotate 17 bits right " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "rcr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "rotate 33 bits right " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;
                            case 4:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "shl";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "multiply by 2 " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "shl";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "multiply by 2 " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;
                            case 5:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "shr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "unsigned divide by 2 " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "shr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "unsigned divide by 2 " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;
                            case 7:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "sar";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "signed divide by 2 " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "sar";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "signed divide by 2 " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case 0xc2:
                    {
                        var wordptr = memory.ToIntPtr(1).ReadUInt16();
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.OpCode = "ret";
                        d.LastDisassembleData.IsRet = true;
                        d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)wordptr, 4);
                        offset += 2;
                        description = "near return to calling procedure and pop " + (d.LastDisassembleData.ParameterValue) + " bytes from stack";
                    }
                    break;
                case 0xc3:
                    {
                        description = "near return to calling procedure";
                        d.LastDisassembleData.OpCode = "ret";
                        d.LastDisassembleData.IsRet = true;
                    }
                    break;
                case 0xc4:
                    {
                        if (d.SymbolHandler.Process.IsX64 == false)
                        {
                            description = "load far pointer";
                            d.LastDisassembleData.OpCode = "les";
                            if (d.Prefix2.Contains(0x66))
                                d.LastDisassembleData.Parameters = d.R16(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 1, ref last, ATmrPos.Right);
                            else
                                d.LastDisassembleData.Parameters = d.R32(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 0, ref last, ATmrPos.Right);
                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                        }
                    }
                    break;
                case 0xc5:
                    {
                        if (d.SymbolHandler.Process.IsX64 == false)
                        {
                            description = "load far pointer";
                            d.LastDisassembleData.OpCode = "lds";
                            if (d.Prefix2.Contains(0x66))
                                d.LastDisassembleData.Parameters = d.R16(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 1, ref last, ATmrPos.Right);
                            else
                                d.LastDisassembleData.Parameters = d.R32(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 0, ref last, ATmrPos.Right);
                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                        }
                    }
                    break;
                case 0xc6:
                    {
                        if (memory[1] == 0xf8)
                        {
                            offset += 1;
                            d.LastDisassembleData.OpCode = "xabort";
                            description = "transactional abort";
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[2];
                            d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                            d.LastDisassembleData.SeparatorCount += 1;
                            d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory[2], 2);
                        }
                        else
                            switch (d.GetReg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "copy memory";
                                        d.LastDisassembleData.OpCode = "mov";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    break;
                                default:
                                    {
                                        description = "not defined by the intel documentation";
                                        d.LastDisassembleData.OpCode = "db";
                                        d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory[0], 2);
                                    }
                                    break;
                            }
                    }
                    break;
                case 0xc7:
                    {
                        if (memory[1] == 0xf8)
                        {
                            description = "Transactional Begin";
                            d.LastDisassembleData.OpCode = "xbegin";
                            if (d.MarkIpRelativeInstructions)
                            {
                                d.LastDisassembleData.RipRelative = 1;
                                d.RipRelative = true;
                            }
                            offset += 4;
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                            if (d.Is64Bit)
                                d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                            else
                                d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                            d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                            d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                            d.LastDisassembleData.SeparatorCount += 1;
                        }
                        else
                            switch (d.GetReg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "copy memory";
                                        if (d.Prefix2.Contains(0x66))
                                        {
                                            d.LastDisassembleData.OpCode = "mov";
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                            var wordptr = memory.ReadUInt16((int)last);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)wordptr, 4);
                                            offset = (UIntPtr)(offset.ToUInt64() + last + 1);
                                        }
                                        else
                                        {
                                            d.LastDisassembleData.OpCode = "mov";
                                            if (d.RexW)
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                            else
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                            var dwordptr = memory.ReadUInt32((int)last);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                                            if (d.RexW)
                                                d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)dwordptr, 8);
                                            else
                                                d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)dwordptr, 8);
                                            offset = (UIntPtr)(offset.ToUInt64() + last + 3);
                                        }
                                    }
                                    break;
                                default:
                                    {
                                        description = "not defined by the intel documentation";
                                        d.LastDisassembleData.OpCode = "db";
                                        d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory[0], 2);
                                    }
                                    break;
                            }
                    }
                    break;
                case 0xc8:
                    {
                        description = "make stack frame for procedure parameters";
                        var wordptr = memory.ToIntPtr(1).ReadUInt16();
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 3;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.OpCode = "enter";
                        d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)wordptr, 4) + ',' + d.IntToHexSigned((UIntPtr)memory[3], 2);
                        offset += 3;
                    }
                    break;
                case 0xc9:
                    {
                        description = "high level procedure exit";
                        d.LastDisassembleData.OpCode = "leave";
                    }
                    break;
                case 0xca:
                    {
                        description = "far return to calling procedure and pop 2 bytes from stack";
                        var wordptr = memory.ToIntPtr(1).ReadUInt16();
                        d.LastDisassembleData.OpCode = "ret";
                        d.LastDisassembleData.IsRet = true;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)wordptr, 4);
                        offset += 2;
                    }
                    break;
                case 0xcb:
                    {
                        description = "far return to calling procedure";
                        d.LastDisassembleData.OpCode = "ret";
                        d.LastDisassembleData.IsRet = true;
                    }
                    break;
                case 0xcc:
                    {
                        //should not be shown if its being debugged using int 3'
                        description = "call to interrupt procedure-3:trap to debugger";
                        d.LastDisassembleData.OpCode = "int 3";
                    }
                    break;
                case 0xcd:
                    {
                        description = "call to interrupt procedure";
                        d.LastDisassembleData.OpCode = "int";
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory[1], 2);
                        offset += 1;
                    }
                    break;
                case 0xce:
                    {
                        description = "call to interrupt procedure-4:if overflow flag=1";
                        d.LastDisassembleData.OpCode = "into";
                    }
                    break;
                case 0xcf:
                    {
                        description = "interrupt return";
                        if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.OpCode = "iret";
                        else
                        {
                            if (d.RexW)
                                d.LastDisassembleData.OpCode = "iretq";
                            else
                                d.LastDisassembleData.OpCode = "iretd";
                        }
                    }
                    break;
                case 0xd0:
                    {
                        switch (d.GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "rotate eight bits left once";
                                    d.LastDisassembleData.OpCode = "rol";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8) + '1';
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 1:
                                {
                                    description = "rotate eight bits right once";
                                    d.LastDisassembleData.OpCode = "ror";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8) + '1';
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 2:
                                {
                                    description = "rotate nine bits left once";
                                    d.LastDisassembleData.OpCode = "rcl";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8) + '1';
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 3:
                                {
                                    description = "rotate nine bits right once";
                                    d.LastDisassembleData.OpCode = "rcr";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8) + '1';
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 4:
                                {
                                    description = "multiply by 2, once";
                                    d.LastDisassembleData.OpCode = "shl";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8) + '1';
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 5:
                                {
                                    description = "unsigned divide by 2, once";
                                    d.LastDisassembleData.OpCode = "shr";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8) + '1';
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 6:
                                {
                                    description = "not defined by the intel documentation";
                                    d.LastDisassembleData.OpCode = "db";
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory[0], 2) + ' ' + d.IntToHexSigned((UIntPtr)memory[1], 2);
                                }
                                break;
                            case 7:
                                {
                                    description = "signed divide by 2, once";
                                    d.LastDisassembleData.OpCode = "sar";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8) + '1';
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                        }
                    }
                    break;
                case 0xd1:
                    {
                        switch (d.GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "rotate 16 bits left once";
                                        d.LastDisassembleData.OpCode = "rol";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "rotate 32 bits left once";
                                        d.LastDisassembleData.OpCode = "rol";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 1:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "rotate 16 bits right once";
                                        d.LastDisassembleData.OpCode = "ror";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "rotate 32 bits right once";
                                        d.LastDisassembleData.OpCode = "ror";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 2:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "rotate 17 bits left once";
                                        d.LastDisassembleData.OpCode = "rcl";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "rotate 33 bits left once";
                                        d.LastDisassembleData.OpCode = "rcl";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 3:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "rotate 17 bits right once";
                                        d.LastDisassembleData.OpCode = "rcr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "rotate 33 bits right once";
                                        d.LastDisassembleData.OpCode = "rcr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 4:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "multiply by 2, once";
                                        d.LastDisassembleData.OpCode = "shl";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "multiply by 2, once";
                                        d.LastDisassembleData.OpCode = "shl";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 5:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "unsigned divide by 2, once";
                                        d.LastDisassembleData.OpCode = "shr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unsigned divide by 2, once";
                                        d.LastDisassembleData.OpCode = "shr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 6:
                                {
                                    description = "undefined by the intel documentation";
                                    d.LastDisassembleData.OpCode = "db";
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory[0], 2);
                                }
                                break;
                            case 7:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "signed divide by 2, once";
                                        d.LastDisassembleData.OpCode = "sar";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "signed divide by 2, once";
                                        d.LastDisassembleData.OpCode = "sar";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case 0xd2:
                    {
                        switch (d.GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "rotate eight bits left cl times";
                                    d.LastDisassembleData.OpCode = "rol";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8) + d.ColorReg + "cl" + d.EndColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 1:
                                {
                                    description = "rotate eight bits right cl times";
                                    d.LastDisassembleData.OpCode = "ror";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8) + d.ColorReg + "cl" + d.EndColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 2:
                                {
                                    description = "rotate nine bits left cl times";
                                    d.LastDisassembleData.OpCode = "rcl";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8) + d.ColorReg + "cl" + d.EndColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 3:
                                {
                                    description = "rotate nine bits right cl times";
                                    d.LastDisassembleData.OpCode = "rcr";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8) + d.ColorReg + "cl" + d.EndColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 4:
                                {
                                    description = "multiply by 2, cl times";
                                    d.LastDisassembleData.OpCode = "shl";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8) + d.ColorReg + "cl" + d.EndColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 5:
                                {
                                    description = "unsigned divide by 2, cl times";
                                    d.LastDisassembleData.OpCode = "shr";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8) + d.ColorReg + "cl" + d.EndColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 6:
                                {
                                    description = "multiply by 2, cl times";
                                    d.LastDisassembleData.OpCode = "shl";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8) + d.ColorReg + "cl" + d.EndColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 7:
                                {
                                    description = "signed divide by 2, cl times";
                                    d.LastDisassembleData.OpCode = "sar";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8) + d.ColorReg + "cl" + d.EndColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                        }
                    }
                    break;
                case 0xd3:
                    {
                        switch (d.GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "rotate 16 bits left cl times";
                                        d.LastDisassembleData.OpCode = "rol";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16) + d.ColorReg + "cl" + d.EndColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "rotate 32 bits left cl times";
                                        d.LastDisassembleData.OpCode = "rol";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + d.ColorReg + "cl" + d.EndColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 1:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "rotate 16 bits right cl times";
                                        d.LastDisassembleData.OpCode = "ror";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16) + d.ColorReg + "cl" + d.EndColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "rotate 32 bits right cl times";
                                        d.LastDisassembleData.OpCode = "ror";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + d.ColorReg + "cl" + d.EndColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 2:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "rotate 17 bits left cl times";
                                        d.LastDisassembleData.OpCode = "rcl";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16) + d.ColorReg + "cl" + d.EndColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "rotate 33 bits left cl times";
                                        d.LastDisassembleData.OpCode = "rcl";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + d.ColorReg + "cl" + d.EndColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 3:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "rotate 17 bits right cl times";
                                        d.LastDisassembleData.OpCode = "rcr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16) + d.ColorReg + "cl" + d.EndColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "rotate 33 bits right cl times";
                                        d.LastDisassembleData.OpCode = "rcr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + d.ColorReg + "cl" + d.EndColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 4:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "multiply by 2, cl times";
                                        d.LastDisassembleData.OpCode = "shl";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16) + d.ColorReg + "cl" + d.EndColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "multiply by 2, cl times";
                                        d.LastDisassembleData.OpCode = "shl";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + d.ColorReg + "cl" + d.EndColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 5:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "unsigned divide by 2, cl times";
                                        d.LastDisassembleData.OpCode = "shr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16) + d.ColorReg + "cl" + d.EndColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unsigned divide by 2, cl times";
                                        d.LastDisassembleData.OpCode = "shr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + d.ColorReg + "cl" + d.EndColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 7:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "signed divide by 2, cl times";
                                        d.LastDisassembleData.OpCode = "sar";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16) + d.ColorReg + "cl" + d.EndColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "signed divide by 2, cl times";
                                        d.LastDisassembleData.OpCode = "sar";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + d.ColorReg + "cl" + d.EndColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case 0xd4:
                    {  // aam
                        offset += 1;
                        d.LastDisassembleData.OpCode = "aam";
                        description = "ascii adjust ax after multiply";
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        if (memory[1] != 0xa)
                            d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory[1], 2);
                    }
                    break;
                case 0xd5:
                    {  // aad
                        offset += 1;
                        d.LastDisassembleData.OpCode = "aad";
                        description = "ascii adjust ax before division";
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        if (memory[1] != 0xa) d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory[1], 2);
                    }
                    break;
                case 0xd7:
                    {
                        description = "table look-up translation";
                        d.LastDisassembleData.OpCode = "xlatb";
                    }
                    break;
                case 0xd8:
                    {
                        switch (d.GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    //fadd
                                    description = "add";
                                    d.LastDisassembleData.OpCode = "fadd";
                                    last = 2;
                                    if (memory[1] >= 0xc0)
                                        d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xc0) + ')';
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 1:
                                {
                                    description = "multiply";
                                    last = 2;
                                    if (memory[1] >= 0xc8)
                                    {
                                        d.LastDisassembleData.OpCode = "fmul";
                                        d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xc8) + ')';
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "fmul";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 2:
                                {
                                    description = "compare real";
                                    last = 2;
                                    if (memory[1] >= 0xd0)
                                    {
                                        d.LastDisassembleData.OpCode = "fcom";
                                        d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xd0) + ')';
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "fcom";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 3:
                                {
                                    description = "compare real and pop register stack";
                                    last = 2;
                                    if (memory[1] >= 0xd8)
                                    {
                                        d.LastDisassembleData.OpCode = "fcomp";
                                        d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xd8) + ')';
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "fcomp";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 4:
                                {
                                    description = "substract";
                                    last = 2;
                                    if (memory[1] >= 0xe0)
                                    {
                                        d.LastDisassembleData.OpCode = "fsub";
                                        d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xe0) + ')';
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "fsub";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 5:
                                {
                                    description = "reverse substract";
                                    last = 2;
                                    if (memory[1] >= 0xe8)
                                    {
                                        d.LastDisassembleData.OpCode = "fsubr";
                                        d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xe8) + ')';
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "fsubr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 6:
                                {
                                    description = "divide";
                                    last = 2;
                                    if (memory[1] >= 0xf0)
                                    {
                                        d.LastDisassembleData.OpCode = "fdiv";
                                        d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xf0) + ')';
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "fdiv";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 7:
                                {
                                    description = "reverse divide";
                                    last = 2;
                                    if (memory[1] >= 0xf8)
                                    {
                                        d.LastDisassembleData.OpCode = "fdivr";
                                        d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xf8) + ')';
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "fdivr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                        }
                    }
                    break;
                case 0xd9:
                    {
                        d.LastDisassembleData.IsFloat = true;
                        if (AMathUtils.InRange(memory[1], 0x00, 0xbf))
                        {
                            switch (d.GetReg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "load floating point value";
                                        d.LastDisassembleData.OpCode = "fld";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 2:
                                    {
                                        description = "store single";
                                        d.LastDisassembleData.OpCode = "fst";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 3:
                                    {
                                        description = "store single";
                                        d.LastDisassembleData.OpCode = "fstp";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 4:
                                    {
                                        description = "load fpu environment";
                                        d.LastDisassembleData.OpCode = "fldenv";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 5:
                                    {
                                        description = "load control word";
                                        d.LastDisassembleData.OpCode = "fldcw";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 6:
                                    {
                                        description = "store fpu environment";
                                        d.LastDisassembleData.OpCode = "fnstenv";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 7:
                                    {
                                        description = "store control word";
                                        d.LastDisassembleData.OpCode = "fnstcw";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                            }
                        }
                        else if (AMathUtils.InRange(memory[1], 0xc0, 0xc7))
                        {
                            description = "push st(i) onto the fpu register stack";
                            d.LastDisassembleData.OpCode = "fld";
                            d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xc0) + ')';
                            offset += 1;
                        }
                        else if (AMathUtils.InRange(memory[1], 0xc8, 0xcf))
                        {
                            description = "exchange register contents";
                            d.LastDisassembleData.OpCode = "fxch";
                            d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xc8) + ')';
                            offset += 1;
                        }
                        else if (AMathUtils.InRange(memory[1], 0xd9, 0xdf))
                        {
                            description = "exchange register contents";
                            d.LastDisassembleData.OpCode = "fxch";
                            d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xd9) + ')';
                            offset += 1;
                        }
                        else
                        {
                            switch (memory[1])
                            {
                                case 0xd0:
                                    {
                                        description = "no operation";
                                        d.LastDisassembleData.OpCode = "fnop";
                                        offset += 1;
                                    }
                                    break;
                                case 0xe0:
                                    {
                                        description = "change sign";
                                        d.LastDisassembleData.OpCode = "fchs";
                                        offset += 1;
                                    }
                                    break;
                                case 0xe1:
                                    {
                                        description = "absolute value";
                                        d.LastDisassembleData.OpCode = "fabs";
                                        offset += 1;
                                    }
                                    break;
                                case 0xe4:
                                    {
                                        description = "test";
                                        d.LastDisassembleData.OpCode = "ftst";
                                        offset += 1;
                                    }
                                    break;
                                case 0xe5:
                                    {
                                        description = "examine";
                                        d.LastDisassembleData.OpCode = "fxam";
                                        offset += 1;
                                    }
                                    break;
                                case 0xe8:
                                    {
                                        description = "Push +1.0 onto the FPU register stack";
                                        d.LastDisassembleData.OpCode = "fld1";
                                        offset += 1;
                                    }
                                    break;
                                case 0xe9:
                                    {
                                        description = "Push log2(10) onto the FPU register stack";
                                        d.LastDisassembleData.OpCode = "fldl2t";
                                        offset += 1;
                                    }
                                    break;
                                case 0xea:
                                    {
                                        description = "Push log2(e) onto the FPU register stack";
                                        d.LastDisassembleData.OpCode = "fldl2e";
                                        offset += 1;
                                    }
                                    break;
                                case 0xeb:
                                    {
                                        description = "Push \"pi\" onto the FPU register stackload constant";
                                        d.LastDisassembleData.OpCode = "fldpi";
                                        offset += 1;
                                    }
                                    break;
                                case 0xec:
                                    {
                                        description = "Push log10(2) onto the FPU register stack";
                                        d.LastDisassembleData.OpCode = "fldlg2";
                                        offset += 1;
                                    }
                                    break;
                                case 0xed:
                                    {
                                        description = "Push log e(2) onto the FPU register stack";
                                        d.LastDisassembleData.OpCode = "fldln2";
                                        offset += 1;
                                    }
                                    break;
                                case 0xee:
                                    {
                                        description = "Push +0.0 onto the FPU register stack";
                                        d.LastDisassembleData.OpCode = "fldz";
                                        offset += 1;
                                    }
                                    break;
                                case 0xf0:
                                    {
                                        description = "compute 2^x-1";
                                        d.LastDisassembleData.OpCode = "f2xm1";
                                        offset += 1;
                                    }
                                    break;
                                case 0xf1:
                                    {
                                        description = "compute y*log(2)x";
                                        d.LastDisassembleData.OpCode = "fyl2x";
                                        offset += 1;
                                    }
                                    break;
                                case 0xf2:
                                    {
                                        description = "partial tangent";
                                        d.LastDisassembleData.OpCode = "fptan";
                                        offset += 1;
                                    }
                                    break;
                                case 0xf3:
                                    {
                                        description = "partial arctangent";
                                        d.LastDisassembleData.OpCode = "fpatan";
                                        offset += 1;
                                    }
                                    break;
                                case 0xf4:
                                    {
                                        description = "extract exponent and significand";
                                        d.LastDisassembleData.OpCode = "fxtract";
                                        offset += 1;
                                    }
                                    break;
                                case 0xf5:
                                    {
                                        description = "partial remainder";
                                        d.LastDisassembleData.OpCode = "fprem1";
                                        offset += 1;
                                    }
                                    break;
                                case 0xf6:
                                    {
                                        description = "decrement stack-top pointer";
                                        d.LastDisassembleData.OpCode = "fdecstp";
                                        offset += 1;
                                    }
                                    break;
                                case 0xf7:
                                    {
                                        description = "increment stack-top pointer";
                                        d.LastDisassembleData.OpCode = "fincstp";
                                        offset += 1;
                                    }
                                    break;
                                case 0xf8:
                                    {
                                        description = "partial remainder";
                                        d.LastDisassembleData.OpCode = "fprem";
                                        offset += 1;
                                    }
                                    break;
                                case 0xf9:
                                    {
                                        description = "compute y*log(2)(x+1)";
                                        d.LastDisassembleData.OpCode = "fyl2xp1";
                                        offset += 1;
                                    }
                                    break;
                                case 0xfa:
                                    {
                                        description = "square root";
                                        d.LastDisassembleData.OpCode = "fsqrt";
                                        offset += 1;
                                    }
                                    break;
                                case 0xfb:
                                    {
                                        description = "sine and cosine";
                                        d.LastDisassembleData.OpCode = "fsincos";
                                        offset += 1;
                                    }
                                    break;
                                case 0xfc:
                                    {
                                        description = "round to integer";
                                        d.LastDisassembleData.OpCode = "frndint";
                                        offset += 1;
                                    }
                                    break;
                                case 0xfd:
                                    {
                                        description = "scale";
                                        d.LastDisassembleData.OpCode = "fscale";
                                        offset += 1;
                                    }
                                    break;
                                case 0xfe:
                                    {
                                        description = "sine";
                                        d.LastDisassembleData.OpCode = "fsin";
                                        offset += 1;
                                    }
                                    break;
                                case 0xff:
                                    {
                                        description = "cosine";
                                        d.LastDisassembleData.OpCode = "fcos";
                                        offset += 1;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
                case 0xda:
                    {
                        if (memory[1] < 0xbf)
                        {
                            switch (d.GetReg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "add";
                                        d.LastDisassembleData.OpCode = "fiadd";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 1:
                                    {
                                        description = "multiply";
                                        d.LastDisassembleData.OpCode = "fimul";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 2:
                                    {
                                        description = "compare integer";
                                        d.LastDisassembleData.OpCode = "ficom";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 3:
                                    {
                                        description = "compare integer";
                                        d.LastDisassembleData.OpCode = "ficomp";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 4:
                                    {
                                        description = "subtract";
                                        d.LastDisassembleData.OpCode = "fisub";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 5:
                                    {
                                        description = "reverse subtract";
                                        d.LastDisassembleData.OpCode = "fisubr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 6:
                                    {
                                        description = "divide";
                                        d.LastDisassembleData.OpCode = "fidiv";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 7:
                                    {
                                        description = "reverse divide";
                                        d.LastDisassembleData.OpCode = "fidivr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (d.GetReg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "floating-point: move if below";
                                        d.LastDisassembleData.OpCode = "fcmovb";
                                        d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xc0) + ')';
                                        offset += 1;
                                    }
                                    break;
                                case 1:
                                    {
                                        description = "floating-point: move if equal";
                                        d.LastDisassembleData.OpCode = "fcmove";
                                        d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xc8) + ')';
                                        offset += 1;
                                    }
                                    break;
                                case 2:
                                    {
                                        description = "floating-point: move if below or equal";
                                        d.LastDisassembleData.OpCode = "fcmovbe";
                                        d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xd0) + ')';
                                        offset += 1;
                                    }
                                    break;
                                case 3:
                                    {
                                        description = "floating-point: move if unordered";
                                        d.LastDisassembleData.OpCode = "fcmovu";
                                        d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xd8) + ')';
                                        offset += 1;
                                    }
                                    break;
                                case 5:
                                    {
                                        switch (memory[1])
                                        {
                                            case 0xe9:
                                                {
                                                    description = "unordered compare real";
                                                    d.LastDisassembleData.OpCode = "fucompp";
                                                    offset += 1;
                                                }
                                                break;
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                    break;
                case 0xdb:
                    {
                        if (AMathUtils.InRange(memory[1], 0x00, 0xbf))
                        {
                            switch (d.GetReg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "load integer";
                                        d.LastDisassembleData.OpCode = "fild";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 1:
                                    {
                                        description = "store integer with truncation";
                                        d.LastDisassembleData.OpCode = "fisttp";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 2:
                                    {
                                        description = "store integer";
                                        d.LastDisassembleData.OpCode = "fist";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 3:
                                    {
                                        description = "store integer";
                                        d.LastDisassembleData.OpCode = "fistp";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 5:
                                    {
                                        d.LastDisassembleData.IsFloat = true;
                                        description = "load floating point value";
                                        d.LastDisassembleData.OpCode = "fld";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 80);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 7:
                                    {
                                        d.LastDisassembleData.IsFloat = true;
                                        description = "store extended";
                                        d.LastDisassembleData.OpCode = "fstp";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 80);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                            }
                        }
                        else if (AMathUtils.InRange(memory[1], 0xc0, 0xc7))
                        {
                            description = "floating-point: move if not below";
                            d.LastDisassembleData.OpCode = "fcmovnb";
                            d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xc0) + ')';
                            offset += 1;
                        }
                        else if (AMathUtils.InRange(memory[1], 0xc8, 0xcf))
                        {
                            description = "floating-point: move if not equal";
                            d.LastDisassembleData.OpCode = "fcmovne";
                            d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xc8) + ')';
                            offset += 1;
                        }
                        else if (AMathUtils.InRange(memory[1], 0xd0, 0xd7))
                        {
                            description = "floating-point: move if not below or equal";
                            d.LastDisassembleData.OpCode = "fcmovnbe";
                            d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xd0) + ')';
                            offset += 1;
                        }
                        else if (AMathUtils.InRange(memory[1], 0xd8, 0xdf))
                        {
                            description = "floating-point: move if not unordered";
                            d.LastDisassembleData.OpCode = "fcmovnu";
                            d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xd8) + ')';
                            offset += 1;
                        }
                        else
                        {
                            switch (memory[1])
                            {
                                case 0xe2:
                                    {
                                        description = "clear exceptions";
                                        d.LastDisassembleData.OpCode = "fnclex";
                                        offset += 1;
                                    }
                                    break;
                                case 0xe3:
                                    {
                                        description = "initialize floating-point unit";
                                        d.LastDisassembleData.OpCode = "fninit";
                                        offset += 1;
                                    }
                                    break;
                                case 0xe8:
                                case 0xe9:
                                case 0xea:
                                case 0xeb:
                                case 0xec:
                                case 0xed:
                                case 0xee:
                                case 0xef:
                                    {
                                        description = "floating-point: compare real and set eflags";
                                        d.LastDisassembleData.OpCode = "fucomi";
                                        d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xe8) + ')';
                                        offset += 1;
                                    }
                                    break;
                                case 0xf0:
                                case 0xf1:
                                case 0xf2:
                                case 0xf3:
                                case 0xf4:
                                case 0xf5:
                                case 0xf6:
                                case 0xf7:
                                    {
                                        description = "floating-point: compare real and set eflags";
                                        d.LastDisassembleData.OpCode = "fcomi";
                                        d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xf0) + ')';
                                        offset += 1;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
                case 0xdc:
                    {
                        d.LastDisassembleData.IsFloat = true;
                        switch (d.GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    //fadd
                                    description = "add";
                                    last = 2;
                                    if (memory[1] >= 0xc0)
                                    {
                                        d.LastDisassembleData.OpCode = "fadd";
                                        d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xc0) + "),st(0)";
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "fadd";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 1:
                                {
                                    description = "multiply";
                                    last = 2;
                                    if (memory[1] >= 0xc8)
                                    {
                                        d.LastDisassembleData.OpCode = "fmul";
                                        d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xc8) + "),st(0)";
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "fmul";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 2:
                                {
                                    description = "compare real";
                                    last = 2;
                                    d.LastDisassembleData.OpCode = "fcom";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 3:
                                {
                                    description = "compare real";
                                    last = 2;
                                    d.LastDisassembleData.OpCode = "fcomp";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 4:
                                {
                                    description = "subtract";
                                    last = 2;
                                    if (memory[1] >= 0xe0)
                                    {
                                        d.LastDisassembleData.OpCode = "fsubr";
                                        d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xe0) + "),st(0)";
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "fsub";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 5:
                                {
                                    description = "reverse subtract";
                                    last = 2;
                                    if (memory[1] >= 0xe8)
                                    {
                                        d.LastDisassembleData.OpCode = "fsub";
                                        d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xe8) + "),st(0)";
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "fsubr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 6:
                                {
                                    description = "divide";
                                    last = 2;
                                    if (memory[1] >= 0xf0)
                                    {
                                        d.LastDisassembleData.OpCode = "fdivr";
                                        d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xf0) + "),st(0)";
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "fdiv";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 7:
                                {
                                    description = "reverse divide";
                                    last = 2;
                                    if (memory[1] >= 0xf8)
                                    {
                                        d.LastDisassembleData.OpCode = "fdiv";
                                        d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xf8) + "),st(0)";
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "fdivr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                        }
                    }
                    break;
                case 0xdd:
                    {
                        if (AMathUtils.InRange(memory[1], 0x00, 0xbf))
                        {
                            switch (d.GetReg(memory[1]))
                            {
                                case 0:
                                    {
                                        d.LastDisassembleData.IsFloat = true;
                                        description = "load floating point value";
                                        d.LastDisassembleData.OpCode = "fld";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 1:
                                    {
                                        description = "store integer with truncation";
                                        d.LastDisassembleData.OpCode = "fisttp";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 2:
                                    {
                                        d.LastDisassembleData.IsFloat = true;
                                        description = "store double";
                                        d.LastDisassembleData.OpCode = "fst";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 3:
                                    {
                                        d.LastDisassembleData.IsFloat = true;
                                        description = "store double";
                                        d.LastDisassembleData.OpCode = "fstp";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 4:
                                    {
                                        description = "restore fpu state";
                                        d.LastDisassembleData.OpCode = "frstor";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 6:
                                    {
                                        description = "store fpu state";
                                        d.LastDisassembleData.OpCode = "fnsave";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 7:
                                    {
                                        description = "store status word";
                                        d.LastDisassembleData.OpCode = "fnstsw";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                            }
                        }
                        else if (AMathUtils.InRange(memory[1], 0xc0, 0xc7))
                        {
                            description = "free floating-point register";
                            d.LastDisassembleData.OpCode = "ffree";
                            d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xc0) + ')';
                            offset += 1;
                        }
                        else if (AMathUtils.InRange(memory[1], 0xd0, 0xd7))
                        {
                            description = "store real";
                            d.LastDisassembleData.OpCode = "fst";
                            d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xd0) + ')';
                            offset += 1;
                        }
                        else if (AMathUtils.InRange(memory[1], 0xd8, 0xdf))
                        {
                            description = "store real";
                            d.LastDisassembleData.OpCode = "fstp";
                            d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xd8) + ')';
                            offset += 1;
                        }
                        else if (AMathUtils.InRange(memory[1], 0xe0, 0xe7))
                        {
                            description = "unordered compare real";
                            d.LastDisassembleData.OpCode = "fucom";
                            d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xe0) + ')';
                            offset += 1;
                        }
                        else if (AMathUtils.InRange(memory[1], 0xe8, 0xef))
                        {
                            description = "unordered compare real";
                            d.LastDisassembleData.OpCode = "fucomp";
                            d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xe8) + ')';
                            offset += 1;
                        }
                        else
                        {
                            d.LastDisassembleData.OpCode = "db";
                            d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory[0], 2);
                        }
                    }
                    break;
                case 0xde:
                    {
                        switch (d.GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    //faddp
                                    description = "add and pop";
                                    last = 2;
                                    if (memory[1] == 0xc1) d.LastDisassembleData.OpCode = "faddp";
                                    else
                                    if (memory[1] >= 0xc0)
                                    {
                                        d.LastDisassembleData.OpCode = "faddp";
                                        d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xc0) + "),st(0)";
                                    }
                                    else
                                    {
                                        description = "add";
                                        d.LastDisassembleData.OpCode = "fiadd";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 1:
                                {
                                    description = "multiply";
                                    last = 2;
                                    if (memory[1] >= 0xc8)
                                    {
                                        d.LastDisassembleData.OpCode = "fmulp";
                                        d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xc8) + "),st(0)";
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "fimul";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 2:
                                {
                                    description = "compare integer";
                                    last = 2;
                                    d.LastDisassembleData.OpCode = "ficom";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 3:
                                {
                                    if (memory[1] < 0xc0)
                                    {
                                        description = "compare integer";
                                        d.LastDisassembleData.OpCode = "ficomp";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    if (memory[1] == 0xd9)
                                    {
                                        description = "compare real and pop register stack twice";
                                        d.LastDisassembleData.OpCode = "fcompp";
                                        offset += 1;
                                    }
                                }
                                break;
                            case 4:
                                {
                                    description = "subtract";
                                    last = 2;
                                    if (memory[1] >= 0xe0)
                                    {
                                        d.LastDisassembleData.OpCode = "fsubrp";
                                        d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xe0) + "),st(0)";
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "fisub";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 5:
                                {
                                    description = "reverse divide";
                                    last = 2;
                                    if (memory[1] >= 0xe8)
                                    {
                                        description = "subtract and pop from stack";
                                        d.LastDisassembleData.OpCode = "fsubp";
                                        d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xe8) + "),st(0)";
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "fisubr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 6:
                                {
                                    description = "reverse divide";
                                    last = 2;
                                    if (memory[1] >= 0xf0)
                                    {
                                        d.LastDisassembleData.OpCode = "fdivrp";
                                        d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xf0) + "),st(0)";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "divide";
                                        d.LastDisassembleData.OpCode = "fidiv";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 7:
                                {
                                    description = "divide";
                                    last = 2;
                                    if (memory[1] >= 0xf8)
                                    {
                                        d.LastDisassembleData.OpCode = "fdivp";
                                        d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xf8) + "),st(0)";
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "fdivr";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                        }
                    }
                    break;
                case 0xdf:
                    {
                        if (AMathUtils.InRange(memory[1], 0xc0, 0xc7))
                        {
                            description = "free floating-point register and pop (might not work)";
                            d.LastDisassembleData.OpCode = "ffreep";
                            d.LastDisassembleData.Parameters = "st(" + (memory[1] - 0xc0) + ')';
                            offset += 1;
                        }
                        else
                            switch (d.GetReg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "load integer";
                                        d.LastDisassembleData.OpCode = "fild";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 16);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 1:
                                    {
                                        description = "store integer with truncation";
                                        d.LastDisassembleData.OpCode = "fisttp";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 16);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 2:
                                    {
                                        description = "store integer";
                                        d.LastDisassembleData.OpCode = "fist";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 16);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 3:
                                    {
                                        description = "store integer";
                                        d.LastDisassembleData.OpCode = "fistp";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 16);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 4:
                                    {
                                        description = "load binary coded decimal";
                                        last = 2;
                                        if (memory[1] >= 0xe0)
                                        {
                                            d.LastDisassembleData.OpCode = "fnstsw";
                                            d.LastDisassembleData.Parameters = d.ColorReg + "ax" + d.EndColor;
                                        }
                                        else
                                        {
                                            d.LastDisassembleData.OpCode = "fbld";
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 80);
                                        }
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                case 5:
                                    {
                                        if (memory[1] < 0xc0)
                                        {
                                            description = "load integer";
                                            d.LastDisassembleData.OpCode = "fild";
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                        if (memory[1] >= 0xe8)
                                        {
                                            description = "compare real and set eflags";
                                            d.LastDisassembleData.OpCode = "fucomip";
                                            d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xe8) + ')';
                                            offset += 1;
                                        }
                                    }
                                    break;
                                case 6:
                                    {
                                        if (memory[1] >= 0xf0)
                                        {
                                            description = "compare real and set eflags";
                                            d.LastDisassembleData.OpCode = "fcomip";
                                            d.LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xf0) + ')';
                                            offset += 1;
                                        }
                                        else
                                        {
                                            description = "store bcd integer and pop";
                                            d.LastDisassembleData.OpCode = "fbstp";
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 80);
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                    break;
                                case 7:
                                    {
                                        description = "store integer";
                                        d.LastDisassembleData.OpCode = "fistp";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                                default:
                                    {
                                        d.LastDisassembleData.OpCode = "db";
                                        d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory[0], 2);
                                    }
                                    break;
                            }
                    }
                    break;
                case 0xe0:
                    {
                        description = "loop according to ecx counter";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_zf) == 0;
                        d.LastDisassembleData.OpCode = "loopne";
                        offset += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                    }
                    break;
                case 0xe1:
                    {
                        description = "loop according to ecx counter";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_zf) != 0;
                        d.LastDisassembleData.OpCode = "loope";
                        offset += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                    }
                    break;
                case 0xe2:
                    {
                        description = "loop according to ecx counting";
                        d.LastDisassembleData.OpCode = "loop";
                        // todo readd me
                        //if context<>nil then
                        //d.LastDisassembleData.willJumpAccordingToContext:=context^.{$ifdef CPU64}RCX{$else}ECX{$endif}<>0;
                        d.LastDisassembleData.IsJump = true;
                        offset += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                    }
                    break;
                case 0xe3:
                    {
                        description = "jump short if cx=0";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        if (d.Prefix2.Contains(0x66))
                        {
                            d.LastDisassembleData.OpCode = "jcxz";
                            // todo readd me
                            //if context<>nil then
                            //d.LastDisassembleData.willJumpAccordingToContext:=((context^.{$ifdef CPU64}RCX{$else}ECX{$endif}) and $ffff)=0;
                        }
                        else
                        {
                            d.LastDisassembleData.OpCode = "jecxz";
                            // todo readd me
                            //if context<>nil then
                            //d.LastDisassembleData.willJumpAccordingToContext:=context^.{$ifdef CPU64}RCX{$else}ECX{$endif}=0;
                        }
                        offset += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                    }
                    break;
                case 0xe4:
                    {
                        description = "input from port";
                        d.LastDisassembleData.OpCode = "in";
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.Parameters = d.ColorReg + "al" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)memory[1], 2);
                        offset += 1;
                    }
                    break;
                case 0xe5:
                    {
                        description = "input from port";
                        d.LastDisassembleData.OpCode = "in";
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.Parameters = d.ColorReg + "ax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)memory[1], 2);
                        else d.LastDisassembleData.Parameters = d.ColorReg + "eax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)memory[1], 2);
                        offset += 1;
                    }
                    break;
                case 0xe6:
                    {
                        description = "output to port";
                        d.LastDisassembleData.OpCode = "out";
                        d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory[1], 2) + ',' + d.ColorReg + "al" + d.EndColor;
                        offset += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                    }
                    break;
                case 0xe7:
                    {
                        description = "output toport";
                        d.LastDisassembleData.OpCode = "out";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory[1], 2) + ',' + d.ColorReg + "ax" + d.EndColor;
                        else
                            d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory[1], 2) + ',' + d.ColorReg + "eax" + d.EndColor;
                        offset += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                    }
                    break;
                case 0xe8:
                    {
                        //call
                        //this time no $66 prefix because it will only run in win32
                        description = "call procedure";
                        d.LastDisassembleData.OpCode = "call";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsCall = true;
                        if (d.MarkIpRelativeInstructions)
                        {
                            d.LastDisassembleData.RipRelative = 1;
                            d.RipRelative = true;
                        }
                        offset += 4;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(1));
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(1));
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                    }
                    break;
                case 0xe9:
                    {
                        description = "jump near";
                        d.LastDisassembleData.IsJump = true;
                        if (d.Prefix2.Contains(0x66))
                        {
                            d.LastDisassembleData.OpCode = "jmp";
                            offset += 2;
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt16(1));
                            d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                        }
                        else
                        {
                            d.LastDisassembleData.OpCode = "jmp";
                            if (d.MarkIpRelativeInstructions)
                            {
                                d.LastDisassembleData.RipRelative = 1;
                                d.RipRelative = true;
                            }
                            offset += 4;
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                            if (d.Is64Bit)
                                d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt16)memory.ReadInt32(1));
                            else
                                d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt16)memory.ReadInt32(1));
                            d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                        }
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                    }
                    break;
                case 0xea:
                    {
                        description = "jump far";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 5;
                        d.LastDisassembleData.SeparatorCount += 1;
                        var wordptr = memory.ToIntPtr(5).ReadUInt16();
                        d.LastDisassembleData.OpCode = "jmp";
                        d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)wordptr, 4) + ':';
                        var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)dwordptr, 8);
                        offset += 6;
                    }
                    break;
                case 0xeb:
                    {
                        description = "jump short";
                        d.LastDisassembleData.OpCode = "jmp";
                        d.LastDisassembleData.IsJump = true;
                        offset += 1;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                    }
                    break;
                case 0xec:
                    {
                        description = "input from port";
                        d.LastDisassembleData.OpCode = "in";
                        d.LastDisassembleData.Parameters = d.ColorReg + "al" + d.EndColor + ',' + d.ColorReg + "dx" + d.EndColor;
                    }
                    break;
                case 0xed:
                    {
                        description = "input from port";
                        d.LastDisassembleData.OpCode = "in";
                        if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.Parameters = d.ColorReg + "ax" + d.EndColor + ',' + d.ColorReg + "dx" + d.EndColor;
                        else
                            d.LastDisassembleData.Parameters = d.ColorReg + "eax" + d.EndColor + ',' + d.ColorReg + "dx" + d.EndColor;
                    }
                    break;
                case 0xee:
                    {
                        description = "input from port";
                        d.LastDisassembleData.OpCode = "out";
                        d.LastDisassembleData.Parameters = d.ColorReg + "dx" + d.EndColor + ',' + d.ColorReg + "al" + d.EndColor;
                    }
                    break;
                case 0xef:
                    {
                        description = "input from port";
                        d.LastDisassembleData.OpCode = "out";
                        if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.Parameters = d.ColorReg + "dx" + d.EndColor + ',' + d.ColorReg + "ax" + d.EndColor;
                        else
                            d.LastDisassembleData.Parameters = d.ColorReg + "dx" + d.EndColor + ',' + d.ColorReg + "eax" + d.EndColor;
                    }
                    break;
                case 0xf3:
                    {
                        ;
                    }
                    break;
                case 0xf4:
                    {
                        description = "halt";
                        d.LastDisassembleData.OpCode = "hlt";
                    }
                    break;
                case 0xf5:
                    {
                        description = "complement carry flag";
                        d.LastDisassembleData.OpCode = "cmc";
                    }
                    break;
                case 0xf6:
                    {
                        switch (d.GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "logical compare";
                                    d.LastDisassembleData.OpCode = "test";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                }
                                break;
                            case 2:
                                {
                                    description = "one's complement negation";
                                    d.LastDisassembleData.OpCode = "not";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 3:
                                {
                                    description = "two's complement negation";
                                    d.LastDisassembleData.OpCode = "neg";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 4:
                                {
                                    description = "unsigned multiply";
                                    d.LastDisassembleData.OpCode = "mul";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 5:
                                {
                                    description = "signed multiply";
                                    d.LastDisassembleData.OpCode = "imul";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 6:
                                {
                                    description = "unsigned divide";
                                    d.LastDisassembleData.OpCode = "div";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 7:
                                {
                                    description = "signed divide";
                                    d.LastDisassembleData.OpCode = "idiv";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            default:
                                {
                                    d.LastDisassembleData.OpCode = "db";
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory[0], 2);
                                }
                                break;
                        }
                    }
                    break;
                case 0xf7:
                    {
                        switch (d.GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "logical compare";
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "test";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        var wordptr = memory.ReadUInt16((int)last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)wordptr, 4);
                                        offset = (UIntPtr)(offset.ToUInt64() + last + 1);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "test";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        var dwordptr = memory.ReadUInt32((int)last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                                        if (d.RexW)
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)dwordptr, 8);
                                        else
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)dwordptr, 8);
                                        offset = (UIntPtr)(offset.ToUInt64() + last + 3);
                                    }
                                }
                                break;
                            case 2:
                                {
                                    description = "one's complement negation";
                                    d.LastDisassembleData.OpCode = "not";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 3:
                                {
                                    description = "two's complement negation";
                                    d.LastDisassembleData.OpCode = "neg";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 4:
                                {
                                    description = "unsigned multiply";
                                    d.LastDisassembleData.OpCode = "mul";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 5:
                                {
                                    description = "signed multiply";
                                    d.LastDisassembleData.OpCode = "imul";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 6:
                                {
                                    description = "unsigned divide";
                                    d.LastDisassembleData.OpCode = "div";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 7:
                                {
                                    description = "signed divide";
                                    d.LastDisassembleData.OpCode = "idiv";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            default:
                                {
                                    d.LastDisassembleData.OpCode = "db";
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory[0], 2);
                                }
                                break;
                        }
                    }
                    break;
                case 0xf8:
                    {
                        description = "clear carry flag";
                        d.LastDisassembleData.OpCode = "clc";
                    }
                    break;
                case 0xf9:
                    {
                        description = "set carry flag";
                        d.LastDisassembleData.OpCode = "stc";
                    }
                    break;
                case 0xfa:
                    {
                        description = "clear interrupt flag";
                        d.LastDisassembleData.OpCode = "cli";
                    }
                    break;
                case 0xfb:
                    {
                        description = "set interrupt flag";
                        d.LastDisassembleData.OpCode = "sti";
                    }
                    break;
                case 0xfc:
                    {
                        description = "clear direction flag";
                        d.LastDisassembleData.OpCode = "cld";
                    }
                    break;
                case 0xfd:
                    {
                        description = "set direction flag";
                        d.LastDisassembleData.OpCode = "std";
                    }
                    break;
                case 0xfe:
                    {
                        switch (d.GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "increment by 1";
                                    d.LastDisassembleData.OpCode = "inc";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 1:
                                {
                                    description = "decrement by 1";
                                    d.LastDisassembleData.OpCode = "dec";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 7);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            default:
                                {
                                    d.LastDisassembleData.OpCode = "db";
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory[0], 2);
                                }
                                break;
                        }
                    }
                    break;
                case 0xff:
                    {
                        switch (d.GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "increment by 1";
                                    d.LastDisassembleData.OpCode = "inc";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 1:
                                {
                                    description = "decrement by 1";
                                    d.LastDisassembleData.OpCode = "dec";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 2:
                                {
                                    //call
                                    description = "call procedure";
                                    d.LastDisassembleData.OpCode = "call";
                                    d.LastDisassembleData.IsJump = true;
                                    d.LastDisassembleData.IsCall = true;
                                    if (memory[1] >= 0xc0)
                                    {
                                        if (d.Is64Bit)
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                        else
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                    }
                                    else
                                    {
                                        if (d.Is64Bit)
                                        {
                                            if ((memory[1] == 0x15) && (memory.ReadUInt32(2) == 2) && (memory.ReadUInt16(6) == 0x8eb))  //special 16 byte call
                                            {
                                                d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory.ReadUInt64(8), 8);
                                                d.LastDisassembleData.ParameterValue = (UIntPtr)memory.ReadUInt64(8);
                                                d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                                last += 8 + 4 + 2 + 2;
                                                d.LastDisassembleData.Separators[0] = 2;
                                                d.LastDisassembleData.Separators[1] = 2 + 4;
                                                d.LastDisassembleData.Separators[2] = 2 + 4 + 2;
                                                d.LastDisassembleData.SeparatorCount = 3;
                                            }
                                            else
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                        }
                                        else
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 3:
                                {
                                    //call
                                    description = "call procedure";
                                    d.LastDisassembleData.OpCode = "call";
                                    d.LastDisassembleData.IsJump = true;
                                    d.LastDisassembleData.IsCall = true;
                                    if (d.Is64Bit)
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 4:
                                {
                                    //jmp
                                    description = "jump near";
                                    d.LastDisassembleData.OpCode = "jmp";
                                    d.LastDisassembleData.IsJump = true;
                                    if (d.Is64Bit)
                                    {
                                        if ((memory[1] == 0x25) && (memory.ReadUInt32(2) == 0))  //special 14 byte jmp
                                        {
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory.ReadUInt64(6);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                            d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory.ReadUInt64(6), 8);
                                            last += 8 + 4 + 2;
                                            d.LastDisassembleData.Separators[0] = 2;
                                            d.LastDisassembleData.Separators[1] = 2 + 4;
                                            d.LastDisassembleData.SeparatorCount = 2;
                                        }
                                        else
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                    }
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 5:
                                {
                                    //jmp
                                    description = "jump far";
                                    d.LastDisassembleData.OpCode = "jmp far";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                    d.LastDisassembleData.IsJump = true;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 6:
                                {
                                    description = "push word or doubleword onto the stack";
                                    d.LastDisassembleData.OpCode = "push";
                                    if (d.Prefix2.Contains(0x66))
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last);
                                    else
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            default:
                                {
                                    d.LastDisassembleData.OpCode = "db";
                                    d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory[0], 2);
                                }
                                break;
                        }
                    }
                    break;
                default:
                    return false;
            }
            return true;
        }
        #endregion
    }
}
