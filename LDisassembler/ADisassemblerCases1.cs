using System;
using Sputnik.LBinary;
using Sputnik.LMarshal;
using SputnikAsm.LDisassembler.LEnums;
using SputnikAsm.LUtils;

namespace SputnikAsm.LDisassembler
{
    public partial class ADisassembler
    {
        #region DisassembleProcess1
        private Boolean DisassembleProcess1(UBytePtr memory, ref UIntPtr offset, ref int prefixSize, ref UInt32 last, ref String description)
        {
            switch (memory[0])
            {
                case 0:
                    {
                        if ((_aggressiveAlignment & (((offset.ToUInt64()) & 0xf) == 0) && (memory[1] != 0)) || ((memory[1] == 0x55) && (memory[2] == 0x89) && (memory[3] == 0xe5)))
                        {
                            description = "Filler";
                            LastDisassembleData.OpCode = "db";
                            LastDisassembleData.Parameters = AStringUtils.IntToHex(memory[0], 2);
                        }
                        else
                        {
                            description = "Add";

                            LastDisassembleData.OpCode = "add";
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last) + R8(memory[1]);
                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                        }
                    }
                    break;

                case 0x1:
                    {
                        description = "Add";

                        LastDisassembleData.OpCode = "add";
                        if (_prefix2.Contains(0x66)) LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last) + R16(memory[1]);
                        else
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + R32(memory[1]);


                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x2:
                    {
                        description = "Add";

                        LastDisassembleData.OpCode = "add";
                        LastDisassembleData.Parameters = R8(memory[1]) + ModRm(memory, _prefix2, 1, 2, ref last, ATmrPos.Right);

                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x3:
                    {
                        description = "Add";
                        LastDisassembleData.OpCode = "add";
                        if (_prefix2.Contains(0x66)) LastDisassembleData.Parameters = R16(memory[1]) + ModRm(memory, _prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            LastDisassembleData.Parameters = R32(memory[1]) + ModRm(memory, _prefix2, 1, 0, ref last, ATmrPos.Right);


                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;



                case 0x4:
                    {
                        description = "Add " + AStringUtils.IntToHex(memory[1], 2) + " to AL";
                        LastDisassembleData.OpCode = "add";
                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        LastDisassembleData.Parameters = _colorReg + "al" + _endColor + ',' + IntToHexSigned((UIntPtr)memory[1], 2);

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        offset += 1;
                    }
                    break;

                case 0x5:
                    {
                        LastDisassembleData.OpCode = "add";
                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;


                        var wordptr = memory.ToIntPtr(1).ReadUInt16();
                        var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                        if (_prefix2.Contains(0x66))
                        {
                            LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                            LastDisassembleData.Parameters = _colorReg + "ax" + _endColor + ',' + IntToHexSigned((UIntPtr)wordptr, 4);

                            description = "add " + AStringUtils.IntToHex(wordptr, 4) + " to ax";



                            offset += 2;
                        }
                        else
                        {
                            if (RexW)
                            {
                                LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                                LastDisassembleData.Parameters = _colorReg + "rax" + _endColor + ',' + IntToHexSigned((UIntPtr)dwordptr, 8);

                                description = "add " + AStringUtils.IntToHex(dwordptr, 8) + " to rax (sign extended)";
                            }
                            else
                            {
                                LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                                LastDisassembleData.Parameters = _colorReg + "eax" + _endColor + ',' + IntToHexSigned((UIntPtr)dwordptr, 8);

                                description = "add " + AStringUtils.IntToHex(dwordptr, 8) + " to eax";
                            }
                            offset += 4;
                        }

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;
                    }
                    break;

                case 0x6:
                    {
                        LastDisassembleData.OpCode = "push";
                        LastDisassembleData.Parameters = _colorReg + "es" + _endColor;
                        description = "place es on the stack";
                    }
                    break;

                case 0x7:
                    {
                        LastDisassembleData.OpCode = "pop";
                        LastDisassembleData.Parameters = _colorReg + "es" + _endColor;
                        description = "remove es from the stack";
                    }
                    break;

                case 0x8:
                    {
                        description = "logical inclusive or";
                        LastDisassembleData.OpCode = "or";
                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 2, ref last) + R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0x9:
                    {
                        description = "logical inclusive or";
                        LastDisassembleData.OpCode = "or";
                        if (_prefix2.Contains(0x66)) LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 1, ref last) + R16(memory[1]);
                        else
                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 1, 0, ref last) + R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0xa:
                    {
                        description = "logical inclusive or";
                        LastDisassembleData.OpCode = "or";
                        LastDisassembleData.Parameters = R8(memory[1]) + ModRm(memory, _prefix2, 1, 2, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0xb:
                    {
                        description = "logical inclusive or";
                        LastDisassembleData.OpCode = "or";
                        if (_prefix2.Contains(0x66)) LastDisassembleData.Parameters = R16(memory[1]) + ModRm(memory, _prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            LastDisassembleData.Parameters = R32(memory[1]) + ModRm(memory, _prefix2, 1, 0, ref last, ATmrPos.Right);

                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;

                case 0xc:
                    {
                        description = "logical inclusive or";
                        LastDisassembleData.OpCode = "or";
                        LastDisassembleData.Parameters = _colorReg + "al" + _endColor + ',' + IntToHexSigned((UIntPtr)memory[1], 2);
                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ParameterValue = (UIntPtr)memory[1];

                        LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                        LastDisassembleData.SeparatorCount += 1;

                        offset += 1;
                    }
                    break;

                case 0xd:
                    {
                        description = "logical inclusive or";
                        LastDisassembleData.OpCode = "or";
                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;

                        if (_prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();

                            LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                            LastDisassembleData.Parameters = _colorReg + "ax" + _endColor + ',' + IntToHexSigned(LastDisassembleData.ParameterValue, 4);

                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;

                            if (RexW)
                            {
                                LastDisassembleData.Parameters = _colorReg + "rax" + _endColor + ',' + IntToHexSigned((UIntPtr)LastDisassembleData.ParameterValue, 8);
                                description += " (sign-extended)";
                            }
                            else
                                LastDisassembleData.Parameters = _colorReg + "eax" + _endColor + ',' + IntToHexSigned(LastDisassembleData.ParameterValue, 8);


                            LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = 1;
                            LastDisassembleData.SeparatorCount += 1;
                            offset += 4;
                        }
                    }
                    break;

                case 0xe:
                    {
                        description = "place cs on the stack";
                        LastDisassembleData.OpCode = "push";
                        LastDisassembleData.Parameters = _colorReg + "cs" + _endColor;
                    }
                    break;

                case 0xf:
                    {  
                        //simd extensions
                        if (_prefix2.Contains(0xf0))
                            LastDisassembleData.Prefix = "lock ";
                        else
                            LastDisassembleData.Prefix = ""; //these usually treat the f2/f3 prefix differently

                        switch (memory[1])
                        {
                            case 0:
                                {
                                    switch (GetReg(memory[2]))
                                    {
                                        case 0:
                                            {
                                                LastDisassembleData.OpCode = "sldt";
                                                description = "store local descriptor table register";
                                                if (_prefix2.Contains(0x66)) LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last, 16);
                                                else
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);

                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 1:
                                            {
                                                description = "store task register";
                                                LastDisassembleData.OpCode = "str";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last, 16);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 2:
                                            {
                                                description = "load local descriptor table register";
                                                LastDisassembleData.OpCode = "lldt";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last, 16);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 3:
                                            {
                                                description = "load task register";
                                                LastDisassembleData.OpCode = "ltr";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last, 16);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 4:
                                            {
                                                description = "verify a segment for reading";
                                                LastDisassembleData.OpCode = "verr";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last, 16);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 5:
                                            {
                                                description = "verify a segment for writing";
                                                LastDisassembleData.OpCode = "verw";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last, 16);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        default:
                                            {
                                                LastDisassembleData.OpCode = "db";
                                                LastDisassembleData.Parameters = AStringUtils.IntToHex(memory[0], 2);
                                                description = "not specified by the intel documentation";
                                            }
                                            break;

                                    }

                                }
                                break;

                            case 0x1:
                                {
                                    switch (memory[2])
                                    {
                                        case 0xc1:
                                            {
                                                description = "call to vm monitor by causing vm exit";
                                                LastDisassembleData.OpCode = "vmcall";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xc2:
                                            {
                                                description = "launch virtual machine managed by current vmcs";
                                                LastDisassembleData.OpCode = "vmlaunch";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xc3:
                                            {
                                                description = "resume virtual machine managed by current vmcs";
                                                LastDisassembleData.OpCode = "vmresume";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xc4:
                                            {
                                                description = "leaves vmx operation";
                                                LastDisassembleData.OpCode = "vmxoff";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xc8:
                                            {
                                                description = "set up monitor address";
                                                LastDisassembleData.OpCode = "monitor";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xc9:
                                            {
                                                description = "Monitor wait";
                                                LastDisassembleData.OpCode = "mwait";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xca:
                                            {
                                                description = "Clear AC flag in EFLAGS register";
                                                LastDisassembleData.OpCode = "clac";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xd0:
                                            {
                                                description = "Get value of extended control register";
                                                LastDisassembleData.OpCode = "xgetbv";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xd1:
                                            {
                                                description = "Set value of extended control register";
                                                LastDisassembleData.OpCode = "xsetbv";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xd5:
                                            {
                                                description = "Transactional end";
                                                LastDisassembleData.OpCode = "xend";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xd6:
                                            {
                                                description = "Test if in transactional execution";
                                                LastDisassembleData.OpCode = "xtest";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xf8:
                                            {
                                                description = "Swap GS base register";
                                                LastDisassembleData.OpCode = "swapgs";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xf9:
                                            {
                                                description = "Read time-stamp counter and processor ID";
                                                LastDisassembleData.OpCode = "rdtscp";
                                                offset += 2;
                                            }
                                            break;

                                        default:
                                            {
                                                switch (GetReg(memory[2]))
                                                {
                                                    case 0:
                                                        {
                                                            description = "store global descriptor table register";
                                                            LastDisassembleData.OpCode = "sgdt";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;

                                                    case 1:
                                                        {
                                                            description = "store interrupt descriptor table register";
                                                            LastDisassembleData.OpCode = "sidt";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;

                                                    case 2:
                                                        {
                                                            description = "load global descriptor table register";
                                                            LastDisassembleData.OpCode = "lgdt";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;

                                                    case 3:
                                                        {
                                                            description = "load interupt descriptor table register";
                                                            LastDisassembleData.OpCode = "lidt";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;

                                                    case 4:
                                                        {
                                                            description = "store machine status word";
                                                            LastDisassembleData.OpCode = "smsw";

                                                            if (_prefix2.Contains(0x66)) LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last);
                                                            else LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;

                                                    case 6:
                                                        {
                                                            description = "load machine status word";
                                                            LastDisassembleData.OpCode = "lmsw";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 1, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;

                                                    case 7:
                                                        {
                                                            description = "invalidate tlb entry";
                                                            LastDisassembleData.OpCode = "invplg";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;
                                                }
                                            }
                                            break;
                                    }
                                }
                                break;

                            case 0x2:
                                {
                                    description = "load access rights byte";
                                    LastDisassembleData.OpCode = "lar";
                                    if (_prefix2.Contains(0x66)) LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 2, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            /*0f*/
                            case 0x3:
                                {
                                    description = "load segment limit";
                                    LastDisassembleData.OpCode = "lsl";
                                    if (_prefix2.Contains(0x66)) LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 2, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x5:
                                {
                                    description = "fast system call";
                                    LastDisassembleData.OpCode = "syscall";
                                    offset += 1;
                                }
                                break;

                            case 0x6:
                                {
                                    description = "clear task-switched flag in cr0";
                                    LastDisassembleData.OpCode = "clts";
                                    offset += 1;
                                }
                                break;

                            case 0x7:
                                {
                                    description = "return from fast system call";
                                    LastDisassembleData.OpCode = "sysret";
                                    offset += 1;
                                }
                                break;

                            case 0x8:
                                {
                                    description = "invalidate internal caches";
                                    LastDisassembleData.OpCode = "invd";
                                    offset += 1;
                                }
                                break;

                            case 0x9:
                                {
                                    description = "write back and invalidate cache";
                                    LastDisassembleData.OpCode = "wbinvd";
                                    offset += 1;
                                }
                                break;

                            case 0xb:
                                {
                                    description = "undefined instruction(yes, this one really excists..)";
                                    LastDisassembleData.OpCode = "ud2";
                                    offset += 1;
                                }
                                break;

                            case 0xd:
                                {
                                    switch (GetReg(memory[2]))
                                    {
                                        case 1:
                                            {
                                                description = "Prefetch Data into Caches in Anticipation of a Write";
                                                LastDisassembleData.OpCode = "prefetchw";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 2:
                                            {
                                                description = "Prefetch Vector Data Into Caches with Intent to Write and T1 Hint";
                                                LastDisassembleData.OpCode = "prefetchwt1";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                    }
                                }
                                break;


                            case 0x10:
                                {
                                    LastDisassembleData.IsFloat = true;

                                    if (_prefix2.Contains(0xf2))
                                    {
                                        description = "move scalar double-fp";
                                        _opCodeFlags.L = false; //LIG
                                        _opCodeFlags.SkipExtraRegOnMemoryAccess = true;
                                        LastDisassembleData.IsFloat64 = true;

                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovsd";
                                        else
                                            LastDisassembleData.OpCode = "movsd";

                                        _opCodeFlags.SkipExtraRegOnMemoryAccess = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {
                                        description = "move scalar single-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovss";
                                        else
                                            LastDisassembleData.OpCode = "movss";

                                        _opCodeFlags.SkipExtraRegOnMemoryAccess = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "move unaligned packed double-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "lmovupd";
                                        else
                                            LastDisassembleData.OpCode = "movupd";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "move unaligned four packed single-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovups";
                                        else
                                            LastDisassembleData.OpCode = "movups";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x11:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0xf2))
                                    {
                                        description = "move scalar double-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovsd";
                                        else
                                            LastDisassembleData.OpCode = "movsd";

                                        LastDisassembleData.IsFloat64 = true;

                                        _opCodeFlags.SkipExtraRegOnMemoryAccess = true;
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Left) + Xmm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {
                                        description = "move scalar single-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovss";
                                        else
                                            LastDisassembleData.OpCode = "movss";

                                        _opCodeFlags.SkipExtraRegOnMemoryAccess = true;
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last) + Xmm(memory[2]);
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "move unaligned packed double-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "lmovupd";
                                        else
                                            LastDisassembleData.OpCode = "movupd";

                                        _opCodeFlags.SkipExtraRegOnMemoryAccess = true;
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last) + Xmm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "move unaligned four packed single-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovups";
                                        else
                                            LastDisassembleData.OpCode = "movups";

                                        _opCodeFlags.SkipExtraRegOnMemoryAccess = true;
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last) + Xmm(memory[2]);
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }

                                }
                                break;

                            /*0f*/
                            case 0x12:
                                {
                                    if (_prefix2.Contains(0xf2))
                                    {
                                        description = "move one double-fp and duplicate";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovddup";
                                        else
                                            LastDisassembleData.OpCode = "movddup";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {
                                        description = "move packed single-fp Low and duplicate";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovsldup";
                                        else
                                            LastDisassembleData.OpCode = "movsldup";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "move low packed double-precision floating-point value";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovlpd";
                                        else
                                            LastDisassembleData.OpCode = "movlpd";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "high to low packed single-fp";

                                        if (GetMod(memory[2]) == 3)
                                            LastDisassembleData.OpCode = "movhlps";
                                        else
                                            LastDisassembleData.OpCode = "movlps";

                                        if (_hasVex)
                                            LastDisassembleData.OpCode = 'v' + LastDisassembleData.OpCode;

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x13:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "move low packed double-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovlpd";
                                        else
                                            LastDisassembleData.OpCode = "movlpd";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last) + Xmm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "move low packed single-fp";

                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovlps";
                                        else
                                            LastDisassembleData.OpCode = "movlps";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last) + Xmm(memory[2]);
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            /*0f*/
                            case 0x14:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "unpack low packed single-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vunpcklpd";
                                        else
                                            LastDisassembleData.OpCode = "unpcklpd";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unpack low packed single-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vunpcklps";
                                        else
                                            LastDisassembleData.OpCode = "unpcklps";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x15:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "unpack and interleave high packed double-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vunpckhpd";
                                        else
                                            LastDisassembleData.OpCode = "unpckhpd";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unpack high packed single-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "unpckhps";
                                        else
                                            LastDisassembleData.OpCode = "unpckhps";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x16:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0xf3))
                                    {
                                        description = "move packed single-fp high and duplicate";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovshdup";
                                        else
                                            LastDisassembleData.OpCode = "movshdup";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "move high packed double-precision floating-point value";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovhpd";
                                        else
                                            LastDisassembleData.OpCode = "movhpd";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "high to low packed single-fp";

                                        if (GetMod(memory[2]) == 3)
                                            LastDisassembleData.OpCode = "movlhps";
                                        else
                                            LastDisassembleData.OpCode = "movhps";

                                        if (_hasVex)
                                            LastDisassembleData.OpCode = 'v' + LastDisassembleData.OpCode;

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x17:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "move high packed double-precision floating-point value";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovhpd";
                                        else
                                            LastDisassembleData.OpCode = "movhpd";

                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last) + Xmm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "high to low packed single-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovhps";
                                        else
                                            LastDisassembleData.OpCode = "movhps";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last) + Xmm(memory[2]);
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x18:
                                {
                                    switch (GetReg(memory[2]))
                                    {
                                        case 0:
                                            {
                                                description = "prefetch";
                                                LastDisassembleData.OpCode = "prefetchnta";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 1:
                                            {
                                                description = "prefetch";
                                                LastDisassembleData.OpCode = "prefetchto";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 2:
                                            {
                                                description = "prefetch";
                                                LastDisassembleData.OpCode = "prefetcht1";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 3:
                                            {
                                                description = "prefetch";
                                                LastDisassembleData.OpCode = "prefetcht2";
                                                LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 2, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                    }
                                }
                                break;

                            case 0x1f:
                                {
                                    switch (GetReg(memory[2]))
                                    {
                                        case 0:
                                            {
                                                description = "multibyte nop";
                                                LastDisassembleData.OpCode = "nop";


                                                if (RexW)
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last, 64);
                                                else
                                                {
                                                    if (_prefix2.Contains(0x66))
                                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last, 16);
                                                    else
                                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last, 32);
                                                }

                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                    }
                                }
                                break;



                            case 0x20:
                                {
                                    description = "move from control register";
                                    LastDisassembleData.OpCode = "mov";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last) + Cr(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x21:
                                {
                                    description = "move from debug register";
                                    LastDisassembleData.OpCode = "mov";
                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 0, ref last) + Dr(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x22:
                                {
                                    description = "move to control register";
                                    LastDisassembleData.OpCode = "mov";
                                    LastDisassembleData.Parameters = Cr(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x23:
                                {
                                    description = "move to debug register";
                                    LastDisassembleData.OpCode = "mov";
                                    LastDisassembleData.Parameters = Dr(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x28:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "move aligned packed double-fp values";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovapd";
                                        else
                                            LastDisassembleData.OpCode = "movapd";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "move aligned four packed single-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovaps";
                                        else
                                            LastDisassembleData.OpCode = "movaps";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x29:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "move aligned packed double-fp values";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovapd";
                                        else
                                            LastDisassembleData.OpCode = "movapd";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last) + Xmm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "move aligned four packed single-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovaps";
                                        else
                                            LastDisassembleData.OpCode = "movaps";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last) + Xmm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;


                            case 0x2a:
                                {
                                    if (_prefix2.Contains(0xf2))
                                    {

                                        description = "convert doubleword integer to scalar doubleprecision floating-point value";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vcvtsi2sd";
                                        else
                                            LastDisassembleData.OpCode = "cvtsi2sd";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {

                                        description = "scalar signed int32 to single-fp conversion";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vcvtsi2ss";
                                        else
                                            LastDisassembleData.OpCode = "cvtsi2ss";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (_prefix2.Contains(0x66))
                                        {
                                            description = "convert packed dword's to packed dp-fp's";
                                            LastDisassembleData.OpCode = "cvtpi2pd";
                                            LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                        else
                                        {
                                            description = "packed signed int32 to packed single-fp conversion";
                                            LastDisassembleData.OpCode = "cvtpi2ps";
                                            LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                }
                                break;

                            case 0x2b:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovntpd";
                                        else
                                            LastDisassembleData.OpCode = "movntpd";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last) + Xmm(memory[2]);
                                        description = "move packed double-precision floating-point using non-temporal hint";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovntps";
                                        else
                                            LastDisassembleData.OpCode = "movntps";
                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last) + Xmm(memory[2]);
                                        description = "move aligned four packed single-fp non temporal";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x2c:
                                {
                                    if (_prefix2.Contains(0xf2))
                                    {

                                        description = "convert with truncation scalar double-precision floating point value to signed doubleword integer";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vcvttsd2si";
                                        else
                                            LastDisassembleData.OpCode = "cvttsd2si";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {
                                        description = "scalar single-fp to signed int32 conversion (truncate)";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vcvttss2si";
                                        else
                                            LastDisassembleData.OpCode = "cvttss2si";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (_prefix2.Contains(0x66))
                                        {
                                            description = "packed doubleprecision-fp to packed dword conversion (truncate)";
                                            LastDisassembleData.OpCode = "cvttpd2pi";
                                            LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                        else
                                        {
                                            description = "packed single-fp to packed int32 conversion (truncate)";
                                            LastDisassembleData.OpCode = "cvttps2pi";
                                            LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                }
                                break;

                            case 0x2d:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0xf2))
                                    {
                                        description = "convert scalar double-precision floating-point value to doubleword integer";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vcvtsd2si";
                                        else
                                            LastDisassembleData.OpCode = "cvtsd2si";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {
                                        description = "scalar single-fp to signed int32 conversion";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vcvtss2si";
                                        else
                                            LastDisassembleData.OpCode = "cvtss2si";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (_prefix2.Contains(0x66))
                                        {
                                            description = "convert 2 packed dp-fp's from param 2 to packed signed dword in param1";
                                            LastDisassembleData.OpCode = "cvtpi2ps";
                                            LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                        else
                                        {
                                            description = "packed single-fp to packed int32 conversion";
                                            LastDisassembleData.OpCode = "cvtps2pi";
                                            LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                            LastDisassembleData.DataSize = 4;
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                }
                                break;

                            case 0x2e:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "unordered scalar double-fp compare and set eflags";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vucomisd";
                                        else
                                            LastDisassembleData.OpCode = "ucomisd";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unordered scalar single-fp compare and set eflags";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vucomiss";
                                        else
                                            LastDisassembleData.OpCode = "ucomiss";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;


                            case 0x2f:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "compare scalar ordered double-precision floating point values and set eflags";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vcomisd";
                                        else
                                            LastDisassembleData.OpCode = "comisd";
                                        _opCodeFlags.SkipExtraReg = true;

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "scalar ordered single-fp compare and set eflags";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vcomiss";
                                        else
                                            LastDisassembleData.OpCode = "comiss";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x30:
                                {
                                    description = "write to model specific register";
                                    LastDisassembleData.OpCode = "wrmsr";
                                    offset += 1;
                                }
                                break;

                            case 0x31:
                                {
                                    description = "read time-stamp counter";
                                    LastDisassembleData.OpCode = "rdtsc";
                                    offset += 1;
                                }
                                break;

                            case 0x32:
                                {
                                    description = "read from model specific register";
                                    LastDisassembleData.OpCode = "rdmsr";
                                    offset += 1;
                                }
                                break;

                            case 0x33:
                                {
                                    description = "read performance-monitoring counters";
                                    LastDisassembleData.OpCode = "rdpmc";
                                    offset += 1;
                                }
                                break;

                            case 0x34:
                                {
                                    description = "fast transistion to system call entry point";
                                    LastDisassembleData.OpCode = "sysenter";
                                    LastDisassembleData.IsRet = true;
                                    offset += 1;
                                }
                                break;

                            case 0x35:
                                {
                                    description = "fast transistion from system call entry point";
                                    LastDisassembleData.OpCode = "sysexit";
                                    offset += 1;
                                }
                                break;

                            case 0x37:
                                {
                                    description = "Safermode multipurpose function";
                                    LastDisassembleData.OpCode = "getsec";
                                    offset += 1;
                                }
                                break;

                            /*0f*/
                            case 0x38:
                                {
                                    switch (memory[2])
                                    {
                                        case 0:
                                            {
                                                description = "Packed shuffle bytes";
                                                LastDisassembleData.OpCode = "pshufb";

                                                if (_prefix2.Contains(0x66))
                                                {
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);

                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = 'v' + LastDisassembleData.OpCode;
                                                }
                                                else
                                                    LastDisassembleData.Parameters = Mm(memory[3]) + ModRm(memory, _prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 0x1:
                                            {
                                                description = "Packed horizontal add";
                                                LastDisassembleData.OpCode = "phaddw";

                                                if (_prefix2.Contains(0x66))
                                                {
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);

                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = 'v' + LastDisassembleData.OpCode;
                                                }
                                                else
                                                    LastDisassembleData.Parameters = Mm(memory[3]) + ModRm(memory, _prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x2:
                                            {
                                                description = "Packed horizontal add";
                                                LastDisassembleData.OpCode = "phaddd";

                                                if (_prefix2.Contains(0x66))
                                                {
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);

                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = 'v' + LastDisassembleData.OpCode;
                                                }
                                                else
                                                    LastDisassembleData.Parameters = Mm(memory[3]) + ModRm(memory, _prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 0x3:
                                            {
                                                description = "Packed horizontal add and saturate";
                                                LastDisassembleData.OpCode = "phaddsw";

                                                if (_prefix2.Contains(0x66))
                                                {
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);

                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = 'v' + LastDisassembleData.OpCode;
                                                }
                                                else
                                                    LastDisassembleData.Parameters = Mm(memory[3]) + ModRm(memory, _prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 0x4:
                                            {
                                                description = "Multiply and add signed and unsigned bytes";
                                                LastDisassembleData.OpCode = "pmaddubsw";

                                                if (_prefix2.Contains(0x66))
                                                {
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);

                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = 'v' + LastDisassembleData.OpCode;
                                                }
                                                else
                                                    LastDisassembleData.Parameters = Mm(memory[3]) + ModRm(memory, _prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x5:
                                            {
                                                description = "Packed horizontal subtract";
                                                LastDisassembleData.OpCode = "phsubw";

                                                if (_prefix2.Contains(0x66))
                                                {
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);

                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = 'v' + LastDisassembleData.OpCode;
                                                }
                                                else
                                                    LastDisassembleData.Parameters = Mm(memory[3]) + ModRm(memory, _prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 0x6:
                                            {
                                                description = "Packed horizontal subtract";
                                                LastDisassembleData.OpCode = "phsubd";

                                                if (_prefix2.Contains(0x66))
                                                {
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);

                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = 'v' + LastDisassembleData.OpCode;
                                                }
                                                else
                                                    LastDisassembleData.Parameters = Mm(memory[3]) + ModRm(memory, _prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 0x7:
                                            {
                                                description = "Packed horizontal subtract";
                                                LastDisassembleData.OpCode = "phsubsw";

                                                if (_prefix2.Contains(0x66))
                                                {
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);

                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = 'v' + LastDisassembleData.OpCode;
                                                }
                                                else
                                                    LastDisassembleData.Parameters = Mm(memory[3]) + ModRm(memory, _prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 0x8:
                                            {
                                                description = "Packed SIGN";
                                                LastDisassembleData.OpCode = "psignb";

                                                if (_prefix2.Contains(0x66))
                                                {
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);

                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = 'v' + LastDisassembleData.OpCode;
                                                }
                                                else
                                                    LastDisassembleData.Parameters = Mm(memory[3]) + ModRm(memory, _prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 0x9:
                                            {
                                                description = "Packed SIGN";
                                                LastDisassembleData.OpCode = "psignw";

                                                if (_prefix2.Contains(0x66))
                                                {
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);

                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = 'v' + LastDisassembleData.OpCode;
                                                }
                                                else
                                                    LastDisassembleData.Parameters = Mm(memory[3]) + ModRm(memory, _prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 0xa:
                                            {
                                                description = "Packed SIGN";
                                                LastDisassembleData.OpCode = "psignd";

                                                if (_prefix2.Contains(0x66))
                                                {
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);

                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = 'v' + LastDisassembleData.OpCode;
                                                }
                                                else
                                                    LastDisassembleData.Parameters = Mm(memory[3]) + ModRm(memory, _prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xb:
                                            {
                                                description = "Packed multiply high with round and scale";
                                                LastDisassembleData.OpCode = "phmulhrsw";

                                                if (_prefix2.Contains(0x66))
                                                {
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);

                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = 'v' + LastDisassembleData.OpCode;
                                                }
                                                else
                                                    LastDisassembleData.Parameters = Mm(memory[3]) + ModRm(memory, _prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xc:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "permute single-precision floating-point values";
                                                        LastDisassembleData.OpCode = "vpermilps";
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);

                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }

                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xd:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "permute double-precision floating-point values";
                                                        LastDisassembleData.OpCode = "vpermilpd";
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);

                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }

                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xe:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Packed bit test";
                                                        LastDisassembleData.OpCode = "vtestps";
                                                        _opCodeFlags.SkipExtraReg = true;
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);

                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xf:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Packed bit test";
                                                        LastDisassembleData.OpCode = "vtestpd";
                                                        _opCodeFlags.SkipExtraReg = true;
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);

                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x10:
                                            {
                                                description = "Variable blend packed bytes";
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        LastDisassembleData.OpCode = "vpblendvb";
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + RegNrToStr(ARegisterType.RtXmm, memory[(int)last]);
                                                        offset += 1;
                                                    }
                                                    else
                                                    {
                                                        LastDisassembleData.OpCode = "pblendvb";
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',' + RegNrToStr(ARegisterType.RtXmm, 0);
                                                    }
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x13:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Convert 16-bit FP values to single-precision FP values";
                                                    LastDisassembleData.OpCode = "vcvtph2ps";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;


                                        case 0x14:
                                            {
                                                description = "Variable blend packed single precision floating-point values";
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        LastDisassembleData.OpCode = "vblendvps";
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + RegNrToStr(ARegisterType.RtXmm, memory[(int)last]);
                                                        offset += 1;
                                                    }
                                                    else
                                                    {
                                                        LastDisassembleData.OpCode = "blendvps";
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',' + RegNrToStr(ARegisterType.RtXmm, 0);
                                                    }
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        case 0x15:
                                            {
                                                description = "Variable blend packed double precision floating-point values";
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        LastDisassembleData.OpCode = "vblendvpd invalid";
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + RegNrToStr(ARegisterType.RtXmm, memory[(int)last]);
                                                        offset += 1;
                                                    }
                                                    else
                                                    {
                                                        LastDisassembleData.OpCode = "blendvpd";
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',' + _colorReg + RegNrToStr(ARegisterType.RtXmm, 0) + _endColor;
                                                    }
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        case 0x16:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Permute single-precision floating-point elements";
                                                        LastDisassembleData.OpCode = "vpermps";
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x17:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Logical compare";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vptest";
                                                    else
                                                        LastDisassembleData.OpCode = "ptest";

                                                    _opCodeFlags.SkipExtraReg = true;
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        case 0x18:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Broadcast floating-point-data";
                                                        LastDisassembleData.OpCode = "vbroadcastss";
                                                        _opCodeFlags.SkipExtraReg = true;
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x19:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Broadcast floating-point-data";
                                                        LastDisassembleData.OpCode = "vbroadcastsd";
                                                        _opCodeFlags.SkipExtraReg = true;
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x1a:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Broadcast floating-point-data";
                                                        LastDisassembleData.OpCode = "vbroadcastf128";
                                                        _opCodeFlags.SkipExtraReg = true;
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x1c:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed absolute value";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpabsb";
                                                    else
                                                        LastDisassembleData.OpCode = "pabsb";

                                                    _opCodeFlags.SkipExtraReg = true;
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    description = "Packed absolute value";
                                                    LastDisassembleData.OpCode = "pabsb";
                                                    LastDisassembleData.Parameters = Mm(memory[3]) + ModRm(memory, _prefix2, 3, 3, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x1d:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed absolute value";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpabsw";
                                                    else
                                                        LastDisassembleData.OpCode = "pabsw";

                                                    _opCodeFlags.SkipExtraReg = true;
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    description = "Packed absolute value";
                                                    LastDisassembleData.OpCode = "pabsw";
                                                    LastDisassembleData.Parameters = Mm(memory[3]) + ModRm(memory, _prefix2, 3, 3, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x1e:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed absolute value";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpabsd";
                                                    else
                                                        LastDisassembleData.OpCode = "pabsd";

                                                    _opCodeFlags.SkipExtraReg = true;
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    description = "Packed absolute value";
                                                    LastDisassembleData.OpCode = "pabsd";
                                                    LastDisassembleData.Parameters = Mm(memory[3]) + ModRm(memory, _prefix2, 3, 3, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x20:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with sign extend";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpmovsxbw";
                                                    else
                                                        LastDisassembleData.OpCode = "pmovsxbw";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x21:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with sign extend";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpmovsxbd";
                                                    else
                                                        LastDisassembleData.OpCode = "pmovsxbd";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x22:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with sign extend";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpmovsxbq";
                                                    else
                                                        LastDisassembleData.OpCode = "pmovsxbq";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x23:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with sign extend";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpmovsxwd";
                                                    else
                                                        LastDisassembleData.OpCode = "pmovsxwd";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x24:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with sign extend";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpmovsxwq";
                                                    else
                                                        LastDisassembleData.OpCode = "pmovsxwq";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x25:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with sign extend";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpmovsxdq";
                                                    else
                                                        LastDisassembleData.OpCode = "pmovsxdq";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x28:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Multiple packed signed dword integers";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpmuldq";
                                                    else
                                                        LastDisassembleData.OpCode = "pmuldq";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x29:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Compare packed qword data for equal";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpcmpeqq";
                                                    else
                                                        LastDisassembleData.OpCode = "pcmpeqq";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x2a:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Load double quadword non-temporal aligned hint";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vmovntdqa";
                                                    else
                                                        LastDisassembleData.OpCode = "movntdqa";

                                                    _opCodeFlags.SkipExtraReg = true;
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x2b:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Pack with unsigned saturation";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpackusdw";
                                                    else
                                                        LastDisassembleData.OpCode = "packusdw";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x2c:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Conditional SIMD packed loads and stores";
                                                        LastDisassembleData.OpCode = "vmaskmovps";
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x2d:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Conditional SIMD packed loads and stores";
                                                        LastDisassembleData.OpCode = "vmaskmovpd";
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x2e:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Conditional SIMD packed loads and stores";
                                                        LastDisassembleData.OpCode = "vmaskmovps";
                                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Left) + Xmm(memory[3]);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x2f:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Conditional SIMD packed loads and stores";
                                                        LastDisassembleData.OpCode = "vmaskmovpd";
                                                        LastDisassembleData.Parameters = ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Left) + Xmm(memory[3]);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x30:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with zero extend";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpmovzxbw";
                                                    else
                                                        LastDisassembleData.OpCode = "pmovzxbw";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x31:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with zero extend";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpmovzxbd";
                                                    else
                                                        LastDisassembleData.OpCode = "pmovzxbd";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x32:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with zero extend";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpmovzxbq";
                                                    else
                                                        LastDisassembleData.OpCode = "pmovzxbq";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x33:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with zero extend";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpmovzxwd";
                                                    else
                                                        LastDisassembleData.OpCode = "pmovzxwd";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x34:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with zero extend";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpmovzxwq";
                                                    else
                                                        LastDisassembleData.OpCode = "pmovzxwq";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x35:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with zero extend";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpmovzxdq";
                                                    else
                                                        LastDisassembleData.OpCode = "pmovzxdq";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x36:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Full doublewords element permutation";
                                                        LastDisassembleData.OpCode = "vpermd";
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;


                                        /*0f*//*38*/
                                        case 0x37:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Compare packed data for greater than";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpcmpgtq";
                                                    else
                                                        LastDisassembleData.OpCode = "pcmpgtq";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x38:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Minimum of packed signed byte integers";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpminsb";
                                                    else
                                                        LastDisassembleData.OpCode = "pminsb";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x39:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Minimum of packed dword integers";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpminsd";
                                                    else
                                                        LastDisassembleData.OpCode = "pminsd";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x3a:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Minimum of packed word integers";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpminuw";
                                                    else
                                                        LastDisassembleData.OpCode = "pminuw";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x3b:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Minimum of packed dword integers";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpminud";
                                                    else
                                                        LastDisassembleData.OpCode = "pminud";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x3c:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Maximum of packed signed byte integers";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpmaxsb";
                                                    else
                                                        LastDisassembleData.OpCode = "pmaxsb";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x3d:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Maximum of packed signed dword integers";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpmaxsd";
                                                    else
                                                        LastDisassembleData.OpCode = "pmaxsd";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x3e:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Maximum of packed word integers";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpmaxuw";
                                                    else
                                                        LastDisassembleData.OpCode = "pmaxuw";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x3f:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Maximum of packed unsigned dword integers";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpmaxud";
                                                    else
                                                        LastDisassembleData.OpCode = "pmaxud";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x40:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Multiply Packed Signed Dword Integers and Store Low Result";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpmulld";
                                                    else
                                                        LastDisassembleData.OpCode = "pmulld";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x41:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed horitontal word minimum";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "phminposuw";
                                                    else
                                                        LastDisassembleData.OpCode = "vphminposuw";

                                                    _opCodeFlags.SkipExtraReg = true;
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        case 0x45:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Variable Bit Shift Right Logical";

                                                        if (RexW)
                                                            LastDisassembleData.OpCode = "vpsrlvq";
                                                        else
                                                            LastDisassembleData.OpCode = "vpsrlvd";

                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x46:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Variable bit shift right arithmetic";
                                                        LastDisassembleData.OpCode = "vpsravd";
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x47:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Variable Bit Shift Left Logical";

                                                        if (RexW)
                                                            LastDisassembleData.OpCode = "vpsllvq";
                                                        else
                                                            LastDisassembleData.OpCode = "vpsllvd";

                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;


                                        /*0f*//*38*/
                                        case 0x58:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Broadcast integer data";
                                                        LastDisassembleData.OpCode = "vpbroadcastd";
                                                        _opCodeFlags.SkipExtraReg = true;
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x59:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Broadcast integer data";
                                                        LastDisassembleData.OpCode = "vpbroadcastq";
                                                        _opCodeFlags.SkipExtraReg = true;
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x5a:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Broadcast integer data";
                                                        LastDisassembleData.OpCode = "vpbroadcasti128";
                                                        _opCodeFlags.SkipExtraReg = true;
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x78:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Broadcast integer data";
                                                        LastDisassembleData.OpCode = "vpbroadcastb";
                                                        _opCodeFlags.SkipExtraReg = true;
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x79:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Broadcast integer data";
                                                        LastDisassembleData.OpCode = "vpbroadcastw";
                                                        _opCodeFlags.SkipExtraReg = true;
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x82:
                                            {
                                                description = "Invalidate process-context-identifier";
                                                LastDisassembleData.OpCode = "invpcid";
                                                if (SymbolHandler.Process.IsX64)
                                                    LastDisassembleData.Parameters = R64(memory[3]) + ModRm(memory, _prefix2, 3, 0, ref last, 128, 0, ATmrPos.Right);
                                                else
                                                    LastDisassembleData.Parameters = R32(memory[3]) + ModRm(memory, _prefix2, 3, 0, ref last, 128, 0, ATmrPos.Right);

                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;

                                        case 0x8c:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Conditional SIMD Integer Packed Loads and Stores";
                                                            LastDisassembleData.OpCode = "vpmaskmovq";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Conditional SIMD Integer Packed Loads and Stores";
                                                            LastDisassembleData.OpCode = "vpmaskmovd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x8e:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Conditional SIMD Integer Packed Loads and Stores";
                                                            LastDisassembleData.OpCode = "vpmaskmovq";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Left) + Xmm(memory[3]);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Conditional SIMD Integer Packed Loads and Stores";
                                                            LastDisassembleData.OpCode = "vpmaskmovd";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Left) + Xmm(memory[3]);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;


                                        case 0x96:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused multiple-alnterating add/subtract of packed double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmaddsub132pd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-alnterating add/subtract of precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmaddsub132ps";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x97:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused multiple-alnterating subtract/add of packed double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsubadd132pd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-alnterating subtract/add of precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsubadd132ps";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x98:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused multiple-add of packed double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmadd132pd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-add of packed single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmadd132ps";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x99:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused multiple-add of scalar double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmadd132sd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-add of scalar single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmadd132ss";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x9a:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused multiple-subtract of packed double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub132pd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-subtract of packed single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub132ps";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x9b:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused multiple-subtract of scalar double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub132sd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-subtract of scalar single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub132ss";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x9c:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused negative multiply-add of packed double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfnmadd132pd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-add of packed single-precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfnmadd132ps";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x9d:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused negative multiply-add of scalar double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfnmadd132sd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-add of scalar single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfnmadd132ss";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x9e:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused negative multiply-subtract of packed double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub132pd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-subtract of packed single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub132ps";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x9f:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused negative multiply-subtract of scalar double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub132sd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused begative multiply-subtract of scalar single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub132ss";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xa6:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused multiply-alternating add/subtract of packed double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmaddsub213pd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiply-alternating add/subtract of packed single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmaddsub213ps";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xa7:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused multiply-alternating subtract/add of packed double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsubadd213pd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiply-alternating subtract/add of packed single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsubadd213ps";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xa8:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused multiple-add of packed double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmadd213pd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-add of packed single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmadd213ps";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xa9:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused multiple-add of scalar double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmadd213sd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-add of scalar single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmadd213ss";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xaa:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused multiple-subtract of packed double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub213pd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-subtract of packed single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub213ps";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;


                                        case 0xab:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused multiple-subtract of scalar double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub213sd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-subtract of scalar single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub213ss";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xac:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused negative multiply-add of packed double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfnmadd213pd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-add of packed single-precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfnmadd213ps";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xad:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused negative multiply-add of scalar double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfnmadd213sd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-add of scalar single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfnmadd213ss";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xae:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused negative multiply-subtract of packed double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub213pd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-subtract of packed single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub213ps";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xaf:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused negative multiply-subtract of scalar double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfnmsub213sd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused begative multiply-subtract of scalar single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfnmsub213ss";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xb6:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused multiply-alternating add/subtract of packed double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmaddsub231pd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiply-alternating add/subtract of packed single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmaddsub231ps";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xb7:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused multiply-alternating subtract/add of packed double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsubadd231pd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiply-alternating add/subtract of packed single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsubadd231ps";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xb8:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused multiple-add of packed double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmadd231pd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-add of packed single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmadd231ps";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xb9:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused multiple-add of scalar double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmadd231sd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-add of scalar single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmadd231ss";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xba:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused multiple-subtract of packed double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub231pd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-subtract of packed single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub231ps";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xbb:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused multiple-subtract of scalar double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub231sd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-subtract of scalar single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub231ss";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xbc:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused negative multiply-add of packed double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfnmadd231pd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-add of packed single-precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfnmadd231ps";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xbd:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused negative multiply-add of scalar double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfnmadd231sd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-add of scalar single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfnmadd231ss";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;


                                        case 0xbe:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused negative multiply-subtract of packed double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub231pd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-subtract of packed single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub231ps";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xbf:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        if (RexW)
                                                        {
                                                            description = "Fused negative multiply-subtract of scalar double precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub231sd";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused begative multiply-subtract of scalar single precision floating-point-values";
                                                            LastDisassembleData.OpCode = "vfmsub231ss";
                                                            LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;


                                        case 0xdb:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Perform the AES InvMixColumn transformation";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vaesimc";
                                                    else
                                                        LastDisassembleData.OpCode = "aesimc";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        case 0xdc:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Perform one round of an AES encryption flow";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vaesenc";
                                                    else
                                                        LastDisassembleData.OpCode = "aesenc";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        case 0xdd:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Perform last round of an AES encryption flow";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "caesenclast";
                                                    else
                                                        LastDisassembleData.OpCode = "aesenclast";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xde:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Perform one round of an AES decryption flow";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vaesdec";
                                                    else
                                                        LastDisassembleData.OpCode = "aesdec";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        case 0xdf:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Perform last round of an AES decryption flow";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "caesdeclast";
                                                    else
                                                        LastDisassembleData.OpCode = "aesdeclast";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xf0:
                                            {
                                                if (_prefix2.Contains(0xf2))
                                                {
                                                    description = "Accumulate CRC32 value";
                                                    LastDisassembleData.OpCode = "crc32";
                                                    LastDisassembleData.Parameters = R32(memory[3]) + ModRm(memory, _prefix2, 3, 2, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    description = "Move data after swapping bytes";
                                                    LastDisassembleData.OpCode = "movbe";
                                                    if (_prefix2.Contains(0x66))
                                                        LastDisassembleData.Parameters = R16(memory[3]) + ModRm(memory, _prefix2, 3, 2, ref last, ATmrPos.Right);
                                                    else
                                                        LastDisassembleData.Parameters = R32(memory[3]) + ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        case 0xf1:
                                            {
                                                if (_prefix2.Contains(0xf2))
                                                {
                                                    description = "Accumulate CRC32 value";
                                                    LastDisassembleData.OpCode = "crc32";
                                                    LastDisassembleData.Parameters = R32(memory[3]) + ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    description = "Move data after swapping bytes";
                                                    LastDisassembleData.OpCode = "movbe";
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Left) + R32(memory[3]);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        case 0xf2:
                                            {
                                                if (_hasVex)
                                                {
                                                    description = "Logical AND NOT";
                                                    LastDisassembleData.OpCode = "andn";
                                                    LastDisassembleData.Parameters = R32(memory[3]) + ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xf3:
                                            {
                                                switch (GetReg(memory[3]))
                                                {
                                                    case 1:
                                                        {
                                                            description = "Reset lowerst set bit";
                                                            LastDisassembleData.OpCode = "blsr";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;

                                                    case 2:
                                                        {
                                                            description = "Get mask up to lowest set bit";
                                                            LastDisassembleData.OpCode = "blsmsk";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;

                                                    case 3:
                                                        {
                                                            description = "Extract lowest set isolated bit";
                                                            LastDisassembleData.OpCode = "blsi";
                                                            LastDisassembleData.Parameters = ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;
                                                }
                                            }
                                            break;

                                        case 0xf5:
                                            {
                                                if (_prefix2.Contains(0xf2))
                                                {
                                                    description = "Parallel bits deposit";
                                                    LastDisassembleData.OpCode = "pdep";
                                                    LastDisassembleData.Parameters = R32(memory[3]) + ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Left);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    description = "Zero high bits starting with specified bit position";
                                                    LastDisassembleData.OpCode = "bzhi";
                                                    LastDisassembleData.Parameters = R32(memory[3]) + ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Left);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;


                                        case 0xf6:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "ADX: Unsigned Integer Addition of Two Operands with Carry Flag";
                                                    LastDisassembleData.OpCode = "adcx";
                                                    LastDisassembleData.Parameters = R32(memory[3]) + ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                if (_prefix2.Contains(0xf3))
                                                {
                                                    description = "ADX: Unsigned Integer Addition of Two Operands with Overflow Flag";
                                                    LastDisassembleData.OpCode = "adox";
                                                    LastDisassembleData.Parameters = R32(memory[3]) + ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Unsigned multiple without affecting flags";
                                                        LastDisassembleData.OpCode = "mulx";
                                                        LastDisassembleData.Parameters = R32(memory[3]) + ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xf7:
                                            {
                                                if (_hasVex)
                                                {
                                                    if (_prefix2.Contains(0xf3))
                                                    {
                                                        description = "Shift arithmetically right without affecting flags";
                                                        LastDisassembleData.OpCode = "SARX";
                                                        LastDisassembleData.Parameters = R32(memory[3]) + ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                    else
                                                    if (_prefix2.Contains(0xf2))
                                                    {
                                                        description = "Shift logically right without affecting flags";
                                                        LastDisassembleData.OpCode = "SHRX";
                                                        LastDisassembleData.Parameters = R32(memory[3]) + ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                    else
                                                    if (_prefix2.Contains(0x66))
                                                    {
                                                        description = "Shift logically left without affecting flags";
                                                        LastDisassembleData.OpCode = "SHLX";
                                                        LastDisassembleData.Parameters = R32(memory[3]) + ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                                else
                                                {
                                                    description = "Bit field extract";
                                                    LastDisassembleData.OpCode = "BEXTR";
                                                    LastDisassembleData.Parameters = R32(memory[3]) + ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        default:
                                            {
                                                if (_hasVex)
                                                {
                                                    LastDisassembleData.OpCode = "unknown avx 0F38 " + AStringUtils.IntToHex(memory[2], 2);
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);

                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                    }
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
