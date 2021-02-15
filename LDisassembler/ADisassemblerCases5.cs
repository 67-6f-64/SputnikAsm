using System;
using Sputnik.LBinary;
using Sputnik.LMarshal;
using SputnikAsm.LDisassembler.LEnums;
using SputnikAsm.LUtils;

namespace SputnikAsm.LDisassembler
{
    public partial class ADisassembler
    {
        #region DisassembleProcess5
        private Boolean DisassembleProcess5(UBytePtr memory, ref UIntPtr offset, ref int prefixSize, ref UInt32 last, ref String description)
        {
            switch (memory[0])
            {
                case 0xc1:
                    {
                        switch (GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "rol";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "rotate 16 bits left " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "rol";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];

                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "rotate 32 bits left " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;

                            case 1:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "ror";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "rotate 16 bits right " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "ror";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "rotate 32 bits right " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;

                            case 2:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "rcl";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "rotate 17 bits left " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "rcl";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "rotate 33 bits left " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;

                            case 3:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "rcr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "rotate 17 bits right " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "rcr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "rotate 33 bits right " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;

                            case 4:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "shl";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "multiply by 2 " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "shl";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "multiply by 2 " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;

                            case 5:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "shr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "unsigned divide by 2 " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "shr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "unsigned divide by 2 " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;

                            case 7:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "sar";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        description = "signed divide by 2 " + (memory[(int)last]) + " times";
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "sar";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);
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
                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.OpCode = "ret";
                        LastDisassembleData.IsRet = true;
                        LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)wordptr, 4);
                        offset += 2;

                        description = "near return to calling procedure and pop " + (LastDisassembleData.ParameterValue) + " bytes from stack";


                    }
                    break;

                case 0xc3:
                    {
                        description = "near return to calling procedure";
                        LastDisassembleData.OpCode = "ret";
                        LastDisassembleData.IsRet = true;
                    }
                    break;

                case 0xc4:
                    {
                        if (SymbolHandler.Process.IsX64 == false)
                        {
                            description = "load far pointer";
                            LastDisassembleData.OpCode = "les";
                            if (_prefix2.Contains(0x66))
                                LastDisassembleData.Parameters = R16(memory[1]) + ModRm(memory, _prefix2, 1, 1, ref last, ATmrPos.Right);
                            else
                                LastDisassembleData.Parameters = R32(memory[1]) + ModRm(memory, _prefix2, 1, 0, ref last, ATmrPos.Right);

                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                        }
                    }
                    break;

                case 0xc5:
                    {
                        if (SymbolHandler.Process.IsX64 == false)
                        {
                            description = "load far pointer";
                            LastDisassembleData.OpCode = "lds";
                            if (_prefix2.Contains(0x66))
                                LastDisassembleData.Parameters = R16(memory[1]) + ModRm(memory, _prefix2, 1, 1, ref last, ATmrPos.Right);
                            else
                                LastDisassembleData.Parameters = R32(memory[1]) + ModRm(memory, _prefix2, 1, 0, ref last, ATmrPos.Right);

                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                        }
                    }
                    break;

