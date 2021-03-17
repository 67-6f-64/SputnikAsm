C# .NET port of the Cheat Engine Auto Assembler and Disassembler.

For use in trainers/mods etc.

The full assembler/disassembler is working is up to date with CE as of January 2021.

Auto Assembler is mostly fully complete only aobscan/loadlibrary is missing but isnt hard to implement.

To begin using it you referance the SputnikAsm.dll and Sputnik.dll to a new C# project then we can begin.

The most important step is to initiate the tools on the process we want to mess with in this case Pacwin.exe

```
Copy Codevar m = new AProcessSharp(System.Diagnostics.Process.GetProcessesByName("pacwin")[0], AMemoryType.Remote);
UTokenSp.Activate();
AAsmTools.InitTools(new ASymbolHandler());
AAsmTools.SymbolHandler.Process = m;
Now we can use the AAsmTools to access the parts we need.
```

Lets disassemble from address 0x40230F to 30 cpu instructions first create 

```
var d = AAsmTools.Disassembler; // grab the disassembler
d.IsDataOnly = false;
// d.Is64Bit = true; // here we turn on/off if we force 64bit mode it will autodetect so ignore
var s1 = "";
var ptr = (UIntPtr)0x40230F; // address we want to disassemble from
var i = 30; // number of instructions to disassemble
while (i-- > 0)
{
    try
    {
        var dis = d.Disassemble(ref ptr, ref s1); // begin the disassembly
        var cl = d.LastDisassembleData.Prefix + ' ' + d.LastDisassembleData.OpCode + ' ' + d.LastDisassembleData.Parameters; // make it human readable
        var dec = d.DecodeLastParametersToString(); // extra step to decode the ouput
        d.SplitDisassembledString(dis, false, out var address, out var bytes, out var opcode, out var special); // get the full output code so we can print out
        Console.WriteLine($"0x{address.PadRight(8)} {bytes.PadRight(20)} {opcode} {special} ; {dec}"); // print it to console
    }
    catch (Exception e) // handle any errors
    {
        Console.WriteLine("Error " + e.Message);
        Console.WriteLine(e.Source);
        Console.WriteLine(e.StackTrace);
        break;
    }
}
```

This produces the following output

```
0x040230F  ff 0d 881c4100       dec [00411c88]  ; (3)
0x0402315  e8 7ef4ffff          call 00401798  ; ->
0x040231A  c3                   ret   ;
0x040231B  ff 05 881c4100       inc [00411c88]  ; (3)
0x0402321  6a 14                push 14  ; 20
0x0402323  68 b8010000          push 000001b8  ; 440
0x0402328  e8 64eeffff          call 00401191  ; ->
0x040232D  83 c4 08             add esp,08  ; 8
0x0402330  6a 14                push 14  ; 20
0x0402332  68 08020000          push 00000208  ; 520
0x0402337  e8 55eeffff          call 00401191  ; ->
0x040233C  83 c4 08             add esp,08  ; 8
0x040233F  6a 14                push 14  ; 20
0x0402341  68 e0010000          push 000001e0  ; 480
0x0402346  e8 46eeffff          call 00401191  ; ->
0x040234B  83 c4 08             add esp,08  ; 8
0x040234E  e8 45f4ffff          call 00401798  ; ->
0x0402353  c3                   ret   ;
0x0402354  e8 3ff4ffff          call 00401798  ; ->
0x0402359  c3                   ret   ;
0x040235A  00 00                add [eax],al  ;
0x040235C  55                   push ebp  ;
0x040235D  8b ec                mov ebp,esp  ;
0x040235F  53                   push ebx  ;
0x0402360  56                   push esi  ;
0x0402361  8b 5d 08             mov ebx,[ebp+08]  ;
0x0402364  be 00254100          mov esi,00412500  ; (512)
0x0402369  33 c0                xor eax,eax  ;
0x040236B  eb 09                jmp 00402376  ; ->
0x040236D  3b d3                cmp edx,ebx  ;
Now lets assemble an instruction

Copy Codevar a = AAsmTools.Assembler; // grab the assembler
var b1 = new AByteArray(); // an array to store the compiled bytes
a.Assemble("mov eax, [edx+esi+66]", 0x400300, b1); // assemble the instruction
Console.WriteLine(UBinaryUtils.Expand(b1.TakeAll())); // prints out 8B 44 32 66
```

lets auto assemble a script

