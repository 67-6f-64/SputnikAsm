using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sputnik.LUtils;

namespace SputnikAsm.LSymbolHandler
{
    public class ASymbolHandler
    {
        public TProcess process = new TProcess();
        public UIntPtr GetAddressFromName(String name)
        {
            return GetAddressFromName(name, true, out _);
        }
        public UIntPtr GetAddressFromName(String name, Boolean waitforsymbols)
        {
            return GetAddressFromName(name, waitforsymbols, out _);
        }
        public UIntPtr GetAddressFromName(String name, Boolean waitforsymbols, out Boolean haserror)
        {
            if (UStringUtils.IsXDigit(name))
            {
                haserror = false;
                return (UIntPtr) UStringUtils.StringToUInt64("0x" + name);
            }
            if (name == "shinobi.exe")
            {
                haserror = false;
                return (UIntPtr)0x400300;
            }
            if (name == "cat")
            {
                haserror = false;
                return (UIntPtr)0x777;
            }
            haserror = true;
            return UIntPtr.Zero;
        }
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
    public class TProcess
    {
        public UIntPtr Handle = UIntPtr.Zero;
        public Boolean is64bit = true;
    }
}
