using System;
using SputnikAsm.LProcess.LMemory;

namespace SputnikAsm.LProcess.LMarshaling
{
    /// <summary>
    ///     Interface representing a value within the memory of a remote process.
    /// </summary>
    public interface IAMarshalledValue : IDisposable
    {
        /// <summary>
        ///     The memory allocated where the value is fully written if needed. It can be unused.
        /// </summary>
        IAAllocatedMemory Allocated { get; }

        /// <summary>
        ///     The reference of the value. It can be directly the value or a pointer.
        /// </summary>
        IntPtr Reference { get; }
    }
}