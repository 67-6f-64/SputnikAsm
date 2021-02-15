using System;
using Sputnik.LUtils;
using SputnikAsm.LAutoAssembler;
using SputnikAsm.LAutoAssembler.LCollections;
using SputnikAsm.LCollections;
using SputnikAsm.LProcess;
using SputnikAsm.LProcess.LAssembly;
using SputnikAsm.LProcess.LAssembly.LAssemblers;
using SputnikAsm.LProcess.LMemory;
using SputnikAsm.LUtils;
using SputnikWin.LFeatures.LWindows;

namespace SputnikAsm
{
    class Program
    {
        static void Main(string[] args)
        {
            UTokenSp.Activate();
            //var a = new AAssembler();
            //var b = new AByteArray();
            var m = new AProcessSharp(System.Diagnostics.Process.GetProcessesByName("pacwin")[0], AMemoryType.Remote);
            //a.SymHandler.Process = m;
            //var result = a.Assemble("mov eax, [edx+esi+66]", 0x400300, b); // E9 FB 01 00 00
            //Console.WriteLine("Result: " + result);
            //Console.WriteLine("Bytes:");
            //Console.WriteLine(UBinaryUtils.Expand(b.Raw));

            //Console.ReadKey();
            //Environment.Exit(1);

            var aa = new AAutoAssembler();
            aa.Assembler.SymHandler.Process = m;
            var tokens = new AStringArray();
            aa.Assembler.Tokenize("mov eax, dword ptr[400500]", tokens);
            var cc = @"
            [ENABLE]
alloc(dog, $1000)
400300:
mov eax, dword ptr[400500]
mov eax, edx
jmp dog

dog:
mov eax, edx
jmp 400300

[DISABLE]
dealloc(dog)
            ".Trim();
            var code = new ARefStringArray();
            code.Assign(UStringUtils.GetLines(cc).ToArray());
            aa.RemoveComments(code);
            var scr = new AScriptBytesArray();
            var info = new ADisableInfo();
            var ret = aa.AutoAssemble(m, code, false, true, false, false, info, false, scr);
            Console.WriteLine("Result: " + ret);
            aa.AutoAssemble(m, code, false, true, false, false, info, true, scr);
            foreach (var o in scr.Raw)
                Console.WriteLine("Line: " + o.Type + " " + o.Address.ToUInt64().ToString("X") + " " + AStringUtils.BinToHexStr(o.Bytes.Raw));

            var f = new AAssemblyFactory(m, new ASharpAsm());
            f.Inject(
                new[]
                {
                    "mov, eax 7",
                    "push 0",
                    "add esp, 4",
                    "retn"
                },
                (IntPtr)0x400310);
            
            var reta = f.Execute<int>((IntPtr)0x400310);
            Console.WriteLine("Return: " + reta);


            Console.ReadKey();
            //aa.AutoAssemble(m, code, false, false, false, false, info, false, scr);
            Environment.Exit(1);
        }
    }
}
