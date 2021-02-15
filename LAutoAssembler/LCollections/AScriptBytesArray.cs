using System;
using SputnikAsm.LAutoAssembler.LEnums;
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
        public void Add(AScriptObjectType type, UIntPtr address, AByteArray bytes)
        {
            Add(new AScriptBytes(type, address, bytes));
        }
        #endregion
        #region GetTotalBytes
        public int GetTotalBytes()
        {
            var total = 0;
            foreach (var c in this)
            {
                if (c.Type == AScriptObjectType.Poke)
                    total += c.Bytes.Length;
            }
            return total;
        }
        #endregion
    }
}
