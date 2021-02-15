using System;
using SputnikAsm.LGenerics;

namespace SputnikAsm.LCollections
{
    public class ATByteArray : AArrayManager<UInt16>
    {
        #region Constructor
        public ATByteArray()
            : base()
        {
        }
        public ATByteArray(params UInt16[] bytes)
            : base(bytes)
        {
        }
        #endregion
    }
}
