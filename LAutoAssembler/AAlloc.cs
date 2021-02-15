using System;
using SputnikWin.LExtra.LMemorySharp.Native;

namespace SputnikAsm.LAutoAssembler
{
    public class AAlloc
    {
        #region Variables
        public String Name;
        public UIntPtr Address;
        public UInt32 Size;
        public UIntPtr Preferred;
        public MemoryProtectionFlags Protection;
        #endregion
        #region Constructor
        public AAlloc()
        {
            Name = "";
            Address = UIntPtr.Zero;
            Size = 0;
            Preferred = UIntPtr.Zero;
            Protection = MemoryProtectionFlags.ExecuteReadWrite;
        }
        public AAlloc(String name, UIntPtr address, UInt32 size, UIntPtr preferred, MemoryProtectionFlags protection)
        {
            Name = name;
            Address = address;
            Size = size;
            Preferred = preferred;
            Protection = protection;
        }
        #endregion
    };
}
