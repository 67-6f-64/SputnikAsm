using SputnikAsm.LGenerics;

namespace SputnikAsm.LAutoAssembler.LCollections
{
    public class AAllocArray : AArrayManager<AAlloc>
    {
        #region Constructor
        public AAllocArray()
            : base()
        {
        }
        public AAllocArray(params AAlloc[] values)
            : base(values)
        {
        }
        #endregion
    }
}
