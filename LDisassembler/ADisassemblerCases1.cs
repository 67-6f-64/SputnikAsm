using System;
using Sputnik.LBinary;
using Sputnik.LMarshal;
using SputnikAsm.LDisassembler.LEnums;
using SputnikAsm.LUtils;
namespace SputnikAsm.LDisassembler
{
    public static class ADisassemblerCases1
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
                case 0:
                    {
                        if ((d.AggressiveAlignment & (((offset.ToUInt64()) & 0xf) == 0) && (memory[1] != 0)) || ((memory[1] == 0x55) && (memory[2] == 0x89) && (memory[3] == 0xe5)))
                        {
                            description = "Filler";
                            d.LastDisassembleData.OpCode = "db";
                            d.LastDisassembleData.Parameters = AStringUtils.IntToHex(memory[0], 2);
                        }
                        else
                        {
                            description = "Add";
                            d.LastDisassembleData.OpCode = "add";
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last) + d.R8(memory[1]);
                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                        }
                    }
                    break;
                case 0x1:
                    {
                        description = "Add";
                        d.LastDisassembleData.OpCode = "add";
                        if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last) + d.R16(memory[1]);
                        else
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + d.R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x2:
                    {
                        description = "Add";
                        d.LastDisassembleData.OpCode = "add";
                        d.LastDisassembleData.Parameters = d.R8(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 2, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x3:
                    {
                        description = "Add";
                        d.LastDisassembleData.OpCode = "add";
                        if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.Parameters = d.R16(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            d.LastDisassembleData.Parameters = d.R32(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 0, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x4:
                    {
                        description = "Add " + AStringUtils.IntToHex(memory[1], 2) + " to AL";
                        d.LastDisassembleData.OpCode = "add";
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        d.LastDisassembleData.Parameters = d.ColorReg + "al" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)memory[1], 2);
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        offset += 1;
                    }
                    break;
                case 0x5:
                    {
                        d.LastDisassembleData.OpCode = "add";
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        var wordptr = memory.ToIntPtr(1).ReadUInt16();
                        var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                        if (d.Prefix2.Contains(0x66))
                        {
                            d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                            d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                            d.LastDisassembleData.Parameters = d.ColorReg + "ax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)wordptr, 4);
                            description = "add " + AStringUtils.IntToHex(wordptr, 4) + " to ax";
                            offset += 2;
                        }
                        else
                        {
                            if (d.RexW)
                            {
                                d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                                d.LastDisassembleData.Parameters = d.ColorReg + "rax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)dwordptr, 8);
                                description = "add " + AStringUtils.IntToHex(dwordptr, 8) + " to rax (sign extended)";
                            }
                            else
                            {
                                d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                                d.LastDisassembleData.Parameters = d.ColorReg + "eax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)dwordptr, 8);
                                description = "add " + AStringUtils.IntToHex(dwordptr, 8) + " to eax";
                            }
                            offset += 4;
                        }
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                    }
                    break;
                case 0x6:
                    {
                        d.LastDisassembleData.OpCode = "push";
                        d.LastDisassembleData.Parameters = d.ColorReg + "es" + d.EndColor;
                        description = "place es on the stack";
                    }
                    break;
                case 0x7:
                    {
                        d.LastDisassembleData.OpCode = "pop";
                        d.LastDisassembleData.Parameters = d.ColorReg + "es" + d.EndColor;
                        description = "remove es from the stack";
                    }
                    break;
                case 0x8:
                    {
                        description = "logical inclusive or";
                        d.LastDisassembleData.OpCode = "or";
                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 2, ref last) + d.R8(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0x9:
                    {
                        description = "logical inclusive or";
                        d.LastDisassembleData.OpCode = "or";
                        if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 1, ref last) + d.R16(memory[1]);
                        else
                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 1, 0, ref last) + d.R32(memory[1]);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0xa:
                    {
                        description = "logical inclusive or";
                        d.LastDisassembleData.OpCode = "or";
                        d.LastDisassembleData.Parameters = d.R8(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 2, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0xb:
                    {
                        description = "logical inclusive or";
                        d.LastDisassembleData.OpCode = "or";
                        if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.Parameters = d.R16(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 1, ref last, ATmrPos.Right);
                        else
                            d.LastDisassembleData.Parameters = d.R32(memory[1]) + d.ModRm(memory, d.Prefix2, 1, 0, ref last, ATmrPos.Right);
                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                    }
                    break;
                case 0xc:
                    {
                        description = "logical inclusive or";
                        d.LastDisassembleData.OpCode = "or";
                        d.LastDisassembleData.Parameters = d.ColorReg + "al" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)memory[1], 2);
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        d.LastDisassembleData.ParameterValue = (UIntPtr)memory[1];
                        d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                        d.LastDisassembleData.SeparatorCount += 1;
                        offset += 1;
                    }
                    break;
                case 0xd:
                    {
                        description = "logical inclusive or";
                        d.LastDisassembleData.OpCode = "or";
                        d.LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                        if (d.Prefix2.Contains(0x66))
                        {
                            var wordptr = memory.ToIntPtr(1).ReadUInt16();
                            d.LastDisassembleData.ParameterValue = (UIntPtr)wordptr;
                            d.LastDisassembleData.Parameters = d.ColorReg + "ax" + d.EndColor + ',' + d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 4);
                            offset += 2;
                        }
                        else
                        {
                            var dwordptr = memory.ToIntPtr(1).ReadUInt32();
                            d.LastDisassembleData.ParameterValue = (UIntPtr)dwordptr;
                            if (d.RexW)
                            {
                                d.LastDisassembleData.Parameters = d.ColorReg + "rax" + d.EndColor + ',' + d.IntToHexSigned((UIntPtr)d.LastDisassembleData.ParameterValue, 8);
                                description += " (sign-extended)";
                            }
                            else
                                d.LastDisassembleData.Parameters = d.ColorReg + "eax" + d.EndColor + ',' + d.IntToHexSigned(d.LastDisassembleData.ParameterValue, 8);
                            d.LastDisassembleData.Separators[d.LastDisassembleData.SeparatorCount] = 1;
                            d.LastDisassembleData.SeparatorCount += 1;
                            offset += 4;
                        }
                    }
                    break;
                case 0xe:
                    {
                        description = "place cs on the stack";
                        d.LastDisassembleData.OpCode = "push";
                        d.LastDisassembleData.Parameters = d.ColorReg + "cs" + d.EndColor;
                    }
                    break;
                case 0xf:
                    {  
                        //simd extensions
                        if (d.Prefix2.Contains(0xf0))
                            d.LastDisassembleData.Prefix = "lock ";
                        else
                            d.LastDisassembleData.Prefix = ""; //these usually treat the f2/f3 prefix differently
                        switch (memory[1])
                        {
                            case 0:
                                {
                                    switch (d.GetReg(memory[2]))
                                    {
                                        case 0:
                                            {
                                                d.LastDisassembleData.OpCode = "sldt";
                                                description = "store local descriptor table register";
                                                if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last, 16);
                                                else
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 1:
                                            {
                                                description = "store task register";
                                                d.LastDisassembleData.OpCode = "str";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last, 16);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 2:
                                            {
                                                description = "load local descriptor table register";
                                                d.LastDisassembleData.OpCode = "lldt";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last, 16);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 3:
                                            {
                                                description = "load task register";
                                                d.LastDisassembleData.OpCode = "ltr";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last, 16);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 4:
                                            {
                                                description = "verify a segment for reading";
                                                d.LastDisassembleData.OpCode = "verr";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last, 16);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 5:
                                            {
                                                description = "verify a segment for writing";
                                                d.LastDisassembleData.OpCode = "verw";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last, 16);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        default:
                                            {
                                                d.LastDisassembleData.OpCode = "db";
                                                d.LastDisassembleData.Parameters = AStringUtils.IntToHex(memory[0], 2);
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
                                                d.LastDisassembleData.OpCode = "vmcall";
                                                offset += 2;
                                            }
                                            break;
                                        case 0xc2:
                                            {
                                                description = "launch virtual machine managed by current vmcs";
                                                d.LastDisassembleData.OpCode = "vmlaunch";
                                                offset += 2;
                                            }
                                            break;
                                        case 0xc3:
                                            {
                                                description = "resume virtual machine managed by current vmcs";
                                                d.LastDisassembleData.OpCode = "vmresume";
                                                offset += 2;
                                            }
                                            break;
                                        case 0xc4:
                                            {
                                                description = "leaves vmx operation";
                                                d.LastDisassembleData.OpCode = "vmxoff";
                                                offset += 2;
                                            }
                                            break;
                                        case 0xc8:
                                            {
                                                description = "set up monitor address";
                                                d.LastDisassembleData.OpCode = "monitor";
                                                offset += 2;
                                            }
                                            break;
                                        case 0xc9:
                                            {
                                                description = "Monitor wait";
                                                d.LastDisassembleData.OpCode = "mwait";
                                                offset += 2;
                                            }
                                            break;
                                        case 0xca:
                                            {
                                                description = "Clear AC flag in EFLAGS register";
                                                d.LastDisassembleData.OpCode = "clac";
                                                offset += 2;
                                            }
                                            break;
                                        case 0xd0:
                                            {
                                                description = "Get value of extended control register";
                                                d.LastDisassembleData.OpCode = "xgetbv";
                                                offset += 2;
                                            }
                                            break;
                                        case 0xd1:
                                            {
                                                description = "Set value of extended control register";
                                                d.LastDisassembleData.OpCode = "xsetbv";
                                                offset += 2;
                                            }
                                            break;
                                        case 0xd5:
                                            {
                                                description = "Transactional end";
                                                d.LastDisassembleData.OpCode = "xend";
                                                offset += 2;
                                            }
                                            break;
                                        case 0xd6:
                                            {
                                                description = "Test if in transactional execution";
                                                d.LastDisassembleData.OpCode = "xtest";
                                                offset += 2;
                                            }
                                            break;
                                        case 0xf8:
                                            {
                                                description = "Swap GS base register";
                                                d.LastDisassembleData.OpCode = "swapgs";
                                                offset += 2;
                                            }
                                            break;
                                        case 0xf9:
                                            {
                                                description = "Read time-stamp counter and processor ID";
                                                d.LastDisassembleData.OpCode = "rdtscp";
                                                offset += 2;
                                            }
                                            break;
                                        default:
                                            {
                                                switch (d.GetReg(memory[2]))
                                                {
                                                    case 0:
                                                        {
                                                            description = "store global descriptor table register";
                                                            d.LastDisassembleData.OpCode = "sgdt";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;
                                                    case 1:
                                                        {
                                                            description = "store interrupt descriptor table register";
                                                            d.LastDisassembleData.OpCode = "sidt";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;
                                                    case 2:
                                                        {
                                                            description = "load global descriptor table register";
                                                            d.LastDisassembleData.OpCode = "lgdt";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;
                                                    case 3:
                                                        {
                                                            description = "load interupt descriptor table register";
                                                            d.LastDisassembleData.OpCode = "lidt";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;
                                                    case 4:
                                                        {
                                                            description = "store machine status word";
                                                            d.LastDisassembleData.OpCode = "smsw";
                                                            if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last);
                                                            else d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;
                                                    case 6:
                                                        {
                                                            description = "load machine status word";
                                                            d.LastDisassembleData.OpCode = "lmsw";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 1, ref last);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;
                                                    case 7:
                                                        {
                                                            description = "invalidate tlb entry";
                                                            d.LastDisassembleData.OpCode = "invplg";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last);
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
                                    d.LastDisassembleData.OpCode = "lar";
                                    if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 2, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            /*0f*/
                            case 0x3:
                                {
                                    description = "load segment limit";
                                    d.LastDisassembleData.OpCode = "lsl";
                                    if (d.Prefix2.Contains(0x66)) d.LastDisassembleData.Parameters = d.R16(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 2, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x5:
                                {
                                    description = "fast system call";
                                    d.LastDisassembleData.OpCode = "syscall";
                                    offset += 1;
                                }
                                break;
                            case 0x6:
                                {
                                    description = "clear task-switched flag in cr0";
                                    d.LastDisassembleData.OpCode = "clts";
                                    offset += 1;
                                }
                                break;
                            case 0x7:
                                {
                                    description = "return from fast system call";
                                    d.LastDisassembleData.OpCode = "sysret";
                                    offset += 1;
                                }
                                break;
                            case 0x8:
                                {
                                    description = "invalidate internal caches";
                                    d.LastDisassembleData.OpCode = "invd";
                                    offset += 1;
                                }
                                break;
                            case 0x9:
                                {
                                    description = "write back and invalidate cache";
                                    d.LastDisassembleData.OpCode = "wbinvd";
                                    offset += 1;
                                }
                                break;
                            case 0xb:
                                {
                                    description = "undefined instruction(yes, this one really excists..)";
                                    d.LastDisassembleData.OpCode = "ud2";
                                    offset += 1;
                                }
                                break;
                            case 0xd:
                                {
                                    switch (d.GetReg(memory[2]))
                                    {
                                        case 1:
                                            {
                                                description = "Prefetch Data into Caches in Anticipation of a Write";
                                                d.LastDisassembleData.OpCode = "prefetchw";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 2:
                                            {
                                                description = "Prefetch Vector Data Into Caches with Intent to Write and T1 Hint";
                                                d.LastDisassembleData.OpCode = "prefetchwt1";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 0x10:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        description = "move scalar double-fp";
                                        d.OpCodeFlags.L = false; //LIG
                                        d.OpCodeFlags.SkipExtraRegOnMemoryAccess = true;
                                        d.LastDisassembleData.IsFloat64 = true;
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovsd";
                                        else
                                            d.LastDisassembleData.OpCode = "movsd";
                                        d.OpCodeFlags.SkipExtraRegOnMemoryAccess = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        description = "move scalar single-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovss";
                                        else
                                            d.LastDisassembleData.OpCode = "movss";
                                        d.OpCodeFlags.SkipExtraRegOnMemoryAccess = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "move unaligned packed double-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "lmovupd";
                                        else
                                            d.LastDisassembleData.OpCode = "movupd";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "move unaligned four packed single-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovups";
                                        else
                                            d.LastDisassembleData.OpCode = "movups";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x11:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        description = "move scalar double-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovsd";
                                        else
                                            d.LastDisassembleData.OpCode = "movsd";
                                        d.LastDisassembleData.IsFloat64 = true;
                                        d.OpCodeFlags.SkipExtraRegOnMemoryAccess = true;
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Left) + d.Xmm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        description = "move scalar single-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovss";
                                        else
                                            d.LastDisassembleData.OpCode = "movss";
                                        d.OpCodeFlags.SkipExtraRegOnMemoryAccess = true;
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last) + d.Xmm(memory[2]);
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "move unaligned packed double-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "lmovupd";
                                        else
                                            d.LastDisassembleData.OpCode = "movupd";
                                        d.OpCodeFlags.SkipExtraRegOnMemoryAccess = true;
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last) + d.Xmm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "move unaligned four packed single-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovups";
                                        else
                                            d.LastDisassembleData.OpCode = "movups";
                                        d.OpCodeFlags.SkipExtraRegOnMemoryAccess = true;
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last) + d.Xmm(memory[2]);
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            /*0f*/
                            case 0x12:
                                {
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        description = "move one double-fp and duplicate";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovddup";
                                        else
                                            d.LastDisassembleData.OpCode = "movddup";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        description = "move packed single-fp Low and duplicate";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovsldup";
                                        else
                                            d.LastDisassembleData.OpCode = "movsldup";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "move low packed double-precision floating-point value";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovlpd";
                                        else
                                            d.LastDisassembleData.OpCode = "movlpd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "high to low packed single-fp";
                                        if (d.GetMod(memory[2]) == 3)
                                            d.LastDisassembleData.OpCode = "movhlps";
                                        else
                                            d.LastDisassembleData.OpCode = "movlps";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = 'v' + d.LastDisassembleData.OpCode;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x13:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "move low packed double-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovlpd";
                                        else
                                            d.LastDisassembleData.OpCode = "movlpd";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last) + d.Xmm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "move low packed single-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovlps";
                                        else
                                            d.LastDisassembleData.OpCode = "movlps";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last) + d.Xmm(memory[2]);
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            /*0f*/
                            case 0x14:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "unpack low packed single-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vunpcklpd";
                                        else
                                            d.LastDisassembleData.OpCode = "unpcklpd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unpack low packed single-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vunpcklps";
                                        else
                                            d.LastDisassembleData.OpCode = "unpcklps";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x15:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "unpack and interleave high packed double-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vunpckhpd";
                                        else
                                            d.LastDisassembleData.OpCode = "unpckhpd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unpack high packed single-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "unpckhps";
                                        else
                                            d.LastDisassembleData.OpCode = "unpckhps";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x16:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        description = "move packed single-fp high and duplicate";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovshdup";
                                        else
                                            d.LastDisassembleData.OpCode = "movshdup";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "move high packed double-precision floating-point value";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovhpd";
                                        else
                                            d.LastDisassembleData.OpCode = "movhpd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "high to low packed single-fp";
                                        if (d.GetMod(memory[2]) == 3)
                                            d.LastDisassembleData.OpCode = "movlhps";
                                        else
                                            d.LastDisassembleData.OpCode = "movhps";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = 'v' + d.LastDisassembleData.OpCode;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x17:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "move high packed double-precision floating-point value";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovhpd";
                                        else
                                            d.LastDisassembleData.OpCode = "movhpd";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last) + d.Xmm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "high to low packed single-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovhps";
                                        else
                                            d.LastDisassembleData.OpCode = "movhps";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last) + d.Xmm(memory[2]);
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x18:
                                {
                                    switch (d.GetReg(memory[2]))
                                    {
                                        case 0:
                                            {
                                                description = "prefetch";
                                                d.LastDisassembleData.OpCode = "prefetchnta";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 1:
                                            {
                                                description = "prefetch";
                                                d.LastDisassembleData.OpCode = "prefetchto";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 2:
                                            {
                                                description = "prefetch";
                                                d.LastDisassembleData.OpCode = "prefetcht1";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 3:
                                            {
                                                description = "prefetch";
                                                d.LastDisassembleData.OpCode = "prefetcht2";
                                                d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 2, ref last);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 0x1f:
                                {
                                    switch (d.GetReg(memory[2]))
                                    {
                                        case 0:
                                            {
                                                description = "multibyte nop";
                                                d.LastDisassembleData.OpCode = "nop";
                                                if (d.RexW)
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last, 64);
                                                else
                                                {
                                                    if (d.Prefix2.Contains(0x66))
                                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last, 16);
                                                    else
                                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last, 32);
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
                                    d.LastDisassembleData.OpCode = "mov";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last) + d.Cr(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x21:
                                {
                                    description = "move from debug register";
                                    d.LastDisassembleData.OpCode = "mov";
                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 0, ref last) + d.Dr(memory[2]);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x22:
                                {
                                    description = "move to control register";
                                    d.LastDisassembleData.OpCode = "mov";
                                    d.LastDisassembleData.Parameters = d.Cr(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x23:
                                {
                                    description = "move to debug register";
                                    d.LastDisassembleData.OpCode = "mov";
                                    d.LastDisassembleData.Parameters = d.Dr(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;
                            case 0x28:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "move aligned packed double-fp values";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovapd";
                                        else
                                            d.LastDisassembleData.OpCode = "movapd";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "move aligned four packed single-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovaps";
                                        else
                                            d.LastDisassembleData.OpCode = "movaps";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x29:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "move aligned packed double-fp values";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovapd";
                                        else
                                            d.LastDisassembleData.OpCode = "movapd";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last) + d.Xmm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "move aligned four packed single-fp";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovaps";
                                        else
                                            d.LastDisassembleData.OpCode = "movaps";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last) + d.Xmm(memory[2]);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x2a:
                                {
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        description = "convert doubleword integer to scalar doubleprecision floating-point value";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vcvtsi2sd";
                                        else
                                            d.LastDisassembleData.OpCode = "cvtsi2sd";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        description = "scalar signed int32 to single-fp conversion";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vcvtsi2ss";
                                        else
                                            d.LastDisassembleData.OpCode = "cvtsi2ss";
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.Prefix2.Contains(0x66))
                                        {
                                            description = "convert packed dword's to packed dp-fp's";
                                            d.LastDisassembleData.OpCode = "cvtpi2pd";
                                            d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 3, ref last, ATmrPos.Right);
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                        else
                                        {
                                            description = "packed signed int32 to packed single-fp conversion";
                                            d.LastDisassembleData.OpCode = "cvtpi2ps";
                                            d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                }
                                break;
                            case 0x2b:
                                {
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovntpd";
                                        else
                                            d.LastDisassembleData.OpCode = "movntpd";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last) + d.Xmm(memory[2]);
                                        description = "move packed double-precision floating-point using non-temporal hint";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vmovntps";
                                        else
                                            d.LastDisassembleData.OpCode = "movntps";
                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 2, 4, ref last) + d.Xmm(memory[2]);
                                        description = "move aligned four packed single-fp non temporal";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x2c:
                                {
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        description = "convert with truncation scalar double-precision floating point value to signed doubleword integer";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vcvttsd2si";
                                        else
                                            d.LastDisassembleData.OpCode = "cvttsd2si";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        description = "scalar single-fp to signed int32 conversion (truncate)";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vcvttss2si";
                                        else
                                            d.LastDisassembleData.OpCode = "cvttss2si";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.Prefix2.Contains(0x66))
                                        {
                                            description = "packed doubleprecision-fp to packed dword conversion (truncate)";
                                            d.LastDisassembleData.OpCode = "cvttpd2pi";
                                            d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                        else
                                        {
                                            description = "packed single-fp to packed int32 conversion (truncate)";
                                            d.LastDisassembleData.OpCode = "cvttps2pi";
                                            d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                }
                                break;
                            case 0x2d:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0xf2))
                                    {
                                        description = "convert scalar double-precision floating-point value to doubleword integer";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vcvtsd2si";
                                        else
                                            d.LastDisassembleData.OpCode = "cvtsd2si";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (d.Prefix2.Contains(0xf3))
                                    {
                                        description = "scalar single-fp to signed int32 conversion";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vcvtss2si";
                                        else
                                            d.LastDisassembleData.OpCode = "cvtss2si";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.R32(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (d.Prefix2.Contains(0x66))
                                        {
                                            description = "convert 2 packed dp-fp's from param 2 to packed signed dword in param1";
                                            d.LastDisassembleData.OpCode = "cvtpi2ps";
                                            d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                        else
                                        {
                                            description = "packed single-fp to packed int32 conversion";
                                            d.LastDisassembleData.OpCode = "cvtps2pi";
                                            d.LastDisassembleData.Parameters = d.Mm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                            d.LastDisassembleData.DataSize = 4;
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                }
                                break;
                            case 0x2e:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "unordered scalar double-fp compare and set eflags";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vucomisd";
                                        else
                                            d.LastDisassembleData.OpCode = "ucomisd";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unordered scalar single-fp compare and set eflags";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vucomiss";
                                        else
                                            d.LastDisassembleData.OpCode = "ucomiss";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x2f:
                                {
                                    d.LastDisassembleData.IsFloat = true;
                                    if (d.Prefix2.Contains(0x66))
                                    {
                                        description = "compare scalar ordered double-precision floating point values and set eflags";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vcomisd";
                                        else
                                            d.LastDisassembleData.OpCode = "comisd";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "scalar ordered single-fp compare and set eflags";
                                        if (d.HasVex)
                                            d.LastDisassembleData.OpCode = "vcomiss";
                                        else
                                            d.LastDisassembleData.OpCode = "comiss";
                                        d.OpCodeFlags.SkipExtraReg = true;
                                        d.LastDisassembleData.Parameters = d.Xmm(memory[2]) + d.ModRm(memory, d.Prefix2, 2, 4, ref last, ATmrPos.Right);
                                        d.LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;
                            case 0x30:
                                {
                                    description = "write to model specific register";
                                    d.LastDisassembleData.OpCode = "wrmsr";
                                    offset += 1;
                                }
                                break;
                            case 0x31:
                                {
                                    description = "read time-stamp counter";
                                    d.LastDisassembleData.OpCode = "rdtsc";
                                    offset += 1;
                                }
                                break;
                            case 0x32:
                                {
                                    description = "read from model specific register";
                                    d.LastDisassembleData.OpCode = "rdmsr";
                                    offset += 1;
                                }
                                break;
                            case 0x33:
                                {
                                    description = "read performance-monitoring counters";
                                    d.LastDisassembleData.OpCode = "rdpmc";
                                    offset += 1;
                                }
                                break;
                            case 0x34:
                                {
                                    description = "fast transistion to system call entry point";
                                    d.LastDisassembleData.OpCode = "sysenter";
                                    d.LastDisassembleData.IsRet = true;
                                    offset += 1;
                                }
                                break;
                            case 0x35:
                                {
                                    description = "fast transistion from system call entry point";
                                    d.LastDisassembleData.OpCode = "sysexit";
                                    offset += 1;
                                }
                                break;
                            case 0x37:
                                {
                                    description = "Safermode multipurpose function";
                                    d.LastDisassembleData.OpCode = "getsec";
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
                                                d.LastDisassembleData.OpCode = "pshufb";
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = 'v' + d.LastDisassembleData.OpCode;
                                                }
                                                else
                                                    d.LastDisassembleData.Parameters = d.Mm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 0x1:
                                            {
                                                description = "Packed horizontal add";
                                                d.LastDisassembleData.OpCode = "phaddw";
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = 'v' + d.LastDisassembleData.OpCode;
                                                }
                                                else
                                                    d.LastDisassembleData.Parameters = d.Mm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x2:
                                            {
                                                description = "Packed horizontal add";
                                                d.LastDisassembleData.OpCode = "phaddd";
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = 'v' + d.LastDisassembleData.OpCode;
                                                }
                                                else
                                                    d.LastDisassembleData.Parameters = d.Mm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 0x3:
                                            {
                                                description = "Packed horizontal add and saturate";
                                                d.LastDisassembleData.OpCode = "phaddsw";
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = 'v' + d.LastDisassembleData.OpCode;
                                                }
                                                else
                                                    d.LastDisassembleData.Parameters = d.Mm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 0x4:
                                            {
                                                description = "Multiply and add signed and unsigned bytes";
                                                d.LastDisassembleData.OpCode = "pmaddubsw";
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = 'v' + d.LastDisassembleData.OpCode;
                                                }
                                                else
                                                    d.LastDisassembleData.Parameters = d.Mm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x5:
                                            {
                                                description = "Packed horizontal subtract";
                                                d.LastDisassembleData.OpCode = "phsubw";
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = 'v' + d.LastDisassembleData.OpCode;
                                                }
                                                else
                                                    d.LastDisassembleData.Parameters = d.Mm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 0x6:
                                            {
                                                description = "Packed horizontal subtract";
                                                d.LastDisassembleData.OpCode = "phsubd";
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = 'v' + d.LastDisassembleData.OpCode;
                                                }
                                                else
                                                    d.LastDisassembleData.Parameters = d.Mm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 0x7:
                                            {
                                                description = "Packed horizontal subtract";
                                                d.LastDisassembleData.OpCode = "phsubsw";
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = 'v' + d.LastDisassembleData.OpCode;
                                                }
                                                else
                                                    d.LastDisassembleData.Parameters = d.Mm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 0x8:
                                            {
                                                description = "Packed SIGN";
                                                d.LastDisassembleData.OpCode = "psignb";
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = 'v' + d.LastDisassembleData.OpCode;
                                                }
                                                else
                                                    d.LastDisassembleData.Parameters = d.Mm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 0x9:
                                            {
                                                description = "Packed SIGN";
                                                d.LastDisassembleData.OpCode = "psignw";
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = 'v' + d.LastDisassembleData.OpCode;
                                                }
                                                else
                                                    d.LastDisassembleData.Parameters = d.Mm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 0xa:
                                            {
                                                description = "Packed SIGN";
                                                d.LastDisassembleData.OpCode = "psignd";
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = 'v' + d.LastDisassembleData.OpCode;
                                                }
                                                else
                                                    d.LastDisassembleData.Parameters = d.Mm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0xb:
                                            {
                                                description = "Packed multiply high with round and scale";
                                                d.LastDisassembleData.OpCode = "phmulhrsw";
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = 'v' + d.LastDisassembleData.OpCode;
                                                }
                                                else
                                                    d.LastDisassembleData.Parameters = d.Mm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 3, ref last, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0xc:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "permute single-precision floating-point values";
                                                        d.LastDisassembleData.OpCode = "vpermilps";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0xd:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "permute double-precision floating-point values";
                                                        d.LastDisassembleData.OpCode = "vpermilpd";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0xe:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Packed bit test";
                                                        d.LastDisassembleData.OpCode = "vtestps";
                                                        d.OpCodeFlags.SkipExtraReg = true;
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0xf:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Packed bit test";
                                                        d.LastDisassembleData.OpCode = "vtestpd";
                                                        d.OpCodeFlags.SkipExtraReg = true;
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x10:
                                            {
                                                description = "Variable blend packed bytes";
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        d.LastDisassembleData.OpCode = "vpblendvb";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.RegNrToStr(ARegisterType.RtXmm, memory[(int)last]);
                                                        offset += 1;
                                                    }
                                                    else
                                                    {
                                                        d.LastDisassembleData.OpCode = "pblendvb";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',' + d.RegNrToStr(ARegisterType.RtXmm, 0);
                                                    }
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x13:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Convert 16-bit FP values to single-precision FP values";
                                                    d.LastDisassembleData.OpCode = "vcvtph2ps";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0x14:
                                            {
                                                description = "Variable blend packed single precision floating-point values";
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        d.LastDisassembleData.OpCode = "vblendvps";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.RegNrToStr(ARegisterType.RtXmm, memory[(int)last]);
                                                        offset += 1;
                                                    }
                                                    else
                                                    {
                                                        d.LastDisassembleData.OpCode = "blendvps";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',' + d.RegNrToStr(ARegisterType.RtXmm, 0);
                                                    }
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0x15:
                                            {
                                                description = "Variable blend packed double precision floating-point values";
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        d.LastDisassembleData.OpCode = "vblendvpd invalid";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        d.LastDisassembleData.Parameters = d.LastDisassembleData.Parameters + ',' + d.RegNrToStr(ARegisterType.RtXmm, memory[(int)last]);
                                                        offset += 1;
                                                    }
                                                    else
                                                    {
                                                        d.LastDisassembleData.OpCode = "blendvpd";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right) + ',' + d.ColorReg + d.RegNrToStr(ARegisterType.RtXmm, 0) + d.EndColor;
                                                    }
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0x16:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Permute single-precision floating-point elements";
                                                        d.LastDisassembleData.OpCode = "vpermps";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x17:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Logical compare";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vptest";
                                                    else
                                                        d.LastDisassembleData.OpCode = "ptest";
                                                    d.OpCodeFlags.SkipExtraReg = true;
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
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
                                                        description = "Broadcast floating-point-data";
                                                        d.LastDisassembleData.OpCode = "vbroadcastss";
                                                        d.OpCodeFlags.SkipExtraReg = true;
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
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
                                                        description = "Broadcast floating-point-data";
                                                        d.LastDisassembleData.OpCode = "vbroadcastsd";
                                                        d.OpCodeFlags.SkipExtraReg = true;
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x1a:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Broadcast floating-point-data";
                                                        d.LastDisassembleData.OpCode = "vbroadcastf128";
                                                        d.OpCodeFlags.SkipExtraReg = true;
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x1c:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed absolute value";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpabsb";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pabsb";
                                                    d.OpCodeFlags.SkipExtraReg = true;
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    description = "Packed absolute value";
                                                    d.LastDisassembleData.OpCode = "pabsb";
                                                    d.LastDisassembleData.Parameters = d.Mm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 3, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x1d:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed absolute value";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpabsw";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pabsw";
                                                    d.OpCodeFlags.SkipExtraReg = true;
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    description = "Packed absolute value";
                                                    d.LastDisassembleData.OpCode = "pabsw";
                                                    d.LastDisassembleData.Parameters = d.Mm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 3, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x1e:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed absolute value";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpabsd";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pabsd";
                                                    d.OpCodeFlags.SkipExtraReg = true;
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    description = "Packed absolute value";
                                                    d.LastDisassembleData.OpCode = "pabsd";
                                                    d.LastDisassembleData.Parameters = d.Mm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 3, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x20:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with sign extend";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpmovsxbw";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pmovsxbw";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x21:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with sign extend";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpmovsxbd";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pmovsxbd";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x22:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with sign extend";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpmovsxbq";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pmovsxbq";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x23:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with sign extend";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpmovsxwd";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pmovsxwd";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x24:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with sign extend";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpmovsxwq";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pmovsxwq";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x25:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with sign extend";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpmovsxdq";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pmovsxdq";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x28:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Multiple packed signed dword integers";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpmuldq";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pmuldq";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x29:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Compare packed qword data for equal";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpcmpeqq";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pcmpeqq";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x2a:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Load double quadword non-temporal aligned hint";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vmovntdqa";
                                                    else
                                                        d.LastDisassembleData.OpCode = "movntdqa";
                                                    d.OpCodeFlags.SkipExtraReg = true;
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x2b:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Pack with unsigned saturation";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpackusdw";
                                                    else
                                                        d.LastDisassembleData.OpCode = "packusdw";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x2c:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Conditional SIMD packed loads and stores";
                                                        d.LastDisassembleData.OpCode = "vmaskmovps";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x2d:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Conditional SIMD packed loads and stores";
                                                        d.LastDisassembleData.OpCode = "vmaskmovpd";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x2e:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Conditional SIMD packed loads and stores";
                                                        d.LastDisassembleData.OpCode = "vmaskmovps";
                                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Left) + d.Xmm(memory[3]);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x2f:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Conditional SIMD packed loads and stores";
                                                        d.LastDisassembleData.OpCode = "vmaskmovpd";
                                                        d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Left) + d.Xmm(memory[3]);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x30:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with zero extend";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpmovzxbw";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pmovzxbw";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x31:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with zero extend";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpmovzxbd";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pmovzxbd";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x32:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with zero extend";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpmovzxbq";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pmovzxbq";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x33:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with zero extend";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpmovzxwd";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pmovzxwd";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x34:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with zero extend";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpmovzxwq";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pmovzxwq";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x35:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed move with zero extend";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpmovzxdq";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pmovzxdq";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x36:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Full doublewords element permutation";
                                                        d.LastDisassembleData.OpCode = "vpermd";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x37:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Compare packed data for greater than";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpcmpgtq";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pcmpgtq";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x38:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Minimum of packed signed byte integers";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpminsb";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pminsb";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x39:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Minimum of packed dword integers";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpminsd";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pminsd";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x3a:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Minimum of packed word integers";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpminuw";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pminuw";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x3b:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Minimum of packed dword integers";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpminud";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pminud";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x3c:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Maximum of packed signed byte integers";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpmaxsb";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pmaxsb";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x3d:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Maximum of packed signed dword integers";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpmaxsd";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pmaxsd";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x3e:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Maximum of packed word integers";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpmaxuw";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pmaxuw";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x3f:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Maximum of packed unsigned dword integers";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpmaxud";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pmaxud";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x40:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Multiply Packed Signed Dword Integers and Store Low Result";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vpmulld";
                                                    else
                                                        d.LastDisassembleData.OpCode = "pmulld";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x41:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Packed horitontal word minimum";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "phminposuw";
                                                    else
                                                        d.LastDisassembleData.OpCode = "vphminposuw";
                                                    d.OpCodeFlags.SkipExtraReg = true;
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0x45:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Variable Bit Shift Right Logical";
                                                        if (d.RexW)
                                                            d.LastDisassembleData.OpCode = "vpsrlvq";
                                                        else
                                                            d.LastDisassembleData.OpCode = "vpsrlvd";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x46:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Variable bit shift right arithmetic";
                                                        d.LastDisassembleData.OpCode = "vpsravd";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x47:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Variable Bit Shift Left Logical";
                                                        if (d.RexW)
                                                            d.LastDisassembleData.OpCode = "vpsllvq";
                                                        else
                                                            d.LastDisassembleData.OpCode = "vpsllvd";
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x58:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Broadcast integer data";
                                                        d.LastDisassembleData.OpCode = "vpbroadcastd";
                                                        d.OpCodeFlags.SkipExtraReg = true;
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x59:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Broadcast integer data";
                                                        d.LastDisassembleData.OpCode = "vpbroadcastq";
                                                        d.OpCodeFlags.SkipExtraReg = true;
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x5a:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Broadcast integer data";
                                                        d.LastDisassembleData.OpCode = "vpbroadcasti128";
                                                        d.OpCodeFlags.SkipExtraReg = true;
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x78:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Broadcast integer data";
                                                        d.LastDisassembleData.OpCode = "vpbroadcastb";
                                                        d.OpCodeFlags.SkipExtraReg = true;
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x79:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Broadcast integer data";
                                                        d.LastDisassembleData.OpCode = "vpbroadcastw";
                                                        d.OpCodeFlags.SkipExtraReg = true;
                                                        d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x82:
                                            {
                                                description = "Invalidate process-context-identifier";
                                                d.LastDisassembleData.OpCode = "invpcid";
                                                if (d.SymbolHandler.Process.IsX64)
                                                    d.LastDisassembleData.Parameters = d.R64(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 0, ref last, 128, 0, ATmrPos.Right);
                                                else
                                                    d.LastDisassembleData.Parameters = d.R32(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 0, ref last, 128, 0, ATmrPos.Right);
                                                offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                            }
                                            break;
                                        case 0x8c:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Conditional SIMD Integer Packed Loads and Stores";
                                                            d.LastDisassembleData.OpCode = "vpmaskmovq";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Conditional SIMD Integer Packed Loads and Stores";
                                                            d.LastDisassembleData.OpCode = "vpmaskmovd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x8e:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Conditional SIMD Integer Packed Loads and Stores";
                                                            d.LastDisassembleData.OpCode = "vpmaskmovq";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Left) + d.Xmm(memory[3]);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Conditional SIMD Integer Packed Loads and Stores";
                                                            d.LastDisassembleData.OpCode = "vpmaskmovd";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Left) + d.Xmm(memory[3]);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x96:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused multiple-alnterating add/subtract of packed double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmaddsub132pd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-alnterating add/subtract of precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmaddsub132ps";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x97:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused multiple-alnterating subtract/add of packed double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsubadd132pd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-alnterating subtract/add of precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsubadd132ps";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x98:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused multiple-add of packed double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmadd132pd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-add of packed single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmadd132ps";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x99:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused multiple-add of scalar double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmadd132sd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-add of scalar single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmadd132ss";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x9a:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused multiple-subtract of packed double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub132pd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-subtract of packed single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub132ps";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x9b:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused multiple-subtract of scalar double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub132sd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-subtract of scalar single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub132ss";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0x9c:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused negative multiply-add of packed double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfnmadd132pd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-add of packed single-precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfnmadd132ps";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x9d:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused negative multiply-add of scalar double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfnmadd132sd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-add of scalar single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfnmadd132ss";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x9e:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused negative multiply-subtract of packed double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub132pd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-subtract of packed single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub132ps";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0x9f:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused negative multiply-subtract of scalar double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub132sd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused begative multiply-subtract of scalar single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub132ss";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0xa6:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused multiply-alternating add/subtract of packed double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmaddsub213pd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiply-alternating add/subtract of packed single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmaddsub213ps";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0xa7:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused multiply-alternating subtract/add of packed double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsubadd213pd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiply-alternating subtract/add of packed single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsubadd213ps";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0xa8:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused multiple-add of packed double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmadd213pd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-add of packed single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmadd213ps";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0xa9:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused multiple-add of scalar double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmadd213sd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-add of scalar single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmadd213ss";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0xaa:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused multiple-subtract of packed double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub213pd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-subtract of packed single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub213ps";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0xab:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused multiple-subtract of scalar double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub213sd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-subtract of scalar single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub213ss";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0xac:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused negative multiply-add of packed double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfnmadd213pd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-add of packed single-precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfnmadd213ps";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0xad:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused negative multiply-add of scalar double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfnmadd213sd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-add of scalar single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfnmadd213ss";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0xae:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused negative multiply-subtract of packed double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub213pd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-subtract of packed single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub213ps";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0xaf:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused negative multiply-subtract of scalar double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfnmsub213sd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused begative multiply-subtract of scalar single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfnmsub213ss";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0xb6:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused multiply-alternating add/subtract of packed double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmaddsub231pd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiply-alternating add/subtract of packed single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmaddsub231ps";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0xb7:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused multiply-alternating subtract/add of packed double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsubadd231pd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiply-alternating add/subtract of packed single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsubadd231ps";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0xb8:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused multiple-add of packed double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmadd231pd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-add of packed single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmadd231ps";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0xb9:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused multiple-add of scalar double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmadd231sd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-add of scalar single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmadd231ss";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0xba:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused multiple-subtract of packed double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub231pd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-subtract of packed single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub231ps";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0xbb:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused multiple-subtract of scalar double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub231sd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-subtract of scalar single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub231ss";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0xbc:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused negative multiply-add of packed double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfnmadd231pd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-add of packed single-precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfnmadd231ps";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0xbd:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused negative multiply-add of scalar double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfnmadd231sd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-add of scalar single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfnmadd231ss";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0xbe:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused negative multiply-subtract of packed double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub231pd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-subtract of packed single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub231ps";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0xbf:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        if (d.RexW)
                                                        {
                                                            description = "Fused negative multiply-subtract of scalar double precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub231sd";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        else
                                                        {
                                                            description = "Fused begative multiply-subtract of scalar single precision floating-point-values";
                                                            d.LastDisassembleData.OpCode = "vfmsub231ss";
                                                            d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 0xdb:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Perform the AES InvMixColumn transformation";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vaesimc";
                                                    else
                                                        d.LastDisassembleData.OpCode = "aesimc";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0xdc:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Perform one round of an AES encryption flow";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vaesenc";
                                                    else
                                                        d.LastDisassembleData.OpCode = "aesenc";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0xdd:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Perform last round of an AES encryption flow";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "caesenclast";
                                                    else
                                                        d.LastDisassembleData.OpCode = "aesenclast";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0xde:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Perform one round of an AES decryption flow";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "vaesdec";
                                                    else
                                                        d.LastDisassembleData.OpCode = "aesdec";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0xdf:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "Perform last round of an AES decryption flow";
                                                    if (d.HasVex)
                                                        d.LastDisassembleData.OpCode = "caesdeclast";
                                                    else
                                                        d.LastDisassembleData.OpCode = "aesdeclast";
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0xf0:
                                            {
                                                if (d.Prefix2.Contains(0xf2))
                                                {
                                                    description = "Accumulate CRC32 value";
                                                    d.LastDisassembleData.OpCode = "crc32";
                                                    d.LastDisassembleData.Parameters = d.R32(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 2, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    description = "Move data after swapping bytes";
                                                    d.LastDisassembleData.OpCode = "movbe";
                                                    if (d.Prefix2.Contains(0x66))
                                                        d.LastDisassembleData.Parameters = d.R16(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 2, ref last, ATmrPos.Right);
                                                    else
                                                        d.LastDisassembleData.Parameters = d.R32(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0xf1:
                                            {
                                                if (d.Prefix2.Contains(0xf2))
                                                {
                                                    description = "Accumulate CRC32 value";
                                                    d.LastDisassembleData.OpCode = "crc32";
                                                    d.LastDisassembleData.Parameters = d.R32(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    description = "Move data after swapping bytes";
                                                    d.LastDisassembleData.OpCode = "movbe";
                                                    d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Left) + d.R32(memory[3]);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0xf2:
                                            {
                                                if (d.HasVex)
                                                {
                                                    description = "Logical AND NOT";
                                                    d.LastDisassembleData.OpCode = "andn";
                                                    d.LastDisassembleData.Parameters = d.R32(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0xf3:
                                            {
                                                switch (d.GetReg(memory[3]))
                                                {
                                                    case 1:
                                                        {
                                                            description = "Reset lowerst set bit";
                                                            d.LastDisassembleData.OpCode = "blsr";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;
                                                    case 2:
                                                        {
                                                            description = "Get mask up to lowest set bit";
                                                            d.LastDisassembleData.OpCode = "blsmsk";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;
                                                    case 3:
                                                        {
                                                            description = "Extract lowest set isolated bit";
                                                            d.LastDisassembleData.OpCode = "blsi";
                                                            d.LastDisassembleData.Parameters = d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Right);
                                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                        }
                                                        break;
                                                }
                                            }
                                            break;
                                        case 0xf5:
                                            {
                                                if (d.Prefix2.Contains(0xf2))
                                                {
                                                    description = "Parallel bits deposit";
                                                    d.LastDisassembleData.OpCode = "pdep";
                                                    d.LastDisassembleData.Parameters = d.R32(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Left);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    description = "Zero high bits starting with specified bit position";
                                                    d.LastDisassembleData.OpCode = "bzhi";
                                                    d.LastDisassembleData.Parameters = d.R32(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Left);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        case 0xf6:
                                            {
                                                if (d.Prefix2.Contains(0x66))
                                                {
                                                    description = "ADX: Unsigned Integer Addition of Two Operands with Carry Flag";
                                                    d.LastDisassembleData.OpCode = "adcx";
                                                    d.LastDisassembleData.Parameters = d.R32(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                if (d.Prefix2.Contains(0xf3))
                                                {
                                                    description = "ADX: Unsigned Integer Addition of Two Operands with Overflow Flag";
                                                    d.LastDisassembleData.OpCode = "adox";
                                                    d.LastDisassembleData.Parameters = d.R32(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    if (d.HasVex)
                                                    {
                                                        description = "Unsigned multiple without affecting flags";
                                                        d.LastDisassembleData.OpCode = "mulx";
                                                        d.LastDisassembleData.Parameters = d.R32(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;
                                        /*0f*//*38*/
                                        case 0xf7:
                                            {
                                                if (d.HasVex)
                                                {
                                                    if (d.Prefix2.Contains(0xf3))
                                                    {
                                                        description = "Shift arithmetically right without affecting flags";
                                                        d.LastDisassembleData.OpCode = "SARX";
                                                        d.LastDisassembleData.Parameters = d.R32(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                    else
                                                    if (d.Prefix2.Contains(0xf2))
                                                    {
                                                        description = "Shift logically right without affecting flags";
                                                        d.LastDisassembleData.OpCode = "SHRX";
                                                        d.LastDisassembleData.Parameters = d.R32(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                    else
                                                    if (d.Prefix2.Contains(0x66))
                                                    {
                                                        description = "Shift logically left without affecting flags";
                                                        d.LastDisassembleData.OpCode = "SHLX";
                                                        d.LastDisassembleData.Parameters = d.R32(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Right);
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                                else
                                                {
                                                    description = "Bit field extract";
                                                    d.LastDisassembleData.OpCode = "BEXTR";
                                                    d.LastDisassembleData.Parameters = d.R32(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 0, ref last, ATmrPos.Right);
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;
                                        default:
                                            {
                                                if (d.HasVex)
                                                {
                                                    d.LastDisassembleData.OpCode = "unknown avx 0F38 " + AStringUtils.IntToHex(memory[2], 2);
                                                    d.LastDisassembleData.Parameters = d.Xmm(memory[3]) + d.ModRm(memory, d.Prefix2, 3, 4, ref last, ATmrPos.Right);
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
