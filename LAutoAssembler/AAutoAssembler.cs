using System;
using Sputnik.LString;
using Sputnik.LUtils;
using SputnikAsm.LAssembler;
using SputnikAsm.LCollections;
using SputnikAsm.LUtils;

namespace SputnikAsm.LAutoAssembler
{
    public class AAutoAssembler
    {
        #region Static Variables
        public String rsForwardJumpWithNoLabelDefined = "Forward jump with no label defined";
        public String rsThereIsCodeDefinedWithoutSpecifyingTheAddressItBel = "There is code defined without specifying the address it belongs to";
        public String rsIsNotAValidBytestring = "%s is not a valid bytestring";
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
        public AAutoAssembler()
        {
            Assembler = new AAssembler();
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
                        break; //all done // todo this is meant to be continue?
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
                                            break; //nope // todo should this be continue
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
            for (i = 0; i <= elements.Length - 1; i++)
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
    }
}
