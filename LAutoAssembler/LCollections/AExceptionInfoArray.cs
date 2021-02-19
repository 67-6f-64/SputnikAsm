using Sputnik.LGenerics;

namespace SputnikAsm.LAutoAssembler.LCollections
{
    public class AExceptionInfoArray : UArrayManager<AExceptionInfo>
    {
        #region Constructor
        public AExceptionInfoArray()
            : base()
        {
        }
        public AExceptionInfoArray(params AExceptionInfo[] values)
            : base(values)
        {
        }
        #endregion
    }
}
