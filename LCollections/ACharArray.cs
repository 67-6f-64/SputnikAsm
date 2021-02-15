using System;
using SputnikAsm.LGenerics;

namespace SputnikAsm.LCollections
{
    public class ACharArray : AArrayManager<Char>
    {
        #region Constructor
        public ACharArray()
            : base()
        {
        }
        public ACharArray(params Char[] bytes)
            : base(bytes)
        {
        }
        #endregion
    }
}
