using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SputnikAsm.LDisassembler.LEnums
{
    public enum tregistertype
    {
        rt64,
        rt32,
        rt16,
        rt8,
        rtymm,
        rtxmm,
        rtmm,
        rtsegment,
        rtcontrolregister,
        rtdebugregister
    }
}
