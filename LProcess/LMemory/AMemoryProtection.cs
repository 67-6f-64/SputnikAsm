using System;
using SputnikAsm.LProcess.LNative.LTypes;
using SputnikAsm.LProcess.Utilities;

namespace SputnikAsm.LProcess.LMemory
{
    /// <summary>
    ///     Class providing tools for manipulating memory protection.
    /// </summary>
    public class AMemoryProtection : IDisposable
    {
        protected readonly ASafeMemoryHandle Handle;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AMemoryProtection" /> class.
        /// </summary>
        /// <param name="handle">The reference of the <see cref="ASafeMemoryHandle" /> object.</param>
        /// <param name="baseAddress">The base address of the memory to change the protection.</param>
        /// <param name="size">The size of the memory to change.</param>
        /// <param name="protection">The new protection to apply.</param>
        /// <param name="mustBeDisposed">The resource will be automatically disposed when the finalizer collects the object.</param>
        public AMemoryProtection(ASafeMemoryHandle handle, IntPtr baseAddress, int size,
            MemoryProtectionFlags protection = MemoryProtectionFlags.ExecuteReadWrite,
            bool mustBeDisposed = true)
        {
            // Save the parameters
            Handle = handle;
            BaseAddress = baseAddress;
            NewProtection = protection;
            Size = size;
            MustBeDisposed = mustBeDisposed;
            // Change the memory protection
            AMemoryHelper.ChangeProtection(Handle, baseAddress, size, protection, out var oldProtection);
            OldProtection = oldProtection;
        }

        /// <summary>
        ///     The base address of the altered memory.
        /// </summary>
        public IntPtr BaseAddress { get; }

        /// <summary>
        ///     States if the <see cref="AMemoryProtection" /> object nust be disposed when it is collected.
        /// </summary>
        public bool MustBeDisposed { get; set; }

        /// <summary>
        ///     Defines the new protection applied to the memory.
        /// </summary>
        public MemoryProtectionFlags NewProtection { get; }

        /// <summary>
        ///     References the inital protection of the memory.
        /// </summary>
        public MemoryProtectionFlags OldProtection { get; }

        /// <summary>
        ///     The size of the altered memory.
        /// </summary>
        public int Size { get; }

        /// <summary>
        ///     Restores the initial protection of the memory.
        /// </summary>
        public virtual void Dispose()
        {
            // Restore the memory protection
            AMemoryHelper.ChangeProtection(Handle, BaseAddress, Size, OldProtection);
            // Avoid the finalizer 
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Frees resources and perform other cleanup operations before it is reclaimed by garbage collection.
        /// </summary>
        ~AMemoryProtection()
        {
            if (MustBeDisposed)
                Dispose();
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return
                $"BaseAddress = 0x{BaseAddress.ToInt64():X} NewProtection = {NewProtection} OldProtection = {OldProtection}";
        }
    }
}