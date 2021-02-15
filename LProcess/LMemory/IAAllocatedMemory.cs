using SputnikAsm.LProcess.LMarshaling;

namespace SputnikAsm.LProcess.LMemory
{
    public interface IAAllocatedMemory : IAPointer, IADisposableState
    {
        bool IsAllocated { get; }
        int Size { get; }
        string Identifier { get; }
    }
}