using System;

namespace SputnikAsm.LProcess.LModules
{
    public interface AProcessSharpFunction
    {
        IntPtr BaseAddress { get; }
        string Name { get; }
        T GetDelegate<T>();
    }
}