using System;
using SputnikAsm.LProcess.LMemory;

namespace SputnikAsm.LProcess.LApplied.LPatches
{
    /// <summary>
    ///     A manager class to handle memory patches.
    ///     <remarks>All credits to Apoc.</remarks>
    /// </summary>
    public class APatchManager : AComplexAppliedManager<APatch>, IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="APatchManager" /> class.
        /// </summary>
        /// <param name="processMemory">The process memory.</param>
        public APatchManager(IAMemory processMemory)
        {
            MemoryBase = processMemory;
        }

        /// <summary>
        ///     The reference of the <see cref="MemoryBase" /> object.
        ///     <remarks>This value is invalid if the manager was created for the <see cref="MemorySharp" /> class.</remarks>
        /// </summary>
        protected IAMemory MemoryBase { get; }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            RemoveAll();
        }

        /// <summary>
        ///     Creates a new <see cref="APatch" /> at the specified address.
        /// </summary>
        /// <param name="address">The address to begin the patch.</param>
        /// <param name="patchWith">The bytes to be written as the patch.</param>
        /// <param name="name">The name of the patch.</param>
        /// <returns>A patch object that exposes the required methods to apply and remove the patch.</returns>
        public APatch Create(IntPtr address, byte[] patchWith, string name)
        {
            if (InternalItems.ContainsKey(name))
                return InternalItems[name];
            InternalItems.Add(name, new APatch(address, patchWith, name, MemoryBase));
            return InternalItems[name];
        }

        /// <summary>
        ///     Creates a new <see cref="APatch" /> at the specified address, and applies it.
        /// </summary>
        /// <param name="address">The address to begin the patch.</param>
        /// <param name="patchWith">The bytes to be written as the patch.</param>
        /// <param name="name">The name of the patch.</param>
        /// <returns>A patch object that exposes the required methods to apply and remove the patch.</returns>
        public APatch CreateAndApply(IntPtr address, byte[] patchWith, string name)
        {
            Create(address, patchWith, name);
            InternalItems[name].Enable();
            return InternalItems[name];
        }
    }
}