using System;
using SputnikAsm.LCollections;

namespace SputnikAsm.LDisassembler
{
    public class APrefix : AByteArray
    {
        #region Constructor
        public APrefix()
            : base()
        {
        }
        public APrefix(params Byte[] values)
            : base(values)
        {
        }
        #endregion
    }
}
