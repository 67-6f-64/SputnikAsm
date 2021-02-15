using System;
using Sputnik.LBinary;
using Sputnik.LEngine;
using Sputnik.LUtils;
using SputnikAsm.LAssembler.LEnums;
using SputnikAsm.LCollections;
using SputnikAsm.LUtils;

namespace SputnikAsm.LAssembler
{
    public class ASingleLineAssembler
    {
        #region Variables
        public AAssembler Assembler;
        public Byte rexprefix;
        public int rexprefixlocation;
        public int relativeaddresslocation;
        public UInt64 actualdisplacement;
        public Boolean rex_w_actual;
        public Boolean rex_r_actual;
        public Boolean rex_x_actual;
        public Boolean rex_b_actual;
        #endregion
        #region Properties
        public Boolean rex_w
        {
            get => ((rexprefix >> 3) & 1) == 1;
            set
            {
                if (value)
                    rexprefix = (Byte)((rexprefix & 0xf7) | 8);
                else
                    rexprefix = (Byte)(rexprefix & 0xf7);
            }
        }
        public Boolean rex_r
        {
            get => ((rexprefix >> 2) & 1) == 1;
            set
            {
                if (value)
                    rexprefix = (Byte)((rexprefix & 0xfb) | 4);
                else
                    rexprefix = (Byte)(rexprefix & 0xfb);
            }
        }
        public Boolean rex_x
        {
            get => ((rexprefix >> 1) & 1) == 1;
            set
            {
                if (value)
                    rexprefix = (Byte)((rexprefix & 0xfd) | 2);
                else
                    rexprefix = (Byte)(rexprefix & 0xfd);
            }
        }
        public Boolean rex_b
        {
            get => (rexprefix & 1) == 1;
            set
            {
                if (value)
                    rexprefix = (Byte)((rexprefix & 0xfe) | 1);
                else
                    rexprefix = (Byte)(rexprefix & 0xfe);
            }
        }
        #endregion
        #region Constructor
        public ASingleLineAssembler(AAssembler assembler)
        {
            Assembler = assembler;
            rexprefix = 0;
            rexprefixlocation = 0;
            relativeaddresslocation = 0;
            actualdisplacement = 0;
            rex_w_actual = false;
            rex_r_actual = false;
            rex_x_actual = false;
            rex_b_actual = false;
        }
        #endregion
        #region getreg
        public int getreg(string reg)
        {
            return getreg(reg, true);
        }
        public int getreg(string reg, Boolean exceptonerror)
        {
            var result = 1000;
            if ((reg == "RAX") || (reg == "EAX") || (reg == "AX") || (reg == "AL") || (reg == "MM0") || (reg == "XMM0") || (reg == "ST(0)") || (reg == "ST") || (reg == "ES") || (reg == "CR0") || (reg == "DR0")) result = 0;
            if ((reg == "RCX") || (reg == "ECX") || (reg == "CX") || (reg == "CL") || (reg == "MM1") || (reg == "XMM1") || (reg == "ST(1)") || (reg == "CS") || (reg == "CR1") || (reg == "DR1")) result = 1;
            if ((reg == "RDX") || (reg == "EDX") || (reg == "DX") || (reg == "DL") || (reg == "MM2") || (reg == "XMM2") || (reg == "ST(2)") || (reg == "SS") || (reg == "CR2") || (reg == "DR2")) result = 2;
            if ((reg == "RBX") || (reg == "EBX") || (reg == "BX") || (reg == "BL") || (reg == "MM3") || (reg == "XMM3") || (reg == "ST(3)") || (reg == "DS") || (reg == "CR3") || (reg == "DR3")) result = 3;
            if ((reg == "RSP") || (reg == "ESP") || (reg == "SP") || (reg == "AH") || (reg == "MM4") || (reg == "XMM4") || (reg == "ST(4)") || (reg == "FS") || (reg == "CR4") || (reg == "DR4")) result = 4;
            if ((reg == "RBP") || (reg == "EBP") || (reg == "BP") || (reg == "CH") || (reg == "MM5") || (reg == "XMM5") || (reg == "ST(5)") || (reg == "GS") || (reg == "CR5") || (reg == "DR5")) result = 5;
            if ((reg == "RSI") || (reg == "ESI") || (reg == "SI") || (reg == "DH") || (reg == "MM6") || (reg == "XMM6") || (reg == "ST(6)") || (reg == "HS") || (reg == "CR6") || (reg == "DR6")) result = 6;
            if ((reg == "RDI") || (reg == "EDI") || (reg == "DI") || (reg == "BH") || (reg == "MM7") || (reg == "XMM7") || (reg == "ST(7)") || (reg == "IS") || (reg == "CR7") || (reg == "DR7")) result = 7;
            if (Assembler.symhandler.process.is64bit)
            {
                if (reg == "SPL") result = 4;
                else
                if (reg == "BPL") result = 5;
                else
                if (reg == "SIL") result = 6;
                else
                if (reg == "DIL") result = 7;
                else
                if ((reg == "R8") || (reg == "R8D") || (reg == "R8W") || (reg == "R8L") || (reg == "MM8") || (reg == "XMM8") || (reg == "ST(8)") || (reg == "JS") || (reg == "CR8") || (reg == "DR8")) result = 8;
                if ((reg == "R9") || (reg == "R9D") || (reg == "R9W") || (reg == "R9L") || (reg == "MM9") || (reg == "XMM9") || (reg == "ST(9)") || (reg == "KS") || (reg == "CR9") || (reg == "DR9")) result = 9;
                if ((reg == "R10") || (reg == "R10D") || (reg == "R10W") || (reg == "R10L") || (reg == "MM10") || (reg == "XMM10") || (reg == "ST(10)") || (reg == "KS") || (reg == "CR10") || (reg == "DR10")) result = 10;
                if ((reg == "R11") || (reg == "R11D") || (reg == "R11W") || (reg == "R11L") || (reg == "MM11") || (reg == "XMM11") || (reg == "ST(11)") || (reg == "LS") || (reg == "CR11") || (reg == "DR11")) result = 11;
                if ((reg == "R12") || (reg == "R12D") || (reg == "R12W") || (reg == "R12L") || (reg == "MM12") || (reg == "XMM12") || (reg == "ST(12)") || (reg == "MS") || (reg == "CR12") || (reg == "DR12")) result = 12;
                if ((reg == "R13") || (reg == "R13D") || (reg == "R13W") || (reg == "R13L") || (reg == "MM13") || (reg == "XMM13") || (reg == "ST(13)") || (reg == "NS") || (reg == "CR13") || (reg == "DR13")) result = 13;
                if ((reg == "R14") || (reg == "R14D") || (reg == "R14W") || (reg == "R14L") || (reg == "MM14") || (reg == "XMM14") || (reg == "ST(14)") || (reg == "OS") || (reg == "CR14") || (reg == "DR14")) result = 14;
                if ((reg == "R15") || (reg == "R15D") || (reg == "R15W") || (reg == "R15L") || (reg == "MM15") || (reg == "XMM15") || (reg == "ST(15)") || (reg == "PS") || (reg == "CR15") || (reg == "DR15")) result = 15;
            }
            if (result == 1000 && exceptonerror)
                throw new Exception("Invalid register");
            return result;
        }
        #endregion
        #region createsibscaleindex
        public void createsibscaleindex(AByteArray sib, int index, string reg)
        {
            var tmp = sib[index];
            createsibscaleindex(ref tmp, reg);
            sib[index] = tmp;
        }
        public void createsibscaleindex(ref byte sib, string reg)
        //var i2,i4,i8: integer;
        {
            if (AStringUtils.Pos("*2", reg) != -1)
                Assembler.setsibscale(ref sib, 1);
            else
            if (AStringUtils.Pos("*4", reg) != -1)
                Assembler.setsibscale(ref sib, 2);
            else
            if (AStringUtils.Pos("*8", reg) != -1)
                Assembler.setsibscale(ref sib, 3);
            else Assembler.setsibscale(ref sib, 0);

            if (Assembler.symhandler.process.is64bit)
            {
                if (AStringUtils.Pos("RAX", reg) != -1) setsibindex(ref sib, 0);
                else if (AStringUtils.Pos("RCX", reg) != -1) setsibindex(ref sib, 1);
                else if (AStringUtils.Pos("RDX", reg) != -1) setsibindex(ref sib, 2);
                else if (AStringUtils.Pos("RBX", reg) != -1) setsibindex(ref sib, 3);
                else if ((reg == "") || (AStringUtils.Pos("RSP", reg) != -1)) setsibindex(ref sib, 4);
                else if (AStringUtils.Pos("RBP", reg) != -1) setsibindex(ref sib, 5);
                else if (AStringUtils.Pos("RSI", reg) != -1) setsibindex(ref sib, 6);
                else if (AStringUtils.Pos("RDI", reg) != -1) setsibindex(ref sib, 7);
                else if (AStringUtils.Pos("R8", reg) != -1) setsibindex(ref sib, 8);
                else if (AStringUtils.Pos("R9", reg) != -1) setsibindex(ref sib, 9);
                else if (AStringUtils.Pos("R10", reg) != -1) setsibindex(ref sib, 10);
                else if (AStringUtils.Pos("R11", reg) != -1) setsibindex(ref sib, 11);
                else if (AStringUtils.Pos("R12", reg) != -1) setsibindex(ref sib, 12);
                else if (AStringUtils.Pos("R13", reg) != -1) setsibindex(ref sib, 13);
                else if (AStringUtils.Pos("R14", reg) != -1) setsibindex(ref sib, 14);
                else if (AStringUtils.Pos("R15", reg) != -1) setsibindex(ref sib, 15);
                else
                    throw new Exception("WTF is a " + reg);
            }
            else
            {
                if (AStringUtils.Pos("EAX", reg) != -1) setsibindex(ref sib, 0);
                else if (AStringUtils.Pos("ECX", reg) != -1) setsibindex(ref sib, 1);
                else if (AStringUtils.Pos("EDX", reg) != -1) setsibindex(ref sib, 2);
                else if (AStringUtils.Pos("EBX", reg) != -1) setsibindex(ref sib, 3);
                else if ((reg == "") || (AStringUtils.Pos("ESP", reg) != -1)) setsibindex(ref sib, 4);
                else if (AStringUtils.Pos("EBP", reg) != -1) setsibindex(ref sib, 5);
                else if (AStringUtils.Pos("ESI", reg) != -1) setsibindex(ref sib, 6);
                else if (AStringUtils.Pos("EDI", reg) != -1) setsibindex(ref sib, 7);
                else
                    throw new Exception("WTF is a " + reg);
            }
        }
        #endregion
        #region setsibindex
        public void setsibindex(byte[] sib, int index, byte i)
        {
            var tmp = sib[index];
            setsibindex(ref tmp, i);
            sib[index] = tmp;
        }
        public void setsibindex(AByteArray sib, int index, byte i)
        {
            var tmp = sib[index];
            setsibindex(ref tmp, i);
            sib[index] = tmp;
        }
        public void setsibindex(ref byte sib, byte i)
        {
            sib = (Byte)((sib & 0xc7) | ((i & 7) << 3));
            if (i > 7)
                rex_x = true;
        }
        #endregion
        #region setsibbase
        public void setsibbase(AByteArray sib, int index, byte i)
        {
            var tmp = sib[index];
            setsibbase(ref tmp, i);
            sib[index] = tmp;
        }
        public void setsibbase(ref byte sib, byte i)
        {
            sib = (Byte)((sib & 0xf8) | (i & 7));
            if (i > 7)
                rex_b = true;
        }
        #endregion
        #region setrm
        public void setrm(AByteArray sib, int index, byte i)
        {
            var tmp = sib[index];
            setrm(ref tmp, i);
            sib[index] = tmp;
        }
        public void setrm(ref byte modrm, byte i)
        {
            modrm = (byte)((modrm & 0xf8) | (i & 7));
            if (i > 7)
                rex_b = true;
        }
        #endregion
        #region createmodrm
        public Boolean createmodrm(AByteArray bytes, int reg, string param)
        {
            string address;
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
                Assembler.setmod(modrm, 0, 3);
                if ((param == "RAX") || (param == "EAX") || (param == "AX") || (param == "AL") || (param == "MM0") || (param == "XMM0"))
                    setrm(modrm, 0, 0);
                else if ((param == "RCX") || (param == "ECX") || (param == "CX") || (param == "CL") || (param == "MM1") || (param == "XMM1"))
                    setrm(modrm, 0, 1);
                else if ((param == "RDX") || (param == "EDX") || (param == "DX") || (param == "DL") || (param == "MM2") || (param == "XMM2"))
                    setrm(modrm, 0, 2);
                else if ((param == "RBX") || (param == "EBX") || (param == "BX") || (param == "BL") || (param == "MM3") || (param == "XMM3"))
                    setrm(modrm, 0, 3);
                else if ((param == "SPL") || (param == "RSP") || (param == "ESP") || (param == "SP") || (param == "AH") || (param == "MM4") || (param == "XMM4"))
                    setrm(modrm, 0, 4);
                else if ((param == "BPL") || (param == "RBP") || (param == "EBP") || (param == "BP") || (param == "CH") || (param == "MM5") || (param == "XMM5"))
                    setrm(modrm, 0, 5);
                else if ((param == "SIL") || (param == "RSI") || (param == "ESI") || (param == "SI") || (param == "DH") || (param == "MM6") || (param == "XMM6"))
                    setrm(modrm, 0, 6);
                else if ((param == "DIL") || (param == "RDI") || (param == "EDI") || (param == "DI") || (param == "BH") || (param == "MM7") || (param == "XMM7"))
                    setrm(modrm, 0, 7);
                else if ((param == "R8") || (param == "R8D") || (param == "R8W") || (param == "R8L") || (param == "MM8") || (param == "XMM8"))
                    setrm(modrm, 0, 8);
                else if ((param == "R9") || (param == "R9D") || (param == "R9W") || (param == "R9L") || (param == "MM9") || (param == "XMM9"))
                    setrm(modrm, 0, 9);
                else if ((param == "R10") || (param == "R10D") || (param == "R10W") || (param == "R10L") || (param == "MM10") || (param == "XMM10"))
                    setrm(modrm, 0, 10);
                else if ((param == "R11") || (param == "R11D") || (param == "R11W") || (param == "R11L") || (param == "MM11") || (param == "XMM11"))
                    setrm(modrm, 0, 11);
                else if ((param == "R12") || (param == "R12D") || (param == "R12W") || (param == "R12L") || (param == "MM12") || (param == "XMM12"))
                    setrm(modrm, 0, 12);
                else if ((param == "R13") || (param == "R13D") || (param == "R13W") || (param == "R13L") || (param == "MM13") || (param == "XMM13"))
                    setrm(modrm, 0, 13);
                else if ((param == "R14") || (param == "R14D") || (param == "R14W") || (param == "R14L") || (param == "MM14") || (param == "XMM14"))
                    setrm(modrm, 0, 14);
                else if ((param == "R15") || (param == "R15D") || (param == "R15W") || (param == "R15L") || (param == "MM15") || (param == "XMM15"))
                    setrm(modrm, 0, 15);
                else
                    throw new Exception("I don't understand what you mean with " + param);
            }
            else
                setmodrm(modrm, address, bytes.Length);
            //setreg
            if (reg > 7)
            {
                if (Assembler.symhandler.process.is64bit)
                    rex_r = true;
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
        #region addopcode
        public void addopcode(AByteArray bytes, int i)
        {
            rexprefixlocation = bytes.Length;
            if (AArrayUtils.InArray(Assembler.opcodes[i].bt1, 0x66, 0xf2, 0xf3))
                rexprefixlocation += 1; //mandatory prefixes come before the rex byte
            Assembler.add(bytes, Assembler.opcodes[i].bt1);
            if (Assembler.opcodes[i].bytes > 1)
                Assembler.add(bytes, Assembler.opcodes[i].bt2);
            if (Assembler.opcodes[i].bytes > 2)
                Assembler.add(bytes, Assembler.opcodes[i].bt3);
        }
        #endregion
        #region setmodrm
        public void setmodrm(AByteArray modrm, string address, int offset)
        {
            var reg = new UAv();
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
                if (j > 0)  //a register or a stupid user
                    regs = regs + temp + '+';
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
                Assembler.setmod(modrm, 0, 0);
            else if (((int)(disp) >= -128) && ((int)(disp) <= 127))
                Assembler.setmod(modrm, 0, 1);
            else
                Assembler.setmod(modrm, 0, 2);
            reg1 = "";
            reg2 = "";
            if (AStringUtils.Pos("+", regs) != -1)
            {
                reg1 = AStringUtils.Copy(regs, 1, AStringUtils.Pos("+", regs) - 1);
                reg2 = AStringUtils.Copy(regs, AStringUtils.Pos("+", regs) + 1, regs.Length);
                k = 2;
            }
            else
            {
                reg1 = regs;
                k = 1;
            }
            reg[-1] = new USv(reg1);
            reg[1] = new USv(reg2);
            k = 1;
            if ((reg1 != "") && (reg2 == "") && (AStringUtils.Pos("*", reg1) != -1))
            {
                Assembler.setmod(modrm, 0, 0);
                setrm(modrm, 0, 4);
                modrm.SetLength(2);
                setsibbase(modrm, 1, 5);
                createsibscaleindex(modrm, 1, reg[-1]);
                Assembler.adddword(modrm, (UInt32)disp);
                found = true;
            }
            if ((reg[k] == "") && (reg[-k] == ""))
            {
                //no registers, just a address
                setrm(modrm, 0, 5);
                Assembler.setmod(modrm, 0, 0);
                if (Assembler.symhandler.process.is64bit)
                {
                    if (disp <= 0xffffffff)
                    {
                        //this can be solved with an 0x25 SIB byte
                        modrm.SetLength(2);
                        setrm(modrm, 0, 4);
                        setsibbase(modrm, 1, 5); //no base
                        setsibindex(modrm, 1, 4);
                        Assembler.setsibscale(modrm, 1, 0);
                    }
                    else
                    {
                        actualdisplacement = disp;
                        relativeaddresslocation = offset + 1;
                    }
                }
                Assembler.adddword(modrm, (UInt32)disp);
                found = true;
            }
            // todo add TRY
            if ((reg[k] == "ESP") || (reg[-k] == "ESP") || (reg[k] == "RSP") || (reg[-k] == "RSP"))  //esp takes precedence
            {
                if (reg[-k] == "ESP")
                    k = -k;
                if (reg[-k] == "RSP")
                    k = -k;
                setrm(modrm, 0, 4);
                modrm.SetLength(2);
                setsibbase(modrm, 1, 4);
                createsibscaleindex(modrm, 1, reg[-k]);
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
                    setrm(modrm, 0, 4);
                    modrm.SetLength(2);
                    setsibbase(modrm, 1, 0);
                    createsibscaleindex(modrm, 1, reg[-k]);
                }
                else
                    setrm(modrm, 0, 0); //no sib needed
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
                    setrm(modrm, 0, 4);
                    modrm.SetLength(2);
                    setsibbase(modrm, 1, 1);
                    createsibscaleindex(modrm, 1, reg[-k]);
                }
                else
                    setrm(modrm, 0, 1); //no sib needed
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
                    setrm(modrm, 0, 4);
                    modrm.SetLength(2);
                    setsibbase(modrm, 1, 2);
                    createsibscaleindex(modrm, 1, reg[-k]);
                }
                else
                    setrm(modrm, 0, 2); //no sib needed
                found = true;
                return;
            }
            if ((reg[k] == "EBX") || (reg[-k] == "EBX") || (reg[k] == "RBX") || (reg[-k] == "RBX"))
            {
                if (reg[-k] == "EBX") k = -k;
                if (reg[-k] == "RBX") k = -k;

                if (reg[-k] != "")  //sib needed
                {
                    setrm(modrm, 0, 4);
                    modrm.SetLength(2);
                    setsibbase(modrm, 1, 3);
                    createsibscaleindex(modrm, 1, reg[-k]);
                }
                else
                    setrm(modrm, 0, 3); //no sib needed
                found = true;
                return;
            }
            if ((reg[k] == "ESP") || (reg[-k] == "ESP") || (reg[k] == "RSP") || (reg[-k] == "RSP"))
            {
                if (reg[-k] == "ESP")
                    k = -k;
                if (reg[-k] == "RSP")
                    k = -k;
                setrm(modrm, 0, 4);
                modrm.SetLength(2);
                setsibbase(modrm, 1, 4);
                createsibscaleindex(modrm, 1, reg[-k]);
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
                    Assembler.setmod(modrm, 0, 1);
                if (reg[-k] != "")  //sib needed
                {
                    setrm(modrm, 0, 4);
                    modrm.SetLength(2);
                    setsibbase(modrm, 1, 5);
                    createsibscaleindex(modrm, 1, reg[-k]);
                }
                else
                    setrm(modrm, 0, 5); //no sib needed
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
                    setrm(modrm, 0, 4);
                    modrm.SetLength(2);
                    setsibbase(modrm, 1, 6);
                    createsibscaleindex(modrm, 1, reg[-k]);
                }
                else
                    setrm(modrm, 0, 6); //no sib needed
                found = true;
                return;
            }
            if ((reg[k] == "EDI") || (reg[-k] == "EDI") || (reg[k] == "RDI") || (reg[-k] == "RDI"))
            {
                if (reg[-k] == "EDI") k = -k;
                if (reg[-k] == "RDI") k = -k;

                if (reg[-k] != "")  //sib needed
                {
                    setrm(modrm, 0, 4);
                    modrm.SetLength(2);
                    setsibbase(modrm, 1, 7);
                    createsibscaleindex(modrm, 1, reg[-k]);
                }
                else
                    setrm(modrm, 0, 7); //no sib needed
                found = true;
                return;
            }
            if (Assembler.symhandler.process.is64bit)
            {
                if ((reg[k] == "R8") || (reg[-k] == "R8"))
                {
                    if (reg[-k] == "R8")
                        k = -k;
                    if (reg[-k] != "")  //sib needed
                    {
                        setrm(modrm, 0, 4);
                        modrm.SetLength(2);
                        setsibbase(modrm, 1, 8);
                        createsibscaleindex(modrm, 1, reg[-k]);
                    }
                    else
                        setrm(modrm, 0, 8); //no sib needed
                    found = true;
                    return;
                }
                if ((reg[k] == "R9") || (reg[-k] == "R9"))
                {
                    if (reg[-k] == "R9")
                        k = -k;
                    if (reg[-k] != "")  //sib needed
                    {
                        setrm(modrm, 0, 4);
                        modrm.SetLength(2);
                        setsibbase(modrm, 1, 9);
                        createsibscaleindex(modrm, 1, reg[-k]);
                    }
                    else
                        setrm(modrm, 0, 9); //no sib needed
                    found = true;
                    return;
                }
                if ((reg[k] == "R10") || (reg[-k] == "R10"))
                {
                    if (reg[-k] == "R10")
                        k = -k;
                    if (reg[-k] != "")  //sib needed
                    {
                        setrm(modrm, 0, 4);
                        modrm.SetLength(2);
                        setsibbase(modrm, 1, 10);
                        createsibscaleindex(modrm, 1, reg[-k]);
                    }
                    else
                        setrm(modrm, 0, 10); //no sib needed
                    found = true;
                    return;
                }
                if ((reg[k] == "R11") || (reg[-k] == "R11"))
                {
                    if (reg[-k] == "R11")
                        k = -k;
                    if (reg[-k] != "")  //sib needed
                    {
                        setrm(modrm, 0, 4);
                        modrm.SetLength(2);
                        setsibbase(modrm, 1, 11);
                        createsibscaleindex(modrm, 1, reg[-k]);
                    }
                    else
                        setrm(modrm, 0, 11); //no sib needed
                    found = true;
                    return;
                }
                if ((reg[k] == "R12") || (reg[-k] == "R12"))
                {
                    if (reg[-k] == "R12")
                        k = -k;
                    setrm(modrm, 0, 4);
                    modrm.SetLength(2);
                    setsibbase(modrm, 1, 12);
                    createsibscaleindex(modrm, 1, reg[-k]);
                    found = true;
                    return;
                }
                if ((reg[k] == "R13") || (reg[-k] == "R13"))
                {
                    if (reg[-k] == "R13")
                        k = -k;
                    if (disp == 0)
                        Assembler.setmod(modrm, 0, 1);
                    if (reg[-k] != "")  //sib needed
                    {
                        setrm(modrm, 0, 4);
                        modrm.SetLength(2);
                        setsibbase(modrm, 1, 13);
                        createsibscaleindex(modrm, 1, reg[-k]);
                    }
                    else
                        setrm(modrm, 0, 13); //no sib needed
                    found = true;
                    return;
                }
                if ((reg[k] == "R14") || (reg[-k] == "R14"))
                {
                    if (reg[-k] == "R14")
                        k = -k;
                    if (reg[-k] != "")  //sib needed
                    {
                        setrm(modrm, 0, 4);
                        modrm.SetLength(2);
                        setsibbase(modrm, 1, 14);
                        createsibscaleindex(modrm, 1, reg[-k]);
                    }
                    else
                        setrm(modrm, 0, 14); //no sib needed
                    found = true;
                    return;
                }
                if ((reg[k] == "R15") || (reg[-k] == "R15"))
                {
                    if (reg[-k] == "R15")
                        k = -k;
                    if (reg[-k] != "")  //sib needed
                    {
                        setrm(modrm, 0, 4);
                        modrm.SetLength(2);
                        setsibbase(modrm, 1, 15);
                        createsibscaleindex(modrm, 1, reg[-k]);
                    }
                    else
                        setrm(modrm, 0, 15); //no sib needed
                    found = true;
                    return;
                }
            }
            // catch
            // finally
            if (!found)
                throw new Exception("Invalid address");
            i = Assembler.getmod(modrm[0]);
            if (i == 1)
                Assembler.add(modrm, (Byte)disp);
            if (i == 2)
                Assembler.adddword(modrm, (UInt32)disp);
        }
        #endregion
        #region assemble
        public Boolean assemble(string opcode, UInt64 address, AByteArray bytes, AAssemblerPreference assemblerpreference = AAssemblerPreference.apnone, Boolean skiprangecheck = false)
        {
            AStringArray tokens = new AStringArray();
            var i = 0;
            var j = 0;
            UInt64 v = 0;
            UInt64 v2 = 0;
            var mnemonic = 0;
            var nroftokens = 0;
            ATokenType paramtype1 = ATokenType.ttinvalidtoken;
            ATokenType paramtype2 = ATokenType.ttinvalidtoken;
            ATokenType paramtype3 = ATokenType.ttinvalidtoken;
            var parameter1 = "";
            var parameter2 = "";
            var parameter3 = "";
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
            var is64bit = Assembler.symhandler.process.is64bit;
            relativeaddresslocation = -1;
            rexprefix = 0;
            var result = false;
            Assembler.tokenize(opcode, tokens);
            nroftokens = tokens.Length;
            if (nroftokens == 0)
                return result;
            if (tokens[0] == "DB")
            {
                for (i = 1; i <= nroftokens - 1; i++)
                {
                    if (tokens[i][0] == '\'')  //string
                    {
                        //find the original non uppercase stringpos in the opcode
                        j = AStringUtils.Pos(tokens[i], opcode.ToUpper());
                        if (j != -1)
                        {
                            tempstring = AStringUtils.Copy(opcode, j, tokens[i].Length);
                            Assembler.addstring(bytes, tempstring);
                        }
                        else
                            Assembler.addstring(bytes, tokens[i]); //lets try to save face...
                    }
                    else
                        Assembler.add(bytes, (Byte)AStringUtils.StrToInt("$" + tokens[i]));
                }
                result = true;
                return result;
            }
            mnemonic = -1;
            for (i = 0; i < tokens.Length; i++)
            {
                if (!((tokens[i] == "LOCK") || (tokens[i] == "REP") || (tokens[i] == "REPNE") || (tokens[i] == "REPE")))
                {
                    mnemonic = i;
                    break; // todo figure out if this is meant to be break/continue
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
            overrideshort = AStringUtils.Pos("SHORT ", parameter1) != -1;
            overridelong = (AStringUtils.Pos("LONG ", parameter1) != -1);
            if (Assembler.symhandler.process.is64bit)
                overridefar = (AStringUtils.Pos("FAR ", parameter1) != -1);
            else
                overridelong |= (AStringUtils.Pos("FAR ", parameter1) != -1);
            if (!(overrideshort | overridelong) & (assemblerpreference != AAssemblerPreference.apnone))  //no override chooce by the user and not a normal preference
            {
                if (assemblerpreference == AAssemblerPreference.aplong)
                    overridelong = true;
                else if (assemblerpreference == AAssemblerPreference.apshort)
                    overrideshort = true;
            }
            paramtype1 = Assembler.gettokentype(ref parameter1, parameter2);
            paramtype2 = Assembler.gettokentype(ref parameter2, parameter1);
            paramtype3 = Assembler.gettokentype(ref parameter3, "");
            if (Assembler.symhandler.process.is64bit)
            {
                if (paramtype1 == ATokenType.ttregister8bitwithprefix)
                {
                    rexprefix = (Byte)(rexprefix | 0x40); //it at least has a prefix now
                    paramtype1 = ATokenType.ttregister8bit;
                }
                if (paramtype2 == ATokenType.ttregister8bitwithprefix)
                {
                    rexprefix = (Byte)(rexprefix | 0x40); //it at least has a prefix now
                    paramtype2 = ATokenType.ttregister8bit;
                }
                if (paramtype1 == ATokenType.ttregister64bit)
                {
                    rex_w = true;   //64-bit opperand
                    paramtype1 = ATokenType.ttregister32bit; //we can use the normal 32-bit interpretation assembler code
                }
                if (paramtype2 == ATokenType.ttregister64bit)
                {
                    rex_w = true;
                    paramtype2 = ATokenType.ttregister32bit;
                    if (paramtype1 == ATokenType.ttmemorylocation64)
                        paramtype1 = ATokenType.ttmemorylocation32;
                }
                if (paramtype1 == ATokenType.ttmemorylocation64)
                {
                    rex_w = true;
                    paramtype1 = ATokenType.ttmemorylocation32;
                }
                if (paramtype2 == ATokenType.ttmemorylocation64)
                {
                    rex_w = true;
                    paramtype2 = ATokenType.ttmemorylocation32;
                }
            }
            if (tokens[0] == "DW")
            {
                for (i = 1; i <= nroftokens - 1; i++)
                    Assembler.addword(bytes, (UInt16)AStringUtils.HexStrToInt(tokens[i]));
                result = true;
                return result;
            }
            if (tokens[0] == "DD")
            {
                for (i = 1; i <= nroftokens - 1; i++)
                    Assembler.adddword(bytes, (UInt16) AStringUtils.HexStrToInt(tokens[i]));
                result = true;
                return result;
            }
            if (tokens[0] == "DQ")
            {
                for (i = 1; i <= nroftokens - 1; i++)
                    Assembler.addqword(bytes, (UInt16)AStringUtils.HexStrToInt64(tokens[i]));
                result = true;
                return result;
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
                vtype = Assembler.stringvaluetotype(parameter1);
            }
            if (paramtype2 == ATokenType.ttvalue)
            {
                if (paramtype1 != ATokenType.ttvalue)
                {
                    v = AStringUtils.StrToQWordEx(parameter2);
                    vtype = Assembler.stringvaluetotype(parameter2);
                }
                else
                {
                    //first v field is already in use, use v2
                    v2 = AStringUtils.StrToQWordEx(parameter2);
                    v2type = Assembler.stringvaluetotype(parameter2);
                }
            }
            if (paramtype3 == ATokenType.ttvalue)
            {
                if (paramtype1 != ATokenType.ttvalue)
                {
                    v = AStringUtils.StrToQWordEx(parameter3);
                    vtype = Assembler.stringvaluetotype(parameter3);
                }
                else
                {
                    //first v field is already in use, use v2
                    v2 = AStringUtils.StrToQWordEx(parameter3);
                    v2type = Assembler.stringvaluetotype(parameter3);
                }
            }
            signedvtype = Assembler.signedvaluetotype((int)v);
            signedv2type = Assembler.signedvaluetotype((int)v2);
            result = false;
            //to make it easier for people that don't like the relative addressing limit
            if ((!overrideshort) & (!overridelong) & (Assembler.symhandler.process.is64bit))    //if 64-bit and no override is given
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
                            rexprefix = 0;
                            Assembler.add(bytes, 0xff);
                            if (tokens[mnemonic] == "JMP")
                            {
                                Assembler.add(bytes, 0x25);
                                Assembler.adddword(bytes, 0);
                            }
                            else
                            {
                                Assembler.add(bytes, 0x15); //call
                                Assembler.adddword(bytes, 2);
                                Assembler.add(bytes, 0xeb, 0x8);
                            }
                            Assembler.addqword(bytes, v);
                            result = true;
                            return result;
                        }
                    }
                }
            }
            j = Assembler.getopcodesindex(tokens[mnemonic]); //index scan, better than sorted
            if (j == -1)
                return result;
            startoflist = j;
            endoflist = startoflist;
            while ((endoflist <= Assembler.opcodecount) && (Assembler.opcodes[endoflist].mnemonic == tokens[mnemonic]))
                endoflist += 1;
            endoflist -= 1;
            try
            {
                while (j < Assembler.opcodecount)
                {
                    if (Assembler.opcodes[j].mnemonic != tokens[mnemonic])
                        return result;
                    if ((Assembler.opcodes[j].invalidin32bit & !is64bit) | (Assembler.opcodes[j].invalidin64bit & is64bit))
                    {
                        j += 1;
                        continue; // todo figure out if this is meant to be break/continue
                    }
                    switch (Assembler.opcodes[j].paramtype1)
                    {
                        case AParam.par_noparam:
                            if (parameter1 == "")      //no param
                            {
                                //no param
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //no_param,no_param,no_param
                                        if ((Assembler.opcodes[j].opcode1 == AExtraOpCode.eo_none) && (Assembler.opcodes[j].opcode1 == AExtraOpCode.eo_none))
                                        {
                                            //textraopcode.eo_none,textraopcode.eo_none--no_param,no_param,no_param
                                            addopcode(bytes, j);
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
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_al) && (parameter2 == "AL"))
                                {
                                    //imm8,al
                                    addopcode(bytes, j);
                                    Assembler.add(bytes, (Byte)v);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_ax) && (parameter2 == "AX"))
                                {
                                    //imm8,ax /?
                                    addopcode(bytes, j);
                                    Assembler.add(bytes, (Byte)v);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_eax) && ((parameter2 == "EAX") || (parameter2 == "RAX")))
                                {
                                    //imm8,eax
                                    addopcode(bytes, j);
                                    Assembler.add(bytes, (Byte)v);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    if (vtype == 16)
                                    {
                                        //see if there is also a 'opcode imm16' variant
                                        var k = startoflist;
                                        while ((k <= Assembler.opcodecount) && (Assembler.opcodes[k].mnemonic == tokens[mnemonic]))
                                        {
                                            if (Assembler.opcodes[k].paramtype1 == AParam.par_imm16)
                                            {
                                                addopcode(bytes, k);
                                                Assembler.addword(bytes, (UInt16)v);
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
                                        while ((k <= Assembler.opcodecount) && (Assembler.opcodes[k].mnemonic == tokens[mnemonic]))
                                        {
                                            if (Assembler.opcodes[k].paramtype1 == AParam.par_imm32)
                                            {
                                                addopcode(bytes, k);
                                                Assembler.adddword(bytes, (UInt32)v);
                                                result = true;
                                                return result;
                                            }
                                            k += 1;
                                        }
                                    }
                                    //op imm8
                                    addopcode(bytes, j);
                                    Assembler.add(bytes, (Byte)v);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_imm16:
                            if (paramtype1 == ATokenType.ttvalue)
                            {
                                //imm16,
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //imm16
                                    addopcode(bytes, j);
                                    Assembler.addword(bytes, (UInt16)v);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //imm16,imm8,
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        Assembler.addword(bytes, (UInt16)v);
                                        Assembler.add(bytes, (Byte)v2);
                                        result = true;
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.par_imm32:
                            if (paramtype1 == ATokenType.ttvalue)
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //imm32
                                    addopcode(bytes, j);
                                    Assembler.adddword(bytes, (UInt32)v);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_moffs8:
                            if ((paramtype1 == ATokenType.ttmemorylocation8) | (Assembler.ismemorylocationdefault(parameter1)))
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_al) && (parameter2 == "AL"))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        var k = AStringUtils.Pos("[", parameter1);
                                        var l = AStringUtils.Pos("]", parameter1);
                                        AStringUtils.Val("$" + AStringUtils.Copy(parameter1, k + 1, l - k - 1), out v, out k);
                                        if (k == -1)
                                        {
                                            //verified, it doesn't have a registerbase in it
                                            addopcode(bytes, j);
                                            Assembler.adddword(bytes, (UInt32)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                            }
                            break;
                        case AParam.par_moffs16:
                            if ((paramtype1 == ATokenType.ttmemorylocation16) | (Assembler.ismemorylocationdefault(parameter1)))
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_ax) && (parameter2 == "AX"))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        var k = AStringUtils.Pos("[", parameter1);
                                        var l = AStringUtils.Pos("]", parameter1);
                                        AStringUtils.Val("$" + AStringUtils.Copy(parameter1, k + 1, l - k - 1), out v, out k);
                                        if (k == -1)
                                        {
                                            //verified, it doesn't have a registerbase in it
                                            addopcode(bytes, j);
                                            Assembler.adddword(bytes, (UInt32)v);
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
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_eax) && ((parameter2 == "EAX") || (parameter2 == "RAX")))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        var k = AStringUtils.Pos("[", parameter1);
                                        var l = AStringUtils.Pos("]", parameter1);
                                        AStringUtils.Val("$" + AStringUtils.Copy(parameter1, k + l, l - k - 1), out v, out k);
                                        if (k == -1)
                                        {
                                            //verified, it doesn't have a registerbase in it
                                            addopcode(bytes, j);
                                            Assembler.adddword(bytes, (UInt32)v);
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
                                addopcode(bytes, j);
                                result = true;
                                return result;
                            }
                            break;
                        case AParam.par_al:
                            if (parameter1 == "AL")
                            {
                                //AL,
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_dx) && (parameter2 == "DX"))
                                {
                                    //opcode al,dx
                                    addopcode(bytes, j);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //AL,imm8
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if ((Assembler.opcodes[j].opcode1 == AExtraOpCode.eo_ib) && (Assembler.opcodes[j].opcode2 == AExtraOpCode.eo_none))
                                        {
                                            //verified: AL,imm8
                                            addopcode(bytes, j);
                                            Assembler.add(bytes, (Byte)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_moffs8) & ((paramtype2 == ATokenType.ttmemorylocation8) | (Assembler.ismemorylocationdefault(parameter2))))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        var k = AStringUtils.Pos("[", parameter2);
                                        var l = AStringUtils.Pos("]", parameter2);
                                        AStringUtils.Val("$" + AStringUtils.Copy(parameter2, k + l, l - k - 1), out v, out k);
                                        if (k == -1)
                                        {
                                            //verified, it doesn't have a registerbase in it
                                            addopcode(bytes, j);
                                            Assembler.adddword(bytes, (UInt32)v);
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
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //opcode AX
                                    addopcode(bytes, j);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_dx) && (parameter2 == "DX"))
                                {
                                    //opcode ax,dx
                                    addopcode(bytes, j);
                                    result = true;
                                    return result;
                                }
                                //r16
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_r16) && (paramtype2 == ATokenType.ttregister16bit))
                                {
                                    //eax,r32
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //r32,eax
                                        if (Assembler.opcodes[j].opcode1 == AExtraOpCode.eo_prw)
                                        {
                                            //opcode+rd
                                            addopcode(bytes, j);
                                            bytes[bytes.Length - 1] += (Byte)getreg(parameter2);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_imm16) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //AX,imm16
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //params confirmed it is a ax,imm16
                                        if ((Assembler.opcodes[j].opcode1 == AExtraOpCode.eo_iw) && (Assembler.opcodes[j].opcode2 == AExtraOpCode.eo_none))
                                        {
                                            addopcode(bytes, j);
                                            Assembler.addword(bytes, (UInt16)(v));
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_moffs16) & ((paramtype2 == ATokenType.ttmemorylocation16) | (Assembler.ismemorylocationdefault(parameter2))))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        var k = AStringUtils.Pos("[", parameter2);
                                        var l = AStringUtils.Pos("]", parameter2);
                                        AStringUtils.Val("$" + AStringUtils.Copy(parameter2, k + l, l - k - 1), out v, out k);
                                        if (k == -1)
                                        {
                                            //verified, it doesn't have a registerbase in it
                                            addopcode(bytes, j);
                                            Assembler.adddword(bytes, (UInt32)v);
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
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_dx) && (parameter2 == "DX"))
                                {
                                    //opcode eax,dx
                                    addopcode(bytes, j);
                                    result = true;
                                    return result;
                                }
                                //r32
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_r32) && (paramtype2 == ATokenType.ttregister32bit))
                                {
                                    //eax,r32
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //r32,eax
                                        if (Assembler.opcodes[j].opcode1 == AExtraOpCode.eo_prd)
                                        {
                                            //opcode+rd
                                            addopcode(bytes, j);
                                            var k = getreg(parameter2);
                                            if (k > 7)
                                            {
                                                rex_b = true; //extention to the opcode field
                                                k &= 7;
                                            }
                                            bytes[bytes.Length - 1] += (Byte)k;
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //eax,imm8
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        Assembler.add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_imm32) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //EAX,imm32,
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //eax,imm32
                                        if (signedvtype == 8)
                                        {
                                            //check if there isn't a rm32,imm8 , since that's less bytes
                                            var k = startoflist;
                                            while ((k <= Assembler.opcodecount) && (Assembler.opcodes[k].mnemonic == tokens[mnemonic]))
                                            {
                                                if ((Assembler.opcodes[k].paramtype1 == AParam.par_rm32) &&
                                                   (Assembler.opcodes[k].paramtype2 == AParam.par_imm8))
                                                {
                                                    //yes, there is
                                                    addopcode(bytes, k);
                                                    result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[k].opcode1), parameter1);
                                                    Assembler.add(bytes, (Byte)v);
                                                    return result;
                                                }
                                                k += 1;
                                            }
                                        }
                                        if ((Assembler.opcodes[j].opcode1 == AExtraOpCode.eo_id) && (Assembler.opcodes[j].opcode2 == AExtraOpCode.eo_none))
                                        {
                                            addopcode(bytes, j);
                                            Assembler.adddword(bytes, (UInt32)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_moffs32) & ((paramtype2 == ATokenType.ttmemorylocation32) | (Assembler.ismemorylocationdefault(parameter2))))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        var k = AStringUtils.Pos("[", parameter2);
                                        var l = AStringUtils.Pos("]", parameter2);
                                        AStringUtils.Val("$" + AStringUtils.Copy(parameter2, k + 1, l - k - 1), out v, out k);
                                        if (k == -1)
                                        {
                                            //verified, it doesn't have a registerbase in it
                                            addopcode(bytes, j);
                                            Assembler.adddword(bytes, (UInt32)v);
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
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_al) && (parameter2 == "AL"))
                                {
                                    addopcode(bytes, j);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_ax) && (parameter2 == "AX"))
                                {
                                    addopcode(bytes, j);
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_eax) && ((parameter2 == "EAX") || (parameter2 == "RAX")))
                                {
                                    addopcode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_cs:
                            if (parameter1 == "CS")
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    addopcode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_ds:
                            if (parameter1 == "DS")
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    addopcode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_es:
                            if (parameter1 == "ES")
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    addopcode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_ss:
                            if (parameter1 == "SS")
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    addopcode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_fs:
                            if (parameter1 == "FS")
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    addopcode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;

                        case AParam.par_gs:
                            if (parameter1 == "GS")
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    addopcode(bytes, j);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_r8:
                            if (paramtype1 == ATokenType.ttregister8bit)
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //opcode r8
                                    if (Assembler.opcodes[j].opcode1 == AExtraOpCode.eo_prb)
                                    {
                                        //opcode+rd
                                        addopcode(bytes, j);
                                        var k = getreg(parameter1);
                                        if (k > 7)
                                        {
                                            rex_b = true;
                                            k &= 7;
                                        }
                                        bytes[bytes.Length - 1] += (Byte)k;
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //r8, imm8
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if (Assembler.opcodes[j].opcode1 == AExtraOpCode.eo_prb)
                                        {
                                            addopcode(bytes, j);
                                            var k = getreg(parameter1);
                                            if (k > 7)
                                            {
                                                rex_b = true; //extension to the opcode
                                                k &= 7;
                                            }
                                            bytes[bytes.Length - 1] += (Byte)k;
                                            Assembler.add(bytes, (Byte)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_rm8) & (Assembler.isrm8(paramtype2)))
                                {
                                    //r8,rm8
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.par_r16:
                            if (paramtype1 == ATokenType.ttregister16bit)
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //opcode r16
                                    if (Assembler.opcodes[j].opcode1 == AExtraOpCode.eo_prw)
                                    {
                                        //opcode+rw
                                        addopcode(bytes, j);
                                        var k = getreg(parameter1);
                                        if (k > 7)
                                        {
                                            rex_b = true;
                                            k &= 7;
                                        }
                                        bytes[bytes.Length - 1] += (Byte)k;
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_ax) && (parameter2 == "AX"))
                                {
                                    //r16,ax,
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //r16,ax
                                        if (Assembler.opcodes[j].opcode1 == AExtraOpCode.eo_prw)
                                        {
                                            //opcode+rd
                                            addopcode(bytes, j);
                                            var k = getreg(parameter1);
                                            if (k > 7)
                                            {
                                                rex_b = true;
                                                k &= 7;
                                            }
                                            bytes[bytes.Length - 1] += (Byte)k;
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //r16, imm8
                                    if ((Assembler.opcodes[j].opcode1 == AExtraOpCode.eo_reg) && (Assembler.opcodes[j].opcode2 == AExtraOpCode.eo_ib))
                                    {
                                        if (vtype > 8)
                                        {
                                            //search for r16/imm16
                                            var k = startoflist;
                                            while ((k <= Assembler.opcodecount) && (Assembler.opcodes[k].mnemonic == tokens[mnemonic]))
                                            {
                                                if ((Assembler.opcodes[k].paramtype1 == AParam.par_r16) &&
                                                   (Assembler.opcodes[k].paramtype2 == AParam.par_imm16))
                                                {
                                                    if ((Assembler.opcodes[k].opcode1 == AExtraOpCode.eo_reg) && (Assembler.opcodes[j].opcode2 == AExtraOpCode.eo_ib))
                                                    {
                                                        addopcode(bytes, k);
                                                        result = createmodrm(bytes, getreg(parameter1), parameter1);
                                                        Assembler.addword(bytes, (UInt16)v);
                                                        return result;
                                                    }
                                                }
                                                k += 1;
                                            }
                                        }
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        Assembler.add(bytes, (Byte)v);
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_imm16) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if (Assembler.opcodes[j].opcode1 == AExtraOpCode.eo_prw)
                                        {
                                            addopcode(bytes, j);
                                            var k = getreg(parameter1);
                                            if (k > 7)
                                            {
                                                rex_b = true;
                                                k &= 7;
                                            }
                                            bytes[bytes.Length - 1] += (Byte)k;
                                            Assembler.addword(bytes, (UInt16)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_rm8) & (Assembler.isrm8(paramtype2)))
                                {
                                    //r16,r/m8 (eg: movzx)
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_rm16) & (Assembler.isrm16(paramtype2)))
                                {
                                    //r16,r/m16
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
                                    {
                                        if (Assembler.opcodes[j].opcode2 == AExtraOpCode.eo_ib)
                                        {
                                            //r16,r/m16,imm8
                                            if (vtype > 8)
                                            {
                                                //see if there is a //r16,r/m16,imm16
                                                var k = startoflist;
                                                while ((k <= Assembler.opcodecount) && (Assembler.opcodes[k].mnemonic == tokens[mnemonic]))
                                                {
                                                    if ((Assembler.opcodes[k].paramtype1 == AParam.par_r16) &&
                                                       (Assembler.opcodes[k].paramtype2 == AParam.par_rm16) &&
                                                       (Assembler.opcodes[k].paramtype3 == AParam.par_imm16))
                                                    {
                                                        addopcode(bytes, k);
                                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                                        Assembler.addword(bytes, (UInt16)v);
                                                        return result;
                                                    }
                                                    k += 1;
                                                }
                                            }
                                            addopcode(bytes, j);
                                            result = createmodrm(bytes, getreg(parameter1), parameter2);
                                            Assembler.add(bytes, (Byte)v);
                                            return result;
                                        }
                                    }
                                }
                            }
                            break;
                        case AParam.par_r32:
                            if (paramtype1 == ATokenType.ttregister32bit)
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //opcode r32
                                    if (Assembler.opcodes[j].opcode1 == AExtraOpCode.eo_prd)
                                    {
                                        //opcode+rd
                                        addopcode(bytes, j);
                                        var k = getreg(parameter1);
                                        if (k > 7)
                                        {
                                            rex_b = true;
                                            k &= 7;
                                        }
                                        bytes[bytes.Length - 1] += (Byte)k;
                                        result = true;
                                        return result;
                                    }
                                    else
                                    {
                                        //reg0..reg7
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                        return result;
                                    }
                                }
                                //eax
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_eax) && ((parameter2 == "EAX") || (parameter2 == "RAX")))
                                {
                                    //r32,eax,
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //r32,eax
                                        if (Assembler.opcodes[j].opcode1 == AExtraOpCode.eo_prd)
                                        {
                                            //opcode+rd
                                            addopcode(bytes, j);
                                            var k = getreg(parameter1);
                                            if (k > 7)
                                            {
                                                rex_b = true;
                                                k &= 7;
                                            }
                                            bytes[bytes.Length - 1] += (Byte)k;
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_mm) && (paramtype2 == ATokenType.ttregistermm))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }

                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_imm8) && (parameter3 == ""))
                                    {
                                        //32, mm,imm8
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        Assembler.add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm) && (paramtype2 == ATokenType.ttregisterxmm))
                                {
                                    //r32,xmm,
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter2), parameter1);
                                        return result;
                                    }

                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_imm8) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter2), parameter1);
                                        Assembler.add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_cr) && (paramtype2 == ATokenType.ttregistercr))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_dr) && (paramtype2 == ATokenType.ttregisterdr))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter2), parameter1);
                                        return result;
                                    }
                                }

                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm_m32) & (Assembler.isxmm_m32(paramtype2)))
                                {
                                    //r32,xmm/m32
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_mm_m64) && (Assembler.ismm_m64(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[1] == '['))))
                                {
                                    //r32,mm/m64
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, getreg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm_m64) && (Assembler.isxmm_m64(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[1] == '['))))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm_m128) && (Assembler.isxmm_m64(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[1] == '['))))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_m32) && (paramtype2 == ATokenType.ttmemorylocation32))
                                {
                                    //r32,m32,
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //r32,m32
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_rm8) & (Assembler.isrm8(paramtype2) | (Assembler.ismemorylocationdefault(parameter2))))
                                {
                                    //r32,rm8
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_rm16) & (Assembler.isrm16(paramtype2) | (Assembler.ismemorylocationdefault(parameter2))))
                                {
                                    //r32,rm16
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_rm32) & (Assembler.isrm32(paramtype2)))
                                {
                                    //r32,r/m32
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
                                    {
                                        if (Assembler.opcodes[j].opcode2 == AExtraOpCode.eo_ib)
                                        {
                                            if (vtype > 8)
                                            {
                                                var k = startoflist;
                                                while ((k <= endoflist) && (Assembler.opcodes[k].mnemonic == tokens[mnemonic]))
                                                {
                                                    if ((Assembler.opcodes[k].paramtype1 == AParam.par_r32) &&
                                                       (Assembler.opcodes[k].paramtype2 == AParam.par_rm32) &&
                                                       (Assembler.opcodes[k].paramtype3 == AParam.par_imm32))
                                                    {
                                                        addopcode(bytes, k);
                                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                                        Assembler.adddword(bytes, (UInt32)v);
                                                        return result;
                                                    }
                                                    k += 1;
                                                }
                                            }
                                            //r32,r/m32,imm8
                                            addopcode(bytes, j);
                                            result = createmodrm(bytes, getreg(parameter1), parameter2);
                                            Assembler.add(bytes, (Byte)v);
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_imm32) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //r32,imm32
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if (signedvtype == 8)
                                        {
                                            //check if there isn't a rm32,imm8 , since that's less bytes
                                            var k = startoflist;
                                            while ((k <= Assembler.opcodecount) && (Assembler.opcodes[k].mnemonic == tokens[mnemonic]))
                                            {
                                                if ((Assembler.opcodes[k].paramtype1 == AParam.par_rm32) &&
                                                   (Assembler.opcodes[k].paramtype2 == AParam.par_imm8))
                                                {
                                                    //yes, there is
                                                    addopcode(bytes, k);
                                                    result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[k].opcode1), parameter1);
                                                    Assembler.add(bytes, (Byte)v);
                                                    return result;
                                                }
                                                k += 1;
                                            }
                                        }
                                        if (Assembler.opcodes[j].opcode1 == AExtraOpCode.eo_prd)
                                        {
                                            addopcode(bytes, j);
                                            var k = getreg(parameter1);
                                            if (k > 7)
                                            {
                                                rex_b = true;
                                                k &= 7;
                                            }
                                            bytes[bytes.Length - 1] += (Byte)k;
                                            if (rex_w)
                                                Assembler.addqword(bytes, v);
                                            else
                                                Assembler.adddword(bytes, (UInt32)v);
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //r32, imm8
                                    addopcode(bytes, j);
                                    createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                    Assembler.add(bytes, (Byte)v);
                                    result = true;
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_sreg:
                            if (paramtype1 == ATokenType.ttregistersreg)
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_rm16) & (Assembler.isrm16(paramtype2)))
                                {
                                    //sreg,rm16
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, getreg(parameter1), parameter2);
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_cr:
                            if (paramtype1 == ATokenType.ttregistercr)
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_r32) && (paramtype2 == ATokenType.ttregister32bit))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.par_dr:
                            if (paramtype1 == ATokenType.ttregisterdr)
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_r32) && (paramtype2 == ATokenType.ttregister32bit))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.par_rm8:
                            if (Assembler.isrm8(paramtype1))
                            {
                                //r/m8,
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //opcode r/m8
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_1) && (paramtype2 == ATokenType.ttvalue) && (v == 1))
                                {
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_cl) && (parameter2 == "CL"))
                                {
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //r/m8,imm8,
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //verified it IS r/m8,imm8
                                        addopcode(bytes, j);
                                        createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                        Assembler.add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_r8) && (paramtype2 == ATokenType.ttregister8bit))
                                {
                                    // r/m8,r8
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.par_rm16:
                            if (Assembler.isrm16(paramtype1))
                            {
                                //r/m16,
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //opcode r/m16
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_1) && (paramtype2 == ATokenType.ttvalue) && (v == 1))
                                {
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if (vtype == 16)
                                        {
                                            //perhaps there is a r/m16,imm16
                                            var k = startoflist;
                                            while (k <= endoflist)
                                            {
                                                if (Assembler.opcodes[k].mnemonic != tokens[mnemonic])
                                                    //nope, so continue with r/m,imm16
                                                    continue; // todo figure out if this is meant to be break/continue
                                                if (((Assembler.opcodes[k].paramtype1 == AParam.par_rm16) && (Assembler.opcodes[k].paramtype2 == AParam.par_imm16)) && ((Assembler.opcodes[k].paramtype3 == AParam.par_noparam) && (parameter3 == "")))
                                                {
                                                    //yes, there is
                                                    //r/m16,imm16
                                                    addopcode(bytes, k);
                                                    createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[k].opcode1), parameter1);
                                                    Assembler.addword(bytes, (UInt16)(v));
                                                    result = true;
                                                    return result;
                                                }
                                                k += 1;
                                            }
                                        }
                                        //nope, so it IS r/m16,8
                                        addopcode(bytes, j);
                                        createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                        Assembler.add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_imm16) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //r/m16,imm
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if (vtype == 8)
                                        {
                                            //see if there is a r/m16,imm8 (or if this is the one) (optimisation)
                                            var k = startoflist;
                                            while (k <= endoflist)
                                            {
                                                if (Assembler.opcodes[k].mnemonic != tokens[mnemonic])
                                                    //nope, so continue with r/m,imm16
                                                    continue; // todo figure out if this is meant to be break/continue
                                                if (((Assembler.opcodes[k].paramtype1 == AParam.par_rm16) && (Assembler.opcodes[k].paramtype2 == AParam.par_imm8)) && ((Assembler.opcodes[k].paramtype3 == AParam.par_noparam) && (parameter3 == "")))
                                                {
                                                    //yes, there is
                                                    //r/m16,imm8
                                                    addopcode(bytes, k);
                                                    createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[k].opcode1), parameter1);
                                                    Assembler.add(bytes, (Byte)v);
                                                    result = true;
                                                    return result;
                                                }
                                                k += 1;
                                            }
                                        }
                                        addopcode(bytes, j);
                                        createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                        Assembler.addword(bytes, (UInt16)(v));
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_r16) && (paramtype2 == ATokenType.ttregister16bit))
                                {
                                    //r/m16,r16,
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_cl) && (parameter3 == "CL"))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter2), parameter1);
                                        return result;
                                    }
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter2), parameter1);
                                        return result;
                                    }
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
                                    {
                                        //rm16, r16,imm8
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter2), parameter1);
                                        Assembler.add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_sreg) && (paramtype2 == ATokenType.ttregistersreg))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //r/m16,sreg
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_cl) && (parameter2 == "CL"))
                                {
                                    //rm16,cl
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_rm32:
                            if (Assembler.isrm32(paramtype1))
                            {
                                //r/m32,
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //no 2nd parameter so it is 'opcode r/m32'
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_1) && (paramtype2 == ATokenType.ttvalue) && (v == 1))
                                {
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //rm32,imm8
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if ((vtype > 8) || (Assembler.opcodes[j].signed & (signedvtype > 8)))
                                        {
                                            //the user requests a bigger than 8-bit value, so see if there is also a rm32,imm32 (there are no r/m32,imm16)
                                            var k = startoflist;
                                            while (k <= endoflist)
                                            {
                                                if (Assembler.opcodes[k].mnemonic != tokens[mnemonic])
                                                    continue; // todo figure out if this is meant to be break/continue
                                                if (((Assembler.opcodes[k].paramtype1 == AParam.par_rm32) && (Assembler.opcodes[k].paramtype2 == AParam.par_imm32)) && ((Assembler.opcodes[k].paramtype3 == AParam.par_noparam) && (parameter3 == "")))
                                                {
                                                    //yes, there is
                                                    addopcode(bytes, k);
                                                    createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[k].opcode1), parameter1);
                                                    Assembler.adddword(bytes, (UInt32)v);
                                                    result = true;
                                                    return result;
                                                }
                                                k += 1;
                                            }
                                        }
                                        //r/m32,imm8
                                        addopcode(bytes, j);
                                        createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                        Assembler.add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_imm32) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    //r/m32,imm
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if (signedvtype == 8)
                                        {
                                            //see if there is a r/m32,imm8 (or if this is the one) (optimisation)
                                            var k = startoflist;
                                            while (k <= endoflist)
                                            {
                                                if (Assembler.opcodes[k].mnemonic != tokens[mnemonic])
                                                    //nope, so continue with r/m,imm16
                                                    continue; // todo figure out if this is meant to be break/continue
                                                if (((Assembler.opcodes[k].paramtype1 == AParam.par_rm32) && (Assembler.opcodes[k].paramtype2 == AParam.par_imm8)) && ((Assembler.opcodes[k].paramtype3 == AParam.par_noparam) && (parameter3 == "")) && ((!Assembler.opcodes[k].signed) | (signedvtype == 8)))
                                                {
                                                    //yes, there is
                                                    addopcode(bytes, k);
                                                    createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[k].opcode1), parameter1);
                                                    Assembler.add(bytes, (Byte)v);
                                                    result = true;
                                                    return result;
                                                }
                                                k += 1;
                                            }
                                        }
                                        //no there's none
                                        addopcode(bytes, j);
                                        createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                        Assembler.adddword(bytes, (UInt32)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_cl) && (parameter2 == "CL"))
                                {
                                    //rm32,cl
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_r32) && (paramtype2 == ATokenType.ttregister32bit))
                                {
                                    //r/m32,r32
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter2), parameter1);
                                        return result;
                                    }
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_cl) && (parameter3 == "CL"))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter2), parameter1);
                                        return result;
                                    }
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter2), parameter1);
                                        Assembler.add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_mm) && (paramtype2 == ATokenType.ttregistermm))
                                {
                                    //r32/m32,mm,
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //r32/m32,mm
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter2), parameter1);
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm) && (paramtype2 == ATokenType.ttregisterxmm))
                                {
                                    //r32/m32,xmm,  (movd for example)
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //r32/m32,xmm
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter2), parameter1);
                                    }
                                }
                            }
                            break;
                        case AParam.par_mm:
                            if (paramtype1 == ATokenType.ttregistermm)
                            {
                                //mm,xxxxx
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_mm) && (paramtype2 == ATokenType.ttregistermm))
                                {
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, getreg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm) && (paramtype2 == ATokenType.ttregisterxmm))
                                {
                                    //mm,xmm
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, getreg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_r32_m16) & ((paramtype1 == ATokenType.ttregister32bit) || (paramtype2 == ATokenType.ttmemorylocation16) | Assembler.ismemorylocationdefault(parameter2)))
                                {
                                    //mm,r32/m16,
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
                                    {
                                        //imm8
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_rm32) & (Assembler.isrm32(paramtype2)))
                                {
                                    //mm,rm32
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //xmm,rm32
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm_m32) & (Assembler.isxmm_m32(paramtype2)))
                                {
                                    //mm,xmm/m32
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, getreg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_mm_m64) && (Assembler.ismm_m64(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[1] == '['))))
                                {
                                    //mm,mm/m64
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, getreg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm_m64) && (Assembler.isxmm_m64(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[1] == '['))))
                                {
                                    //mm,xmm/m64
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, getreg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm_m128) && (Assembler.isxmm_m128(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[1] == '['))))
                                {
                                    //mm,xmm/m128
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, getreg(parameter1), parameter2);
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_mm_m64:
                            if (Assembler.ismm_m64(paramtype1) | ((paramtype1 == ATokenType.ttmemorylocation32) && (parameter1[1] == '[')))
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_mm) && (paramtype2 == ATokenType.ttregistermm))
                                {
                                    //mm/m64, mm
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, getreg(parameter2), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_xmm_m64:
                            if (Assembler.isxmm_m64(paramtype1) | ((paramtype1 == ATokenType.ttmemorylocation32) && (parameter1[1] == '[')))
                            {
                                //xmm/m64,
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm) && (paramtype2 == ATokenType.ttregisterxmm))
                                {
                                    //xmm/m64, xmm
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, getreg(parameter2), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_xmm_m128:
                            if (Assembler.isxmm_m128(paramtype1) | ((paramtype1 == ATokenType.ttmemorylocation32) && (parameter1[1] == '[')))
                            {
                                //xmm/m128,
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm) && (paramtype2 == ATokenType.ttregisterxmm))
                                {
                                    //xmm/m128, xmm
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, getreg(parameter2), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_xmm:
                            if (paramtype1 == ATokenType.ttregisterxmm)
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_imm8) && (paramtype2 == ATokenType.ttvalue))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                        Assembler.add(bytes, (Byte)v);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_mm) && (paramtype2 == ATokenType.ttregistermm))
                                {
                                    //xmm,xmm
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, getreg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm) && (paramtype2 == ATokenType.ttregisterxmm))
                                {
                                    //xmm,xmm
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, getreg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_m64) & ((paramtype2 == ATokenType.ttmemorylocation64) | (Assembler.ismemorylocationdefault(parameter2))))
                                {
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, getreg(parameter1), parameter2);
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_rm32) & (Assembler.isrm32(paramtype2)))
                                {
                                    //xmm,rm32,
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //xmm,rm32
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_mm_m64) && (Assembler.ismm_m64(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[1] == '['))))
                                {
                                    //xmm,mm/m64
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //xmm,mm/m64
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm_m32) & Assembler.isxmm_m32(paramtype2))
                                {
                                    //even if the user didn't intend for it to be xmm,m64 it will be, that'll teach the lazy user to forget opperand size
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //xmm,xmm/m32
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
                                    {
                                        addopcode(bytes, j);
                                        createmodrm(bytes, getreg(parameter1), parameter2);
                                        Assembler.add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm_m64) && (Assembler.isxmm_m64(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[1] == '['))))
                                {
                                    //even if the user didn't intend for it to be xmm,m64 it will be, that'll teach the lazy user to forget opperand size
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //xmm,xmm/m64
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
                                    {
                                        addopcode(bytes, j);
                                        createmodrm(bytes, getreg(parameter1), parameter2);
                                        Assembler.add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm_m128) && (Assembler.isxmm_m128(paramtype2) | ((paramtype2 == ATokenType.ttmemorylocation32) && (parameter2[1] == '['))))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        //xmm,xmm/m128
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter1), parameter2);
                                        return result;
                                    }
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_imm8) && (paramtype3 == ATokenType.ttvalue))
                                    {
                                        addopcode(bytes, j);
                                        createmodrm(bytes, getreg(parameter1), parameter2);
                                        Assembler.add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.par_m8:
                            if ((paramtype1 == ATokenType.ttmemorylocation8) | Assembler.ismemorylocationdefault(parameter1))
                            {
                                //m8,xxx
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //m8
                                    //                                 //check if it is especially designed to be 32 bit, or if it is a default anser
                                    //verified, it is a 8 bit location, and if it was detected as 8 it was due to defaulting to 32
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_m16:
                            if ((paramtype1 == ATokenType.ttmemorylocation16) | Assembler.ismemorylocationdefault(parameter1))
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //opcode+rd
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                    return result;
                                }
                            break;
                        case AParam.par_m32:
                            if (paramtype1 == ATokenType.ttmemorylocation32)
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                    return result;
                                }
                                if (Assembler.opcodes[j].paramtype2 == AParam.par_r32)
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm) & ((paramtype2 == ATokenType.ttregisterxmm) | Assembler.ismemorylocationdefault(parameter2)))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) || (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.par_m64:
                            if ((paramtype1 == ATokenType.ttmemorylocation64) || (paramtype1 == ATokenType.ttmemorylocation32))
                            {
                                //m64,
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //m64
                                    if (Assembler.gettokentype(ref parameter1, parameter2) == ATokenType.ttmemorylocation64)
                                    {
                                        //verified, it is a 64 bit location, and if it was detected as 32 it was due to defaulting to 32
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm) & ((paramtype2 == ATokenType.ttregisterxmm) | Assembler.ismemorylocationdefault(parameter2)))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) || (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm) & ((paramtype2 == ATokenType.ttregisterxmm) | Assembler.ismemorylocationdefault(parameter2)))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) || (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.par_m80:
                            if ((paramtype1 == ATokenType.ttmemorylocation80) || ((paramtype1 == ATokenType.ttmemorylocation32) && (parameter1[1] == '[')))
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    addopcode(bytes, j);
                                    result = createmodrm(bytes, Assembler.eotoreg(Assembler.opcodes[j].opcode1), parameter1);
                                    return result;
                                }
                            }
                            break;
                        case AParam.par_m128:
                            if ((paramtype1 == ATokenType.ttmemorylocation128) | (Assembler.ismemorylocationdefault(parameter1)))
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_xmm) && (paramtype2 == ATokenType.ttregisterxmm))
                                {
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        addopcode(bytes, j);
                                        result = createmodrm(bytes, getreg(parameter2), parameter1);
                                        return result;
                                    }
                                }
                            }
                            break;
                        case AParam.par_rel8:
                            if (paramtype1 == ATokenType.ttvalue)
                            {
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //rel8
                                    if (AArrayUtils.InArray(parameter1[1], '-', '+'))
                                    {
                                        if (((!overrideshort) & (vtype > 8)) | (overridelong))
                                        {
                                            //see if there is a 32 bit equivalent opcode (notice I dont do rel 16 because that'll completly screw up eip)
                                            var k = startoflist;
                                            while ((k <= Assembler.opcodecount) && (Assembler.opcodes[k].mnemonic == tokens[mnemonic]))
                                            {
                                                if ((Assembler.opcodes[k].paramtype1 == AParam.par_rel32) && (Assembler.opcodes[k].paramtype2 == AParam.par_noparam))
                                                {
                                                    //yes, there is a 32 bit version
                                                    addopcode(bytes, k);
                                                    Assembler.adddword(bytes, (UInt32)v);
                                                    result = true;
                                                    return result;
                                                }
                                                k += 1;
                                            }
                                        }
                                        addopcode(bytes, j);
                                        Assembler.add(bytes, (Byte)v);
                                        result = true;
                                        return result;
                                    }
                                    else
                                    {
                                        //user typed in a direct address
                                        //        if (not overrideShort) and ((OverrideLong) or (valueTotype(      v-address-       (Assembler.opcodes[j].bytes+1) )>8) ) then
                                        if ((!overrideshort) & ((overridelong) | (Assembler.valuetotype((UInt32)(v - address - (UInt32)(Assembler.opcodes[j].bytes + 1))) > 8)))
                                        {
                                            //the user tried to find a relative address out of it's reach
                                            //see if there is a 32 bit version of the opcode
                                            var k = startoflist;
                                            while ((k <= Assembler.opcodecount) && (Assembler.opcodes[k].mnemonic == tokens[mnemonic]))
                                            {
                                                if ((Assembler.opcodes[k].paramtype1 == AParam.par_rel32) && (Assembler.opcodes[k].paramtype2 == AParam.par_noparam))
                                                {
                                                    //yes, there is a 32 bit version
                                                    addopcode(bytes, k);
                                                    Assembler.adddword(bytes, (UInt32)(v - address - (UInt32)(Assembler.opcodes[k].bytes + 4)));
                                                    result = true;
                                                    return result;
                                                }
                                                k += 1;
                                            }
                                        }
                                        else
                                        {
                                            //8 bit version
                                            addopcode(bytes, j);
                                            Assembler.add(bytes, (Byte)(v - address - (UInt32)(Assembler.opcodes[j].bytes + 1)));
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
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    if (AArrayUtils.InArray(parameter1[1], '-', '+'))
                                    {
                                        //opcode rel32
                                        addopcode(bytes, j);
                                        Assembler.adddword(bytes, (UInt32)v);
                                        result = true;
                                        return result;
                                    }
                                    else
                                    {
                                        //user typed in a direct address
                                        addopcode(bytes, j);
                                        Assembler.adddword(bytes, (UInt32)(v - address - (UInt32)(Assembler.opcodes[j].bytes + 4)));
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
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_st) && (paramtype2 == ATokenType.ttregisterst))
                                {
                                    //st(0),st(x),
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if (Assembler.opcodes[j].opcode1 == AExtraOpCode.eo_pi)
                                        {
                                            //opcode+i
                                            addopcode(bytes, j);
                                            var k = getreg(parameter2);
                                            if (k > 7)
                                            {
                                                rex_b = true;
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
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_noparam) && (parameter2 == ""))
                                {
                                    //st(x)
                                    addopcode(bytes, j);
                                    var k = getreg(parameter1);
                                    if (k > 7)
                                    {
                                        rex_b = true;
                                        k &= 7;
                                    }
                                    bytes[bytes.Length - 1] += (Byte)k;
                                    result = true;
                                    return result;
                                }
                                if ((Assembler.opcodes[j].paramtype2 == AParam.par_st0) && ((parameter2 == "ST(0)") || (parameter2 == "ST")))
                                {
                                    //st(x),st(0)
                                    if ((Assembler.opcodes[j].paramtype3 == AParam.par_noparam) && (parameter3 == ""))
                                    {
                                        if (Assembler.opcodes[j].opcode1 == AExtraOpCode.eo_pi)
                                        {
                                            //opcode+i
                                            addopcode(bytes, j);
                                            var k = getreg(parameter1);
                                            if (k > 7)
                                            {
                                                rex_b = true;
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
                    j += 1;
                }
            }
            finally
            {
                if (result)
                {
                    //insert rex prefix if needed
                    if (Assembler.symhandler.process.is64bit)
                    {
                        if (Assembler.opcodes[j].norexw)
                            rex_w = false;
                        if (rexprefix != 0)
                        {
                            if (rexprefixlocation == -1)
                                throw new Exception("Assembler error");
                            rexprefix = (Byte)(rexprefix | 0x40); //just make sure this is set
                            bytes.SetLength(bytes.Length + 1);
                            for (i = bytes.Length - 1; i >= rexprefixlocation + 1; i--)
                                bytes[i] = bytes[i - 1];
                            bytes[rexprefixlocation] = rexprefix;
                            if (relativeaddresslocation != -1)
                                relativeaddresslocation += 1;
                        }
                        if (relativeaddresslocation != -1)
                        {
                            //adjust the specified address so it's relative (The outside of range check is already done in the modrm generation)
                            if (actualdisplacement > (address + (UInt32)bytes.Length))
                                v = actualdisplacement - (address + (UInt32)bytes.Length);
                            else
                                v = (address + (UInt32)bytes.Length) - actualdisplacement;
                            if (v > 0x7fffffff)
                            {
                                bytes.SetLength(0);
                                if (skiprangecheck == false)   //for syntax checking
                                    throw new Exception("offset too big");
                            }
                            else
                            {
                                unsafe
                                {
                                    fixed (byte* b = bytes.Raw)
                                    {
                                        var vp = (UInt32)(actualdisplacement - (address + (UInt32)bytes.Length));
                                        *(UInt32*)b[relativeaddresslocation] = vp;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
        #endregion
    }
}