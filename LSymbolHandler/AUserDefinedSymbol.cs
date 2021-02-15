using System;

namespace SputnikAsm.LSymbolHandler
{
    public class AUserDefinedSymbol
    {
        #region Variables
        public String Name;
        public UIntPtr Address;
        public String AddressString;
        public UInt32 AllocSize; //if it is a global alloc, allocsize>0
        public IntPtr ProcessId; //the processid this memory was allocated to (in case of processswitches)
        #endregion
        #region Constructor
        public AUserDefinedSymbol()
        {
            Name = "";
            Address = UIntPtr.Zero;
            AllocSize = 0;
            ProcessId = IntPtr.Zero;
        }
        #endregion
        #region IsMatch
        public Boolean IsMatch(String name)
        {
            return String.Equals(Name, name, StringComparison.CurrentCultureIgnoreCase);
        }
        #endregion
    };
}
