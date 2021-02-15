using System;
using SputnikAsm.LCollections;

namespace SputnikAsm.LDisassembler
{
    public class tprefix : AByteArray
    {
        #region Constructor
        public tprefix()
            : base()
        {
        }
        public tprefix(params Byte[] values)
            : base(values)
        {
        }
        #endregion
    }
}
