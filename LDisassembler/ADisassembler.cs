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
    public partial class ADisassembler : IUDisposable
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
        public Boolean RexB => _opCodeFlags.B;
        public Boolean RexX => _opCodeFlags.X;
        public Boolean RexR => _opCodeFlags.R;
        public Boolean RexW => _opCodeFlags.W;
        public AProcessSharp Proc => SymbolHandler.Process;
        public Boolean IsDisposed { get; set; }
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
        public ADisassembler(ASymbolHandler symbolHandler)
        {
            IsDisposed = false;
            SymbolHandler = symbolHandler;
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
        #region Dispose
        public void Dispose()
        {
            if (IsDisposed)
                return;
            IsDisposed = true;
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
            if (ss > 0 && index != 4)
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
        public UIntPtr PreviousOpCode(UIntPtr address)
        {
            var result = UIntPtr.Zero;
            var aggressive = _aggressiveAlignment;
            _aggressiveAlignment = true;
            var x = PreviousOpCodeHelp(address, 80, ref result);
            if (x != address)
            {
                //no match found 80 bytes from the start
                //try 40
                x = PreviousOpCodeHelp(address, 40, ref result);
                if (x != address)
                {
                    //nothing with 40, try 20
                    x = PreviousOpCodeHelp(address, 20, ref result);
                    if (x != address)
                    {
                        //no 20, try 10
                        x = PreviousOpCodeHelp(address, 10, ref result);
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
            _aggressiveAlignment = aggressive;
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
        #region PreviousOpCodeHelp
        public UIntPtr PreviousOpCodeHelp(UIntPtr address, int distance, ref UIntPtr result2)
        {
            var y = UIntPtr.Zero;
            var s = "";
            var x = address - distance;
            while (x.ToUInt64() < address.ToUInt64())
            {
                y = x;
                Disassemble(ref x, ref s);
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
        public void SplitDisassembledString(String disassembled, Boolean showvalues, out String address, out String bytes, out String opcode, out String special, Object context = null)
        {
            var byteInterp = new AByteInterpreter(SymbolHandler);
            var offset = UIntPtr.Zero;
            var value = UIntPtr.Zero;
            var e = 0;
            var i = 0;
            var j = 0;
            var j2 = 0;
            var k = 0;
            var l = 0;
            string ts, ts2, ts3;
            var actualread = 0;
            var valuetype = 0;
            //    tokens: ttokens;
            var fvalue = 0.0f;
            var fvalue2 = 0.0;
            var tempBufBack = UBinaryUtils.NewZeroByteArray(127);
            var tempbuf = new UBytePtr(tempBufBack);
            AVariableType variabletype;
            var tempaddress = UIntPtr.Zero;
            var err = false;
            var isjumper = false;
            var hexstring = "";
            var readBuf = new UBytePtr(UBinaryUtils.NewZeroByteArray(64));
            i = AStringUtils.Pos(" - ", disassembled);
            address = AStringUtils.Copy(disassembled, 1, i - 1).ToUpper();
            i += 3;
            j = AStringUtils.PosEx(" - ", disassembled, i);
            if (j == -1)
                j = disassembled.Length + 1;
            bytes = AStringUtils.Copy(disassembled, i, (j - i));
            j += 3;
            k = AStringUtils.PosEx(" : ", disassembled, j);
            l = k;
            if (k == -1)
                k = disassembled.Length + 1;
            opcode = AStringUtils.Copy(disassembled, j, (k - j));
            if (showvalues)
            {
                ts = "";
                special = "";
                if (HasAddress(opcode, ref tempaddress, context) | (opcode.Length > 3 && opcode.StartsWith("lea")))
                {
                    if (AMemoryHelper.IsAddress(SymbolHandler.Process.Handle, tempaddress.ToIntPtr()))
                    {
                        try
                        {
        
                            if (opcode.StartsWith("lea")) //lea
                            {
                                j = AStringUtils.Pos("[", opcode);
                                j2 = AStringUtils.Pos("]", opcode);
                                ts2 = AStringUtils.Copy(opcode, j + 1, j2 - j - 1);
                                tempaddress = SymbolHandler.GetAddressFromName(ts2, false, out err);
                                if (err)
                                    return; //error
                            }
                        }
                        catch
                        {
                            tempaddress = UIntPtr.Zero; ////////////////////////// REACHED WITH INDEX FIX
                        }
                        isjumper = false;
                        if (opcode.StartsWith("j"))
                            isjumper = true; //jmp, jx
                        if (opcode.StartsWith("loo"))
                            isjumper = true; //loop
                        if (opcode.StartsWith("ca"))
                            isjumper = true; //call
                        valuetype = OpCodeToValueType(opcode);
                        i = AStringUtils.Pos("[", disassembled);
                        if (i != -1)
                        {
                            //it might have an override
                            if (AStringUtils.Pos("qword ptr", opcode) != -1)
                                valuetype = 4;
                            else if (AStringUtils.Pos("dword ptr", opcode) != -1) //usually a double
                                valuetype = 2;
                            else if (AStringUtils.Pos("word ptr", opcode) != -1)
                                valuetype = 1;
                            else if (AStringUtils.Pos("byte ptr", opcode) != -1)
                                valuetype = 0;
                            else
                            {
                                //check the register used
                                j2 = AStringUtils.Pos(",[", opcode);
                                k = AStringUtils.Pos("],", opcode);
                                if (j2 != -1)  //register in front
                                {
                                    l = AStringUtils.Pos(" ", opcode);
                                    ts3 = AStringUtils.Copy(opcode, l + 1, j2 - l - 1);
        
                                    switch (AAsmTools.Assembler.TokenToRegisterBit(ts3.ToUpper()))
                                    {
                                        case ATokenType.Register8Bit:
                                            valuetype = 0;
                                            break;
                                        case ATokenType.Register16Bit:
                                            valuetype = 1;
                                            break;
                                        case ATokenType.Register32Bit:
                                            valuetype = 2;
                                            break;
                                        default: valuetype = 2;
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
                                            valuetype = 0;
                                            break;
                                        case ATokenType.Register16Bit:
                                            valuetype = 1;
                                            break;
                                        case ATokenType.Register32Bit:
                                            valuetype = 2;
                                            break;
                                        default: valuetype = 2;
                                            break;
                                    }
                                } //else no idea, check var
                            }
                        } //not an address specifier
                        if (valuetype == 2)
                        {
                            if (Kernel32.ReadProcessMemory(Proc.Handle, tempaddress.ToIntPtr(), tempbuf.ToIntPtr(), 16, out actualread))
                            {
                                variabletype = byteInterp.FindTypeOfData(tempaddress, tempbuf, 16);
                                switch (variabletype)
                                {
                                    case AVariableType.Single:
                                        valuetype = 3;
                                        break;
                                    case AVariableType.Double:
                                        valuetype = 4;
                                        break;
                                    case AVariableType.String:
                                        valuetype = 5;
                                        break;
                                    case AVariableType.UnicodeString:
                                        valuetype = 6;
                                        break;
                                }
                            }
                        }
                        if (isjumper)
                            valuetype = 2; //handle it as a dword
                        value = UIntPtr.Zero;
                        fvalue = 0;
                        fvalue2 = 0;
                        switch (valuetype)
                        {
                            case 0: // byte
                                if (Kernel32.ReadProcessMemory(Proc.Handle, tempaddress.ToIntPtr(), readBuf.ToIntPtr(), 1, out actualread))
                                {
                                    value = (UIntPtr)readBuf.ReadByte();
                                    ts = AStringUtils.IntToHex(value, 2);
                                }
                                break;
                            case 1: // word
                                if (Kernel32.ReadProcessMemory(Proc.Handle, tempaddress.ToIntPtr(), readBuf.ToIntPtr(), 2, out actualread))
                                {
                                    value = (UIntPtr)readBuf.ReadUInt16();
                                    ts = AStringUtils.IntToHex(value, 4);
                                }
                                break;
                            case 2: // dword
                                if (Kernel32.ReadProcessMemory(Proc.Handle, tempaddress.ToIntPtr(), readBuf.ToIntPtr(), 4, out actualread))
                                {
                                    value = (UIntPtr)readBuf.ReadUInt32();
                                    if (isjumper && (((int)value.ToUInt32() & 0xffff) == 0x25ff))  //it's a jmp [xxxxxxxx]    / call [xxxxxx] ...
                                    {
                                        value = UIntPtr.Zero;
                                        if (Kernel32.ReadProcessMemory(Proc.Handle, (IntPtr)(tempaddress.ToUInt64() + 2), readBuf.ToIntPtr(), 4, out actualread))
                                        {
                                            value = (UIntPtr)readBuf.ReadUInt32();
                                            if (Proc.IsX64)
                                                value = (UIntPtr)(tempaddress.ToUInt64() + 6 + value.ToUInt64());
                                            if (Kernel32.ReadProcessMemory(Proc.Handle, value.ToIntPtr(), readBuf.ToIntPtr(), Proc.PointerSize, out actualread))
                                            {
                                                value = readBuf.ReadUIntPtr();
                                                ts = "->" + SymbolHandler.GetNameFromAddress(value, SymbolHandler.ShowSymbols, SymbolHandler.ShowModules, SymbolHandler.ShowSections, null, out _, 8, false);
                                            }
                                        }
                                    }
                                    else
                                        ts = SymbolHandler.GetNameFromAddress(value, SymbolHandler.ShowSymbols, SymbolHandler.ShowModules, SymbolHandler.ShowSections, null, out _, 8, false);
                                    if (isjumper)
                                    {
                                        //check if ts is a name or a hexadecimal value
                                        //if hex, don't use it
                                        AStringUtils.Val("0x" + ts, out j, out i);
                                        if (i == 0)
                                            ts = ""; //zero the string, it's a hexadecimal string
                                    }
                                }
                                break;
                            case 3: // Single
                                if (Kernel32.ReadProcessMemory(Proc.Handle, tempaddress.ToIntPtr(), readBuf.ToIntPtr(), 4, out actualread))
                                {
                                    fvalue = readBuf.ReadFloat();
                                    ts = UStringUtils.Sprintf("(float)%.4f", fvalue);
                                }
                                break;
                            case 4: // Double
                                if (Kernel32.ReadProcessMemory(Proc.Handle, tempaddress.ToIntPtr(), readBuf.ToIntPtr(), 8, out actualread))
                                {
                                    fvalue2 = readBuf.ReadDouble();
                                    ts = UStringUtils.Sprintf("(double)%.4f", fvalue2);
                                }
                                break;
                            case 5: // String
                                {
                                    Kernel32.ReadProcessMemory(Proc.Handle, tempaddress.ToIntPtr(), tempbuf.ToIntPtr(), 128, out actualread);
                                    tempbuf[127] = 0;
                                    tempbuf[126] = (Byte)'.';
                                    tempbuf[125] = (Byte)'.';
                                    tempbuf[124] = (Byte)'.';
                                    if (actualread > 0)
                                        tempbuf[actualread - 1] = 0;
                                    ts = '"' + UBitConverter.UnpackSingle("z1", 0, tempBufBack).ToString() + '"';
                                }
                                break;
                            case 6: // UnicodeString
                                {
                                    Kernel32.ReadProcessMemory(Proc.Handle, tempaddress.ToIntPtr(), tempbuf.ToIntPtr(), 128, out actualread);
                                    tempbuf[127] = 0;
                                    tempbuf[126] = 0;
                                    tempbuf[125] = 0;
                                    tempbuf[124] = (Byte)'.';
                                    tempbuf[123] = 0;
                                    tempbuf[122] = (Byte)'.';
                                    tempbuf[121] = 0;
                                    tempbuf[120] = (Byte)'.';
                                    if (actualread > 1)
                                    {
                                        tempbuf[actualread - 1] = 0;
                                        tempbuf[actualread - 2] = 0;
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
                        //tempaddress doesn't seem to be an address
                        tempbuf.Zero();
                        variabletype = byteInterp.FindTypeOfData(UIntPtr.Zero, tempbuf, Proc.PointerSize);
                        if (variabletype == AVariableType.Single)
                            ts = UStringUtils.Sprintf("(float)%.4f", tempaddress.ToIntPtr().ReadFloat());
                    }
                }
                special = ts;
            }
            else
                special = "";
        }
        #endregion
        #region DecodeLastParametersToString
        public unsafe String DecodeLastParametersToString()
        {
            var byteInterp = new AByteInterpreter(SymbolHandler);
            var values = new AArrayManager<ADecodeValue>();
            values.Inc();
            values.Inc();
            var bufferBack = UBinaryUtils.NewZeroByteArray(63);
            var buffer = new UBytePtr(bufferBack);
            Byte[] readBuf;
            UIntPtr jumpaddress = UIntPtr.Zero;
            var x = 0;
            var s = "";
            var parametercount = 0;
            var sv1 = "";
            var sv2 = "";
            var i = 0;
            if (LastDisassembleData.CommentsOverride != "")
                return LastDisassembleData.CommentsOverride;
            var result = "";
            if (LastDisassembleData.IsJump)
            {
                if (LastDisassembleData.ModRmValueType == ADisassemblerValueType.Address)
                {
                    jumpaddress = LastDisassembleData.ModRmValue;
                    readBuf = Proc.Memory.Read<Byte>(jumpaddress.ToIntPtr(), Proc.PointerSize);
                    if (readBuf.Length != Proc.PointerSize)
                        return result;
                    jumpaddress = UBitConverter.ToUIntPtr(readBuf);
                }
                else
                {
                    if (LastDisassembleData.ParameterValueType == ADisassemblerValueType.None)
                        return result; //jump with no address (e.g reg)
                    jumpaddress = LastDisassembleData.ParameterValue;
                }
                //check if the bytes at jumpAddress is ff 25 (jmp [xxxxxxxx])
                if (Kernel32.ReadProcessMemory(Proc.Handle, jumpaddress.ToIntPtr(), buffer.ToIntPtr(0), 6, out x))
                {
        
                    if ((buffer[0] == 0xff) && buffer[1] == 0x25)
                    {
                        result = result + "->";  //double, so ->->
                        if (Proc.IsX64)
                            jumpaddress = jumpaddress + 6 + buffer.ReadInt32(2); //jumpaddress+6 because of relative addressing
                        else
                            jumpaddress = (UIntPtr)buffer.ReadUInt32(2);
                        //jumpaddress now contains the address of the address to jump to
                        //so, get the address it actually jumps to
                        readBuf = Proc.Memory.Read<Byte>(jumpaddress.ToIntPtr(), Proc.PointerSize);
                        if (readBuf.Length != Proc.PointerSize)
                            return result;
                        jumpaddress = UBitConverter.ToUIntPtr(readBuf);
                    }
                    s = SymbolHandler.GetNameFromAddress(jumpaddress, ShowSymbols, ShowModules, ShowSections, null, out _, 8, false);
                    if (AStringUtils.Pos(s, LastDisassembleData.Parameters) == 0)  //no need to show a comment if it's exactly the same
                        result = result + "->" + s;
                }
            }
            else
            {
                if (LastDisassembleData.ModRmValueType == ADisassemblerValueType.Address || LastDisassembleData.ParameterValueType != ADisassemblerValueType.None)
                {
                    parametercount = 0;
                    if (LastDisassembleData.ParameterValueType != ADisassemblerValueType.None)
                        parametercount += 1;
                    if (LastDisassembleData.ModRmValueType == ADisassemblerValueType.Address)
                        parametercount += 1;
                    if (LastDisassembleData.ModRmValueType == ADisassemblerValueType.Address)
                    {
                        if ((parametercount > 1) && _modRmPosition == ATmrPos.Right)
                            values[1].Value = LastDisassembleData.ModRmValue;
                        else
                            values[0].Value = LastDisassembleData.ModRmValue;
                    }
                    if (LastDisassembleData.ParameterValueType != ADisassemblerValueType.None)
                    {
                        if (parametercount > 1 && _modRmPosition != ATmrPos.Right)
                            values[1].Value = LastDisassembleData.ParameterValue;
                        else
                            values[0].Value = LastDisassembleData.ParameterValue;
                    }
                    for (i = 0; i <= parametercount - 1; i++)
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
                                    values[i].Type = byteInterp.FindTypeOfData(values[i].Value, buffer, x);
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
                            values[i].Type = byteInterp.FindTypeOfData(UIntPtr.Zero, buffer, x);
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
                            result = result + values[i].S;
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
                        if (AMathUtils.InRange(memory[0], 0x40, 0x4f))  //does it start with a rex prefix ?
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
                    if (noVexPossible == false && AMathUtils.InRange(memory[0], 0xc4, 0xc5))
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
