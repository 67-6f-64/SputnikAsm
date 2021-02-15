using System;
using System.Collections.Generic;
using System.ComponentModel;
using Process.NET.Marshaling;
using Sputnik.LDateTime;
using SputnikAsm.LExtensions;
using SputnikAsm.LProcess.LNative;
using SputnikAsm.LProcess.LNative.LTypes;

namespace SputnikAsm.LProcess.Utilities
{
    /// <summary>
    ///     Static core class providing tools for memory editing.
    /// </summary>
    public static class AMemoryHelper
    {
        /// <summary>
        ///     Reserves a region of memory within the virtual address space of a specified process.
        /// </summary>
        /// <param name="processHandle">The handle to a process.</param>
        /// <param name="size">The size of the region of memory to allocate, in bytes.</param>
        /// <param name="protectionFlags">The memory protection for the region of pages to be allocated.</param>
        /// <param name="allocationFlags">The type of memory allocation.</param>
        /// <returns>The base address of the allocated region.</returns>
        public static IntPtr Allocate(ASafeMemoryHandle processHandle, int size,
            MemoryProtectionFlags protectionFlags = MemoryProtectionFlags.ExecuteReadWrite,
            MemoryAllocationFlags allocationFlags = MemoryAllocationFlags.Commit)
        {
            // Check if the handle is valid
            AHandleManipulator.ValidateAsArgument(processHandle, "processHandle");

            // Allocate a memory page
            var ret = Kernel32.VirtualAllocEx(processHandle, IntPtr.Zero, size, allocationFlags, protectionFlags);

            // Check whether the memory page is valid
            if (ret != IntPtr.Zero)
                return ret;

            // If the pointer isn't valid, throws an exception
            throw new Win32Exception($"Couldn't allocate memory of {size} byte(s).");
        }
        public static IntPtr Allocate(ASafeMemoryHandle processHandle, IntPtr address, int size,
            MemoryProtectionFlags protectionFlags = MemoryProtectionFlags.ExecuteReadWrite,
            MemoryAllocationFlags allocationFlags = MemoryAllocationFlags.Commit)
        {
            // Check if the handle is valid
            AHandleManipulator.ValidateAsArgument(processHandle, "processHandle");

            // Allocate a memory page
            var ret = Kernel32.VirtualAllocEx(processHandle, address, size, allocationFlags, protectionFlags);

            // Check whether the memory page is valid
            if (ret != IntPtr.Zero)
                return ret;

            // If the pointer isn't valid, throws an exception
            throw new Win32Exception($"Couldn't allocate memory of {size} byte(s).");
        }

        /// <summary>
        ///     Closes an open object handle.
        /// </summary>
        /// <param name="handle">A valid handle to an open object.</param>
        public static void CloseHandle(IntPtr handle)
        {
            // Check if the handle is valid
            AHandleManipulator.ValidateAsArgument(handle, "handle");

            // Close the handle
            if (!Kernel32.CloseHandle(handle))
                throw new Win32Exception($"Couldn't close he handle 0x{handle}.");
        }

        /// <summary>
        ///     Releases a region of memory within the virtual address space of a specified process.
        /// </summary>
        /// <param name="processHandle">A handle to a process.</param>
        /// <param name="address">A pointer to the starting address of the region of memory to be freed.</param>
        public static void Free(ASafeMemoryHandle processHandle, IntPtr address)
        {
            // Check if the handles are valid
            AHandleManipulator.ValidateAsArgument(processHandle, "processHandle");
            AHandleManipulator.ValidateAsArgument(address, "address");

            // Free the memory
            if (!Kernel32.VirtualFreeEx(processHandle, address, 0, MemoryReleaseFlags.Release))
                // If the memory wasn't correctly freed, throws an exception
                throw new Win32Exception($"The memory page 0x{address.ToString("X")} cannot be freed.");
        }

