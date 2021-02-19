using System;
using Sputnik.LGenerics;

namespace SputnikAsm.LCollections
{
    public class AByteArray : UArrayManager<Byte>
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
