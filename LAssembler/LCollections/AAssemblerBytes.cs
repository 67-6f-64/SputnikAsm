using System;
using SputnikAsm.LGenerics;

namespace SputnikAsm.LAssembler.LCollections
{
    public class AAssemblerBytes : AArrayManager<Byte>
    {
        #region Constructor
        public AAssemblerBytes()
            : base()
        {
        }
        public AAssemblerBytes(params Byte[] bytes)
            : base(bytes)
        {
        }
        #endregion
    }
}
