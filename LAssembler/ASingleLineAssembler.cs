using System;
using Sputnik.LBinary;
using Sputnik.LGenerics;
using Sputnik.LUtils;
using SputnikAsm.LAssembler.LEnums;
using SputnikAsm.LBinary;
using SputnikAsm.LCollections;
using SputnikAsm.LUtils;

namespace SputnikAsm.LAssembler
{
    public class ASingleLineAssembler
    {
        #region Variables
        public AAssembler Assembler;
        public Byte RexPrefix;
        public int RexPrefixLocation;
        public int RelativeAddressLocation;
        public UInt64 ActualDisplacement;
        public Boolean NeedsAddressSwitchPrefix;
        public UInt64 FAddress;
        #endregion
        #region Properties
        public Boolean RexW
        {
            get => ((RexPrefix >> 3) & 1) == 1;
            set
            {
                if (value)
                    RexPrefix = (Byte)((RexPrefix & 0xf7) | 8);
                else
                    RexPrefix = (Byte)(RexPrefix & 0xf7);
            }
        }
        public Boolean RexR
        {
            get => ((RexPrefix >> 2) & 1) == 1;
            set
            {
                if (value)
                    RexPrefix = (Byte)((RexPrefix & 0xfb) | 4);
                else
                    RexPrefix = (Byte)(RexPrefix & 0xfb);
            }
        }
        public Boolean RexX
        {
            get => ((RexPrefix >> 1) & 1) == 1;
            set
            {
                if (value)
                    RexPrefix = (Byte)((RexPrefix & 0xfd) | 2);
                else
                    RexPrefix = (Byte)(RexPrefix & 0xfd);
            }
        }
        public Boolean RexB
        {
            get => (RexPrefix & 1) == 1;
            set
            {
                if (value)
                    RexPrefix = (Byte)((RexPrefix & 0xfe) | 1);
                else
                    RexPrefix = (Byte)(RexPrefix & 0xfe);
            }
        }
        #endregion
        #region Constructor
        public ASingleLineAssembler(AAssembler assembler)
        {
            Assembler = assembler;
            Reset();
        }
        #endregion
        #region Reset
        public void Reset()
        {
            RexPrefix = 0;
            RexPrefixLocation = 0;
            RelativeAddressLocation = 0;
            ActualDisplacement = 0;
            NeedsAddressSwitchPrefix = false;
            FAddress = 0;
        }
        #endregion
        #region GetReg
        public int GetReg(String reg)
        {
            return GetReg(reg, true);
        }
        public int GetReg(String reg, Boolean exceptOnError)
        {
            var result = 1000;
            switch (reg)
            {
                case "RAX":
                case "EAX":
                case "AX":
                case "AL":
                case "MM0":
                case "XMM0":
                case "YMM0":
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
                case "YMM1":
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
                case "YMM2":
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
                case "YMM3":
                case "ST(3)":
                case "DS":
                case "CR3":
                case "DR3":
                    result = 3;
                    break;
                case "RSP":
                case "ESP":
                case "SP":
                case "AH":
                case "MM4":
                case "XMM4":
                case "YMM4":
                case "ST(4)":
                case "FS":
                case "CR4":
                case "DR4":
                    result = 4;
                    break;
                case "RBP":
                case "EBP":
                case "BP":
                case "CH":
                case "MM5":
                case "XMM5":
                case "YMM5":
                case "ST(5)":
                case "GS":
                case "CR5":
                case "DR5":
                    result = 5;
                    break;
                case "RSI":
                case "ESI":
                case "SI":
                case "DH":
                case "MM6":
                case "XMM6":
                case "YMM6":
                case "ST(6)":
                case "HS":
                case "CR6":
                case "DR6":
                    result = 6;
                    break;
                case "RDI":
                case "EDI":
                case "DI":
                case "BH":
                case "MM7":
                case "XMM7":
                case "YMM7":
                case "ST(7)":
                case "IS":
                case "CR7":
                case "DR7":
                    result = 7;
                    break;
            }
            if (Assembler.SymbolHandler.Process.IsX64)
            {
                switch (reg)
                {
                    case "SPL":
                        result = 4;
                        break;
                    case "BPL":
                        result = 5;
                        break;
                    case "SIL":
                        result = 6;
                        break;
                    case "DIL":
                        result = 7;
                        break;
                    case "R8":
                    case "R8D":
                    case "R8W":
                    case "R8L":
                    case "MM8":
                    case "XMM8":
                    case "YMM8":
                    case "ST(8)":
                    case "JS":
                    case "CR8":
                    case "DR8":
                        result = 8;
                        break;
                    case "R9":
                    case "R9D":
                    case "R9W":
                    case "R9L":
                    case "MM9":
                    case "XMM9":
                    case "YMM9":
                    case "ST(9)":
                    case "KS":
                    case "CR9":
                    case "DR9":
                        result = 9;
                        break;
                }
                switch (reg)
                {
                    case "R10":
                    case "R10D":
                    case "R10W":
                    case "R10L":
                    case "MM10":
                    case "XMM10":
                    case "YMM10":
                    case "ST(10)":
                    case "KS":
                    case "CR10":
                    case "DR10":
                        result = 10;
                        break;
                    case "R11":
                    case "R11D":
                    case "R11W":
                    case "R11L":
                    case "MM11":
                    case "XMM11":
                    case "YMM11":
                    case "ST(11)":
                    case "LS":
                    case "CR11":
                    case "DR11":
                        result = 11;
                        break;
                    case "R12":
                    case "R12D":
                    case "R12W":
                    case "R12L":
                    case "MM12":
                    case "XMM12":
                    case "YMM12":
                    case "ST(12)":
                    case "MS":
                    case "CR12":
                    case "DR12":
                        result = 12;
                        break;
                    case "R13":
                    case "R13D":
                    case "R13W":
                    case "R13L":
                    case "MM13":
                    case "XMM13":
                    case "YMM13":
                    case "ST(13)":
                    case "NS":
                    case "CR13":
                    case "DR13":
                        result = 13;
                        break;
                    case "R14":
                    case "R14D":
                    case "R14W":
                    case "R14L":
                    case "MM14":
                    case "XMM14":
                    case "YMM14":
                    case "ST(14)":
                    case "OS":
                    case "CR14":
                    case "DR14":
                        result = 14;
                        break;
                    case "R15":
                    case "R15D":
                    case "R15W":
                    case "R15L":
                    case "MM15":
                    case "XMM15":
                    case "YMM15":
                    case "ST(15)":
                    case "PS":
                    case "CR15":
                    case "DR15":
                        result = 15;
                        break;
                }
            }
            if (result == 1000 && exceptOnError)
                throw new Exception("Invalid register");
            return result;
        }
        #endregion
        #region CreateSibScaleIndex
        public void CreateSibScaleIndex(AByteArray sib, int index, String reg)
        {
            var tmp = sib[index];
            CreateSibScaleIndex(ref tmp, reg);
            sib[index] = tmp;
        }
        public void CreateSibScaleIndex(ref Byte sib, String reg)
        {
            var hasMultiply = false;
            for (var i = 0; i < reg.Length - 1; i++)
            {
                if (reg[i] != '*')
                    continue;
                hasMultiply = true;
                switch (reg[i + 1])
                {
                    case '1':
                        Assembler.SetSibScale(ref sib, 0);
                        break;
                    case '2':
                        Assembler.SetSibScale(ref sib, 1);
                        break; //*2
                    case '4':
                        Assembler.SetSibScale(ref sib, 2);
                        break; //*4
                    case '8':
                        Assembler.SetSibScale(ref sib, 3);
                        break; //*8
                    default:
                        throw new Exception("Invalid multiplier");
                }
                if (reg.Length > i + 2)
                    throw new Exception("Invalid multiplier");
                break;
            }
            if (!hasMultiply)
                Assembler.SetSibScale(ref sib, 0);
            if (Assembler.SymbolHandler.Process.IsX64)
            {
                if (AStringUtils.Pos("RAX", reg) != -1)
                    SetSibIndex(ref sib, 0);
                else if (AStringUtils.Pos("RCX", reg) != -1)
                    SetSibIndex(ref sib, 1);
                else if (AStringUtils.Pos("RDX", reg) != -1)
                    SetSibIndex(ref sib, 2);
                else if (AStringUtils.Pos("RBX", reg) != -1)
                    SetSibIndex(ref sib, 3);
                else if ((reg == "") || (AStringUtils.Pos("RSP", reg) != -1))
                    SetSibIndex(ref sib, 4);
                else if (AStringUtils.Pos("RBP", reg) != -1)
                    SetSibIndex(ref sib, 5);
                else if (AStringUtils.Pos("RSI", reg) != -1)
                    SetSibIndex(ref sib, 6);
                else if (AStringUtils.Pos("RDI", reg) != -1)
                    SetSibIndex(ref sib, 7);
                else if (AStringUtils.Pos("R8", reg) != -1)
                    SetSibIndex(ref sib, 8);
                else if (AStringUtils.Pos("R9", reg) != -1)
                    SetSibIndex(ref sib, 9);
                else if (AStringUtils.Pos("R10", reg) != -1)
                    SetSibIndex(ref sib, 10);
                else if (AStringUtils.Pos("R11", reg) != -1)
                    SetSibIndex(ref sib, 11);
                else if (AStringUtils.Pos("R12", reg) != -1)
                    SetSibIndex(ref sib, 12);
                else if (AStringUtils.Pos("R13", reg) != -1)
                    SetSibIndex(ref sib, 13);
                else if (AStringUtils.Pos("R14", reg) != -1)
                    SetSibIndex(ref sib, 14);
                else if (AStringUtils.Pos("R15", reg) != -1)
                    SetSibIndex(ref sib, 15);
                else
                {
                    // in case address which is needed
                    if (AStringUtils.Pos("EAX", reg) != -1)
                        SetSibIndex(ref sib, 0);
                    else if (AStringUtils.Pos("ECX", reg) != -1)
                        SetSibIndex(ref sib, 1);
                    else if (AStringUtils.Pos("EDX", reg) != -1)
                        SetSibIndex(ref sib, 2);
                    else if (AStringUtils.Pos("EBX", reg) != -1)
                        SetSibIndex(ref sib, 3);
                    else if (AStringUtils.Pos("ESP", reg) != -1)
                        SetSibIndex(ref sib, 4);
                    else if (AStringUtils.Pos("EBP", reg) != -1)
                        SetSibIndex(ref sib, 5);
                    else if (AStringUtils.Pos("ESI", reg) != -1)
                        SetSibIndex(ref sib, 6);
                    else if (AStringUtils.Pos("EDI", reg) != -1)
                        SetSibIndex(ref sib, 7);
                    else
                        throw new Exception("WTF is a " + reg);
                    //still here, so I guess so
                    NeedsAddressSwitchPrefix = true;
                }
            }
            else
            {
                if (AStringUtils.Pos("EAX", reg) != -1)
                    SetSibIndex(ref sib, 0);
                else if (AStringUtils.Pos("ECX", reg) != -1)
                    SetSibIndex(ref sib, 1);
                else if (AStringUtils.Pos("EDX", reg) != -1)
                    SetSibIndex(ref sib, 2);
                else if (AStringUtils.Pos("EBX", reg) != -1)
                    SetSibIndex(ref sib, 3);
                else if (reg == "" || AStringUtils.Pos("ESP", reg) != -1)
                    SetSibIndex(ref sib, 4);
                else if (AStringUtils.Pos("EBP", reg) != -1)
                    SetSibIndex(ref sib, 5);
                else if (AStringUtils.Pos("ESI", reg) != -1)
                    SetSibIndex(ref sib, 6);
                else if (AStringUtils.Pos("EDI", reg) != -1)
                    SetSibIndex(ref sib, 7);
                else
                    throw new Exception("WTF is a " + reg);
            }
        }
        #endregion
        #region SetSibIndex
        public void SetSibIndex(AByteArray sib, int index, Byte i)
        {
            var tmp = sib[index];
            SetSibIndex(ref tmp, i);
            sib[index] = tmp;
        }
        public void SetSibIndex(ref Byte sib, Byte i)
        {
            sib = (Byte)((sib & 0xc7) | ((i & 7) << 3));
            if (i > 7)
                RexX = true;
        }
        #endregion
        #region SetSibBase
        public void SetSibBase(AByteArray sib, int index, Byte i)
        {
            var tmp = sib[index];
            SetSibBase(ref tmp, i);
            sib[index] = tmp;
        }
        public void SetSibBase(ref Byte sib, Byte i)
        {
            sib = (Byte)((sib & 0xf8) | (i & 7));
            if (i > 7)
                RexB = true;
        }
        #endregion
        #region SetRm
        public void SetRm(AByteArray sib, int index, Byte i)
        {
            var tmp = sib[index];
            SetRm(ref tmp, i);
            sib[index] = tmp;
        }
        public void SetRm(ref Byte modRm, byte i)
        {
            modRm = (Byte)((modRm & 0xf8) | (i & 7));
            if (i > 7)
                RexB = true;
        }
        #endregion
        #region CreateModRm
        public Boolean CreateModRm(AByteArray bytes, int reg, String param)
        {
            String address;
            var modRm = new AByteArray();
            int i;
            modRm.EnsureCapacity(1);
            modRm[0] = 0;
            var bracketStart = AStringUtils.Pos("[", param);
            var bracketEnd = AStringUtils.Pos("]", param);
            if (bracketStart != -1 && bracketEnd != -1)
                address = AStringUtils.Copy(param, bracketStart + 1, bracketEnd - bracketStart - 1);
            else
                address = "";
            if (address == "")
            {
                // register
                // modRm c0 to ff
                Assembler.SetMod(modRm, 0, 3);
                if (param == "RAX" || param == "EAX" || param == "AX" || param == "AL" || param == "MM0" || param == "XMM0" || param == "YMM0")
                    SetRm(modRm, 0, 0);
                else if (param == "RCX" || param == "ECX" || param == "CX" || param == "CL" || param == "MM1" || param == "XMM1" || param == "YMM1")
                    SetRm(modRm, 0, 1);
                else if (param == "RDX" || param == "EDX" || param == "DX" || param == "DL" || param == "MM2" || param == "XMM2" || param == "YMM2")
                    SetRm(modRm, 0, 2);
                else if (param == "RBX" || param == "EBX" || param == "BX" || param == "BL" || param == "MM3" || param == "XMM3" || param == "YMM3")
                    SetRm(modRm, 0, 3);
                else if (param == "SPL" || param == "RSP" || param == "ESP" || param == "SP" || param == "AH" || param == "MM4" || param == "XMM4" || param == "YMM4")
                    SetRm(modRm, 0, 4);
                else if (param == "BPL" || param == "RBP" || param == "EBP" || param == "BP" || param == "CH" || param == "MM5" || param == "XMM5" || param == "YMM5")
                    SetRm(modRm, 0, 5);
                else if (param == "SIL" || param == "RSI" || param == "ESI" || param == "SI" || param == "DH" || param == "MM6" || param == "XMM6" || param == "YMM6")
                    SetRm(modRm, 0, 6);
                else if (param == "DIL" || param == "RDI" || param == "EDI" || param == "DI" || param == "BH" || param == "MM7" || param == "XMM7" || param == "YMM7")
                    SetRm(modRm, 0, 7);
                else if (param == "R8" || param == "R8D" || param == "R8W" || param == "R8L" || param == "MM8" || param == "XMM8" || param == "YMM8")
                    SetRm(modRm, 0, 8);
                else if (param == "R9" || param == "R9D" || param == "R9W" || param == "R9L" || param == "MM9" || param == "XMM9" || param == "YMM9")
                    SetRm(modRm, 0, 9);
                else if (param == "R10" || param == "R10D" || param == "R10W" || param == "R10L" || param == "MM10" || param == "XMM10" || param == "YMM10")
                    SetRm(modRm, 0, 10);
                else if (param == "R11" || param == "R11D" || param == "R11W" || param == "R11L" || param == "MM11" || param == "XMM11" || param == "YMM11")
                    SetRm(modRm, 0, 11);
                else if (param == "R12" || param == "R12D" || param == "R12W" || param == "R12L" || param == "MM12" || param == "XMM12" || param == "YMM12")
                    SetRm(modRm, 0, 12);
                else if (param == "R13" || param == "R13D" || param == "R13W" || param == "R13L" || param == "MM13" || param == "XMM13" || param == "YMM13")
                    SetRm(modRm, 0, 13);
                else if (param == "R14" || param == "R14D" || param == "R14W" || param == "R14L" || param == "MM14" || param == "XMM14" || param == "YMM14")
                    SetRm(modRm, 0, 14);
                else if (param == "R15" || param == "R15D" || param == "R15W" || param == "R15L" || param == "MM15" || param == "XMM15" || param == "YMM15")
                    SetRm(modRm, 0, 15);
                else
                    throw new Exception("I don't understand what you mean with " + param);
            }
            else
                SetModRm(modRm, address, bytes.Length);
            // set reg
            if (reg > 7)
            {
                if (Assembler.SymbolHandler.Process.IsX64)
                    RexR = true;
                else
                    throw new Exception("The assembler tried to set a register value that is too high");
            }
            if (reg == -1)
                reg = 0;
            reg &= 7;
            modRm[0] = (Byte)(modRm[0] + (reg << 3));
            var j = bytes.Length;
            bytes.EnsureCapacity(bytes.Length + modRm.Length);
            for (i = 0; i < modRm.Length; i++)
                bytes[j + i] = modRm[i];
            return true;
        }
        #endregion
        #region AddOpCode
        public void AddOpCode(AByteArray bytes, int i)
        {
            RexPrefixLocation = bytes.Length;
            if (AArrayUtils.InArray(Assembler.OpCodes[i].Bt1, 0x66, 0xf2, 0xf3))
                RexPrefixLocation += 1; //mandatory prefixes come before the rex byte
            Assembler.Add(bytes, Assembler.OpCodes[i].Bt1);
            if (Assembler.OpCodes[i].Bytes > 1)
                Assembler.Add(bytes, Assembler.OpCodes[i].Bt2);
            if (Assembler.OpCodes[i].Bytes > 2)
                Assembler.Add(bytes, Assembler.OpCodes[i].Bt3);
            if (Assembler.OpCodes[i].Bytes > 3)
                Assembler.Add(bytes, Assembler.OpCodes[i].Bt4);
        }
        #endregion
        #region SetModRm
        public void SetModRm(AByteArray modRm, String address, int offset)
        {
            var reg = new USafeDictionary<int, String>();
            var splitUp = new AStringArray();
            var temp = "";
            var i = 0;
            var j = 0;
            var k = 0;
            //first split the address string up
            splitUp.SetLength(0);
            var found = false;
            var go = false;
            var start = 0;
            for (i = 0; i < address.Length; i++)
            {
                if (i == address.Length - 1)
                {
                    splitUp.Inc();
                    splitUp.Last = AStringUtils.Copy(address, start, (i + 1) - start);
                }
                else if (!AArrayUtils.InArray(address[i], '+', '-'))
                    go = true;
                else if (AArrayUtils.InArray(address[i], '+', '-'))
                {
                    if (go)
                    {
                        splitUp.Inc();
                        splitUp.Last = AStringUtils.Copy(address, start, i - start);
                        start = i; //copy the + or - sign
                        go = false;
                    }
                }
            }
            var disp = 0UL;
            var regs = "";
            for (i = 0; i < splitUp.Length; i++)
            {
                var increase = true;
                for (j = 0; j < splitUp[i].Length; j++)
                {
                    if (j < splitUp[i].Length && AArrayUtils.InArray(splitUp[i][j], '+', '-'))
                    {
                        if (splitUp[i][j] == '-')
                            increase = !increase;
                    }
                    else
                    {
                        if (j == splitUp[i].Length)
                            temp = AStringUtils.Copy(splitUp[i], j, splitUp[i].Length - j + 1);
                        else
                            temp = AStringUtils.Copy(splitUp[i], j, splitUp[i].Length - j + 1);
                        break;
                    }
                }
                if (temp.Length == 0)
                    throw new Exception("I don't understand what you mean with " + address);
                var test = 0UL;
                if (temp[0] == '$')
                    AStringUtils.Val(temp, out test, out j);
                else
                    AStringUtils.Val("0x" + temp, out test, out j);
                if (j > 0) //a register or a stupid user
                {
                    if (!increase)
                        throw new Exception("Negative registers can not be encoded");
                    regs += temp + '+';
                }
                else
                {  
                    //a value
                    if (increase)
                        disp += test;
                    else
                        disp -= test;
                }
            }
            if (regs.Length > 0)
                regs = AStringUtils.Copy(regs, 0, regs.Length - 1);
            // regs and disp are now set
            // compare the regs with posibilities     (only 1 time +, and only 1 time *)
            j = 0;
            k = 0;
            for (i = 0; i < regs.Length; i++)
            {
                if (regs[i] == '+')
                    j += 1;
                if (regs[i] == '*')
                    k += 1;
            }
            if (j > 1 || k > 1)
                throw new Exception("I don't understand what you mean with " + address);
            if (disp == 0)
                Assembler.SetMod(modRm, 0, 0);
            else if ((int)disp >= -128 && (int)disp <= 127)
                Assembler.SetMod(modRm, 0, 1);
            else
                Assembler.SetMod(modRm, 0, 2);
            String reg1;
            var reg2 = "";
            if (AStringUtils.Pos("+", regs) != -1)
            {
                reg1 = AStringUtils.Copy(regs, 0, AStringUtils.Pos("+", regs));
                reg2 = AStringUtils.Copy(regs, AStringUtils.Pos("+", regs) + 1, regs.Length);
                k = 2;
            }
            else
            {
                reg1 = regs;
                k = 1;
            }
            reg[-1] = reg1;
            reg[1] = reg2;
            k = 1;
            if (reg1 != "" && reg2 == "" && AStringUtils.Pos("*", reg1) != -1)
            {
                Assembler.SetMod(modRm, 0, 0);
                SetRm(modRm, 0, 4);
                modRm.SetLength(2);
                SetSibBase(modRm, 1, 5);
                CreateSibScaleIndex(modRm, 1, reg[-1]);
                Assembler.AddDWord(modRm, (UInt32)disp);
                found = true;
            }
            if (reg[k] == "" && reg[-k] == "")
            {
                //no registers, just a address
                SetRm(modRm, 0, 5);
                Assembler.SetMod(modRm, 0, 0);
                if (Assembler.SymbolHandler.Process.IsX64)
                {
                    if ((disp <= 0x7fffffff) && Math.Abs((Int64)(FAddress - disp)) > 0x7ffffff0)  //rough estimate
                    {
                        //this can be solved with an 0x25 SIB byte
                        modRm.SetLength(2);
                        SetRm(modRm, 0, 4);
                        SetSibBase(modRm, 1, 5); //no base
                        SetSibIndex(modRm, 1, 4);
                        Assembler.SetSibScale(modRm, 1, 0);
                    }
                    else
                    {
                        ActualDisplacement = disp;
                        RelativeAddressLocation = offset + 1;
                    }
                }
                Assembler.AddDWord(modRm, (UInt32)disp);
                found = true;
            }
            try
            {
                if ((reg[k] == "ESP") || (reg[-k] == "ESP") || (reg[k] == "RSP") || (reg[-k] == "RSP"))  //esp takes precedence
                {
                    if (reg[-k] == "ESP")
                        k = -k;
                    if (reg[-k] == "RSP")
                        k = -k;
                    SetRm(modRm, 0, 4);
                    modRm.SetLength(2);
                    SetSibBase(modRm, 1, 4);
                    CreateSibScaleIndex(modRm, 1, reg[-k]);
                    found = true;
                    return;
                }
                if ((reg[k] == "EAX") || (reg[-k] == "EAX") || (reg[k] == "RAX") || (reg[-k] == "RAX"))
                {
                    if (reg[-k] == "EAX")
                        k = Math.Abs(k);
                    if (reg[-k] == "RAX")
                        k = Math.Abs(k);
                    if (reg[-k] != "")  //sib needed
                    {
                        SetRm(modRm, 0, 4);
                        modRm.SetLength(2);
                        SetSibBase(modRm, 1, 0);
                        CreateSibScaleIndex(modRm, 1, reg[-k]);
                    }
                    else
                        SetRm(modRm, 0, 0); //no sib needed
                    found = true;
                    return;
                }
                if ((reg[k] == "ECX") || (reg[-k] == "ECX") || (reg[k] == "RCX") || (reg[-k] == "RCX"))
                {
                    if (reg[-k] == "ECX")
                        k = -k;
                    if (reg[-k] == "RCX")
                        k = -k;
                    if (reg[-k] != "")  //sib needed
                    {
                        SetRm(modRm, 0, 4);
                        modRm.SetLength(2);
                        SetSibBase(modRm, 1, 1);
                        CreateSibScaleIndex(modRm, 1, reg[-k]);
                    }
                    else
                        SetRm(modRm, 0, 1); //no sib needed
                    found = true;
                    return;
                }
                if ((reg[k] == "EDX") || (reg[-k] == "EDX") || (reg[k] == "RDX") || (reg[-k] == "RDX"))
                {
                    if (reg[-k] == "EDX")
                        k = -k;
                    if (reg[-k] == "RDX")
                        k = -k;
                    if (reg[-k] != "")  //sib needed
                    {
                        SetRm(modRm, 0, 4);
                        modRm.SetLength(2);
                        SetSibBase(modRm, 1, 2);
                        CreateSibScaleIndex(modRm, 1, reg[-k]);
                    }
                    else
                        SetRm(modRm, 0, 2); //no sib needed
                    found = true;
                    return;
                }
                if ((reg[k] == "EBX") || (reg[-k] == "EBX") || (reg[k] == "RBX") || (reg[-k] == "RBX"))
                {
                    if (reg[-k] == "EBX") k = -k;
                    if (reg[-k] == "RBX") k = -k;

                    if (reg[-k] != "")  //sib needed
                    {
                        SetRm(modRm, 0, 4);
                        modRm.SetLength(2);
                        SetSibBase(modRm, 1, 3);
                        CreateSibScaleIndex(modRm, 1, reg[-k]);
                    }
                    else
                        SetRm(modRm, 0, 3); //no sib needed
                    found = true;
                    return;
                }
                if ((reg[k] == "ESP") || (reg[-k] == "ESP") || (reg[k] == "RSP") || (reg[-k] == "RSP"))
                {
                    if (reg[-k] == "ESP")
                        k = -k;
                    if (reg[-k] == "RSP")
                        k = -k;
                    SetRm(modRm, 0, 4);
                    modRm.SetLength(2);
                    SetSibBase(modRm, 1, 4);
                    CreateSibScaleIndex(modRm, 1, reg[-k]);
                    found = true;
                    return;
                }
                if ((reg[k] == "EBP") || (reg[-k] == "EBP") || (reg[k] == "RBP") || (reg[-k] == "RBP"))
                {
                    if (reg[-k] == "EBP")
                        k = -k;
                    if (reg[-k] == "RBP")
                        k = -k;
                    if (disp == 0)
                        Assembler.SetMod(modRm, 0, 1);
                    if (reg[-k] != "")  //sib needed
                    {
                        SetRm(modRm, 0, 4);
                        modRm.SetLength(2);
                        SetSibBase(modRm, 1, 5);
                        CreateSibScaleIndex(modRm, 1, reg[-k]);
                    }
                    else
                        SetRm(modRm, 0, 5); //no sib needed
                    found = true;
                    return;
                }
                if ((reg[k] == "ESI") || (reg[-k] == "ESI") || (reg[k] == "RSI") || (reg[-k] == "RSI"))
                {
                    if (reg[-k] == "ESI")
                        k = -k;
                    if (reg[-k] == "RSI")
                        k = -k;
                    if (reg[-k] != "")  //sib needed
                    {
                        SetRm(modRm, 0, 4);
                        modRm.SetLength(2);
                        SetSibBase(modRm, 1, 6);
                        CreateSibScaleIndex(modRm, 1, reg[-k]);
                    }
                    else
                        SetRm(modRm, 0, 6); //no sib needed
                    found = true;
                    return;
                }
                if ((reg[k] == "EDI") || (reg[-k] == "EDI") || (reg[k] == "RDI") || (reg[-k] == "RDI"))
                {
                    if (reg[-k] == "EDI") k = -k;
                    if (reg[-k] == "RDI") k = -k;

                    if (reg[-k] != "")  //sib needed
                    {
                        SetRm(modRm, 0, 4);
                        modRm.SetLength(2);
                        SetSibBase(modRm, 1, 7);
                        CreateSibScaleIndex(modRm, 1, reg[-k]);
                    }
                    else
                        SetRm(modRm, 0, 7); //no sib needed
                    found = true;
                    return;
                }
                if (Assembler.SymbolHandler.Process.IsX64)
                {
                    if ((reg[k] == "R8") || (reg[-k] == "R8"))
                    {
                        if (reg[-k] == "R8")
                            k = -k;
                        if (reg[-k] != "")  //sib needed
                        {
                            SetRm(modRm, 0, 4);
                            modRm.SetLength(2);
                            SetSibBase(modRm, 1, 8);
                            CreateSibScaleIndex(modRm, 1, reg[-k]);
                        }
                        else
                            SetRm(modRm, 0, 8); //no sib needed
                        found = true;
                        return;
                    }
                    if ((reg[k] == "R9") || (reg[-k] == "R9"))
                    {
                        if (reg[-k] == "R9")
                            k = -k;
                        if (reg[-k] != "")  //sib needed
                        {
                            SetRm(modRm, 0, 4);
                            modRm.SetLength(2);
                            SetSibBase(modRm, 1, 9);
                            CreateSibScaleIndex(modRm, 1, reg[-k]);
                        }
                        else
                            SetRm(modRm, 0, 9); //no sib needed
                        found = true;
                        return;
                    }
                    if ((reg[k] == "R10") || (reg[-k] == "R10"))
                    {
                        if (reg[-k] == "R10")
                            k = -k;
                        if (reg[-k] != "")  //sib needed
                        {
                            SetRm(modRm, 0, 4);
                            modRm.SetLength(2);
                            SetSibBase(modRm, 1, 10);
                            CreateSibScaleIndex(modRm, 1, reg[-k]);
                        }
                        else
                            SetRm(modRm, 0, 10); //no sib needed
                        found = true;
                        return;
                    }
                    if ((reg[k] == "R11") || (reg[-k] == "R11"))
                    {
                        if (reg[-k] == "R11")
                            k = -k;
                        if (reg[-k] != "")  //sib needed
                        {
                            SetRm(modRm, 0, 4);
                            modRm.SetLength(2);
                            SetSibBase(modRm, 1, 11);
                            CreateSibScaleIndex(modRm, 1, reg[-k]);
                        }
                        else
                            SetRm(modRm, 0, 11); //no sib needed
                        found = true;
                        return;
                    }
                    if ((reg[k] == "R12") || (reg[-k] == "R12"))
                    {
                        if (reg[-k] == "R12")
                            k = -k;
                        SetRm(modRm, 0, 4);
                        modRm.SetLength(2);
                        SetSibBase(modRm, 1, 12);
                        CreateSibScaleIndex(modRm, 1, reg[-k]);
                        found = true;
                        return;
                    }
                    if ((reg[k] == "R13") || (reg[-k] == "R13"))
                    {
                        if (reg[-k] == "R13")
                            k = -k;
                        if (disp == 0)
                            Assembler.SetMod(modRm, 0, 1);
                        if (reg[-k] != "")  //sib needed
                        {
                            SetRm(modRm, 0, 4);
                            modRm.SetLength(2);
                            SetSibBase(modRm, 1, 13);
                            CreateSibScaleIndex(modRm, 1, reg[-k]);
                        }
                        else
                            SetRm(modRm, 0, 13); //no sib needed
                        found = true;
                        return;
                    }
                    if ((reg[k] == "R14") || (reg[-k] == "R14"))
                    {
                        if (reg[-k] == "R14")
                            k = -k;
                        if (reg[-k] != "")  //sib needed
                        {
                            SetRm(modRm, 0, 4);
                            modRm.SetLength(2);
                            SetSibBase(modRm, 1, 14);
                            CreateSibScaleIndex(modRm, 1, reg[-k]);
                        }
                        else
                            SetRm(modRm, 0, 14); //no sib needed
                        found = true;
                        return;
                    }
                    if ((reg[k] == "R15") || (reg[-k] == "R15"))
                    {
                        if (reg[-k] == "R15")
                            k = -k;
                        if (reg[-k] != "")  //sib needed
                        {
                            SetRm(modRm, 0, 4);
                            modRm.SetLength(2);
                            SetSibBase(modRm, 1, 15);
                            CreateSibScaleIndex(modRm, 1, reg[-k]);
                        }
                        else
                            SetRm(modRm, 0, 15); //no sib needed
                        found = true;
                        return;
                    }
                }
            }
            finally
            {
                if (!found)
                    throw new Exception("Invalid address");
                i = Assembler.GetMod(modRm[0]);
                if (i == 1)
                    Assembler.Add(modRm, (Byte) disp);
                if (i == 2)
                    Assembler.AddDWord(modRm, (UInt32) disp);
            }
        }
        #endregion
        #region Invalid64BitValueFor32BitField -- todo fill this in!
        public void Invalid64BitValueFor32BitField(UInt64 v)
        {
            //qword newv;
            //
            //if ((((cardinal)v >> 32) == 0) && ((((cardinal)v >> 31) & 1) == 1))  //could be saved
            //{
            //    newv = qword($ffffffff00000000) | v;
            //    if ((naggedtheuseraboutwrongsignedvalue == false) && (getcurrentthreadid == mainthreadid))
            //    {
            //        naggedtheuseraboutwrongsignedvalueanswer = messagedlg(format(rsinvalid64bitvaluefor32bitfield, set::of(v, newv, eos)), mtwarning, set::of(mbyes, mbno, eos), 0) == mryes;
            //        naggedtheuseraboutwrongsignedvalue = true;
            //    }
            //
            //    if (naggedtheuseraboutwrongsignedvalue & (naggedtheuseraboutwrongsignedvalueanswer == false))
            //        create(rsinvalidvaluefor32bit);
            //}
            //else
            //    create(rsinvalidvaluefor32bit);
        }
        #endregion
        #region Assemble
        public Boolean Assemble(String opCode, UInt64 address, AByteArray bytes, AAssemblerPreference aPref = AAssemblerPreference.None, Boolean skipRangeCheck = false)
        {
            var tokens = new AStringArray();
            var i = 0;
            var j = 0;
            UInt64 v = 0;
            UInt64 v2 = 0;
            var paramType1 = ATokenType.Invalid;
            var paramType2 = ATokenType.Invalid;
            var paramType3 = ATokenType.Invalid;
            var paramType4 = ATokenType.Invalid;
            var parameter1 = "";
            var parameter2 = "";
            var parameter3 = "";
            var parameter4 = "";
            var oldParamType1 = ATokenType.Invalid;
            var oldParamType2 = ATokenType.Invalid;
            //first,last: integer;
            var startOfList = 0;
            var endOfList = 0;
            var is64bit = Assembler.SymbolHandler.Process.IsX64;
            var candoaddressswitch = false;
            var vexvvvv = 0xf;
            FAddress = address;
            RelativeAddressLocation = -1;
            RexPrefix = 0;
            var result = false;
            Assembler.Tokenize(opCode, tokens);
            var nrOfTokens = tokens.Length;
            if (nrOfTokens == 0)
                return false;
            switch (tokens[0][0])
            {
                case 'A':  //A* //allign
                    {
                        if (tokens[0] == "ALIGN")
                        {
                            if (nrOfTokens >= 2)
                            {
                                i = AStringUtils.HexStrToInt(tokens[1]);
                                Byte b;
                                if (nrOfTokens >= 3)
                                    b = (Byte)AStringUtils.HexStrToInt(tokens[2]);
                                else
                                    b = 0;
                                var k = i - (Int64)address % i;
                                if (k == i)
                                    return true;
                                for (i = 0; i < k; i++)
                                    Assembler.Add(bytes, b);
                                result = true;
                                return true;
                            }
                        }
                    }
                    break;
                case 'D': //D*
                    {
                        if (tokens[0] == "DB")
                        {
                            for (i = 1; i < nrOfTokens; i++)
                            {
                                if (tokens[i][0] == '\'')  //string
                                {
                                    //find the original non uppercase string pos in the opcode
                                    j = AStringUtils.Pos(tokens[i], opCode.ToUpper());
                                    if (j != -1)
                                    {
                                        var tempString = AStringUtils.Copy(opCode, j, tokens[i].Length);
                                        Assembler.AddString(bytes, tempString);
                                    }
                                    else
                                        Assembler.AddString(bytes, tokens[i]); //lets try to save face...
                                }
                                else
                                {    //db 00 00 ?? ?? ?? ?? 00 00
                                    if ((tokens[i].Length >= 1 && (AArrayUtils.InArray(tokens[i][0], '?', '*'))) &&
                                       (tokens[i].Length < 2 || (tokens[i].Length == 2 && tokens[i][1] == tokens[i][0])))
                                    {
                                        //wildcard
                                        v = 0;
                                        var b = (Byte)Assembler.SymbolHandler.Process.Memory.Read<Byte>(((IntPtr)(address + (UInt64)i - 1)));
                                        Assembler.Add(bytes, b);
                                    }
                                    else
                                        Assembler.Add(bytes, (Byte)AStringUtils.HexStrToInt(tokens[i]));
                                }
                            }
                            result = true;
                            return true;
                        }
                        if (tokens[0] == "DW")
                        {
                            for (i = 1; i < nrOfTokens; i++)
                            {
                                if (tokens[i][0] == '\'')  //string
                                {
                                    j = AStringUtils.Pos(tokens[i], opCode.ToUpper());
                                    if (j != -1)
                                    {
                                        var tempString = AStringUtils.Copy(opCode, j, tokens[i].Length);
                                        Assembler.AddWideString(bytes, tempString);
                                    }
                                    else
                                        Assembler.AddWideString(bytes, tokens[i]); //lets try to save face...
                                }
                                else
                                    Assembler.AddWord(bytes, (UInt16)AStringUtils.HexStrToInt(tokens[i]));
                            }
                            result = true;
                            return true;
                        }
                        if (tokens[0] == "DD")
                        {
                            for (i = 1; i < nrOfTokens; i++)
                                Assembler.AddDWord(bytes, (UInt32)AStringUtils.HexStrToInt(tokens[i]));
                            result = true;
                            return true;
                        }
                        if (tokens[0] == "DQ")
                        {
                            for (i = 1; i < nrOfTokens; i++)
                                Assembler.AddQWord(bytes, (UInt64)AStringUtils.HexStrToInt64(tokens[i]));
                            result = true;
                            return true;
                        }
                    }
                    break;
                case 'N': //N*
                    {
                        if (tokens.Length == 2 && tokens[0] == "NOP" && UStringUtils.IsXDigit(tokens[1]))  //NOP HEXVALUE
                        {
                            j = AStringUtils.HexStrToInt(tokens[1]);
                            Assembler.Add(bytes, UBinaryUtils.NewByteArray(0x90, j));
                            return true;
                        }
                    }
                    break;
            }
            // for (i = 0; i <= length(extraassemblers) - 1; i++)
            // {
            //     if (assigned(extraassemblers[i]))
            //         extraassemblers[i](address, opcode, bytes);
            // 
            //     result = length(bytes) > 0;
            //     if (result)
            //         return result;
            // }
            // if (processhandler.systemarchitecture == archarm)
            // {
            //     //handle it by the arm assembler
            //     // for i:=0 to nroftokens do
            //     //   tempstring:=tempstring+tokens[i]+' ';   //seperators like "," are gone, but the armassembler doesn't really care about that  (only tokens matter)
            // 
            //     result = armassemble(address, opcode, bytes);
            //     return result;
            // }
            var mnemonic = -1;
            for (i = 0; i < tokens.Length; i++)
            {
                if (!((tokens[i] == "LOCK") || (tokens[i] == "REP") || (tokens[i] == "REPNE") || (tokens[i] == "REPE")))
                {
                    mnemonic = i;
                    break;
                }
            }
            if (mnemonic == -1)
                return result;
            bytes.SetLength(mnemonic);
            for (i = 0; i < mnemonic; i++)
            {
                if (tokens[i] == "REP")
                    bytes[i] = 0xf3;
                else if (tokens[i] == "REPNE")
                    bytes[i] = 0xf2;
                else if (tokens[i] == "REPE")
                    bytes[i] = 0xf3;
                else if (tokens[i] == "LOCK")
                    bytes[i] = 0xf0;
            }
            //this is just to speed up the finding of the right opcode
            //I could have done a if mnemonic=... then ... else if mnemonic=... then ..., but that would be slow, VERY SLOW
            if ((nrOfTokens - 1) >= mnemonic + 1)
                parameter1 = tokens[mnemonic + 1];
            else
                parameter1 = "";
            if ((nrOfTokens - 1) >= mnemonic + 2)
                parameter2 = tokens[mnemonic + 2];
            else
                parameter2 = "";
            if ((nrOfTokens - 1) >= mnemonic + 3)
                parameter3 = tokens[mnemonic + 3];
            else
                parameter3 = "";
            if ((nrOfTokens - 1) >= mnemonic + 4)
                parameter4 = tokens[mnemonic + 4];
            else
                parameter4 = "";
            var overrideShort = AStringUtils.Pos("SHORT ", parameter1, true) != -1;
            var overrideLong = (AStringUtils.Pos("LONG ", parameter1, true) != -1);
            var overrideFar = false;
            if (Assembler.SymbolHandler.Process.IsX64)
                overrideFar = (AStringUtils.Pos("FAR ", parameter1, true) != -1);
            else
                overrideLong |= (AStringUtils.Pos("FAR ", parameter1, true) != -1);
            if (!(overrideShort | overrideLong | overrideFar) & (aPref != AAssemblerPreference.None))  //no override choice by the user and not a normal preference
            {
                if (aPref == AAssemblerPreference.Far)
                    overrideFar = true;
                if (aPref == AAssemblerPreference.Long)
                    overrideLong = true;
                else if (aPref == AAssemblerPreference.Short)
                    overrideShort = true;
            }
            paramType1 = Assembler.GetTokenType(ref parameter1, parameter2);
            paramType2 = Assembler.GetTokenType(ref parameter2, parameter1);
            paramType3 = Assembler.GetTokenType(ref parameter3, "");
            paramType4 = Assembler.GetTokenType(ref parameter4, "");
            if (Assembler.SymbolHandler.Process.IsX64)
            {
                if (paramType1 == ATokenType.Register8BitWithPrefix)
                {
                    RexPrefix = (Byte)(RexPrefix | 0x40); //it at least has a prefix now
                    paramType1 = ATokenType.Register8Bit;
                }
                if (paramType2 == ATokenType.Register8BitWithPrefix)
                {
                    RexPrefix = (Byte)(RexPrefix | 0x40); //it at least has a prefix now
                    paramType2 = ATokenType.Register8Bit;
                }
                if (paramType1 == ATokenType.Register64Bit)
                {
                    RexW = true;   //64-bit opperand
                    paramType1 = ATokenType.Register32Bit; //we can use the normal 32-bit interpretation assembler code
                }
                if (paramType2 == ATokenType.Register64Bit)
                {
                    RexW = true;
                    paramType2 = ATokenType.Register32Bit;
                }
                if (paramType3 == ATokenType.Register64Bit)
                {
                    RexW = true;
                    paramType3 = ATokenType.Register32Bit;
                }
                if (paramType1 == ATokenType.MemoryLocation64)
                {
                    RexW = true;
                    paramType1 = ATokenType.MemoryLocation32;
                }
                if (paramType2 == ATokenType.MemoryLocation64)
                {
                    RexW = true;
                    paramType2 = ATokenType.MemoryLocation32;
                }
            }
            if (tokens[0][0] == 'R')  //R*
            {
                //only 2 tokens, the first token is 4 chars and it starts with RES
                if (tokens.Length == 2 && tokens[0].Length == 4 && AStringUtils.Copy(tokens[0], 0, 3).ToUpper() == "RES")
                {
                    //RES* X
                    switch (tokens[0][3])
                    {
                        case 'B':
                            i = 1; break; //1 byte long entries
                        case 'W':
                            i = 2; break; //2 byte long entries
                        case 'D':
                            i = 4; break; //4 byte long entries
                        case 'Q':
                            i = 8; break; //8 byte long entries
                        default:
                            throw new Exception("Invalid");
                    }
                    i *= AStringUtils.StrToInt(tokens[1]);
                    bytes.SetLength(i);
                    var bt = (Byte[])Assembler.SymbolHandler.Process.Memory.Read((IntPtr)address, i);
                    if (bt.Length == bytes.Length)
                    {
                        for (i = 0; i < bt.Length; i++)
                            bytes[i] = bt[i];
                    }
                    else
                    {
                        for (j = 0; j <= i - 1; j++)
                            bytes[j] = 0; //init the bytes to 0 (actually it should be uninitialized, but really... (Use structs for that)}
                    }
                    result = true;
                    return true;
                }
            }
            if ((paramType1 >= ATokenType.MemoryLocation) && (paramType1 <= ATokenType.MemoryLocation128))
            {
                if (AStringUtils.Pos("ES:", parameter1) != -1)
                {
                    bytes.SetLength(bytes.Length + 1);
                    bytes[bytes.Length - 1] = 0x26;
                }
                if (AStringUtils.Pos("CS:", parameter1) != -1)
                {
                    bytes.SetLength(bytes.Length + 1);
                    bytes[bytes.Length - 1] = 0x2e;
                }
                if (AStringUtils.Pos("SS:", parameter1) != -1)
                {
                    bytes.SetLength(bytes.Length + 1);
                    bytes[bytes.Length - 1] = 0x36;
                }
                if (AStringUtils.Pos("FS:", parameter1) != -1)
                {
                    bytes.SetLength(bytes.Length + 1);
                    bytes[bytes.Length - 1] = 0x64;
                }
                if (AStringUtils.Pos("GS:", parameter1) != -1)
                {
                    bytes.SetLength(bytes.Length + 1);
                    bytes[bytes.Length - 1] = 0x65;
                }
            }
            if ((paramType2 >= ATokenType.MemoryLocation) && (paramType2 <= ATokenType.MemoryLocation128))
            {
                if (AStringUtils.Pos("ES:", parameter2) != -1)
                {
                    bytes.SetLength(bytes.Length + 1);
                    bytes[bytes.Length - 1] = 0x26;
                }
                if (AStringUtils.Pos("CS:", parameter2) != -1)
                {
                    bytes.SetLength(bytes.Length + 1);
                    bytes[bytes.Length - 1] = 0x2e;
                }
                if (AStringUtils.Pos("SS:", parameter2) != -1)
                {
                    bytes.SetLength(bytes.Length + 1);
                    bytes[bytes.Length - 1] = 0x36;
                }
                if (AStringUtils.Pos("FS:", parameter2) != -1)
                {
                    bytes.SetLength(bytes.Length + 1);
                    bytes[bytes.Length - 1] = 0x64;
                }
                if (AStringUtils.Pos("GS:", parameter2) != -1)
                {
                    bytes.SetLength(bytes.Length + 1);
                    bytes[bytes.Length - 1] = 0x65;
                }
            }
            v = 0;
            v2 = 0;
            var vType = 0;
            var v2Type = 0;
            if (paramType1 == ATokenType.Value)
            {
                v = AStringUtils.StrToQWordEx(parameter1);
                vType = Assembler.StringValueToType(parameter1);
            }
            if (paramType2 == ATokenType.Value)
            {
                if (paramType1 != ATokenType.Value)
                {
                    v = AStringUtils.StrToQWordEx(parameter2);
                    vType = Assembler.StringValueToType(parameter2);
                }
                else
                {
                    //first v field is already in use, use v2
                    v2 = AStringUtils.StrToQWordEx(parameter2);
                    v2Type = Assembler.StringValueToType(parameter2);
                }
            }
            if (paramType3 == ATokenType.Value)
            {
                if (paramType1 != ATokenType.Value)
                {
                    v = AStringUtils.StrToQWordEx(parameter3);
                    vType = Assembler.StringValueToType(parameter3);
                }
                else
                {
                    //first v field is already in use, use v2
                    v2 = AStringUtils.StrToQWordEx(parameter3);
                    v2Type = Assembler.StringValueToType(parameter3);
                }
            }
            if (paramType4 == ATokenType.Value)
            {
                v = AStringUtils.StrToQWordEx(parameter4);
                vType = Assembler.StringValueToType(parameter4);
            }
            var signedVType = Assembler.SignedValueToType((IntPtr)v);
            result = false;
            //to make it easier for people that don't like the relative addressing limit
            if ((!overrideShort) & (!overrideLong) & (Assembler.SymbolHandler.Process.IsX64))    //if 64-bit and no override is given
            {
                //check if this is a jmp or call with relative value
                if ((tokens[mnemonic] == "JMP") || (tokens[mnemonic] == "CALL"))
                {
                    if (paramType1 == ATokenType.Value)
                    {
                        //if the relative distance is too big, then replace with with jmp/call [2], jmp+8, DQ address
                        if (address > v)
                            v2 = address - v;
                        else
                            v2 = v - address;
                        if ((v2 > 0x7fffffff) | overrideFar)  //the user WANTS it to be called as a 'far' jump even if it's not needed
                        {
                            //restart
                            bytes.SetLength(0);
                            RexPrefix = 0;
                            Assembler.Add(bytes, 0xff);
                            if (tokens[mnemonic] == "JMP")
                            {
                                Assembler.Add(bytes, 0x25);
                                Assembler.AddDWord(bytes, 0);
                            }
                            else
                            {
                                Assembler.Add(bytes, 0x15); //call
                                Assembler.AddDWord(bytes, 2);
                                Assembler.Add(bytes, 0xeb, 0x8);
                            }
                            Assembler.AddQWord(bytes, v);
                            result = true;
                            return result;
                        }
                    }
                }
            }
            j = Assembler.GetOpCodesIndex(tokens[mnemonic]); //index scan, better than sorted
            if (j == -1)
                return result;
            startOfList = j;
            endOfList = startOfList;
            while (endOfList < Assembler.OpCodeCount && Assembler.OpCodes[endOfList].Mnemonic == tokens[mnemonic])
                endOfList += 1;
            endOfList -= 1;
            try
            {
                while (j < Assembler.OpCodeCount)
                {
                    if (Assembler.OpCodes[j].Mnemonic != tokens[mnemonic])
                        return result;
                    if ((Assembler.OpCodes[j].InvalidIn32Bit & !is64bit) | (Assembler.OpCodes[j].InvalidIn64Bit & is64bit))
                    {
                        j += 1;
                        continue; // todo figure out if this is meant to be break/continue
                    }
                    oldParamType1 = paramType1;
                    oldParamType2 = paramType2;
                    if (Assembler.OpCodes[j].W0)
                    {
                        //undo rex_w change
                        if (paramType1 == ATokenType.MemoryLocation32)
                            paramType1 = Assembler.GetTokenType(ref parameter1, parameter2);
                        if (paramType2 == ATokenType.MemoryLocation32)
                            paramType2 = Assembler.GetTokenType(ref parameter2, parameter3);
                    }
                    candoaddressswitch = Assembler.OpCodes[j].CanDoAddressSwitch;
                    switch (Assembler.OpCodes[j].ParamType1)
                    {
                        case AParam.None:
                            if (parameter1 == "")      //no param
                            {
                                //no param
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //no_param,no_param,no_param
                                        if ((Assembler.OpCodes[j].OpCode1 == AExtraOpCode.None) && (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.None))
                                        {
                                            //textraopcode.eo_none,textraopcode.eo_none--no_param,no_param,no_param
                                            AddOpCode(bytes, j);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                            }
                            break;
                        case AParam.Imm8:
                            if (paramType1 == ATokenType.Value)
                            {
                                //imm8,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Al) && (parameter2 == "AL"))
                                {
                                    //imm8,al
                                    AddOpCode(bytes, j);
                                    Assembler.Add(bytes, (Byte)v);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Ax) && (parameter2 == "AX"))
                                {
                                    //imm8,ax /?
                                    AddOpCode(bytes, j);
                                    Assembler.Add(bytes, (Byte)v);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Eax) && ((parameter2 == "EAX") || (parameter2 == "RAX")))
                                {
                                    //imm8,eax
                                    AddOpCode(bytes, j);
                                    Assembler.Add(bytes, (Byte)v);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    if (vType == 16)
                                    {
                                        //see if there is also a 'opcode imm16' variant
                                        var k = startOfList;
                                        while ((k < Assembler.OpCodeCount) && (Assembler.OpCodes[k].Mnemonic == tokens[mnemonic]))
                                        {
                                            if (Assembler.OpCodes[k].ParamType1 == AParam.Imm16)
                                            {
                                                AddOpCode(bytes, k);
                                                Assembler.AddWord(bytes, (UInt16)v);
                                                result = true;
                                                return result;
                                            }
                                            k += 1;
                                        }
                                    }
                                    if ((vType == 32) || (signedVType > 8))
                                    {
                                        //see if there is also a 'opcode imm32' variant
                                        var k = startOfList;
                                        while ((k < Assembler.OpCodeCount) && (Assembler.OpCodes[k].Mnemonic == tokens[mnemonic]))
                                        {
                                            if (Assembler.OpCodes[k].ParamType1 == AParam.Imm32)
                                            {
                                                if ((signedVType == 64) & RexW)
                                                    Invalid64BitValueFor32BitField(v);
                                                AddOpCode(bytes, k);
                                                Assembler.AddDWord(bytes, (UInt32)v);
                                                result = true;
                                                return result;
                                            }
                                            k += 1;
                                        }
                                    }
                                    //op imm8
                                    AddOpCode(bytes, j);
                                    Assembler.Add(bytes, (Byte)v);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.Imm16:
                            if (paramType1 == ATokenType.Value)
                            {
                                //imm16,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    //imm16
                                    if ((vType == 32) || (signedVType > 8))
                                    {
                                        //see if there is also a 'opcode imm32' variant
                                        var k = startOfList;
                                        while (k < Assembler.OpCodeCount && Assembler.OpCodes[k].Mnemonic == tokens[mnemonic])
                                        {
                                            if (Assembler.OpCodes[k].ParamType1 == AParam.Imm32)
                                            {
                                                if ((signedVType == 64) & RexW)
                                                    Invalid64BitValueFor32BitField(v);
                                                AddOpCode(bytes, k);
                                                Assembler.AddDWord(bytes, (UInt32)v);
                                                result = true;
                                                return true;
                                            }
                                            k += 1;
                                        }
                                    }
                                    AddOpCode(bytes, j);
                                    Assembler.AddWord(bytes, (UInt16)v);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Imm8) && (paramType2 == ATokenType.Value))
                                {
                                    //imm16,imm8,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        Assembler.AddWord(bytes, (UInt16)v);
                                        Assembler.Add(bytes, (Byte)v2);
                                        result = true;
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.Imm32:
                            if (paramType1 == ATokenType.Value)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    //imm32
                                    if ((signedVType == 64) & RexW)
                                        Invalid64BitValueFor32BitField(v);
                                    AddOpCode(bytes, j);
                                    Assembler.AddDWord(bytes, (UInt32)v);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.Moffs8:
                            if ((paramType1 == ATokenType.MemoryLocation8) | (Assembler.IsMemoryLocationDefault(parameter1)))
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Al) && (parameter2 == "AL"))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        var k = AStringUtils.Pos("[", parameter1);
                                        var l = AStringUtils.Pos("]", parameter1);
                                        AStringUtils.Val("0x" + AStringUtils.Copy(parameter1, k + 1, l - k - 1), out v, out k);
                                        if (k == 0)
                                        {
                                            //verified, it doesn't have a register base in it
                                            AddOpCode(bytes, j);
                                            if (Assembler.SymbolHandler.Process.IsX64)
                                                Assembler.AddQWord(bytes, v);
                                            else
                                                Assembler.AddDWord(bytes, (UInt32)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                            }
                            break;
                        case AParam.Moffs16:
                            if ((paramType1 == ATokenType.MemoryLocation16) | (Assembler.IsMemoryLocationDefault(parameter1)))
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Ax) && (parameter2 == "AX"))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        var k = AStringUtils.Pos("[", parameter1);
                                        var l = AStringUtils.Pos("]", parameter1);
                                        AStringUtils.Val("0x" + AStringUtils.Copy(parameter1, k + 1, l - k - 1), out v, out k);
                                        if (k == 0)
                                        {
                                            //verified, it doesn't have a register base in it
                                            AddOpCode(bytes, j);
                                            if (Assembler.SymbolHandler.Process.IsX64)
                                                Assembler.AddQWord(bytes, v);
                                            else
                                                Assembler.AddDWord(bytes, (UInt32)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                            }
                            break;
                        case AParam.Moffs32:
                            if (paramType1 == ATokenType.MemoryLocation32)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Eax) && ((parameter2 == "EAX") || (parameter2 == "RAX")))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        var k = AStringUtils.Pos("[", parameter1);
                                        var l = AStringUtils.Pos("]", parameter1);
                                        AStringUtils.Val("0x" + AStringUtils.Copy(parameter1, k + l, l - k - 1), out v, out k);
                                        if (k == 0)
                                        {
                                            //verified, it doesn't have a registerbase in it
                                            AddOpCode(bytes, j);
                                            if (Assembler.SymbolHandler.Process.IsX64)
                                                Assembler.AddQWord(bytes, v);
                                            else
                                                Assembler.AddDWord(bytes, (UInt32)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                            }
                            break;
                        case AParam.N3:
                            if ((paramType1 == ATokenType.Value) && (v == 3))
                            {
                                //int 3
                                AddOpCode(bytes, j);
                                result = true;
                                return result;
                            }
                            break;
                        case AParam.Al:
                            if (parameter1 == "AL")
                            {
                                //AL,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Dx) && (parameter2 == "DX"))
                                {
                                    //opcode al,dx
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Imm8) && (paramType2 == ATokenType.Value))
                                {
                                    //AL,imm8
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        if ((Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Ib) && (Assembler.OpCodes[j].OpCode2 == AExtraOpCode.None))
                                        {
                                            //verified: AL,imm8
                                            AddOpCode(bytes, j);
                                            Assembler.Add(bytes, (Byte)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Moffs8) & ((paramType2 == ATokenType.MemoryLocation8) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        var k = AStringUtils.Pos("[", parameter2);
                                        var l = AStringUtils.Pos("]", parameter2);
                                        AStringUtils.Val("0x" + AStringUtils.Copy(parameter2, k + l, l - k - 1), out v, out k);
                                        if (k == 0)
                                        {
                                            //verified, it doesn't have a registerbase in it
                                            AddOpCode(bytes, j);
                                            if (Assembler.SymbolHandler.Process.IsX64)
                                                Assembler.AddQWord(bytes, v);
                                            else
                                                Assembler.AddDWord(bytes, (UInt32)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                            }
                            break;
                        case AParam.Ax:
                            if (parameter1 == "AX")
                            {
                                //AX,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    //opcode AX
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Dx) && (parameter2 == "DX"))
                                {
                                    //opcode ax,dx
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                                //r16
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.R16) && (paramType2 == ATokenType.Register16Bit))
                                {
                                    //eax,r32
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //r32,eax
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Prw)
                                        {
                                            //opcode+rd
                                            AddOpCode(bytes, j);
                                            bytes[bytes.Length - 1] += (Byte)GetReg(parameter2);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Imm16) && (paramType2 == ATokenType.Value))
                                {
                                    //AX,imm16
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //params confirmed it is a ax,imm16
                                        if ((Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Iw) && (Assembler.OpCodes[j].OpCode2 == AExtraOpCode.None))
                                        {
                                            AddOpCode(bytes, j);
                                            Assembler.AddWord(bytes, (UInt16)(v));
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Moffs16) & ((paramType2 == ATokenType.MemoryLocation16) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        var k = AStringUtils.Pos("[", parameter2);
                                        var l = AStringUtils.Pos("]", parameter2);
                                        AStringUtils.Val("0x" + AStringUtils.Copy(parameter2, k + l, l - k - 1), out v, out k);
                                        if (k == 0)
                                        {
                                            //verified, it doesn't have a registerbase in it
                                            AddOpCode(bytes, j);
                                            if (Assembler.SymbolHandler.Process.IsX64)
                                                Assembler.AddQWord(bytes, v);
                                            else
                                                Assembler.AddDWord(bytes, (UInt32)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                            }
                            break;
                        case AParam.Eax:
                            if ((parameter1 == "EAX") || (parameter1 == "RAX"))
                            {
                                //eAX,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Dx) && (parameter2 == "DX"))
                                {
                                    //opcode eax,dx
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                                //r32
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.R32) && (paramType2 == ATokenType.Register32Bit))
                                {
                                    //eax,r32
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //r32,eax
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Prd)
                                        {
                                            //opcode+rd
                                            AddOpCode(bytes, j);
                                            var k = GetReg(parameter2);
                                            if (k > 7)
                                            {
                                                RexB = true; //extention to the opcode field
                                                k &= 7;
                                            }
                                            bytes[bytes.Length - 1] += (Byte)k;
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Imm8) && (paramType2 == ATokenType.Value))
                                {
                                    //eax,imm8
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        Assembler.Add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Imm32) && (paramType2 == ATokenType.Value))
                                {
                                    //EAX,imm32,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //eax,imm32
                                        if (signedVType == 8)
                                        {
                                            //check if there isn't a rm32,imm8 , since that's less bytes
                                            var k = startOfList;
                                            while ((k < Assembler.OpCodeCount) && (Assembler.OpCodes[k].Mnemonic == tokens[mnemonic]))
                                            {
                                                if ((Assembler.OpCodes[k].ParamType1 == AParam.Rm32) &&
                                                   (Assembler.OpCodes[k].ParamType2 == AParam.Imm8))
                                                {
                                                    //yes, there is
                                                    AddOpCode(bytes, k);
                                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[k].OpCode1), parameter1);
                                                    Assembler.Add(bytes, (Byte)v);
                                                    return result;
                                                }
                                                k += 1;
                                            }
                                        }
                                        if ((Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Id) && (Assembler.OpCodes[j].OpCode2 == AExtraOpCode.None))
                                        {
                                            if ((signedVType == 64) & RexW)
                                                Invalid64BitValueFor32BitField(v);
                                            AddOpCode(bytes, j);
                                            Assembler.AddDWord(bytes, (UInt32)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Moffs32) & ((paramType2 == ATokenType.MemoryLocation32) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        var k = AStringUtils.Pos("[", parameter2);
                                        var l = AStringUtils.Pos("]", parameter2);
                                        AStringUtils.Val("0x" + AStringUtils.Copy(parameter2, k + 1, l - k - 1), out v, out k);
                                        if (k == 0)
                                        {
                                            //verified, it doesn't have a registerbase in it
                                            AddOpCode(bytes, j);
                                            if (Assembler.SymbolHandler.Process.IsX64)
                                                Assembler.AddQWord(bytes, v);
                                            else
                                                Assembler.AddDWord(bytes, (UInt32)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                            }
                            break;
                        case AParam.Dx:
                            if (parameter1 == "DX")
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Al) && (parameter2 == "AL"))
                                {
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Ax) && (parameter2 == "AX"))
                                {
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Eax) && ((parameter2 == "EAX") || (parameter2 == "RAX")))
                                {
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.Cs:
                            if (parameter1 == "CS")
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.Ds:
                            if (parameter1 == "DS")
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.Es:
                            if (parameter1 == "ES")
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.Ss:
                            if (parameter1 == "SS")
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.Fs:
                            if (parameter1 == "FS")
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;

                        case AParam.Gs:
                            if (parameter1 == "GS")
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.R8:
                            if (paramType1 == ATokenType.Register8Bit)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    //opcode r8
                                    if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Prb)
                                    {
                                        //opcode+rd
                                        AddOpCode(bytes, j);
                                        var k = GetReg(parameter1);
                                        if (k > 7)
                                        {
                                            RexB = true;
                                            k &= 7;
                                        }
                                        bytes[bytes.Length - 1] += (Byte)k;
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Imm8) && (paramType2 == ATokenType.Value))
                                {
                                    //r8, imm8
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Prb)
                                        {
                                            AddOpCode(bytes, j);
                                            var k = GetReg(parameter1);
                                            if (k > 7)
                                            {
                                                RexB = true; //extension to the opcode
                                                k &= 7;
                                            }
                                            bytes[bytes.Length - 1] += (Byte)k;
                                            Assembler.Add(bytes, (Byte)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Rm8) & (Assembler.IsMem8(paramType2)))
                                {
                                    //r8,rm8
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.R16:
                            if (paramType1 == ATokenType.Register16Bit)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    //opcode r16
                                    if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Prw)
                                    {
                                        //opcode+rw
                                        AddOpCode(bytes, j);
                                        var k = GetReg(parameter1);
                                        if (k > 7)
                                        {
                                            RexB = true;
                                            k &= 7;
                                        }
                                        bytes[bytes.Length - 1] += (Byte)k;
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Ax) && (parameter2 == "AX"))
                                {
                                    //r16,ax,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //r16,ax
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Prw)
                                        {
                                            //opcode+rd
                                            AddOpCode(bytes, j);
                                            var k = GetReg(parameter1);
                                            if (k > 7)
                                            {
                                                RexB = true;
                                                k &= 7;
                                            }
                                            bytes[bytes.Length - 1] += (Byte)k;
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Imm8) && (paramType2 == ATokenType.Value))
                                {
                                    //r16, imm8
                                    if ((Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Reg) && (Assembler.OpCodes[j].OpCode2 == AExtraOpCode.Ib))
                                    {
                                        if (vType > 8)
                                        {
                                            //search for r16/imm16
                                            var k = startOfList;
                                            while ((k < Assembler.OpCodeCount) && (Assembler.OpCodes[k].Mnemonic == tokens[mnemonic]))
                                            {
                                                if ((Assembler.OpCodes[k].ParamType1 == AParam.R16) &&
                                                   (Assembler.OpCodes[k].ParamType2 == AParam.Imm16))
                                                {
                                                    if ((Assembler.OpCodes[k].OpCode1 == AExtraOpCode.Reg) && (Assembler.OpCodes[j].OpCode2 == AExtraOpCode.Ib))
                                                    {
                                                        AddOpCode(bytes, k);
                                                        result = CreateModRm(bytes, GetReg(parameter1), parameter1);
                                                        Assembler.AddWord(bytes, (UInt16)v);
                                                        return result;
                                                    }
                                                }
                                                k += 1;
                                            }
                                        }
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Imm16) && (paramType2 == ATokenType.Value))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Prw)
                                        {
                                            AddOpCode(bytes, j);
                                            var k = GetReg(parameter1);
                                            if (k > 7)
                                            {
                                                RexB = true;
                                                k &= 7;
                                            }
                                            bytes[bytes.Length - 1] += (Byte)k;
                                            Assembler.AddWord(bytes, (UInt16)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Rm8) & (Assembler.IsMem8(paramType2)))
                                {
                                    //r16,r/m8 (eg: movzx)
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Rm16) & (Assembler.IsMem16(paramType2)))
                                {
                                    //r16,r/m16
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Imm8) && (paramType3 == ATokenType.Value))
                                    {
                                        if (Assembler.OpCodes[j].OpCode2 == AExtraOpCode.Ib)
                                        {
                                            //r16,r/m16,imm8
                                            if (vType > 8)
                                            {
                                                //see if there is a //r16,r/m16,imm16
                                                var k = startOfList;
                                                while ((k < Assembler.OpCodeCount) && (Assembler.OpCodes[k].Mnemonic == tokens[mnemonic]))
                                                {
                                                    if ((Assembler.OpCodes[k].ParamType1 == AParam.R16) &&
                                                       (Assembler.OpCodes[k].ParamType2 == AParam.Rm16) &&
                                                       (Assembler.OpCodes[k].ParamType3 == AParam.Imm16))
                                                    {
                                                        AddOpCode(bytes, k);
                                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                                        Assembler.AddWord(bytes, (UInt16)v);
                                                        return result;
                                                    }
                                                    k += 1;
                                                }
                                            }
                                            AddOpCode(bytes, j);
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                            Assembler.Add(bytes, (Byte)v);
                                            return result;
                                        }
                                    }
                                }
                            }
                            break;
                        case AParam.R32:
                            if (paramType1 == ATokenType.Register32Bit)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    //opcode r32
                                    if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Prd)
                                    {
                                        //opcode+rd
                                        AddOpCode(bytes, j);
                                        var k = GetReg(parameter1);
                                        if (k > 7)
                                        {
                                            RexB = true;
                                            k &= 7;
                                        }
                                        bytes[bytes.Length - 1] += (Byte)k;
                                        result = true;
                                        return result;
                                    }
                                    else
                                    {
                                        //reg0..reg7
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.R32) && (paramType2 == ATokenType.Register32Bit))
                                {
                                    //r32,r32,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Rm32) & (Assembler.IsMem32(paramType3)))
                                    {
                                        //r32,r32,rm32
                                        if (Assembler.OpCodes[j].VexExtraParam == 2)
                                        {
                                            AddOpCode(bytes, j);
                                            vexvvvv = (~GetReg(parameter2)) & 0xf;
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                            return result;
                                        }
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.M32) && (paramType3 == ATokenType.MemoryLocation32))
                                    {
                                        //r32,r32,m32
                                        if (Assembler.OpCodes[j].VexExtraParam == 2)
                                        {
                                            AddOpCode(bytes, j);
                                            vexvvvv = (~GetReg(parameter2)) & 0xf;
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                            return result;
                                        }
                                    }
                                }
                                //eax
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Eax) && ((parameter2 == "EAX") || (parameter2 == "RAX")))
                                {
                                    //r32,eax,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //r32,eax
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Prd)
                                        {
                                            //opcode+rd
                                            AddOpCode(bytes, j);
                                            var k = GetReg(parameter1);
                                            if (k > 7)
                                            {
                                                RexB = true;
                                                k &= 7;
                                            }
                                            bytes[bytes.Length - 1] += (Byte)k;
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Mm) && (paramType2 == ATokenType.RegisterMm))
                                {
                                    //r32, mm,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Imm8) && (paramType3 == ATokenType.Value))
                                    {
                                        //r32, mm,imm8
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Ymm) && (paramType2 == ATokenType.RegisterYmm))
                                {
                                    //r32,ymm,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm) && (paramType2 == ATokenType.RegisterXmm))
                                {
                                    //r32,xmm,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }

                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Imm8) && (paramType3 == ATokenType.Value))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Cr) && (paramType2 == ATokenType.RegisterCr))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Dr) && (paramType2 == ATokenType.RegisterDr))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                }

                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm32) & (Assembler.IsXmm32(paramType2)))
                                {
                                    //r32,xmm/m32
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Mmm64) && (Assembler.IsRmm64(paramType2) | ((paramType2 == ATokenType.MemoryLocation32) && (parameter2[0] == '['))))
                                {
                                    //r32,mm/m64
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm64) && (Assembler.IsXmm64(paramType2) | ((paramType2 == ATokenType.MemoryLocation32) && (parameter2[0] == '['))))
                                {
                                    //r32,xmm/m64
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm128) && (Assembler.IsXmm64(paramType2) | ((paramType2 == ATokenType.MemoryLocation32) && (parameter2[0] == '['))))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.M32) && (paramType2 == ATokenType.MemoryLocation32))
                                {
                                    //r32,m32,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //r32,m32
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.M128) & ((paramType2 == ATokenType.MemoryLocation128) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    //r32,m128,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //r32,m128
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Rm8) & (Assembler.IsMem8(paramType2) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    //r32,rm8
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Rm16) & (Assembler.IsMem16(paramType2) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    //r32,rm16
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Rm32) & (Assembler.IsMem32(paramType2)))
                                {
                                    //r32,r/m32
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Reg)
                                        {
                                            AddOpCode(bytes, j);
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                            return result;
                                        }
                                        else
                                        {
                                            if (Assembler.OpCodes[j].VexExtraParam == 1)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter1)) & 0xf;
                                                result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                                return result;
                                            }
                                        }
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Imm8) && (paramType3 == ATokenType.Value))
                                    {
                                        if (Assembler.OpCodes[j].OpCode2 == AExtraOpCode.Ib)
                                        {
                                            if (vType > 8)
                                            {
                                                var k = startOfList;
                                                while ((k <= endOfList) && (Assembler.OpCodes[k].Mnemonic == tokens[mnemonic]))
                                                {
                                                    if ((Assembler.OpCodes[k].ParamType1 == AParam.R32) &&
                                                       (Assembler.OpCodes[k].ParamType2 == AParam.Rm32) &&
                                                       (Assembler.OpCodes[k].ParamType3 == AParam.Imm32))
                                                    {
                                                        if ((signedVType == 64) & RexW)
                                                            Invalid64BitValueFor32BitField(v);
                                                        AddOpCode(bytes, k);
                                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                                        Assembler.AddDWord(bytes, (UInt32)v);
                                                        return result;
                                                    }
                                                    k += 1;
                                                }
                                            }
                                            //r32,r/m32,imm8
                                            AddOpCode(bytes, j);
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                            Assembler.Add(bytes, (Byte)v);
                                            return result;
                                        }
                                    }
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.R32)
                                    {
                                        //r32,rm32,r32
                                        if (Assembler.OpCodes[j].VexExtraParam == 3)
                                        {
                                            AddOpCode(bytes, j);
                                            vexvvvv = (~GetReg(parameter2)) & 0xf;
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Imm32) && (paramType2 == ATokenType.Value))
                                {
                                    //r32,imm32
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Prd)
                                        {
                                            AddOpCode(bytes, j);
                                            var k = GetReg(parameter1);
                                            if (k > 7)
                                            {
                                                RexB = true;
                                                k &= 7;
                                            }
                                            bytes[bytes.Length - 1] += (Byte)k;
                                            if (RexW)
                                                Assembler.AddQWord(bytes, v);
                                            else
                                                Assembler.AddDWord(bytes, (UInt32)v);
                                            result = true;
                                            return result;
                                        }
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Reg)   //probably imul reg,imm32
                                        {
                                            if (signedVType == 8)
                                            {
                                                var k = startOfList;
                                                while ((k <= endOfList) && (Assembler.OpCodes[k].Mnemonic == tokens[mnemonic]))  //check for an reg,imm8
                                                {
                                                    if ((Assembler.OpCodes[k].ParamType1 == AParam.R32) &&
                                                        (Assembler.OpCodes[k].ParamType2 == AParam.Imm8))
                                                    {
                                                        AddOpCode(bytes, k);
                                                        CreateModRm(bytes, GetReg(parameter1), parameter1);
                                                        Assembler.Add(bytes, (byte)(v));
                                                        result = true;
                                                        return result;
                                                    }
                                                    k += 1;
                                                }
                                            }
                                            if ((signedVType == 64) & RexW)
                                                Invalid64BitValueFor32BitField(v);
                                            AddOpCode(bytes, j);
                                            CreateModRm(bytes, GetReg(parameter1), parameter1);
                                            Assembler.AddDWord(bytes, (UInt32)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Imm8) && (paramType2 == ATokenType.Value))
                                {
                                    //r32, imm8
                                    if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Prd)
                                    {
                                        AddOpCode(bytes, j);
                                        CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                        Assembler.Add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                    if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Reg)   //probably imul r32,imm8
                                    {
                                        AddOpCode(bytes, j);
                                        CreateModRm(bytes, GetReg(parameter1), parameter1);
                                        Assembler.Add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.SReg:
                            if (paramType1 == ATokenType.RegisterSReg)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Rm16) & (Assembler.IsMem16(paramType2)))
                                {
                                    //sreg,rm16
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                            }
                            break;
                        case AParam.Cr:
                            if (paramType1 == ATokenType.RegisterCr)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.R32) && (paramType2 == ATokenType.Register32Bit))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.Dr:
                            if (paramType1 == ATokenType.RegisterDr)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.R32) && (paramType2 == ATokenType.Register32Bit))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.Rm8:

                            if (Assembler.IsMem8(paramType1) | (Assembler.IsMemoryLocationDefault(parameter1) & Assembler.OpCodes[j].DefaultType))
                            {
                                //r/m8,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    //opcode r/m8
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.N1) && (paramType2 == ATokenType.Value) && (v == 1))
                                {
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Cl) && (parameter2 == "CL"))
                                {
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Imm8) && (paramType2 == ATokenType.Value))
                                {
                                    //r/m8,imm8,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //verified it IS r/m8,imm8
                                        AddOpCode(bytes, j);
                                        CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                        Assembler.Add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.R8) && (paramType2 == ATokenType.Register8Bit))
                                {
                                    // r/m8,r8
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm) && (paramType2 == ATokenType.RegisterXmm))
                                {
                                    // r/m8,xmm
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.Rm16:
                            if (Assembler.IsMem16(paramType1))
                            {
                                //r/m16,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    //opcode r/m16
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.N1) && (paramType2 == ATokenType.Value) && (v == 1))
                                {
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Imm8) && (paramType2 == ATokenType.Value))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        if (vType == 16)
                                        {
                                            //perhaps there is a r/m16,imm16
                                            var k = startOfList;
                                            while (k <= endOfList)
                                            {
                                                if (Assembler.OpCodes[k].Mnemonic != tokens[mnemonic])
                                                    continue; //nope, so continue with r/m,imm16
                                                if (((Assembler.OpCodes[k].ParamType1 == AParam.Rm16) && (Assembler.OpCodes[k].ParamType2 == AParam.Imm16)) && ((Assembler.OpCodes[k].ParamType3 == AParam.None) && (parameter3 == "")))
                                                {
                                                    //yes, there is
                                                    //r/m16,imm16
                                                    AddOpCode(bytes, k);
                                                    CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[k].OpCode1), parameter1);
                                                    Assembler.AddWord(bytes, (UInt16)(v));
                                                    result = true;
                                                    return result;
                                                }
                                                k += 1;
                                            }
                                        }
                                        //nope, so it IS r/m16,8
                                        AddOpCode(bytes, j);
                                        CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                        Assembler.Add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Imm16) && (paramType2 == ATokenType.Value))
                                {
                                    //r/m16,imm
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        if (vType == 8)
                                        {
                                            //see if there is a r/m16,imm8 (or if this is the one) (optimisation)
                                            var k = startOfList;
                                            while (k <= endOfList)
                                            {
                                                if (Assembler.OpCodes[k].Mnemonic != tokens[mnemonic])
                                                    continue; //nope, so continue with r/m,imm16
                                                if (((Assembler.OpCodes[k].ParamType1 == AParam.Rm16) && (Assembler.OpCodes[k].ParamType2 == AParam.Imm8)) && ((Assembler.OpCodes[k].ParamType3 == AParam.None) && (parameter3 == "")))
                                                {
                                                    //yes, there is
                                                    //r/m16,imm8
                                                    AddOpCode(bytes, k);
                                                    CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[k].OpCode1), parameter1);
                                                    Assembler.Add(bytes, (Byte)v);
                                                    result = true;
                                                    return result;
                                                }
                                                k += 1;
                                            }
                                        }
                                        AddOpCode(bytes, j);
                                        CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                        Assembler.AddWord(bytes, (UInt16)(v));
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.R16) && (paramType2 == ATokenType.Register16Bit))
                                {
                                    //r/m16,r16,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Cl) && (parameter3 == "CL"))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Imm8) && (paramType3 == ATokenType.Value))
                                    {
                                        //rm16, r16,imm8
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.SReg) && (paramType2 == ATokenType.RegisterSReg))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //r/m16,sreg
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Cl) && (parameter2 == "CL"))
                                {
                                    //rm16,cl
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.Rm32:
                            if (Assembler.IsMem32(paramType1) | Assembler.IsMem32(oldParamType1))
                            {
                                //r/m32,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    //no 2nd parameter so it is 'opcode r/m32'
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.N1) && (paramType2 == ATokenType.Value) && (v == 1))
                                {
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Imm8) && (paramType2 == ATokenType.Value))
                                {
                                    //rm32,imm8
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        if ((vType > 8) || (Assembler.OpCodes[j].Signed & (signedVType > 8)))
                                        {
                                            //the user requests a bigger than 8-bit value, so see if there is also a rm32,imm32 (there are no r/m32,imm16)
                                            var k = startOfList;
                                            while (k <= endOfList)
                                            {
                                                if (Assembler.OpCodes[k].Mnemonic != tokens[mnemonic])
                                                    continue; // maybe we can find one...
                                                if (((Assembler.OpCodes[k].ParamType1 == AParam.Rm32) && (Assembler.OpCodes[k].ParamType2 == AParam.Imm32)) && ((Assembler.OpCodes[k].ParamType3 == AParam.None) && (parameter3 == "")))
                                                {
                                                    //yes, there is
                                                    if ((signedVType == 64) & RexW)
                                                        Invalid64BitValueFor32BitField(v);
                                                    AddOpCode(bytes, k);
                                                    CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[k].OpCode1), parameter1);
                                                    Assembler.AddDWord(bytes, (UInt32)v);
                                                    result = true;
                                                    return result;
                                                }
                                                k += 1;
                                            }
                                        }
                                        //r/m32,imm8
                                        AddOpCode(bytes, j);
                                        CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                        Assembler.Add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Imm32) && (paramType2 == ATokenType.Value))
                                {
                                    //r/m32,imm
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        if (signedVType == 8)
                                        {
                                            //see if there is a r/m32,imm8 (or if this is the one) (optimisation)
                                            var k = startOfList;
                                            while (k <= endOfList)
                                            {
                                                if (Assembler.OpCodes[k].Mnemonic != tokens[mnemonic])
                                                    //nope, so continue with r/m,imm16
                                                    continue; // maybe we can find one...
                                                if (((Assembler.OpCodes[k].ParamType1 == AParam.Rm32) && (Assembler.OpCodes[k].ParamType2 == AParam.Imm8)) && ((Assembler.OpCodes[k].ParamType3 == AParam.None) && (parameter3 == "")) && ((!Assembler.OpCodes[k].Signed) | (signedVType == 8)))
                                                {
                                                    //yes, there is
                                                    AddOpCode(bytes, k);
                                                    CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[k].OpCode1), parameter1);
                                                    Assembler.Add(bytes, (Byte)v);
                                                    result = true;
                                                    return result;
                                                }
                                                k += 1;
                                            }
                                        }
                                        //no there's none
                                        if ((signedVType == 64) & RexW)
                                        {
                                            //perhaps it's an old user and assumes it can be sign extended automagically
                                            Invalid64BitValueFor32BitField(v);
                                        }
                                        AddOpCode(bytes, j);
                                        CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                        Assembler.AddDWord(bytes, (UInt32)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Cl) && (parameter2 == "CL"))
                                {
                                    //rm32,cl
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.R32) && (paramType2 == ATokenType.Register32Bit))
                                {
                                    //r/m32,r32
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Cl) && (parameter3 == "CL"))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Imm8) && (paramType3 == ATokenType.Value))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Mm) && (paramType2 == ATokenType.RegisterMm))
                                {
                                    //r32/m32,mm,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //r32/m32,mm
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm) && (paramType2 == ATokenType.RegisterXmm))
                                {
                                    //r32/m32,xmm,  (movd for example)
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //r32/m32,xmm
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Imm8) && (paramType3 == ATokenType.Value))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.Mm:
                            if (paramType1 == ATokenType.RegisterMm)
                            {
                                //mm,xxxxx
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Imm8) && (paramType2 == ATokenType.Value))
                                {
                                    //mm,imm8
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Mm) && (paramType2 == ATokenType.RegisterMm))
                                {
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm) && (paramType2 == ATokenType.RegisterXmm))
                                {
                                    //mm,xmm
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.R32M16) & ((paramType1 == ATokenType.Register32Bit) || (paramType2 == ATokenType.MemoryLocation16) | Assembler.IsMemoryLocationDefault(parameter2)))
                                {
                                    //mm,r32/m16,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Imm8) && (paramType3 == ATokenType.Value))
                                    {
                                        //imm8
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Rm32) & (Assembler.IsMem32(paramType2)))
                                {
                                    //mm,rm32
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //mm,rm32
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm32) & (Assembler.IsXmm32(paramType2)))
                                {
                                    //mm,xmm/m32
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Mmm64) && (Assembler.IsRmm64(paramType2) | ((paramType2 == ATokenType.MemoryLocation32) && (parameter2[0] == '['))))
                                {
                                    //mm,mm/m64
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.None)
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }

                                    if (Assembler.OpCodes[j].ParamType3 == AParam.Imm8)
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }


                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm64) && (Assembler.IsXmm64(paramType2) | ((paramType2 == ATokenType.MemoryLocation32) && (parameter2[0] == '['))))
                                {
                                    //mm,xmm/m64
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm128) && (Assembler.IsXmm128(paramType2) | ((paramType2 == ATokenType.MemoryLocation32) && (parameter2[0] == '['))))
                                {
                                    //mm,xmm/m128
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                            }
                            break;
                        case AParam.Mmm64:
                            if (Assembler.IsRmm64(paramType1) | ((paramType1 == ATokenType.MemoryLocation32) && (parameter1[0] == '[')))
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Mm) && (paramType2 == ATokenType.RegisterMm))
                                {
                                    //mm/m64, mm
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.Xmm32:
                            if (Assembler.IsXmm32(paramType1) | ((paramType1 == ATokenType.MemoryLocation32) && (parameter1[0] == '[')))
                            {
                                //xmm/m32,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm) && (paramType2 == ATokenType.RegisterXmm))
                                {
                                    //xmm/m32, xmm
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.Xmm64:
                            if (Assembler.IsXmm64(paramType1) | ((paramType1 == ATokenType.MemoryLocation32) && (parameter1[0] == '[')))
                            {
                                //xmm/m64,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm) && (paramType2 == ATokenType.RegisterXmm))
                                {
                                    //xmm/m64, xmm
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.Xmm128:
                            if (Assembler.IsXmm128(paramType1) | ((paramType1 == ATokenType.MemoryLocation32) && (parameter1[0] == '[')))
                            {
                                //xmm/m128,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm) && (paramType2 == ATokenType.RegisterXmm))
                                {
                                    //xmm/m128, xmm
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.Ymm256:
                            if (Assembler.IsYmm256(paramType1) | ((paramType1 == ATokenType.MemoryLocation32) && (parameter1[0] == '[')))
                            {
                                //ymm_m256,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Ymm) && (paramType2 == ATokenType.RegisterYmm))
                                {
                                    //ymm_m256, ymm
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.Ymm:
                            if (paramType1 == ATokenType.RegisterYmm)
                            {
                                //ymm,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm16) & (Assembler.IsXmm16(paramType2, parameter2)))
                                {
                                    //ymm,xmm/m16
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.None)
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm32) && (Assembler.IsXmm32(paramType2) | ((paramType2 == ATokenType.MemoryLocation32) && (parameter2[0] == '['))))
                                {
                                    //ymm,xmm/m32
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.None)
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm64) && (Assembler.IsXmm64(paramType2) | ((paramType2 == ATokenType.MemoryLocation32) && (parameter2[0] == '['))))
                                {
                                    //ymm,xmm/m64
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.None)
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm128) && (Assembler.IsXmm128(paramType2) | ((paramType2 == ATokenType.MemoryLocation32) && (parameter2[0] == '['))))
                                {
                                    //ymm,xmm/m128
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.None)
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Ymm) && (paramType2 == ATokenType.RegisterYmm))
                                {
                                    //ymm,ymm,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Imm8) && (paramType3 == ATokenType.Value))
                                    {
                                        //ymm,ymm,imm8
                                        if (Assembler.OpCodes[j].VexExtraParam == 1)
                                        {
                                            AddOpCode(bytes, j);
                                            vexvvvv = (~GetReg(parameter1)) & 0xf;
                                            result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter2);
                                            Assembler.Add(bytes, (Byte)v);
                                            return result;
                                        }
                                        else
                                        {
                                            AddOpCode(bytes, j);
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                            Assembler.Add(bytes, (Byte)v);
                                            return result;
                                        }
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.M128) & ((paramType3 == ATokenType.MemoryLocation128) | (Assembler.IsMemoryLocationDefault(parameter3))))
                                    {
                                        //ymm,ymm,m128,
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.None)
                                        {
                                            //ymm,ymm,m128
                                            if (Assembler.OpCodes[j].VexExtraParam == 2)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter2)) & 0xf;
                                                result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                                return result;
                                            }
                                        }
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.M256) & ((paramType3 == ATokenType.MemoryLocation256) | (Assembler.IsMemoryLocationDefault(parameter3))))
                                    {
                                        //ymm,ymm,m256,
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.None)
                                        {
                                            //ymm,ymm,m256
                                            if (Assembler.OpCodes[j].VexExtraParam == 2)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter2)) & 0xf;
                                                result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                                return result;
                                            }
                                        }
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Ymm256) && (Assembler.IsYmm256(paramType3) | ((paramType3 == ATokenType.MemoryLocation32) && (parameter3[0] == '['))))
                                    {
                                        //ymm,ymm,ymm/m256
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.None)
                                        {
                                            if (Assembler.OpCodes[j].VexExtraParam == 2)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter2)) & 0xf;
                                                result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                                return result;
                                            }
                                        }
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.Imm8)
                                        {
                                            //ymm,ymm,ymm/m256,imm8
                                            AddOpCode(bytes, j);
                                            vexvvvv = (~GetReg(parameter2)) & 0xf;
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                            Assembler.Add(bytes, (Byte)AStringUtils.StrToInt(parameter4));
                                            return result;
                                        }
                                        if ((Assembler.OpCodes[j].ParamType4 == AParam.Ymm) && (paramType4 == ATokenType.RegisterYmm))
                                        {
                                            //ymm,ymm,ymm/m128,ymm
                                            AddOpCode(bytes, j);
                                            vexvvvv = (~GetReg(parameter2)) & 0xf;
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                            Assembler.Add(bytes, (Byte)(GetReg(parameter4) << 4));
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.M128) & ((paramType2 == ATokenType.MemoryLocation128) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    //ymm,m128,
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.None)
                                    {
                                        //ymm,m128
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.M256) & ((paramType2 == ATokenType.MemoryLocation256) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    //ymm,m256,
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.None)
                                    {
                                        //ymm,m256
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.Xmm:
                            if (paramType1 == ATokenType.RegisterXmm)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Imm8) && (paramType2 == ATokenType.Value))
                                {
                                    //xmm,imm8
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Mm) && (paramType2 == ATokenType.RegisterMm))
                                {
                                    //xmm,mm
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm) && (paramType2 == ATokenType.RegisterXmm))
                                {
                                    //xmm,xmm,
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.None)
                                    {
                                        //xmm,xmm
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.Imm8)
                                    {
                                        //xmm,xmm,imm8
                                        if (Assembler.OpCodes[j].VexExtraParam == 1)
                                        {
                                            vexvvvv = (~GetReg(parameter1)) & 0xf;
                                            result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter2);
                                            Assembler.Add(bytes, (Byte)v);
                                            return result;
                                        }
                                        else
                                        {
                                            AddOpCode(bytes, j);
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                            Assembler.Add(bytes, (Byte)v);
                                            return result;
                                        }
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Xmm) && (paramType3 ==ATokenType.RegisterXmm))
                                    {
                                        //xmm,xmm,xmm
                                        if (Assembler.OpCodes[j].VexExtraParam == 2)
                                        {
                                            AddOpCode(bytes, j);
                                            vexvvvv = (~GetReg(parameter2)) & 0xf;
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                            return result;
                                        }
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.R32M8) & ((paramType3 ==ATokenType.Register32Bit) || (paramType3 ==ATokenType.MemoryLocation8) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                    {
                                        //xmm,xmm,r32/m8,
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.None)
                                        {
                                            if (Assembler.OpCodes[j].VexExtraParam == 2)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter2)) & 0xf;
                                                result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                                return result;
                                            }
                                        }
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.Imm8)
                                        {
                                            //xmm,xmm,r32/m8,imm8
                                            AddOpCode(bytes, j);
                                            vexvvvv = (~GetReg(parameter2)) & 0xf;
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                            Assembler.Add(bytes, (Byte)AStringUtils.StrToInt(parameter4));
                                            return result;
                                        }
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Rm32) && (Assembler.IsMem32(paramType3) | ((paramType3 ==ATokenType.MemoryLocation32) && (parameter3[0] == '['))))
                                    {
                                        //xmm,xmm,rm32
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.None)
                                        {
                                            if (Assembler.OpCodes[j].VexExtraParam == 2)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter2)) & 0xf;
                                                result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                                return result;
                                            }
                                        }
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.Imm8)
                                        {
                                            //xmm,xmm,rm32,imm8
                                            AddOpCode(bytes, j);
                                            vexvvvv = (~GetReg(parameter2)) & 0xf;
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                            Assembler.Add(bytes, (Byte)AStringUtils.StrToInt(parameter4));
                                            return result;
                                        }
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Xmm32) && (Assembler.IsXmm32(paramType3) | ((paramType3 ==ATokenType.MemoryLocation32) && (parameter3[0] == '['))))
                                    {
                                        //xmm,xmm,xmm/m32,
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.None)
                                        {
                                            if (Assembler.OpCodes[j].VexExtraParam == 2)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter2)) & 0xf;
                                                result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                                return result;
                                            }
                                        }
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.Imm8)
                                        {
                                            //xmm,xmm,xmm/m32,imm8
                                            AddOpCode(bytes, j);
                                            vexvvvv = (~GetReg(parameter2)) & 0xf;
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                            Assembler.Add(bytes, (Byte)AStringUtils.StrToInt(parameter4));
                                            return result;
                                        }
                                    }

                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.M64) & ((paramType3 ==ATokenType.MemoryLocation64) | (Assembler.IsMemoryLocationDefault(parameter3))))
                                    {
                                        //xmm,xmm,m64,
                                        if (Assembler.OpCodes[j].VexExtraParam == 2)
                                        {
                                            AddOpCode(bytes, j);
                                            vexvvvv = (~GetReg(parameter2)) & 0xf;
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                            return result;
                                        }
                                    }

                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.M128) & ((paramType3 ==ATokenType.MemoryLocation128) | (Assembler.IsMemoryLocationDefault(parameter3))))
                                    {
                                        //xmm,xmm,m128,
                                        if (Assembler.OpCodes[j].VexExtraParam == 2)
                                        {
                                            AddOpCode(bytes, j);
                                            vexvvvv = (~GetReg(parameter2)) & 0xf;
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                            return result;
                                        }
                                    }

                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Xmm64) && (Assembler.IsXmm64(paramType3) | ((paramType3 ==ATokenType.MemoryLocation32) && (parameter3[0] == '['))))
                                    {
                                        //xmm,xmm,xmm/m64
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.None)
                                        {
                                            if (Assembler.OpCodes[j].VexExtraParam == 2)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter2)) & 0xf;
                                                result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                                return result;
                                            }
                                        }

                                        if (Assembler.OpCodes[j].ParamType4 == AParam.Imm8)
                                        {
                                            //xmm,xmm,xmm/m64,imm8
                                            if (Assembler.OpCodes[j].VexExtraParam == 2)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter2)) & 0xf;
                                                result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                                Assembler.Add(bytes, (Byte)AStringUtils.StrToInt(parameter4));
                                                return result;
                                            }
                                        }
                                    }

                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Xmm128) && (Assembler.IsXmm128(paramType3) | ((paramType3 ==ATokenType.MemoryLocation32) && (parameter3[0] == '['))))
                                    {
                                        //xmm,xmm,xmm/m128,
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.None)
                                        {
                                            //xmm,xmm,xmm/m128
                                            if (Assembler.OpCodes[j].VexExtraParam == 2)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter2)) & 0xf;
                                                result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                                return result;
                                            }
                                        }

                                        if ((Assembler.OpCodes[j].ParamType4 == AParam.Xmm) && (paramType4 ==ATokenType.RegisterXmm))
                                        {
                                            //xmm,xmm,xmm/128,xmm  (vblendvpd/vps)
                                            if (Assembler.OpCodes[j].VexExtraParam == 2)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter2)) & 0xf;
                                                result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                                Assembler.Add(bytes, (Byte)(GetReg(parameter4) << 4));
                                                return result;
                                            }
                                        }

                                        if (Assembler.OpCodes[j].ParamType4 == AParam.Imm8)
                                        {
                                            //xmm,xmm,xmm/m128,imm8
                                            if (Assembler.OpCodes[j].VexExtraParam == 2)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter2)) & 0xf;
                                                result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                                Assembler.Add(bytes, (Byte)AStringUtils.StrToInt(parameter4));
                                                return result;
                                            }
                                        }
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Ymm256) && (Assembler.IsYmm256(paramType2) | ((paramType2 ==ATokenType.MemoryLocation32) && (parameter2[0] == '['))))
                                {
                                    //xmm,ymm/m256
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.M32) & ((paramType2 ==ATokenType.MemoryLocation32) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    //xmm,m32
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.M64) & ((paramType2 == ATokenType.MemoryLocation64) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    //xmm,m64
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.M128) & ((paramType2 ==ATokenType.MemoryLocation128) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    //xmm,m128,
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.None)
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }

                                    if (Assembler.OpCodes[i].ParamType3 == AParam.Imm8)
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }

                                }

                                if ((Assembler.OpCodes[j].ParamType2 == AParam.R32M8) & ((paramType2 ==ATokenType.Register32Bit) || (paramType2 ==ATokenType.MemoryLocation8) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    //xmm,r32/m8,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Imm8) && (paramType3 ==ATokenType.Value))
                                    {
                                        AddOpCode(bytes, j);
                                        CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        Assembler.Add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Rm32) & (Assembler.IsMem32(paramType2)))
                                {
                                    //xmm,rm32,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //xmm,rm32
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Mmm64) && (Assembler.IsRmm64(paramType2) | ((paramType2 == ATokenType.MemoryLocation32) && (parameter2[0] == '['))))
                                {
                                    //xmm,mm/m64
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //xmm,mm/m64
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm8) & Assembler.IsXmm8(paramType2, parameter2))
                                {
                                    //xmm,xmm/m8,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //xmm,xmm/m8
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }

                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm16) & Assembler.IsXmm16(paramType2, parameter2))
                                {
                                    //xmm,xmm/m16,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //xmm,xmm/m16
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm32) & Assembler.IsXmm32(paramType2))
                                {
                                    //xmm,xmm/m32,
                                    //even if the user didn't intend for it to be xmm,m64 it will be, that'll teach the lazy user to forget opperand size
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //xmm,xmm/m32
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Imm8) && (paramType3 == ATokenType.Value))
                                    {
                                        AddOpCode(bytes, j);
                                        CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        Assembler.Add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm64) && (Assembler.IsXmm64(paramType2) | ((paramType2 == ATokenType.MemoryLocation32) && (parameter2[0] == '['))))
                                {
                                    //even if the user didn't intend for it to be xmm,m64 it will be, that'll teach the lazy user to forget opperand size
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //xmm,xmm/m64
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Imm8) && (paramType3 == ATokenType.Value))
                                    {
                                        AddOpCode(bytes, j);
                                        CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        Assembler.Add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm128) && (Assembler.IsXmm128(paramType2) | ((paramType2 == ATokenType.MemoryLocation32) && (parameter2[0] == '['))))
                                {
                                    //xmm,xmm/m128,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        //xmm,xmm/m128
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Imm8) && (paramType3 == ATokenType.Value))
                                    {
                                        //xmm,xmm/m128,imm8
                                        AddOpCode(bytes, j);
                                        CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        Assembler.Add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.M8:
                            if ((paramType1 == ATokenType.MemoryLocation8) | Assembler.IsMemoryLocationDefault(parameter1))
                            {
                                //m8,xxx
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    //m8
                                    //                                 //check if it is especially designed to be 32 bit, or if it is a default anser
                                    //verified, it is a 8 bit location, and if it was detected as 8 it was due to defaulting to 32
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.M16:
                            if ((paramType1 ==ATokenType.MemoryLocation16) | Assembler.IsMemoryLocationDefault(parameter1))
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    //opcode+rd
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.M32:
                            if (paramType1 == ATokenType.MemoryLocation32)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                                if (Assembler.OpCodes[j].ParamType2 == AParam.R32)
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm) & ((paramType2 == ATokenType.RegisterXmm) | Assembler.IsMemoryLocationDefault(parameter2)))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) || (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.M64:
                            if ((paramType1 == ATokenType.MemoryLocation64) || (paramType1 == ATokenType.MemoryLocation32))
                            {
                                //m64,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    //m64
                                    if ((Assembler.GetTokenType(ref parameter1, parameter2) == ATokenType.MemoryLocation64) | Assembler.IsMemoryLocationDefault(parameter1))
                                    {
                                        //verified, it is a 64 bit location, and if it was detected as 32 it was due to defaulting to 32
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm) && (paramType2 == ATokenType.RegisterXmm))
                                {
                                    //m64,xmm
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) || (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.M80:
                            if ((paramType1 == ATokenType.MemoryLocation80) || ((paramType1 == ATokenType.MemoryLocation32) && (parameter1[0] == '[')))
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.M128:
                            if ((paramType1 == ATokenType.MemoryLocation128) | (Assembler.IsMemoryLocationDefault(parameter1)))
                            {
                                //m128,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Xmm) && (paramType2 == ATokenType.RegisterXmm))
                                {
                                    //m128,xmm
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Xmm) && (paramType3 ==ATokenType.RegisterXmm))
                                    {
                                        //m128,xmm,xmm
                                        if ((Assembler.OpCodes[j].ParamType4 == AParam.None) && (parameter4 == ""))
                                        {
                                            if (Assembler.OpCodes[j].VexExtraParam == 2)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter2)) & 0xf;
                                                result = CreateModRm(bytes, GetReg(parameter3), parameter1);
                                                return result;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case AParam.M256:
                            if ((paramType1 ==ATokenType.MemoryLocation256) | (Assembler.IsMemoryLocationDefault(parameter1)))
                            {
                                //m256,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.Ymm) && (paramType2 ==ATokenType.RegisterYmm))
                                {
                                    //m256,ymm,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.Ymm) && (paramType3 ==ATokenType.RegisterYmm))
                                    {
                                        //m256,ymm,ymm
                                        if ((Assembler.OpCodes[j].ParamType4 == AParam.None) && (parameter4 == ""))
                                        {
                                            if (Assembler.OpCodes[j].VexExtraParam == 2)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter2)) & 0xf;
                                                result = CreateModRm(bytes, GetReg(parameter3), parameter1);
                                                return result;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case AParam.Rel8:
                            if (paramType1 == ATokenType.Value)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    //rel8
                                    if (AArrayUtils.InArray(parameter1[0], '-', '+'))
                                    {
                                        if (((!overrideShort) & (vType > 8)) | (overrideLong))
                                        {
                                            //see if there is a 32 bit equivalent opcode (notice I dont do rel 16 because that'll completely screw up eip)
                                            var k = startOfList;
                                            while ((k < Assembler.OpCodeCount) && (Assembler.OpCodes[k].Mnemonic == tokens[mnemonic]))
                                            {
                                                if ((Assembler.OpCodes[k].ParamType1 == AParam.Rel32) && (Assembler.OpCodes[k].ParamType2 == AParam.None))
                                                {
                                                    //yes, there is a 32 bit version
                                                    AddOpCode(bytes, k);
                                                    Assembler.AddDWord(bytes, (UInt32)v);
                                                    result = true;
                                                    return result;
                                                }
                                                k += 1;
                                            }
                                        }
                                        AddOpCode(bytes, j);
                                        Assembler.Add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                    else
                                    {
                                        //user typed in a direct address
                                        if ((!overrideShort) & ((overrideLong) | (Assembler.ValueToType((IntPtr)(v - address - (UInt64)(Assembler.OpCodes[j].Bytes + 1))) > 8)))
                                        {
                                            //the user tried to find a relative address out of it's reach
                                            //see if there is a 32 bit version of the opcode
                                            var k = startOfList;
                                            while ((k < Assembler.OpCodeCount) && (Assembler.OpCodes[k].Mnemonic == tokens[mnemonic]))
                                            {
                                                if ((Assembler.OpCodes[k].ParamType1 == AParam.Rel32) && (Assembler.OpCodes[k].ParamType2 == AParam.None))
                                                {
                                                    //yes, there is a 32 bit version
                                                    AddOpCode(bytes, k);
                                                    Assembler.AddDWord(bytes, (UInt32)(v - address - (UInt32)(Assembler.OpCodes[k].Bytes + 4)));
                                                    result = true;
                                                    return result;
                                                }
                                                k += 1;
                                            }
                                        }
                                        else
                                        {
                                            //8 bit version
                                            AddOpCode(bytes, j);
                                            var b = (Byte)((UInt32)(v - address - (UInt32)(Assembler.OpCodes[j].Bytes + 1)) & 0xff);
                                            // b:=b and $ff;
                                            Assembler.Add(bytes, b);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                            }
                            break;
                        case AParam.Rel32:
                            if (paramType1 == ATokenType.Value)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    if (AArrayUtils.InArray(parameter1[0], '-', '+'))
                                    {
                                        //opcode rel32
                                        AddOpCode(bytes, j);
                                        Assembler.AddDWord(bytes, (UInt32)v);
                                        result = true;
                                        return result;
                                    }
                                    else
                                    {
                                        //user typed in a direct address
                                        AddOpCode(bytes, j);
                                        Assembler.AddDWord(bytes, (UInt32)(v - address - (UInt32)(Assembler.OpCodes[j].Bytes + 4)));
                                        result = true;
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.St0:
                            if ((parameter1 == "ST(0)") || (parameter1 == "ST"))
                            {
                                //st(0),
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.St) && (paramType2 == ATokenType.RegisterSt))
                                {
                                    //st(0),st(x),
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Pi)
                                        {
                                            //opcode+i
                                            AddOpCode(bytes, j);
                                            var k = GetReg(parameter2);
                                            if (k > 7)
                                            {
                                                RexB = true;
                                                k &= 7;
                                            }
                                            bytes[bytes.Length - 1] += (Byte)k;
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                            }
                            break;
                        case AParam.St:
                            if (paramType1 == ATokenType.RegisterSt)
                            {
                                //st(x),
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.None) && (parameter2 == ""))
                                {
                                    //st(x)
                                    AddOpCode(bytes, j);
                                    var k = GetReg(parameter1);
                                    if (k > 7)
                                    {
                                        RexB = true;
                                        k &= 7;
                                    }
                                    bytes[bytes.Length - 1] += (Byte)k;
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.St0) && ((parameter2 == "ST(0)") || (parameter2 == "ST")))
                                {
                                    //st(x),st(0)
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.None) && (parameter3 == ""))
                                    {
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.Pi)
                                        {
                                            //opcode+i
                                            AddOpCode(bytes, j);
                                            var k = GetReg(parameter1);
                                            if (k > 7)
                                            {
                                                RexB = true;
                                                k &= 7;
                                            }
                                            bytes[bytes.Length - 1] += (Byte)k;
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    paramType1 = oldParamType1;
                    paramType2 = oldParamType2;
                    j += 1;
                }
            }
            finally
            {
                if (result)
                {
                    //insert rex prefix if needed
                    if (Assembler.SymbolHandler.Process.IsX64)
                    {
                        if (Assembler.OpCodes[j].W0)
                            RexW = false;
                        if (Assembler.OpCodes[j].W1)
                            RexW = true;
                        if (Assembler.OpCodes[j].HasVex)
                        {
                            //setup a vex prefix. Check if a 2 byte or 3 byte prefix is needed
                            //3 byte is needed when mmmmmm(vexLeadingOpcode>1) or rex.X/B or W are used
                            //vexOpcodeExtension: oe_F2; vexLeadingOpcode: lo_0f
                            var bigVex = (Assembler.OpCodes[j].VexLeadingOpCode > AVexLeadingOpCode.N0F) | RexB | RexX | RexW;
                            if (bigVex == false)
                            {
                                //2byte vex
                                bytes.SetLength(bytes.Length + 2);
                                for (i = bytes.Length - 1; i >= RexPrefixLocation + 2; i--)
                                    bytes[i] = bytes[i - 2];
                                bytes[RexPrefixLocation] = 0xc5; //2 byte VEX
                                var vex2 = new AVex2Byte(bytes, RexPrefixLocation + 1);
                                vex2.Pp = (Byte)Assembler.OpCodes[j].VexLeadingOpCode;
                                vex2.L = Assembler.OpCodes[j].VexL;
                                vex2.Vvvv = (Byte)vexvvvv;
                                vex2.R = (Byte)(RexR ? 0 : 1);
                                if (RelativeAddressLocation != -1)
                                    RelativeAddressLocation += 2;
                            }
                            else
                            {
                                //3byte vex
                                bytes.SetLength(bytes.Length + 3);
                                for (i = bytes.Length - 1; i >= RexPrefixLocation + 3; i--)
                                    bytes[i] = bytes[i - 3];
                                bytes[RexPrefixLocation] = 0xc4; //3 byte VEX
                                var vex3 = new AVex3Byte(bytes, RexPrefixLocation + 1);
                                vex3.Mmmmm = (Byte)(Assembler.OpCodes[j].VexLeadingOpCode);
                                vex3.B = (Byte)(RexB ? 0 : 1);
                                vex3.X = (Byte)(RexX ? 0 : 1); 
                                vex3.R = (Byte)(RexR ? 0 : 1);
                                vex3.Pp = (Byte)(Assembler.OpCodes[j].VexLeadingOpCode);
                                vex3.L = Assembler.OpCodes[j].VexL;
                                vex3.Vvvv = (Byte)vexvvvv;
                                vex3.W = (Byte)(RexW ? 1 : 0); //not inverted
                                if (RelativeAddressLocation != -1)
                                    RelativeAddressLocation += 3;
                            }
                            RexPrefix = 0;  //vex and rex can not co-exist
                        }
                        if (RexPrefix != 0)
                        {
                            if (RexPrefixLocation == -1)
                                throw new Exception("Assembler error");
                            RexPrefix = (Byte)(RexPrefix | 0x40); //just make sure this is set
                            bytes.SetLength(bytes.Length + 1);
                            for (i = bytes.Length - 1; i >= RexPrefixLocation + 1; i--)
                                bytes[i] = bytes[i - 1];
                            bytes[RexPrefixLocation] = RexPrefix;
                            if (RelativeAddressLocation != -1)
                                RelativeAddressLocation += 1;
                        }
                        if (RelativeAddressLocation != -1)
                        {
                            //adjust the specified address so it's relative (The outside of range check is already done in the modrm generation)
                            if (ActualDisplacement > (address + (UInt32)bytes.Length))
                                v = ActualDisplacement - (address + (UInt32)bytes.Length);
                            else
                                v = (address + (UInt32)bytes.Length) - ActualDisplacement;
                            if (v > 0x7fffffff)
                            {
                                bytes.SetLength(0);
                                if (skipRangeCheck == false)   //for syntax checking
                                    throw new Exception("offset too big");
                            }
                            else
                            {
                                using (var p = new UBytePtr(bytes.Buffer))
                                {
                                    var vp = (UInt32)(ActualDisplacement - (address + (UInt32)bytes.Length));
                                    p.WriteUInt32(vp, RelativeAddressLocation);
                                }
                            }
                        }
                    }
                    if (NeedsAddressSwitchPrefix)  //add it
                    {
                        if (candoaddressswitch)
                        {
                            //put 0x67 in front
                            bytes.SetLength(bytes.Length + 1);
                            for (i = bytes.Length - 1; i >= 1; i--)
                                bytes[i] = bytes[i - 1];
                            bytes[0] = 0x67;
                        }
                        else
                            throw new Exception("Invalid address");
                    }
                }
            }
            return result;
        }
        #endregion
    }
}
