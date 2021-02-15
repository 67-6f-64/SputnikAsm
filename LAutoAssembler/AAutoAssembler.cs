using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sputnik.LFileSystem;
using Sputnik.LString;
using Sputnik.LUtils;
using SputnikAsm.LAssembler;
using SputnikAsm.LAssembler.LEnums;
using SputnikAsm.LAutoAssembler.LCollections;
using SputnikAsm.LCollections;
using SputnikAsm.LExtensions;
using SputnikAsm.LGenerics;
using SputnikAsm.LSymbolHandler;
using SputnikAsm.LUtils;
using SputnikWin.LExtra.LMemorySharp.Native;

namespace SputnikAsm.LAutoAssembler
{
    public class AAutoAssembler
    {
        #region Internal Classes
        #region ALabels
        internal class ALabels
        {
            #region Variables
            public String Name;
            public Boolean Defined;
            public Boolean InsideAllocatedMemory;
            public UIntPtr Address;
            public int AssemblerLine;
            public AByteArray References; //index of assembled array
            public AByteArray References2; //index of assemblerlines array
            #endregion
            #region Constructor
            public ALabels()
            {
                Name = "";
                Defined = false;
                Address = UIntPtr.Zero;
                AssemblerLine = 0;
                References = new AByteArray();
                References2 = new AByteArray();
            }
            #endregion
        }
        #endregion
        #region AAssembled
        internal class AAssembled
        {
            #region Variables
            public UIntPtr Address;
            public AByteArray Bytes;
            #endregion
            #region Constructor
            public AAssembled()
            {
                Address = UIntPtr.Zero;
                Bytes = new AByteArray();
            }
            public AAssembled(UIntPtr address, AByteArray bytes)
            {
                Address = address;
                Bytes = bytes;
            }
            #endregion
        }
        #endregion
        #region ALoadLibrary
        internal class ALoadLibrary
        {
            #region Variables
            public String Address;
            public String FileName;
            #endregion
            #region Constructor
            public ALoadLibrary()
            {
                Address = "";
                FileName = "";
            }
            public ALoadLibrary(String address, String fileName)
            {
                Address = address;
                FileName = fileName;
            }
            #endregion
        }
        #endregion
        #region ADefines
        internal class ADefines
        {
            #region Variables
            public String Name;
            public String Whatever;
            #endregion
            #region Constructor
            public ADefines()
            {
                Name = "";
                Whatever = "";
            }
            public ADefines(String name, String whatever)
            {
                Name = name;
                Whatever = whatever;
            }
            #endregion
        }
        #endregion
        #region AFullAccess
        internal class AFullAccess
        {
            #region Variables
            public UIntPtr Address;
            public UInt32 Size;
            #endregion
            #region Constructor
            public AFullAccess()
            {
                Address = UIntPtr.Zero;
                Size = 0;
            }
            public AFullAccess(UIntPtr address, UInt32 size)
            {
                Address = address;
                Size = size;
            }
            #endregion
        }
        #endregion
        #endregion
        #region Static Variables
        public String rsForwardJumpWithNoLabelDefined = "Forward jump with no label defined";
        public String rsThereIsCodeDefinedWithoutSpecifyingTheAddressItBel = "There is code defined without specifying the address it belongs to";
        public String rsIsNotAvalidByteString = "%s is not a valid bytestring";
        public String rsTheBytesAtAreNotWhatWasExpected = "The bytes at %s are not what was expected";
        public String rsTheMemoryAtCanNotBeRead = "The memory at +%s can not be read";
        public String rsWrongSyntaxASSERTAddress1122335566 = "Wrong syntax. ASSERT(address,11 22 33 ** 55 66)";
        public String rsIsNotAValidSize = "%s is not a valid size";
        public String rsWrongSyntaxGLOBALALLOCNameSize = "Wrong syntax. GLOBALALLOC(name,size)";
        public String rsCouldNotBeFound = "%s could not be found";
        public String rsWrongSyntaxIncludeFilenameCea = "Wrong syntax. Include(filename.cea)";
        public String rsWrongSyntaxCreateThreadAddress = "Wrong syntax. CreateThread(address)";
        public String rsCouldNotBeInjected = "%s could not be injected";
        public String rsWrongSyntaxLoadLibraryFilename = "Wrong syntax. LoadLibrary(filename)";
        public String rsWrongSyntaxLuaCall = "Wrong Syntax. LuaCall(luacommand)";
        public String rsInvalidAddressForReadMem = "Invalid address for ReadMem";
        public String rsInvalidSizeForReadMem = "Invalid size for ReadMem";
        public String rsTheMemoryAtCouldNotBeFullyRead = "The memory at %s could not be fully read";
        public String rsWrongSyntaxReadMemAddressSize = "Wrong syntax. ReadMem(address,size)";
        public String rsTheFileDoesNotExist = "The file %s does not exist";
        public String rsWrongSyntaxLoadBinaryAddressFilename = "Wrong syntax. LoadBinary(address,filename)";
        public String rsSyntaxError = "Syntax error";
        public String rsTheArrayOfByteCouldNotBeFound = "The array of byte '%s' could not be found";
        public String rsWrongSyntaxAOBSCANName11223355 = "Wrong syntax. AOBSCAN(name,11 22 33 ** 55)";
        public String rsWrongSyntaxAOBSCANMODULEName11223355 = "Wrong syntax. AOBSCANMODULE(name, module, 11 22 33 ** 55)";
        public String rsDefineAlreadyDefined = "Define %s already defined";
        public String rsWrongSyntaxDEFINENameWhatever = "Wrong syntax. DEFINE(name,whatever)";
        public String rsSyntaxErrorFullAccessAddressSize = "Syntax error. FullAccess(address,size)";
        public String rsIsNotAValidIdentifier = "%s is not a valid identifier";
        public String rsIsBeingRedeclared = "%s is being redeclared";
        public String rsLabelIsBeingDefinedMoreThanOnce = "label %s is being defined more than once";
        public String rsLabelIsNotDefinedInTheScript = "label %s is not defined in the script";
        public String rsTheIdentifierHasAlreadyBeenDeclared = "The identifier %s has already been declared";
        public String rsWrongSyntaxALLOCIdentifierSizeinbytes = "Wrong syntax. ALLOC(identifier,sizeinbytes)";
        public String rsNeedToUseKernelmodeReadWriteprocessmemory = "You need to use kernelmode read/writeprocessmemory if you want to use KALLOC";
        public String rsSorryButWithoutTheDriverKALLOCWillNotFunction = "Sorry, but without the driver KALLOC will not function";
        public String rsWrongSyntaxKallocIdentifierSizeinbytes = "Wrong syntax. kalloc(identifier,sizeinbytes)";
        public String rsThisAddressSpecifierIsNotValid = "This address specifier is not valid";
        public String rsThisInstructionCanTBeCompiled = "This instruction can't be compiled";
        public String rsErrorInLine = "Error in line %s (%s) :%s";
        public String rsWasSupposedToBeAddedToTheSymbollistButItIsnTDeclar = "%s was supposed to be added to the symbollist, but it isn't declared";
        public String rsTheAddressInCreatethreadIsNotValid = "The address in createthread(%s) is not valid";
        public String rsTheAddressInLoadbinaryIsNotValid = "The address in loadbinary(%s,%s) is not valid";
        public String rsThisCodeCanBeInjectedAreYouSure = "This code can be injected. Are you sure?";
        public String rsFailureToAllocateMemory = "Failure to allocate memory";
        public String rsNotAllInstructionsCouldBeInjected = "Not all instructions could be injected";
        public String rsTheFollowingKernelAddressesWhereAllocated = "The following kernel addresses where allocated";
        public String rsTheCodeInjectionWasSuccessfull = "The code injection was successfull";
        public String rsYouCanOnlyHaveOneEnableSection = "You can only have one enable section";
        public String rsYouCanOnlyHaveOneDisableSection = "You can only have one disable section";
        public String rsYouHavnTSpecifiedAEnableSection = "You have not specified a enable section";
        public String rsYouHavnTSpecifiedADisableSection = "You have not specified a disable section";
        public String rsWrongSyntaxSHAREDALLOCNameSize = "Wrong syntax. SHAREDALLOC(name,size)";
        public String rsInvalidInteger = "Invalid integer";
        #endregion
        #region Variables
        public AAssembler Assembler;
        #endregion
        #region Constructor
        public AAutoAssembler(AAssembler assembler)
        {
            Assembler = assembler;
        }
        #endregion
        #region RemoveComments
        public void RemoveComments(ARefStringArray code)
        {
            var inString = false;
            var inComment = false;
            for (var i = 0; i < code.Length; i++)
            {
                var currentLine = code[i].Value;
                using (var p = new UCharPtr(currentLine))
                {
                    for (var j = 0; j < currentLine.Length; j++)
                    {
                        if (inComment)
                        {
                            //inside a comment, remove everything till a } is encountered
                            if ((p[j] == '}') || ((p[j] == '*') && (j < p.Size) && (p[j + 1] == '/')))
                            {
                                inComment = false; //and continue parsing the code...
                                if ((p[j] == '*') && (j < currentLine.Length) && (p[j + 1] == '/'))
                                    p[j + 1] = ' ';
                            }
                            p[j] = ' ';
                        }
                        else
                        {
                            if (p[j] == '\'')
                                inString = !inString;
                            if (p[j] == '\t')
                                p[j] = ' '; //tabs are basicly comments
                            if (inString)
                                continue;
                            //not inside a string, so comment markers need to be dealt with
                            if ((p[j] == '/') && (j < p.Size) && (p[j + 1] == '/'))  //- comment (only the rest of the line)
                            {
                                //cut off till the end of the line (and might as well jump out now)
                                currentLine = AStringUtils.Copy(currentLine, 0, j);
                                break;
                            }
                            if ((p[j] == '{') || ((p[j] == '/') && (j < p.Size) && (p[j + 1] == '*')))
                            {
                                inComment = true;
                                p[j] = ' '; //replace from here till the first } with spaces, this goes on for multiple lines
                            }
                        }
                    }
                }
                code[i].Value = currentLine.Trim();
            }
        }
        #endregion
        #region UnlabeledLabels
        public void UnlabeledLabels(ARefStringArray code)
        {
            //unlabeled label support
            //For those reading the source, PLEASE , try not to code scripts like that
            //the scripts you make look like crap, and are hard to read. (like using goto in a c app)
            //this is just to make one guy happy
            var labels = new AStringArray();
            var i = 0;
            while (i < code.Length)
            {
                var currentLine = code[i].Value;
                if (currentLine.Length > 1)
                {
                    if (currentLine == "@@:")
                    {
                        currentLine = "RandomLabel" +
                                      (Char) ('A' + UMathUtils.RandomByte(26)) +
                                      (Char) ('A' + UMathUtils.RandomByte(26)) +
                                      (Char) ('A' + UMathUtils.RandomByte(26)) +
                                      (Char) ('A' + UMathUtils.RandomByte(26)) +
                                      (Char) ('A' + UMathUtils.RandomByte(26)) +
                                      (Char) ('A' + UMathUtils.RandomByte(26)) +
                                      (Char) ('A' + UMathUtils.RandomByte(26)) +
                                      (Char) ('A' + UMathUtils.RandomByte(26)) +
                                      (Char) ('A' + UMathUtils.RandomByte(26)) +
                                      ':';
                        code[i].Value = currentLine;
                        code.Insert(0, "label(" + AStringUtils.Copy(currentLine, 0, currentLine.Length - 1) + ')');
                        i += 1;
                        labels.SetLength(labels.Length + 1);
                        labels.Last = AStringUtils.Copy(currentLine, 0, currentLine.Length - 1);
                    }
                    else if (currentLine[currentLine.Length - 1] == ':')
                    {
                        labels.SetLength(labels.Length + 1);
                        labels.Last = AStringUtils.Copy(currentLine, 0, currentLine.Length - 1);
                    }
                }
                i += 1;
            }
            //all label definitions have been filled in
            //now change @F (forward) and @B (back) to the labels in front and behind
            var lastSeenLabel = -1;
            for (i = 0; i < code.Length; i++)
            {
                var currentLine = code[i].Value;
                if (currentLine.Length > 1)
                {
                    if (currentLine[currentLine.Length - 1] == ':')
                    {
                        //find this in the array
                        currentLine = AStringUtils.Copy(currentLine, 0, currentLine.Length - 1);
                        for (var j = (lastSeenLabel + 1); j < labels.Length; j++)  //lastseenlabel+1 since it is ordered in definition
                        {
                            if (String.Equals(currentLine, labels[j], StringComparison.CurrentCultureIgnoreCase))
                            {
                                lastSeenLabel = j;
                                break;
                            }
                        }
                        currentLine += ':';
                        //lastseenlabel is now updated to the current pos
                    }
                    else if (AStringUtils.Pos("@f", currentLine.ToLower()) != -1)   //forward
                    {
                        //forward label, so labels[lastseenlabel+1]
                        if (lastSeenLabel + 1 >= labels.Length)
                            throw new Exception(rsForwardJumpWithNoLabelDefined);
                        currentLine = ReplaceToken(currentLine, "@f", labels[lastSeenLabel + 1]);
                        currentLine = ReplaceToken(currentLine, "@F", labels[lastSeenLabel + 1]);
                    }
                    else if (AStringUtils.Pos("@b", currentLine.ToLower()) != -1)  //back
                    {
                        //forward label, so labels[lastseenlabel]
                        if (lastSeenLabel == -1)
                            throw new Exception(rsThereIsCodeDefinedWithoutSpecifyingTheAddressItBel);
                        currentLine = ReplaceToken(currentLine, "@b", labels[lastSeenLabel]);
                        currentLine = ReplaceToken(currentLine, "@B", labels[lastSeenLabel]);
                    }
                }
                code[i].Value = currentLine;
            }
        }
        #endregion
        #region GetEnableAndDisablePos
        public Boolean GetEnableAndDisablePos(ARefStringArray code, out int enablePos, out int disablePos)
        {
            var result = false;
            enablePos = -1;
            disablePos = -1;
            for (var i = 0; i < code.Length; i++)
            {
                var currentLine = code[i].Value;
                var j = AStringUtils.Pos("//", currentLine);
                if (j != -1)
                    currentLine = AStringUtils.Copy(currentLine, 1, j - 1);
                currentLine = currentLine.Trim();
                if (currentLine.Length == 0)
                    continue;
                if (currentLine.StartsWith("//"))
                    continue; //skip
                switch (currentLine.ToUpper())
                {
                    case "[ENABLE]":
                    {
                        result = true; //there's at least a enable section, so it's ok
                        if (enablePos != -1)
                        {
                            enablePos = -2;
                            return true;
                        }
                        enablePos = i;
                        break;
                    }
                    case "[DISABLE]" when disablePos != -1:
                        disablePos = -2;
                        return result;
                    case "[DISABLE]":
                        disablePos = i;
                        break;
                }
            }
            return result;
        }
        #endregion
        #region GetScript
        public ARefStringArray GetScript(ARefStringArray code, Boolean enableScript)
        {
            var ret = new ARefStringArray();
            GetScript(code, ret, enableScript);
            return ret;
        }
        public void GetScript(ARefStringArray code, ARefStringArray newScript, Boolean enableScript)
        {
            var insideEnable = false;
            var insideDisable = false;
            for (var i = 0; i < code.Length; i++)
            {
                switch (code[i].Value.ToUpper())
                {
                    case "[ENABLE]":
                        insideEnable = true;
                        insideDisable = false;
                        continue;
                    case "[DISABLE]":
                        insideEnable = false;
                        insideDisable = true;
                        continue;
                }
                if (!insideEnable && !insideDisable || insideEnable && enableScript || insideDisable && !enableScript)
                {
                    if (String.IsNullOrEmpty(code[i].Value))
                        continue;
                    newScript.Add(code[i]);
                }
            }
        }
        #endregion
        #region Tokenize
        public void Tokenize(String input, ARefStringArray tokens)
        {
            tokens.Clear();
            var a = -1;
            for (var i = 0; i < input.Length; i++)
            {
                if (ACharUtils.InRange(input[i], 'a', 'z') ||
                    ACharUtils.InRange(input[i], 'A', 'Z') ||
                    ACharUtils.InRange(input[i], '0', '9') ||
                    input[i] == '.' ||
                    input[i] == '_' ||
                    input[i] == '#' ||
                    input[i] == '@'
                    )
                {
                    if (a == -1)
                        a = i;
                }
                else
                {
                    if (a != -1)
                        tokens.Add(AStringUtils.Copy(input, a, i - a), a);
                    a = -1;
                }
            }
            if (a != -1)
                tokens.Add(AStringUtils.Copy(input, a, input.Length), a);
        }
        #endregion
        #region TokenCheck
        public Boolean TokenCheck(String input, String token)
        {
            var tokens = new ARefStringArray();
            Tokenize(input, tokens);
            for (var i = 0; i < tokens.Length; i++)
            {
                if (tokens[i].Value == token)
                    return true;
            }
            return false;
        }
        #endregion
        #region ReplaceToken
        public String ReplaceToken(String input, String token, String replaceWith)
        {
            var tokens = new ARefStringArray();
            Tokenize(input, tokens);
            var result = input;
            for (var i = 0; i < tokens.Length; i++)
            {
                if (tokens[i].Value != token)
                    continue;
                var j = tokens[i].Position;
                result = AStringUtils.Copy(input, 0, j) + replaceWith + AStringUtils.Copy(input, j + token.Length, input.Length);
            }
            return result;
        }
        #endregion
        #region TokenizeStruct
        public void TokenizeStruct(String input, ARefStringArray tokens)
        {
            var delims = new ACharArray(' ');
            tokens.Clear();
            var count = AStringUtils.WordCount(input, delims);
            for (var i = 1; i <= count; i++)
                tokens.Add(AStringUtils.ExtractWord(i, input, delims));
        }
        #endregion
        #region ReplaceStructWithDefines
        public void ReplaceStructWithDefines(ARefStringArray code, int linenr)
        {
            int currentOffset;
            string structname;
            string elementname;
            int i, j, k;
            ARefStringArray tokens;
            ARefStringArray elements;
            int starttoken;
            var bytesize = 0;
            Boolean endfound;
            int lastlinenr;
            lastlinenr = linenr;
            endfound = false;
            structname = AStringUtils.Copy(code[linenr].Value, 7, code[linenr].Length).Trim();
            currentOffset = 0;
            tokens = new ARefStringArray();
            elements = new ARefStringArray();
            for (i = linenr + 1; i < code.Length; i++)
            {
                lastlinenr = i;
                TokenizeStruct(code[i].Value, tokens);
                j = 0;
                if (tokens.Length > 0 && !String.IsNullOrEmpty(tokens[0].Value))
                {
                    //first check if it's a label definition
                    if (tokens[0].Value[tokens[0].Length - 1] == ':')
                    {
                        elementname = AStringUtils.Copy(tokens[0].Value, 0, tokens[0].Length - 1);
                        if (Assembler.GetOpCodesIndex(elementname) != -1)
                            StructError(elementname + " is a reserved word", structname, lastlinenr + 1);
                        elements.Add(elementname, currentOffset);
                        j = 1;
                    }
                    //then check if it's the end of the struct
                    if (tokens[0].Value.ToUpper() == "ENDSTRUCT" || tokens[0].Value.ToUpper() == "ENDS")
                    {
                        endfound = true;
                        break; //all done
                    }
                }
                //if it's neither a label or structure end then it's a size defining token
                while (j < tokens.Length)
                {
                    tokens[j] = tokens[j];
                    switch (tokens[j].Value[0])
                    {
                        case 'R':
                            {
                                //could be res*
                                if ((tokens[j].Length == 4) && (AStringUtils.Copy(tokens[j].Value, 0, 3) == "RES"))
                                {
                                    switch (tokens[j].Value[3])
                                    {
                                        case 'B':
                                            bytesize = 1;
                                            break;
                                        case 'W':
                                            bytesize = 2;
                                            break;
                                        case 'D':
                                            bytesize = 4;
                                            break;
                                        case 'Q':
                                            bytesize = 8;
                                            break;
                                        default:
                                            StructError(null, structname, lastlinenr + 1);
                                            break;
                                    }
                                    //now get the count
                                    j += 1;
                                    if (j >= tokens.Length)
                                        StructError(null, structname, lastlinenr + 1);
                                    currentOffset += bytesize * AStringUtils.StrToInt(tokens[j].Value);
                                }
                                else
                                    StructError(null, structname, lastlinenr + 1);
                            }
                            break;
                        case 'D':
                            {
                                //could be d* ?
                                if (tokens[j].Length == 2)
                                {
                                    switch (tokens[j].Value[1])
                                    {
                                        case 'B':
                                            bytesize = 1;
                                            break;
                                        case 'W':
                                            bytesize = 2;
                                            break;
                                        case 'D':
                                            bytesize = 4;
                                            break;
                                        case 'Q':
                                            bytesize = 8;
                                            break;
                                        default:
                                            StructError(null, structname, lastlinenr + 1);
                                            break;
                                    }
                                    j += 1;
                                    if (j >= tokens.Length)
                                        StructError(null, structname, lastlinenr + 1);
                                    currentOffset += bytesize;
                                    //check if there are more ?'s after this (in case of dw ? ? ?)
                                    while (j < tokens.Length - 1)
                                    {
                                        if (tokens[j + 1].Value == "?")   //check from the spot in front
                                        {
                                            currentOffset += bytesize;
                                            j += 1;
                                        }
                                        else
                                            break; //nope
                                    }

                                }
                                else
                                    StructError(null, structname, lastlinenr + 1);
                            }
                            break;
                        default:
                            //we already dealth with labels, so this is wrong
                            StructError("No idea what '" + tokens[j].Value + "' is", structname, lastlinenr + 1);
                            break;
                    }
                    j += 1; //next token
                }
            }
            if (endfound == false)
                StructError("No end found", structname, lastlinenr + 1);
            // the elements have been filled in, delete the structure (between linenr and lastlinenr)
            // and inject define(element,offset)
            // and define(structname.element,offset)
            for (i = lastlinenr; i >= linenr; i--)
                code.RemoveAt(i);
            for (i = 0; i < elements.Length; i++)
            {
                code.Insert(linenr, "define(" + elements[i].Value + ',' + AStringUtils.IntToHex(elements[i].Position, 1) + ')');
                code.Insert(linenr, "define(" + structname + '.' + elements[i].Value + ',' + AStringUtils.IntToHex(elements[i].Position, 1) + ')');
            }
            code.Insert(linenr, "define(" + structname + "_size," + AStringUtils.IntToHex(currentOffset, 1) + ')');
        }
        #endregion
        #region StructError
        private void StructError(String reason, String structName, int lineNumber)
        {
            var error = "Error in the structure definition of " + structName + " at line " + lineNumber;
            if (!String.IsNullOrEmpty(reason))
                error = error + " :" + reason;
            else
                error += '.';
            throw new Exception(error);
        }
        #endregion
        #region AobScans -- todo make this work!!!
        public void AobScans(AProcess process, ARefStringArray code, Boolean syntaxCheckOnly)
        {

        }
        #endregion
        #region AutoAssemble
        public Boolean AutoAssemble(AProcess process, ARefStringArray code, Boolean popUpMessages, Boolean enable, Boolean syntaxCheckOnly, AAllocArray allocArray, AStringArray registeredSymbols, Boolean createScript, ARefStringArray newScript)
        {
            //add line numbers to the code
            for (var i = 0; i < code.Length; i++)
                code[i].Position = i + 1;
            GetEnableAndDisablePos(code, out var enablePos, out var disablePos);
            if (enablePos == -2)
            {
                if (!popUpMessages)
                    return false;
                throw new Exception(rsYouCanOnlyHaveOneEnableSection);
            }
            if (disablePos == -2)
            {
                if (!popUpMessages)
                    return false;
                throw new Exception(rsYouCanOnlyHaveOneDisableSection);
            }
            var tempStrings = new ARefStringArray();
            if (enablePos == -1 && disablePos == -1)
                tempStrings.AddRange(code); // everything
            else
            {
                if (enablePos == -1)
                {
                    if (!popUpMessages)
                        return false;
                    throw new Exception(rsYouHavnTSpecifiedAEnableSection);
                }
                if (disablePos == -1)
                {
                    if (!popUpMessages)
                        return false;
                    throw new Exception(rsYouHavnTSpecifiedADisableSection);
                }
                switch (enable)
                {
                    case true:
                        GetScript(code, tempStrings, true);
                        break;
                    case false:
                        GetScript(code, tempStrings, false);
                        break;
                }
            }
            var result = AutoAssemble2(process, tempStrings, popUpMessages, syntaxCheckOnly, allocArray, registeredSymbols, createScript, newScript);
            return result;
        }
        #endregion
        #region AutoAssemble2
        public Boolean AutoAssemble2(AProcess process, ARefStringArray code, Boolean popUpMessages, Boolean syntaxCheckOnly, AAllocArray allocArray, AStringArray registeredSymbols, Boolean createScript, ARefStringArray newScript)
        {
            var i = 0;
            var j = 0;
            var k = 0;
            var l = 0;
            var e = 0;
            String currentline = "";
            int currentlinenr = 0;
            var currentaddress = UIntPtr.Zero;
            var assembled = new AArrayManager<AAssembled>();
            UInt64 x = 0;
            UInt32 op = 0;
            UInt64 op2 = 0;
            Boolean ok1 = false;
            var ok2 = false;
            var loadbinary = new AArrayManager<ALoadLibrary>();
            var globalallocs = new AAllocArray();
            var allocs = new AAllocArray();
            var kallocs = new AAllocArray();
            var sallocs = new AAllocArray();
            var labels = new AArrayManager<ALabels>();
            var defines = new AArrayManager<ADefines>();
            var fullaccess = new AArrayManager<AFullAccess>();
            var dealloc = new AArrayManager<UIntPtr>();
            var addsymbollist = new AStringArray();
            var deletesymbollist = new AStringArray();
            var createthread = new AStringArray();
            var a = 0;
            var b = 0;
            var c = 0;
            var d = 0;
            var s1 = "";
            var s2 = "";
            var s3 = "";
            var assemblerlines = new AStringArray();
            var varsize = 0;
            var tokens = new ARefStringArray();
            var baseaddress = UIntPtr.Zero;
            var include = new ARefStringArray();
            UInt32 bw = 0;
            UInt64 bw2 = 0;
            var testPtr = UIntPtr.Zero;
            var bytes = new ATByteArray();
            UIntPtr prefered = UIntPtr.Zero;
            var intPtrSize = 0;
            var intPtrHexSize = 0;
            var result = false;
            allocs.SetLength(0);
            kallocs.SetLength(0);
            globalallocs.SetLength(0);
            sallocs.SetLength(0);
            createthread.SetLength(0);
            currentaddress = UIntPtr.Zero;
            if (Assembler.Is64Bit)
                intPtrSize = 8;
            else
                intPtrSize = 4;
            intPtrHexSize = intPtrSize * 2;
            if (syntaxCheckOnly && (registeredSymbols != null))
            {
                //add the symbols as defined labels
                labels.SetLength(registeredSymbols.Length);
                for (i = 0; i < registeredSymbols.Length; i++)
                {
                    labels[i].Name = registeredSymbols[i];
                    labels[i].Defined = true;
                    labels[i].Address = UIntPtr.Zero;
                    labels[i].AssemblerLine = 0;
                    labels[i].References.SetLength(0);
                    labels[i].References2.SetLength(0);
                }
            }
            var symHandler = Assembler.SymHandler;
            symHandler.Process = process;
            symHandler.WaitForSymbolsLoaded();
            //2 pass scanner
            try
            {
                assembled.SetLength(1);
                kallocs.SetLength(0);
                allocs.SetLength(0);
                dealloc.SetLength(0);
                assemblerlines.SetLength(0);
                fullaccess.SetLength(0);
                addsymbollist.SetLength(0);
                deletesymbollist.SetLength(0);
                defines.SetLength(0);
                loadbinary.SetLength(0);
                tokens = new ARefStringArray();
                RemoveComments(code);
                UnlabeledLabels(code);
                AobScans(process, code, syntaxCheckOnly);
                //first pass
                i = 0;
                while (i < code.Length)
                {
                    try
                    {
                        try
                        {
                            currentline = code[i].Value;
                            currentlinenr = code[i].Position;
                            //check if useless
                            if (currentline.Length == 0)
                                continue;
                            if (AStringUtils.Copy(currentline, 0, 2) == "//")
                                continue; //skip
                            currentline = currentline.Trim();
                            for (j = 0; j < defines.Length; j++)
                                currentline = ReplaceToken(currentline, defines[j].Name, defines[j].Whatever);
                            if (currentline.Length == 0)
                                continue;
                            if (AStringUtils.Copy(currentline, 0, 2) == "//")
                                continue; //skip
                            assemblerlines.SetLength(assemblerlines.Length + 1);
                            assemblerlines.Last = currentline;
                            //do this first. Do not touch register symbol with any kind of define/label/whatsoever
                            #region Command REGISTERSYMBOL()
                            if (AStringUtils.Copy(currentline, 0, 15).ToUpper() == "REGISTERSYMBOL(")
                            {
                                //add this symbol to the register symbol list
                                a = AStringUtils.Pos("(", currentline);
                                b = AStringUtils.Pos(")", currentline);
                                if (a > 0 && b > 0)
                                {
                                    s1 = AStringUtils.Copy(currentline, a + 1, b - a - 1).Trim();
                                    addsymbollist.SetLength(addsymbollist.Length + 1);
                                    addsymbollist.Last = s1;
                                    registeredSymbols?.Add(s1);
                                }
                                else
                                    throw new Exception(rsSyntaxError);
                                assemblerlines.SetLength(assemblerlines.Length - 1);
                                continue;
                            }
                            #endregion
                            //if the newline is empty then it has been handled and the plugin doesn't want it to be added for phase2
                            if (currentline.Length == 0)
                            {
                                assemblerlines.SetLength(assemblerlines.Length - 1);
                                continue;
                            }
                            //otherwise it hasn't been handled, or it has been handled and the string is a compatible string that passes the phase1 tests (so variable names converted to 00000000 and whatever else is needed)
                            //plugins^^^
                            #region Command ASSERT()
                            if (AStringUtils.Copy(currentline, 0, 7).ToUpper() == "ASSERT(")  //assert(address,aob)
                            {
                                if (!syntaxCheckOnly)
                                {
                                    a = AStringUtils.Pos("(", currentline);
                                    b = AStringUtils.Pos(",", currentline);
                                    c = AStringUtils.Pos(")", currentline);
                                    if (a > 0 && b > 0 && c > 0)
                                    {
                                        s1 = AStringUtils.Copy(currentline, a + 1, b - a - 1).Trim(); //address
                                        s2 = AStringUtils.Copy(currentline, b + 1, c - b - 1).Trim(); //aob
                                        if (createScript)
                                            continue;
                                        testPtr = symHandler.GetAddressFromName(s1, false);
                                        bytes.SetLength(0);
                                        try
                                        {
                                            AStringUtils.ConvertStringToBytes(s2, true, bytes);
                                        }
                                        catch
                                        {
                                            throw new Exception(UStringUtils.Sprintf(rsIsNotAvalidByteString, s2));
                                        }
                                        if (bytes.Length > 0)
                                        {
                                            var byteBuf = (Byte[])process.ReadMem((IntPtr)testPtr.ToUInt64(), ReadType.Binary, bytes.Length);
                                            if (byteBuf.Length == bytes.Length)
                                            {
                                                for (j = 0; j < bytes.Length; j++)
                                                {
                                                    if (bytes[j] != byteBuf[j])
                                                    {
                                                        if (AMathUtils.InRangeX(bytes[j], Byte.MinValue, Byte.MaxValue))
                                                            throw new Exception(UStringUtils.Sprintf(rsTheBytesAtAreNotWhatWasExpected, s1));
                                                    }
                                                }
                                            }
                                            else
                                                throw new Exception(UStringUtils.Sprintf(rsTheMemoryAtCanNotBeRead, s1));
                                        }
                                        else
                                            throw new Exception(UStringUtils.Sprintf(rsIsNotAvalidByteString, s2));

                                    }
                                    else
                                        throw new Exception(rsWrongSyntaxASSERTAddress1122335566);
                                }
                                assemblerlines.SetLength(assemblerlines.Length - 1);
                                continue;
                            }
                            #endregion
                            #region Command SHAREDALLOC() -- todo
                            /*
                            if (uppercase(copy(currentline, 1, 12)) == "SHAREDALLOC(")
                            {
                                a = pos("(", currentline);
                                b = pos(",", currentline);
                                c = pos(")", currentline);
                                if ((a > 0) && (b > 0) && (c > 0))
                                {
                                    s1 = trim(copy(currentline, a + 1, b - a - 1));
                                    s2 = trim(copy(currentline, b + 1, c - b - 1));

                                    //try
                                    x = strtoint(s2);
                                    //except
                                    Create(Format(rsIsNotAValidSize, set::of(s2, eos)));
                                    //end;

                                    setlength(sallocs, length(sallocs) + 1);
                                    sallocs[length(sallocs) - 1].address = allocateSharedMemoryIntoTargetProcess(s1, x);
                                    sallocs[length(sallocs) - 1].varname = s1;
                                    sallocs[length(sallocs) - 1].size = x;

                                    setlength(assemblerlines, length(assemblerlines) - 1);
                                    continue_;

                                }
                                else
                                    Create(rsWrongSyntaxSHAREDALLOCNameSize);
                            }
                            */
                            #endregion
                            #region Command GLOBALALLOC() -- todo
                            /*
                            if (uppercase(copy(currentline, 1, 12)) == "GLOBALALLOC(")
                            {
                                a = pos("(", currentline);
                                b = pos(",", currentline);
                                c = pos(")", currentline);
                                if ((a > 0) && (b > 0) && (c > 0))
                                {
                                    s1 = trim(copy(currentline, a + 1, b - a - 1));
                                    s2 = trim(copy(currentline, b + 1, c - b - 1));

                                    try
                                    {
                                        x = strtoint(s2);
                                    }
                                    catch
                                    {
                                        throw new Exception(UStringUtils.Sprintf(rsIsNotAValidSize, set::of(s2, eos)));
                                    }

                                    //define it here already
                                    symHandler.SetUserdefinedSymbolAllocSize(s1, x);

                                    setlength(globalallocs, length(globalallocs) + 1);
                                    globalallocs[length(globalallocs) - 1].address = symHandler.GetUserdefinedSymbolByName(s1);
                                    globalallocs[length(globalallocs) - 1].varname = s1;
                                    globalallocs[length(globalallocs) - 1].size = x;

                                    setlength(assemblerlines, length(assemblerlines) - 1);
                                    continue_;

                                }
                                else throw new Exception(rsWrongSyntaxGLOBALALLOCNameSize);

                            }
                            */
                            #endregion
                            #region Command INCLUDE()
                            if (AStringUtils.Copy(currentline, 0, 8).ToUpper() == "INCLUDE(")
                            {
                                a = AStringUtils.Pos("(", currentline);
                                b = AStringUtils.Pos(")", currentline);
                                if (a > 0 && b > 0)
                                {
                                    s1 = AStringUtils.Copy(currentline, a + 1, b - a - 1).Trim();

                                    if (String.IsNullOrEmpty(UIo.Path.GetExtension(s1)))
                                        s1 += ".cea";
                                    if (!UIo.File.Exists(s1))
                                        throw new Exception(UStringUtils.Sprintf(rsCouldNotBeFound, s1));
                                    include = new ARefStringArray();
                                    include.AddRange(UIo.File.ReadAllLines(s1));
                                    RemoveComments(include);
                                    UnlabeledLabels(include);
                                    AobScans(process, include, syntaxCheckOnly);
                                    for (j = i + 1; j <= (i + 1) + (include.Length - 1); j++)
                                        code.Insert(j, include[j - (i + 1)]);
                                    assemblerlines.SetLength(assemblerlines.Length - 1);
                                    continue;
                                }
                                else
                                    throw new Exception(rsWrongSyntaxIncludeFilenameCea);
                            }
                            #endregion
                            #region Command CREATETHREAD() -- todo
                            /*
                            if (uppercase(copy(currentline, 1, 13)) == "CREATETHREAD(")
                            {
                                //load a binary file into memory , this one already executes BEFORE the 2nd pass to get addressnames correct
                                a = pos("(", currentline);
                                b = pos(")", currentline);
                                if ((a > 0) && (b > 0))
                                {
                                    s1 = trim(copy(currentline, a + 1, b - a - 1));

                                    setlength(createthread, length(createthread) + 1);
                                    createthread[length(createthread) - 1] = s1;

                                    setlength(assemblerlines, length(assemblerlines) - 1);
                                    continue_;
                                }
                                else throw new Exception(rsWrongSyntaxCreateThreadAddress);
                            }
                            */
                            #endregion
                            #region Command READMEM()
                            if (AStringUtils.Copy(currentline, 0, 8).ToUpper() == "READMEM(")
                            {
                                //read memory and place it here (readmem(address,size) )
                                a = AStringUtils.Pos("(", currentline);
                                b = AStringUtils.Pos(",", currentline);
                                c = AStringUtils.Pos(")", currentline);
                                if ((a > 0) && (b > 0) && (c > 0))
                                {
                                    s1 = AStringUtils.Copy(currentline, a + 1, b - a - 1).Trim();
                                    s2 = AStringUtils.Copy(currentline, b + 1, c - b - 1).Trim();
                                    //read memory and replace with lines of DB xx xx xx xx xx xx xx xx
                                    testPtr = symHandler.GetAddressFromName(s1);
                                    a = AStringUtils.StrToInt(s2);
                                    if (a == 0)
                                        throw new Exception(rsInvalidSizeForReadMem);
                                    var byteBuf = (Byte[])process.ReadMem((IntPtr)testPtr.ToUInt64(), ReadType.Binary, a);
                                    if (byteBuf.Length <= 0 | (byteBuf.Length < a))
                                        throw new Exception(UStringUtils.Sprintf(rsTheMemoryAtCouldNotBeFullyRead, s1));
                                    //still here so everything ok
                                    assemblerlines.SetLength(assemblerlines.Length - 1);
                                    s1 = "db";
                                    for (j = 0; j <= a - 1; j++)
                                    {
                                        s1 = s1 + ' ' + AStringUtils.IntToHex(byteBuf[j], 2);
                                        if (j % 16 == 15)
                                        {
                                            assemblerlines.SetLength(assemblerlines.Length + 1);
                                            assemblerlines.Last = s1;
                                            s1 = "db";
                                        }
                                    }
                                    if (s1.Length > 2)
                                    {
                                        assemblerlines.SetLength(assemblerlines.Length + 1);
                                        assemblerlines.Last = s1;
                                    }
                                }
                                else
                                    throw new Exception(rsWrongSyntaxReadMemAddressSize);
                                continue;
                            }
                            #endregion
                            #region Command LOADBINARY() -- todo
                            /*
                            if (uppercase(copy(currentline, 1, 11)) == "LOADBINARY(")
                            {
                                //load a binary file into memory
                                a = pos("(", currentline);
                                b = pos(",", currentline);
                                c = pos(")", currentline);
                                if ((a > 0) && (b > 0) && (c > 0))
                                {
                                    s1 = trim(copy(currentline, a + 1, b - a - 1));
                                    s2 = trim(copy(currentline, b + 1, c - b - 1));

                                    if (~fileexists(s2)) throw new Exception(UStringUtils.Sprintf(rsTheFileDoesNotExist, set::of(s2, eos)));

                                    setlength(loadbinary, length(loadbinary) + 1);
                                    loadbinary[length(loadbinary) - 1].address = s1;
                                    loadbinary[length(loadbinary) - 1].filename = s2;

                                    setlength(assemblerlines, length(assemblerlines) - 1);
                                    continue_;
                                }
                                else throw new Exception(rsWrongSyntaxLoadBinaryAddressFilename);
                            }
                            */
                            #endregion
                            #region Command UNREGISTERSYMBOL() -- todo
                            /*
                            if (uppercase(copy(currentline, 1, 17)) == "UNREGISTERSYMBOL(")
                            {
                                //add this symbol to the register symbollist
                                a = pos("(", currentline);
                                b = pos(")", currentline);

                                if ((a > 0) && (b > 0))
                                {
                                    s1 = trim(copy(currentline, a + 1, b - a - 1));

                                    setlength(deletesymbollist, length(deletesymbollist) + 1);
                                    deletesymbollist[length(deletesymbollist) - 1] = s1;
                                }
                                else throw new Exception(rsSyntaxError);

                                setlength(assemblerlines, length(assemblerlines) - 1);
                                continue_;
                            }
                            */
                            #endregion
                            #region Command DEFINE() -- todo
                            /*
                            if (uppercase(copy(currentline, 1, 7)) == "DEFINE(")
                            {
                                //syntax: alloc(x,size)    x=variable name size=bytes
                                //allocate memory
                                a = pos("(", currentline);
                                b = pos(",", currentline);
                                c = pos(")", currentline);
                                if ((a > 0) && (b > 0) && (c > 0))
                                {
                                    s1 = trim(copy(currentline, a + 1, b - a - 1));
                                    s2 = copy(currentline, b + 1, c - b - 1);

                                    for (j = 0; j <= length(defines) - 1; j++)
                                        if (uppercase(defines[j].name) == uppercase(s1))
                                            throw new Exception(UStringUtils.Sprintf(rsDefineAlreadyDefined, set::of(s1, eos)));

                                    setlength(defines, length(defines) + 1);
                                    defines[length(defines) - 1].name = s1;
                                    defines[length(defines) - 1].whatever = s2;

                                    setlength(assemblerlines, length(assemblerlines) - 1);   //don't bother with this in the 2nd pass
                                    continue_;
                                }
                                else throw new Exception(rsWrongSyntaxDEFINENameWhatever);
                            }
                            */
                            #endregion
                            #region Command STRUCT() -- todo
                            /*
                            if (uppercase(copy(currentline, 1, 7)) == "STRUCT ")
                            {
                                replaceStructWithDefines(code, i);
                                setlength(assemblerlines, length(assemblerlines) - 1);
                                i -= 1; //repeat from this line
                                continue_;
                            }
                            */
                            #endregion
                            #region Command FULLACCESS() -- todo
                            /*
                            if (uppercase(copy(currentline, 1, 11)) == "FULLACCESS(")
                            {
                                a = pos("(", currentline);
                                b = pos(",", currentline);
                                c = pos(")", currentline);

                                if ((a > 0) && (b > 0) && (c > 0))
                                {
                                    s1 = trim(copy(currentline, a + 1, b - a - 1));
                                    s2 = trim(copy(currentline, b + 1, c - b - 1));

                                    setlength(fullaccess, length(fullaccess) + 1);
                                    fullaccess[length(fullaccess) - 1].address = symHandler.getAddressFromName(s1);
                                    fullaccess[length(fullaccess) - 1].size = strtoint(s2);
                                }
                                else throw new Exception(rsSyntaxErrorFullAccessAddressSize);

                                setlength(assemblerlines, length(assemblerlines) - 1);
                                continue_;
                            }
                            */
                            #endregion
                            #region Command LABEL()
                            if (AStringUtils.Copy(currentline, 0, 6).ToUpper() == "LABEL(")
                            {
                                //syntax: label(x)  x=name of the label
                                //later on in the code there has to be a line with "labelname:"
                                a = AStringUtils.Pos("(", currentline);
                                b = AStringUtils.Pos(")", currentline);
                                if (a != -1 && b != -1)
                                {
                                    s1 = AStringUtils.Copy(currentline, a + 1, b - a - 1).Trim();
                                    AStringUtils.Val("0x" + s1, out j, out a);
                                    if (a == 0)
                                        throw new Exception(UStringUtils.Sprintf(rsIsNotAValidIdentifier, s1));
                                    varsize = s1.Length;
                                    while (j < labels.Length && labels[j].Name.Length > varsize)
                                    {
                                        if (labels[j].Name == s1)
                                            throw new Exception(UStringUtils.Sprintf(rsIsBeingRedeclared, s1));
                                        j += 1;
                                    }
                                    j = labels.Length;//quickfix
                                    l = j;
                                    //check for the line "labelname:"
                                    ok1 = false;
                                    for (j = 0; j <= code.Length - 1; j++)
                                    {
                                        if (code[j].Value.Trim() == s1 + ':')
                                        {
                                            if (ok1)
                                                throw new Exception(UStringUtils.Sprintf(rsLabelIsBeingDefinedMoreThanOnce, s1));
                                            ok1 = true;
                                        }
                                    }
                                    if (!ok1)
                                        throw new Exception(UStringUtils.Sprintf(rsLabelIsNotDefinedInTheScript, s1));
                                    //still here so ok
                                    //insert it
                                    labels.SetLength(labels.Length + 1);
                                    for (k = labels.Length - 1; k >= j + 1; k--)
                                        labels[k] = labels[k - 1];
                                    labels[l].Name = s1;
                                    labels[l].Defined = false;
                                    assemblerlines.SetLength(assemblerlines.Length - 1);
                                    labels[l].References.SetLength(0);
                                    labels[l].References2.SetLength(0);
                                    continue;
                                }
                                else
                                    throw new Exception(rsSyntaxError);
                            }
                            #endregion
                            #region Command DEALLOC()
                            if (AStringUtils.Copy(currentline, 0, 8).ToUpper() == "DEALLOC(")
                            {
                                if (allocArray != null) //memory dealloc=possible
                                {
                                    //syntax: dealloc(x)  x=name of region to deallocate
                                    //later on in the code there has to be a line with "labelname:"
                                    a = AStringUtils.Pos("(", currentline);
                                    b = AStringUtils.Pos(")", currentline);
                                    if (a > 0 && b > 0)
                                    {
                                        s1 = AStringUtils.Copy(currentline, a + 1, b - a - 1).Trim();
                                        //find s1 in the ceallocarray
                                        for (j = 0; j < allocArray.Length; j++)
                                        {
                                            if (String.Equals(allocArray[j].Name, s1, StringComparison.CurrentCultureIgnoreCase))
                                            {
                                                dealloc.SetLength(dealloc.Length + 1);
                                                dealloc.Last = allocArray[j].Address;
                                            }
                                        }
                                    }
                                }
                                assemblerlines.SetLength(assemblerlines.Length - 1);
                                continue;
                            }
                            #endregion
                            #region Command ALLOC()
                            if (AStringUtils.Copy(currentline, 0, 6).ToUpper() == "ALLOC(")
                            {
                                //syntax: alloc(x,size)    x=variable name size=bytes
                                //or
                                //syntax: alloc(x,size,prefered region)    x=variable name size=bytes
                                //allocate memory
                                a = AStringUtils.Pos("(", currentline);
                                b = AStringUtils.Pos(",", currentline);
                                c = AStringUtils.PosEx(",", currentline, b + 1);
                                d = AStringUtils.Pos(")", currentline);
                                if (a > 0 && b > 0 && d > 0)
                                {
                                    s1 = AStringUtils.Copy(currentline, a + 1, b - a - 1).Trim();
                                    if (c > 0)
                                    {
                                        s2 = AStringUtils.Copy(currentline, b + 1, c - b - 1).Trim();
                                        s3 = AStringUtils.Copy(currentline, c + 1, d - c - 1).Trim();
                                    }
                                    else
                                    {
                                        s2 = AStringUtils.Copy(currentline, b + 1, d - b - 1).Trim();
                                        s3 = "";
                                    }
                                    AStringUtils.Val("0x" + s1, out j, out a);
                                    if (a == 0)
                                        throw new Exception(UStringUtils.Sprintf(rsIsNotAValidIdentifier, s1));
                                    varsize = s1.Length;
                                    //check for duplicate identifiers
                                    j = 0;
                                    while (j < allocs.Length && allocs[j].Name.Length > varsize)
                                    {
                                        if (allocs[j].Name == s1)
                                            throw new Exception(UStringUtils.Sprintf(rsTheIdentifierHasAlreadyBeenDeclared, s1));

                                        j += 1;
                                    }
                                    j = allocs.Length; //quickfix
                                    allocs.SetLength(allocs.Length + 1);
                                    //longest varnames first so the rename of a shorter matching var wont override the longer one
                                    //move up the other allocs so I can inser this element (A linked list might have been better)
                                    for (k = allocs.Length - 1; k >= j + 1; k--)
                                        allocs[k] = allocs[k - 1];
                                    allocs[j].Name = s1;
                                    allocs[j].Size = AStringUtils.StrToDWord(s2);
                                    if (s3 != "")
                                        allocs[j].Preferred = symHandler.GetAddressFromName(s3);
                                    else
                                        allocs[j].Preferred = UIntPtr.Zero;
                                    assemblerlines.SetLength(assemblerlines.Length - 1);   //don't bother with this in the 2nd pass
                                    continue;
                                }
                                else throw new Exception(rsWrongSyntaxALLOCIdentifierSizeinbytes);
                            }
                            #endregion
                            //replace ALLOC identifiers with values so the assemble error check doesnt crash on that
                            if (process.IsX64)
                            {
                                for (j = 0; j < allocs.Length; j++)
                                    currentline = ReplaceToken(currentline, allocs[j].Name, "ffffffffffffffff");
                            }
                            else
                            {
                                for (j = 0; j < allocs.Length; j++)
                                    currentline = ReplaceToken(currentline, allocs[j].Name, "00000000");
                            }
                            #region Command KALLOC() -- todo
                            /*
                            if (uppercase(copy(currentline, 1, 7)) == "KALLOC(")
                            {
                                if (~DBKReadWrite)
                                    Create(rsNeedToUseKernelmodeReadWriteprocessmemory);

                                if (DBKLoaded == false)
                                    Create(rsSorryButWithoutTheDriverKALLOCWillNotFunction);

                                //syntax: kalloc(x,size)    x=variable name size=bytes
                                //kallocate memory
                                a = pos("(", currentline);
                                b = pos(",", currentline);
                                c = pos(")", currentline);

                                if ((a > 0) && (b > 0) && (c > 0))
                                {
                                    s1 = trim(copy(currentline, a + 1, b - a - 1));
                                    s2 = trim(copy(currentline, b + 1, c - b - 1));

                                    val(string('$') + s1, j, a);
                                    if (a == 0)
                                        Create(Format(rsIsNotAValidIdentifier, set::of(s1, eos)));

                                    varsize = length(s1);

                                    //check for duplicate identifiers
                                    j = 0;
                                    while ((j < length(kallocs)) && (length(kallocs[j].varname) > varsize))
                                    {
                                        if (kallocs[j].varname == s1)
                                            Create(Format(rsTheIdentifierHasAlreadyBeenDeclared, set::of(s1, eos)));

                                        j += 1;
                                    }

                                    j = length(kallocs);//quickfix

                                    setlength(kallocs, length(kallocs) + 1);

                                    //longest varnames first so the rename of a shorter matching var wont override the longer one
                                    //move up the other kallocs so I can inser this element (A linked list might have been better)
                                    for (k = length(kallocs) - 1; k >= j + 1; k--)
                                        kallocs[k] = kallocs[k - 1];

                                    kallocs[j].varname = s1;
                                    kallocs[j].size = StrToInt(s2);

                                    setlength(assemblerlines, length(assemblerlines) - 1);   //don't bother with this in the 2nd pass
                                    continue_;
                                }
                                else
                                    Create(rsWrongSyntaxKallocIdentifierSizeinbytes);
                            }
                            */
                            #endregion
                            //replace KALLOC identifiers with values so the assemble error check doesnt crash on that
                            if (process.IsX64)
                            {
                                for (j = 0; j < kallocs.Length; j++)
                                    currentline = ReplaceToken(currentline, kallocs[j].Name, "ffffffffffffffff");
                            }
                            else
                            {
                                for (j = 0; j < kallocs.Length; j++)
                                    currentline = ReplaceToken(currentline, kallocs[j].Name, "00000000");
                            }
                            //check for assembler errors
                            //address
                            if (currentline[currentline.Length - 1] == ':')
                            {
                                try
                                {
                                    ok1 = false;
                                    for (j = 0; j < labels.Length; j++)
                                    {
                                        if (currentline != labels[j].Name + ':')
                                            continue;
                                        labels[j].AssemblerLine = assemblerlines.Length - 1;
                                        ok1 = true;
                                        break;
                                    }
                                    if (ok1)
                                        continue; //no check
                                    try
                                    {
                                        // todo is this ok as just an int?!?!?
                                        j = (int)symHandler.GetAddressFromName(AStringUtils.Copy(currentline, 0, currentline.Length - 1));
                                    }
                                    catch
                                    {
                                        currentline = AStringUtils.IntToHex(symHandler.GetAddressFromName(AStringUtils.Copy(currentline, 1, currentline.Length - 1)), 8) + ':';
                                        assemblerlines[assemblerlines.Length - 1] = currentline;
                                    }
                                    continue; //next line
                                }
                                catch
                                {
                                    throw new Exception(rsThisAddressSpecifierIsNotValid);
                                }
                            }
                            //replace label references with 00000000 so the assembler check doesn't complain about it
                            if (process.IsX64)
                            {
                                for (j = 0; j < labels.Length; j++)
                                    currentline = ReplaceToken(currentline, labels[j].Name, "ffffffffffffffff");
                            }
                            else
                            {
                                for (j = 0; j < labels.Length; j++)
                                    currentline = ReplaceToken(currentline, labels[j].Name, "00000000");
                            }
                            try
                            {
                                //replace identifiers in the line with their address
                                if (!Assembler.Assemble(currentline, currentaddress.ToUInt64(), assembled[0].Bytes, AAssemblerPreference.apnone, true))
                                    throw new Exception("bla");
                            }
                            catch
                            {
                                throw new Exception(rsThisInstructionCanTBeCompiled);
                            }
                        }
                        finally
                        {
                            i += 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(UStringUtils.Sprintf(rsErrorInLine, currentlinenr, currentline, ex.Message));
                    }
                }
                #region Sanity Check (Symbols)
                if (addsymbollist.Length > 0)
                {
                    //now scan the addsymbollist entries for allocs and labels and see if they exist
                    for (i = 0; i < addsymbollist.Length; i++)
                    {
                        ok1 = false;
                        for (j = 0; j < allocs.Length; j++) //scan allocs
                        {
                            if (String.Equals(addsymbollist[i], allocs[j].Name, StringComparison.CurrentCultureIgnoreCase))
                            {
                                ok1 = true;
                                break;
                            }
                        }
                        if (!ok1) //scan labels
                        {
                            for (j = 0; j < labels.Length; j++)
                            {
                                if (String.Equals(addsymbollist[i], labels[j].Name, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    ok1 = true;
                                    break;
                                }
                            }
                        }
                        if (!ok1) //scan defines
                        {
                            for (j = 0; j < defines.Length; j++)
                            {
                                if (String.Equals(addsymbollist[i], defines[j].Name, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    ok1 = true;
                                    break;
                                }
                            }
                        }
                        if (!ok1)
                            throw new Exception(UStringUtils.Sprintf(rsWasSupposedToBeAddedToTheSymbollistButItIsnTDeclar, addsymbollist[i]));
                    }
                }
                #endregion
                #region Sanity Check (Create Thread) -- todo
                //if (createthread.Length > 0)
                //{
                //    for (i = 0; i < createthread.Length; i++)
                //    {
                //        ok1 = true;
                //
                //        //try
                //        testPtr = symHandler.getAddressFromName(createthread[i]);
                //        //except
                //        ok1 = false;
                //        //end;
                //        if (!ok1)
                //        {
                //            for (j = 0; j <= length(labels) - 1; j++)
                //            {
                //                if (uppercase(labels[j].labelname) == uppercase(createthread[i]))
                //                {
                //                    ok1 = true;
                //                    flush();
                //                }
                //            }
                //        }
                //        if (!ok1)
                //        {
                //            for (j = 0; j <= length(allocs) - 1; j++)
                //            {
                //                if (uppercase(allocs[j].varname) == uppercase(createthread[i]))
                //                {
                //                    ok1 = true;
                //                    flush();
                //                }
                //            }
                //        }
                //        if (!ok1)
                //        {
                //            for (j = 0; j <= length(kallocs) - 1; j++)
                //            {
                //                if (uppercase(kallocs[j].varname) == uppercase(createthread[i]))
                //                {
                //                    ok1 = true;
                //                    flush();
                //                }
                //            }
                //        }
                //        if (!ok1)
                //        {
                //            for (j = 0; j <= length(defines) - 1; j++)
                //            {
                //                if (uppercase(defines[j].name) == uppercase(createthread[i]))
                //                {
                //                    //try
                //                    testPtr = symHandler.getAddressFromName(defines[j].whatever);
                //                    ok1 = true;
                //                    //except
                //                    //end;
                //                    flush();
                //                }
                //            }
                //        }
                //        if (!ok1)
                //            throw new Exception(UStringUtils.Sprintf(rsTheAddressInCreatethreadIsNotValid, createthread[i]));
                //    }
                //}
                #endregion
                #region Sanity Check (Load Binary) -- todo
                //if (length(loadbinary) > 0)
                //{
                //
                //    for (i = 0; i <= length(loadbinary) - 1; i++)
                //    {
                //        ok1 = true;
                //
                //        //try
                //        testPtr = symHandler.getAddressFromName(loadbinary[i].address);
                //        //except
                //        ok1 = false;
                //        //end;
                //
                //        if (~ok1)
                //            for (j = 0; j <= length(labels) - 1; j++)
                //                if (uppercase(labels[j].labelname) == uppercase(loadbinary[i].address))
                //                {
                //                    ok1 = true;
                //                    flush();
                //                }
                //
                //        if (~ok1)
                //            for (j = 0; j <= length(allocs) - 1; j++)
                //                if (uppercase(allocs[j].varname) == uppercase(loadbinary[i].address))
                //                {
                //                    ok1 = true;
                //                    flush();
                //                }
                //        if (~ok1)
                //            for (j = 0; j <= length(kallocs) - 1; j++)
                //                if (uppercase(kallocs[j].varname) == uppercase(loadbinary[i].address))
                //                {
                //                    ok1 = true;
                //                    flush();
                //                }
                //
                //        if (~ok1)
                //            for (j = 0; j <= length(defines) - 1; j++)
                //                if (uppercase(defines[j].name) == uppercase(loadbinary[i].address))
                //                {
                //                    //try
                //                    testPtr = symHandler.getAddressFromName(defines[j].whatever);
                //                    ok1 = true;
                //                    //except
                //                    //end;
                //                    flush();
                //                }
                //
                //        if (~ok1)
                //            throw new Exception(UStringUtils.Sprintf(rsTheAddressInLoadbinaryIsNotValid, set::of(loadbinary[i].address, loadbinary[i].filename, eos)));
                //
                //    }
                //}
                #endregion
                if (syntaxCheckOnly)
                {
                    result = true;
                    return result;
                }
                if (popUpMessages && MessageBox.Show(rsThisCodeCanBeInjectedAreYouSure, "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
                {
                    return result;
                    //Code is injectable ,but Do not inject it!
                }
                #region Allocate The Memory
                if (allocs.Length > 0)
                {
                    j = 0; //entry to go from
                    prefered = allocs[0].Preferred;
                    x = allocs[0].Size;
                    for (i = 1; i <= allocs.Length - 1; i++)
                    {
                        //does this entry have a prefered location?
                        if (allocs[i].Preferred != UIntPtr.Zero)
                        {
                            //if yes, is it the same as the previous entry? (or was the previous one that doesn't care?)
                            if (prefered != allocs[i].Preferred && prefered != UIntPtr.Zero)
                            {
                                //different preferred address
                                if (x > 0)  //it has some previous entries with compatible locations
                                {
                                    k = 10;
                                    allocs[j].Address = UIntPtr.Zero;
                                    while (k > 0 && allocs[j].Address == UIntPtr.Zero)
                                    {
                                        //try allocating until a memory region has been found (e.g due to quick allocating by the game)
                                        allocs[j].Address = process.AllocNear(prefered.ToIntPtr(), (int)x, MemoryProtectionFlags.ExecuteReadWrite, MemoryAllocationFlags.Reserve | MemoryAllocationFlags.Commit).ToUIntPtr();
                                        if (allocs[j].Address == UIntPtr.Zero)
                                            OutputDebugString(rsFailureToAllocateMemory + " 1");
                                        k -= 1;
                                    }
                                    if (allocs[j].Address == UIntPtr.Zero)
                                        allocs[j].Address = process.Alloc((int)x, MemoryProtectionFlags.ExecuteReadWrite, MemoryAllocationFlags.Reserve | MemoryAllocationFlags.Commit).ToUIntPtr();
                                    if (allocs[j].Address == UIntPtr.Zero)
                                        OutputDebugString(rsFailureToAllocateMemory + " 2");
                                    //adjust the addresses of entries that are part of this block
                                    for (k = j + 1; k <= i - 1; k++)
                                        allocs[k].Address = (UIntPtr)(allocs[k - 1].Address.ToUInt64() + allocs[k - 1].Size);
                                    x = 0;
                                }
                                //new preferred address
                                j = i;
                                prefered = allocs[i].Preferred;
                            }
                        }
                        //no preferred location specified, OR same preferred location
                        x += allocs[i].Size;
                    }
                    if (x > 0)
                    {
                        //adjust the address of entries that are part of this final block
                        k = 10;
                        allocs[j].Address = UIntPtr.Zero;
                        while (k > 0 && allocs[j].Address == UIntPtr.Zero)
                        {
                            i = 0;
                            prefered = process.FindFreeBlockForRegion(prefered, (UInt32)x);
                            allocs[j].Address = process.Alloc(prefered.ToIntPtr(), (int)x, MemoryProtectionFlags.ExecuteReadWrite, MemoryAllocationFlags.Reserve | MemoryAllocationFlags.Commit).ToUIntPtr();
                            if (allocs[j].Address == UIntPtr.Zero)
                                OutputDebugString(rsFailureToAllocateMemory + " 3");
                            k -= 1;
                        }
                        if (allocs[j].Address == UIntPtr.Zero)
                            allocs[j].Address = process.Alloc((int)x, MemoryProtectionFlags.ExecuteReadWrite, MemoryAllocationFlags.Reserve | MemoryAllocationFlags.Commit).ToUIntPtr();
                        if (allocs[j].Address == UIntPtr.Zero)
                            throw new Exception(rsFailureToAllocateMemory + " 4");
                        for (i = j + 1; i <= allocs.Length - 1; i++)
                            allocs[i].Address = (UIntPtr)(allocs[i - 1].Address.ToUInt64() + allocs[i - 1].Size);
                    }
                }
                #endregion
                //-----------------------2nd pass------------------------
                //assembler lines only contains label specifiers and assembler instructions
                assembled.SetLength(0);
                for (i = 0; i < assemblerlines.Length; i++)
                {
                    currentline = assemblerlines[i];
                    Tokenize(currentline, tokens);
                    //if alloc then replace with the address
                    for (j = 0; j < allocs.Length; j++)
                        currentline = ReplaceToken(currentline, allocs[j].Name, AStringUtils.IntToHex(allocs[j].Address, 8));
                    //if kalloc then replace with the address
                    for (j = 0; j < kallocs.Length; j++)
                        currentline = ReplaceToken(currentline, kallocs[j].Name, AStringUtils.IntToHex(kallocs[j].Address, 8));
                    for (j = 0; j < defines.Length; j++)
                        currentline = ReplaceToken(currentline, defines[j].Name, defines[j].Whatever);
                    ok1 = false;
                    if (currentline[currentline.Length - 1] != ':') //if it's not a definition then
                    {
                        for (j = 0; j < labels.Length; j++)
                        {
                            if (!TokenCheck(currentline, labels[j].Name))
                                continue;
                            if (!labels[j].Defined)
                            {
                                //the address hasn't been found yet
                                //this is the part that causes those nops after a short jump below the current instruction
                                //close
                                s1 = ReplaceToken(currentline, labels[j].Name, AStringUtils.IntToHex(currentaddress, 8));
                                //far and big
                                if (process.IsX64)  //and not in region
                                    currentline = ReplaceToken(currentline, labels[j].Name, AStringUtils.IntToHex(currentaddress + 0xfffff, 8));
                                else
                                    currentline = ReplaceToken(currentline, labels[j].Name, AStringUtils.IntToHex(currentaddress + 0xfffff, 8));
                                assembled.SetLength(assembled.Length + 1);
                                assembled.Last.Address = currentaddress;
                                Assembler.Assemble(currentline, currentaddress.ToUInt64(), assembled[assembled.Length - 1].Bytes, AAssemblerPreference.apnone, true);
                                a = assembled[assembled.Length - 1].Bytes.Length;
                                Assembler.Assemble(s1, currentaddress.ToUInt64(), assembled[assembled.Length - 1].Bytes, AAssemblerPreference.apnone, true);
                                b = assembled[assembled.Length - 1].Bytes.Length;
                                if (a > b)  //pick the biggest one
                                    Assembler.Assemble(currentline, currentaddress.ToUInt64(), assembled[assembled.Length - 1].Bytes);
                                labels[j].References.SetLength(labels[j].References.Length + 1);
                                labels[j].References[labels[j].References.Length - 1] = (Byte)(assembled.Length - 1); // todo is this maent to be a object?
                                labels[j].References2.SetLength(labels[j].References2.Length + 1);
                                labels[j].References2[labels[j].References2.Length - 1] = (Byte)i; // todo is this maent to be a object?
                                currentaddress += assembled[assembled.Length - 1].Bytes.Length;
                                ok1 = true;
                            }
                            else
                                currentline = ReplaceToken(currentline, labels[j].Name, AStringUtils.IntToHex(labels[j].Address, 8));
                            break;
                        }
                    }
                    if (ok1)
                        continue;
                    if (currentline[currentline.Length - 1] == ':')
                    {
                        ok1 = false;
                        for (j = 0; j < labels.Length; j++)
                        {
                            if (i == labels[j].AssemblerLine)
                            {
                                labels[j].Address = currentaddress;
                                labels[j].Defined = true;
                                ok1 = true;
                                //reassemble the instructions that had no target
                                for (k = 0; k < labels[j].References.Length; k++)
                                {
                                    a = assembled[labels[j].References[k]].Bytes.Length; //original size of the assembled code
                                    s1 = ReplaceToken(assemblerlines[labels[j].References2[k]], labels[j].Name, AStringUtils.IntToHex(labels[j].Address, 8));
                                    if (Assembler.Is64Bit & process.IsX64)
                                        Assembler.Assemble(s1, assembled[labels[j].References[k]].Address.ToUInt64(), assembled[labels[j].References[k]].Bytes);
                                    else
                                        Assembler.Assemble(s1, assembled[labels[j].References[k]].Address.ToUInt64(), assembled[labels[j].References[k]].Bytes, AAssemblerPreference.aplong);
                                    b = assembled[labels[j].References[k]].Bytes.Length; //new size
                                    assembled[labels[j].References[k]].Bytes.SetLength(a); //original size (original size is always bigger or equal than newsize)
                                                                                            //fill the difference with nops (not the most efficient approach, but it should work)
                                    for (l = b; l < a; l++)
                                        assembled[labels[j].References[k]].Bytes[l] = 0x90;
                                }
                                break;
                            }
                        }
                        if (ok1)
                            continue;
                        try
                        {
                            currentaddress = symHandler.GetAddressFromName(AStringUtils.Copy(currentline, 0, currentline.Length - 1));
                            continue; //next line
                        }
                        catch
                        {
                            throw new Exception(rsThisAddressSpecifierIsNotValid);
                        }
                    }
                    assembled.SetLength(assembled.Length + 1);
                    assembled[assembled.Length - 1].Address = currentaddress;
                    Assembler.Assemble(currentline, currentaddress.ToUInt64(), assembled[assembled.Length - 1].Bytes);
                    currentaddress += assembled[assembled.Length - 1].Bytes.Length;
                }
                //end of loop
                ok2 = true;
                // unprotect memory
                for (i = 0; i < fullaccess.Length; i++)
                {
                    if (createScript)
                        newScript.Add("FullAccess " + AStringUtils.IntToHex(fullaccess[i].Address, intPtrHexSize) + " " + fullaccess[i].Size);
                    else
                        process.FullAccess((IntPtr)fullaccess[i].Address.ToUInt64(), (int)fullaccess[i].Size);
                }
                #region Load Binaries -- todo
                //load binaries
                //if (length(loadbinary) > 0)
                //{
                //    for (i = 0; i <= length(loadbinary) - 1; i++)
                //    {
                //        ok1 = true;
                //        //try
                //        testPtr = symHandler.getAddressFromName(loadbinary[i].address);
                //        //except
                //        ok1 = false;
                //        //end;
                //
                //        if (~ok1)
                //            for (j = 0; j <= length(labels) - 1; j++)
                //                if (uppercase(labels[j].labelname) == uppercase(loadbinary[i].address))
                //                {
                //                    ok1 = true;
                //                    testPtr = labels[j].address;
                //                    flush();
                //                }
                //
                //        if (~ok1)
                //            for (j = 0; j <= length(allocs) - 1; j++)
                //                if (uppercase(allocs[j].varname) == uppercase(loadbinary[i].address))
                //                {
                //                    ok1 = true;
                //                    testPtr = allocs[j].address;
                //                    flush();
                //                }
                //
                //        if (~ok1)
                //            for (j = 0; j <= length(kallocs) - 1; j++)
                //                if (uppercase(kallocs[j].varname) == uppercase(loadbinary[i].address))
                //                {
                //                    ok1 = true;
                //                    testPtr = kallocs[j].address;
                //                    flush();
                //                }
                //
                //        if (~ok1)
                //            for (j = 0; j <= length(defines) - 1; j++)
                //                if (uppercase(defines[j].name) == uppercase(loadbinary[i].address))
                //                {
                //                    //try
                //                    testPtr = symHandler.getAddressFromName(defines[j].whatever);
                //                    ok1 = true;
                //                    //except
                //                    //end;
                //
                //                    flush();
                //                }
                //
                //        if (ok1)
                //        {
                //            binaryfile = tmemorystream.Create;
                //            //try
                //            binaryfile.LoadFromFile(loadbinary[i].filename);
                //            // UBERFOX BELOW -- convert binary file into pokes
                //            if (createScript)
                //            {
                //                newScript.Add(Concat("Poke ", IntToHex(testPtr, intPtrHexSize), ' ', BinToStr(PointerToByteArray(binaryfile.Memory, binaryfile.Size))));
                //                //newScript.Add(Concat('PokeFile ', IntToHex(testPtr, intPtrHexSize), ' ', loadbinary[i].filename));
                //                ok1 = true;
                //            }
                //            else
                //            {
                //                ok2 = writeprocessmemory(processHandle, UIntToPtr(testPtr), binaryfile.Memory, binaryfile.Size, bw2);
                //            }
                //            // UBERFOX ABOVE
                //            //finally
                //            binaryfile.free;
                //            //end;
                //        }
                //    }
                //}
                #endregion
                //we're still here so, inject it
                for (i = 0; i < assembled.Length; i++)
                {
                    testPtr = assembled[i].Address;
                    if (createScript)
                    {
                        newScript.Add("Poke " + AStringUtils.IntToHex(testPtr, intPtrHexSize) + " " + AStringUtils.BinToHexStr(assembled[i].Bytes.Raw));
                        ok1 = true;
                        ok2 = true;
                    }
                    else
                    {
                        ok1 = process.FullAccess((IntPtr)testPtr.ToUInt64(), assembled[i].Bytes.Length);
                        ok2 = process.WriteMem((IntPtr)testPtr.ToUInt64(), assembled[i].Bytes.Raw) == assembled[i].Bytes.Length;
                    }
                    if (!ok1)
                        ok2 = false;
                }
                if (!ok2)
                {
                    if (popUpMessages)
                        MessageBox.Show(rsNotAllInstructionsCouldBeInjected);
                }
                else
                {
                    //if ceallocarray<>nil then
                    {
                        //see if all allocs are deallocated
                        if (dealloc.Length == allocArray.Length)  //free everything
                        {
                            if (Assembler.Is64Bit)
                                baseaddress = (UIntPtr)0xFFFFFFFFFFFFFFFF;
                            else
                                baseaddress = (UIntPtr)0xffffffff;
                            for (i = 0; i < allocArray.Length; i++)
                            {
                                if (allocArray[i].Address.ToUInt64() < baseaddress.ToUInt64())
                                    baseaddress = allocArray[i].Address;
                            }
                            process.Free(baseaddress.ToIntPtr());
                        }
                        allocArray.SetLength(allocs.Length);
                        for (i = 0; i < allocs.Length; i++)
                            allocArray[i] = allocs[i];
                    }
                    //check the addsymbollist array and deletesymbollist array
                    //first delete
                    for (i = 0; i < deletesymbollist.Length; i++)
                        symHandler.DeleteUserDefinedSymbol(deletesymbollist[i]);
                    //now scan the addsymbollist array and add them to the userdefined list
                    for (i = 0; i < addsymbollist.Length; i++)
                    {
                        ok1 = false;
                        for (j = 0; j < allocs.Length; j++)
                        {
                            if (String.Equals(addsymbollist[i], allocs[j].Name, StringComparison.CurrentCultureIgnoreCase))
                            {
                                symHandler.DeleteUserDefinedSymbol(addsymbollist[i]); //delete old one so you can add the new one
                                symHandler.AddUserDefinedSymbol(AStringUtils.IntToHex(allocs[j].Address, 8), addsymbollist[i]);
                                ok1 = true;
                                break;
                            }
                        }
                        if (!ok1)
                        {
                            for (j = 0; j < labels.Length; j++)
                            {
                                if (String.Equals(addsymbollist[i], labels[j].Name, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    symHandler.DeleteUserDefinedSymbol(addsymbollist[i]); //delete old one so you can add the new one
                                    symHandler.AddUserDefinedSymbol(AStringUtils.IntToHex(labels[j].Address, 8), addsymbollist[i]);
                                    ok1 = true;
                                }
                            }
                        }
                        if (!ok1)
                        {
                            for (j = 0; j < defines.Length; j++)
                            {
                                if (String.Equals(addsymbollist[i], defines[j].Name, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    symHandler.DeleteUserDefinedSymbol(addsymbollist[i]); //delete old one so you can add the new one
                                    symHandler.AddUserDefinedSymbol(defines[j].Whatever, addsymbollist[i]);
                                    ok1 = true;
                                }
                            }
                        }
                    }
                    #region Create Threads -- todo
                    //still here, so create threads if needed
                    //if (length(createthread) > 0)
                    //{
                    //    for (i = 0; i <= length(createthread) - 1; i++)
                    //    {
                    //        ok1 = true;
                    //        try
                    //        {
                    //            testPtr = symHandler.getAddressFromName(createthread[i]);
                    //        }
                    //        catch
                    //        {
                    //            ok1 = false;
                    //        }
                    //        if (!ok1)
                    //        {
                    //            for (j = 0; j <= length(labels) - 1; j++)
                    //            {
                    //                if (uppercase(labels[j].labelname) == uppercase(createthread[i]))
                    //                {
                    //                    ok1 = true;
                    //                    testPtr = labels[j].address;
                    //                    flush();
                    //                }
                    //            }
                    //        }
                    //        if (!ok1)
                    //        {
                    //            for (j = 0; j <= length(allocs) - 1; j++)
                    //            {
                    //                if (uppercase(allocs[j].varname) == uppercase(createthread[i]))
                    //                {
                    //                    ok1 = true;
                    //                    testPtr = allocs[j].address;
                    //                    flush();
                    //                }
                    //            }
                    //        }
                    //        if (!ok1)
                    //        {
                    //            for (j = 0; j <= length(kallocs) - 1; j++)
                    //            {
                    //                if (uppercase(kallocs[j].varname) == uppercase(createthread[i]))
                    //                {
                    //                    ok1 = true;
                    //                    testPtr = kallocs[j].address;
                    //                    flush();
                    //                }
                    //            }
                    //        }
                    //        if (!ok1)
                    //        {
                    //            for (j = 0; j <= length(defines) - 1; j++)
                    //            {
                    //                if (uppercase(defines[j].name) == uppercase(createthread[i]))
                    //                {
                    //                    testPtr = symHandler.getAddressFromName(defines[j].whatever);
                    //                    ok1 = true;
                    //                    flush();
                    //                }
                    //            }
                    //        }
                    //        if (ok1)  //address found
                    //        {
                    //            //try
                    //            // UBERFOX BELOW -- do nothing -- ignore creating threads
                    //            if (createScript)
                    //            {
                    //                ok2 = true;
                    //            }
                    //            else
                    //            {
                    //                ok2 = createremotethread(processHandle, nil, 0, UIntToPtr(testPtr), nil, 0, bw) > 0;
                    //            }
                    //            // UBERFOX ABOVE
                    //            //finally
                    //            //end;
                    //        }
                    //    }
                    //}
                    #endregion
                    if (popUpMessages)
                    {
                        s1 = "";
                        for (i = 0; i < globalallocs.Length; i++)
                            s1 = s1 + "\r\n" + globalallocs[i].Name + '=' + AStringUtils.IntToHex(globalallocs[i].Address, 8);
                        for (i = 0; i < allocs.Length; i++)
                            s1 = s1 + "\r\n" + allocs[i].Name + '=' + AStringUtils.IntToHex(allocs[i].Address, 8);
                        if (kallocs.Length > 0)
                        {
                            s1 = "\r\n" + rsTheFollowingKernelAddressesWhereAllocated + ':';
                            for (i = 0; i < kallocs.Length; i++)
                                s1 = s1 + "\r\n" + kallocs[i].Name + '=' + AStringUtils.IntToHex(kallocs[i].Address, 8);
                        }
                        MessageBox.Show(rsTheCodeInjectionWasSuccessfull + s1);
                    }
                }
                result = ok2;
            }
            finally
            {
                for (i = 0; i < assembled.Length; i++)
                    assembled[i].Bytes.SetLength(0);
                assembled.SetLength(0);
                tokens.Clear();
            }
            return result;
        }
        #endregion
        #region OutputDebugString
        private void OutputDebugString(String msg)
        {
            Console.WriteLine("Debug: " + msg);
        }
        #endregion
    }
}
