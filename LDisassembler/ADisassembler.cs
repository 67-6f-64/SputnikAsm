using System;
using System.Windows.Forms;
using Process.NET.Marshaling;
using Sputnik.LBinary;
using Sputnik.LString;
using Sputnik.LUtils;
using SputnikAsm.LBinary;
using SputnikAsm.LDisassembler.LEnums;
using SputnikAsm.LExtensions;
using SputnikAsm.LProcess;
using SputnikAsm.LProcess.LMemory;
using SputnikAsm.LProcess.LNative;
using SputnikAsm.LProcess.LNative.LTypes;
using SputnikAsm.LProcess.Utilities;
using SputnikAsm.LSymbolHandler;
using SputnikAsm.LUtils;

namespace SputnikAsm.LDisassembler
{
    public partial class ADisassembler
    {
        #region Constants
        const int BIT_REX_W = 8;
        const int BIT_REX_R = 4;
        const int BIT_REX_X = 2;
        const int BIT_REX_B = 1;
        #endregion
        #region Properties
        public Boolean RexB => _opCodeFlags.B;
        public Boolean RexX => _opCodeFlags.X;
        public Boolean RexR => _opCodeFlags.R;
        public Boolean RexW => _opCodeFlags.W;
        public AProcessSharp Proc => SymbolHandler.Process;
        #endregion
        #region Variables
        private UBytePtr _memory;
        private AOpCodeFlags _opCodeFlags;
        private APrefix _prefix;
        private APrefix _prefix2;
        private Boolean _hasVex;
        private Boolean _ripRelative;
        private String _colorHex;
        private String _colorReg;
        private String _colorSymbol;
        private String _endColor;
        private Boolean _aggressiveAlignment;
        private ATmrPos _modRmPosition;
        private Byte _rexPrefix;
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
        public ADisassembler()
        {
            SymbolHandler = new ASymbolHandler();
            SymbolHandler.Process = new AProcessSharp(System.Diagnostics.Process.GetCurrentProcess().Id, AMemoryType.Remote);
            _rexPrefix = 0;
            _colorHex = "";
            _colorReg = "";
            _colorSymbol = "";
            _endColor = "";
            _opCodeFlags = new AOpCodeFlags();
            LastDisassembleData = new ALastDisassembleData();
            Debug = false;
            SyntaxHighlighting = false;
            _modRmPosition = ATmrPos.None;
            _aggressiveAlignment = false;
            _memory = new UBytePtr(64);
            _hasVex = false;
            _ripRelative = false;
            _prefix = new APrefix();
            _prefix2 = new APrefix();
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
        #region RegNrToStr
        private String RegNrToStr(ARegisterType listType, int nr)
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
                            case 4: result = _rexPrefix == 0 ? "ah" : "spl"; break;
                            case 5: result = _rexPrefix == 0 ? "ch" : "bpl"; break;
                            case 6: result = _rexPrefix == 0 ? "dh" : "sil"; break;
                            case 7: result = _rexPrefix == 0 ? "bh" : "dil"; break;
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
        private String Rd(Byte bt)
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
            result = _colorReg + result + _endColor;
            return result;
        }
        #endregion
        #region Rd8
        private String Rd8(Byte bt)
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
                case 4: result = _rexPrefix == 0 ? "ah" : "spl"; break;
                case 5: result = _rexPrefix == 0 ? "ch" : "bpl"; break;
                case 6: result = _rexPrefix == 0 ? "dh" : "sil"; break;
                case 7: result = _rexPrefix == 0 ? "bh" : "dil"; break;
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
            result = _colorReg + result + _endColor;
            return result;
        }
        #endregion
        #region Rd16
        private String Rd16(Byte bt)
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
            result = _colorReg + result + _endColor;
            return result;
        }
        #endregion
        #region GetReg
        private Byte GetReg(Byte bt)
        {
            var result = (bt >> 3) & 7;
            if (RexR)
                result |= 8; //extend the reg field
            return (Byte)result;
        }
        #endregion
        #region GetSegmentOverride
        private String GetSegmentOverride(APrefix prefix)
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
        private Byte GetBitOf(UInt64 bt, int bit)
        {
            var result = ABitUtils.GetBit(bit, bt);
            return (Byte)result;
        }
        #endregion
        #region GetMod
        private Byte GetMod(Byte bt)
        {
            var result = (bt >> 6) & 3;
            return (Byte) result;
        }
        #endregion
        #region R8
        private String R8(Byte bt)
        {
            var regNr = GetReg(bt);
            var result = _colorReg + RegNrToStr(ARegisterType.Rt8, regNr) + _endColor;
            return result;
        }
        #endregion
        #region R16
        private String R16(Byte bt)
        {
            var regNr = GetReg(bt);
            var result = _colorReg + RegNrToStr(ARegisterType.Rt16, regNr) + _endColor;
            return result;
        }
        #endregion
        #region R32
        private String R32(Byte bt)
        {
            var regNr = GetReg(bt);
            String result;
            if (RexW)
                result = _colorReg + RegNrToStr(ARegisterType.Rt64, regNr) + _endColor;
            else
                result = _colorReg + RegNrToStr(ARegisterType.Rt32, regNr) + _endColor;
            return result;
        }
        #endregion
        #region R64
        private String R64(Byte bt)
        {
            var regNr = GetReg(bt);
            var result = _colorReg + RegNrToStr(ARegisterType.Rt64, regNr) + _endColor;
            return result;
        }
        #endregion
        #region Xmm
        private String Xmm(Byte bt)
        {
            var regNr = GetReg(bt);
            String result;
            if (_opCodeFlags.L)
                result = _colorReg + RegNrToStr(ARegisterType.RtYmm, regNr) + _endColor;
            else
                result = _colorReg + RegNrToStr(ARegisterType.RtXmm, regNr) + _endColor;
            return result;
        }
        #endregion
        #region Mm
        private String Mm(Byte bt)
        {
            var regNr = GetReg(bt);
            var result = _colorReg + RegNrToStr(ARegisterType.RtMm, regNr) + _endColor;
            return result;
        }
        #endregion
        #region SReg
        private String SReg(Byte bt)
        {
            var regNr = GetReg(bt);
            var result = _colorReg + RegNrToStr(ARegisterType.RtSegment, regNr) + _endColor;
            return result;
        }
        #endregion
        #region Cr
        private String Cr(Byte bt)
        {
            var regNr = GetReg(bt);
            var result = _colorReg + RegNrToStr(ARegisterType.RtControlRegister, regNr) + _endColor;
            return result;
        }
        #endregion
        #region Dr
        private String Dr(Byte bt)
        {
            var regNr = GetReg(bt);
            var result = _colorReg + RegNrToStr(ARegisterType.RtDebugRegister, regNr) + _endColor;
            return result;
        }
        #endregion
        #region GetRm
        private Byte GetRm(Byte bt)
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
        private String ModRm(UBytePtr memory, APrefix prefix, int modRmByte, int inst, ref UInt32 last, int operandSize, int addressSize = 0, ATmrPos position = ATmrPos.Left)
        {
            var result  = ModRm2(memory, prefix, modRmByte, inst, ref last, operandSize, addressSize, position);
            return result;
        }
        private String ModRm(UBytePtr memory, APrefix prefix, int modRmByte, int inst,  ref UInt32 last, ATmrPos position = ATmrPos.Left)
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
        private String ModRm2(UBytePtr memory, APrefix prefix, int modRmByte, int inst, ref UInt32 last, int operandSize = 0, int addressSize = 0, ATmrPos position = ATmrPos.Left)
        {
            _modRmPosition = position;
            var result = "";
            var showExtraReg = _hasVex & (_opCodeFlags.SkipExtraReg == false);
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
                            if (showExtraReg & _opCodeFlags.SkipExtraRegOnMemoryAccess)
                                showExtraReg = false;
                            switch (GetRm(memory[modRmByte]))
                            {
                                case 0:
                                    result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "ax" + _endColor + ']';
                                    break;
                                case 1:
                                    result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "cx" + _endColor + ']';
                                    break;
                                case 2:
                                    result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "dx" + _endColor + ']';
                                    break;
                                case 3:
                                    result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "bx" + _endColor + ']';
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
                                            _ripRelative = true;
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
                                    result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "si" + _endColor + ']';
                                    break;
                                case 7:
                                    result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "di" + _endColor + ']';
                                    break;
                                case 8:
                                    result = GetSegmentOverride(prefix) + '[' + _colorReg + "r8" + _endColor + ']';
                                    break;
                                case 9:
                                    result = GetSegmentOverride(prefix) + '[' + _colorReg + "r9" + _endColor + ']';
                                    break;
                                case 10:
                                    result = GetSegmentOverride(prefix) + '[' + _colorReg + "r10" + _endColor + ']';
                                    break;
                                case 11:
                                    result = GetSegmentOverride(prefix) + '[' + _colorReg + "r11" + _endColor + ']';
                                    break;
                                case 12:
                                    result = GetSegmentOverride(prefix) + '[' + _colorReg + "r12" + _endColor + ']';
                                    break;
                                case 13:
                                    result = GetSegmentOverride(prefix) + '[' + _colorReg + "r13" + _endColor + ']';
                                    break;
                                case 14:
                                    result = GetSegmentOverride(prefix) + '[' + _colorReg + "r14" + _endColor + ']';
                                    break;
                                case 15:
                                    result = GetSegmentOverride(prefix) + '[' + _colorReg + "r15" + _endColor + ']';
                                    break;
                            }
                            if (operandSize != 0)
                                LastDisassembleData.DataSize = operandSize / 8;
                            result = operandString + result;
                        }
                        break;
                    case 1:
                        {
                            if (showExtraReg & _opCodeFlags.SkipExtraRegOnMemoryAccess)
                                showExtraReg = false;
                            if (GetRm(memory[modRmByte]) != 4)
                            {
                                LastDisassembleData.ModRmValueType = ADisassemblerValueType.Value;
                                LastDisassembleData.ModRmValue = (UIntPtr)(SByte)memory[modRmByte + 1];
                            }
                            switch (GetRm(memory[modRmByte]))
                            {
                                case 0:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "ax" + _endColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "ax" + _endColor + IntToHexSigned((UIntPtr)(SByte)memory[modRmByte + 1], 2) + ']';
                                    break;
                                case 1:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "cx" + _endColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "cx" + _endColor + IntToHexSigned((UIntPtr)(SByte)memory[modRmByte + 1], 2) + ']';
                                    break;
                                case 2:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "dx" + _endColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "dx" + _endColor + IntToHexSigned((UIntPtr)(SByte)memory[modRmByte + 1], 2) + ']';
                                    break;
                                case 3:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "bx" + _endColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "bx" + _endColor + IntToHexSigned((UIntPtr)(SByte)memory[modRmByte + 1], 2) + ']';
                                    break;
                                case 4:
                                    result = GetSegmentOverride(prefix) + '[' + Sib(memory, modRmByte + 1, ref last, addressSize) + ']';
                                    last -= 1;
                                    break;
                                case 5:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "bp" + _endColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "bp" + _endColor + IntToHexSigned((UIntPtr)(SByte)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 6:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "si" + _endColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "si" + _endColor + IntToHexSigned((UIntPtr)(SByte)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 7:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "di" + _endColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "di" + _endColor + IntToHexSigned((UIntPtr)(SByte)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 8:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r8" + _endColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r8" + _endColor + IntToHexSigned((UIntPtr)(SByte)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 9:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r9" + _endColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r9" + _endColor + IntToHexSigned((UIntPtr)(SByte)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 10:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r10" + _endColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r10" + _endColor + IntToHexSigned((UIntPtr)(SByte)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 11:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r11" + _endColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r11" + _endColor + IntToHexSigned((UIntPtr)(SByte)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 12:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r12" + _endColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r12" + _endColor + IntToHexSigned((UIntPtr)(SByte)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 13:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r13" + _endColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r13" + _endColor + IntToHexSigned((UIntPtr)(SByte)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 14:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r14" + _endColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r14" + _endColor + IntToHexSigned((UIntPtr)(SByte)memory[modRmByte + 1], 2, true, 2) + ']';
                                    break;
                                case 15:
                                    if ((SByte)memory[modRmByte + 1] >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r15" + _endColor + '+' + IntToHexSigned((UIntPtr)memory[modRmByte + 1], 2) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r15" + _endColor + IntToHexSigned((UIntPtr)(SByte)memory[modRmByte + 1], 2, true, 2) + ']';
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
                            if (showExtraReg & _opCodeFlags.SkipExtraRegOnMemoryAccess)
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
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "ax" + _endColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "ax" + _endColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;

                                case 1:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "cx" + _endColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "cx" + _endColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;

                                case 2:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "dx" + _endColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "dx" + _endColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 3:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "bx" + _endColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "bx" + _endColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 4:
                                    result = GetSegmentOverride(prefix) + '[' + Sib(memory, modRmByte + 1, ref last, addressSize) + ']';
                                    last -= 4;
                                    break;
                                case 5:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "bp" + _endColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "bp" + _endColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 6:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "si" + _endColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "si" + _endColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 7:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "di" + _endColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + regPrefix + "di" + _endColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 8:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r8" + _endColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r8" + _endColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 9:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r9" + _endColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r9" + _endColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 10:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r10" + _endColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r10" + _endColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 11:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r11" + _endColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r11" + _endColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 12:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r12" + _endColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r12" + _endColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 13:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r13" + _endColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r13" + _endColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;

                                case 14:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r14" + _endColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r14" + _endColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
                                    break;
                                case 15:
                                    if ((int)value >= 0)
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r15" + _endColor + '+' + IntToHexSigned(value, 8) + ']';
                                    else
                                        result = GetSegmentOverride(prefix) + '[' + _colorReg + "r15" + _endColor + '-' + IntToHexSigned((UIntPtr)(-(int)value), 8) + ']';
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
                                            result = _opCodeFlags.L ? "ymm0" : "xmm0";
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
                                            result = _opCodeFlags.L ? "ymm1" : "xmm1";
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
                                            result = _opCodeFlags.L ? "ymm2" : "xmm2";
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
                                            result = _opCodeFlags.L ? "ymm3" : "xmm3";
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
                                            result = _rexPrefix != 0 ? "spl" : "ah";
                                            break;
                                        case 3:
                                            result = "mm4";
                                            break;
                                        case 4:
                                            result = _opCodeFlags.L ? "ymm4" : "xmm4";
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
                                            result = _rexPrefix != 0 ? "bpl" : "ch";
                                            break;
                                        case 3:
                                            result = "mm5";
                                            break;
                                        case 4:
                                            result = _opCodeFlags.L ? "ymm5" : "xmm5";
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
                                            result = _rexPrefix != 0 ? "sil" : "dh";
                                            break;
                                        case 3:
                                            result = "mm6";
                                            break;
                                        case 4:
                                            result = _opCodeFlags.L ? "ymm6" : "xmm6";
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
                                            result = _rexPrefix != 0 ? "dil" : "bh";
                                            break;
                                        case 3:
                                            result = "mm7";
                                            break;
                                        case 4:
                                            result = _opCodeFlags.L ? "ymm7" : "xmm7";
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
                                            result = _opCodeFlags.L ? "ymm8" : "xmm8";
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
                                            result = _opCodeFlags.L ? "ymm9" : "xmm9";
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
                                            result = _opCodeFlags.L ? "ymm10" : "xmm10";
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
                                            result = _opCodeFlags.L ? "ymm11" : "xmm11";
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
                                        case 4: result = _opCodeFlags.L ? "ymm12" : "xmm12";
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
                                            result = _opCodeFlags.L ? "ymm13" : "xmm13";
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
                                            result = _opCodeFlags.L ? "ymm14" : "xmm14";
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
                                        case 4: result = _opCodeFlags.L ? "ymm15" : "xmm15";
                                            break;
                                    }
                                    break;
                            }
                            result = _colorReg + result + _endColor;
                        }
                        break;
                }
                if (showExtraReg)
                {
                    switch (inst)
                    {
                        case 0:
                            ep = RegNrToStr(RexW ? ARegisterType.Rt64 : ARegisterType.Rt32, ~_opCodeFlags.Vvvv & 0xf);
                            break;
                        case 1:
                            ep = RegNrToStr(ARegisterType.Rt16, ~_opCodeFlags.Vvvv & 0xf);
                            break;
                        case 2:
                            ep = RegNrToStr(ARegisterType.Rt8, ~_opCodeFlags.Vvvv & 0xf);
                            break;
                        case 3:
                            ep = RegNrToStr(ARegisterType.RtMm, ~_opCodeFlags.Vvvv & 0xf);
                            break;
                        case 4:
                            ep = RegNrToStr(_opCodeFlags.L ? ARegisterType.RtYmm : ARegisterType.RtXmm, ~_opCodeFlags.Vvvv & 0xf);
                            break;
                    }
                    switch (position)
                    {
                        case ATmrPos.Left:
                        case ATmrPos.None:
                            result = result + ',' + _colorReg + ep + _endColor;
                            break;
                        case ATmrPos.Right:
                            result = _colorReg + ep + _endColor + ',' + result;
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
        private String Sib(UBytePtr memory, int sibByte, ref UInt32 last, int addressSize = 0)
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
                result = _colorReg + result + _endColor;
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
                indexString = _colorReg + indexString + _endColor;
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
            if ((ss > 0) && (index != 4))
                indexString = indexString + '*' + _colorHex + LastDisassembleData.SibScaler + _endColor;
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
                    if (displacementString[1] == '-')
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
        private int OpCode4Fst(String opCode)
        {
            var result = 2; //float
            if (opCode.Length >= 4)
            {
                switch (opCode[3])
                {
                    case 'c':
                    case 'e':
                    case 's':
                        result = 1;
                        break;
                }
            }
            return result;
        }
        #endregion
        #region OpCode3Fn
        private int OpCode3Fn(String opCode)
        {
            var result = 2; //float
            if (opCode.Length < 3)
                return result;
            switch (opCode[2])
            {
                case 's':
                    result = 1;
                    break; //fnst
            }
            return result;
        }
        #endregion
        #region OpCode3Fs
        private int OpCode3Fs(String opCode)
        {
            var result = 2; //float
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
        private int OpCode2F(String opCode)
        {
            var result = 2;
            if (opCode.Length < 2)
                return result;
            switch (opCode[1])
            {
                case 'i':
                    result = 1; break; //fixxxxx
                case 'n':
                    result = OpCode3Fn(opCode); break;
                case 's':
                    result = OpCode3Fs(opCode); break;
            }
            return result;
        }
        #endregion
        #region OpCodeToValueType
        private int OpCodeToValueType(String opCode)
        {
            var result = 1;
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
        #region PreviousOpCode
        public UIntPtr PreviousOpCode(UIntPtr address, ADisassembler d = null)
        {
            var result = UIntPtr.Zero;
            if (d == null)
                d = this;
            var aggressive = d._aggressiveAlignment;
            d._aggressiveAlignment = true;
            var x = PreviousOpCodeHelp(d, address, 80, ref result);
            if (x != address)
            {
                //no match found 80 bytes from the start
                //try 40
                x = PreviousOpCodeHelp(d, address, 40, ref result);
                if (x != address)
                {
                    //nothing with 40, try 20
                    x = PreviousOpCodeHelp(d, address, 20, ref result);
                    if (x != address)
                    {
                        //no 20, try 10
                        x = PreviousOpCodeHelp(d, address, 10, ref result);
                        if (x != address)
                        {
                            //and if all else fails try to find the closest one
                            result = address - 1;
                            for (var i = 1; i <= 20; i++)
                            {
                                x = address - i;
                                var s = "";
                                d.Disassemble(ref x, ref s);
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
            d._aggressiveAlignment = aggressive;
            return result;
        }
        #endregion
        #region GetLastByteString
        private String GetLastByteString()
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
                        //cloaked = hascloakedregioninrange(lastdisassembledata.address + i, 1, va, pa);
                        //if (cloaked) result += "{C00FF00}"; //green
                    }
                    // todo add this!
                    changed = false;//hasaddressbeenchanged(lastdisassembledata.address + i);
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
            var i = 0;
            var j = 0;
            var haserror = false;
            var result = false;
            if (d == "")
                return result;
            if (AStringUtils.Pos(" ", d) == -1)
                return result;
            //if the opcode has a , then get the last part
            i = AStringUtils.Pos(",", d);
            if (i != -1)
                d = AStringUtils.Copy(d, i + 1, d.Length);
            if (context == null)
            {
                if (AStringUtils.Pos("+", d) != -1)
                    return result; //it has an offset, so also a register. without a context, this is impossible
                //check O for a hexadecimal value of 8 bytes and longer.
                if (Has4ByteHexString(d, ref s))
                {
                    address = (UIntPtr)AStringUtils.StrToQWordEx(s); //s already has the $ in front
                    result = AMemoryHelper.IsAddress(SymbolHandler.Process.Handle, address);
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
                address = SymbolHandler.GetAddressFromName(d, false, out haserror);
                result = !haserror;
            }
            return result;
        }
        #endregion
        #region PreviousOpCodeHelp
        public UIntPtr PreviousOpCodeHelp(ADisassembler d, UIntPtr address, int distance, ref UIntPtr result2)
        {
            var y = UIntPtr.Zero;
            var s = "";
            var x = address - distance;
            while (x.ToUInt64() < address.ToUInt64())
            {
                y = x;
                d.Disassemble(ref x, ref s);
            }
            var result = x;
            result2 = y;
            return result;
        }
        #endregion
        #region IntToHexSigned
        private String IntToHexSigned(UIntPtr value, int chars, Boolean signed = false, int signedSize = 0)
        {
            if (ShowSymbols || ShowModules)
                return IntToHexSignedWithSymbols(value, chars, signed, signedSize);
            return IntToHexSignedWithoutSymbols(value, chars, signed, signedSize);
        }
        #endregion
        #region IntToHexSignedWithoutSymbols
        private String IntToHexSignedWithoutSymbols(UIntPtr value, int chars, Boolean signed = false, int signedSize = 0)
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
                            result = _colorHex + '-' + AStringUtils.IntToHex(-(SByte)value, chars) + _endColor;
                        else
                            result = _colorHex + AStringUtils.IntToHex((SByte)value, chars) + _endColor;
                        break;
                    }
                    case 4:
                    {
                        if ((Int16)value < 0)
                            result = _colorHex + '-' + AStringUtils.IntToHex(-(Int16)value, chars) + _endColor;
                        else
                            result = _colorHex + AStringUtils.IntToHex((Int16)value, chars) + _endColor;
                        break;
                    }
                    case 8:
                    {
                        if ((Int64)value < 0)
                            result = _colorHex + '-' + AStringUtils.IntToHex(-(Int64)value, chars) + _endColor;
                        else
                            result = _colorHex + AStringUtils.IntToHex((Int64)value, chars) + _endColor;
                        break;
                    }
                    default:
                        result = _colorHex + AStringUtils.IntToHex(value, chars) + _endColor;
                        break;
                }
            }
            else
                result = _colorHex + AStringUtils.IntToHex(value, chars) + _endColor;
            return result;
        }
        #endregion
        #region IntToHexSignedWithSymbols
        private String IntToHexSignedWithSymbols(UIntPtr value, int chars, Boolean signed = false, int signedSize = 0)
        {
            String result;
            if ((ShowSymbols | ShowModules | ShowSections) & (chars >= 8))
            {
                result = SymbolHandler.GetNameFromAddress(value, ShowSymbols, ShowModules, ShowSections, null, out var found, chars, false);
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
                        result = _colorHex + result + _endColor;
                    else
                        result = _colorSymbol + result + _endColor;
                }
            }
            else
                result = IntToHexSignedWithoutSymbols(value, chars, signed, signedSize);
            return result;
        }
        #endregion
        #region SplitDisassembledString -- todo
        //public void SplitDisassembledString(String disassembled, Boolean showvalues, ref String address, ref String bytes, ref String opcode, ref String special, pcontext context = null)
        //{
        //    var offset = UIntPtr.Zero;
        //    var value = UIntPtr.Zero;
        //    var e = 0;
        //    var i = 0;
        //    var j = 0;
        //    var j2 = 0;
        //    var k = 0;
        //    var l = 0;
        //    string ts, ts2, ts3;
        //    var actualread = UIntPtr.Zero;
        //    var valuetype = 0;
        //    //    tokens: ttokens;
        //    var fvalue = 0.0f;
        //    var fvalue2 = 0.0;
        //    var tempbuf = new UStringBuilder(127);
        //    var pc = "";
        //    var pwc = "";
        //    tvariabletype variabletype;
        //    var tempaddress = UIntPtr.Zero;
        //    var err = false;
        //    var isjumper = false;
        //    var hexstring = "";
        //    i = AStringUtils.Pos(" - ", disassembled);
        //    address = AStringUtils.Copy(disassembled, 1, i - 1).ToUpper();
        //    i += 3;
        //    j = AStringUtils.PosEx(" - ", disassembled, i);
        //    if (j == -1)
        //        j = disassembled.Length + 1;
        //    bytes = AStringUtils.Copy(disassembled, i, (j - i));
        //    j += 3;
        //    k = AStringUtils.PosEx(" : ", disassembled, j);
        //    l = k;
        //    if (k == -1)
        //        k = disassembled.Length + 1;
        //    opcode = AStringUtils.Copy(disassembled, j, (k - j));
        //    if (showvalues)
        //    {
        //        ts = "";
        //        special = "";
        //        if ((hasaddress(opcode, tempaddress, context)) | ((opcode.Length > 3) && (opcode[0] == 'l') && (opcode[1] == 'e') && (opcode[2] == 'a')))
        //        {
        //            if (isaddress(tempaddress))
        //            {
        //                try
        //                {
        //
        //                    if ((opcode[0] == 'l') && (opcode[1] == 'e') && (opcode[2] == 'a')) //lea
        //                    {
        //                        j = AStringUtils.Pos("[", opcode);
        //                        j2 = AStringUtils.Pos("]", opcode);
        //                        ts2 = AStringUtils.Copy(opcode, j + 1, j2 - j - 1);
        //                        tempaddress = SymbolHandler.GetAddressFromName(ts2, false, out err);
        //                        if (err)
        //                            return; //error
        //                    }
        //                }
        //                catch
        //                {
        //                    tempaddress = UIntPtr.Zero; ////////////////////////// REACHED WITH INDEX FIX
        //                }
        //                isjumper = false;
        //                if (opcode[1] == 'j')
        //                    isjumper = true; //jmp, jx
        //                if ((opcode[1] == 'l') && (opcode[2] == 'o') && (opcode[3] == 'o'))
        //                    isjumper = true; //loop
        //                if ((opcode[1] == 'c') && (opcode[2] == 'a'))
        //                    isjumper = true; //call
        //                valuetype = opcodetovaluetype(opcode);
        //                i = pos("[", disassembled);
        //                if (i > 0)
        //                {
        //                    //it might have an override
        //                    if (pos("qword ptr", opcode) > 0)
        //                        valuetype = 4;
        //                    else if (pos("dword ptr", opcode) > 0) //usually a double
        //                        valuetype = 2;
        //                    else if (pos("word ptr", opcode) > 0)
        //                        valuetype = 1;
        //                    else if (pos("byte ptr", opcode) > 0)
        //                        valuetype = 0;
        //                    else
        //                    {
        //                        //check the register used
        //                        j2 = pos(",[", opcode);
        //                        k = pos("],", opcode);
        //                        if (j2 > 0)  //register in front
        //                        {
        //                            l = pos(" ", opcode);
        //                            ts3 = copy(opcode, l + 1, j2 - l - 1);
        //
        //                            switch (tokentoregisterbit(uppercase(ts3)))
        //                            {
        //                                case ttregister8bit:
        //                                    valuetype = 0;
        //                                    break;
        //                                case ttregister16bit:
        //                                    valuetype = 1;
        //                                    break;
        //                                case ttregister32bit:
        //                                    valuetype = 2;
        //                                    break;
        //                                default: valuetype = 2;
        //                                    break;
        //                            }
        //                        }
        //                        else
        //                        if (k > 0)   //register after ],
        //                        {
        //                            l = pos("],", opcode);
        //                            ts3 = copy(opcode, l + 2, length(opcode) - l - 1);
        //
        //                            switch (tokentoregisterbit(uppercase(ts3)))
        //                            {
        //                                case ttregister8bit: valuetype = 0; break;
        //                                case ttregister16bit: valuetype = 1; break;
        //                                case ttregister32bit: valuetype = 2; break;
        //                                default: valuetype = 2; break;
        //                            }
        //                        } //else no idea, check var
        //                    }
        //                } //not an address specifier
        //
        //                if (valuetype == 2)
        //                {
        //                    if (readprocessmemory(processhandle, (pointer)(tempaddress), &tempbuf[0], 16, actualread))
        //                    {
        //                        variabletype = findtypeofdata(tempaddress, &tempbuf[0], 16);
        //                        switch (variabletype)
        //                        {
        //                            case vtsingle: valuetype = 3; break;
        //                            case vtdouble: valuetype = 4; break;
        //                            case vtstring: valuetype = 5; break;
        //                            case vtunicodestring: valuetype = 6; break;
        //                        }
        //                    }
        //                }
        //                if (isjumper)
        //                    valuetype = 2; //handle it as a dword
        //                value = 0;
        //                fvalue = 0;
        //                fvalue2 = 0;
        //                switch (valuetype)
        //                {
        //                    case 0: if (readprocessmemory(processhandle, (pointer)(tempaddress), &value, 1, actualread)) ts = AStringUtils.IntToHex(value, 2); break;
        //                    case 1: if (readprocessmemory(processhandle, (pointer)(tempaddress), &value, 2, actualread)) ts = AStringUtils.IntToHex(value, 4); break;
        //                    case 2:
        //                        if (readprocessmemory(processhandle, (pointer)(tempaddress), &value, 4, actualread))
        //                        {
        //                            if (isjumper && ((value & 0xffff) == 0x25ff))  //it's a jmp [xxxxxxxx]    / call [xxxxxx] ...
        //                            {
        //                                value = 0;
        //                                if (readprocessmemory(processhandle, (pointer)(tempaddress + 2), &value, 4, actualread))
        //                                {
        //                                    if (is64bit)
        //                                        value = tempaddress + 6 + value;
        //                                    if (readprocessmemory(processhandle, (pointer)(value), &value, processhandler.pointersize, actualread))
        //                                        ts = "->" + symhandler.getnamefromaddress(value, symhandler.showsymbols, symhandler.showmodules, symhandler.showsections, nil, nil, 8, false);
        //                                }
        //                            }
        //                            else
        //                                ts = symhandler.getnamefromaddress(value, symhandler.showsymbols, symhandler.showmodules, symhandler.showsections, nil, nil, 8, false);
        //                            if (isjumper)
        //                            {
        //                                //check if ts is a name or a hexadecimal value
        //                                //if hex, don't use it
        //                                val("$" + ts, j, i);
        //                                if (i == 0)
        //                                    ts = ""; //zero the string, it's a hexadecimal string
        //                            }
        //                        }
        //                        break;
        //                    case 3:
        //                        if (readprocessmemory(processhandle, (pointer)(tempaddress), &fvalue, 4, actualread))
        //                            ts = format("(float)%.4f", set::of(fvalue, eos));
        //                        break;
        //                    case 4:
        //                        if (readprocessmemory(processhandle, (pointer)(tempaddress), &fvalue2, 8, actualread))
        //                            ts = format("(double)%.4f", set::of(fvalue2, eos));
        //                        break;
        //                    case 5:
        //                        {
        //                            actualread = 0;
        //                            readprocessmemory(processhandle, (pointer)(tempaddress), &tempbuf[0], 128, actualread);
        //                            tempbuf[127] = 0;
        //                            tempbuf[126] = ord('.');
        //                            tempbuf[125] = ord('.');
        //                            tempbuf[124] = ord('.');
        //                            if (actualread > 0)
        //                                tempbuf[actualread - 1] = 0;
        //                            pc = &tempbuf[0];
        //                            ts = '"' + pc + '"';
        //                        }
        //                        break;
        //                    case 6:
        //                        {
        //                            actualread = 0;
        //                            readprocessmemory(processhandle, (pointer)(tempaddress), &tempbuf[0], 128, actualread);
        //                            tempbuf[127] = 0;
        //                            tempbuf[126] = 0;
        //                            tempbuf[125] = 0;
        //                            tempbuf[124] = ord('.');
        //                            tempbuf[123] = 0;
        //                            tempbuf[122] = ord('.');
        //                            tempbuf[121] = 0;
        //                            tempbuf[120] = ord('.');
        //                            if (actualread > 1)
        //                            {
        //                                tempbuf[actualread - 1] = 0;
        //                                tempbuf[actualread - 2] = 0;
        //                            }
        //                            pwc = &tempbuf[0];
        //                            ts = "\"\"" + pwc + "\"\"";
        //                        }
        //                        break;
        //                }
        //                if (ts != "")
        //                    ts = '[' + ts + ']';
        //            }
        //            else
        //            {
        //                //tempaddress doesn't seem to be an address
        //                variabletype = findtypeofdata(0, &tempaddress, processhandler.pointersize);
        //                if (variabletype == vtsingle)
        //                    ts = UStringUtils.Sprintf("(float)%.4f", tempaddress.ToIntPtr().ReadFloat());
        //            }
        //        }
        //        special = ts;
        //    }
        //    else
        //        special = "";
        //}
        #endregion
        #region DecodeLastParametersToString -- todo
        //public String DecodeLastParametersToString()
        //{
        //    UIntPtr jumpaddress = UIntPtr.Zero;
        //    var buffer = new AByteArray();
        //    buffer.SetLength(63);
        //    var x = UIntPtr.Zero;
        //    var a = false;
        //    var s = "";
        //    var parametercount = 0;
        //    var sv1 = "";
        //    var sv2 = "";
        //    var i = 0;
        //    if (lastdisassembledata.commentsoverride != "")
        //        return lastdisassembledata.commentsoverride;
        //    var result = "";
        //    if (lastdisassembledata.isjump)
        //    {
        //        if (lastdisassembledata.modrmvaluetype == tdisassemblervaluetype.tdisassemblervaluetype.dvtaddress)
        //        {
        //            jumpaddress = lastdisassembledata.modrmvalue;
        //            if (~readprocessmemory(processhandle, (pointer)(jumpaddress), &jumpaddress, processhandler.pointersize, x))
        //                return result;
        //        }
        //        else
        //        {
        //            if (lastdisassembledata.parametervaluetype == tdisassemblervaluetype.tdisassemblervaluetype.dvtnone)
        //                return result; //jump with no address (e.g reg)
        //            jumpaddress = lastdisassembledata.parametervalue;
        //        }
        //        //check if the bytes at jumpAddress is ff 25 (jmp [xxxxxxxx])
        //        if (readprocessmemory(processhandle, (pointer)(jumpaddress), &buffer[0], 6, x))
        //        {
        //
        //            if ((buffer[0] == 0xff) && (buffer[1] == 0x25))
        //            {
        //                result = result + "->";  //double, so ->->
        //                if (is64bit)
        //                    jumpaddress = jumpaddress + 6 + pinteger(&buffer[2]); //jumpaddress+6 because of relative addressing
        //                else
        //                    jumpaddress = pdword(&buffer[2]);
        //                //jumpaddress now contains the address of the address to jump to
        //                //so, get the address it actually jumps to
        //                if (~readprocessmemory(processhandle, (pointer)(jumpaddress), &jumpaddress, processhandler.pointersize, x))
        //                    return result;
        //            }
        //            s = symhandler.getnamefromaddress(jumpaddress, symhandler.showsymbols, symhandler.showmodules, symhandler.showsections, nil, nil, 8, false);
        //            if (pos(s, lastdisassembledata.parameters) == 0)  //no need to show a comment if it's exactly the same
        //                result = result + "->" + s;
        //        }
        //    }
        //    else
        //    {
        //        if ((lastdisassembledata.modrmvaluetype == tdisassemblervaluetype.tdisassemblervaluetype.dvtaddress) || (lastdisassembledata.parametervaluetype != tdisassemblervaluetype.tdisassemblervaluetype.dvtnone))
        //        {
        //            a = false;
        //            parametercount = 0;
        //            if (lastdisassembledata.parametervaluetype != tdisassemblervaluetype.tdisassemblervaluetype.dvtnone)
        //                parametercount += 1;
        //            if (lastdisassembledata.modrmvaluetype == tdisassemblervaluetype.tdisassemblervaluetype.dvtaddress)
        //                parametercount += 1;
        //            if (lastdisassembledata.modrmvaluetype == tdisassemblervaluetype.tdisassemblervaluetype.dvtaddress)
        //            {
        //                if ((parametercount > 1) && (modrmposition == mright))
        //                    values[1].value = lastdisassembledata.modrmvalue;
        //                else
        //                    values[0].value = lastdisassembledata.modrmvalue;
        //            }
        //            if (lastdisassembledata.parametervaluetype != tdisassemblervaluetype.tdisassemblervaluetype.dvtnone)
        //            {
        //                if ((parametercount > 1) && (modrmposition != mright))
        //                    values[1].value = lastdisassembledata.parametervalue;
        //                else
        //                    values[0].value = lastdisassembledata.parametervalue;
        //            }
        //            //for (i = 0; i <= parametercount - 1; i++)
        //            //{
        //            //    values[i].s = "";
        //            //    if (isaddress(values[i].value))
        //            //    {
        //            //        values[i].isaddress = true;
        //            //        x = 0;
        //            //        values[i].vtype = vtdword;
        //            //        readprocessmemory(processhandle, (pointer)(values[i].value), &buffer[0], 63, x);
        //            //        if (x > 0)
        //            //        {
        //            //            if (lastdisassembledata.isfloat)
        //            //            {
        //            //                switch (lastdisassembledata.datasize)
        //            //                {
        //            //                    case 4:
        //            //                        values[i].vtype = vtsingle;
        //            //                        break;
        //            //                    case 8:
        //            //                        values[i].vtype = vtdouble;
        //            //                        break;
        //            //                    case 10:
        //            //                        values[i].vtype = vtqword;
        //            //                        break; 
        //            //                }
        //            //            }
        //            //            else
        //            //                values[i].vtype = findtypeofdata(values[i].value, &buffer[0], x);
        //            //        }
        //            //        else
        //            //        {
        //            //            values[i].s = "";
        //            //            continue;
        //            //        }
        //            //    }
        //            //    else
        //            //    {
        //            //        x = sizeof(values[i].value);
        //            //        pptruint(&buffer[0]) = values[i].value; //assign it so I don't have to make two compare routines
        //            //        values[i].vtype = findtypeofdata(0, &buffer[0], x);
        //            //        values[i].isaddress = false;
        //            //    }
        //            //    switch (values[i].vtype)
        //            //    {
        //            //        case vtbyte:
        //            //            values[i].s = (buffer[0]);
        //            //            break;
        //            //        case vtword:
        //            //            values[i].s = (psmallint(&buffer[0]));
        //            //            break;
        //            //        case vtdword:
        //            //            if (a)
        //            //                values[i].s = AStringUtils.IntToHex(pdword(&buffer[0]), 8);
        //            //            else
        //            //                values[i].s = (pinteger(&buffer[0]));
        //            //            break;
        //            //        case vtqword:
        //            //            values[i].s = (pint64(&buffer[0]));
        //            //            break;
        //            //        case vtsingle:
        //            //            values[i].s = format("%.2f", set::of(psingle(&buffer[0]), eos));
        //            //            break;
        //            //        case vtdouble:
        //            //            values[i].s = format("%.2f", set::of(pdouble(&buffer[0]), eos));
        //            //            break;
        //            //        case vtstring:
        //            //            {
        //            //                buffer[x] = 0;
        //            //                values[i].s = '"' + (pchar)(&buffer[0]) + '"';
        //            //            }
        //            //            break;
        //            //        case vtunicodestring:
        //            //            {
        //            //                buffer[x] = 0;
        //            //                if (x > 0)
        //            //                    buffer[x - 1] = 0;
        //            //
        //            //                values[i].s = '"' + pwidechar(&buffer[0]) + '"';
        //            //            }
        //            //            break;
        //            //        case vtpointer:
        //            //            {
        //            //                if (SymbolHandler.Process.IsX64)
        //            //                    values[i].s = AStringUtils.IntToHex(pqword(&buffer[0]), 8);
        //            //                else
        //            //                    values[i].s = AStringUtils.IntToHex(pdword(&buffer[0]), 8);
        //            //
        //            //            }
        //            //            break;
        //            //    }
        //            //    // result:=VariableTypeToString(vtype);
        //            //    if (values[i].isaddress & (values[i].s != ""))
        //            //        values[i].s = '(' + values[i].s + ')';
        //            //    if (i == 0)
        //            //        result = result + values[i].s;
        //            //    else
        //            //        result = result + ',' + values[i].s;
        //            //}
        //        }
        //    }
        //    return result;
        //}
        #endregion
        #region SetSyntaxHighlighting
        public void SetSyntaxHighlighting(Boolean state)
        {
            SyntaxHighlighting = state;
            if (state)
            {
                _endColor = "{N}";
                _colorHex = "{H}";
                _colorReg = "{R}";
                _colorSymbol = "{S}";
            }
            else
            {
                //no color codes
                _endColor = "";
                _colorHex = "";
                _colorReg = "";
                _colorSymbol = "";
            }
        }
        #endregion
        #region ReadMemory
        private int ReadMemory(UIntPtr address, UIntPtr destination, int size)
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
                //    lastdisassembledata.address = offset;
                //    lastdisassembledata.seperatorcount = 0;
                //    defaultbinutils.disassemble(lastdisassembledata);
                //    result = AStringUtils.IntToHex(lastdisassembledata.address, 8);
                //    result = result + " - ";
                //    for (i = 0; i <= length(lastdisassembledata.bytes) - 1; i++)
                //        result = result + AStringUtils.IntToHex(lastdisassembledata.bytes[i], 2) + ' ';
                //    result = result + " - ";
                //    result = result + lastdisassembledata.opcode;
                //    result = result + ' ';
                //    result = result + lastdisassembledata.parameters;
                //    if (length(lastdisassembledata.bytes) > 0)
                //        offset += length(lastdisassembledata.bytes);
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
                //    lastdisassembledata = armdisassembler.lastdisassembledata;
                //    return result;
                //}
                _modRmPosition = ATmrPos.None;
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
                //        if (length(lastdisassembledata.bytes) == 0)  //BAD!
                //            setlength(lastdisassembledata.bytes, 1);
                //
                //        offset += length(lastdisassembledata.bytes);
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
                //             if (length(lastdisassembledata.bytes) == 0)  //BAD!
                //                 setlength(lastdisassembledata.bytes, 1);
                // 
                //             offset += length(lastdisassembledata.bytes);
                //             return result;
                //         }
                //     }
                // }
                _ripRelative = false;
                if (IsDataOnly)
                    result = "";
                else
                    result = AStringUtils.IntToHex(offset, 8) + " - ";
                var isPrefix = true;
                _prefix = new APrefix(0xf0, 0xf2, 0xf3, 0x2e, 0x36, 0x3e, 0x26, 0x64, 0x65, 0x66, 0x67);
                _prefix2 = new APrefix();
                var startOffset = offset;
                var initialOffset = offset;
                for (i = 32; i <= 63; i++) //debug code
                    _memory[i] = 0xce;
                var actualRead = ReadMemory(offset, _memory.ToIntPtr().ToUIntPtr(), 32);
                var memory = _memory.Shift(0);
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
                        if (_prefix.Contains(memory[0]))
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
                            _prefix2.Add(memory[0]);
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
                    if (_prefix2.Contains(0xf0))
                    {
                        tempResult = "lock ";
                        noVexPossible = true;
                    }
                    if (_prefix2.Contains(0xf2))
                    {
                        tempResult += "repne ";
                        noVexPossible = true;
                    }
                    if (_prefix2.Contains(0xf3))
                    {
                        tempResult += "repe ";
                        noVexPossible = true;
                    }
                    LastDisassembleData.Prefix = tempResult;
                    _opCodeFlags.Clear();
                    _rexPrefix = 0;
                    if (Is64Bit)
                    {
                        if (AMathUtils.InRangeX(memory[0], 0x40, 0x4f))  //does it start with a rex prefix ?
                        {
                            LastDisassembleData.Bytes.Inc();
                            LastDisassembleData.Bytes.Last = memory[0];
                            _rexPrefix = memory[0];
                            _opCodeFlags.B = (_rexPrefix & BIT_REX_B) == BIT_REX_B;
                            _opCodeFlags.X = (_rexPrefix & BIT_REX_X) == BIT_REX_X;
                            _opCodeFlags.R = (_rexPrefix & BIT_REX_R) == BIT_REX_R;
                            _opCodeFlags.W = (_rexPrefix & BIT_REX_W) == BIT_REX_W;
                            if (!IsDataOnly)
                                result = result + IntToHexSigned((UIntPtr)_rexPrefix, 2) + ' ';
                            offset += 1;
                            startOffset += 1;
                            _prefix2.Add(_rexPrefix);
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
                    if (noVexPossible == false && AMathUtils.InRangeX(memory[0], 0xc4, 0xc5))
                    {
                        _hasVex = true;
                        int bytesToMove;
                        if (memory[0] == 0xc5)
                        {
                            //2 byte VEX
                            prefixSize += 2;
                            var vex2 = new AVex2Byte(memory.ToIntPtr(1));
                            _opCodeFlags.Pp = vex2.Pp;
                            _opCodeFlags.L = vex2.L == 1;
                            _opCodeFlags.Vvvv = vex2.Vvvv;
                            _opCodeFlags.R = vex2.R == 0;
                            _opCodeFlags.Mmmmm = 1;
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
                            _opCodeFlags.Pp = vex3.Pp;
                            _opCodeFlags.L = vex3.L == 1;
                            _opCodeFlags.Vvvv = vex3.Vvvv;
                            _opCodeFlags.W = vex3.W == 1; //this one is NOT inverted
                            _opCodeFlags.Mmmmm = vex3.Mmmmm;
                            _opCodeFlags.B = vex3.B == 0;
                            _opCodeFlags.X = vex3.X == 0;
                            _opCodeFlags.R = vex3.R == 0;
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
                            switch (_opCodeFlags.Mmmmm)
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
                        switch (_opCodeFlags.Pp)
                        {
                            case 1:
                                _prefix2.Add(0x66);
                                break;
                            case 2:
                                _prefix2.Add(0xf3);
                                break;
                            case 3:
                                _prefix2.Add(0xf2);
                                break;
                        }
                    }
                    else
                        _hasVex = false;
                    //compatibility fix for code that still checks for rex.* or sets it as a temporary flag replacement
                    _rexPrefix = (Byte)(_opCodeFlags.B ? _rexPrefix | BIT_REX_B : _rexPrefix);
                    _rexPrefix = (Byte)(_opCodeFlags.X ? _rexPrefix | BIT_REX_X : _rexPrefix);
                    _rexPrefix = (Byte)(_opCodeFlags.R ? _rexPrefix | BIT_REX_R : _rexPrefix);
                    _rexPrefix = (Byte)(_opCodeFlags.W ? _rexPrefix | BIT_REX_W : _rexPrefix);
                    if (
                        !DisassembleProcess1(memory, ref offset, ref prefixSize, ref last, ref description) &&
                        !DisassembleProcess2(memory, ref offset, ref prefixSize, ref last, ref description) &&
                        !DisassembleProcess3(memory, ref offset, ref prefixSize, ref last, ref description) &&
                        !DisassembleProcess4(memory, ref offset, ref prefixSize, ref last, ref description) &&
                        !DisassembleProcess5(memory, ref offset, ref prefixSize, ref last, ref description)
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
                                AArrayUtils.CopyMemory(p1, k, _memory.ToIntPtr(), k, (int)td);
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
                    if (_ripRelative)
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
                //lastdisassembledata.iscloaked = hascloakedregioninrange(lastdisassembledata.address, length(lastdisassembledata.bytes), va, pa);
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
                //        if (length(lastdisassembledata.bytes) > 0)
                //            offset = initialOffset + length(lastdisassembledata.bytes);
                //    }
                //}
            }
            catch
            {
                //outputdebugstring(AStringUtils.IntToHex(startOffset,8)+':disassembler exception:'+e.message);
                ///MessageBox(0,pchar('disassembler exception at '+ AStringUtils.IntToHex(startOffset,8)+#13#10+e.message+#13#10+#13#10+'Please provide dark byte the bytes that are at this address so he can fix it'#13#10'(Open another CE instance and in the hexadecimal view go to this address)'),'debug here',MB_OK);
                throw new Exception("error make this work");
            }
            return result ?? "";
        }
        #endregion
    }
}
