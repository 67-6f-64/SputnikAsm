using System;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace SputnikAsm.LProcess.LNative.LTypes
{
    /// <summary>
    ///     Represents a Win32 handle safely managed.
    /// </summary>
    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    public sealed class ASafeMemoryHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        ///     Parameterless constructor for handles built by the system (like <see cref="NativeMethods.OpenProcess" />).
        /// </summary>
        public ASafeMemoryHandle() : base(true)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ASafeMemoryHandle" /> class, specifying the handle to keep in safe.
        /// </summary>
        /// <param name="handle">The handle to keep in safe.</param>
        public ASafeMemoryHandle(IntPtr handle) : base(true)
        {
            SetHandle(handle);
        }

        /// <summary>
        ///     Executes the code required to free the handle.
        /// </summary>
        /// <returns>
        ///     True if the handle is released successfully; otherwise, in the event of a catastrophic failure, false. In this
        ///     case, it generates a releaseHandleFailed MDA Managed Debugging Assistant.
        /// </returns>
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        protected override bool ReleaseHandle()
        {
            // Check whether the handle is set AND whether the handle has been successfully closed
            return handle != IntPtr.Zero && Kernel32.CloseHandle(handle);
        }
    }
}