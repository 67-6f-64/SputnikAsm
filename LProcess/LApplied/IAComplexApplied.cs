namespace SputnikAsm.LProcess.LApplied
{
    public interface IAComplexApplied : IAApplied
    {
        bool DisabledDueToRules { get; set; }
        bool IgnoreRules { get; }
        void Enable(bool disableDueToRules);
        void Disable(bool disableDueToRules);
    }
}