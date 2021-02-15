using System;
using SputnikAsm.LCollections;

namespace SputnikAsm.LAutoAssembler
{
    public class AScriptBytes
    {
        #region Variables
        public String Type;
        public UIntPtr Address;
        public AByteArray Bytes;
        #endregion
        #region Constructor
        public AScriptBytes()
        {
            Type = "";
            Address = UIntPtr.Zero;
            Bytes = new AByteArray();
        }
        public AScriptBytes(String type, UIntPtr address, AByteArray bytes)
        {
            Type = type;
            Address = address;
            Bytes = bytes;
        }
        #endregion
    }
}
