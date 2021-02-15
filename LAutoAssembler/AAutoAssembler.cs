using System;

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
    }
}
