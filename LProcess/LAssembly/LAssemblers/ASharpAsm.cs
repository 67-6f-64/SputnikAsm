using System;
using Sputnik.LUtils;
using SputnikAsm.LAssembler;
using SputnikAsm.LAutoAssembler;
using SputnikAsm.LAutoAssembler.LCollections;
using SputnikAsm.LCollections;

namespace SputnikAsm.LProcess.LAssembly.LAssemblers
{
    public class ASharpAsm : IAAssembler
    {
        public AScriptBytesArray Assemble(string asm)
        {
            // Assemble and return the code
            return Assemble(asm, IntPtr.Zero);
        }
        public AScriptBytesArray Assemble(string asm, IntPtr baseAddress)
        {
            // Assemble and return the code
            var code = new ARefStringArray();
            code.Assign(UStringUtils.GetLines(asm).ToArray());
            code.Insert(0, $"{baseAddress.ToString("X")}:");
            AAsmTools.AutoAssembler.RemoveComments(code);
            var ret = AAsmTools.AutoAssembler.Assemble(AAsmTools.SymbolHandler.Process, code);
            return ret;
        }
    }
}
