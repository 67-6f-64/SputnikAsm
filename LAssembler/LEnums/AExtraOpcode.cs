namespace SputnikAsm.LAssembler.LEnums
{
    public enum AExtraOpCode
    {
        eo_none,
        eo_reg0, eo_reg1, eo_reg2, eo_reg3, eo_reg4, eo_reg5, eo_reg6, eo_reg7, // /digit
        eo_reg, //  /r
        eo_cb, eo_cw, eo_cd, eo_cp,
        eo_ib, eo_iw, eo_id,
        eo_prb, eo_prw, eo_prd,
        eo_pi,
        last_textraopcode
    }
}
