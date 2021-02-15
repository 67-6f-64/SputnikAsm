using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tack.LAutoAssembler
{
    public class tindex
    {
        public int startentry;
        public tindexarray subindex;
        public int nextentry;
        public tindex()
        {
            startentry = 0;
            subindex = null;
            nextentry = 0;
        }
    }
}
