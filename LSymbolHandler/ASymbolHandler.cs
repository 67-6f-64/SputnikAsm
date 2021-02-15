using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Sputnik;
using Sputnik.LFileSystem;
using Sputnik.LGenerics;
using Sputnik.LUtils;
using SputnikAsm.LCollections;
using SputnikAsm.LExtensions;
using SputnikAsm.LGenerics;
using SputnikAsm.LProcess;
using SputnikAsm.LProcess.Utilities;
using SputnikAsm.LUtils;

namespace SputnikAsm.LSymbolHandler
{

    public class ASymbolHandler
    {
        #region Internal Classes
        #region AModuleInfo
        internal class AModuleInfo
        {
            #region Variables
            public uint BaseSize;
            public IntPtr BaseAddress;
            public Boolean IsSystemModule;
            public String ModuleName;
            public String ModulePath;
            #endregion
            #region Constructor
            public AModuleInfo()
            {
            }
            #endregion
            #region IsMatch
            public Boolean IsMatch(String moduleName)
            {
                return String.Equals(ModuleName, moduleName, StringComparison.CurrentCultureIgnoreCase);
            }
            #endregion
        }
        #endregion
        #region AModuleInfoArray
        internal class AModuleInfoArray : AArrayManager<AModuleInfo>
        {
            #region Constructor
            public AModuleInfoArray()
                : base()
            {
            }
            public AModuleInfoArray(params AModuleInfo[] values)
                : base(values)
            {
            }
            #endregion
            #region Find
            public AModuleInfo Find(String moduleName)
            {
                foreach (var c in this)
                {
                    if (c.IsMatch(moduleName))
                        return c;
                }
                return null;
            }
            #endregion
            #region FindBaseAddress
            public IntPtr FindBaseAddress(String moduleName)
            {
                foreach (var c in this)
                {
                    if (c.IsMatch(moduleName))
                        return c.BaseAddress;
                }
                return IntPtr.Zero;
            }
            #endregion
        }
        #endregion
        #region ASymbolLoaderThread
        internal class ASymbolLoaderThread
        {
            #region Native
            [Flags]
            public enum SymFlag : uint
            {
                VALUEPRESENT = 0x00000001,
                REGISTER = 0x00000008,
                REGREL = 0x00000010,
                FRAMEREL = 0x00000020,
                PARAMETER = 0x00000040,
                LOCAL = 0x00000080,
                CONSTANT = 0x00000100,
                EXPORT = 0x00000200,
                FORWARDER = 0x00000400,
                FUNCTION = 0x00000800,
                VIRTUAL = 0x00001000,
                THUNK = 0x00002000,
                TLSREL = 0x00004000,
            }
            [Flags]
            public enum SymTagEnum : uint
            {
                Null,
                Exe,
                Compiland,
                CompilandDetails,
                CompilandEnv,
                Function,
                Block,
                Data,
                Annotation,
                Label,
                PublicSymbol,
                UDT,
                Enum,
                FunctionType,
                PointerType,
                ArrayType,
                BaseType,
                Typedef,
                BaseClass,
                Friend,
                FunctionArgType,
                FuncDebugStart,
                FuncDebugEnd,
                UsingNamespace,
                VTableShape,
                VTable,
                Custom,
                Thunk,
                CustomType,
                ManagedType,
                Dimension
            };
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            public struct SYMBOL_INFO
            {
                public uint SizeOfStruct;
                public uint TypeIndex;
                public ulong Reserved1;
                public ulong Reserved2;
                public uint Reserved3;
                public uint Size;
                public ulong ModBase;
                public SymFlag Flags;
                public ulong Value;
                public ulong Address;
                public uint Register;
                public uint Scope;
                public SymTagEnum Tag;
                public int NameLen;
                public int MaxNameLen;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
                public string Name;
            }
            public delegate Boolean SymEnumSymbolsProc(ref SYMBOL_INFO pSymInfo, uint symbolSize, IntPtr context);
            public delegate Boolean SymEnumerateModules64Proc([MarshalAs(UnmanagedType.LPWStr)] String moduleName, UInt64 baseOfDll, IntPtr context);
            public delegate Boolean SymEnumerateSymbols64Proc([MarshalAs(UnmanagedType.LPWStr)] String symbolName, UInt64 symbolAddress, uint symbolSize, IntPtr context);
            [DllImport("dbghelp.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern Boolean SymInitializeW(IntPtr hProcess, String userSearchPath, Boolean fInvadeProcess);
            [DllImport("dbghelp.dll", SetLastError = true)]
            public static extern Boolean SymCleanup(IntPtr hProcess);
            [DllImport("dbghelp.dll", SetLastError = true)]
            public static extern uint SymSetOptions(uint symOptions);
            [DllImport("dbghelp.dll", SetLastError = true)]
            public static extern uint SymGetOptions();
            [DllImport("dbghelp.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SymEnumSymbolsW(IntPtr hProcess, ulong baseOfDll, string mask, SymEnumSymbolsProc enumSymbolsCallback, IntPtr userContext);

            [DllImport("dbghelp.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern ulong SymLoadModuleExW(IntPtr hProcess, IntPtr hFile, String imageName, String moduleName, ulong baseOfDll, int sizeOfDll, IntPtr data, int flags);
            [DllImport("dbghelp.dll", SetLastError = true)]
            public static extern Boolean SymUnloadModule(IntPtr hProcess, ulong baseOfDll);
            [DllImport("psapi.dll", SetLastError = true)]
            public static extern bool EnumProcessModulesEx(IntPtr hProcess, [In][Out] IntPtr[] lphModule, uint cb, out uint lpcbNeeded, uint dwFilterFlag);
            [DllImport("dbghelp.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern Boolean SymEnumerateModulesW64(IntPtr hProcess, SymEnumerateModules64Proc enumModulesCallback, IntPtr userContext);
            [DllImport("dbghelp.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern Boolean SymEnumerateSymbolsW64(IntPtr hProcess, UInt64 baseOfDll, SymEnumerateSymbols64Proc enumSymbolsCallback, IntPtr userContext);

            [DllImport("psapi.dll", CharSet = CharSet.Unicode)]
            public static extern uint GetModuleFileNameExW(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, int nSize);
            #endregion
            #region GetModuleList
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            public struct MODULEENTRY32
            {
                internal uint dwSize;
                internal uint th32ModuleID;
                internal uint th32ProcessID;
                internal uint GlblcntUsage;
                internal uint ProccntUsage;
                internal IntPtr modBaseAddr;
                internal uint modBaseSize;
                internal IntPtr hModule;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
                internal String szModule;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
                internal String szExePath;
            }
            [Flags]
            public enum SnapshotFlags : uint
            {
                HeapList = 0x00000001,
                Process = 0x00000002,
                Thread = 0x00000004,
                Module = 0x00000008,
                Module32 = 0x00000010,
                Inherit = 0x80000000,
                All = 0x0000001F
            }
            [DllImport("kernel32.dll")]
            public static extern bool Module32FirstW(IntPtr hSnapshot, ref MODULEENTRY32 lpme);
            [DllImport("kernel32.dll")]
            public static extern bool Module32NextW(IntPtr hSnapshot, ref MODULEENTRY32 lpme);
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr CreateToolhelp32Snapshot(SnapshotFlags dwFlags, uint th32ProcessID);
            public static AModuleInfoArray GetModuleList(int pid)
            {
                var processList = new AModuleInfoArray();
                var snapshot = CreateToolhelp32Snapshot(SnapshotFlags.Module | SnapshotFlags.Module32, (uint)pid);
                var mod = new MODULEENTRY32() { dwSize = (uint)Marshal.SizeOf(typeof(MODULEENTRY32)) };
                if (!Module32FirstW(snapshot, ref mod))
                    return new AModuleInfoArray();
                var winDir = UEnvironment.GetWinDir();
                do
                {
                    processList.Inc();
                    processList.Last.ModuleName = mod.szModule;
                    processList.Last.BaseAddress = mod.modBaseAddr;
                    processList.Last.BaseSize = mod.modBaseSize;
                    processList.Last.ModulePath = mod.szExePath;
                    processList.Last.IsSystemModule = AStringUtils.Pos(winDir, mod.szExePath, true) != -1;
                }
                while (Module32NextW(snapshot, ref mod));
                return processList;
            }
            #endregion
            public AArrayManager<ASymbol> SymbolList;
            public AProcessSharp Process;
            public Boolean IsLoading;
            public String CurrentModuleName;
            public Boolean Terminated;
            public ASymbolLoaderThread()
            {
                SymbolList = new AArrayManager<ASymbol>();
                Process = null;
                IsLoading = false;
                CurrentModuleName = "";
                Terminated = false;
            }
            public void Execute()
            {
                try
                {
                    IsLoading = true;
                    var h = Process.Native.Handle;
                    SymSetOptions(SymGetOptions() | 0x00000001); // Case Insensitive
                    SymSetOptions(SymGetOptions() | 0x00002000); // Include 32Bit Modules
                    if (!SymInitializeW(h, null, true))
                        return;
                    //LoadDllSymbols(true);
                    //SymEnumerateModulesW64(h, Em, IntPtr.Zero);
                    SymEnumSymbolsW(h, 0, "*!*", EnumerateSymbols, IntPtr.Zero);
                    SymCleanup(h);
                }
                finally
                {
                    IsLoading = false;
                }
            }
            #region EnumerateSymbols
            public Boolean EnumerateSymbols(ref SYMBOL_INFO symInfo, uint symbolSize, IntPtr context)
            {
                SymbolList.Add(new ASymbol(symInfo.Name, (UIntPtr)symInfo.Address, symbolSize));
                return !Terminated;
            }
            #endregion
            #region Em
            public Boolean Em(String moduleName, ulong baseOfDll, IntPtr context)
            {
                CurrentModuleName = moduleName;
                return !Terminated && SymEnumerateSymbolsW64(Process.Native.Handle, baseOfDll, Es, IntPtr.Zero);
            }
            #endregion
            #region Es
            public Boolean Es(String symName, ulong symbolAddress, uint symbolSize, IntPtr context)
            {
                if (symName == "MessageBoxA")
                    Console.WriteLine(symName + " | " + symbolAddress.ToString("X") + " | " + symbolSize);
                SymbolList.Add(new ASymbol(symName, (UIntPtr)symbolAddress, symbolSize));
                return !Terminated;
            }
            #endregion
            #region LoadDllSymbols
            public void LoadDllSymbols(Boolean useSnapshot)
            {
                var h = Process.Native.Handle;
                if (useSnapshot)
                {
                    foreach (var m in GetModuleList(Process.Native.Id))
                    {
                        CurrentModuleName = m.ModuleName;
                        SymLoadModuleExW(h, IntPtr.Zero, m.ModulePath, null, 0, 0, IntPtr.Zero, 0);
                    }
                }
                else
                {
                    var processX86 = Process.IsX86;
                    var currentProcessX64 = Environment.Is64BitProcess;
                    var hMods = new IntPtr[1024];
                    var uiSize = (uint)(Marshal.SizeOf(typeof(IntPtr)) * (hMods.Length));
                    var winDir = UEnvironment.GetWinDir();
                    var winSysFolderX86 = UIo.Path.Combine(winDir, "System32");
                    var winSysFolderX64 = UIo.Path.Combine(winDir, "SysWOW64");
                    if (!EnumProcessModulesEx(h, hMods, uiSize, out var needed, 0x03)) // 0x03 = List Modules ALl
                        return;
                    var totalModules = (int)(needed / (Marshal.SizeOf(typeof(IntPtr))));
                    for (var i = 0; i < totalModules; i++)
                    {
                        var sb = new StringBuilder(1024);
                        GetModuleFileNameExW(h, hMods[i], sb, sb.Capacity);
                        var f = sb.ToString();
                        var loaded = SymLoadModuleExW(h, IntPtr.Zero, f, null, 0, 0, IntPtr.Zero, 0);
                        if (loaded != 0)
                            continue;
                        if (!processX86 || !currentProcessX64)
                            continue;
                        var isWindowsDll = AStringUtils.Pos(winDir, f, true) != -1;
                        var isWindowsDllX86 = isWindowsDll && AStringUtils.Pos(winSysFolderX86, f, true) != -1;
                        if (!isWindowsDllX86)
                            continue;
                        f = f.Replace(winSysFolderX86, winSysFolderX64);
                        SymLoadModuleExW(h, IntPtr.Zero, f, null, 0, 0, IntPtr.Zero, 0);
                    }
                }
            }
            #endregion
        }
        #endregion
        #endregion
        #region Static Variables
        private static readonly Object SymbolLock = new Object();
        #endregion
        #region Variables
        public AProcessSharp Process;
        public AArrayManager<ASymbol> SymbolList;
        public AArrayManager<AUserDefinedSymbol> UserDefinedSymbols;
        private AModuleInfoArray _moduleList;
        private ASymbolLoaderThread _symbolLoaderThread;
        #endregion
        #region Constructor
        public ASymbolHandler()
        {
            Process = null;
            _symbolLoaderThread = null;
            SymbolList = new AArrayManager<ASymbol>();
            UserDefinedSymbols = new AArrayManager<AUserDefinedSymbol>();
            _moduleList = new AModuleInfoArray();
    }
        #endregion
        #region Tokenize
        public void Tokenize(String s, AStringArray tokens)
        {
            String t;
            var last = 0;
            var inQuote = false;
            for (var i = 0; i < s.Length; i++)
            {
                if (!AArrayUtils.InArray(s[i], '"', '[', ']', '+', '-', '*'))
                    continue;
                if (s[i] == '"')
                {
                    if (!inQuote)
                        last = i + 1;

                    inQuote = !inQuote;
                }
                if (inQuote)
                    continue;
                t = AStringUtils.Copy(s, last, i - last).Trim();
                if (t != "")
                {
                    tokens.SetLength(tokens.Length + 1);
                    tokens.Last = t;
                }
                //store separator char as well, unless it's "
                if (s[i] != '"')
                {
                    tokens.SetLength(tokens.Length + 1);
                    tokens.Last = s[i].ToString();
                }
                last = i + 1;
            }
            //last part
            t = AStringUtils.Copy(s, last, s.Length).Trim();
            if (t == "")
                return;
            tokens.SetLength(tokens.Length + 1);
            tokens.Last = t;
        }
        #endregion
        #region ParseAsPointer
        public Boolean ParseAsPointer(String s, AStringArray list)
        {
            //parse the string
            var currentLevel = 0;
            var prolog = true;
            var temps = "";
            var isPointer = false;
            for (var i = 0; i < s.Length; i++)
            {
                if (s[i] == '[')
                {
                    if (prolog)
                    {
                        currentLevel += 1;
                        isPointer = true;
                    }
                    else
                        return false; //bracket open after the prolog is not allowed
                }
                else
                {
                    if (prolog)
                    {
                        if (!AArrayUtils.InArray(s[i], '\t', ' '))   //no space or tab
                            prolog = false;
                    }
                    if (!prolog)
                    {
                        //definition, currentLevel is set, now parse till last ] (currentLevel=0)
                        if (s[i] == ']')  //end of a level
                        {
                            currentLevel -= 1;
                            if (temps == "")
                                temps = "+0";
                            list.Add(temps);

                            temps = "";
                            if (currentLevel < 0)
                                return false;
                            continue;
                        }
                        temps += s[i];
                    }
                }
            }
            if (temps == "")
                temps = "+0";
            if ((isPointer) && (temps != ""))
                list.Add(temps);
            if (currentLevel > 0)
                return false;
            return isPointer;
        }
        #endregion
        #region GetAddressFromSymbol
        public UIntPtr GetAddressFromSymbol(String s)
        {
            for (var i = 0; i < SymbolList.Length; i++)
            {
                if (SymbolList[i].IsMatch(s))
                    return SymbolList[i].Address;
            }
            return UIntPtr.Zero;
        }
        #endregion
        #region GetAddressFromName
        public UIntPtr GetAddressFromName(String name)
        {
            return GetAddressFromName(name, true);
        }
        public UIntPtr GetAddressFromName(String name, Boolean waitForSymbols)
        {
            var result = GetAddressFromName(name, waitForSymbols, out var x);
            if (x)
                throw new Exception(UStringUtils.Sprintf("Failure determining what %s means", name));
            return result;
        }
        public UIntPtr GetAddressFromName(String name, Boolean waitForSymbols, out Boolean hasError)
        {
            var result = UIntPtr.Zero;
            const int calcAddition = 0;
            const int calcSubstraction = 1;
            int offset;
            int i, j;
            Boolean error;
            var tokens = new AStringArray();
            string mathstring;
            var hasMultiplication = false;
            int nextoperation = 0;
            int regnr;
            name = name.Trim();
            var hasPointer = false;
            hasError = false;
            offset = 0;
            AStringUtils.Val("0x" + name, out result, out i);
            if (i == 0)
                return result; //it's a valid hexadecimal string
            if (AStringUtils.Copy(name, 1, 2).ToLower() == "0x")
            {
                AStringUtils.Val(name, out result, out i);
                if (i == 0)
                    return result;
            }
            //not a hexadecimal string
            Tokenize(name, tokens);
            //first check the most basic thing
            if (tokens.Length == 0)
            {
                hasError = true;
                return result;
            }
            /*--if it starts with a * or ends with *, - or +, then it's a bad formula--*/
            if (tokens[0][0] == '*' || AArrayUtils.InArray(tokens[tokens.Length - 1][0], '*', '+', '-'))
            {
                hasError = true;
                return result;
            }
            /*----convert the tokens into hexadecimal values--------*/
            lock (SymbolLock)
            {
                //try {tokens format ex.:=array('islandtribe.exe','+','AB754')}
                for (i = 0; i < tokens.Length; i++)
                {
                    if (!AArrayUtils.InArray(tokens[i][0], '[', ']', '+', '-', '*'))
                    {
                        AStringUtils.Val("0x" + tokens[i], out result, out j);
                        if (j <= 0)
                            continue; // hexadecimal value 
                                      //not a hexadecimal value
                        var mi = Process.ModuleFactory.FetchModule(tokens[i]);
                        if (mi != null)
                        {
                            tokens[i] = AStringUtils.IntToHex(mi.BaseAddress.ToUIntPtr(), 8);
                            continue;
                        }
                        var mib = _moduleList.FindBaseAddress(tokens[i]);
                        if (mib != IntPtr.Zero)
                        {
                            tokens[i] = AStringUtils.IntToHex(mib.ToUIntPtr(), 8);
                            continue;
                        }
                        regnr = -1;// getreg(tokens[i].ToUpper(), false); // todo fill this in?
                        if (regnr != -1)
                        {
                            #region NO CONTEXT
                            // //if (context<>nil) and (context^.{$ifdef cpu64}Rip{$else}Eip{$endif}<>0) then
                            // if (context != nil)
                            // {
                            //     //get the register value, and because this is an address specifier, use the full 32-bits
                            //     switch (regnr)
                            //     {
                            //         // 0: tokens[i]:=inttohex(context^.{$ifdef cpu64}rax{$else}eax{$endif},8);
                            //         // 1: tokens[i]:=inttohex(context^.{$ifdef cpu64}rcx{$else}ecx{$endif},8);
                            //         // 2: tokens[i]:=inttohex(context^.{$ifdef cpu64}rdx{$else}edx{$endif},8);
                            //         // 3: tokens[i]:=inttohex(context^.{$ifdef cpu64}rbx{$else}ebx{$endif},8);
                            //         // 4: tokens[i]:=inttohex(context^.{$ifdef cpu64}rsp{$else}esp{$endif},8);
                            //         // 5: tokens[i]:=inttohex(context^.{$ifdef cpu64}rbp{$else}ebp{$endif},8);
                            //         // 6: tokens[i]:=inttohex(context^.{$ifdef cpu64}rsi{$else}esi{$endif},8);
                            //         // 7: tokens[i]:=inttohex(context^.{$ifdef cpu64}rdi{$else}edi{$endif},8);
                            //         case 0: tokens[i] = inttohex(context->eax, 8); break;
                            //         case 1: tokens[i] = inttohex(context->ecx, 8); break;
                            //         case 2: tokens[i] = inttohex(context->edx, 8); break;
                            //         case 3: tokens[i] = inttohex(context->ebx, 8); break;
                            //         case 4: tokens[i] = inttohex(context->esp, 8); break;
                            //         case 5: tokens[i] = inttohex(context->ebp, 8); break;
                            //         case 6: tokens[i] = inttohex(context->esi, 8); break;
                            //         case 7: tokens[i] = inttohex(context->edi, 8); break;
                            //         //{$ifdef cpu64}
                            //         case 8: tokens[i] = inttohex(context->r8, 8); break;
                            //         case 9: tokens[i] = inttohex(context->r9, 8); break;
                            //         case 10: tokens[i] = inttohex(context->r10, 8); break;
                            //         case 11: tokens[i] = inttohex(context->r11, 8); break;
                            //         case 12: tokens[i] = inttohex(context->r12, 8); break;
                            //         case 13: tokens[i] = inttohex(context->r13, 8); break;
                            //         case 14: tokens[i] = inttohex(context->r14, 8); break;
                            //         case 15: tokens[i] = inttohex(context->r15, 8); break;
                            //             //{$endif}
                            //     }
                            // 
                            //     continue_; //handled
                            // }
                            // //not handled, but since it's a register, quit now
                            #endregion
                        }
                        else
                        {
                            /*-----------------user defined symbol-------------------*/
                            result = GetUserDefinedSymbolByName(tokens[i]);
                            if (result != UIntPtr.Zero)
                            {
                                tokens[i] = AStringUtils.IntToHex(result, 8);
                                continue;
                            }
                            /*----------------Process Module Symbol----------------*/
                            if (waitForSymbols)
                                WaitForSymbolsLoaded();
                            result = GetAddressFromSymbol(tokens[i]);
                            if (result != UIntPtr.Zero)
                            {
                                tokens[i] = AStringUtils.IntToHex(result, 8);
                                continue;
                            }
                        }
                        //not a register or symbol
                        //One last attempt to fix it, check if it is the last symbol, if not add it. (perhaps the symbol got split into tokens)
                        if (i < tokens.Length - 1)
                        {
                            tokens[i + 1] = tokens[i] + tokens[i + 1]; //(if not, it will error out eventually anyhow)
                            tokens[i] = ""; //empty
                        }
                        else
                        {
                            hasError = true;
                            return result;
                        }
                    }
                    else
                    { //it's not a real token
                        switch (tokens[i][0])
                        {
                            case '*':
                                hasMultiplication = true;
                                break;
                            case '[':
                            case ']':
                                hasPointer = true;
                                break;
                        }
                    }
                }
            }
            mathstring = "";
            for (i = 0; i < tokens.Length; i++)
                mathstring += tokens[i];
            if (hasPointer)
            {
                result = GetAddressFromPointer(mathstring, out error);
                if (!error)
                {
                    result += offset;
                    return result;
                }
                //it has a pointer notation but the pointer didn't get handled... ERROR!
                hasError = true;
                return result;
            }
            //handle the math string
            if (hasMultiplication)
            {
                //first do the multiplications
                for (i = 0; i < tokens.Length; i++)
                {
                    if (tokens[i] == "*")
                    {
                        //multiply the left and right
                        tokens[i - 1] = AStringUtils.IntToHex(AStringUtils.StrToQWordEx("0x" + tokens[i - 1]) * AStringUtils.StrToQWord("0x" + tokens[i + 1]), 8);
                        tokens[i] = "";
                        tokens[i + 1] = "";
                    }
                }
            }
            result = UIntPtr.Zero;
            //handle addition and subtraction
            nextoperation = calcAddition;
            for (i = 0; i < tokens.Length; i++)
            {
                if (tokens[i].Length > 0)
                {
                    switch (tokens[i][0])
                    {
                        case '+':
                            nextoperation = calcAddition;
                            break;
                        case '-':
                            {
                                if (nextoperation == calcSubstraction)
                                    nextoperation = calcAddition;
                                else //--=+
                                    nextoperation = calcSubstraction;
                            }
                            break;
                        default: //else of case;
                            {//do the calculation
                                switch (nextoperation)
                                {
                                    case calcAddition:
                                        result = (UIntPtr)(result.ToUInt64() + AStringUtils.StrToQWordEx("0x" + tokens[i]));
                                        break;
                                    case calcSubstraction:
                                        result = (UIntPtr)(result.ToUInt64() + AStringUtils.StrToQWordEx("0x" + tokens[i]));
                                        break;
                                }
                            }
                            break;
                    }
                } //end of if length(tokens[i])...
            }
            return result;
        }
        #endregion
        #region GetAddressFromPointer
        public UIntPtr GetAddressFromPointer(String s, out Boolean error)
        {
            error = true;
            var offsets = new AArrayManager<Int32>();
            var list = new AStringArray();
            if (!ParseAsPointer(s, list))
                return UIntPtr.Zero;
            UIntPtr baseAddress;
            try
            {
                baseAddress = GetAddressFromName(list[0]);
            }
            catch
            {
                return UIntPtr.Zero;
            }
            offsets.SetLength(list.Length - 1);
            for (var i = 1; i < list.Length; i++) //start from the first offset
            {
                var off = AStringUtils.Copy(list[i], 1, list[i].Length);
                try
                {
                    offsets[i - 1] = (int)AStringUtils.StrToQWordEx("0x" + off);
                }
                catch
                {
                    return UIntPtr.Zero;
                }
                if (list[i][1] == '-')
                    offsets[i - 1] = -offsets[i - 1];
            }
            //still here so notation was correct and baseAddress+offsets are filled in
            //now read
            var realAddress2 = baseAddress;
            for (var i = 0; i < offsets.Length; i++)
            {
                var realAddress = Process.Memory.Read<UIntPtr>(realAddress2.ToIntPtr());
                if (realAddress != UIntPtr.Zero)
                    realAddress2 = realAddress + offsets[i];
                else
                    return UIntPtr.Zero;
            }
            error = false;
            return realAddress2;
        }
        #endregion
        #region WaitForSymbolsLoaded
        public void WaitForSymbolsLoaded()
        {
            var needReload = true;
            var oldModuleList = _moduleList;
            _moduleList = ASymbolLoaderThread.GetModuleList(Process.Native.Id);
            if (oldModuleList.Length > 0 && oldModuleList.Length == _moduleList.Length)
            {
                needReload = false;
                for (var i = 0; i < oldModuleList.Length; i++)
                {
                    var nameChanged = oldModuleList[i].ModuleName != _moduleList[i].ModuleName;
                    var addressChanged = oldModuleList[i].BaseAddress != _moduleList[i].BaseAddress;
                    needReload = nameChanged || addressChanged;
                    if (needReload)
                        break;
                }
            }
            if (needReload | SymbolList.Length == 0)
            {
                lock (SymbolLock)
                {
                    _symbolLoaderThread = new ASymbolLoaderThread();
                    _symbolLoaderThread.Process = Process;
                    _symbolLoaderThread.Execute();
                    while (_symbolLoaderThread.IsLoading)
                        Thread.Sleep(10);
                    SymbolList.Assign(_symbolLoaderThread.SymbolList.TakeAll());
                }
            }
        }
        #endregion
        #region AddUserDefinedSymbol
        public void AddUserDefinedSymbol(String addressString, String name)
        {
            if (UserDefinedSymbols.Contains(c => c.IsMatch(name)))
                throw new Exception(name + " " + "already exists");
            var address = GetAddressFromName(addressString);
            if (address == UIntPtr.Zero)
                throw new Exception("You can''t add a symbol with a zero address");
            UserDefinedSymbols.SetLength(UserDefinedSymbols.Length + 1);
            UserDefinedSymbols.Last.Address = address;
            UserDefinedSymbols.Last.AddressString = addressString;
            UserDefinedSymbols.Last.Name = name;
            UserDefinedSymbols.Last.AllocSize = 0;
            UserDefinedSymbols.Last.ProcessId = 0;
        }
        #endregion
        #region DeleteUserDefinedSymbol
        public Boolean DeleteUserDefinedSymbol(String name)
        {
            var k = UserDefinedSymbols.Length;
            for (var i = 0; i < k; i++)
            {
                if (!UserDefinedSymbols[i].IsMatch(name))
                    continue;
                if ((UserDefinedSymbols[i].AllocSize > 0) && (UserDefinedSymbols[i].ProcessId == Process.Native.Id))
                {
                    if (UserDefinedSymbols[i].Address != UIntPtr.Zero)
                        AMemoryHelper.Free(Process.Handle, UserDefinedSymbols[i].Address.ToIntPtr());
                }
                //now move up all the others and decrease the list
                for (var j = i; j < k && j + 1 < k; j++)
                    UserDefinedSymbols[j] = UserDefinedSymbols[j + 1];
                UserDefinedSymbols.SetLength(k - 1);
                return true;
            }
            return false;
        }
        #endregion
        #region GetUserDefinedSymbolByName
        public UIntPtr GetUserDefinedSymbolByName(String symbolName)
        {
            for (var i = 0; i < UserDefinedSymbols.Length; i++)
            {
                if (UserDefinedSymbols[i].IsMatch(symbolName))
                    return UserDefinedSymbols[i].Address;
            }
            return UIntPtr.Zero;
        }
        #endregion
        #region GetUserDefinedSymbolByAddress
        public String GetUserDefinedSymbolByAddress(UIntPtr address)
        {
            for (var i = 0; i < UserDefinedSymbols.Length; i++)
            {
                if (UserDefinedSymbols[i].Address == address)
                    return UserDefinedSymbols[i].Name;
            }
            return "";
        }
        #endregion
        #region SetUserDefinedSymbolAllocSize
        public void SetUserDefinedSymbolAllocSize(String name, UInt32 size)
        {
            const String PREV_DEC = "The symbol named %s was previously declared with a size of %s instead of %s." +
                                    " all scripts that use this memory must give the same size. " +
                                    "Adjust the size, or delete the old alloc from the userdefined symbol list";
            if (size == 0)
                throw new Exception("Please provide a bigger size");
            UIntPtr p;
            int i;
            for (i = 0; i < UserDefinedSymbols.Length; i++)
            {
                if (!UserDefinedSymbols[i].IsMatch(name))
                    continue; //it exists, check first
                if (UserDefinedSymbols[i].AllocSize > 0 && UserDefinedSymbols[i].ProcessId == Process.Native.Id)
                {
                    if (size != UserDefinedSymbols[i].AllocSize)
                        throw new Exception(UStringUtils.Sprintf(PREV_DEC, UserDefinedSymbols[i].Name, UserDefinedSymbols[i].AllocSize, size));
                }
                if (UserDefinedSymbols[i].ProcessId != Process.Native.Id)
                {
                    p = AMemoryHelper.Allocate(Process.Handle, (int)size).ToUIntPtr();
                    if (p == UIntPtr.Zero)
                        throw new Exception("Error allocating memory");
                    UserDefinedSymbols[i].Address = p;
                    UserDefinedSymbols[i].AddressString = AStringUtils.IntToHex(p, 8);
                    UserDefinedSymbols[i].AllocSize = size;
                    UserDefinedSymbols[i].ProcessId = Process.Native.Id;
                }
                return; // Redefined the symbol and exit;
            }
            //Still here, symbol Not exists, let's define a new one.
            p = AMemoryHelper.Allocate(Process.Handle, (int)size).ToUIntPtr();
            if (p == UIntPtr.Zero)
                throw new Exception("Error allocating memory");
            AddUserDefinedSymbol(AStringUtils.IntToHex(p, 8), name);
            UserDefinedSymbols[i].AllocSize = size;
            UserDefinedSymbols[i].ProcessId = Process.Native.Id;
        }
        #endregion
    }
}
