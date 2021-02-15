using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tack.LAutoAssembler
{
    public enum ttokentype
    {
        ttinvalidtoken, ttregister8bit, ttregister16bit, ttregister32bit, ttregister64bit, ttregister8bitwithprefix,
        ttregistermm, ttregisterxmm, ttregisterst, ttregistersreg,
        ttregistercr, ttregisterdr, ttmemorylocation, ttmemorylocation8,
        ttmemorylocation16, ttmemorylocation32, ttmemorylocation64,
        ttmemorylocation80, ttmemorylocation128, ttvalue, last_ttokentype
    }
}
