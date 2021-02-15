using System;
using Sputnik.LUtils;
using Tack.LGenerics;

namespace Tack.LAutoAssembler
{
    public class ttokens : TArrayManager<String>
    {
        #region Constructor
        public ttokens()
            : base()
        {
        }
        public ttokens(params String[] tokens)
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
