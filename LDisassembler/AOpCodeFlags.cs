using System;

namespace SputnikAsm.LDisassembler
{
    public class AOpCodeFlags
    {
        #region Variables
        public int Pp;
        public Boolean L;
        public int Vvvv;
        public int Mmmmm;
        public Boolean W;
        public Boolean B;
        public Boolean X;
        public Boolean R;
        public Boolean SkipExtraRegOnMemoryAccess;
        public Boolean SkipExtraReg;
        #endregion
        #region Constructor
        public AOpCodeFlags()
        {
            Clear();
        }
        #endregion
        #region Clear
        public void Clear()
        {
            Pp = 0;
            L = false;
            Vvvv = 0;
            Mmmmm = 0;
            W = false;
            B = false;
            X = false;
            R = false;
            SkipExtraRegOnMemoryAccess = false;
            SkipExtraReg = false;
        }
        #endregion
    }
}
