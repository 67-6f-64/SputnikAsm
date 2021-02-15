using System;
using SputnikAsm.LCollections;
using SputnikAsm.LDisassembler.LEnums;

namespace SputnikAsm.LDisassembler
{
    public class ALastDisassembleData
    {
        #region Variables
        public UIntPtr Address;
        public String Prefix;
        public int PrefixSize;
        public String OpCode;
        public String Parameters;
        public String Description;
        public String CommentsOverride;
        public AByteArray Bytes;
        public int SeparatorCount;
        public int[] Separators; //an index in the byte array describing the seperators (prefix/instruction/modrm/sib/extra)
        public ADisassemblerValueType ModRmValueType;
        public UIntPtr ModRmValue;
        public ADisassemblerValueType ParameterValueType;
        public UIntPtr ParameterValue;
        //  ValueType: TValueType; //if it's not unknown the value type will say what type of value it is (e.g for the FP types)
        public int DataSize;
        public Boolean IsFloat; //True if the data it reads/writes is a float (only when sure)
        public Boolean IsFloat64;
        public Boolean IsCloaked;
        public Boolean HasSib;
        public int SibIndex;
        public int SibScaler;
        public Boolean IsJump; //set for anything that can change eip/rip
        public Boolean IsCall; //set if it's a call
        public Boolean IsRet; //set if it's a ret
        public Boolean IsConditionalJump; //set if it's only effective when an conditon is met
        public Boolean WillJumpAccordingToContext; //only valid if a context was provided with the disassembler and isconditionaljump is true
        public int RipRelative; //0 or contains the offset where the rip relative part of the code is
        public ADisassemblerClass Disassembler;
        //todo: add an isreader/iswriter
        //and what registers/flags it could potentially access/modify
        #endregion
        #region Constructor
        public ALastDisassembleData()
        {
            Address = UIntPtr.Zero;
            Prefix = "";
            PrefixSize = 0;
            OpCode = "";
            Parameters = "";
            Description = "";
            CommentsOverride = "";
            Bytes = new AByteArray();
            SeparatorCount = 5;
            Separators = new int[SeparatorCount];
            ModRmValueType = ADisassemblerValueType.None;
            ModRmValue = UIntPtr.Zero;
            ParameterValueType = ADisassemblerValueType.None;
            ParameterValue = UIntPtr.Zero;
            DataSize = 0;
            IsFloat = false;
            IsFloat64 = false;
            IsCloaked = false;
            HasSib = false;
            SibIndex = 0;
            SibScaler = 0;
            IsJump = false;
            IsCall = false;
            IsRet = false;
            IsConditionalJump = false;
            WillJumpAccordingToContext = false;
            RipRelative = 0;
            Disassembler = ADisassemblerClass.X86;
        }
        #endregion
    }
}
