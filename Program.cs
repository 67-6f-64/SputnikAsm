using System;
using Sputnik.LUtils;
using SputnikAsm.LAssembler;
using SputnikAsm.LAutoAssembler;
using SputnikAsm.LCollections;

namespace SputnikAsm
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new AAssembler();
            var b = new AByteArray();

            var result = a.assemble("mov eax, dword ptr [400500]", 0x400300, b); // A1 09 00 00 00
            //var result = a.assemble("jmp 400500", 0x400300, b); // E9 FB 01 00 00
            Console.WriteLine("Result: " + result);
            Console.WriteLine("Bytes:");
            Console.WriteLine(UBinaryUtils.Expand(b.Raw));


            var cc = @"
            mov eax, edx
            [Enable]
            mov esi, edi
            [Disable]
            lea eax, [esi]
            ";
            var aa = new AAutoAssembler();
            var code = new AStringArray();
            code.Assign(UStringUtils.GetLines(cc).ToArray());
            aa.RemoveComments(code);
            foreach(var l in code.Raw)
                Console.WriteLine("Code: " + l);

            var ret = aa.GetEnableAndDisablePos(code, out int epos, out int dpos);
            Console.WriteLine("Found " + ret + " Enable " + epos + " Disable " + dpos);


            Console.ReadKey();
        }
    }
}