        /// <summary>
        ///     etrieves information about the specified process.
        /// </summary>
        /// <param name="processHandle">A handle to the process to query.</param>
        /// <returns>A <see cref="ProcessBasicInformation" /> structure containg process information.</returns>
        public static ProcessBasicInformation NtQueryInformationProcess(ASafeMemoryHandle processHandle)
        {
            // Check if the handle is valid
            AHandleManipulator.ValidateAsArgument(processHandle, "processHandle");

            // Create a structure to store process info
            var info = new ProcessBasicInformation();

            // Get the process info
            var ret = Nt.NtQueryInformationProcess(processHandle, ProcessInformationClass.ProcessBasicInformation,
                ref info, info.Size, IntPtr.Zero);

            // If the function succeeded
            if (ret == 0)
                return info;

            // Else, couldn't get the process info, throws an exception
            throw new ApplicationException($"Couldn't get the information from the process, error code '{ret}'.");
        }

        /// <summary>
        ///     Opens an existing local process object.
        /// </summary>
        /// <param name="accessFlags">The access level to the process object.</param>
        /// <param name="processId">The identifier of the local process to be opened.</param>
        /// <returns>An open handle to the specified process.</returns>
        public static ASafeMemoryHandle OpenProcess(ProcessAccessFlags accessFlags, int processId)
        {
            // Get an handle from the remote process
            var handle = Kernel32.OpenProcess(accessFlags, false, processId);

            // Check whether the handle is valid
            if (!handle.IsInvalid && !handle.IsClosed)
                return handle;

            // Else the handle isn't valid, throws an exception
            throw new Win32Exception($"Couldn't open the process {processId}.");
        }

        /// <summary>
        ///     Reads an array of bytes in the memory form the target process.
        /// </summary>
        /// <param name="processHandle">A handle to the process with memory that is being read.</param>
        /// <param name="address">A pointer to the base address in the specified process from which to read.</param>
        /// <param name="size">The number of bytes to be read from the specified process.</param>
        /// <returns>The collection of read bytes.</returns>
        public static byte[] ReadBytes(ASafeMemoryHandle processHandle, IntPtr address, int size)
        {
            // Check if the handles are valid
            AHandleManipulator.ValidateAsArgument(processHandle, "processHandle");
            AHandleManipulator.ValidateAsArgument(address, "address");

            // Allocate the buffer
            var buffer = new byte[size];
            int nbBytesRead;

            // Read the data from the target process
            if (Kernel32.ReadProcessMemory(processHandle, address, buffer, size, out nbBytesRead) && size == nbBytesRead)
                return buffer;

            // Else the data couldn't be read, throws an exception
            throw new Win32Exception($"Couldn't read {size} byte(s) from 0x{address.ToString("X")}.");
        }
        public static Boolean ChangeProtection(ASafeMemoryHandle processHandle, IntPtr address, int size, MemoryProtectionFlags protection)
        {
            return ChangeProtection(processHandle, address, size, protection, out _);
        }
        public static Boolean ChangeProtection(ASafeMemoryHandle processHandle, IntPtr address, int size, MemoryProtectionFlags protection, out MemoryProtectionFlags oldProtection)
        {
            // Check if the handles are valid
            AHandleManipulator.ValidateAsArgument(processHandle, "processHandle");
            AHandleManipulator.ValidateAsArgument(address, "address");

            // Change the protection in the target process
            if (Kernel32.VirtualProtectEx(processHandle, address, size, protection, out oldProtection))
            {
                // Return the old protection
                return true;
            }

            // Else the protection couldn't be changed
            return false;
        }
        public static Boolean FullAccess(ASafeMemoryHandle processHandle, IntPtr address, int size)
        {
            return ChangeProtection(processHandle, address, size, MemoryProtectionFlags.ExecuteReadWrite, out _);
        }
        public static Boolean FullAccess(ASafeMemoryHandle processHandle, IntPtr address, int size, out MemoryProtectionFlags oldProtection)
        {
            return ChangeProtection(processHandle, address, size, MemoryProtectionFlags.ExecuteReadWrite, out oldProtection);
        }

        /// <summary>
        ///     Retrieves information about a range of pages within the virtual address space of a specified process.
        /// </summary>
        /// <param name="processHandle">A handle to the process whose memory information is queried.</param>
        /// <param name="baseAddress">A pointer to the base address of the region of pages to be queried.</param>
        /// <returns>
        ///     A <see cref="MemoryBasicInformation" /> structures in which information about the specified page range is
        ///     returned.
        /// </returns>
        public static MemoryBasicInformation Query(ASafeMemoryHandle processHandle, IntPtr baseAddress)
        {
            // Allocate the structure to store information of memory
            MemoryBasicInformation memoryInfo;

            // Query the memory region
            if (
                Kernel32.VirtualQueryEx(processHandle, baseAddress, out memoryInfo,
                    MarshalType<MemoryBasicInformation>.Size) != 0)
                return memoryInfo;

            // Else the information couldn't be got
            throw new Win32Exception($"Couldn't query information about the memory region 0x{baseAddress.ToString("X")}");
        }

