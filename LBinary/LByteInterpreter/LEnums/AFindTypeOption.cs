using System;

namespace SputnikAsm.LBinary.LByteInterpreter.LEnums
{
    [Flags]
    public enum AFindTypeOption
    {
        None = 1 << 0,
        NoString = 1 << 1,
        NoDouble = 1 << 2,
    }
}
