using System;
using Sputnik.LBinary;
using Sputnik.LMarshal;
using SputnikAsm.LDisassembler.LEnums;

namespace SputnikAsm.LDisassembler
{
    public static class ADisassemblerCases4
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
                case 0x10:
                    {
                        description = "add with carry";
                        d.LastDisassembleData.OpCode = "adc";
                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last) + d.R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x11:
                    {
                        description = "add with carry";
                        d.LastDisassembleData.OpCode = "adc";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last) + d.R16(memory[1]);
                        else
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + d.R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x12:
                    {
                        description = "add with carry";
                        d.LastDisassembleData.OpCode = "adc";
                        d.LastDisassembleData.Parameters = d.R8(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8, 0, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x13:
                    {
                        description = "add with carry";
                        d.LastDisassembleData.OpCode = "adc";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.R16(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            d.LastDisassembleData.Parameters = d.R32(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 0, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x14:
                    {
                        description = "add with carry";
                        d.LastDisassembleData.OpCode = "adc";
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        d.LastDisassembleData.Parameters = d.ColorReg + "al" + d.EndColor + ',' + d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 2);
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        offset += 1;
                    }
                    break;
                case 0x15:
                    {
                        description = "add with carry";
                        d.LastDisassembleData.OpCode = "adc";
                        if (d.Prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                            d.LastDisassembleData.Parameters = d.ColorReg + "ax" + d.EndColor + ',' + d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                            if (d.RexW)
                                d.LastDisassembleData.Parameters = d.ColorReg + "rax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)d.LastDisassembleData.ParameterValue, 8);
                            else
                                d.LastDisassembleData.Parameters = d.ColorReg + "eax" + d.EndColor + ',' + d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                            offset += 4;
                        }
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                    }
                    break;
                case 0x16:
                    {
                        description = "place ss on the stack";
                        d.LastDisassembleData.OpCode = "push";
                        d.LastDisassembleData.Parameters = d.ColorReg + "ss" + d.EndColor;
                    }
                    break;
                case 0x17:
                    {
                        description = "remove ss from the stack";
                        d.LastDisassembleData.OpCode = "pop";
                        d.LastDisassembleData.Parameters = d.ColorReg + "ss" + d.EndColor;
                    }
                    break;
                case 0x18:
                    {
                        description = "integer subtraction with borrow";
                        d.LastDisassembleData.OpCode = "sbb";
                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last) + d.R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x19:
                    {
                        description = "integer subtraction with borrow";
                        d.LastDisassembleData.OpCode = "sbb";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last) + d.R16(memory[1]);
                        else
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + d.R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x1a:
                    {
                        description = "integer subtraction with borrow";
                        d.LastDisassembleData.OpCode = "sbb";
                        d.LastDisassembleData.Parameters = d.R8(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8, 0, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x1b:
                    {
                        description = "integer subtraction with borrow";
                        d.LastDisassembleData.OpCode = "sbb";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.R16(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            d.LastDisassembleData.Parameters = d.R32(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 0, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x1c:
                    {
                        description = "integer subtraction with borrow";
                        d.LastDisassembleData.OpCode = "sbb";
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.Parameters = d.ColorReg + "al" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)memory[1], 2);
                        offset += 1;
                    }
                    break;
                case 0x1d:
                    {
                        d.LastDisassembleData.OpCode = "sbb";
                        description = "integer subtraction with borrow";
                        if (d.Prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                            d.LastDisassembleData.Parameters = d.ColorReg + "ax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                            if (d.RexW)
                                d.LastDisassembleData.Parameters = d.ColorReg + "rax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)d.LastDisassembleData.ParameterValue, 8);
                            else
                                d.LastDisassembleData.Parameters = d.ColorReg + "eax" + d.EndColor + ',' + d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                            offset += 4;
                        }
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                    }
                    break;
                case 0x1e:
                    {
                        description = "place ds on the stack";
                        d.LastDisassembleData.OpCode = "push";
                        d.LastDisassembleData.Parameters = d.ColorReg + "ds" + d.EndColor;
                    }
                    break;
                case 0x1f:
                    {
                        description = "remove ds from the stack";
                        d.LastDisassembleData.OpCode = "pop";
                        d.LastDisassembleData.Parameters = d.ColorReg + "ds" + d.EndColor;
                    }
                    break;
                case 0x20:
                    {
                        description = "logical and";
                        d.LastDisassembleData.OpCode = "and";
                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last) + d.R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x21:
                    {
                        description = "logical and";
                        d.LastDisassembleData.OpCode = "and";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last) + d.R16(memory[1]);
                        else
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + d.R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x22:
                    {
                        description = "logical and";
                        d.LastDisassembleData.OpCode = "and";
                        d.LastDisassembleData.Parameters = d.R8(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 2, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x23:
                    {
                        description = "logical and";
                        d.LastDisassembleData.OpCode = "and";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.R16(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            d.LastDisassembleData.Parameters = d.R32(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 0, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x24:
                    {
                        description = "logical and";
                        d.LastDisassembleData.OpCode = "and";
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        d.LastDisassembleData.Parameters = d.ColorReg + "al" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)memory[1], 2);
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        offset += 1;
                    }
                    break;
                case 0x25:
                    {
                        description = "logical and";
                        d.LastDisassembleData.OpCode = "and";
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        if (d.Prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                            d.LastDisassembleData.Parameters = d.ColorReg + "ax" + d.EndColor + ',' + d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                            if (d.RexW)
                                d.LastDisassembleData.Parameters = d.ColorReg + "rax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)d.LastDisassembleData.ParameterValue, 8);
                            else
                                d.LastDisassembleData.Parameters = d.ColorReg + "eax" + d.EndColor + ',' + d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                            offset += 4;
                        }
                    }
                    break;
                case 0x27:
                    {
                        description = "decimal adjust al after addition";
                        d.LastDisassembleData.OpCode = "daa";
                    }
                    break;
                case 0x28:
                    {
                        description = "subtract";
                        d.LastDisassembleData.OpCode = "sub";
                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last) + d.R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x29:
                    {
                        description = "subtract";
                        d.LastDisassembleData.OpCode = "sub";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last) + d.R16(memory[1]);
                        else
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + d.R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x2a:
                    {
                        description = "subtract";
                        d.LastDisassembleData.OpCode = "sub";
                        d.LastDisassembleData.Parameters = d.R8(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 2, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x2b:
                    {
                        description = "subtract";
                        d.LastDisassembleData.OpCode = "sub";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.R16(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            d.LastDisassembleData.Parameters = d.R32(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 0, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x2c:
                    {
                        description = "subtract";
                        d.LastDisassembleData.OpCode = "sub";
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.Parameters = d.ColorReg + "al" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)memory[1], 2);
                        offset += 1;
                    }
                    break;
                case 0x2d:
                    {
                        description = "subtract";
                        d.LastDisassembleData.OpCode = "sub";
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        if (d.Prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                            d.LastDisassembleData.Parameters = d.ColorReg + "ax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                            if (d.RexW)
                                d.LastDisassembleData.Parameters = d.ColorReg + "rax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)dwordptr, 8);
                            else
                                d.LastDisassembleData.Parameters = d.ColorReg + "eax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)dwordptr, 8);
                            offset += 4;
                        }
                    }
                    break;
                case 0x2f:
                    {
                        description = "decimal adjust al after subtraction";
                        d.LastDisassembleData.OpCode = "das";
                    }
                    break;
                case 0x30:
                    {
                        description = "logical exclusive or";
                        d.LastDisassembleData.OpCode = "xor";
                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last) + d.R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x31:
                    {
                        description = "logical exclusive or";
                        d.LastDisassembleData.OpCode = "xor";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last) + d.R16(memory[1]);
                        else
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + d.R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x32:
                    {
                        description = "logical exclusive or";
                        d.LastDisassembleData.OpCode = "xor";
                        d.LastDisassembleData.Parameters = d.R8(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 2, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x33:
                    {
                        description = "logical exclusive or";
                        d.LastDisassembleData.OpCode = "xor";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.R16(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            d.LastDisassembleData.Parameters = d.R32(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 0, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x34:
                    {
                        description = "logical exclusive or";
                        d.LastDisassembleData.OpCode = "xor";
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.Parameters = d.ColorReg + "al" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)memory[1], 2);
                        offset += 1;
                    }
                    break;
                case 0x35:
                    {
                        description = "logical exclusive or";
                        d.LastDisassembleData.OpCode = "xor";
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        if (d.Prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                            d.LastDisassembleData.Parameters = d.ColorReg + "ax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                            if (d.RexW)
                                d.LastDisassembleData.Parameters = d.ColorReg + "rax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)dwordptr, 8);
                            else
                                d.LastDisassembleData.Parameters = d.ColorReg + "eax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)dwordptr, 8);
                            offset += 4;
                        }
                    }
                    break;
                case 0x37:
                    {  //aaa
                        d.LastDisassembleData.OpCode = "aaa";
                        description = "ascii adjust al after addition";
                    }
                    break;
                //---------
                case 0x38:
                    {//cmp
                        description = "compare two operands";
                        d.LastDisassembleData.OpCode = "cmp";
                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last) + d.R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x39:
                    {
                        description = "compare two operands";
                        d.LastDisassembleData.OpCode = "cmp";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last) + d.R16(memory[1]);
                        else
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + d.R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x3a:
                    {
                        description = "compare two operands";
                        d.LastDisassembleData.OpCode = "cmp";
                        d.LastDisassembleData.Parameters = d.R8(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 2, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x3b:
                    {
                        description = "compare two operands";
                        d.LastDisassembleData.OpCode = "cmp";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.R16(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            d.LastDisassembleData.Parameters = d.R32(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 0, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                //---------
                case 0x3c:
                    {
                        description = "compare two operands";
                        d.LastDisassembleData.OpCode = "cmp";
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.Parameters = d.ColorReg + "al" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)memory[1], 2);
                        offset += 1;
                    }
                    break;
                case 0x3d:
                    {
                        description = "compare two operands";
                        d.LastDisassembleData.OpCode = "cmp";
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        if (d.Prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                            d.LastDisassembleData.Parameters = d.ColorReg + "ax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                            if (d.RexW)
                                d.LastDisassembleData.Parameters = d.ColorReg + "rax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)dwordptr, 8);
                            else
                                d.LastDisassembleData.Parameters = d.ColorReg + "eax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)dwordptr, 8);
                            offset += 4;
                        }
                    }
                    break;
                //prefix bytes need fixing
                case 0x3f:
                    {  //aas
                        if (d.SymbolHandler.Process.IsX86)
                        {
                            d.LastDisassembleData.OpCode = "db";
                            d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)0x3f, 1);
                        }
                        else
                        {
                            d.LastDisassembleData.OpCode = "aas";
                            description = "ascii adjust al after subtraction";
                        }
                    }
                    break;
                case 0x40:
                case 0x41:
                case 0x42:
                case 0x43:
                case 0x44:
                case 0x45:
                case 0x46:
                case 0x47:
                    {
                        description = "increment by 1";
                        d.LastDisassembleData.OpCode = "inc";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.Rd16((Byte)(memory[0] - 0x40));
                        else
                            d.LastDisassembleData.Parameters = d.Rd((Byte)(memory[0] - 0x40));
                    }
                    break;
                case 0x48:
                case 0x49:
                case 0x4a:
                case 0x4b:
                case 0x4c:
                case 0x4d:
                case 0x4e:
                case 0x4f:
                    {
                        description = "decrement by 1";
                        d.LastDisassembleData.OpCode = "dec";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.Rd16((Byte)(memory[0] - 0x48));
                        else
                            d.LastDisassembleData.Parameters = d.Rd((Byte)(memory[0] - 0x48));
                    }
                    break;
                case 0x50:
                case 0x51:
                case 0x52:
                case 0x53:
                case 0x54:
                case 0x55:
                case 0x56:
                case 0x57:
                    {
                        description = "push word or doubleword onto the stack";
                        if (d.Is64Bit) d.OpCodeFlags.W = true;
                        d.LastDisassembleData.OpCode = "push";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.Rd16((Byte)(memory[0] - 0x50));
                        else
                            d.LastDisassembleData.Parameters = d.Rd((Byte)(memory[0] - 0x50));
                    }
                    break;
                case 0x58:
                case 0x59:
                case 0x5a:
                case 0x5b:
                case 0x5c:
                case 0x5d:
                case 0x5e:
                case 0x5f:
                    {
                        description = "pop a value from the stack";
                        if (d.Is64Bit) d.OpCodeFlags.W = true; //so rd will pick the 64-bit version
                        d.LastDisassembleData.OpCode = "pop";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.Rd16((Byte)(memory[0] - 0x58));
                        else
                            d.LastDisassembleData.Parameters = d.Rd((Byte)(memory[0] - 0x58));
                    }
                    break;
                case 0x60:
                    {
                        description = "push all general-purpose registers";
                        if (d.Is64Bit) description += " (invalid)";
                        if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.OpCode = "pusha";
                        else
                            d.LastDisassembleData.OpCode = "pushad";
                        if (d.Is64Bit)
                        {
                            description += " (invalid)";
                            d.LastDisassembleData.OpCode = "pushad (invalid)";
                        }
                    }
                    break;
                case 0x61:
                    {
                        description = "pop all general-purpose registers";
                        if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.OpCode = "popa";
                        else
                            d.LastDisassembleData.OpCode = "popad";
                        if (d.Is64Bit)
                        {
                            description += " (invalid)";
                            d.LastDisassembleData.OpCode = "popad (invalid)";
                        }
                    }
                    break;
                case 0x62:
                    {
                        //bound
                        description = "check array index against bounds";
                        d.LastDisassembleData.OpCode = "bound";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.R16(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            d.LastDisassembleData.Parameters = d.R32(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 0, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x63:
                    {
                        //arpl or movsxd
                        if (d.Is64Bit)
                        {
                            d.LastDisassembleData.OpCode = "movsxd";
                            d.OpCodeFlags.W = false;
                            d.LastDisassembleData.Parameters = ' ' + d.R64(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32, 0, ATmrPos.Right);
                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                            description = "Move doubleword to quadword with signextension";
                        }
                        else
                        {
                            d.LastDisassembleData.OpCode = "arpl";
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last) + d.R16(memory[1]);
                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                            description = "adjust rpl field of segment selector";
                        }
                    }
                    break;
                case 0x68:
                    {
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        if (d.Prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                            d.LastDisassembleData.OpCode = "push";
                            d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                            d.LastDisassembleData.OpCode = "push";
                            if (d.SymbolHandler.Process.IsX64)
                            {
                                var intptr = memory.ToIntPtr(1).ReadInt32();
                                d.LastDisassembleData.ParameterValue = (UIntPtr)intptr;
                                d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)intptr, 8);
                            }
                            else
                                d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)dwordptr, 8);
                            offset += 4;
                        }
                        description = "push word or doubleword onto the stack (sign extended)";
                    }
                    break;
                case 0x69:
                    {
                        description = "signed multiply";
                        if (d.Prefix2.Contains(0x66))
                        {
                            d.LastDisassembleData.OpCode = "imul";
                            d.LastDisassembleData.Parameters = d.R16(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 1, ref last, ATmrPos.Right);
                            var wordptr = memory.ReadUInt16((int)last);
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                            offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 2);
                        }
                        else
                        {
                            d.LastDisassembleData.OpCode = "imul";
                            d.LastDisassembleData.Parameters = d.R32(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 0, ref last, ATmrPos.Right);
                            var dwordptr = memory.ReadUInt32((int)last);
                            if (d.RexW)
                                d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.IntToHexSigned((UIntPtr)dwordptr, 8);
                            else
                                d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.IntToHexSigned((UIntPtr)dwordptr, 8);
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                            offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 4);
                        }
                    }
                    break;
                case 0x6a:
                    {
                        d.LastDisassembleData.OpCode = "push";
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)memory[1], 2, true, 1);
                        offset += 1;
                        description = "push byte onto the stack";
                    }
                    break;
                case 0x6b:
                    {
                        description = "signed multiply";
                        d.LastDisassembleData.OpCode = "imul";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.R16(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            d.LastDisassembleData.Parameters = d.R32(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 0, ref last, ATmrPos.Right);
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                        d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 1);
                    }
                    break;
                case 0x6c:
                    {
                        //m8, dx
                        description = "input from port to string";
                        d.LastDisassembleData.OpCode = "insb";
                    }
                    break;
                case 0x6d:
                    {
                        //m8, dx
                        description = "input from port to string";
                        if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.OpCode = "insw";
                        else
                            d.LastDisassembleData.OpCode = "insd";
                    }
                    break;
                case 0x6e:
                    {
                        //m8, dx
                        description = "output string to port";
                        d.LastDisassembleData.OpCode = "outsb";
                    }
                    break;
                case 0x6f:
                    {
                        //m8, dx
                        description = "output string to port";
                        if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.OpCode = "outsw";
                        else
                            d.LastDisassembleData.OpCode = "outsd";
                    }
                    break;
                case 0x70:
                    {
                        description = "jump short if overflow (OF=1)";
                        d.LastDisassembleData.OpCode = "jo";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_of) != 0;
                        offset += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                    }
                    break;
                case 0x71:
                    {
                        description = "jump short if not overflow (OF=0)";
                        d.LastDisassembleData.OpCode = "jno";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_of) == 0;
                        offset += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                    }
                    break;
                case 0x72:
                    {
                        description = "jump short if below/carry (CF=1)";
                        d.LastDisassembleData.OpCode = "jb";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_cf) != 0;
                        offset += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                    }
                    break;
                case 0x73:
                    {
                        description = "jump short if above or equal (CF=0)";
                        d.LastDisassembleData.OpCode = "jae";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_cf) == 0;
                        offset += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                    }
                    break;
                case 0x74:
                    {
                        description = "jump short if equal (ZF=1)";
                        d.LastDisassembleData.OpCode = "je";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_zf) != 0;
                        offset += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                    }
                    break;
                case 0x75:
                    {
                        description = "jump short if not equal (ZF=0)";
                        d.LastDisassembleData.OpCode = "jne";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_zf) == 0;
                        offset += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                    }
                    break;
                case 0x76:
                    {
                        description = "jump short if not above (ZF=1 or CF=1)";
                        d.LastDisassembleData.OpCode = "jna";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & (eflags_cf | eflags_zf)) != 0;
                        offset += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                    }
                    break;
                case 0x77:
                    {
                        description = "jump short if above (ZF=0 and CF=0)";
                        d.LastDisassembleData.OpCode = "ja";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & (eflags_cf | eflags_zf)) == 0;
                        offset += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                    }
                    break;
                case 0x78:
                    {
                        description = "jump short if sign (SF=1)";
                        d.LastDisassembleData.OpCode = "js";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_sf) != 0;
                        offset += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                    }
                    break;
                case 0x79:
                    {
                        description = "jump short if not sign (SF=0)";
                        d.LastDisassembleData.OpCode = "jns";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_sf) == 0;
                        offset += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                    }
                    break;
                case 0x7a:
                    {
                        description = "jump short if parity (PF=1)";
                        d.LastDisassembleData.OpCode = "jp";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_pf) != 0;
                        offset += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                    }
                    break;
                case 0x7b:
                    {
                        description = "jump short if not parity (PF=0)";
                        d.LastDisassembleData.OpCode = "jnp";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_pf) == 0;
                        offset += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                    }
                    break;
                case 0x7c:
                    {
                        description = "jump short if not greater or equal (SF~=OF)";
                        d.LastDisassembleData.OpCode = "jl"; //jnge
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_sf) != (context->eflags & eflags_of);
                        offset += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                    }
                    break;
                case 0x7d:
                    {
                        description = "jump short if not less (greater or equal) (SF=OF)";
                        d.LastDisassembleData.OpCode = "jnl";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //d.LastDisassembleData.willjumpaccordingtocontext = (context->eflags & eflags_sf) == (context->eflags & eflags_of);
                        offset += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                    }
                    break;
                case 0x7e:
                    {
                        description = "jump short if less or equal (ZF=1 or SF~=OF)";
                        d.LastDisassembleData.OpCode = "jle";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //d.LastDisassembleData.willjumpaccordingtocontext = ((context->eflags & eflags_sf) != (context->eflags & eflags_of)) || ((context->eflags & eflags_zf) != 0);
                        offset += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                    }
                    break;
                case 0x7f:
                    {
                        description = "jump short if greater (ZF=0 or SF=OF)";
                        d.LastDisassembleData.OpCode = "jg";
                        d.LastDisassembleData.IsJump = true;
                        d.LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //d.LastDisassembleData.willjumpaccordingtocontext = ((context->eflags & eflags_sf) == (context->eflags & eflags_of)) && ((context->eflags & eflags_zf) == 0);
                        offset += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        if (d.Is64Bit)
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            d.LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);
                        d.LastDisassembleData.Parameters = d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                    }
                    break;
                case 0x80:
                case 0x82:
                    {
                        switch (d.GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    //add
                                    d.LastDisassembleData.OpCode = "add";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "add x to y";
                                }
                                break;
                            case 1:
                                {
                                    //adc
                                    d.LastDisassembleData.OpCode = "or";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "logical inclusive or";
                                }
                                break;
                            case 2:
                                {
                                    //adc
                                    d.LastDisassembleData.OpCode = "adc";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "add with carry";
                                }
                                break;
                            case 3:
                                {
                                    //sbb
                                    d.LastDisassembleData.OpCode = "sbb";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "integer subtraction with borrow";
                                }
                                break;
                            case 4:
                                {
                                    //and
                                    d.LastDisassembleData.OpCode = "and";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "logical and";
                                }
                                break;
                            case 5:
                                {
                                    d.LastDisassembleData.OpCode = "sub";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "subtract";
                                }
                                break;
                            case 6:
                                {
                                    d.LastDisassembleData.OpCode = "xor";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "logical exclusive or";
                                }
                                break;
                            case 7:
                                {
                                    d.LastDisassembleData.OpCode = "cmp";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "compare two operands";
                                }
                                break;
                        }
                    }
                    break;
                case 0x81:
                    {
                        switch (d.GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    //add
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "add";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        var wordptr = memory.ReadUInt16((int)last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)wordptr, 4);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 2);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "add";
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
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 4);
                                    }
                                    //                      offset:=offset+last;
                                    description = "add x to y";
                                }
                                break;
                            case 1:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "or";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        var wordptr = memory.ReadUInt16((int)last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)wordptr, 4);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 2);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "or";
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
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 4);
                                    }
                                    description = "logical inclusive or";
                                }
                                break;
                            case 2:
                                {
                                    //adc
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "adc";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        var wordptr = memory.ReadUInt16((int)last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)wordptr, 4);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 2);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "adc";
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
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 4);
                                    }
                                    description = "add with carry";
                                }
                                break;
                            case 3:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "sbb";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        var wordptr = memory.ReadUInt16((int)last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)wordptr, 4);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 2);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "sbb";
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
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 4);
                                    }
                                    description = "integer subtraction with borrow";
                                }
                                break;
                            case 4:
                                {
                                    //and
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "and";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        var wordptr = memory.ReadUInt16((int)last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)wordptr, 4);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 2);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "and";
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
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 4);
                                    }
                                    description = "logical and";
                                }
                                break;
                            case 5:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "sub";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last);
                                        var wordptr = memory.ReadUInt16((int)last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)wordptr, 4);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 2);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "sub";
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
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 4);
                                    }
                                    description = "subtract";
                                }
                                break;
                            case 6:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "xor";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        var wordptr = memory.ReadUInt16((int)last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)wordptr, 4);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 2);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "xor";
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
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 4);
                                    }
                                    description = "logical exclusive or";
                                }
                                break;
                            case 7:
                                {
                                    //cmp
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "cmp";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        var wordptr = memory.ReadUInt16((int)last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)wordptr, 4);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 2);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "cmp";
                                        if (d.RexW)
                                        {
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                            d.LastDisassembleData.DataSize = 8; ;
                                        }
                                        else
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last);
                                        var dwordptr = memory.ReadUInt32((int)last);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                                        if (d.RexW)
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)dwordptr, 8);
                                        else
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)dwordptr, 8);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 4);
                                    }
                                    description = "compare two operands";
                                }
                                break;
                        }
                    }
                    break;
                case 0x83:
                    {
                        switch (d.GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "add";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2, true);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "add";
                                        if (d.RexW)
                                        {
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2, true);
                                        }
                                        else
                                        {
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2, true);
                                        }
                                    }
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "add (sign extended)";
                                }
                                break;
                            case 1:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "or";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "or";
                                        if (d.RexW)
                                        {
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                        else
                                        {
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                    }
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "add (sign extended)";
                                }
                                break;
                            case 2:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "adc";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "adc";
                                        if (d.RexW)
                                        {
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                        else
                                        {
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                    }
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "add with carry (sign extended)";
                                }
                                break;
                            case 3:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "sbb";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "sbb";
                                        if (d.RexW)
                                        {
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                        else
                                        {
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                    }
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "integer subtraction with borrow (sign extended)";
                                }
                                break;
                            case 4:
                                {
                                    //and
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "and";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "and";
                                        if (d.RexW)
                                        {
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                        else
                                        {
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                    }
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "logical and (sign extended)";
                                }
                                break;
                            case 5:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "sub";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "sub";
                                        if (d.RexW)
                                        {
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                        else
                                        {
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                    }
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "subtract";
                                }
                                break;
                            case 6:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "xor";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "xor";
                                        if (d.RexW)
                                        {
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                        else
                                        {
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                    }
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "logical exclusive or";
                                }
                                break;
                            case 7:
                                {
                                    //cmp
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        d.LastDisassembleData.OpCode = "cmp";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last, 16);
                                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    }
                                    else
                                    {
                                        d.LastDisassembleData.OpCode = "cmp";
                                        if (d.RexW)
                                        {
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 64);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                        else
                                        {
                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last, 32);
                                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                    }
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "compare two operands";
                                }
                                break;
                        }
                    }
                    break;
                case 0x84:
                    {
                        description = "logical compare";
                        d.LastDisassembleData.OpCode = "test";
                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last) + d.R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x85:
                    {
                        description = "logical compare";
                        d.LastDisassembleData.OpCode = "test";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last) + d.R16(memory[1]);
                        else
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + d.R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x86:
                    {
                        description = "exchange memory with register";
                        d.LastDisassembleData.OpCode = "xchg";
                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last) + d.R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x87:
                    {
                        description = "exchange memory with register";
                        d.LastDisassembleData.OpCode = "xchg";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last) + d.R16(memory[1]);
                        else
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + d.R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x88:
                    {
                        description = "copy memory";
                        d.LastDisassembleData.OpCode = "mov";
                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last) + d.R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x89:
                    {
                        description = "copy memory";
                        d.LastDisassembleData.OpCode = "mov";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last) + d.R16(memory[1]);
                        else
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + d.R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x8a:
                    {
                        description = "copy memory";
                        d.LastDisassembleData.OpCode = "mov";
                        d.LastDisassembleData.Parameters = d.R8(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 2, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x8b:
                    {
                        description = "copy memory";
                        d.LastDisassembleData.OpCode = "mov";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.R16(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            d.LastDisassembleData.Parameters = d.R32(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 0, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x8c:
                    {
                        description = "copy memory";
                        d.LastDisassembleData.OpCode = "mov";
                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last) + d.SReg(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x8d:
                    {
                        description = "load effective address";
                        d.LastDisassembleData.OpCode = "lea";
                        if (d.Prefix2.Contains(0x66))
                        {
                            if (d.SymbolHandler.Process.IsX64 & (d.Prefix2.Contains(0x67)))
                                d.LastDisassembleData.Parameters = d.R16(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 1, ref last, 0, 32, ATmrPos.Right);
                            else
                                d.LastDisassembleData.Parameters = d.R16(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 1, ref last, 0, 0, ATmrPos.Right);
                        }
                        else
                        {
                            if (d.SymbolHandler.Process.IsX64 & (d.Prefix2.Contains(0x67)))
                                d.LastDisassembleData.Parameters = d.R32(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 0, ref last, 0, 32, ATmrPos.Right);
                            else
                                d.LastDisassembleData.Parameters = d.R32(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 0, ref last, 0, 0, ATmrPos.Right);
                        }
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x8e:
                    {
                        description = "copy memory";
                        d.LastDisassembleData.OpCode = "mov";
                        d.LastDisassembleData.Parameters = d.SReg(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 1, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x8f:
                    {
                        switch (d.GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "pop a value from the stack";
                                    d.LastDisassembleData.OpCode = "pop";
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
                                    d.LastDisassembleData.Parameters = d.ColorHex + "8f" + d.EndColor;
                                    description = "undefined by the intel specification";
                                }
                                break;
                        }
                    }
                    break;
                case 0x90:
                    {
                        description = "no operation";
                        d.LastDisassembleData.OpCode = "nop";
                        if (prefixSize > 0)
                            d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)(prefixSize + 1), 1);
                    }
                    break;
                case 0x91:
                case 0x92:
                case 0x93:
                case 0x94:
                case 0x95:
                case 0x96:
                case 0x97:
                    {
                        description = "exchange register with register";
                        d.LastDisassembleData.OpCode = "xchg";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.ColorReg + "ax" + d.EndColor + ',' + d.Rd16((Byte)(memory[0] - 0x90));
                        else
                        {
                            if (d.RexW)
                                d.LastDisassembleData.Parameters = d.ColorReg + "rax" + d.EndColor + ',' + d.Rd((Byte)(memory[0] - 0x90));
                            else
                                d.LastDisassembleData.Parameters = d.ColorReg + "eax" + d.EndColor + ',' + d.Rd((Byte)(memory[0] - 0x90));
                        }
                    }
                    break;
                case 0x98:
                    {
                        //cbw/cwde
                        if (d.Prefix2.Contains(0x66))
                        {
                            d.LastDisassembleData.OpCode = "cbw";
                            description = "convert byte to word";
                        }
                        else
                        {
                            if (d.RexW)
                            {
                                d.LastDisassembleData.OpCode = "cdqe";
                                description = "convert doubleword to quadword";
                            }
                            else
                            {
                                d.LastDisassembleData.OpCode = "cwde";
                                description = "convert word to doubleword";
                            }
                        }
                    }
                    break;
                case 0x99:
                    {
                        if (d.Prefix2.Contains(0x66))
                        {
                            description = "convert word to doubleword";
                            d.LastDisassembleData.OpCode = "cwd";
                        }
                        else
                        {
                            if (d.RexW)
                            {
                                d.LastDisassembleData.OpCode = "cqo";
                                description = "convert quadword to octword";
                            }
                            else
                            {
                                d.LastDisassembleData.OpCode = "cdq";
                                description = "convert doubleword to quadword";
                            }
                        }
                    }
                    break;
                case 0x9a:
                    {
                        description = "call procedure";
                        var wordptr = memory.ReadUInt16(5);
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 5;
                        d.LastDisassembleData.SeparatorCount += 1;
                        if (d.Is64Bit)
                            d.LastDisassembleData.OpCode = "call (invalid)";
                        else
                            d.LastDisassembleData.OpCode = "call";
                        d.LastDisassembleData.Parameters = d.IntToHexSigned((UIntPtr)wordptr, 4) + ':';
                        var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                        offset += 6;
                    }
                    break;
                case 0x9b:
                    {
                        switch (memory[1])
                        {
                            case 0xd9:
                                {
                                    switch (d.GetReg(memory[2]))
                                    {
                                        case 6:
                                            {
                                                description = "store fpu environment";
                                                d.LastDisassembleData.OpCode = "wait:fstenv";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 7:
                                            {
                                                description = "store control word";
                                                d.LastDisassembleData.OpCode = "wait:fstcw";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        default:
                                            {
                                                description = "wait";
                                                d.LastDisassembleData.OpCode = "wait";
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 0xdb:
                                {
                                    switch (memory[2])
                                    {
                                        case 0xe2:
                                            {
                                                description = "clear exceptions";
                                                d.LastDisassembleData.OpCode = "wait:fclex";
                                                offset += 2;
                                            }
                                            break;
                                        case 0xe3:
                                            {
                                                description = "initialize floaring-point unit";
                                                d.LastDisassembleData.OpCode = "wait:finit";
                                                offset += 2;
                                            }
                                            break;
                                        default:
                                            {
                                                description = "wait";
                                                d.LastDisassembleData.OpCode = "wait";
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 0xdd:
                                {
                                    switch (d.GetReg(memory[2]))
                                    {
                                        case 6:
                                            {
                                                description = "store fpu state";
                                                d.LastDisassembleData.OpCode = "wait:fsave";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 7:
                                            {
                                                description = "store status word";
                                                d.LastDisassembleData.OpCode = "wait:fstsw";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        default:
                                            {
                                                description = "wait";
                                                d.LastDisassembleData.OpCode = "wait";
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 0xdf:
                                {
                                    switch (memory[2])
                                    {
                                        case 0xe0:
                                            {
                                                description = "store status word";
                                                d.LastDisassembleData.OpCode = "wait:fstsw ax";
                                                offset += 2;
                                            }
                                            break;
                                        default:
                                            {
                                                description = "wait";
                                                d.LastDisassembleData.OpCode = "wait";
                                            }
                                            break;
                                    }
                                }
                                break;
                            default:
                                {
                                    description = "wait";
                                    d.LastDisassembleData.OpCode = "wait";
                                }
                                break;
                        }
                    }
                    break;
                case 0x9c:
                    {
                        description = "push eflags register onto the stack";
                        if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.OpCode = "pushf";
                        else
                        {
                            if (d.Is64Bit)
                                d.LastDisassembleData.OpCode = "pushfq";
                            else
                                d.LastDisassembleData.OpCode = "pushfd";
                        }
                    }
                    break;
                case 0x9d:
                    {
                        description = "pop stack into eflags register";
                        if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.OpCode = "popf";
                        else
                        {
                            if (d.Is64Bit)
                                d.LastDisassembleData.OpCode = "popfq";
                            else
                                d.LastDisassembleData.OpCode = "popfd";
                        }
                    }
                    break;
                case 0x9e:
                    {
                        description = "store ah into flags";
                        d.LastDisassembleData.OpCode = "sahf";
                    }
                    break;
                case 0x9f:
                    {
                        description = "load status flag into ah register";
                        d.LastDisassembleData.OpCode = "lahf";
                    }
                    break;
                case 0xa0:
                    {
                        description = "copy memory";
                        var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                        d.LastDisassembleData.OpCode = "mov";
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        if (d.SymbolHandler.Process.IsX64)
                        {
                            var qwordptr = memory.ToIntPtr(1).ReadUInt64();
                            d.LastDisassembleData.ParameterValue = (UIntPtr)qwordptr;
                            d.LastDisassembleData.Parameters = d.ColorReg + "al" + d.EndColor + ',' + d.GetSegmentOverride(d.Prefix2) + '[' + d.IntToHexSigned((UIntPtr)qwordptr, 8) + ']';
                            offset += 8;
                        }
                        else
                        {
                            d.LastDisassembleData.Parameters = d.ColorReg + "al" + d.EndColor + ',' + d.GetSegmentOverride(d.Prefix2) + '[' + d.IntToHexSigned((UIntPtr)dwordptr, 8) + ']';
                            offset += 4;
                        }
                    }
                    break;
                case 0xa1:
                    {
                        description = "copy memory";
                        d.LastDisassembleData.OpCode = "mov";
                        var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        if (d.Prefix2.Contains(0x66))
                        {
                            d.LastDisassembleData.Parameters = d.ColorReg + "ax" + d.EndColor + ',' + d.GetSegmentOverride(d.Prefix2) + '[' + d.IntToHexSigned((UIntPtr)dwordptr, 8) + ']';
                        }
                        else
                        {
                            if (d.RexW)
                                d.LastDisassembleData.Parameters = d.ColorReg + "rax" + d.EndColor + ',';
                            else
                                d.LastDisassembleData.Parameters = d.ColorReg + "eax" + d.EndColor + ',';
                            if (d.SymbolHandler.Process.IsX64)
                            {
                                var qwordptr = memory.ToIntPtr(1).ReadUInt64();
                                d.LastDisassembleData.ParameterValue = (UIntPtr)qwordptr;
                                d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + d.GetSegmentOverride(d.Prefix2) + '[' + d.IntToHexSigned((UIntPtr)qwordptr, 8) + ']';
                            }
                            else
                                d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + d.GetSegmentOverride(d.Prefix2) + '[' + d.IntToHexSigned((UIntPtr)dwordptr, 8) + ']';
                        }
                        if (d.SymbolHandler.Process.IsX64)
                            offset += 8;
                        else
                            offset += 4;
                    }
                    break;
                case 0xa2:
                    {
                        description = "copy memory";
                        var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                        d.LastDisassembleData.OpCode = "mov";
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        if (d.SymbolHandler.Process.IsX64)
                        {
                            var qwordptr = memory.ToIntPtr(1).ReadUInt64();
                            d.LastDisassembleData.ParameterValue = (UIntPtr)qwordptr;
                            d.LastDisassembleData.Parameters = d.GetSegmentOverride(d.Prefix2) + '[' + d.IntToHexSigned((UIntPtr)qwordptr, 8) + "]," + d.ColorReg + "al" + d.EndColor;
                        }
                        else
                            d.LastDisassembleData.Parameters = d.GetSegmentOverride(d.Prefix2) + '[' + d.IntToHexSigned((UIntPtr)dwordptr, 8) + "]," + d.ColorReg + "al" + d.EndColor;
                        if (d.SymbolHandler.Process.IsX64)
                            offset += 8;
                        else
                            offset += 4;
                    }
                    break;
                case 0xa3:
                    {
                        description = "copy memory";
                        d.LastDisassembleData.OpCode = "mov";
                        var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        if (d.SymbolHandler.Process.IsX64)
                        {
                            var qwordptr = memory.ToIntPtr(1).ReadUInt64();
                            d.LastDisassembleData.ParameterValue = (UIntPtr)qwordptr;
                            d.LastDisassembleData.Parameters = d.GetSegmentOverride(d.Prefix2) + '[' + d.IntToHexSigned((UIntPtr)qwordptr, 8) + "],";
                        }
                        else
                            d.LastDisassembleData.Parameters = d.GetSegmentOverride(d.Prefix2) + '[' + d.IntToHexSigned((UIntPtr)dwordptr, 8) + "],";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + d.ColorReg + "ax" + d.EndColor;
                        else
                        {
                            if (d.RexW)
                                d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + d.ColorReg + "rax" + d.EndColor;
                            else
                                d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + d.ColorReg + "eax" + d.EndColor;
                        }
                        if (d.SymbolHandler.Process.IsX64)
                            offset += 8;
                        else
                            offset += 4;
                    }
                    break;
                case 0xa4:
                    {
                        description = "move data from string to string";
                        d.LastDisassembleData.OpCode = "movsb";
                    }
                    break;
                case 0xa5:
                    {
                        description = "move data from string to string";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.OpCode = "movsw";
                        else
                        {
                            if (d.RexW)
                                d.LastDisassembleData.OpCode = "movsq";
                            else
                                d.LastDisassembleData.OpCode = "movsd";
                        }
                    }
                    break;
                case 0xa6:
                    {
                        description = "compare string operands";
                        d.LastDisassembleData.OpCode = "cmpsb";
                    }
                    break;
                case 0xa7:
                    {
                        description = "compare string operands";
                        if (d.Prefix2.Contains(0x66))
                            d.LastDisassembleData.OpCode = "cmpsw";
                        else
                        {
                            if (d.RexW)
                                d.LastDisassembleData.OpCode = "cmpsq";
                            else
                                d.LastDisassembleData.OpCode = "cmpsd";
                        }
                    }
                    break;
                case 0xa8:
                    {
                        description = "logical compare";
                        d.LastDisassembleData.OpCode = "test";
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        d.LastDisassembleData.Parameters = d.ColorReg + "al" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)memory[1], 2);
                        offset += 1;
                    }
                    break;
                case 0xa9:
                    {
                        description = "logical compare";
                        d.LastDisassembleData.OpCode = "test";
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        if (d.Prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                            d.LastDisassembleData.Parameters = d.ColorReg + "ax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                            if (d.RexW)
                                d.LastDisassembleData.Parameters = d.ColorReg + "rax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)dwordptr, 8);
                            else
                                d.LastDisassembleData.Parameters = d.ColorReg + "eax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)dwordptr, 8);
                            offset += 4;
                        }
                    }
                    break;
                case 0xaa:
                    {
                        description = "store string";
                        d.LastDisassembleData.OpCode = "stosb";
                    }
                    break;
                case 0xab:
                    {
                        description = "store string";
                        if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.OpCode = "stosw";
                        else
                        {
                            if (d.RexW)
                                d.LastDisassembleData.OpCode = "stosq";
                            else
                                d.LastDisassembleData.OpCode = "stosd";
                        }
                    }
                    break;
                case 0xac:
                    {
                        description = "load string";
                        d.LastDisassembleData.OpCode = "lodsb";
                    }
                    break;
                case 0xad:
                    {
                        description = "load string";
                        if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.OpCode = "lodsw";
                        else
                        {
                            if (d.RexW)
                                d.LastDisassembleData.OpCode = "lodsq";
                            else
                                d.LastDisassembleData.OpCode = "lodsd";
                        }
                    }
                    break;
                case 0xae:
                    {
                        description = "compare al with byte at es:edi and set status flag";
                        d.LastDisassembleData.OpCode = "scasb";
                    }
                    break;
                case 0xaf:
                    {
                        description = "scan string";
                        if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.OpCode = "scasw";
                        else
                        {
                            if (d.RexW)
                                d.LastDisassembleData.OpCode = "scasq";
                            else
                                d.LastDisassembleData.OpCode = "scasd";
                        }
                    }
                    break;
                case 0xb0:
                case 0xb1:
                case 0xb2:
                case 0xb3:
                case 0xb4:
                case 0xb5:
                case 0xb6:
                case 0xb7:
                    {
                        description = "copy memory";
                        d.LastDisassembleData.OpCode = "mov";
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        //              if Rex_B
                        d.LastDisassembleData.Parameters = d.Rd8((Byte)(memory[0] - 0xb0)) + ',' + d.IntToHexSigned((UIntPtr)memory[1], 2);
                        offset += 1;
                    }
                    break;
                case 0xb8:
                case 0xb9:
                case 0xba:
                case 0xbb:
                case 0xbc:
                case 0xbd:
                case 0xbe:
                case 0xbf:
                    {
                        description = "copy memory";
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        if (d.Prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                            d.LastDisassembleData.OpCode = "mov";
                            d.LastDisassembleData.Parameters = d.Rd16((Byte)(memory[0] - 0xb8)) + ',' + d.IntToHexSigned((UIntPtr)wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            if (d.RexW)
                            {
                                d.LastDisassembleData.OpCode = "mov";
                                d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                                d.LastDisassembleData.Parameters = d.Rd((Byte)(memory[0] - 0xb8)) + ',' + d.IntToHexSigned((UIntPtr)dwordptr, 16);
                                offset += 8;
                            }
                            else
                            {
                                d.LastDisassembleData.OpCode = "mov";
                                d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                                d.LastDisassembleData.Parameters = d.Rd((Byte)(memory[0] - 0xb8)) + ',' + d.IntToHexSigned((UIntPtr)dwordptr, 8);
                                offset += 4;
                            }
                        }
                    }
                    break;
                case 0xc0:
                    {
                        switch (d.GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    d.LastDisassembleData.OpCode = "rol";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    description = "rotate eight bits left " + (memory[(int)last]) + " times";
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                }
                                break;
                            case 1:
                                {
                                    d.LastDisassembleData.OpCode = "ror";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    description = "rotate eight bits right " + (memory[(int)last]) + " times";
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                }
                                break;
                            case 2:
                                {
                                    d.LastDisassembleData.OpCode = "rcl";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    description = "rotate nine bits left " + (memory[(int)last]) + " times";
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                }
                                break;
                            case 3:
                                {
                                    d.LastDisassembleData.OpCode = "rcr";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    description = "rotate nine bits right " + (memory[(int)last]) + " times";
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                }
                                break;
                            case 4:
                                {
                                    d.LastDisassembleData.OpCode = "shl";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    description = "multiply by 2, " + (memory[(int)last]) + " times";
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                }
                                break;
                            case 5:
                                {
                                    d.LastDisassembleData.OpCode = "shr";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    description = "unsigned divide by 2, " + (memory[(int)last]) + " times";
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                }
                                break;
                            /*not in intel spec*/
                            case 6:
                                {
                                    d.LastDisassembleData.OpCode = "rol";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    description = "rotate eight bits left " + (memory[(int)last]) + " times";
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                }
                                break;
                            /*^^^^^^^^^^^^^^^^^^*/
                            case 7:
                                {
                                    d.LastDisassembleData.OpCode = "sar";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last, 8);
                                    d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    d.LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    d.LastDisassembleData.Parameters += d.IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    description = "signed divide by 2, " + (memory[(int)last]) + " times";
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
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
