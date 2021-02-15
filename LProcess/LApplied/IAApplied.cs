using SputnikAsm.LProcess.LMarshaling;

namespace SputnikAsm.LProcess.LApplied
{
    public interface IAApplied : IADisposableState
    {
        string Identifier { get; }
        bool IsEnabled { get; }
        void Disable();
        void Enable();
    }
}