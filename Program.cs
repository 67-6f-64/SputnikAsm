using System;
using Sputnik.LBinary;
using Sputnik.LMarshal;
using Sputnik.LUtils;
using SputnikAsm.LAssembler;
using SputnikAsm.LAutoAssembler;
using SputnikAsm.LAutoAssembler.LCollections;
using SputnikAsm.LBinary;
using SputnikAsm.LCollections;
using SputnikAsm.LDisassembler;
using SputnikAsm.LDisassembler.LEnums;
using SputnikAsm.LExtensions;
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

            UTokenSp.Activate();
            var a = new AAssembler();
            var b1 = new AByteArray();
            var m = new AProcessSharp(System.Diagnostics.Process.GetProcessesByName("pacwin")[0], AMemoryType.Remote);
            //var m2 = new AProcessSharp(System.Diagnostics.Process.GetCurrentProcess(), AMemoryType.Remote);
            a.SymHandler.Process = m;
            var aa = new AAutoAssembler();
            //var result = a.Assemble("mov eax, [edx+esi+66]", 0x400300, b1); // E9 FB 01 00 00
            //var result = a.Assemble("mov rax,[1122334455778899]", 0x400300, b1); // E9 FB 01 00 00
            //Console.WriteLine("Result: " + result);
            //Console.WriteLine("Bytes:");
            //Console.WriteLine(UBinaryUtils.Expand(b1.TakeAll()));
            //
            //
            var d = new ADisassembler(a.SymHandler);
            //var sd = "";
            //using (var pt = new UBytePtr(b1.TakeAll()))
            //{
            //    var ptt = pt.ToIntPtr().ToUIntPtr();
            //    d.Disassemble(ref ptt, ref sd);
            //}
            //Console.WriteLine(d.LastDisassembleData.Prefix + ' ' + d.LastDisassembleData.OpCode + ' ' + d.LastDisassembleData.Parameters);
            
            //Console.ReadKey();
            //Environment.Exit(1);

            // aa.Assembler.SymHandler.Process = m;
            // var cd = @"
            // 400300:
            // mov rax, [411c88]
            // mov rax, dword ptr[11223344556677]
            // ".Trim();
            // var codex = new ARefStringArray();
            // codex.Assign(UStringUtils.GetLines(cd).ToArray());
            // aa.RemoveComments(codex);
            // 
            // var b = aa.Assemble(aa.SelfSymbolHandler.Process, codex);
            d.IsDataOnly = false;
            // d.Is64Bit = true;
            // d.SymbolHandler.Process = m;
            var s1 = "";
            // var bytes = new UBytePtr(b[0].Bytes.TakeAll());
            // //var ptr = bytes.ToIntPtr().ToUIntPtr();
            // 
            var ptr = (UIntPtr)0x40230F;
            var i = 10;
            while (i-- > 0)
            {
                try
                {
                    d.Disassemble(ref ptr, ref s1);
                    var currentline = d.LastDisassembleData.Prefix + ' ' + d.LastDisassembleData.OpCode + ' ' + d.LastDisassembleData.Parameters;
                    Console.WriteLine(currentline);
                }
                catch
                {
                    break;
                }
            }
            // Console.ReadKey();
            // Environment.Exit(1);



            a.SymHandler.Process = m;

            var cc = @"
[ENABLE]
400300:
mov edx, dword ptr[411c88]
reassemble(40230f);
reassemble(pacwin.exe+2379);
cat:
reassemble(pacwin.exe+237C);
call messageboxa
jmp cat


400314:
[DISABLE]
            ".Trim();
           var code = new ARefStringArray();
           code.Assign(UStringUtils.GetLines(cc).ToArray());
           aa.RemoveComments(code);
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
