using System;
using Sputnik.LBinary;
using Sputnik.LUtils;
using SputnikAsm.LAssembler;
using SputnikAsm.LAutoAssembler;
using SputnikAsm.LCollections;
using SputnikAsm.LUtils;

namespace SputnikAsm
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new AAssembler();
            var b = new AByteArray();

            var result = a.Assemble("mov eax, dword ptr [400500]", 0x400300, b); // A1 09 00 00 00
            //var result = a.Assemble("jmp 400500", 0x400300, b); // E9 FB 01 00 00
            Console.WriteLine("Result: " + result);
            Console.WriteLine("Bytes:");
            Console.WriteLine(UBinaryUtils.Expand(b.Raw));


            var cc = @"
            [Enable]
            mov esi, edi
push 7
            [Disable]
            lea eax, [esi]
            call kittenmeister
            ";
            var aa = new AAutoAssembler();
            var code = new ARefStringArray();
            code.Assign(UStringUtils.GetLines(cc).ToArray());
            aa.RemoveComments(code);
            Console.WriteLine("Full Script:\n" + code);

            Console.WriteLine("Enable Script:\n" + aa.GetScript(code, true));
            Console.WriteLine("Disable Script:\n" + aa.GetScript(code, false));

            var ret = aa.GetEnableAndDisablePos(code, out var epos, out var dpos);
            Console.WriteLine("Found " + ret + " Enable " + epos + " Disable " + dpos);

            var tt = new ARefStringArray();
            var st1 = @"

STRUCT stackview
returnaddress: DD ?
param1: 
  DD ?
param2: DB ? ? ? ?
ENDSTRUCT
";
            aa.TokenizeStruct(st1, tt);
            aa.ReplaceStructWithDefines(tt, 0);
            foreach (var tok in tt.Raw)
            {
                Console.WriteLine("Token: " + tok.Value);
            }

            Console.ReadKey();
        }
    }
}
