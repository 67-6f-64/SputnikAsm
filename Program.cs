using System;
using Sputnik.LUtils;
using SputnikAsm.LAssembler;
using SputnikAsm.LAutoAssembler;
using SputnikAsm.LAutoAssembler.LCollections;
using SputnikAsm.LCollections;
using SputnikAsm.LSymbolHandler;

namespace SputnikAsm
{
    class Program
    {
        static void Main(string[] args)
        {
            //var v2 = new AVex2Byte();
            //var v3 = new AVex3Byte();
            //
            //Console.WriteLine(v2.ToUInt64());
            //
            //v2.L = 1;
            //v2.Pp = 1;
            //v2.Vvvv = 11;
            //
            //Console.WriteLine(v2.ToUInt64());
            //Console.WriteLine(UBinaryUtils.Expand(UBitConverter.PackSingle("K", v2.ToUInt64())));
            //
            //Console.WriteLine(v2.L);
            //Console.WriteLine(v2.Pp);
            //Console.WriteLine(v2.Vvvv);
            //
            //Console.ReadKey();
            //Environment.Exit(1);


            var pacWin = new AProcess("pacwin.exe");
            var a = new AAssembler();
            var b = new AByteArray();
            a.SymHandler.Process = pacWin;

            //var result = a.Assemble("mov eax, [edx+esi+66]", 0x400300, b); // E9 FB 01 00 00
            //Console.WriteLine("Result: " + result);
            //Console.WriteLine("Bytes:");
            //Console.WriteLine(UBinaryUtils.Expand(b.Raw));

            //Console.ReadKey();
            //Environment.Exit(1);

            var cc = @"
            [ENABLE]
registersymbol(meeko)
alloc(dog, $1000)
label(noerror)
label(cat)
400300:
mov eax, dword ptr[400500]
mov eax, edx
cat:
mov eax,[ecx+10]
jmp short noerror
xor eax,eax
noerror:
jmp dog
jmp cat
meeko:
    db 00 00 00 00

dog:
mov eax, edx
jmp 400300

[DISABLE]
            ".Trim();
            var aa = new AAutoAssembler(a);
            var code = new ARefStringArray();
            code.Assign(UStringUtils.GetLines(cc).ToArray());
            aa.RemoveComments(code);

            var scr = new ARefStringArray();
            var info = new ADisableInfo();
            var ret = aa.AutoAssemble(pacWin, code, false, true, false, false, info, false, scr);
            Console.WriteLine("Result: " + ret);
            //aa.AutoAssemble(pacWin, code, false, true, false, false, info, true, scr);
            //foreach (var o in scr.Raw)
            //{
            //    Console.WriteLine("Line: " + o.Value);
            //}
            Console.WriteLine("meeko Loc: " + a.SymHandler.GetUserDefinedSymbolByName("meeko").ToUInt64().ToString("X"));

            // Console.WriteLine(proc.ReadMem((IntPtr)0x411C88, ReadType.Int32));
            // proc.Poke(scr.ToString());

            var labels = new AStringArray();
            aa.GetPotentialLabels(code, labels);
            foreach (var o in labels.Raw)
                Console.WriteLine("Label: " + o);

            Console.ReadKey();
        }
    }
}
