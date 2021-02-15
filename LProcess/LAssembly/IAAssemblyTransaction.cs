using System;
using SputnikAsm.LAutoAssembler.LCollections;

namespace SputnikAsm.LProcess.LAssembly
{
    public interface IAAssemblyTransaction
    {
        IntPtr Address { get; }
        bool IsAutoExecuted { get; set; }

        void AddLine(string asm, params object[] args);
        AScriptBytesArray Assemble();
        void Clear();
        void Dispose();
        T GetExitCode<T>();
        void InsertLine(int index, string asm, params object[] args);
    }
}