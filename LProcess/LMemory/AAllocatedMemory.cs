using System;
using SputnikAsm.LProcess.LNative.LTypes;
using SputnikAsm.LProcess.Utilities;

namespace SputnikAsm.LProcess.LMemory
{
    /// <summary>
    ///     Class representing an allocated memory in a remote process.
    /// </summary>
    public class AAllocatedMemory : AMemoryRegion, IAAllocatedMemory
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AAllocatedMemory" /> class.
        /// </summary>
        /// <param name="processPlus">The reference of the <see cref="AProcessSharp" /> object.</param>
        /// <param name="name"></param>
        /// <param name="size">The size of the allocated memory.</param>
        /// <param name="protection">The protection of the allocated memory.</param>
        /// <param name="mustBeDisposed">The allocated memory will be released when the finalizer collects the object.</param>
        public AAllocatedMemory(AProcessSharp processPlus, string name, int size,
            MemoryProtectionFlags protection = MemoryProtectionFlags.ExecuteReadWrite,
            bool mustBeDisposed = true)
            : base(processPlus, AMemoryHelper.Allocate(processPlus.Handle, size, protection))
        {
            // Set local vars
            Identifier = name;
            MustBeDisposed = mustBeDisposed;
            IsDisposed = false;
            Size = size;
        }

        /// <summary>
        ///     Gets a value indicating whether the element is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether the element must be disposed when the Garbage Collector collects the object.
        /// </summary>
        public bool MustBeDisposed { get; set; }

        /// <summary>
        ///     Releases all resources used by the <see cref="AAllocatedMemory" /> object.
        /// </summary>
        /// <remarks>Don't use the IDisposable pattern because the class is sealed.</remarks>
        public virtual void Dispose()
        {
            if (!IsDisposed)
            {
                // Set the flag to true
                IsDisposed = true;
                // Release the allocated memory
                Release();
                // Remove this object from the collection of allocated memory
                Process.MemoryFactory.Deallocate(this);
                // Remove the pointer
                BaseAddress = IntPtr.Zero;
                // Avoid the finalizer 
                GC.SuppressFinalize(this);
            }
        }

        public bool IsAllocated => IsDisposed;
        public int Size { get; }
        public string Identifier { get; }

        /// <summary>
        ///     Frees resources and perform other cleanup operations before it is reclaimed by garbage collection.
        /// </summary>
        ~AAllocatedMemory()
        {
            if (MustBeDisposed)
                Dispose();
        }
    }
}