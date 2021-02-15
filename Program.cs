using System;
using System.Net.Sockets;
using Sputnik.LBinary;
using Sputnik.LMarshal;
using Sputnik.LUtils;
using SputnikAsm.LAssembler;
using SputnikAsm.LAutoAssembler;
using SputnikAsm.LAutoAssembler.LCollections;
using SputnikAsm.LBinary;
using SputnikAsm.LBinary.LByteInterpreter;
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
using SputnikAsm.LSymbolHandler;
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

            var m = new AProcessSharp(System.Diagnostics.Process.GetProcessesByName("pacwin")[0], AMemoryType.Remote);
            UTokenSp.Activate();
            AAsmTools.InitTools(new ASymbolHandler());
            AAsmTools.SymbolHandler.Process = m;
            var a = AAsmTools.Assembler;
            var b1 = new AByteArray();
            //var m2 = new AProcessSharp(System.Diagnostics.Process.GetCurrentProcess(), AMemoryType.Remote);
            a.SymbolHandler.Process = m;
            var aa = AAsmTools.AutoAssembler;
            //var result = a.Assemble("mov eax, [edx+esi+66]", 0x400300, b1); // E9 FB 01 00 00
            //var result = a.Assemble("mov rax,[1122334455778899]", 0x400300, b1); // E9 FB 01 00 00
            //Console.WriteLine("Result: " + result);
            //Console.WriteLine("Bytes:");
            //Console.WriteLine(UBinaryUtils.Expand(b1.TakeAll()));
            //
            //
            var d = AAsmTools.Disassembler;
            //var sd = "";
            //using (var pt = new UBytePtr(b1.TakeAll()))
            //{
            //    var ptt = pt.ToIntPtr().ToUIntPtr();
            //    d.Disassemble(ref ptt, ref sd);
            //}
            //Console.WriteLine(d.LastDisassembleData.Prefix + ' ' + d.LastDisassembleData.OpCode + ' ' + d.LastDisassembleData.Parameters);

            var bi = AAsmTools.ByteInterpreter;
            var bp = m.Memory.Read((IntPtr)0x411C88, 32);
            using (var bip = new UBytePtr(bp))
            {
                Console.WriteLine("Found dataType: " + bi.FindTypeOfData((UIntPtr)0x411C88, bip, 8));
            }

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
            var i = 30;
            while (i-- > 0)
            {
                try
                {
                    var dis = d.Disassemble(ref ptr, ref s1);
                    var cl = d.LastDisassembleData.Prefix + ' ' + d.LastDisassembleData.OpCode + ' ' + d.LastDisassembleData.Parameters;
                    var dec = d.DecodeLastParametersToString();
                    //Console.WriteLine(cl + " ; " + dec);
                    d.SplitDisassembledString(dis, false, out var address, out var bytes, out var opcode, out var special);
                    Console.WriteLine($"0x{address.PadRight(8)} {bytes.PadRight(20)} {opcode} {special} ; {dec}");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error " + e.Message);
                    Console.WriteLine(e.Source);
                    Console.WriteLine(e.StackTrace);
                    break;
                }
            }
            // Console.ReadKey();
            // Environment.Exit(1);



            a.SymbolHandler.Process = m;

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
