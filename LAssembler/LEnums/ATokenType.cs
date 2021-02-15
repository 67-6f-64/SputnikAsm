namespace SputnikAsm.LAssembler.LEnums
{
    public enum ATokenType
    {
        ttinvalidtoken, ttregister8bit, ttregister16bit, ttregister32bit, ttregister64bit, ttregister8bitwithprefix,
        ttregistermm, ttregisterxmm, ttregisterymm, ttregisterst, ttregistersreg,
        ttregistercr, ttregisterdr, ttmemorylocation, ttmemorylocation8,
        ttmemorylocation16, ttmemorylocation32, ttmemorylocation64,
        ttmemorylocation80, ttmemorylocation128, ttmemorylocation256, ttvalue
    }
}
