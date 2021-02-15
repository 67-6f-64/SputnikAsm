using System;
using SputnikAsm.LProcess.LNative.LTypes;
using SputnikAsm.LProcess.Utilities;

namespace SputnikAsm.LProcess.LMemory
{
    /// <summary>
    ///     Represents a contiguous block of memory in the remote process.
    /// </summary>
    public class AMemoryRegion : AMemoryPointer, IEquatable<AMemoryRegion>
    {
        public AMemoryRegion(AProcessSharp processPlus, IntPtr baseAddress) : base(processPlus, baseAddress)
        {
        }

        /// <summary>
        ///     Contains information about the memory.
        /// </summary>
        public MemoryBasicInformation Information => AMemoryHelper.Query(Process.Handle, BaseAddress);

        /// <summary>
        ///     Gets if the <see cref="AMemoryRegion" /> is valid.
        /// </summary>
        public override bool IsValid => base.IsValid && Information.State != MemoryStateFlags.Free;

        /// <summary>
        ///     Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        public bool Equals(AMemoryRegion other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) ||
                   (BaseAddress.Equals(other.BaseAddress) && Process.Equals(other.Process) &&
                    Information.RegionSize.Equals(other.Information.RegionSize));
        }

        /// <summary>
        ///     Changes the protection of the n next bytes in remote process.
        /// </summary>
        /// <param name="protection">The new protection to apply.</param>
        /// <param name="mustBeDisposed">The resource will be automatically disposed when the finalizer collects the object.</param>
        /// <returns>A new instance of the <see cref="AMemoryProtection" /> class.</returns>
        public AMemoryProtection ChangeProtection(
            MemoryProtectionFlags protection = MemoryProtectionFlags.ExecuteReadWrite, bool mustBeDisposed = true)
        {
            // todo how to handle the regionsize?
            return new AMemoryProtection(Process.Handle, BaseAddress, (int)Information.RegionSize, protection, mustBeDisposed);
        }

        /// <summary>
        ///     Determines whether the specified object is equal to the current object.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((AMemoryRegion) obj);
        }

        /// <summary>
        ///     Serves as a hash function for a particular type.
        /// </summary>
        public override int GetHashCode()
        {
            return BaseAddress.GetHashCode() ^ Process.GetHashCode() ^ Information.RegionSize.GetHashCode();
        }

        public static bool operator ==(AMemoryRegion left, AMemoryRegion right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AMemoryRegion left, AMemoryRegion right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///     Releases the memory used by the region.
        /// </summary>
        public void Release()
        {
            // Release the memory
            AMemoryHelper.Free(Process.Handle, BaseAddress);
            // Remove the pointer
            BaseAddress = IntPtr.Zero;
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return
                $"BaseAddress = 0x{BaseAddress.ToInt64():X} Size = 0x{Information.RegionSize:X} Protection = {Information.Protect}";
        }
    }
}