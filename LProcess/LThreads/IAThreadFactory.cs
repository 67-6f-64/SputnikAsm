using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SputnikAsm.LProcess.LThreads
{
    public interface IAThreadFactory : IDisposable
    {
        IARemoteThread this[int threadId] { get; }

        IARemoteThread MainThread { get; }
        IEnumerable<ProcessThread> NativeThreads { get; }
        IEnumerable<IARemoteThread> RemoteThreads { get; }

        IARemoteThread Create(IntPtr address, bool isStarted = true);
        IARemoteThread Create(IntPtr address, dynamic parameter, bool isStarted = true);
        IARemoteThread CreateAndJoin(IntPtr address);
        IARemoteThread CreateAndJoin(IntPtr address, dynamic parameter);
        IARemoteThread GetThreadById(int id);
        void ResumeAll();
        void SuspendAll();
    }
}