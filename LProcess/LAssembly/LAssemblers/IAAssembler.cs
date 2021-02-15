using System;
using SputnikAsm.LAutoAssembler.LCollections;

namespace SputnikAsm.LProcess.LAssembly.LAssemblers
{
    /// <summary>
    ///     Interface defining an assembler.
    /// </summary>
    public interface IAAssembler
    {
        /// <summary>
        ///     Assemble the specified assembly code.
        /// </summary>
        /// <param name="asm">The assembly code.</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        AScriptBytesArray Assemble(AProcessSharp process, String asm);

        /// <summary>
        ///     Assemble the specified assembly code at a base address.
        /// </summary>
        /// <param name="asm">The assembly code.</param>
        /// <param name="baseAddress">The address where the code is rebased.</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        AScriptBytesArray Assemble(AProcessSharp process, String asm, IntPtr baseAddress);
    }
}