using System;
using SputnikAsm.LAssembler;
using SputnikAsm.LAutoAssembler;
using SputnikAsm.LBinary.LByteInterpreter;
using SputnikAsm.LDisassembler;
using SputnikAsm.LProcess;
using SputnikAsm.LProcess.LMemory;
using SputnikAsm.LSymbolHandler;

namespace SputnikAsm
{
    public static class AAsmTools
    {
        #region Static Variables
        public static ASymbolHandler SelfSymbolHandler;
        public static ASymbolHandler SymbolHandler;
        public static AAssembler Assembler;
        public static AAutoAssembler AutoAssembler;
        public static ADisassembler Disassembler;
        public static AByteInterpreter ByteInterpreter;
        #endregion
        #region Variables
        public static Boolean Initiated;
        #endregion
        #region InitTools
        public static void InitTools(ASymbolHandler symbolHandler)
        {
            if (Initiated)
                return;
            Initiated = true;
            SymbolHandler = symbolHandler;
            Assembler = new AAssembler(SymbolHandler);
            AutoAssembler = new AAutoAssembler(Assembler);
            Disassembler = new ADisassembler(SymbolHandler);
            ByteInterpreter = new AByteInterpreter(SymbolHandler);
            SelfSymbolHandler = new ASymbolHandler();
            SelfSymbolHandler.Process = new AProcessSharp(System.Diagnostics.Process.GetCurrentProcess().Id, AMemoryType.Remote);
        }
        #endregion
        #region SetActiveProcess
        public static void SetActiveProcess(AProcessSharp process)
        {
            SymbolHandler.Process = process; // todo add a refresh here
        }
        #endregion
    }
}
