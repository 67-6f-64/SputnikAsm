using SputnikAsm.LAssembler;
using Sputnik.LGenerics;

namespace SputnikAsm.LCollections
{
    public class AIndexArray : UArrayManager<AIndex>
    {
        #region Constructor
        public AIndexArray()
            : base()
        {
        }
        public AIndexArray(params AIndex[] values)
            : base(values)
        {
        }
        public AIndexArray(int size)
            : this()
        {
            EnsureCapacity(size);
        }
        #endregion
        #region NextElement
        public override AIndex NextElement(int index)
        {
            return new AIndex();
        }
        #endregion
    }
}
