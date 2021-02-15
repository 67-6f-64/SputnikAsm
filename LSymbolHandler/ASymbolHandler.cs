using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Sputnik.LBinary;
using Sputnik.LEngine;
using Sputnik.LEnums;
using Sputnik.LInterfaces;
using Sputnik.LMarshal;
using Sputnik.LUtils;
using SputnikAsm.LGenerics;
using SputnikWin;
using SputnikWin.LExtra.LMemorySharp.Native;
using SputnikWin.LFileSystem;
using SputnikWin.LUtils;

namespace SputnikAsm.LSymbolHandler
{
    public class ASymbolHandler
    {
        public AProcess Process = new AProcess();
        public AArrayManager<ASymbol> SymbolList;
        #region Constructor
        public ASymbolHandler()
        {
            SymbolList = new AArrayManager<ASymbol>();
        }
        #endregion
        #region GetAddressFromSymbol
        public UIntPtr GetAddressFromSymbol(String s)
        {
            for (var i = 0; i < SymbolList.Length; i++)
            {
                if (String.Equals(s, SymbolList[i].Name, StringComparison.CurrentCultureIgnoreCase))
                    return SymbolList[i].Address;
            }
            return UIntPtr.Zero;
        }
        #endregion
        #region GetAddressFromName
        public UIntPtr GetAddressFromName(String name)
        {
            return GetAddressFromName(name, true);
        }
        public UIntPtr GetAddressFromName(string name, Boolean waitForSymbols)
        {
            var result = GetAddressFromName(name, waitForSymbols, out var x);
            if (x)
                throw new Exception(UStringUtils.Sprintf("Failure determining what %s means", name));
            return result;
        }
        public UIntPtr GetAddressFromName(String name, Boolean waitforsymbols, out Boolean haserror)
        {
            if (UStringUtils.IsXDigit(name))
            {
                haserror = false;
                return (UIntPtr)UStringUtils.StringToUInt64("0x" + name);
            }
            haserror = true;
            return UIntPtr.Zero;
        }
        #endregion
        public void WaitForSymbolsLoaded()
        {
        }
        public void AddUserDefinedSymbol(String address, String name)
        {
        }
        public void DeleteUserdefinedSymbol(String name)
        {
        }
    }
}
