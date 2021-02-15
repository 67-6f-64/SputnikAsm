using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SputnikAsm.LDisassembler
{
    public class asd
    {
        #region a
        public void a()
        {
            switch (memory[0])
            {  //opcode
                case 0:
                    {


                        if ((aggressivealignment & (((offset) & 0xf) == 0) && (memory[1] != 0)) || ((memory[1] == 0x55) && (memory[2] == 0x89) && (memory[3] == 0xe5)))
                        {
                            description = "Filler";
                            lastdisassembledata.opcode = "db";
                            lastdisassembledata.parameters = inttohex(memory[0], 2);
                        }
                        else
                        {
                            description = "Add";

                            lastdisassembledata.opcode = "add";
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last) + r8(memory[1]);

                            offset += last - 1;
                        }
                    }
                    break;

                case 0x1:
                    {
                        description = "Add";

                        lastdisassembledata.opcode = "add";
                        if (prefix2.has(0x66)) lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last) + r16(memory[1]);
                        else
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + r32(memory[1]);


                        offset += last - 1;
                    }
                    break;

                case 0x2:
                    {
                        description = "Add";

                        lastdisassembledata.opcode = "add";
                        lastdisassembledata.parameters = r8(memory[1]) + modrm(memory, prefix2, 1, 2, last, mright);

                        offset += last - 1;
                    }
                    break;

                case 0x3:
                    {
                        description = "Add";
                        lastdisassembledata.opcode = "add";
                        if (prefix2.has(0x66)) lastdisassembledata.parameters = r16(memory[1]) + modrm(memory, prefix2, 1, 1, last, mright);
                        else
                            lastdisassembledata.parameters = r32(memory[1]) + modrm(memory, prefix2, 1, 0, last, mright);


                        offset += last - 1;
                    }
                    break;



                case 0x4:
                    {
                        description = string("Add ") + inttohex(memory[1], 2) + " to AL";
                        lastdisassembledata.opcode = "add";
                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = memory[1];
                        lastdisassembledata.parameters = colorreg + "al" + endcolor + ',' + inttohexs(memory[1], 2);

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        offset += 1;
                    }
                    break;

                case 0x5:
                    {
                        lastdisassembledata.opcode = "add";
                        lastdisassembledata.parametervaluetype = dvtvalue;


                        wordptr = &memory[1];
                        dwordptr = &memory[1];
                        if (prefix2.has(0x66))
                        {
                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = wordptr;
                            lastdisassembledata.parameters = colorreg + "ax" + endcolor + ',' + inttohexs(wordptr, 4);

                            description = string("add ") + inttohex(wordptr, 4) + " to ax";



                            offset += 2;
                        }
                        else
                        {
                            if (rex_w)
                            {
                                lastdisassembledata.parametervaluetype = dvtvalue;
                                lastdisassembledata.parametervalue = dwordptr;
                                lastdisassembledata.parameters = colorreg + "rax" + endcolor + ',' + inttohexs((longint)(dwordptr), 8);

                                description = string("add ") + inttohex(dwordptr, 8) + " to rax (sign extended)";
                            }
                            else
                            {
                                lastdisassembledata.parametervaluetype = dvtvalue;
                                lastdisassembledata.parametervalue = dwordptr;
                                lastdisassembledata.parameters = colorreg + "eax" + endcolor + ',' + inttohexs(dwordptr, 8);

                                description = string("add ") + inttohex(dwordptr, 8) + " to eax";
                            }
                            offset += 4;
                        }

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;
                    }
                    break;

                case 0x6:
                    {
                        lastdisassembledata.opcode = "push";
                        lastdisassembledata.parameters = colorreg + "es" + endcolor;
                        description = "place es on the stack";
                    }
                    break;

                case 0x7:
                    {
                        lastdisassembledata.opcode = "pop";
                        lastdisassembledata.parameters = colorreg + "es" + endcolor;
                        description = "remove es from the stack";
                    }
                    break;

                case 0x8:
                    {
                        description = "logical inclusive or";
                        lastdisassembledata.opcode = "or";
                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last) + r8(memory[1]);
                        offset += last - 1;
                    }
                    break;

                case 0x9:
                    {
                        description = "logical inclusive or";
                        lastdisassembledata.opcode = "or";
                        if (prefix2.has(0x66)) lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last) + r16(memory[1]);
                        else
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + r32(memory[1]);
                        offset += last - 1;
                    }
                    break;

                case 0xa:
                    {
                        description = "logical inclusive or";
                        lastdisassembledata.opcode = "or";
                        lastdisassembledata.parameters = r8(memory[1]) + modrm(memory, prefix2, 1, 2, last, mright);
                        offset += last - 1;
                    }
                    break;

                case 0xb:
                    {
                        description = "logical inclusive or";
                        lastdisassembledata.opcode = "or";
                        if (prefix2.has(0x66)) lastdisassembledata.parameters = r16(memory[1]) + modrm(memory, prefix2, 1, 1, last, mright);
                        else
                            lastdisassembledata.parameters = r32(memory[1]) + modrm(memory, prefix2, 1, 0, last, mright);

                        offset += last - 1;
                    }
                    break;

                case 0xc:
                    {
                        description = "logical inclusive or";
                        lastdisassembledata.opcode = "or";
                        lastdisassembledata.parameters = colorreg + "al" + endcolor + ',' + inttohexs(memory[1], 2);
                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = memory[1];

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        offset += 1;
                    }
                    break;

                case 0xd:
                    {
                        description = "logical inclusive or";
                        lastdisassembledata.opcode = "or";
                        lastdisassembledata.parametervaluetype = dvtvalue;

                        if (prefix2.has(0x66))
                        {
                            wordptr = &memory[1];

                            lastdisassembledata.parametervalue = wordptr;
                            lastdisassembledata.parameters = colorreg + "ax" + endcolor + ',' + inttohexs(lastdisassembledata.parametervalue, 4);

                            offset += 2;
                        }
                        else
                        {
                            dwordptr = &memory[1];
                            lastdisassembledata.parametervalue = dwordptr;

                            if (rex_w)
                            {
                                lastdisassembledata.parameters = colorreg + "rax" + endcolor + ',' + inttohexs((longint)(lastdisassembledata.parametervalue), 8);
                                description = description + " (sign-extended)";
                            }
                            else
                                lastdisassembledata.parameters = colorreg + "eax" + endcolor + ',' + inttohexs(lastdisassembledata.parametervalue, 8);


                            lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                            lastdisassembledata.seperatorcount += 1;
                            offset += 4;
                        }
                    }
                    break;

                case 0xe:
                    {
                        description = "place cs on the stack";
                        lastdisassembledata.opcode = "push";
                        lastdisassembledata.parameters = colorreg + "cs" + endcolor;
                    }
                    break;

                case 0xf:
                    {  //simd extensions
                        if (prefix2.has(0xf0))
                            lastdisassembledata.prefix = "lock ";
                        else
                            lastdisassembledata.prefix = ""; //these usually treat the f2/f3 prefix differently

                        switch (memory[1])
                        {
                            case 0:
                                {
                                    switch (getreg(memory[2]))
                                    {
                                        case 0:
                                            {
                                                lastdisassembledata.opcode = "sldt";
                                                description = "store local descriptor table register";
                                                if (prefix2.has(0x66)) lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last, 16);
                                                else
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 1:
                                            {
                                                description = "store task register";
                                                lastdisassembledata.opcode = "str";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last, 16);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 2:
                                            {
                                                description = "load local descriptor table register";
                                                lastdisassembledata.opcode = "lldt";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last, 16);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 3:
                                            {
                                                description = "load task register";
                                                lastdisassembledata.opcode = "ltr";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last, 16);
                                                offset += last - 1; ;
                                            }
                                            break;

                                        case 4:
                                            {
                                                description = "verify a segment for reading";
                                                lastdisassembledata.opcode = "verr";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last, 16);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 5:
                                            {
                                                description = "verify a segment for writing";
                                                lastdisassembledata.opcode = "verw";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last, 16);
                                                offset += last - 1;
                                            }
                                            break;
                                        default:
                                            {
                                                lastdisassembledata.opcode = "db";
                                                lastdisassembledata.parameters = inttohex(memory[0], 2);
                                                description = "not specified by the intel documentation";
                                            }

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
                                                lastdisassembledata.opcode = "vmcall";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xc2:
                                            {
                                                description = "launch virtual machine managed by current vmcs";
                                                lastdisassembledata.opcode = "vmlaunch";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xc3:
                                            {
                                                description = "resume virtual machine managed by current vmcs";
                                                lastdisassembledata.opcode = "vmresume";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xc4:
                                            {
                                                description = "leaves vmx operation";
                                                lastdisassembledata.opcode = "vmxoff";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xc8:
                                            {
                                                description = "set up monitor address";
                                                lastdisassembledata.opcode = "monitor";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xc9:
                                            {
                                                description = "Monitor wait";
                                                lastdisassembledata.opcode = "mwait";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xca:
                                            {
                                                description = "Clear AC flag in EFLAGS register";
                                                lastdisassembledata.opcode = "clac";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xd0:
                                            {
                                                description = "Get value of extended control register";
                                                lastdisassembledata.opcode = "xgetbv";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xd1:
                                            {
                                                description = "Set value of extended control register";
                                                lastdisassembledata.opcode = "xsetbv";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xd5:
                                            {
                                                description = "Transactional end";
                                                lastdisassembledata.opcode = "xend";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xd6:
                                            {
                                                description = "Test if in transactional execution";
                                                lastdisassembledata.opcode = "xtest";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xf8:
                                            {
                                                description = "Swap GS base register";
                                                lastdisassembledata.opcode = "swapgs";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xf9:
                                            {
                                                description = "Read time-stamp counter and processor ID";
                                                lastdisassembledata.opcode = "rdtscp";
                                                offset += 2;
                                            }
                                            break;

                                        default:
                                            {
                                                switch (getreg(memory[2]))
                                                {
                                                    case 0:
                                                        {
                                                            description = "store global descriptor table register";
                                                            lastdisassembledata.opcode = "sgdt";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                            offset += last - 1;
                                                        }
                                                        break;

                                                    case 1:
                                                        {
                                                            description = "store interrupt descriptor table register";
                                                            lastdisassembledata.opcode = "sidt";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                            offset += last - 1;
                                                        }
                                                        break;

                                                    case 2:
                                                        {
                                                            description = "load global descriptor table register";
                                                            lastdisassembledata.opcode = "lgdt";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                            offset += last - 1;
                                                        }
                                                        break;

                                                    case 3:
                                                        {
                                                            description = "load interupt descriptor table register";
                                                            lastdisassembledata.opcode = "lidt";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                            offset += last - 1;
                                                        }
                                                        break;

                                                    case 4:
                                                        {
                                                            description = "store machine status word";
                                                            lastdisassembledata.opcode = "smsw";

                                                            if (prefix2.has(0x66)) lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last);
                                                            else lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                            offset += last - 1;
                                                        }
                                                        break;

                                                    case 6:
                                                        {
                                                            description = "load machine status word";
                                                            lastdisassembledata.opcode = "lmsw";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last);
                                                            offset += last - 1;
                                                        }
                                                        break;

                                                    case 7:
                                                        {
                                                            description = "invalidate tlb entry";
                                                            lastdisassembledata.opcode = "invplg";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                            offset += last - 1;
                                                        }
                                                        break;
                                                }
                                            }
                                    }
                                }
                                break;

                            case 0x2:
                                {
                                    description = "load access rights byte";
                                    lastdisassembledata.opcode = "lar";
                                    if (prefix2.has(0x66)) lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 2, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            /*0f*/
                            case 0x3:
                                {
                                    description = "load segment limit";
                                    lastdisassembledata.opcode = "lsl";
                                    if (prefix2.has(0x66)) lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 2, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0x5:
                                {
                                    description = "fast system call";
                                    lastdisassembledata.opcode = "syscall";
                                    offset += 1;
                                }
                                break;

                            case 0x6:
                                {
                                    description = "clear task-switched flag in cr0";
                                    lastdisassembledata.opcode = "clts";
                                    offset += 1;
                                }
                                break;

                            case 0x7:
                                {
                                    description = "return from fast system call";
                                    lastdisassembledata.opcode = "sysret";
                                    offset += 1;
                                }
                                break;

                            case 0x8:
                                {
                                    description = "invalidate internal caches";
                                    lastdisassembledata.opcode = "invd";
                                    offset += 1;
                                }
                                break;

                            case 0x9:
                                {
                                    description = "write back and invalidate cache";
                                    lastdisassembledata.opcode = "wbinvd";
                                    offset += 1;
                                }
                                break;

                            case 0xb:
                                {
                                    description = "undefined instruction(yes, this one really excists..)";
                                    lastdisassembledata.opcode = "ud2";
                                    offset += 1;
                                }
                                break;

                            case 0xd:
                                {
                                    switch (getreg(memory[2]))
                                    {
                                        case 1:
                                            {
                                                description = "Prefetch Data into Caches in Anticipation of a Write";
                                                lastdisassembledata.opcode = "prefetchw";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 2:
                                            {
                                                description = "Prefetch Vector Data Into Caches with Intent to Write and T1 Hint";
                                                lastdisassembledata.opcode = "prefetchwt1";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last);
                                                offset += last - 1;
                                            }
                                            break;
                                    }
                                }
                                break;


                            case 0x10:
                                {
                                    lastdisassembledata.isfloat = true;

                                    if (prefix2.has(0xf2))
                                    {
                                        description = "move scalar double-fp";
                                        opcodeflags.l = false; //LIG
                                        opcodeflags.skipextraregonmemoryaccess = true;
                                        lastdisassembledata.isfloat64 = true;

                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovsd";
                                        else
                                            lastdisassembledata.opcode = "movsd";

                                        opcodeflags.skipextraregonmemoryaccess = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {
                                        description = "move scalar single-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovss";
                                        else
                                            lastdisassembledata.opcode = "movss";

                                        opcodeflags.skipextraregonmemoryaccess = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0x66))
                                    {
                                        description = "move unaligned packed double-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "lmovupd";
                                        else
                                            lastdisassembledata.opcode = "movupd";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "move unaligned four packed single-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovups";
                                        else
                                            lastdisassembledata.opcode = "movups";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x11:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0xf2))
                                    {
                                        description = "move scalar double-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovsd";
                                        else
                                            lastdisassembledata.opcode = "movsd";

                                        lastdisassembledata.isfloat64 = true;

                                        opcodeflags.skipextraregonmemoryaccess = true;
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last, mleft) + xmm(memory[2]);
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {
                                        description = "move scalar single-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovss";
                                        else
                                            lastdisassembledata.opcode = "movss";

                                        opcodeflags.skipextraregonmemoryaccess = true;
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last) + xmm(memory[2]);
                                        lastdisassembledata.datasize = 4;
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0x66))
                                    {
                                        description = "move unaligned packed double-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "lmovupd";
                                        else
                                            lastdisassembledata.opcode = "movupd";

                                        opcodeflags.skipextraregonmemoryaccess = true;
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last) + xmm(memory[2]);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "move unaligned four packed single-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovups";
                                        else
                                            lastdisassembledata.opcode = "movups";

                                        opcodeflags.skipextraregonmemoryaccess = true;
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last) + xmm(memory[2]);
                                        lastdisassembledata.datasize = 4;
                                        offset += last - 1;
                                    }

                                }
                                break;

                            /*0f*/
                            case 0x12:
                                {
                                    if (prefix2.has(0xf2))
                                    {
                                        description = "move one double-fp and duplicate";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovddup";
                                        else
                                            lastdisassembledata.opcode = "movddup";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {
                                        description = "move packed single-fp Low and duplicate";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovsldup";
                                        else
                                            lastdisassembledata.opcode = "movsldup";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0x66))
                                    {
                                        description = "move low packed double-precision floating-point value";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovlpd";
                                        else
                                            lastdisassembledata.opcode = "movlpd";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "high to low packed single-fp";

                                        if (getmod(memory[2]) == 3)
                                            lastdisassembledata.opcode = "movhlps";
                                        else
                                            lastdisassembledata.opcode = "movlps";

                                        if (hasvex)
                                            lastdisassembledata.opcode = string('v') + lastdisassembledata.opcode;

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x13:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0x66))
                                    {
                                        description = "move low packed double-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovlpd";
                                        else
                                            lastdisassembledata.opcode = "movlpd";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last) + xmm(memory[2]);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "move low packed single-fp";

                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovlps";
                                        else
                                            lastdisassembledata.opcode = "movlps";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last) + xmm(memory[2]);
                                        lastdisassembledata.datasize = 4;
                                        offset += last - 1;
                                    }
                                }
                                break;

                            /*0f*/
                            case 0x14:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0x66))
                                    {
                                        description = "unpack low packed single-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vunpcklpd";
                                        else
                                            lastdisassembledata.opcode = "unpcklpd";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "unpack low packed single-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vunpcklps";
                                        else
                                            lastdisassembledata.opcode = "unpcklps";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x15:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0x66))
                                    {
                                        description = "unpack and interleave high packed double-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vunpckhpd";
                                        else
                                            lastdisassembledata.opcode = "unpckhpd";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "unpack high packed single-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "unpckhps";
                                        else
                                            lastdisassembledata.opcode = "unpckhps";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x16:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0xf3))
                                    {
                                        description = "move packed single-fp high and duplicate";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovshdup";
                                        else
                                            lastdisassembledata.opcode = "movshdup";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0x66))
                                    {
                                        description = "move high packed double-precision floating-point value";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovhpd";
                                        else
                                            lastdisassembledata.opcode = "movhpd";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "high to low packed single-fp";

                                        if (getmod(memory[2]) == 3)
                                            lastdisassembledata.opcode = "movlhps";
                                        else
                                            lastdisassembledata.opcode = "movhps";

                                        if (hasvex)
                                            lastdisassembledata.opcode = string('v') + lastdisassembledata.opcode;

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x17:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0x66))
                                    {
                                        description = "move high packed double-precision floating-point value";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovhpd";
                                        else
                                            lastdisassembledata.opcode = "movhpd";

                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last) + xmm(memory[2]);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "high to low packed single-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovhps";
                                        else
                                            lastdisassembledata.opcode = "movhps";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last) + xmm(memory[2]);
                                        lastdisassembledata.datasize = 4;
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x18:
                                {
                                    switch (getreg(memory[2]))
                                    {
                                        case 0:
                                            {
                                                description = "prefetch";
                                                lastdisassembledata.opcode = "prefetchnta";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 1:
                                            {
                                                description = "prefetch";
                                                lastdisassembledata.opcode = "prefetchto";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 2:
                                            {
                                                description = "prefetch";
                                                lastdisassembledata.opcode = "prefetcht1";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 3:
                                            {
                                                description = "prefetch";
                                                lastdisassembledata.opcode = "prefetcht2";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last);
                                                offset += last - 1;
                                            }
                                            break;

                                    }
                                }
                                break;

                            case 0x1f:
                                {
                                    switch (getreg(memory[2]))
                                    {
                                        case 0:
                                            {
                                                description = "multibyte nop";
                                                lastdisassembledata.opcode = "nop";


                                                if (rex_w)
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last, 64);
                                                else
                                                {
                                                    if (prefix2.has(0x66))
                                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last, 16);
                                                    else
                                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last, 32);
                                                }

                                                offset += last - 1;
                                            }
                                            break;
                                    }
                                }
                                break;



                            case 0x20:
                                {
                                    description = "move from control register";
                                    lastdisassembledata.opcode = "mov";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last) + cr(memory[2]);
                                    offset += last - 1;
                                }
                                break;

                            case 0x21:
                                {
                                    description = "move from debug register";
                                    lastdisassembledata.opcode = "mov";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last) + dr(memory[2]);
                                    offset += last - 1;
                                }
                                break;

                            case 0x22:
                                {
                                    description = "move to control register";
                                    lastdisassembledata.opcode = "mov";
                                    lastdisassembledata.parameters = cr(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);
                                    offset += last - 1;
                                }
                                break;

                            case 0x23:
                                {
                                    description = "move to debug register";
                                    lastdisassembledata.opcode = "mov";
                                    lastdisassembledata.parameters = dr(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);
                                    offset += last - 1;
                                }
                                break;

                            case 0x28:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "move aligned packed double-fp values";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovapd";
                                        else
                                            lastdisassembledata.opcode = "movapd";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "move aligned four packed single-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovaps";
                                        else
                                            lastdisassembledata.opcode = "movaps";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x29:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "move aligned packed double-fp values";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovapd";
                                        else
                                            lastdisassembledata.opcode = "movapd";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last) + xmm(memory[2]);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "move aligned four packed single-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovaps";
                                        else
                                            lastdisassembledata.opcode = "movaps";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last) + xmm(memory[2]);
                                        offset += last - 1;
                                    }
                                }
                                break;


                            case 0x2a:
                                {
                                    if (prefix2.has(0xf2))
                                    {

                                        description = "convert doubleword integer to scalar doubleprecision floating-point value";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vcvtsi2sd";
                                        else
                                            lastdisassembledata.opcode = "cvtsi2sd";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {

                                        description = "scalar signed int32 to single-fp conversion";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vcvtsi2ss";
                                        else
                                            lastdisassembledata.opcode = "cvtsi2ss";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (prefix2.has(0x66))
                                        {
                                            description = "convert packed dword's to packed dp-fp's";
                                            lastdisassembledata.opcode = "cvtpi2pd";
                                            lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                            offset += last - 1;
                                        }
                                        else
                                        {
                                            description = "packed signed int32 to packed single-fp conversion";
                                            lastdisassembledata.opcode = "cvtpi2ps";
                                            lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                            offset += last - 1;
                                        }
                                    }
                                }
                                break;

                            case 0x2b:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovntpd";
                                        else
                                            lastdisassembledata.opcode = "movntpd";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last) + xmm(memory[2]);
                                        description = "move packed double-precision floating-point using non-temporal hint";
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovntps";
                                        else
                                            lastdisassembledata.opcode = "movntps";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last) + xmm(memory[2]);
                                        description = "move aligned four packed single-fp non temporal";
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x2c:
                                {
                                    if (prefix2.has(0xf2))
                                    {

                                        description = "convert with truncation scalar double-precision floating point value to signed doubleword integer";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vcvttsd2si";
                                        else
                                            lastdisassembledata.opcode = "cvttsd2si";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {
                                        description = "scalar single-fp to signed int32 conversion (truncate)";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vcvttss2si";
                                        else
                                            lastdisassembledata.opcode = "cvttss2si";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (prefix2.has(0x66))
                                        {
                                            description = "packed doubleprecision-fp to packed dword conversion (truncate)";
                                            lastdisassembledata.opcode = "cvttpd2pi";
                                            lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                            offset += last - 1;
                                        }
                                        else
                                        {
                                            description = "packed single-fp to packed int32 conversion (truncate)";
                                            lastdisassembledata.opcode = "cvttps2pi";
                                            lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                            offset += last - 1;
                                        }
                                    }
                                }
                                break;

                            case 0x2d:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0xf2))
                                    {
                                        description = "convert scalar double-precision floating-point value to doubleword integer";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vcvtsd2si";
                                        else
                                            lastdisassembledata.opcode = "cvtsd2si";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {
                                        description = "scalar single-fp to signed int32 conversion";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vcvtss2si";
                                        else
                                            lastdisassembledata.opcode = "cvtss2si";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (prefix2.has(0x66))
                                        {
                                            description = "convert 2 packed dp-fp's from param 2 to packed signed dword in param1";
                                            lastdisassembledata.opcode = "cvtpi2ps";
                                            lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                            offset += last - 1;
                                        }
                                        else
                                        {
                                            description = "packed single-fp to packed int32 conversion";
                                            lastdisassembledata.opcode = "cvtps2pi";
                                            lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                            lastdisassembledata.datasize = 4;
                                            offset += last - 1;
                                        }
                                    }
                                }
                                break;

                            case 0x2e:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0x66))
                                    {
                                        description = "unordered scalar double-fp compare and set eflags";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vucomisd";
                                        else
                                            lastdisassembledata.opcode = "ucomisd";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "unordered scalar single-fp compare and set eflags";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vucomiss";
                                        else
                                            lastdisassembledata.opcode = "ucomiss";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;
                                        offset += last - 1;
                                    }
                                }
                                break;


                            case 0x2f:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0x66))
                                    {
                                        description = "compare scalar ordered double-precision floating point values and set eflags";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vcomisd";
                                        else
                                            lastdisassembledata.opcode = "comisd";
                                        opcodeflags.skipextrareg = true;

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "scalar ordered single-fp compare and set eflags";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vcomiss";
                                        else
                                            lastdisassembledata.opcode = "comiss";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x30:
                                {
                                    description = "write to model specific register";
                                    lastdisassembledata.opcode = "wrmsr";
                                    offset += 1;
                                }
                                break;

                            case 0x31:
                                {
                                    description = "read time-stamp counter";
                                    lastdisassembledata.opcode = "rdtsc";
                                    offset += 1;
                                }
                                break;

                            case 0x32:
                                {
                                    description = "read from model specific register";
                                    lastdisassembledata.opcode = "rdmsr";
                                    offset += 1;
                                }
                                break;

                            case 0x33:
                                {
                                    description = "read performance-monitoring counters";
                                    lastdisassembledata.opcode = "rdpmc";
                                    offset += 1;
                                }
                                break;

                            case 0x34:
                                {
                                    description = "fast transistion to system call entry point";
                                    lastdisassembledata.opcode = "sysenter";
                                    lastdisassembledata.isret = true;
                                    offset += 1;
                                }
                                break;

                            case 0x35:
                                {
                                    description = "fast transistion from system call entry point";
                                    lastdisassembledata.opcode = "sysexit";
                                    offset += 1;
                                }
                                break;

                            case 0x37:
                                {
                                    description = "Safermode multipurpose function";
                                    lastdisassembledata.opcode = "getsec";
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
                                                lastdisassembledata.opcode = "pshufb";

                                                if (prefix2.has(0x66))
                                                {
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);

                                                    if (hasvex)
                                                        lastdisassembledata.opcode = string('v') + lastdisassembledata.opcode;
                                                }
                                                else
                                                    lastdisassembledata.parameters = mm(memory[3]) + modrm(memory, prefix2, 3, 3, last, mright);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 0x1:
                                            {
                                                description = "Packed horizontal add";
                                                lastdisassembledata.opcode = "phaddw";

                                                if (prefix2.has(0x66))
                                                {
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);

                                                    if (hasvex)
                                                        lastdisassembledata.opcode = string('v') + lastdisassembledata.opcode;
                                                }
                                                else
                                                    lastdisassembledata.parameters = mm(memory[3]) + modrm(memory, prefix2, 3, 3, last, mright);
                                                offset += last - 1;
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x2:
                                            {
                                                description = "Packed horizontal add";
                                                lastdisassembledata.opcode = "phaddd";

                                                if (prefix2.has(0x66))
                                                {
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);

                                                    if (hasvex)
                                                        lastdisassembledata.opcode = string('v') + lastdisassembledata.opcode;
                                                }
                                                else
                                                    lastdisassembledata.parameters = mm(memory[3]) + modrm(memory, prefix2, 3, 3, last, mright);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 0x3:
                                            {
                                                description = "Packed horizontal add and saturate";
                                                lastdisassembledata.opcode = "phaddsw";

                                                if (prefix2.has(0x66))
                                                {
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);

                                                    if (hasvex)
                                                        lastdisassembledata.opcode = string('v') + lastdisassembledata.opcode;
                                                }
                                                else
                                                    lastdisassembledata.parameters = mm(memory[3]) + modrm(memory, prefix2, 3, 3, last, mright);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 0x4:
                                            {
                                                description = "Multiply and add signed and unsigned bytes";
                                                lastdisassembledata.opcode = "pmaddubsw";

                                                if (prefix2.has(0x66))
                                                {
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);

                                                    if (hasvex)
                                                        lastdisassembledata.opcode = string('v') + lastdisassembledata.opcode;
                                                }
                                                else
                                                    lastdisassembledata.parameters = mm(memory[3]) + modrm(memory, prefix2, 3, 3, last, mright);
                                                offset += last - 1;
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x5:
                                            {
                                                description = "Packed horizontal subtract";
                                                lastdisassembledata.opcode = "phsubw";

                                                if (prefix2.has(0x66))
                                                {
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);

                                                    if (hasvex)
                                                        lastdisassembledata.opcode = string('v') + lastdisassembledata.opcode;
                                                }
                                                else
                                                    lastdisassembledata.parameters = mm(memory[3]) + modrm(memory, prefix2, 3, 3, last, mright);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 0x6:
                                            {
                                                description = "Packed horizontal subtract";
                                                lastdisassembledata.opcode = "phsubd";

                                                if (prefix2.has(0x66))
                                                {
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);

                                                    if (hasvex)
                                                        lastdisassembledata.opcode = string('v') + lastdisassembledata.opcode;
                                                }
                                                else
                                                    lastdisassembledata.parameters = mm(memory[3]) + modrm(memory, prefix2, 3, 3, last, mright);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 0x7:
                                            {
                                                description = "Packed horizontal subtract";
                                                lastdisassembledata.opcode = "phsubsw";

                                                if (prefix2.has(0x66))
                                                {
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);

                                                    if (hasvex)
                                                        lastdisassembledata.opcode = string('v') + lastdisassembledata.opcode;
                                                }
                                                else
                                                    lastdisassembledata.parameters = mm(memory[3]) + modrm(memory, prefix2, 3, 3, last, mright);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 0x8:
                                            {
                                                description = "Packed SIGN";
                                                lastdisassembledata.opcode = "psignb";

                                                if (prefix2.has(0x66))
                                                {
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);

                                                    if (hasvex)
                                                        lastdisassembledata.opcode = string('v') + lastdisassembledata.opcode;
                                                }
                                                else
                                                    lastdisassembledata.parameters = mm(memory[3]) + modrm(memory, prefix2, 3, 3, last, mright);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 0x9:
                                            {
                                                description = "Packed SIGN";
                                                lastdisassembledata.opcode = "psignw";

                                                if (prefix2.has(0x66))
                                                {
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);

                                                    if (hasvex)
                                                        lastdisassembledata.opcode = string('v') + lastdisassembledata.opcode;
                                                }
                                                else
                                                    lastdisassembledata.parameters = mm(memory[3]) + modrm(memory, prefix2, 3, 3, last, mright);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 0xa:
                                            {
                                                description = "Packed SIGN";
                                                lastdisassembledata.opcode = "psignd";

                                                if (prefix2.has(0x66))
                                                {
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);

                                                    if (hasvex)
                                                        lastdisassembledata.opcode = string('v') + lastdisassembledata.opcode;
                                                }
                                                else
                                                    lastdisassembledata.parameters = mm(memory[3]) + modrm(memory, prefix2, 3, 3, last, mright);
                                                offset += last - 1;
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xb:
                                            {
                                                description = "Packed multiply high with round and scale";
                                                lastdisassembledata.opcode = "phmulhrsw";

                                                if (prefix2.has(0x66))
                                                {
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);

                                                    if (hasvex)
                                                        lastdisassembledata.opcode = string('v') + lastdisassembledata.opcode;
                                                }
                                                else
                                                    lastdisassembledata.parameters = mm(memory[3]) + modrm(memory, prefix2, 3, 3, last, mright);
                                                offset += last - 1;
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xc:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "permute single-precision floating-point values";
                                                        lastdisassembledata.opcode = "vpermilps";
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);

                                                        offset += last - 1;
                                                    }
                                                }

                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xd:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "permute double-precision floating-point values";
                                                        lastdisassembledata.opcode = "vpermilpd";
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);

                                                        offset += last - 1;
                                                    }
                                                }

                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xe:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Packed bit test";
                                                        lastdisassembledata.opcode = "vtestps";
                                                        opcodeflags.skipextrareg = true;
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);

                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xf:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Packed bit test";
                                                        lastdisassembledata.opcode = "vtestpd";
                                                        opcodeflags.skipextrareg = true;
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);

                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x10:
                                            {
                                                description = "Variable blend packed bytes";
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        lastdisassembledata.opcode = "vpblendvb";
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                        lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + regnrtostr(rtxmm, memory[last]);
                                                        offset += 1;
                                                    }
                                                    else
                                                    {
                                                        lastdisassembledata.opcode = "pblendvb";
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',' + regnrtostr(rtxmm, 0);
                                                    }
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x13:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Convert 16-bit FP values to single-precision FP values";
                                                    lastdisassembledata.opcode = "vcvtph2ps";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;


                                        case 0x14:
                                            {
                                                description = "Variable blend packed single precision floating-point values";
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        lastdisassembledata.opcode = "vblendvps";
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                        lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + regnrtostr(rtxmm, memory[last]);
                                                        offset += 1;
                                                    }
                                                    else
                                                    {
                                                        lastdisassembledata.opcode = "blendvps";
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',' + regnrtostr(rtxmm, 0);
                                                    }
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        case 0x15:
                                            {
                                                description = "Variable blend packed double precision floating-point values";
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        lastdisassembledata.opcode = "vblendvpd invalid";
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                        lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + regnrtostr(rtxmm, memory[last]);
                                                        offset += 1;
                                                    }
                                                    else
                                                    {
                                                        lastdisassembledata.opcode = "blendvpd";
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',' + colorreg + regnrtostr(rtxmm, 0) + endcolor;
                                                    }
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        case 0x16:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Permute single-precision floating-point elements";
                                                        lastdisassembledata.opcode = "vpermps";
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x17:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Logical compare";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vptest";
                                                    else
                                                        lastdisassembledata.opcode = "ptest";

                                                    opcodeflags.skipextrareg = true;
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        case 0x18:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Broadcast floating-point-data";
                                                        lastdisassembledata.opcode = "vbroadcastss";
                                                        opcodeflags.skipextrareg = true;
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x19:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Broadcast floating-point-data";
                                                        lastdisassembledata.opcode = "vbroadcastsd";
                                                        opcodeflags.skipextrareg = true;
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x1a:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Broadcast floating-point-data";
                                                        lastdisassembledata.opcode = "vbroadcastf128";
                                                        opcodeflags.skipextrareg = true;
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x1c:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed absolute value";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpabsb";
                                                    else
                                                        lastdisassembledata.opcode = "pabsb";

                                                    opcodeflags.skipextrareg = true;
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                                else
                                                {
                                                    description = "Packed absolute value";
                                                    lastdisassembledata.opcode = "pabsb";
                                                    lastdisassembledata.parameters = mm(memory[3]) + modrm(memory, prefix2, 3, 3, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x1d:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed absolute value";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpabsw";
                                                    else
                                                        lastdisassembledata.opcode = "pabsw";

                                                    opcodeflags.skipextrareg = true;
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                                else
                                                {
                                                    description = "Packed absolute value";
                                                    lastdisassembledata.opcode = "pabsw";
                                                    lastdisassembledata.parameters = mm(memory[3]) + modrm(memory, prefix2, 3, 3, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x1e:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed absolute value";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpabsd";
                                                    else
                                                        lastdisassembledata.opcode = "pabsd";

                                                    opcodeflags.skipextrareg = true;
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                                else
                                                {
                                                    description = "Packed absolute value";
                                                    lastdisassembledata.opcode = "pabsd";
                                                    lastdisassembledata.parameters = mm(memory[3]) + modrm(memory, prefix2, 3, 3, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x20:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed move with sign extend";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpmovsxbw";
                                                    else
                                                        lastdisassembledata.opcode = "pmovsxbw";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x21:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed move with sign extend";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpmovsxbd";
                                                    else
                                                        lastdisassembledata.opcode = "pmovsxbd";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x22:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed move with sign extend";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpmovsxbq";
                                                    else
                                                        lastdisassembledata.opcode = "pmovsxbq";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x23:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed move with sign extend";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpmovsxwd";
                                                    else
                                                        lastdisassembledata.opcode = "pmovsxwd";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x24:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed move with sign extend";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpmovsxwq";
                                                    else
                                                        lastdisassembledata.opcode = "pmovsxwq";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x25:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed move with sign extend";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpmovsxdq";
                                                    else
                                                        lastdisassembledata.opcode = "pmovsxdq";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x28:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Multiple packed signed dword integers";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpmuldq";
                                                    else
                                                        lastdisassembledata.opcode = "pmuldq";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x29:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Compare packed qword data for equal";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpcmpeqq";
                                                    else
                                                        lastdisassembledata.opcode = "pcmpeqq";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x2a:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Load double quadword non-temporal aligned hint";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vmovntdqa";
                                                    else
                                                        lastdisassembledata.opcode = "movntdqa";

                                                    opcodeflags.skipextrareg = true;
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x2b:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Pack with unsigned saturation";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpackusdw";
                                                    else
                                                        lastdisassembledata.opcode = "packusdw";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x2c:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Conditional SIMD packed loads and stores";
                                                        lastdisassembledata.opcode = "vmaskmovps";
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x2d:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Conditional SIMD packed loads and stores";
                                                        lastdisassembledata.opcode = "vmaskmovpd";
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x2e:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Conditional SIMD packed loads and stores";
                                                        lastdisassembledata.opcode = "vmaskmovps";
                                                        lastdisassembledata.parameters = modrm(memory, prefix2, 3, 4, last, mleft) + xmm(memory[3]);
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x2f:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Conditional SIMD packed loads and stores";
                                                        lastdisassembledata.opcode = "vmaskmovpd";
                                                        lastdisassembledata.parameters = modrm(memory, prefix2, 3, 4, last, mleft) + xmm(memory[3]);
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x30:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed move with zero extend";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpmovzxbw";
                                                    else
                                                        lastdisassembledata.opcode = "pmovzxbw";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x31:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed move with zero extend";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpmovzxbd";
                                                    else
                                                        lastdisassembledata.opcode = "pmovzxbd";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x32:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed move with zero extend";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpmovzxbq";
                                                    else
                                                        lastdisassembledata.opcode = "pmovzxbq";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x33:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed move with zero extend";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpmovzxwd";
                                                    else
                                                        lastdisassembledata.opcode = "pmovzxwd";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x34:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed move with zero extend";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpmovzxwq";
                                                    else
                                                        lastdisassembledata.opcode = "pmovzxwq";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x35:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed move with zero extend";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpmovzxdq";
                                                    else
                                                        lastdisassembledata.opcode = "pmovzxdq";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x36:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Full doublewords element permutation";
                                                        lastdisassembledata.opcode = "vpermd";
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;


                                        /*0f*//*38*/
                                        case 0x37:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Compare packed data for greater than";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpcmpgtq";
                                                    else
                                                        lastdisassembledata.opcode = "pcmpgtq";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x38:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Minimum of packed signed byte integers";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpminsb";
                                                    else
                                                        lastdisassembledata.opcode = "pminsb";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x39:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Minimum of packed dword integers";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpminsd";
                                                    else
                                                        lastdisassembledata.opcode = "pminsd";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x3a:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Minimum of packed word integers";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpminuw";
                                                    else
                                                        lastdisassembledata.opcode = "pminuw";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x3b:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Minimum of packed dword integers";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpminud";
                                                    else
                                                        lastdisassembledata.opcode = "pminud";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x3c:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Maximum of packed signed byte integers";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpmaxsb";
                                                    else
                                                        lastdisassembledata.opcode = "pmaxsb";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x3d:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Maximum of packed signed dword integers";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpmaxsd";
                                                    else
                                                        lastdisassembledata.opcode = "pmaxsd";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x3e:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Maximum of packed word integers";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpmaxuw";
                                                    else
                                                        lastdisassembledata.opcode = "pmaxuw";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x3f:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Maximum of packed unsigned dword integers";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpmaxud";
                                                    else
                                                        lastdisassembledata.opcode = "pmaxud";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x40:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Multiply Packed Signed Dword Integers and Store Low Result";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpmulld";
                                                    else
                                                        lastdisassembledata.opcode = "pmulld";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x41:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed horitontal word minimum";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "phminposuw";
                                                    else
                                                        lastdisassembledata.opcode = "vphminposuw";

                                                    opcodeflags.skipextrareg = true;
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        case 0x45:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Variable Bit Shift Right Logical";

                                                        if (rex_w)
                                                            lastdisassembledata.opcode = "vpsrlvq";
                                                        else
                                                            lastdisassembledata.opcode = "vpsrlvd";

                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x46:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Variable bit shift right arithmetic";
                                                        lastdisassembledata.opcode = "vpsravd";
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x47:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Variable Bit Shift Left Logical";

                                                        if (rex_w)
                                                            lastdisassembledata.opcode = "vpsllvq";
                                                        else
                                                            lastdisassembledata.opcode = "vpsllvd";

                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;


                                        /*0f*//*38*/
                                        case 0x58:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Broadcast integer data";
                                                        lastdisassembledata.opcode = "vpbroadcastd";
                                                        opcodeflags.skipextrareg = true;
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x59:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Broadcast integer data";
                                                        lastdisassembledata.opcode = "vpbroadcastq";
                                                        opcodeflags.skipextrareg = true;
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x5a:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Broadcast integer data";
                                                        lastdisassembledata.opcode = "vpbroadcasti128";
                                                        opcodeflags.skipextrareg = true;
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x78:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Broadcast integer data";
                                                        lastdisassembledata.opcode = "vpbroadcastb";
                                                        opcodeflags.skipextrareg = true;
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x79:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Broadcast integer data";
                                                        lastdisassembledata.opcode = "vpbroadcastw";
                                                        opcodeflags.skipextrareg = true;
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x82:
                                            {
                                                description = "Invalidate process-context-identifier";
                                                lastdisassembledata.opcode = "invpcid";
                                                if (processhandler.is64bit)
                                                    lastdisassembledata.parameters = r64(memory[3]) + modrm(memory, prefix2, 3, 0, last, 128, 0, mright);
                                                else
                                                    lastdisassembledata.parameters = r32(memory[3]) + modrm(memory, prefix2, 3, 0, last, 128, 0, mright);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 0x8c:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Conditional SIMD Integer Packed Loads and Stores";
                                                            lastdisassembledata.opcode = "vpmaskmovq";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Conditional SIMD Integer Packed Loads and Stores";
                                                            lastdisassembledata.opcode = "vpmaskmovd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x8e:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Conditional SIMD Integer Packed Loads and Stores";
                                                            lastdisassembledata.opcode = "vpmaskmovq";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 3, 4, last, mleft) + xmm(memory[3]);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Conditional SIMD Integer Packed Loads and Stores";
                                                            lastdisassembledata.opcode = "vpmaskmovd";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 3, 4, last, mleft) + xmm(memory[3]);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;


                                        case 0x96:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused multiple-alnterating add/subtract of packed double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmaddsub132pd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-alnterating add/subtract of precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmaddsub132ps";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x97:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused multiple-alnterating subtract/add of packed double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsubadd132pd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-alnterating subtract/add of precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsubadd132ps";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x98:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused multiple-add of packed double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmadd132pd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-add of packed single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmadd132ps";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x99:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused multiple-add of scalar double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmadd132sd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-add of scalar single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmadd132ss";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x9a:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused multiple-subtract of packed double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub132pd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-subtract of packed single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub132ps";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x9b:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused multiple-subtract of scalar double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub132sd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-subtract of scalar single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub132ss";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0x9c:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused negative multiply-add of packed double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfnmadd132pd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-add of packed single-precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfnmadd132ps";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x9d:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused negative multiply-add of scalar double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfnmadd132sd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-add of scalar single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfnmadd132ss";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x9e:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused negative multiply-subtract of packed double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub132pd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-subtract of packed single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub132ps";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x9f:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused negative multiply-subtract of scalar double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub132sd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused begative multiply-subtract of scalar single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub132ss";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xa6:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused multiply-alternating add/subtract of packed double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmaddsub213pd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiply-alternating add/subtract of packed single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmaddsub213ps";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xa7:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused multiply-alternating subtract/add of packed double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsubadd213pd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiply-alternating subtract/add of packed single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsubadd213ps";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xa8:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused multiple-add of packed double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmadd213pd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-add of packed single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmadd213ps";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xa9:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused multiple-add of scalar double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmadd213sd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-add of scalar single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmadd213ss";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xaa:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused multiple-subtract of packed double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub213pd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-subtract of packed single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub213ps";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;


                                        case 0xab:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused multiple-subtract of scalar double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub213sd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-subtract of scalar single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub213ss";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xac:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused negative multiply-add of packed double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfnmadd213pd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-add of packed single-precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfnmadd213ps";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xad:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused negative multiply-add of scalar double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfnmadd213sd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-add of scalar single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfnmadd213ss";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xae:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused negative multiply-subtract of packed double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub213pd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-subtract of packed single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub213ps";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xaf:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused negative multiply-subtract of scalar double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfnmsub213sd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused begative multiply-subtract of scalar single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfnmsub213ss";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xb6:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused multiply-alternating add/subtract of packed double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmaddsub231pd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiply-alternating add/subtract of packed single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmaddsub231ps";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xb7:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused multiply-alternating subtract/add of packed double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsubadd231pd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiply-alternating add/subtract of packed single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsubadd231ps";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xb8:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused multiple-add of packed double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmadd231pd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-add of packed single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmadd231ps";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xb9:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused multiple-add of scalar double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmadd231sd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-add of scalar single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmadd231ss";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xba:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused multiple-subtract of packed double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub231pd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-subtract of packed single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub231ps";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xbb:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused multiple-subtract of scalar double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub231sd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused multiple-subtract of scalar single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub231ss";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xbc:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused negative multiply-add of packed double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfnmadd231pd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-add of packed single-precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfnmadd231ps";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xbd:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused negative multiply-add of scalar double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfnmadd231sd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-add of scalar single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfnmadd231ss";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;


                                        case 0xbe:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused negative multiply-subtract of packed double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub231pd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused negative multiply-subtract of packed single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub231ps";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 0xbf:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        if (rex_w)
                                                        {
                                                            description = "Fused negative multiply-subtract of scalar double precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub231sd";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "Fused begative multiply-subtract of scalar single precision floating-point-values";
                                                            lastdisassembledata.opcode = "vfmsub231ss";
                                                            lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            break;


                                        case 0xdb:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Perform the AES InvMixColumn transformation";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vaesimc";
                                                    else
                                                        lastdisassembledata.opcode = "aesimc";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        case 0xdc:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Perform one round of an AES encryption flow";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vaesenc";
                                                    else
                                                        lastdisassembledata.opcode = "aesenc";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        case 0xdd:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Perform last round of an AES encryption flow";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "caesenclast";
                                                    else
                                                        lastdisassembledata.opcode = "aesenclast";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xde:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Perform one round of an AES decryption flow";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vaesdec";
                                                    else
                                                        lastdisassembledata.opcode = "aesdec";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        case 0xdf:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Perform last round of an AES decryption flow";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "caesdeclast";
                                                    else
                                                        lastdisassembledata.opcode = "aesdeclast";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xf0:
                                            {
                                                if (prefix2.has(0xf2))
                                                {
                                                    description = "Accumulate CRC32 value";
                                                    lastdisassembledata.opcode = "crc32";
                                                    lastdisassembledata.parameters = r32(memory[3]) + modrm(memory, prefix2, 3, 2, last, mright);
                                                    offset += last - 1;
                                                }
                                                else
                                                {
                                                    description = "Move data after swapping bytes";
                                                    lastdisassembledata.opcode = "movbe";
                                                    if (prefix2.has(0x66))
                                                        lastdisassembledata.parameters = r16(memory[3]) + modrm(memory, prefix2, 3, 2, last, mright);
                                                    else
                                                        lastdisassembledata.parameters = r32(memory[3]) + modrm(memory, prefix2, 3, 0, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        case 0xf1:
                                            {
                                                if (prefix2.has(0xf2))
                                                {
                                                    description = "Accumulate CRC32 value";
                                                    lastdisassembledata.opcode = "crc32";
                                                    lastdisassembledata.parameters = r32(memory[3]) + modrm(memory, prefix2, 3, 0, last, mright);
                                                    offset += last - 1;
                                                }
                                                else
                                                {
                                                    description = "Move data after swapping bytes";
                                                    lastdisassembledata.opcode = "movbe";
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 3, 0, last, mleft) + r32(memory[3]);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        case 0xf2:
                                            {
                                                if (hasvex)
                                                {
                                                    description = "Logical AND NOT";
                                                    lastdisassembledata.opcode = "andn";
                                                    lastdisassembledata.parameters = r32(memory[3]) + modrm(memory, prefix2, 3, 0, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xf3:
                                            {
                                                switch (getreg(memory[3]))
                                                {
                                                    case 1:
                                                        {
                                                            description = "Reset lowerst set bit";
                                                            lastdisassembledata.opcode = "blsr";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 3, 0, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        break;

                                                    case 2:
                                                        {
                                                            description = "Get mask up to lowest set bit";
                                                            lastdisassembledata.opcode = "blsmsk";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 3, 0, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        break;

                                                    case 3:
                                                        {
                                                            description = "Extract lowest set isolated bit";
                                                            lastdisassembledata.opcode = "blsi";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 3, 0, last, mright);
                                                            offset += last - 1;
                                                        }
                                                        break;
                                                }
                                            }
                                            break;

                                        case 0xf5:
                                            {
                                                if (prefix2.has(0xf2))
                                                {
                                                    description = "Parallel bits deposit";
                                                    lastdisassembledata.opcode = "pdep";
                                                    lastdisassembledata.parameters = r32(memory[3]) + modrm(memory, prefix2, 3, 0, last, mleft);
                                                    offset += last - 1;
                                                }
                                                else
                                                {
                                                    description = "Zero high bits starting with specified bit position";
                                                    lastdisassembledata.opcode = "bzhi";
                                                    lastdisassembledata.parameters = r32(memory[3]) + modrm(memory, prefix2, 3, 0, last, mleft);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;


                                        case 0xf6:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "ADX: Unsigned Integer Addition of Two Operands with Carry Flag";
                                                    lastdisassembledata.opcode = "adcx";
                                                    lastdisassembledata.parameters = r32(memory[3]) + modrm(memory, prefix2, 3, 0, last, mright);
                                                    offset += last - 1;
                                                }
                                                else
                                                if (prefix2.has(0xf3))
                                                {
                                                    description = "ADX: Unsigned Integer Addition of Two Operands with Overflow Flag";
                                                    lastdisassembledata.opcode = "adox";
                                                    lastdisassembledata.parameters = r32(memory[3]) + modrm(memory, prefix2, 3, 0, last, mright);
                                                    offset += last - 1;
                                                }
                                                else
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Unsigned multiple without affecting flags";
                                                        lastdisassembledata.opcode = "mulx";
                                                        lastdisassembledata.parameters = r32(memory[3]) + modrm(memory, prefix2, 3, 0, last, mright);
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*38*/
                                        case 0xf7:
                                            {
                                                if (hasvex)
                                                {
                                                    if (prefix2.has(0xf3))
                                                    {
                                                        description = "Shift arithmetically right without affecting flags";
                                                        lastdisassembledata.opcode = "SARX";
                                                        lastdisassembledata.parameters = r32(memory[3]) + modrm(memory, prefix2, 3, 0, last, mright);
                                                        offset += last - 1;
                                                    }
                                                    else
                                                    if (prefix2.has(0xf2))
                                                    {
                                                        description = "Shift logically right without affecting flags";
                                                        lastdisassembledata.opcode = "SHRX";
                                                        lastdisassembledata.parameters = r32(memory[3]) + modrm(memory, prefix2, 3, 0, last, mright);
                                                        offset += last - 1;
                                                    }
                                                    else
                                                    if (prefix2.has(0x66))
                                                    {
                                                        description = "Shift logically left without affecting flags";
                                                        lastdisassembledata.opcode = "SHLX";
                                                        lastdisassembledata.parameters = r32(memory[3]) + modrm(memory, prefix2, 3, 0, last, mright);
                                                        offset += last - 1;
                                                    }
                                                }
                                                else
                                                {
                                                    description = "Bit field extract";
                                                    lastdisassembledata.opcode = "BEXTR";
                                                    lastdisassembledata.parameters = r32(memory[3]) + modrm(memory, prefix2, 3, 0, last, mright);
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        default:
                                            {
                                                if (hasvex)
                                                {
                                                    lastdisassembledata.opcode = string("unknown avx 0F38 ") + inttohex(memory[2], 2);
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);

                                                    offset += last - 1;
                                                }
                                            }
                                    }
                                }
                                break;

                            case 0x3a:
                                {
                                    switch (memory[2])
                                    {
                                        case 0:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Qwords element permutation";
                                                        lastdisassembledata.opcode = "vpermq";

                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                        last += 1;
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x1:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Permute double-precision floating-point elements";
                                                        lastdisassembledata.opcode = "vpermpd";

                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                        last += 1;
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x2:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Blend packed dwords";
                                                        lastdisassembledata.opcode = "vblenddd";

                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                        last += 1;
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x4:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Permute single-prevision floating-point values";
                                                        lastdisassembledata.opcode = "vpermilps";

                                                        opcodeflags.skipextrareg = true;
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                        last += 1;
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;


                                        case 0x5:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Permute double-prevision floating-point values";
                                                        lastdisassembledata.opcode = "vpermilpd";

                                                        opcodeflags.skipextrareg = true;
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                        last += 1;
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x6:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Permute floating-point values";
                                                        lastdisassembledata.opcode = "vperm2f128";

                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                        last += 1;
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;


                                        /*0f*//*3a*/
                                        case 0x8:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Round scalar single precision floating-point values";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vroundps";
                                                    else
                                                        lastdisassembledata.opcode = "roundps";

                                                    opcodeflags.skipextrareg = true;
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        case 0x9:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Round packed double precision floating-point values";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vroundpd";
                                                    else
                                                        lastdisassembledata.opcode = "roundpd";

                                                    opcodeflags.skipextrareg = true;

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        case 0xa:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Round scalar single precision floating-point values";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vroundss";
                                                    else
                                                        lastdisassembledata.opcode = "roundss";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        case 0xb:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Round packed single precision floating-point values";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vroundsd";
                                                    else
                                                        lastdisassembledata.opcode = "roundsd";



                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0xc:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Blend packed single precision floating-point values";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vblendps";
                                                    else
                                                        lastdisassembledata.opcode = "blendps";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0xd:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Blend packed double precision floating-point values";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vblendpd";
                                                    else
                                                        lastdisassembledata.opcode = "blendpd";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0xe:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Blend packed words";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpblendw";
                                                    else
                                                        lastdisassembledata.opcode = "pblendw";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0xf:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed align right";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpalignr";
                                                    else
                                                        lastdisassembledata.opcode = "palignr";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                                else
                                                {
                                                    lastdisassembledata.opcode = "palignr";
                                                    lastdisassembledata.parameters = mm(memory[3]) + modrm(memory, prefix2, 3, 3, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x14:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Extract byte";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpextrb";
                                                    else
                                                        lastdisassembledata.opcode = "pextrb";

                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 3, 2, last, mleft) + xmm(memory[3]) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x15:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Extract word";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpextrw";
                                                    else
                                                        lastdisassembledata.opcode = "pextrw";

                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 3, 1, last, mleft) + xmm(memory[3]) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x16:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (rex_w)
                                                    {
                                                        description = "Extract qword";
                                                        lastdisassembledata.opcode = "pextrq";
                                                    }
                                                    else
                                                    {
                                                        description = "Extract dword";
                                                        lastdisassembledata.opcode = "pextrd";
                                                    }

                                                    if (hasvex) lastdisassembledata.opcode = string('v') + lastdisassembledata.opcode;

                                                    opcodeflags.skipextrareg = true;
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 3, 2, last, mleft) + xmm(memory[3]) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        case 0x17:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Extract packed single precision floating-point value";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vextractps";
                                                    else
                                                        lastdisassembledata.opcode = "extractps";

                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 3, 4, last, mleft) + xmm(memory[3]) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;


                                        case 0x18:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Insert packed floating-point values";
                                                        lastdisassembledata.opcode = "vinsertf128";
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                        last += 1;
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x19:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Extract packed floating-point values";
                                                        lastdisassembledata.opcode = "vextractf128";
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                        last += 1;
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;


                                        /*0f*//*3a*/
                                        case 0x1d:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Convert single-precision FP value to 16-bit FP value";
                                                        lastdisassembledata.opcode = "vcvtps2ph";
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                        last += 1;
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x20:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Insert Byte";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpinsrb";
                                                    else
                                                        lastdisassembledata.opcode = "pinsrb";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 0, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x21:
                                            {    //C4 E3 79 21 80 B8 00 00 00 20
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Insert Scalar Single-Precision Floating-Point Value";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vinsertps";
                                                    else
                                                        lastdisassembledata.opcode = "insertps";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        case 0x22:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (rex_w)
                                                    {
                                                        description = "Insert qword";
                                                        lastdisassembledata.opcode = "pinsrq";
                                                    }
                                                    else
                                                    {
                                                        description = "Insert dword";
                                                        lastdisassembledata.opcode = "pinsrd";
                                                    }

                                                    if (hasvex)
                                                        lastdisassembledata.opcode = string('v') + lastdisassembledata.opcode;

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 0, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        case 0x38:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Insert packed integer values";
                                                        lastdisassembledata.opcode = "vinserti128";
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                        last += 1;
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x39:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Extract packed integer values";
                                                        lastdisassembledata.opcode = "vextracti128";
                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                        last += 1;
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x40:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Dot product of packed single precision floating-point values";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vdpps";
                                                    else
                                                        lastdisassembledata.opcode = "dpps";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        case 0x41:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Dot product of packed double precision floating-point values";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vdppd";
                                                    else
                                                        lastdisassembledata.opcode = "dppd";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x42:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Compute multiple packed sums of absolute difference";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vmpsadbw";
                                                    else
                                                        lastdisassembledata.opcode = "mpsadbw";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x44:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Carry-less multiplication quadword";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpclmulqdq";
                                                    else
                                                        lastdisassembledata.opcode = "pclmulqdq";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x46:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Permute integer values";
                                                        lastdisassembledata.opcode = "vperm2i128";

                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                        last += 1;
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x4a:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Variable Blend Packed Single Precision Floating-Point Values";
                                                        lastdisassembledata.opcode = "vblendvps";

                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                        if (opcodeflags.l)
                                                            lastdisassembledata.parameters = lastdisassembledata.parameters + colorreg + regnrtostr(rtymm, ((cardinal)memory[last] >> 4) & 0xf) + endcolor;
                                                        else
                                                            lastdisassembledata.parameters = lastdisassembledata.parameters + colorreg + regnrtostr(rtxmm, ((cardinal)memory[last] >> 4) & 0xf) + endcolor;

                                                        last += 1;
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x4b:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Variable Blend Packed Double Precision Floating-Point Values";
                                                        lastdisassembledata.opcode = "vblendvpd";

                                                        lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                        if (opcodeflags.l)
                                                            lastdisassembledata.parameters = lastdisassembledata.parameters + colorreg + regnrtostr(rtymm, ((cardinal)memory[last] >> 4) & 0xf) + endcolor;
                                                        else
                                                            lastdisassembledata.parameters = lastdisassembledata.parameters + colorreg + regnrtostr(rtxmm, ((cardinal)memory[last] >> 4) & 0xf) + endcolor;

                                                        last += 1;
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;


                                        /*0f*//*3a*/
                                        case 0x60:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed compare explicit length string, return mask";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpcmpestrm";
                                                    else
                                                        lastdisassembledata.opcode = "pcmpestrm";

                                                    opcodeflags.skipextrareg = true;
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x61:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed compare explicit length string, return index";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpcmpestri";
                                                    else
                                                        lastdisassembledata.opcode = "pcmpestri";

                                                    opcodeflags.skipextrareg = true;
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x62:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed compare implicit length string, return mask";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpcmpistrm";
                                                    else
                                                        lastdisassembledata.opcode = "pcmpistrm";

                                                    opcodeflags.skipextrareg = true;
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x63:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "Packed compare implicit length string, return index";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpcmpistri";
                                                    else
                                                        lastdisassembledata.opcode = "pcmpistri";

                                                    opcodeflags.skipextrareg = true;
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        case 0xdf:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "AES round key generation assist";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vaeskeygenassist";
                                                    else
                                                        lastdisassembledata.opcode = "aeskeygenassist";

                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright) + ',';
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                            break;

                                        case 0xf0:
                                            {
                                                if (prefix2.has(0xf2))
                                                {
                                                    if (hasvex)
                                                    {
                                                        description = "Rotate right logical without affecting flags";
                                                        lastdisassembledata.opcode = "rorx";
                                                        opcodeflags.skipextrareg = true;
                                                        lastdisassembledata.parameters = r32(memory[3]) + modrm(memory, prefix2, 3, 0, last, mright) + ',';
                                                        lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohex(memory[last], 2);
                                                        last += 1;
                                                        offset += last - 1;
                                                    }
                                                }
                                            }
                                            break;



                                        default:
                                            {
                                                if (hasvex)
                                                {
                                                    lastdisassembledata.opcode = string("unknown avx 0F3A ") + inttohex(memory[2], 2);
                                                    lastdisassembledata.parameters = xmm(memory[3]) + modrm(memory, prefix2, 3, 4, last, mright);
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohex(memory[last], 2);
                                                    last += 1;
                                                    offset += last - 1;
                                                }
                                            }
                                    }
                                }
                                break;


                            case 0x40:
                                {
                                    description = "move if overflow";
                                    lastdisassembledata.opcode = "cmovo";
                                    if (prefix2.has(0x66)) lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);
                                    offset += last - 1;
                                }
                                break;

                            case 0x41:
                                {
                                    description = "move if not overflow";
                                    lastdisassembledata.opcode = "cmovno";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0x42:
                                {
                                    description = "move if below/ move if carry";
                                    lastdisassembledata.opcode = "cmovb";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0x43:
                                {
                                    description = "move if above or equal/ move if not carry";
                                    lastdisassembledata.opcode = "cmovae";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0x44:
                                {
                                    description = "move if equal/move if zero";
                                    lastdisassembledata.opcode = "cmove";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0x45:
                                {
                                    description = "move if not equal/move if not zero";
                                    lastdisassembledata.opcode = "cmovne";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0x46:
                                {
                                    description = "move if below or equal";
                                    lastdisassembledata.opcode = "cmovbe";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;


                            case 0x47:
                                {
                                    description = "move if above";
                                    lastdisassembledata.opcode = "cmova";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0x48:
                                {
                                    description = "move if sign";
                                    lastdisassembledata.opcode = "cmovs";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0x49:
                                {
                                    description = "move if not sign";
                                    lastdisassembledata.opcode = "cmovns";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0x4a:
                                {
                                    description = "move if parity even";
                                    lastdisassembledata.opcode = "cmovpe";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0x4b:
                                {
                                    description = "move if not parity/move if parity odd";
                                    lastdisassembledata.opcode = "cmovnp";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0x4c:
                                {
                                    description = "move if less";
                                    lastdisassembledata.opcode = "cmovl";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0x4d:
                                {
                                    description = "move if greater or equal";
                                    lastdisassembledata.opcode = "cmovge";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0x4e:
                                {
                                    description = "move if less or equal";
                                    lastdisassembledata.opcode = "cmovle";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);


                                    offset += last - 1;
                                }
                                break;

                            case 0x4f:
                                {
                                    description = "move if greater";
                                    lastdisassembledata.opcode = "cmovg";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0x50:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0x66))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovmskpd";
                                        else
                                            lastdisassembledata.opcode = "movmskpd";

                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        description = "extract packed double-precision floating-point sign mask";
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovmskps";
                                        else
                                            lastdisassembledata.opcode = "movmskps";

                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;

                                        description = "move mask to integer";
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x51:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0xf2))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vsqrtsd";
                                        else
                                            lastdisassembledata.opcode = "sqrtsd";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        description = "scalar double-fp square root";
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vsqrtss";
                                        else
                                            lastdisassembledata.opcode = "sqrtss";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        description = "scalar single-fp square root";
                                        lastdisassembledata.datasize = 4;
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0x66))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vsqrtpd";
                                        else
                                            lastdisassembledata.opcode = "sqrtpd";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        description = "packed double-fp square root";

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vsqrtps";
                                        else
                                            lastdisassembledata.opcode = "sqrtps";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        description = "packed single-fp square root";

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x52:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0xf3))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vrsqrtss";
                                        else
                                            lastdisassembledata.opcode = "rsqrtss";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "packed single-fp square root reciprocal";
                                        lastdisassembledata.datasize = 4;
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vrsqrtps";
                                        else
                                            lastdisassembledata.opcode = "rsqrtps";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "scalar single-fp square root reciprocal";
                                        lastdisassembledata.datasize = 4;
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x53:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0xf3))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vrcpss";
                                        else
                                            lastdisassembledata.opcode = "rcpss";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "Compute Reciprocal of Scalar Single-Precision Floating-Point Values";
                                        lastdisassembledata.datasize = 4;
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vrcpps";
                                        else
                                            lastdisassembledata.opcode = "rcpps";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "Compute Reciprocals of Packed Single-Precision Floating-Point Values";
                                        lastdisassembledata.datasize = 4;
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x54:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vandpd";
                                        else
                                            lastdisassembledata.opcode = "andpd";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "bit-wise logical and of xmm2/m128 and xmm1";
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vandps";
                                        else
                                            lastdisassembledata.opcode = "andps";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;

                                        description = "bit-wise logical and for single fp";
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x55:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "bit-wise logical and not of packed double-precision fp values";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vandnpd";
                                        else
                                            lastdisassembledata.opcode = "andnpd";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "bit-wise logical and not for single-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vandnps";
                                        else
                                            lastdisassembledata.opcode = "andnps";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x56:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "bit-wise logical or of double-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vorpd";
                                        else
                                            lastdisassembledata.opcode = "orpd";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);


                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "bit-wise logical or for single-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vorps";
                                        else
                                            lastdisassembledata.opcode = "orps";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x57:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "bit-wise logical xor for double-fp data";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vxorpd";
                                        else
                                            lastdisassembledata.opcode = "xorpd";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);


                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "bit-wise logical xor for single-fp data";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vxorps";
                                        else
                                            lastdisassembledata.opcode = "xorps";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x58:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0xf2))
                                    {
                                        //delete the repne from the tempresult
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vaddsd";
                                        else
                                            lastdisassembledata.opcode = "addsd";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "add the lower sp fp number from xmm2/mem to xmm1.";
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {
                                        //delete the repe from the tempresult

                                        if (hasvex)
                                            lastdisassembledata.opcode = "vaddss";
                                        else
                                            lastdisassembledata.opcode = "addss";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;

                                        description = "add the lower sp fp number from xmm2/mem to xmm1.";
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (prefix2.has(0x66))
                                        {
                                            if (hasvex)
                                                lastdisassembledata.opcode = "vaddpd";
                                            else
                                                lastdisassembledata.opcode = "addpd";

                                            lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                            description = "add packed double-precision floating-point values from xmm2/mem to xmm1";
                                            offset += last - 1;
                                        }
                                        else
                                        {
                                            if (hasvex)
                                                lastdisassembledata.opcode = "vaddps";
                                            else
                                                lastdisassembledata.opcode = "addps";

                                            lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                            lastdisassembledata.datasize = 4;

                                            description = "add packed sp fp numbers from xmm2/mem to xmm1";
                                            offset += last - 1;
                                        }
                                    }
                                }
                                break;

                            case 0x59:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0xf2))
                                    {

                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmulsd";
                                        else
                                            lastdisassembledata.opcode = "mulsd";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "scalar double-fp multiply";
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {

                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmulss";
                                        else
                                            lastdisassembledata.opcode = "mulss";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;

                                        description = "scalar single-fp multiply";
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0x66))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmulpd";
                                        else
                                            lastdisassembledata.opcode = "mulpd";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "packed double-fp multiply";
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmulps";
                                        else
                                            lastdisassembledata.opcode = "mulps";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;

                                        description = "packed single-fp multiply";
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x5a:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0xf2))
                                    {

                                        if (hasvex)
                                            lastdisassembledata.opcode = "vcvtsd2ss";
                                        else
                                            lastdisassembledata.opcode = "cvtsd2ss";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "convert scalar double-precision floating-point value to scalar single-precision floating-point value";
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {

                                        if (hasvex)
                                            lastdisassembledata.opcode = "vcvtss2sd";
                                        else
                                            lastdisassembledata.opcode = "cvtss2sd";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;

                                        description = "convert scalar single-precision floating-point value to scalar double-precision floating-point value";
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (prefix2.has(0x66))
                                        {
                                            if (hasvex)
                                                lastdisassembledata.opcode = "vcvtpd2ps";
                                            else
                                                lastdisassembledata.opcode = "cvtpd2ps";
                                            opcodeflags.skipextrareg = true;

                                            lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                            description = "convert packed double precision fp values to packed single precision fp values";
                                            offset += last - 1;
                                        }
                                        else
                                        {
                                            if (hasvex)
                                                lastdisassembledata.opcode = "vcvtps2pd";
                                            else
                                                lastdisassembledata.opcode = "cvtps2pd";

                                            opcodeflags.skipextrareg = true;
                                            lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                            lastdisassembledata.datasize = 4;

                                            description = "convert packed single precision fp values to packed double precision fp values";
                                            offset += last - 1;
                                        }
                                    }
                                }
                                break;

                            case 0x5b:
                                {

                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.isfloat = true;
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vcvtps2dq";
                                        else
                                            lastdisassembledata.opcode = "cvtps2dq";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;

                                        description = "convert ps-precision fpoint values to packed dword's ";
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vcvttps2dq";
                                        else
                                            lastdisassembledata.opcode = "cvttps2dq";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "Convert with Truncation Packed Single-Precision FP Values to Packed Dword Integers";
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vcvtdq2ps";
                                        else
                                            lastdisassembledata.opcode = "cvtdq2ps";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "convert packed dword's to ps-precision fpoint values";
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x5c:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0xf2))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vsubsd";
                                        else
                                            lastdisassembledata.opcode = "subsd";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "scalar double-fp subtract";
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vsubss";
                                        else
                                            lastdisassembledata.opcode = "subss";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;

                                        description = "scalar single-fp subtract";
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0x66))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vsubpd";
                                        else
                                            lastdisassembledata.opcode = "subpd";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "packed double-fp subtract";
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vsubps";
                                        else
                                            lastdisassembledata.opcode = "subps";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4; //4*4 actually

                                        description = "packed single-fp subtract";
                                        offset += last - 1;
                                    }
                                }
                                break;


                            case 0x5d:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0xf2))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vminsd";
                                        else
                                            lastdisassembledata.opcode = "minsd";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "scalar single-fp minimum";
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vminss";
                                        else
                                            lastdisassembledata.opcode = "minss";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;

                                        description = "scalar single-fp minimum";
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (prefix2.has(0x66))
                                        {
                                            if (hasvex)
                                                lastdisassembledata.opcode = "vminpd";
                                            else
                                                lastdisassembledata.opcode = "minpd";

                                            lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                            description = "packed double-fp minimum";
                                            offset += last - 1;
                                        }
                                        else
                                        {
                                            if (hasvex)
                                                lastdisassembledata.opcode = "vminps";
                                            else
                                                lastdisassembledata.opcode = "minps";

                                            lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                            description = "packed single-fp minimum";
                                            offset += last - 1;
                                        }
                                    }
                                }
                                break;

                            case 0x5e:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0xf2))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "divsd";
                                        else
                                            lastdisassembledata.opcode = "divsd";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "scalar double-precision-fp divide";
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {

                                        if (hasvex)
                                            lastdisassembledata.opcode = "vdivss";
                                        else
                                            lastdisassembledata.opcode = "divss";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;

                                        description = "scalar single-fp divide";
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (prefix2.has(0x66))
                                        {
                                            if (hasvex)
                                                lastdisassembledata.opcode = "vdivpd";
                                            else
                                                lastdisassembledata.opcode = "divpd";

                                            lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                            description = "packed double-precision fp divide";
                                            offset += last - 1;
                                        }
                                        else
                                        {
                                            if (hasvex)
                                                lastdisassembledata.opcode = "vdivps";
                                            else
                                                lastdisassembledata.opcode = "divps";
                                            lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                            lastdisassembledata.datasize = 4;

                                            description = "packed single-fp divide";
                                            offset += last - 1;
                                        }
                                    }
                                }
                                break;

                            case 0x5f:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0xf2))
                                    {

                                        description = "scalar double-fp maximum";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmaxsd";
                                        else
                                            lastdisassembledata.opcode = "maxsd";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {

                                        description = "scalar single-fp maximum";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmaxss";
                                        else
                                            lastdisassembledata.opcode = "maxss";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.datasize = 4;

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (prefix2.has(0x66))
                                        {
                                            description = "packed double-fp maximum";
                                            if (hasvex)
                                                lastdisassembledata.opcode = "vmaxpd";
                                            else
                                                lastdisassembledata.opcode = "maxpd";
                                            lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                            offset += last - 1;
                                        }
                                        else
                                        {
                                            description = "packed single-fp maximum";
                                            if (hasvex)
                                                lastdisassembledata.opcode = "vmaxps";
                                            else
                                                lastdisassembledata.opcode = "maxps";

                                            lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                            lastdisassembledata.datasize = 4;

                                            offset += last - 1;
                                        }
                                    }
                                }
                                break;

                            case 0x60:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "unpack low packed data";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpunpcklbw";
                                        else
                                            lastdisassembledata.opcode = "punpcklbw";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "unpack low packed data";
                                        lastdisassembledata.opcode = "punpcklbw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x61:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "unpack low packed data";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "punpcklwd";
                                        else
                                            lastdisassembledata.opcode = "punpcklwd";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "unpack low packed data";
                                        lastdisassembledata.opcode = "punpcklwd";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x62:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "unpack low packed data";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpunpckldq";
                                        else
                                            lastdisassembledata.opcode = "punpckldq";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "unpack low packed data";
                                        lastdisassembledata.opcode = "punpckldq";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x63:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "pack with signed saturation";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "packsswb";
                                        else
                                            lastdisassembledata.opcode = "vpacksswb";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "pack with signed saturation";
                                        lastdisassembledata.opcode = "packsswb";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x64:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed compare for greater than";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpcmpgtb";
                                        else
                                            lastdisassembledata.opcode = "pcmpgtb";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed compare for greater than";
                                        lastdisassembledata.opcode = "pcmpgtb";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x65:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed compare for greater than";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpcmpgtw";
                                        else
                                            lastdisassembledata.opcode = "pcmpgtw";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed compare for greater than";
                                        lastdisassembledata.opcode = "pcmpgtw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x66:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed compare for greater than";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpcmpgtd";
                                        else
                                            lastdisassembledata.opcode = "pcmpgtd";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed compare for greater than";
                                        lastdisassembledata.opcode = "pcmpgtd";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;


                            case 0x67:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "pack with unsigned saturation";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpackuswb";
                                        else
                                            lastdisassembledata.opcode = "packuswb";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "pack with unsigned saturation";
                                        lastdisassembledata.opcode = "packuswb";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x68:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "unpack high packed data";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpunpckhbw";
                                        else
                                            lastdisassembledata.opcode = "punpckhbw";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "unpack high packed data";
                                        lastdisassembledata.opcode = "punpckhbw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x69:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "unpack high packed data";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpunpckhwd";
                                        else
                                            lastdisassembledata.opcode = "punpckhwd";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "unpack high packed data";
                                        lastdisassembledata.opcode = "punpckhwd";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x6a:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "unpack high packed data";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpunpckhdq";
                                        else
                                            lastdisassembledata.opcode = "punpckhdq";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "unpack high packed data";
                                        lastdisassembledata.opcode = "punpckhdq";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x6b:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "pack with signed saturation";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "packssdw";
                                        else
                                            lastdisassembledata.opcode = "packssdw";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "pack with signed saturation";
                                        lastdisassembledata.opcode = "packssdw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x6c:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "unpack low packed data";
                                        lastdisassembledata.opcode = "punpcklqdq";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x6d:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "unpack high packed data";
                                        lastdisassembledata.opcode = "punpckhqdq";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;


                            case 0x6e:
                                {
                                    //lastdisassembledata.isfloat:=true; //not sure
                                    if (rex_w)
                                    {
                                        description = "move quadword";
                                        lastdisassembledata.opcode = "movq";
                                    }
                                    else
                                    {
                                        description = "move doubleword";
                                        lastdisassembledata.opcode = "movd";
                                    }

                                    if (hasvex)
                                        lastdisassembledata.opcode = string('v') + lastdisassembledata.opcode;

                                    opcodeflags.skipextrareg = true;
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);
                                    else
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0x6f:
                                {
                                    if (prefix2.has(0xf3))
                                    {

                                        description = "move unaligned double quadword";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovdqu";
                                        else
                                            lastdisassembledata.opcode = "movdqu";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0x66))
                                    {
                                        description = "move aligned double quadword";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovdqa";
                                        else
                                            lastdisassembledata.opcode = "movdqa";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "move 64 bits";
                                        lastdisassembledata.opcode = "movq";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 4, last);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x70:
                                {
                                    if (prefix2.has(0xf2))
                                    {

                                        description = "shuffle packed low words";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpshuflw";
                                        else
                                            lastdisassembledata.opcode = "pshuflw";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(lastdisassembledata.parametervalue, 2);
                                        offset += last;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {

                                        description = "shuffle packed high words";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpshufhw";
                                        else
                                            lastdisassembledata.opcode = "pshufhw";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(lastdisassembledata.parametervalue, 2);
                                        offset += last;
                                    }
                                    else
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed shuffle doubleword";
                                        lastdisassembledata.opcode = "pshufd";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(lastdisassembledata.parametervalue, 2);
                                        offset += last;
                                    }
                                    else
                                    {
                                        description = "packed shuffle word";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpshufw";
                                        else
                                            lastdisassembledata.opcode = "pshufw";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(lastdisassembledata.parametervalue, 2);
                                        offset += last;
                                    }
                                }
                                break;

                            case 0x71:
                                {
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[3];
                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 3;
                                    lastdisassembledata.seperatorcount += 1;


                                    switch (getreg(memory[2]))
                                    {
                                        case 2:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "packed shift right logical";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpsrlw";
                                                    else
                                                        lastdisassembledata.opcode = "psrlw";

                                                    lastdisassembledata.parameters = xmm(memory[2]) + ',' + inttohexs(memory[3], 2);
                                                    offset += 3;
                                                }
                                                else
                                                {
                                                    description = "packed shift right logical";
                                                    lastdisassembledata.opcode = "psrlw";
                                                    lastdisassembledata.parameters = mm(memory[2]) + ',' + inttohexs(memory[3], 2);
                                                    offset += 3;
                                                }
                                            }
                                            break;

                                        case 4:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "shift packed data right arithmetic";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpsraw";
                                                    else
                                                        lastdisassembledata.opcode = "psraw";

                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last);
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);

                                                    offset += last - 1 + 1;
                                                }
                                                else
                                                {
                                                    description = "packed shift left logical";
                                                    lastdisassembledata.opcode = "psraw";
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 3, last);
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);

                                                    offset += last - 1 + 1;
                                                }
                                            }
                                            break;

                                        case 6:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "packed shift left logical";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpsllw";
                                                    else
                                                        lastdisassembledata.opcode = "psllw";

                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last);
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);

                                                    offset += last - 1 + 1;
                                                }
                                                else
                                                {
                                                    description = "packed shift left logical";
                                                    lastdisassembledata.opcode = "psllw";
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 3, last);
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);

                                                    offset += last - 1 + 1;
                                                }
                                            }
                                            break;
                                    }
                                }
                                break;

                            case 0x72:
                                {
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[3];
                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 3;
                                    lastdisassembledata.seperatorcount += 1;

                                    switch (getreg(memory[2]))
                                    {
                                        case 2:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "packed shift right logical";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpsrld";
                                                    else
                                                        lastdisassembledata.opcode = "psrld";
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last);
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                                    offset += 3;
                                                }
                                                else
                                                {
                                                    description = "packed shift right logical";
                                                    lastdisassembledata.opcode = "psrld";
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 3, last);
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                                    offset += 3;
                                                }
                                            }
                                            break;

                                        case 4:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "packed shift right arithmetic";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpsrad";
                                                    else
                                                        lastdisassembledata.opcode = "psrad";
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last);
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);

                                                    offset += 3;
                                                }
                                                else
                                                {
                                                    description = "packed shift right arithmetic";
                                                    lastdisassembledata.opcode = "psrad";
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 3, last);
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);

                                                    offset += 3;
                                                }
                                            }
                                            break;

                                        case 6:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "packed shift left logical";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "pslld";
                                                    else
                                                        lastdisassembledata.opcode = "pslld";

                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last);
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohexs(memory[last], 2);
                                                    offset += 3;
                                                }
                                                else
                                                {
                                                    description = "packed shift left logical";
                                                    lastdisassembledata.opcode = "pslld";
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 3, last);
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                                    offset += 3;
                                                }
                                            }
                                            break;
                                    }
                                }
                                break;

                            case 0x73:
                                {
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[3];
                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 3;
                                    lastdisassembledata.seperatorcount += 1;

                                    switch (getreg(memory[2]))
                                    {
                                        case 2:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "packed shift right logical";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpsrlq";
                                                    else
                                                        lastdisassembledata.opcode = "psrlq";
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last, mright);
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohexs(memory[last], 2);
                                                    offset += 3;
                                                }
                                                else
                                                {
                                                    description = "packed shift right logical";
                                                    lastdisassembledata.opcode = "psrlq";
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 3, last, mright);
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohexs(memory[last], 2);
                                                    offset += 3;
                                                }
                                                Delete(lastdisassembledata.parameters, 1, 1);
                                            }
                                            break;

                                        case 3:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "shift double quadword right logical";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpsrldq";
                                                    else
                                                        lastdisassembledata.opcode = "psrldq";

                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last, mright);
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohexs(memory[last], 2);
                                                    offset += 3;
                                                }
                                                Delete(lastdisassembledata.parameters, 1, 1);
                                            }
                                            break;

                                        case 6:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "packed shift left logical";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpsllq";
                                                    else
                                                        lastdisassembledata.opcode = "psllq";
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last, mright);
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohexs(memory[last], 2);
                                                    offset += 3;
                                                }
                                                else
                                                {
                                                    description = "packed shift left logical";
                                                    lastdisassembledata.opcode = "psllq";
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 3, last, mright);
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohexs(memory[last], 2);
                                                    offset += 3;
                                                }
                                                Delete(lastdisassembledata.parameters, 1, 1);
                                            }
                                            break;

                                        case 7:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    description = "shift double quadword left logical";
                                                    if (hasvex)
                                                        lastdisassembledata.opcode = "vpslldq";
                                                    else
                                                        lastdisassembledata.opcode = "pslldq";
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last, mright);
                                                    lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohexs(memory[last], 2);

                                                    Delete(lastdisassembledata.parameters, 1, 1);
                                                    offset += 3;
                                                }
                                            }
                                            break;
                                    }
                                }
                                break;



                            case 0x74:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed compare for equal";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpcmpeqb";
                                        else
                                            lastdisassembledata.opcode = "pcmpeqb";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed compare for equal";
                                        lastdisassembledata.opcode = "pcmpeqb";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x75:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed compare for equal";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpcmpeqw";
                                        else
                                            lastdisassembledata.opcode = "pcmpeqw";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed compare for equal";
                                        lastdisassembledata.opcode = "pcmpeqw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x76:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed compare for equal";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpcmpeqd";
                                        else
                                            lastdisassembledata.opcode = "pcmpeqd";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed compare for equal";
                                        lastdisassembledata.opcode = "pcmpeqd";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;


                            case 0x77:
                                {
                                    if (hasvex)
                                    {
                                        if (opcodeflags.l)
                                        {
                                            description = "Zero all YMM registers";
                                            lastdisassembledata.opcode = "vzeroall";
                                            offset += 1;
                                        }
                                        else
                                        {
                                            description = "Zero upper bits of YMM registers";
                                            lastdisassembledata.opcode = "vzeroupper";
                                            offset += 1;
                                        }
                                    }
                                    else
                                    {
                                        description = "empty mmx™ state";
                                        lastdisassembledata.opcode = "emms";
                                        offset += 1;
                                    }
                                }
                                break;

                            case 0x78:
                                {
                                    description = "reads a specified vmcs field (32 bits)";
                                    lastdisassembledata.opcode = "vmread";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last) + r32(memory[2]);
                                    offset += last - 1;
                                }
                                break;

                            case 0x79:
                                {
                                    description = "writes a specified vmcs field (32 bits)";
                                    lastdisassembledata.opcode = "vmwrite";
                                    lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0x7c:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vhaddpd";
                                        else
                                            lastdisassembledata.opcode = "haddpd";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "packed double-fp horizontal add";
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf2))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vhaddps";
                                        else
                                            lastdisassembledata.opcode = "haddps";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "packed single-fp horizontal add";
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x7d:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vhsubpd";
                                        else
                                            lastdisassembledata.opcode = "hsubpd";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "packed double-fp horizontal subtract";
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf2))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vhsubps";
                                        else
                                            lastdisassembledata.opcode = "hsubps";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        description = "packed single-fp horizontal subtract";
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x7e:
                                {

                                    if (prefix2.has(0xf3))
                                    {

                                        description = "move quadword";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovq";
                                        else
                                            lastdisassembledata.opcode = "movq";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0x66))
                                    {
                                        if (rex_w)
                                        {
                                            description = "move 64 bits";
                                            lastdisassembledata.opcode = "movq";
                                        }
                                        else
                                        {
                                            description = "move 32 bits";
                                            lastdisassembledata.opcode = "movd";
                                        }

                                        if (hasvex)
                                            lastdisassembledata.opcode = string('v') + lastdisassembledata.opcode;

                                        opcodeflags.skipextrareg = true;

                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last) + xmm(memory[2]);  //r32/rm32,xmm
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (rex_w)
                                        {
                                            description = "move 64 bits";
                                            lastdisassembledata.opcode = "movq";
                                        }
                                        else
                                        {
                                            description = "move 32 bits";
                                            lastdisassembledata.opcode = "movd";
                                        }


                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last) + mm(memory[2]); //r32/rm32,mm
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x7f:
                                {
                                    if (prefix2.has(0xf3))
                                    {

                                        description = "move unaligned double quadword";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovdqu";
                                        else
                                            lastdisassembledata.opcode = "movdqu";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last) + xmm(memory[2]);
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0x66))
                                    {
                                        description = "move aligned double quadword";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovdqa";
                                        else
                                            lastdisassembledata.opcode = "movdqa";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last) + xmm(memory[2]);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "move 64 bits";
                                        lastdisassembledata.opcode = "movq";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 3, last) + mm(memory[2]);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0x80:
                                {
                                    description = "jump near if overflow (OF=1)";
                                    lastdisassembledata.opcode = "jo";
                                    lastdisassembledata.isjump = true;
                                    lastdisassembledata.isconditionaljump = true;
                                    if (context != nil)
                                        lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_of) != 0;

                                    offset += 1 + 4;
                                    if (markiprelativeinstructions)
                                    {
                                        lastdisassembledata.riprelative = 2;
                                        riprelative = true;
                                    }

                                    lastdisassembledata.parametervaluetype = dvtaddress;
                                    if (is64bit)
                                        lastdisassembledata.parametervalue = qword(offset + qword(pint(&memory[2])));
                                    else
                                        lastdisassembledata.parametervalue = dword(offset + qword(pint(&memory[2])));

                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 2;
                                    lastdisassembledata.seperatorcount += 1;



                                    lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);
                                }
                                break;

                            case 0x81:
                                {
                                    description = "jump near if not overflow (OF=0)";
                                    lastdisassembledata.opcode = "jno";
                                    lastdisassembledata.isjump = true;
                                    lastdisassembledata.isconditionaljump = true;
                                    if (context != nil)
                                        lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_of) == 0;

                                    offset += 1 + 4;
                                    if (markiprelativeinstructions)
                                    {
                                        lastdisassembledata.riprelative = 2;
                                        riprelative = true;
                                    }

                                    lastdisassembledata.parametervaluetype = dvtaddress;
                                    if (is64bit)
                                        lastdisassembledata.parametervalue = qword(offset + qword(pint(&memory[2])));
                                    else
                                        lastdisassembledata.parametervalue = dword(offset + qword(pint(&memory[2])));

                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 2;
                                    lastdisassembledata.seperatorcount += 1;



                                    lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                                }
                                break;

                            case 0x82:
                                {
                                    description = "jump near if below/carry (CF=1)";

                                    lastdisassembledata.opcode = "jb";
                                    lastdisassembledata.isjump = true;
                                    lastdisassembledata.isconditionaljump = true;

                                    if (context != nil)
                                        lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_cf) != 0;

                                    offset += 1 + 4;
                                    if (markiprelativeinstructions)
                                    {
                                        lastdisassembledata.riprelative = 2;
                                        riprelative = true;
                                    }

                                    lastdisassembledata.parametervaluetype = dvtaddress;
                                    if (is64bit)
                                        lastdisassembledata.parametervalue = qword(offset + qword(pint(&memory[2])));
                                    else
                                        lastdisassembledata.parametervalue = dword(offset + qword(pint(&memory[2])));

                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 2;
                                    lastdisassembledata.seperatorcount += 1;



                                    lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                                }
                                break;

                            case 0x83:
                                {
                                    description = "jump near if above or equal (CF=0)";
                                    lastdisassembledata.opcode = "jae";
                                    lastdisassembledata.isjump = true;
                                    lastdisassembledata.isconditionaljump = true;
                                    if (context != nil)
                                        lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_cf) == 0;

                                    offset += 1 + 4;
                                    if (markiprelativeinstructions)
                                    {
                                        lastdisassembledata.riprelative = 2;
                                        riprelative = true;
                                    }

                                    lastdisassembledata.parametervaluetype = dvtaddress;
                                    if (is64bit)
                                        lastdisassembledata.parametervalue = qword(offset + qword(pint(&memory[2])));
                                    else
                                        lastdisassembledata.parametervalue = dword(offset + qword(pint(&memory[2])));

                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 2;
                                    lastdisassembledata.seperatorcount += 1;



                                    lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);
                                }
                                break;

                            case 0x84:
                                {
                                    description = "jump near if equal (ZF=1)";

                                    lastdisassembledata.opcode = "je";
                                    lastdisassembledata.isjump = true;
                                    lastdisassembledata.isconditionaljump = true;
                                    if (context != nil)
                                        lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_zf) != 0;

                                    offset += 1 + 4;
                                    if (markiprelativeinstructions)
                                    {
                                        lastdisassembledata.riprelative = 2;
                                        riprelative = true;
                                    }

                                    lastdisassembledata.parametervaluetype = dvtaddress;
                                    if (is64bit)
                                        lastdisassembledata.parametervalue = qword(offset + qword(pint(&memory[2])));
                                    else
                                        lastdisassembledata.parametervalue = dword(offset + qword(pint(&memory[2])));

                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 2;
                                    lastdisassembledata.seperatorcount += 1;



                                    lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);
                                }
                                break;


                            case 0x85:
                                {
                                    description = "jump near if not equal (ZF=0)";
                                    lastdisassembledata.opcode = "jne";
                                    lastdisassembledata.isjump = true;
                                    lastdisassembledata.isconditionaljump = true;
                                    if (context != nil)
                                        lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_zf) == 0;

                                    offset += 1 + 4;
                                    if (markiprelativeinstructions)
                                    {
                                        lastdisassembledata.riprelative = 2;
                                        riprelative = true;
                                    }

                                    lastdisassembledata.parametervaluetype = dvtaddress;
                                    if (is64bit)
                                        lastdisassembledata.parametervalue = qword(offset + qword(pint(&memory[2])));
                                    else
                                        lastdisassembledata.parametervalue = dword(offset + qword(pint(&memory[2])));

                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 2;
                                    lastdisassembledata.seperatorcount += 1;



                                    lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                                }
                                break;

                            case 0x86:
                                {
                                    description = "jump near if below or equal (CF=1 or ZF=1)";
                                    lastdisassembledata.opcode = "jbe";
                                    lastdisassembledata.isjump = true;
                                    lastdisassembledata.isconditionaljump = true;
                                    if (context != nil)
                                        lastdisassembledata.willjumpaccordingtocontext = (context->eflags & (eflags_cf | eflags_zf)) != 0;

                                    offset += 1 + 4;
                                    if (markiprelativeinstructions)
                                    {
                                        lastdisassembledata.riprelative = 2;
                                        riprelative = true;
                                    }


                                    lastdisassembledata.parametervaluetype = dvtaddress;
                                    if (is64bit)
                                        lastdisassembledata.parametervalue = qword(offset + qword(pint(&memory[2])));
                                    else
                                        lastdisassembledata.parametervalue = dword(offset + qword(pint(&memory[2])));

                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 2;
                                    lastdisassembledata.seperatorcount += 1;



                                    lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);
                                }
                                break;

                            case 0x87:
                                {
                                    description = "jump near if above (CF=0 and ZF=0)";
                                    lastdisassembledata.opcode = "ja";
                                    lastdisassembledata.isjump = true;
                                    lastdisassembledata.isconditionaljump = true;
                                    if (context != nil)
                                        lastdisassembledata.willjumpaccordingtocontext = (context->eflags & (eflags_cf | eflags_zf)) == 0;


                                    offset += 1 + 4;
                                    if (markiprelativeinstructions)
                                    {
                                        lastdisassembledata.riprelative = 2;
                                        riprelative = true;
                                    }

                                    lastdisassembledata.parametervaluetype = dvtaddress;
                                    if (is64bit)
                                        lastdisassembledata.parametervalue = qword(offset + qword(pint(&memory[2])));
                                    else
                                        lastdisassembledata.parametervalue = dword(offset + qword(pint(&memory[2])));

                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 2;
                                    lastdisassembledata.seperatorcount += 1;



                                    lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);
                                }
                                break;

                            case 0x88:
                                {
                                    description = "jump near if sign (SF=1)";
                                    lastdisassembledata.opcode = "js";
                                    lastdisassembledata.isjump = true;
                                    lastdisassembledata.isconditionaljump = true;
                                    if (context != nil)
                                        lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_sf) != 0;

                                    offset += 1 + 4;
                                    if (markiprelativeinstructions)
                                    {
                                        lastdisassembledata.riprelative = 2;
                                        riprelative = true;
                                    }

                                    lastdisassembledata.parametervaluetype = dvtaddress;
                                    if (is64bit)
                                        lastdisassembledata.parametervalue = qword(offset + qword(pint(&memory[2])));
                                    else
                                        lastdisassembledata.parametervalue = dword(offset + qword(pint(&memory[2])));

                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 2;
                                    lastdisassembledata.seperatorcount += 1;



                                    lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);
                                }
                                break;

                            case 0x89:
                                {
                                    description = "jump near if not sign (SF=0)";
                                    lastdisassembledata.opcode = "jns";
                                    lastdisassembledata.isjump = true;
                                    lastdisassembledata.isconditionaljump = true;
                                    if (context != nil)
                                        lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_sf) == 0;

                                    offset += 1 + 4;
                                    if (markiprelativeinstructions)
                                    {
                                        lastdisassembledata.riprelative = 2;
                                        riprelative = true;
                                    }

                                    lastdisassembledata.parametervaluetype = dvtaddress;
                                    if (is64bit)
                                        lastdisassembledata.parametervalue = qword(offset + qword(pint(&memory[2])));
                                    else
                                        lastdisassembledata.parametervalue = dword(offset + qword(pint(&memory[2])));

                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 2;
                                    lastdisassembledata.seperatorcount += 1;



                                    lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);
                                }
                                break;

                            case 0x8a:
                                {
                                    description = "jump near if parity (PF=1)";
                                    lastdisassembledata.opcode = "jp";
                                    lastdisassembledata.isjump = true;
                                    lastdisassembledata.isconditionaljump = true;
                                    if (context != nil)
                                        lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_pf) != 0;

                                    offset += 1 + 4;
                                    if (markiprelativeinstructions)
                                    {
                                        lastdisassembledata.riprelative = 2;
                                        riprelative = true;
                                    }

                                    lastdisassembledata.parametervaluetype = dvtaddress;
                                    if (is64bit)
                                        lastdisassembledata.parametervalue = qword(offset + qword(pint(&memory[2])));
                                    else
                                        lastdisassembledata.parametervalue = dword(offset + qword(pint(&memory[2])));

                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 2;
                                    lastdisassembledata.seperatorcount += 1;



                                    lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);
                                }
                                break;

                            case 0x8b:
                                {
                                    description = "jump near if not parity (PF=0)";
                                    lastdisassembledata.opcode = "jnp";
                                    lastdisassembledata.isjump = true;
                                    lastdisassembledata.isconditionaljump = true;
                                    if (context != nil)
                                        lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_pf) == 0;

                                    offset += 1 + 4;
                                    if (markiprelativeinstructions)
                                    {
                                        lastdisassembledata.riprelative = 2;
                                        riprelative = true;
                                    }

                                    lastdisassembledata.parametervaluetype = dvtaddress;
                                    if (is64bit)
                                        lastdisassembledata.parametervalue = qword(offset + qword(pint(&memory[2])));
                                    else
                                        lastdisassembledata.parametervalue = dword(offset + qword(pint(&memory[2])));

                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 2;
                                    lastdisassembledata.seperatorcount += 1;



                                    lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);
                                }
                                break;

                            case 0x8c:
                                {
                                    description = "jump near if less (SF~=OF)";
                                    lastdisassembledata.opcode = "jl";
                                    lastdisassembledata.isjump = true;
                                    lastdisassembledata.isconditionaljump = true;
                                    if (context != nil)
                                        lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_sf) != (context->eflags & eflags_of);

                                    offset += 1 + 4;
                                    if (markiprelativeinstructions)
                                    {
                                        lastdisassembledata.riprelative = 2;
                                        riprelative = true;
                                    }

                                    lastdisassembledata.parametervaluetype = dvtaddress;
                                    if (is64bit)
                                        lastdisassembledata.parametervalue = qword(offset + qword(pint(&memory[2])));
                                    else
                                        lastdisassembledata.parametervalue = dword(offset + qword(pint(&memory[2])));

                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 2;
                                    lastdisassembledata.seperatorcount += 1;



                                    lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);
                                }
                                break;

                            case 0x8d:
                                {
                                    description = "jump near if not less (SF=OF)";
                                    lastdisassembledata.opcode = "jnl";
                                    lastdisassembledata.isjump = true;
                                    lastdisassembledata.isconditionaljump = true;
                                    if (context != nil)
                                        lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_sf) == (context->eflags & eflags_of);

                                    offset += 1 + 4;
                                    if (markiprelativeinstructions)
                                    {
                                        lastdisassembledata.riprelative = 2;
                                        riprelative = true;
                                    }

                                    lastdisassembledata.parametervaluetype = dvtaddress;
                                    if (is64bit)
                                        lastdisassembledata.parametervalue = qword(offset + qword(pint(&memory[2])));
                                    else
                                        lastdisassembledata.parametervalue = dword(offset + qword(pint(&memory[2])));

                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 2;
                                    lastdisassembledata.seperatorcount += 1;



                                    lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);
                                }
                                break;

                            case 0x8e:
                                {
                                    description = "jump near if not greater (ZF=1 or SF~=OF)";
                                    lastdisassembledata.opcode = "jng";
                                    lastdisassembledata.isjump = true;
                                    lastdisassembledata.isconditionaljump = true;
                                    if (context != nil)
                                        lastdisassembledata.willjumpaccordingtocontext = ((context->eflags & eflags_sf) != (context->eflags & eflags_of)) || ((context->eflags & eflags_zf) != 0);


                                    offset += 1 + 4;
                                    if (markiprelativeinstructions)
                                    {
                                        lastdisassembledata.riprelative = 2;
                                        riprelative = true;
                                    }

                                    lastdisassembledata.parametervaluetype = dvtaddress;
                                    if (is64bit)
                                        lastdisassembledata.parametervalue = qword(offset + qword(pint(&memory[2])));
                                    else
                                        lastdisassembledata.parametervalue = dword(offset + qword(pint(&memory[2])));

                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 2;
                                    lastdisassembledata.seperatorcount += 1;



                                    lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);
                                }
                                break;

                            case 0x8f:
                                {
                                    description = "jump near if greater (ZF=0 and SF=OF)";
                                    lastdisassembledata.opcode = "jg";
                                    lastdisassembledata.isjump = true;
                                    lastdisassembledata.isconditionaljump = true;
                                    if (context != nil)
                                        lastdisassembledata.willjumpaccordingtocontext = ((context->eflags & eflags_sf) == (context->eflags & eflags_of)) && ((context->eflags & eflags_zf) == 0);


                                    offset += 1 + 4;
                                    if (markiprelativeinstructions)
                                    {
                                        lastdisassembledata.riprelative = 2;
                                        riprelative = true;
                                    }

                                    lastdisassembledata.parametervaluetype = dvtaddress;
                                    if (is64bit)
                                        lastdisassembledata.parametervalue = qword(offset + qword(pint(&memory[2])));
                                    else
                                        lastdisassembledata.parametervalue = dword(offset + qword(pint(&memory[2])));

                                    lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 2;
                                    lastdisassembledata.seperatorcount += 1;



                                    lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);
                                }
                                break;

                            case 0x90:
                                {
                                    description = "set byte if overflow";
                                    lastdisassembledata.opcode = "seto";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 0x91:
                                {
                                    description = "set byte if not overfloww";
                                    lastdisassembledata.opcode = "setno";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 0x92:
                                {
                                    description = "set byte if below/carry";
                                    lastdisassembledata.opcode = "setb";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 0x93:
                                {
                                    description = "set byte if above or equal";
                                    lastdisassembledata.opcode = "setae";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 0x94:
                                {
                                    description = "set byte if equal";
                                    lastdisassembledata.opcode = "sete";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 0x95:
                                {
                                    description = "set byte if not equal";
                                    lastdisassembledata.opcode = "setne";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 0x96:
                                {
                                    description = "set byte if below or equal";
                                    lastdisassembledata.opcode = "setbe";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 0x97:
                                {
                                    description = "set byte if above";
                                    lastdisassembledata.opcode = "seta";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 0x98:
                                {
                                    description = "set byte if sign";
                                    lastdisassembledata.opcode = "sets";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 0x99:
                                {
                                    description = "set byte if not sign";
                                    lastdisassembledata.opcode = "setns";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 0x9a:
                                {
                                    description = "set byte if parity";
                                    lastdisassembledata.opcode = "setp";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 0x9b:
                                {
                                    description = "set byte if not parity";
                                    lastdisassembledata.opcode = "setnp";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 0x9c:
                                {
                                    description = "set byte if less";
                                    lastdisassembledata.opcode = "setl";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 0x9d:
                                {
                                    description = "set byte if greater or equal";
                                    lastdisassembledata.opcode = "setge";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last, 8);
                                    offset += last - 1;

                                }
                                break;

                            case 0x9e:
                                {
                                    description = "set byte if less or equal";
                                    lastdisassembledata.opcode = "setle";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last, 8);
                                    offset += last - 1;

                                }
                                break;

                            case 0x9f:
                                {
                                    description = "set byte if greater";
                                    lastdisassembledata.opcode = "setg";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last, 8);
                                    offset += last - 1;


                                }
                                break;

                            case 0xa0:
                                {
                                    description = "push word or doubleword onto the stack";
                                    lastdisassembledata.opcode = "push";
                                    lastdisassembledata.parameters = "fs";
                                    offset += 1;
                                }
                                break;

                            case 0xa1:
                                {
                                    description = "pop a value from the stack";
                                    lastdisassembledata.opcode = "pop";
                                    lastdisassembledata.parameters = "fs";
                                    offset += 1;
                                }
                                break;


                            case 0xa2:
                                {
                                    description = "cpu identification";
                                    lastdisassembledata.opcode = "cpuid";
                                    offset += 1;
                                }
                                break;

                            case 0xa3:
                                {
                                    description = "bit test";
                                    lastdisassembledata.opcode = "bt";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last) + r16(memory[2]);
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last) + r32(memory[2]);

                                    offset += last - 1;
                                }
                                break;

                            case 0xa4:
                                {
                                    description = "double precision shift left";
                                    lastdisassembledata.opcode = "shld";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last) + r16(memory[2]);
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last) + r32(memory[2]);

                                    lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohex(memory[last], 2);
                                    last += 1;
                                    offset += last - 1;

                                }
                                break;

                            case 0xa5:
                                {
                                    description = "double precision shift left";
                                    lastdisassembledata.opcode = "shld";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last) + r16(memory[2]) + ',' + colorreg + "cl" + endcolor;
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last) + r32(memory[2]) + ',' + colorreg + "cl" + endcolor;
                                    offset += last - 1;

                                }
                                break;

                            case 0xa8:
                                {
                                    description = "push word or doubleword onto the stack";
                                    lastdisassembledata.opcode = "push";
                                    lastdisassembledata.parameters = "gs";
                                    offset += 1;
                                }
                                break;

                            case 0xa9:
                                {
                                    description = "pop a value from the stack";
                                    lastdisassembledata.opcode = "pop";
                                    lastdisassembledata.parameters = "gs";
                                    offset += 1;
                                }
                                break;

                            case 0xaa:
                                {
                                    description = "resume from system management mode";
                                    lastdisassembledata.opcode = "rsm";
                                    offset += 1;
                                }
                                break;

                            case 0xab:
                                {
                                    description = "bit test and set";
                                    lastdisassembledata.opcode = "bts";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last) + r16(memory[2]);
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last) + r32(memory[2]);
                                    offset += last - 1;

                                }
                                break;

                            case 0xac:
                                {
                                    description = "double precision shift right";
                                    lastdisassembledata.opcode = "shrd";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last) + r16(memory[2]);
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last) + r32(memory[2]);

                                    lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohex(memory[last], 2);
                                    last += 1;
                                    offset += last - 1;
                                }
                                break;

                            case 0xad:
                                {
                                    description = "double precision shift right";
                                    lastdisassembledata.opcode = "shrd";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last) + r16(memory[2]) + ',' + colorreg + "cl" + endcolor;
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last) + r32(memory[2]) + ',' + colorreg + "cl" + endcolor;
                                    offset += last - 1;

                                }
                                break;

                            case 0xae:
                                {
                                    switch (memory[2])
                                    {
                                        case 0xf0:
                                            {
                                                description = "memory fence";
                                                lastdisassembledata.opcode = "mfence";
                                                offset += 1;
                                            }
                                            break;

                                        case 0xf8:
                                            {
                                                description = "store fence";
                                                lastdisassembledata.opcode = "sfence";
                                                offset += 1;
                                            }
                                            break;

                                        default:
                                            switch (getreg(memory[2]))
                                            {
                                                case 0:
                                                    {
                                                        if (prefix2.has(0xf3))
                                                        {
                                                            description = "read fs base address";
                                                            lastdisassembledata.opcode = "rdfsbase";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "store fp and mmx state and streaming simd extension state";
                                                            lastdisassembledata.opcode = "fxsave";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                    break;

                                                case 1:
                                                    {
                                                        if (prefix2.has(0xf3))
                                                        {
                                                            description = "read gs base address";
                                                            lastdisassembledata.opcode = "rdgsbase";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "restore fp and mmx state and streaming simd extension state";
                                                            lastdisassembledata.opcode = "fxrstor";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                    break;

                                                case 2:
                                                    {
                                                        if (prefix2.has(0xf3))
                                                        {
                                                            description = "write fs base address";
                                                            lastdisassembledata.opcode = "wrfsbase";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "load streaming simd extension control/status";
                                                            if (hasvex)
                                                                lastdisassembledata.opcode = "vldmxcsr";
                                                            else
                                                                lastdisassembledata.opcode = "ldmxcsr";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                    break;

                                                case 3:
                                                    {
                                                        if (prefix2.has(0xf3))
                                                        {
                                                            description = "write gs base address";
                                                            lastdisassembledata.opcode = "wrgsbase";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                            offset += last - 1;
                                                        }
                                                        else
                                                        {
                                                            description = "store streaming simd extension control/status";
                                                            if (hasvex)
                                                                lastdisassembledata.opcode = "stmxcsr";
                                                            else
                                                                lastdisassembledata.opcode = "stmxcsr";

                                                            opcodeflags.skipextrareg = true;
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                    break;

                                                case 4:
                                                    {
                                                        description = "save processor extended state";
                                                        if (rex_w)
                                                            lastdisassembledata.opcode = "xsave64";
                                                        else
                                                            lastdisassembledata.opcode = "xsave";
                                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                        offset += last - 1;
                                                    }
                                                    break;

                                                case 5:
                                                    {
                                                        if (getmod(memory[2]) == 3)
                                                        {
                                                            description = "Load Fence";
                                                            lastdisassembledata.opcode = "lfence";
                                                            offset += 2;
                                                        }
                                                        else
                                                        {
                                                            description = "restore processor extended state";
                                                            if (rex_w)
                                                                lastdisassembledata.opcode = "xrstor64";
                                                            else
                                                                lastdisassembledata.opcode = "xrstor";
                                                            lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                            offset += last - 1;
                                                        }
                                                    }
                                                    break;

                                                case 6:
                                                    {
                                                        description = "save processor extended status optimized";
                                                        if (rex_w)
                                                            lastdisassembledata.opcode = "xsaveopt64";
                                                        else
                                                            lastdisassembledata.opcode = "xsaveopt";
                                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                        offset += last - 1;
                                                    }
                                                    break;

                                                case 7:
                                                    {
                                                        ;

                                                    }
                                                    break;

                                            }

                                    }



                                }
                                break;

                            case 0xaf:
                                {
                                    description = "signed multiply";
                                    lastdisassembledata.opcode = "imul";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0xb0:
                                {
                                    description = "compare and exchange";
                                    lastdisassembledata.opcode = "cmpxchg";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last) + r8(memory[2]);
                                    offset += last - 1;
                                }
                                break;

                            case 0xb1:
                                {
                                    description = "compare and exchange";
                                    lastdisassembledata.opcode = "cmpxchg";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last) + r16(memory[2]);
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last) + r32(memory[2]);
                                    offset += last - 1;
                                }
                                break;

                            case 0xb2:
                                {
                                    description = "load far pointer";
                                    lastdisassembledata.opcode = "lss";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0xb3:
                                {
                                    description = "bit test and reset";
                                    lastdisassembledata.opcode = "btr";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last) + r16(memory[2]);
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last) + r32(memory[2]);
                                    offset += last - 1;

                                }
                                break;

                            case 0xb4:
                                {
                                    description = "load far pointer";
                                    lastdisassembledata.opcode = "lfs";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0xb5:
                                {
                                    description = "load far pointer";
                                    lastdisassembledata.opcode = "lgs";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0xb6:
                                {
                                    description = "Move with zero-extend";
                                    lastdisassembledata.opcode = "movzx";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 2, last, 8, 0, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 2, last, 8, 0, mright);


                                    offset += last - 1;
                                }
                                break;

                            case 0xb7:
                                {
                                    description = "Move with zero-extend";
                                    lastdisassembledata.opcode = "movzx";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, 16, 0, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 1, last, 16, 0, mright);


                                    offset += last - 1;
                                }
                                break;

                            case 0xb8:
                                {
                                    if (prefix2.has(0xf3))
                                    {
                                        description = "Return the Count of Number of Bits Set to 1";
                                        lastdisassembledata.opcode = "popcnt";
                                        if (prefix2.has(0x66))
                                            lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                        else
                                            lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;


                            case 0xba:
                                {
                                    lastdisassembledata.parametervaluetype = dvtvalue;


                                    switch (getreg(memory[2]))
                                    {
                                        case 4:
                                            {
                                                //bt
                                                description = "bit test";
                                                lastdisassembledata.opcode = "bt";
                                                if (prefix2.has(0x66))
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last);
                                                else
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);     //notice the difference in the modrm 4th parameter

                                                lastdisassembledata.parametervalue = memory[last];
                                                lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);

                                                offset += last - 1 + 1;
                                            }
                                            break;

                                        case 5:
                                            {
                                                //bts
                                                description = "bit test and set";
                                                lastdisassembledata.opcode = "bts";
                                                if (prefix2.has(0x66))
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last);
                                                else
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);     //notice the difference in the modrm 4th parameter

                                                lastdisassembledata.parametervalue = memory[last];
                                                lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                                offset += last - 1 + 1;
                                            }
                                            break;

                                        case 6:
                                            {
                                                //btr
                                                description = "bit test and reset";
                                                lastdisassembledata.opcode = "btr";
                                                if (prefix2.has(0x66))
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last);
                                                else
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);     //notice the difference in the modrm 4th parameter

                                                lastdisassembledata.parametervalue = memory[last];
                                                lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);

                                                offset += last - 1 + 1;
                                            }
                                            break;

                                        case 7:
                                            {
                                                //btc
                                                description = "bit test and complement";
                                                lastdisassembledata.opcode = "btc";
                                                if (prefix2.has(0x66))
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last);
                                                else
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);     //notice the difference in the modrm 4th parameter

                                                lastdisassembledata.parametervalue = memory[last];
                                                lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);

                                                offset += last - 1 + 1;
                                            }
                                            break;

                                    }

                                }
                                break;

                            case 0xbb:
                                {
                                    description = "bit test and complement";
                                    lastdisassembledata.opcode = "btc";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last) + r16(memory[2]);
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last) + r32(memory[2]);
                                    offset += last - 1;

                                }
                                break;


                            case 0xbc:
                                {
                                    if (prefix2.has(0xf3))
                                    {
                                        description = "count the number of trailing zero bits";
                                        lastdisassembledata.opcode = "tzcnt";
                                        if (prefix2.has(0x66))
                                            lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                        else
                                            lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        //bsf
                                        description = "bit scan forward";
                                        lastdisassembledata.opcode = "bsf";
                                        if (prefix2.has(0x66))
                                            lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                        else
                                            lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);


                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xbd:
                                {
                                    if (prefix2.has(0xf3))
                                    {
                                        description = "count the number of leading zero bits";
                                        lastdisassembledata.opcode = "lzcnt";
                                        if (prefix2.has(0x66))
                                            lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                        else
                                            lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        //bsf
                                        description = "bit scan reverse";
                                        lastdisassembledata.opcode = "bsr";
                                        if (prefix2.has(0x66))
                                            lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 1, last, mright);
                                        else
                                            lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);


                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xbe:
                                {
                                    description = "move with sign-extension";
                                    lastdisassembledata.opcode = "movsx";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = r16(memory[2]) + modrm(memory, prefix2, 2, 2, last, 8, 0, mright);
                                    else
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 2, last, 8, 0, mright);



                                    offset += last - 1;
                                }
                                break;

                            case 0xbf:
                                {
                                    description = "move with sign-extension";
                                    lastdisassembledata.opcode = "movsx";
                                    lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 1, last, 16, 0, mright);

                                    offset += last - 1;
                                }
                                break;

                            case 0xc0:
                                {
                                    description = "exchange and add";
                                    lastdisassembledata.opcode = "xadd";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 2, last) + r8(memory[2]);
                                    offset += last - 1;
                                }
                                break;

                            case 0xc1:
                                {
                                    description = "exchange and add";
                                    lastdisassembledata.opcode = "xadd";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last) + r16(memory[2]);
                                    else

                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last) + r32(memory[2]);
                                    offset += last - 1;
                                }
                                break;

                            case 0xc2:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0xf2))
                                    {

                                        description = "compare scalar dpuble-precision floating-point values";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vcmpsd";
                                        else
                                            lastdisassembledata.opcode = "cmpsd";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, 128, 0, mright);

                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohexs(lastdisassembledata.parametervalue, 2);
                                        offset += last;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {
                                        description = "packed single-fp compare";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vcmpss";
                                        else
                                            lastdisassembledata.opcode = "cmpss";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, 128, 0, mright);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohexs(lastdisassembledata.parametervalue, 2);
                                        lastdisassembledata.datasize = 4;
                                        offset += last;
                                    }
                                    else
                                    {
                                        if (prefix2.has(0x66))
                                        {
                                            description = "compare packed double-precision floating-point values";
                                            if (hasvex)
                                                lastdisassembledata.opcode = "vcmppd";
                                            else
                                                lastdisassembledata.opcode = "cmppd";
                                            lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, 128, 0, mright);
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = memory[last];
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohexs(lastdisassembledata.parametervalue, 2);
                                            offset += last;
                                        }
                                        else
                                        {
                                            description = "packed single-fp compare";
                                            if (hasvex)
                                                lastdisassembledata.opcode = "vcmpps";
                                            else
                                                lastdisassembledata.opcode = "cmpps";
                                            lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, 128, 0, mright);
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = memory[last];
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohexs(lastdisassembledata.parametervalue, 2);
                                            lastdisassembledata.datasize = 4;
                                            offset += last;
                                        }
                                    }
                                }
                                break;

                            case 0xc3:
                                {
                                    description = "store doubleword using non-temporal hint";
                                    lastdisassembledata.opcode = "movnti";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last) + r32(memory[2]);
                                    offset += last;
                                }
                                break;

                            case 0xc4:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "insert word";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpinsrw";
                                        else
                                            lastdisassembledata.opcode = "pinsrw";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(lastdisassembledata.parametervalue, 2);
                                        offset += last;
                                    }
                                    else
                                    {
                                        description = "insert word";
                                        lastdisassembledata.opcode = "pinsrw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 0, last, mright);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(lastdisassembledata.parametervalue, 2);
                                        offset += last;
                                    }
                                }
                                break;

                            case 0xc5:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "extract word";
                                        lastdisassembledata.opcode = "pextrw";
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 4, last);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(lastdisassembledata.parametervalue, 2);
                                        offset += 3;
                                    }
                                    else
                                    {
                                        description = "extract word";
                                        lastdisassembledata.opcode = "pextrw";
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(lastdisassembledata.parametervalue, 2);
                                        offset += 3;
                                    }
                                }
                                break;

                            case 0xc6:
                                {
                                    lastdisassembledata.isfloat = true;
                                    if (prefix2.has(0x66))
                                    {
                                        description = "shuffle double-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vshufpd";
                                        else
                                            lastdisassembledata.opcode = "shufpd";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohexs(lastdisassembledata.parametervalue, 2);
                                        offset += last;
                                    }
                                    else
                                    {
                                        description = "shuffle single-fp";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vshufps";
                                        else
                                            lastdisassembledata.opcode = "shufps";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohexs(lastdisassembledata.parametervalue, 2);
                                        lastdisassembledata.datasize = 4;
                                        offset += last;
                                    }
                                }
                                break;

                            case 0xc7:
                                {
                                    switch (getreg(memory[2]))
                                    {
                                        case 1:
                                            {
                                                description = "compare and exchange 8 bytes";
                                                lastdisassembledata.opcode = "cmpxchg8b";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 3:
                                            {
                                                description = "restore processor extended status supervisor";
                                                if (rex_w)
                                                    lastdisassembledata.opcode = "xrstors64";
                                                else
                                                    lastdisassembledata.opcode = "xrstors";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 4:
                                            {
                                                description = "save processor extended state with compaction";
                                                if (rex_w)
                                                    lastdisassembledata.opcode = "xsavec";
                                                else
                                                    lastdisassembledata.opcode = "xsavec64";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 5:
                                            {
                                                description = "save processor extended state supervisor";
                                                if (rex_w)
                                                    lastdisassembledata.opcode = "xsaves";
                                                else
                                                    lastdisassembledata.opcode = "xsaves64";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                offset += last - 1;
                                            }
                                            break;


                                        case 6:
                                            {
                                                if (prefix2.has(0x66))
                                                {
                                                    if (getmod(memory[2]) == 3)  //reg
                                                    {
                                                        description = "read random numer";
                                                        lastdisassembledata.opcode = "rdrand";
                                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last);
                                                        offset += last - 1;
                                                    }
                                                    else
                                                    {
                                                        description = "copy vmcs data to vmcs region in memory";
                                                        lastdisassembledata.opcode = "vmclear";
                                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);

                                                        offset += last - 1;
                                                    }
                                                }
                                                else
                                                if (prefix2.has(0xf3))
                                                {
                                                    description = "enter vmx root operation";
                                                    lastdisassembledata.opcode = "vmxon";
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);

                                                    offset += last - 1;
                                                }
                                                else
                                                {
                                                    //check if it's a memory or register access
                                                    //if register it's rdrand else vmptrld
                                                    if (getmod(memory[2]) == 3)  //reg
                                                    {
                                                        description = "read random numer";
                                                        lastdisassembledata.opcode = "rdrand";
                                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                        offset += last - 1;
                                                    }
                                                    else
                                                    {
                                                        description = "loads the current vmcs pointer from memory";
                                                        lastdisassembledata.opcode = "vmptrld";
                                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);

                                                        offset += last - 1;
                                                    }


                                                }
                                            }
                                            break;

                                        case 7:
                                            {
                                                if (getmod(memory[2]) == 3)  //reg
                                                {
                                                    description = "read random SEED";
                                                    lastdisassembledata.opcode = "rdseed";
                                                    if (prefix2.has(0x66))
                                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 1, last);
                                                    else
                                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);

                                                    offset += last - 1;
                                                }
                                                else
                                                {
                                                    description = "stores the current vmcs pointer into memory";
                                                    lastdisassembledata.opcode = "vmptrst";
                                                    lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);

                                                    offset += last - 1;
                                                }
                                            }
                                            break;
                                    }

                                }
                                break;

                            case RANGE_8(0xc8, 0xcf):
                                {
                                    //bswap
                                    description = "byte swap";
                                    lastdisassembledata.opcode = "bswap";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = rd16(memory[1] - 0xc8);
                                    else
                                        lastdisassembledata.parameters = rd(memory[1] - 0xc8);

                                    offset += 1;
                                }
                                break;

                            case 0xd0:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "Packed Double-FP Add/Subtract";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vaddsubpd";
                                        else
                                            lastdisassembledata.opcode = "addsubpd";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf2))
                                    {
                                        description = "Packed Single-FP Add/Subtract";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vaddsubps";
                                        else
                                            lastdisassembledata.opcode = "addsubps";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xd1:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed shift right logical";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpsrlw";
                                        else
                                            lastdisassembledata.opcode = "psrlw";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed shift right logical";
                                        lastdisassembledata.opcode = "psrlw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xd2:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed shift right logical";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpsrld";
                                        else
                                            lastdisassembledata.opcode = "psrld";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed shift right logical";
                                        lastdisassembledata.opcode = "psrld";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xd3:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed shift right logical";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpsrlq";
                                        else
                                            lastdisassembledata.opcode = "psrlq";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed shift right logical";
                                        lastdisassembledata.opcode = "psrlq";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xd4:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "add packed quadword integers";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpaddq";
                                        else
                                            lastdisassembledata.opcode = "paddq";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "add packed quadword integers";
                                        lastdisassembledata.opcode = "paddq";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;


                            case 0xd5:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed multiply low";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpmullw";
                                        else
                                            lastdisassembledata.opcode = "pmullw";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed multiply low";
                                        lastdisassembledata.opcode = "pmullw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xd6:
                                {
                                    if (prefix2.has(0xf2))
                                    {

                                        description = "move low quadword from xmm to mmx technology register";
                                        lastdisassembledata.opcode = "movdq2q";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {

                                        description = "move low quadword from xmm to mmx technology register";
                                        lastdisassembledata.opcode = "movq2dq";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0x66))
                                    {
                                        description = "move low quadword from xmm to mmx technology register";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmovq";
                                        else
                                            lastdisassembledata.opcode = "movq";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last) + xmm(memory[2]);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "move quadword from mmx technology to xmm register";
                                        lastdisassembledata.opcode = "movq2dq";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last) + mm(memory[2]);
                                        offset += last - 1;
                                    }

                                }
                                break;


                            case 0xd7:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "move byte mask to integer";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpmovmskb";
                                        else
                                            lastdisassembledata.opcode = "pmovmskb";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "move byte mask to integer";
                                        lastdisassembledata.opcode = "pmovmskb";
                                        lastdisassembledata.parameters = r32(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xd8:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed subtract unsigned with saturation";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpsubusb";
                                        else
                                            lastdisassembledata.opcode = "psubusb";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed subtract unsigned with saturation";
                                        lastdisassembledata.opcode = "psubusb";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xd9:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed subtract unsigned with saturation";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpsubusw";
                                        else
                                            lastdisassembledata.opcode = "psubusw";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed subtract unsigned with saturation";
                                        lastdisassembledata.opcode = "psubusw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xda:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed unsigned integer byte minimum";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpminub";
                                        else
                                            lastdisassembledata.opcode = "pminub";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed unsigned integer byte minimum";
                                        lastdisassembledata.opcode = "pminub";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xdb:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "logical and";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpand";
                                        else
                                            lastdisassembledata.opcode = "pand";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "logical and";
                                        lastdisassembledata.opcode = "pand";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xdc:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed add unsigned with saturation";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpaddusb";
                                        else
                                            lastdisassembledata.opcode = "paddusb";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed add unsigned with saturation";
                                        lastdisassembledata.opcode = "paddusb";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xdd:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed add unsigned with saturation";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpaddusw";
                                        else
                                            lastdisassembledata.opcode = "paddusw";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed add unsigned with saturation";
                                        lastdisassembledata.opcode = "paddusw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xde:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed unsigned integer byte maximum";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpmaxub";
                                        else
                                            lastdisassembledata.opcode = "pmaxub";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mleft);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed unsigned integer byte maximum";
                                        lastdisassembledata.opcode = "pmaxub";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xdf:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "logical and not";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpandn";
                                        else
                                            lastdisassembledata.opcode = "pandn";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "logical and not";
                                        lastdisassembledata.opcode = "pandn";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xe0:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed average";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpavgb";
                                        else
                                            lastdisassembledata.opcode = "pavgb";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed average";
                                        lastdisassembledata.opcode = "pavgb";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xe1:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed shift right arithmetic";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpsraw";
                                        else
                                            lastdisassembledata.opcode = "psraw";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed shift right arithmetic";
                                        lastdisassembledata.opcode = "psraw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xe2:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed shift left logical";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpsrad";
                                        else
                                            lastdisassembledata.opcode = "psrad";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed shift left logical";
                                        lastdisassembledata.opcode = "psrad";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xe3:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed average";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpavgw";
                                        else
                                            lastdisassembledata.opcode = "pavgw";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed average";
                                        lastdisassembledata.opcode = "pavgw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xe4:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed multiply high unsigned";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpmulhuw";
                                        else
                                            lastdisassembledata.opcode = "pmulhuw";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed multiply high unsigned";
                                        lastdisassembledata.opcode = "pmulhuw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xe5:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed multiply high";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpmulhw";
                                        else
                                            lastdisassembledata.opcode = "pmulhw";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed multiply high";
                                        lastdisassembledata.opcode = "pmulhw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xe6:
                                {
                                    if (prefix2.has(0xf2))
                                    {

                                        description = "convert two packed signed dwords from param2 to two packed dp-floating point values in param1";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vcvtpd2dq";
                                        else
                                            lastdisassembledata.opcode = "cvtpd2dq";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    if (prefix2.has(0xf3))
                                    {

                                        description = "convert two packed signed dwords from param2 to two packed dp-floating point values in param1";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vcvtdq2pd";
                                        else
                                            lastdisassembledata.opcode = "cvtdq2pd";
                                        lastdisassembledata.parameters = xmm(memory[2]) + ',';
                                        opcodeflags.l = false;
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        if (prefix2.has(0x66))
                                        {
                                            description = "convert with truncation packed double-precision floating-point values to packed doubleword integers";
                                            if (hasvex)
                                                lastdisassembledata.opcode = "vcvttpd2dq";
                                            else
                                                lastdisassembledata.opcode = "cvttpd2dq";

                                            opcodeflags.skipextrareg = true;
                                            lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                            offset += last - 1;
                                        }
                                    }
                                }
                                break;

                            case 0xe7:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        if (hasvex)
                                            lastdisassembledata.opcode = "movntdq";
                                        else
                                            lastdisassembledata.opcode = "vmovntdq";

                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 4, last) + xmm(memory[2]);
                                        description = "move double quadword using non-temporal hint";
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "movntq";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 2, 3, last) + mm(memory[2]);
                                        description = "move 64 bits non temporal";
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xe8:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed subtract with saturation";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpsubsb";
                                        else
                                            lastdisassembledata.opcode = "psubsb";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed subtract with saturation";
                                        lastdisassembledata.opcode = "psubsb";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xe9:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed subtract with saturation";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpsubsw";
                                        else
                                            lastdisassembledata.opcode = "psubsw";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed subtract with saturation";
                                        lastdisassembledata.opcode = "psubsw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xea:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed signed integer word minimum";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpminsw";
                                        else
                                            lastdisassembledata.opcode = "pminsw";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed signed integer word minimum";
                                        lastdisassembledata.opcode = "pminsw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xeb:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "bitwise logical or";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpor";
                                        else
                                            lastdisassembledata.opcode = "por";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "bitwise logical or";
                                        lastdisassembledata.opcode = "por";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xec:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed add with saturation";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpaddsb";
                                        else
                                            lastdisassembledata.opcode = "paddsb";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed add with saturation";
                                        lastdisassembledata.opcode = "paddsb";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xed:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed add with saturation";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpaddsw";
                                        else
                                            lastdisassembledata.opcode = "paddsw";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed add with saturation";
                                        lastdisassembledata.opcode = "paddsw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xee:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed signed integer word maximum";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpmaxsw";
                                        else
                                            lastdisassembledata.opcode = "pmaxsw";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed signed integer word maximum";
                                        lastdisassembledata.opcode = "pmaxsw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xef:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "logical exclusive or";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpxor";
                                        else
                                            lastdisassembledata.opcode = "pxor";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "logical exclusive or";
                                        lastdisassembledata.opcode = "pxor";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xf0:
                                {
                                    if (prefix2.has(0xf2))
                                    {
                                        description = "load unaligned integer 128 bits";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vlddqu";
                                        else
                                            lastdisassembledata.opcode = "lddqu";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                        offset += 1;
                                }
                                break;


                            case 0xf1:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed shift left logical";
                                        lastdisassembledata.opcode = "psllw";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed shift left logical";
                                        lastdisassembledata.opcode = "psllw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xf2:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed shift left logical";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpslld";
                                        else
                                            lastdisassembledata.opcode = "pslld";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed shift left logical";
                                        lastdisassembledata.opcode = "pslld";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xf3:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed shift left logical";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpsllq";
                                        else
                                            lastdisassembledata.opcode = "psllq";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed shift left logical";
                                        lastdisassembledata.opcode = "psllq";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xf4:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "multiply packed unsigned doubleword integers";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "pmuludq";
                                        else
                                            lastdisassembledata.opcode = "vpmuludq";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "multiply packed unsigned doubleword integers";
                                        lastdisassembledata.opcode = "pmuludq";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;


                            case 0xf5:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed multiply and add";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpmaddwd";
                                        else
                                            lastdisassembledata.opcode = "pmaddwd";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed multiply and add";
                                        lastdisassembledata.opcode = "pmaddwd";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xf6:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed sum of absolute differences";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpsadbw";
                                        else
                                            lastdisassembledata.opcode = "psadbw";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed sum of absolute differences";
                                        lastdisassembledata.opcode = "psadbw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xf7:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "store selected bytes of double quadword";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vmaskmovdqu";
                                        else
                                            lastdisassembledata.opcode = "maskmovdqu";

                                        opcodeflags.skipextrareg = true;
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "byte mask write";
                                        lastdisassembledata.opcode = "maskmovq";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xf8:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed subtract";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpsubb";
                                        else
                                            lastdisassembledata.opcode = "psubb";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed subtract";
                                        lastdisassembledata.opcode = "psubb";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xf9:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed subtract";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpsubw";
                                        else
                                            lastdisassembledata.opcode = "psubw";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed subtract";
                                        lastdisassembledata.opcode = "psubw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xfa:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed subtract";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpsubd";
                                        else
                                            lastdisassembledata.opcode = "psubd";

                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed subtract";
                                        lastdisassembledata.opcode = "psubd";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xfb:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed subtract";
                                        lastdisassembledata.opcode = "psubq";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed subtract";
                                        lastdisassembledata.opcode = "psubq";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xfc:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed add";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpaddb";
                                        else
                                            lastdisassembledata.opcode = "paddb";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed add";
                                        lastdisassembledata.opcode = "paddb";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xfd:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed add";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpaddw";
                                        else
                                            lastdisassembledata.opcode = "paddw";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed add";
                                        lastdisassembledata.opcode = "paddw";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 0xfe:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "packed add";
                                        if (hasvex)
                                            lastdisassembledata.opcode = "vpaddd";
                                        else
                                            lastdisassembledata.opcode = "paddd";
                                        lastdisassembledata.parameters = xmm(memory[2]) + modrm(memory, prefix2, 2, 4, last, mright);

                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "packed add";
                                        lastdisassembledata.opcode = "paddd";
                                        lastdisassembledata.parameters = mm(memory[2]) + modrm(memory, prefix2, 2, 3, last, mright);

                                        offset += last - 1;
                                    }
                                }
                                break;


                        }



                    }
                    break;

                //

                //

                case 0x10:
                    {
                        description = "add with carry";
                        lastdisassembledata.opcode = "adc";
                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last) + r8(memory[1]);
                        offset += last - 1;
                    }
                    break;

                case 0x11:
                    {
                        description = "add with carry";
                        lastdisassembledata.opcode = "adc";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last) + r16(memory[1]);
                        else
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + r32(memory[1]);
                        offset += last - 1;

                    }
                    break;

                case 0x12:
                    {
                        description = "add with carry";
                        lastdisassembledata.opcode = "adc";
                        lastdisassembledata.parameters = r8(memory[1]) + modrm(memory, prefix2, 1, 2, last, 8, 0, mright);

                        offset += last - 1;
                    }
                    break;

                case 0x13:
                    {
                        description = "add with carry";
                        lastdisassembledata.opcode = "adc";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = r16(memory[1]) + modrm(memory, prefix2, 1, 1, last, mright);
                        else
                            lastdisassembledata.parameters = r32(memory[1]) + modrm(memory, prefix2, 1, 0, last, mright);

                        offset += last - 1;
                    }
                    break;

                case 0x14:
                    {
                        description = "add with carry";
                        lastdisassembledata.opcode = "adc";
                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = memory[1];
                        lastdisassembledata.parameters = colorreg + "al" + endcolor + ',' + inttohexs(lastdisassembledata.parametervalue, 2);

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        offset += 1;
                    }
                    break;

                case 0x15:
                    {
                        description = "add with carry";
                        lastdisassembledata.opcode = "adc";
                        if (prefix2.has(0x66))
                        {
                            wordptr = &memory[1];
                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = wordptr;

                            lastdisassembledata.parameters = colorreg + "ax" + endcolor + ',' + inttohexs(lastdisassembledata.parametervalue, 4);
                            offset += 2;
                        }
                        else
                        {
                            dwordptr = &memory[1];
                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = dwordptr;

                            if (rex_w)
                                lastdisassembledata.parameters = colorreg + "rax" + endcolor + ',' + inttohexs((longint)(lastdisassembledata.parametervalue), 8);
                            else
                                lastdisassembledata.parameters = colorreg + "eax" + endcolor + ',' + inttohexs(lastdisassembledata.parametervalue, 8);
                            offset += 4;
                        }

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                    }
                    break;

                case 0x16:
                    {
                        description = "place ss on the stack";
                        lastdisassembledata.opcode = "push";
                        lastdisassembledata.parameters = colorreg + "ss" + endcolor;
                    }
                    break;

                case 0x17:
                    {
                        description = "remove ss from the stack";
                        lastdisassembledata.opcode = "pop";
                        lastdisassembledata.parameters = colorreg + "ss" + endcolor;
                    }
                    break;

                case 0x18:
                    {
                        description = "integer subtraction with borrow";
                        lastdisassembledata.opcode = "sbb";
                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last) + r8(memory[1]);
                        offset += last - 1;
                    }
                    break;

                case 0x19:
                    {
                        description = "integer subtraction with borrow";
                        lastdisassembledata.opcode = "sbb";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last) + r16(memory[1]);
                        else
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + r32(memory[1]);
                        offset += last - 1;
                    }
                    break;

                case 0x1a:
                    {
                        description = "integer subtraction with borrow";
                        lastdisassembledata.opcode = "sbb";
                        lastdisassembledata.parameters = r8(memory[1]) + modrm(memory, prefix2, 1, 2, last, 8, 0, mright);

                        offset += last - 1;
                    }
                    break;

                case 0x1b:
                    {
                        description = "integer subtraction with borrow";
                        lastdisassembledata.opcode = "sbb";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = r16(memory[1]) + modrm(memory, prefix2, 1, 1, last, mright);
                        else
                            lastdisassembledata.parameters = r32(memory[1]) + modrm(memory, prefix2, 1, 0, last, mright);


                        offset += last - 1;
                    }
                    break;

                case 0x1c:
                    {
                        description = "integer subtraction with borrow";
                        lastdisassembledata.opcode = "sbb";
                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = memory[1];
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parameters = colorreg + "al" + endcolor + ',' + inttohexs(memory[1], 2);


                        offset += 1;
                    }
                    break;

                case 0x1d:
                    {
                        lastdisassembledata.opcode = "sbb";
                        description = "integer subtraction with borrow";
                        if (prefix2.has(0x66))
                        {
                            wordptr = &memory[1];

                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = wordptr;

                            lastdisassembledata.parameters = colorreg + "ax" + endcolor + ',' + inttohexs(wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            dwordptr = &memory[1];
                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = dwordptr;

                            if (rex_w)
                                lastdisassembledata.parameters = colorreg + "rax" + endcolor + ',' + inttohexs((longint)(lastdisassembledata.parametervalue), 8);
                            else
                                lastdisassembledata.parameters = colorreg + "eax" + endcolor + ',' + inttohexs(lastdisassembledata.parametervalue, 8);

                            offset += 4;
                        }

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                    }
                    break;

                case 0x1e:
                    {
                        description = "place ds on the stack";
                        lastdisassembledata.opcode = "push";
                        lastdisassembledata.parameters = colorreg + "ds" + endcolor;
                    }
                    break;

                case 0x1f:
                    {
                        description = "remove ds from the stack";
                        lastdisassembledata.opcode = "pop";
                        lastdisassembledata.parameters = colorreg + "ds" + endcolor;
                    }
                    break;

                case 0x20:
                    {
                        description = "logical and";
                        lastdisassembledata.opcode = "and";
                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last) + r8(memory[1]);
                        offset += last - 1;
                    }
                    break;

                case 0x21:
                    {
                        description = "logical and";
                        lastdisassembledata.opcode = "and";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last) + r16(memory[1]);
                        else
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + r32(memory[1]);
                        offset += last - 1;

                    }
                    break;

                case 0x22:
                    {
                        description = "logical and";
                        lastdisassembledata.opcode = "and";
                        lastdisassembledata.parameters = r8(memory[1]) + modrm(memory, prefix2, 1, 2, last, mright);
                        offset += last - 1;
                    }
                    break;

                case 0x23:
                    {
                        description = "logical and";
                        lastdisassembledata.opcode = "and";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = r16(memory[1]) + modrm(memory, prefix2, 1, 1, last, mright);
                        else
                            lastdisassembledata.parameters = r32(memory[1]) + modrm(memory, prefix2, 1, 0, last, mright);

                        offset += last - 1;
                    }
                    break;


                case 0x24:
                    {
                        description = "logical and";
                        lastdisassembledata.opcode = "and";
                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = memory[1];
                        lastdisassembledata.parameters = colorreg + "al" + endcolor + ',' + inttohexs(memory[1], 2);

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;


                        offset += 1;
                    }
                    break;

                case 0x25:
                    {
                        description = "logical and";
                        lastdisassembledata.opcode = "and";
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;


                        if (prefix2.has(0x66))
                        {
                            wordptr = &memory[1];
                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = wordptr;
                            lastdisassembledata.parameters = colorreg + "ax" + endcolor + ',' + inttohexs(lastdisassembledata.parametervalue, 4);
                            offset += 2;
                        }
                        else
                        {
                            dwordptr = &memory[1];
                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = dwordptr;

                            if (rex_w)
                                lastdisassembledata.parameters = colorreg + "rax" + endcolor + ',' + inttohexs((longint)(lastdisassembledata.parametervalue), 8);
                            else
                                lastdisassembledata.parameters = colorreg + "eax" + endcolor + ',' + inttohexs(lastdisassembledata.parametervalue, 8);
                            offset += 4;
                        }
                    }
                    break;

                case 0x27:
                    {
                        description = "decimal adjust al after addition";
                        lastdisassembledata.opcode = "daa";
                    }
                    break;

                case 0x28:
                    {
                        description = "subtract";
                        lastdisassembledata.opcode = "sub";
                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last) + r8(memory[1]);
                        offset += last - 1;
                    }
                    break;

                case 0x29:
                    {
                        description = "subtract";
                        lastdisassembledata.opcode = "sub";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last) + r16(memory[1]);
                        else
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + r32(memory[1]);
                        offset += last - 1;

                    }
                    break;

                case 0x2a:
                    {
                        description = "subtract";
                        lastdisassembledata.opcode = "sub";
                        lastdisassembledata.parameters = r8(memory[1]) + modrm(memory, prefix2, 1, 2, last, mright);

                        offset += last - 1;
                    }
                    break;

                case 0x2b:
                    {
                        description = "subtract";
                        lastdisassembledata.opcode = "sub";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = r16(memory[1]) + modrm(memory, prefix2, 1, 1, last, mright);
                        else
                            lastdisassembledata.parameters = r32(memory[1]) + modrm(memory, prefix2, 1, 0, last, mright);

                        offset += last - 1;
                    }
                    break;

                case 0x2c:
                    {
                        description = "subtract";
                        lastdisassembledata.opcode = "sub";

                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = memory[1];
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parameters = colorreg + "al" + endcolor + ',' + inttohexs(memory[1], 2);



                        offset += 1;
                    }
                    break;

                case 0x2d:
                    {
                        description = "subtract";
                        lastdisassembledata.opcode = "sub";


                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        if (prefix2.has(0x66))
                        {
                            wordptr = &memory[1];
                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = wordptr;

                            lastdisassembledata.parameters = colorreg + "ax" + endcolor + ',' + inttohexs(wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            dwordptr = &memory[1];
                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = dwordptr;


                            if (rex_w)
                                lastdisassembledata.parameters = colorreg + "rax" + endcolor + ',' + inttohexs((longint)(dwordptr), 8);
                            else
                                lastdisassembledata.parameters = colorreg + "eax" + endcolor + ',' + inttohexs(dwordptr, 8);
                            offset += 4;
                        }
                    }
                    break;


                case 0x2f:
                    {
                        description = "decimal adjust al after subtraction";
                        lastdisassembledata.opcode = "das";
                    }
                    break;

                case 0x30:
                    {
                        description = "logical exclusive or";
                        lastdisassembledata.opcode = "xor";
                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last) + r8(memory[1]);
                        offset += last - 1;
                    }
                    break;

                case 0x31:
                    {
                        description = "logical exclusive or";
                        lastdisassembledata.opcode = "xor";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last) + r16(memory[1]);
                        else
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + r32(memory[1]);
                        offset += last - 1;

                    }
                    break;

                case 0x32:
                    {
                        description = "logical exclusive or";
                        lastdisassembledata.opcode = "xor";
                        lastdisassembledata.parameters = r8(memory[1]) + modrm(memory, prefix2, 1, 2, last, mright);

                        offset += last - 1;
                    }
                    break;

                case 0x33:
                    {
                        description = "logical exclusive or";
                        lastdisassembledata.opcode = "xor";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = r16(memory[1]) + modrm(memory, prefix2, 1, 1, last, mright);
                        else
                            lastdisassembledata.parameters = r32(memory[1]) + modrm(memory, prefix2, 1, 0, last, mright);

                        offset += last - 1;
                    }
                    break;

                case 0x34:
                    {
                        description = "logical exclusive or";
                        lastdisassembledata.opcode = "xor";
                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = memory[1];
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parameters = colorreg + "al" + endcolor + ',' + inttohexs(memory[1], 2);
                        offset += 1;
                    }
                    break;

                case 0x35:
                    {
                        description = "logical exclusive or";
                        lastdisassembledata.opcode = "xor";


                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        if (prefix2.has(0x66))
                        {
                            wordptr = &memory[1];
                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = wordptr;

                            lastdisassembledata.parameters = colorreg + "ax" + endcolor + ',' + inttohexs(wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            dwordptr = &memory[1];
                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = dwordptr;

                            if (rex_w)
                                lastdisassembledata.parameters = colorreg + "rax" + endcolor + ',' + inttohexs((longint)(dwordptr), 8);
                            else
                                lastdisassembledata.parameters = colorreg + "eax" + endcolor + ',' + inttohexs(dwordptr, 8);
                            offset += 4;
                        }
                    }
                    break;


                case 0x37:
                    {  //aaa
                        lastdisassembledata.opcode = "aaa";
                        description = "ascii adjust al after addition";
                    }
                    break;

                //---------
                case 0x38:
                    {//cmp
                        description = "compare two operands";
                        lastdisassembledata.opcode = "cmp";
                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last) + r8(memory[1]);
                        offset += last - 1;
                    }
                    break;

                case 0x39:
                    {
                        description = "compare two operands";
                        lastdisassembledata.opcode = "cmp";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last) + r16(memory[1]);
                        else
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + r32(memory[1]);

                        offset += last - 1;

                    }
                    break;

                case 0x3a:
                    {
                        description = "compare two operands";
                        lastdisassembledata.opcode = "cmp";
                        lastdisassembledata.parameters = r8(memory[1]) + modrm(memory, prefix2, 1, 2, last, mright);

                        offset += last - 1;
                    }
                    break;

                case 0x3b:
                    {
                        description = "compare two operands";
                        lastdisassembledata.opcode = "cmp";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = r16(memory[1]) + modrm(memory, prefix2, 1, 1, last, mright);
                        else
                            lastdisassembledata.parameters = r32(memory[1]) + modrm(memory, prefix2, 1, 0, last, mright);

                        offset += last - 1;
                    }
                    break;

                //---------

                case 0x3c:
                    {
                        description = "compare two operands";
                        lastdisassembledata.opcode = "cmp";

                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = memory[1];
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parameters = colorreg + "al" + endcolor + ',' + inttohexs(memory[1], 2);
                        offset += 1;
                    }
                    break;

                case 0x3d:
                    {
                        description = "compare two operands";
                        lastdisassembledata.opcode = "cmp";
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;


                        if (prefix2.has(0x66))
                        {
                            wordptr = &memory[1];
                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = wordptr;

                            lastdisassembledata.parameters = colorreg + "ax" + endcolor + ',' + inttohexs(wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            dwordptr = &memory[1];
                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = dwordptr;


                            if (rex_w)
                                lastdisassembledata.parameters = colorreg + "rax" + endcolor + ',' + inttohexs((longint)(dwordptr), 8);
                            else
                                lastdisassembledata.parameters = colorreg + "eax" + endcolor + ',' + inttohexs(dwordptr, 8);
                            offset += 4;
                        }
                    }
                    break;

                //prefix bytes need fixing
                case 0x3f:
                    {  //aas
                        if (processhandler.is64bit)
                        {
                            lastdisassembledata.opcode = "db";
                            lastdisassembledata.parameters = inttohexs(0x3f, 1);
                        }
                        else
                        {
                            lastdisassembledata.opcode = "aas";
                            description = "ascii adjust al after subtraction";
                        }
                    }
                    break;

                case RANGE_8(0x40, 0x47):
                    {
                        description = "increment by 1";
                        lastdisassembledata.opcode = "inc";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = rd16(memory[0] - 0x40);
                        else
                            lastdisassembledata.parameters = rd(memory[0] - 0x40);
                    }
                    break;

                case RANGE_8(0x48, 0x4f):
                    {
                        description = "decrement by 1";
                        lastdisassembledata.opcode = "dec";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = rd16(memory[0] - 0x48);
                        else
                            lastdisassembledata.parameters = rd(memory[0] - 0x48);
                    }
                    break;

                case RANGE_8(0x50, 0x57):
                    {
                        description = "push word or doubleword onto the stack";

                        if (is64bit) opcodeflags.w = true;

                        lastdisassembledata.opcode = "push";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = rd16(memory[0] - 0x50);
                        else
                            lastdisassembledata.parameters = rd(memory[0] - 0x50);
                    }
                    break;

                case RANGE_8(0x58, 0x5f):
                    {
                        description = "pop a value from the stack";
                        if (is64bit) opcodeflags.w = true; //so rd will pick the 64-bit version
                        lastdisassembledata.opcode = "pop";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = rd16(memory[0] - 0x58);
                        else
                            lastdisassembledata.parameters = rd(memory[0] - 0x58);
                    }
                    break;

                case 0x60:
                    {
                        description = "push all general-purpose registers";
                        if (is64bit) description = description + " (invalid)";
                        if (prefix2.has(0x66)) lastdisassembledata.opcode = "pusha";
                        else
                            lastdisassembledata.opcode = "pushad";

                        if (is64bit)
                        {
                            description = description + " (invalid)";
                            lastdisassembledata.opcode = "pushad (invalid)";
                        }
                    }
                    break;

                case 0x61:
                    {
                        description = "pop all general-purpose registers";
                        if (prefix2.has(0x66)) lastdisassembledata.opcode = "popa";
                        else
                            lastdisassembledata.opcode = "popad";

                        if (is64bit)
                        {
                            description = description + " (invalid)";
                            lastdisassembledata.opcode = "popad (invalid)";
                        }

                    }
                    break;

                case 0x62:
                    {
                        //bound
                        description = "check array index against bounds";
                        lastdisassembledata.opcode = "bound";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = r16(memory[1]) + modrm(memory, prefix2, 1, 1, last, mright);
                        else
                            lastdisassembledata.parameters = r32(memory[1]) + modrm(memory, prefix2, 1, 0, last, mright);

                        offset += last - 1;

                    }
                    break;

                case 0x63:
                    {
                        //arpl or movsxd
                        if (is64bit)
                        {
                            lastdisassembledata.opcode = "movsxd";
                            opcodeflags.w = false;

                            lastdisassembledata.parameters = string(' ') + r64(memory[1]) + modrm(memory, prefix2, 1, 0, last, 32, 0, mright);
                            offset += last - 1;
                            description = "Move doubleword to quadword with signextension";
                        }
                        else
                        {
                            lastdisassembledata.opcode = "arpl";
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last) + r16(memory[1]);
                            offset += last - 1;
                            description = "adjust rpl field of segment selector";
                        }
                    }
                    break;

                case 0x68:
                    {
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        if (prefix2.has(0x66))
                        {
                            wordptr = &memory[1];
                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = wordptr;

                            lastdisassembledata.opcode = "push";
                            lastdisassembledata.parameters = inttohexs(wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            dwordptr = &memory[1];
                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = dwordptr;

                            lastdisassembledata.opcode = "push";
                            if (processhandler.is64bit)
                                lastdisassembledata.parameters = inttohexs((longint)(dwordptr), 8);
                            else
                                lastdisassembledata.parameters = inttohexs(dwordptr, 8);
                            offset += 4;
                        }
                        description = "push word or doubleword onto the stack (sign extended)";
                    }
                    break;

                case 0x69:
                    {
                        description = "signed multiply";
                        if (prefix2.has(0x66))
                        {
                            lastdisassembledata.opcode = "imul";
                            lastdisassembledata.parameters = r16(memory[1]) + modrm(memory, prefix2, 1, 1, last, mright);
                            wordptr = &memory[last];

                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = wordptr;

                            offset += last - 1 + 2;
                        }
                        else
                        {
                            lastdisassembledata.opcode = "imul";
                            lastdisassembledata.parameters = r32(memory[1]) + modrm(memory, prefix2, 1, 0, last, mright);
                            dwordptr = &memory[last];
                            if (rex_w)
                                lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohexs((longint)(dwordptr), 8);
                            else
                                lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohexs(dwordptr, 8);

                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = dwordptr;

                            offset += last - 1 + 4;
                        }
                    }
                    break;

                case 0x6a:
                    {
                        lastdisassembledata.opcode = "push";

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = memory[1];

                        lastdisassembledata.parameters = inttohexs(memory[1], 2, true, 1);
                        offset += 1;
                        description = "push byte onto the stack";
                    }
                    break;

                case 0x6b:
                    {

                        description = "signed multiply";
                        lastdisassembledata.opcode = "imul";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = r16(memory[1]) + modrm(memory, prefix2, 1, 1, last, mright);
                        else
                            lastdisassembledata.parameters = r32(memory[1]) + modrm(memory, prefix2, 1, 0, last, mright);

                        lastdisassembledata.parametervalue = memory[last];
                        lastdisassembledata.parameters = lastdisassembledata.parameters + ',' + inttohexs(memory[last], 2);
                        offset += last - 1 + 1;
                    }
                    break;

                case 0x6c:
                    {
                        //m8, dx
                        description = "input from port to string";
                        lastdisassembledata.opcode = "insb";
                    }
                    break;

                case 0x6d:
                    {
                        //m8, dx
                        description = "input from port to string";
                        if (prefix2.has(0x66)) lastdisassembledata.opcode = "insw";
                        else
                            lastdisassembledata.opcode = "insd";
                    }
                    break;

                case 0x6e:
                    {
                        //m8, dx
                        description = "output string to port";
                        lastdisassembledata.opcode = "outsb";
                    }
                    break;

                case 0x6f:
                    {
                        //m8, dx
                        description = "output string to port";
                        if (prefix2.has(0x66)) lastdisassembledata.opcode = "outsw";
                        else
                            lastdisassembledata.opcode = "outsd";
                    }
                    break;


                case 0x70:
                    {
                        description = "jump short if overflow (OF=1)";
                        lastdisassembledata.opcode = "jo";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;
                        if (context != nil)
                            lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_of) != 0;

                        offset += 1;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parametervaluetype = dvtaddress;

                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword((shortint)(memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword((shortint)(memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);



                    }
                    break;

                case 0x71:
                    {
                        description = "jump short if not overflow (OF=0)";
                        lastdisassembledata.opcode = "jno";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;
                        if (context != nil)
                            lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_of) == 0;

                        offset += 1;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parametervaluetype = dvtaddress;

                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword((shortint)(memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword((shortint)(memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                    }
                    break;

                case 0x72:
                    {
                        description = "jump short if below/carry (CF=1)";
                        lastdisassembledata.opcode = "jb";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;
                        if (context != nil)
                            lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_cf) != 0;
                        offset += 1;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parametervaluetype = dvtaddress;

                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword((shortint)(memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword((shortint)(memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                    }
                    break;

                case 0x73:
                    {
                        description = "jump short if above or equal (CF=0)";
                        lastdisassembledata.opcode = "jae";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;
                        if (context != nil)
                            lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_cf) == 0;

                        offset += 1;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parametervaluetype = dvtaddress;

                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword((shortint)(memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword((shortint)(memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                    }
                    break;

                case 0x74:
                    {
                        description = "jump short if equal (ZF=1)";
                        lastdisassembledata.opcode = "je";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;
                        if (context != nil)
                            lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_zf) != 0;

                        offset += 1;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parametervaluetype = dvtaddress;

                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword((shortint)(memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword((shortint)(memory[1])));



                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                    }
                    break;

                case 0x75:
                    {
                        description = "jump short if not equal (ZF=0)";
                        lastdisassembledata.opcode = "jne";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;
                        if (context != nil)
                            lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_zf) == 0;
                        offset += 1;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parametervaluetype = dvtaddress;

                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword((shortint)(memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword((shortint)(memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                    }
                    break;

                case 0x76:
                    {
                        description = "jump short if not above (ZF=1 or CF=1)";
                        lastdisassembledata.opcode = "jna";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;
                        if (context != nil)
                            lastdisassembledata.willjumpaccordingtocontext = (context->eflags & (eflags_cf | eflags_zf)) != 0;


                        offset += 1;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parametervaluetype = dvtaddress;

                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword((shortint)(memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword((shortint)(memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                    }
                    break;

                case 0x77:
                    {
                        description = "jump short if above (ZF=0 and CF=0)";
                        lastdisassembledata.opcode = "ja";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;
                        if (context != nil)
                            lastdisassembledata.willjumpaccordingtocontext = (context->eflags & (eflags_cf | eflags_zf)) == 0;


                        offset += 1;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parametervaluetype = dvtaddress;

                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword((shortint)(memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword((shortint)(memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                    }
                    break;

                case 0x78:
                    {
                        description = "jump short if sign (SF=1)";
                        lastdisassembledata.opcode = "js";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;
                        if (context != nil)
                            lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_sf) != 0;

                        offset += 1;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parametervaluetype = dvtaddress;

                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword((shortint)(memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword((shortint)(memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                    }
                    break;

                case 0x79:
                    {
                        description = "jump short if not sign (SF=0)";
                        lastdisassembledata.opcode = "jns";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;
                        if (context != nil)
                            lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_sf) == 0;

                        offset += 1;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parametervaluetype = dvtaddress;

                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword((shortint)(memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword((shortint)(memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                    }
                    break;

                case 0x7a:
                    {
                        description = "jump short if parity (PF=1)";
                        lastdisassembledata.opcode = "jp";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;
                        if (context != nil)
                            lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_pf) != 0;

                        offset += 1;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parametervaluetype = dvtaddress;

                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword((shortint)(memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword((shortint)(memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                    }
                    break;

                case 0x7b:
                    {
                        description = "jump short if not parity (PF=0)";
                        lastdisassembledata.opcode = "jnp";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;
                        if (context != nil)
                            lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_pf) == 0;

                        offset += 1;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parametervaluetype = dvtaddress;

                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword((shortint)(memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword((shortint)(memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                    }
                    break;

                case 0x7c:
                    {
                        description = "jump short if not greater or equal (SF~=OF)";
                        lastdisassembledata.opcode = "jl"; //jnge
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;
                        if (context != nil)
                            lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_sf) != (context->eflags & eflags_of);


                        offset += 1;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parametervaluetype = dvtaddress;

                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword((shortint)(memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword((shortint)(memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                    }
                    break;

                case 0x7d:
                    {
                        description = "jump short if not less (greater or equal) (SF=OF)";
                        lastdisassembledata.opcode = "jnl";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;
                        if (context != nil)
                            lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_sf) == (context->eflags & eflags_of);


                        offset += 1;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parametervaluetype = dvtaddress;

                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword((shortint)(memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword((shortint)(memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                    }
                    break;

                case 0x7e:
                    {
                        description = "jump short if less or equal (ZF=1 or SF~=OF)";
                        lastdisassembledata.opcode = "jle";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;
                        if (context != nil)
                            lastdisassembledata.willjumpaccordingtocontext = ((context->eflags & eflags_sf) != (context->eflags & eflags_of)) || ((context->eflags & eflags_zf) != 0);


                        offset += 1;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword((shortint)(memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword((shortint)(memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);
                        lastdisassembledata.parametervaluetype = dvtaddress;
                    }
                    break;

                case 0x7f:
                    {
                        description = "jump short if greater (ZF=0 or SF=OF)";
                        lastdisassembledata.opcode = "jg";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;
                        if (context != nil)
                            lastdisassembledata.willjumpaccordingtocontext = ((context->eflags & eflags_sf) == (context->eflags & eflags_of)) && ((context->eflags & eflags_zf) == 0);


                        offset += 1;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword((shortint)(memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword((shortint)(memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);
                        lastdisassembledata.parametervaluetype = dvtaddress;
                    }
                    break;

                case 0x80:
                case 0x82:
                    {
                        switch (getreg(memory[1]))
                        {
                            case 0:
                                {
                                    //add
                                    lastdisassembledata.opcode = "add";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[last];
                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    offset = offset + last;
                                    description = "add x to y";
                                }
                                break;

                            case 1:
                                {
                                    //adc
                                    lastdisassembledata.opcode = "or";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[last];
                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    offset = offset + last;
                                    description = "logical inclusive or";
                                }
                                break;


                            case 2:
                                {
                                    //adc
                                    lastdisassembledata.opcode = "adc";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[last];
                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    offset = offset + last;
                                    description = "add with carry";
                                }
                                break;

                            case 3:
                                {
                                    //sbb
                                    lastdisassembledata.opcode = "sbb";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[last];
                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    offset = offset + last;
                                    description = "integer subtraction with borrow";
                                }
                                break;

                            case 4:
                                {
                                    //and
                                    lastdisassembledata.opcode = "and";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[last];
                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    offset = offset + last;
                                    description = "logical and";
                                }
                                break;

                            case 5:
                                {
                                    lastdisassembledata.opcode = "sub";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[last];
                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    offset = offset + last;
                                    description = "subtract";
                                }
                                break;

                            case 6:
                                {
                                    lastdisassembledata.opcode = "xor";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[last];
                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    offset = offset + last;
                                    description = "logical exclusive or";
                                }
                                break;

                            case 7:
                                {
                                    lastdisassembledata.opcode = "cmp";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[last];
                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    offset = offset + last;
                                    description = "compare two operands";
                                }
                                break;

                        }
                    }
                    break;

                case 0x81:
                    {
                        switch (getreg(memory[1]))
                        {
                            case 0:
                                {
                                    //add
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "add";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        wordptr = &memory[last];
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = wordptr;

                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(wordptr, 4);
                                        offset += last - 1 + 2;
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "add";
                                        if (rex_w)
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                        else
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);
                                        dwordptr = &memory[last];
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = dwordptr;

                                        if (rex_w)
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs((longint)(dwordptr), 8);
                                        else
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(dwordptr, 8);
                                        offset += last - 1 + 4;
                                    }

                                    //                      offset:=offset+last;
                                    description = "add x to y";
                                }
                                break;

                            case 1:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "or";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        wordptr = &memory[last];
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = wordptr;

                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(wordptr, 4);
                                        offset += last - 1 + 2;
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "or";
                                        if (rex_w)
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                        else
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                        dwordptr = &memory[last];
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = dwordptr;

                                        if (rex_w)
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs((longint)(dwordptr), 8);
                                        else
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(dwordptr, 8);
                                        offset += last - 1 + 4;
                                    }


                                    description = "logical inclusive or";
                                }
                                break;

                            case 2:
                                {
                                    //adc
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "adc";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        wordptr = &memory[last];
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = wordptr;

                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(wordptr, 4);
                                        offset += last - 1 + 2;
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "adc";
                                        if (rex_w)
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                        else
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);
                                        dwordptr = &memory[last];
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = dwordptr;

                                        if (rex_w)
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs((longint)(dwordptr), 8);
                                        else
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(dwordptr, 8);
                                        offset += last - 1 + 4;
                                    }


                                    description = "add with carry";
                                }
                                break;

                            case 3:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "sbb";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        wordptr = &memory[last];
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = wordptr;

                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(wordptr, 4);
                                        offset += last - 1 + 2;
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "sbb";
                                        if (rex_w)
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                        else
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);
                                        dwordptr = &memory[last];
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = dwordptr;

                                        if (rex_w)
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs((longint)(dwordptr), 8);
                                        else
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(dwordptr, 8);
                                        offset += last - 1 + 4;
                                    }


                                    description = "integer subtraction with borrow";
                                }
                                break;


                            case 4:
                                {
                                    //and
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "and";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        wordptr = &memory[last];
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = wordptr;

                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(wordptr, 4);
                                        offset += last - 1 + 2;
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "and";
                                        if (rex_w)
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                        else
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);
                                        dwordptr = &memory[last];
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = dwordptr;

                                        if (rex_w)
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs((longint)(dwordptr), 8);
                                        else
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(dwordptr, 8);
                                        offset += last - 1 + 4;
                                    }


                                    description = "logical and";
                                }
                                break;

                            case 5:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "sub";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last);
                                        wordptr = &memory[last];
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = wordptr;

                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(wordptr, 4);
                                        offset += last - 1 + 2;
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "sub";
                                        if (rex_w)
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                        else
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);
                                        dwordptr = &memory[last];
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = dwordptr;

                                        if (rex_w)
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs((longint)(dwordptr), 8);
                                        else
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(dwordptr, 8);
                                        offset += last - 1 + 4;
                                    }


                                    description = "subtract";
                                }
                                break;

                            case 6:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "xor";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        wordptr = &memory[last];
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = wordptr;

                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(wordptr, 4);
                                        offset += last - 1 + 2;
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "xor";
                                        if (rex_w)
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                        else
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);
                                        dwordptr = &memory[last];
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = dwordptr;

                                        if (rex_w)
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs((longint)(dwordptr), 8);
                                        else
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(dwordptr, 8);
                                        offset += last - 1 + 4;
                                    }
                                    description = "logical exclusive or";
                                }
                                break;

                            case 7:
                                {
                                    //cmp
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "cmp";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        wordptr = &memory[last];
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = wordptr;

                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(wordptr, 4);
                                        offset += last - 1 + 2;
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "cmp";
                                        if (rex_w)
                                        {
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                            lastdisassembledata.datasize = 8; ;
                                        }
                                        else
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);
                                        dwordptr = &memory[last];
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = dwordptr;

                                        if (rex_w)
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs((longint)(dwordptr), 8);
                                        else
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(dwordptr, 8);
                                        offset += last - 1 + 4;
                                    }

                                    description = "compare two operands";
                                }
                                break;


                        }
                    }
                    break;

                case 0x83:
                    {
                        switch (getreg(memory[1]))
                        {
                            case 0:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "add";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];

                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2, true);
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "add";

                                        if (rex_w)
                                        {
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = memory[last];
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2, true);
                                        }
                                        else
                                        {
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = memory[last];

                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2, true);
                                        }

                                    }

                                    offset += last;
                                    description = "add (sign extended)";
                                }
                                break;

                            case 1:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "or";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];

                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "or";
                                        if (rex_w)
                                        {
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = memory[last];
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        }
                                        else
                                        {
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = memory[last];
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        }
                                    }

                                    offset += last;
                                    description = "add (sign extended)";
                                }
                                break;


                            case 2:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "adc";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "adc";
                                        if (rex_w)
                                        {
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = memory[last];
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        }
                                        else
                                        {
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = memory[last];
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        }

                                    }

                                    offset += last;
                                    description = "add with carry (sign extended)";
                                }
                                break;

                            case 3:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "sbb";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];

                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "sbb";
                                        if (rex_w)
                                        {
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = memory[last];
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        }
                                        else
                                        {
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = memory[last];
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        }
                                    }

                                    offset += last;
                                    description = "integer subtraction with borrow (sign extended)";
                                }
                                break;

                            case 4:
                                {
                                    //and
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "and";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "and";
                                        if (rex_w)
                                        {
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = memory[last];
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        }
                                        else
                                        {
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = memory[last];
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        }

                                    }

                                    offset = offset + last;
                                    description = "logical and (sign extended)";
                                }
                                break;

                            case 5:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "sub";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "sub";
                                        if (rex_w)
                                        {
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = memory[last];
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        }
                                        else
                                        {
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = memory[last];
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        }
                                    }

                                    offset = offset + last;
                                    description = "subtract";
                                }
                                break;

                            case 6:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "xor";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "xor";
                                        if (rex_w)
                                        {
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = memory[last];
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        }
                                        else
                                        {
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = memory[last];
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        }
                                    }

                                    offset = offset + last;
                                    description = "logical exclusive or";
                                }
                                break;

                            case 7:
                                {
                                    //cmp
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "cmp";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "cmp";
                                        if (rex_w)
                                        {
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = memory[last];
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        }
                                        else
                                        {
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = memory[last];
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        }
                                    }

                                    offset = offset + last;
                                    description = "compare two operands";
                                }
                                break;


                        }
                    }
                    break;

                case 0x84:
                    {
                        description = "logical compare";
                        lastdisassembledata.opcode = "test";
                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last) + r8(memory[1]);
                        offset += last - 1;
                    }
                    break;

                case 0x85:
                    {
                        description = "logical compare";
                        lastdisassembledata.opcode = "test";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last) + r16(memory[1]);
                        else
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + r32(memory[1]);
                        offset += last - 1;
                    }
                    break;

                case 0x86:
                    {
                        description = "exchange memory with register";
                        lastdisassembledata.opcode = "xchg";
                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last) + r8(memory[1]);
                        offset += last - 1;
                    }
                    break;

                case 0x87:
                    {
                        description = "exchange memory with register";
                        lastdisassembledata.opcode = "xchg";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last) + r16(memory[1]);
                        else
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + r32(memory[1]);
                        offset += last - 1;
                    }
                    break;

                case 0x88:
                    {
                        description = "copy memory";
                        lastdisassembledata.opcode = "mov";
                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last) + r8(memory[1]);
                        offset += last - 1;
                    }
                    break;

                case 0x89:
                    {
                        description = "copy memory";
                        lastdisassembledata.opcode = "mov";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last) + r16(memory[1]);
                        else
                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + r32(memory[1]);
                        offset += last - 1;
                    }
                    break;

                case 0x8a:
                    {
                        description = "copy memory";
                        lastdisassembledata.opcode = "mov";
                        lastdisassembledata.parameters = r8(memory[1]) + modrm(memory, prefix2, 1, 2, last, mright);

                        offset += last - 1;
                    }
                    break;

                case 0x8b:
                    {
                        description = "copy memory";
                        lastdisassembledata.opcode = "mov";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = r16(memory[1]) + modrm(memory, prefix2, 1, 1, last, mright);
                        else
                            lastdisassembledata.parameters = r32(memory[1]) + modrm(memory, prefix2, 1, 0, last, mright);

                        offset += last - 1;
                    }
                    break;

                case 0x8c:
                    {
                        description = "copy memory";
                        lastdisassembledata.opcode = "mov";
                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last) + sreg(memory[1]);
                        offset += last - 1;
                    }
                    break;

                case 0x8d:
                    {
                        description = "load effective address";
                        lastdisassembledata.opcode = "lea";
                        if (prefix2.has(0x66))
                        {
                            if (processhandler.is64bit & (prefix2.has(0x67)))
                                lastdisassembledata.parameters = r16(memory[1]) + modrm(memory, prefix2, 1, 1, last, 0, 32, mright);
                            else
                                lastdisassembledata.parameters = r16(memory[1]) + modrm(memory, prefix2, 1, 1, last, 0, 0, mright);
                        }
                        else
                        {
                            if (processhandler.is64bit & (prefix2.has(0x67)))
                                lastdisassembledata.parameters = r32(memory[1]) + modrm(memory, prefix2, 1, 0, last, 0, 32, mright);
                            else
                                lastdisassembledata.parameters = r32(memory[1]) + modrm(memory, prefix2, 1, 0, last, 0, 0, mright);
                        }

                        offset += last - 1;
                    }
                    break;

                case 0x8e:
                    {
                        description = "copy memory";
                        lastdisassembledata.opcode = "mov";
                        lastdisassembledata.parameters = sreg(memory[1]) + modrm(memory, prefix2, 1, 1, last, mright);

                        offset += last - 1;
                    }
                    break;

                case 0x8f:
                    {
                        switch (getreg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "pop a value from the stack";
                                    lastdisassembledata.opcode = "pop";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                    offset += last - 1;
                                }
                                break;

                            default:
                                {
                                    lastdisassembledata.opcode = "db";
                                    lastdisassembledata.parameters = colorhex + "8f" + endcolor;
                                    description = "undefined by the intel specification";
                                }
                        }
                    }
                    break;


                case 0x90:
                    {
                        description = "no operation";
                        lastdisassembledata.opcode = "nop";
                        if (prefixsize > 0)
                            lastdisassembledata.parameters = inttohexs(prefixsize + 1, 1);
                    }
                    break;

                case RANGE_7(0x91, 0x97):
                    {
                        description = "exchange register with register";
                        lastdisassembledata.opcode = "xchg";

                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = colorreg + "ax" + endcolor + ',' + rd16(memory[0] - 0x90);
                        else
                        {
                            if (rex_w)
                                lastdisassembledata.parameters = colorreg + "rax" + endcolor + ',' + rd(memory[0] - 0x90);
                            else
                                lastdisassembledata.parameters = colorreg + "eax" + endcolor + ',' + rd(memory[0] - 0x90);
                        }
                    }
                    break;


                case 0x98:
                    {
                        //cbw/cwde
                        if (prefix2.has(0x66))
                        {
                            lastdisassembledata.opcode = "cbw";
                            description = "convert byte to word";
                        }
                        else
                        {
                            if (rex_w)
                            {
                                lastdisassembledata.opcode = "cdqe";
                                description = "convert doubleword to quadword";
                            }
                            else
                            {
                                lastdisassembledata.opcode = "cwde";
                                description = "convert word to doubleword";
                            }
                        }
                    }
                    break;

                case 0x99:
                    {
                        if (prefix2.has(0x66))
                        {
                            description = "convert word to doubleword";
                            lastdisassembledata.opcode = "cwd";
                        }
                        else
                        {

                            if (rex_w)
                            {
                                lastdisassembledata.opcode = "cqo";
                                description = "convert quadword to octword";
                            }
                            else
                            {
                                lastdisassembledata.opcode = "cdq";
                                description = "convert doubleword to quadword";
                            }
                        }
                    }
                    break;

                case 0x9a:
                    {
                        description = "call procedure";
                        wordptr = &memory[5];

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 5;
                        lastdisassembledata.seperatorcount += 1;



                        if (is64bit)
                            lastdisassembledata.opcode = "call (invalid)";
                        else
                            lastdisassembledata.opcode = "call";


                        lastdisassembledata.parameters = inttohexs(wordptr, 4) + ':';
                        dwordptr = &memory[1];

                        lastdisassembledata.parametervaluetype = dvtaddress;
                        lastdisassembledata.parametervalue = dwordptr;

                        offset += 6;
                    }
                    break;

                case 0x9b:
                    {
                        switch (memory[1])
                        {

                            case 0xd9:
                                {
                                    switch (getreg(memory[2]))
                                    {
                                        case 6:
                                            {
                                                description = "store fpu environment";
                                                lastdisassembledata.opcode = "wait:fstenv";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                offset += last - 1;
                                            }
                                            break;


                                        case 7:
                                            {
                                                description = "store control word";
                                                lastdisassembledata.opcode = "wait:fstcw";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                offset += last - 1;
                                            }
                                            break;

                                        default:
                                            {
                                                description = "wait";
                                                lastdisassembledata.opcode = "wait";
                                            }

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
                                                lastdisassembledata.opcode = "wait:fclex";
                                                offset += 2;
                                            }
                                            break;

                                        case 0xe3:
                                            {
                                                description = "initialize floaring-point unit";
                                                lastdisassembledata.opcode = "wait:finit";
                                                offset += 2;
                                            }
                                            break;
                                        default:
                                            {
                                                description = "wait";
                                                lastdisassembledata.opcode = "wait";
                                            }
                                    }
                                }
                                break;

                            case 0xdd:
                                {
                                    switch (getreg(memory[2]))
                                    {
                                        case 6:
                                            {
                                                description = "store fpu state";
                                                lastdisassembledata.opcode = "wait:fsave";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                offset += last - 1;
                                            }
                                            break;

                                        case 7:
                                            {
                                                description = "store status word";
                                                lastdisassembledata.opcode = "wait:fstsw";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 2, 0, last);
                                                offset += last - 1;
                                            }
                                            break;

                                        default:
                                            {
                                                description = "wait";
                                                lastdisassembledata.opcode = "wait";
                                            }
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
                                                lastdisassembledata.opcode = "wait:fstsw ax";
                                                offset += 2;
                                            }
                                            break;

                                        default:
                                            {
                                                description = "wait";
                                                lastdisassembledata.opcode = "wait";
                                            }
                                    }
                                }
                                break;

                            default:
                                {
                                    description = "wait";
                                    lastdisassembledata.opcode = "wait";
                                }

                        }

                    }
                    break;

                case 0x9c:
                    {
                        description = "push eflags register onto the stack";
                        if (prefix2.has(0x66)) lastdisassembledata.opcode = "pushf";
                        else
                        {
                            if (is64bit)
                                lastdisassembledata.opcode = "pushfq";
                            else
                                lastdisassembledata.opcode = "pushfd";
                        }
                    }
                    break;

                case 0x9d:
                    {
                        description = "pop stack into eflags register";
                        if (prefix2.has(0x66)) lastdisassembledata.opcode = "popf";
                        else
                        {
                            if (is64bit)
                                lastdisassembledata.opcode = "popfq";
                            else
                                lastdisassembledata.opcode = "popfd";
                        }
                    }
                    break;

                case 0x9e:
                    {
                        description = "store ah into flags";
                        lastdisassembledata.opcode = "sahf";
                    }
                    break;

                case 0x9f:
                    {
                        description = "load status flag into ah register";
                        lastdisassembledata.opcode = "lahf";
                    }
                    break;

                case 0xa0:
                    {
                        description = "copy memory";
                        dwordptr = &memory[1];
                        lastdisassembledata.opcode = "mov";
                        lastdisassembledata.parametervaluetype = dvtaddress;
                        lastdisassembledata.parametervalue = dwordptr;
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;


                        if (processhandler.is64bit)
                        {
                            lastdisassembledata.parameters = colorreg + "al" + endcolor + ',' + getsegmentoverride(prefix2) + '[' + inttohexs(pqword(dwordptr), 8) + ']';
                            offset += 8;
                        }
                        else
                        {
                            lastdisassembledata.parameters = colorreg + "al" + endcolor + ',' + getsegmentoverride(prefix2) + '[' + inttohexs(dwordptr, 8) + ']';
                            offset += 4;
                        }


                    }
                    break;

                case 0xa1:
                    {
                        description = "copy memory";
                        lastdisassembledata.opcode = "mov";
                        dwordptr = &memory[1];


                        lastdisassembledata.parametervaluetype = dvtaddress;
                        lastdisassembledata.parametervalue = dwordptr;
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        if (prefix2.has(0x66))
                        {
                            lastdisassembledata.parameters = colorreg + "ax" + endcolor + ',' + getsegmentoverride(prefix2) + '[' + inttohexs(dwordptr, 8) + ']';
                        }
                        else
                        {
                            if (rex_w)
                                lastdisassembledata.parameters = colorreg + "rax" + endcolor + ',';
                            else
                                lastdisassembledata.parameters = colorreg + "eax" + endcolor + ',';


                            if (processhandler.is64bit)
                                lastdisassembledata.parameters = lastdisassembledata.parameters + getsegmentoverride(prefix2) + '[' + inttohexs(pqword(dwordptr), 8) + ']';
                            else
                                lastdisassembledata.parameters = lastdisassembledata.parameters + getsegmentoverride(prefix2) + '[' + inttohexs(dwordptr, 8) + ']';

                        }

                        if (processhandler.is64bit)
                            offset += 8;
                        else
                            offset += 4;

                    }
                    break;

                case 0xa2:
                    {
                        description = "copy memory";
                        dwordptr = &memory[1];
                        lastdisassembledata.opcode = "mov";

                        lastdisassembledata.parametervaluetype = dvtaddress;
                        lastdisassembledata.parametervalue = dwordptr;
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        if (processhandler.is64bit)
                            lastdisassembledata.parameters = getsegmentoverride(prefix2) + '[' + inttohexs(pqword(dwordptr), 8) + "]," + colorreg + "al" + endcolor;
                        else
                            lastdisassembledata.parameters = getsegmentoverride(prefix2) + '[' + inttohexs(dwordptr, 8) + "]," + colorreg + "al" + endcolor;

                        if (processhandler.is64bit)
                            offset += 8;
                        else
                            offset += 4;
                    }
                    break;

                case 0xa3:
                    {
                        description = "copy memory";
                        lastdisassembledata.opcode = "mov";
                        dwordptr = &memory[1];

                        lastdisassembledata.parametervaluetype = dvtaddress;
                        lastdisassembledata.parametervalue = dwordptr;
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        if (processhandler.is64bit)
                            lastdisassembledata.parameters = getsegmentoverride(prefix2) + '[' + inttohexs(pqword(dwordptr), 8) + "],";
                        else
                            lastdisassembledata.parameters = getsegmentoverride(prefix2) + '[' + inttohexs(dwordptr, 8) + "],";

                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = lastdisassembledata.parameters + colorreg + "ax" + endcolor;
                        else
                        {
                            if (rex_w)
                                lastdisassembledata.parameters = lastdisassembledata.parameters + colorreg + "rax" + endcolor;
                            else
                                lastdisassembledata.parameters = lastdisassembledata.parameters + colorreg + "eax" + endcolor;
                        }

                        if (processhandler.is64bit)
                            offset += 8;
                        else
                            offset += 4;
                    }
                    break;

                case 0xa4:
                    {
                        description = "move data from string to string";
                        lastdisassembledata.opcode = "movsb";
                    }
                    break;

                case 0xa5:
                    {
                        description = "move data from string to string";
                        if (prefix2.has(0x66))
                            lastdisassembledata.opcode = "movsw";
                        else
                        {
                            if (rex_w)
                                lastdisassembledata.opcode = "movsq";
                            else
                                lastdisassembledata.opcode = "movsd";
                        }
                    }
                    break;

                case 0xa6:
                    {
                        description = "compare string operands";
                        lastdisassembledata.opcode = "cmpsb";
                    }
                    break;

                case 0xa7:
                    {
                        description = "compare string operands";
                        if (prefix2.has(0x66))
                            lastdisassembledata.opcode = "cmpsw";
                        else
                        {
                            if (rex_w)
                                lastdisassembledata.opcode = "cmpsq";
                            else
                                lastdisassembledata.opcode = "cmpsd";
                        }
                    }
                    break;

                case 0xa8:
                    {
                        description = "logical compare";
                        lastdisassembledata.opcode = "test";

                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = memory[1];
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parameters = colorreg + "al" + endcolor + ',' + inttohexs(memory[1], 2);
                        offset += 1;
                    }
                    break;

                case 0xa9:
                    {
                        description = "logical compare";
                        lastdisassembledata.opcode = "test";

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        if (prefix2.has(0x66))
                        {
                            wordptr = &memory[1];
                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = wordptr;

                            lastdisassembledata.parameters = colorreg + "ax" + endcolor + ',' + inttohexs(wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            dwordptr = &memory[1];
                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = dwordptr;

                            if (rex_w)
                                lastdisassembledata.parameters = colorreg + "rax" + endcolor + ',' + inttohexs((longint)(dwordptr), 8);
                            else
                                lastdisassembledata.parameters = colorreg + "eax" + endcolor + ',' + inttohexs(dwordptr, 8);
                            offset += 4;
                        }
                    }
                    break;

                case 0xaa:
                    {
                        description = "store string";
                        lastdisassembledata.opcode = "stosb";
                    }
                    break;

                case 0xab:
                    {
                        description = "store string";
                        if (prefix2.has(0x66)) lastdisassembledata.opcode = "stosw";
                        else
                        {
                            if (rex_w)
                                lastdisassembledata.opcode = "stosq";
                            else
                                lastdisassembledata.opcode = "stosd";
                        }
                    }
                    break;

                case 0xac:
                    {
                        description = "load string";
                        lastdisassembledata.opcode = "lodsb";
                    }
                    break;

                case 0xad:
                    {
                        description = "load string";
                        if (prefix2.has(0x66)) lastdisassembledata.opcode = "lodsw";
                        else
                        {
                            if (rex_w)
                                lastdisassembledata.opcode = "lodsq";
                            else
                                lastdisassembledata.opcode = "lodsd";
                        }
                    }
                    break;

                case 0xae:
                    {
                        description = "compare al with byte at es:edi and set status flag";
                        lastdisassembledata.opcode = "scasb";
                    }
                    break;

                case 0xaf:
                    {
                        description = "scan string";
                        if (prefix2.has(0x66)) lastdisassembledata.opcode = "scasw";
                        else
                        {
                            if (rex_w)
                                lastdisassembledata.opcode = "scasq";
                            else
                                lastdisassembledata.opcode = "scasd";
                        }
                    }
                    break;

                case RANGE_8(0xb0, 0xb7):
                    {
                        description = "copy memory";
                        lastdisassembledata.opcode = "mov";
                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = memory[1];
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        //              if Rex_B

                        lastdisassembledata.parameters = rd8(memory[0] - 0xb0) + ',' + inttohexs(memory[1], 2);
                        offset += 1;
                    }
                    break;

                case RANGE_8(0xb8, 0xbf):
                    {
                        description = "copy memory";

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;


                        if (prefix2.has(0x66))
                        {
                            wordptr = &memory[1];
                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = wordptr;

                            lastdisassembledata.opcode = "mov";
                            lastdisassembledata.parameters = rd16(memory[0] - 0xb8) + ',' + inttohexs(wordptr, 4);
                            offset += 2;
                        }
                        else
                        {
                            dwordptr = &memory[1];
                            lastdisassembledata.parametervaluetype = dvtvalue;


                            if (rex_w)
                            {
                                lastdisassembledata.opcode = "mov";
                                lastdisassembledata.parametervalue = pqword(dwordptr);
                                lastdisassembledata.parameters = rd(memory[0] - 0xb8) + ',' + inttohexs(pqword(dwordptr), 16);
                                offset += 8;
                            }
                            else
                            {
                                lastdisassembledata.opcode = "mov";
                                lastdisassembledata.parametervalue = dwordptr;

                                lastdisassembledata.parameters = rd(memory[0] - 0xb8) + ',' + inttohexs(dwordptr, 8);
                                offset += 4;
                            }
                        }
                    }
                    break;

                case 0xc0:
                    {
                        switch (getreg(memory[1]))
                        {
                            case 0:
                                {
                                    lastdisassembledata.opcode = "rol";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);

                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[last];

                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    description = string("rotate eight bits left ") + inttostr(memory[last]) + " times";
                                    offset += last;
                                }
                                break;

                            case 1:
                                {
                                    lastdisassembledata.opcode = "ror";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[last];
                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    description = string("rotate eight bits right ") + inttostr(memory[last]) + " times";
                                    offset += last;
                                }
                                break;

                            case 2:
                                {
                                    lastdisassembledata.opcode = "rcl";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[last];
                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    description = string("rotate nine bits left ") + inttostr(memory[last]) + " times";
                                    offset += last;
                                }
                                break;

                            case 3:
                                {
                                    lastdisassembledata.opcode = "rcr";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[last];
                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    description = string("rotate nine bits right ") + inttostr(memory[last]) + " times";
                                    offset += last;
                                }
                                break;

                            case 4:
                                {
                                    lastdisassembledata.opcode = "shl";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[last];
                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    description = string("multiply by 2, ") + inttostr(memory[last]) + " times";
                                    offset += last;
                                }
                                break;

                            case 5:
                                {
                                    lastdisassembledata.opcode = "shr";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[last];
                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    description = string("unsigned divide by 2, ") + inttostr(memory[last]) + " times";
                                    offset += last;
                                }
                                break;

                            /*not in intel spec*/
                            case 6:
                                {
                                    lastdisassembledata.opcode = "rol";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[last];
                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    description = string("rotate eight bits left ") + inttostr(memory[last]) + " times";
                                    offset += last;
                                }
                                break;
                            /*^^^^^^^^^^^^^^^^^^*/

                            case 7:
                                {
                                    lastdisassembledata.opcode = "sar";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[last];
                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    description = string("signed divide by 2, ") + inttostr(memory[last]) + " times";
                                    offset += last;
                                }
                                break;

                        }
                    }
                    break;

                case 0xc1:
                    {
                        switch (getreg(memory[1]))
                        {
                            case 0:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "rol";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        description = string("rotate 16 bits left ") + inttostr(memory[last]) + " times";
                                        offset += last;
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "rol";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];

                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        description = string("rotate 32 bits left ") + inttostr(memory[last]) + " times";
                                        offset += last;
                                    }
                                }
                                break;

                            case 1:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "ror";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        description = string("rotate 16 bits right ") + inttostr(memory[last]) + " times";
                                        offset += last;
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "ror";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        description = string("rotate 32 bits right ") + inttostr(memory[last]) + " times";
                                        offset += last;
                                    }
                                }
                                break;

                            case 2:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "rcl";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        description = string("rotate 17 bits left ") + inttostr(memory[last]) + " times";
                                        offset += last;
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "rcl";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        description = string("rotate 33 bits left ") + inttostr(memory[last]) + " times";
                                        offset += last;
                                    }
                                }
                                break;

                            case 3:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "rcr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        description = string("rotate 17 bits right ") + inttostr(memory[last]) + " times";
                                        offset += last;
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "rcr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        description = string("rotate 33 bits right ") + inttostr(memory[last]) + " times";
                                        offset += last;
                                    }
                                }
                                break;

                            case 4:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "shl";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        description = string("multiply by 2 ") + inttostr(memory[last]) + " times";
                                        offset += last;
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "shl";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        description = string("multiply by 2 ") + inttostr(memory[last]) + " times";
                                        offset += last;
                                    }
                                }
                                break;

                            case 5:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "shr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        description = string("unsigned divide by 2 ") + inttostr(memory[last]) + " times";
                                        offset += last;
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "shr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        description = string("unsigned divide by 2 ") + inttostr(memory[last]) + " times";
                                        offset += last;
                                    }
                                }
                                break;

                            case 7:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "sar";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        description = string("signed divide by 2 ") + inttostr(memory[last]) + " times";
                                        offset += last;
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "sar";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];
                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        description = string("signed divide by 2 ") + inttostr(memory[last]) + " times";
                                        offset += last;
                                    }
                                }
                                break;

                        }
                    }
                    break;

                case 0xc2:
                    {

                        wordptr = &memory[1];
                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = wordptr;
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.opcode = "ret";
                        lastdisassembledata.isret = true;
                        lastdisassembledata.parameters = inttohexs(wordptr, 4);
                        offset += 2;

                        description = string("near return to calling procedure and pop ") + inttostr(lastdisassembledata.parametervalue) + " bytes from stack";


                    }
                    break;

                case 0xc3:
                    {
                        description = "near return to calling procedure";
                        lastdisassembledata.opcode = "ret";
                        lastdisassembledata.isret = true;
                    }
                    break;

                case 0xc4:
                    {
                        if (processhandler.is64bit == false)
                        {
                            description = "load far pointer";
                            lastdisassembledata.opcode = "les";
                            if (prefix2.has(0x66))
                                lastdisassembledata.parameters = r16(memory[1]) + modrm(memory, prefix2, 1, 1, last, mright);
                            else
                                lastdisassembledata.parameters = r32(memory[1]) + modrm(memory, prefix2, 1, 0, last, mright);

                            offset += last - 1;
                        }
                    }
                    break;

                case 0xc5:
                    {
                        if (processhandler.is64bit == false)
                        {
                            description = "load far pointer";
                            lastdisassembledata.opcode = "lds";
                            if (prefix2.has(0x66))
                                lastdisassembledata.parameters = r16(memory[1]) + modrm(memory, prefix2, 1, 1, last, mright);
                            else
                                lastdisassembledata.parameters = r32(memory[1]) + modrm(memory, prefix2, 1, 0, last, mright);

                            offset += last - 1;
                        }
                    }
                    break;

                case 0xc6:
                    {
                        if (memory[1] == 0xf8)
                        {
                            offset += 1;
                            lastdisassembledata.opcode = "xabort";
                            description = "transactional abort";

                            lastdisassembledata.parametervaluetype = dvtvalue;
                            lastdisassembledata.parametervalue = memory[2];
                            lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                            lastdisassembledata.seperatorcount += 1;
                            lastdisassembledata.parameters = inttohexs(memory[2], 2);

                        }
                        else
                            switch (getreg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "copy memory";
                                        lastdisassembledata.opcode = "mov";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);
                                        lastdisassembledata.parametervaluetype = dvtvalue;
                                        lastdisassembledata.parametervalue = memory[last];

                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                        offset += last;
                                    }
                                    break;

                                default:
                                    {
                                        description = "not defined by the intel documentation";
                                        lastdisassembledata.opcode = "db";
                                        lastdisassembledata.parameters = inttohexs(memory[0], 2);
                                    }
                            }
                    }
                    break;

                case 0xc7:
                    {
                        if (memory[1] == 0xf8)
                        {
                            description = "Transactional Begin";
                            lastdisassembledata.opcode = "xbegin";

                            if (markiprelativeinstructions)
                            {
                                lastdisassembledata.riprelative = 1;
                                riprelative = true;
                            }
                            offset += 4;
                            lastdisassembledata.parametervaluetype = dvtaddress;



                            if (is64bit)
                                lastdisassembledata.parametervalue = qword(offset + pinteger(&memory[2]));
                            else
                                lastdisassembledata.parametervalue = dword(offset + pinteger(&memory[2]));

                            lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                            lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                            lastdisassembledata.seperatorcount += 1;

                        }
                        else
                            switch (getreg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "copy memory";
                                        if (prefix2.has(0x66))
                                        {
                                            lastdisassembledata.opcode = "mov";
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);

                                            wordptr = &memory[last];
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = wordptr;

                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(wordptr, 4);
                                            offset += last + 1;
                                        }
                                        else
                                        {
                                            lastdisassembledata.opcode = "mov";

                                            if (rex_w)
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                            else
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                            dwordptr = &memory[last];
                                            lastdisassembledata.parametervaluetype = dvtvalue;
                                            lastdisassembledata.parametervalue = dwordptr;


                                            if (rex_w)
                                                lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs((longint)(dwordptr), 8);
                                            else
                                                lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(dwordptr, 8);

                                            offset += last + 3;
                                        }
                                    }
                                    break;

                                default:
                                    {
                                        description = "not defined by the intel documentation";
                                        lastdisassembledata.opcode = "db";
                                        lastdisassembledata.parameters = inttohexs(memory[0], 2);
                                    }

                            }
                    }
                    break;

                case 0xc8:
                    {
                        description = "make stack frame for procedure parameters";
                        wordptr = &memory[1];
                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = wordptr;
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 3;
                        lastdisassembledata.seperatorcount += 1;


                        lastdisassembledata.opcode = "enter";
                        lastdisassembledata.parameters = inttohexs(wordptr, 4) + ',' + inttohexs(memory[3], 2);
                        offset += 3;
                    }
                    break;

                case 0xc9:
                    {
                        description = "high level procedure exit";
                        lastdisassembledata.opcode = "leave";
                    }
                    break;

                case 0xca:
                    {
                        description = "far return to calling procedure and pop 2 bytes from stack";
                        wordptr = &memory[1];
                        lastdisassembledata.opcode = "ret";
                        lastdisassembledata.isret = true;

                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = wordptr;
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parameters = inttohexs(wordptr, 4);
                        offset += 2;
                    }
                    break;

                case 0xcb:
                    {
                        description = "far return to calling procedure";
                        lastdisassembledata.opcode = "ret";
                        lastdisassembledata.isret = true;
                    }
                    break;

                case 0xcc:
                    {
                        //should not be shown if its being debugged using int 3'
                        description = "call to interrupt procedure-3:trap to debugger";
                        lastdisassembledata.opcode = "int 3";
                    }
                    break;

                case 0xcd:
                    {
                        description = "call to interrupt procedure";
                        lastdisassembledata.opcode = "int";
                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = memory[1];
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parameters = inttohexs(memory[1], 2);
                        offset += 1;
                    }
                    break;

                case 0xce:
                    {
                        description = "call to interrupt procedure-4:if overflow flag=1";
                        lastdisassembledata.opcode = "into";
                    }
                    break;

                case 0xcf:
                    {
                        description = "interrupt return";
                        if (prefix2.has(0x66)) lastdisassembledata.opcode = "iret";
                        else
                        {
                            if (rex_w)
                                lastdisassembledata.opcode = "iretq";
                            else
                                lastdisassembledata.opcode = "iretd";
                        }
                    }
                    break;

                case 0xd0:
                    {
                        switch (getreg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "rotate eight bits left once";
                                    lastdisassembledata.opcode = "rol";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8) + '1';
                                    offset += last - 1;
                                }
                                break;

                            case 1:
                                {
                                    description = "rotate eight bits right once";
                                    lastdisassembledata.opcode = "ror";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8) + '1';
                                    offset += last - 1;
                                }
                                break;


                            case 2:
                                {
                                    description = "rotate nine bits left once";
                                    lastdisassembledata.opcode = "rcl";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8) + '1';
                                    offset += last - 1;
                                }
                                break;

                            case 3:
                                {
                                    description = "rotate nine bits right once";
                                    lastdisassembledata.opcode = "rcr";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8) + '1';
                                    offset += last - 1;
                                }
                                break;

                            case 4:
                                {
                                    description = "multiply by 2, once";
                                    lastdisassembledata.opcode = "shl";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8) + '1';
                                    offset += last - 1;
                                }
                                break;

                            case 5:
                                {
                                    description = "unsigned divide by 2, once";
                                    lastdisassembledata.opcode = "shr";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8) + '1';
                                    offset += last - 1;
                                }
                                break;

                            case 6:
                                {
                                    description = "not defined by the intel documentation";
                                    lastdisassembledata.opcode = "db";
                                    lastdisassembledata.parameters = inttohexs(memory[0], 2) + ' ' + inttohexs(memory[1], 2);
                                }
                                break;

                            case 7:
                                {
                                    description = "signed divide by 2, once";
                                    lastdisassembledata.opcode = "sar";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8) + '1';
                                    offset += last - 1;
                                }
                                break;

                        }
                    }
                    break;

                case 0xd1:
                    {
                        switch (getreg(memory[1]))
                        {
                            case 0:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "rotate 16 bits left once";
                                        lastdisassembledata.opcode = "rol";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16) + '1';
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "rotate 32 bits left once";
                                        lastdisassembledata.opcode = "rol";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + '1';
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 1:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "rotate 16 bits right once";
                                        lastdisassembledata.opcode = "ror";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16) + '1';
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "rotate 32 bits right once";
                                        lastdisassembledata.opcode = "ror";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + '1';
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 2:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "rotate 17 bits left once";
                                        lastdisassembledata.opcode = "rcl";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16) + '1';
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "rotate 33 bits left once";
                                        lastdisassembledata.opcode = "rcl";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + '1';
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 3:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "rotate 17 bits right once";
                                        lastdisassembledata.opcode = "rcr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16) + '1';
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "rotate 33 bits right once";
                                        lastdisassembledata.opcode = "rcr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + '1';
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 4:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "multiply by 2, once";
                                        lastdisassembledata.opcode = "shl";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16) + '1';
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "multiply by 2, once";
                                        lastdisassembledata.opcode = "shl";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + '1';
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 5:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "unsigned divide by 2, once";
                                        lastdisassembledata.opcode = "shr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16) + '1';
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "unsigned divide by 2, once";
                                        lastdisassembledata.opcode = "shr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + '1';
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 6:
                                {
                                    description = "undefined by the intel documentation";
                                    lastdisassembledata.opcode = "db";
                                    lastdisassembledata.parameters = inttohexs(memory[0], 2);
                                }
                                break;

                            case 7:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "signed divide by 2, once";
                                        lastdisassembledata.opcode = "sar";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16) + '1';
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "signed divide by 2, once";
                                        lastdisassembledata.opcode = "sar";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + '1';
                                        offset += last - 1;
                                    }
                                }
                                break;

                        }
                    }
                    break;


                case 0xd2:
                    {
                        switch (getreg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "rotate eight bits left cl times";
                                    lastdisassembledata.opcode = "rol";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8) + colorreg + "cl" + endcolor;
                                    offset += last - 1;
                                }
                                break;

                            case 1:
                                {
                                    description = "rotate eight bits right cl times";
                                    lastdisassembledata.opcode = "ror";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8) + colorreg + "cl" + endcolor;
                                    offset += last - 1;
                                }
                                break;

                            case 2:
                                {
                                    description = "rotate nine bits left cl times";
                                    lastdisassembledata.opcode = "rcl";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8) + colorreg + "cl" + endcolor;
                                    offset += last - 1;
                                }
                                break;

                            case 3:
                                {
                                    description = "rotate nine bits right cl times";
                                    lastdisassembledata.opcode = "rcr";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8) + colorreg + "cl" + endcolor;
                                    offset += last - 1;
                                }
                                break;

                            case 4:
                                {
                                    description = "multiply by 2, cl times";
                                    lastdisassembledata.opcode = "shl";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8) + colorreg + "cl" + endcolor;
                                    offset += last - 1;
                                }
                                break;

                            case 5:
                                {
                                    description = "unsigned divide by 2, cl times";
                                    lastdisassembledata.opcode = "shr";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8) + colorreg + "cl" + endcolor;
                                    offset += last - 1;
                                }
                                break;

                            case 6:
                                {
                                    description = "multiply by 2, cl times";
                                    lastdisassembledata.opcode = "shl";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8) + colorreg + "cl" + endcolor;
                                    offset += last - 1;
                                }
                                break;

                            case 7:
                                {
                                    description = "signed divide by 2, cl times";
                                    lastdisassembledata.opcode = "sar";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8) + colorreg + "cl" + endcolor;
                                    offset += last - 1;
                                }
                                break;


                        }
                    }
                    break;

                case 0xd3:
                    {
                        switch (getreg(memory[1]))
                        {
                            case 0:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "rotate 16 bits left cl times";
                                        lastdisassembledata.opcode = "rol";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16) + colorreg + "cl" + endcolor;
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "rotate 32 bits left cl times";
                                        lastdisassembledata.opcode = "rol";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + colorreg + "cl" + endcolor;
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 1:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "rotate 16 bits right cl times";
                                        lastdisassembledata.opcode = "ror";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16) + colorreg + "cl" + endcolor;
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "rotate 32 bits right cl times";
                                        lastdisassembledata.opcode = "ror";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + colorreg + "cl" + endcolor;
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 2:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "rotate 17 bits left cl times";
                                        lastdisassembledata.opcode = "rcl";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16) + colorreg + "cl" + endcolor;
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "rotate 33 bits left cl times";
                                        lastdisassembledata.opcode = "rcl";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + colorreg + "cl" + endcolor;
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 3:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "rotate 17 bits right cl times";
                                        lastdisassembledata.opcode = "rcr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16) + colorreg + "cl" + endcolor;
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "rotate 33 bits right cl times";
                                        lastdisassembledata.opcode = "rcr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + colorreg + "cl" + endcolor;
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 4:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "multiply by 2, cl times";
                                        lastdisassembledata.opcode = "shl";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16) + colorreg + "cl" + endcolor;
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "multiply by 2, cl times";
                                        lastdisassembledata.opcode = "shl";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + colorreg + "cl" + endcolor;
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 5:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "unsigned divide by 2, cl times";
                                        lastdisassembledata.opcode = "shr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16) + colorreg + "cl" + endcolor;
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "unsigned divide by 2, cl times";
                                        lastdisassembledata.opcode = "shr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + colorreg + "cl" + endcolor;
                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 7:
                                {
                                    if (prefix2.has(0x66))
                                    {
                                        description = "signed divide by 2, cl times";
                                        lastdisassembledata.opcode = "sar";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16) + colorreg + "cl" + endcolor;
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "signed divide by 2, cl times";
                                        lastdisassembledata.opcode = "sar";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last) + colorreg + "cl" + endcolor;
                                        offset += last - 1;
                                    }
                                }
                                break;

                        }
                    }
                    break;


                case 0xd4:
                    {  // aam
                        offset += 1;
                        lastdisassembledata.opcode = "aam";
                        description = "ascii adjust ax after multiply";

                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = memory[1];
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        if (memory[1] != 0xa)
                            lastdisassembledata.parameters = inttohexs(memory[1], 2);
                    }
                    break;

                case 0xd5:
                    {  // aad
                        offset += 1;
                        lastdisassembledata.opcode = "aad";
                        description = "ascii adjust ax before division";
                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = memory[1];
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        if (memory[1] != 0xa) lastdisassembledata.parameters = inttohexs(memory[1], 2);
                    }
                    break;

                case 0xd7:
                    {
                        description = "table look-up translation";
                        lastdisassembledata.opcode = "xlatb";
                    }
                    break;

                case 0xd8:
                    {
                        switch (getreg(memory[1]))
                        {
                            case 0:
                                {
                                    //fadd
                                    description = "add";
                                    lastdisassembledata.opcode = "fadd";
                                    last = 2;
                                    if (memory[1] >= 0xc0)
                                        lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xc0) + ')';
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);

                                    offset += last - 1;
                                }
                                break;

                            case 1:
                                {
                                    description = "multiply";
                                    last = 2;
                                    if (memory[1] >= 0xc8)
                                    {
                                        lastdisassembledata.opcode = "fmul";
                                        lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xc8) + ')';
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "fmul";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);

                                    }
                                    offset += last - 1;
                                }
                                break;


                            case 2:
                                {
                                    description = "compare real";
                                    last = 2;
                                    if (memory[1] >= 0xd0)
                                    {
                                        lastdisassembledata.opcode = "fcom";
                                        lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xd0) + ')';
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "fcom";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);

                                    }
                                    offset += last - 1;
                                }
                                break;

                            case 3:
                                {
                                    description = "compare real and pop register stack";
                                    last = 2;
                                    if (memory[1] >= 0xd8)
                                    {
                                        lastdisassembledata.opcode = "fcomp";
                                        lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xd8) + ')';
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "fcomp";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);

                                    }
                                    offset += last - 1;
                                }
                                break;

                            case 4:
                                {
                                    description = "substract";
                                    last = 2;
                                    if (memory[1] >= 0xe0)
                                    {
                                        lastdisassembledata.opcode = "fsub";
                                        lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xe0) + ')';
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "fsub";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);

                                    }
                                    offset += last - 1;
                                }
                                break;

                            case 5:
                                {
                                    description = "reverse substract";
                                    last = 2;
                                    if (memory[1] >= 0xe8)
                                    {
                                        lastdisassembledata.opcode = "fsubr";
                                        lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xe8) + ')';
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "fsubr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);

                                    }
                                    offset += last - 1;
                                }
                                break;

                            case 6:
                                {
                                    description = "divide";
                                    last = 2;
                                    if (memory[1] >= 0xf0)
                                    {
                                        lastdisassembledata.opcode = "fdiv";
                                        lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xf0) + ')';
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "fdiv";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);

                                    }
                                    offset += last - 1;
                                }
                                break;

                            case 7:
                                {
                                    description = "reverse divide";
                                    last = 2;
                                    if (memory[1] >= 0xf8)
                                    {
                                        lastdisassembledata.opcode = "fdivr";
                                        lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xf8) + ')';
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "fdivr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);

                                    }
                                    offset += last - 1;
                                }
                                break;
                        }

                    }
                    break;

                case 0xd9:
                    {
                        lastdisassembledata.isfloat = true;
                        switch (memory[1])
                        {
                            case 0... 0xbf : {

                                    switch (getreg(memory[1]))
                                    {
                                        case 0:
                                            {
                                                description = "load floating point value";
                                                lastdisassembledata.opcode = "fld";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 2:
                                            {
                                                description = "store single";
                                                lastdisassembledata.opcode = "fst";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 3:
                                            {
                                                description = "store single";
                                                lastdisassembledata.opcode = "fstp";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 4:
                                            {
                                                description = "load fpu environment";
                                                lastdisassembledata.opcode = "fldenv";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 5:
                                            {
                                                description = "load control word";
                                                lastdisassembledata.opcode = "fldcw";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 6:
                                            {
                                                description = "store fpu environment";
                                                lastdisassembledata.opcode = "fnstenv";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 7:
                                            {
                                                description = "store control word";
                                                lastdisassembledata.opcode = "fnstcw";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                                offset += last - 1;
                                            }
                                            break;


                                    }
                                }
                                break;

                            case RANGE_8(0xc0, 0xc7):
                                {
                                    description = "push st(i) onto the fpu register stack";
                                    lastdisassembledata.opcode = "fld";
                                    lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xc0) + ')';
                                    offset += 1;
                                }
                                break;

                            case RANGE_8(0xc8, 0xcf):
                                {
                                    description = "exchange register contents";
                                    lastdisassembledata.opcode = "fxch";
                                    lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xc8) + ')';
                                    offset += 1;
                                }
                                break;


                            case RANGE_7(0xd9, 0xdf):
                                {
                                    description = "exchange register contents";
                                    lastdisassembledata.opcode = "fxch";
                                    lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xd9) + ')';
                                    offset += 1;
                                }
                                break;


                            case 0xd0:
                                {
                                    description = "no operation";
                                    lastdisassembledata.opcode = "fnop";
                                    offset += 1;
                                }
                                break;

                            case 0xe0:
                                {
                                    description = "change sign";
                                    lastdisassembledata.opcode = "fchs";
                                    offset += 1;
                                }
                                break;

                            case 0xe1:
                                {
                                    description = "absolute value";
                                    lastdisassembledata.opcode = "fabs";
                                    offset += 1;
                                }
                                break;

                            case 0xe4:
                                {
                                    description = "test";
                                    lastdisassembledata.opcode = "ftst";
                                    offset += 1;
                                }
                                break;

                            case 0xe5:
                                {
                                    description = "examine";
                                    lastdisassembledata.opcode = "fxam";
                                    offset += 1;
                                }
                                break;



                            case 0xe8:
                                {
                                    description = "Push +1.0 onto the FPU register stack";
                                    lastdisassembledata.opcode = "fld1";
                                    offset += 1;
                                }
                                break;

                            case 0xe9:
                                {
                                    description = "Push log2(10) onto the FPU register stack";
                                    lastdisassembledata.opcode = "fldl2t";
                                    offset += 1;
                                }
                                break;

                            case 0xea:
                                {
                                    description = "Push log2(e) onto the FPU register stack";
                                    lastdisassembledata.opcode = "fldl2e";
                                    offset += 1;
                                }
                                break;

                            case 0xeb:
                                {
                                    description = "Push \"pi\" onto the FPU register stackload constant";
                                    lastdisassembledata.opcode = "fldpi";
                                    offset += 1;
                                }
                                break;

                            case 0xec:
                                {
                                    description = "Push log10(2) onto the FPU register stack";
                                    lastdisassembledata.opcode = "fldlg2";
                                    offset += 1;
                                }
                                break;

                            case 0xed:
                                {
                                    description = "Push log e(2) onto the FPU register stack";
                                    lastdisassembledata.opcode = "fldln2";
                                    offset += 1;
                                }
                                break;

                            case 0xee:
                                {
                                    description = "Push +0.0 onto the FPU register stack";
                                    lastdisassembledata.opcode = "fldz";
                                    offset += 1;
                                }
                                break;


                            case 0xf0:
                                {
                                    description = "compute 2^x-1";
                                    lastdisassembledata.opcode = "f2xm1";
                                    offset += 1;
                                }
                                break;

                            case 0xf1:
                                {
                                    description = "compute y*log(2)x";
                                    lastdisassembledata.opcode = "fyl2x";
                                    offset += 1;
                                }
                                break;

                            case 0xf2:
                                {
                                    description = "partial tangent";
                                    lastdisassembledata.opcode = "fptan";
                                    offset += 1;
                                }
                                break;

                            case 0xf3:
                                {
                                    description = "partial arctangent";
                                    lastdisassembledata.opcode = "fpatan";
                                    offset += 1;
                                }
                                break;

                            case 0xf4:
                                {
                                    description = "extract exponent and significand";
                                    lastdisassembledata.opcode = "fxtract";
                                    offset += 1;
                                }
                                break;

                            case 0xf5:
                                {
                                    description = "partial remainder";
                                    lastdisassembledata.opcode = "fprem1";
                                    offset += 1;
                                }
                                break;

                            case 0xf6:
                                {
                                    description = "decrement stack-top pointer";
                                    lastdisassembledata.opcode = "fdecstp";
                                    offset += 1;
                                }
                                break;

                            case 0xf7:
                                {
                                    description = "increment stack-top pointer";
                                    lastdisassembledata.opcode = "fincstp";
                                    offset += 1;
                                }
                                break;

                            case 0xf8:
                                {
                                    description = "partial remainder";
                                    lastdisassembledata.opcode = "fprem";
                                    offset += 1;
                                }
                                break;

                            case 0xf9:
                                {
                                    description = "compute y*log(2)(x+1)";
                                    lastdisassembledata.opcode = "fyl2xp1";
                                    offset += 1;
                                }
                                break;

                            case 0xfa:
                                {
                                    description = "square root";
                                    lastdisassembledata.opcode = "fsqrt";
                                    offset += 1;
                                }
                                break;

                            case 0xfb:
                                {
                                    description = "sine and cosine";
                                    lastdisassembledata.opcode = "fsincos";
                                    offset += 1;
                                }
                                break;


                            case 0xfc:
                                {
                                    description = "round to integer";
                                    lastdisassembledata.opcode = "frndint";
                                    offset += 1;
                                }
                                break;

                            case 0xfd:
                                {
                                    description = "scale";
                                    lastdisassembledata.opcode = "fscale";
                                    offset += 1;
                                }
                                break;

                            case 0xfe:
                                {
                                    description = "sine";
                                    lastdisassembledata.opcode = "fsin";
                                    offset += 1;
                                }
                                break;

                            case 0xff:
                                {
                                    description = "cosine";
                                    lastdisassembledata.opcode = "fcos";
                                    offset += 1;
                                }
                                break;
                        }
                    }
                    break;

                case 0xda:
                    {
                        if (memory[1] < 0xbf)
                        {
                            switch (getreg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "add";
                                        lastdisassembledata.opcode = "fiadd";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                        offset += last - 1;
                                    }
                                    break;

                                case 1:
                                    {
                                        description = "multiply";
                                        lastdisassembledata.opcode = "fimul";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                        offset += last - 1;
                                    }
                                    break;

                                case 2:
                                    {
                                        description = "compare integer";
                                        lastdisassembledata.opcode = "ficom";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                        offset += last - 1;
                                    }
                                    break;

                                case 3:
                                    {
                                        description = "compare integer";
                                        lastdisassembledata.opcode = "ficomp";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                        offset += last - 1;
                                    }
                                    break;

                                case 4:
                                    {
                                        description = "subtract";
                                        lastdisassembledata.opcode = "fisub";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                        offset += last - 1;
                                    }
                                    break;

                                case 5:
                                    {
                                        description = "reverse subtract";
                                        lastdisassembledata.opcode = "fisubr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                        offset += last - 1;
                                    }
                                    break;


                                case 6:
                                    {
                                        description = "divide";
                                        lastdisassembledata.opcode = "fidiv";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);

                                        offset += last - 1;
                                    }
                                    break;

                                case 7:
                                    {
                                        description = "reverse divide";
                                        lastdisassembledata.opcode = "fidivr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                        offset += last - 1;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (getreg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "floating-point: move if below";
                                        lastdisassembledata.opcode = "fcmovb";
                                        lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xc0) + ')';
                                        offset += 1;
                                    }
                                    break;

                                case 1:
                                    {
                                        description = "floating-point: move if equal";
                                        lastdisassembledata.opcode = "fcmove";
                                        lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xc8) + ')';
                                        offset += 1;
                                    }
                                    break;

                                case 2:
                                    {
                                        description = "floating-point: move if below or equal";
                                        lastdisassembledata.opcode = "fcmovbe";
                                        lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xd0) + ')';
                                        offset += 1;
                                    }
                                    break;

                                case 3:
                                    {
                                        description = "floating-point: move if unordered";
                                        lastdisassembledata.opcode = "fcmovu";
                                        lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xd8) + ')';
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
                                                    lastdisassembledata.opcode = "fucompp";
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
                        switch (memory[1])
                        {
                            case 0... 0xbf : {
                                    switch (getreg(memory[1]))
                                    {
                                        case 0:
                                            {
                                                description = "load integer";
                                                lastdisassembledata.opcode = "fild";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 1:
                                            {
                                                description = "store integer with truncation";
                                                lastdisassembledata.opcode = "fisttp";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 2:
                                            {
                                                description = "store integer";
                                                lastdisassembledata.opcode = "fist";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 3:
                                            {
                                                description = "store integer";
                                                lastdisassembledata.opcode = "fistp";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 5:
                                            {
                                                lastdisassembledata.isfloat = true;
                                                description = "load floating point value";
                                                lastdisassembledata.opcode = "fld";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 80);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 7:
                                            {
                                                lastdisassembledata.isfloat = true;
                                                description = "store extended";
                                                lastdisassembledata.opcode = "fstp";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 80);

                                                offset += last - 1;
                                            }
                                            break;

                                    }
                                }
                                break;

                            case RANGE_8(0xc0, 0xc7):
                                {
                                    description = "floating-point: move if not below";
                                    lastdisassembledata.opcode = "fcmovnb";
                                    lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xc0) + ')';
                                    offset += 1;
                                }
                                break;

                            case RANGE_8(0xc8, 0xcf):
                                {
                                    description = "floating-point: move if not equal";
                                    lastdisassembledata.opcode = "fcmovne";
                                    lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xc8) + ')';
                                    offset += 1;
                                }
                                break;

                            case RANGE_8(0xd0, 0xd7):
                                {
                                    description = "floating-point: move if not below or equal";
                                    lastdisassembledata.opcode = "fcmovnbe";
                                    lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xd0) + ')';
                                    offset += 1;
                                }
                                break;

                            case RANGE_8(0xd8, 0xdf):
                                {
                                    description = "floating-point: move if not unordered";
                                    lastdisassembledata.opcode = "fcmovnu";
                                    lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xd8) + ')';
                                    offset += 1;
                                }
                                break;

                            case 0xe2:
                                {
                                    description = "clear exceptions";
                                    lastdisassembledata.opcode = "fnclex";
                                    offset += 1;
                                }
                                break;

                            case 0xe3:
                                {
                                    description = "initialize floating-point unit";
                                    lastdisassembledata.opcode = "fninit";
                                    offset += 1;
                                }
                                break;

                            case RANGE_8(0xe8, 0xef):
                                {
                                    description = "floating-point: compare real and set eflags";
                                    lastdisassembledata.opcode = "fucomi";
                                    lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xe8) + ')';
                                    offset += 1;
                                }
                                break;

                            case RANGE_8(0xf0, 0xf7):
                                {
                                    description = "floating-point: compare real and set eflags";
                                    lastdisassembledata.opcode = "fcomi";
                                    lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xf0) + ')';
                                    offset += 1;
                                }
                                break;
                        }


                    }
                    break;

                case 0xdc:
                    {
                        lastdisassembledata.isfloat = true;
                        switch (getreg(memory[1]))
                        {
                            case 0:
                                {
                                    //fadd
                                    description = "add";
                                    last = 2;
                                    if (memory[1] >= 0xc0)
                                    {
                                        lastdisassembledata.opcode = "fadd";
                                        lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xc0) + "),st(0)";
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "fadd";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);

                                    }
                                    offset += last - 1;
                                }
                                break;

                            case 1:
                                {
                                    description = "multiply";
                                    last = 2;
                                    if (memory[1] >= 0xc8)
                                    {
                                        lastdisassembledata.opcode = "fmul";
                                        lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xc8) + "),st(0)";
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "fmul";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);

                                    }
                                    offset += last - 1;
                                }
                                break;

                            case 2:
                                {
                                    description = "compare real";
                                    last = 2;
                                    lastdisassembledata.opcode = "fcom";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);

                                    offset += last - 1;
                                }
                                break;

                            case 3:
                                {
                                    description = "compare real";
                                    last = 2;
                                    lastdisassembledata.opcode = "fcomp";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);

                                    offset += last - 1;
                                }
                                break;

                            case 4:
                                {
                                    description = "subtract";
                                    last = 2;
                                    if (memory[1] >= 0xe0)
                                    {
                                        lastdisassembledata.opcode = "fsubr";
                                        lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xe0) + "),st(0)";
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "fsub";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);

                                    }
                                    offset += last - 1;
                                }
                                break;

                            case 5:
                                {
                                    description = "reverse subtract";
                                    last = 2;
                                    if (memory[1] >= 0xe8)
                                    {
                                        lastdisassembledata.opcode = "fsub";
                                        lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xe8) + "),st(0)";
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "fsubr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);

                                    }


                                    offset += last - 1;
                                }
                                break;


                            case 6:
                                {
                                    description = "divide";
                                    last = 2;
                                    if (memory[1] >= 0xf0)
                                    {
                                        lastdisassembledata.opcode = "fdivr";
                                        lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xf0) + "),st(0)";
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "fdiv";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);

                                    }
                                    offset += last - 1;
                                }
                                break;

                            case 7:
                                {
                                    description = "reverse divide";
                                    last = 2;
                                    if (memory[1] >= 0xf8)
                                    {
                                        lastdisassembledata.opcode = "fdiv";
                                        lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xf8) + "),st(0)";
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "fdivr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);

                                    }
                                    offset += last - 1;
                                }
                                break;
                        }
                    }
                    break;

                case 0xdd:
                    {
                        switch (memory[1])
                        {
                            case 0... 0xbf :  {
                                    switch (getreg(memory[1]))
                                    {
                                        case 0:
                                            {
                                                lastdisassembledata.isfloat = true;
                                                description = "load floating point value";
                                                lastdisassembledata.opcode = "fld";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 1:
                                            {
                                                description = "store integer with truncation";
                                                lastdisassembledata.opcode = "fisttp";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 2:
                                            {
                                                lastdisassembledata.isfloat = true;
                                                description = "store double";
                                                lastdisassembledata.opcode = "fst";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 3:
                                            {
                                                lastdisassembledata.isfloat = true;
                                                description = "store double";
                                                lastdisassembledata.opcode = "fstp";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 4:
                                            {
                                                description = "restore fpu state";
                                                lastdisassembledata.opcode = "frstor";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 6:
                                            {
                                                description = "store fpu state";
                                                lastdisassembledata.opcode = "fnsave";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                                offset += last - 1;
                                            }
                                            break;

                                        case 7:
                                            {
                                                description = "store status word";
                                                lastdisassembledata.opcode = "fnstsw";
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                                offset += last - 1;
                                            }
                                            break;

                                    }

                                }
                                break;

                            case RANGE_8(0xc0, 0xc7):
                                {
                                    description = "free floating-point register";
                                    lastdisassembledata.opcode = "ffree";
                                    lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xc0) + ')';
                                    offset += 1;
                                }
                                break;

                            case RANGE_8(0xd0, 0xd7):
                                {
                                    description = "store real";
                                    lastdisassembledata.opcode = "fst";
                                    lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xd0) + ')';
                                    offset += 1;
                                }
                                break;

                            case RANGE_8(0xd8, 0xdf):
                                {
                                    description = "store real";
                                    lastdisassembledata.opcode = "fstp";
                                    lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xd8) + ')';
                                    offset += 1;
                                }
                                break;

                            case RANGE_8(0xe0, 0xe7):
                                {
                                    description = "unordered compare real";
                                    lastdisassembledata.opcode = "fucom";
                                    lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xe0) + ')';
                                    offset += 1;
                                }
                                break;

                            case RANGE_8(0xe8, 0xef):
                                {
                                    description = "unordered compare real";
                                    lastdisassembledata.opcode = "fucomp";
                                    lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xe8) + ')';
                                    offset += 1;
                                }
                                break;
                            default:
                                {
                                    lastdisassembledata.opcode = "db";
                                    lastdisassembledata.parameters = inttohexs(memory[0], 2);
                                }

                        }
                    }
                    break;

                case 0xde:
                    {
                        switch (getreg(memory[1]))
                        {
                            case 0:
                                {
                                    //faddp
                                    description = "add and pop";
                                    last = 2;
                                    if (memory[1] == 0xc1) lastdisassembledata.opcode = "faddp";
                                    else
                                    if (memory[1] >= 0xc0)
                                    {
                                        lastdisassembledata.opcode = "faddp";
                                        lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xc0) + "),st(0)";
                                    }
                                    else
                                    {
                                        description = "add";
                                        lastdisassembledata.opcode = "fiadd";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                    }
                                    offset += last - 1;
                                }
                                break;

                            case 1:
                                {
                                    description = "multiply";
                                    last = 2;
                                    if (memory[1] >= 0xc8)
                                    {
                                        lastdisassembledata.opcode = "fmulp";
                                        lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xc8) + "),st(0)";
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "fimul";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                    }

                                    offset += last - 1;
                                }
                                break;

                            case 2:
                                {
                                    description = "compare integer";
                                    last = 2;
                                    lastdisassembledata.opcode = "ficom";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);
                                    offset += last - 1;
                                }
                                break;


                            case 3:
                                {
                                    if (memory[1] < 0xc0)
                                    {
                                        description = "compare integer";
                                        lastdisassembledata.opcode = "ficomp";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                        offset += last - 1;
                                    }

                                    if (memory[1] == 0xd9)
                                    {
                                        description = "compare real and pop register stack twice";
                                        lastdisassembledata.opcode = "fcompp";
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
                                        lastdisassembledata.opcode = "fsubrp";
                                        lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xe0) + "),st(0)";
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "fisub";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                    }
                                    offset += last - 1;
                                }
                                break;


                            case 5:
                                {
                                    description = "reverse divide";
                                    last = 2;
                                    if (memory[1] >= 0xe8)
                                    {
                                        description = "subtract and pop from stack";
                                        lastdisassembledata.opcode = "fsubp";
                                        lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xe8) + "),st(0)";
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "fisubr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                    }

                                    offset += last - 1;
                                }
                                break;


                            case 6:
                                {
                                    description = "reverse divide";
                                    last = 2;
                                    if (memory[1] >= 0xf0)
                                    {
                                        lastdisassembledata.opcode = "fdivrp";
                                        lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xf0) + "),st(0)";
                                        offset += last - 1;
                                    }
                                    else
                                    {
                                        description = "divide";
                                        lastdisassembledata.opcode = "fidiv";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);

                                        offset += last - 1;
                                    }
                                }
                                break;

                            case 7:
                                {
                                    description = "divide";
                                    last = 2;
                                    if (memory[1] >= 0xf8)
                                    {
                                        lastdisassembledata.opcode = "fdivp";
                                        lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xf8) + "),st(0)";
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "fdivr";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                    }
                                    offset += last - 1;
                                }
                                break;

                        }
                    }
                    break;

                case 0xdf:
                    {
                        if (set::of(range(0xc0, 0xc7), eos).has(memory[1]))
                        {
                            description = "free floating-point register and pop (might not work)";
                            lastdisassembledata.opcode = "ffreep";
                            lastdisassembledata.parameters = string("st(") + inttostr(memory[1] - 0xc0) + ')';
                            offset += 1;
                        }
                        else
                            switch (getreg(memory[1]))
                            {
                                case 0:
                                    {
                                        description = "load integer";
                                        lastdisassembledata.opcode = "fild";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 16);

                                        offset += last - 1;
                                    }
                                    break;

                                case 1:
                                    {
                                        description = "store integer with truncation";
                                        lastdisassembledata.opcode = "fisttp";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 16);

                                        offset += last - 1;
                                    }
                                    break;

                                case 2:
                                    {
                                        description = "store integer";
                                        lastdisassembledata.opcode = "fist";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 16);

                                        offset += last - 1;
                                    }
                                    break;

                                case 3:
                                    {
                                        description = "store integer";
                                        lastdisassembledata.opcode = "fistp";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 16);

                                        offset += last - 1;
                                    }
                                    break;

                                case 4:
                                    {
                                        description = "load binary coded decimal";
                                        last = 2;
                                        if (memory[1] >= 0xe0)
                                        {
                                            lastdisassembledata.opcode = "fnstsw";
                                            lastdisassembledata.parameters = colorreg + "ax" + endcolor;
                                        }
                                        else
                                        {
                                            lastdisassembledata.opcode = "fbld";
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 80);

                                        }
                                        offset += last - 1;
                                    }
                                    break;

                                case 5:
                                    {
                                        if (memory[1] < 0xc0)
                                        {
                                            description = "load integer";
                                            lastdisassembledata.opcode = "fild";
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);

                                            offset += last - 1;
                                        }

                                        if (memory[1] >= 0xe8)
                                        {
                                            description = "compare real and set eflags";
                                            lastdisassembledata.opcode = "fucomip";
                                            lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xe8) + ')';
                                            offset += 1;
                                        }
                                    }
                                    break;

                                case 6:
                                    {
                                        if (memory[1] >= 0xf0)
                                        {
                                            description = "compare real and set eflags";
                                            lastdisassembledata.opcode = "fcomip";
                                            lastdisassembledata.parameters = string("st(0),st(") + inttostr(memory[1] - 0xf0) + ')';
                                            offset += 1;
                                        }
                                        else
                                        {
                                            description = "store bcd integer and pop";
                                            lastdisassembledata.opcode = "fbstp";
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 80);

                                            offset += last - 1;
                                        }
                                    }
                                    break;

                                case 7:
                                    {
                                        description = "store integer";
                                        lastdisassembledata.opcode = "fistp";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);

                                        offset += last - 1;
                                    }
                                    break;

                                default:
                                    {
                                        lastdisassembledata.opcode = "db";
                                        lastdisassembledata.parameters = inttohexs(memory[0], 2);
                                    }
                            }

                    }
                    break;

                case 0xe0:
                    {
                        description = "loop according to ecx counter";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;
                        if (context != nil)
                            lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_zf) == 0;

                        lastdisassembledata.opcode = "loopne";

                        offset += 1;

                        lastdisassembledata.parametervaluetype = dvtaddress;
                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword(pshortint(&memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword(pshortint(&memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;
                    }
                    break;

                case 0xe1:
                    {
                        description = "loop according to ecx counter";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;
                        if (context != nil)
                            lastdisassembledata.willjumpaccordingtocontext = (context->eflags & eflags_zf) != 0;

                        lastdisassembledata.opcode = "loope";
                        offset += 1;

                        lastdisassembledata.parametervaluetype = dvtaddress;
                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword(pshortint(&memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword(pshortint(&memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;
                    }
                    break;

                case 0xe2:
                    {
                        description = "loop according to ecx counting";
                        lastdisassembledata.opcode = "loop";
                        // todo readd me
                        //if context<>nil then
                        //lastdisassembledata.willJumpAccordingToContext:=context^.{$ifdef CPU64}RCX{$else}ECX{$endif}<>0;

                        lastdisassembledata.isjump = true;
                        offset += 1;

                        lastdisassembledata.parametervaluetype = dvtaddress;
                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword(pshortint(&memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword(pshortint(&memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;
                    }
                    break;

                case 0xe3:
                    {
                        description = "jump short if cx=0";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.isconditionaljump = true;

                        if (prefix2.has(0x66))
                        {
                            lastdisassembledata.opcode = "jcxz";
                            // todo readd me
                            //if context<>nil then
                            //lastdisassembledata.willJumpAccordingToContext:=((context^.{$ifdef CPU64}RCX{$else}ECX{$endif}) and $ffff)=0;

                        }
                        else
                        {
                            lastdisassembledata.opcode = "jecxz";
                            // todo readd me
                            //if context<>nil then
                            //lastdisassembledata.willJumpAccordingToContext:=context^.{$ifdef CPU64}RCX{$else}ECX{$endif}=0;

                        }
                        offset += 1;

                        lastdisassembledata.parametervaluetype = dvtaddress;



                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword(pshortint(&memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword(pshortint(&memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;
                    }
                    break;

                case 0xe4:
                    {
                        description = "input from port";
                        lastdisassembledata.opcode = "in";
                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = memory[1];
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                        lastdisassembledata.parameters = colorreg + "al" + endcolor + ',' + inttohexs(memory[1], 2);
                        offset += 1;

                    }
                    break;

                case 0xe5:
                    {
                        description = "input from port";
                        lastdisassembledata.opcode = "in";

                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = memory[1];
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;


                        if (prefix2.has(0x66)) lastdisassembledata.parameters = colorreg + "ax" + endcolor + ',' + inttohexs(memory[1], 2);
                        else lastdisassembledata.parameters = colorreg + "eax" + endcolor + ',' + inttohexs(memory[1], 2);
                        offset += 1;

                    }
                    break;

                case 0xe6:
                    {
                        description = "output to port";
                        lastdisassembledata.opcode = "out";
                        lastdisassembledata.parameters = inttohexs(memory[1], 2) + ',' + colorreg + "al" + endcolor;
                        offset += 1;

                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = memory[1];
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;
                    }
                    break;

                case 0xe7:
                    {
                        description = "output toport";
                        lastdisassembledata.opcode = "out";
                        if (prefix2.has(0x66))
                            lastdisassembledata.parameters = inttohexs(memory[1], 2) + ',' + colorreg + "ax" + endcolor;
                        else
                            lastdisassembledata.parameters = inttohexs(memory[1], 2) + ',' + colorreg + "eax" + endcolor;

                        offset += 1;

                        lastdisassembledata.parametervaluetype = dvtvalue;
                        lastdisassembledata.parametervalue = memory[1];
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;
                    }
                    break;

                case 0xe8:
                    {
                        //call
                        //this time no $66 prefix because it will only run in win32
                        description = "call procedure";
                        lastdisassembledata.opcode = "call";
                        lastdisassembledata.isjump = true;
                        lastdisassembledata.iscall = true;

                        if (markiprelativeinstructions)
                        {
                            lastdisassembledata.riprelative = 1;
                            riprelative = true;
                        }
                        offset += 4;
                        lastdisassembledata.parametervaluetype = dvtaddress;

                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword(pinteger(&memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword(pinteger(&memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                    }
                    break;

                case 0xe9:
                    {
                        description = "jump near";
                        lastdisassembledata.isjump = true;

                        if (prefix2.has(0x66))
                        {
                            lastdisassembledata.opcode = "jmp";

                            offset += 2;
                            lastdisassembledata.parametervaluetype = dvtaddress;
                            lastdisassembledata.parametervalue = dword(offset + qword(psmallint(&memory[1])));
                            lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);
                        }
                        else
                        {
                            lastdisassembledata.opcode = "jmp";

                            if (markiprelativeinstructions)
                            {
                                lastdisassembledata.riprelative = 1;
                                riprelative = true;
                            }

                            offset += 4;
                            lastdisassembledata.parametervaluetype = dvtaddress;

                            if (is64bit)
                                lastdisassembledata.parametervalue = qword(offset + qword(pinteger(&memory[1])));
                            else
                                lastdisassembledata.parametervalue = dword(offset + qword(pinteger(&memory[1])));

                            lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);
                        }

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                    }
                    break;

                case 0xea:
                    {
                        description = "jump far";
                        lastdisassembledata.isjump = true;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;
                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 5;
                        lastdisassembledata.seperatorcount += 1;


                        wordptr = &memory[5];
                        lastdisassembledata.opcode = "jmp";
                        lastdisassembledata.parameters = inttohexs(wordptr, 4) + ':';
                        dwordptr = &memory[1];

                        lastdisassembledata.parametervaluetype = dvtaddress;
                        lastdisassembledata.parametervalue = dwordptr;


                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(dwordptr, 8);
                        offset += 6;
                    }
                    break;

                case 0xeb:
                    {
                        description = "jump short";
                        lastdisassembledata.opcode = "jmp";
                        lastdisassembledata.isjump = true;

                        offset += 1;

                        if (is64bit)
                            lastdisassembledata.parametervalue = qword(offset + qword(pshortint(&memory[1])));
                        else
                            lastdisassembledata.parametervalue = dword(offset + qword(pshortint(&memory[1])));

                        lastdisassembledata.parameters = inttohexs(lastdisassembledata.parametervalue, 8);

                        lastdisassembledata.parametervaluetype = dvtaddress;

                        lastdisassembledata.seperators[lastdisassembledata.seperatorcount] = 1;
                        lastdisassembledata.seperatorcount += 1;

                    }
                    break;

                case 0xec:
                    {
                        description = "input from port";
                        lastdisassembledata.opcode = "in";
                        lastdisassembledata.parameters = colorreg + "al" + endcolor + ',' + colorreg + "dx" + endcolor;
                    }
                    break;

                case 0xed:
                    {
                        description = "input from port";
                        lastdisassembledata.opcode = "in";
                        if (prefix2.has(0x66)) lastdisassembledata.parameters = colorreg + "ax" + endcolor + ',' + colorreg + "dx" + endcolor;
                        else
                            lastdisassembledata.parameters = colorreg + "eax" + endcolor + ',' + colorreg + "dx" + endcolor;
                    }
                    break;

                case 0xee:
                    {
                        description = "input from port";
                        lastdisassembledata.opcode = "out";
                        lastdisassembledata.parameters = colorreg + "dx" + endcolor + ',' + colorreg + "al" + endcolor;
                    }
                    break;

                case 0xef:
                    {
                        description = "input from port";
                        lastdisassembledata.opcode = "out";
                        if (prefix2.has(0x66)) lastdisassembledata.parameters = colorreg + "dx" + endcolor + ',' + colorreg + "ax" + endcolor;
                        else
                            lastdisassembledata.parameters = colorreg + "dx" + endcolor + ',' + colorreg + "eax" + endcolor;
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
                        lastdisassembledata.opcode = "hlt";
                    }
                    break;

                case 0xf5:
                    {
                        description = "complement carry flag";
                        lastdisassembledata.opcode = "cmc";
                    }
                    break;

                case 0xf6:
                    {
                        switch (getreg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "logical compare";
                                    lastdisassembledata.opcode = "test";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);
                                    lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(memory[last], 2);
                                    lastdisassembledata.parametervaluetype = dvtvalue;
                                    lastdisassembledata.parametervalue = memory[last];


                                    offset += last;
                                }
                                break;

                            case 2:
                                {
                                    description = "one's complement negation";
                                    lastdisassembledata.opcode = "not";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 3:
                                {
                                    description = "two's complement negation";
                                    lastdisassembledata.opcode = "neg";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 4:
                                {
                                    description = "unsigned multiply";
                                    lastdisassembledata.opcode = "mul";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 5:
                                {
                                    description = "signed multiply";
                                    lastdisassembledata.opcode = "imul";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 6:
                                {
                                    description = "unsigned divide";
                                    lastdisassembledata.opcode = "div";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 7:
                                {
                                    description = "signed divide";
                                    lastdisassembledata.opcode = "idiv";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;
                            default:
                                {
                                    lastdisassembledata.opcode = "db";
                                    lastdisassembledata.parameters = inttohexs(memory[0], 2);
                                }

                        }
                    }
                    break;

                case 0xf7:
                    {
                        switch (getreg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "logical compare";
                                    if (prefix2.has(0x66))
                                    {
                                        lastdisassembledata.opcode = "test";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                        wordptr = &memory[last];
                                        lastdisassembledata.parametervaluetype = dvtaddress;
                                        lastdisassembledata.parametervalue = wordptr;

                                        lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(wordptr, 4);
                                        offset += last + 1;
                                    }
                                    else
                                    {
                                        lastdisassembledata.opcode = "test";
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);
                                        dwordptr = &memory[last];
                                        lastdisassembledata.parametervaluetype = dvtaddress;
                                        lastdisassembledata.parametervalue = dwordptr;
                                        if (rex_w)
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs((longint)(dwordptr), 8);
                                        else
                                            lastdisassembledata.parameters = lastdisassembledata.parameters + inttohexs(dwordptr, 8);
                                        offset += last + 3;
                                    }
                                }
                                break;

                            case 2:
                                {
                                    description = "one's complement negation";
                                    lastdisassembledata.opcode = "not";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                    offset += last - 1;
                                }
                                break;

                            case 3:
                                {
                                    description = "two's complement negation";
                                    lastdisassembledata.opcode = "neg";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);


                                    offset += last - 1;
                                }
                                break;

                            case 4:
                                {
                                    description = "unsigned multiply";
                                    lastdisassembledata.opcode = "mul";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);


                                    offset += last - 1;
                                }
                                break;

                            case 5:
                                {
                                    description = "signed multiply";
                                    lastdisassembledata.opcode = "imul";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                    offset += last - 1;
                                }
                                break;

                            case 6:
                                {
                                    description = "unsigned divide";
                                    lastdisassembledata.opcode = "div";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);


                                    offset += last - 1;
                                }
                                break;

                            case 7:
                                {
                                    description = "signed divide";
                                    lastdisassembledata.opcode = "idiv";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);


                                    offset += last - 1;
                                }
                                break;

                            default:
                                {
                                    lastdisassembledata.opcode = "db";
                                    lastdisassembledata.parameters = inttohexs(memory[0], 2);
                                }
                        }
                    }
                    break;

                case 0xf8:
                    {
                        description = "clear carry flag";
                        lastdisassembledata.opcode = "clc";
                    }
                    break;

                case 0xf9:
                    {
                        description = "set carry flag";
                        lastdisassembledata.opcode = "stc";
                    }
                    break;

                case 0xfa:
                    {
                        description = "clear interrupt flag";
                        lastdisassembledata.opcode = "cli";
                    }
                    break;

                case 0xfb:
                    {
                        description = "set interrupt flag";
                        lastdisassembledata.opcode = "sti";
                    }
                    break;

                case 0xfc:
                    {
                        description = "clear direction flag";
                        lastdisassembledata.opcode = "cld";
                    }
                    break;

                case 0xfd:
                    {
                        description = "set direction flag";
                        lastdisassembledata.opcode = "std";
                    }
                    break;

                case 0xfe:
                    {
                        switch (getreg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "increment by 1";
                                    lastdisassembledata.opcode = "inc";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 8);

                                    offset += last - 1;
                                }
                                break;

                            case 1:
                                {
                                    description = "decrement by 1";
                                    lastdisassembledata.opcode = "dec";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 2, last, 7);

                                    offset += last - 1;
                                }
                                break;

                            default:
                                {
                                    lastdisassembledata.opcode = "db";
                                    lastdisassembledata.parameters = inttohexs(memory[0], 2);
                                }
                        }
                    }
                    break;

                case 0xff:
                    {
                        switch (getreg(memory[1]))
                        {
                            case 0:
                                {
                                    description = "increment by 1";
                                    lastdisassembledata.opcode = "inc";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);


                                    offset += last - 1;
                                }
                                break;

                            case 1:
                                {
                                    description = "decrement by 1";
                                    lastdisassembledata.opcode = "dec";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last, 16);
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);

                                    offset += last - 1;
                                }
                                break;

                            case 2:
                                {
                                    //call
                                    description = "call procedure";
                                    lastdisassembledata.opcode = "call";
                                    lastdisassembledata.isjump = true;
                                    lastdisassembledata.iscall = true;

                                    if (memory[1] >= 0xc0)
                                    {
                                        if (is64bit)
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                        else
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);

                                    }
                                    else
                                    {
                                        if (is64bit)
                                        {

                                            if ((memory[1] == 0x15) && (pdword(&memory[2]) == 2) && (pword(&memory[6]) == 0x8eb))  //special 16 byte call
                                            {
                                                lastdisassembledata.parameters = inttohexs(pqword(&memory[8]), 8);
                                                lastdisassembledata.parametervalue = pqword(&memory[8]);
                                                lastdisassembledata.parametervaluetype = dvtaddress;

                                                last += 8 + 4 + 2 + 2;

                                                lastdisassembledata.seperators[0] = 2;
                                                lastdisassembledata.seperators[1] = 2 + 4;
                                                lastdisassembledata.seperators[2] = 2 + 4 + 2;
                                                lastdisassembledata.seperatorcount = 3;

                                            }
                                            else
                                                lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                        }
                                        else
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);
                                    }

                                    offset += last - 1;
                                }
                                break;

                            case 3:
                                {
                                    //call
                                    description = "call procedure";
                                    lastdisassembledata.opcode = "call";
                                    lastdisassembledata.isjump = true;
                                    lastdisassembledata.iscall = true;

                                    if (is64bit)
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);

                                    offset += last - 1;
                                }
                                break;

                            case 4:
                                {
                                    //jmp
                                    description = "jump near";
                                    lastdisassembledata.opcode = "jmp";
                                    lastdisassembledata.isjump = true;


                                    if (is64bit)
                                    {
                                        if ((memory[1] == 0x25) && (pdword(&memory[2]) == 0))  //special 14 byte jmp
                                        {
                                            lastdisassembledata.parametervalue = pqword(&memory[6]);
                                            lastdisassembledata.parametervaluetype = dvtaddress;

                                            lastdisassembledata.parameters = inttohexs(pqword(&memory[6]), 8);
                                            last += 8 + 4 + 2;

                                            lastdisassembledata.seperators[0] = 2;
                                            lastdisassembledata.seperators[1] = 2 + 4;
                                            lastdisassembledata.seperatorcount = 2;

                                        }
                                        else
                                            lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 64);
                                    }
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last, 32);


                                    offset += last - 1;
                                }
                                break;

                            case 5:
                                {
                                    //jmp
                                    description = "jump far";
                                    lastdisassembledata.opcode = "jmp far";
                                    lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);
                                    lastdisassembledata.isjump = true;

                                    offset += last - 1;
                                }
                                break;

                            case 6:
                                {
                                    description = "push word or doubleword onto the stack";
                                    lastdisassembledata.opcode = "push";
                                    if (prefix2.has(0x66))
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 1, last);
                                    else
                                        lastdisassembledata.parameters = modrm(memory, prefix2, 1, 0, last);


                                    offset += last - 1;
                                }
                                break;
                            default:
                                {
                                    lastdisassembledata.opcode = "db";
                                    lastdisassembledata.parameters = inttohexs(memory[0], 2);
                                }

                        }

                    }
                    break;

                default:
                    {
                        lastdisassembledata.opcode = "db";
                        lastdisassembledata.parameters = inttohex(memory[0], 2);
                    }
            }
        }
        #endregion
    }
}
