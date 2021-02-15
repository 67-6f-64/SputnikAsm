using System;

namespace SputnikAsm.LProcess.LThreads
{
    public interface IAFrozenThread : IDisposable
    {
        IARemoteThread Thread { get; }
    }
}