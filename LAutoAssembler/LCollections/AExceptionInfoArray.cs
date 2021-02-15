using SputnikAsm.LGenerics;

namespace SputnikAsm.LAutoAssembler.LCollections
{
    public class AExceptionInfoArray : AArrayManager<AExceptionInfo>
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
