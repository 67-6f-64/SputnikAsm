using System;
using System.Threading.Tasks;
using SputnikAsm.LProcess.LAssembly.LAssemblers;
using SputnikAsm.LProcess.LMemory;
using SputnikAsm.LProcess.LNative.LTypes;

namespace SputnikAsm.LProcess.LAssembly
{
    public interface IAAssemblyFactory : IDisposable
    {
        IAAssembler Assembler { get; set; }
        AProcessSharp Process { get; }
        AAssemblyTransaction BeginTransaction(bool autoExecute = true);
        AAssemblyTransaction BeginTransaction(IntPtr address, bool autoExecute = true);

        IntPtr Execute(IntPtr address);
        IntPtr Execute(IntPtr address, dynamic parameter);
        IntPtr Execute(IntPtr address, CallingConventions callingConvention, params dynamic[] parameters);
        T Execute<T>(IntPtr address);
        T Execute<T>(IntPtr address, dynamic parameter);
        T Execute<T>(IntPtr address, CallingConventions callingConvention, params dynamic[] parameters);
        Task<IntPtr> ExecuteAsync(IntPtr address);
        Task<IntPtr> ExecuteAsync(IntPtr address, dynamic parameter);

        Task<IntPtr> ExecuteAsync(IntPtr address, CallingConventions callingConvention,
            params dynamic[] parameters);

        Task<T> ExecuteAsync<T>(IntPtr address);
        Task<T> ExecuteAsync<T>(IntPtr address, dynamic parameter);

        Task<T> ExecuteAsync<T>(IntPtr address, CallingConventions callingConvention,
            params dynamic[] parameters);

        IAAllocatedMemory Inject(string[] asm);
        IAAllocatedMemory Inject(string asm);
        void Inject(string[] asm, IntPtr address);
        void Inject(string asm, IntPtr address);
        IntPtr InjectAndExecute(string[] asm);
        IntPtr InjectAndExecute(string asm);
        IntPtr InjectAndExecute(string[] asm, IntPtr address);
        IntPtr InjectAndExecute(string asm, IntPtr address);
        T InjectAndExecute<T>(string[] asm);
        T InjectAndExecute<T>(string asm);
        T InjectAndExecute<T>(string[] asm, IntPtr address);
        T InjectAndExecute<T>(string asm, IntPtr address);
        Task<IntPtr> InjectAndExecuteAsync(string[] asm);
        Task<IntPtr> InjectAndExecuteAsync(string asm);
        Task<IntPtr> InjectAndExecuteAsync(string[] asm, IntPtr address);
        Task<IntPtr> InjectAndExecuteAsync(string asm, IntPtr address);
        Task<T> InjectAndExecuteAsync<T>(string[] asm);
        Task<T> InjectAndExecuteAsync<T>(string asm);
        Task<T> InjectAndExecuteAsync<T>(string[] asm, IntPtr address);
        Task<T> InjectAndExecuteAsync<T>(string asm, IntPtr address);
    }
}