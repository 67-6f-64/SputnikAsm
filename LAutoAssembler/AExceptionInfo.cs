using System;

namespace SputnikAsm.LAutoAssembler
{
    public class AExceptionInfo
    {
        #region Variables
        public String TryLabel;
        public String ExceptLabel;
        #endregion
        #region Constructor
        public AExceptionInfo()
        {
            TryLabel = "";
            ExceptLabel = "";
        }
        public AExceptionInfo(String tryLabel, String exceptLabel)
        {
            TryLabel = tryLabel;
            ExceptLabel = exceptLabel;
        }
        #endregion
    };
}