                case 0xc6:
                    {
                        if (memory[1] == 0xf8)
                        {
                            offset += 1;
                            LastDisassembleData.OpCode = "xabort";
                            description = "transactional abort";

                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)memory[2];
                            LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                            LastDisassembleData.SeparatorCount += 1;
                            LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory[2], 2);

                        }
                        else
                            switch (GetReg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "copy memory";
                                        LastDisassembleData.OpCode = "mov";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];

                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    break;

                                default:
                                    {
                                        description = "not defined by the intel documentation";
                                        LastDisassembleData.OpCode = "db";
                                        LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory[0], 2);
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
                            LastDisassembleData.OpCode = "xbegin";

                            if (MarkIpRelativeInstructions)
                            {
                                LastDisassembleData.RipRelative = 1;
                                _ripRelative = true;
                            }
                            offset += 4;
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;



                            if (Is64Bit)
                                LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));
                            else
                                LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(2));

                            LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                            LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                            LastDisassembleData.SeparatorCount += 1;

                        }
                        else
                            switch (GetReg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "copy memory";
                                        if (_prefix2.Contains(0x66))
                                        {
                                            LastDisassembleData.OpCode = "mov";
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);

                                            var wordptr = memory.ReadUInt16((int)last);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                                            LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)wordptr, 4);
                                            offset = (UIntPtr)(offset.ToUInt64() + last + 1);
                                        }
                                        else
                                        {
                                            LastDisassembleData.OpCode = "mov";

                                            if (RexW)
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                            else
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                            var dwordptr = memory.ReadUInt32((int)last);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                            LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;


                                            if (RexW)
                                                LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)dwordptr, 8);
                                            else
                                                LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)dwordptr, 8);

                                            offset = (UIntPtr)(offset.ToUInt64() + last + 3);
                                        }
                                    }
                                    break;

                                default:
                                    {
                                        description = "not defined by the intel documentation";
                                        LastDisassembleData.OpCode = "db";
                                        LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory[0], 2);
                                    }
                                    break;

                            }
                    }
                    break;

                case 0xc8:
                    {
                        description = "make stack frame for procedure parameters";
                        var wordptr = memory.ToIntPtr(1).ReadUInt16();
                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 3;
                        LastDisassembleData.SeparatorCount += 1;


                        LastDisassembleData.OpCode = "enter";
                        LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)wordptr, 4) + ',' + IntToHexSigned((UIntPtr)memory[3], 2);
                        offset += 3;
                    }
                    break;

                case 0xc9:
                    {
                        description = "high level procedure exit";
                        LastDisassembleData.OpCode = "leave";
                    }
                    break;

                case 0xca:
                    {
                        description = "far return to calling procedure and pop 2 bytes from stack";
                        var wordptr = memory.ToIntPtr(1).ReadUInt16();
                        LastDisassembleData.OpCode = "ret";
                        LastDisassembleData.IsRet = true;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)wordptr, 4);
                        offset += 2;
                    }
                    break;

                case 0xcb:
                    {
                        description = "far return to calling procedure";
                        LastDisassembleData.OpCode = "ret";
                        LastDisassembleData.IsRet = true;
                    }
                    break;

                case 0xcc:
                    {
                        //should not be shown if its being debugged using int 3'
                        description = "call to interrupt procedure-3:trap to debugger";
                        LastDisassembleData.OpCode = "int 3";
                    }
                    break;

                case 0xcd:
                    {
                        description = "call to interrupt procedure";
                        LastDisassembleData.OpCode = "int";
                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory[1], 2);
                        offset += 1;
                    }
                    break;

                case 0xce:
                    {
                        description = "call to interrupt procedure-4:if overflow flag=1";
                        LastDisassembleData.OpCode = "into";
                    }
                    break;

                case 0xcf:
                    {
                        description = "interrupt return";
                        if (_prefix2.Contains(0x66)) LastDisassembleData.OpCode = "iret";
                        else
                        {
                            if (RexW)
                                LastDisassembleData.OpCode = "iretq";
                            else
                                LastDisassembleData.OpCode = "iretd";
                        }
                    }
                    break;

                case 0xd0:
                    {
                        switch (GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "rotate eight bits left once";
                                    LastDisassembleData.OpCode = "rol";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8) + '1';
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 1:
                                {
                                    description = "rotate eight bits right once";
                                    LastDisassembleData.OpCode = "ror";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8) + '1';
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;


                            case 2:
                                {
                                    description = "rotate nine bits left once";
                                    LastDisassembleData.OpCode = "rcl";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8) + '1';
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 3:
                                {
                                    description = "rotate nine bits right once";
                                    LastDisassembleData.OpCode = "rcr";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8) + '1';
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 4:
                                {
                                    description = "multiply by 2, once";
                                    LastDisassembleData.OpCode = "shl";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8) + '1';
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 5:
                                {
                                    description = "unsigned divide by 2, once";
                                    LastDisassembleData.OpCode = "shr";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8) + '1';
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 6:
                                {
                                    description = "not defined by the intel documentation";
                                    LastDisassembleData.OpCode = "db";
                                    LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory[0], 2) + ' ' + IntToHexSigned((UIntPtr)memory[1], 2);
                                }
                                break;

                            case 7:
                                {
                                    description = "signed divide by 2, once";
                                    LastDisassembleData.OpCode = "sar";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8) + '1';
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                        }
                    }
                    break;

                case 0xd1:
                    {
                        switch (GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "rotate 16 bits left once";
                                        LastDisassembleData.OpCode = "rol";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "rotate 32 bits left once";
                                        LastDisassembleData.OpCode = "rol";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 1:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "rotate 16 bits right once";
                                        LastDisassembleData.OpCode = "ror";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "rotate 32 bits right once";
                                        LastDisassembleData.OpCode = "ror";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 2:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "rotate 17 bits left once";
                                        LastDisassembleData.OpCode = "rcl";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "rotate 33 bits left once";
                                        LastDisassembleData.OpCode = "rcl";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 3:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "rotate 17 bits right once";
                                        LastDisassembleData.OpCode = "rcr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "rotate 33 bits right once";
                                        LastDisassembleData.OpCode = "rcr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 4:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "multiply by 2, once";
                                        LastDisassembleData.OpCode = "shl";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "multiply by 2, once";
                                        LastDisassembleData.OpCode = "shl";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 5:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "unsigned divide by 2, once";
                                        LastDisassembleData.OpCode = "shr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unsigned divide by 2, once";
                                        LastDisassembleData.OpCode = "shr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 6:
                                {
                                    description = "undefined by the intel documentation";
                                    LastDisassembleData.OpCode = "db";
                                    LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory[0], 2);
                                }
                                break;

                            case 7:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "signed divide by 2, once";
                                        LastDisassembleData.OpCode = "sar";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "signed divide by 2, once";
                                        LastDisassembleData.OpCode = "sar";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + '1';
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                        }
                    }
                    break;


                case 0xd2:
                    {
                        switch (GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "rotate eight bits left cl times";
                                    LastDisassembleData.OpCode = "rol";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8) + _colorReg + "cl" + _endColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 1:
                                {
                                    description = "rotate eight bits right cl times";
                                    LastDisassembleData.OpCode = "ror";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8) + _colorReg + "cl" + _endColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 2:
                                {
                                    description = "rotate nine bits left cl times";
                                    LastDisassembleData.OpCode = "rcl";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8) + _colorReg + "cl" + _endColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 3:
                                {
                                    description = "rotate nine bits right cl times";
                                    LastDisassembleData.OpCode = "rcr";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8) + _colorReg + "cl" + _endColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 4:
                                {
                                    description = "multiply by 2, cl times";
                                    LastDisassembleData.OpCode = "shl";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8) + _colorReg + "cl" + _endColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 5:
                                {
                                    description = "unsigned divide by 2, cl times";
                                    LastDisassembleData.OpCode = "shr";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8) + _colorReg + "cl" + _endColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 6:
                                {
                                    description = "multiply by 2, cl times";
                                    LastDisassembleData.OpCode = "shl";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8) + _colorReg + "cl" + _endColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 7:
                                {
                                    description = "signed divide by 2, cl times";
                                    LastDisassembleData.OpCode = "sar";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8) + _colorReg + "cl" + _endColor;
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;


                        }
                    }
                    break;

                case 0xd3:
                    {
                        switch (GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "rotate 16 bits left cl times";
                                        LastDisassembleData.OpCode = "rol";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16) + _colorReg + "cl" + _endColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "rotate 32 bits left cl times";
                                        LastDisassembleData.OpCode = "rol";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + _colorReg + "cl" + _endColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 1:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "rotate 16 bits right cl times";
                                        LastDisassembleData.OpCode = "ror";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16) + _colorReg + "cl" + _endColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "rotate 32 bits right cl times";
                                        LastDisassembleData.OpCode = "ror";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + _colorReg + "cl" + _endColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 2:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "rotate 17 bits left cl times";
                                        LastDisassembleData.OpCode = "rcl";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16) + _colorReg + "cl" + _endColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "rotate 33 bits left cl times";
                                        LastDisassembleData.OpCode = "rcl";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + _colorReg + "cl" + _endColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 3:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "rotate 17 bits right cl times";
                                        LastDisassembleData.OpCode = "rcr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16) + _colorReg + "cl" + _endColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "rotate 33 bits right cl times";
                                        LastDisassembleData.OpCode = "rcr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + _colorReg + "cl" + _endColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 4:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "multiply by 2, cl times";
                                        LastDisassembleData.OpCode = "shl";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16) + _colorReg + "cl" + _endColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "multiply by 2, cl times";
                                        LastDisassembleData.OpCode = "shl";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + _colorReg + "cl" + _endColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 5:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "unsigned divide by 2, cl times";
                                        LastDisassembleData.OpCode = "shr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16) + _colorReg + "cl" + _endColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unsigned divide by 2, cl times";
                                        LastDisassembleData.OpCode = "shr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + _colorReg + "cl" + _endColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 7:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "signed divide by 2, cl times";
                                        LastDisassembleData.OpCode = "sar";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16) + _colorReg + "cl" + _endColor;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "signed divide by 2, cl times";
                                        LastDisassembleData.OpCode = "sar";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + _colorReg + "cl" + _endColor;
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
                        LastDisassembleData.OpCode = "aam";
                        description = "ascii adjust ax after multiply";

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        if (memory[1] != 0xa)
                            LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory[1], 2);
                    }
                    break;

                case 0xd5:
                    {  // aad
                        offset += 1;
                        LastDisassembleData.OpCode = "aad";
                        description = "ascii adjust ax before division";
                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        if (memory[1] != 0xa) LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory[1], 2);
                    }
                    break;

                case 0xd7:
                    {
                        description = "table look-up translation";
                        LastDisassembleData.OpCode = "xlatb";
                    }
                    break;

                case 0xd8:
                    {
                        switch (GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    //fadd
                                    description = "add";
                                    LastDisassembleData.OpCode = "fadd";
                                    last = 2;
                                    if (memory[1] >= 0xc0)
                                        LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xc0) + ')';
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 1:
                                {
                                    description = "multiply";
                                    last = 2;
                                    if (memory[1] >= 0xc8)
                                    {
                                        LastDisassembleData.OpCode = "fmul";
                                        LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xc8) + ')';
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "fmul";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);

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
                                        LastDisassembleData.OpCode = "fcom";
                                        LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xd0) + ')';
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "fcom";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);

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
                                        LastDisassembleData.OpCode = "fcomp";
                                        LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xd8) + ')';
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "fcomp";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);

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
                                        LastDisassembleData.OpCode = "fsub";
                                        LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xe0) + ')';
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "fsub";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);

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
                                        LastDisassembleData.OpCode = "fsubr";
                                        LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xe8) + ')';
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "fsubr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);

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
                                        LastDisassembleData.OpCode = "fdiv";
                                        LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xf0) + ')';
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "fdiv";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);

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
                                        LastDisassembleData.OpCode = "fdivr";
                                        LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xf8) + ')';
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "fdivr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);

                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                        }

                    }
                    break;

                case 0xd9:
                    {
                        LastDisassembleData.IsFloat = true;
                        if (AMathUtils.InRange(memory[1], 0x00, 0xbf))
                        {
                            switch (GetReg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "load floating point value";
                                        LastDisassembleData.OpCode = "fld";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 2:
                                    {
                                        description = "store single";
                                        LastDisassembleData.OpCode = "fst";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 3:
                                    {
                                        description = "store single";
                                        LastDisassembleData.OpCode = "fstp";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 4:
                                    {
                                        description = "load fpu environment";
                                        LastDisassembleData.OpCode = "fldenv";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 5:
                                    {
                                        description = "load control word";
                                        LastDisassembleData.OpCode = "fldcw";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 6:
                                    {
                                        description = "store fpu environment";
                                        LastDisassembleData.OpCode = "fnstenv";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 7:
                                    {
                                        description = "store control word";
                                        LastDisassembleData.OpCode = "fnstcw";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                            }
                        }
                        else if (AMathUtils.InRange(memory[1], 0xc0, 0xc7))
                        {
                            description = "push st(i) onto the fpu register stack";
                            LastDisassembleData.OpCode = "fld";
                            LastDisassembleData.Parameters = "st(" + (memory[1] - 0xc0) + ')';
                            offset += 1;
                        }
                        else if (AMathUtils.InRange(memory[1], 0xc8, 0xcf))
                        {
                            description = "exchange register contents";
                            LastDisassembleData.OpCode = "fxch";
                            LastDisassembleData.Parameters = "st(" + (memory[1] - 0xc8) + ')';
                            offset += 1;
                        }
                        else if (AMathUtils.InRange(memory[1], 0xd9, 0xdf))
                        {
                            description = "exchange register contents";
                            LastDisassembleData.OpCode = "fxch";
                            LastDisassembleData.Parameters = "st(" + (memory[1] - 0xd9) + ')';
                            offset += 1;
                        }
                        else
                        {
                            switch (memory[1])
                            {
                                case 0xd0:
                                    {
                                        description = "no operation";
                                        LastDisassembleData.OpCode = "fnop";
                                        offset += 1;
                                    }
                                    break;

                                case 0xe0:
                                    {
                                        description = "change sign";
                                        LastDisassembleData.OpCode = "fchs";
                                        offset += 1;
                                    }
                                    break;

                                case 0xe1:
                                    {
                                        description = "absolute value";
                                        LastDisassembleData.OpCode = "fabs";
                                        offset += 1;
                                    }
                                    break;

                                case 0xe4:
                                    {
                                        description = "test";
                                        LastDisassembleData.OpCode = "ftst";
                                        offset += 1;
                                    }
                                    break;

                                case 0xe5:
                                    {
                                        description = "examine";
                                        LastDisassembleData.OpCode = "fxam";
                                        offset += 1;
                                    }
                                    break;



                                case 0xe8:
                                    {
                                        description = "Push +1.0 onto the FPU register stack";
                                        LastDisassembleData.OpCode = "fld1";
                                        offset += 1;
                                    }
                                    break;

                                case 0xe9:
                                    {
                                        description = "Push log2(10) onto the FPU register stack";
                                        LastDisassembleData.OpCode = "fldl2t";
                                        offset += 1;
                                    }
                                    break;

                                case 0xea:
                                    {
                                        description = "Push log2(e) onto the FPU register stack";
                                        LastDisassembleData.OpCode = "fldl2e";
                                        offset += 1;
                                    }
                                    break;

                                case 0xeb:
                                    {
                                        description = "Push \"pi\" onto the FPU register stackload constant";
                                        LastDisassembleData.OpCode = "fldpi";
                                        offset += 1;
                                    }
                                    break;

                                case 0xec:
                                    {
                                        description = "Push log10(2) onto the FPU register stack";
                                        LastDisassembleData.OpCode = "fldlg2";
                                        offset += 1;
                                    }
                                    break;

                                case 0xed:
                                    {
                                        description = "Push log e(2) onto the FPU register stack";
                                        LastDisassembleData.OpCode = "fldln2";
                                        offset += 1;
                                    }
                                    break;

                                case 0xee:
                                    {
                                        description = "Push +0.0 onto the FPU register stack";
                                        LastDisassembleData.OpCode = "fldz";
                                        offset += 1;
                                    }
                                    break;


                                case 0xf0:
                                    {
                                        description = "compute 2^x-1";
                                        LastDisassembleData.OpCode = "f2xm1";
                                        offset += 1;
                                    }
                                    break;

                                case 0xf1:
                                    {
                                        description = "compute y*log(2)x";
                                        LastDisassembleData.OpCode = "fyl2x";
                                        offset += 1;
                                    }
                                    break;

                                case 0xf2:
                                    {
                                        description = "partial tangent";
                                        LastDisassembleData.OpCode = "fptan";
                                        offset += 1;
                                    }
                                    break;

                                case 0xf3:
                                    {
                                        description = "partial arctangent";
                                        LastDisassembleData.OpCode = "fpatan";
                                        offset += 1;
                                    }
                                    break;

                                case 0xf4:
                                    {
                                        description = "extract exponent and significand";
                                        LastDisassembleData.OpCode = "fxtract";
                                        offset += 1;
                                    }
                                    break;

                                case 0xf5:
                                    {
                                        description = "partial remainder";
                                        LastDisassembleData.OpCode = "fprem1";
                                        offset += 1;
                                    }
                                    break;

                                case 0xf6:
                                    {
                                        description = "decrement stack-top pointer";
                                        LastDisassembleData.OpCode = "fdecstp";
                                        offset += 1;
                                    }
                                    break;

                                case 0xf7:
                                    {
                                        description = "increment stack-top pointer";
                                        LastDisassembleData.OpCode = "fincstp";
                                        offset += 1;
                                    }
                                    break;

                                case 0xf8:
                                    {
                                        description = "partial remainder";
                                        LastDisassembleData.OpCode = "fprem";
                                        offset += 1;
                                    }
                                    break;

                                case 0xf9:
                                    {
                                        description = "compute y*log(2)(x+1)";
                                        LastDisassembleData.OpCode = "fyl2xp1";
                                        offset += 1;
                                    }
                                    break;

                                case 0xfa:
                                    {
                                        description = "square root";
                                        LastDisassembleData.OpCode = "fsqrt";
                                        offset += 1;
                                    }
                                    break;

                                case 0xfb:
                                    {
                                        description = "sine and cosine";
                                        LastDisassembleData.OpCode = "fsincos";
                                        offset += 1;
                                    }
                                    break;


                                case 0xfc:
                                    {
                                        description = "round to integer";
                                        LastDisassembleData.OpCode = "frndint";
                                        offset += 1;
                                    }
                                    break;

                                case 0xfd:
                                    {
                                        description = "scale";
                                        LastDisassembleData.OpCode = "fscale";
                                        offset += 1;
                                    }
                                    break;

                                case 0xfe:
                                    {
                                        description = "sine";
                                        LastDisassembleData.OpCode = "fsin";
                                        offset += 1;
                                    }
                                    break;

                                case 0xff:
                                    {
                                        description = "cosine";
                                        LastDisassembleData.OpCode = "fcos";
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
                            switch (GetReg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "add";
                                        LastDisassembleData.OpCode = "fiadd";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 1:
                                    {
                                        description = "multiply";
                                        LastDisassembleData.OpCode = "fimul";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 2:
                                    {
                                        description = "compare integer";
                                        LastDisassembleData.OpCode = "ficom";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 3:
                                    {
                                        description = "compare integer";
                                        LastDisassembleData.OpCode = "ficomp";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 4:
                                    {
                                        description = "subtract";
                                        LastDisassembleData.OpCode = "fisub";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 5:
                                    {
                                        description = "reverse subtract";
                                        LastDisassembleData.OpCode = "fisubr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;


                                case 6:
                                    {
                                        description = "divide";
                                        LastDisassembleData.OpCode = "fidiv";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 7:
                                    {
                                        description = "reverse divide";
                                        LastDisassembleData.OpCode = "fidivr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (GetReg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "floating-point: move if below";
                                        LastDisassembleData.OpCode = "fcmovb";
                                        LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xc0) + ')';
                                        offset += 1;
                                    }
                                    break;

                                case 1:
                                    {
                                        description = "floating-point: move if equal";
                                        LastDisassembleData.OpCode = "fcmove";
                                        LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xc8) + ')';
                                        offset += 1;
                                    }
                                    break;

                                case 2:
                                    {
                                        description = "floating-point: move if below or equal";
                                        LastDisassembleData.OpCode = "fcmovbe";
                                        LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xd0) + ')';
                                        offset += 1;
                                    }
                                    break;

                                case 3:
                                    {
                                        description = "floating-point: move if unordered";
                                        LastDisassembleData.OpCode = "fcmovu";
                                        LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xd8) + ')';
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
                                                    LastDisassembleData.OpCode = "fucompp";
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
                            switch (GetReg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "load integer";
                                        LastDisassembleData.OpCode = "fild";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 1:
                                    {
                                        description = "store integer with truncation";
                                        LastDisassembleData.OpCode = "fisttp";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 2:
                                    {
                                        description = "store integer";
                                        LastDisassembleData.OpCode = "fist";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 3:
                                    {
                                        description = "store integer";
                                        LastDisassembleData.OpCode = "fistp";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 5:
                                    {
                                        LastDisassembleData.IsFloat = true;
                                        description = "load floating point value";
                                        LastDisassembleData.OpCode = "fld";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 80);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 7:
                                    {
                                        LastDisassembleData.IsFloat = true;
                                        description = "store extended";
                                        LastDisassembleData.OpCode = "fstp";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 80);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                            }
                        }
                        else if (AMathUtils.InRange(memory[1], 0xc0, 0xc7))
                        {
                            description = "floating-point: move if not below";
                            LastDisassembleData.OpCode = "fcmovnb";
                            LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xc0) + ')';
                            offset += 1;
                        }
                        else if (AMathUtils.InRange(memory[1], 0xc8, 0xcf))
                        {
                            description = "floating-point: move if not equal";
                            LastDisassembleData.OpCode = "fcmovne";
                            LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xc8) + ')';
                            offset += 1;
                        }
                        else if (AMathUtils.InRange(memory[1], 0xd0, 0xd7))
                        {
                            description = "floating-point: move if not below or equal";
                            LastDisassembleData.OpCode = "fcmovnbe";
                            LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xd0) + ')';
                            offset += 1;
                        }
                        else if (AMathUtils.InRange(memory[1], 0xd8, 0xdf))
                        {
                            description = "floating-point: move if not unordered";
                            LastDisassembleData.OpCode = "fcmovnu";
                            LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xd8) + ')';
                            offset += 1;
                        }
                        else
                        {
                            switch (memory[1])
                            {
                                case 0xe2:
                                    {
                                        description = "clear exceptions";
                                        LastDisassembleData.OpCode = "fnclex";
                                        offset += 1;
                                    }
                                    break;

                                case 0xe3:
                                    {
                                        description = "initialize floating-point unit";
                                        LastDisassembleData.OpCode = "fninit";
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
                                        LastDisassembleData.OpCode = "fucomi";
                                        LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xe8) + ')';
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
                                        LastDisassembleData.OpCode = "fcomi";
                                        LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xf0) + ')';
                                        offset += 1;
                                    }
                                    break;
                            }
                        }
                    }
                    break;

                case 0xdc:
                    {
                        LastDisassembleData.IsFloat = true;
                        switch (GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    //fadd
                                    description = "add";
                                    last = 2;
                                    if (memory[1] >= 0xc0)
                                    {
                                        LastDisassembleData.OpCode = "fadd";
                                        LastDisassembleData.Parameters = "st(" + (memory[1] - 0xc0) + "),st(0)";
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "fadd";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);

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
                                        LastDisassembleData.OpCode = "fmul";
                                        LastDisassembleData.Parameters = "st(" + (memory[1] - 0xc8) + "),st(0)";
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "fmul";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);

                                    }
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 2:
                                {
                                    description = "compare real";
                                    last = 2;
                                    LastDisassembleData.OpCode = "fcom";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 3:
                                {
                                    description = "compare real";
                                    last = 2;
                                    LastDisassembleData.OpCode = "fcomp";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 4:
                                {
                                    description = "subtract";
                                    last = 2;
                                    if (memory[1] >= 0xe0)
                                    {
                                        LastDisassembleData.OpCode = "fsubr";
                                        LastDisassembleData.Parameters = "st(" + (memory[1] - 0xe0) + "),st(0)";
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "fsub";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);

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
                                        LastDisassembleData.OpCode = "fsub";
                                        LastDisassembleData.Parameters = "st(" + (memory[1] - 0xe8) + "),st(0)";
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "fsubr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);

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
                                        LastDisassembleData.OpCode = "fdivr";
                                        LastDisassembleData.Parameters = "st(" + (memory[1] - 0xf0) + "),st(0)";
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "fdiv";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);

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
                                        LastDisassembleData.OpCode = "fdiv";
                                        LastDisassembleData.Parameters = "st(" + (memory[1] - 0xf8) + "),st(0)";
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "fdivr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);

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
                            switch (GetReg(memory[1]))
                            {
                                case 0:
                                    {
                                        LastDisassembleData.IsFloat = true;
                                        description = "load floating point value";
                                        LastDisassembleData.OpCode = "fld";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 1:
                                    {
                                        description = "store integer with truncation";
                                        LastDisassembleData.OpCode = "fisttp";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 2:
                                    {
                                        LastDisassembleData.IsFloat = true;
                                        description = "store double";
                                        LastDisassembleData.OpCode = "fst";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 3:
                                    {
                                        LastDisassembleData.IsFloat = true;
                                        description = "store double";
                                        LastDisassembleData.OpCode = "fstp";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 4:
                                    {
                                        description = "restore fpu state";
                                        LastDisassembleData.OpCode = "frstor";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 6:
                                    {
                                        description = "store fpu state";
                                        LastDisassembleData.OpCode = "fnsave";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 7:
                                    {
                                        description = "store status word";
                                        LastDisassembleData.OpCode = "fnstsw";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                            }
                        }
                        else if (AMathUtils.InRange(memory[1], 0xc0, 0xc7))
                        {
                            description = "free floating-point register";
                            LastDisassembleData.OpCode = "ffree";
                            LastDisassembleData.Parameters = "st(" + (memory[1] - 0xc0) + ')';
                            offset += 1;
                        }
                        else if (AMathUtils.InRange(memory[1], 0xd0, 0xd7))
                        {
                            description = "store real";
                            LastDisassembleData.OpCode = "fst";
                            LastDisassembleData.Parameters = "st(" + (memory[1] - 0xd0) + ')';
                            offset += 1;
                        }
                        else if (AMathUtils.InRange(memory[1], 0xd8, 0xdf))
                        {
                            description = "store real";
                            LastDisassembleData.OpCode = "fstp";
                            LastDisassembleData.Parameters = "st(" + (memory[1] - 0xd8) + ')';
                            offset += 1;
                        }
                        else if (AMathUtils.InRange(memory[1], 0xe0, 0xe7))
                        {
                            description = "unordered compare real";
                            LastDisassembleData.OpCode = "fucom";
                            LastDisassembleData.Parameters = "st(" + (memory[1] - 0xe0) + ')';
                            offset += 1;
                        }
                        else if (AMathUtils.InRange(memory[1], 0xe8, 0xef))
                        {
                            description = "unordered compare real";
                            LastDisassembleData.OpCode = "fucomp";
                            LastDisassembleData.Parameters = "st(" + (memory[1] - 0xe8) + ')';
                            offset += 1;
                        }
                        else
                        {
                            LastDisassembleData.OpCode = "db";
                            LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory[0], 2);
                        }
                    }
                    break;

                case 0xde:
                    {
                        switch (GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    //faddp
                                    description = "add and pop";
                                    last = 2;
                                    if (memory[1] == 0xc1) LastDisassembleData.OpCode = "faddp";
                                    else
                                    if (memory[1] >= 0xc0)
                                    {
                                        LastDisassembleData.OpCode = "faddp";
                                        LastDisassembleData.Parameters = "st(" + (memory[1] - 0xc0) + "),st(0)";
                                    }
                                    else
                                    {
                                        description = "add";
                                        LastDisassembleData.OpCode = "fiadd";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

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
                                        LastDisassembleData.OpCode = "fmulp";
                                        LastDisassembleData.Parameters = "st(" + (memory[1] - 0xc8) + "),st(0)";
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "fimul";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                    }

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 2:
                                {
                                    description = "compare integer";
                                    last = 2;
                                    LastDisassembleData.OpCode = "ficom";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;


                            case 3:
                                {
                                    if (memory[1] < 0xc0)
                                    {
                                        description = "compare integer";
                                        LastDisassembleData.OpCode = "ficomp";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }

                                    if (memory[1] == 0xd9)
                                    {
                                        description = "compare real and pop register stack twice";
                                        LastDisassembleData.OpCode = "fcompp";
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
                                        LastDisassembleData.OpCode = "fsubrp";
                                        LastDisassembleData.Parameters = "st(" + (memory[1] - 0xe0) + "),st(0)";
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "fisub";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

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
                                        LastDisassembleData.OpCode = "fsubp";
                                        LastDisassembleData.Parameters = "st(" + (memory[1] - 0xe8) + "),st(0)";
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "fisubr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

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
                                        LastDisassembleData.OpCode = "fdivrp";
                                        LastDisassembleData.Parameters = "st(" + (memory[1] - 0xf0) + "),st(0)";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "divide";
                                        LastDisassembleData.OpCode = "fidiv";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);

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
                                        LastDisassembleData.OpCode = "fdivp";
                                        LastDisassembleData.Parameters = "st(" + (memory[1] - 0xf8) + "),st(0)";
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "fdivr";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

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
                            LastDisassembleData.OpCode = "ffreep";
                            LastDisassembleData.Parameters = "st(" + (memory[1] - 0xc0) + ')';
                            offset += 1;
                        }
                        else
                            switch (GetReg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "load integer";
                                        LastDisassembleData.OpCode = "fild";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 16);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 1:
                                    {
                                        description = "store integer with truncation";
                                        LastDisassembleData.OpCode = "fisttp";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 16);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 2:
                                    {
                                        description = "store integer";
                                        LastDisassembleData.OpCode = "fist";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 16);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 3:
                                    {
                                        description = "store integer";
                                        LastDisassembleData.OpCode = "fistp";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 16);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 4:
                                    {
                                        description = "load binary coded decimal";
                                        last = 2;
                                        if (memory[1] >= 0xe0)
                                        {
                                            LastDisassembleData.OpCode = "fnstsw";
                                            LastDisassembleData.Parameters = _colorReg + "ax" + _endColor;
                                        }
                                        else
                                        {
                                            LastDisassembleData.OpCode = "fbld";
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 80);

                                        }
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                case 5:
                                    {
                                        if (memory[1] < 0xc0)
                                        {
                                            description = "load integer";
                                            LastDisassembleData.OpCode = "fild";
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);

                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }

                                        if (memory[1] >= 0xe8)
                                        {
                                            description = "compare real and set eflags";
                                            LastDisassembleData.OpCode = "fucomip";
                                            LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xe8) + ')';
                                            offset += 1;
                                        }
                                    }
                                    break;

                                case 6:
                                    {
                                        if (memory[1] >= 0xf0)
                                        {
                                            description = "compare real and set eflags";
                                            LastDisassembleData.OpCode = "fcomip";
                                            LastDisassembleData.Parameters = "st(0),st(" + (memory[1] - 0xf0) + ')';
                                            offset += 1;
                                        }
                                        else
                                        {
                                            description = "store bcd integer and pop";
                                            LastDisassembleData.OpCode = "fbstp";
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 80);

                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                    break;

                                case 7:
                                    {
                                        description = "store integer";
                                        LastDisassembleData.OpCode = "fistp";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    break;

                                default:
                                    {
                                        LastDisassembleData.OpCode = "db";
                                        LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory[0], 2);
                                    }
                                    break;
                            }

                    }
                    break;

                case 0xe0:
                    {
                        description = "loop according to ecx counter";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_zf) == 0;

                        LastDisassembleData.OpCode = "loopne";

                        offset += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;
                    }
                    break;

                case 0xe1:
                    {
                        description = "loop according to ecx counter";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;
                        //if (context != nil)
                        //lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_zf) != 0;

                        LastDisassembleData.OpCode = "loope";
                        offset += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;
                    }
                    break;

                case 0xe2:
                    {
                        description = "loop according to ecx counting";
                        LastDisassembleData.OpCode = "loop";
                        // todo readd me
                        //if context<>nil then
                        //lastdisassembledata.willJumpAccordingToContext:=context^.{$ifdef CPU64}RCX{$else}ECX{$endif}<>0;

                        LastDisassembleData.IsJump = true;
                        offset += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;
                    }
                    break;

                case 0xe3:
                    {
                        description = "jump short if cx=0";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsConditionalJump = true;

                        if (_prefix2.Contains(0x66))
                        {
                            LastDisassembleData.OpCode = "jcxz";
                            // todo readd me
                            //if context<>nil then
                            //lastdisassembledata.willJumpAccordingToContext:=((context^.{$ifdef CPU64}RCX{$else}ECX{$endif}) and $ffff)=0;

                        }
                        else
                        {
                            LastDisassembleData.OpCode = "jecxz";
                            // todo readd me
                            //if context<>nil then
                            //lastdisassembledata.willJumpAccordingToContext:=context^.{$ifdef CPU64}RCX{$else}ECX{$endif}=0;

                        }
                        offset += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;



                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;
                    }
                    break;

                case 0xe4:
                    {
                        description = "input from port";
                        LastDisassembleData.OpCode = "in";
                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        LastDisassembleData.Parameters = _colorReg + "al" + _endColor + ',' + IntToHexSigned((UIntPtr)memory[1], 2);
                        offset += 1;

                    }
                    break;

                case 0xe5:
                    {
                        description = "input from port";
                        LastDisassembleData.OpCode = "in";

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;


                        if (_prefix2.Contains(0x66)) LastDisassembleData.Parameters = _colorReg + "ax" + _endColor + ',' + IntToHexSigned((UIntPtr)memory[1], 2);
                        else LastDisassembleData.Parameters = _colorReg + "eax" + _endColor + ',' + IntToHexSigned((UIntPtr)memory[1], 2);
                        offset += 1;

                    }
                    break;

                case 0xe6:
                    {
                        description = "output to port";
                        LastDisassembleData.OpCode = "out";
                        LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory[1], 2) + ',' + _colorReg + "al" + _endColor;
                        offset += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;
                    }
                    break;

                case 0xe7:
                    {
                        description = "output toport";
                        LastDisassembleData.OpCode = "out";
                        if (_prefix2.Contains(0x66))
                            LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory[1], 2) + ',' + _colorReg + "ax" + _endColor;
                        else
                            LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory[1], 2) + ',' + _colorReg + "eax" + _endColor;

                        offset += 1;

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;
                    }
                    break;

                case 0xe8:
                    {
                        //call
                        //this time no $66 prefix because it will only run in win32
                        description = "call procedure";
                        LastDisassembleData.OpCode = "call";
                        LastDisassembleData.IsJump = true;
                        LastDisassembleData.IsCall = true;

                        if (MarkIpRelativeInstructions)
                        {
                            LastDisassembleData.RipRelative = 1;
                            _ripRelative = true;
                        }
                        offset += 4;
                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt64)memory.ReadInt32(1));
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt32(1));

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                    }
                    break;

                case 0xe9:
                    {
                        description = "jump near";
                        LastDisassembleData.IsJump = true;

                        if (_prefix2.Contains(0x66))
                        {
                            LastDisassembleData.OpCode = "jmp";

                            offset += 2;
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt64)memory.ReadInt16(1));
                            LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                        }
                        else
                        {
                            LastDisassembleData.OpCode = "jmp";

                            if (MarkIpRelativeInstructions)
                            {
                                LastDisassembleData.RipRelative = 1;
                                _ripRelative = true;
                            }

                            offset += 4;
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                            if (Is64Bit)
                                LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + (UInt16)memory.ReadInt32(1));
                            else
                                LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + (UInt16)memory.ReadInt32(1));

                            LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);
                        }

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                    }
                    break;

                case 0xea:
                    {
                        description = "jump far";
                        LastDisassembleData.IsJump = true;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;
                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 5;
                        LastDisassembleData.SeparatorCount += 1;


                        var wordptr = memory.ToIntPtr(5).ReadUInt16();
                        LastDisassembleData.OpCode = "jmp";
                        LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)wordptr, 4) + ':';
                        var dwordptr = memory.ToIntPtr(1).ReadUInt32();

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                        LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;


                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)dwordptr, 8);
                        offset += 6;
                    }
                    break;

                case 0xeb:
                    {
                        description = "jump short";
                        LastDisassembleData.OpCode = "jmp";
                        LastDisassembleData.IsJump = true;

                        offset += 1;

                        if (Is64Bit)
                            LastDisassembleData.ParameterValue = (UIntPtr)(offset.ToUInt64() + memory[1]);
                        else
                            LastDisassembleData.ParameterValue = (UIntPtr)(UInt32)(offset.ToUInt64() + memory[1]);

                        LastDisassembleData.Parameters = IntToHexSigned(LastDisassembleData.ParameterValue, 8);

                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                    }
                    break;

                case 0xec:
                    {
                        description = "input from port";
                        LastDisassembleData.OpCode = "in";
                        LastDisassembleData.Parameters = _colorReg + "al" + _endColor + ',' + _colorReg + "dx" + _endColor;
                    }
                    break;

                case 0xed:
                    {
                        description = "input from port";
                        LastDisassembleData.OpCode = "in";
                        if (_prefix2.Contains(0x66)) LastDisassembleData.Parameters = _colorReg + "ax" + _endColor + ',' + _colorReg + "dx" + _endColor;
                        else
                            LastDisassembleData.Parameters = _colorReg + "eax" + _endColor + ',' + _colorReg + "dx" + _endColor;
                    }
                    break;

                case 0xee:
                    {
                        description = "input from port";
                        LastDisassembleData.OpCode = "out";
                        LastDisassembleData.Parameters = _colorReg + "dx" + _endColor + ',' + _colorReg + "al" + _endColor;
                    }
                    break;

                case 0xef:
                    {
                        description = "input from port";
                        LastDisassembleData.OpCode = "out";
                        if (_prefix2.Contains(0x66)) LastDisassembleData.Parameters = _colorReg + "dx" + _endColor + ',' + _colorReg + "ax" + _endColor;
                        else
                            LastDisassembleData.Parameters = _colorReg + "dx" + _endColor + ',' + _colorReg + "eax" + _endColor;
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
                        LastDisassembleData.OpCode = "hlt";
                    }
                    break;

                case 0xf5:
                    {
                        description = "complement carry flag";
                        LastDisassembleData.OpCode = "cmc";
                    }
                    break;

                case 0xf6:
                    {
                        switch (GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "logical compare";
                                    LastDisassembleData.OpCode = "test";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);
                                    LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                    LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                    LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];


                                    offset = (UIntPtr)(offset.ToUInt64() + last);
                                }
                                break;

                            case 2:
                                {
                                    description = "one's complement negation";
                                    LastDisassembleData.OpCode = "not";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 3:
                                {
                                    description = "two's complement negation";
                                    LastDisassembleData.OpCode = "neg";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 4:
                                {
                                    description = "unsigned multiply";
                                    LastDisassembleData.OpCode = "mul";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 5:
                                {
                                    description = "signed multiply";
                                    LastDisassembleData.OpCode = "imul";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 6:
                                {
                                    description = "unsigned divide";
                                    LastDisassembleData.OpCode = "div";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 7:
                                {
                                    description = "signed divide";
                                    LastDisassembleData.OpCode = "idiv";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            default:
                                {
                                    LastDisassembleData.OpCode = "db";
                                    LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory[0], 2);
                                }
                                break;

                        }
                    }
                    break;

                case 0xf7:
                    {
                        switch (GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "logical compare";
                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.OpCode = "test";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                        var wordptr = memory.ReadUInt16((int)last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                        LastDisassembleData.ParameterValue = (UIntPtr)wordptr;

                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)wordptr, 4);
                                        offset = (UIntPtr)(offset.ToUInt64() + last + 1);
                                    }
                                    else
                                    {
                                        LastDisassembleData.OpCode = "test";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);
                                        var dwordptr = memory.ReadUInt32((int)last);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;
                                        LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                                        if (RexW)
                                            LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)dwordptr, 8);
                                        else
                                            LastDisassembleData.Parameters = LastDisassembleData.Parameters + IntToHexSigned((UIntPtr)dwordptr, 8);
                                        offset = (UIntPtr)(offset.ToUInt64() + last + 3);
                                    }
                                }
                                break;

                            case 2:
                                {
                                    description = "one's complement negation";
                                    LastDisassembleData.OpCode = "not";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 3:
                                {
                                    description = "two's complement negation";
                                    LastDisassembleData.OpCode = "neg";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);


                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 4:
                                {
                                    description = "unsigned multiply";
                                    LastDisassembleData.OpCode = "mul";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);


                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 5:
                                {
                                    description = "signed multiply";
                                    LastDisassembleData.OpCode = "imul";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 6:
                                {
                                    description = "unsigned divide";
                                    LastDisassembleData.OpCode = "div";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);


                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 7:
                                {
                                    description = "signed divide";
                                    LastDisassembleData.OpCode = "idiv";
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
                                    LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory[0], 2);
                                }
                                break;
                        }
                    }
                    break;

                case 0xf8:
                    {
                        description = "clear carry flag";
                        LastDisassembleData.OpCode = "clc";
                    }
                    break;

                case 0xf9:
                    {
                        description = "set carry flag";
                        LastDisassembleData.OpCode = "stc";
                    }
                    break;

                case 0xfa:
                    {
                        description = "clear interrupt flag";
                        LastDisassembleData.OpCode = "cli";
                    }
                    break;

                case 0xfb:
                    {
                        description = "set interrupt flag";
                        LastDisassembleData.OpCode = "sti";
                    }
                    break;

                case 0xfc:
                    {
                        description = "clear direction flag";
                        LastDisassembleData.OpCode = "cld";
                    }
                    break;

                case 0xfd:
                    {
                        description = "set direction flag";
                        LastDisassembleData.OpCode = "std";
                    }
                    break;

                case 0xfe:
                    {
                        switch (GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "increment by 1";
                                    LastDisassembleData.OpCode = "inc";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 8);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 1:
                                {
                                    description = "decrement by 1";
                                    LastDisassembleData.OpCode = "dec";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last, 7);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            default:
                                {
                                    LastDisassembleData.OpCode = "db";
                                    LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory[0], 2);
                                }
                                break;
                        }
                    }
                    break;

                case 0xff:
                    {
                        switch (GetReg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "increment by 1";
                                    LastDisassembleData.OpCode = "inc";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);


                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 1:
                                {
                                    description = "decrement by 1";
                                    LastDisassembleData.OpCode = "dec";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last, 16);
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 2:
                                {
                                    //call
                                    description = "call procedure";
                                    LastDisassembleData.OpCode = "call";
                                    LastDisassembleData.IsJump = true;
                                    LastDisassembleData.IsCall = true;

                                    if (memory[1] >= 0xc0)
                                    {
                                        if (Is64Bit)
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                        else
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);

                                    }
                                    else
                                    {
                                        if (Is64Bit)
                                        {

                                            if ((memory[1] == 0x15) && (memory.ReadUInt32(2) == 2) && (memory.ReadUInt16(6) == 0x8eb))  //special 16 byte call
                                            {
                                                LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory.ReadUInt64(8), 8);
                                                LastDisassembleData.ParameterValue = (UIntPtr)memory.ReadUInt64(8);
                                                LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                                                last += 8 + 4 + 2 + 2;

                                                LastDisassembleData.Separators[0] = 2;
                                                LastDisassembleData.Separators[1] = 2 + 4;
                                                LastDisassembleData.Separators[2] = 2 + 4 + 2;
                                                LastDisassembleData.SeparatorCount = 3;

                                            }
                                            else
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                        }
                                        else
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);
                                    }

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 3:
                                {
                                    //call
                                    description = "call procedure";
                                    LastDisassembleData.OpCode = "call";
                                    LastDisassembleData.IsJump = true;
                                    LastDisassembleData.IsCall = true;

                                    if (Is64Bit)
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 4:
                                {
                                    //jmp
                                    description = "jump near";
                                    LastDisassembleData.OpCode = "jmp";
                                    LastDisassembleData.IsJump = true;


                                    if (Is64Bit)
                                    {
                                        if ((memory[1] == 0x25) && (memory.ReadUInt32(2) == 0))  //special 14 byte jmp
                                        {
                                            LastDisassembleData.ParameterValue = (UIntPtr)memory.ReadUInt64(6);
                                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Address;

                                            LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory.ReadUInt64(6), 8);
                                            last += 8 + 4 + 2;

                                            LastDisassembleData.Separators[0] = 2;
                                            LastDisassembleData.Separators[1] = 2 + 4;
                                            LastDisassembleData.SeparatorCount = 2;

                                        }
                                        else
                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 64);
                                    }
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last, 32);


                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 5:
                                {
                                    //jmp
                                    description = "jump far";
                                    LastDisassembleData.OpCode = "jmp far";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);
                                    LastDisassembleData.IsJump = true;

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 6:
                                {
                                    description = "push word or doubleword onto the stack";
                                    LastDisassembleData.OpCode = "push";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last);
                                    else
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last);


                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            default:
                                {
                                    LastDisassembleData.OpCode = "db";
                                    LastDisassembleData.Parameters = IntToHexSigned((UIntPtr)memory[0], 2);
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
