using System;
using Sputnik.LUtils;
using SputnikAsm.LGenerics;

namespace SputnikAsm.LCollections
{
    public class AStringArray : AArrayManager<String>
    {
        #region Constructor
        public AStringArray()
            : base()
        {
        }
        public AStringArray(params String[] tokens)
            : base(tokens)
        {
        }
        #endregion
        #region NextElement
        public override String NextElement(int index)
        {
            return UStringUtils.Empty;
        }
        #endregion
    }
}
