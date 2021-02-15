using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Sputnik.LBinary;
using Sputnik.LDateTime;
using Sputnik.LEngine;
using Sputnik.LEnums;
using Sputnik.LMarshal;
using Sputnik.LUtils;
using SputnikAsm.LExtensions;
using SputnikWin;
using SputnikWin.LExtra.LMemorySharp.Native;
using SputnikWin.LFileSystem;
using SputnikWin.LUtils;

namespace SputnikAsm.LSymbolHandler
{
    #region SpkMemProtFlags
    /// <summary>
    /// Memory-protection options list.
    /// </summary>
    [Flags]
    public enum SpkMemProtFlags
    {
        /// <summary>
        /// Disables all access to the committed region of pages. An attempt to read from, write to, or execute the committed region results in an access violation.
        /// This value is not officially present in the Microsoft's enumeration but can occur according to the MEMORY_BASIC_INFORMATION structure documentation.
        /// </summary>
        ZeroAccess = 0x0,
        /// <summary>
        /// Enables execute access to the committed region of pages. An attempt to read from or write to the committed region results in an access violation.
        /// This flag is not supported by the CreateFileMapping function.
        /// </summary>
        Execute = 0x10,
        /// <summary>
        /// Enables execute or read-only access to the committed region of pages. An attempt to write to the committed region results in an access violation.
        /// </summary>
        ExecuteRead = 0x20,
        /// <summary>
        /// Enables execute, read-only, or read/write access to the committed region of pages.
        /// </summary>
        ExecuteReadWrite = 0x40,
        /// <summary>
        /// Enables execute, read-only, or copy-on-write access to a mapped view of a file mapping object. 
        /// An attempt to write to a committed copy-on-write page results in a private copy of the page being made for the process. 
        /// The private page is marked as PAGE_EXECUTE_READWRITE, and the change is written to the new page.
        /// This flag is not supported by the VirtualAlloc or <see cref="System.Runtime.InteropServices.NativeMethods.VirtualAllocEx"/> functions. 
        /// </summary>
        ExecuteWriteCopy = 0x80,
        /// <summary>
        /// Disables all access to the committed region of pages. An attempt to read from, write to, or execute the committed region results in an access violation.
        /// This flag is not supported by the CreateFileMapping function.
        /// </summary>
        NoAccess = 0x01,
        /// <summary>
        /// Enables read-only access to the committed region of pages. An attempt to write to the committed region results in an access violation. 
        /// If Data Execution Prevention is enabled, an attempt to execute code in the committed region results in an access violation.
        /// </summary>
        ReadOnly = 0x02,
        /// <summary>
        /// Enables read-only or read/write access to the committed region of pages. 
        /// If Data Execution Prevention is enabled, attempting to execute code in the committed region results in an access violation.
        /// </summary>
        ReadWrite = 0x04,
        /// <summary>
        /// Enables read-only or copy-on-write access to a mapped view of a file mapping object. 
        /// An attempt to write to a committed copy-on-write page results in a private copy of the page being made for the process. 
        /// The private page is marked as PAGE_READWRITE, and the change is written to the new page. 
        /// If Data Execution Prevention is enabled, attempting to execute code in the committed region results in an access violation.
        /// This flag is not supported by the VirtualAlloc or <see cref="System.Runtime.InteropServices.NativeMethods.VirtualAllocEx"/> functions.
        /// </summary>
        WriteCopy = 0x08,
        /// <summary>
        /// Pages in the region become guard pages. 
        /// Any attempt to access a guard page causes the system to raise a STATUS_GUARD_PAGE_VIOLATION exception and turn off the guard page status. 
        /// Guard pages thus act as a one-time access alarm. For more information, see Creating Guard Pages.
        /// When an access attempt leads the system to turn off guard page status, the underlying page protection takes over.
        /// If a guard page exception occurs during a system service, the service typically returns a failure status indicator.
        /// This value cannot be used with PAGE_NOACCESS.
        /// This flag is not supported by the CreateFileMapping function.
        /// </summary>
        Guard = 0x100,
        /// <summary>
        /// Sets all pages to be non-cachable. Applications should not use this attribute except when explicitly required for a device. 
        /// Using the interlocked functions with memory that is mapped with SEC_NOCACHE can result in an EXCEPTION_ILLEGAL_INSTRUCTION exception.
        /// The PAGE_NOCACHE flag cannot be used with the PAGE_GUARD, PAGE_NOACCESS, or PAGE_WRITECOMBINE flags.
        /// The PAGE_NOCACHE flag can be used only when allocating private memory with the VirtualAlloc, <see cref="System.Runtime.InteropServices.NativeMethods.VirtualAllocEx"/>, or VirtualAllocExNuma functions. 
        /// To enable non-cached memory access for shared memory, specify the SEC_NOCACHE flag when calling the CreateFileMapping function.
        /// </summary>
        NoCache = 0x200,
        /// <summary>
        /// Sets all pages to be write-combined.
        /// Applications should not use this attribute except when explicitly required for a device. 
        /// Using the interlocked functions with memory that is mapped as write-combined can result in an EXCEPTION_ILLEGAL_INSTRUCTION exception.
        /// The PAGE_WRITECOMBINE flag cannot be specified with the PAGE_NOACCESS, PAGE_GUARD, and PAGE_NOCACHE flags.
        /// The PAGE_WRITECOMBINE flag can be used only when allocating private memory with the VirtualAlloc, <see cref="System.Runtime.InteropServices.NativeMethods.VirtualAllocEx"/>, or VirtualAllocExNuma functions. 
        /// To enable write-combined memory access for shared memory, specify the SEC_WRITECOMBINE flag when calling the CreateFileMapping function.
        /// </summary>
        WriteCombine = 0x400
    }
    #endregion
    #region WriteType
    public enum WriteType : byte
    {
        None,
        Binary,
        Char,
        CharW,
        Bool,
        Byte,
        SByte,
        Int16,
        Int32,
        Int64,
        UInt16,
        UInt32,
        UInt64,
        IntPtr,
        UIntPtr,
        Float,
        Double,
        StringAscii,
        StringUtf7,
        StringUtf8,
        StringUtf16,
        StringUtf16Big,
        StringUtf32
    }
    #endregion
    #region ReadType
    public enum ReadType : byte
    {
        None,
        Binary,
        Char,
        CharW,
        Bool,
        Byte,
        SByte,
        Int16,
        Int32,
        Int64,
        UInt16,
        UInt32,
        UInt64,
        Float,
        Double,
        IntPtr,
        UIntPtr,
        StringAscii,
        StringUtf7,
        StringUtf8,
        StringUtf16,
        StringUtf16Big,
        StringUtf32,
    }
    #endregion
    #region TitleMatchMode
    public enum TitleMatchMode : byte
    {
        Left,
        Right,
        Partial,
        Full,
        Advanced,
    }
    #endregion
    #region PokeCode
    public class PokeCode
    {
        public IntPtr Address;
        public Byte[] OpCodes;
        public PokeCode(IntPtr address, Byte[] opCodes)
        {
            Address = address;
            OpCodes = opCodes;
        }
    }
    #endregion
    #region OpCodes
    public class OpCodes
    {
        public Byte[] Data;
        public OpCodes(String opCodes)
        {
            if (String.IsNullOrEmpty(opCodes))
                opCodes = "";
            opCodes = opCodes.Replace(" ", "").Replace("\t", "");
            Data = Enumerable.Range(0, opCodes.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(opCodes.Substring(x, 2), 16))
                .ToArray();
        }
    }
    #endregion
    #region WindowCore
    public static class WindowCore
    {
        #region EnumTopLevelWindows
        public static IEnumerable<IntPtr> EnumTopLevelWindows()
        {
            return EnumChildWindows(IntPtr.Zero);
        }
        #endregion
        #region EnumChildWindows
        public static IEnumerable<IntPtr> EnumChildWindows(IntPtr parentHandle)
        {
            var list = new List<IntPtr>();
            EnumWindowsProc callback = delegate (IntPtr windowHandle, IntPtr lParam)
            {
                list.Add(windowHandle);
                return true;
            };
            NativeMethods.EnumChildWindows(parentHandle, callback, IntPtr.Zero);
            return list.ToArray();
        }
        #endregion
        #region EnumAllWindows
        public static IEnumerable<IntPtr> EnumAllWindows()
        {
            var list = new List<IntPtr>();
            foreach (var topWindow in EnumTopLevelWindows())
            {
                list.Add(topWindow);
                list.AddRange(EnumChildWindows(topWindow));

            }
            return list;
        }
        #endregion
        #region GetWindowProcessId
        public static int GetWindowProcessId(IntPtr windowHandle)
        {
            if (!Validator.Validate(windowHandle, "windowHandle"))
                return -1;
            NativeMethods.GetWindowThreadProcessId(windowHandle, out var processId);
            return processId;
        }
        #endregion
        #region GetWindowThreadId
        /// <summary>
        /// Retrieves the identifier of the thread that created the specified window.
        /// </summary>
        /// <param name="windowHandle">A handle to the window.</param>
        /// <returns>The return value is the identifier of the thread that created the window.</returns>
        public static int GetWindowThreadId(IntPtr windowHandle)
        {
            // Check if the handle is valid
            if (!Validator.Validate(windowHandle, "windowHandle"))
                return -1;
            // Get the thread id
            return NativeMethods.GetWindowThreadProcessId(windowHandle, out var trash);
        }
        #endregion
        #region GetClassName
        public static String GetClassName(IntPtr windowHandle)
        {
            // Check if the handle is valid
            if (!Validator.Validate(windowHandle, "windowHandle"))
                return "";
            // Get the window class name
            var stringBuilder = new StringBuilder(Char.MaxValue);
            if (NativeMethods.GetClassName(windowHandle, stringBuilder, stringBuilder.Capacity) == 0)
                return "";
            return stringBuilder.ToString();
        }
        #endregion
        #region GetWindowText
        public static String GetWindowText(IntPtr windowHandle)
        {
            if (!Validator.Validate(windowHandle, "windowHandle"))
                return "";
            var capacity = NativeMethods.GetWindowTextLength(windowHandle);
            if (capacity == 0)
                return "";
            var stringBuilder = new StringBuilder(capacity + 1);
            return NativeMethods.GetWindowText(windowHandle, stringBuilder, stringBuilder.Capacity) == 0
                ? ""
                : stringBuilder.ToString();
        }
        #endregion
        #region GetForegroundWindow
        public static IntPtr GetForegroundWindow()
        {
            return NativeMethods.GetForegroundWindow();
        }
        #endregion
        #region GetSystemMetrics
        public static int GetSystemMetrics(SystemMetrics metric)
        {
            return NativeMethods.GetSystemMetrics(metric);
        }
        #endregion
        #region GetWindowPlacement
        public static WindowPlacement GetWindowPlacement(IntPtr windowHandle)
        {
            if (!Validator.Validate(windowHandle, "windowHandle"))
                return new WindowPlacement();
            WindowPlacement placement;
            placement.Length = Marshal.SizeOf(typeof(WindowPlacement));
            return !NativeMethods.GetWindowPlacement(windowHandle, out placement) ? new WindowPlacement() : placement;
        }
        #endregion
        #region FlashWindow
        public static Boolean FlashWindow(IntPtr windowHandle)
        {
            return Validator.Validate(windowHandle, "windowHandle") && NativeMethods.FlashWindow(windowHandle, true);
        }
        #endregion
        #region FlashWindowEx
        public static Boolean FlashWindowEx(IntPtr windowHandle, FlashWindowFlags flags, uint count, TimeSpan timeout)
        {
            // Check if the handle is valid
            if (!Validator.Validate(windowHandle, "windowHandle"))
                return false;
            var flashInfo = new FlashInfo
            {
                Size = Marshal.SizeOf(typeof(FlashInfo)),
                Hwnd = windowHandle,
                Flags = flags,
                Count = count,
                Timeout = Convert.ToInt32(timeout.TotalMilliseconds)
            };
            return NativeMethods.FlashWindowEx(ref flashInfo);
        }
        public static Boolean FlashWindowEx(IntPtr windowHandle, FlashWindowFlags flags, uint count)
        {
            return FlashWindowEx(windowHandle, flags, count, TimeSpan.FromMilliseconds(0));
        }
        public static Boolean FlashWindowEx(IntPtr windowHandle, FlashWindowFlags flags)
        {
            return FlashWindowEx(windowHandle, flags, 0);
        }
        #endregion
        #region MapVirtualKey
        public static uint MapVirtualKey(uint key, TranslationTypes translation)
        {
            return NativeMethods.MapVirtualKey(key, translation);
        }
        public static uint MapVirtualKey(Keys key, TranslationTypes translation)
        {
            return MapVirtualKey((uint)key, translation);
        }
        #endregion
        #region PostMessage
        public static Boolean PostMessage(IntPtr windowHandle, uint message, UIntPtr wParam, UIntPtr lParam)
        {
            return Validator.Validate(windowHandle, "windowHandle") && NativeMethods.PostMessage(windowHandle, message, wParam, lParam);
        }
        public static Boolean PostMessage(IntPtr windowHandle, WindowsMessages message, UIntPtr wParam, UIntPtr lParam)
        {
            return PostMessage(windowHandle, (uint)message, wParam, lParam);
        }
        #endregion
        #region SendMessage
        public static IntPtr SendMessage(IntPtr windowHandle, uint message, UIntPtr wParam, IntPtr lParam)
        {
            return !Validator.Validate(windowHandle, "windowHandle")
                ? IntPtr.Zero
                : NativeMethods.SendMessage(windowHandle, message, wParam, lParam);
        }
        public static IntPtr SendMessage(IntPtr windowHandle, WindowsMessages message, UIntPtr wParam, IntPtr lParam)
        {
            return SendMessage(windowHandle, (uint)message, wParam, lParam);
        }
        #endregion
        #region SetForegroundWindow
        public static Boolean SetForegroundWindow(IntPtr windowHandle)
        {
            if (!Validator.Validate(windowHandle, "windowHandle"))
                return false;
            if (GetForegroundWindow() == windowHandle)
                return true;
            ShowWindow(windowHandle, WindowStates.Restore);
            return NativeMethods.SetForegroundWindow(windowHandle);
        }
        #endregion
        #region SetWindowPlacement
        public static Boolean SetWindowPlacement(IntPtr windowHandle, int left, int top, int height, int width)
        {
            if (!Validator.Validate(windowHandle, "windowHandle"))
                return true;
            var placement = GetWindowPlacement(windowHandle);
            placement.NormalPosition.Left = left;
            placement.NormalPosition.Top = top;
            placement.NormalPosition.Height = height;
            placement.NormalPosition.Width = width;
            return SetWindowPlacement(windowHandle, placement);
        }
        public static Boolean SetWindowPlacement(IntPtr windowHandle, WindowPlacement placement)
        {
            if (!Validator.Validate(windowHandle, "windowHandle"))
                return false;
            if (Debugger.IsAttached && placement.ShowCmd == WindowStates.ShowNormal)
                placement.ShowCmd = WindowStates.Restore;
            return NativeMethods.SetWindowPlacement(windowHandle, ref placement);
        }
        #endregion
        #region SetWindowText
        public static Boolean SetWindowText(IntPtr windowHandle, String title)
        {
            return Validator.Validate(windowHandle, "windowHandle") && NativeMethods.SetWindowText(windowHandle, title);
        }
        #endregion
        #region ShowWindow
        public static Boolean ShowWindow(IntPtr windowHandle, WindowStates state)
        {
            return Validator.Validate(windowHandle, "windowHandle") && NativeMethods.ShowWindow(windowHandle, state);
        }
        #endregion
    }
    #endregion
    #region ApplicationFinder
    public static class ApplicationFinder
    {
        #region Property TopLevelWindows
        public static IEnumerable<IntPtr> TopLevelWindows => WindowCore.EnumTopLevelWindows();

