using System;

namespace SputnikAsm.LDisassembler
{
    public class opcodeflags
    {
        #region Variables
        public int pp;
        public Boolean l;
        public int vvvv;
        public int mmmmm;
        public Boolean w;
        public Boolean b;
        public Boolean x;
        public Boolean r;
        public Boolean skipextraregonmemoryaccess;
        public Boolean skipextrareg;
        #endregion
        #region Constructor
        public opcodeflags()
        {
            Clear();
        }
        #endregion
        #region Clear
        public void Clear()
        {
            pp = 0;
            l = false;
            vvvv = 0;
            mmmmm = 0;
            w = false;
            b = false;
            x = false;
            r = false;
            skipextraregonmemoryaccess = false;
            skipextrareg = false;
        }
        #endregion
    }
}