```
var aa = AAsmTools.AutoAssembler; // grab the auto assembler

// make the script
var cc = @"
[ENABLE]
alloc(storage, 1000)
label(caption)
label(message)

400300:
push 0
push caption
push message
push 0
call MessageBoxA
push 0
add esp, 4
retn
// storage zone
storage:
caption:
    db 'caption', 00
message:
    db 'message', 00
[DISABLE]
".Trim();

// place the code into an array
var code = new ARefStringArray();
code.Assign(UStringUtils.GetLines(cc).ToArray());
aa.RemoveComments(code);

// create an array to hold any bytes from the script (if needed)
var scr = new AScriptBytesArray();

// this will store all the information we need to disable the script later if we want to
var info = new ADisableInfo();

// run the "enable" part of the script as shown by the "true"
var ret = aa.AutoAssemble(m, code, false, true, false, false, info, false, scr);

Console.WriteLine("Result: " + ret); // prints "true" if the code was injected into the game

//aa.AutoAssemble(m, code, false, false, false, false, info, false, scr); // we could run the disable
If we want to get the bytes instead of actually injecting we can run this function instead

Copy Codeaa.AutoAssemble(m, code, false, true, false, false, info, true, scr); // produce script
foreach (var o in scr) // loop through script and print
    Console.WriteLine(o.Type + " " + o.Address.ToUInt64().ToString("X") + " " + AStringUtils.BinToHexStr(o.Bytes.TakeAll()));

// Produces
// Poke 400300 6A00680000530068080053006A00E82D0CAB756A0083C404C3
// Poke 530000 63617074696F6E006D65737361676500
```

Here is another way to inject code

```
Copy Codevar f = new AAssemblyFactory(m, new ASharpAsm()); // create the assembler
f.Inject( // create injection
    new[]
    {   // code to inject
        "mov, eax 7",
        "push 0",
        "add esp, 4",
        "retn"
    },
    (IntPtr) 0x400310); // address to inject

// we can also execute the code afterwards
var v = f.Execute<int>((IntPtr)0x400310); // execute it
Console.WriteLine(v); // returns 7
```
    
Another way of doing the above
```
Copy Codevar f = new AAssemblyFactory(m, new ASharpAsm()); // create the assembler
f.InjectAndExecute( // create injection
    new[]
    {   // add the code to run the messagebox inside the game
        "alloc(storage, 1000)",
        "label(caption)",
        "label(message)",
        "push 0",
        "push caption",
        "push message",
        "push 0",
        "call MessageBoxA",
        "push 0",
        "add esp, 4",
        "retn",
        // storage zone
        "storage:",
        "caption:",
        "    db 'caption', 00",
        "message:",
        "    db 'message', 00",
    },
    (IntPtr)0x400300); // address to inject the code at
```
That causes the game run a message box window

Now lets do something really cool and allocate some memory in our program and write some assembly instructions to that memory location then execute that location as if it was a normal C# program and return the result

```
// Define the delegate that will use the memory address and behave like a normal C# function
[UnmanagedFunctionPointer(CallingConvention.StdCall)]
public delegate int ITest(int a);
public static ITest iTest;

static void Main(string[] args)
{
    // activate the assembly/disassembly tools
    UTokenSp.Activate();
    AAsmTools.InitTools(new ASymbolHandler());
    var m = AAsmTools.SelfSymbolHandler.Process;
    AAsmTools.SymbolHandler.Process = m;

    // grab the auto assembler
    var aa = AAsmTools.AutoAssembler;

    // define a function we want to execute
    var cc = @"
    [ENABLE]
    alloc(func, $1000)
    registersymbol(func)

    func: // define function here
    mov eax, #1337 // return 1337 
    ret

    [DISABLE]
    unregistersymbol(func)
    ".Trim();

    // compile the function
    var code = new ARefStringArray();
    code.Assign(UStringUtils.GetLines(cc).ToArray());
    aa.RemoveComments(code);
    var scr = new AScriptBytesArray();
    var info = new ADisableInfo();
    var ret = aa.AutoAssemble(m, code, false, true, false, false, info, false, scr);
    Console.WriteLine("Result: " + ret);

    // get the address of the function
    var procAddress = AAsmTools.SymbolHandler.GetUserDefinedSymbolByName("func");
    Console.WriteLine("Symbol: " + procAddress.ToUInt64().ToString("X"));

    // shift the function pointer into a c# function so we can call it
    iTest = (ITest)Marshal.GetDelegateForFunctionPointer(procAddress.ToIntPtr(), typeof(ITest));

    // call the function!
    Console.WriteLine(iTest.Invoke(1));
    Console.ReadKey();
    Environment.Exit(1);
}

// Final output
// Result: True
// Symbol: 2771A030000
// 1337
```

As you can see this provides all kinds of powerful tools.
