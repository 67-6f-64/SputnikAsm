namespace SputnikAsm.LAssembler.LEnums
{
    public enum ATokenType
    {
        Invalid, 
        Register8Bit, 
        Register16Bit, 
        Register32Bit, 
        Register64Bit, 
        Register8BitWithPrefix,
        RegisterMm, 
        RegisterXmm, 
        RegisterYmm, 
        RegisterSt, 
        RegisterSReg,
        RegisterCr, 
        RegisterDr, 
        MemoryLocation,
        MemoryLocation8,
        MemoryLocation16, 
        MemoryLocation32, 
        MemoryLocation64,
        MemoryLocation80, 
        MemoryLocation128,
        MemoryLocation256,
        Value
    }
}
