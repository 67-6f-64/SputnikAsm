using System;
using System.Diagnostics;
using SputnikAsm.LProcess.LNative.LTypes;

namespace SputnikAsm.LProcess.LThreads
{
    public interface IARemoteThread : IDisposable
    {
        ThreadContext Context { get; set; }
        ASafeMemoryHandle Handle { get; }
        int Id { get; }
        bool IsAlive { get; }
        bool IsMainThread { get; }
        bool IsSuspended { get; }
        bool IsTerminated { get; }
        ProcessThread Native { get; }

        T GetExitCode<T>();
        int GetHashCode();
        IntPtr GetRealSegmentAddress(SegmentRegisters segment);
        void Join();
        WaitValues Join(TimeSpan time);
        void Refresh();
        void Resume();
        IAFrozenThread Suspend();
        void Terminate(int exitCode = 0);
    }
}