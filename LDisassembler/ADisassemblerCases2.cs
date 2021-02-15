using System;
using Sputnik.LBinary;
using Sputnik.LMarshal;
using SputnikAsm.LDisassembler.LEnums;
using SputnikAsm.LUtils;

namespace SputnikAsm.LDisassembler
{
    public partial class ADisassembler
    {
        #region DisassembleProcess2
        private Boolean DisassembleProcess2(UBytePtr memory, ref UIntPtr offset, ref int prefixSize, ref UInt32 last, ref String description)
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
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Qwords element permutation";
                                                        LastDisassembleData.OpCode = "vpermq";

                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x1:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Permute double-precision floating-point elements";
                                                        LastDisassembleData.OpCode = "vpermpd";

                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x2:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Blend packed dwords";
                                                        LastDisassembleData.OpCode = "vblenddd";

                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x4:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Permute single-prevision floating-point values";
                                                        LastDisassembleData.OpCode = "vpermilps";

                                                        _opCodeFlags.SkipExtraReg = true;
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;


                                        case 0x5:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Permute double-prevision floating-point values";
                                                        LastDisassembleData.OpCode = "vpermilpd";

                                                        _opCodeFlags.SkipExtraReg = true;
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x6:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Permute floating-point values";
                                                        LastDisassembleData.OpCode = "vperm2f128";

                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;


                                        /*0f*//*3a*/
                                        case 0x8:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Round scalar single precision floating-point values";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vroundps";
                                                    else
                                                        LastDisassembleData.OpCode = "roundps";

                                                    _opCodeFlags.SkipExtraReg = true;
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        case 0x9:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Round packed double precision floating-point values";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vroundpd";
                                                    else
                                                        LastDisassembleData.OpCode = "roundpd";

                                                    _opCodeFlags.SkipExtraReg = true;

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        case 0xa:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Round scalar single precision floating-point values";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vroundss";
                                                    else
                                                        LastDisassembleData.OpCode = "roundss";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        case 0xb:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Round packed single precision floating-point values";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vroundsd";
                                                    else
                                                        LastDisassembleData.OpCode = "roundsd";



                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0xc:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Blend packed single precision floating-point values";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vblendps";
                                                    else
                                                        LastDisassembleData.OpCode = "blendps";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0xd:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Blend packed double precision floating-point values";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vblendpd";
                                                    else
                                                        LastDisassembleData.OpCode = "blendpd";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0xe:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Blend packed words";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpblendw";
                                                    else
                                                        LastDisassembleData.OpCode = "pblendw";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0xf:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed align right";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpalignr";
                                                    else
                                                        LastDisassembleData.OpCode = "palignr";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                                else
                                                {
                                                    LastDisassembleData.OpCode = "palignr";
                                                    LastDisassembleData.Parameters = Mm(memory[3]) + ModRm(memory, _prefix2, 3, 3, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x14:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Extract byte";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpextrb";
                                                    else
                                                        LastDisassembleData.OpCode = "pextrb";

                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 3, 2, ref last, ATmrPos.Left) + Xmm(memory[3]) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x15:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Extract word";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpextrw";
                                                    else
                                                        LastDisassembleData.OpCode = "pextrw";

                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 3, 1, ref last, ATmrPos.Left) + Xmm(memory[3]) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x16:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (RexW)
                                                    {
                                                        description = "Extract qword";
                                                        LastDisassembleData.OpCode = "pextrq";
                                                    }
                                                    else
                                                    {
                                                        description = "Extract dword";
                                                        LastDisassembleData.OpCode = "pextrd";
                                                    }

                                                    if (_hasVex) LastDisassembleData.OpCode = 'v' + LastDisassembleData.OpCode;

