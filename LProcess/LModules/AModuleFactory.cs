using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using SputnikAsm.LProcess.LMemory;

namespace SputnikAsm.LProcess.LModules
{
    /// <summary>
    ///     Class providing tools for manipulating modules and libraries.
    /// </summary>
    public class AModuleFactory : IAModuleFactory
    {
        /// <summary>
        ///     The list containing all injected modules (writable).
        /// </summary>
        protected readonly List<AInjectedModule> InternalInjectedModules;

        protected readonly AProcessSharp ProcessPlus;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AModuleFactory" /> class.
        /// </summary>
        /// <param name="processPlus">The reference of the <see cref="AProcessSharp" /> object.</param>
        public AModuleFactory(AProcessSharp processPlus)
        {
            // Save the parameter
            ProcessPlus = processPlus;
            // Create a list containing all injected modules
            InternalInjectedModules = new List<AInjectedModule>();
        }

        /// <summary>
        ///     Gets a pointer from the remote process.
        /// </summary>
        /// <param name="address">The address of the pointer.</param>
        /// <returns>A new instance of a <see cref="IAPointer" /> class.</returns>
        public IAPointer this[IntPtr address] => new AMemoryPointer(ProcessPlus, address);

        /// <summary>
        ///     Gets the main module for the remote process.
        /// </summary>
        public AProcessSharpModule MainModule
        {
            get
            {
                // Convert module name with lower chars
                if (ProcessPlus?.Native?.MainModule != null)
                {
                    var moduleName = ProcessPlus.Native.MainModule.ModuleName.ToLower();
                    // Check if the module name has an extension
                    if (!Path.HasExtension(moduleName))
                        moduleName += ".dll";
                    // Fetch and return the module
                    return new ARemoteModule(ProcessPlus, NativeModules.First(m => m.ModuleName.ToLower() == moduleName));
                }
                return null;
            }
        }

        /// <summary>
        ///     Gets the modules that have been loaded in the remote process.
        /// </summary>
        public IEnumerable<AProcessSharpModule> RemoteModules => NativeModules.Select(FetchModule);

        /// <summary>
        ///     Gets the native modules that have been loaded in the remote process.
        /// </summary>
        public IEnumerable<ProcessModule> NativeModules => ProcessPlus.Native.Modules.Cast<ProcessModule>();

        /// <summary>
        ///     Gets the specified module in the remote process.
        /// </summary>
        /// <param name="moduleName">The name of module (not case sensitive).</param>
        /// <returns>A new instance of a <see cref="ARemoteModule" /> class.</returns>
        public AProcessSharpModule this[string moduleName] => FetchModule(moduleName);

        /// <summary>
        ///     Releases all resources used by the <see cref="AModuleFactory" /> object.
        /// </summary>
        public virtual void Dispose()
        {
            // Release all injected modules which must be disposed
            foreach (var injectedModule in InternalInjectedModules.Where(m => m.MustBeDisposed))
                injectedModule.Dispose();
            // Clean the cached functions related to this process
            foreach (
                var cachedFunction in
                    ARemoteModule.CachedFunctions.ToArray()
                        .Where(cachedFunction => cachedFunction.Key.Item2 == ProcessPlus.Handle))
                ARemoteModule.CachedFunctions.Remove(cachedFunction);
            // Avoid the finalizer
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     A collection containing all injected modules.
        /// </summary>
        public IEnumerable<AInjectedModule> InjectedModules => InternalInjectedModules.AsReadOnly();

        /// <summary>
        ///     Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count.
        /// </summary>
        /// <param name="moduleName">The name of module to eject.</param>
        public void Eject(string moduleName)
        {
            // Fint the module to eject
            var module = RemoteModules.FirstOrDefault(m => m.Name == moduleName);
            // Eject the module is it's valid
            if (module != null)
                ARemoteModule.InternalEject(ProcessPlus, module);
        }

        /// <summary>
        ///     Injects the specified module into the address space of the remote process.
        /// </summary>
        /// <param name="path">
        ///     The path of the module. This can be either a library module (a .dll file) or an executable module
        ///     (an .exe file).
        /// </param>
        /// <param name="mustBeDisposed">The module will be ejected when the finalizer collects the object.</param>
        /// <returns>A new instance of the <see cref="AInjectedModule" />class.</returns>
        public AInjectedModule Inject(string path, bool mustBeDisposed = true)
        {
            // Injects the module
            var module = AInjectedModule.InternalInject(ProcessPlus, path);
            // Add the module in the list
            InternalInjectedModules.Add(module);
            // Return the module
            return module;
        }

        /// <summary>
        ///     Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count.
        /// </summary>
        /// <param name="module">The module to eject.</param>
        public void Eject(AProcessSharpModule module)
        {
            // If the module is valid
            if (!module.IsValid) return;

            // Find if the module is an injected one
            var injected = InternalInjectedModules.FirstOrDefault(m => m.Equals(module));
            if (injected != null)
                InternalInjectedModules.Remove(injected);

            // Eject the module
            ARemoteModule.InternalEject(ProcessPlus, module);
        }

        /// <summary>
        ///     Frees resources and perform other cleanup operations before it is reclaimed by garbage collection.
        /// </summary>
        ~AModuleFactory()
        {
            Dispose();
        }

        /// <summary>
        ///     Fetches a module from the remote process.
        /// </summary>
        /// <param name="moduleName">
        ///     A module name (not case sensitive). If the file name extension is omitted, the default library
        ///     extension .dll is appended.
        /// </param>
        /// <returns>A new instance of a <see cref="ARemoteModule" /> class.</returns>
        public AProcessSharpModule FetchModule(string moduleName)
        {
            // Convert module name with lower chars
            moduleName = moduleName.ToLower();
            // Main?
            var main = ProcessPlus.ModuleFactory.MainModule;
            if (moduleName == main.Name.ToLower())
                return main;
            // Check if the module name has an extension
            var hasExt = Path.HasExtension(moduleName);
            if (!hasExt)
                moduleName += ".dll";
            // Fetch and return the module
            foreach (var m in NativeModules)
            {
                if (m.ModuleName.ToLower() == moduleName)
                    return new ARemoteModule(ProcessPlus, m);
            }
            return null;
        }

        /// <summary>
        ///     Fetches a module from the remote process.
        /// </summary>
        /// <param name="module">A module in the remote process.</param>
        /// <returns>A new instance of a <see cref="ARemoteModule" /> class.</returns>
        public AProcessSharpModule FetchModule(ProcessModule module)
        {
            return FetchModule(module.ModuleName);
        }
    }
}