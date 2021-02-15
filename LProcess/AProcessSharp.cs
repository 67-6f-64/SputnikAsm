using System;
using System.Diagnostics;
using SputnikAsm.LProcess.LMemory;
using SputnikAsm.LProcess.LModules;
using SputnikAsm.LProcess.LNative;
using SputnikAsm.LProcess.LNative.LTypes;
using SputnikAsm.LProcess.LThreads;
using SputnikAsm.LProcess.LWindows;
using SputnikAsm.LProcess.Utilities;

namespace SputnikAsm.LProcess
{
    /// <summary>
    ///     A class that offsers several tools to interact with a process.
    /// </summary>
    /// <seealso cref="AProcessSharp" />
    public class AProcessSharp
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AProcessSharp" /> class.
        /// </summary>
        /// <param name="native">The native process.</param>
        /// <param name="type">The type of memory being manipulated.</param>
        public AProcessSharp(System.Diagnostics.Process native,AMemoryType type)
        {
            native.EnableRaisingEvents = true;

            native.Exited += (s, e) =>
            {
                ProcessExited?.Invoke(s, e);
                HandleProcessExiting();
            };

            Native = native;

            Handle = AMemoryHelper.OpenProcess(ProcessAccessFlags.AllAccess, Native.Id);
            switch (type)
            {
                case AMemoryType.Local:
                    Memory = new ALocalProcessMemory(Handle);
                    break;
                case AMemoryType.Remote:
                    Memory = new AExternalProcessMemory(Handle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            native.ErrorDataReceived += OutputDataReceived;
            native.OutputDataReceived += OutputDataReceived;

            ThreadFactory = new AThreadFactory(this);
            ModuleFactory = new AModuleFactory(this);
            MemoryFactory = new AMemoryFactory(this);
            WindowFactory = new AWindowFactory(this);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AProcessSharp" /> class.
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <param name="type">The type of memory being manipulated.</param>
        public AProcessSharp(string processName, AMemoryType type) : this(AProcessHelper.FromName(processName), type)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AProcessSharp" /> class.
        /// </summary>
        /// <param name="processId">The process id of the process to open with all rights.</param>
        /// <param name="type">The type of memory being manipulated.</param>
        public AProcessSharp(int processId, AMemoryType type) : this(AProcessHelper.FromProcessId(processId), type)
        {
        }

        /// <summary>
        /// Raises when the <see cref="AProcessSharp"/> object is disposed.
        /// </summary>
        public event EventHandler OnDispose;

        /// <summary>
        ///     Class for reading and writing memory.
        /// </summary>
        public IAMemory Memory { get; set; }

        /// <summary>
        ///     Provide access to the opened process.
        /// </summary>
        public System.Diagnostics.Process Native { get; set; }

        /// <summary>
        ///     The process handle opened with all rights.
        /// </summary>
        public ASafeMemoryHandle Handle { get; set; }

        /// <summary>
        ///     Factory for manipulating threads.
        /// </summary>
        public IAThreadFactory ThreadFactory { get; set; }

        /// <summary>
        ///     Factory for manipulating modules and libraries.
        /// </summary>
        public IAModuleFactory ModuleFactory { get; set; }

        /// <summary>
        ///     Factory for manipulating memory space.
        /// </summary>
        public IAMemoryFactory MemoryFactory { get; set; }

        /// <summary>
        ///     Factory for manipulating windows.
        /// </summary>
        public IAWindowFactory WindowFactory { get; set; }

        /// <summary>
        ///     Gets the <see cref="AProcessSharpModule" /> with the specified module name.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <returns>IProcessModule.</returns>
        public AProcessSharpModule this[string moduleName] => ModuleFactory[moduleName];

        /// <summary>
        ///     Gets the <see cref="IAPointer" /> with the specified address.
        /// </summary>
        /// <param name="intPtr">The address the pointer is located at in memory.</param>
        /// <returns>IPointer.</returns>
        public IAPointer this[IntPtr intPtr] => new AMemoryPointer(this, intPtr);

        protected bool IsDisposed { get; set; }
        protected bool MustBeDisposed { get; set; } = true;
        public Boolean IsX86 => ANative.IsProcessId64Bit(Native.Id) == 0;
        public Boolean IsX64 => ANative.IsProcessId64Bit(Native.Id) == 1;
        public int PointerSize => IsX64 ? 8 : 4;
        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public virtual void Dispose()
        {
            if (!IsDisposed)
            {
                IsDisposed = true;

                OnDispose?.Invoke(this, EventArgs.Empty);
                ThreadFactory?.Dispose();
                ModuleFactory?.Dispose();
                MemoryFactory?.Dispose();
                WindowFactory?.Dispose();
                Handle?.Close();
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        ///     Handles the process exiting.
        /// </summary>
        /// <remarks>Created 2012-02-15</remarks>
        protected virtual void HandleProcessExiting()
        {
        }

        /// <summary>
        ///     Event queue for all listeners interested in ProcessExited events.
        /// </summary>
        public event EventHandler ProcessExited;

        private static void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Trace.WriteLine(e.Data);
        }

        ~AProcessSharp()
        {
            if (MustBeDisposed)
            {
                Dispose();
            }
        }
    }
}