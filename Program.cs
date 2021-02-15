using System;
using Sputnik.LBinary;
using Sputnik.LUtils;
using SputnikAsm.LAssembler;
using SputnikAsm.LAutoAssembler;
using SputnikAsm.LAutoAssembler.LCollections;
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

            //var result = a.Assemble("mov eax, edx", 0x400300, b); // A1 09 00 00 00
            var result = a.Assemble("mov eax, dword ptr [400500]", 0x400300, b); // A1 09 00 00 00
            //var result = a.Assemble("jmp 400500", 0x400300, b); // E9 FB 01 00 00
            Console.WriteLine("Result: " + result);
            Console.WriteLine("Bytes:");
            Console.WriteLine(UBinaryUtils.Expand(b.Raw));
            
            
            var cc = @"
            [ENABLE]
            400300:
            mov eax, edx

            [DISABLE]
            400300:
            ".Trim();
            var aa = new AAutoAssembler();
            var code = new ARefStringArray();
            code.Assign(UStringUtils.GetLines(cc).ToArray());
            aa.RemoveComments(code);

            var scr = new ARefStringArray();
            var ret = aa.AutoAssemble(UIntPtr.Zero, code, false, true, false, new AAllocArray(), new AStringArray(), true, scr);
            Console.WriteLine("Result: " + ret);
            foreach (var o in scr.Raw)
            {
                Console.WriteLine("Line: " + o.Value);
            }


            Console.ReadKey();
        }
    }
}
