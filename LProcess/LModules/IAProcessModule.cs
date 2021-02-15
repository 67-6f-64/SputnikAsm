using System.Diagnostics;
using SputnikAsm.LProcess.LMemory;

namespace SputnikAsm.LProcess.LModules
{
    public interface AProcessSharpModule : IAPointer
    {
        AProcessSharpFunction this[string functionName] { get; }

        bool IsMainModule { get; }
        string Name { get; }
        ProcessModule Native { get; }
        string Path { get; }
        int Size { get; }
        void Eject();

        AProcessSharpFunction FindFunction(string functionName);
    }
}