        #endregion
        #region Property Windows
        public static IEnumerable<IntPtr> Windows => WindowCore.EnumAllWindows();

        #endregion
        #region FromProcessId
        public static Process FromProcessId(int processId)
        {
            return Process.GetProcessById(processId);
        }
        #endregion
        #region FromProcessName
        public static IEnumerable<Process> FromProcessName(String processName)
        {
            return Process.GetProcessesByName(processName);
        }
        #endregion
        #region FromWindowClassName
        public static IEnumerable<Process> FromWindowClassName(String className)
        {
            return Windows.Where(window => WindowCore.GetClassName(window) == className).Select(FromWindowHandle);
        }
        #endregion
        #region FromWindowHandle
        public static Process FromWindowHandle(IntPtr windowHandle)
        {
            return FromProcessId(WindowCore.GetWindowProcessId(windowHandle));
        }
        #endregion
        #region FromWindowTitle
        public static IEnumerable<Process> FromWindowTitle(String windowTitle)
        {
            return Windows.Where(window => WindowCore.GetWindowText(window) == windowTitle).Select(FromWindowHandle);
        }
        #endregion
        #region FromWindowTitleContains
        public static IEnumerable<Process> FromWindowTitleContains(String windowTitle)
        {
            return Windows.Where(window => WindowCore.GetWindowText(window).Contains(windowTitle)).Select(FromWindowHandle);
        }
        #endregion
    }
    #endregion
    #region MemoryCore
    public static class MemoryCore
    {
        #region Allocate
        public static IntPtr Allocate(
            SafeMemoryHandle processHandle,
            int size,
            MemoryProtectionFlags protectionFlags = MemoryProtectionFlags.ExecuteReadWrite,
            MemoryAllocationFlags allocationFlags = MemoryAllocationFlags.Commit
        )
        {
            if (!Validator.Validate(processHandle, "processHandle"))
                return IntPtr.Zero;
            var ret = NativeMethods.VirtualAllocEx(processHandle, IntPtr.Zero, size, allocationFlags, protectionFlags);
            return ret != IntPtr.Zero ? ret : IntPtr.Zero;
        }
        public static IntPtr Allocate(
            SafeMemoryHandle processHandle,
            IntPtr address,
            int size,
            MemoryProtectionFlags protectionFlags = MemoryProtectionFlags.ExecuteReadWrite,
            MemoryAllocationFlags allocationFlags = MemoryAllocationFlags.Commit
        )
        {
            if (!Validator.Validate(processHandle, "processHandle"))
                return IntPtr.Zero;
            var ret = NativeMethods.VirtualAllocEx(processHandle, address, size, allocationFlags, protectionFlags);
            return ret != IntPtr.Zero ? ret : IntPtr.Zero;
        }
        #endregion
        #region CloseHandle
        public static Boolean CloseHandle(IntPtr handle)
        {
            return Validator.Validate(handle, "handle") && NativeMethods.CloseHandle(handle);
        }
        #endregion
        #region Free
        public static Boolean Free(SafeMemoryHandle processHandle, IntPtr address)
        {
            if (!Validator.Validate(processHandle, "processHandle"))
                return false;
            if (!Validator.Validate(address, "address"))
                return false;
            NativeMethods.VirtualFreeEx(processHandle, address, 0, MemoryReleaseFlags.Release);
            return true;
        }
        #endregion
        #region OpenProcess
        public static SafeMemoryHandle OpenProcess(ProcessAccessFlags accessFlags, IntPtr processId)
        {
            var handle = NativeMethods.OpenProcess(accessFlags, false, processId);
            if (!handle.IsInvalid && !handle.IsClosed)
                return handle;
            return null;
        }
        #endregion
        #region ChangeProtection
        public static MemoryProtectionFlags ChangeProtection(SafeMemoryHandle processHandle, IntPtr address, int size, MemoryProtectionFlags protection)
        {
            if (!Validator.Validate(processHandle, "processHandle"))
                return MemoryProtectionFlags.ZeroAccess;
            if (!Validator.Validate(address, "address"))
                return MemoryProtectionFlags.ZeroAccess;
            if (NativeMethods.VirtualProtectEx(processHandle, address, size, protection, out var oldProtection))
                return oldProtection;
            return MemoryProtectionFlags.ZeroAccess;
        }
        #endregion
        #region ReadBytes
        public static Byte[] ReadBytes(SafeMemoryHandle processHandle, IntPtr address, int size)
        {
            if (!Validator.Validate(processHandle, "processHandle"))
                return UBinaryUtils.EmptyArray;
            if (!Validator.Validate(address, "address"))
                return UBinaryUtils.EmptyArray;
            var buffer = new Byte[size];
            if (NativeMethods.ReadProcessMemory(processHandle, address, buffer, size, out var nbBytesRead) && size == nbBytesRead)
                return buffer;
            return UBinaryUtils.EmptyArray;
        }
        #endregion
        #region WriteBytes
        public static int WriteBytes(SafeMemoryHandle processHandle, IntPtr address, Byte[] byteArray)
        {
            if (!Validator.Validate(processHandle, "processHandle"))
                return 0;
            if (!Validator.Validate(address, "address"))
                return 0;
            if (!NativeMethods.WriteProcessMemory(processHandle, address, byteArray, byteArray.Length, out var nbBytesWritten))
                return 0;
            return nbBytesWritten == byteArray.Length ? nbBytesWritten : 0;
        }
        #endregion
    }
    #endregion
    #region Validator
    public static class Validator
    {
        #region Validate
        public static Boolean Validate(IntPtr handle, string argumentName)
        {
            return handle != IntPtr.Zero;
        }
        public static Boolean Validate(SafeMemoryHandle handle, string argumentName)
        {
            return handle != null && !handle.IsClosed && !handle.IsInvalid;
        }
        #endregion
    }
    #endregion
    public class AProcess
    {
        #region Variables
        public TitleMatchMode WinCaptionMatchMode;
        public TitleMatchMode WinClassMatchMode;
        public Boolean WinCaptionMatchIgnoreCase;
        public Boolean WinClassMatchIgnoreCase;
        private readonly String _gameCap;
        private readonly String _gameCls;
        private readonly String _gameProcess;
        private readonly Boolean _usingProcess;
        private UProcess _process;
        #endregion
        #region Properties
        private Boolean _isDisposed;
        public Boolean IsX86 => UNativeWin.IsProcessId64Bit(GetId()) == 0;
        public Boolean IsX64 => UNativeWin.IsProcessId64Bit(GetId()) == 1;
        public int PointerSize => IsX64 ? 8 : 4;
        public Boolean IsDisposed
        {
            get => !IsValid();
            set => _isDisposed = value;
        }
        #endregion
        #region Constructors
        public AProcess()
        {
            WinCaptionMatchMode = TitleMatchMode.Left;
            WinClassMatchMode = TitleMatchMode.Left;
            WinCaptionMatchIgnoreCase = true;
            WinClassMatchIgnoreCase = true;
            _process = null;
            _isDisposed = false;
            _usingProcess = false;
        }
        public AProcess(String gameProcess)
            : this()
        {
            _usingProcess = true;
            _gameProcess = gameProcess;
            FindTarget();
        }
        public AProcess(String gameCap, String gameCls)
            : this()
        {
            _usingProcess = false;
            _gameCap = gameCap;
            _gameCls = gameCls;
            FindTarget();
        }
        public AProcess(Process process)
            : this()
        {
            _process = new UProcess(process);
        }
        public AProcess(IntPtr processId)
            : this()
        {
            _process = new UProcess(processId);
        }
        public AProcess(UProcess process)
            : this()
        {
            _process = process;
        }
        #endregion
        #region Destructor
        ~AProcess()
        {
            Dispose();
        }
        #endregion
        #region Dispose
        public void Dispose()
        {
            _isDisposed = true;
            _process?.Dispose();
            _process = null;
        }
        #endregion
        #region IsValid
        public Boolean IsValid()
        {
            return IsCoreValid() && _process.IsValid();
        }
        private Boolean IsCoreValid()
        {
            try
            {
                return !_isDisposed && _process != null && !_process.HasExited;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #region GetMainModule
        public UProcessModule GetMainModule()
        {
            if (!IsValid())
                return null;
            var mainModule = _process.GetMainModule();
            return mainModule;
        }
        #endregion
        #region GetModule
        public UProcessModule GetModule(String moduleName)
        {
            if (!IsValid())
                return null;
            var mainModule = _process.GetMainModule();
            if (String.Equals(mainModule.ModuleName, moduleName, StringComparison.CurrentCultureIgnoreCase))
                return mainModule;
            var ret = _process.GetModules().FirstOrDefault(m => String.Equals(m.ModuleName, moduleName, StringComparison.CurrentCultureIgnoreCase));
            return ret;
        }
        #endregion
        #region FindTarget, FindWindow, IsWindowMatch
        #region FindTarget
        public void FindTarget()
        {
            Process found = null;
            if (_usingProcess)
            {
                var pid = UProcessUtils.ProcessId(_gameProcess);
                if (pid != IntPtr.Zero)
                    found = Process.GetProcessById(pid.ToInt32());
                else
                {
                    var f = Process.GetProcessesByName(_gameProcess);
                    if (f.Length > 0)
                        found = f[0];
                }
            }
            else
            {
                var p = FindWindow(_gameCap, _gameCls);
                if (p == IntPtr.Zero)
                    return;
                found = ApplicationFinder.FromWindowHandle(p);
            }
            if (found == null)
                return;
            _process?.Dispose();
            _process = new UProcess(found);
        }
        #endregion
        #region FindWindow
        private IntPtr FindWindow(String gameCap, String gameCls)
        {
            foreach (var c in ApplicationFinder.Windows)
            {
                if (c == IntPtr.Zero)
                    continue;
                var wCap = WindowCore.GetWindowText(c);
                var wCls = WindowCore.GetClassName(c);
                var wMatch = IsWindowMatch(wCap, gameCap, WinCaptionMatchMode, WinCaptionMatchIgnoreCase);
                var cMatch = IsWindowMatch(wCls, gameCls, WinClassMatchMode, WinClassMatchIgnoreCase);
                if (wMatch && cMatch)
                    return c;
            }
            return IntPtr.Zero;
        }
        #endregion
        #region IsWindowMatch
        private static Boolean IsWindowMatch(String text, String find, TitleMatchMode mode, Boolean ignoreCase)
        {
            var tNull = String.IsNullOrEmpty(text);
            var fNull = String.IsNullOrEmpty(find);
            if (tNull && fNull)
                return true;
            if (tNull)
                text = "";
            if (fNull)
                find = "";
            switch (mode)
            {
                case TitleMatchMode.Left:
                    return ignoreCase ? text.ToLower().StartsWith(find.ToLower()) : text.StartsWith(find);
                case TitleMatchMode.Right:
                    return ignoreCase ? text.ToLower().EndsWith(find.ToLower()) : text.EndsWith(find);
                case TitleMatchMode.Partial:
                    return ignoreCase ? text.ToLower().Contains(find.ToLower()) : text.Contains(find);
                case TitleMatchMode.Full:
                    return ignoreCase ? text.ToLower() == find.ToLower() : text == find;
            }
            return false;
        }
        #endregion
        #endregion
        #region GetHandle
        public IntPtr GetHandle()
        {
            return _process.GetHandle();
        }
        #endregion
        #region GetNativeHandle
        public IntPtr GetNativeHandle()
        {
            return _process.GetNativeHandle();
        }
        #endregion
        #region GetId
        public IntPtr GetId()
        {
            return _process.GetId();
        }
        #endregion
        #region GetAddress
        public IntPtr GetAddress(String addressString)
        {
            if (!IsValid())
                return IntPtr.Zero;
            var retAddress = _process.GetMainModule().BaseAddress;
            var len = addressString.Length;
            var sb = new StringBuilder();
            var firstSymFound = false;
            var readToNextQuot = false;
            var lastAction = '\0';
            for (var i = 0; i < len + 1; i++)
            {
                var c = i < len ? addressString[i] : '\0';
                var append = true;
                switch (c)
                {
                    case '\'':
                    case '"':
                        readToNextQuot = !readToNextQuot;
                        break;
                    case '+':
                    case '-':
                    case '/':
                    case '*':
                        if (readToNextQuot)
                            break;
                        append = false;
                        if (!firstSymFound)
                        {
                            var processName = sb.ToString();
                            sb = new StringBuilder();
                            if (!String.IsNullOrEmpty(processName))
                            {
                                retAddress = GetBaseAddress(processName);
                                if (retAddress == IntPtr.Zero)
                                    return IntPtr.Zero;
                            }
                            firstSymFound = true;
                        }
                        if (lastAction != '\0')
                            goto nextAction;
                        lastAction = c;
                        break;
                    case '\0':
                        var setLastAction = false;
                        goto nextActionNoSet;
                    nextAction:
                        setLastAction = true;
                    nextActionNoSet:
                        if (lastAction != '\0')
                        {
                            var actionText = sb.ToString();
                            if (!String.IsNullOrEmpty(actionText))
                            {
                                if (!UStringUtils.StartsWith0X(actionText))
                                    actionText = "0x" + actionText;
                                sb = new StringBuilder();
                                if (UStringUtils.IsXDigit(actionText.Substring(2)))
                                {
                                    switch (lastAction)
                                    {
                                        case '+':
                                            retAddress = (IntPtr)(retAddress.ToInt64() + UStringUtils.StringToInt64(actionText));
                                            break;
                                        case '-':
                                            retAddress = (IntPtr)(retAddress.ToInt64() - UStringUtils.StringToInt64(actionText));
                                            break;
                                        case '/':
                                            retAddress = (IntPtr)(retAddress.ToInt64() / UStringUtils.StringToInt64(actionText));
                                            break;
                                        case '*':
                                            retAddress = (IntPtr)(retAddress.ToInt64() * UStringUtils.StringToInt64(actionText));
                                            break;
                                    }
                                }
                            }
                            lastAction = setLastAction ? c : '\0';
                        }
                        break;
                }
                if (append)
                    sb.Append(c);
            }
            return retAddress;
        }
        #endregion
        #region GetBaseAddress
        public IntPtr GetBaseAddress(String moduleName)
        {
            if (!IsValid())
                return IntPtr.Zero;
            var mainModule = _process.GetMainModule();
            if (mainModule.ModuleName == moduleName)
                return mainModule.BaseAddress;
            foreach (var m in _process.GetModules().Where(m => m.ModuleName == moduleName))
                return m.BaseAddress;
            return IntPtr.Zero;
        }
        #endregion
        #region FullAccess
        public Boolean FullAccess(IntPtr pAddress, int size)
        {
            return ChangeAccess(pAddress, size, SpkMemProtFlags.ExecuteReadWrite);
        }
        #endregion
        #region ChangeAccess
        public Boolean ChangeAccess(IntPtr pAddress, int size, SpkMemProtFlags newProtection)
        {
            if (!IsValid())
                return false;
            if (pAddress == IntPtr.Zero)
                return false;
            var protection = (MemoryProtectionFlags)newProtection;
            return NativeMethods.VirtualProtectEx(_process.GetSafeHandle(), pAddress, size, protection, out _);
        }
        #endregion
        #region ReadMem
        #region ReadMem (MemorySharp)
        public Object ReadMem(IntPtr address, ReadType type, int size = -1)
        {
            if (!IsValid())
                return InternalReturnError(type);
            if (address == IntPtr.Zero)
                return InternalReturnError(type);
            var ret = InternalReadMem(address, type, size);
            return ret;
        }
        #endregion
        #region InternalReturnError
        private static Object InternalReturnError(ReadType type)
        {
            switch (type)
            {
                case ReadType.Binary:
                    return new Byte[] { };
                case ReadType.Bool:
                    return false;
                case ReadType.Byte:
                    return (Byte)0;
                case ReadType.SByte:
                    return (SByte)0;
                case ReadType.Int16:
                    return (Int16)0;
                case ReadType.Int32:
                    return (Int32)0;
                case ReadType.Int64:
                    return (Int64)0;
                case ReadType.UInt16:
                    return (UInt16)0;
                case ReadType.UInt32:
                    return (UInt32)0;
                case ReadType.UInt64:
                    return (UInt64)0;
                case ReadType.Float:
                    return (Single)0;
                case ReadType.Double:
                    return (Double)0;
                case ReadType.IntPtr:
                    return IntPtr.Zero;
                case ReadType.UIntPtr:
                    return UIntPtr.Zero;
                case ReadType.StringAscii:
                case ReadType.StringUtf8:
                    return "";
                default:
                    return null;
            }
        }
        #endregion
        #region InternalReadMem
        private Object InternalReadMem(IntPtr address, ReadType type, int size = -1)
        {
            Byte[] bytes;
            switch (type)
            {
                case ReadType.Binary:
                    return InternalReadByteArray(address, size);
                case ReadType.Char:
                    return (Char)InternalReadByteArray(address, 1)[0];
                case ReadType.CharW:
                    bytes = InternalReadByteArray(address, UMarshalHelper.UnicodeCharSize);
                    return UBitConverter.ToChar(bytes);
                case ReadType.Bool:
                    return InternalReadByteArray(address, 1)[0] > 0;
                case ReadType.Byte:
                    return InternalReadByteArray(address, 1)[0];
                case ReadType.SByte:
                    return (SByte)InternalReadByteArray(address, 1)[0];
                case ReadType.Int16:
                    bytes = InternalReadByteArray(address, UMarshalHelper.Int16Size);
                    return UBitConverter.ToInt16(bytes);
                case ReadType.Int32:
                    bytes = InternalReadByteArray(address, UMarshalHelper.Int32Size);
                    return UBitConverter.ToInt32(bytes);
                case ReadType.Int64:
                    bytes = InternalReadByteArray(address, UMarshalHelper.Int64Size);
                    return UBitConverter.ToInt64(bytes);
                case ReadType.UInt16:
                    bytes = InternalReadByteArray(address, UMarshalHelper.UInt16Size);
                    return UBitConverter.ToUInt16(bytes);
                case ReadType.UInt32:
                    bytes = InternalReadByteArray(address, UMarshalHelper.UInt32Size);
                    return UBitConverter.ToUInt32(bytes);
                case ReadType.UInt64:
                    bytes = InternalReadByteArray(address, UMarshalHelper.UInt64Size);
                    return UBitConverter.ToUInt64(bytes);
                case ReadType.Float:
                    bytes = InternalReadByteArray(address, UMarshalHelper.FloatSize);
                    return UBitConverter.ToSingle(bytes);
                case ReadType.Double:
                    bytes = InternalReadByteArray(address, UMarshalHelper.DoubleSize);
                    return UBitConverter.ToDouble(bytes);
                case ReadType.IntPtr:
                    bytes = InternalReadByteArray(address, PointerSize);
                    return UBitConverter.ToIntPtr(bytes);
                case ReadType.UIntPtr:
                    bytes = InternalReadByteArray(address, PointerSize);
                    return UBitConverter.ToUIntPtr(bytes);
                case ReadType.StringAscii:
                    bytes = InternalReadByteArray(address, size);
                    return Encoding.ASCII.GetString(bytes);
                case ReadType.StringUtf7:
                    bytes = InternalReadByteArray(address, size);
                    return Encoding.UTF7.GetString(bytes);
                case ReadType.StringUtf8:
                    bytes = InternalReadByteArray(address, size);
                    return Encoding.UTF8.GetString(bytes);
                case ReadType.StringUtf16:
                    bytes = InternalReadByteArray(address, size);
                    return Encoding.Unicode.GetString(bytes);
                case ReadType.StringUtf16Big:
                    bytes = InternalReadByteArray(address, size);
                    return Encoding.BigEndianUnicode.GetString(bytes);
                case ReadType.StringUtf32:
                    bytes = InternalReadByteArray(address, size);
                    return Encoding.UTF32.GetString(bytes);
                default:
                    return InternalReturnError(type);
            }
        }
        #endregion
        #region InternalReadByteArray
        private Byte[] InternalReadByteArray(IntPtr pAddress, int iSize)
        {
            return MemoryCore.ReadBytes(_process.GetSafeHandle(), pAddress, iSize);
        }
        #endregion
        #endregion
        #region WriteMem
        #region WriteMem (Core)
        public Int64 WriteMem(IntPtr address, USv value)
        {
            switch (value.Type)
            {
                case USvType.Char:
                    return WriteMem(address, value.ToChar());
                case USvType.Bool:
                    return WriteMem(address, value.ToBool());
                case USvType.Int64:
                    return WriteMem(address, value.ToInt64());
                case USvType.UInt64:
                    return WriteMem(address, value.ToUInt64());
                case USvType.IntPtr:
                    return WriteMem(address, value.ToIntPtr());
                case USvType.UIntPtr:
                    return WriteMem(address, value.ToUIntPtr());
                case USvType.Float:
                    return WriteMem(address, value.ToFloat());
                case USvType.Double:
                    return WriteMem(address, value.ToDouble());
                case USvType.String:
                    var result = UStringUtils.StringToNumber(value.ToString(), out var i, out var l, out var d);
                    if ((result & UStringUtils.NumberInfo.IsNumber) != 0)
                    {
                        if ((result & UStringUtils.NumberInfo.Double) != 0)
                            return WriteMem(address, d);
                        if ((result & UStringUtils.NumberInfo.LongInteger) != 0)
                            return WriteMem(address, l);
                        if ((result & UStringUtils.NumberInfo.Integer) != 0)
                            return WriteMem(address, i);
                    }
                    break;
            }
            return 0;
        }
        public Int64 WriteMem(IntPtr address, WriteType type, USv value)
        {
            switch (type)
            {
                case WriteType.Char:
                    return WriteMem(address, (Byte)value.ToChar());
                case WriteType.CharW:
                    return WriteMem(address, value.ToChar());
                case WriteType.Bool:
                    return WriteMem(address, value.ToBool());
                case WriteType.Int16:
                    return WriteMem(address, value.ToInt16());
                case WriteType.Int32:
                    return WriteMem(address, value.ToInt32());
                case WriteType.Int64:
                    return WriteMem(address, value.ToInt64());
                case WriteType.UInt16:
                    return WriteMem(address, value.ToUInt16());
                case WriteType.UInt32:
                    return WriteMem(address, value.ToUInt32());
                case WriteType.UInt64:
                    return WriteMem(address, value.ToUInt64());
                case WriteType.IntPtr:
                    return WriteMem(address, value.ToIntPtr());
                case WriteType.UIntPtr:
                    return WriteMem(address, value.ToUIntPtr());
                case WriteType.Float:
                    return WriteMem(address, value.ToFloat());
                case WriteType.Double:
                    return WriteMem(address, value.ToDouble());
                case WriteType.StringAscii:
                    return WriteMem(address, Encoding.ASCII.GetBytes(value.ToString()));
                case WriteType.StringUtf8:
                    return WriteMem(address, Encoding.UTF8.GetBytes(value.ToString()));
                case WriteType.StringUtf7:
                    return WriteMem(address, Encoding.UTF7.GetBytes(value.ToString()));
                case WriteType.StringUtf16:
                    return WriteMem(address, Encoding.Unicode.GetBytes(value.ToString()));
                case WriteType.StringUtf32:
                    return WriteMem(address, Encoding.UTF32.GetBytes(value.ToString()));
                case WriteType.StringUtf16Big:
                    return WriteMem(address, Encoding.BigEndianUnicode.GetBytes(value.ToString()));
            }
            return 0;
        }
        public Int64 WriteMem(IntPtr address, Byte[] value)
        {
            return WriteMem(address, (Object)value);
        }
        public Int64 WriteMem(IntPtr address, Boolean value)
        {
            return WriteMem(address, (Object)value);
        }
        public Int64 WriteMem(IntPtr address, Byte value)
        {
            return WriteMem(address, (Object)value);
        }
        public Int64 WriteMem(IntPtr address, SByte value)
        {
            return WriteMem(address, (Object)value);
        }
        public Int64 WriteMem(IntPtr address, Int16 value)
        {
            return WriteMem(address, (Object)value);
        }
        public Int64 WriteMem(IntPtr address, Int32 value)
        {
            return WriteMem(address, (Object)value);
        }
        public Int64 WriteMem(IntPtr address, Int64 value)
        {
            return WriteMem(address, (Object)value);
        }
        public Int64 WriteMem(IntPtr address, UInt16 value)
        {
            return WriteMem(address, (Object)value);
        }
        public Int64 WriteMem(IntPtr address, UInt32 value)
        {
            return WriteMem(address, (Object)value);
        }
        public Int64 WriteMem(IntPtr address, UInt64 value)
        {
            return WriteMem(address, (Object)value);
        }
        public Int64 WriteMem(IntPtr address, Single value)
        {
            return WriteMem(address, (Object)value);
        }
        public Int64 WriteMem(IntPtr address, Double value)
        {
            return WriteMem(address, (Object)value);
        }
        public Int64 WriteMem(IntPtr address, String value)
        {
            return WriteMem(address, (Object)value);
        }
        public Int64 WriteMem(IntPtr address, Object value)
        {
            if (address == IntPtr.Zero)
                return -1;
            var ret = InternalWriteMem(address, value);
            return ret;
        }
        #endregion
        #region InternalWriteMem
        private Int64 InternalWriteMem(IntPtr address, params Object[] values)
        {
            if (address == IntPtr.Zero)
                return -1;
            var iAddress = address.ToInt64();
            return values.Sum(cc => InternalPokeValue(ref iAddress, cc));
        }
        #endregion
        #region InternalPokeValue
        private Int64 InternalPokeValue(ref Int64 iAddress, Object value)
        {
            while (true)
            {
                if (!IsValid())
                    return -1;
                Int64 bytesWritten = 0;
                if (value is OpCodes)
                {
                    var opCodes = value as OpCodes;
                    bytesWritten += InternalWriteByteArray(new IntPtr(iAddress), opCodes.Data);
                    iAddress += opCodes.Data.Length;
                    return bytesWritten;
                }
                if (value is Byte[])
                {
                    var pBytes = (Byte[])value;
                    bytesWritten += InternalWriteByteArray(new IntPtr(iAddress), pBytes);
                    iAddress += pBytes.Length;
                    return bytesWritten;
                }
                if (value is USv)
                {
                    var sv = value as USv;
                    if (sv.IsRef())
                        sv = sv.GetRealSv();
                    if (sv == null)
                        return bytesWritten;
                    switch (sv.Type)
                    {
                        case USvType.Binary:
                            value = sv.ToBinary().Data;
                            continue;
                        case USvType.Bool:
                            value = sv.ToBool();
                            continue;
                        case USvType.Int64:
                            value = sv.ToInt64();
                            continue;
                        case USvType.UInt64:
                            value = sv.ToUInt64();
                            continue;
                        case USvType.IntPtr:
                            value = sv.ToIntPtr();
                            continue;
                        case USvType.UIntPtr:
                            value = sv.ToUIntPtr();
                            continue;
                        case USvType.Float:
                            value = sv.ToFloat();
                            continue;
                        case USvType.Double:
                            value = sv.ToDouble();
                            continue;
                        case USvType.String:
                            value = sv.ToString();
                            continue;
                        case USvType.Array:
                            Int64 count = 0;
                            foreach (var c in sv.GetArray().GetHash())
                                count += InternalPokeValue(ref iAddress, c);
                            return count;
                        case USvType.Object:
                            {
                                var obj = sv.ToObject();
                                if (obj != null)
                                {
                                    value = obj;
                                    continue;
                                }
                                return -1;
                            }
                        default:
                            return -1;
                    }
                }
                switch (Type.GetTypeCode(value.GetType()))
                {
                    case TypeCode.Boolean:
                        {
                            var pBytes = UBitConverter.GetBytes((Byte)((Boolean)value ? 1 : 0));
                            bytesWritten += InternalWriteByteArray(new IntPtr(iAddress), pBytes);
                            iAddress += pBytes.Length;
                            break;
                        }
                    case TypeCode.Byte:
                        {
                            var pBytes = UBitConverter.GetBytes((Byte)value);
                            bytesWritten += InternalWriteByteArray(new IntPtr(iAddress), pBytes);
                            iAddress += pBytes.Length;
                            break;
                        }
                    case TypeCode.SByte:
                        {
                            var pBytes = UBitConverter.GetBytes((SByte)value);
                            bytesWritten += InternalWriteByteArray(new IntPtr(iAddress), pBytes);
                            iAddress += pBytes.Length;
                            break;
                        }
                    case TypeCode.Int16:
                        {
                            var pBytes = UBitConverter.GetBytes((Int16)value);
                            bytesWritten += InternalWriteByteArray(new IntPtr(iAddress), pBytes);
                            iAddress += pBytes.Length;
                            break;
                        }
                    case TypeCode.Int32:
                        {
                            var pBytes = UBitConverter.GetBytes((Int32)value);
                            bytesWritten += InternalWriteByteArray(new IntPtr(iAddress), pBytes);
                            iAddress += pBytes.Length;
                            break;
                        }
                    case TypeCode.Int64:
                        {
                            var pBytes = UBitConverter.GetBytes((Int64)value);
                            bytesWritten += InternalWriteByteArray(new IntPtr(iAddress), pBytes);
                            iAddress += pBytes.Length;
                            break;
                        }
                    case TypeCode.UInt16:
                        {
                            var pBytes = UBitConverter.GetBytes((UInt16)value);
                            bytesWritten += InternalWriteByteArray(new IntPtr(iAddress), pBytes);
                            iAddress += pBytes.Length;
                            break;
                        }
                    case TypeCode.UInt32:
                        {
                            var pBytes = UBitConverter.GetBytes((UInt32)value);
                            bytesWritten += InternalWriteByteArray(new IntPtr(iAddress), pBytes);
                            iAddress += pBytes.Length;
                            break;
                        }
                    case TypeCode.UInt64:
                        {
                            var pBytes = UBitConverter.GetBytes((UInt64)value);
                            bytesWritten += InternalWriteByteArray(new IntPtr(iAddress), pBytes);
                            iAddress += pBytes.Length;
                            break;
                        }
                    case TypeCode.Single:
                        {
                            var pBytes = UBitConverter.GetBytes((Single)value);
                            bytesWritten += InternalWriteByteArray(new IntPtr(iAddress), pBytes);
                            iAddress += pBytes.Length;
                            break;
                        }
                    case TypeCode.Double:
                        {
                            var pBytes = UBitConverter.GetBytes((Double)value);
                            bytesWritten += InternalWriteByteArray(new IntPtr(iAddress), pBytes);
                            iAddress += pBytes.Length;
                            break;
                        }
                    case TypeCode.String:
                        {
                            var pBytes = Encoding.UTF8.GetBytes((String)value);
                            bytesWritten += InternalWriteByteArray(new IntPtr(iAddress), pBytes);
                            iAddress += pBytes.Length;
                            break;
                        }
                    default:
                    {
                        if (value is IntPtr ptr)
                        {
                            switch (PointerSize)
                            {
                                case 4:
                                {
                                    var pBytes = UBitConverter.GetBytes(ptr.ToInt32());
                                    bytesWritten += InternalWriteByteArray(new IntPtr(iAddress), pBytes);
                                    iAddress += pBytes.Length;
                                    break;
                                }
                                case 8:
                                {
                                    var pBytes = UBitConverter.GetBytes(ptr.ToInt64());
                                    bytesWritten += InternalWriteByteArray(new IntPtr(iAddress), pBytes);
                                    iAddress += pBytes.Length;
                                    break;
                                }
                            }
                        }
                        if (value is UIntPtr uPtr)
                        {
                            switch (PointerSize)
                            {
                                case 4:
                                {
                                    var pBytes = UBitConverter.GetBytes(uPtr.ToUInt32());
                                    bytesWritten += InternalWriteByteArray(new IntPtr(iAddress), pBytes);
                                    iAddress += pBytes.Length;
                                    break;
                                }
                                case 8:
                                {
                                    var pBytes = UBitConverter.GetBytes(uPtr.ToUInt64());
                                    bytesWritten += InternalWriteByteArray(new IntPtr(iAddress), pBytes);
                                    iAddress += pBytes.Length;
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
                return bytesWritten;
            }
        }
        #endregion
        #region InternalWriteByteArray
        private Int64 InternalWriteByteArray(IntPtr pAddress, Byte[] newVal)
        {
            if (!IsValid())
                return -1;
            if (pAddress == IntPtr.Zero)
                return 0;
            FullAccess(pAddress, newVal.Length);
            return MemoryCore.WriteBytes(_process.GetSafeHandle(), pAddress, newVal);
        }
        #endregion
        #endregion
        #region Poke
        public void Poke(String pokeScript)
        {
            if (!IsValid())
                return;
            var pokeCodes = GetPokes(pokeScript);
            foreach (var c in pokeCodes)
                WriteMem(c.Address, c.OpCodes);
        }
        private static IEnumerable<PokeCode> GetPokes(String pokeScript)
        {
            var ret = new List<PokeCode>();
            foreach (var l in UStringUtils.GetLines(pokeScript))
            {
                var line = l.Trim();
                if (string.IsNullOrEmpty(line))
                    continue;
                if (!line.StartsWith("Poke "))
                    continue;
                var g = URegexUtils.MatchGroupRet(line, @"^Poke\s+(\w+)\s+(.*)$");
                if (g.Count != 3)
                    continue;
                var addressStr = g[1].ToString().Trim();
                var opCodes = g[2].ToString().Trim();
                opCodes = URegexUtils.Replace(opCodes, @"\s+", "");
                var address = UStringUtils.StringToInt64("0x" + addressStr);
                if (opCodes.Length % 2 != 0)
                    continue;
                const int splitSize = 32;
                var offset = 0;
                while (offset < opCodes.Length)
                {
                    var possible = (opCodes.Length - offset) / 2;
                    if (possible <= 0)
                        break;
                    if (possible > splitSize)
                        possible = splitSize;
                    var size = possible * 2;
                    var section = opCodes.Substring(offset, size);
                    var bStr = UByteStringUtils.BinaryHex(section);
                    if (bStr == null)
                        break;
                    ret.Add(new PokeCode((IntPtr)address, bStr.Data));
                    address += possible;
                    offset += size;
                }
            }
            return ret;
        }
        #endregion
        #region HasFocus
        public Boolean HasFocus()
        {
            var id = WindowCore.GetWindowProcessId(WindowCore.GetForegroundWindow());
            return (IntPtr)id == _process.GetId();
        }
        #endregion
        #region Alloc
        public IntPtr Alloc(int size, MemoryProtectionFlags protectionFlags, MemoryAllocationFlags allocFlags)
        {
            if (!IsValid())
                return IntPtr.Zero;
            return MemoryCore.Allocate(_process.GetSafeHandle(), size, protectionFlags, allocFlags);
        }
        public IntPtr Alloc(IntPtr address, int size, MemoryProtectionFlags protectionFlags, MemoryAllocationFlags allocFlags)
        {
            if (!IsValid())
                return IntPtr.Zero;
            return MemoryCore.Allocate(_process.GetSafeHandle(), address, size, protectionFlags, allocFlags);
        }
        #endregion
        #region AllocNear
        public IntPtr AllocNear(IntPtr preferred, int size, MemoryProtectionFlags protectionFlags, MemoryAllocationFlags allocFlags)
        {
            if (!IsValid())
                return IntPtr.Zero;
            var address = (IntPtr)FindFreeBlockForRegion(preferred.ToUIntPtr(), (UInt32)size).ToUInt64();
            return MemoryCore.Allocate(_process.GetSafeHandle(), address, size, protectionFlags, allocFlags);
        }
        #endregion
        #region LastChanceAllocPreferred
        public IntPtr LastChanceAllocPreferred(IntPtr preferred, int size, MemoryProtectionFlags protection)
        {
            var startTime = UDateTime.Ticks();
            var address = UIntPtr.Zero;
            var distance = 0UL;
            var count = 0;
            if (preferred.ToInt64() % 65536 > 0)
                preferred = (IntPtr)(preferred.ToInt64() - (preferred.ToInt64() % 65536));
            while (address == UIntPtr.Zero && (count < 10 || (UDateTime.Ticks() < startTime + 10000)) && (distance < 0x80000000UL))
            {
                address = AllocNear((IntPtr)(preferred.ToUIntPtr().ToUInt64() + distance), size, protection, MemoryAllocationFlags.Reserve | MemoryAllocationFlags.Commit).ToUIntPtr();
                if (address == UIntPtr.Zero)
                    distance += 65536;
                count += 1;
            }
            return address.ToIntPtr();
        }
        #endregion
        #region Free
        public Boolean Free(IntPtr pointer)
        {
            if (!IsValid())
                return false;
            return MemoryCore.Free(_process.GetSafeHandle(), pointer);
        }
        #endregion
        #region FindFreeBlockForRegion -- to do
        public UIntPtr FindFreeBlockForRegion(UIntPtr baseAddress, UInt32 size) // todo fill this in
        {
            return UIntPtr.Zero;
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
        #region CreateThread
        public Boolean CreateThread(IntPtr address, IntPtr parameter, out int threadId)
        {
            var sh = _process.GetSafeHandle();
            var h = NativeMethods.CreateRemoteThread(sh, IntPtr.Zero, 0, address, parameter, 0, out threadId);
            return h != null && h.DangerousGetHandle() != IntPtr.Zero;
        }
        #endregion
    }
}
