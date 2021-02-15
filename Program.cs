using System;
using Sputnik.LUtils;
using SputnikAsm.LAssembler;
using SputnikAsm.LAssembler.LCollections;

namespace SputnikAsm
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new AAssembler();
            var b = new AAssemblerBytes();

            //var result = a.assemble("mov eax, edx", 400300, b); // 8B C2
            var result = a.assemble("mov eax, dword ptr[400350]", 0x400300, b); // 8B C2
            Console.WriteLine("Result: " + result);
            Console.WriteLine("Bytes:");
            Console.WriteLine(UBinaryUtils.Expand(b.Raw));

            Console.ReadKey();
        }
    }
}
