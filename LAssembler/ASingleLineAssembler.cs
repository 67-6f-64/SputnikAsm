using System;
using Sputnik.LGenerics;
using Sputnik.LUtils;
using SputnikAsm.LAssembler.LEnums;
using SputnikAsm.LCollections;
using SputnikAsm.LSymbolHandler;
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
            if (Assembler.SymHandler.Process.IsX64)
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
            for (var i = 0; i < reg.Length; i++)
            {
                if (reg[i] == '*')
                {
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
                    if (reg.Length > i + 1)
                        throw new Exception("Invalid multiplier");
                    break;
                }
            }
            if (!hasMultiply)
                Assembler.SetSibScale(ref sib, 0);
            if (Assembler.SymHandler.Process.IsX64)
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
                    //in case addressswitch is needed
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
                else if ((reg == "") || (AStringUtils.Pos("ESP", reg) != -1))
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
        public void SetRm(ref Byte modrm, byte i)
        {
            modrm = (Byte)((modrm & 0xf8) | (i & 7));
            if (i > 7)
                RexB = true;
        }
        #endregion
        #region CreateModRm
        public Boolean CreateModRm(AByteArray bytes, int reg, String param)
        {
            String address;
            var modrm = new AByteArray();
            int i, j;
            modrm.EnsureCapacity(1);
            modrm[0] = 0;
            var brackStart = AStringUtils.Pos("[", param);
            var brackEnd = AStringUtils.Pos("]", param);
            if (brackStart != -1 && brackEnd != -1)
                address = AStringUtils.Copy(param, brackStart + 1, brackEnd - brackStart - 1);
            else
                address = "";
            if (address == "")
            {
                //register //modrm c0 to ff
                Assembler.SetMod(modrm, 0, 3);
                if (param == "RAX" || param == "EAX" || param == "AX" || param == "AL" || param == "MM0" || param == "XMM0" || param == "YMM0")
                    SetRm(modrm, 0, 0);
                else if (param == "RCX" || param == "ECX" || param == "CX" || param == "CL" || param == "MM1" || param == "XMM1" || param == "YMM1")
                    SetRm(modrm, 0, 1);
                else if (param == "RDX" || param == "EDX" || param == "DX" || param == "DL" || param == "MM2" || param == "XMM2" || param == "YMM2")
                    SetRm(modrm, 0, 2);
                else if (param == "RBX" || param == "EBX" || param == "BX" || param == "BL" || param == "MM3" || param == "XMM3" || param == "YMM3")
                    SetRm(modrm, 0, 3);
                else if (param == "SPL" || param == "RSP" || param == "ESP" || param == "SP" || param == "AH" || param == "MM4" || param == "XMM4" || param == "YMM4")
                    SetRm(modrm, 0, 4);
                else if (param == "BPL" || param == "RBP" || param == "EBP" || param == "BP" || param == "CH" || param == "MM5" || param == "XMM5" || param == "YMM5")
                    SetRm(modrm, 0, 5);
                else if (param == "SIL" || param == "RSI" || param == "ESI" || param == "SI" || param == "DH" || param == "MM6" || param == "XMM6" || param == "YMM6")
                    SetRm(modrm, 0, 6);
                else if (param == "DIL" || param == "RDI" || param == "EDI" || param == "DI" || param == "BH" || param == "MM7" || param == "XMM7" || param == "YMM7")
                    SetRm(modrm, 0, 7);
                else if (param == "R8" || param == "R8D" || param == "R8W" || param == "R8L" || param == "MM8" || param == "XMM8" || param == "YMM8")
                    SetRm(modrm, 0, 8);
                else if (param == "R9" || param == "R9D" || param == "R9W" || param == "R9L" || param == "MM9" || param == "XMM9" || param == "YMM9")
                    SetRm(modrm, 0, 9);
                else if (param == "R10" || param == "R10D" || param == "R10W" || param == "R10L" || param == "MM10" || param == "XMM10" || param == "YMM10")
                    SetRm(modrm, 0, 10);
                else if (param == "R11" || param == "R11D" || param == "R11W" || param == "R11L" || param == "MM11" || param == "XMM11" || param == "YMM11")
                    SetRm(modrm, 0, 11);
                else if (param == "R12" || param == "R12D" || param == "R12W" || param == "R12L" || param == "MM12" || param == "XMM12" || param == "YMM12")
                    SetRm(modrm, 0, 12);
                else if (param == "R13" || param == "R13D" || param == "R13W" || param == "R13L" || param == "MM13" || param == "XMM13" || param == "YMM13")
                    SetRm(modrm, 0, 13);
                else if (param == "R14" || param == "R14D" || param == "R14W" || param == "R14L" || param == "MM14" || param == "XMM14" || param == "YMM14")
                    SetRm(modrm, 0, 14);
                else if (param == "R15" || param == "R15D" || param == "R15W" || param == "R15L" || param == "MM15" || param == "XMM15" || param == "YMM15")
                    SetRm(modrm, 0, 15);
                else
                    throw new Exception("I don't understand what you mean with " + param);
            }
            else
                SetModRm(modrm, address, bytes.Length);
            //setreg
            if (reg > 7)
            {
                if (Assembler.SymHandler.Process.IsX64)
                    RexR = true;
                else
                    throw new Exception("The assembler tried to set a register value that is too high");
            }
            if (reg == -1)
                reg = 0;
            reg &= 7;
            modrm[0] = (Byte)(modrm[0] + (reg << 3));
            j = bytes.Length;
            bytes.EnsureCapacity(bytes.Length + modrm.Length);
            for (i = 0; i < modrm.Length; i++)
                bytes[j + i] = modrm[i];
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
        public void SetModRm(AByteArray modrm, String address, int offset)
        {
            var reg = new USafeDictionary<int, String>();
            var splitup = new AStringArray();
            var regs = "";
            var reg1 = "";
            var reg2 = "";
            var temp = "";
            var increase = false;
            var i = 0;
            var j = 0;
            var k = 0;
            var l = 0;
            UInt64 disp = 0;
            UInt64 test = 0;
            //first split the address string up
            splitup.SetLength(0);
            var found = false;
            var go = false;
            var start = 0;
            for (i = 0; i <= address.Length; i++)
            {
                if (i == address.Length)
                {
                    splitup.SetLength(splitup.Length + 1);
                    splitup[splitup.Length - 1] = AStringUtils.Copy(address, start, (i + 1) - start);
                }
                else if (!AArrayUtils.InArray(address[i], '+', '-'))
                    go = true;
                else if (AArrayUtils.InArray(address[i], '+', '-'))
                {
                    if (go)
                    {
                        splitup.SetLength(splitup.Length + 1);
                        splitup[splitup.Length - 1] = AStringUtils.Copy(address, start, i - start);
                        start = i; //copy the + or - sign
                        go = false;
                    }
                }
            }
            disp = 0;
            regs = "";
            for (i = 0; i < splitup.Length; i++)
            {
                increase = true;
                for (j = 0; j <= splitup[i].Length; j++)
                {
                    if (j < splitup[i].Length && AArrayUtils.InArray(splitup[i][j], '+', '-'))
                    {
                        if (splitup[i][j] == '-')
                            increase = !increase;
                    }
                    else
                    {
                        if (j == splitup[i].Length)
                            temp = AStringUtils.Copy(splitup[i], j, splitup[i].Length - j + 1);
                        else
                            temp = AStringUtils.Copy(splitup[i], j, splitup[i].Length - j + 1);
                        break;
                    }
                }
                if (temp.Length == 0)
                    throw new Exception("I don't understand what you mean with " + address);
                if (temp[0] == '$')
                    AStringUtils.Val(temp, out test, out j);
                else
                    AStringUtils.Val("0x" + temp, out test, out j);
                if (j > 0) //a register or a stupid user
                {
                    if (!increase)
                        throw new Exception("Negative registers can not be encoded");
                    regs = regs + temp + '+';
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
            //regs and disp are now set
            //compare the regs with posibilities     (only 1 time +, and only 1 time *)
            j = 0; k = 0;
            for (i = 0; i < regs.Length; i++)
            {
                if (regs[i] == '+')
                    j += 1;
                if (regs[i] == '*')
                    k += 1;
            }
            if ((j > 1) || (k > 1))
                throw new Exception("I don't understand what you mean with " + address);
            if (disp == 0)
                Assembler.SetMod(modrm, 0, 0);
            else if (((int)(disp) >= -128) && ((int)(disp) <= 127))
                Assembler.SetMod(modrm, 0, 1);
            else
                Assembler.SetMod(modrm, 0, 2);
            reg1 = "";
            reg2 = "";
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
            if ((reg1 != "") && (reg2 == "") && (AStringUtils.Pos("*", reg1) != -1))
            {
                Assembler.SetMod(modrm, 0, 0);
                SetRm(modrm, 0, 4);
                modrm.SetLength(2);
                SetSibBase(modrm, 1, 5);
                CreateSibScaleIndex(modrm, 1, reg[-1]);
                Assembler.AddDWord(modrm, (UInt32)disp);
                found = true;
            }
            if ((reg[k] == "") && (reg[-k] == ""))
            {
                //no registers, just a address
                SetRm(modrm, 0, 5);
                Assembler.SetMod(modrm, 0, 0);
                if (Assembler.SymHandler.Process.IsX64)
                {
                    if ((disp <= 0x7fffffff) && Math.Abs((Int64)(FAddress - disp)) > 0x7ffffff0)  //rough estimate
                    {
                        //this can be solved with an 0x25 SIB byte
                        modrm.SetLength(2);
                        SetRm(modrm, 0, 4);
                        SetSibBase(modrm, 1, 5); //no base
                        SetSibIndex(modrm, 1, 4);
                        Assembler.SetSibScale(modrm, 1, 0);
                    }
                    else
                    {
                        ActualDisplacement = disp;
                        RelativeAddressLocation = offset + 1;
                    }
                }
                Assembler.AddDWord(modrm, (UInt32)disp);
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
                    SetRm(modrm, 0, 4);
                    modrm.SetLength(2);
                    SetSibBase(modrm, 1, 4);
                    CreateSibScaleIndex(modrm, 1, reg[-k]);
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
                        SetRm(modrm, 0, 4);
                        modrm.SetLength(2);
                        SetSibBase(modrm, 1, 0);
                        CreateSibScaleIndex(modrm, 1, reg[-k]);
                    }
                    else
                        SetRm(modrm, 0, 0); //no sib needed
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
                        SetRm(modrm, 0, 4);
                        modrm.SetLength(2);
                        SetSibBase(modrm, 1, 1);
                        CreateSibScaleIndex(modrm, 1, reg[-k]);
                    }
                    else
                        SetRm(modrm, 0, 1); //no sib needed
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
                        SetRm(modrm, 0, 4);
                        modrm.SetLength(2);
                        SetSibBase(modrm, 1, 2);
                        CreateSibScaleIndex(modrm, 1, reg[-k]);
                    }
                    else
                        SetRm(modrm, 0, 2); //no sib needed
                    found = true;
                    return;
                }
                if ((reg[k] == "EBX") || (reg[-k] == "EBX") || (reg[k] == "RBX") || (reg[-k] == "RBX"))
                {
                    if (reg[-k] == "EBX") k = -k;
                    if (reg[-k] == "RBX") k = -k;

                    if (reg[-k] != "")  //sib needed
                    {
                        SetRm(modrm, 0, 4);
                        modrm.SetLength(2);
                        SetSibBase(modrm, 1, 3);
                        CreateSibScaleIndex(modrm, 1, reg[-k]);
                    }
                    else
                        SetRm(modrm, 0, 3); //no sib needed
                    found = true;
                    return;
                }
                if ((reg[k] == "ESP") || (reg[-k] == "ESP") || (reg[k] == "RSP") || (reg[-k] == "RSP"))
                {
                    if (reg[-k] == "ESP")
                        k = -k;
                    if (reg[-k] == "RSP")
                        k = -k;
                    SetRm(modrm, 0, 4);
                    modrm.SetLength(2);
                    SetSibBase(modrm, 1, 4);
                    CreateSibScaleIndex(modrm, 1, reg[-k]);
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
                        Assembler.SetMod(modrm, 0, 1);
                    if (reg[-k] != "")  //sib needed
                    {
                        SetRm(modrm, 0, 4);
                        modrm.SetLength(2);
                        SetSibBase(modrm, 1, 5);
                        CreateSibScaleIndex(modrm, 1, reg[-k]);
                    }
                    else
                        SetRm(modrm, 0, 5); //no sib needed
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
                        SetRm(modrm, 0, 4);
                        modrm.SetLength(2);
                        SetSibBase(modrm, 1, 6);
                        CreateSibScaleIndex(modrm, 1, reg[-k]);
                    }
                    else
                        SetRm(modrm, 0, 6); //no sib needed
                    found = true;
                    return;
                }
                if ((reg[k] == "EDI") || (reg[-k] == "EDI") || (reg[k] == "RDI") || (reg[-k] == "RDI"))
                {
                    if (reg[-k] == "EDI") k = -k;
                    if (reg[-k] == "RDI") k = -k;

                    if (reg[-k] != "")  //sib needed
                    {
                        SetRm(modrm, 0, 4);
                        modrm.SetLength(2);
                        SetSibBase(modrm, 1, 7);
                        CreateSibScaleIndex(modrm, 1, reg[-k]);
                    }
                    else
                        SetRm(modrm, 0, 7); //no sib needed
                    found = true;
                    return;
                }
                if (Assembler.SymHandler.Process.IsX64)
                {
                    if ((reg[k] == "R8") || (reg[-k] == "R8"))
                    {
                        if (reg[-k] == "R8")
                            k = -k;
                        if (reg[-k] != "")  //sib needed
                        {
                            SetRm(modrm, 0, 4);
                            modrm.SetLength(2);
                            SetSibBase(modrm, 1, 8);
                            CreateSibScaleIndex(modrm, 1, reg[-k]);
                        }
                        else
                            SetRm(modrm, 0, 8); //no sib needed
                        found = true;
                        return;
                    }
                    if ((reg[k] == "R9") || (reg[-k] == "R9"))
                    {
                        if (reg[-k] == "R9")
                            k = -k;
                        if (reg[-k] != "")  //sib needed
                        {
                            SetRm(modrm, 0, 4);
                            modrm.SetLength(2);
                            SetSibBase(modrm, 1, 9);
                            CreateSibScaleIndex(modrm, 1, reg[-k]);
                        }
                        else
                            SetRm(modrm, 0, 9); //no sib needed
                        found = true;
                        return;
                    }
                    if ((reg[k] == "R10") || (reg[-k] == "R10"))
                    {
                        if (reg[-k] == "R10")
                            k = -k;
                        if (reg[-k] != "")  //sib needed
                        {
                            SetRm(modrm, 0, 4);
                            modrm.SetLength(2);
                            SetSibBase(modrm, 1, 10);
                            CreateSibScaleIndex(modrm, 1, reg[-k]);
                        }
                        else
                            SetRm(modrm, 0, 10); //no sib needed
                        found = true;
                        return;
                    }
                    if ((reg[k] == "R11") || (reg[-k] == "R11"))
                    {
                        if (reg[-k] == "R11")
                            k = -k;
                        if (reg[-k] != "")  //sib needed
                        {
                            SetRm(modrm, 0, 4);
                            modrm.SetLength(2);
                            SetSibBase(modrm, 1, 11);
                            CreateSibScaleIndex(modrm, 1, reg[-k]);
                        }
                        else
                            SetRm(modrm, 0, 11); //no sib needed
                        found = true;
                        return;
                    }
                    if ((reg[k] == "R12") || (reg[-k] == "R12"))
                    {
                        if (reg[-k] == "R12")
                            k = -k;
                        SetRm(modrm, 0, 4);
                        modrm.SetLength(2);
                        SetSibBase(modrm, 1, 12);
                        CreateSibScaleIndex(modrm, 1, reg[-k]);
                        found = true;
                        return;
                    }
                    if ((reg[k] == "R13") || (reg[-k] == "R13"))
                    {
                        if (reg[-k] == "R13")
                            k = -k;
                        if (disp == 0)
                            Assembler.SetMod(modrm, 0, 1);
                        if (reg[-k] != "")  //sib needed
                        {
                            SetRm(modrm, 0, 4);
                            modrm.SetLength(2);
                            SetSibBase(modrm, 1, 13);
                            CreateSibScaleIndex(modrm, 1, reg[-k]);
                        }
                        else
                            SetRm(modrm, 0, 13); //no sib needed
                        found = true;
                        return;
                    }
                    if ((reg[k] == "R14") || (reg[-k] == "R14"))
                    {
                        if (reg[-k] == "R14")
                            k = -k;
                        if (reg[-k] != "")  //sib needed
                        {
                            SetRm(modrm, 0, 4);
                            modrm.SetLength(2);
                            SetSibBase(modrm, 1, 14);
                            CreateSibScaleIndex(modrm, 1, reg[-k]);
                        }
                        else
                            SetRm(modrm, 0, 14); //no sib needed
                        found = true;
                        return;
                    }
                    if ((reg[k] == "R15") || (reg[-k] == "R15"))
                    {
                        if (reg[-k] == "R15")
                            k = -k;
                        if (reg[-k] != "")  //sib needed
                        {
                            SetRm(modrm, 0, 4);
                            modrm.SetLength(2);
                            SetSibBase(modrm, 1, 15);
                            CreateSibScaleIndex(modrm, 1, reg[-k]);
                        }
                        else
                            SetRm(modrm, 0, 15); //no sib needed
                        found = true;
                        return;
                    }
                }
            }
            finally
            {
                if (!found)
                    throw new Exception("Invalid address");
                i = Assembler.GetMod(modrm[0]);
                if (i == 1)
                    Assembler.Add(modrm, (Byte) disp);
                if (i == 2)
                    Assembler.AddDWord(modrm, (UInt32) disp);
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
            //    newv = qword(0) | v;
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
        public Boolean Assemble(String opCode, UInt64 address, AByteArray bytes, AAssemblerPreference aPref = AAssemblerPreference.apnone, Boolean skipRangeCheck = false)
        {
            var tokens = new AStringArray();
            var i = 0;
            var j = 0;
            UInt64 v = 0;
            UInt64 v2 = 0;
            UInt64 newv = 0;
            var mnemonic = 0;
            var nroftokens = 0;
            var paramtype1 = ATokenType.ttinvalidtoken;
            var paramtype2 = ATokenType.ttinvalidtoken;
            var paramtype3 = ATokenType.ttinvalidtoken;
            var paramtype4 = ATokenType.ttinvalidtoken;
            var parameter1 = "";
            var parameter2 = "";
            var parameter3 = "";
            var parameter4 = "";
            var oldParamtype1 = ATokenType.ttinvalidtoken;
            var oldParamtype2 = ATokenType.ttinvalidtoken;
            var vtype = 0;
            var v2type = 0;
            var signedvtype = 0;
            var signedv2type = 0;
            //first,last: integer;
            var startoflist = 0;
            var endoflist = 0;
            var tempstring = "";
            var overrideshort = false;
            var overridelong = false;
            var overridefar = false;
            var is64bit = Assembler.SymHandler.Process.IsX64;
            Byte b = 0;
            var br = UIntPtr.Zero;
            var candoaddressswitch = false;
            var bigvex = false;
            var vexvvvv = 0xf;
            var cannotencodewithrexw = false;
            FAddress = address;
            RelativeAddressLocation = -1;
            RexPrefix = 0;
            var result = false;
            Assembler.Tokenize(opCode, tokens);
            nroftokens = tokens.Length;
            if (nroftokens == 0)
                return false;
            switch (tokens[0][0])
            {
                case 'A':  //A* //allign
                    {
                        if (tokens[0] == "ALIGN")
                        {
                            if (nroftokens >= 2)
                            {
                                i = AStringUtils.HexStrToInt(tokens[1]);
                                if (nroftokens >= 3)
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
                            for (i = 1; i < nroftokens; i++)
                            {
                                if (tokens[i][0] == '\'')  //string
                                {
                                    //find the original non uppercase string pos in the opcode
                                    j = AStringUtils.Pos(tokens[i], opCode.ToUpper());
                                    if (j != -1)
                                    {
                                        tempstring = AStringUtils.Copy(opCode, j, tokens[i].Length);
                                        Assembler.AddString(bytes, tempstring);
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
                                        b = (Byte)Assembler.SymHandler.Process.ReadMem((IntPtr)(address + (UInt64)i - 1), ReadType.Byte, 1);
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
                            for (i = 1; i < nroftokens; i++)
                            {
                                if (tokens[i][0] == '\'')  //string
                                {
                                    j = AStringUtils.Pos(tokens[i], opCode.ToUpper());
                                    if (j != -1)
                                    {
                                        tempstring = AStringUtils.Copy(opCode, j, tokens[i].Length);
                                        Assembler.AddWideString(bytes, tempstring);
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
                            for (i = 1; i < nroftokens; i++)
                                Assembler.AddDWord(bytes, (UInt32)AStringUtils.HexStrToInt(tokens[i]));
                            result = true;
                            return true;
                        }
                        if (tokens[0] == "DQ")
                        {
                            for (i = 1; i < nroftokens; i++)
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
            mnemonic = -1;
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
            if ((nroftokens - 1) >= mnemonic + 1)
                parameter1 = tokens[mnemonic + 1];
            else
                parameter1 = "";
            if ((nroftokens - 1) >= mnemonic + 2)
                parameter2 = tokens[mnemonic + 2];
            else
                parameter2 = "";
            if ((nroftokens - 1) >= mnemonic + 3)
                parameter3 = tokens[mnemonic + 3];
            else
                parameter3 = "";
            if ((nroftokens - 1) >= mnemonic + 4)
                parameter4 = tokens[mnemonic + 4];
            else
                parameter4 = "";
            overrideshort = AStringUtils.Pos("SHORT ", parameter1, true) != -1;
            overridelong = (AStringUtils.Pos("LONG ", parameter1, true) != -1);
            if (Assembler.SymHandler.Process.IsX64)
                overridefar = (AStringUtils.Pos("FAR ", parameter1, true) != -1);
            else
                overridelong |= (AStringUtils.Pos("FAR ", parameter1, true) != -1);
            if (!(overrideshort | overridelong | overridefar) & (aPref != AAssemblerPreference.apnone))  //no override choice by the user and not a normal preference
            {
                if (aPref == AAssemblerPreference.apfar)
                    overridefar = true;
                if (aPref == AAssemblerPreference.aplong)
                    overridelong = true;
                else if (aPref == AAssemblerPreference.apshort)
                    overrideshort = true;
            }
            paramtype1 = Assembler.GetTokenType(ref parameter1, parameter2);
            paramtype2 = Assembler.GetTokenType(ref parameter2, parameter1);
            paramtype3 = Assembler.GetTokenType(ref parameter3, "");
            paramtype4 = Assembler.GetTokenType(ref parameter4, "");
            if (Assembler.SymHandler.Process.IsX64)
            {
                if (paramtype1 == ATokenType.ttregister8bitwithprefix)
                {
                    RexPrefix = (Byte)(RexPrefix | 0x40); //it at least has a prefix now
                    paramtype1 = ATokenType.ttregister8bit;
                }
                if (paramtype2 == ATokenType.ttregister8bitwithprefix)
                {
                    RexPrefix = (Byte)(RexPrefix | 0x40); //it at least has a prefix now
                    paramtype2 = ATokenType.ttregister8bit;
                }
                if (paramtype1 == ATokenType.ttregister64bit)
                {
                    RexW = true;   //64-bit opperand
                    paramtype1 = ATokenType.ttregister32bit; //we can use the normal 32-bit interpretation assembler code
                }
                if (paramtype2 == ATokenType.ttregister64bit)
                {
                    RexW = true;
                    paramtype2 = ATokenType.ttregister32bit;
                }
                if (paramtype3 == ATokenType.ttregister64bit)
                {
                    RexW = true;
                    paramtype3 = ATokenType.ttregister32bit;
                }
                if (paramtype1 == ATokenType.ttmemorylocation64)
                {
                    RexW = true;
                    paramtype1 = ATokenType.ttmemorylocation32;
                }
                if (paramtype2 == ATokenType.ttmemorylocation64)
                {
                    RexW = true;
                    paramtype2 = ATokenType.ttmemorylocation32;
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
                    var bt = (Byte[])Assembler.SymHandler.Process.ReadMem((IntPtr)address, ReadType.Binary, i);
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
            if ((paramtype1 >= ATokenType.ttmemorylocation) && (paramtype1 <= ATokenType.ttmemorylocation128))
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
            if ((paramtype2 >= ATokenType.ttmemorylocation) && (paramtype2 <= ATokenType.ttmemorylocation128))
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
            vtype = 0;
            v2type = 0;
            if (paramtype1 == ATokenType.ttvalue)
            {
                v = AStringUtils.StrToQWordEx(parameter1);
                vtype = Assembler.StringValueToType(parameter1);
            }
            if (paramtype2 == ATokenType.ttvalue)
            {
                if (paramtype1 != ATokenType.ttvalue)
                {
                    v = AStringUtils.StrToQWordEx(parameter2);
                    vtype = Assembler.StringValueToType(parameter2);
                }
                else
                {
                    //first v field is already in use, use v2
                    v2 = AStringUtils.StrToQWordEx(parameter2);
                    v2type = Assembler.StringValueToType(parameter2);
                }
            }
            if (paramtype3 == ATokenType.ttvalue)
            {
                if (paramtype1 != ATokenType.ttvalue)
                {
                    v = AStringUtils.StrToQWordEx(parameter3);
                    vtype = Assembler.StringValueToType(parameter3);
                }
                else
                {
                    //first v field is already in use, use v2
                    v2 = AStringUtils.StrToQWordEx(parameter3);
                    v2type = Assembler.StringValueToType(parameter3);
                }
            }
            if (paramtype4 == ATokenType.ttvalue)
            {
                v = AStringUtils.StrToQWordEx(parameter4);
                vtype = Assembler.StringValueToType(parameter4);
            }
            signedvtype = Assembler.SignedValueToType((IntPtr)v);
            signedv2type = Assembler.SignedValueToType((IntPtr)v2);
            result = false;
            //to make it easier for people that don't like the relative addressing limit
            if ((!overrideshort) & (!overridelong) & (Assembler.SymHandler.Process.IsX64))    //if 64-bit and no override is given
            {
                //check if this is a jmp or call with relative value
                if ((tokens[mnemonic] == "JMP") || (tokens[mnemonic] == "CALL"))
                {
                    if (paramtype1 == ATokenType.ttvalue)
                    {
                        //if the relative distance is too big, then replace with with jmp/call [2], jmp+8, DQ address
                        if (address > v)
                            v2 = address - v;
                        else
                            v2 = v - address;
                        if ((v2 > 0x7fffffff) | (overridefar))  //the user WANTS it to be called as a 'far' jump even if it's not needed
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
            startoflist = j;
            endoflist = startoflist;
            while (endoflist < Assembler.OpCodeCount && Assembler.OpCodes[endoflist].Mnemonic == tokens[mnemonic])
                endoflist += 1;
            endoflist -= 1;
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
                    oldParamtype1 = paramtype1;
                    oldParamtype2 = paramtype2;
                    if (Assembler.OpCodes[j].W0)
                    {
                        //undo rex_w change
                        if (paramtype1 == ATokenType.ttmemorylocation32)
                            paramtype1 = Assembler.GetTokenType(ref parameter1, parameter2);
                        if (paramtype2 == ATokenType.ttmemorylocation32)
                            paramtype2 = Assembler.GetTokenType(ref parameter2, parameter3);
                    }
                    candoaddressswitch = Assembler.OpCodes[j].CanDoAddressSwitch;
                    switch (Assembler.OpCodes[j].ParamType1)
                    {
                        case AParam.par_noparam:
                            if (parameter1 == "")      //no param
                            {
                                //no param
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //no_param,no_param,no_param
                                        if ((Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_none) && (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_none))
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
                        case AParam.par_imm8:
                            if (paramtype1 == ATokenType.ttvalue)
                            {
                                //imm8,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_al) && (parameter2 == "AL"))
                                {
                                    //imm8,al
                                    AddOpCode(bytes, j);
                                    Assembler.Add(bytes, (Byte)v);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_ax) && (parameter2 == "AX"))
                                {
                                    //imm8,ax /?
                                    AddOpCode(bytes, j);
                                    Assembler.Add(bytes, (Byte)v);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_eax) && ((parameter2 == "EAX") || (parameter2 == "RAX")))
                                {
                                    //imm8,eax
                                    AddOpCode(bytes, j);
                                    Assembler.Add(bytes, (Byte)v);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    if (vtype == 16)
                                    {
                                        //see if there is also a 'opcode imm16' variant
                                        var k = startoflist;
                                        while ((k < Assembler.OpCodeCount) && (Assembler.OpCodes[k].Mnemonic == tokens[mnemonic]))
                                        {
                                            if (Assembler.OpCodes[k].ParamType1 == AParam.par_imm16)
                                            {
                                                AddOpCode(bytes, k);
                                                Assembler.AddWord(bytes, (UInt16)v);
                                                result = true;
                                                return result;
                                            }
                                            k += 1;
                                        }
                                    }
                                    if ((vtype == 32) || (signedvtype > 8))
                                    {
                                        //see if there is also a 'opcode imm32' variant
                                        var k = startoflist;
                                        while ((k < Assembler.OpCodeCount) && (Assembler.OpCodes[k].Mnemonic == tokens[mnemonic]))
                                        {
                                            if (Assembler.OpCodes[k].ParamType1 == AParam.par_imm32)
                                            {
                                                if ((signedvtype == 64) & RexW)
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
                        case AParam.par_imm16:
                            if (paramtype1 == ATokenType.ttvalue)
                            {
                                //imm16,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //imm16
                                    if ((vtype == 32) || (signedvtype > 8))
                                    {
                                        //see if there is also a 'opcode imm32' variant
                                        var k = startoflist;
                                        while (k < Assembler.OpCodeCount && Assembler.OpCodes[k].Mnemonic == tokens[mnemonic])
                                        {
                                            if (Assembler.OpCodes[k].ParamType1 == AParam.par_imm32)
                                            {
                                                if ((signedvtype == 64) & RexW)
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //imm16,imm8,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
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
                        case AParam.par_imm32:
                            if (paramtype1 == ATokenType.ttvalue)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //imm32
                                    if ((signedvtype == 64) & RexW)
                                        Invalid64BitValueFor32BitField(v);
                                    AddOpCode(bytes, j);
                                    Assembler.AddDWord(bytes, (UInt32)v);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_moffs8:
                            if ((paramtype1 == ATokenType.ttmemorylocation8) | (Assembler.IsMemoryLocationDefault(parameter1)))
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_al) && (parameter2 == "AL"))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        var k = AStringUtils.Pos("[", parameter1);
                                        var l = AStringUtils.Pos("]", parameter1);
                                        AStringUtils.Val("0x" + AStringUtils.Copy(parameter1, k + 1, l - k - 1), out v, out k);
                                        if (k == 0)
                                        {
                                            //verified, it doesn't have a register base in it
                                            AddOpCode(bytes, j);
                                            if (Assembler.SymHandler.Process.IsX64)
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
                        case AParam.par_moffs16:
                            if ((paramtype1 == ATokenType.ttmemorylocation16) | (Assembler.IsMemoryLocationDefault(parameter1)))
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_ax) && (parameter2 == "AX"))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        var k = AStringUtils.Pos("[", parameter1);
                                        var l = AStringUtils.Pos("]", parameter1);
                                        AStringUtils.Val("0x" + AStringUtils.Copy(parameter1, k + 1, l - k - 1), out v, out k);
                                        if (k == 0)
                                        {
                                            //verified, it doesn't have a register base in it
                                            AddOpCode(bytes, j);
                                            if (Assembler.SymHandler.Process.IsX64)
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
                        case AParam.par_moffs32:
                            if (paramtype1 == ATokenType.ttmemorylocation32)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_eax) && ((parameter2 == "EAX") || (parameter2 == "RAX")))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        var k = AStringUtils.Pos("[", parameter1);
                                        var l = AStringUtils.Pos("]", parameter1);
                                        AStringUtils.Val("0x" + AStringUtils.Copy(parameter1, k + l, l - k - 1), out v, out k);
                                        if (k == 0)
                                        {
                                            //verified, it doesn't have a registerbase in it
                                            AddOpCode(bytes, j);
                                            if (Assembler.SymHandler.Process.IsX64)
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
                        case AParam.par_3:
                            if ((paramtype1 == ATokenType.ttvalue) && (v == 3))
                            {
                                //int 3
                                AddOpCode(bytes, j);
                                result = true;
                                return result;
                            }
                            break;
                        case AParam.par_al:
                            if (parameter1 == "AL")
                            {
                                //AL,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_dx) && (parameter2 == "DX"))
                                {
                                    //opcode al,dx
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //AL,imm8
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if ((Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_ib) && (Assembler.OpCodes[j].OpCode2 == AExtraOpCode.eo_none))
                                        {
                                            //verified: AL,imm8
                                            AddOpCode(bytes, j);
                                            Assembler.Add(bytes, (Byte)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_moffs8) & ((paramtype2 == ATokenType.ttmemorylocation8) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        var k = AStringUtils.Pos("[", parameter2);
                                        var l = AStringUtils.Pos("]", parameter2);
                                        AStringUtils.Val("0x" + AStringUtils.Copy(parameter2, k + l, l - k - 1), out v, out k);
                                        if (k == 0)
                                        {
                                            //verified, it doesn't have a registerbase in it
                                            AddOpCode(bytes, j);
                                            if (Assembler.SymHandler.Process.IsX64)
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
                        case AParam.par_ax:
                            if (parameter1 == "AX")
                            {
                                //AX,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //opcode AX
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_dx) && (parameter2 == "DX"))
                                {
                                    //opcode ax,dx
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                                //r16
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_r16) && (paramtype2 == ATokenType.ttregister16bit))
                                {
                                    //eax,r32
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //r32,eax
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_prw)
                                        {
                                            //opcode+rd
                                            AddOpCode(bytes, j);
                                            bytes[bytes.Length - 1] += (Byte)GetReg(parameter2);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_imm16) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //AX,imm16
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //params confirmed it is a ax,imm16
                                        if ((Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_iw) && (Assembler.OpCodes[j].OpCode2 == AExtraOpCode.eo_none))
                                        {
                                            AddOpCode(bytes, j);
                                            Assembler.AddWord(bytes, (UInt16)(v));
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_moffs16) & ((paramtype2 == ATokenType.ttmemorylocation16) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        var k = AStringUtils.Pos("[", parameter2);
                                        var l = AStringUtils.Pos("]", parameter2);
                                        AStringUtils.Val("0x" + AStringUtils.Copy(parameter2, k + l, l - k - 1), out v, out k);
                                        if (k == 0)
                                        {
                                            //verified, it doesn't have a registerbase in it
                                            AddOpCode(bytes, j);
                                            if (Assembler.SymHandler.Process.IsX64)
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
                        case AParam.par_eax:
                            if ((parameter1 == "EAX") || (parameter1 == "RAX"))
                            {
                                //eAX,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_dx) && (parameter2 == "DX"))
                                {
                                    //opcode eax,dx
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                                //r32
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_r32) && (paramtype2 == ATokenType.ttregister32bit))
                                {
                                    //eax,r32
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //r32,eax
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_prd)
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //eax,imm8
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        Assembler.Add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_imm32) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //EAX,imm32,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //eax,imm32
                                        if (signedvtype == 8)
                                        {
                                            //check if there isn't a rm32,imm8 , since that's less bytes
                                            var k = startoflist;
                                            while ((k < Assembler.OpCodeCount) && (Assembler.OpCodes[k].Mnemonic == tokens[mnemonic]))
                                            {
                                                if ((Assembler.OpCodes[k].ParamType1 == AParam.par_rm32) &&
                                                   (Assembler.OpCodes[k].ParamType2 == AParam.par_imm8))
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
                                        if ((Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_id) && (Assembler.OpCodes[j].OpCode2 == AExtraOpCode.eo_none))
                                        {
                                            if ((signedvtype == 64) & RexW)
                                                Invalid64BitValueFor32BitField(v);
                                            AddOpCode(bytes, j);
                                            Assembler.AddDWord(bytes, (UInt32)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_moffs32) & ((paramtype2 == ATokenType.ttmemorylocation32) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        var k = AStringUtils.Pos("[", parameter2);
                                        var l = AStringUtils.Pos("]", parameter2);
                                        AStringUtils.Val("0x" + AStringUtils.Copy(parameter2, k + 1, l - k - 1), out v, out k);
                                        if (k == 0)
                                        {
                                            //verified, it doesn't have a registerbase in it
                                            AddOpCode(bytes, j);
                                            if (Assembler.SymHandler.Process.IsX64)
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
                        case AParam.par_dx:
                            if (parameter1 == "DX")
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_al) && (parameter2 == "AL"))
                                {
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_ax) && (parameter2 == "AX"))
                                {
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_eax) && ((parameter2 == "EAX") || (parameter2 == "RAX")))
                                {
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_cs:
                            if (parameter1 == "CS")
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_ds:
                            if (parameter1 == "DS")
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_es:
                            if (parameter1 == "ES")
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_ss:
                            if (parameter1 == "SS")
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_fs:
                            if (parameter1 == "FS")
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;

                        case AParam.par_gs:
                            if (parameter1 == "GS")
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    AddOpCode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_r8:
                            if (paramtype1 == ATokenType.ttregister8bit)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //opcode r8
                                    if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_prb)
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //r8, imm8
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_prb)
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_rm8) & (Assembler.IsMem8(paramtype2)))
                                {
                                    //r8,rm8
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.par_r16:
                            if (paramtype1 == ATokenType.ttregister16bit)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //opcode r16
                                    if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_prw)
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_ax) && (parameter2 == "AX"))
                                {
                                    //r16,ax,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //r16,ax
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_prw)
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //r16, imm8
                                    if ((Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_reg) && (Assembler.OpCodes[j].OpCode2 == AExtraOpCode.eo_ib))
                                    {
                                        if (vtype > 8)
                                        {
                                            //search for r16/imm16
                                            var k = startoflist;
                                            while ((k < Assembler.OpCodeCount) && (Assembler.OpCodes[k].Mnemonic == tokens[mnemonic]))
                                            {
                                                if ((Assembler.OpCodes[k].ParamType1 == AParam.par_r16) &&
                                                   (Assembler.OpCodes[k].ParamType2 == AParam.par_imm16))
                                                {
                                                    if ((Assembler.OpCodes[k].OpCode1 == AExtraOpCode.eo_reg) && (Assembler.OpCodes[j].OpCode2 == AExtraOpCode.eo_ib))
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_imm16) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_prw)
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_rm8) & (Assembler.IsMem8(paramtype2)))
                                {
                                    //r16,r/m8 (eg: movzx)
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_rm16) & (Assembler.IsMem16(paramtype2)))
                                {
                                    //r16,r/m16
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
                                    {
                                        if (Assembler.OpCodes[j].OpCode2 == AExtraOpCode.eo_ib)
                                        {
                                            //r16,r/m16,imm8
                                            if (vtype > 8)
                                            {
                                                //see if there is a //r16,r/m16,imm16
                                                var k = startoflist;
                                                while ((k < Assembler.OpCodeCount) && (Assembler.OpCodes[k].Mnemonic == tokens[mnemonic]))
                                                {
                                                    if ((Assembler.OpCodes[k].ParamType1 == AParam.par_r16) &&
                                                       (Assembler.OpCodes[k].ParamType2 == AParam.par_rm16) &&
                                                       (Assembler.OpCodes[k].ParamType3 == AParam.par_imm16))
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
                        case AParam.par_r32:
                            if (paramtype1 == ATokenType.ttregister32bit)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //opcode r32
                                    if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_prd)
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_r32) && (paramtype2 == ATokenType.ttregister32bit))
                                {
                                    //r32,r32,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_rm32) & (Assembler.IsMem32(paramtype3)))
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
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_m32) && (paramtype3 == ATokenType.ttmemorylocation32))
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_eax) && ((parameter2 == "EAX") || (parameter2 == "RAX")))
                                {
                                    //r32,eax,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //r32,eax
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_prd)
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_mm) && (paramtype2 == ATokenType.ttregistermm))
                                {
                                    //r32, mm,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
                                    {
                                        //r32, mm,imm8
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_ymm) && (paramtype2 == ATokenType.ttregisterymm))
                                {
                                    //r32,ymm,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm) && (paramtype2 == ATokenType.ttregisterxmm))
                                {
                                    //r32,xmm,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }

                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_cr) && (paramtype2 == ATokenType.ttregistercr))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_dr) && (paramtype2 == ATokenType.ttregisterdr))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                }

                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm_m32) & (Assembler.IsXmm32(paramtype2)))
                                {
                                    //r32,xmm/m32
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_mm_m64) && (Assembler.IsRmm64(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[0] == '['))))
                                {
                                    //r32,mm/m64
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm_m64) && (Assembler.IsXmm64(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[0] == '['))))
                                {
                                    //r32,xmm/m64
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm_m128) && (Assembler.IsXmm64(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[0] == '['))))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_m32) && (paramtype2 == ATokenType.ttmemorylocation32))
                                {
                                    //r32,m32,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //r32,m32
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_m128) & ((paramtype2 == ATokenType.ttmemorylocation128) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    //r32,m128,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //r32,m128
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_rm8) & (Assembler.IsMem8(paramtype2) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    //r32,rm8
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_rm16) & (Assembler.IsMem16(paramtype2) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    //r32,rm16
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_rm32) & (Assembler.IsMem32(paramtype2)))
                                {
                                    //r32,r/m32
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_reg)
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
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
                                    {
                                        if (Assembler.OpCodes[j].OpCode2 == AExtraOpCode.eo_ib)
                                        {
                                            if (vtype > 8)
                                            {
                                                var k = startoflist;
                                                while ((k <= endoflist) && (Assembler.OpCodes[k].Mnemonic == tokens[mnemonic]))
                                                {
                                                    if ((Assembler.OpCodes[k].ParamType1 == AParam.par_r32) &&
                                                       (Assembler.OpCodes[k].ParamType2 == AParam.par_rm32) &&
                                                       (Assembler.OpCodes[k].ParamType3 == AParam.par_imm32))
                                                    {
                                                        if ((signedvtype == 64) & RexW)
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
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.par_r32)
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_imm32) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //r32,imm32
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_prd)
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
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_reg)   //probably imul reg,imm32
                                        {
                                            if (signedvtype == 8)
                                            {
                                                var k = startoflist;
                                                while ((k <= endoflist) && (Assembler.OpCodes[k].Mnemonic == tokens[mnemonic]))  //check for an reg,imm8
                                                {
                                                    if ((Assembler.OpCodes[k].ParamType1 == AParam.par_r32) &&
                                                        (Assembler.OpCodes[k].ParamType2 == AParam.par_imm8))
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
                                            if ((signedvtype == 64) & RexW)
                                                Invalid64BitValueFor32BitField(v);
                                            AddOpCode(bytes, j);
                                            CreateModRm(bytes, GetReg(parameter1), parameter1);
                                            Assembler.AddDWord(bytes, (UInt32)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //r32, imm8
                                    if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_prd)
                                    {
                                        AddOpCode(bytes, j);
                                        CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                        Assembler.Add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                    if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_reg)   //probably imul r32,imm8
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
                        case AParam.par_sreg:
                            if (paramtype1 == ATokenType.ttregistersreg)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_rm16) & (Assembler.IsMem16(paramtype2)))
                                {
                                    //sreg,rm16
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_cr:
                            if (paramtype1 == ATokenType.ttregistercr)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_r32) && (paramtype2 == ATokenType.ttregister32bit))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.par_dr:
                            if (paramtype1 == ATokenType.ttregisterdr)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_r32) && (paramtype2 == ATokenType.ttregister32bit))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.par_rm8:

                            if (Assembler.IsMem8(paramtype1) | (Assembler.IsMemoryLocationDefault(parameter1) & Assembler.OpCodes[j].DefaultType))
                            {
                                //r/m8,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //opcode r/m8
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_1) && (paramtype2 == ATokenType.ttvalue) && (v == 1))
                                {
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_cl) && (parameter2 == "CL"))
                                {
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //r/m8,imm8,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //verified it IS r/m8,imm8
                                        AddOpCode(bytes, j);
                                        CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                        Assembler.Add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_r8) && (paramtype2 == ATokenType.ttregister8bit))
                                {
                                    // r/m8,r8
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm) && (paramtype2 == ATokenType.ttregisterxmm))
                                {
                                    // r/m8,xmm
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.par_rm16:
                            if (Assembler.IsMem16(paramtype1))
                            {
                                //r/m16,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //opcode r/m16
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_1) && (paramtype2 == ATokenType.ttvalue) && (v == 1))
                                {
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if (vtype == 16)
                                        {
                                            //perhaps there is a r/m16,imm16
                                            var k = startoflist;
                                            while (k <= endoflist)
                                            {
                                                if (Assembler.OpCodes[k].Mnemonic != tokens[mnemonic])
                                                    continue; //nope, so continue with r/m,imm16
                                                if (((Assembler.OpCodes[k].ParamType1 == AParam.par_rm16) && (Assembler.OpCodes[k].ParamType2 == AParam.par_imm16)) && ((Assembler.OpCodes[k].ParamType3 == AParam.par_noparam) && (parameter3 == "")))
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_imm16) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //r/m16,imm
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if (vtype == 8)
                                        {
                                            //see if there is a r/m16,imm8 (or if this is the one) (optimisation)
                                            var k = startoflist;
                                            while (k <= endoflist)
                                            {
                                                if (Assembler.OpCodes[k].Mnemonic != tokens[mnemonic])
                                                    continue; //nope, so continue with r/m,imm16
                                                if (((Assembler.OpCodes[k].ParamType1 == AParam.par_rm16) && (Assembler.OpCodes[k].ParamType2 == AParam.par_imm8)) && ((Assembler.OpCodes[k].ParamType3 == AParam.par_noparam) && (parameter3 == "")))
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_r16) && (paramtype2 == ATokenType.ttregister16bit))
                                {
                                    //r/m16,r16,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_cl) && (parameter3 == "CL"))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
                                    {
                                        //rm16, r16,imm8
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_sreg) && (paramtype2 == ATokenType.ttregistersreg))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //r/m16,sreg
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_cl) && (parameter2 == "CL"))
                                {
                                    //rm16,cl
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_rm32:
                            if (Assembler.IsMem32(paramtype1) | Assembler.IsMem32(oldParamtype1))
                            {
                                //r/m32,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //no 2nd parameter so it is 'opcode r/m32'
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_1) && (paramtype2 == ATokenType.ttvalue) && (v == 1))
                                {
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //rm32,imm8
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if ((vtype > 8) || (Assembler.OpCodes[j].Signed & (signedvtype > 8)))
                                        {
                                            //the user requests a bigger than 8-bit value, so see if there is also a rm32,imm32 (there are no r/m32,imm16)
                                            var k = startoflist;
                                            while (k <= endoflist)
                                            {
                                                if (Assembler.OpCodes[k].Mnemonic != tokens[mnemonic])
                                                    continue; // maybe we can find one...
                                                if (((Assembler.OpCodes[k].ParamType1 == AParam.par_rm32) && (Assembler.OpCodes[k].ParamType2 == AParam.par_imm32)) && ((Assembler.OpCodes[k].ParamType3 == AParam.par_noparam) && (parameter3 == "")))
                                                {
                                                    //yes, there is
                                                    if ((signedvtype == 64) & RexW)
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_imm32) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //r/m32,imm
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if (signedvtype == 8)
                                        {
                                            //see if there is a r/m32,imm8 (or if this is the one) (optimisation)
                                            var k = startoflist;
                                            while (k <= endoflist)
                                            {
                                                if (Assembler.OpCodes[k].Mnemonic != tokens[mnemonic])
                                                    //nope, so continue with r/m,imm16
                                                    continue; // maybe we can find one...
                                                if (((Assembler.OpCodes[k].ParamType1 == AParam.par_rm32) && (Assembler.OpCodes[k].ParamType2 == AParam.par_imm8)) && ((Assembler.OpCodes[k].ParamType3 == AParam.par_noparam) && (parameter3 == "")) && ((!Assembler.OpCodes[k].Signed) | (signedvtype == 8)))
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
                                        if ((signedvtype == 64) & RexW)
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_cl) && (parameter2 == "CL"))
                                {
                                    //rm32,cl
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_r32) && (paramtype2 == ATokenType.ttregister32bit))
                                {
                                    //r/m32,r32
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_cl) && (parameter3 == "CL"))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_mm) && (paramtype2 == ATokenType.ttregistermm))
                                {
                                    //r32/m32,mm,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //r32/m32,mm
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm) && (paramtype2 == ATokenType.ttregisterxmm))
                                {
                                    //r32/m32,xmm,  (movd for example)
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //r32/m32,xmm
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.par_mm:
                            if (paramtype1 == ATokenType.ttregistermm)
                            {
                                //mm,xxxxx
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //mm,imm8
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_mm) && (paramtype2 == ATokenType.ttregistermm))
                                {
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm) && (paramtype2 == ATokenType.ttregisterxmm))
                                {
                                    //mm,xmm
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_r32_m16) & ((paramtype1 == ATokenType.ttregister32bit) || (paramtype2 == ATokenType.ttmemorylocation16) | Assembler.IsMemoryLocationDefault(parameter2)))
                                {
                                    //mm,r32/m16,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
                                    {
                                        //imm8
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_rm32) & (Assembler.IsMem32(paramtype2)))
                                {
                                    //mm,rm32
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //mm,rm32
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm_m32) & (Assembler.IsXmm32(paramtype2)))
                                {
                                    //mm,xmm/m32
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_mm_m64) && (Assembler.IsRmm64(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[0] == '['))))
                                {
                                    //mm,mm/m64
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.par_noparam)
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }

                                    if (Assembler.OpCodes[j].ParamType3 == AParam.par_imm8)
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }


                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm_m64) && (Assembler.IsXmm64(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[0] == '['))))
                                {
                                    //mm,xmm/m64
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm_m128) && (Assembler.IsXmm128(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[0] == '['))))
                                {
                                    //mm,xmm/m128
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_mm_m64:
                            if (Assembler.IsRmm64(paramtype1) | ((paramtype1 == ATokenType.ttmemorylocation32) && (parameter1[0] == '[')))
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_mm) && (paramtype2 == ATokenType.ttregistermm))
                                {
                                    //mm/m64, mm
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_xmm_m32:
                            if (Assembler.IsXmm32(paramtype1) | ((paramtype1 == ATokenType.ttmemorylocation32) && (parameter1[0] == '[')))
                            {
                                //xmm/m32,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm) && (paramtype2 == ATokenType.ttregisterxmm))
                                {
                                    //xmm/m32, xmm
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_xmm_m64:
                            if (Assembler.IsXmm64(paramtype1) | ((paramtype1 == ATokenType.ttmemorylocation32) && (parameter1[0] == '[')))
                            {
                                //xmm/m64,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm) && (paramtype2 == ATokenType.ttregisterxmm))
                                {
                                    //xmm/m64, xmm
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_xmm_m128:
                            if (Assembler.IsXmm128(paramtype1) | ((paramtype1 == ATokenType.ttmemorylocation32) && (parameter1[0] == '[')))
                            {
                                //xmm/m128,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm) && (paramtype2 == ATokenType.ttregisterxmm))
                                {
                                    //xmm/m128, xmm
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_ymm_m256:
                            if (Assembler.IsYmm256(paramtype1) | ((paramtype1 == ATokenType.ttmemorylocation32) && (parameter1[1] == '[')))
                            {
                                //ymm_m256,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_ymm) && (paramtype2 == ATokenType.ttregisterymm))
                                {
                                    //ymm_m256, ymm
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_ymm:
                            if (paramtype1 == ATokenType.ttregisterymm)
                            {
                                //ymm,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm_m16) & (Assembler.IsXmm16(paramtype2, parameter2)))
                                {
                                    //ymm,xmm/m16
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.par_noparam)
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm_m32) && (Assembler.IsXmm32(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[0] == '['))))
                                {
                                    //ymm,xmm/m32
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.par_noparam)
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm_m64) && (Assembler.IsXmm64(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[0] == '['))))
                                {
                                    //ymm,xmm/m64
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.par_noparam)
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm_m128) && (Assembler.IsXmm128(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[0] == '['))))
                                {
                                    //ymm,xmm/m128
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.par_noparam)
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_ymm) && (paramtype2 == ATokenType.ttregisterymm))
                                {
                                    //ymm,ymm,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
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
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_m128) & ((paramtype3 == ATokenType.ttmemorylocation128) | (Assembler.IsMemoryLocationDefault(parameter3))))
                                    {
                                        //ymm,ymm,m128,
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.par_noparam)
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
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_m256) & ((paramtype3 == ATokenType.ttmemorylocation256) | (Assembler.IsMemoryLocationDefault(parameter3))))
                                    {
                                        //ymm,ymm,m256,
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.par_noparam)
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
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_ymm_m256) && (Assembler.IsYmm256(paramtype3) | ((paramtype3 == ATokenType.ttmemorylocation32) && (parameter3[1] == '['))))
                                    {
                                        //ymm,ymm,ymm/m256
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.par_noparam)
                                        {
                                            if (Assembler.OpCodes[j].VexExtraParam == 2)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter2)) & 0xf;
                                                result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                                return result;
                                            }
                                        }
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.par_imm8)
                                        {
                                            //ymm,ymm,ymm/m256,imm8
                                            AddOpCode(bytes, j);
                                            vexvvvv = (~GetReg(parameter2)) & 0xf;
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                            Assembler.Add(bytes, (Byte)AStringUtils.StrToInt(parameter4));
                                            return result;
                                        }
                                        if ((Assembler.OpCodes[j].ParamType4 == AParam.par_ymm) && (paramtype4 == ATokenType.ttregisterymm))
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_m128) & ((paramtype2 == ATokenType.ttmemorylocation128) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    //ymm,m128,
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.par_noparam)
                                    {
                                        //ymm,m128
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_m256) & ((paramtype2 == ATokenType.ttmemorylocation256) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    //ymm,m256,
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.par_noparam)
                                    {
                                        //ymm,m256
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.par_xmm:
                            if (paramtype1 == ATokenType.ttregisterxmm)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //xmm,imm8
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_mm) && (paramtype2 == ATokenType.ttregistermm))
                                {
                                    //xmm,mm
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm) && (paramtype2 == ATokenType.ttregisterxmm))
                                {
                                    //xmm,xmm,
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.par_noparam)
                                    {
                                        //xmm,xmm
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.par_imm8)
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
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_xmm) && (paramtype3 ==ATokenType.ttregisterxmm))
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
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_r32_m8) & ((paramtype3 ==ATokenType.ttregister32bit) || (paramtype3 ==ATokenType.ttmemorylocation8) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                    {
                                        //xmm,xmm,r32/m8,
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.par_noparam)
                                        {
                                            if (Assembler.OpCodes[j].VexExtraParam == 2)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter2)) & 0xf;
                                                result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                                return result;
                                            }
                                        }
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.par_imm8)
                                        {
                                            //xmm,xmm,r32/m8,imm8
                                            AddOpCode(bytes, j);
                                            vexvvvv = (~GetReg(parameter2)) & 0xf;
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                            Assembler.Add(bytes, (Byte)AStringUtils.StrToInt(parameter4));
                                            return result;
                                        }
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_rm32) && (Assembler.IsMem32(paramtype3) | ((paramtype3 ==ATokenType.ttmemorylocation32) && (parameter3[1] == '['))))
                                    {
                                        //xmm,xmm,rm32
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.par_noparam)
                                        {
                                            if (Assembler.OpCodes[j].VexExtraParam == 2)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter2)) & 0xf;
                                                result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                                return result;
                                            }
                                        }
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.par_imm8)
                                        {
                                            //xmm,xmm,rm32,imm8
                                            AddOpCode(bytes, j);
                                            vexvvvv = (~GetReg(parameter2)) & 0xf;
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                            Assembler.Add(bytes, (Byte)AStringUtils.StrToInt(parameter4));
                                            return result;
                                        }
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_xmm_m32) && (Assembler.IsXmm32(paramtype3) | ((paramtype3 ==ATokenType.ttmemorylocation32) && (parameter3[1] == '['))))
                                    {
                                        //xmm,xmm,xmm/m32,
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.par_noparam)
                                        {
                                            if (Assembler.OpCodes[j].VexExtraParam == 2)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter2)) & 0xf;
                                                result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                                return result;
                                            }
                                        }
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.par_imm8)
                                        {
                                            //xmm,xmm,xmm/m32,imm8
                                            AddOpCode(bytes, j);
                                            vexvvvv = (~GetReg(parameter2)) & 0xf;
                                            result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                            Assembler.Add(bytes, (Byte)AStringUtils.StrToInt(parameter4));
                                            return result;
                                        }
                                    }

                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_m64) & ((paramtype3 ==ATokenType.ttmemorylocation64) | (Assembler.IsMemoryLocationDefault(parameter3))))
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

                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_m128) & ((paramtype3 ==ATokenType.ttmemorylocation128) | (Assembler.IsMemoryLocationDefault(parameter3))))
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

                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_xmm_m64) && (Assembler.IsXmm64(paramtype3) | ((paramtype3 ==ATokenType.ttmemorylocation32) && (parameter3[1] == '['))))
                                    {
                                        //xmm,xmm,xmm/m64
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.par_noparam)
                                        {
                                            if (Assembler.OpCodes[j].VexExtraParam == 2)
                                            {
                                                AddOpCode(bytes, j);
                                                vexvvvv = (~GetReg(parameter2)) & 0xf;
                                                result = CreateModRm(bytes, GetReg(parameter1), parameter3);
                                                return result;
                                            }
                                        }

                                        if (Assembler.OpCodes[j].ParamType4 == AParam.par_imm8)
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

                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_xmm_m128) && (Assembler.IsXmm128(paramtype3) | ((paramtype3 ==ATokenType.ttmemorylocation32) && (parameter3[1] == '['))))
                                    {
                                        //xmm,xmm,xmm/m128,
                                        if (Assembler.OpCodes[j].ParamType4 == AParam.par_noparam)
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

                                        if ((Assembler.OpCodes[j].ParamType4 == AParam.par_xmm) && (paramtype4 ==ATokenType.ttregisterxmm))
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

                                        if (Assembler.OpCodes[j].ParamType4 == AParam.par_imm8)
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_ymm_m256) && (Assembler.IsYmm256(paramtype2) | ((paramtype2 ==ATokenType.ttmemorylocation32) && (parameter2[1] == '['))))
                                {
                                    //xmm,ymm/m256
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_m32) & ((paramtype2 ==ATokenType.ttmemorylocation32) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    //xmm,m32
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_m64) & ((paramtype2 == ATokenType.ttmemorylocation64) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    //xmm,m64
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_m128) & ((paramtype2 ==ATokenType.ttmemorylocation128) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    //xmm,m128,
                                    if (Assembler.OpCodes[j].ParamType3 == AParam.par_noparam)
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }

                                    if (Assembler.OpCodes[i].ParamType3 == AParam.par_imm8)
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        Assembler.Add(bytes, (Byte)v);
                                        return result;
                                    }

                                }

                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_r32_m8) & ((paramtype2 ==ATokenType.ttregister32bit) || (paramtype2 ==ATokenType.ttmemorylocation8) | (Assembler.IsMemoryLocationDefault(parameter2))))
                                {
                                    //xmm,r32/m8,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_imm8) && (paramtype3 ==ATokenType.ttvalue))
                                    {
                                        AddOpCode(bytes, j);
                                        CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        Assembler.Add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_rm32) & (Assembler.IsMem32(paramtype2)))
                                {
                                    //xmm,rm32,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //xmm,rm32
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_mm_m64) && (Assembler.IsRmm64(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[0] == '['))))
                                {
                                    //xmm,mm/m64
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //xmm,mm/m64
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm_m8) & Assembler.IsXmm8(paramtype2, parameter2))
                                {
                                    //xmm,xmm/m8,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //xmm,xmm/m8
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }

                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm_m16) & Assembler.IsXmm16(paramtype2, parameter2))
                                {
                                    //xmm,xmm/m16,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //xmm,xmm/m16
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm_m32) & Assembler.IsXmm32(paramtype2))
                                {
                                    //xmm,xmm/m32,
                                    //even if the user didn't intend for it to be xmm,m64 it will be, that'll teach the lazy user to forget opperand size
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //xmm,xmm/m32
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
                                    {
                                        AddOpCode(bytes, j);
                                        CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        Assembler.Add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm_m64) && (Assembler.IsXmm64(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[0] == '['))))
                                {
                                    //even if the user didn't intend for it to be xmm,m64 it will be, that'll teach the lazy user to forget opperand size
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //xmm,xmm/m64
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
                                    {
                                        AddOpCode(bytes, j);
                                        CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        Assembler.Add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm_m128) && (Assembler.IsXmm128(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[0] == '['))))
                                {
                                    //xmm,xmm/m128,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //xmm,xmm/m128
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter1), parameter2);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
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
                        case AParam.par_m8:
                            if ((paramtype1 == ATokenType.ttmemorylocation8) | Assembler.IsMemoryLocationDefault(parameter1))
                            {
                                //m8,xxx
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
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
                        case AParam.par_m16:
                            if ((paramtype1 ==ATokenType.ttmemorylocation16) | Assembler.IsMemoryLocationDefault(parameter1))
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //opcode+rd
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_m32:
                            if (paramtype1 == ATokenType.ttmemorylocation32)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                                if (Assembler.OpCodes[j].ParamType2 == AParam.par_r32)
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm) & ((paramtype2 == ATokenType.ttregisterxmm) | Assembler.IsMemoryLocationDefault(parameter2)))
                                {
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) || (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.par_m64:
                            if ((paramtype1 == ATokenType.ttmemorylocation64) || (paramtype1 == ATokenType.ttmemorylocation32))
                            {
                                //m64,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //m64
                                    if ((Assembler.GetTokenType(ref parameter1, parameter2) == ATokenType.ttmemorylocation64) | Assembler.IsMemoryLocationDefault(parameter1))
                                    {
                                        //verified, it is a 64 bit location, and if it was detected as 32 it was due to defaulting to 32
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                        return result;
                                    }
                                }
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm) && (paramtype2 == ATokenType.ttregisterxmm))
                                {
                                    //m64,xmm
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) || (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.par_m80:
                            if ((paramtype1 == ATokenType.ttmemorylocation80) || ((paramtype1 == ATokenType.ttmemorylocation32) && (parameter1[0] == '[')))
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    AddOpCode(bytes, j);
                                    result = CreateModRm(bytes, Assembler.EoToReg(Assembler.OpCodes[j].OpCode1), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_m128:
                            if ((paramtype1 == ATokenType.ttmemorylocation128) | (Assembler.IsMemoryLocationDefault(parameter1)))
                            {
                                //m128,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_xmm) && (paramtype2 == ATokenType.ttregisterxmm))
                                {
                                    //m128,xmm
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_xmm) && (paramtype3 ==ATokenType.ttregisterxmm))
                                    {
                                        //m128,xmm,xmm
                                        if ((Assembler.OpCodes[j].ParamType4 == AParam.par_noparam) && (parameter4 == ""))
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
                        case AParam.par_m256:
                            if ((paramtype1 ==ATokenType.ttmemorylocation256) | (Assembler.IsMemoryLocationDefault(parameter1)))
                            {
                                //m256,
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_ymm) && (paramtype2 ==ATokenType.ttregisterymm))
                                {
                                    //m256,ymm,
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        AddOpCode(bytes, j);
                                        result = CreateModRm(bytes, GetReg(parameter2), parameter1);
                                        return result;
                                    }
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_ymm) && (paramtype3 ==ATokenType.ttregisterymm))
                                    {
                                        //m256,ymm,ymm
                                        if ((Assembler.OpCodes[j].ParamType4 == AParam.par_noparam) && (parameter4 == ""))
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
                        case AParam.par_rel8:
                            if (paramtype1 == ATokenType.ttvalue)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //rel8
                                    if (AArrayUtils.InArray(parameter1[0], '-', '+'))
                                    {
                                        if (((!overrideshort) & (vtype > 8)) | (overridelong))
                                        {
                                            //see if there is a 32 bit equivalent opcode (notice I dont do rel 16 because that'll completely screw up eip)
                                            var k = startoflist;
                                            while ((k < Assembler.OpCodeCount) && (Assembler.OpCodes[k].Mnemonic == tokens[mnemonic]))
                                            {
                                                if ((Assembler.OpCodes[k].ParamType1 == AParam.par_rel32) && (Assembler.OpCodes[k].ParamType2 == AParam.par_noparam))
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
                                        if ((!overrideshort) & ((overridelong) | (Assembler.ValueToType((IntPtr)(v - address - (UInt64)(Assembler.OpCodes[j].Bytes + 1))) > 8)))
                                        {
                                            //the user tried to find a relative address out of it's reach
                                            //see if there is a 32 bit version of the opcode
                                            var k = startoflist;
                                            while ((k < Assembler.OpCodeCount) && (Assembler.OpCodes[k].Mnemonic == tokens[mnemonic]))
                                            {
                                                if ((Assembler.OpCodes[k].ParamType1 == AParam.par_rel32) && (Assembler.OpCodes[k].ParamType2 == AParam.par_noparam))
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
                                            b = (Byte)((UInt32)(v - address - (UInt32)(Assembler.OpCodes[j].Bytes + 1)) & 0xff);
                                            // b:=b and $ff;
                                            Assembler.Add(bytes, b);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                            }
                            break;
                        case AParam.par_rel32:
                            if (paramtype1 == ATokenType.ttvalue)
                            {
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
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
                        case AParam.par_st0:
                            if ((parameter1 == "ST(0)") || (parameter1 == "ST"))
                            {
                                //st(0),
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_st) && (paramtype2 == ATokenType.ttregisterst))
                                {
                                    //st(0),st(x),
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_pi)
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
                        case AParam.par_st:
                            if (paramtype1 == ATokenType.ttregisterst)
                            {
                                //st(x),
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_noparam) && (parameter2 == ""))
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
                                if ((Assembler.OpCodes[j].ParamType2 == AParam.par_st0) && ((parameter2 == "ST(0)") || (parameter2 == "ST")))
                                {
                                    //st(x),st(0)
                                    if ((Assembler.OpCodes[j].ParamType3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if (Assembler.OpCodes[j].OpCode1 == AExtraOpCode.eo_pi)
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
                    paramtype1 = oldParamtype1;
                    paramtype2 = oldParamtype2;
                    j += 1;
                }
            }
            finally
            {
                if (result)
                {
                    //insert rex prefix if needed
                    if (Assembler.SymHandler.Process.IsX64)
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
                            bigvex = (Assembler.OpCodes[j].VexLeadingOpCode > AVexLeadingOpCode.lo_0f) | RexB | RexX | RexW;
                            if (bigvex == false)
                            {
                                //2byte vex
                                bytes.SetLength(bytes.Length + 2);
                                for (i = bytes.Length - 1; i >= RexPrefixLocation + 2; i--)
                                    bytes[i] = bytes[i - 2];
                                bytes[RexPrefixLocation] = 0xc5; //2 byte VEX
                                // todo get 2 byte vex working!
                                //pvex2byte(&bytes[RexPrefixLocation + 1])->pp = (int) (Assembler.OpCodes[j].VexLeadingOpCode);
                                //pvex2byte(&bytes[RexPrefixLocation + 1])->l = Assembler.OpCodes[j].VexL;
                                //pvex2byte(&bytes[RexPrefixLocation + 1])->vvvv = vexvvvv;
                                //pvex2byte(&bytes[RexPrefixLocation + 1])->r = RexR ? 0 : 1;
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
                                // todo get 3 byte vex working!
                                //pvex3byte(&bytes[RexPrefixLocation + 1])->mmmmm = (int)(Assembler.OpCodes[j].VexLeadingOpCode);
                                //pvex3byte(&bytes[RexPrefixLocation + 1])->b = RexB ? 0 : 1;
                                //pvex3byte(&bytes[RexPrefixLocation + 1])->x = RexX ? 0 : 1;
                                //pvex3byte(&bytes[RexPrefixLocation + 1])->r = RexR ? 0 : 1;
                                //pvex3byte(&bytes[RexPrefixLocation + 1])->pp = (int)(Assembler.OpCodes[j].VexLeadingOpCode);
                                //pvex3byte(&bytes[RexPrefixLocation + 1])->l = Assembler.OpCodes[j].VexL;
                                //pvex3byte(&bytes[RexPrefixLocation + 1])->vvvv = vexvvvv;
                                //pvex3byte(&bytes[RexPrefixLocation + 1])->w = RexW ? 1 : 0; //not inverted
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
                                unsafe
                                {
                                    fixed (byte* bt = bytes.Raw)
                                    {
                                        var vp = (UInt32)(ActualDisplacement - (address + (UInt32)bytes.Length));
                                        *(UInt32*)bt[RelativeAddressLocation] = vp;
                                    }
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
