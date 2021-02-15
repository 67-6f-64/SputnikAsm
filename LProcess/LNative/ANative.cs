using System;
using System.Runtime.InteropServices;
using SputnikAsm.LProcess.LNative.LTypes;
using SputnikAsm.LProcess.Utilities;

namespace SputnikAsm.LProcess.LNative
{
    public static class ANative
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWow64Process([In] IntPtr process, [Out] out bool wow64Process);
        #region IsProcessId64Bit
        public static int IsProcessId64Bit(int pid)
        {
            SystemInfo sysInfo;
            if (Environment.OSVersion.Version.Major > 5 || (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1))
                Kernel32.GetNativeSystemInfo(out sysInfo);
            else
                Kernel32.GetSystemInfo(out sysInfo);
            // if OS is not 64 bit, no process will be either    
            if (sysInfo.ProcessorArchitecture != ProcessorArchitecture.X64)
                return 0;
            var hProcess = Kernel32.OpenProcess(ProcessAccessFlags.QueryInformation, false, pid);
            if (hProcess.IsInvalid)
                return -1;
            if (!IsWow64Process(hProcess.DangerousGetHandle(), out var isWow64))
            {
                hProcess.Dispose();
                return -1;
            }
            hProcess.Dispose();
            return isWow64 ? 0 : 1;
        }
        #endregion
    }
}
