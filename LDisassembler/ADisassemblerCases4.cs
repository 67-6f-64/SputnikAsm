using System;
using Sputnik.LBinary;
using Sputnik.LMarshal;
using SputnikAsm.LDisassembler.LEnums;
using SputnikAsm.LUtils;

namespace SputnikAsm.LDisassembler
{
    public partial class ADisassembler
    {
        #region DisassembleProcess4
        private Boolean DisassembleProcess4(UBytePtr memory, ref UIntPtr offset, ref int prefixSize, ref UInt32 last, ref String description)
        {
            switch (memory[0])
            {
                case 0x10:
                    {
                        description = "add with carry";
                        LastDisassembleData.OpCode = "adc";
                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last) + R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x11:
                    {
                        description = "add with carry";
                        LastDisassembleData.OpCode = "adc";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last) + R16(memory[1]);
                        else
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());

                    }
                    break;

                case 0x12:
                    {
                        description = "add with carry";
                        LastDisassembleData.OpCode = "adc";
                        LastDisassembleData.Parameters = R8(memory[1]) + ModRm(memory, _prefix2, 1, 2, ref last, 8, 0, ATmrPos.Right);

                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x13:
                    {
                        description = "add with carry";
                        LastDisassembleData.OpCode = "adc";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = R16(memory[1]) + ModRm(memory, _prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            LastDisassembleData.Parameters = R32(memory[1]) + ModRm(memory, _prefix2, 1, 0, ref last, ATmrPos.Right);

                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x14:
                    {
                        description = "add with carry";
                        LastDisassembleData.OpCode = "adc";
                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        LastDisassembleData.Parameters = _colorReg + "al" + _endColor + ',' + IntToHexSigned(LastDisassembleData.ParameterValue, 2);

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        offset += 1;
                    }
                    break;

                case 0x15:
                    {
                        description = "add with carry";
                        LastDisassembleData.OpCode = "adc";
                        if (_prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                            LastDisassembleData.Parameters = _colorReg + "ax" + _endColor + ',' + IntToHexSigned(LastDisassembleData.ParameterValue, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;

                            if (RexW)
                                LastDisassembleData.Parameters = _colorReg + "rax" + _endColor + ',' + IntToHexSigned((UIntPtr)LastDisassembleData.ParameterValue, 8);
                            else
                                LastDisassembleData.Parameters = _colorReg + "eax" + _endColor + ',' + IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                            offset += 4;
                        }

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                    }
                    break;

                case 0x16:
                    {
                        description = "place ss on the stack";
                        LastDisassembleData.OpCode = "push";
                        LastDisassembleData.Parameters = _colorReg + "ss" + _endColor;
                    }
                    break;

                case 0x17:
                    {
                        description = "remove ss from the stack";
                        LastDisassembleData.OpCode = "pop";
                        LastDisassembleData.Parameters = _colorReg + "ss" + _endColor;
                    }
                    break;

                case 0x18:
                    {
                        description = "integer subtraction with borrow";
                        LastDisassembleData.OpCode = "sbb";
                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last) + R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x19:
                    {
                        description = "integer subtraction with borrow";
                        LastDisassembleData.OpCode = "sbb";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last) + R16(memory[1]);
                        else
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x1a:
                    {
                        description = "integer subtraction with borrow";
                        LastDisassembleData.OpCode = "sbb";
                        LastDisassembleData.Parameters = R8(memory[1]) + ModRm(memory, _prefix2, 1, 2, ref last, 8, 0, ATmrPos.Right);

                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x1b:
                    {
                        description = "integer subtraction with borrow";
                        LastDisassembleData.OpCode = "sbb";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = R16(memory[1]) + ModRm(memory, _prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            LastDisassembleData.Parameters = R32(memory[1]) + ModRm(memory, _prefix2, 1, 0, ref last, ATmrPos.Right);


                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x1c:
                    {
                        description = "integer subtraction with borrow";
                        LastDisassembleData.OpCode = "sbb";
                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.Parameters = _colorReg + "al" + _endColor + ',' + IntToHexSigned((UIntPtr)memory[1], 2);


                        offset += 1;
                    }
                    break;

                case 0x1d:
                    {
                        LastDisassembleData.OpCode = "sbb";
                        description = "integer subtraction with borrow";
                        if (_prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();

                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                            LastDisassembleData.Parameters = _colorReg + "ax" + _endColor + ',' + IntToHexSigned((UIntPtr)wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;

                            if (RexW)
                                LastDisassembleData.Parameters = _colorReg + "rax" + _endColor + ',' + IntToHexSigned((UIntPtr)LastDisassembleData.ParameterValue, 8);
                            else
                                LastDisassembleData.Parameters = _colorReg + "eax" + _endColor + ',' + IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                            offset += 4;
                        }

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                    }
                    break;

                case 0x1e:
                    {
                        description = "place ds on the stack";
                        LastDisassembleData.OpCode = "push";
                        LastDisassembleData.Parameters = _colorReg + "ds" + _endColor;
                    }
                    break;

                case 0x1f:
                    {
                        description = "remove ds from the stack";
                        LastDisassembleData.OpCode = "pop";
                        LastDisassembleData.Parameters = _colorReg + "ds" + _endColor;
                    }
                    break;

                case 0x20:
                    {
                        description = "logical and";
                        LastDisassembleData.OpCode = "and";
                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last) + R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x21:
                    {
                        description = "logical and";
                        LastDisassembleData.OpCode = "and";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last) + R16(memory[1]);
                        else
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());

                    }
                    break;

                case 0x22:
                    {
                        description = "logical and";
                        LastDisassembleData.OpCode = "and";
                        LastDisassembleData.Parameters = R8(memory[1]) + ModRm(memory, _prefix2, 1, 2, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x23:
                    {
                        description = "logical and";
                        LastDisassembleData.OpCode = "and";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = R16(memory[1]) + ModRm(memory, _prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            LastDisassembleData.Parameters = R32(memory[1]) + ModRm(memory, _prefix2, 1, 0, ref last, ATmrPos.Right);

                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;


                case 0x24:
                    {
                        description = "logical and";
                        LastDisassembleData.OpCode = "and";
                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        LastDisassembleData.Parameters = _colorReg + "al" + _endColor + ',' + IntToHexSigned((UIntPtr)memory[1], 2);

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;


                        offset += 1;
                    }
                    break;

                case 0x25:
                    {
                        description = "logical and";
                        LastDisassembleData.OpCode = "and";
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;


                        if (_prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                            LastDisassembleData.Parameters = _colorReg + "ax" + _endColor + ',' + IntToHexSigned(LastDisassembleData.ParameterValue, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;

                            if (RexW)
                                LastDisassembleData.Parameters = _colorReg + "rax" + _endColor + ',' + IntToHexSigned((UIntPtr)LastDisassembleData.ParameterValue, 8);
                            else
                                LastDisassembleData.Parameters = _colorReg + "eax" + _endColor + ',' + IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                            offset += 4;
                        }
                    }
                    break;

                case 0x27:
                    {
                        description = "decimal adjust al after addition";
                        LastDisassembleData.OpCode = "daa";
                    }
                    break;

                case 0x28:
                    {
                        description = "subtract";
                        LastDisassembleData.OpCode = "sub";
                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last) + R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x29:
                    {
                        description = "subtract";
                        LastDisassembleData.OpCode = "sub";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last) + R16(memory[1]);
                        else
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());

                    }
                    break;

                case 0x2a:
                    {
                        description = "subtract";
                        LastDisassembleData.OpCode = "sub";
                        LastDisassembleData.Parameters = R8(memory[1]) + ModRm(memory, _prefix2, 1, 2, ref last, ATmrPos.Right);

                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x2b:
                    {
                        description = "subtract";
                        LastDisassembleData.OpCode = "sub";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = R16(memory[1]) + ModRm(memory, _prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            LastDisassembleData.Parameters = R32(memory[1]) + ModRm(memory, _prefix2, 1, 0, ref last, ATmrPos.Right);

                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x2c:
                    {
                        description = "subtract";
                        LastDisassembleData.OpCode = "sub";

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.Parameters = _colorReg + "al" + _endColor + ',' + IntToHexSigned((UIntPtr)memory[1], 2);



                        offset += 1;
                    }
                    break;

                case 0x2d:
                    {
                        description = "subtract";
                        LastDisassembleData.OpCode = "sub";


                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        if (_prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                            LastDisassembleData.Parameters = _colorReg + "ax" + _endColor + ',' + IntToHexSigned((UIntPtr)wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;


                            if (RexW)
                                LastDisassembleData.Parameters = _colorReg + "rax" + _endColor + ',' + IntToHexSigned((UIntPtr)dwordptr, 8);
                            else
                                LastDisassembleData.Parameters = _colorReg + "eax" + _endColor + ',' + IntToHexSigned((UIntPtr)dwordptr, 8);
                            offset += 4;
                        }
                    }
                    break;


                case 0x2f:
                    {
                        description = "decimal adjust al after subtraction";
                        LastDisassembleData.OpCode = "das";
                    }
                    break;

                case 0x30:
                    {
                        description = "logical exclusive or";
                        LastDisassembleData.OpCode = "xor";
                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last) + R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x31:
                    {
                        description = "logical exclusive or";
                        LastDisassembleData.OpCode = "xor";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last) + R16(memory[1]);
                        else
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());

                    }
                    break;

                case 0x32:
                    {
                        description = "logical exclusive or";
                        LastDisassembleData.OpCode = "xor";
                        LastDisassembleData.Parameters = R8(memory[1]) + ModRm(memory, _prefix2, 1, 2, ref last, ATmrPos.Right);

                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x33:
                    {
                        description = "logical exclusive or";
                        LastDisassembleData.OpCode = "xor";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = R16(memory[1]) + ModRm(memory, _prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            LastDisassembleData.Parameters = R32(memory[1]) + ModRm(memory, _prefix2, 1, 0, ref last, ATmrPos.Right);

                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x34:
                    {
                        description = "logical exclusive or";
                        LastDisassembleData.OpCode = "xor";
                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.Parameters = _colorReg + "al" + _endColor + ',' + IntToHexSigned((UIntPtr)memory[1], 2);
                        offset += 1;
                    }
                    break;

                case 0x35:
                    {
                        description = "logical exclusive or";
                        LastDisassembleData.OpCode = "xor";


                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        if (_prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                            LastDisassembleData.Parameters = _colorReg + "ax" + _endColor + ',' + IntToHexSigned((UIntPtr)wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;

                            if (RexW)
                                LastDisassembleData.Parameters = _colorReg + "rax" + _endColor + ',' + IntToHexSigned((UIntPtr)dwordptr, 8);
                            else
                                LastDisassembleData.Parameters = _colorReg + "eax" + _endColor + ',' + IntToHexSigned((UIntPtr)dwordptr, 8);
                            offset += 4;
                        }
                    }
                    break;


                case 0x37:
                    {  //aaa
                        LastDisassembleData.OpCode = "aaa";
                        description = "ascii adjust al after addition";
                    }
                    break;

                //---------
                case 0x38:
                    {//cmp
                        description = "compare two operands";
                        LastDisassembleData.OpCode = "cmp";
                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last) + R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x39:
                    {
                        description = "compare two operands";
                        LastDisassembleData.OpCode = "cmp";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last) + R16(memory[1]);
                        else
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + R32(memory[1]);

                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());

                    }
                    break;

                case 0x3a:
                    {
                        description = "compare two operands";
                        LastDisassembleData.OpCode = "cmp";
                        LastDisassembleData.Parameters = R8(memory[1]) + ModRm(memory, _prefix2, 1, 2, ref last, ATmrPos.Right);

                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x3b:
                    {
                        description = "compare two operands";
                        LastDisassembleData.OpCode = "cmp";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = R16(memory[1]) + ModRm(memory, _prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            LastDisassembleData.Parameters = R32(memory[1]) + ModRm(memory, _prefix2, 1, 0, ref last, ATmrPos.Right);

                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                //---------

                case 0x3c:
                    {
                        description = "compare two operands";
                        LastDisassembleData.OpCode = "cmp";

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.Parameters = _colorReg + "al" + _endColor + ',' + IntToHexSigned((UIntPtr)memory[1], 2);
                        offset += 1;
                    }
                    break;

                case 0x3d:
                    {
                        description = "compare two operands";
                        LastDisassembleData.OpCode = "cmp";
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;


                        if (_prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                            LastDisassembleData.Parameters = _colorReg + "ax" + _endColor + ',' + IntToHexSigned((UIntPtr)wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;


                            if (RexW)
                                LastDisassembleData.Parameters = _colorReg + "rax" + _endColor + ',' + IntToHexSigned((UIntPtr)dwordptr, 8);
                            else
                                LastDisassembleData.Parameters = _colorReg + "eax" + _endColor + ',' + IntToHexSigned((UIntPtr)dwordptr, 8);
                            offset += 4;
                        }
                    }
                    break;

                //prefix bytes need fixing
                case 0x3f:
                    {  //aas
                        if (SymbolHandler.Process.IsX86)
                        {
                            LastDisassembleData.OpCode = "db";
                            LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)0x3f, 1);
                        }
                        else
                        {
                            LastDisassembleData.OpCode = "aas";
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
                        LastDisassembleData.OpCode = "inc";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = Rd16((Byte)(memory[0] - 0x40));
                        else
                            LastDisassembleData.Parameters = Rd((Byte)(memory[0] - 0x40));
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
                        LastDisassembleData.OpCode = "dec";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = Rd16((Byte)(memory[0] - 0x48));
                        else
                            LastDisassembleData.Parameters = Rd((Byte)(memory[0] - 0x48));
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

                        if (Is64Bit) _opCodeFlags.W = true;

                        LastDisassembleData.OpCode = "push";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = Rd16((Byte)(memory[0] - 0x50));
                        else
                            LastDisassembleData.Parameters = Rd((Byte)(memory[0] - 0x50));
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
                        if (Is64Bit) _opCodeFlags.W = true; //so rd will pick the 64-bit version
                        LastDisassembleData.OpCode = "pop";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = Rd16((Byte)(memory[0] - 0x58));
                        else
                            LastDisassembleData.Parameters = Rd((Byte)(memory[0] - 0x58));
                    }
                    break;

                case 0x60:
                    {
                        description = "push all general-purpose registers";
                        if (Is64Bit) description += " (invalid)";
                        if (_prefix2.Contains(0x66)) LastDisassembleData.OpCode = "pusha";
                        else
                            LastDisassembleData.OpCode = "pushad";

                        if (Is64Bit)
                        {
                            description += " (invalid)";
                            LastDisassembleData.OpCode = "pushad (invalid)";
                        }
                    }
                    break;

                case 0x61:
                    {
                        description = "pop all general-purpose registers";
                        if (_prefix2.Contains(0x66)) LastDisassembleData.OpCode = "popa";
                        else
                            LastDisassembleData.OpCode = "popad";

                        if (Is64Bit)
                        {
                            description += " (invalid)";
                            LastDisassembleData.OpCode = "popad (invalid)";
                        }

                    }
                    break;

                case 0x62:
                    {
                        //bound
                        description = "check array index against bounds";
                        LastDisassembleData.OpCode = "bound";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = R16(memory[1]) + ModRm(memory, _prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            LastDisassembleData.Parameters = R32(memory[1]) + ModRm(memory, _prefix2, 1, 0, ref last, ATmrPos.Right);

                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());

                    }
                    break;

                case 0x63:
                    {
                        //arpl or movsxd
                        if (Is64Bit)
                        {
                            LastDisassembleData.OpCode = "movsxd";
                            _opCodeFlags.W = false;

                            LastDisassembleData.Parameters = ' ' + R64(memory[1]) + ModRm(memory, _prefix2, 1, 0, ref last, 32, 0, ATmrPos.Right);
                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                            description = "Move doubleword to quadword with signextension";
                        }
                        else
                        {
                            LastDisassembleData.OpCode = "arpl";
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last) + R16(memory[1]);
                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                            description = "adjust rpl field of segment selector";
                        }
                    }
                    break;

                case 0x68:
                    {
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        if (_prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                            LastDisassembleData.OpCode = "push";
                            LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;

                            LastDisassembleData.OpCode = "push";
                            if (SymbolHandler.Process.IsX64)
                            {
                                var intptr = memory.ToIntPtr(1).ReadInt32();
                                LastDisassembleData.ParameterValue = (UIntPtr)intptr;
                                LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)intptr, 8);
                            }
                            else
                                LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)dwordptr, 8);
                            offset += 4;
                        }
                        description = "push word or doubleword onto the stack (sign extended)";
                    }
                    break;

                case 0x69:
                    {
                        description = "signed multiply";
                        if (_prefix2.Contains(0x66))
                        {
                            LastDisassembleData.OpCode = "imul";
                            LastDisassembleData.Parameters = R16(memory[1]) + ModRm(memory, _prefix2, 1, 1, ref last, ATmrPos.Right);
                            var wordptr = memory.ReadUInt16((int)last);

                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                            offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 2);
                        }
                        else
                        {
                            LastDisassembleData.OpCode = "imul";
                            LastDisassembleData.Parameters = R32(memory[1]) + ModRm(memory, _prefix2, 1, 0, ref last, ATmrPos.Right);
                            var dwordptr = memory.ReadUInt32((int)last);
                            if (RexW)
                                LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + IntToHexSigned((UIntPtr)dwordptr, 8);
                            else
                                LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + IntToHexSigned((UIntPtr)dwordptr, 8);

                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;

                            offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 4);
                        }
                    }
                    break;

                case 0x6a:
                    {
                        LastDisassembleData.OpCode = "push";

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)memory[1];

                        LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory[1], 2, true, 1);
                        offset += 1;
                        description = "push byte onto the stack";
                    }
                    break;

                case 0x6b:
                    {

                        description = "signed multiply";
                        LastDisassembleData.OpCode = "imul";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = R16(memory[1]) + ModRm(memory, _prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            LastDisassembleData.Parameters = R32(memory[1]) + ModRm(memory, _prefix2, 1, 0, ref last, ATmrPos.Right);

                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 1);
                    }
                    break;

                case 0x6c:
                    {
                        //m8, dx
                        description = "input from port to string";
                        LastDisassembleData.OpCode = "insb";
                    }
                    break;

                case 0x6d:
                    {
                        //m8, dx
                        description = "input from port to string";
                        if (_prefix2.Contains(0x66)) LastDisassembleData.OpCode = "insw";
                        else
                            LastDisassembleData.OpCode = "insd";
                    }
                    break;

                case 0x6e:
                    {
                        //m8, dx
                        description = "output string to port";
                        LastDisassembleData.OpCode = "outsb";
                    }
                    break;

                case 0x6f:
                    {
                        //m8, dx
                        description = "output string to port";
                        if (_prefix2.Contains(0x66)) LastDisassembleData.OpCode = "outsw";
                        else
                            LastDisassembleData.OpCode = "outsd";
                    }
                    break;


                case 0x70:
                    {
                        description = "jump short if overflow (OF=1)";
                        LastDisassembleData.OpCode = "jo";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_of) != 0;

                        offset += 1;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);



                    }
                    break;

                case 0x71:
                    {
                        description = "jump short if not overflow (OF=0)";
                        LastDisassembleData.OpCode = "jno";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_of) == 0;

                        offset += 1;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                    }
                    break;

                case 0x72:
                    {
                        description = "jump short if below/carry (CF=1)";
                        LastDisassembleData.OpCode = "jb";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_cf) != 0;
                        offset += 1;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                    }
                    break;

                case 0x73:
                    {
                        description = "jump short if above or equal (CF=0)";
                        LastDisassembleData.OpCode = "jae";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_cf) == 0;

                        offset += 1;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                    }
                    break;

                case 0x74:
                    {
                        description = "jump short if equal (ZF=1)";
                        LastDisassembleData.OpCode = "je";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_zf) != 0;

                        offset += 1;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);



                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                    }
                    break;

