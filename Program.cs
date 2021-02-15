using System;
using Sputnik.LUtils;
using SputnikAsm.LAssembler;
using SputnikAsm.LAutoAssembler;
using SputnikAsm.LAutoAssembler.LCollections;
using SputnikAsm.LCollections;
using SputnikAsm.LGenerics;
using SputnikAsm.LProcess;
using SputnikAsm.LProcess.LAssembly;
using SputnikAsm.LProcess.LAssembly.LAssemblers;
using SputnikAsm.LProcess.LMemory;
using SputnikAsm.LProcess.Utilities;
using SputnikAsm.LUtils;
using SputnikWin.LFeatures.LWindows;

namespace SputnikAsm
{
    class Program
    {
        static void Main(string[] args)
        {
            //var Brackets = new ACharArray('(', ')', '[', ']', '{', '}');
            //var StdWordDelims = new ACharArray(',', '.', ';', '/', '\\', ':', '\'', '"', '`', '(', ')', '[', ']', '{', '}');
            //StdWordDelims.AddRange(Brackets.TakeAll());
            //StdWordDelims.AddRange(ACharUtils.Range('\0', ' ').TakeAll());
            //
            //Console.WriteLine("done");
            //Console.ReadKey();
            //Environment.Exit(1);


            UTokenSp.Activate();
            //var a = new AAssembler();
            //var b = new AByteArray();
            var m = new AProcessSharp(System.Diagnostics.Process.GetProcessesByName("pacwin")[0], AMemoryType.Remote);
            //a.SymHandler.Process = m;
            //var result = a.Assemble("mov eax, [edx+esi+66]", 0x400300, b); // E9 FB 01 00 00
            //Console.WriteLine("Result: " + result);
            //Console.WriteLine("Bytes:");
            //Console.WriteLine(UBinaryUtils.Expand(b.Raw));

            var aa = new AAutoAssembler();
            aa.Assembler.SymHandler.Process = m;



            var cc = @"
            [ENABLE]
createThread(exec)
400300:

data:
    db 00 00 00 00

exec:
mov, eax 7
exec2:
inc eax
mov [data], eax
jmp exec2
push 0
add esp, 4
retn

[DISABLE]
dealloc(dog)
            ".Trim();
           var code = new ARefStringArray();
           code.Assign(UStringUtils.GetLines(cc).ToArray());
           aa.RemoveComments(code);

           aa.GetEnableAndDisablePos(code, out var en, out var de);

           var scr = new AScriptBytesArray();
           var info = new ADisableInfo();
           var ret = aa.AutoAssemble(m, code, false, true, false, false, info, false, scr);
           Console.WriteLine("Loaded 2");
           Console.WriteLine("Result: " + ret);
           aa.AutoAssemble(m, code, false, true, false, false, info, true, scr);
           Console.WriteLine("Loaded 3");
            foreach (var o in scr)
               Console.WriteLine("Line: " + o.Type + " " + o.Address.ToUInt64().ToString("X") + " " + AStringUtils.BinToHexStr(o.Bytes.TakeAll()));
           
           // var f = new AAssemblyFactory(m, new ASharpAsm());
           // f.Inject(
           //     new[]
           //     {
           //         "mov, eax 7",
           //         "push 0",
           //         "add esp, 4",
           //         "retn"
           //     },
           //     (IntPtr) 0x400310);
           //
           // var v = f.Execute<int>((IntPtr)0x400310);
           // Console.WriteLine("Return: " + v);


         //f.InjectAndExecute(
         //    new[]
         //    {
         //        "alloc(storage, 1000)",
         //        "label(caption)",
         //        "label(message)",
         //        "push 0",
         //        "push caption",
         //        "push message",
         //        "push 0",
         //        "call MessageBoxA",
         //        "push 0",
         //        "add esp, 4",
         //        "retn",
         //        // storage zone
         //        "storage:",
         //        "caption:",
         //        "    db 'caption', 00",
         //        "message:",
         //        "    db 'message', 00",
         //    },
         //    (IntPtr)0x400300);

            Console.ReadKey();
            //aa.AutoAssemble(m, code, false, false, false, false, info, false, scr);
            Environment.Exit(1);
        }
    }
}
