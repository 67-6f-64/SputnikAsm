using System;
using Sputnik.LUtils;
using SputnikAsm.LAutoAssembler;
using SputnikAsm.LAutoAssembler.LCollections;
using SputnikAsm.LCollections;

namespace SputnikAsm.LProcess.LAssembly.LAssemblers
{
    public class ASharpAsm : IAAssembler
    {
        private AAutoAssembler _autoAssembler;
        public AScriptBytesArray Assemble(AProcessSharp process, string asm)
        {
            // Assemble and return the code
            return Assemble(process, asm, IntPtr.Zero);
        }
        public AScriptBytesArray Assemble(AProcessSharp process, string asm, IntPtr baseAddress)
        {
            // Assemble and return the code
            if (_autoAssembler == null)
                _autoAssembler = new AAutoAssembler();
            _autoAssembler.Assembler.SymHandler.Process = process;
            var code = new ARefStringArray();
            code.Assign(UStringUtils.GetLines(asm).ToArray());
            code.Insert(0, $"{baseAddress.ToString("X")}:");
            _autoAssembler.RemoveComments(code);
            var ret = _autoAssembler.Assemble(process, code);
            return ret;
        }
    }
}
