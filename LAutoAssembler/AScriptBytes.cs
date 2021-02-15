using System;
using SputnikAsm.LAutoAssembler.LEnums;
using SputnikAsm.LCollections;

namespace SputnikAsm.LAutoAssembler
{
    public class AScriptBytes
    {
        #region Variables
        public AScriptObjectType Type;
        public UIntPtr Address;
        public AByteArray Bytes;
        #endregion
        #region Constructor
        public AScriptBytes()
        {
            Type = AScriptObjectType.None;
            Address = UIntPtr.Zero;
            Bytes = new AByteArray();
        }
        public AScriptBytes(AScriptObjectType type, UIntPtr address, AByteArray bytes)
        {
            Type = type;
            Address = address;
            Bytes = bytes;
        }
        #endregion
    }
}
