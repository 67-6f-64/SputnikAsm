using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sputnik.LBinary;
using Sputnik.LStructs;
using Sputnik.LUtils;
using Tack.LAutoAssembler;

namespace Tack
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new AutoAssembler();
            var b = new tassemblerbytes();

            //var result = a.assemble("mov eax, edx", 400300, b); // 8B C2
            var result = a.assemble("mov eax, dword ptr[esi]", 0x400300, b); // 8B C2
            Console.WriteLine("Result: " + result);
            Console.WriteLine("Bytes:");
            Console.WriteLine(UBinaryUtils.Expand(b.Raw));

            Console.ReadKey();
        }
    }
}