        /// <summary>
        ///     Retrieves information about a range of pages within the virtual address space of a specified process.
        /// </summary>
        /// <param name="processHandle">A handle to the process whose memory information is queried.</param>
        /// <param name="addressFrom">A pointer to the starting address of the region of pages to be queried.</param>
        /// <param name="addressTo">A pointer to the ending address of the region of pages to be queried.</param>
        /// <returns>A collection of <see cref="MemoryBasicInformation" /> structures.</returns>
        public static IEnumerable<MemoryBasicInformation> Query(ASafeMemoryHandle processHandle, IntPtr addressFrom,
            IntPtr addressTo)
        {
            // Check if the handle is valid
            AHandleManipulator.ValidateAsArgument(processHandle, "processHandle");

            // Convert the addresses to Int64
            var numberFrom = addressFrom.ToInt64();
            var numberTo = addressTo.ToInt64();

            // The first address must be lower than the second
            if (numberFrom >= numberTo)
                throw new ArgumentException("The starting address must be lower than the ending address.", "addressFrom");

            // Create the variable storing the result of the call of VirtualQueryEx
            int ret;

            // Enumerate the memory pages
            do
            {
                // Allocate the structure to store information of memory
                MemoryBasicInformation memoryInfo;

                // Get the next memory page
                ret = Kernel32.VirtualQueryEx(processHandle, new IntPtr(numberFrom), out memoryInfo,
                    MarshalType<MemoryBasicInformation>.Size);

                // Increment the starting address with the size of the page
                numberFrom += memoryInfo.RegionSize;

                // Return the memory page
                if (memoryInfo.State != MemoryStateFlags.Free)
                    yield return memoryInfo;
            } while (numberFrom < numberTo && ret != 0);
        }

