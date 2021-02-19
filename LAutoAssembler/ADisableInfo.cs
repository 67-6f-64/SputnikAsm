using System;
using SputnikAsm.LAutoAssembler.LCollections;
using SputnikAsm.LCollections;
using Sputnik.LGenerics;
using SputnikAsm.LSymbolHandler;

namespace SputnikAsm.LAutoAssembler
{
    public class ADisableInfo
    {
        #region Variables
        public AAllocArray Allocs;
        public AStringArray RegisteredSymbols;
        public UArrayManager<Exception> Exceptions;
        public ASymbolHandler CCodeSymbols;
        public Boolean DoNotFreeCCodeSymbols;
        public ARefStringArray AllSymbols; //filled at the end with all known symbols (allocs, labels, kallocs, aobscan results, defines that are addresses, etc...)
        #endregion
        #region Constructor
        public ADisableInfo()
        {
            Allocs = new AAllocArray();
            RegisteredSymbols = new AStringArray();
            Exceptions = new UArrayManager<Exception>();
            CCodeSymbols = new ASymbolHandler();
            DoNotFreeCCodeSymbols = false;
            AllSymbols = new ARefStringArray();
        }
        #endregion
    }
}
