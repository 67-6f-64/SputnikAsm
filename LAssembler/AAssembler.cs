using System;
using System.Diagnostics;
using Sputnik.LUtils;
using SputnikAsm.LAssembler.LEnums;
using SputnikAsm.LCollections;
using SputnikAsm.LSymbolHandler;
using SputnikAsm.LUtils;
using SputnikWin;

namespace SputnikAsm.LAssembler
{
    public class AAssembler
    {
        #region Variables
        public AOpCode[] OpCodes;
        public int Parameter1, Parameter2, Parameter3;
        public int OpCodeNr;
        public AIndexArray AssemblerIndex;
        public ASymbolHandler SymHandler = new ASymbolHandler();
        public ASingleLineAssembler Assembler;
        #endregion
        #region Properties
        public Boolean Is64Bit { get; }
        public int OpCodeCount => OpCodes.Length;
        #endregion
        #region Constructor
        public AAssembler()
        {
            Is64Bit = UNativeWin.IsProcessId64Bit((IntPtr)Process.GetCurrentProcess().Id) == 1;
            OpCodes = AOpCodes.GetOpCodes();
            Parameter1 = 0;
            Parameter2 = 0;
            Parameter3 = 0;
            OpCodeNr = 0;
            AssemblerIndex = new AIndexArray(25);
            var lastEntry = 0;
            AIndex lastIndex = null;
            for (var i = 0; i < AssemblerIndex.Length; i++)
            {
                AssemblerIndex[i].StartEntry = -1;
                AssemblerIndex[i].NextEntry = -1;
                AssemblerIndex[i].SubIndex = null;
                for (var j = lastEntry; j < OpCodeCount; j++)
                {
                    if (OpCodes[j].Mnemonic[0] == 'A' + i)
                    {
                        //found the first entry with this as first character
                        if (lastIndex != null)
                            lastIndex.NextEntry = j;
                        lastIndex = AssemblerIndex[i];
                        AssemblerIndex[i].StartEntry = j;
                        AssemblerIndex[i].SubIndex = null; //default initialization
                        lastEntry = j;
                        break; // todo check if should be continue
                    }
                    if (OpCodes[j].Mnemonic[0] > 'A' + i)
                        break; // passed it // todo check if should be continue
                }
            }
            if (AssemblerIndex.Last.StartEntry != -1)
                AssemblerIndex.Last.NextEntry = OpCodeCount;
            //fill in the subindexes
            for (var i = 0; i < AssemblerIndex.Length; i++)
            {
                if (AssemblerIndex[i].StartEntry == -1)
                    continue;
                //initialize subindex
                AssemblerIndex[i].SubIndex = new AIndexArray(25);
                for (var j = 0; j < AssemblerIndex.Length; j++)
                {
                    AssemblerIndex[i].SubIndex[j].StartEntry = -1;
                    AssemblerIndex[i].SubIndex[j].NextEntry = -1;
                    AssemblerIndex[i].SubIndex[j].SubIndex = null;
                }
                lastIndex = null;
                if (AssemblerIndex[i].NextEntry == -1)  //last one in the list didn't get a assignment
                    AssemblerIndex[i].NextEntry = OpCodeCount + 1;
                for (var j = 0; j < AssemblerIndex.Length; j++)
                {
                    for (var k = AssemblerIndex[i].StartEntry; k < AssemblerIndex[i].NextEntry - 1; k++)
                    {
                        if (OpCodes[k].Mnemonic[0] == 'A' + j)
                        {
                            if (lastIndex != null)
                                lastIndex.NextEntry = k;
                            lastIndex = AssemblerIndex[i].SubIndex[j];
                            AssemblerIndex[i].SubIndex[j].StartEntry = k;
                            break; // todo check if should be continue
                        }
                    }
                }
            }
            Assembler = new ASingleLineAssembler(this);
        }
        #endregion
        #region GetOpCodesIndex
        public int GetOpCodesIndex(String opCode)
        {
            int i;
            int index1, index2;
            AIndex bestindex;
            int minindex, maxindex;
            opCode = opCode.ToUpper();
            var result = -1;
            if (opCode.Length <= 0)
                return result;
            index1 = opCode[0] - 'A';
            if ((index1 < 0) || (index1 >= 25))
                return result; //not alphabetical
            bestindex = AssemblerIndex[index1];
            if (bestindex.StartEntry == -1)
                return result;
            if ((AssemblerIndex[index1].SubIndex != null) && (opCode.Length > 1))
            {
                index2 = opCode[0] - 'A';
                if ((index2 < 0) || (index2 >= 25))
                    return result; //not alphabetical
                bestindex = AssemblerIndex[index1].SubIndex[index2];
                if (bestindex.StartEntry == -1)
                    return result; //no subitem2
            }
            minindex = bestindex.StartEntry;
            maxindex = bestindex.NextEntry;
            if (maxindex == -1)
                if (AssemblerIndex[index1].NextEntry != -1)
                    maxindex = AssemblerIndex[index1].NextEntry;
                else
                    maxindex = OpCodeCount;
            if (maxindex > OpCodeCount)
                maxindex = OpCodeCount;
            //now scan from minindex to maxindex for opcode
            for (i = minindex; i <= maxindex; i++)
            {
                if (OpCodes[i].Mnemonic == opCode)
                {
                    result = i; //found it
                    return result;
                }
                if (OpCodes[i].Mnemonic[0] != opCode[0])
                    return result;
            }
            //still here, not found, -1
            return -1;
        }
        #endregion
        #region IsMemoryLocationDefault
        public Boolean IsMemoryLocationDefault(String parameter)
        {
            return parameter.StartsWith("[") && parameter.EndsWith("]");
        }
        #endregion
        #region Add
        public void Add(AByteArray bytes, params Byte[] a)
        {
            bytes.EnsureCapacity(bytes.Length + a.Length);
            for (var i = 0; i < a.Length; i++)
                bytes[bytes.Length - a.Length + i] = a[i];
        }
        public void Add(AByteArray bytes, Byte a)
        {
            bytes.EnsureCapacity(bytes.Length + 1);
            bytes.Last = a;
        }
        #endregion
        #region AddWord
        public void AddWord(AByteArray bytes, UInt16 a)
        {
            Add(bytes, (Byte)a);
            Add(bytes, (Byte)(a >> 8));
        }
        #endregion
        #region AddDWord
        public void AddDWord(AByteArray bytes, UInt32 a)
        {
            Add(bytes, (Byte)a);
            Add(bytes, (Byte)(a >> 8));
            Add(bytes, (Byte)(a >> 16));
            Add(bytes, (Byte)(a >> 24));
        }
        #endregion
        #region AddQWord
        public void AddQWord(AByteArray bytes, UInt64 a)
        {
            Add(bytes, (Byte)a);
            Add(bytes, (Byte)(a >> 8));
            Add(bytes, (Byte)(a >> 16));
            Add(bytes, (Byte)(a >> 24));
            Add(bytes, (Byte)(a >> 32));
            Add(bytes, (Byte)(a >> 40));
            Add(bytes, (Byte)(a >> 48));
            Add(bytes, (Byte)(a >> 56));
        }
        #endregion
        #region AddString
        public void AddString(AByteArray bytes, String s)
        {
            var j = bytes.Length;
            bytes.EnsureCapacity(bytes.Length + s.Length - 2); //not the quotes;
            for (var i = 1; i < s.Length - 1; i++, j++)
                bytes[j] = (Byte)s[i];
        }
        #endregion
        #region ValueToType
        public int ValueToType(UInt32 value)
        {
            var result = 32;
            if (value <= 0xffff) 
            {
                result = 16;
                if (value >= 0x8000)
                    result = 32;
            }
            if (value <= 0xff) 
            {
                result = 8;
                if (value >= 0x80)
                    result = 16;
            }
            if (result == 32)
            {
                if ((int)(value) < 0)
                {
                    if ((int)(value) >= -128)
                        result = 8;
                    else if ((int)(value) >= -32768)
                        result = 16;
                }
            }
            return result;
        }
        #endregion
        #region SignedValueToType
        public int SignedValueToType(int value)
        {
            var result = 8;
            if ((value < -128) || (value > 127))
                result = 16;
            if ((value < -32768) || (value > 32767))
                result = 32;
            return result;
        }
        #endregion
        #region StringValueToType
        public int StringValueToType(String value)
        {
            //this function converts a string to a value type depending on how it is written
            var result = 0;
            AStringUtils.Val(value, out UInt32 x, out var err);
            if (err > 0)
                return 0;
            if (value.Length == 17)
                result = 64;
            else if (value.Length == 9)
                result = 32;
            else if (value.Length == 5)
            {
                result = 16;
                if (x > 65535)
                    result = 32;
            }
            else if (value.Length == 3)
            {
                result = 8;
                if (x > 255)
                    result = 16;
            }
            if (result == 0)
                result = ValueToType(x); //not a specific ammount of characters given
            return result;
        }
        #endregion
        #region GetReg
        public int GetReg(String reg)
        {
            return GetReg(reg, true);
        }
        public int GetReg(String reg, Boolean exceptOnError)
        {
            var result = -1;
            switch (reg)
            {
                case "RAX":
                case "EAX":
                case "AX":
                case "AL":
                case "MM0":
                case "XMM0":
                case "ST(0)":
                case "ST":
                case "ES":
                case "CR0":
                case "DR0":
                    result = 0;
                    break;
                case "RCX":
                case "ECX":
                case "CX":
                case "CL":
                case "MM1":
                case "XMM1":
                case "ST(1)":
                case "CS":
                case "CR1":
                case "DR1":
                    result = 1;
                    break;
                case "RDX":
                case "EDX":
                case "DX":
                case "DL":
                case "MM2":
                case "XMM2":
                case "ST(2)":
                case "SS":
                case "CR2":
                case "DR2":
                    result = 2;
                    break;
                case "RBX":
                case "EBX":
                case "BX":
                case "BL":
                case "MM3":
                case "XMM3":
                case "ST(3)":
                case "DS":
                case "CR3":
                case "DR3":
                    result = 3;
                    break;
                case "SPL":
                case "RSP":
                case "ESP":
                case "SP":
                case "AH":
                case "MM4":
                case "XMM4":
                case "ST(4)":
                case "FS":
                case "CR4":
                case "DR4":
                    result = 4;
                    break;
                case "BPL":
                case "RBP":
                case "EBP":
                case "BP":
                case "CH":
                case "MM5":
                case "XMM5":
                case "ST(5)":
                case "GS":
                case "CR5":
                case "DR5":
                    result = 5;
                    break;
                case "SIL":
                case "RSI":
                case "ESI":
                case "SI":
                case "DH":
                case "MM6":
                case "XMM6":
                case "ST(6)":
                case "HS":
                case "CR6":
                case "DR6":
                    result = 6;
                    break;
                case "DIL":
                case "RDI":
                case "EDI":
                case "DI":
                case "BH":
                case "MM7":
                case "XMM7":
                case "ST(7)":
                case "IS":
                case "CR7":
                case "DR7":
                    result = 7;
                    break;
                case "R8":
                    result = 8;
                    break;
                case "R9":
                    result = 9;
                    break;
                case "R10":
                    result = 10;
                    break;
                case "R11":
                    result = 11;
                    break;
                case "R12":
                    result = 12;
                    break;
                case "R13":
                    result = 13;
                    break;
                case "R14":
                    result = 14;
                    break;
                case "R15":
                    result = 15;
                    break;
            }
            if (result == -1 && exceptOnError)
                throw new Exception("Invalid register");
            return result;
        }
        #endregion
        #region TokenToRegisterBit
        public ATokenType TokenToRegisterBit(String token)
        {
            var result = ATokenType.ttregister32bit;
            switch (token)
            {
                case "AL":
                case "CL":
                case "DL":
                case "BL":
                case "AH":
                case "CH":
                case "DH":
                case "BH":
                    result = ATokenType.ttregister8bit;
                    break;
                case "AX":
                case "CX":
                case "DX":
                case "BX":
                case "SP":
                case "BP":
                case "SI":
                case "DI":
                    result = ATokenType.ttregister16bit;
                    break;
                case "EAX":
                case "ECX":
                case "EDX":
                case "EBX":
                case "ESP":
                case "EBP":
                case "ESI":
                case "EDI":
                    result = ATokenType.ttregister32bit;
                    break;
                case "MM0":
                case "MM1":
                case "MM2":
                case "MM3":
                case "MM4":
                case "MM5":
                case "MM6":
                case "MM7":
                    result = ATokenType.ttregistermm;
                    break;
                case "XMM0":
                case "XMM1":
                case "XMM2":
                case "XMM3":
                case "XMM4":
                case "XMM5":
                case "XMM6":
                case "XMM7":
                    result = ATokenType.ttregisterxmm;
                    break;
                case "ST":
                case "ST(0)":
                case "ST(1)":
                case "ST(2)":
                case "ST(3)":
                case "ST(4)":
                case "ST(5)":
                case "ST(6)":
                case "ST(7)":
                    result = ATokenType.ttregisterst;
                    break;
                case "ES":
                case "CS":
                case "SS":
                case "DS":
                case "FS":
                case "GS":
                case "HS":
                case "IS":
                    result = ATokenType.ttregistersreg;
                    break;
                case "CR0":
                case "CR1":
                case "CR2":
                case "CR3":
                case "CR4":
                case "CR5":
                case "CR6":
                case "CR7":
                    result = ATokenType.ttregistercr;
                    break;
                case "DR0":
                case "DR1":
                case "DR2":
                case "DR3":
                case "DR4":
                case "DR5":
                case "DR6":
                case "DR7":
                    result = ATokenType.ttregisterdr;
                    break;
                default:
                {
                    if (SymHandler.process.is64bit)
                    {
                        switch (token)
                        {
                            case "RAX":
                            case "RCX":
                            case "RDX":
                            case "RBX":
                            case "RSP":
                            case "RBP":
                            case "RSI":
                            case "RDI":
                            case "R8":
                            case "R9":
                            case "R10":
                            case "R11":
                            case "R12":
                            case "R13":
                            case "R14":
                            case "R15":
                                result = ATokenType.ttregister64bit;
                                break;
                            case "SPL":
                            case "BPL":
                            case "SIL":
                            case "DIL":
                                result = ATokenType.ttregister8bitwithprefix;
                                break;
                            case "R8L":
                            case "R9L":
                            case "R10L":
                            case "R11L":
                            case "R12L":
                            case "R13L":
                            case "R14L":
                            case "R15L":
                                result = ATokenType.ttregister8bit;
                                break;
                            case "R8W":
                            case "R9W":
                            case "R10W":
                            case "R11W":
                            case "R12W":
                            case "R13W":
                            case "R14W":
                            case "R15W":
                                result = ATokenType.ttregister16bit;
                                break;
                            case "R8D":
                            case "R9D":
                            case "R10D":
                            case "R11D":
                            case "R12D":
                            case "R13D":
                            case "R14D":
                            case "R15D":
                                result = ATokenType.ttregister32bit;
                                break;
                            case "XMM8":
                            case "XMM9":
                            case "XMM10":
                            case "XMM11":
                            case "XMM12":
                            case "XMM13":
                            case "XMM14":
                            case "XMM15":
                                result = ATokenType.ttregisterxmm;
                                break;
                            case "CR8":
                            case "CR9":
                            case "CR10":
                            case "CR11":
                            case "CR12":
                            case "CR13":
                            case "CR14":
                            case "CR15":
                                result = ATokenType.ttregistercr;
                                break;
                        }
                    }
                    break;
                }
            }
            return result;
        }
        #endregion
        #region IsMem8
        public Boolean IsMem8(ATokenType p)
        {
            return p == ATokenType.ttmemorylocation8 || p == ATokenType.ttregister8bit;
        }
        #endregion
        #region IsMem16
        public Boolean IsMem16(ATokenType p)
        {
            return p == ATokenType.ttmemorylocation16 || p == ATokenType.ttregister16bit;
        }
        #endregion
        #region IsMem32
        public Boolean IsMem32(ATokenType p)
        {
            return p == ATokenType.ttmemorylocation32 || p == ATokenType.ttregister32bit;
        }
        #endregion
        #region IsRmm32
        public Boolean IsRmm32(ATokenType p)
        {
            return p == ATokenType.ttregistermm || p == ATokenType.ttmemorylocation32;
        }
        #endregion
        #region IsRmm64
        public Boolean IsRmm64(ATokenType p)
        {
            return p == ATokenType.ttregistermm || p == ATokenType.ttmemorylocation64;
        }
        #endregion
        #region IsXmm32
        public Boolean IsXmm32(ATokenType p)
        {
            return p == ATokenType.ttregisterxmm || p == ATokenType.ttmemorylocation32;
        }
        #endregion
        #region IsXmm64
        public Boolean IsXmm64(ATokenType p)
        {
            return p == ATokenType.ttregisterxmm || p == ATokenType.ttmemorylocation64;
        }
        #endregion
        #region IsXmm128
        public Boolean IsXmm128(ATokenType p)
        {
            return p == ATokenType.ttregisterxmm || p == ATokenType.ttmemorylocation128;
        }
        #endregion
        #region EoToReg
        public int EoToReg(AExtraOpCode eo)
        {
            var result = -1;
            switch (eo)
            {
                case AExtraOpCode.eo_reg0:
                    result = 0;
                    break;
                case AExtraOpCode.eo_reg1:
                    result = 1;
                    break;
                case AExtraOpCode.eo_reg2:
                    result = 2;
                    break;
                case AExtraOpCode.eo_reg3:
                    result = 3;
                    break;
                case AExtraOpCode.eo_reg4:
                    result = 4;
                    break;
                case AExtraOpCode.eo_reg5:
                    result = 5;
                    break;
                case AExtraOpCode.eo_reg6:
                    result = 6;
                    break;
                case AExtraOpCode.eo_reg7:
                    result = 7;
                    break;
            }
            return result;
        }
        #endregion
        #region SetMod
        public void SetMod(AByteArray modrm, int index, Byte i)
        {
            var tmp = modrm[index];
            SetMod(ref tmp, i);
            modrm[index] = tmp;
        }
        public void SetMod(ref Byte modrm, Byte i)
        {
            modrm = (Byte)((modrm & 0x3f) | (i << 6));
        }
        #endregion
        #region GetMod
        public Byte GetMod(Byte modrm)
        {
            return (Byte)(modrm >> 6);
        }
        #endregion
        #region SetSibScale
        public void SetSibScale(AByteArray sib, int index, Byte i)
        {
            var tmp = sib[index];
            SetSibScale(ref tmp, i);
            sib[index] = tmp;
        }
        public Byte SetSibScale(ref Byte sib, Byte i)
        {
            return (Byte)((sib & 0x3f) | (i << 6));
        }
        #endregion
        #region GetTokenType
        public ATokenType GetTokenType(ref String token, String token2)
        {
            var result = ATokenType.ttinvalidtoken;
            if (token.Length == 0)
                return result;
            result = TokenToRegisterBit(token);
            //filter these 2 words
            token = UStringUtils.Replace(token, "LONG ", "", true);
            token = UStringUtils.Replace(token, "SHORT ", "", true);
            token = UStringUtils.Replace(token, "FAR ", "", true);
            var temp = AStringUtils.ConvertHexStrToRealStr(token);
            AStringUtils.Val(temp, out UInt64 _, out var err);
            if (err == 0)
            {
                result = ATokenType.ttvalue;
                token = temp;
            }
            if (AStringUtils.Pos("[", token) != -1)
            {
                if (AStringUtils.Pos("DQWORD ", token) != -1)
                    result = ATokenType.ttmemorylocation128;
                else if (AStringUtils.Pos("TBYTE ", token) != -1)
                    result = ATokenType.ttmemorylocation80;
                else if (AStringUtils.Pos("TWORD ", token) != -1)
                    result = ATokenType.ttmemorylocation80;
                else if (AStringUtils.Pos("QWORD ", token) != -1)
                    result = ATokenType.ttmemorylocation64;
                else if (AStringUtils.Pos("DWORD ", token) != -1)
                    result = ATokenType.ttmemorylocation32;
                else if (AStringUtils.Pos("WORD ", token) != -1)
                    result = ATokenType.ttmemorylocation16;
                else if (AStringUtils.Pos("BYTE ", token) != -1)
                    result = ATokenType.ttmemorylocation8;
                else
                    result = ATokenType.ttmemorylocation;
            }
            if (result == ATokenType.ttmemorylocation)
            {
                if (token2 == "")
                {
                    result = ATokenType.ttmemorylocation32;
                    return result;
                }
                //I need the helper param to figure it out
                switch (TokenToRegisterBit(token2))
                {
                    case ATokenType.ttregister8bit:
                    case ATokenType.ttregister8bitwithprefix:
                        result = ATokenType.ttmemorylocation8;
                        break;
                    case ATokenType.ttregistersreg:
                    case ATokenType.ttregister16bit:
                        result = ATokenType.ttmemorylocation16;
                        break;
                    case ATokenType.ttregister64bit:
                        result = ATokenType.ttmemorylocation64;
                        break;
                    default:
                        result = ATokenType.ttmemorylocation32;
                        break;
                }
            }
            return result;
        }
        #endregion
        #region Tokenize
        public Boolean Tokenize(String opCode, AStringArray tokens)
        {
            int i, j, last;
            Boolean quoted;
            Char quotechar;
            String t;
            Boolean ispartial;
            quotechar = '\0';
            tokens.SetLength(0);
            if (opCode.Length > 0)
                opCode = opCode.TrimEnd(' ', ',');
            last = 0;
            quoted = false;
            for (i = 0; i <= opCode.Length; i++)
            {
                //check if this is a quote char
                if (i < opCode.Length && ((opCode[i] == '\'') || (opCode[i] == '"')))
                {
                    if (quoted)  //check if it's the end quote
                    {
                        if (opCode[i] == quotechar)
                            quoted = false;
                    }
                    else
                    {
                        quoted = true;
                        quotechar = opCode[i];
                    }
                }
                //check if we encounter a token seperator. (space or , )
                //but only check when it's not inside a quoted string
                if ((i == opCode.Length) || ((!quoted) && ((opCode[i] == ' ') || (opCode[i] == ','))))
                {
                    tokens.SetLength(tokens.Length + 1);
                    if (i == opCode.Length)
                        j = i - last + 1;
                    else
                        j = i - last;
                    tokens.Last = AStringUtils.Copy(opCode, last, j);
                    if ((j > 0) && (tokens.Last[0] != '$') && ((j < 7) || (AStringUtils.Pos("KERNEL_", tokens.Last.ToUpper()) == -1)))  //only uppercase if it's not kernel_
                    {
                        //don't uppercase empty strings, kernel_ strings or strings starting with $
                        if (tokens.Last.Length > 2)
                        {
                            if (!AArrayUtils.InArray(tokens.Last[0], '\'', '"'))  //if not a quoted string then make it uppercase
                                tokens.Last = tokens.Last.ToUpper();
                        }
                        else
                            tokens.Last = tokens.Last.ToUpper();
                    }
                    //6.1: Optimized this lookup. Instead of a 18 compares a full string lookup on each token it now only compares up to 4 times
                    t = tokens.Last;
                    ispartial = false;
                    if (t.Length >= 3)  //3 characters is good enough to get the general idea, then do a string compare to verify
                    {
                        switch (t[0])
                        {
                            case 'B': //BYTE, BYTE PTR
                                {
                                    if ((t[1] == 'Y') && (t[2] == 'T'))  //could be BYTE
                                        ispartial = (t == "BYTE") || (t == "BYTE PTR");
                                }
                                break;
                            case 'D': //DQWORD, DWORD, DQWORD PTR, DWORD PTR
                                {
                                    switch (t[1])
                                    {
                                        case 'Q': //DQWORD or DQWORD PTR
                                            {
                                                if (t[2] == 'W')
                                                    ispartial = (t == "DQWORD") || (t == "DQWORD PTR");
                                            }
                                            break;

                                        case 'W': //DWORD or DWORD PTR
                                            {
                                                if (t[2] == 'O')
                                                    ispartial = (t == "DWORD") || (t == "DWORD PTR");
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 'F': //FAR
                                {
                                    if ((t[1] == 'A') && (t[2] == 'R'))
                                        ispartial = (t == "FAR");
                                }
                                break;
                            case 'L': //LONG
                                {
                                    if ((t[1] == 'O') && (t[2] == 'N'))
                                        ispartial = (t == "LONG");
                                }
                                break;
                            case 'Q': //QWORD, QWORD PTR
                                {
                                    if ((t[1] == 'W') && (t[2] == 'O'))  //could be QWORD
                                        ispartial = (t == "QWORD") || (t == "QWORD PTR");
                                }
                                break;
                            case 'S': //SHORT
                                {
                                    if ((t[1] == 'H') && (t[2] == 'O'))
                                        ispartial = (t == "SHORT");
                                }
                                break;
                            case 'T': //TBYTE, TWORD, TBYTE PTR, TWORD PTR,
                                {
                                    switch (t[1])
                                    {
                                        case 'B': //TBYTE or TBYTE PTR
                                            {
                                                if (t[2] == 'Y')
                                                    ispartial = (t == "TBYTE") || (t == "TBYTE PTR");
                                            }
                                            break;

                                        case 'W': //TWORD or TWORD PTR
                                            {
                                                if (t[2] == 'O')
                                                    ispartial = (t == "TWORD") || (t == "TWORD PTR");
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 'W': //WORD, WORD PTR
                                {
                                    if ((t[1] == 'O') && (t[3] == 'R'))  //could be WORD
                                        ispartial = (t == "WORD") || (t == "WORD PTR");
                                }
                                break;
                        }
                    }
                    if (ispartial)
                        tokens.SetLength(tokens.Length - 1);
                    else
                    {
                        last = i + 1;
                        if (tokens.Length > 1)
                        {
                            var lastElem = tokens.Last;
                            Rewrite(ref lastElem); //Rewrite
                            tokens.Last = lastElem;
                        }
                    }
                }
            }
            i = 0;
            while (i < tokens.Length)
            {
                if ((tokens[i] == "") || (tokens[i] == " ") || (tokens[i] == ","))
                {
                    for (j = i; j < tokens.Length - 1; j++)
                        tokens[j] = tokens[j + 1];
                    tokens.SetLength(tokens.Length - 1);
                    continue;
                }
                i++;
            }
            return true;
        }
        #endregion
        #region Rewrite
        public Boolean Rewrite(ref String token)
        {
            if (token.Length == 0)
                return false; //empty string
            var tokens = new AStringArray();
            var quoteChar = '\0';
            tokens.SetLength(0);
            String temp;
            /* 5.4: special pointer notation case */
            if (token.Length > 4 && token.StartsWith("[[") && token.EndsWith("]]"))
            {
                //looks like a pointer in a address specifier (idiot user detected...)
                temp = "[" + AStringUtils.IntToHex(SymHandler.GetAddressFromName(AStringUtils.Copy(token, 2, token.Length - 4), false, out var haserror), 8) + ']';
                if (!haserror)
                    token = temp;
                else
                    throw new Exception("Invalid");
            }
            /* 5.4 ^^^ */
            temp = "";
            var i = 0;
            var inquote = false;
            while (i < token.Length)
            {
                if (AArrayUtils.InArray(token[i], '\'', '"'))
                {
                    if (inquote)
                    {
                        if (token[i] == quoteChar)
                            inquote = false;
                    }
                    else
                    {
                        //start of a quote
                        quoteChar = token[i];
                        inquote = true;
                    }
                }
                if (!inquote)
                {
                    if (AArrayUtils.InArray(token[i], '[', ']', '+', '-', '*'))
                    {
                        if (temp != "")
                        {
                            tokens.SetLength(tokens.Length + 1);
                            tokens.Last = temp;
                            temp = "";
                        }
                        tokens.SetLength(tokens.Length + 1);
                        tokens[tokens.Length - 1] = token[i].ToString();
                        i++;
                        continue;
                    }
                }
                temp += token[i];
                i++;
            }
            if (temp != "")
            {
                tokens.SetLength(tokens.Length + 1);
                tokens[tokens.Length - 1] = temp;
                temp = "";
            }
            for (i = 0; i < tokens.Length; i++)
            {
                if (tokens[i].Length > 1 && !AArrayUtils.InArray(tokens[i][0], '[', ']', '+', '-', '*'))  //3/16/2011: 11:15 (replaced or with and)
                {
                    AStringUtils.Val("0x" + tokens[i], out Int64 _, out var err);
                    if ((err != 0) && (GetReg(tokens[i], false) == -1))     //not a hexadecimal value and not a register
                    {
                        temp = AStringUtils.IntToHex(SymHandler.GetAddressFromName(tokens[i], false, out var hasError), 8);
                        if (!hasError)
                            tokens[i] = temp; //can be rewritten as a hexadecimal
                        else
                        {
                            if (i < tokens.Length - 1)
                            {
                                //perhaps it can be concatenated with the next one
                                if ((tokens[i + 1].Length > 0) && (!(AArrayUtils.InArray(tokens[i + 1][0], '\'', '"', '[', ']', '(', ')'))))  //not an invalid token char
                                {
                                    tokens[i + 1] = tokens[i] + tokens[i + 1];
                                    tokens[i] = "";
                                }
                            }
                        }
                    }
                }
            }
            //do some calculations
            //check multiply first
            for (i = 1; i <= tokens.Length - 2; i++)
            {
                if (tokens[i] == "*")
                {
                    AStringUtils.Val("0x" + tokens[i - 1], out Int64 a, out var err);
                    AStringUtils.Val("0x" + tokens[i + 1], out Int64 b, out var err2);
                    if ((err == 0) && (err2 == 0))
                    {
                        a *= b;
                        tokens[i - 1] = AStringUtils.IntToHex(a, 8);
                        tokens.Remove(i, 2);
                        i -= 2;
                    }
                }
            }
            for (i = 1; i <= tokens.Length - 2; i++)
            {
                //get the value of the token before and after this token
                AStringUtils.Val("0x" + tokens[i - 1], out Int64 a, out var err);
                AStringUtils.Val("0x" + tokens[i + 1], out Int64 b, out var err2);
                //if no error, check if this token is a mathemetical value
                if ((err == 0) && (err2 == 0))
                {
                    switch (tokens[i][0])
                    {
                        case '+':
                            {
                                a += b;
                                tokens[i - 1] = AStringUtils.IntToHex(a, 8);
                                tokens.Remove(i, 2);
                                i -= 2;
                            }
                            break;

                        case '-':
                            {
                                a -= b;
                                tokens[i - 1] = AStringUtils.IntToHex(a, 8);
                                tokens.Remove(i, 2);
                                i -= 2;
                            }
                            break;
                    }
                }
                else
                {
                    if ((err2 == 0) && (tokens[i] != "") && (tokens[i][0] == '-') && (tokens[i - 1] != "#"))  //before is not a valid value, but after it is. and this is a -  (so -value) (don't mess with #-10000)
                    {
                        tokens[i] = "+";
                        tokens[i + 1] = AStringUtils.IntToHex(-b, 8);
                    }
                }
            }
            token = "";
            for (i = 0; i <= tokens.Length - 1; i++)
                token += tokens[i];
            tokens.SetLength(0);
            return true;
        }
        #endregion
        #region Assemble
        public Boolean Assemble(String opCode, UInt64 address, AByteArray bytes, AAssemblerPreference aPref = AAssemblerPreference.apnone, Boolean skipRangeCheck = false)
        {
            var result = Assembler.Assemble(opCode, address, bytes, aPref, skipRangeCheck);
            return result;
        }
        #endregion
    }
}
