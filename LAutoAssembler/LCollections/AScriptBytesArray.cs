using System;
using SputnikAsm.LCollections;
using SputnikAsm.LGenerics;

namespace SputnikAsm.LAutoAssembler.LCollections
{
    public class AScriptBytesArray : AArrayManager<AScriptBytes>
    {
        #region Constructor
        public AScriptBytesArray()
            : base()
        {
        }
        public AScriptBytesArray(params AScriptBytes[] values)
            : base(values)
        {
        }
        #endregion
        #region Add
        public void Add(String type, UIntPtr address, AByteArray bytes)
        {
            Add(new AScriptBytes(type, address, bytes));
        }
        #endregion
    }
}
