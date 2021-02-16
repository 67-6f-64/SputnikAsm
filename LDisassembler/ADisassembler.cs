using System;
using System.Deployment.Internal;
using System.Windows.Forms;
using Sputnik.LBinary;
using Sputnik.LInterfaces;
using Sputnik.LMarshal;
using Sputnik.LString;
using Sputnik.LUtils;
using SputnikAsm.LAssembler.LEnums;
using SputnikAsm.LBinary;
using SputnikAsm.LBinary.LByteInterpreter;
using SputnikAsm.LCollections;
using SputnikAsm.LDisassembler.LEnums;
using SputnikAsm.LExtensions;
using SputnikAsm.LGenerics;
using SputnikAsm.LMemScan.LEnums;
using SputnikAsm.LProcess;
using SputnikAsm.LProcess.LNative;
using SputnikAsm.LProcess.Utilities;
using SputnikAsm.LSymbolHandler;
using SputnikAsm.LUtils;

namespace SputnikAsm.LDisassembler
{
    public class ADisassembler : IUDisposable
    {
        #region Internal Classes
        #region ADecodeValue
        internal class ADecodeValue
        {
            #region Variables
            public UIntPtr Value;
            public AVariableType Type;
            public Boolean IsAddress;
            public String S;
            #endregion
            #region Constructor
            public ADecodeValue()
            {
                Value = UIntPtr.Zero;
                Type = AVariableType.Byte;
                IsAddress = false;
                S = "";
            }
            #endregion
        }
        #endregion
        #endregion
        #region Constants
        const int BIT_REX_W = 8;
        const int BIT_REX_R = 4;
        const int BIT_REX_X = 2;
        const int BIT_REX_B = 1;
        #endregion
        #region Properties
        public Boolean RexB => OpCodeFlags.B;
        public Boolean RexX => OpCodeFlags.X;
        public Boolean RexR => OpCodeFlags.R;
        public Boolean RexW => OpCodeFlags.W;
        public AProcessSharp Proc => SymbolHandler.Process;
        public Boolean IsDisposed { get; set; }
        #endregion
        #region Variables
        public UBytePtr Memory;
        public AOpCodeFlags OpCodeFlags;
        public APrefix Prefix;
        public APrefix Prefix2;
        public Boolean HasVex;
        public Boolean RipRelative;
        public String ColorHex;
        public String ColorReg;
        public String ColorSymbol;
        public String EndColor;
        public ATmrPos ModRmPosition;
        public Byte RexPrefix;
        public Boolean AggressiveAlignment;
        public ALastDisassembleData LastDisassembleData;
        public Boolean Debug;
        public Boolean ShowSymbols;
        public Boolean ShowModules;
        public Boolean ShowSections;
        public Boolean IsDataOnly;
        public Boolean Is64Bit;
        public Boolean Is64BitOverride;
        public Boolean Is64BitOverrideState;
        public Boolean MarkIpRelativeInstructions;
        public Boolean SyntaxHighlighting;
        public ASymbolHandler SymbolHandler;
        public Boolean SupportCloak;
        #endregion
        #region Constructor
        public ADisassembler(ASymbolHandler symbolHandler)
        {
            IsDisposed = false;
            SymbolHandler = symbolHandler;
            RexPrefix = 0;
            ColorHex = "";
            ColorReg = "";
            ColorSymbol = "";
            EndColor = "";
            OpCodeFlags = new AOpCodeFlags();
            LastDisassembleData = new ALastDisassembleData();
            Debug = false;
            SyntaxHighlighting = false;
            ModRmPosition = ATmrPos.None;
            AggressiveAlignment = false;
            Memory = new UBytePtr(64);
            HasVex = false;
            RipRelative = false;
            Prefix = new APrefix();
            Prefix2 = new APrefix();
            ShowSymbols = false;
            ShowModules = false;
            ShowSections = false;
            IsDataOnly = false;
            Is64Bit = false;
            Is64BitOverride = false;
            Is64BitOverrideState = false;
            MarkIpRelativeInstructions = false;
            SyntaxHighlighting = false;
            SupportCloak = false;
        }
        #endregion
        #region Dispose
        public void Dispose()
        {
            if (IsDisposed)
                return;
            IsDisposed = true;
        }
        #endregion
        #region RegNrToStr
        public String RegNrToStr(ARegisterType listType, int nr)
        {
            var result = "Error";
            switch (listType)
            {
                case ARegisterType.Rt8:
                    {
                        switch (nr)
                        {
                            case 0: result = "al"; break;
                            case 1: result = "cl"; break;
                            case 2: result = "dl"; break;
                            case 3: result = "bl"; break;
                            case 4: result = RexPrefix == 0 ? "ah" : "spl"; break;
                            case 5: result = RexPrefix == 0 ? "ch" : "bpl"; break;
                            case 6: result = RexPrefix == 0 ? "dh" : "sil"; break;
                            case 7: result = RexPrefix == 0 ? "bh" : "dil"; break;
                            case 8: result = "r8l"; break;
                            case 9: result = "r9l"; break;
                            case 10: result = "r10l"; break;
                            case 11: result = "r11l"; break;
                            case 12: result = "r12l"; break;
                            case 13: result = "r13l"; break;
                            case 14: result = "r14l"; break;
                            case 15: result = "r15l"; break;
                        }
                    }
                    break;

                case ARegisterType.Rt16:
                    {
                        switch (nr)
                        {
                            case 0: result = "ax"; break;
                            case 1: result = "cx"; break;
                            case 2: result = "dx"; break;
                            case 3: result = "bx"; break;
                            case 4: result = "sp"; break;
                            case 5: result = "bp"; break;
                            case 6: result = "si"; break;
                            case 7: result = "di"; break;
                            case 8: result = "r8w"; break;
                            case 9: result = "r9w"; break;
                            case 10: result = "r10w"; break;
                            case 11: result = "r11w"; break;
                            case 12: result = "r12w"; break;
                            case 13: result = "r13w"; break;
                            case 14: result = "r14w"; break;
                            case 15: result = "r15w"; break;
                        }
                    }
                    break;

                case ARegisterType.Rt32:
                    {
                        switch (nr)
                        {
                            case 0: result = "eax"; break;
                            case 1: result = "ecx"; break;
                            case 2: result = "edx"; break;
                            case 3: result = "ebx"; break;
                            case 4: result = "esp"; break;
                            case 5: result = "ebp"; break;
                            case 6: result = "esi"; break;
                            case 7: result = "edi"; break;
                            case 8: result = "r8d"; break;
                            case 9: result = "r9d"; break;
                            case 10: result = "r10d"; break;
                            case 11: result = "r11d"; break;
                            case 12: result = "r12d"; break;
                            case 13: result = "r13d"; break;
                            case 14: result = "r14d"; break;
                            case 15: result = "r15d"; break;
                        }
                    }
                    break;

                case ARegisterType.Rt64:
                    {
                        switch (nr)
                        {
                            case 0: result = "rax"; break;
                            case 1: result = "rcx"; break;
                            case 2: result = "rdx"; break;
                            case 3: result = "rbx"; break;
                            case 4: result = "rsp"; break;
                            case 5: result = "rbp"; break;
                            case 6: result = "rsi"; break;
                            case 7: result = "rdi"; break;
                            case 8: result = "r8"; break;
                            case 9: result = "r9"; break;
                            case 10: result = "r10"; break;
                            case 11: result = "r11"; break;
                            case 12: result = "r12"; break;
                            case 13: result = "r13"; break;
                            case 14: result = "r14"; break;
                            case 15: result = "r15"; break;
                        }
                    }
                    break;

                case ARegisterType.RtDebugRegister:
                    {
                        switch (nr)
                        {
                            case 0: result = "dr0"; break;
                            case 1: result = "dr1"; break;
                            case 2: result = "dr2"; break;
                            case 3: result = "dr3"; break;
                            case 4: result = "dr4"; break;
                            case 5: result = "dr5"; break;
                            case 6: result = "dr6"; break;
                            case 7: result = "dr7"; break;
                            case 8: result = "dr8"; break;//Do not exist, but let's implement the encoding
                            case 9: result = "dr9"; break;
                            case 10: result = "dr10"; break;
                            case 11: result = "dr11"; break;
                            case 12: result = "dr12"; break;
                            case 13: result = "dr13"; break;
                            case 14: result = "dr14"; break;
                            case 15: result = "dr15"; break;
                        }
                    }
                    break;

                case ARegisterType.RtControlRegister:
                    {
                        switch (nr)
                        {
                            case 0: result = "cr0"; break;
                            case 1: result = "cr1"; break;
                            case 2: result = "cr2"; break;
                            case 3: result = "cr3"; break;
                            case 4: result = "cr4"; break;
                            case 5: result = "cr5"; break;
                            case 6: result = "cr6"; break;
                            case 7: result = "cr7"; break;
                            case 8: result = "cr8"; break;
                            case 9: result = "cr9"; break;
                            case 10: result = "cr10"; break;
                            case 11: result = "cr11"; break;
                            case 12: result = "cr12"; break;
                            case 13: result = "cr13"; break;
                            case 14: result = "cr14"; break;
                            case 15: result = "cr15"; break;
                        }
                    }
                    break;
                case ARegisterType.RtSegment:
                    {
                        switch (nr)
                        {
                            case 0: result = "es"; break;
                            case 1: result = "cs"; break;
                            case 2: result = "ss"; break;
                            case 3: result = "ds"; break;
                            case 4: result = "fs"; break;
                            case 5: result = "gs"; break;
                            case 6: result = "hs"; break;  //as if...
                            case 7: result = "is"; break;
                            case 8: result = "js"; break;
                            case 9: result = "ks"; break;
                            case 10: result = "ls"; break;
                            case 11: result = "ms"; break;
                            case 12: result = "ns"; break;
                            case 13: result = "os"; break;
                            case 14: result = "ps"; break;
                            case 15: result = "qs"; break;
                        }
                    }
                    break;

                case ARegisterType.RtMm:
                    {
                        switch (nr)
                        {
                            case 0: result = "mm0"; break;
                            case 1: result = "mm1"; break;
                            case 2: result = "mm2"; break;
                            case 3: result = "mm3"; break;
                            case 4: result = "mm4"; break;
                            case 5: result = "mm5"; break;
                            case 6: result = "mm6"; break;
                            case 7: result = "mm7"; break;
                            case 8: result = "mm8"; break;
                            case 9: result = "mm9"; break;
                            case 10: result = "mm10"; break;
                            case 11: result = "mm11"; break;
                            case 12: result = "mm12"; break;
                            case 13: result = "mm13"; break;
                            case 14: result = "mm14"; break;
                            case 15: result = "mm15"; break;
                        }
                    }
                    break;

                case ARegisterType.RtXmm:
                    {
                        switch (nr)
                        {
                            case 0: result = "xmm0"; break;
                            case 1: result = "xmm1"; break;
                            case 2: result = "xmm2"; break;
                            case 3: result = "xmm3"; break;
                            case 4: result = "xmm4"; break;
                            case 5: result = "xmm5"; break;
                            case 6: result = "xmm6"; break;
                            case 7: result = "xmm7"; break;
                            case 8: result = "xmm8"; break;
                            case 9: result = "xmm9"; break;
                            case 10: result = "xmm10"; break;
                            case 11: result = "xmm11"; break;
                            case 12: result = "xmm12"; break;
                            case 13: result = "xmm13"; break;
                            case 14: result = "xmm14"; break;
                            case 15: result = "xmm15"; break;
                        }
                    }
                    break;

                case ARegisterType.RtYmm:
                    {
                        switch (nr)
                        {
                            case 0: result = "ymm0"; break;
                            case 1: result = "ymm1"; break;
                            case 2: result = "ymm2"; break;
                            case 3: result = "ymm3"; break;
                            case 4: result = "ymm4"; break;
                            case 5: result = "ymm5"; break;
                            case 6: result = "ymm6"; break;
                            case 7: result = "ymm7"; break;
                            case 8: result = "ymm8"; break;
                            case 9: result = "ymm9"; break;
                            case 10: result = "ymm10"; break;
                            case 11: result = "ymm11"; break;
                            case 12: result = "ymm12"; break;
                            case 13: result = "ymm13"; break;
                            case 14: result = "ymm14"; break;
                            case 15: result = "ymm15"; break;
                        }
                    }
                    break;
            }
            return result;
        }
        #endregion
        #region Rd
        public String Rd(Byte bt)
        {
            String result;
            if (RexB)
                bt |= 8;
            switch (bt)
            {
                case 0: result = "eax"; break;
                case 1: result = "ecx"; break;
                case 2: result = "edx"; break;
                case 3: result = "ebx"; break;
                case 4: result = "esp"; break;
                case 5: result = "ebp"; break;
                case 6: result = "esi"; break;
                case 7: result = "edi"; break;
                case 8: result = "r8"; break;
                case 9: result = "r9"; break;
                case 10: result = "r10"; break;
                case 11: result = "r11"; break;
                case 12: result = "r12"; break;
                case 13: result = "r13"; break;
                case 14: result = "r14"; break;
                case 15: result = "r15"; break;
                default: result = ""; break;
            }
            if (!RexW)
            {
                //not a rex_w field
                if (bt >= 8)  //but the bt field is higher than 8 (so 32-bit addressing, increased register)
                    result += 'd'; //32-bit variant
            }
            else
            {
                using (var p = new UCharPtr(result))
                    p[0] = 'r'; //replace eax,ebx with rax,rbx...
            }
            result = ColorReg + result + EndColor;
            return result;
        }
        #endregion
        #region Rd8
        public String Rd8(Byte bt)
        {
            String result;
            if (RexB)
                bt |= 8;
            switch (bt)
            {
                case 0: result = "al"; break;
                case 1: result = "cl"; break;
                case 2: result = "dl"; break;
                case 3: result = "bl"; break;
                case 4: result = RexPrefix == 0 ? "ah" : "spl"; break;
                case 5: result = RexPrefix == 0 ? "ch" : "bpl"; break;
                case 6: result = RexPrefix == 0 ? "dh" : "sil"; break;
                case 7: result = RexPrefix == 0 ? "bh" : "dil"; break;
                case 8: result = "r8l"; break;
                case 9: result = "r9l"; break;
                case 10: result = "r10l"; break;
                case 11: result = "r11l"; break;
                case 12: result = "r12l"; break;
                case 13: result = "r13l"; break;
                case 14: result = "r14l"; break;
                case 15: result = "r15l"; break;
                default: result = ""; break;
            }
            result = ColorReg + result + EndColor;
            return result;
        }
        #endregion
        #region Rd16
        public String Rd16(Byte bt)
        {
            String result;
            if (RexB)
                bt |= 8;
            switch (bt)
            {
                case 0: result = "ax"; break;
                case 1: result = "cx"; break;
                case 2: result = "dx"; break;
                case 3: result = "bx"; break;
                case 4: result = "sp"; break;
                case 5: result = "bp"; break;
                case 6: result = "si"; break;
                case 7: result = "di"; break;
                case 8: result = "r8w"; break;
                case 9: result = "r9w"; break;
                case 10: result = "r10w"; break;
                case 11: result = "r11w"; break;
                case 12: result = "r12w"; break;
                case 13: result = "r13w"; break;
                case 14: result = "r14w"; break;
                case 15: result = "r15w"; break;
                default: result = ""; break;
            }
            result = ColorReg + result + EndColor;
            return result;
        }
        #endregion
        #region GetReg
        public Byte GetReg(Byte bt)
        {
            var result = (bt >> 3) & 7;
            if (RexR)
                result |= 8; //extend the reg field
            return (Byte)result;
        }
        #endregion
        #region GetSegmentOverride
        public String GetSegmentOverride(APrefix prefix)
        {
            var result = "";
            if (prefix.Contains(0x2e))
                result = "cs:";
            else if (prefix.Contains(0x26))
                result = "es:";
            else if (prefix.Contains(0x36))
                result = "ss:";
            else if (prefix.Contains(0x3e))
                result = "";
            else if (prefix.Contains(0x64))
                result = "fs:";
            else if (prefix.Contains(0x65))
                result = "gs:";
            return result;
        }
        #endregion
        #region GetBitOf
        public Byte GetBitOf(UInt64 bt, int bit)
        {
            var result = ABitUtils.GetBit(bit, bt);
            return (Byte)result;
        }
        #endregion
        #region GetMod
        public Byte GetMod(Byte bt)
        {
            var result = (bt >> 6) & 3;
            return (Byte) result;
        }
        #endregion
        #region R8
        public String R8(Byte bt)
        {
            var regNr = GetReg(bt);
            var result = ColorReg + RegNrToStr(ARegisterType.Rt8, regNr) + EndColor;
            return result;
        }
        #endregion
        #region R16
        public String R16(Byte bt)
        {
            var regNr = GetReg(bt);
            var result = ColorReg + RegNrToStr(ARegisterType.Rt16, regNr) + EndColor;
            return result;
        }
        #endregion
        #region R32
        public String R32(Byte bt)
        {
            var regNr = GetReg(bt);
            String result;
            if (RexW)
                result = ColorReg + RegNrToStr(ARegisterType.Rt64, regNr) + EndColor;
            else
                result = ColorReg + RegNrToStr(ARegisterType.Rt32, regNr) + EndColor;
            return result;
        }
        #endregion
        #region R64
        public String R64(Byte bt)
        {
            var regNr = GetReg(bt);
            var result = ColorReg + RegNrToStr(ARegisterType.Rt64, regNr) + EndColor;
            return result;
        }
        #endregion
        #region Xmm
        public String Xmm(Byte bt)
        {
            var regNr = GetReg(bt);
            String result;
            if (OpCodeFlags.L)
                result = ColorReg + RegNrToStr(ARegisterType.RtYmm, regNr) + EndColor;
            else
                result = ColorReg + RegNrToStr(ARegisterType.RtXmm, regNr) + EndColor;
            return result;
        }
        #endregion
        #region Mm
        public String Mm(Byte bt)
        {
            var regNr = GetReg(bt);
            var result = ColorReg + RegNrToStr(ARegisterType.RtMm, regNr) + EndColor;
            return result;
        }
        #endregion
        #region SReg
        public String SReg(Byte bt)
        {
            var regNr = GetReg(bt);
            var result = ColorReg + RegNrToStr(ARegisterType.RtSegment, regNr) + EndColor;
            return result;
        }
        #endregion
        #region Cr
        public String Cr(Byte bt)
        {
            var regNr = GetReg(bt);
            var result = ColorReg + RegNrToStr(ARegisterType.RtControlRegister, regNr) + EndColor;
            return result;
        }
        #endregion
        #region Dr
        public String Dr(Byte bt)
        {
            var regNr = GetReg(bt);
            var result = ColorReg + RegNrToStr(ARegisterType.RtDebugRegister, regNr) + EndColor;
            return result;
        }
        #endregion
        #region GetRm
        public Byte GetRm(Byte bt)
        {
            var result = bt & 7;
            //if this instruction does NOT have a SIB byte, only then apply the rex_B bit
            //It has an SIB byte if RM==4 and mod!=3
            if (RexB & (!((result == 4) && (GetMod(bt) != 3))))
                result |= 8;
            return (Byte)result;
        }
        #endregion
        #region ModRm
        public String ModRm(UBytePtr memory, APrefix prefix, int modRmByte, int inst, ref UInt32 last, int operandSize, int addressSize = 0, ATmrPos position = ATmrPos.Left)
        {
            var result  = ModRm2(memory, prefix, modRmByte, inst, ref last, operandSize, addressSize, position);
            return result;
        }
        public String ModRm(UBytePtr memory, APrefix prefix, int modRmByte, int inst,  ref UInt32 last, ATmrPos position = ATmrPos.Left)
        {
            switch (inst)
            {
                case 0:
                    LastDisassembleData.DataSize = Proc.PointerSize;
                    break;
                case 1:
                    LastDisassembleData.DataSize = 2;
                    break;
                case 2:
                    LastDisassembleData.DataSize = 1;
                    break;
                case 3:
                    LastDisassembleData.DataSize = 4;
                    break;
                case 4:
                    LastDisassembleData.DataSize = 8;
                    break;
            }
            var result = ModRm2(memory, prefix, modRmByte, inst, ref last, 0, 0, position);
            return result;
        }
        #endregion
        #region ModRm2
        public String ModRm2(UBytePtr memory, APrefix prefix, int modRmByte, int inst, ref UInt32 last, int operandSize = 0, int addressSize = 0, ATmrPos position = ATmrPos.Left)
        {
            ModRmPosition = position;
            var result = "";
            var showExtraReg = HasVex & (OpCodeFlags.SkipExtraReg == false);
            var regPrefix = Is64Bit ? 'r' : 'e';
            var ep = "";
            String preStr;
            String postStr;
            switch (position)
            {
                case ATmrPos.Left:
                case ATmrPos.None:
                    preStr = "";
                    postStr = ",";
                    break;
                case ATmrPos.Right:
                    preStr = ",";
                    postStr = "";
                    break;
                default:
                    preStr = "";
                    postStr = "";
                    break;
            }
            String operandString;
            switch (operandSize)
            {
                case 8:
                    operandString = "byte ptr ";
                    break;
                case 16:
                    operandString = "word ptr ";
                    break;
                case 32:
                    operandString = "dword ptr ";
                    break;
                case 64:
                    operandString = "qword ptr ";
                    break;
                case 80:
                    operandString = "tword ptr ";
                    break;
                case 128:
                    operandString = "dqword ptr ";
                    break;
                case 256:
                    operandString = "YMMword ptr ";
                    break;
                default:
                    operandString = "";
                    break;
            }
            LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = modRmByte;
            LastDisassembleData.SeparatorCount += 1;
            last = (UInt32)(modRmByte + 1);
            LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = (int)last;
            LastDisassembleData.SeparatorCount += 1;
            if (!Proc.IsX64 & prefix.Contains(0x67))
            {
                // put some 16-bit stuff in here
                // but since this is a 32-bit debugger only ,forget it...
            }
            else
            {
                switch (GetMod(memory[modRmByte]))
                {
                    case 0:
                        {
                            if (showExtraReg & OpCodeFlags.SkipExtraRegOnMemoryAccess)
                                showExtraReg = false;
                            switch (GetRm(memory[modRmByte]))
                            {
                                case 0:
                                    result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "ax" + EndColor + ']';
                                    break;
                                case 1:
                                    result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "cx" + EndColor + ']';
                                    break;
                                case 2:
                                    result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "dx" + EndColor + ']';
                                    break;
                                case 3:
                                    result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "bx" + EndColor + ']';
                                    break;
                                case 4:
                                    //has an sib
                                    result = GetSegmentOverride(prefix) + '[' + Sib(memory, modRmByte + 1, ref last, addressSize) + ']';
                                    break;
                                case 5:
                                    {
                                        //followed by a disp32
                                        if (Is64Bit)
                                        {
                                            var value = (UIntPtr)memory.ReadUInt64(modRmByte + 1);
                                            RipRelative = true;
                                            result = GetSegmentOverride(prefix) + '[' + IntToHexSignedWithoutSymbols(value, 8) + ']';
                                            LastDisassembleData.ModRmValueType = ADisassemblerValueType.Address;
                                            LastDisassembleData.ModRmValue = value;
                                            LastDisassembleData.RipRelative = modRmByte + 1;

                                        }
                                        else
                                        {
                                            var value = (UIntPtr)memory.ReadUInt32(modRmByte + 1);
                                            result = GetSegmentOverride(prefix) + '[' + IntToHexSigned(value, 8) + ']';
                                            LastDisassembleData.ModRmValueType = ADisassemblerValueType.Address;
                                            LastDisassembleData.ModRmValue = value;
                                        }
                                        last += 4;
                                    }
                                    break;
                                case 6:
                                    result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "si" + EndColor + ']';
                                    break;
                                case 7:
                                    result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "di" + EndColor + ']';
                                    break;
                                case 8:
                                    result = GetSegmentOverride(prefix) + '[' + ColorReg + "r8" + EndColor + ']';
                                    break;
                                case 9:
                                    result = GetSegmentOverride(prefix) + '[' + ColorReg + "r9" + EndColor + ']';
                                    break;
                                case 10:
                                    result = GetSegmentOverride(prefix) + '[' + ColorReg + "r10" + EndColor + ']';
                                    break;
                                case 11:
                                    result = GetSegmentOverride(prefix) + '[' + ColorReg + "r11" + EndColor + ']';
                                    break;
                                case 12:
                                    result = GetSegmentOverride(prefix) + '[' + ColorReg + "r12" + EndColor + ']';
                                    break;
                                case 13:
                                    result = GetSegmentOverride(prefix) + '[' + ColorReg + "r13" + EndColor + ']';
                                    break;
                                case 14:
                                    result = GetSegmentOverride(prefix) + '[' + ColorReg + "r14" + EndColor + ']';
                                    break;
                                case 15:
                                    result = GetSegmentOverride(prefix) + '[' + ColorReg + "r15" + EndColor + ']';
                                    break;
                            }
                            if (operandSize != 0)
                                LastDisassembleData.DataSize = operandSize / 8;
                            result = operandString + result;
                        }
                        break;
                    case 1:
                        {
                            if (showExtraReg & OpCodeFlags.SkipExtraRegOnMemoryAccess)
                                showExtraReg = false;
                            if (GetRm(memory[modRmByte]) != 4)
                            {
                                LastDisassembleData.ModRmValueType = ADisassemblerValueType.Value;
                                LastDisassembleData.ModRmValue = (UIntPtr)memory[modRmByte + 1];
                            }
                            switch (GetRm(memory[modRmByte]))
                            {
                                case 0:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "ax" + EndColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "ax" + EndColor + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    break;
                                case 1:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "cx" + EndColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "cx" + EndColor + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    break;
                                case 2:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "dx" + EndColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "dx" + EndColor + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    break;
                                case 3:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "bx" + EndColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "bx" + EndColor + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    break;
                                case 4:
                                    result = GetSegmentOverride(prefix) + '[' + Sib(memory, modRmByte + 1, ref last, addressSize) + ']';
                                    last -= 1;
                                    break;
                                case 5:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "bp" + EndColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "bp" + EndColor + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 6:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "si" + EndColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "si" + EndColor + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 7:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "di" + EndColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "di" + EndColor + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 8:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r8" + EndColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r8" + EndColor + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 9:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r9" + EndColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r9" + EndColor + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 10:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r10" + EndColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r10" + EndColor + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 11:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r11" + EndColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r11" + EndColor + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 12:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r12" + EndColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r12" + EndColor + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 13:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r13" + EndColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r13" + EndColor + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 14:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r14" + EndColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r14" + EndColor + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 15:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r15" + EndColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r15" + EndColor + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                            }
                            last += 1;
                            LastDisassembleData.DataSize = operandSize / 8;
                            result = operandString + result;
                        }
                        break;
                    case 2:
                        {
                            var value = (UIntPtr)memory.ReadUInt32(modRmByte + 1);
                            if (showExtraReg & OpCodeFlags.SkipExtraRegOnMemoryAccess)
                                showExtraReg = false;
                            if (GetRm(memory[modRmByte]) != 4)
                            {
                                LastDisassembleData.ModRmValueType = ADisassemblerValueType.Value;
                                LastDisassembleData.ModRmValue = value;
                            }
                            switch (GetRm(memory[modRmByte]))
                            {
                                case 0:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "ax" + EndColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "ax" + EndColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;

                                case 1:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "cx" + EndColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "cx" + EndColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;

                                case 2:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "dx" + EndColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "dx" + EndColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 3:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "bx" + EndColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "bx" + EndColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 4:
                                    result = GetSegmentOverride(prefix) + '[' + Sib(memory, modRmByte + 1, ref last, addressSize) + ']';
                                    last -= 4;
                                    break;
                                case 5:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "bp" + EndColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "bp" + EndColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 6:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "si" + EndColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "si" + EndColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 7:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "di" + EndColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + regPrefix + "di" + EndColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 8:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r8" + EndColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r8" + EndColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 9:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r9" + EndColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r9" + EndColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 10:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r10" + EndColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r10" + EndColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 11:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r11" + EndColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r11" + EndColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 12:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r12" + EndColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r12" + EndColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 13:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r13" + EndColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r13" + EndColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;

                                case 14:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r14" + EndColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r14" + EndColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 15:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r15" + EndColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + ColorReg + "r15" + EndColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                            }
                            last += 4;
                            LastDisassembleData.DataSize = operandSize / 8;
                            result = operandString + result;
                        }
                        break;
                    case 3:
                        {
                            LastDisassembleData.DataSize = 0;
                            switch (GetRm(memory[modRmByte]))
                            {
                                case 0:
                                    switch (inst)
                                    {
                                        case 0:
                                            result = RexW | (operandSize == 64) ? "rax" : "eax";
                                            break;
                                        case 1:
                                            result = "ax";
                                            break;
                                        case 2:
                                            result = "al";
                                            break;
                                        case 3:
                                            result = "mm0";
                                            break;
                                        case 4:
                                            result = OpCodeFlags.L ? "ymm0" : "xmm0";
                                            break;
                                    }
                                    break;
                                case 1:
                                    switch (inst)
                                    {
                                        case 0:
                                            result = RexW | (operandSize == 64) ? "rcx" : "ecx";
                                            break;
                                        case 1:
                                            result = "cx";
                                            break;
                                        case 2:
                                            result = "cl";
                                            break;
                                        case 3:
                                            result = "mm1";
                                            break;
                                        case 4:
                                            result = OpCodeFlags.L ? "ymm1" : "xmm1";
                                            break;
                                    }
                                    break;
                                case 2:
                                    switch (inst)
                                    {
                                        case 0:
                                            result = RexW | (operandSize == 64) ? "rdx" : "edx";
                                            break;
                                        case 1:
                                            result = "dx";
                                            break;
                                        case 2:
                                            result = "dl";
                                            break;
                                        case 3:
                                            result = "mm2";
                                            break;
                                        case 4:
                                            result = OpCodeFlags.L ? "ymm2" : "xmm2";
                                            break;
                                    }
                                    break;
                                case 3:
                                    switch (inst)
                                    {
                                        case 0:
                                            result = RexW | (operandSize == 64) ? "rbx" : "ebx";
                                            break;
                                        case 1:
                                            result = "bx";
                                            break;
                                        case 2:
                                            result = "bl";
                                            break;
                                        case 3:
                                            result = "mm3";
                                            break;
                                        case 4:
                                            result = OpCodeFlags.L ? "ymm3" : "xmm3";
                                            break;
                                    }
                                    break;
                                case 4:
                                    switch (inst)
                                    {
                                        case 0:
                                            result = RexW | (operandSize == 64) ? "rsp" : "esp";
                                            break;
                                        case 1:
                                            result = "sp";
                                            break;
                                        case 2:
                                            result = RexPrefix != 0 ? "spl" : "ah";
                                            break;
                                        case 3:
                                            result = "mm4";
                                            break;
                                        case 4:
                                            result = OpCodeFlags.L ? "ymm4" : "xmm4";
                                            break;
                                    }
                                    break;
                                case 5:
                                    switch (inst)
                                    {
                                        case 0:
                                            result = RexW | (operandSize == 64) ? "rbp" : "ebp";
                                            break;
                                        case 1:
                                            result = "bp";
                                            break;
                                        case 2:
                                            result = RexPrefix != 0 ? "bpl" : "ch";
                                            break;
                                        case 3:
                                            result = "mm5";
                                            break;
                                        case 4:
                                            result = OpCodeFlags.L ? "ymm5" : "xmm5";
                                            break;
                                    }
                                    break;
                                case 6:
                                    switch (inst)
                                    {
                                        case 0:
                                            result = RexW | (operandSize == 64) ? "rsi" : "esi";
                                            break;
                                        case 1:
                                            result = "si";
                                            break;
                                        case 2:
                                            result = RexPrefix != 0 ? "sil" : "dh";
                                            break;
                                        case 3:
                                            result = "mm6";
                                            break;
                                        case 4:
                                            result = OpCodeFlags.L ? "ymm6" : "xmm6";
                                            break;
                                    }
                                    break;
                                case 7:
                                    switch (inst)
                                    {
                                        case 0:
                                            result = RexW | (operandSize == 64) ? "rdi" : "edi";
                                            break;
                                        case 1:
                                            result = "di";
                                            break;
                                        case 2:
                                            result = RexPrefix != 0 ? "dil" : "bh";
                                            break;
                                        case 3:
                                            result = "mm7";
                                            break;
                                        case 4:
                                            result = OpCodeFlags.L ? "ymm7" : "xmm7";
                                            break;
                                    }
                                    break;
                                case 8:
                                    switch (inst)
                                    {
                                        case 0:
                                            result = RexW | (operandSize == 64) ? "r8" : "r8d";
                                            break;
                                        case 1:
                                            result = "r8w";
                                            break;
                                        case 2:
                                            result = "r8l";
                                            break;
                                        case 3:
                                            result = "mm8";
                                            break;
                                        case 4:
                                            result = OpCodeFlags.L ? "ymm8" : "xmm8";
                                            break;
                                    }
                                    break;
                                case 9:
                                    switch (inst)
                                    {
                                        case 0:
                                            result = RexW | (operandSize == 64) ? "r9" : "r9d";
                                            break;
                                        case 1:
                                            result = "r9w";
                                            break;
                                        case 2:
                                            result = "r9l";
                                            break;
                                        case 3:
                                            result = "mm9";
                                            break;
                                        case 4:
                                            result = OpCodeFlags.L ? "ymm9" : "xmm9";
                                            break;
                                    }
                                    break;
                                case 10:
                                    switch (inst)
                                    {
                                        case 0:
                                            result = RexW | (operandSize == 64) ? "r10" : "r10d";
                                            break;
                                        case 1:
                                            result = "r10w";
                                            break;
                                        case 2:
                                            result = "r10l";
                                            break;
                                        case 3:
                                            result = "mm10";
                                            break;
                                        case 4:
                                            result = OpCodeFlags.L ? "ymm10" : "xmm10";
                                            break;
                                    }
                                    break;
                                case 11:
                                    switch (inst)
                                    {
                                        case 0:
                                            result = RexW | (operandSize == 64) ? "r11" : "r11d";
                                            break;
                                        case 1:
                                            result = "r11w";
                                            break;
                                        case 2:
                                            result = "r11l";
                                            break;
                                        case 3:
                                            result = "mm11";
                                            break;
                                        case 4: 
                                            result = OpCodeFlags.L ? "ymm11" : "xmm11";
                                            break;
                                    }
                                    break;
                                case 12:
                                    switch (inst)
                                    {
                                        case 0:
                                            result = RexW | (operandSize == 64) ? "r12" : "r12d";
                                            break;
                                        case 1: result = "r12w";
                                            break;
                                        case 2: result = "r12l";
                                            break;
                                        case 3: result = "mm12";
                                            break;
                                        case 4: result = OpCodeFlags.L ? "ymm12" : "xmm12";
                                            break;
                                    }
                                    break;
                                case 13:
                                    switch (inst)
                                    {
                                        case 0:
                                            result = RexW | (operandSize == 64) ? "r13" : "r13d";
                                            break;
                                        case 1:
                                            result = "r13w";
                                            break;
                                        case 2:
                                            result = "r13l";
                                            break;
                                        case 3:
                                            result = "mm13";
                                            break;
                                        case 4:
                                            result = OpCodeFlags.L ? "ymm13" : "xmm13";
                                            break;
                                    }
                                    break;
                                case 14:
                                    switch (inst)
                                    {
                                        case 0:
                                            result = RexW | (operandSize == 64) ? "r14" : "r14d";
                                            break;
                                        case 1:
                                            result = "r14w";
                                            break;
                                        case 2:
                                            result = "r14l";
                                            break;
                                        case 3:
                                            result = "mm14";
                                            break;
                                        case 4:
                                            result = OpCodeFlags.L ? "ymm14" : "xmm14";
                                            break;
                                    }
                                    break;
                                case 15:
                                    switch (inst)
                                    {
                                        case 0:
                                            result = RexW | (operandSize == 64) ? "r15" : "r15d";
                                            break;
                                        case 1:
                                            result = "r15w"; break;
                                        case 2:
                                            result = "r15l"; break;
                                        case 3:
                                            result = "mm15"; break;
                                        case 4: result = OpCodeFlags.L ? "ymm15" : "xmm15";
                                            break;
                                    }
                                    break;
                            }
                            result = ColorReg + result + EndColor;
                        }
                        break;
                }
                if (showExtraReg)
                {
                    switch (inst)
                    {
                        case 0:
                            ep = RegNrToStr(RexW ? ARegisterType.Rt64 : ARegisterType.Rt32, ~OpCodeFlags.Vvvv & 0xf);
                            break;
                        case 1:
                            ep = RegNrToStr(ARegisterType.Rt16, ~OpCodeFlags.Vvvv & 0xf);
                            break;
                        case 2:
                            ep = RegNrToStr(ARegisterType.Rt8, ~OpCodeFlags.Vvvv & 0xf);
                            break;
                        case 3:
                            ep = RegNrToStr(ARegisterType.RtMm, ~OpCodeFlags.Vvvv & 0xf);
                            break;
                        case 4:
                            ep = RegNrToStr(OpCodeFlags.L ? ARegisterType.RtYmm : ARegisterType.RtXmm, ~OpCodeFlags.Vvvv & 0xf);
                            break;
                    }
                    switch (position)
                    {
                        case ATmrPos.Left:
                        case ATmrPos.None:
                            result = result + ',' + ColorReg + ep + EndColor;
                            break;
                        case ATmrPos.Right:
                            result = ColorReg + ep + EndColor + ',' + result;
                            break;
                    }
                }
                result = preStr + result + postStr;
            }
            if (last != (modRmByte + 1))  //add an extra seperator since some bytes have been added, usually the last one, except when the opcode has a immeadiate value followed, which this seperator will then separate
            {
                LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = (int)last;
                LastDisassembleData.SeparatorCount += 1;
            }
            return result;
        }
        #endregion
        #region Sib
        public String Sib(UBytePtr memory, int sibByte, ref UInt32 last, int addressSize = 0)
        {
            var result = "";
            last += 1;  //sib byte
            LastDisassembleData.Separators[LastDisassembleData.SeparatorCount] = (int)last;
            LastDisassembleData.SeparatorCount += 1;
            var ss = (memory[sibByte] >> 6) & 3;
            var index = (memory[sibByte] >> 3) & 7;
            if (RexX)
                index |= 8;
            var mod = GetMod(memory[sibByte - 1]);
            //var rm = GetRm(memory[sibByte - 1]);
            var mBase = memory[sibByte] & 7;
            if (RexB)
                /*and (_mod<>0)*/
                mBase |= 8;
            switch (mBase)
            {
                case 0:
                    result = "eax";
                    break;
                case 1:
                    result = "ecx";
                    break;
                case 2:
                    result = "edx";
                    break;
                case 3:
                    result = "ebx";
                    break;
                case 4:
                    result = "esp";
                    break;
                case 5:
                    if (mod != 0)
                        result = "ebp";
                    break;
                case 6:
                    result = "esi";
                    break;
                case 7:
                    result = "edi";
                    break;
                case 8:
                    result = "r8";
                    break;
                case 9:
                    result = "r9";
                    break;
                case 10:
                    result = "r10";
                    break;
                case 11:
                    result = "r11";
                    break;
                case 12:
                    result = "r12";
                    break;
                case 13:
                    result = "r13";
                    break;
                case 14:
                    result = "r14";
                    break;
                case 15:
                    result = "r15";
                    break;
            }
            if (Is64Bit)
            {
                if (result != "")
                    result = "r" + result.Substring(1); //quick replace
            }
            if (result != "")
                result = ColorReg + result + EndColor;
            String indexString;
            switch (index)
            {
                case 0:
                    indexString = "eax";
                    break;
                case 1:
                    indexString = "ecx";
                    break;
                case 2:
                    indexString = "edx";
                    break;
                case 3:
                    indexString = "ebx";
                    break;
                case 4:
                    indexString = "";
                    break;//'esp';
                case 5:
                    indexString = "ebp";
                    break;
                case 6:
                    indexString = "esi";
                    break;
                case 7:
                    indexString = "edi";
                    break;
                case 8:
                    indexString = "r8";
                    break;
                case 9:
                    indexString = "r9";
                    break;
                case 10:
                    indexString = "r10";
                    break;
                case 11:
                    indexString = "r11";
                    break;
                case 12:
                    indexString = "r12";
                    break;
                case 13:
                    indexString = "r13";
                    break;
                case 14:
                    indexString = "r14";
                    break;
                case 15:
                    indexString = "r15";
                    break;
                default:
                    indexString = "";
                    break;
            }
            if (Is64Bit & (addressSize != 32))
            {
                if (indexString != "")
                    indexString = "r" + indexString.Substring(1); //quick replace
            }
            if (indexString != "")
                indexString = ColorReg + indexString + EndColor;
            if ((Is64Bit) & ((mBase & 7) == 5) && (index == 4) && (mod == 0))  //disp32
            {
                //special case for 64-bit
                //sib has a 32-bit displacement value (starting at 0000000000000000)
                var value = (UIntPtr)memory.ReadUInt64(sibByte - 1);
                LastDisassembleData.ModRmValueType = ADisassemblerValueType.Address;
                LastDisassembleData.ModRmValue = value;
                result = IntToHexSigned(value, 8);
                last += 4;
                return result;
            }
            switch (ss)
            {
                case 0:
                    LastDisassembleData.SibScaler = 1;
                    break;
                case 1:
                    LastDisassembleData.SibScaler = 2;
                    break;
                case 2:
                    LastDisassembleData.SibScaler = 4;
                    break;
                case 3:
                    LastDisassembleData.SibScaler = 8;
                    break;
            }
            if (ss > 0 && index != 4)
                indexString = indexString + '*' + ColorHex + LastDisassembleData.SibScaler + EndColor;
            if (indexString != "")
            {
                if (result == "")
                    result = indexString;
                else
                    result = result + '+' + indexString;
            }
            //mod 0 : [scaled index]+disp32
            //mod 1 : [scaled index]+disp8+ebp
            //mod 2 : [scaled index]+disp32+ebp
            var displacementString = "";
            switch (mod)
            {
                case 0: //sib with a mod of 0. scaled index + disp32
                    {
                        if (mBase == 5)
                        {
                            var value = (UIntPtr)memory.ReadUInt32(sibByte - 1);
                            LastDisassembleData.ModRmValueType = ADisassemblerValueType.Value;
                            LastDisassembleData.ModRmValue = value;
                            if ((int)value < 0)
                                displacementString = "-" + IntToHexSigned((UIntPtr)(-(int)value), 8);
                            else
                                displacementString = IntToHexSigned(value, 8);
                            last += 4;
                        }
                    }
                    break;
                case 1: //scaled index + ebp+ disp 8
                    {
                        var value = memory.ReadSByte(sibByte - 1);
                        //displacementstring:=colorreg+'EBP'+endcolor;
                        LastDisassembleData.ModRmValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ModRmValue = (UIntPtr)value;
                        if (value < 0)
                            displacementString = "-" + IntToHexSigned((UIntPtr)(-value), 2);
                        else
                            displacementString = IntToHexSigned((UIntPtr)value, 2);
                        last += 1;
                    }
                    break;

                case 2: //scaled index + ebp+disp 32
                    {
                        var value = (UIntPtr)memory.ReadUInt32(sibByte - 1);
                        //displacementstring:=colorreg+'EBP'+endcolor;
                        LastDisassembleData.ModRmValueType = ADisassemblerValueType.Value;
                        LastDisassembleData.ModRmValue = value;
                        if ((int)value < 0)
                            displacementString = "-" + IntToHexSigned((UIntPtr)(-(int)value), 8);
                        else
                            displacementString = IntToHexSigned(value, 8);
                        last += 4;
                    }
                    break;
            }
            if (result == "")
                result = displacementString;
            else
            {
                if (displacementString != "")
                {
                    if (displacementString[0] == '-')
                        result += displacementString; //already starts with a sign
                    else
                        result = result + '+' + displacementString;
                }
            }
            LastDisassembleData.HasSib = true;
            LastDisassembleData.SibIndex = index;
            if (Debug)
                result = result + " ss=" + ss + " index=" + index + " base=" + mBase;
            return result;
        }
        #endregion
        #region OpCode4Fst
        public int OpCode4Fst(String opCode)
        {
            var result = 3; //float
            if (opCode.Length >= 4)
            {
                switch (opCode[3])
                {
                    case 'c':
                    case 'e':
                    case 's':
                        result = 2;
                        break;
                }
            }
            return result;
        }
        #endregion
        #region OpCode3Fn
        public int OpCode3Fn(String opCode)
        {
            var result = 3; //float
            if (opCode.Length < 3)
                return result;
            switch (opCode[2])
            {
                case 's':
                    result = 2;
                    break; //fnst
            }
            return result;
        }
        #endregion
        #region OpCode3Fs
        public int OpCode3Fs(String opCode)
        {
            var result = 3; //float
            if (opCode.Length < 3)
                return result;
            switch (opCode[2])
            {
                case 't':
                    result = OpCode4Fst(opCode);
                    break; //fstxxx  (fst, fstp, fstcw, fstenv, fstsw)
            }
            return result;
        }
        #endregion
        #region OpCode2F
        public int OpCode2F(String opCode)
        {
            var result = 3;
            if (opCode.Length < 2)
                return result;
            switch (opCode[1])
            {
                case 'i':
                    result = 2;
                    break; //fixxxxx
                case 'n':
                    result = OpCode3Fn(opCode); break;
                case 's':
                    result = OpCode3Fs(opCode); break;
            }
            return result;
        }
        #endregion
        #region OpCodeToValueType
        public int OpCodeToValueType(String opCode)
        {
            var result = 2;
            if (opCode.Length < 1)
                return result;
            switch (opCode[0])
            {
                case 'f':
                    result = OpCode2F(opCode);
                    break;
            }
            return result;
        }
        #endregion
        #region GetLastByteString
        public String GetLastByteString()
        {
            var cloaked = false;
            var changed = false;
            var result = "";
            for (var i = 0; i < LastDisassembleData.Bytes.Length; i++)
            {
                if (SyntaxHighlighting)
                {
                    if (LastDisassembleData.IsCloaked)
                    {
                        //check if this byte is cloaked (due to pageboundaries)
                        // todo handle cloak
                        //cloaked = hascloakedregioninrange(LastDisassembleData.address + i, 1, va, pa);
                        //if (cloaked) result += "{C00FF00}"; //green
                    }
                    // todo add this!
                    changed = false;//hasaddressbeenchanged(LastDisassembleData.address + i);
                    if (changed)
                        result += "{C0000FF}"; //red
                }
                result += AStringUtils.IntToHex(LastDisassembleData.Bytes[i], 2);
                if (i < LastDisassembleData.PrefixSize)
                    result += ' ';
                else
                    for (var j = 0; j <= LastDisassembleData.SeparatorCount - 1; j++)
                        if (LastDisassembleData.Separators[j] == i + 1)   //followed by a seperator
                            result += ' ';
                if (SyntaxHighlighting & ((LastDisassembleData.IsCloaked & cloaked) || changed))
                {
                    result += "{N}"; //back to default
                    cloaked = false;
                    changed = false;
                }
            }
            return result;
        }
        #endregion
        #region Has4ByteHexString
        public Boolean Has4ByteHexString(String d, ref String hexString)
        {
            var result = false;
            var hexCount = 0;
            var lastHexCount = 0;
            var lastMatch = 0;
            for (var i = d.Length - 1; i >= 0; i--)
            {
                if (ACharUtils.InRange(d[i], 'a', 'f') || ACharUtils.InRange(d[i], 'A', 'F') || ACharUtils.InRange(d[i], '0', '9'))
                {
                    hexCount++;
                    if (hexCount > lastHexCount)
                    {
                        lastMatch = i;
                        lastHexCount = hexCount;
                    }
                }
                else
                    hexCount = 0;
            }
            if (lastHexCount >= 8)
            {
                //it has at least a 4 byte hexadecimal value, so an address specifier
                hexString = "$" + AStringUtils.Copy(d, lastMatch, lastHexCount);
                result = true;
            }
            return result;
        }
        #endregion
        #region HasAddress
        public Boolean HasAddress(String d, ref UIntPtr address, Object context = null)
        {
            var s = "";
            if (d == "")
                return false;
            if (AStringUtils.Pos(" ", d) == -1)
                return false;
            var result = false;
            //if the opcode has a , then get the last part
            var i = AStringUtils.Pos(",", d);
            if (i != -1)
                d = AStringUtils.Copy(d, i + 1, d.Length);
            if (context == null)
            {
                if (AStringUtils.Pos("+", d) != -1)
                    return false; //it has an offset, so also a register. without a context, this is impossible
                //check O for a hexadecimal value of 8 bytes and longer.
                if (Has4ByteHexString(d, ref s))
                {
                    address = (UIntPtr)AStringUtils.StrToQWordEx(s); //s already has the $ in front
                    result = AMemoryHelper.IsAddress(SymbolHandler.Process.Handle, address.ToIntPtr());
                }
            }
            else
            {
                //a slower but more effective address detector
                //strip of everything before the space, and if there's a [ ] get what's inbetween
                //then use the symbolhandler to find out what it is
                i = AStringUtils.Pos(" ", d);
                if (i != -1)  //it has a space , so a instruction is still present, strip it
                    d = AStringUtils.Copy(d, i + 1, d.Length);
                i = AStringUtils.Pos("[", d);
                if (i != -1)
                    d = AStringUtils.Copy(d, i + 1, AStringUtils.Pos("]", d) - i - 1);
                address = SymbolHandler.GetAddressFromName(d, false, out var hasError);
                result = !hasError;
            }
            return result;
        }
        #endregion
        #region PreviousOpCode
        public UIntPtr PreviousOpCode(UIntPtr address)
        {
            var aggressive = AggressiveAlignment;
            AggressiveAlignment = true;
            var x = PreviousOpCodeHelp(address, 80, out var result);
            if (x != address)
            {
                //no match found 80 bytes from the start
                //try 40
                x = PreviousOpCodeHelp(address, 40, out result);
                if (x != address)
                {
                    //nothing with 40, try 20
                    x = PreviousOpCodeHelp(address, 20, out result);
                    if (x != address)
                    {
                        //no 20, try 10
                        x = PreviousOpCodeHelp(address, 10, out result);
                        if (x != address)
                        {
                            //and if all else fails try to find the closest one
                            result = address - 1;
                            for (var i = 1; i <= 20; i++)
                            {
                                x = address - i;
                                var s = "";
                                Disassemble(ref x, ref s);
                                if (x == address)
                                {
                                    result = address - i;
                                    return result;
                                }
                            }
                        }

                    }
                }
            }
            AggressiveAlignment = aggressive;
            return result;
        }
        #endregion
        #region PreviousOpCodeHelp
        public UIntPtr PreviousOpCodeHelp(UIntPtr address, int distance, out UIntPtr result)
        {
            var y = UIntPtr.Zero;
            var s = "";
            var x = address - distance;
            while (x.ToUInt64() < address.ToUInt64())
            {
                y = x;
                Disassemble(ref x, ref s);
            }
            result = y;
            return x;
        }
        #endregion
        #region IntToHexSigned
        public String IntToHexSigned(UIntPtr value, int chars, Boolean signed = false, int signedSize = 0)
        {
            if (ShowSymbols || ShowModules)
                return IntToHexSignedWithSymbols(value, chars, signed, signedSize);
            return IntToHexSignedWithoutSymbols(value, chars, signed, signedSize);
        }
        #endregion
        #region IntToHexSignedWithoutSymbols
        public String IntToHexSignedWithoutSymbols(UIntPtr value, int chars, Boolean signed = false, int signedSize = 0)
        {
            String result;
            if (chars == 2)
            {
                signed = true;
                signedSize = 2;
            }
            if (signed)
            {
                switch (signedSize)
                {
                    case 2:
                    {
                        if ((SByte)value < 0)
                            result = ColorHex + '-' + AStringUtils.IntToHex(-(SByte)value, chars) + EndColor;
                        else
                            result = ColorHex + AStringUtils.IntToHex((SByte)value, chars) + EndColor;
                        break;
                    }
                    case 4:
                    {
                        if ((Int16)value < 0)
                            result = ColorHex + '-' + AStringUtils.IntToHex(-(Int16)value, chars) + EndColor;
                        else
                            result = ColorHex + AStringUtils.IntToHex((Int16)value, chars) + EndColor;
                        break;
                    }
                    case 8:
                    {
                        if ((Int64)value < 0)
                            result = ColorHex + '-' + AStringUtils.IntToHex(-(Int64)value, chars) + EndColor;
                        else
                            result = ColorHex + AStringUtils.IntToHex((Int64)value, chars) + EndColor;
                        break;
                    }
                    default:
                        result = ColorHex + AStringUtils.IntToHex(value, chars) + EndColor;
                        break;
                }
            }
            else
                result = ColorHex + AStringUtils.IntToHex(value, chars) + EndColor;
            return result;
        }
        #endregion
        #region IntToHexSignedWithSymbols
        public String IntToHexSignedWithSymbols(UIntPtr value, int chars, Boolean signed = false, int signedSize = 0)
        {
            String result;
            if ((ShowSymbols | ShowModules | ShowSections) & (chars >= 8))
            {
                result = SymbolHandler.GetNameFromAddress(value, ShowSymbols, ShowModules, ShowSections, null, out var found, chars);
                //when found, and the symbol contains a space or comma, put the symbolname in quotes
                if (found && (AStringUtils.Pos(" ", result) != -1 || AStringUtils.Pos(",", result) != -1))
                {
                    for (var i = result.Length - 1; i >= 0; i--)
                    {
                        if (AArrayUtils.InArray(result[i], '-', '+') || (i == 1))
                        {
                            if (i > 1)
                                result = '"' + AStringUtils.Copy(result, 1, i - 1) + '"' + AStringUtils.Copy(result, i, result.Length);
                            else
                                result = '"' + result + '"';
                            break;
                        }
                    }
                }
                if (SyntaxHighlighting)
                {
                    if (!found)
                        result = ColorHex + result + EndColor;
                    else
                        result = ColorSymbol + result + EndColor;
                }
            }
            else
                result = IntToHexSignedWithoutSymbols(value, chars, signed, signedSize);
            return result;
        }
        #endregion
        #region SplitDisassembledString
        public void SplitDisassembledString(String disassembled, Boolean showValues, out String address, out String bytes, out String opcode, out String special, Object context = null)
        {
            var tempBufBack = UBinaryUtils.NewZeroByteArray(127);
            var tempBuf = new UBytePtr(tempBufBack);
            var tempAddress = UIntPtr.Zero;
            var readBuf = new UBytePtr(UBinaryUtils.NewZeroByteArray(64));
            var i = AStringUtils.Pos(" - ", disassembled);
            address = AStringUtils.Copy(disassembled, 1, i - 1).ToUpper();
            i += 3;
            var j = AStringUtils.PosEx(" - ", disassembled, i);
            if (j == -1)
                j = disassembled.Length + 1;
            bytes = AStringUtils.Copy(disassembled, i, j - i);
            j += 3;
            var k = AStringUtils.PosEx(" : ", disassembled, j);
            var l = k;
            if (k == -1)
                k = disassembled.Length + 1;
            opcode = AStringUtils.Copy(disassembled, j, k - j);
            if (showValues)
            {
                var ts = "";
                special = "";
                if (HasAddress(opcode, ref tempAddress, context) | (opcode.Length > 3 && opcode.StartsWith("lea")))
                {
                    AVariableType variableType;
                    if (AMemoryHelper.IsAddress(SymbolHandler.Process.Handle, tempAddress.ToIntPtr()))
                    {
                        int j2;
                        try
                        {
                            if (opcode.StartsWith("lea")) //lea
                            {
                                j = AStringUtils.Pos("[", opcode);
                                j2 = AStringUtils.Pos("]", opcode);
                                var ts2 = AStringUtils.Copy(opcode, j + 1, j2 - j - 1);
                                tempAddress = SymbolHandler.GetAddressFromName(ts2, false, out var err);
                                if (err)
                                    return; //error
                            }
                        }
                        catch
                        {
                            tempAddress = UIntPtr.Zero;
                        }
                        var isJumper = opcode.StartsWith("j") ||
                                       opcode.StartsWith("loo") ||
                                       opcode.StartsWith("ca");
                        var valueType = OpCodeToValueType(opcode);
                        i = AStringUtils.Pos("[", disassembled);
                        if (i != -1)
                        {
                            //it might have an override
                            if (AStringUtils.Pos("qword ptr", opcode) != -1)
                                valueType = 4;
                            else if (AStringUtils.Pos("dword ptr", opcode) != -1) //usually a double
                                valueType = 2;
                            else if (AStringUtils.Pos("word ptr", opcode) != -1)
                                valueType = 1;
                            else if (AStringUtils.Pos("byte ptr", opcode) != -1)
                                valueType = 0;
                            else
                            {
                                //check the register used
                                j2 = AStringUtils.Pos(",[", opcode);
                                k = AStringUtils.Pos("],", opcode);
                                String ts3;
                                if (j2 != -1)  //register in front
                                {
                                    l = AStringUtils.Pos(" ", opcode);
                                    ts3 = AStringUtils.Copy(opcode, l + 1, j2 - l - 1);
                                    switch (AAsmTools.Assembler.TokenToRegisterBit(ts3.ToUpper()))
                                    {
                                        case ATokenType.Register8Bit:
                                            valueType = 0;
                                            break;
                                        case ATokenType.Register16Bit:
                                            valueType = 1;
                                            break;
                                        case ATokenType.Register32Bit:
                                            valueType = 2;
                                            break;
                                        default: valueType = 2;
                                            break;
                                    }
                                }
                                else
                                if (k != -1)   //register after ],
                                {
                                    l = AStringUtils.Pos("],", opcode);
                                    ts3 = AStringUtils.Copy(opcode, l + 2, opcode.Length - l - 1);
                                    switch (AAsmTools.Assembler.TokenToRegisterBit(ts3.ToUpper()))
                                    {
                                        case ATokenType.Register8Bit:
                                            valueType = 0;
                                            break;
                                        case ATokenType.Register16Bit:
                                            valueType = 1;
                                            break;
                                        case ATokenType.Register32Bit:
                                            valueType = 2;
                                            break;
                                        default: valueType = 2;
                                            break;
                                    }
                                } //else no idea, check var
                            }
                        } //not an address specifier
                        if (valueType == 2)
                        {
                            if (Kernel32.ReadProcessMemory(Proc.Handle, tempAddress.ToIntPtr(), tempBuf.ToIntPtr(), 16, out _))
                            {
                                variableType = AAsmTools.ByteInterpreter.FindTypeOfData(tempAddress, tempBuf, 16);
                                switch (variableType)
                                {
                                    case AVariableType.Single:
                                        valueType = 3;
                                        break;
                                    case AVariableType.Double:
                                        valueType = 4;
                                        break;
                                    case AVariableType.String:
                                        valueType = 5;
                                        break;
                                    case AVariableType.UnicodeString:
                                        valueType = 6;
                                        break;
                                }
                            }
                        }
                        if (isJumper)
                            valueType = 2; //handle it as a dword
                        switch (valueType)
                        {
                            case 0: // byte
                            {
                                if (Kernel32.ReadProcessMemory(Proc.Handle, tempAddress.ToIntPtr(), readBuf.ToIntPtr(), 1, out _))
                                {
                                    var value = (UIntPtr)readBuf.ReadByte();
                                    ts = AStringUtils.IntToHex(value, 2);
                                }
                                break;
                            }
                            case 1: // word
                            {
                                if (Kernel32.ReadProcessMemory(Proc.Handle, tempAddress.ToIntPtr(), readBuf.ToIntPtr(), 2, out _))
                                {
                                    var value = (UIntPtr)readBuf.ReadUInt16();
                                    ts = AStringUtils.IntToHex(value, 4);
                                }
                                break;
                            }
                            case 2: // dword
                            {
                                if (Kernel32.ReadProcessMemory(Proc.Handle, tempAddress.ToIntPtr(), readBuf.ToIntPtr(), 4, out _))
                                {
                                    var value = (UIntPtr)readBuf.ReadUInt32();
                                    if (isJumper && (((int)value.ToUInt32() & 0xffff) == 0x25ff))  //it's a jmp [xxxxxxxx]    / call [xxxxxx] ...
                                    {
                                        if (Kernel32.ReadProcessMemory(Proc.Handle, (IntPtr)(tempAddress.ToUInt64() + 2), readBuf.ToIntPtr(), 4, out _))
                                        {
                                            value = (UIntPtr)readBuf.ReadUInt32();
                                            if (Proc.IsX64)
                                                value = (UIntPtr)(tempAddress.ToUInt64() + 6 + value.ToUInt64());
                                            if (Kernel32.ReadProcessMemory(Proc.Handle, value.ToIntPtr(), readBuf.ToIntPtr(), Proc.PointerSize, out _))
                                            {
                                                value = readBuf.ReadUIntPtr();
                                                ts = "->" + SymbolHandler.GetNameFromAddress(value, SymbolHandler.ShowSymbols, SymbolHandler.ShowModules, SymbolHandler.ShowSections, null, out _, 8, false);
                                            }
                                        }
                                    }
                                    else
                                        ts = SymbolHandler.GetNameFromAddress(value, SymbolHandler.ShowSymbols, SymbolHandler.ShowModules, SymbolHandler.ShowSections, null, out _, 8, false);
                                    if (isJumper)
                                    {
                                        //check if ts is a name or a hexadecimal value
                                        //if hex, don't use it
                                        AStringUtils.Val("0x" + ts, out j, out i);
                                        if (i == 0)
                                            ts = ""; //zero the string, it's a hexadecimal string
                                    }
                                }
                                break;
                            }
                            case 3: // Single
                            {
                                if (Kernel32.ReadProcessMemory(Proc.Handle, tempAddress.ToIntPtr(), readBuf.ToIntPtr(), 4, out _))
                                {
                                    var value = readBuf.ReadFloat();
                                    ts = UStringUtils.Sprintf("(float)%.4f", value);
                                }
                                break;
                            }
                            case 4: // Double
                            {
                                if (Kernel32.ReadProcessMemory(Proc.Handle, tempAddress.ToIntPtr(), readBuf.ToIntPtr(), 8, out _))
                                {
                                    var value = readBuf.ReadDouble();
                                    ts = UStringUtils.Sprintf("(double)%.4f", value);
                                }
                                break;
                            }
                            case 5: // String
                                {
                                    Kernel32.ReadProcessMemory(Proc.Handle, tempAddress.ToIntPtr(), tempBuf.ToIntPtr(), 128, out var actualRead);
                                    tempBuf[127] = 0;
                                    tempBuf[126] = (Byte)'.';
                                    tempBuf[125] = (Byte)'.';
                                    tempBuf[124] = (Byte)'.';
                                    if (actualRead > 0)
                                        tempBuf[actualRead - 1] = 0;
                                    ts = '"' + UBitConverter.UnpackSingle("z1", 0, tempBufBack).ToString() + '"';
                                }
                                break;
                            case 6: // UnicodeString
                                {
                                    Kernel32.ReadProcessMemory(Proc.Handle, tempAddress.ToIntPtr(), tempBuf.ToIntPtr(), 128, out var actualRead);
                                    tempBuf[127] = 0;
                                    tempBuf[126] = 0;
                                    tempBuf[125] = 0;
                                    tempBuf[124] = (Byte)'.';
                                    tempBuf[123] = 0;
                                    tempBuf[122] = (Byte)'.';
                                    tempBuf[121] = 0;
                                    tempBuf[120] = (Byte)'.';
                                    if (actualRead > 1)
                                    {
                                        tempBuf[actualRead - 1] = 0;
                                        tempBuf[actualRead - 2] = 0;
                                    }
                                    ts = "\"\"" + UBitConverter.UnpackSingle("z7", 0, tempBufBack) + "\"\"";
                                }
                                break;
                        }
                        if (ts != "")
                            ts = '[' + ts + ']';
                    }
                    else
                    {
                        //tempAddress doesn't seem to be an address
                        tempBuf.Zero();
                        variableType = AAsmTools.ByteInterpreter.FindTypeOfData(UIntPtr.Zero, tempBuf, Proc.PointerSize);
                        if (variableType == AVariableType.Single)
                            ts = UStringUtils.Sprintf("(float)%.4f", tempAddress.ToIntPtr().ReadFloat());
                    }
                }
                special = ts;
            }
            else
                special = "";
        }
        #endregion
        #region DecodeLastParametersToString
        public String DecodeLastParametersToString()
        {
            var values = new AArrayManager<ADecodeValue>();
            values.Inc();
            values.Inc();
            var bufferBack = UBinaryUtils.NewZeroByteArray(63);
            var buffer = new UBytePtr(bufferBack);
            int x;
            if (LastDisassembleData.CommentsOverride != "")
                return LastDisassembleData.CommentsOverride;
            var result = "";
            if (LastDisassembleData.IsJump)
            {
                Byte[] readBuf;
                UIntPtr jumpAddress;
                if (LastDisassembleData.ModRmValueType == ADisassemblerValueType.Address)
                {
                    jumpAddress = LastDisassembleData.ModRmValue;
                    readBuf = Proc.Memory.Read<Byte>(jumpAddress.ToIntPtr(), Proc.PointerSize);
                    if (readBuf.Length != Proc.PointerSize)
                        return result;
                    jumpAddress = UBitConverter.ToUIntPtr(readBuf);
                }
                else
                {
                    if (LastDisassembleData.ParameterValueType == ADisassemblerValueType.None)
                        return result; //jump with no address (e.g reg)
                    jumpAddress = LastDisassembleData.ParameterValue;
                }
                //check if the bytes at jumpAddress is ff 25 (jmp [xxxxxxxx])
                if (Kernel32.ReadProcessMemory(Proc.Handle, jumpAddress.ToIntPtr(), buffer.ToIntPtr(0), 6, out x))
                {
        
                    if ((buffer[0] == 0xff) && buffer[1] == 0x25)
                    {
                        result += "->";  //double, so ->->
                        if (Proc.IsX64)
                            jumpAddress = jumpAddress + 6 + buffer.ReadInt32(2); //jumpaddress+6 because of relative addressing
                        else
                            jumpAddress = (UIntPtr)buffer.ReadUInt32(2);
                        //jumpaddress now contains the address of the address to jump to
                        //so, get the address it actually jumps to
                        readBuf = Proc.Memory.Read<Byte>(jumpAddress.ToIntPtr(), Proc.PointerSize);
                        if (readBuf.Length != Proc.PointerSize)
                            return result;
                        jumpAddress = UBitConverter.ToUIntPtr(readBuf);
                    }
                    var s = SymbolHandler.GetNameFromAddress(jumpAddress, ShowSymbols, ShowModules, ShowSections, null, out _, 8, false);
                    if (AStringUtils.Pos(s, LastDisassembleData.Parameters) == 0)  //no need to show a comment if it's exactly the same
                        result = result + "->" + s;
                }
            }
            else
            {
                if (LastDisassembleData.ModRmValueType == ADisassemblerValueType.Address || LastDisassembleData.ParameterValueType != ADisassemblerValueType.None)
                {
                    var parameterCount = 0;
                    if (LastDisassembleData.ParameterValueType != ADisassemblerValueType.None)
                        parameterCount += 1;
                    if (LastDisassembleData.ModRmValueType == ADisassemblerValueType.Address)
                        parameterCount += 1;
                    if (LastDisassembleData.ModRmValueType == ADisassemblerValueType.Address)
                    {
                        if ((parameterCount > 1) && ModRmPosition == ATmrPos.Right)
                            values[1].Value = LastDisassembleData.ModRmValue;
                        else
                            values[0].Value = LastDisassembleData.ModRmValue;
                    }
                    if (LastDisassembleData.ParameterValueType != ADisassemblerValueType.None)
                    {
                        if (parameterCount > 1 && ModRmPosition != ATmrPos.Right)
                            values[1].Value = LastDisassembleData.ParameterValue;
                        else
                            values[0].Value = LastDisassembleData.ParameterValue;
                    }
                    int i;
                    for (i = 0; i <= parameterCount - 1; i++)
                    {
                        values[i].S = "";
                        if (AMemoryHelper.IsAddress(Proc.Handle, values[i].Value.ToIntPtr()))
                        {
                            values[i].IsAddress = true;
                            values[i].Type = AVariableType.DWord;
                            Kernel32.ReadProcessMemory(Proc.Handle, values[i].Value.ToIntPtr(), buffer.ToIntPtr(), buffer.Capacity, out x);
                            if (x > 0)
                            {
                                if (LastDisassembleData.IsFloat)
                                {
                                    switch (LastDisassembleData.DataSize)
                                    {
                                        case 4:
                                            values[i].Type = AVariableType.Single;
                                            break;
                                        case 8:
                                            values[i].Type = AVariableType.Double;
                                            break;
                                        case 10:
                                            values[i].Type = AVariableType.QWord;
                                            break; 
                                    }
                                }
                                else
                                    values[i].Type = AAsmTools.ByteInterpreter.FindTypeOfData(values[i].Value, buffer, x);
                            }
                            else
                            {
                                values[i].S = "";
                                continue;
                            }
                        }
                        else
                        {
                            x = Proc.PointerSize;
                            buffer.WriteUIntPtr(values[i].Value); //assign it so I don't have to make two compare routines
                            values[i].Type = AAsmTools.ByteInterpreter.FindTypeOfData(UIntPtr.Zero, buffer, x);
                            values[i].IsAddress = false;
                        }
                        switch (values[i].Type)
                        {
                            case AVariableType.Byte:
                                values[i].S = buffer[0].ToString();
                                break;
                            case AVariableType.Word:
                                values[i].S = buffer.ReadInt16().ToString();
                                break;
                            case AVariableType.DWord:
                                values[i].S = buffer.ReadInt32().ToString();
                                break;
                            case AVariableType.QWord:
                                values[i].S = buffer.ReadInt64().ToString();
                                break;
                            case AVariableType.Single:
                                values[i].S = UStringUtils.Sprintf("%.2f", buffer.ReadFloat());
                                break;
                            case AVariableType.Double:
                                values[i].S = UStringUtils.Sprintf("%.2f", buffer.ReadDouble()); 
                                break;
                            case AVariableType.String:
                                {
                                    buffer[x] = 0;
                                    values[i].S = '"' + UBitConverter.UnpackSingle("z1", 0, bufferBack).ToString() + '"';
                                }
                                break;
                            case AVariableType.UnicodeString:
                                {
                                    buffer[x] = 0;
                                    if (x > 0)
                                        buffer[x - 1] = 0;
                                    values[i].S = '"' + UBitConverter.UnpackSingle("z7", 0, bufferBack).ToString() + '"';
                                }
                                break;
                            case AVariableType.Pointer:
                                {
                                    if (SymbolHandler.Process.IsX64)
                                        values[i].S = AStringUtils.IntToHex(buffer.ReadUInt64(), 8);
                                    else
                                        values[i].S = AStringUtils.IntToHex(buffer.ReadUInt32(), 8);
                                }
                                break;
                        }
                        // result:=VariableTypeToString(vtype);
                        if (values[i].IsAddress & (values[i].S != ""))
                            values[i].S = '(' + values[i].S + ')';
                        if (i == 0)
                            result += values[i].S;
                        else
                            result = result + ',' + values[i].S;
                    }
                }
            }
            return result;
        }
        #endregion
        #region SetSyntaxHighlighting
        public void SetSyntaxHighlighting(Boolean state)
        {
            SyntaxHighlighting = state;
            if (state)
            {
                EndColor = "{N}";
                ColorHex = "{H}";
                ColorReg = "{R}";
                ColorSymbol = "{S}";
            }
            else
            {
                //no color codes
                EndColor = "";
                ColorHex = "";
                ColorReg = "";
                ColorSymbol = "";
            }
        }
        #endregion
        #region ReadMemory
        public int ReadMemory(UIntPtr address, UIntPtr destination, int size)
        {
            // todo handle cloak support
            //readprocessmemorywithcloaksupport(processhandle, (pointer)(address), destination, size, actualread);
            Kernel32.ReadProcessMemory(SymbolHandler.Process.Handle, address.ToIntPtr(), destination.ToIntPtr(), size, out var actualread);
            if (actualread == 0 && ((address.ToUInt64() + ((UInt64) size & 0xfffffffffffff000UL)) > (address.ToUInt64() & 0xfffffffffffff000UL))) //did not read a single byte and overlaps a pageboundary
            {
                var p1 = 0;
                do
                {
                    var i = Math.Min(size, (int) (4096 - (address.ToUInt64() & 0xfff)));
                    // todo handle cloak support
                    //readprocessmemorywithcloaksupport(processhandle, (pointer)(address), destination, i, actualread);
                    Kernel32.ReadProcessMemory(SymbolHandler.Process.Handle, address.ToIntPtr(), destination.ToIntPtr(), i, out actualread);
                    p1 += actualread;
                    address += actualread;
                    size -= actualread;
                    destination += actualread;
                } while (!(actualread == 0 || size == 0));
                return p1;
            }
            return actualread;
        }
        #endregion
        #region Disassemble
        public String Disassemble(ref UIntPtr offset)
        {
            var ignore = "";
            var result = Disassemble(ref offset, ref ignore);
            return result;
        }
        public String Disassemble(ref UIntPtr offset, ref String description)
        {
            String result;
            try
            {
                var i = 0;
                var j = 0;
                var k = 0;
                LastDisassembleData.IsFloat = false;
                LastDisassembleData.IsFloat64 = false;
                LastDisassembleData.IsCloaked = false;
                LastDisassembleData.CommentsOverride = "";
                // todo add the binutils
                //if (defaultbinutils != nil)
                //{
                //    //use this
                //    LastDisassembleData.address = offset;
                //    LastDisassembleData.seperatorcount = 0;
                //    defaultbinutils.disassemble(LastDisassembleData);
                //    result = AStringUtils.IntToHex(LastDisassembleData.address, 8);
                //    result = result + " - ";
                //    for (i = 0; i <= length(LastDisassembleData.bytes) - 1; i++)
                //        result = result + AStringUtils.IntToHex(LastDisassembleData.bytes[i], 2) + ' ';
                //    result = result + " - ";
                //    result = result + LastDisassembleData.opcode;
                //    result = result + ' ';
                //    result = result + LastDisassembleData.parameters;
                //    if (length(LastDisassembleData.bytes) > 0)
                //        offset += length(LastDisassembleData.bytes);
                //    else
                //    {
                //        if (processhandler.systemarchitecture == archarm)
                //        {
                //            if ((offset | 1) == 1)
                //                offset += 2;
                //            else
                //                offset += 4;
                //        }
                //        else
                //            offset += 1;
                //    }
                //    return result;
                //}
                if (Is64BitOverride)
                    Is64Bit = Is64BitOverrideState;
                else
                {
                    Is64Bit = SymbolHandler.Process.IsX64;
                    if (Environment.Is64BitOperatingSystem)
                    {
                        if (offset.ToUInt64() >= 0x100000000UL)
                            Is64Bit = true;
                    }
                    // todo make this work!
                    //if (SymbolHandler.GetModuleByAddress(offset, out mi))
                    //    is64bit = mi.is64bitmodule;
                }
                // todo handle arm
                //if (processhandler.systemarchitecture == archarm)
                //{
                //    result = armdisassembler.disassemble(offset);
                //    LastDisassembleData = armdisassembler.LastDisassembleData;
                //    return result;
                //}
                ModRmPosition = ATmrPos.None;
                var last = 0U;
                var tempResult = "";
                LastDisassembleData.Bytes.SetLength(0);
                LastDisassembleData.Address = offset;
                LastDisassembleData.SeparatorCount = 0;
                LastDisassembleData.Prefix = "";
                LastDisassembleData.PrefixSize = 0;
                LastDisassembleData.OpCode = "";
                LastDisassembleData.Parameters = "";
                LastDisassembleData.IsJump = false;
                LastDisassembleData.IsCall = false;
                LastDisassembleData.IsRet = false;
                LastDisassembleData.IsConditionalJump = false;
                LastDisassembleData.ModRmValueType = ADisassemblerValueType.None;
                LastDisassembleData.ParameterValueType = ADisassemblerValueType.None;
                LastDisassembleData.HasSib = false;
                LastDisassembleData.DataSize = 0;
                LastDisassembleData.RipRelative = 0;
                // todo uncomment user override
                //if (assigned(ondisassembleoverride))  //check if the user has defined it's own disassembler
                //{
                //    //if so, call the OnDisassemble propery, and if it returns true don't handle the original
                //    if (ondisassembleoverride(self, offset, LastDisassembleData, result, description))
                //    {
                //        if (length(LastDisassembleData.bytes) == 0)  //BAD!
                //            setlength(LastDisassembleData.bytes, 1);
                //
                //        offset += length(LastDisassembleData.bytes);
                //        return result;
                //    }
                //}
                // //also check global overrides
                // for (i = 0; i <= length(globaldisassembleoverrides) - 1; i++)
                // {
                //     if (assigned(globaldisassembleoverrides[i]))
                //     {
                //         if (globaldisassembleoverrides[i](self, offset, LastDisassembleData, result, description))
                //         {
                //             if (length(LastDisassembleData.bytes) == 0)  //BAD!
                //                 setlength(LastDisassembleData.bytes, 1);
                // 
                //             offset += length(LastDisassembleData.bytes);
                //             return result;
                //         }
                //     }
                // }
                RipRelative = false;
                if (IsDataOnly)
                    result = "";
                else
                    result = AStringUtils.IntToHex(offset, 8) + " - ";
                var isPrefix = true;
                Prefix = new APrefix(0xf0, 0xf2, 0xf3, 0x2e, 0x36, 0x3e, 0x26, 0x64, 0x65, 0x66, 0x67);
                Prefix2 = new APrefix();
                var startOffset = offset;
                var initialOffset = offset;
                for (i = 32; i <= 63; i++) //debug code
                    Memory[i] = 0xce;
                var actualRead = ReadMemory(offset, Memory.ToIntPtr().ToUIntPtr(), 32);
                var memory = Memory.Shift(0);
                if (actualRead > 0)
                {
                    //{$ifndef jni}
                    //if debuggerthread<>nil then
                    //  for i:=0 to actualRead-1 do
                    //    if memory[i]=$cc then
                    //    begin
                    //      //memory[i]:=debuggerthread.getrealbyte(offset+i);
                    //
                    //      repairbreakbyte(offset+i, memory[i]);
                    //    end;
                    //{$endif}
                    while (isPrefix)
                    {
                        offset += 1; //offset will always inc by 1
                        if (Prefix.Contains(memory[0]))
                        {
                            if (LastDisassembleData.Bytes.Length > 10)
                            {
                                //prevent a too long prefix from crashing the disassembler (e.g 12GB filled with one prefix....)
                                isPrefix = false;
                                break;
                            }
                            LastDisassembleData.Bytes.Inc();
                            LastDisassembleData.Bytes.Last = memory[0];
                            if (!IsDataOnly)
                                result = result + IntToHexSigned((UIntPtr)memory[0], 2) + ' ';
                            isPrefix = true;
                            startOffset += 1;
                            Prefix2.Add(memory[0]);
                            memory = memory.Shift(1);
                            if (offset.ToUInt64() > initialOffset.ToUInt64() + 24)  //too long
                            {
                                description = "";
                                LastDisassembleData.OpCode = "??";
                                offset = initialOffset + 1;
                                return result;
                            }

                        }
                        else
                            isPrefix = false;
                    }
                    var noVexPossible = false;
                    if (Prefix2.Contains(0xf0))
                    {
                        tempResult = "lock ";
                        noVexPossible = true;
                    }
                    if (Prefix2.Contains(0xf2))
                    {
                        tempResult += "repne ";
                        noVexPossible = true;
                    }
                    if (Prefix2.Contains(0xf3))
                    {
                        tempResult += "repe ";
                        noVexPossible = true;
                    }
                    LastDisassembleData.Prefix = tempResult;
                    OpCodeFlags.Clear();
                    RexPrefix = 0;
                    if (Is64Bit)
                    {
                        if (AMathUtils.InRange(memory[0], 0x40, 0x4f))  //does it start with a rex prefix ?
                        {
                            LastDisassembleData.Bytes.Inc();
                            LastDisassembleData.Bytes.Last = memory[0];
                            RexPrefix = memory[0];
                            OpCodeFlags.B = (RexPrefix & BIT_REX_B) == BIT_REX_B;
                            OpCodeFlags.X = (RexPrefix & BIT_REX_X) == BIT_REX_X;
                            OpCodeFlags.R = (RexPrefix & BIT_REX_R) == BIT_REX_R;
                            OpCodeFlags.W = (RexPrefix & BIT_REX_W) == BIT_REX_W;
                            if (!IsDataOnly)
                                result = result + IntToHexSigned((UIntPtr)RexPrefix, 2) + ' ';
                            offset += 1;
                            startOffset += 1;
                            Prefix2.Add(RexPrefix);
                            memory = memory.Shift(1);
                            noVexPossible = true;
                            if (offset.ToUInt64() > initialOffset.ToUInt64() + 24)
                            {
                                description = "";
                                LastDisassembleData.OpCode = "??";
                                offset = initialOffset + 1;
                                return result;
                            }
                        }
                    }
                    var prefixSize = LastDisassembleData.Bytes.Length;
                    LastDisassembleData.PrefixSize = prefixSize;
                    if (noVexPossible == false && AMathUtils.InRange(memory[0], 0xc4, 0xc5))
                    {
                        HasVex = true;
                        int bytesToMove;
                        if (memory[0] == 0xc5)
                        {
                            //2 byte VEX
                            prefixSize += 2;
                            var vex2 = new AVex2Byte(memory.ToIntPtr(1));
                            OpCodeFlags.Pp = vex2.Pp;
                            OpCodeFlags.L = vex2.L == 1;
                            OpCodeFlags.Vvvv = vex2.Vvvv;
                            OpCodeFlags.R = vex2.R == 0;
                            OpCodeFlags.Mmmmm = 1;
                            i = LastDisassembleData.Bytes.Length;
                            LastDisassembleData.Bytes.SetLength(i + 2);
                            LastDisassembleData.Bytes[i] = memory[0];
                            LastDisassembleData.Bytes[i + 1] = memory[1];
                            memory[1] = 0xf;
                            bytesToMove = 1;
                            memory = memory.Shift(1);
                            offset += 1;
                        }
                        else
                        {
                            //3 byte vex
                            prefixSize += 3;
                            var vex3 = new AVex3Byte(memory.ToIntPtr(1));
                            OpCodeFlags.Pp = vex3.Pp;
                            OpCodeFlags.L = vex3.L == 1;
                            OpCodeFlags.Vvvv = vex3.Vvvv;
                            OpCodeFlags.W = vex3.W == 1; //this one is NOT inverted
                            OpCodeFlags.Mmmmm = vex3.Mmmmm;
                            OpCodeFlags.B = vex3.B == 0;
                            OpCodeFlags.X = vex3.X == 0;
                            OpCodeFlags.R = vex3.R == 0;
                            i = LastDisassembleData.Bytes.Length;
                            LastDisassembleData.Bytes.SetLength(i + 3);
                            LastDisassembleData.Bytes[i] = memory[0];
                            LastDisassembleData.Bytes[i + 1] = memory[1];
                            LastDisassembleData.Bytes[i + 2] = memory[2];
                            /* mmmmm:
                            00000: Reserved for future use (will #UD)
                            00001: implied 0F leading opcode byte
                            00010: implied 0F 38 leading opcode bytes
                            00011: implied 0F 3A leading opcode bytes
                            00100-11111: Reserved for future use (will #UD)
                            */
                            bytesToMove = 3; //number of bytes to shift
                            switch (OpCodeFlags.Mmmmm)
                            {
                                case 1:
                                    {
                                        bytesToMove = 2;
                                        memory[2] = 0xf;
                                    }
                                    break;
                                case 2:
                                    {
                                        bytesToMove = 1;
                                        memory[1] = 0xf;
                                        memory[2] = 0x38;
                                    }
                                    break;
                                case 3:
                                    {
                                        bytesToMove = 1;
                                        memory[1] = 0xf;
                                        memory[2] = 0x3a;
                                    }
                                    break; //else invalid
                            }
                            memory = memory.Shift(bytesToMove);
                            offset += bytesToMove;
                        }
                        switch (OpCodeFlags.Pp)
                        {
                            case 1:
                                Prefix2.Add(0x66);
                                break;
                            case 2:
                                Prefix2.Add(0xf3);
                                break;
                            case 3:
                                Prefix2.Add(0xf2);
                                break;
                        }
                    }
                    else
                        HasVex = false;
                    //compatibility fix for code that still checks for rex.* or sets it as a temporary flag replacement
                    RexPrefix = (Byte)(OpCodeFlags.B ? RexPrefix | BIT_REX_B : RexPrefix);
                    RexPrefix = (Byte)(OpCodeFlags.X ? RexPrefix | BIT_REX_X : RexPrefix);
                    RexPrefix = (Byte)(OpCodeFlags.R ? RexPrefix | BIT_REX_R : RexPrefix);
                    RexPrefix = (Byte)(OpCodeFlags.W ? RexPrefix | BIT_REX_W : RexPrefix);
                    if (
                        !ADisassemblerCases1.Process(this, memory, ref offset, ref prefixSize, ref last, ref description) &&
                        !ADisassemblerCases2.Process(this, memory, ref offset, ref prefixSize, ref last, ref description) &&
                        !ADisassemblerCases3.Process(this, memory, ref offset, ref prefixSize, ref last, ref description) &&
                        !ADisassemblerCases4.Process(this, memory, ref offset, ref prefixSize, ref last, ref description) &&
                        !ADisassemblerCases5.Process(this, memory, ref offset, ref prefixSize, ref last, ref description)
                        )
                    {
                        LastDisassembleData.OpCode = "db";
                        LastDisassembleData.Parameters = AStringUtils.IntToHex(memory[0], 2);
                    }
                    if (LastDisassembleData.Parameters != "" && LastDisassembleData.Parameters[LastDisassembleData.Parameters.Length - 1] == ',')
                        LastDisassembleData.Parameters = UStringUtils.SubStr(LastDisassembleData.Parameters, 0, -1); // todo check if this actually shrinks
                    LastDisassembleData.Description = description;
                    //copy the remaining bytes
                    k = LastDisassembleData.Bytes.Length;
                    if ((offset.ToIntPtr().ToInt64() - initialOffset.ToIntPtr().ToInt64()) < k)
                        offset = initialOffset + k;
                    LastDisassembleData.Bytes.SetLength((int)(offset.ToUInt64() - initialOffset.ToUInt64()));
                    if ((k >= 32) || (k < 0))
                        MessageBox.Show(AStringUtils.IntToHex(startOffset, 8) + "disassembler error 1", "debug here");
                    var td = (UInt32)(offset.ToUInt64() - initialOffset.ToUInt64() - (UInt64)k);
                    i = (int)(k + td);
                    if ((td >= 32) || (i >= 32) || (i < 0))
                        MessageBox.Show(AStringUtils.IntToHex(startOffset, 8) + "disassembler error 2", "debug here");
                    if (td > 0)
                    {
                        var breakNow = false;
                        try
                        {
                            using (var p1 = new UBytePtr(LastDisassembleData.Bytes.Buffer))
                                AArrayUtils.CopyMemory(p1, k, Memory.ToIntPtr(), k, (int)td);
                        }
                        catch
                        {
                            breakNow = true;
                        }
                        if (breakNow)
                            MessageBox.Show(AStringUtils.IntToHex(startOffset, 8) + "disassembler error 3", "debug here");
                    }
                    //adjust for the prefix.
                    if (k != 0)
                    {
                        for (i = 0; i <= LastDisassembleData.SeparatorCount - 1; i++)
                            LastDisassembleData.Separators[i] += prefixSize;

                        if (LastDisassembleData.RipRelative != 0)
                            LastDisassembleData.RipRelative += prefixSize;
                    }
                    //  result:=result+'- '+tempResult;
                    if (RipRelative)
                    {
                        //add the current offset to the code between []
                        LastDisassembleData.ModRmValue = (UIntPtr)(offset.ToUInt64() + ((UIntPtr)((int)LastDisassembleData.ModRmValue)).ToUInt64()); //sign extended increase
                        i = AStringUtils.Pos("[", LastDisassembleData.Parameters);
                        j = AStringUtils.PosEx("]", LastDisassembleData.Parameters, i);
                        var tempAddress = LastDisassembleData.ModRmValue;
                        tempResult = AStringUtils.Copy(LastDisassembleData.Parameters, 1, i);
                        tempResult += IntToHexSigned(tempAddress, 8);
                        LastDisassembleData.Parameters = tempResult + AStringUtils.Copy(LastDisassembleData.Parameters, j, LastDisassembleData.Parameters.Length);
                    }
                }
                else
                {
                    LastDisassembleData.OpCode = "??";
                    offset += 1;
                }
                // todo handle cloak
                //# ifdef windows
                //string result;
                //LastDisassembleData.iscloaked = hascloakedregioninrange(LastDisassembleData.address, length(LastDisassembleData.bytes), va, pa);
                //#else
                LastDisassembleData.IsCloaked = false;
                //#endif
                if (!IsDataOnly)
                {
                    result = AStringUtils.IntToHex(LastDisassembleData.Address, 8) + " - " + GetLastByteString();
                    result += " - ";
                    result = result + LastDisassembleData.Prefix + LastDisassembleData.OpCode;
                    result += ' ';
                    result += LastDisassembleData.Parameters;
                }
                // todo handle custom override
                //if (assigned(onpostdisassemble))
                //{
                //    tempResult = result;
                //    tempDescription = description;
                //
                //    if (onpostdisassemble(self, initialOffset, LastDisassembleData, tempResult, tempDescription))
                //    {
                //        result = tempResult;
                //        description = tempDescription;
                //
                //        if (length(LastDisassembleData.bytes) > 0)
                //            offset = initialOffset + length(LastDisassembleData.bytes);
                //    }
                //}
            }
            catch (Exception e)
            {
                // todo make this work
                //outputdebugstring(AStringUtils.IntToHex(startOffset,8)+':disassembler exception:'+e.message);
                //MessageBox(0,pchar('disassembler exception at '+ AStringUtils.IntToHex(startOffset,8)+#13#10+e.message+#13#10+#13#10+'Please provide dark byte the bytes that are at this address so he can fix it'#13#10'(Open another CE instance and in the hexadecimal view go to this address)'),'debug here',MB_OK);
                throw new Exception("disassembler exception: " + e.Message);
            }
            return result ?? "";
        }
        #endregion
    }
}
