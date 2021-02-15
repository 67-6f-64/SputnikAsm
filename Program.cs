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

            b.EnsureCapacity(0);
            a.singlelineassembler.createmodrm(b, 6, "[7]");

            Console.WriteLine(UBinaryUtils.Expand(b.Raw));

            Console.ReadKey();
        }
    }
}
