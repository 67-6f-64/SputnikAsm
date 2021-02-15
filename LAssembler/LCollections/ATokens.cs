using System;
using Sputnik.LUtils;
using SputnikAsm.LGenerics;

namespace SputnikAsm.LAssembler.LCollections
{
    public class ATokens : AArrayManager<String>
    {
        #region Constructor
        public ATokens()
            : base()
        {
        }
        public ATokens(params String[] tokens)
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
