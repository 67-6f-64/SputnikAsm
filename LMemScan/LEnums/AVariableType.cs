namespace SputnikAsm.LMemScan.LEnums
{
    public enum AVariableType
    {
        Byte = 0, 
        Word = 1, 
        DWord = 2, 
        QWord = 3, 
        Single = 4,
        Double = 5, 
        String = 6, 
        UnicodeString = 7, 
        ByteArray = 8, 
        Binary = 9, 
        All = 10, // grouped and MultiByteArray are special types
        AutoAssembler = 11, 
        Pointer = 12, 
        Custom = 13, 
        Grouped = 14, 
        ByteArrays = 15
    }
}
