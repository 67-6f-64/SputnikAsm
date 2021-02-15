using System;
using SputnikAsm.LAutoAssembler.LCollections;
using SputnikAsm.LCollections;
using SputnikAsm.LGenerics;
using SputnikAsm.LSymbolHandler;

namespace SputnikAsm.LAutoAssembler
{
    public class ADisableInfo
    {
        #region Variables
        public AAllocArray Allocs;
        public AStringArray RegisteredSymbols;
        public AArrayManager<Exception> Exceptions;
        public ASymbolHandler CCodeSymbols;
        public Boolean DoNotFreeCCodeSymbols;
        public ARefStringArray AllSymbols; //filled at the end with all known symbols (allocs, labels, kallocs, aobscan results, defines that are addresses, etc...)
        #endregion
        #region Constructor
        public ADisableInfo()
        {
            Allocs = new AAllocArray();
            RegisteredSymbols = new AStringArray();
            Exceptions = new AArrayManager<Exception>();
            CCodeSymbols = new ASymbolHandler();
            DoNotFreeCCodeSymbols = false;
            AllSymbols = new ARefStringArray();
        }
        #endregion
    }
}
