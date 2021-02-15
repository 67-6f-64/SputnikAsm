using System;
using Sputnik.LBinary;
using Sputnik.LMarshal;
using Sputnik.LUtils;
using SputnikAsm.LAssembler;
using SputnikAsm.LAutoAssembler;
using SputnikAsm.LAutoAssembler.LCollections;
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


            //var b = new AByteArray();
            //aa.Assembler.Assemble("mov eax, [edx+esi+66]", 0x400300, b);
            //aa.Assembler.Assemble("mov eax, edx", 0x400300, b);

            var cd = @"
mov rax, [FFFFFFFFFFFFFFFF]
            ".Trim();
            var codex = new ARefStringArray();
            codex.Assign(UStringUtils.GetLines(cd).ToArray());
            aa.RemoveComments(codex);

            var b = aa.Assemble(aa.SelfSymbolHandler.Process, codex);
            var d = new Disassembler();
            d.dataonly = true;
            //d.is64bit = true;
            d.SymbolHandler.Process = m;
            var s1 = "";
            //var bytes = new UBytePtr(b[0].Bytes.TakeAll());
            //var ptr = bytes.ToIntPtr().ToUIntPtr();

            var ptr = (UIntPtr)0x40230F;
            var i = 30;
            while (i-- > 0)
            {
                try
                {
                    d.disassemble(ref ptr, ref s1);
                    var currentline = d.lastdisassembledata.prefix + ' ' + d.lastdisassembledata.opcode + ' ' +
                                      d.lastdisassembledata.parameters;
                    Console.WriteLine(currentline);
                }
                catch
                {
                    break;
                }
            }
            Console.ReadKey();
            Environment.Exit(1);




            var cc = @"
[ENABLE]
400300:
mov eax, edx6
lea edx, esi
[DISABLE]
dealloc(dog)
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
           
            var f = new AAssemblyFactory(m, new ASharpAsm());
            f.Inject(
                new[]
                {
                    "mov, eax 7",
                    "push 0",
                    "add esp, 4",
                    "retn"
                },
                (IntPtr) 0x400310);
           
            var v = f.Execute<int>((IntPtr)0x400310);
            Console.WriteLine("Return: " + v);


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
