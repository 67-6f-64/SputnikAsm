using System;

namespace SputnikAsm.LAutoAssembler
{
    public class AAlloc
    {
        #region Variables
        public String Name;
        public UIntPtr Address;
        public UInt32 Size;
        public UIntPtr Preferred;
        #endregion
        #region Constructor
        public AAlloc()
        {
            Name = "";
            Address = UIntPtr.Zero;
            Size = 0;
            Preferred = UIntPtr.Zero;
        }
        public AAlloc(String name, UIntPtr address, UInt32 size, UIntPtr preferred)
        {
            Name = name;
            Address = address;
            Size = size;
            Preferred = preferred;
        }
        #endregion
    };
}