                                                    _opCodeFlags.SkipExtraReg = true;
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 3, 2, ref last, ATmrPos.Left) + Xmm(memory[3]) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        case 0x17:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Extract packed single precision floating-point value";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vextractps";
                                                    else
                                                        LastDisassembleData.OpCode = "extractps";

                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Left) + Xmm(memory[3]) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
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
                                                        description = "Insert packed floating-point values";
                                                        LastDisassembleData.OpCode = "vinsertf128";
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
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
                                                        description = "Extract packed floating-point values";
                                                        LastDisassembleData.OpCode = "vextractf128";
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;


                                        /*0f*//*3a*/
                                        case 0x1d:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Convert single-precision FP value to 16-bit FP value";
                                                        LastDisassembleData.OpCode = "vcvtps2ph";
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x20:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Insert Byte";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpinsrb";
                                                    else
                                                        LastDisassembleData.OpCode = "pinsrb";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x21:
                                            {    //C4 E3 79 21 80 B8 00 00 00 20
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Insert Scalar Single-Precision Floating-Point Value";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vinsertps";
                                                    else
                                                        LastDisassembleData.OpCode = "insertps";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        case 0x22:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (RexW)
                                                    {
                                                        description = "Insert qword";
                                                        LastDisassembleData.OpCode = "pinsrq";
                                                    }
                                                    else
                                                    {
                                                        description = "Insert dword";
                                                        LastDisassembleData.OpCode = "pinsrd";
                                                    }

                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = 'v' + LastDisassembleData.OpCode;

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        case 0x38:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Insert packed integer values";
                                                        LastDisassembleData.OpCode = "vinserti128";
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x39:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Extract packed integer values";
                                                        LastDisassembleData.OpCode = "vextracti128";
                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        case 0x40:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Dot product of packed single precision floating-point values";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vdpps";
                                                    else
                                                        LastDisassembleData.OpCode = "dpps";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        case 0x41:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Dot product of packed double precision floating-point values";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vdppd";
                                                    else
                                                        LastDisassembleData.OpCode = "dppd";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x42:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Compute multiple packed sums of absolute difference";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vmpsadbw";
                                                    else
                                                        LastDisassembleData.OpCode = "mpsadbw";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x44:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Carry-less multiplication quadword";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpclmulqdq";
                                                    else
                                                        LastDisassembleData.OpCode = "pclmulqdq";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x46:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Permute integer values";
                                                        LastDisassembleData.OpCode = "vperm2i128";

                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x4a:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Variable Blend Packed Single Precision Floating-Point Values";
                                                        LastDisassembleData.OpCode = "vblendvps";

                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        if (_opCodeFlags.L)
                                                            LastDisassembleData.Parameters = LastDisassembleData.Parameters + _colorReg + RegNrToStr(ARegisterType.RtYmm, ((int)memory[(int)last] >> 4) & 0xf) + _endColor;
                                                        else
                                                            LastDisassembleData.Parameters = LastDisassembleData.Parameters + _colorReg + RegNrToStr(ARegisterType.RtXmm, ((int)memory[(int)last] >> 4) & 0xf) + _endColor;

                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x4b:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Variable Blend Packed Double Precision Floating-Point Values";
                                                        LastDisassembleData.OpCode = "vblendvpd";

                                                        LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                        if (_opCodeFlags.L)
                                                            LastDisassembleData.Parameters = LastDisassembleData.Parameters + _colorReg + RegNrToStr(ARegisterType.RtYmm, ((int)memory[(int)last] >> 4) & 0xf) + _endColor;
                                                        else
                                                            LastDisassembleData.Parameters = LastDisassembleData.Parameters + _colorReg + RegNrToStr(ARegisterType.RtXmm, ((int)memory[(int)last] >> 4) & 0xf) + _endColor;

                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;


                                        /*0f*//*3a*/
                                        case 0x60:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed compare explicit length string, return mask";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpcmpestrm";
                                                    else
                                                        LastDisassembleData.OpCode = "pcmpestrm";

                                                    _opCodeFlags.SkipExtraReg = true;
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x61:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed compare explicit length string, return index";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpcmpestri";
                                                    else
                                                        LastDisassembleData.OpCode = "pcmpestri";

                                                    _opCodeFlags.SkipExtraReg = true;
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x62:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed compare implicit length string, return mask";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpcmpistrm";
                                                    else
                                                        LastDisassembleData.OpCode = "pcmpistrm";

                                                    _opCodeFlags.SkipExtraReg = true;
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        /*0f*//*3a*/
                                        case 0x63:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "Packed compare implicit length string, return index";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpcmpistri";
                                                    else
                                                        LastDisassembleData.OpCode = "pcmpistri";

                                                    _opCodeFlags.SkipExtraReg = true;
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        case 0xdf:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "AES round key generation assist";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vaeskeygenassist";
                                                    else
                                                        LastDisassembleData.OpCode = "aeskeygenassist";

                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right) + ',';
                                                    LastDisassembleData.Parameters += AStringUtils.IntToHex(memory[(int)last], 2);
                                                    last += 1;
                                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                }
                                            }
                                            break;

                                        case 0xf0:
                                            {
                                                if (_prefix2.Contains(0xf2))
                                                {
                                                    if (_hasVex)
                                                    {
                                                        description = "Rotate right logical without affecting flags";
                                                        LastDisassembleData.OpCode = "rorx";
                                                        _opCodeFlags.SkipExtraReg = true;
                                                        LastDisassembleData.Parameters = R32(memory[3]) + ModRm(memory, _prefix2, 3, 0, ref last, ATmrPos.Right) + ',';
                                                        LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + AStringUtils.IntToHex(memory[(int)last], 2);
                                                        last += 1;
                                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                                    }
                                                }
                                            }
                                            break;



                                        default:
                                            {
                                                if (_hasVex)
                                                {
                                                    LastDisassembleData.OpCode = "unknown avx 0F3A " + AStringUtils.IntToHex(memory[2], 2);
                                                    LastDisassembleData.Parameters = Xmm(memory[3]) + ModRm(memory, _prefix2, 3, 4, ref last, ATmrPos.Right);
                                                    LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + AStringUtils.IntToHex(memory[(int)last], 2);
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
                                    LastDisassembleData.OpCode = "cmovo";
                                    if (_prefix2.Contains(0x66)) LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);
                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x41:
                                {
                                    description = "move if not overflow";
                                    LastDisassembleData.OpCode = "cmovno";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x42:
                                {
                                    description = "move if below/ move if carry";
                                    LastDisassembleData.OpCode = "cmovb";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x43:
                                {
                                    description = "move if above or equal/ move if not carry";
                                    LastDisassembleData.OpCode = "cmovae";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x44:
                                {
                                    description = "move if equal/move if zero";
                                    LastDisassembleData.OpCode = "cmove";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x45:
                                {
                                    description = "move if not equal/move if not zero";
                                    LastDisassembleData.OpCode = "cmovne";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x46:
                                {
                                    description = "move if below or equal";
                                    LastDisassembleData.OpCode = "cmovbe";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;


                            case 0x47:
                                {
                                    description = "move if above";
                                    LastDisassembleData.OpCode = "cmova";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x48:
                                {
                                    description = "move if sign";
                                    LastDisassembleData.OpCode = "cmovs";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x49:
                                {
                                    description = "move if not sign";
                                    LastDisassembleData.OpCode = "cmovns";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x4a:
                                {
                                    description = "move if parity even";
                                    LastDisassembleData.OpCode = "cmovpe";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x4b:
                                {
                                    description = "move if not parity/move if parity odd";
                                    LastDisassembleData.OpCode = "cmovnp";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x4c:
                                {
                                    description = "move if less";
                                    LastDisassembleData.OpCode = "cmovl";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x4d:
                                {
                                    description = "move if greater or equal";
                                    LastDisassembleData.OpCode = "cmovge";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x4e:
                                {
                                    description = "move if less or equal";
                                    LastDisassembleData.OpCode = "cmovle";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);


                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x4f:
                                {
                                    description = "move if greater";
                                    LastDisassembleData.OpCode = "cmovg";
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = R16(memory[2]) + ModRm(memory, _prefix2, 2, 1, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x50:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0x66))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovmskpd";
                                        else
                                            LastDisassembleData.OpCode = "movmskpd";

                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "extract packed double-precision floating-point sign mask";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovmskps";
                                        else
                                            LastDisassembleData.OpCode = "movmskps";

                                        LastDisassembleData.Parameters = R32(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;

                                        description = "move mask to integer";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x51:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0xf2))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vsqrtsd";
                                        else
                                            LastDisassembleData.OpCode = "sqrtsd";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "scalar double-fp square root";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vsqrtss";
                                        else
                                            LastDisassembleData.OpCode = "sqrtss";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "scalar single-fp square root";
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0x66))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vsqrtpd";
                                        else
                                            LastDisassembleData.OpCode = "sqrtpd";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "packed double-fp square root";

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vsqrtps";
                                        else
                                            LastDisassembleData.OpCode = "sqrtps";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        description = "packed single-fp square root";

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x52:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0xf3))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vrsqrtss";
                                        else
                                            LastDisassembleData.OpCode = "rsqrtss";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "packed single-fp square root reciprocal";
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vrsqrtps";
                                        else
                                            LastDisassembleData.OpCode = "rsqrtps";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "scalar single-fp square root reciprocal";
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x53:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0xf3))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vrcpss";
                                        else
                                            LastDisassembleData.OpCode = "rcpss";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "Compute Reciprocal of Scalar Single-Precision Floating-Point Values";
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vrcpps";
                                        else
                                            LastDisassembleData.OpCode = "rcpps";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "Compute Reciprocals of Packed Single-Precision Floating-Point Values";
                                        LastDisassembleData.DataSize = 4;
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x54:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vandpd";
                                        else
                                            LastDisassembleData.OpCode = "andpd";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "bit-wise logical and of xmm2/m128 and xmm1";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vandps";
                                        else
                                            LastDisassembleData.OpCode = "andps";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;

                                        description = "bit-wise logical and for single fp";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x55:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "bit-wise logical and not of packed double-precision fp values";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vandnpd";
                                        else
                                            LastDisassembleData.OpCode = "andnpd";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "bit-wise logical and not for single-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vandnps";
                                        else
                                            LastDisassembleData.OpCode = "andnps";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x56:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "bit-wise logical or of double-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vorpd";
                                        else
                                            LastDisassembleData.OpCode = "orpd";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);


                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "bit-wise logical or for single-fp";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vorps";
                                        else
                                            LastDisassembleData.OpCode = "orps";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x57:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "bit-wise logical xor for double-fp data";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vxorpd";
                                        else
                                            LastDisassembleData.OpCode = "xorpd";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);


                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "bit-wise logical xor for single-fp data";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vxorps";
                                        else
                                            LastDisassembleData.OpCode = "xorps";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x58:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0xf2))
                                    {
                                        //delete the repne from the tempResult
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vaddsd";
                                        else
                                            LastDisassembleData.OpCode = "addsd";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "add the lower sp fp number from xmm2/mem to xmm1.";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {
                                        //delete the repe from the tempResult

                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vaddss";
                                        else
                                            LastDisassembleData.OpCode = "addss";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;

                                        description = "add the lower sp fp number from xmm2/mem to xmm1.";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (_prefix2.Contains(0x66))
                                        {
                                            if (_hasVex)
                                                LastDisassembleData.OpCode = "vaddpd";
                                            else
                                                LastDisassembleData.OpCode = "addpd";

                                            LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                            description = "add packed double-precision floating-point values from xmm2/mem to xmm1";
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                        else
                                        {
                                            if (_hasVex)
                                                LastDisassembleData.OpCode = "vaddps";
                                            else
                                                LastDisassembleData.OpCode = "addps";

                                            LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                            LastDisassembleData.DataSize = 4;

                                            description = "add packed sp fp numbers from xmm2/mem to xmm1";
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                }
                                break;

                            case 0x59:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0xf2))
                                    {

                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmulsd";
                                        else
                                            LastDisassembleData.OpCode = "mulsd";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "scalar double-fp multiply";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {

                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmulss";
                                        else
                                            LastDisassembleData.OpCode = "mulss";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;

                                        description = "scalar single-fp multiply";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0x66))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmulpd";
                                        else
                                            LastDisassembleData.OpCode = "mulpd";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "packed double-fp multiply";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmulps";
                                        else
                                            LastDisassembleData.OpCode = "mulps";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;

                                        description = "packed single-fp multiply";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x5a:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0xf2))
                                    {

                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vcvtsd2ss";
                                        else
                                            LastDisassembleData.OpCode = "cvtsd2ss";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "convert scalar double-precision floating-point value to scalar single-precision floating-point value";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {

                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vcvtss2sd";
                                        else
                                            LastDisassembleData.OpCode = "cvtss2sd";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;

                                        description = "convert scalar single-precision floating-point value to scalar double-precision floating-point value";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (_prefix2.Contains(0x66))
                                        {
                                            if (_hasVex)
                                                LastDisassembleData.OpCode = "vcvtpd2ps";
                                            else
                                                LastDisassembleData.OpCode = "cvtpd2ps";
                                            _opCodeFlags.SkipExtraReg = true;

                                            LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                            description = "convert packed double precision fp values to packed single precision fp values";
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                        else
                                        {
                                            if (_hasVex)
                                                LastDisassembleData.OpCode = "vcvtps2pd";
                                            else
                                                LastDisassembleData.OpCode = "cvtps2pd";

                                            _opCodeFlags.SkipExtraReg = true;
                                            LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                            LastDisassembleData.DataSize = 4;

                                            description = "convert packed single precision fp values to packed double precision fp values";
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                }
                                break;

                            case 0x5b:
                                {

                                    if (_prefix2.Contains(0x66))
                                    {
                                        LastDisassembleData.IsFloat = true;
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vcvtps2dq";
                                        else
                                            LastDisassembleData.OpCode = "cvtps2dq";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;

                                        description = "convert ps-precision fpoint values to packed dword's ";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vcvttps2dq";
                                        else
                                            LastDisassembleData.OpCode = "cvttps2dq";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "Convert with Truncation Packed Single-Precision FP Values to Packed Dword Integers";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vcvtdq2ps";
                                        else
                                            LastDisassembleData.OpCode = "cvtdq2ps";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "convert packed dword's to ps-precision fpoint values";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x5c:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0xf2))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vsubsd";
                                        else
                                            LastDisassembleData.OpCode = "subsd";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "scalar double-fp subtract";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vsubss";
                                        else
                                            LastDisassembleData.OpCode = "subss";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;

                                        description = "scalar single-fp subtract";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0x66))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vsubpd";
                                        else
                                            LastDisassembleData.OpCode = "subpd";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "packed double-fp subtract";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vsubps";
                                        else
                                            LastDisassembleData.OpCode = "subps";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4; //4*4 actually

                                        description = "packed single-fp subtract";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;


                            case 0x5d:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0xf2))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vminsd";
                                        else
                                            LastDisassembleData.OpCode = "minsd";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "scalar single-fp minimum";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vminss";
                                        else
                                            LastDisassembleData.OpCode = "minss";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;

                                        description = "scalar single-fp minimum";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (_prefix2.Contains(0x66))
                                        {
                                            if (_hasVex)
                                                LastDisassembleData.OpCode = "vminpd";
                                            else
                                                LastDisassembleData.OpCode = "minpd";

                                            LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                            description = "packed double-fp minimum";
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                        else
                                        {
                                            if (_hasVex)
                                                LastDisassembleData.OpCode = "vminps";
                                            else
                                                LastDisassembleData.OpCode = "minps";

                                            LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                            description = "packed single-fp minimum";
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                }
                                break;

                            case 0x5e:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0xf2))
                                    {
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "divsd";
                                        else
                                            LastDisassembleData.OpCode = "divsd";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        description = "scalar double-precision-fp divide";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {

                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vdivss";
                                        else
                                            LastDisassembleData.OpCode = "divss";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;

                                        description = "scalar single-fp divide";
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (_prefix2.Contains(0x66))
                                        {
                                            if (_hasVex)
                                                LastDisassembleData.OpCode = "vdivpd";
                                            else
                                                LastDisassembleData.OpCode = "divpd";

                                            LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                            description = "packed double-precision fp divide";
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                        else
                                        {
                                            if (_hasVex)
                                                LastDisassembleData.OpCode = "vdivps";
                                            else
                                                LastDisassembleData.OpCode = "divps";
                                            LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                            LastDisassembleData.DataSize = 4;

                                            description = "packed single-fp divide";
                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                }
                                break;

                            case 0x5f:
                                {
                                    LastDisassembleData.IsFloat = true;
                                    if (_prefix2.Contains(0xf2))
                                    {

                                        description = "scalar double-fp maximum";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmaxsd";
                                        else
                                            LastDisassembleData.OpCode = "maxsd";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {

                                        description = "scalar single-fp maximum";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmaxss";
                                        else
                                            LastDisassembleData.OpCode = "maxss";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.DataSize = 4;

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        if (_prefix2.Contains(0x66))
                                        {
                                            description = "packed double-fp maximum";
                                            if (_hasVex)
                                                LastDisassembleData.OpCode = "vmaxpd";
                                            else
                                                LastDisassembleData.OpCode = "maxpd";
                                            LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                        else
                                        {
                                            description = "packed single-fp maximum";
                                            if (_hasVex)
                                                LastDisassembleData.OpCode = "vmaxps";
                                            else
                                                LastDisassembleData.OpCode = "maxps";

                                            LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                            LastDisassembleData.DataSize = 4;

                                            offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                        }
                                    }
                                }
                                break;

                            case 0x60:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "unpack low packed data";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpunpcklbw";
                                        else
                                            LastDisassembleData.OpCode = "punpcklbw";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unpack low packed data";
                                        LastDisassembleData.OpCode = "punpcklbw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x61:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "unpack low packed data";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "punpcklwd";
                                        else
                                            LastDisassembleData.OpCode = "punpcklwd";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unpack low packed data";
                                        LastDisassembleData.OpCode = "punpcklwd";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x62:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "unpack low packed data";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpunpckldq";
                                        else
                                            LastDisassembleData.OpCode = "punpckldq";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unpack low packed data";
                                        LastDisassembleData.OpCode = "punpckldq";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x63:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "pack with signed saturation";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "packsswb";
                                        else
                                            LastDisassembleData.OpCode = "vpacksswb";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "pack with signed saturation";
                                        LastDisassembleData.OpCode = "packsswb";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x64:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed compare for greater than";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpcmpgtb";
                                        else
                                            LastDisassembleData.OpCode = "pcmpgtb";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed compare for greater than";
                                        LastDisassembleData.OpCode = "pcmpgtb";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x65:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed compare for greater than";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpcmpgtw";
                                        else
                                            LastDisassembleData.OpCode = "pcmpgtw";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed compare for greater than";
                                        LastDisassembleData.OpCode = "pcmpgtw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x66:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed compare for greater than";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpcmpgtd";
                                        else
                                            LastDisassembleData.OpCode = "pcmpgtd";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "packed compare for greater than";
                                        LastDisassembleData.OpCode = "pcmpgtd";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;


                            case 0x67:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "pack with unsigned saturation";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpackuswb";
                                        else
                                            LastDisassembleData.OpCode = "packuswb";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "pack with unsigned saturation";
                                        LastDisassembleData.OpCode = "packuswb";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x68:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "unpack high packed data";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpunpckhbw";
                                        else
                                            LastDisassembleData.OpCode = "punpckhbw";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unpack high packed data";
                                        LastDisassembleData.OpCode = "punpckhbw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x69:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "unpack high packed data";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpunpckhwd";
                                        else
                                            LastDisassembleData.OpCode = "punpckhwd";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unpack high packed data";
                                        LastDisassembleData.OpCode = "punpckhwd";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x6a:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "unpack high packed data";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpunpckhdq";
                                        else
                                            LastDisassembleData.OpCode = "punpckhdq";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "unpack high packed data";
                                        LastDisassembleData.OpCode = "punpckhdq";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x6b:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "pack with signed saturation";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "packssdw";
                                        else
                                            LastDisassembleData.OpCode = "packssdw";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "pack with signed saturation";
                                        LastDisassembleData.OpCode = "packssdw";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x6c:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "unpack low packed data";
                                        LastDisassembleData.OpCode = "punpcklqdq";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x6d:
                                {
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "unpack high packed data";
                                        LastDisassembleData.OpCode = "punpckhqdq";
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;


                            case 0x6e:
                                {
                                    //lastdisassembledata.isfloat:=true; //not sure
                                    if (RexW)
                                    {
                                        description = "move quadword";
                                        LastDisassembleData.OpCode = "movq";
                                    }
                                    else
                                    {
                                        description = "move doubleword";
                                        LastDisassembleData.OpCode = "movd";
                                    }

                                    if (_hasVex)
                                        LastDisassembleData.OpCode = 'v' + LastDisassembleData.OpCode;

                                    _opCodeFlags.SkipExtraReg = true;
                                    if (_prefix2.Contains(0x66))
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);
                                    else
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 0, ref last, ATmrPos.Right);

                                    offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                }
                                break;

                            case 0x6f:
                                {
                                    if (_prefix2.Contains(0xf3))
                                    {

                                        description = "move unaligned double quadword";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vmovdqu";
                                        else
                                            LastDisassembleData.OpCode = "movdqu";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

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
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                    else
                                    {
                                        description = "move 64 bits";
                                        LastDisassembleData.OpCode = "movq";
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last);

                                        offset = (UIntPtr)((last - 1) + offset.ToUInt64());
                                    }
                                }
                                break;

                            case 0x70:
                                {
                                    if (_prefix2.Contains(0xf2))
                                    {

                                        description = "shuffle packed low words";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpshuflw";
                                        else
                                            LastDisassembleData.OpCode = "pshuflw";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);

                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters += IntToHexSigned(LastDisassembleData.ParameterValue, 2);
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    if (_prefix2.Contains(0xf3))
                                    {

                                        description = "shuffle packed high words";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpshufhw";
                                        else
                                            LastDisassembleData.OpCode = "pshufhw";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters += IntToHexSigned(LastDisassembleData.ParameterValue, 2);
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    if (_prefix2.Contains(0x66))
                                    {
                                        description = "packed shuffle doubleword";
                                        LastDisassembleData.OpCode = "pshufd";

                                        LastDisassembleData.Parameters = Xmm(memory[2]) + ModRm(memory, _prefix2, 2, 4, ref last, ATmrPos.Right);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters += IntToHexSigned(LastDisassembleData.ParameterValue, 2);
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                    else
                                    {
                                        description = "packed shuffle word";
                                        if (_hasVex)
                                            LastDisassembleData.OpCode = "vpshufw";
                                        else
                                            LastDisassembleData.OpCode = "pshufw";

                                        _opCodeFlags.SkipExtraReg = true;
                                        LastDisassembleData.Parameters = Mm(memory[2]) + ModRm(memory, _prefix2, 2, 3, ref last, ATmrPos.Right);
                                        LastDisassembleData.ParameterValueType = ADisassemblerValueType.Value;
                                        LastDisassembleData.ParameterValue = (UIntPtr)memory[(int)last];
                                        LastDisassembleData.Parameters += IntToHexSigned(LastDisassembleData.ParameterValue, 2);
                                        offset = (UIntPtr)(offset.ToUInt64() + last);
                                    }
                                }
                                break;

                            case 0x71:
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
                                                        LastDisassembleData.OpCode = "vpsrlw";
                                                    else
                                                        LastDisassembleData.OpCode = "psrlw";

                                                    LastDisassembleData.Parameters = Xmm(memory[2]) + ',' + IntToHexSigned((UIntPtr)memory[3], 2);
                                                    offset += 3;
                                                }
                                                else
                                                {
                                                    description = "packed shift right logical";
                                                    LastDisassembleData.OpCode = "psrlw";
                                                    LastDisassembleData.Parameters = Mm(memory[2]) + ',' + IntToHexSigned((UIntPtr)memory[3], 2);
                                                    offset += 3;
                                                }
                                            }
                                            break;

                                        case 4:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "shift packed data right arithmetic";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpsraw";
                                                    else
                                                        LastDisassembleData.OpCode = "psraw";

                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last);
                                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);

                                                    offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 1);
                                                }
                                                else
                                                {
                                                    description = "packed shift left logical";
                                                    LastDisassembleData.OpCode = "psraw";
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 3, ref last);
                                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);

                                                    offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 1);
                                                }
                                            }
                                            break;

                                        case 6:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "packed shift left logical";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpsllw";
                                                    else
                                                        LastDisassembleData.OpCode = "psllw";

                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last);
                                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);

                                                    offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 1);
                                                }
                                                else
                                                {
                                                    description = "packed shift left logical";
                                                    LastDisassembleData.OpCode = "psllw";
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 3, ref last);
                                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);

                                                    offset = (UIntPtr)(offset.ToUInt64() + last - 1 + 1);
                                                }
                                            }
                                            break;
                                    }
                                }
                                break;

                            case 0x72:
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
                                                        LastDisassembleData.OpCode = "vpsrld";
                                                    else
                                                        LastDisassembleData.OpCode = "psrld";
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last);
                                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                                else
                                                {
                                                    description = "packed shift right logical";
                                                    LastDisassembleData.OpCode = "psrld";
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 3, ref last);
                                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                            }
                                            break;

                                        case 4:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "packed shift right arithmetic";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "vpsrad";
                                                    else
                                                        LastDisassembleData.OpCode = "psrad";
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last);
                                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);

                                                    offset += 3;
                                                }
                                                else
                                                {
                                                    description = "packed shift right arithmetic";
                                                    LastDisassembleData.OpCode = "psrad";
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 3, ref last);
                                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);

                                                    offset += 3;
                                                }
                                            }
                                            break;

                                        case 6:
                                            {
                                                if (_prefix2.Contains(0x66))
                                                {
                                                    description = "packed shift left logical";
                                                    if (_hasVex)
                                                        LastDisassembleData.OpCode = "pslld";
                                                    else
                                                        LastDisassembleData.OpCode = "pslld";

                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 4, ref last);
                                                    LastDisassembleData.Parameters = LastDisassembleData.Parameters + ',' + IntToHexSigned((UIntPtr)memory[(int)last], 2);
                                                    offset += 3;
                                                }
                                                else
                                                {
                                                    description = "packed shift left logical";
                                                    LastDisassembleData.OpCode = "pslld";
                                                    LastDisassembleData.Parameters = ModRm(memory, _prefix2, 2, 3, ref last);
                                                    LastDisassembleData.Parameters += IntToHexSigned((UIntPtr)memory[(int)last], 2);
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
