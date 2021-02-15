using System;
using System.Collections.Generic;
using System.Linq;
using SputnikAsm.LProcess.LNative.LTypes;
using SputnikAsm.LProcess.Utilities;

namespace SputnikAsm.LProcess.LMemory
{
    /// <summary>
    ///     Class providing tools for manipulating memory space.
    /// </summary>
    public class AMemoryFactory : IAMemoryFactory
    {
        /// <summary>
        ///     The list containing all allocated memory.
        /// </summary>
        protected readonly List<IAAllocatedMemory> InternalRemoteAllocations;

        /// <summary>
        ///     The reference of the <see cref="Process" /> object.
        /// </summary>
        protected readonly AProcessSharp Process;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AMemoryFactory" /> class.
        /// </summary>
        /// <param name="process">The reference of the <see cref="Process" /> object.</param>
        public AMemoryFactory(AProcessSharp process)
        {
            // Save the parameter
            Process = process;
            // Create a list containing all allocated memory
            InternalRemoteAllocations = new List<IAAllocatedMemory>();
        }

        /// <summary>
        ///     A collection containing all allocated memory in the remote process.
        /// </summary>
        public IEnumerable<IAAllocatedMemory> Allocations => InternalRemoteAllocations.AsReadOnly();

        /// <summary>
        ///     Gets the <see cref="IAAllocatedMemory" /> with the specified name.
        /// </summary>
        /// <value>
        ///     The <see cref="IAAllocatedMemory" />.
        /// </value>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public IAAllocatedMemory this[string name]
        {
            get { return InternalRemoteAllocations.FirstOrDefault(am => am.Identifier == name); }
        }

        /// <summary>
        ///     Gets all blocks of memory allocated in the remote process.
        /// </summary>
        public IEnumerable<AMemoryRegion> Regions
        {
            get
            {
                var size = IntPtr.Size;
                var adresseTo = size == 8 ? new IntPtr(0x7fffffffffffffff) : new IntPtr(0x7fffffff);
                return
                    AMemoryHelper.Query(Process.Handle, IntPtr.Zero, adresseTo)
                        .Select(page => new AMemoryRegion(Process, page.BaseAddress));
            }
        }

        /// <summary>
        ///     Allocates a region of memory within the virtual address space of the remote process.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="size">The size of the memory to allocate.</param>
        /// <param name="protection">The protection of the memory to allocate.</param>
        /// <param name="mustBeDisposed">The allocated memory will be released when the finalizer collects the object.</param>
        public IAAllocatedMemory Allocate(string name, int size,
            MemoryProtectionFlags protection = MemoryProtectionFlags.ExecuteReadWrite, bool mustBeDisposed = true)
        {
            // Allocate a memory space
            var memory = new AAllocatedMemory(Process, name, size, protection, mustBeDisposed);
            // Add the memory in the list
            InternalRemoteAllocations.Add(memory);
            return memory;
        }

        /// <summary>
        ///     Deallocates a region of memory previously allocated within the virtual address space of the remote process.
        /// </summary>
        /// <param name="allocation">The allocated memory to release.</param>
        public void Deallocate(IAAllocatedMemory allocation)
        {
            // Dispose the element
            if (!allocation.IsDisposed)
                allocation.Dispose();
            // Remove the element from the allocated memory list
            if (InternalRemoteAllocations.Contains(allocation))
                InternalRemoteAllocations.Remove(allocation);
        }

        /// <summary>
        ///     Releases all resources used by the <see cref="AMemoryFactory" /> object.
        /// </summary>
        public virtual void Dispose()
        {
            // Release all allocated memories which must be disposed
            foreach (var allocatedMemory in InternalRemoteAllocations.Where(m => m.MustBeDisposed).ToArray())
                allocatedMemory.Dispose();
            // Avoid the finalizer
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Frees resources and perform other cleanup operations before it is reclaimed by garbage collection.
        /// </summary>
        ~AMemoryFactory()
        {
            Dispose();
        }
    }
}