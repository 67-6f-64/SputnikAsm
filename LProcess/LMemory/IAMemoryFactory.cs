using System;
using System.Collections.Generic;
using SputnikAsm.LProcess.LNative.LTypes;

namespace SputnikAsm.LProcess.LMemory
{
    public interface IAMemoryFactory : IDisposable
    {
        IEnumerable<AMemoryRegion> Regions { get; }
        IEnumerable<IAAllocatedMemory> Allocations { get; }
        IAAllocatedMemory this[string name] { get; }

        IAAllocatedMemory Allocate(string name, int size,
            MemoryProtectionFlags protection = MemoryProtectionFlags.ExecuteReadWrite,
            bool mustBeDisposed = true);

        void Deallocate(IAAllocatedMemory allocation);
    }
}