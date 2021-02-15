using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SputnikAsm.LProcess.LModules
{
    public interface IAModuleFactory : IDisposable
    {
        AProcessSharpModule this[string moduleName] { get; }

        IEnumerable<AInjectedModule> InjectedModules { get; }
        AProcessSharpModule MainModule { get; }
        IEnumerable<AProcessSharpModule> RemoteModules { get; }
        IEnumerable<ProcessModule> NativeModules { get; }
        void Eject(string moduleName);
        void Eject(AProcessSharpModule module);
        AInjectedModule Inject(string path, bool mustBeDisposed = true);
        AProcessSharpModule FetchModule(string moduleName);
    }
}