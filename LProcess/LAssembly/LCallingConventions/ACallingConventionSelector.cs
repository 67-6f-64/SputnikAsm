using System;
using SputnikAsm.LProcess.LNative.LTypes;
using SputnikAsm.LProcess.Utilities;

namespace SputnikAsm.LProcess.LAssembly.LCallingConventions
{
    /// <summary>
    ///     Static class providing calling convention instances.
    /// </summary>
    public static class ACallingConventionSelector
    {
        /// <summary>
        ///     Gets a calling convention object according the given type.
        /// </summary>
        /// <param name="callingConvention">The type of calling convention to get.</param>
        /// <returns>The return value is a singleton of a <see cref="IACallingConvention" /> child.</returns>
        public static IACallingConvention Get(CallingConventions callingConvention)
        {
            switch (callingConvention)
            {
                case CallingConventions.Cdecl:
                    return ASingleton<ACdeclCallingConvention>.Instance;
                case CallingConventions.Stdcall:
                    return ASingleton<AStdCallCallingConvention>.Instance;
                case CallingConventions.Fastcall:
                    return ASingleton<AFastCallCallingConvention>.Instance;
                case CallingConventions.Thiscall:
                    return ASingleton<AThisCallCallingConvention>.Instance;
                default:
                    throw new ApplicationException("Unsupported calling convention.");
            }
        }
    }
}