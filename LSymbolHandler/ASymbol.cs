using System;

namespace SputnikAsm.LSymbolHandler
{
    public class ASymbol
    {
        #region Variables
        public String Name;
        public UIntPtr Address;
        public UInt32 Size;
        #endregion
        #region Constructor
        public ASymbol()
        {
            Name = "";
            Address = UIntPtr.Zero;
            Size = 0;
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