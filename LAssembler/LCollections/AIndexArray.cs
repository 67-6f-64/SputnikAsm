using SputnikAsm.LGenerics;

namespace SputnikAsm.LAssembler.LCollections
{
    public class AIndexArray : AArrayManager<AIndex>
    {
        #region Constructor
        public AIndexArray()
            : base()
        {
        }
        public AIndexArray(params AIndex[] tokens)
            : base(tokens)
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
