using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SputnikAsm.LProcess.LNative.LTypes;
using SputnikAsm.LProcess.Utilities;

namespace SputnikAsm.LProcess.LExtensions
{
    public static class AProcessExtensions
    {
        public static IList<ProcessThread> GetThreads(this System.Diagnostics.Process process)
        {
            return process.Threads.Cast<ProcessThread>().ToList();
        }

        public static IList<ProcessModule> GetModules(this System.Diagnostics.Process process)
        {
            return process.Modules.Cast<ProcessModule>().ToList();
        }

        public static ASafeMemoryHandle Open(this System.Diagnostics.Process process, ProcessAccessFlags processAccessFlags = ProcessAccessFlags.AllAccess)
        {
            return AMemoryHelper.OpenProcess(processAccessFlags, process.Id);
        }

        public static string GetVersionInfo(this System.Diagnostics.Process process)
        {
            return
                $"{process.MainModule.FileVersionInfo.FileDescription} {process.MainModule.FileVersionInfo.FileMajorPart}.{process.MainModule.FileVersionInfo.FileMinorPart}.{process.MainModule.FileVersionInfo.FileBuildPart} {process.MainModule.FileVersionInfo.FilePrivatePart}";
        }

    }
}
