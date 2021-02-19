using System;
using Sputnik.LGenerics;

namespace SputnikAsm.LCollections
{
    public class ATByteArray : UArrayManager<UInt16>
    {
        #region Constructor
        public ATByteArray()
            : base()
        {
        }
        public ATByteArray(params UInt16[] values)
            : base(values)
        {
        }
        #endregion
    }
}
