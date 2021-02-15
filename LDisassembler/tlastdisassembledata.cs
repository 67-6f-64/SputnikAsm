using System;
using SputnikAsm.LCollections;
using SputnikAsm.LDisassembler.LEnums;

namespace SputnikAsm.LDisassembler
{
    public class tlastdisassembledata
    {
        #region Variables
        public UIntPtr address;
        public String prefix;
        public int prefixsize;
        public String opcode;
        public String parameters;
        public String description;
        public String commentsoverride;
        public AByteArray bytes;
        public int seperatorcount;
        public int[] seperators; //an index in the byte array describing the seperators (prefix/instruction/modrm/sib/extra)
        public tdisassemblervaluetype modrmvaluetype;
        public UIntPtr modrmvalue;
        public tdisassemblervaluetype parametervaluetype;
        public UIntPtr parametervalue;
        //  ValueType: TValueType; //if it's not unknown the value type will say what type of value it is (e.g for the FP types)
        public int datasize;
        public Boolean isfloat; //True if the data it reads/writes is a float (only when sure)
        public Boolean isfloat64;
        public Boolean iscloaked;
        public Boolean hassib;
        public int sibindex;
        public int sibscaler;
        public Boolean isjump; //set for anything that can change eip/rip
        public Boolean iscall; //set if it's a call
        public Boolean isret; //set if it's a ret
        public Boolean isconditionaljump; //set if it's only effective when an conditon is met
        public Boolean willjumpaccordingtocontext; //only valid if a context was provided with the disassembler and isconditionaljump is true
        public int riprelative; //0 or contains the offset where the rip relative part of the code is
        public tdisassemblerclass disassembler;
        //todo: add an isreader/iswriter
        //and what registers/flags it could potentially access/modify
        #endregion
        #region Constructor
        public tlastdisassembledata()
        {
            address = UIntPtr.Zero;
            prefix = "";
            prefixsize = 0;
            opcode = "";
            parameters = "";
            description = "";
            commentsoverride = "";
            bytes = new AByteArray();
            seperatorcount = 5;
            seperators = new int[seperatorcount];
            modrmvaluetype = tdisassemblervaluetype.dvtnone;
            modrmvalue = UIntPtr.Zero;
            parametervaluetype = tdisassemblervaluetype.dvtnone;
            parametervalue = UIntPtr.Zero;
            datasize = 0;
            isfloat = false;
            isfloat64 = false;
            iscloaked = false;
            hassib = false;
            sibindex = 0;
            sibscaler = 0;
            isjump = false;
            iscall = false;
            isret = false;
            isconditionaljump = false;
            willjumpaccordingtocontext = false;
            riprelative = 0;
            disassembler = tdisassemblerclass.dcx86;
        }
        #endregion
    }
}
