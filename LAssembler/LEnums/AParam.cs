﻿namespace SputnikAsm.LAssembler.LEnums
{
    public enum AParam
    {
        par_noparam,
        par_1,
        par_3,
        par_al,
        par_ax,
        par_eax,
        par_cl,
        par_dx,
        par_cs,
        par_ds,
        par_es,
        par_ss,
        par_fs,
        par_gs,
        //regs
        par_r8,
        par_r16,
        par_r32,
        par_r64, //just for a few occasions
        par_mm,
        par_xmm,
        par_st,
        par_st0,
        par_sreg,
        par_cr,
        par_dr,
        //memorylocs
        par_m8,
        par_m16,
        par_m32,
        par_m64,
        par_m80,
        par_m128,
        par_moffs8,
        par_moffs16,
        par_moffs32,
        //regs+memorylocs
        par_rm8,
        par_rm16,
        par_rm32,
        par_r32_m16,
        par_mm_m32,
        par_mm_m64,
        par_xmm_m32,
        par_xmm_m64,
        par_xmm_m128,
        //values
        par_imm8,
        par_imm16,
        par_imm32,
        //relatives
        par_rel8,
        par_rel16,
        par_rel32,
        last_tparam
    }
}