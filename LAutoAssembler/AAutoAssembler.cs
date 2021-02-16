using System;
using System.Windows.Forms;
using Sputnik.LDateTime;
using Sputnik.LFileSystem;
using Sputnik.LString;
using Sputnik.LUtils;
using SputnikAsm.LAssembler;
using SputnikAsm.LAssembler.LEnums;
using SputnikAsm.LAutoAssembler.LCollections;
using SputnikAsm.LAutoAssembler.LEnums;
using SputnikAsm.LCollections;
using SputnikAsm.LDisassembler;
using SputnikAsm.LExtensions;
using SputnikAsm.LGenerics;
using SputnikAsm.LProcess;
using SputnikAsm.LProcess.LNative.LTypes;
using SputnikAsm.LProcess.Utilities;
using SputnikAsm.LString;
using SputnikAsm.LUtils;

namespace SputnikAsm.LAutoAssembler
{
    public class AAutoAssembler
    {
        #region Internal Classes
        #region AAssemblerLine
        internal class AAssemblerLine
        {
            #region Variables
            public String Line;
            public int LineNr;
            #endregion
            #region Constructor
            public AAssemblerLine()
            {
                Line = "";
                LineNr = 0;
            }
            #endregion
        }
        #endregion
        #region ALabels
        internal class ALabels
        {
            #region Variables
            public String Name;
            public Boolean Defined;
            public Boolean AfterCCode;
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
                AfterCCode = false;
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
            public int CreateThreadAndWait;
            #endregion
            #region Constructor
            public AAssembled()
            {
                Address = UIntPtr.Zero;
                Bytes = new AByteArray();
                CreateThreadAndWait = 01;
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
        #region AReadMems
        internal class AReadMems
        {
            #region Variables
            public AByteArray Bytes;
            public int Length;
            #endregion
            #region Constructor
            public AReadMems()
            {
                Bytes = new AByteArray();
                Length = 0;
            }
            public AReadMems(AByteArray bytes, int length)
            {
                Bytes = bytes;
                Length = length;
            }
            #endregion
        }
        #endregion
        #region ATryElem
        internal class ATryElem
        {
            #region Variables
            public int LineNr;
            public int TryNr;
            public Boolean HasExcept;
            public String TryLabel;
            public String ExceptLabel;
            #endregion
            #region Constructor
            public ATryElem()
            {
                LineNr = 0;
                TryNr = 0;
                HasExcept = false;
                TryLabel = "";
                ExceptLabel = "";
            }
            #endregion
        }
        #endregion
        #region ACreateThreadAndWait
        internal class ACreateThreadAndWait
        {
            #region Variables
            public String Name;
            public int Position;
            public int Timeout;
            #endregion
            #region Constructor
            public ACreateThreadAndWait()
            {
                Name = "";
                Position = 0;
                Timeout = 0;
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
        public String rsWrongSyntaxReAssemble = "Wrong syntax. Reassemble(address)";
        public String rsWrongSyntaxAOBSCANREGION = "Wrong syntax. AOBSCANREGION(name, startaddress, stopaddress, 11 22 33 ** 55)";
        public String rsTheAddressInCreatethreadAndWaitIsNotValid = "The address in createthreadandwait(%s) is not valid";
        public String rsAAErrorInTheStructureDefinitionOf = "Error in the structure definition of %s at line %d";
        public String rsAAIsAReservedWord = "%s is a reserved word";
        public String rsAANoIdeaWhatXis = "No idea what %s is";
        public String rsAANoEndFound = "No end found";
        public String rsAATheArrayOfByteNamed = "The array of byte named %s could not be found";
        public String rsXCouldNotBeFound = "%s could not be found";
        public String rsAAErrorWhileSacnningForAobs = "Error while scanning for AOB's : ";
        public String rsAAError = "Error: ";
        public String rsAAModuleNotFound = "module not found:";
        public String rsAALuaErrorInTheScriptAtLine = "Lua error in the script at line ";
        public String rsGoTo = "Go to ";
        public String rsMissingExcept = "The {$TRY} at line %d has no matching {$EXCEPT}";
        public String rsNoPreferedRangeAllocWarning = "None of the ALLOC statements specify a "
                                    +"preferred address.  Did you take into account that the JMP instruction is"
                                    +" going to be 14 bytes long?";
        public String rsFailureAlloc = "Failure allocating memory near %.8x";

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
            var braceComment = false;
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
                            if ((braceComment && currentLine[j] == '}') || (!braceComment && currentLine[j] == '*' && j < currentLine.Length && currentLine[j + 1] == '/'))
                            {
                                inComment = false; //and continue parsing the code...
                                if (!braceComment)
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
                            if (currentLine[j] == '{' || (currentLine[j] == '/' && (j < currentLine.Length) && currentLine[j + 1] == '*'))
                            {
                                inComment = true;
                                braceComment = currentLine[j] == '{';
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
                        currentLine = "RandomLabel" + // todo remake this ffs
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
        #region GetEnableOrDisableScript
        public void GetEnableOrDisableScript(ARefStringArray code, ARefStringArray newScript, Boolean enableScript)
        {
            var insideEnable = false;
            var insideDisable = false;
            for (var i = 0; i < code.Length; i++)
            {
                switch (code[i].Value.Trim().ToUpper())
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
            var inQuote = false;
            var inQuote2 = false;
            for (var i = 0; i < input.Length; i++)
            {
                if (inQuote & (input[i] != '\''))
                    continue;
                if (inQuote2 & (input[i] != '"'))
                    continue;
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
                    switch (input[i])
                    {
                        case '\'':
                        {
                            if (inQuote)
                            {
                                if (a != -1)
                                    tokens.Add(AStringUtils.Copy(input, a, i - a), a);
                                a = -1;
                                inQuote = false;
                            }
                            else
                            {
                                inQuote = true;
                                a = i;
                            }
                            continue;
                        }
                        case '"':
                        {
                            if (inQuote2)
                            {
                                if (a != -1)
                                    tokens.Add(AStringUtils.Copy(input, a, i - a), a);
                                a = -1;
                                inQuote2 = false;
                            }
                            else
                            {
                                inQuote2 = true;
                                a = i;
                            }
                            continue;
                        }
                    }
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
            for (var i = tokens.Length - 1; i >= 0; i--)
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
        public void ReplaceStructWithDefines(ARefStringArray code, int lineNr)
        {
            var lastLineNr = lineNr;
            var endFound = false;
            var structName = AStringUtils.Copy(code[lineNr].Value, 7, code[lineNr].Length).Trim();
            var currentOffset = 0;
            var tokens = new ARefStringArray();
            var elements = new ARefStringArray();
            for (var i = lineNr + 1; i < code.Length; i++)
            {
                lastLineNr = i;
                TokenizeStruct(code[i].Value, tokens);
                var j = 0;
                if (tokens.Length > 0 && !String.IsNullOrEmpty(tokens[0].Value))
                {
                    //first check if it's a label definition
                    if (tokens[0].Value[tokens[0].Length - 1] == ':')
                    {
                        var elementName = AStringUtils.Copy(tokens[0].Value, 0, tokens[0].Length - 1);
                        if (Assembler.GetOpCodesIndex(elementName) != -1)
                            StructError(UStringUtils.Sprintf(rsAAIsAReservedWord, elementName), structName, lastLineNr + 1);
                        elements.Add(elementName, currentOffset);
                        j = 1;
                    }
                    //then check if it's the end of the struct
                    if (tokens[0].Value.ToUpper() == "ENDSTRUCT" || tokens[0].Value.ToUpper() == "ENDS")
                    {
                        endFound = true;
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
                                    var byteSize = 0;
                                    switch (tokens[j].Value[3])
                                    {
                                        case 'B':
                                            byteSize = 1;
                                            break;
                                        case 'W':
                                            byteSize = 2;
                                            break;
                                        case 'D':
                                            byteSize = 4;
                                            break;
                                        case 'Q':
                                            byteSize = 8;
                                            break;
                                        default:
                                            StructError(null, structName, lastLineNr + 1);
                                            break;
                                    }
                                    //now get the count
                                    j += 1;
                                    if (j >= tokens.Length)
                                        StructError(null, structName, lastLineNr + 1);
                                    currentOffset += byteSize * AStringUtils.StrToInt(tokens[j].Value);
                                }
                                else
                                    StructError(null, structName, lastLineNr + 1);
                            }
                            break;
                        case 'D':
                            {
                                //could be d* ?
                                if (tokens[j].Length == 2)
                                {
                                    var byteSize = 0;
                                    switch (tokens[j].Value[1])
                                    {
                                        case 'B':
                                            byteSize = 1;
                                            break;
                                        case 'W':
                                            byteSize = 2;
                                            break;
                                        case 'D':
                                            byteSize = 4;
                                            break;
                                        case 'Q':
                                            byteSize = 8;
                                            break;
                                        default:
                                            StructError(null, structName, lastLineNr + 1);
                                            break;
                                    }
                                    j += 1;
                                    if (j >= tokens.Length)
                                        StructError(null, structName, lastLineNr + 1);
                                    currentOffset += byteSize;
                                    //check if there are more ?'s after this (in case of dw ? ? ?)
                                    while (j < tokens.Length - 1)
                                    {
                                        if (tokens[j + 1].Value == "?")   //check from the spot in front
                                        {
                                            currentOffset += byteSize;
                                            j += 1;
                                        }
                                        else
                                            break; //nope
                                    }
                                }
                                else
                                    StructError(null, structName, lastLineNr + 1);
                            }
                            break;
                        default:
                            //we already dealt with labels, so this is wrong
                            StructError(UStringUtils.Sprintf(rsAANoIdeaWhatXis, tokens[j].Value), structName, lastLineNr + 1);
                            break;
                    }
                    j += 1; //next token
                }
            }
            if (endFound == false)
                StructError(rsAANoEndFound, structName, lastLineNr + 1);
            // the elements have been filled in, delete the structure (between linenr and lastlinenr)
            // and inject define(element,offset)
            // and define(structName.element,offset)
            for (var i = lastLineNr; i >= lineNr; i--)
                code.RemoveAt(i);
            for (var i = 0; i < elements.Length; i++)
            {
                code.Insert(lineNr, "define(" + elements[i].Value + ',' + AStringUtils.IntToHex(elements[i].Position, 1) + ')');
                code.Insert(lineNr, "define(" + structName + '.' + elements[i].Value + ',' + AStringUtils.IntToHex(elements[i].Position, 1) + ')');
            }
            code.Insert(lineNr, "define(" + structName + "_size," + AStringUtils.IntToHex(currentOffset, 1) + ')');
        }
        #endregion
        #region StructError
        private void StructError(String reason, String structName, int lineNr)
        {
            var error = UStringUtils.Sprintf(rsAAErrorInTheStructureDefinitionOf, structName, lineNr);
            if (!String.IsNullOrEmpty(reason))
                error = error + " :" + reason;
            else
                error += '.';
            throw new Exception(error);
        }
        #endregion
        #region GetPotentialLabels
        public void GetPotentialLabels(ARefStringArray code, AStringArray labels)
        {
            for (var i = 0; i < code.Length; i++)
            {
                var currentLine = code[i].Value.Trim();
                if (String.IsNullOrEmpty(currentLine) || (currentLine[currentLine.Length - 1] != ':'))
                    continue;
                if (AStringUtils.Pos("+", currentLine) == -1 && AStringUtils.Pos(".", currentLine) == -1)
                    labels.Add(AStringUtils.Copy(currentLine, 0, currentLine.Length - 1));
            }
        }
        #endregion
        #region ParseTryExcept
        public void ParseTryExcept(ARefStringArray code, AExceptionInfoArray exceptionList)
        {
            var tryList = new AArrayManager<ATryElem>();
            var tryNr = 0;
            tryList.SetLength(0);
            for (var i = 0; i < code.Length; i++)
            {
                if (code[i].Value.ToUpper().Trim() == "{$TRY}")
                {
                    tryNr += 1;
                    var j = tryList.Length;
                    tryList.SetLength(j + 1);
                    tryList[j].TryNr = tryNr;
                    tryList[j].HasExcept = false;
                    tryList[j].LineNr = code[i].Position;
                    tryList[j].TryLabel = "tryoperation_" + tryNr;
                    code[i].Value = tryList[j].TryLabel + ":";
                }
                if (code[i].Value.ToUpper().Trim() == "{$EXCEPT}")
                {
                    //find the last try that doesn't have an except filled in
                    var found = false;
                    for (var j = tryList.Length - 1; j >= 0; j--)
                    {
                        if (tryList[j].HasExcept == false)
                        {
                            tryList[j].HasExcept = true;
                            tryList[j].ExceptLabel = "tryoperation" + tryList[j].TryNr + "_except";
                            code[i].Value = tryList[j].ExceptLabel + ":";
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        throw new Exception(UStringUtils.Sprintf("Found an {$EXCEPT} at line %d with no matching {$TRY}", code[i].Position));
                }
            }
            exceptionList.SetLength(tryList.Length);
            for (var i = 0; i < tryList.Length; i++)
            {
                code.Insert(0, "label(" + tryList[i].TryLabel + ')');
                code.Insert(0, "label(" + tryList[i].ExceptLabel + ')');
                exceptionList[i].TryLabel = tryList[i].TryLabel;
                exceptionList[i].ExceptLabel = tryList[i].ExceptLabel;
                if (tryList[i].HasExcept == false)
                    throw new Exception(UStringUtils.Sprintf(rsMissingExcept, tryList[i].LineNr));
            }
        }
        #endregion
        #region StripCpuSpecificCode
        public void StripCpuSpecificCode(ARefStringArray code, Boolean strip32Bit)
        {
            var inExcludedBitBlock = false;
            for (var i = 0; i < code.Length; i++)
            {
                var s = code[i].Value.Trim().ToUpper();
                if (s == "[32-BIT]")
                {
                    if (strip32Bit)
                        inExcludedBitBlock = true;
                    code[i].Value = " ";
                }
                if (s == "[/32-BIT]")
                {
                    if (strip32Bit)
                        inExcludedBitBlock = false;
                    code[i].Value = " ";
                }
                if (s == "[64-BIT]")
                {
                    if (!strip32Bit)
                        inExcludedBitBlock = true;
                    code[i].Value = " ";
                }
                if (s == "[/64-BIT]")
                {
                    if (!strip32Bit)
                        inExcludedBitBlock = false;
                    code[i].Value = " ";
                }
                if (inExcludedBitBlock)
                    code[i].Value = " ";
            }
        }
        #endregion
        #region AobScans -- todo make this work!!!
        public Boolean AobScans(AProcessSharp process, ARefStringArray code, Boolean syntaxCheckOnly)
        {
            return false;
        }
        #endregion
        #region GetAddressFromScript
        private UIntPtr GetAddressFromScript(
            String name, 
            Boolean targetSelf, 
            AArrayManager<ALabels> labels, 
            AAllocArray allocs, 
            AAllocArray kallocs, 
            AArrayManager<ADefines> defines
            )
        {
            try
            {
                UIntPtr result;
                if (targetSelf)
                    result = AAsmTools.SelfSymbolHandler.GetAddressFromName(name);
                else
                    result = Assembler.SymbolHandler.GetAddressFromName(name);
                if (result != UIntPtr.Zero)
                    return result;
            }
            catch
            {
                // ignored
            }
            name = name.ToUpper();
            for (var j = 0; j < labels.Length; j++)
            {
                if (labels[j].Name.ToUpper() == name)
                    return labels[j].Address;
            }
            for (var j = 0; j < allocs.Length; j++)
            {
                if (allocs[j].Name.ToUpper() == name)
                    return allocs[j].Address;
            }
            for (var j = 0; j < kallocs.Length; j++)
            {
                if (kallocs[j].Name.ToUpper() == name)
                    return kallocs[j].Address;
            }
            for (var j = 0; j < defines.Length; j++)
            {
                if (defines[j].Name.ToUpper() == name)
                    return Assembler.SymbolHandler.GetAddressFromName(defines[j].Whatever);
            }
            return UIntPtr.Zero;
        }
        #endregion
        #region AutoAssemble
        public Boolean AutoAssemble(AProcessSharp process, ARefStringArray code, Boolean popUpMessages, Boolean enable, Boolean syntaxCheckOnly, Boolean targetSelf, ADisableInfo disableInfo, Boolean createScript, AScriptBytesArray scriptBytes)
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
                        GetEnableOrDisableScript(code, tempStrings, true);
                        break;
                    case false:
                        GetEnableOrDisableScript(code, tempStrings, false);
                        break;
                }
            }
            var strip32BitCode = process.IsX64;
            if (targetSelf)
                strip32BitCode = Assembler.Is64Bit;
            if (strip32BitCode)
                StripCpuSpecificCode(tempStrings, true);
            var result = AutoAssemble2(process, tempStrings, popUpMessages, syntaxCheckOnly, targetSelf, disableInfo, createScript, scriptBytes);
            return result;
        }
        #endregion
        #region AutoAssemble2
        public Boolean AutoAssemble2(AProcessSharp process, ARefStringArray code, Boolean popUpMessages, Boolean syntaxCheckOnly, Boolean targetSelf, ADisableInfo disableInfo, Boolean createScript, AScriptBytesArray scriptBytes)
        {
            var currentLine = "";
            var currentLineNr = 0;
            var nops = new AByteArray();
            var currentAddress = UIntPtr.Zero;
            int i;
            int j;
            int k;
            int l;
            Boolean ok1;
            Boolean ok2;
            var assembled = new AArrayManager<AAssembled>();
            var loadBinary = new AArrayManager<ALoadLibrary>();
            var readMems = new AArrayManager<AReadMems>();
            var globalAllocs = new AAllocArray();
            var allocs = new AAllocArray();
            var kAllocs = new AAllocArray();
            var sAllocs = new AAllocArray();
            var labels = new AArrayManager<ALabels>();
            var defines = new AArrayManager<ADefines>();
            var fullAccess = new AArrayManager<AFullAccess>();
            var dealloc = new AArrayManager<UIntPtr>();
            var addSymbolList = new AStringArray();
            var deleteSymbolList = new AStringArray();
            var createThread = new AStringArray();
            var createThreadAndWait = new AArrayManager<ACreateThreadAndWait>();
            var assemblerLines = new AArrayManager<AAssemblerLine>();
            var exceptionList = new AExceptionInfoArray();
            var potentialLabels = new AStringArray();
            var hasTryExcept = false;
            var result = false;
            //add all symbols as defines
            if (disableInfo != null)
            {
                defines.SetLength(disableInfo.AllSymbols.Length);
                for (i = 0; i < disableInfo.AllSymbols.Length; i++)
                {
                    defines[i].Name = disableInfo.AllSymbols[i].Value;
                    defines[i].Whatever = AStringUtils.IntToHex(disableInfo.AllSymbols[i].Pointer, 8);
                }
            }
            var symHandler = Assembler.SymbolHandler;
            symHandler.Process = process;
            symHandler.WaitForSymbolsLoaded();
            //2 pass scanner
            try
            {
                assembled.SetLength(1);
                readMems.SetLength(0);
                kAllocs.SetLength(0);
                allocs.SetLength(0);
                dealloc.SetLength(0);
                assemblerLines.SetLength(0);
                fullAccess.SetLength(0);
                addSymbolList.SetLength(0);
                deleteSymbolList.SetLength(0);
                defines.SetLength(0);
                loadBinary.SetLength(0);
                exceptionList.SetLength(0);
                defines.SetLength(0);
                var tokens = new ARefStringArray();
                // todo fill this in for additional stuff
                //if (!targetSelf)
                //    for (i = 0; i < autoassemblerprologues.Length; i++)
                //        if (assigned(autoassemblerprologues[i]))
                //            autoassemblerprologues[i](code, syntaxcheckonly);
                //
                //luacode(code, syntaxcheckonly, memrec); //replaces {$lua}/{$asm} blocks with the output of those functions
                //autoassemblercodepass1(code, dataforaacodepass2, syntaxcheckonly, targetself); //replaces the {$luacode} and {$ccode} blocks with a call to extra routines added to the script
                //still here
                //one more time getting rid of {$ASM} lines that have been added while they shouldn't be required
                for (i = 0; i < code.Length; i++)
                {
                    if (code[i].Value.Trim().ToUpper() == "{$ASM}")
                        code[i].Value = "";
                }
                var strictMode = false;
                for (i = 0; i < code.Length; i++)
                {
                    currentLine = code[i].Value.Trim().ToUpper();
                    if (currentLine == "{$STRICT}")
                        strictMode = true;
                    if (currentLine == "{$TRY}")
                        hasTryExcept = true;
                }
                if (hasTryExcept)
                    ParseTryExcept(code, exceptionList);
                RemoveComments(code); //also trims each line
                UnlabeledLabels(code);
                if (!strictMode)
                    GetPotentialLabels(code, potentialLabels);
                //6.3: do the aobscans first
                //this will break scripts that use define(state,33) aobscan(name, 11 22 state 44 55), but really, live with it
                var usesAobScan = AobScans(process, code, syntaxCheckOnly);
                // todo fil this in
                // if not targetself then
                // for i:= 0 to length(AutoAssemblerProloguesPostAOBSCAN) - 1 do
                //     if assigned(AutoAssemblerProloguesPostAOBSCAN[i]) then
                // AutoAssemblerProloguesPostAOBSCAN[i](code, syntaxcheckonly);
                //first pass
                i = 0;
                while (i < code.Length)
                {
                    try
                    {
                        try
                        {
                            currentLine = code[i].Value;
                            currentLineNr = code[i].Position;
                            //check if useless
                            if (currentLine.Length == 0)
                                continue;
                            if (AStringUtils.Copy(currentLine, 0, 2) == "//")
                                continue; //skip
                            //do this first. Do not touch register symbol with any kind of define/label/whatsoever
                            #region Command REGISTERSYMBOL()
                            if (AStringUtils.Copy(currentLine, 0, 15).ToUpper() == "REGISTERSYMBOL(")
                            {
                                //add this symbol to the register symbol list
                                var a = AStringUtils.Pos("(", currentLine);
                                var b = AStringUtils.Pos(")", currentLine);
                                if (a != -1 && b != -1)
                                {
                                    var s1 = AStringUtils.Copy(currentLine, a + 1, b - a - 1).Trim();
                                    var sList = s1.Split(',', ' ');
                                    for (var sli = 0; sli < sList.Length; sli++)
                                    {
                                        s1 = sList[sli];
                                        addSymbolList.SetLength(addSymbolList.Length + 1);
                                        addSymbolList.Last = s1;
                                    }
                                    disableInfo?.RegisteredSymbols?.Add(s1);
                                }
                                else
                                    throw new Exception(rsSyntaxError);
                                assemblerLines.SetLength(assemblerLines.Length - 1);
                                continue;
                            }
                            #endregion
                            #region Command DEFINE()
                            //apply defines (before DEFINE since define(bla, 123) and define(xxx, bla+123) should work
                            //also, do not touch define with any previous define
                            if (AStringUtils.Copy(currentLine, 0, 7).ToUpper() == "DEFINE(")
                            {
                                //syntax: define(x,whatever)    x=variable name size=bytes
                                //allocate memory
                                var a = AStringUtils.Pos("(", currentLine);
                                var b = AStringUtils.Pos(",", currentLine);
                                var c = AStringUtils.RPos(")", currentLine);
                                if (a != -1 && b != -1 && c != -1)
                                {
                                    var s1 = AStringUtils.Copy(currentLine, a + 1, b - a - 1).Trim();
                                    var s2 = AStringUtils.Copy(currentLine, b + 1, c - b - 1).Trim();
                                    //apply earlier defines to the second part
                                    for (j = 0; j < defines.Length; j++)
                                        s2 = ReplaceToken(s2, defines[j].Name, defines[j].Whatever);
                                    ok1 = true;
                                    for (j = 0; j < defines.Length; j++)
                                    {
                                        if (!String.Equals(defines[j].Name, s1, StringComparison.CurrentCultureIgnoreCase))
                                            continue;
                                        //redefined from here on
                                        ok1 = false;
                                        defines[defines.Length - 1].Whatever = s2;
                                    }
                                    if (ok1)  //not duplicate, create it
                                    {
                                        defines.SetLength(defines.Length + 1);
                                        defines.Last.Name = s1;
                                        defines.Last.Whatever = s2;
                                    }
                                    continue;
                                }
                                else
                                    throw new Exception(rsWrongSyntaxDEFINENameWhatever + " Got " + currentLine);
                            }
                            //normal loop code
                            for (j = 0; j < defines.Length; j++)
                                currentLine = ReplaceToken(currentLine, defines[j].Name, defines[j].Whatever);
                            assemblerLines.SetLength(assemblerLines.Length + 1);
                            assemblerLines.Last.LineNr = currentLineNr;
                            assemblerLines.Last.Line = currentLine;
                            #endregion
                            //if the newline is empty then it has been handled and the plugin doesn't want it to be added for phase2
                            if (currentLine.Length == 0)
                            {
                                assemblerLines.SetLength(assemblerLines.Length - 1);
                                continue;
                            }
                            //otherwise it hasn't been handled, or it has been handled and the string is a compatible string that passes the phase1 tests (so variable names converted to 00000000 and whatever else is needed)
                            //plugins^^^
                            #region Command ASSERT()
                            if (AStringUtils.Copy(currentLine, 0, 7).ToUpper() == "ASSERT(")  //assert(address,aob)
                            {
                                if (!syntaxCheckOnly)
                                {
                                    var a = AStringUtils.Pos("(", currentLine);
                                    var b = AStringUtils.Pos(",", currentLine);
                                    var c = AStringUtils.Pos(")", currentLine);
                                    if (a != -1 && b != -1 && c != -1)
                                    {
                                        var s1 = AStringUtils.Copy(currentLine, a + 1, b - a - 1).Trim(); //address
                                        var s2 = AStringUtils.Copy(currentLine, b + 1, c - b - 1).Trim(); //aob
                                        if (createScript)
                                            continue;
                                        var testPtr = symHandler.GetAddressFromName(s1, false);
                                        var bytes = new ATByteArray();
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
                                            var byteBuf = process.Memory.Read(testPtr.ToIntPtr(), bytes.Length);
                                            if (byteBuf.Length == bytes.Length)
                                            {
                                                for (j = 0; j < bytes.Length; j++)
                                                {
                                                    if (bytes[j] != byteBuf[j])
                                                    {
                                                        if (AMathUtils.InRange(bytes[j], Byte.MinValue, Byte.MaxValue))
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
                                assemblerLines.SetLength(assemblerLines.Length - 1);
                                continue;
                            }
                            #endregion
                            #region Command SHAREDALLOC() -- todo
                            /*
                            if (uppercase(copy(currentline, 1, 12)) == "SHAREDALLOC(")
                            {
                                var a = pos("(", currentline);
                                var b = pos(",", currentline);
                                var c = pos(")", currentline);
                                if (a != -1 && b != -1 && c != -1)
                                {
                                    var s1 = trim(copy(currentline, a + 1, b - a - 1));
                                    var s2 = trim(copy(currentline, b + 1, c - b - 1));
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
                                    continue;
                                }
                                else
                                    Create(rsWrongSyntaxSHAREDALLOCNameSize);
                            }
                            */
                            #endregion
                            #region Command GLOBALALLOC()
                            if (AStringUtils.Copy(currentLine, 0, 12).ToUpper() == "GLOBALALLOC(")
                            {
                                var a = AStringUtils.Pos("(", currentLine);
                                var b = AStringUtils.Pos(",", currentLine);
                                var c = AStringUtils.PosEx(",", currentLine, b + 1);
                                var d = AStringUtils.Pos(")", currentLine);
                                if (a != -1 && b != -1 && d != -1)
                                {
                                    var s1 = AStringUtils.Copy(currentLine, a + 1, b - a - 1).Trim();
                                    String s2, s3;
                                    if (c != -1)
                                    {
                                        s2 = AStringUtils.Copy(currentLine, b + 1, c - b - 1).Trim();
                                        s3 = AStringUtils.Copy(currentLine, c + 1, d - c - 1).Trim();
                                    }
                                    else
                                    {
                                        s2 = AStringUtils.Copy(currentLine, b + 1, d - b - 1).Trim();
                                        s3 = "";
                                    }
                                    var x = 0u;
                                    try
                                    {
                                        x = AStringUtils.StrToDWord(s2);
                                    }
                                    catch
                                    {
                                        throw new Exception(UStringUtils.Sprintf(rsIsNotAValidSize, s2));
                                    }
                                    //define it here already
                                    if (s3 != "")
                                        Assembler.SymbolHandler.SetUserDefinedSymbolAllocSize(s1, x, Assembler.SymbolHandler.GetAddressFromName(s3));
                                    else
                                        Assembler.SymbolHandler.SetUserDefinedSymbolAllocSize(s1, x);
                                    globalAllocs.Inc();
                                    globalAllocs.Last.Address = Assembler.SymbolHandler.GetUserDefinedSymbolByName(s1);
                                    globalAllocs.Last.Name = s1;
                                    globalAllocs.Last.Size = x;
                                    assemblerLines.Dec();
                                    continue;
                                }
                                else
                                    throw new Exception(rsWrongSyntaxGLOBALALLOCNameSize);
                            }
                            #endregion
                            #region Command INCLUDE()
                            if (AStringUtils.Copy(currentLine, 0, 8).ToUpper() == "INCLUDE(")
                            {
                                var a = AStringUtils.Pos("(", currentLine);
                                var b = AStringUtils.Pos(")", currentLine);
                                if (a != -1 && b != -1)
                                {
                                    var s1 = AStringUtils.Copy(currentLine, a + 1, b - a - 1).Trim();
                                    if (String.IsNullOrEmpty(UIo.Path.GetExtension(s1)))
                                        s1 += ".cea";
                                    if (!UIo.File.Exists(s1))
                                        throw new Exception(UStringUtils.Sprintf(rsCouldNotBeFound, s1));
                                    var include = new ARefStringArray();
                                    include.AddRange(UIo.File.ReadAllLines(s1));
                                    RemoveComments(include);
                                    UnlabeledLabels(include);
                                    for (j = i + 1; j <= (i + 1) + (include.Length - 1); j++)
                                        code.Insert(j, include[j - (i + 1)]);
                                    assemblerLines.SetLength(assemblerLines.Length - 1);
                                    continue;
                                }
                                else
                                    throw new Exception(rsWrongSyntaxIncludeFilenameCea);
                            }
                            #endregion
                            #region Command CREATETHREAD()
                            if (AStringUtils.Copy(currentLine, 0, 13).ToUpper() == "CREATETHREAD(")
                            {
                                var a = AStringUtils.Pos("(", currentLine);
                                var b = AStringUtils.Pos(")", currentLine);
                                if (a != -1 && b != -1)
                                {
                                    var s1 = AStringUtils.Copy(currentLine, a + 1, b - a - 1).Trim();
                                    createThread.SetLength(createThread.Length + 1);
                                    createThread.Last = s1;

                                    assemblerLines.SetLength(assemblerLines.Length - 1);
                                    continue;
                                }
                                else
                                    throw new Exception(rsWrongSyntaxCreateThreadAddress);
                            }
                            #endregion
                            #region Command CREATETHREADANDWAIT()
                            if (AStringUtils.Copy(currentLine, 0, 20).ToUpper() == "CREATETHREADANDWAIT(")
                            {
                                var a = AStringUtils.Pos("(", currentLine);
                                var b = AStringUtils.Pos(")", currentLine);
                                if (a != -1 && b != -1)
                                {
                                    var s1 = AStringUtils.Copy(currentLine, a + 1, b - a - 1).Trim();
                                    var sList = s1.Split(',');
                                    if (sList.Length == 2)
                                    {
                                        try
                                        {
                                            j = AStringUtils.StrToInt(sList[1].Trim());
                                        }
                                        catch
                                        {
                                            throw new Exception("Invalid timeout for CreateThreadAndWait");
                                        }
                                    }
                                    else
                                        j = 5000;
                                    createThreadAndWait.SetLength(createThreadAndWait.Length + 1);
                                    createThreadAndWait.Last.Name = sList[0];
                                    createThreadAndWait.Last.Position = assemblerLines.Length - 1;
                                    createThreadAndWait.Last.Timeout = j;
                                    assemblerLines.SetLength(assemblerLines.Length - 1);
                                    continue;
                                }
                                else
                                    throw new Exception(rsWrongSyntaxCreateThreadAddress);
                            }
                            #endregion
                            #region Command READMEM()
                            if (AStringUtils.Copy(currentLine, 0, 8).ToUpper() == "READMEM(")
                            {
                                //read memory and place it here (readmem(address,size) )
                                var a = AStringUtils.Pos("(", currentLine);
                                var b = AStringUtils.Pos(",", currentLine);
                                var c = AStringUtils.Pos(")", currentLine);
                                if (a != -1 && b != -1 && c != -1)
                                {
                                    var s1 = AStringUtils.Copy(currentLine, a + 1, b - a - 1).Trim();
                                    var s2 = AStringUtils.Copy(currentLine, b + 1, c - b - 1).Trim();
                                    //read memory and replace with lines of DB xx xx xx xx xx xx xx xx
                                    var testPtr = symHandler.GetAddressFromName(s1);
                                    a = AStringUtils.StrToInt(s2);
                                    if (a == 0)
                                        throw new Exception(rsInvalidSizeForReadMem);
                                    Byte[] byteBuf;
                                    if (!syntaxCheckOnly)
                                    {
                                        byteBuf = process.Memory.Read(testPtr.ToIntPtr(), a);
                                        if (byteBuf.Length <= 0 | (byteBuf.Length < a))
                                            throw new Exception(UStringUtils.Sprintf(rsTheMemoryAtCouldNotBeFullyRead, s1));
                                    }
                                    else
                                        byteBuf = UBinaryUtils.EmptyArray;
                                    //still here so everything ok
                                    assemblerLines[assemblerLines.Length - 1].LineNr = currentLineNr;
                                    assemblerLines[assemblerLines.Length - 1].Line = "<READMEM" + readMems.Length + '>';
                                    readMems.SetLength(readMems.Length + 1);
                                    readMems.Last.Length = a;
                                    readMems.Last.Bytes = new AByteArray(byteBuf);
                                    byteBuf = null;
                                }
                                else
                                    throw new Exception(rsWrongSyntaxReadMemAddressSize);
                                continue;
                            }
                            #endregion
                            #region Command REASSEMBLE()
                            if (AStringUtils.Copy(currentLine, 0, 11).ToUpper() == "REASSEMBLE(")
                            {
                                var a = AStringUtils.Pos("(", currentLine);
                                var b = AStringUtils.Pos(")", currentLine);
                                if (a != -1 && b != -1)
                                {
                                    var s1 = AStringUtils.Copy(currentLine, a + 1, b - a - 1).Trim();
                                    UIntPtr testPtr;
                                    try
                                    {
                                        testPtr = Assembler.SymbolHandler.GetAddressFromName(s1);
                                    }
                                    catch
                                    {
                                        throw new Exception(UStringUtils.Sprintf(rsXCouldNotBeFound, s1));
                                    }
                                    using (var disassembler = new ADisassembler(Assembler.SymbolHandler))
                                    {
                                        disassembler.IsDataOnly = true;
                                        disassembler.Disassemble(ref testPtr, ref s1);
                                        if (syntaxCheckOnly)
                                            currentLine = "nop";
                                        else
                                            currentLine = disassembler.LastDisassembleData.Prefix + ' ' + disassembler.LastDisassembleData.OpCode + ' ' + disassembler.LastDisassembleData.Parameters;
                                        assemblerLines.Last.LineNr = currentLineNr;
                                        assemblerLines.Last.Line = currentLine;
                                    }
                                }
                                else
                                    throw new Exception(rsWrongSyntaxReAssemble);
                            }
                            #endregion
                            #region Command LOADBINARY() -- todo
                            /*
                            if (uppercase(copy(currentline, 1, 12)) == "LOADLIBRARY(")
                            {
                                //load a library into memory , this one already executes BEFORE the 2nd pass to get addressnames correct
                                var a = pos("(", currentline);
                                var b = pos(")", currentline);
                                if (a != -1 && b != -1)
                                {
                                    var s1 = trim(copy(currentline, a + 1, b - a - 1));
                                    if ((length(s1) > 1) && ((s1[0] == '\'') || (s1[0] == '"')))
                                        s1 = ansidequotedstr(s1, s1[0]);
                                    if (pos(":", s1) == 0)
                                    {
                                        s2 = extractfilename(s1);
                                        if (getconnection == nil)  //no connection, so local. Check if the file can be found locally and if so, set the specific path
                                        {
                                            if (fileexists(cheatenginedir + s2)) s1 = cheatenginedir + s2;
                                            else
                                            if (fileexists(getcurrentdir + pathdelim + s2)) s1 = getcurrentdir + pathdelim + s2;
                                            else
                                            if (fileexists(cheatenginedir + s1)) s1 = cheatenginedir + s1;
                                        }
                                        //else just hope it's in the dll searchpath
                                    } //else direct file path
                                    // try
                                    if (symhandler.getmodulebyname(extractfilename(s1), mi) == false)  //check if it's already injected
                                    {
                                        injectdll(s1, "");
                                        symhandler.reinitialize;
                                    }
                                    symhandler.waitforsymbolsloaded;
                                    //except
                                    create(format(rscouldnotbeinjected, set::of(s1, eos)));
                                    //end;
                                    setlength(assemblerlines, length(assemblerlines) - 1);
                                    continue;
                                }
                                else
                                    create(rswrongsyntaxloadlibraryfilename);
                            }
                            */
                            #endregion
                            #region Command LUACALL() -- todo
                            /*
                            if (uppercase(copy(currentline,1,8))=="LUACALL(") 
                            {
                                //execute a given lua command
                                var a = pos("(", currentline);
                                var b = length(currentline);
                                if (currentline[b] != ')')
                                    b = -1;
                                if ((a != -1) && (b != -1))
                                {
                                    s1 = trim(copy(currentline, a + 1, b - a - 1));
                                    lua_doscript(s1); //raises an exception on error, which is exactly what we want here
                                    setlength(assemblerlines, length(assemblerlines) - 1);
                                    continue;
                                }
                                else
                                    create(rswrongsyntaxluacall);
                            }
                            */
                            #endregion
                            #region Command UNREGISTERSYMBOL()
                            if (disableInfo != null && AStringUtils.Copy(currentLine, 0, 17).ToUpper() == "UNREGISTERSYMBOL(")
                            {
                                //add this symbol to the register symbollist
                                var a = AStringUtils.Pos("(", currentLine);
                                var b = AStringUtils.Pos(")", currentLine);
                                if (a != -1 && b != -1)
                                {
                                    var s1 = AStringUtils.Copy(currentLine, a + 1, b - a - 1).Trim();
                                    if (s1 == "*")
                                    {
                                        j = deleteSymbolList.Length;
                                        deleteSymbolList.SetLength(j + disableInfo.RegisteredSymbols.Length);

                                        for (k = 0; k <= disableInfo.RegisteredSymbols.Length - 1; k++)
                                            deleteSymbolList[j + k] = disableInfo.RegisteredSymbols[k];
                                    }
                                    else
                                    {
                                        var sList = s1.Split(',', ' ');
                                        for (var sli = 0; sli < sList.Length; sli++)
                                        {
                                            s1 = sList[sli];
                                            deleteSymbolList.SetLength(deleteSymbolList.Length + 1);
                                            deleteSymbolList.Last = s1;
                                        }
                                    }
                                }
                                else
                                    throw new Exception(rsSyntaxError);
                                assemblerLines.SetLength(assemblerLines.Length - 1);
                                continue;
                            }
                            #endregion
                            #region Command STRUCT()
                            if (AStringUtils.Copy(currentLine, 0, 7).ToUpper() == "STRUCT ")
                            {
                                ReplaceStructWithDefines(code, i);
                                assemblerLines.SetLength(assemblerLines.Length - 1);
                                i -= 1; //repeat from this line
                                continue;
                            }
                            #endregion
                            #region Command FULLACCESS()
                            if (AStringUtils.Copy(currentLine, 0, 11).ToUpper() == "FULLACCESS(")
                            {
                                var a = AStringUtils.Pos("(", currentLine);
                                var b = AStringUtils.Pos(",", currentLine);
                                var c = AStringUtils.Pos(")", currentLine);
                                if (a != -1 && b != -1 && c != -1)
                                {
                                    var s1 = AStringUtils.Copy(currentLine, a + 1, b - a - 1).Trim();
                                    var s2 = AStringUtils.Copy(currentLine, b + 1, c - b - 1).Trim();
                                    fullAccess.SetLength(fullAccess.Length + 1);
                                    fullAccess.Last.Address = symHandler.GetAddressFromName(s1);
                                    fullAccess.Last.Size = AStringUtils.StrToDWord(s2);
                                }
                                else
                                    throw new Exception(rsSyntaxErrorFullAccessAddressSize);
                                assemblerLines.SetLength(assemblerLines.Length - 1);
                                continue;
                            }
                            #endregion
                            #region Command LABEL()
                            if (AStringUtils.Copy(currentLine, 0, 6).ToUpper() == "LABEL(")
                            {
                                //syntax: label(x)  x=name of the label
                                //later on in the code there has to be a line with "labelname:"
                                var a = AStringUtils.Pos("(", currentLine);
                                var b = AStringUtils.Pos(")", currentLine);
                                if (a != -1 && b != -1)
                                {
                                    var s1 = AStringUtils.Copy(currentLine, a + 1, b - a - 1).Trim();
                                    var sList = s1.Split(',', ' ');
                                    for (var sli = 0; sli < sList.Length; sli++)
                                    {
                                        s1 = sList[sli];
                                        AStringUtils.Val("$" + s1, out j, out a);
                                        if (a == 0)
                                            throw new Exception(UStringUtils.Sprintf(rsIsNotAValidIdentifier, s1));
                                        var varSize = s1.Length;
                                        j = 0;
                                        while (j < labels.Length && labels[j].Name.Length >= varSize)
                                        {
                                            if (labels[j].Name == s1)
                                                throw new Exception(UStringUtils.Sprintf(rsIsBeingRedeclared, s1));
                                            j += 1;
                                        }
                                        j = labels.Length; // quickfix
                                        l = j;
                                        //check for the line "labelname:"
                                        ok1 = false;
                                        for (j = 0; j < code.Length; j++)
                                        {
                                            if (code[j].Value.Trim() != s1 + ':')
                                                continue;
                                            if (ok1)
                                                throw new Exception(UStringUtils.Sprintf(rsLabelIsBeingDefinedMoreThanOnce, s1));
                                            ok1 = true;
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
                                        labels[l].References.SetLength(0);
                                        labels[l].References2.SetLength(0);
                                    }
                                    assemblerLines.SetLength(assemblerLines.Length - 1);
                                    continue;
                                }
                                else
                                    throw new Exception(rsSyntaxError);
                            }
                            #endregion
                            #region Command DEALLOC()
                            if (disableInfo != null && AStringUtils.Copy(currentLine, 0, 8).ToUpper() == "DEALLOC(")
                            {
                                //syntax: dealloc(x)  x=name of region to deallocate
                                //later on in the code there has to be a line with "labelname:"
                                var a = AStringUtils.Pos("(", currentLine);
                                var b = AStringUtils.Pos(")", currentLine);
                                if (a != -1 && b != -1)
                                {
                                    var s1 = AStringUtils.Copy(currentLine, a + 1, b - a - 1).Trim();
                                    if (s1 == "*")
                                    {
                                        //everything that the script allocated
                                        dealloc.SetLength(disableInfo.Allocs.Length);
                                        for (j = 0; j < disableInfo.Allocs.Length; j++)
                                            dealloc[j] = disableInfo.Allocs[j].Address;
                                    }
                                    else
                                    {
                                        var sList = s1.Split(',', ' ');
                                        for (var sli = 0; sli < sList.Length; sli++)
                                        {
                                            s1 = sList[sli];
                                            //find s1 in the ceallocarray
                                            for (j = 0; j < disableInfo.Allocs.Length; j++)
                                            {
                                                if (!String.Equals(disableInfo.Allocs[j].Name, s1, StringComparison.CurrentCultureIgnoreCase))
                                                    continue;
                                                dealloc.SetLength(dealloc.Length + 1);
                                                dealloc.Last = disableInfo.Allocs[j].Address;
                                            }
                                        }
                                    }
                                }
                                assemblerLines.SetLength(assemblerLines.Length - 1);
                                continue;
                            }
                            #endregion
                            #region Command ALLOC()
                            if (AStringUtils.Copy(currentLine, 0, 6).ToUpper() == "ALLOC(")
                            {
                                //syntax: alloc(x,size)    x=variable name size=bytes
                                //or
                                //syntax: alloc(x,size,prefered region)    x=variable name size=bytes
                                //allocate memory
                                var a = AStringUtils.Pos("(", currentLine);
                                var b = AStringUtils.Pos(",", currentLine);
                                var c = AStringUtils.PosEx(",", currentLine, b + 1);
                                var d = AStringUtils.Pos(")", currentLine);
                                if (a != -1 && b != -1 && d != -1)
                                {
                                    var s1 = AStringUtils.Copy(currentLine, a + 1, b - a - 1).Trim();
                                    String s2, s3;
                                    if (c != -1)
                                    {
                                        s2 = AStringUtils.Copy(currentLine, b + 1, c - b - 1).Trim();
                                        s3 = AStringUtils.Copy(currentLine, c + 1, d - c - 1).Trim();
                                    }
                                    else
                                    {
                                        s2 = AStringUtils.Copy(currentLine, b + 1, d - b - 1).Trim();
                                        s3 = "";
                                    }
                                    AStringUtils.Val("0x" + s1, out j, out a);
                                    if (a == 0)
                                        throw new Exception(UStringUtils.Sprintf(rsIsNotAValidIdentifier, s1));
                                    var varSize = s1.Length;
                                    //check for duplicate identifiers
                                    j = 0;
                                    while (j < allocs.Length && allocs[j].Name.Length > varSize)
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
                                    allocs[j].Protection = MemoryProtectionFlags.ExecuteReadWrite;
                                    if (AStringUtils.Copy(currentLine, 1, 8).ToUpper() == "ALLOCNX(")
                                        allocs[j].Protection = MemoryProtectionFlags.ReadWrite;
                                    else if (AStringUtils.Copy(currentLine, 1, 8).ToUpper() == "ALLOCXO(")
                                        allocs[j].Protection = MemoryProtectionFlags.ReadOnly;
                                    assemblerLines.SetLength(assemblerLines.Length - 1);   //don't bother with this in the 2nd pass
                                    continue;
                                }
                                else
                                    throw new Exception(rsWrongSyntaxALLOCIdentifierSizeinbytes);
                            }
                            #endregion
                            //replace ALLOC identifiers with values so the assemble error check doesnt crash on that
                            if (process.IsX64)
                            {
                                for (j = 0; j < allocs.Length; j++)
                                    currentLine = ReplaceToken(currentLine, allocs[j].Name, "ffffffffffffffff");
                            }
                            else
                            {
                                for (j = 0; j < allocs.Length; j++)
                                    currentLine = ReplaceToken(currentLine, allocs[j].Name, "00000000");
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
                                var a = pos("(", currentline);
                                var b = pos(",", currentline);
                                var c = pos(")", currentline);
                                if (a != -1 && b != -1 && c != -1)
                                {
                                    var s1 = trim(copy(currentline, a + 1, b - a - 1));
                                    var s2 = trim(copy(currentline, b + 1, c - b - 1));
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
                                    continue;
                                }
                                else
                                    Create(rsWrongSyntaxKallocIdentifierSizeinbytes);
                            }
                            */
                            #endregion
                            //replace KALLOC identifiers with values so the assemble error check doesnt crash on that
                            if (process.IsX64)
                            {
                                for (j = 0; j < kAllocs.Length; j++)
                                    currentLine = ReplaceToken(currentLine, kAllocs[j].Name, "ffffffffffffffff");
                            }
                            else
                            {
                                for (j = 0; j < kAllocs.Length; j++)
                                    currentLine = ReplaceToken(currentLine, kAllocs[j].Name, "00000000");
                            }
                            //check for assembler errors
                            //address
                            if (currentLine[currentLine.Length - 1] == ':')
                            {
                                try
                                {
                                    ok1 = false;
                                    for (j = 0; j < labels.Length; j++)
                                    {
                                        if (currentLine != labels[j].Name + ':')
                                            continue;
                                        labels[j].AssemblerLine = assemblerLines.Length - 1;
                                        ok1 = true;
                                        break;
                                    }
                                    if (ok1)
                                        continue; //no check
                                    //still here, so more complex
                                    if (syntaxCheckOnly & (disableInfo != null))
                                    {
                                        //replace tokens with registered symbols from the enable part
                                        for (j = 0; j < disableInfo.RegisteredSymbols.Length; j++)
                                            currentLine = ReplaceToken(currentLine, disableInfo.RegisteredSymbols[j], "00000000");
                                    }
                                    try
                                    {
                                        // todo is this ok as just an int?!?!?
                                        var s1 = AStringUtils.Copy(currentLine, 0, currentLine.Length - 1);
                                        if (!String.IsNullOrEmpty(s1))
                                            symHandler.GetAddressFromName(s1);
                                    }
                                    catch
                                    {
                                        currentLine = AStringUtils.IntToHex(symHandler.GetAddressFromName(AStringUtils.Copy(currentLine, 1, currentLine.Length - 1)), 8) + ':';
                                        assemblerLines[assemblerLines.Length - 1].LineNr = currentLineNr;
                                        assemblerLines[assemblerLines.Length - 1].Line = currentLine;
                                    }
                                }
                                catch
                                {
                                    if (potentialLabels.IndexOf(AStringUtils.Copy(currentLine, 0, currentLine.Length - 1)) == -1)
                                        throw new Exception(rsThisAddressSpecifierIsNotValid);

                                    j = labels.Length;
                                    labels.SetLength(j + 1);
                                    labels[j].Name = AStringUtils.Copy(currentLine, 0, currentLine.Length - 1);
                                    labels[j].AssemblerLine = assemblerLines.Length - 1;
                                    labels[j].Defined = false;
                                    labels[j].References.SetLength(0);
                                    labels[j].References2.SetLength(0);
                                }
                                continue; //next line
                            }
                            //replace label references with 00000000 so the assembler check doesn't complain about it
                            if (process.IsX64)
                            {
                                for (j = 0; j < labels.Length; j++)
                                    currentLine = ReplaceToken(currentLine, labels[j].Name, "ffffffffffffffff");
                            }
                            else
                            {
                                for (j = 0; j < labels.Length; j++)
                                    currentLine = ReplaceToken(currentLine, labels[j].Name, "00000000");
                            }
                            try
                            {
                                //replace identifiers in the line with their address
                                ok1 = false;
                                try
                                {
                                    ok1 = Assembler.Assemble(currentLine, currentAddress.ToUInt64(), assembled[0].Bytes, AAssemblerPreference.None, true);
                                }
                                catch
                                {
                                    // ignored
                                }
                                if (!ok1)  //the instruction could not be assembled as it is right now
                                {
                                    //try potential labels
                                    ok1 = false;
                                    for (j = 0; j < potentialLabels.Length; j++)
                                    {
                                        if (process.IsX64)
                                            currentLine = ReplaceToken(currentLine, potentialLabels[j], "ffffffffffffffff");
                                        else
                                            currentLine = ReplaceToken(currentLine, potentialLabels[j], "00000000");
                                        try
                                        {
                                            ok1 = Assembler.Assemble(currentLine, currentAddress.ToUInt64(), assembled[0].Bytes, AAssemblerPreference.None, true);
                                            if (ok1)
                                            {
                                                //define this potential label as a full label
                                                k = labels.Length;
                                                labels.SetLength(k + 1);
                                                labels[k].Name = potentialLabels[j];
                                                labels[k].Defined = false;
                                                labels[k].AfterCCode = false;
                                                labels[k].References.SetLength(0);
                                                labels[k].References2.SetLength(0);
                                                break;
                                            }
                                        }
                                        catch
                                        {
                                            //don't quit yet
                                        }
                                    }
                                    #region C-Symbols -- todo
                                    // todo add back in when its time to make the C code stuff
                                    // //c-symbol addition
                                    // if (!ok1)
                                    // {
                                    //     //last chance, try the c-code symbols
                                    //     for (j = 0; j <= length(dataforaacodepass2.cdata.symbols) - 1; j++)
                                    //     {
                                    //         if (process.IsX64)
                                    //             currentline = ReplaceToken(currentline, dataforaacodepass2.cdata.symbols[j].name, "ffffffffffffffff");
                                    //         else
                                    //             currentline = ReplaceToken(currentline, dataforaacodepass2.cdata.symbols[j].name, "00000000");
                                    //         try
                                    //         {
                                    // 
                                    //             ok1 = Assembler.Assemble(currentline, currentaddress.ToUInt64(), assembled[0].Bytes, AAssemblerPreference.apnone, true);
                                    //             if (ok1)
                                    //             {
                                    //                 //define this c-code symbol as an undefined label
                                    //                 k = length(labels);
                                    //                 setlength(labels, k + 1);
                                    //                 labels[k].labelname = dataforaacodepass2.cdata.symbols[j].name;
                                    //                 labels[k].defined = false;
                                    //                 labels[k].afterccode = true;
                                    //                 labels[k].assemblerline = -1;
                                    //                 setlength(labels[k].references, 0);
                                    //                 setlength(labels[k].references2, 0);
                                    //                 flush();
                                    //             }
                                    //         }
                                    //         catch
                                    //         {
                                    //             //don't quit yet
                                    //         }
                                    //     }
                                    // }
                                    // //c-symbol addition^
                                    #endregion
                                }
                                if (!ok1)
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
                        throw new Exception(UStringUtils.Sprintf(rsErrorInLine, currentLineNr, currentLine, ex.Message));
                    }
                }
                #region Sanity Check (Symbols)
                if (addSymbolList.Length > 0)
                {
                    //now scan the addsymbollist entries for allocs and labels and see if they exist
                    for (i = 0; i < addSymbolList.Length; i++)
                    {
                        ok1 = false;
                        for (j = 0; j < allocs.Length; j++) //scan allocs
                        {
                            if (String.Equals(addSymbolList[i], allocs[j].Name, StringComparison.CurrentCultureIgnoreCase))
                            {
                                ok1 = true;
                                break;
                            }
                        }
                        if (!ok1) //scan labels
                        {
                            for (j = 0; j < labels.Length; j++)
                            {
                                if (String.Equals(addSymbolList[i], labels[j].Name, StringComparison.CurrentCultureIgnoreCase))
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
                                if (String.Equals(addSymbolList[i], defines[j].Name, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    ok1 = true;
                                    break;
                                }
                            }
                        }
                        if (!ok1)
                            throw new Exception(UStringUtils.Sprintf(rsWasSupposedToBeAddedToTheSymbollistButItIsnTDeclar, addSymbolList[i]));
                    }
                }
                #endregion
                #region Sanity Check (Create Thread)
                if (createThread.Length > 0)
                {
                    for (i = 0; i < createThread.Length; i++)
                    {
                        ok1 = true;
                        try
                        {
                            symHandler.GetAddressFromName(createThread[i]);
                        }
                        catch
                        {
                            ok1 = false;
                        }
                        if (!ok1)
                        {
                            for (j = 0; j < labels.Length; j++)
                            {
                                if (String.Equals(labels[j].Name, createThread[i], StringComparison.CurrentCultureIgnoreCase))
                                {
                                    ok1 = true;
                                    break;
                                }
                            }
                        }
                        if (!ok1)
                        {
                            for (j = 0; j < allocs.Length; j++)
                            {
                                if (String.Equals(allocs[j].Name, createThread[i], StringComparison.CurrentCultureIgnoreCase))
                                {
                                    ok1 = true;
                                    break;
                                }
                            }
                        }
                        if (!ok1)
                        {
                            for (j = 0; j < kAllocs.Length; j++)
                            {
                                if (String.Equals(kAllocs[j].Name, createThread[i], StringComparison.CurrentCultureIgnoreCase))
                                {
                                    ok1 = true;
                                    break;
                                }
                            }
                        }
                        if (!ok1)
                        {
                            for (j = 0; j < defines.Length; j++)
                            {
                                if (String.Equals(defines[j].Name, createThread[i], StringComparison.CurrentCultureIgnoreCase))
                                {
                                    try
                                    {
                                        symHandler.GetAddressFromName(defines[j].Whatever);
                                        ok1 = true;
                                    }
                                    catch
                                    {
                                        // ignored
                                    }
                                    break;
                                }
                            }
                        }
                        if (!ok1)
                            throw new Exception(UStringUtils.Sprintf(rsTheAddressInCreatethreadIsNotValid, createThread[i]));
                    }
                }
                #endregion
                #region Sanity Check (Create Thread And Wait)
                if (createThreadAndWait.Length > 0)
                {
                    for (i = 0; i < createThreadAndWait.Length; i++)
                    {
                        ok1 = true;
                        try
                        {
                            symHandler.GetAddressFromName(createThreadAndWait[i].Name);
                        }
                        catch
                        {
                            ok1 = false;
                        }
                        if (!ok1)
                        {
                            for (j = 0; j < labels.Length; j++)
                            {
                                if (String.Equals(labels[j].Name, createThreadAndWait[i].Name, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    ok1 = true;
                                    break;
                                }
                            }
                        }
                        if (!ok1)
                        {
                            for (j = 0; j < allocs.Length; j++)
                            {
                                if (String.Equals(allocs[j].Name, createThreadAndWait[i].Name, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    ok1 = true;
                                    break;
                                }
                            }
                        }
                        if (!ok1)
                        {
                            for (j = 0; j < kAllocs.Length; j++)
                            {
                                if (String.Equals(kAllocs[j].Name, createThreadAndWait[i].Name, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    ok1 = true;
                                    break;
                                }
                            }
                        }
                        if (!ok1)
                        {
                            for (j = 0; j < defines.Length; j++)
                            {
                                if (String.Equals(defines[j].Name, createThreadAndWait[i].Name, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    try
                                    {
                                        symHandler.GetAddressFromName(defines[j].Whatever);
                                        ok1 = true;
                                    }
                                    catch
                                    {
                                        // ignored
                                    }
                                    break;
                                }
                            }
                        }
                        if (!ok1)
                            throw new Exception(UStringUtils.Sprintf(rsTheAddressInCreatethreadAndWaitIsNotValid, createThreadAndWait[i].Name));
                    }
                }
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
                if (popUpMessages & process.IsX64 & usesAobScan & (allocs.Length > 0))
                {
                    //check if a preferred address is used
                    var prefered = UIntPtr.Zero;
                    for (i = 0; i < allocs.Length; i++)
                    {
                        if (allocs[i].Preferred != UIntPtr.Zero)
                        {
                            prefered = allocs[i].Preferred;
                            break;
                        }
                    }
                    if (prefered == UIntPtr.Zero && MessageBox.Show(rsNoPreferedRangeAllocWarning, "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        return false;
                }
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
                    k = allocs.Length;
                    i = 0;
                    while (i < k)
                    {
                        if (allocs[i].Name.StartsWith("ceinternal_autofree"))
                        {
                            //move it to the back
                            var tempAlloc = allocs[i];
                            for (j = i; j < allocs.Length - 1; j++)
                                allocs[j] = allocs[j + 1];
                            allocs[allocs.Length - 1] = tempAlloc;
                            k -= 1;
                        }
                        else
                            i += 1;
                    }
                    j = 0; //entry to go from
                    var preferred = allocs[0].Preferred;
                    var protection = allocs[0].Protection;
                    var allocSz = allocs[0].Size;
                    for (i = 1; i <= allocs.Length - 1; i++)
                    {
                        //does this entry have a preferred location or a non default protection
                        if (allocs[i].Preferred != UIntPtr.Zero || allocs[i].Protection != MemoryProtectionFlags.ExecuteReadWrite)
                        {
                            //if yes, is it the same as the previous entry? (or was the previous one that doesn't care?)
                            if (preferred == UIntPtr.Zero)
                                preferred = allocs[i].Preferred;
                            //if yes, is it the same as the previous entry? (or was the previous one that doesn't care?)
                            if (preferred != allocs[i].Preferred && protection != allocs[i].Protection)
                            {
                                //different preferred address or protection
                                if (allocSz > 0)  //it has some previous entries with compatible locations
                                {
                                    k = 10;
                                    allocs[j].Address = UIntPtr.Zero;
                                    while (k > 0 && allocs[j].Address == UIntPtr.Zero)
                                    {
                                        //try allocating until a memory region has been found (e.g due to quick allocating by the game)
                                        if (preferred == UIntPtr.Zero && j > 0)  //if not a preferred address but there is a previous alloc, allocate near there
                                            preferred = allocs[j - 1].Address;
                                        var oldPreferred = preferred;
                                        preferred = AMemoryHelper.FindFreeBlockForRegion(process.Handle, preferred.ToIntPtr(), (int)allocSz).ToUIntPtr();
                                        if (preferred == UIntPtr.Zero && oldPreferred != UIntPtr.Zero)
                                            preferred = oldPreferred;
                                        // todo save old protection!
                                        allocs[j].Address = AMemoryHelper.Allocate(process.Handle, preferred.ToIntPtr(), (int)allocSz, MemoryProtectionFlags.ExecuteReadWrite, MemoryAllocationFlags.Reserve | MemoryAllocationFlags.Commit).ToUIntPtr();
                                        if (allocs[j].Address == UIntPtr.Zero)
                                        {
                                            OutputDebugString(rsFailureToAllocateMemory + " 1");
                                            preferred += 65536;
                                        }
                                        k -= 1;
                                    }
                                    if (allocs[j].Address == UIntPtr.Zero)
                                        allocs[j].Address = AMemoryHelper.LastChanceAllocPreferred(process.Handle, preferred.ToIntPtr(), (int)allocSz, protection).ToUIntPtr();
                                    if (allocs[j].Address == UIntPtr.Zero)
                                        throw new Exception(UStringUtils.Sprintf(rsFailureAlloc, preferred));
                                    if (allocs[j].Address == UIntPtr.Zero)
                                        OutputDebugString(rsFailureToAllocateMemory + " 2");
                                    //adjust the addresses of entries that are part of this block
                                    for (k = j + 1; k <= i - 1; k++)
                                        allocs[k].Address = (UIntPtr)(allocs[k - 1].Address.ToUInt64() + allocs[k - 1].Size);
                                    allocSz = 0;
                                }
                                //new preferred address
                                j = i;
                                preferred = allocs[i].Preferred;
                                protection = allocs[i].Protection;
                            }
                        }
                        //no preferred location specified, OR same preferred location
                        allocSz += allocs[i].Size;
                    }
                    if (allocSz > 0)
                    {
                        //adjust the address of entries that are part of this final block
                        k = 10;
                        allocs[j].Address = UIntPtr.Zero;
                        while (k > 0 && allocs[j].Address == UIntPtr.Zero)
                        {
                            i = 0;
                            preferred = AMemoryHelper.FindFreeBlockForRegion(process.Handle, preferred.ToIntPtr(), (int)allocSz).ToUIntPtr();
                            allocs[j].Address = AMemoryHelper.Allocate(process.Handle, preferred.ToIntPtr(), (int)allocSz, MemoryProtectionFlags.ExecuteReadWrite, MemoryAllocationFlags.Reserve | MemoryAllocationFlags.Commit).ToUIntPtr();
                            if (allocs[j].Address == UIntPtr.Zero)
                                OutputDebugString(rsFailureToAllocateMemory + " 3");
                            k -= 1;
                        }
                        if (allocs[j].Address == UIntPtr.Zero)
                            allocs[j].Address = AMemoryHelper.Allocate(process.Handle, (int)allocSz, MemoryProtectionFlags.ExecuteReadWrite, MemoryAllocationFlags.Reserve | MemoryAllocationFlags.Commit).ToUIntPtr();
                        if (allocs[j].Address == UIntPtr.Zero)
                            throw new Exception(rsFailureToAllocateMemory + " 4");
                        for (i = j + 1; i < allocs.Length; i++)
                            allocs[i].Address = (UIntPtr)(allocs[i - 1].Address.ToUInt64() + allocs[i - 1].Size);
                    }
                }
                #endregion
                #region 2nd Pass
                //-----------------------2nd pass------------------------
                //assemblerlines only contains label specifiers and assembler instructions
                assembled.SetLength(0);
                currentLineNr = 0;
                try
                {
                    for (i = 0; i < assemblerLines.Length; i++)
                    {
                        currentLine = assemblerLines[i].Line;
                        currentLineNr = assemblerLines[i].LineNr;
                        var createThreadAndWaitId = -1;
                        for (j = 0; j < createThreadAndWait.Length; j++) //there can be multiple at the time of assembly.  All entries up to the higest value will be picked at a blockwrite (and made 0 so next blockwrite won't do them)
                        {
                            if (i > createThreadAndWait[j].Position || i == assemblerLines.Length - 1)  //if it's the last line, then do all remaining
                                createThreadAndWaitId = j;
                        }
                        Tokenize(currentLine, tokens);
                        //if alloc then replace with the address
                        for (j = 0; j < allocs.Length; j++)
                            currentLine = ReplaceToken(currentLine, allocs[j].Name, AStringUtils.IntToHex(allocs[j].Address, 8));
                        //if kalloc then replace with the address
                        for (j = 0; j < kAllocs.Length; j++)
                            currentLine = ReplaceToken(currentLine, kAllocs[j].Name, AStringUtils.IntToHex(kAllocs[j].Address, 8));
                        for (j = 0; j < defines.Length; j++)
                            currentLine = ReplaceToken(currentLine, defines[j].Name, defines[j].Whatever);
                        ok1 = false;
                        if (currentLine[currentLine.Length - 1] != ':')  //if it's not a definition then
                        {
                            for (j = 0; j < labels.Length; j++)
                            {
                                if (!TokenCheck(currentLine, labels[j].Name))
                                    continue;
                                if (!labels[j].Defined)
                                {
                                    //the address hasn't been found yet
                                    //this is the part that causes those nops after a short jump below the current instruction
                                    //problem: The size of these instructions determine where this label will be defined
                                    //close
                                    var s1 = ReplaceToken(currentLine, labels[j].Name, AStringUtils.IntToHex(currentAddress, 8));
                                    //far and big
                                    // todo add back when arm is added
                                    //if (processhandler.systemarchitecture == archarm)
                                    //    currentline = ReplaceToken(currentline, labels[j].Name, AStringUtils.IntToHex(currentaddress + 0x4fffff8, 8));
                                    //else
                                    {
                                        if (process.IsX64)  //and not in region
                                        {
                                            //check if between here and the definition of labels[j].labelname is an write pointer change specifier to a region too far away from currentaddress, if not, LONG will suffice
                                            //tip: you 'could' disassemble everything inbetween and see if a small jmp is possible as well (just a lot slower)
                                            var mustBeFar = false;
                                            for (l = i + 1; l < assemblerLines.Length; l++)
                                            {
                                                var currentLine2 = assemblerLines[l].Line;
                                                if (currentLine2 == labels[j].Name + ':')
                                                    break; //reached the label
                                                if (currentLine2[currentLine2.Length - 1] == ':')
                                                {
                                                    //check if it's just a label or alloc in the same group
                                                    for (k = 0; k < defines.Length; k++)
                                                        currentLine2 = ReplaceToken(currentLine2, defines[k].Name, defines[k].Whatever);
                                                    var s2 = AStringUtils.Copy(currentLine2, 0, currentLine2.Length - 1);
                                                    for (k = 0; k < allocs.Length; k++)
                                                    {
                                                        if (allocs[k].Name == s2)
                                                        {
                                                            var diff = UIntPtr.Zero;
                                                            if (currentAddress.ToUInt64() > allocs[k].Address.ToUInt64())
                                                                diff = (UIntPtr)(currentAddress.ToUInt64() - allocs[k].Address.ToUInt64());
                                                            else
                                                                diff = (UIntPtr)(allocs[k].Address.ToUInt64() - currentAddress.ToUInt64());
                                                            if (diff.ToUInt64() >= 0x80000000)
                                                            {
                                                                mustBeFar = true;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    if (mustBeFar)
                                                        break;
                                                    for (k = 0; k < kAllocs.Length; k++)
                                                    {
                                                        if (kAllocs[k].Name == s2)
                                                        {
                                                            var diff = UIntPtr.Zero;
                                                            if (currentAddress.ToUInt64() > kAllocs[k].Address.ToUInt64())
                                                                diff = (UIntPtr)(currentAddress.ToUInt64() - kAllocs[k].Address.ToUInt64());
                                                            else
                                                                diff = (UIntPtr)(kAllocs[k].Address.ToUInt64() - currentAddress.ToUInt64());
                                                            if (diff.ToUInt64() >= 0x80000000)
                                                            {
                                                                mustBeFar = true;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    if (mustBeFar)
                                                        break;
                                                    //if it's a label it's ok
                                                    ok1 = false;
                                                    for (k = 0; k < labels.Length; k++)
                                                    {
                                                        if (labels[k].Name == s2)
                                                        {
                                                            ok1 = true;
                                                            break;
                                                        }
                                                    }
                                                    if (ok1)
                                                        continue; //it's a label, no need to do a heavy symbol lookupl // not an alloc or kalloc
                                                    try
                                                    {
                                                        var testPtr = symHandler.GetAddressFromName(AStringUtils.Copy(currentLine2, 0, currentLine2.Length - 1));
                                                        var diff = UIntPtr.Zero;
                                                        if (currentAddress.ToUInt64() > testPtr.ToUInt64())
                                                            diff = (UIntPtr)(currentAddress.ToUInt64() - testPtr.ToUInt64());
                                                        else
                                                            diff = (UIntPtr)(testPtr.ToUInt64() - currentAddress.ToUInt64());
                                                        if (diff.ToUInt64() >= 0x80000000)
                                                        {
                                                            mustBeFar = true;
                                                            break;
                                                        }
                                                    }
                                                    catch
                                                    {
                                                        mustBeFar = true;
                                                    }
                                                    if (mustBeFar)
                                                        break;
                                                }
                                            }
                                            if (mustBeFar)
                                                currentLine = ReplaceToken(currentLine, labels[j].Name, AStringUtils.IntToHex(currentAddress + 0xfffff, 8));
                                            else
                                                currentLine = ReplaceToken(currentLine, labels[j].Name, AStringUtils.IntToHex(currentAddress + 0xfffff, 8));
                                        }
                                        else
                                            currentLine = ReplaceToken(currentLine, labels[j].Name, AStringUtils.IntToHex(currentAddress + 0xfffff, 8));
                                    }
                                    assembled.Inc();
                                    assembled.Last.CreateThreadAndWait = createThreadAndWaitId;
                                    assembled.Last.Address = currentAddress;
                                    Assembler.Assemble(currentLine, currentAddress.ToUInt64(), assembled.Last.Bytes, AAssemblerPreference.None, true);
                                    var a = assembled.Last.Bytes.Length;
                                    Assembler.Assemble(s1, currentAddress.ToUInt64(), assembled.Last.Bytes, AAssemblerPreference.None, true);
                                    var b = assembled.Last.Bytes.Length;
                                    if (a > b)  //pick the biggest one
                                        Assembler.Assemble(currentLine, currentAddress.ToUInt64(), assembled.Last.Bytes);
                                    labels[j].References.Inc();
                                    labels[j].References.Last = (Byte)(assembled.Length - 1);
                                    labels[j].References2.Inc();
                                    labels[j].References2.Last = (Byte)i;
                                    currentAddress += assembled.Last.Bytes.Length;
                                    ok1 = true;
                                }
                                else
                                    currentLine = ReplaceToken(currentLine, labels[j].Name, AStringUtils.IntToHex(labels[j].Address, 8));
                                //break;
                            }
                        }
                        if (ok1)
                            continue;
                        if (currentLine[currentLine.Length - 1] == ':')
                        {
                            ok1 = false;
                            for (j = 0; j < labels.Length; j++)
                            {
                                if (i != labels[j].AssemblerLine)
                                    continue;
                                if (labels[j].Defined)
                                    currentAddress = labels[j].Address;
                                else
                                {
                                    labels[j].Address = currentAddress;
                                    labels[j].Defined = true;
                                }
                                ok1 = true;
                                //reassemble the instructions that had no target
                                for (k = 0; k < labels[j].References.Length; k++)
                                {
                                    var a = assembled[labels[j].References[k]].Bytes.Length; //original size of the assembled code
                                    var s1 = ReplaceToken(assemblerLines[labels[j].References2[k]].Line, labels[j].Name, AStringUtils.IntToHex(labels[j].Address, 8));
                                    if (process.IsX64)
                                        Assembler.Assemble(s1, assembled[labels[j].References[k]].Address.ToUInt64(), assembled[labels[j].References[k]].Bytes);
                                    else
                                        Assembler.Assemble(s1, assembled[labels[j].References[k]].Address.ToUInt64(), assembled[labels[j].References[k]].Bytes, AAssemblerPreference.None);
                                    var b = assembled[labels[j].References[k]].Bytes.Length; //new size
                                    assembled[labels[j].References[k]].Bytes.SetLength(a); //original size (original size is always bigger or equal than newsize)
                                    if (b < a && a < 12)  //try to grow the instruction as some people cry about nops (unless it was a megajmp/call as those are less efficient)
                                    {
                                        //try a bigger one
                                        Assembler.Assemble(s1, assembled[labels[j].References[k]].Address.ToUInt64(), nops, AAssemblerPreference.Long);
                                        if (nops.Length == a)  //found a match size
                                        {
                                            AArrayUtils.CopyMemory(assembled[labels[j].References[k]].Bytes, nops, a);
                                            b = a;
                                        }
                                    }
                                    //fill the difference with nops (not the most efficient approach, but it should work)
                                    // todo add back when arm is added
                                    //if (processhandler.systemarchitecture == archarm)
                                    //{
                                    //    for (l = 0; l <= ((a - b + 3) / 4) - 1; l++)
                                    //        pdword(&assembled[labels[j].References[k]].Bytes[b + l * 4]) = 0xe1a00000;      //<mov r0,r0: (nop equivalent)
                                    //}
                                    //else
                                    {
                                        // perhaps make it so if a-b>8 then replace with the far version
                                        Assembler.Assemble("nop " + AStringUtils.IntToHex(a - b, 1), 0, nops);
                                        for (l = b; l <= a - 1; l++)
                                            assembled[labels[j].References[k]].Bytes[l] = nops[l - b];
                                    }
                                }
                                break;
                            }
                            if (ok1)
                                continue;
                            try
                            {
                                currentAddress = symHandler.GetAddressFromName(AStringUtils.Copy(currentLine, 0, currentLine.Length - 1));
                                continue; //next line
                            }
                            catch
                            {

                                throw new Exception(rsThisAddressSpecifierIsNotValid);
                            }
                        }
                        assembled.Inc();
                        assembled.Last.Address = currentAddress;
                        assembled.Last.CreateThreadAndWait = createThreadAndWaitId;
                        if ((currentLine != "") && (currentLine[1] == '<'))  //special assembler instruction
                        {
                            if (AStringUtils.Copy(currentLine, 0, 8).ToUpper() == "<READMEM")
                            {
                                //lets try this for once
                                l = UStringUtils.Atoi(currentLine, 8);
                                assembled.Last.Bytes.SetLength(readMems[l].Length);
                                AArrayUtils.CopyMemory(assembled.Last.Bytes, readMems[l].Bytes, readMems[l].Length);
                            }
                            else
                                Assembler.Assemble(currentLine, currentAddress.ToUInt64(), assembled.Last.Bytes);
                        }
                        else
                            Assembler.Assemble(currentLine, currentAddress.ToUInt64(), assembled.Last.Bytes);
                        currentAddress += assembled.Last.Bytes.Length;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(currentLineNr + ':' + ex.Message);
                }
                ok2 = true;
                #endregion
                //unprotectmemory
                for (i = 0; i < fullAccess.Length; i++)
                {
                    if (createScript)
                        scriptBytes.Add(AScriptObjectType.FullAccess, fullAccess[i].Address, new AByteArray());
                    else
                        AMemoryHelper.FullAccess(process.Handle, fullAccess[i].Address.ToIntPtr(), (int)fullAccess[i].Size);
                }
                #region Load Binaries -- todo
                //load binaries
                //if (length(loadbinary) > 0)
                //{
                //    for (i = 0; i <= length(loadbinary) - 1; i++)
                //    {
                //        testptr = getaddressfromscript(loadbinary[i].address);
                //        if (testptr != 0)
                //        {
                //            binaryfile = tmemorystream.create;
                //            //try
                //            binaryfile.loadfromfile(loadbinary[i].filename);
                //            if (createScript)
                //            {
                //                newScript.Add(Concat("Poke ", IntToHex(testPtr, intPtrHexSize), ' ', BinToStr(PointerToByteArray(binaryfile.Memory, binaryfile.Size))));
                //                //newScript.Add(Concat('PokeFile ', IntToHex(testPtr, intPtrHexSize), ' ', loadbinary[i].filename));
                //                ok1 = true;
                //            }
                //            else
                //            {
                //                ok2 = writeprocessmemory(processhandle, (pointer)(testptr), binaryfile.memory, binaryfile.size, x);
                //            }
                //            //finally
                //            binaryfile.free;
                //            //end;
                //        }
                //        else
                //            create("Failure ");
                //    }
                //}
                #endregion
                #region dataForAACodePass2 -- todo
                // //fill in the addresses requested by dataForAACodePass2 and finish the compilation
                // if (dataforaacodepass2.cdata.cscript != nil)
                // {
                //     dataforaacodepass2.cdata.address = getaddressfromscript("ceinternal_autofree_ccode"); //warning: do not step over this with the debugger
                //     for (i = 0; i <= length(dataforaacodepass2.cdata.references) - 1; i++)
                //         dataforaacodepass2.cdata.references[i].address = getaddressfromscript(dataforaacodepass2.cdata.references[i].name);
                //     if (disableInfo != nil)
                //         autoassemblercodepass2(dataforaacodepass2, disableInfo.ccodesymbols);
                //     else
                //         autoassemblercodepass2(dataforaacodepass2, nil);
                //     if (disableInfo != nil)
                //     {
                //         if (targetself)
                //             selfsymhandler.addsymbollist(disableInfo.ccodesymbols);
                //         else
                //             symhandler.addsymbollist(disableInfo.ccodesymbols);
                //     }
                //     //reassemble c-code reference
                //     for (j = 0; j <= length(labels) - 1; j++)
                //     {
                //         if (labels[j].afterccode)
                //         {
                //             ok1 = false;
                //             for (k = 0; k <= length(dataforaacodepass2.cdata.symbols) - 1; k++)
                //             {
                //                 if (labels[j].labelname == dataforaacodepass2.cdata.symbols[k].name)
                //                 {
                //                     labels[j].address = dataforaacodepass2.cdata.symbols[k].address;
                //                     ok1 = labels[j].address != 0;
                //                     flush();
                //                 }
                //             }
                //             if (~ok1)
                //                 create(string("Failure getting the address for c-symbol ") + labels[j].labelname);
                //             //todo: change to a function so both originallabel and this can use it
                //             for (k = 0; k <= length(labels[j].references) - 1; k++)
                //             {
                //                 a = length(assembled[labels[j].references[k]].bytes); //original size of the assembled code
                //                 s1 = replacetoken(assemblerlines[labels[j].references2[k]].line, labels[j].labelname, AStringUtils.IntToHex(labels[j].address, 8));
                //                 if (processhandler.is64bit)
                //                     assemble(s1, assembled[labels[j].references[k]].address, assembled[labels[j].references[k]].bytes);
                //                 else
                //                     assemble(s1, assembled[labels[j].references[k]].address, assembled[labels[j].references[k]].bytes, aplong);
                // 
                //                 b = length(assembled[labels[j].references[k]].bytes); //new size
                //                 setlength(assembled[labels[j].references[k]].bytes, a); //original size (original size is always bigger or equal than newsize)
                //                 if ((b < a) && (a < 12))  //try to grow the instruction as some people cry about nops (unless it was a megajmp/call as those are less efficient)
                //                 {
                //                     //try a bigger one
                //                     assemble(s1, assembled[labels[j].references[k]].address, nops, aplong);
                //                     if (length(nops) == a)  //found a match size
                //                     {
                //                         copymemory(&assembled[labels[j].references[k]].bytes[0], &nops[0], a);
                //                         b = a;
                //                     }
                //                 }
                //                 //fill the difference with nops (not the most efficient approach, but it should work)
                //                 if (processhandler.systemarchitecture == archarm)
                //                 {
                //                     for (l = 0; l <= ((a - b + 3) / 4) - 1; l++)
                //                         pdword(&assembled[labels[j].references[k]].bytes[b + l * 4]) = 0xe1a00000;      //<mov r0,r0: (nop equivalent)
                //                 }
                //                 else
                //                 {
                //                     assemble(string("nop ") + AStringUtils.IntToHex(a - b, 1), 0, nops);
                // 
                //                     for (l = b; l <= a - 1; l++)
                //                         assembled[labels[j].references[k]].bytes[l] = nops[l - b];
                //                 }
                //             }
                //         }
                //     }
                // }
                #endregion
                #region Exceptions -- todo
                // //we're still here so inject the rest of it
                // //addresses are known here, so parse the exception list if there is one
                // if (length(exceptionlist) > 0)
                // {
                //     initializeautoassemblerexceptionhandler;
                //     for (i = length(exceptionlist) - 1; i >= 0; i--) //add it in the reverse order so the nested try/excepts come first
                //         autoassemblerexceptionhandleraddexceptionrange(getaddressfromscript(exceptionlist[i].trylabel), getaddressfromscript(exceptionlist[i].exceptlabel));
                //     autoassemblerexceptionhandlerapplychanges;
                // }
                #endregion
                #region Combine Assembly Lines (Squish)
                j = 0;
                for (i = 1; i < assembled.Length; i++)
                {
                    if (assembled[i].Address == (assembled[j].Address + assembled[j].Bytes.Length))  //matches the previous entry
                    {
                        //group
                        k = assembled[j].Bytes.Length;
                        assembled[j].Bytes.SetLength(k + assembled[i].Bytes.Length);
                        AArrayUtils.CopyMemory(assembled[j].Bytes, k, assembled[i].Bytes, assembled[i].Bytes.Length);
                        assembled[j].CreateThreadAndWait = Math.Max(assembled[j].CreateThreadAndWait, assembled[i].CreateThreadAndWait); //should always pick i
                        //mark it as empty
                        assembled[i].Bytes.SetLength(0);
                        assembled[i].Address = UIntPtr.Zero;
                        assembled[i].CreateThreadAndWait = -1;
                    }
                    else
                        j = i; //new block
                }
                #endregion
                //we're still here so, inject it
                for (i = 0; i < assembled.Length; i++)
                {
                    if (assembled[i].Bytes.Length == 0)
                        continue;
                    var testPtr = assembled[i].Address;
                    if (createScript)
                    {
                        scriptBytes.Add(AScriptObjectType.Poke, testPtr, new AByteArray(assembled[i].Bytes.TakeAll()));
                        ok1 = true;
                        ok2 = true;
                    }
                    else
                    {
                        ok1 = AMemoryHelper.FullAccess(process.Handle, testPtr.ToIntPtr(), assembled[i].Bytes.Length);
                        ok2 = process.Memory.Write((IntPtr)testPtr.ToUInt64(), assembled[i].Bytes.TakeAll()) == assembled[i].Bytes.Length;
                    }
                    if (!ok1)
                        ok2 = false;
                    #region Create Thread And Wait
                    if (ok2 & (assembled[i].CreateThreadAndWait != -1))
                    {
                        //create threads
                        for (j = 0; j <= assembled[i].CreateThreadAndWait; j++)
                        {
                            if (createThreadAndWait[j].Position != -1)
                            {
                                //create the thread and wait for it's result
                                testPtr = GetAddressFromScript(createThreadAndWait[j].Name, targetSelf, labels, allocs, kAllocs, defines);
                                var threadHandle = process.ThreadFactory.Create(testPtr.ToIntPtr());
                                ok2 = threadHandle.Id != 0;
                                if (ok2)
                                {
                                    try
                                    {
                                        k = createThreadAndWait[j].Timeout;
                                        var y = 0u;
                                        if (k <= 0)
                                            y = 0;
                                        else
                                            y = (uint)k;
                                        var epoch = UDateTime.EpochMil();
                                        while (threadHandle.IsAlive)
                                        {
                                            if (y > 0 && UDateTime.EpochMil(epoch) > y)
                                            {
                                                threadHandle.Terminate();
                                                threadHandle.Dispose();
                                                break;
                                            }
                                            UDateTime.Sleep(10);
                                        }
                                    }
                                    catch
                                    {
                                        threadHandle?.Dispose();
                                    }
                                }
                                createThreadAndWait[j].Position = -1; //mark it as handled
                            }
                        }
                    }
                    #endregion
                }
                if (!ok2)
                {
                    if (popUpMessages)
                        MessageBox.Show(rsNotAllInstructionsCouldBeInjected);
                }
                else
                {
                    if (disableInfo != null)
                    {
                        //see if all allocs are deallocated
                        for (i = 0; i < disableInfo.Allocs.Length; i++)
                        {
                            //free the ceinternal_autofree entries (if they aren't already marked)
                            if (disableInfo.Allocs[i].Name.ToUpper().StartsWith("ceinternal_autofree"))
                            {
                                ok1 = false;
                                for (j = 0; j < dealloc.Length; j++)
                                {
                                    if (dealloc[j] == disableInfo.Allocs[i].Address)
                                    {
                                        ok1 = true;
                                        break;
                                    }
                                }
                                if (ok1 == false)  //not in the list yet, add it
                                {
                                    j = dealloc.Length;
                                    dealloc.SetLength(j - 1);
                                    dealloc[j] = disableInfo.Allocs[i].Address;
                                }
                            }
                        }
                        if (dealloc.Length > 0 && (dealloc.Length == disableInfo.Allocs.Length))  //free everything
                        {
                            for (i = 0; i < disableInfo.Allocs.Length; i++)
                            {
                                AMemoryHelper.Free(process.Handle, dealloc[i].ToIntPtr());
                                // todo add back when exceptions added
                                //if ((targetSelf == false) & AllocsAddToUnexpectedExceptionList)
                                //    RemoveUnexpectedExceptionRegion(dealloc[i], 0);
                            }
                            // todo add back when cc code adde
                            //disableInfo.CCodeSymbols.clear;
                            //disableInfo.CCodeSymbols.unregisterlist;
                        }
                        disableInfo.Allocs.SetLength(allocs.Length);
                        for (i = 0; i < allocs.Length; i++)
                            disableInfo.Allocs[i] = allocs[i];
                    }
                    #region Exceptions
                    //if ((length(disableinfo.exceptions) > 0) & (autoassemblerexceptionhandlerhasentries))
                    //{
                    //    for (i = 0; i <= length(disableinfo.exceptions) - 1; i++)
                    //        autoassemblerexceptionhandlerremoveexceptionrange(disableinfo.exceptions[i]);
                    //    autoassemblerexceptionhandlerapplychanges;
                    //}
                    //setlength(disableinfo.exceptions, length(exceptionlist));
                    //for (i = 0; i <= length(disableinfo.exceptions) - 1; i++)
                    //    disableinfo.exceptions[i] = getaddressfromscript(exceptionlist[i].trylabel);
                    #endregion
                    //check the addsymbollist array and deletesymbollist array
                    //first delete
                    for (i = 0; i < deleteSymbolList.Length; i++)
                        symHandler.DeleteUserDefinedSymbol(deleteSymbolList[i]);
                    //now scan the addsymbollist array and add them to the userdefined list
                    for (i = 0; i < addSymbolList.Length; i++)
                    {
                        ok1 = false;
                        for (j = 0; j < allocs.Length; j++)
                        {

                            if (String.Equals(addSymbolList[i], allocs[j].Name, StringComparison.CurrentCultureIgnoreCase))
                            {
                                try
                                {
                                    symHandler.DeleteUserDefinedSymbol(addSymbolList[i]); //delete old one so you can add the new one
                                    symHandler.AddUserDefinedSymbol(AStringUtils.IntToHex(allocs[j].Address, 8), addSymbolList[i]);
                                    ok1 = true;
                                }
                                catch
                                {
                                    //don't crash when it's already defined or address=0
                                }
                                break;
                            }
                        }
                        if (!ok1)
                        {
                            for (j = 0; j < labels.Length; j++)
                            {
                                if (String.Equals(addSymbolList[i], labels[j].Name, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    try
                                    {
                                        symHandler.DeleteUserDefinedSymbol(addSymbolList[i]); //delete old one so you can add the new one
                                        symHandler.AddUserDefinedSymbol(AStringUtils.IntToHex(labels[j].Address, 8), addSymbolList[i]);
                                        ok1 = true;
                                    }
                                    catch
                                    {
                                        //don't crash when it's already defined or address=0
                                    }

                                }
                            }
                        }
                        if (!ok1)
                        {
                            for (j = 0; j < defines.Length; j++)
                            {
                                if (String.Equals(addSymbolList[i], defines[j].Name, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    try
                                    {
                                        symHandler.DeleteUserDefinedSymbol(addSymbolList[i]); //delete old one so you can add the new one
                                        symHandler.AddUserDefinedSymbol(defines[j].Whatever, addSymbolList[i]);
                                        ok1 = true;
                                    }
                                    catch
                                    {
                                        //don't crash when it's already defined or address=0
                                    }
                                }
                            }
                        }
                    }
                    #region Create Threads
                    //still here, so create threads if needed
                    if (createThread.Length > 0)
                    {
                        for (i = 0; i < createThread.Length; i++)
                        {
                            ok1 = true;
                            var testPtr = UIntPtr.Zero;
                            try
                            {
                                testPtr = symHandler.GetAddressFromName(createThread[i]);
                            }
                            catch
                            {
                                ok1 = false;
                            }
                            if (!ok1)
                            {
                                for (j = 0; j < labels.Length; j++)
                                {
                                    if (String.Equals(labels[j].Name, createThread[i], StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        ok1 = true;
                                        testPtr = labels[j].Address;
                                        break;
                                    }
                                }
                            }
                            if (!ok1)
                            {
                                for (j = 0; j < allocs.Length; j++)
                                {
                                    if (String.Equals(kAllocs[j].Name, createThread[i], StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        ok1 = true;
                                        testPtr = allocs[j].Address;
                                        break;
                                    }
                                }
                            }
                            if (!ok1)
                            {
                                for (j = 0; j < kAllocs.Length; j++)
                                {
                                    if (String.Equals(kAllocs[j].Name, createThread[i], StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        ok1 = true;
                                        testPtr = kAllocs[j].Address;
                                        break;
                                    }
                                }
                            }
                            if (!ok1)
                            {
                                for (j = 0; j < defines.Length; j++)
                                {
                                    if (String.Equals(defines[j].Name, createThread[i], StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        try
                                        {
                                            testPtr = symHandler.GetAddressFromName(defines[j].Whatever);
                                            ok1 = true;
                                        }
                                        catch
                                        {
                                            // ignored
                                        }
                                        break;
                                    }
                                }
                            }
                            if (ok1)  //address found
                            {
                                try
                                {
                                    var threadHandle = process.ThreadFactory.Create(testPtr.ToIntPtr());
                                    // todo why are we closing the thread?
                                    ok2 = threadHandle.Id != 0;
                                    if (ok2)
                                        threadHandle.Dispose(); // lose control of the thread and let it run wild
                                }
                                catch
                                {
                                    // ignored
                                }
                            }
                        }
                    }
                    #endregion
                    //fill "allSymbols"
                    if (disableInfo != null)
                    {
                        for (i = 0; i < labels.Length; i++)
                            disableInfo.AllSymbols.Add(new ARefString(labels[i].Name, labels[i].Address));
                        for (i = 0; i < allocs.Length; i++)
                            disableInfo.AllSymbols.Add(new ARefString(allocs[i].Name, allocs[i].Address));
                        for (i = 0; i < kAllocs.Length; i++)
                            disableInfo.AllSymbols.Add(new ARefString(kAllocs[i].Name, kAllocs[i].Address));
                        for (i = 0; i < defines.Length; i++)
                        {
                            var testPtr = symHandler.GetAddressFromName(defines[i].Whatever, false, out ok1);
                            if (ok1 == false)
                                disableInfo.AllSymbols.Add(new ARefString(defines[i].Name, testPtr));
                        }
                    }
                    if (popUpMessages)
                    {
                        var testPtr = UIntPtr.Zero;
                        var s1 = "";
                        for (i = 0; i < globalAllocs.Length; i++)
                        {
                            if (testPtr == UIntPtr.Zero)
                                testPtr = globalAllocs[i].Address;
                            s1 = s1 + "\r\n" + globalAllocs[i].Name + '=' + AStringUtils.IntToHex(globalAllocs[i].Address, 8);
                        }
                        for (i = 0; i < allocs.Length; i++)
                        {
                            if (allocs[i].Name.ToUpper().StartsWith("ceinternal_"))
                                continue; //don't show these
                            if (testPtr == UIntPtr.Zero)
                                testPtr = allocs[i].Address;
                            s1 = s1 + "\r\n" + allocs[i].Name + '=' + AStringUtils.IntToHex(allocs[i].Address, 8);
                        }
                        if (kAllocs.Length > 0)
                        {
                            if (testPtr == UIntPtr.Zero)
                                testPtr = kAllocs[i].Address;
                            s1 = "\r\n" + rsTheFollowingKernelAddressesWhereAllocated + ':';
                            for (i = 0; i < kAllocs.Length; i++)
                                s1 = s1 + "\r\n" + kAllocs[i].Name + '=' + AStringUtils.IntToHex(kAllocs[i].Address, 8);
                        }
                        MessageBox.Show(rsTheCodeInjectionWasSuccessfull + s1);
                    }
                }
                result = ok2;
                // todo add back when exceptions are added
                //if (result & allocsaddtounexpectedexceptionlist & (~targetself))
                //{
                //    for (i = 0; i < allocs.Length; i++)
                //        addunexpectedexceptionregion(allocs[i].address, allocs[i].size);
                //}
            }
            finally
            {
                if (targetSelf)
                {
                    // todo add back when handling self
                    //processhandler.processhandle = oldhandle;
                    //symhandler = oldsymhandler;
                }
            }
            return result;
        }
        #endregion
        #region Assemble
        public AScriptBytesArray Assemble(AProcessSharp process, ARefStringArray code)
        {
            var scr = new AScriptBytesArray();
            var info = new ADisableInfo();
            var ret = AutoAssemble2(process, code, false, false, false, info, true, scr);
            if (!ret || scr.Length < 1)
                return new AScriptBytesArray();
            return scr;
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
