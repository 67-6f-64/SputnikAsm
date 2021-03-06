namespace SputnikAsm.LProcess.LApplied
{
    public class AComplexAppliedManager<T> : AAppliedManager<T>, IAComplexAppliedManager<T> where T : IAComplexApplied
    {
        public void Disable(T item, bool dueToRules)
        {
            Disable(item.Identifier, dueToRules);
        }

        public void Disable(string name, bool dueToRules)
        {
            InternalItems[name].Disable(dueToRules);
        }

        public void Enable(T item, bool dueToRules)
        {
            Enable(item.Identifier, dueToRules);
        }

        public void Enable(string name, bool dueToRules)
        {
            InternalItems[name].Enable(dueToRules);
        }

        public void DisableAll(bool dueToRules)
        {
            foreach (var value in InternalItems.Values)
                value.Disable(dueToRules);
        }

        public void EnableAll(bool dueToRules)
        {
            foreach (var value in InternalItems.Values)
                value.Enable(dueToRules);
        }
    }
}