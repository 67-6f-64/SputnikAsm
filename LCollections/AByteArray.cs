using System;
using SputnikAsm.LGenerics;

namespace SputnikAsm.LCollections
{
    public class AByteArray : AArrayManager<Byte>
    {
        #region Constructor
        public AByteArray()
            : base()
        {
        }
        public AByteArray(params Byte[] values)
            : base(values)
        {
        }
        #endregion
    }
}
