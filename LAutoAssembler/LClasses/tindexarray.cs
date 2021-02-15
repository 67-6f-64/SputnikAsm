using System;
using Tack.LGenerics;

namespace Tack.LAutoAssembler
{
    public class tindexarray : TArrayManager<tindex>
    {
        #region Constructor
        public tindexarray()
            : base()
        {
        }
        public tindexarray(params tindex[] tokens)
            : base(tokens)
        {
        }
        public tindexarray(int size)
            : this()
        {
            EnsureCapacity(size);
        }
        #endregion
        #region NextElement
        public override tindex NextElement(int index)
        {
            return new tindex();
        }
        #endregion
    }
}