        /// <summary>
        ///     Writes data to an area of memory in a specified process.
        /// </summary>
        /// <param name="processHandle">A handle to the process memory to be modified.</param>
        /// <param name="address">A pointer to the base address in the specified process to which data is written.</param>
        /// <param name="byteArray">A buffer that contains data to be written in the address space of the specified process.</param>
        /// <returns>The number of bytes written.</returns>
        public static int WriteBytes(ASafeMemoryHandle processHandle, IntPtr address, byte[] byteArray)
        {
            // Check if the handles are valid
            AHandleManipulator.ValidateAsArgument(processHandle, "processHandle");
            AHandleManipulator.ValidateAsArgument(address, "address");

            // Create the variable storing the number of bytes written
            int nbBytesWritten;

            // Write the data to the target process
            if (Kernel32.WriteProcessMemory(processHandle, address, byteArray, byteArray.Length, out nbBytesWritten))
                // Check whether the length of the data written is equal to the inital array
                if (nbBytesWritten == byteArray.Length)
                    return nbBytesWritten;

            // Else the data couldn't be written, throws an exception
            throw new Win32Exception($"Couldn't write {byteArray.Length} bytes to 0x{address.ToString("X")}");
        }
        #region FindFreeBlockForRegion -- to do
        public static IntPtr FindFreeBlockForRegion(ASafeMemoryHandle processHandle, IntPtr baseAddress, int size)
        {
            return IntPtr.Zero;
            //MemoryBasicInformation32 mbi = new MemoryBasicInformation32();
            //UIntPtr x, b, offset;
            //UIntPtr minAddress, maxAddress;
            //if (!process.is64Bit)
            //    return UIntPtr.Zero; //don't bother
            ////64-bit
            //if (baseAddress == 0)
            //    return UIntPtr.Zero;
            //minAddress = baseAddress - 0x70000000; //let's add in some extra overhead to skip the last fffffff
            //maxAddress = baseAddress + 0x70000000;
            //if ((minAddress > PtrToUInt(systeminfo.lpMaximumApplicationAddress)) || (minAddress < PtrToUInt(systeminfo.lpMinimumApplicationAddress)))
            //    minAddress = PtrToUInt(systeminfo.lpMinimumApplicationAddress);
            //if ((maxAddress < PtrToUInt(systeminfo.lpMinimumApplicationAddress)) || (maxAddress > PtrToUInt(systeminfo.lpMaximumApplicationAddress)))
            //    maxAddress = PtrToUInt(systeminfo.lpMaximumApplicationAddress);
            //b = minAddress;
            //ZeroMemory(&mbi, sizeof(mbi));
            //while (VirtualQueryEx(process.Handle, UIntToPtr(b), mbi, sizeof(mbi)) == sizeof(mbi))
            //{
            //    if (mbi.BaseAddress > UIntToPtr(maxAddress)) return FindFreeBlockForRegion_result; //no memory found, just return 0 and let windows decide
            //
            //    if ((mbi.State == MEM_FREE) && ((mbi.RegionSize) > size))
            //    {
            //        if ((PtrToUInt(mbi.baseaddress) % systeminfo.dwAllocationGranularity) > 0)
            //        {
            //            //the whole size can not be used
            //            x = PtrToUInt(mbi.baseaddress);
            //            offset = systeminfo.dwAllocationGranularity - (x % systeminfo.dwAllocationGranularity);
            //
            //            //check if there's enough left
            //            if ((mbi.regionsize - offset) > size)
            //            {
            //                //yes
            //                x = x + offset;
            //
            //                if (x < base)
            //                {
            //                    x = x + (mbi.regionsize - offset) - size;
            //                    if (x > base) x = base;
            //
            //                    //now decrease x till it's alligned properly
            //                    x = x - (x % systeminfo.dwAllocationGranularity);
            //                }
            //
            //                //if the difference is closer then use that
            //                if (abs(PtrInt(x - base)) < abs(PtrInt(PtrToUInt(result) - base)))
            //                    result = UIntToPtr(x);
            //            }
            //            //nope
            //
            //        }
            //        else
            //        {
            //            x = PtrToUInt(mbi.BaseAddress);
            //            if (x < base)  //try to get it the closest possible (so to the end of the region-size and aligned by dwAllocationGranularity)
            //            {
            //                x = (x + mbi.RegionSize) - size;
            //                if (x > base) x = base;
            //
            //                //now decrease x till it's alligned properly
            //                x = x - (x % systeminfo.dwAllocationGranularity);
            //            }
            //
            //            if (abs(ptrInt(x - base)) < abs(ptrInt(PtrToUInt(result) - base)))
            //                result = UIntToPtr(x);
            //        }
            //
            //    }
            //    b = PtrToUInt(mbi.BaseAddress) + mbi.RegionSize;
            //}
            //return FindFreeBlockForRegion_result;
        }
        #endregion
        #region LastChanceAllocPreferred
        public static IntPtr LastChanceAllocPreferred(ASafeMemoryHandle processHandle, IntPtr preferred, int size, MemoryProtectionFlags protection)
        {
            var startTime = UDateTime.Ticks();
            var address = UIntPtr.Zero;
            var distance = 0UL;
            var count = 0;
            if (preferred.ToInt64() % 65536 > 0)
                preferred = (IntPtr)(preferred.ToInt64() - (preferred.ToInt64() % 65536));
            while (address == UIntPtr.Zero && (count < 10 || (UDateTime.Ticks() < startTime + 10000)) && (distance < 0x80000000UL))
            {
                address = Allocate(processHandle, (IntPtr)(preferred.ToUIntPtr().ToUInt64() + distance), size, protection, MemoryAllocationFlags.Reserve | MemoryAllocationFlags.Commit).ToUIntPtr();
                if (address == UIntPtr.Zero)
                    distance += 65536;
                count += 1;
            }
            return address.ToIntPtr();
        }
        #endregion
        public static Boolean IsAddress(ASafeMemoryHandle processHandle, UIntPtr address)
        {
            var ret = Kernel32.VirtualQueryEx(processHandle, address.ToIntPtr(), out var memoryInfo, MarshalType<MemoryBasicInformation>.Size);
            return ret != 0 && memoryInfo.State == MemoryStateFlags.Commit;
        }
    }
}