                case 0x75:
                    {
                        description = "jump short if not equal (ZF=0)";
                        LastDisassembleData.OpCode = "jne";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_zf) == 0;
                        offset += 1;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                    }
                    break;

                case 0x76:
                    {
                        description = "jump short if not above (ZF=1 or CF=1)";
                        LastDisassembleData.OpCode = "jna";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & (eflags_cf | eflags_zf)) != 0;


                        offset += 1;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                    }
                    break;

                case 0x77:
                    {
                        description = "jump short if above (ZF=0 and CF=0)";
                        LastDisassembleData.OpCode = "ja";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & (eflags_cf | eflags_zf)) == 0;


                        offset += 1;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                    }
                    break;

                case 0x78:
                    {
                        description = "jump short if sign (SF=1)";
                        LastDisassembleData.OpCode = "js";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_sf) != 0;

                        offset += 1;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                    }
                    break;

                case 0x79:
                    {
                        description = "jump short if not sign (SF=0)";
                        LastDisassembleData.OpCode = "jns";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_sf) == 0;

                        offset += 1;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                    }
                    break;

                case 0x7a:
                    {
                        description = "jump short if parity (PF=1)";
                        LastDisassembleData.OpCode = "jp";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_pf) != 0;

                        offset += 1;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                    }
                    break;

                case 0x7b:
                    {
                        description = "jump short if not parity (PF=0)";
                        LastDisassembleData.OpCode = "jnp";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_pf) == 0;

                        offset += 1;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                    }
                    break;

                case 0x7c:
                    {
                        description = "jump short if not greater or equal (SF~=OF)";
                        LastDisassembleData.OpCode = "jl"; //jnge
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_sf) != (context->eflags & eflags_of);


                        offset += 1;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                    }
                    break;

                case 0x7d:
                    {
                        description = "jump short if not less (greater or equal) (SF=OF)";
                        LastDisassembleData.OpCode = "jnl";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_sf) == (context->eflags & eflags_of);


                        offset += 1;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                    }
                    break;

                case 0x7e:
                    {
                        description = "jump short if less or equal (ZF=1 or SF~=OF)";
                        LastDisassembleData.OpCode = "jle";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //lastdisassembledata.willjumpaccordingtocontext = ((context->eflags & eflags_sf) != (context->eflags & eflags_of)) || ((context->eflags & eflags_zf) != 0);


                        offset += 1;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                    }
                    break;

                case 0x7f:
                    {
                        description = "jump short if greater (ZF=0 or SF=OF)";
                        LastDisassembleData.OpCode = "jg";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //lastdisassembledata.willjumpaccordingtocontext = ((context->eflags & eflags_sf) == (context->eflags & eflags_of)) && ((context->eflags & eflags_zf) == 0);


                        offset += 1;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                    }
                    break;

                case 0x80:
                case 0x82:
                    {
                        switch (GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    //add
                                    LastDisassembleData.OpCode = "add";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);
                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "add x to y";
                                }
                                break;

                            case 1:
                                {
                                    //adc
                                    LastDisassembleData.OpCode = "or";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);
                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "logical inclusive or";
                                }
                                break;


                            case 2:
                                {
                                    //adc
                                    LastDisassembleData.OpCode = "adc";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);
                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "add with carry";
                                }
                                break;

                            case 3:
                                {
                                    //sbb
                                    LastDisassembleData.OpCode = "sbb";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);
                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "integer subtraction with borrow";
                                }
                                break;

                            case 4:
                                {
                                    //and
                                    LastDisassembleData.OpCode = "and";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);
                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "logical and";
                                }
                                break;

                            case 5:
                                {
                                    LastDisassembleData.OpCode = "sub";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);
                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "subtract";
                                }
                                break;

                            case 6:
                                {
                                    LastDisassembleData.OpCode = "xor";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);
                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "logical exclusive or";
                                }
                                break;

                            case 7:
                                {
                                    LastDisassembleData.OpCode = "cmp";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);
                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "compare two operands";
                                }
                                break;

                        }
                    }
                    break;

                case 0x81:
                    {
                        switch (GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    //add
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "add";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        var wordptr = memory.ReadUInt16((int)last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                                        LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)wordptr, 4);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 2);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "add";
                                        if (RexW)
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                        else
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);
                                        var dwordptr = memory.ReadUInt32((int)last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;

                                        if (RexW)
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)dwordptr, 8);
                                        else
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)dwordptr, 8);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 4);
                                    }

                                    //                      offset:=offset+last;
                                    description = "add x to y";
                                }
                                break;

                            case 1:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "or";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        var wordptr = memory.ReadUInt16((int)last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                                        LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)wordptr, 4);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 2);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "or";
                                        if (RexW)
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                        else
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                        var dwordptr = memory.ReadUInt32((int)last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;

                                        if (RexW)
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)dwordptr, 8);
                                        else
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)dwordptr, 8);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 4);
                                    }


                                    description = "logical inclusive or";
                                }
                                break;

                            case 2:
                                {
                                    //adc
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "adc";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        var wordptr = memory.ReadUInt16((int)last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                                        LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)wordptr, 4);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 2);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "adc";
                                        if (RexW)
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                        else
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);
                                        var dwordptr = memory.ReadUInt32((int)last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;

                                        if (RexW)
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)dwordptr, 8);
                                        else
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)dwordptr, 8);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 4);
                                    }


                                    description = "add with carry";
                                }
                                break;

                            case 3:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "sbb";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        var wordptr = memory.ReadUInt16((int)last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                                        LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)wordptr, 4);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 2);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "sbb";
                                        if (RexW)
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                        else
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);
                                        var dwordptr = memory.ReadUInt32((int)last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;

                                        if (RexW)
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)dwordptr, 8);
                                        else
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)dwordptr, 8);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 4);
                                    }


                                    description = "integer subtraction with borrow";
                                }
                                break;


                            case 4:
                                {
                                    //and
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "and";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        var wordptr = memory.ReadUInt16((int)last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                                        LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)wordptr, 4);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 2);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "and";
                                        if (RexW)
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                        else
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);
                                        var dwordptr = memory.ReadUInt32((int)last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;

                                        if (RexW)
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)dwordptr, 8);
                                        else
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)dwordptr, 8);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 4);
                                    }


                                    description = "logical and";
                                }
                                break;

                            case 5:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "sub";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last);
                                        var wordptr = memory.ReadUInt16((int)last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                                        LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)wordptr, 4);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 2);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "sub";
                                        if (RexW)
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                        else
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);
                                        var dwordptr = memory.ReadUInt32((int)last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;

                                        if (RexW)
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)dwordptr, 8);
                                        else
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)dwordptr, 8);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 4);
                                    }


                                    description = "subtract";
                                }
                                break;

                            case 6:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "xor";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        var wordptr = memory.ReadUInt16((int)last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                                        LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)wordptr, 4);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 2);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "xor";
                                        if (RexW)
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                        else
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);
                                        var dwordptr = memory.ReadUInt32((int)last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;

                                        if (RexW)
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)dwordptr, 8);
                                        else
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)dwordptr, 8);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 4);
                                    }
                                    description = "logical exclusive or";
                                }
                                break;

                            case 7:
                                {
                                    //cmp
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "cmp";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        var wordptr = memory.ReadUInt16((int)last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                                        LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)wordptr, 4);
                                        offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 2);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "cmp";
                                        if (RexW)
                                        {
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                            LastDisassembleData.DataSize = 8; ;
                                        }
                                        else
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);
                                        var dwordptr = memory.ReadUInt32((int)last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;

                                        if (RexW)
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)dwordptr, 8);
                                        else
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)dwordptr, 8);
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
                        switch (GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "add";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];

                                        LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2, true);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "add";

                                        if (RexW)
                                        {
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2, true);
                                        }
                                        else
                                        {
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];

                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2, true);
                                        }

                                    }

                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "add (sign extended)";
                                }
                                break;

                            case 1:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "or";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];

                                        LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "or";
                                        if (RexW)
                                        {
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                        else
                                        {
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                    }

                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "add (sign extended)";
                                }
                                break;


                            case 2:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "adc";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "adc";
                                        if (RexW)
                                        {
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                        else
                                        {
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }

                                    }

                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "add with carry (sign extended)";
                                }
                                break;

                            case 3:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "sbb";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];

                                        LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "sbb";
                                        if (RexW)
                                        {
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                        else
                                        {
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                    }

                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "integer subtraction with borrow (sign extended)";
                                }
                                break;

                            case 4:
                                {
                                    //and
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "and";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "and";
                                        if (RexW)
                                        {
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                        else
                                        {
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }

                                    }

                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "logical and (sign extended)";
                                }
                                break;

                            case 5:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "sub";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "sub";
                                        if (RexW)
                                        {
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                        else
                                        {
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                    }

                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "subtract";
                                }
                                break;

                            case 6:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "xor";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "xor";
                                        if (RexW)
                                        {
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                        else
                                        {
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                    }

                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                    description = "logical exclusive or";
                                }
                                break;

                            case 7:
                                {
                                    //cmp
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "cmp";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "cmp";
                                        if (RexW)
                                        {
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        }
                                        else
                                        {
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                            LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
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
                        LastDisassembleData.OpCode = "test";
                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last) + R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x85:
                    {
                        description = "logical compare";
                        LastDisassembleData.OpCode = "test";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last) + R16(memory[1]);
                        else
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x86:
                    {
                        description = "exchange memory with register";
                        LastDisassembleData.OpCode = "xchg";
                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last) + R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x87:
                    {
                        description = "exchange memory with register";
                        LastDisassembleData.OpCode = "xchg";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last) + R16(memory[1]);
                        else
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x88:
                    {
                        description = "copy memory";
                        LastDisassembleData.OpCode = "mov";
                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last) + R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x89:
                    {
                        description = "copy memory";
                        LastDisassembleData.OpCode = "mov";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last) + R16(memory[1]);
                        else
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x8a:
                    {
                        description = "copy memory";
                        LastDisassembleData.OpCode = "mov";
                        LastDisassembleData.Parameters = R8(memory[1]) + ModRm(memory, _prefix2, 1, 2, ref last, ATmrPos.Right);

                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x8b:
                    {
                        description = "copy memory";
                        LastDisassembleData.OpCode = "mov";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = R16(memory[1]) + ModRm(memory, _prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            LastDisassembleData.Parameters = R32(memory[1]) + ModRm(memory, _prefix2, 1, 0, ref last, ATmrPos.Right);

                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x8c:
                    {
                        description = "copy memory";
                        LastDisassembleData.OpCode = "mov";
                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last) + SReg(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x8d:
                    {
                        description = "load effective address";
                        LastDisassembleData.OpCode = "lea";
                        if (_prefix2.Contains(0x66))
                        {
                            if (SymbolHandler.Process.IsX64 & (_prefix2.Contains(0x67)))
                                LastDisassembleData.Parameters = R16(memory[1]) + ModRm(memory, _prefix2, 1, 1, ref last, 0, 32, ATmrPos.Right);
                            else
                                LastDisassembleData.Parameters = R16(memory[1]) + ModRm(memory, _prefix2, 1, 1, ref last, 0, 0, ATmrPos.Right);
                        }
                        else
                        {
                            if (SymbolHandler.Process.IsX64 & (_prefix2.Contains(0x67)))
                                LastDisassembleData.Parameters = R32(memory[1]) + ModRm(memory, _prefix2, 1, 0, ref last, 0, 32, ATmrPos.Right);
                            else
                                LastDisassembleData.Parameters = R32(memory[1]) + ModRm(memory, _prefix2, 1, 0, ref last, 0, 0, ATmrPos.Right);
                        }

                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x8e:
                    {
                        description = "copy memory";
                        LastDisassembleData.OpCode = "mov";
                        LastDisassembleData.Parameters = SReg(memory[1]) + ModRm(memory, _prefix2, 1, 1, ref last, ATmrPos.Right);

                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x8f:
                    {
                        switch (GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "pop a value from the stack";
                                    LastDisassembleData.OpCode = "pop";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            default:
                                {
                                    LastDisassembleData.OpCode = "db";
                                    LastDisassembleData.Parameters = _colorHex + "8f" + _endColor;
                                    description = "undefined by the intel specification";
                                }
                                break;
                        }
                    }
                    break;


                case 0x90:
                    {
                        description = "no operation";
                        LastDisassembleData.OpCode = "nop";
                        if (prefixSize > 0)
                            LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)(prefixSize + 1), 1);
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
                        LastDisassembleData.OpCode = "xchg";

                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = _colorReg + "ax" + _endColor + ',' + Rd16((Byte)(memory[0] - 0x90));
                        else
                        {
                            if (RexW)
                                LastDisassembleData.Parameters = _colorReg + "rax" + _endColor + ',' + Rd((Byte)(memory[0] - 0x90));
                            else
                                LastDisassembleData.Parameters = _colorReg + "eax" + _endColor + ',' + Rd((Byte)(memory[0] - 0x90));
                        }
                    }
                    break;


                case 0x98:
                    {
                        //cbw/cwde
                        if (_prefix2.Contains(0x66))
                        {
                            LastDisassembleData.OpCode = "cbw";
                            description = "convert byte to word";
                        }
                        else
                        {
                            if (RexW)
                            {
                                LastDisassembleData.OpCode = "cdqe";
                                description = "convert doubleword to quadword";
                            }
                            else
                            {
                                LastDisassembleData.OpCode = "cwde";
                                description = "convert word to doubleword";
                            }
                        }
                    }
                    break;

                case 0x99:
                    {
                        if (_prefix2.Contains(0x66))
                        {
                            description = "convert word to doubleword";
                            LastDisassembleData.OpCode = "cwd";
                        }
                        else
                        {

                            if (RexW)
                            {
                                LastDisassembleData.OpCode = "cqo";
                                description = "convert quadword to octword";
                            }
                            else
                            {
                                LastDisassembleData.OpCode = "cdq";
                                description = "convert doubleword to quadword";
                            }
                        }
                    }
                    break;

                case 0x9a:
                    {
                        description = "call procedure";
                        var wordptr = memory.ReadUInt16(5);

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 5;
                        LastDisassembleData.SeparatorCount += 1;



                        if (Is64Bit)
                            LastDisassembleData.OpCode = "call (invalid)";
                        else
                            LastDisassembleData.OpCode = "call";


                        LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)wordptr, 4) + ':';
                        var dwordptr = memory.ToIntPtr(1).ReadUInt32();

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;

                        offset += 6;
                    }
                    break;

                case 0x9b:
                    {
                        switch (memory[1])
                        {

                            case 0xd9:
                                {
                                    switch (GetReg(memory[2]))
                                    {
                                        case 6:
                                            {
                                                description = "store fpu environment";
                                                LastDisassembleData.OpCode = "wait:fstenv";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;


                                        case 7:
                                            {
                                                description = "store control word";
                                                LastDisassembleData.OpCode = "wait:fstcw";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        default:
                                            {
                                                description = "wait";
                                                LastDisassembleData.OpCode = "wait";
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
                                                LastDisassembleData.OpCode = "wait:fclex";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xe3:
                                            {
                                                description = "initialize floaring-point unit";
                                                LastDisassembleData.OpCode = "wait:finit";
                                                offset += 2;
                                            }
                                            break;
                                        default:
                                            {
                                                description = "wait";
                                                LastDisassembleData.OpCode = "wait";
                                            }
                                            break;
                                    }
                                }
                                break;

                            case 0xdd:
                                {
                                    switch (GetReg(memory[2]))
                                    {
                                        case 6:
                                            {
                                                description = "store fpu state";
                                                LastDisassembleData.OpCode = "wait:fsave";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 7:
                                            {
                                                description = "store status word";
                                                LastDisassembleData.OpCode = "wait:fstsw";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        default:
                                            {
                                                description = "wait";
                                                LastDisassembleData.OpCode = "wait";
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
                                                LastDisassembleData.OpCode = "wait:fstsw ax";
                                                offset += 2;
                                            }
                                            break;

                                        default:
                                            {
                                                description = "wait";
                                                LastDisassembleData.OpCode = "wait";
                                            }
                                            break;
                                    }
                                }
                                break;

                            default:
                                {
                                    description = "wait";
                                    LastDisassembleData.OpCode = "wait";
                                }
                                break;

                        }

                    }
                    break;

                case 0x9c:
                    {
                        description = "push eflags register onto the stack";
                        if (_prefix2.Contains(0x66)) LastDisassembleData.OpCode = "pushf";
                        else
                        {
                            if (Is64Bit)
                                LastDisassembleData.OpCode = "pushfq";
                            else
                                LastDisassembleData.OpCode = "pushfd";
                        }
                    }
                    break;

                case 0x9d:
                    {
                        description = "pop stack into eflags register";
                        if (_prefix2.Contains(0x66)) LastDisassembleData.OpCode = "popf";
                        else
                        {
                            if (Is64Bit)
                                LastDisassembleData.OpCode = "popfq";
                            else
                                LastDisassembleData.OpCode = "popfd";
                        }
                    }
                    break;

                case 0x9e:
                    {
                        description = "store ah into flags";
                        LastDisassembleData.OpCode = "sahf";
                    }
                    break;

                case 0x9f:
                    {
                        description = "load status flag into ah register";
                        LastDisassembleData.OpCode = "lahf";
                    }
                    break;

                case 0xa0:
                    {
                        description = "copy memory";
                        var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                        LastDisassembleData.OpCode = "mov";
                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;


                        if (SymbolHandler.Process.IsX64)
                        {
                            var qwordptr = memory.ToIntPtr(1).ReadUInt64();
                            LastDisassembleData.ParameterValue = (UIntPtr)qwordptr;
                            LastDisassembleData.Parameters = _colorReg + "al" + _endColor + ',' + GetSegmentOverride(_prefix2) + '[' + IntToHexSigned((UIntPtr)qwordptr, 8) + ']';
                            offset += 8;
                        }
                        else
                        {
                            LastDisassembleData.Parameters = _colorReg + "al" + _endColor + ',' + GetSegmentOverride(_prefix2) + '[' + IntToHexSigned((UIntPtr)dwordptr, 8) + ']';
                            offset += 4;
                        }


                    }
                    break;

                case 0xa1:
                    {
                        description = "copy memory";
                        LastDisassembleData.OpCode = "mov";
                        var dwordptr = memory.ToIntPtr(1).ReadUInt32();


                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        if (_prefix2.Contains(0x66))
                        {
                            LastDisassembleData.Parameters = _colorReg + "ax" + _endColor + ',' + GetSegmentOverride(_prefix2) + '[' + IntToHexSigned((UIntPtr)dwordptr, 8) + ']';
                        }
                        else
                        {
                            if (RexW)
                                LastDisassembleData.Parameters = _colorReg + "rax" + _endColor + ',';
                            else
                                LastDisassembleData.Parameters = _colorReg + "eax" + _endColor + ',';


                            if (SymbolHandler.Process.IsX64)
                            {
                                var qwordptr = memory.ToIntPtr(1).ReadUInt64();
                                LastDisassembleData.ParameterValue = (UIntPtr)qwordptr;
                                LastDisassembleData.Parameters = LastDisassembleData.Parameters + GetSegmentOverride(_prefix2) + '[' + IntToHexSigned((UIntPtr)qwordptr, 8) + ']';
                            }
                            else
                                LastDisassembleData.Parameters = LastDisassembleData.Parameters + GetSegmentOverride(_prefix2) + '[' + IntToHexSigned((UIntPtr)dwordptr, 8) + ']';

                        }

                        if (SymbolHandler.Process.IsX64)
                            offset += 8;
                        else
                            offset += 4;

                    }
                    break;

                case 0xa2:
                    {
                        description = "copy memory";
                        var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                        LastDisassembleData.OpCode = "mov";

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        if (SymbolHandler.Process.IsX64)
                        {
                            var qwordptr = memory.ToIntPtr(1).ReadUInt64();
                            LastDisassembleData.ParameterValue = (UIntPtr)qwordptr;
                            LastDisassembleData.Parameters = GetSegmentOverride(_prefix2) + '[' + IntToHexSigned((UIntPtr)qwordptr, 8) + "]," + _colorReg + "al" + _endColor;
                        }
                        else
                            LastDisassembleData.Parameters = GetSegmentOverride(_prefix2) + '[' + IntToHexSigned((UIntPtr)dwordptr, 8) + "]," + _colorReg + "al" + _endColor;

                        if (SymbolHandler.Process.IsX64)
                            offset += 8;
                        else
                            offset += 4;
                    }
                    break;

                case 0xa3:
                    {
                        description = "copy memory";
                        LastDisassembleData.OpCode = "mov";
                        var dwordptr = memory.ToIntPtr(1).ReadUInt32();

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        if (SymbolHandler.Process.IsX64)
                        {
                            var qwordptr = memory.ToIntPtr(1).ReadUInt64();
                            LastDisassembleData.ParameterValue = (UIntPtr)qwordptr;
                            LastDisassembleData.Parameters = GetSegmentOverride(_prefix2) + '[' + IntToHexSigned((UIntPtr)qwordptr, 8) + "],";
                        }
                        else
                            LastDisassembleData.Parameters = GetSegmentOverride(_prefix2) + '[' + IntToHexSigned((UIntPtr)dwordptr, 8) + "],";

                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = LastDisassembleData.Parameters + _colorReg + "ax" + _endColor;
                        else
                        {
                            if (RexW)
                                LastDisassembleData.Parameters = LastDisassembleData.Parameters + _colorReg + "rax" + _endColor;
                            else
                                LastDisassembleData.Parameters = LastDisassembleData.Parameters + _colorReg + "eax" + _endColor;
                        }

                        if (SymbolHandler.Process.IsX64)
                            offset += 8;
                        else
                            offset += 4;
                    }
                    break;

                case 0xa4:
                    {
                        description = "move data from string to string";
                        LastDisassembleData.OpCode = "movsb";
                    }
                    break;

                case 0xa5:
                    {
                        description = "move data from string to string";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.OpCode = "movsw";
                        else
                        {
                            if (RexW)
                                LastDisassembleData.OpCode = "movsq";
                            else
                                LastDisassembleData.OpCode = "movsd";
                        }
                    }
                    break;

                case 0xa6:
                    {
                        description = "compare string operands";
                        LastDisassembleData.OpCode = "cmpsb";
                    }
                    break;

                case 0xa7:
                    {
                        description = "compare string operands";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.OpCode = "cmpsw";
                        else
                        {
                            if (RexW)
                                LastDisassembleData.OpCode = "cmpsq";
                            else
                                LastDisassembleData.OpCode = "cmpsd";
                        }
                    }
                    break;

                case 0xa8:
                    {
                        description = "logical compare";
                        LastDisassembleData.OpCode = "test";

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.Parameters = _colorReg + "al" + _endColor + ',' + IntToHexSigned((UIntPtr)memory[1], 2);
                        offset += 1;
                    }
                    break;

                case 0xa9:
                    {
                        description = "logical compare";
                        LastDisassembleData.OpCode = "test";

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        if (_prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                            LastDisassembleData.Parameters = _colorReg + "ax" + _endColor + ',' + IntToHexSigned((UIntPtr)wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;

                            if (RexW)
                                LastDisassembleData.Parameters = _colorReg + "rax" + _endColor + ',' + IntToHexSigned((UIntPtr)dwordptr, 8);
                            else
                                LastDisassembleData.Parameters = _colorReg + "eax" + _endColor + ',' + IntToHexSigned((UIntPtr)dwordptr, 8);
                            offset += 4;
                        }
                    }
                    break;

                case 0xaa:
                    {
                        description = "store string";
                        LastDisassembleData.OpCode = "stosb";
                    }
                    break;

                case 0xab:
                    {
                        description = "store string";
                        if (_prefix2.Contains(0x66)) LastDisassembleData.OpCode = "stosw";
                        else
                        {
                            if (RexW)
                                LastDisassembleData.OpCode = "stosq";
                            else
                                LastDisassembleData.OpCode = "stosd";
                        }
                    }
                    break;

                case 0xac:
                    {
                        description = "load string";
                        LastDisassembleData.OpCode = "lodsb";
                    }
                    break;

                case 0xad:
                    {
                        description = "load string";
                        if (_prefix2.Contains(0x66)) LastDisassembleData.OpCode = "lodsw";
                        else
                        {
                            if (RexW)
                                LastDisassembleData.OpCode = "lodsq";
                            else
                                LastDisassembleData.OpCode = "lodsd";
                        }
                    }
                    break;

                case 0xae:
                    {
                        description = "compare al with byte at es:edi and set status flag";
                        LastDisassembleData.OpCode = "scasb";
                    }
                    break;

                case 0xaf:
                    {
                        description = "scan string";
                        if (_prefix2.Contains(0x66)) LastDisassembleData.OpCode = "scasw";
                        else
                        {
                            if (RexW)
                                LastDisassembleData.OpCode = "scasq";
                            else
                                LastDisassembleData.OpCode = "scasd";
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
                        LastDisassembleData.OpCode = "mov";
                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        //              if Rex_B

                        LastDisassembleData.Parameters = Rd8((Byte)(memory[0] - 0xb0)) + ',' + IntToHexSigned((UIntPtr)memory[1], 2);
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

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;


                        if (_prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                            LastDisassembleData.OpCode = "mov";
                            LastDisassembleData.Parameters = Rd16((Byte)(memory[0] - 0xb8)) + ',' + IntToHexSigned((UIntPtr)wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;


                            if (RexW)
                            {
                                LastDisassembleData.OpCode = "mov";
                                LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                                LastDisassembleData.Parameters = Rd((Byte)(memory[0] - 0xb8)) + ',' + IntToHexSigned((UIntPtr)dwordptr, 16);
                                offset += 8;
                            }
                            else
                            {
                                LastDisassembleData.OpCode = "mov";
                                LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;

                                LastDisassembleData.Parameters = Rd((Byte)(memory[0] - 0xb8)) + ',' + IntToHexSigned((UIntPtr)dwordptr, 8);
                                offset += 4;
                            }
                        }
                    }
                    break;

                case 0xc0:
                    {
                        switch (GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    LastDisassembleData.OpCode = "rol";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);

                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];

                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    description = "rotate eight bits left " + (memory[(int)last]) + " times";
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                }
                                break;

                            case 1:
                                {
                                    LastDisassembleData.OpCode = "ror";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);
                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    description = "rotate eight bits right " + (memory[(int)last]) + " times";
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                }
                                break;

                            case 2:
                                {
                                    LastDisassembleData.OpCode = "rcl";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);
                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    description = "rotate nine bits left " + (memory[(int)last]) + " times";
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                }
                                break;

                            case 3:
                                {
                                    LastDisassembleData.OpCode = "rcr";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);
                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    description = "rotate nine bits right " + (memory[(int)last]) + " times";
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                }
                                break;

                            case 4:
                                {
                                    LastDisassembleData.OpCode = "shl";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);
                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    description = "multiply by 2, " + (memory[(int)last]) + " times";
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                }
                                break;

                            case 5:
                                {
                                    LastDisassembleData.OpCode = "shr";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);
                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    description = "unsigned divide by 2, " + (memory[(int)last]) + " times";
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                }
                                break;

                            /*not in intel spec*/
                            case 6:
                                {
                                    LastDisassembleData.OpCode = "rol";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);
                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    description = "rotate eight bits left " + (memory[(int)last]) + " times";
                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                }
                                break;
                            /*^^^^^^^^^^^^^^^^^^*/

                            case 7:
                                {
                                    LastDisassembleData.OpCode = "sar";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);
                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
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
