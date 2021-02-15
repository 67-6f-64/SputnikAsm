using System;
using System.Runtime.InteropServices;
using SputnikAsm.LProcess.LMemory;

namespace SputnikAsm.LProcess.LModules
{
    /// <summary>
    ///     Class representing a function in the remote process.
    /// </summary>
    public class ARemoteFunction : AMemoryPointer, AProcessSharpFunction
    {
        public ARemoteFunction(AProcessSharp processPlus, IntPtr address, string functionName) : base(processPlus, address)
        {
            // Save the parameter
            Name = functionName;
        }

        /// <summary>
        ///     The name of the function.
        /// </summary>
        public string Name { get; }

        public T GetDelegate<T>()
        {
            return Marshal.GetDelegateForFunctionPointer<T>(BaseAddress);
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return $"BaseAddress = 0x{BaseAddress.ToInt64():X} Name = {Name}";
        }
    }
}