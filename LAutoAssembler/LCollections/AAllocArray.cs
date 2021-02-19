using Sputnik.LGenerics;

namespace SputnikAsm.LAutoAssembler.LCollections
{
    public class AAllocArray : UArrayManager<AAlloc>
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
