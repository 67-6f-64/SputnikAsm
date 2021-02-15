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
            // var k2 = new AArrayManager<int>();
            // k2.Add(10);
            // k2.Add(20);
            // k2.Add(30);
            // k2.Insert(1, new [] { 4, 5, 6 });
            // foreach (var o in k2.Raw)
            // {
            //     Console.WriteLine("Raw: " + o);
            // }
            // Console.ReadKey();
            // Environment.Exit(1);


            var pacWin = new AProcess("pacwin.exe");
            var a = new AAssembler();
            var b = new AByteArray();
            a.SymHandler.Process = pacWin;

            // //var result = a.Assemble("mov eax, edx", 0x400300, b); // 
            // var result = a.Assemble("mov eax, dword ptr [400500]", 0x400300, b); // A1 09 00 00 00
            // //var result = a.Assemble("jmp 400500", 0x400300, b); // E9 FB 01 00 00
            // Console.WriteLine("Result: " + result);
            // Console.WriteLine("Bytes:");
            // Console.WriteLine(UBinaryUtils.Expand(b.Raw));
            
            
            var cc = @"
registersymbol(cat);
            [ENABLE]
alloc(dog,$1000);
label(cat);
400300:
jmp 600700
mov eax, edx
inc esi
cat:
jmp dog

dog:
push 10
push 20
mov eax, edx
call 400700

[DISABLE]
400300:
mov eax, edx
            ".Trim();
            var aa = new AAutoAssembler(a);
            var code = new ARefStringArray();
            code.Assign(UStringUtils.GetLines(cc).ToArray());
            aa.RemoveComments(code);

            var scr = new ARefStringArray();
            //var ret = aa.AutoAssemble(pacWin, code, false, true, false, new AAllocArray(), new AStringArray(), true, scr);
            var ret = aa.AutoAssemble(pacWin, code, false, true, false, new AAllocArray(), new AStringArray(), false, scr);
            Console.WriteLine("Result: " + ret);
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
