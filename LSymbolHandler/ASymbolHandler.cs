using System;
using Sputnik.LUtils;
using SputnikAsm.LAssembler;
using SputnikAsm.LCollections;
using SputnikAsm.LGenerics;
using SputnikAsm.LUtils;
using SputnikWin.LExtra.LMemorySharp.Native;
using SputnikWin.LFileSystem;

namespace SputnikAsm.LSymbolHandler
{
    public class ASymbolHandler
    {
        #region Variables
        public AProcess Process = new AProcess();
        public AArrayManager<ASymbol> SymbolList;
        public AArrayManager<AUserDefinedSymbol> UserDefinedSymbols;
        #endregion
        #region Constructor
        public ASymbolHandler()
        {
            SymbolList = new AArrayManager<ASymbol>();
            UserDefinedSymbols = new AArrayManager<AUserDefinedSymbol>();
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
        #region BeginRead
        private void BeginRead()
        {
        }
        #endregion
        #region EndRead
        private void EndRead()
        {
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
            BeginRead();
            //try {tokens format ex.:=array('islandtribe.exe','+','AB754')}
            for (i = 0; i < tokens.Length; i++)
            {
                if (!AArrayUtils.InArray(tokens[i][0], '[', ']', '+', '-', '*'))
                {
                    AStringUtils.Val("0x" + tokens[i], out result, out j);
                    if (j <= 0)
                        continue; // hexadecimal value 
                    //not a hexadecimal value
                    var mi = Process.GetModule(tokens[i]);
                    if (mi != null)
                    {
                        tokens[i] = AStringUtils.IntToHex((UIntPtr)mi.BaseAddress.ToInt64(), 8);
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
                        case '[': case ']':
                            hasPointer = true;
                            break;
                    }
                }
            }
            EndRead();
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
            var offsets = new AArrayManager<int>();
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
                var realAddress = (UIntPtr)Process.ReadMem((IntPtr)realAddress2.ToUInt64(), ReadType.UIntPtr);
                if (realAddress != UIntPtr.Zero)
                    realAddress2 = realAddress + offsets[i];
                else
                    return UIntPtr.Zero;
            }
            error = false;
            return realAddress2;
        }
        #endregion
        public void WaitForSymbolsLoaded()
        {
            // todo load all necessary symbols APIs etc
        }
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
            UserDefinedSymbols.Last.ProcessId = IntPtr.Zero;
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
                if ((UserDefinedSymbols[i].AllocSize > 0) && (UserDefinedSymbols[i].ProcessId == Process.GetId()))
                {
                    if (UserDefinedSymbols[i].Address != UIntPtr.Zero)
                        Process.Free((IntPtr)UserDefinedSymbols[i].Address.ToUInt64());
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
                if (UserDefinedSymbols[i].AllocSize > 0 && UserDefinedSymbols[i].ProcessId == Process.GetId())
                {
                    if (size != UserDefinedSymbols[i].AllocSize)
                        throw new Exception(UStringUtils.Sprintf(PREV_DEC, UserDefinedSymbols[i].Name, UserDefinedSymbols[i].AllocSize, size));
                }
                if (UserDefinedSymbols[i].ProcessId != Process.GetId())
                {
                    p = (UIntPtr)Process.Alloc((int)size, MemoryProtectionFlags.ExecuteReadWrite, MemoryAllocationFlags.Commit).ToInt64();
                    if (p == UIntPtr.Zero)
                        throw new Exception("Error allocating memory");
                    UserDefinedSymbols[i].Address = p;
                    UserDefinedSymbols[i].AddressString = AStringUtils.IntToHex(p, 8);
                    UserDefinedSymbols[i].AllocSize = size;
                    UserDefinedSymbols[i].ProcessId = Process.GetId();
                }
                return; // Redefined the symbol and exit;
            }
            //Still here, symbol Not exists, let's define a new one.
            p = (UIntPtr)Process.Alloc((int)size, MemoryProtectionFlags.ExecuteReadWrite, MemoryAllocationFlags.Commit).ToInt64();
            if (p == UIntPtr.Zero)
                throw new Exception("Error allocating memory");
            AddUserDefinedSymbol(AStringUtils.IntToHex(p, 8), name);
            UserDefinedSymbols[i].AllocSize = size;
            UserDefinedSymbols[i].ProcessId = Process.GetId();
        }
        #endregion
    }
}
