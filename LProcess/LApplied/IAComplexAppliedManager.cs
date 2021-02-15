namespace SputnikAsm.LProcess.LApplied
{
    public interface IAComplexAppliedManager<T> : IAAppliedManager<T> where T : IAApplied
    {
        void Disable(T item, bool dueToRules);
        void Disable(string name, bool dueToRules);
        void Enable(T item, bool dueToRules);
        void Enable(string name, bool dueToRules);
        void DisableAll(bool dueToRules);
        void EnableAll(bool dueToRules);
    }
}