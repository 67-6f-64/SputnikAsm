using System;
using Sputnik.LUtils;
using SputnikAsm.LAssembler;
using SputnikAsm.LAutoAssembler;
using SputnikAsm.LAutoAssembler.LCollections;
using SputnikAsm.LCollections;
using SputnikAsm.LGenerics;
using SputnikAsm.LSymbolHandler;

namespace SputnikAsm
{
    class Program
    {
        static void Main(string[] args)
        {
            var pacWin = new AProcess("pacwin.exe");
            var a = new AAssembler();
            var b = new AByteArray();
            a.SymHandler.Process = pacWin;

            //var result = a.Assemble("mov eax, edx", 0x400300, b); // 
            //B8 05 03 40 00
            //8B 05 05 03 40 00
            var result = a.Assemble("mov eax, [400305]", 0x400300, b); // A1 09 00 00 00
            //var result = a.Assemble("jmp 400500", 0x400300, b); // E9 FB 01 00 00
            //var result = a.Assemble("mov eax, [eax+esi+7]", 0x400300, b); // 8B 44 30 07
            Console.WriteLine("Result: " + result);
            Console.WriteLine("Bytes:");
            Console.WriteLine(UBinaryUtils.Expand(b.Raw));

             //Console.ReadKey();
             //Environment.Exit(1);


            var cc = @"
            [ENABLE]
400300:
mov eax, [400500]

[DISABLE]
            ".Trim();
            var aa = new AAutoAssembler(a);
            var code = new ARefStringArray();
            code.Assign(UStringUtils.GetLines(cc).ToArray());
            aa.RemoveComments(code);

            var scr = new ARefStringArray();
            var allocs = new AAllocArray();
            var ret = aa.AutoAssemble(pacWin, code, false, true, false, allocs, new AStringArray(), false, scr);
            Console.WriteLine("Result: " + ret);
            aa.AutoAssemble(pacWin, code, false, true, false, allocs, new AStringArray(), true, scr);
            foreach (var o in scr.Raw)
            {
                Console.WriteLine("Line: " + o.Value);
            }
            Console.WriteLine("Cat Loc: " + a.SymHandler.GetUserDefinedSymbolByName("cat").ToUInt64().ToString("X"));

            // Console.WriteLine(proc.ReadMem((IntPtr)0x411C88, ReadType.Int32));
            // proc.Poke(scr.ToString());

            Console.ReadKey();
        }
    }
}
