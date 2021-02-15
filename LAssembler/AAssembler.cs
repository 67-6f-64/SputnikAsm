using System;
using Sputnik.LUtils;
using SputnikAsm.LAssembler.LEnums;
using SputnikAsm.LCollections;
using SputnikAsm.LUtils;

namespace SputnikAsm.LAssembler
{
    public class AAssembler
    {
        #region dumy
        public class TSymhandler
        {
            public TProcess process= new TProcess();
            public IntPtr getaddressfromname(String name, Boolean waitforsymbols, out Boolean haserror)
            {
                if (name == "shinobi.exe")
                {
                    haserror = false;
                    return (IntPtr) 0x400300;
                }
                if (name == "cat")
                {
                    haserror = false;
                    return (IntPtr)0x777;
                }
                haserror = true;
                return IntPtr.Zero;
            }
        }
        public class TProcess
        {
            public Boolean is64bit = true;
        }
        #endregion
        #region opcodes
        public AOpCode[] opcodes;
        #endregion
        #region Variables
        public int parameter1, parameter2, parameter3;
        public int opcodenr;
        public AIndexArray assemblerindex;
        public TSymhandler symhandler = new TSymhandler();
        public ASingleLineAssembler singlelineassembler;
        #endregion
        #region Properties
        public int opcodecount => opcodes.Length;
        #endregion
        #region Constructor
        public AAssembler()
        {
            opcodes = AOpCodes.GetOpCodes();
            parameter1 = 0;
            parameter2 = 0;
            parameter3 = 0;
            opcodenr = 0;
            assemblerindex = new AIndexArray(25);
            var lastentry = 0;
            AIndex lastindex = null;
            for (var i = 0; i < assemblerindex.Length; i++)
            {
                assemblerindex[i].StartEntry = -1;
                assemblerindex[i].NextEntry = -1;
                assemblerindex[i].SubIndex = null;
                for (var j = lastentry; j < opcodecount; j++)
                {
                    if (opcodes[j].mnemonic[0] == 'A' + i)
                    {
                        //found the first entry with this as first character
                        if (lastindex != null)
                            lastindex.NextEntry = j;
                        lastindex = assemblerindex[i];
                        assemblerindex[i].StartEntry = j;
                        assemblerindex[i].SubIndex = null; //default initialization
                        lastentry = j;
                        break; // todo check if should be continue
                    }
                    if (opcodes[j].mnemonic[0] > 'A' + i)
                        break; // passed it // todo check if should be continue
                }
            }
            if (assemblerindex.Last.StartEntry != -1)
                assemblerindex.Last.NextEntry = opcodecount;
            //fill in the subindexes
            for (var i = 0; i < assemblerindex.Length; i++)
            {
                if (assemblerindex[i].StartEntry == -1)
                    continue;
                //initialize subindex
                assemblerindex[i].SubIndex = new AIndexArray(25);
                for (var j = 0; j < assemblerindex.Length; j++)
                {
                    assemblerindex[i].SubIndex[j].StartEntry = -1;
                    assemblerindex[i].SubIndex[j].NextEntry = -1;
                    assemblerindex[i].SubIndex[j].SubIndex = null;
                }
                lastindex = null;
                if (assemblerindex[i].NextEntry == -1)  //last one in the list didn't get a assignment
                    assemblerindex[i].NextEntry = opcodecount + 1;
                for (var j = 0; j < assemblerindex.Length; j++)
                {
                    for (var k = assemblerindex[i].StartEntry; k < assemblerindex[i].NextEntry - 1; k++)
                    {
                        if (opcodes[k].mnemonic[0] == 'A' + j)
                        {
                            if (lastindex != null)
                                lastindex.NextEntry = k;
                            lastindex = assemblerindex[i].SubIndex[j];
                            assemblerindex[i].SubIndex[j].StartEntry = k;
                            break; // todo check if should be continue
                        }
                    }
                }
            }
            singlelineassembler = new ASingleLineAssembler(this);
        }
        #endregion
        #region getopcodesindex
        /*
        will return the first entry in the opcodes list for this opcode
        If not found, -1
        */
        public int getopcodesindex(string opcode)
        {
            int i;
            int index1, index2;
            AIndex bestindex;
            int minindex, maxindex;
            opcode = opcode.ToUpper();
            var result = -1;
            if (opcode.Length <= 0)
                return result;
            index1 = opcode[0] - 'A';
            if ((index1 < 0) || (index1 >= 25))
                return result; //not alphabetical
            bestindex = assemblerindex[index1];
            if (bestindex.StartEntry == -1)
                return result;
            if ((assemblerindex[index1].SubIndex != null) && (opcode.Length > 1))
            {
                index2 = opcode[0] - 'A';
                if ((index2 < 0) || (index2 >= 25))
                    return result; //not alphabetical
                bestindex = assemblerindex[index1].SubIndex[index2];
                if (bestindex.StartEntry == -1)
                    return result; //no subitem2
            }
            minindex = bestindex.StartEntry;
            maxindex = bestindex.NextEntry;
            if (maxindex == -1)
                if (assemblerindex[index1].NextEntry != -1)
                    maxindex = assemblerindex[index1].NextEntry;
                else
                    maxindex = opcodecount;
            if (maxindex > opcodecount)
                maxindex = opcodecount;
            //now scan from minindex to maxindex for opcode
            for (i = minindex; i <= maxindex; i++)
            {
                if (opcodes[i].mnemonic == opcode)
                {
                    result = i; //found it
                    return result;
                }
                if (opcodes[i].mnemonic[0] != opcode[0])
                    return result;
            }
            //still here, not found, -1
            return -1;
        }
        #endregion
        #region ismemorylocationdefault
        public Boolean ismemorylocationdefault(string parameter)
        {
            return parameter[0] == '[' && parameter[parameter.Length - 1] == ']';
        }
        #endregion
        #region add
        public void add(AByteArray bytes, params Byte[] a)
        {
            bytes.EnsureCapacity(bytes.Length + a.Length);
            for (var i = 0; i < a.Length; i++)
                bytes[bytes.Length - a.Length + i] = a[i];
        }
        public void add(AByteArray bytes, Byte a)
        {
            add(bytes, new[] {a});
        }
        #endregion
        #region addword
        public void addword(AByteArray bytes, UInt16 a)
        {
            add(bytes, (Byte)a);
            add(bytes, (Byte)(a >> 8));
        }
        #endregion
        #region adddword
        public void adddword(AByteArray bytes, UInt32 a)
        {
            add(bytes, (Byte)a);
            add(bytes, (Byte)(a >> 8));
            add(bytes, (Byte)(a >> 16));
            add(bytes, (Byte)(a >> 24));
        }
        #endregion
        #region addqword
        public void addqword(AByteArray bytes, UInt64 a)
        {
            add(bytes, (Byte)a);
            add(bytes, (Byte)(a >> 8));
            add(bytes, (Byte)(a >> 16));
            add(bytes, (Byte)(a >> 24));
            add(bytes, (Byte)(a >> 32));
            add(bytes, (Byte)(a >> 40));
            add(bytes, (Byte)(a >> 48));
            add(bytes, (Byte)(a >> 56));
        }
        #endregion
        #region addstring
        public void addstring(AByteArray bytes, String s)
        {
            var j = bytes.Length;
            bytes.EnsureCapacity(bytes.Length + s.Length - 2); //not the quotes;
            for (var i = 1; i < s.Length - 1; i++, j++)
                bytes[j] = (Byte)s[i];
        }
        #endregion
        #region valuetotype
        public int valuetotype(UInt32 value)
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
        #region signedvaluetotype
        public int signedvaluetotype(int value)
        {
            var result = 8;
            if ((value < -128) || (value > 127))
                result = 16;
            if ((value < -32768) || (value > 32767))
                result = 32;
            return result;
        }
        #endregion
        #region stringvaluetotype
        public int stringvaluetotype(string value)
        {
            //this function converts a string to a valuetype depending on how it is written
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
                result = valuetotype(x); //not a specific ammount of characters given
            return result;
        }
        #endregion
        #region getreg
        public int getreg(string reg)
        {
            return getreg(reg, true);
        }
        public int getreg(string reg, Boolean exceptonerror)
        {
            var result = -1;
            if ((reg == "RAX") || (reg == "EAX") || (reg == "AX") || (reg == "AL") || (reg == "MM0") || (reg == "XMM0") || (reg == "ST(0)") || (reg == "ST") || (reg == "ES") || (reg == "CR0") || (reg == "DR0"))
                result = 0;
            if ((reg == "RCX") || (reg == "ECX") || (reg == "CX") || (reg == "CL") || (reg == "MM1") || (reg == "XMM1") || (reg == "ST(1)") || (reg == "CS") || (reg == "CR1") || (reg == "DR1"))
                result = 1;
            if ((reg == "RDX") || (reg == "EDX") || (reg == "DX") || (reg == "DL") || (reg == "MM2") || (reg == "XMM2") || (reg == "ST(2)") || (reg == "SS") || (reg == "CR2") || (reg == "DR2"))
                result = 2;
            if ((reg == "RBX") || (reg == "EBX") || (reg == "BX") || (reg == "BL") || (reg == "MM3") || (reg == "XMM3") || (reg == "ST(3)") || (reg == "DS") || (reg == "CR3") || (reg == "DR3"))
                result = 3;
            if ((reg == "SPL") || (reg == "RSP") || (reg == "ESP") || (reg == "SP") || (reg == "AH") || (reg == "MM4") || (reg == "XMM4") || (reg == "ST(4)") || (reg == "FS") || (reg == "CR4") || (reg == "DR4"))
                result = 4;
            if ((reg == "BPL") || (reg == "RBP") || (reg == "EBP") || (reg == "BP") || (reg == "CH") || (reg == "MM5") || (reg == "XMM5") || (reg == "ST(5)") || (reg == "GS") || (reg == "CR5") || (reg == "DR5"))
                result = 5;
            if ((reg == "SIL") || (reg == "RSI") || (reg == "ESI") || (reg == "SI") || (reg == "DH") || (reg == "MM6") || (reg == "XMM6") || (reg == "ST(6)") || (reg == "HS") || (reg == "CR6") || (reg == "DR6"))
                result = 6;
            if ((reg == "DIL") || (reg == "RDI") || (reg == "EDI") || (reg == "DI") || (reg == "BH") || (reg == "MM7") || (reg == "XMM7") || (reg == "ST(7)") || (reg == "IS") || (reg == "CR7") || (reg == "DR7"))
                result = 7;
            if (reg == "R8")
                result = 8;
            if (reg == "R9")
                result = 9;
            if (reg == "R10")
                result = 10;
            if (reg == "R11")
                result = 11;
            if (reg == "R12")
                result = 12;
            if (reg == "R13")
                result = 13;
            if (reg == "R14")
                result = 14;
            if (reg == "R15")
                result = 15;
            if (result == -1 && exceptonerror)
                throw new Exception("Invalid register");
            return result;
        }
        #endregion
        #region tokentoregisterbit
        public ATokenType tokentoregisterbit(string token)
        {
            var result = ATokenType.ttregister32bit;
            if (token == "AL") result = ATokenType.ttregister8bit;
            else
            if (token == "CL") result = ATokenType.ttregister8bit;
            else
            if (token == "DL") result = ATokenType.ttregister8bit;
            else
            if (token == "BL") result = ATokenType.ttregister8bit;
            else
            if (token == "AH") result = ATokenType.ttregister8bit;
            else
            if (token == "CH") result = ATokenType.ttregister8bit;
            else
            if (token == "DH") result = ATokenType.ttregister8bit;
            else if (token == "BH") result = ATokenType.ttregister8bit;
            else
            if (token == "AX") result = ATokenType.ttregister16bit;
            else
            if (token == "CX") result = ATokenType.ttregister16bit;
            else
            if (token == "DX") result = ATokenType.ttregister16bit;
            else
            if (token == "BX") result = ATokenType.ttregister16bit;
            else
            if (token == "SP") result = ATokenType.ttregister16bit;
            else
            if (token == "BP") result = ATokenType.ttregister16bit;
            else
            if (token == "SI") result = ATokenType.ttregister16bit;
            else
            if (token == "DI") result = ATokenType.ttregister16bit;
            else

            if (token == "EAX") result = ATokenType.ttregister32bit;
            else
            if (token == "ECX") result = ATokenType.ttregister32bit;
            else
            if (token == "EDX") result = ATokenType.ttregister32bit;
            else
            if (token == "EBX") result = ATokenType.ttregister32bit;
            else
            if (token == "ESP") result = ATokenType.ttregister32bit;
            else
            if (token == "EBP") result = ATokenType.ttregister32bit;
            else
            if (token == "ESI") result = ATokenType.ttregister32bit;
            else
            if (token == "EDI") result = ATokenType.ttregister32bit;
            else

            if (token == "MM0") result = ATokenType.ttregistermm;
            else
            if (token == "MM1") result = ATokenType.ttregistermm;
            else
            if (token == "MM2") result = ATokenType.ttregistermm;
            else
            if (token == "MM3") result = ATokenType.ttregistermm;
            else
            if (token == "MM4") result = ATokenType.ttregistermm;
            else
            if (token == "MM5") result = ATokenType.ttregistermm;
            else
            if (token == "MM6") result = ATokenType.ttregistermm;
            else
            if (token == "MM7") result = ATokenType.ttregistermm;
            else

            if (token == "XMM0") result = ATokenType.ttregisterxmm;
            else
            if (token == "XMM1") result = ATokenType.ttregisterxmm;
            else
            if (token == "XMM2") result = ATokenType.ttregisterxmm;
            else
            if (token == "XMM3") result = ATokenType.ttregisterxmm;
            else
            if (token == "XMM4") result = ATokenType.ttregisterxmm;
            else
            if (token == "XMM5") result = ATokenType.ttregisterxmm;
            else
            if (token == "XMM6") result = ATokenType.ttregisterxmm;
            else
            if (token == "XMM7") result = ATokenType.ttregisterxmm;
            else


            if (token == "ST") result = ATokenType.ttregisterst;
            else
            if (token == "ST(0)") result = ATokenType.ttregisterst;
            else
            if (token == "ST(1)") result = ATokenType.ttregisterst;
            else
            if (token == "ST(2)") result = ATokenType.ttregisterst;
            else
            if (token == "ST(3)") result = ATokenType.ttregisterst;
            else
            if (token == "ST(4)") result = ATokenType.ttregisterst;
            else
            if (token == "ST(5)") result = ATokenType.ttregisterst;
            else
            if (token == "ST(6)") result = ATokenType.ttregisterst;
            else
            if (token == "ST(7)") result = ATokenType.ttregisterst;
            else

            if (token == "ES") result = ATokenType.ttregistersreg;
            else
            if (token == "CS") result = ATokenType.ttregistersreg;
            else
            if (token == "SS") result = ATokenType.ttregistersreg;
            else
            if (token == "DS") result = ATokenType.ttregistersreg;
            else
            if (token == "FS") result = ATokenType.ttregistersreg;
            else
            if (token == "GS") result = ATokenType.ttregistersreg;
            else
            if (token == "HS") result = ATokenType.ttregistersreg;
            else
            if (token == "IS") result = ATokenType.ttregistersreg;
            else

            if (token == "CR0") result = ATokenType.ttregistercr;
            else
            if (token == "CR1") result = ATokenType.ttregistercr;
            else
            if (token == "CR2") result = ATokenType.ttregistercr;
            else
            if (token == "CR3") result = ATokenType.ttregistercr;
            else
            if (token == "CR4") result = ATokenType.ttregistercr;
            else
            if (token == "CR5") result = ATokenType.ttregistercr;
            else
            if (token == "CR6") result = ATokenType.ttregistercr;
            else
            if (token == "CR7") result = ATokenType.ttregistercr;
            else


            if (token == "DR0") result = ATokenType.ttregisterdr;
            else
            if (token == "DR1") result = ATokenType.ttregisterdr;
            else
            if (token == "DR2") result = ATokenType.ttregisterdr;
            else
            if (token == "DR3") result = ATokenType.ttregisterdr;
            else
            if (token == "DR4") result = ATokenType.ttregisterdr;
            else
            if (token == "DR5") result = ATokenType.ttregisterdr;
            else
            if (token == "DR6") result = ATokenType.ttregisterdr;
            else
            if (token == "DR7") result = ATokenType.ttregisterdr;
            else
            if (symhandler.process.is64bit)
            {
                if (token == "RAX") result = ATokenType.ttregister64bit;
                else
                if (token == "RCX") result = ATokenType.ttregister64bit;
                else
                if (token == "RDX") result = ATokenType.ttregister64bit;
                else
                if (token == "RBX") result = ATokenType.ttregister64bit;
                else
                if (token == "RSP") result = ATokenType.ttregister64bit;
                else
                if (token == "RBP") result = ATokenType.ttregister64bit;
                else
                if (token == "RSI") result = ATokenType.ttregister64bit;
                else
                if (token == "RDI") result = ATokenType.ttregister64bit;
                else
                if (token == "R8") result = ATokenType.ttregister64bit;
                else
                if (token == "R9") result = ATokenType.ttregister64bit;
                else
                if (token == "R10") result = ATokenType.ttregister64bit;
                else
                if (token == "R11") result = ATokenType.ttregister64bit;
                else
                if (token == "R12") result = ATokenType.ttregister64bit;
                else
                if (token == "R13") result = ATokenType.ttregister64bit;
                else
                if (token == "R14") result = ATokenType.ttregister64bit;
                else
                if (token == "R15") result = ATokenType.ttregister64bit;
                else

                if (token == "SPL") result = ATokenType.ttregister8bitwithprefix;
                else
                if (token == "BPL") result = ATokenType.ttregister8bitwithprefix;
                else
                if (token == "SIL") result = ATokenType.ttregister8bitwithprefix;
                else
                if (token == "DIL") result = ATokenType.ttregister8bitwithprefix;
                else


                if (token == "R8L") result = ATokenType.ttregister8bit;
                else
                if (token == "R9L") result = ATokenType.ttregister8bit;
                else
                if (token == "R10L") result = ATokenType.ttregister8bit;
                else
                if (token == "R11L") result = ATokenType.ttregister8bit;
                else
                if (token == "R12L") result = ATokenType.ttregister8bit;
                else
                if (token == "R13L") result = ATokenType.ttregister8bit;
                else
                if (token == "R14L") result = ATokenType.ttregister8bit;
                else
                if (token == "R15L") result = ATokenType.ttregister8bit;
                else

                if (token == "R8W") result = ATokenType.ttregister16bit;
                else
                if (token == "R9W") result = ATokenType.ttregister16bit;
                else
                if (token == "R10W") result = ATokenType.ttregister16bit;
                else
                if (token == "R11W") result = ATokenType.ttregister16bit;
                else
                if (token == "R12W") result = ATokenType.ttregister16bit;
                else
                if (token == "R13W") result = ATokenType.ttregister16bit;
                else
                if (token == "R14W") result = ATokenType.ttregister16bit;
                else
                if (token == "R15W") result = ATokenType.ttregister16bit;
                else

                if (token == "R8D") result = ATokenType.ttregister32bit;
                else
                if (token == "R9D") result = ATokenType.ttregister32bit;
                else
                if (token == "R10D") result = ATokenType.ttregister32bit;
                else
                if (token == "R11D") result = ATokenType.ttregister32bit;
                else
                if (token == "R12D") result = ATokenType.ttregister32bit;
                else
                if (token == "R13D") result = ATokenType.ttregister32bit;
                else
                if (token == "R14D") result = ATokenType.ttregister32bit;
                else
                if (token == "R15D") result = ATokenType.ttregister32bit;
                else

                if (token == "XMM8") result = ATokenType.ttregisterxmm;
                else
                if (token == "XMM9") result = ATokenType.ttregisterxmm;
                else
                if (token == "XMM10") result = ATokenType.ttregisterxmm;
                else
                if (token == "XMM11") result = ATokenType.ttregisterxmm;
                else
                if (token == "XMM12") result = ATokenType.ttregisterxmm;
                else
                if (token == "XMM13") result = ATokenType.ttregisterxmm;
                else
                if (token == "XMM14") result = ATokenType.ttregisterxmm;
                else
                if (token == "XMM15") result = ATokenType.ttregisterxmm;
                else
                if (token == "CR8") result = ATokenType.ttregistercr;
                else
                if (token == "CR9") result = ATokenType.ttregistercr;
                else
                if (token == "CR10") result = ATokenType.ttregistercr;
                else
                if (token == "CR11") result = ATokenType.ttregistercr;
                else
                if (token == "CR12") result = ATokenType.ttregistercr;
                else
                if (token == "CR13") result = ATokenType.ttregistercr;
                else
                if (token == "CR14") result = ATokenType.ttregistercr;
                else
                if (token == "CR15") result = ATokenType.ttregistercr;
            }
            return result;
        }
        #endregion
        #region isrm8
        public Boolean isrm8(ATokenType parametertype)
        {
            var result = (parametertype == ATokenType.ttmemorylocation8) || (parametertype == ATokenType.ttregister8bit);
            return result;
        }
        #endregion
        #region isrm16
        public Boolean isrm16(ATokenType parametertype)
        {
            var result = (parametertype == ATokenType.ttmemorylocation16) || (parametertype == ATokenType.ttregister16bit);
            return result;
        }
        #endregion
        #region isrm32
        public Boolean isrm32(ATokenType parametertype)
        {
            var result = (parametertype == ATokenType.ttmemorylocation32) || (parametertype == ATokenType.ttregister32bit);
            return result;
        }
        #endregion
        #region ismm_m32
        public Boolean ismm_m32(ATokenType parametertype)
        {
            var result = (parametertype == ATokenType.ttregistermm) || (parametertype == ATokenType.ttmemorylocation32);
            return result;
        }
        #endregion
        #region ismm_m64
        public Boolean ismm_m64(ATokenType parametertype)
        {
            var result = (parametertype == ATokenType.ttregistermm) || (parametertype == ATokenType.ttmemorylocation64);
            return result;
        }
        #endregion
        #region isxmm_m32
        public Boolean isxmm_m32(ATokenType parametertype)
        {
            var result = (parametertype == ATokenType.ttregisterxmm) || (parametertype == ATokenType.ttmemorylocation32);
            return result;
        }
        #endregion
        #region isxmm_m64
        public Boolean isxmm_m64(ATokenType parametertype)
        {
            var result = (parametertype == ATokenType.ttregisterxmm) || (parametertype == ATokenType.ttmemorylocation64);
            return result;
        }
        #endregion
        #region isxmm_m128
        public Boolean isxmm_m128(ATokenType parametertype)
        {
            var result = (parametertype == ATokenType.ttregisterxmm) || (parametertype == ATokenType.ttmemorylocation128);
            return result;
        }
        #endregion
        #region eotoreg
        public int eotoreg(AExtraOpCode eo)
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
        #region setmod
        public void setmod(AByteArray modrm, int index, byte i)
        {
            var tmp = modrm[index];
            setmod(ref tmp, i);
            modrm[index] = tmp;
        }
        public void setmod(ref byte modrm, byte i)
        {
            modrm = (Byte)((modrm & 0x3f) | (i << 6));
        }
        #endregion
        #region getmod
        public byte getmod(byte modrm)
        {
            return (Byte)(modrm >> 6);
        }
        #endregion
        #region setsibscale
        public void setsibscale(AByteArray sib, int index, byte i)
        {
            var tmp = sib[index];
            setsibscale(ref tmp, i);
            sib[index] = tmp;
        }
        public byte setsibscale(ref byte sib, byte i)
        {
            return (Byte)((sib & 0x3f) | (i << 6));
        }
        #endregion
        #region gettokentype
        public ATokenType gettokentype(ref string token, string token2)
        {
            var result = ATokenType.ttinvalidtoken;
            if (token.Length == 0)
                return result;
            result = tokentoregisterbit(token);
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
                switch (tokentoregisterbit(token2))
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
        #region tokenize
        public Boolean tokenize(String opcode, AStringArray tokens)
        {
            int i, j, last;
            Boolean quoted;
            char quotechar;
            string t;
            Boolean ispartial;
            quotechar = '\0';
            tokens.SetLength(0);
            if (opcode.Length > 0)
                opcode = opcode.TrimEnd(' ', ',');
            last = 0;
            quoted = false;
            for (i = 0; i <= opcode.Length; i++)
            {
                //check if this is a quote char
                if (i < opcode.Length && ((opcode[i] == '\'') || (opcode[i] == '"')))
                {
                    if (quoted)  //check if it's the end quote
                    {
                        if (opcode[i] == quotechar)
                            quoted = false;
                    }
                    else
                    {
                        quoted = true;
                        quotechar = opcode[i];
                    }
                }
                //check if we encounter a token seperator. (space or , )
                //but only check when it's not inside a quoted string
                if ((i == opcode.Length) || ((!quoted) && ((opcode[i] == ' ') || (opcode[i] == ','))))
                {
                    tokens.SetLength(tokens.Length + 1);
                    if (i == opcode.Length)
                        j = i - last + 1;
                    else
                        j = i - last;
                    tokens.Last = AStringUtils.Copy(opcode, last, j);
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
                            rewrite(ref lastElem); //Rewrite
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
        #region rewrite
        public Boolean rewrite(ref string token)
        {
            if (token.Length == 0)
                return false; //empty string
            var tokens = new AStringArray();
            var quotechar = '\0';
            tokens.SetLength(0);
            String temp;
            /* 5.4: special pointer notation case */
            if (token.Length > 4 && token.StartsWith("[[") && token.EndsWith("]]"))
            {
                //looks like a pointer in a address specifier (idiot user detected...)
                temp = "[" + AStringUtils.IntToHex(symhandler.getaddressfromname(AStringUtils.Copy(token, 2, token.Length - 4), false, out var haserror), 8) + ']';
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
                        if (token[i] == quotechar)
                            inquote = false;
                    }
                    else
                    {
                        //start of a quote
                        quotechar = token[i];
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
                    if ((err != 0) && (getreg(tokens[i], false) == -1))     //not a hexadecimal value and not a register
                    {
                        temp = AStringUtils.IntToHex(symhandler.getaddressfromname(tokens[i], false, out var haserror), 8);
                        if (!haserror)
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
        #region assmble
        public Boolean assemble(String opcode, UInt64 address, AByteArray bytes, AAssemblerPreference assemblerPreference = AAssemblerPreference.apnone, Boolean skiprangecheck = false)
        {
            var result = singlelineassembler.assemble(opcode, address, bytes, assemblerPreference, skiprangecheck);
            return result;
        }
        #endregion
    }
}