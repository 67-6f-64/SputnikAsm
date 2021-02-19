using System;
using Sputnik.LGenerics;

namespace SputnikAsm.LCollections
{
    public class ACharArray : UArrayManager<Char>
    {
        #region Constructor
        public ACharArray()
            : base()
        {
        }
        public ACharArray(params Char[] values)
            : base(values)
        {
        }
        #endregion
    }
}
