using System;
using Tack.LGenerics;

namespace Tack.LAutoAssembler
{
    public class tassemblerbytes : TArrayManager<Byte>
    {
        #region Constructor
        public tassemblerbytes()
            : base()
        {
        }
        public tassemblerbytes(params Byte[] bytes)
            : base(bytes)
        {
        }
        #endregion
    }
}
