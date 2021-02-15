using System;
using Sputnik.LUtils;
using SputnikAsm.LGenerics;

namespace SputnikAsm.LCollections
{
    public class AStringArray : AArrayManager<String>
    {
        #region Constructor
        public AStringArray()
            : base()
        {
        }
        public AStringArray(params String[] values)
            : base(values)
        {
        }
        #endregion
        #region NextElement
        public override String NextElement(int index)
        {
            return UStringUtils.Empty;
        }
        #endregion
        #region ToString
        public override String ToString()
        {
            return String.Join("\n", TakeAll());
        }
        public String ToString(String separator)
        {
            return String.Join(separator, TakeAll());
        }
        #endregion
    }
}
