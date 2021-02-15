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
            var pacWin = new AProcess("pacwin.exe");
            var a = new AAssembler();
            var b = new AByteArray();

            // var result = a.Assemble("mov eax, edx", 0x400300, b); // A1 09 00 00 00
            // //var result = a.Assemble("mov eax, dword ptr [400500]", 0x400300, b); // A1 09 00 00 00
            // //var result = a.Assemble("jmp 400500", 0x400300, b); // E9 FB 01 00 00
            // Console.WriteLine("Result: " + result);
            // Console.WriteLine("Bytes:");
            // Console.WriteLine(UBinaryUtils.Expand(b.Raw));
            
            
            var cc = @"
            [ENABLE]
            400300:
            jmp 600700
            mov eax, edx
            inc esi
            dec edx
            mov eax, dword ptr[411C88]

            [DISABLE]
            400300:
            mov eax, edx
            ".Trim();
            var aa = new AAutoAssembler();
            var code = new ARefStringArray();
            code.Assign(UStringUtils.GetLines(cc).ToArray());
            aa.RemoveComments(code);

            var scr = new ARefStringArray();
            var ret = aa.AutoAssemble(pacWin, code, false, true, false, new AAllocArray(), new AStringArray(), false, scr);
            Console.WriteLine("Result: " + ret);
            foreach (var o in scr.Raw)
            {
                Console.WriteLine("Line: " + o.Value);
            }

            // Console.WriteLine(proc.ReadMem((IntPtr)0x411C88, ReadType.Int32));
            // proc.Poke(scr.ToString());

            Console.ReadKey();
        }
    }
}
