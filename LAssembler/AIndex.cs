using SputnikAsm.LCollections;

namespace SputnikAsm.LAssembler
{
    public class AIndex
    {
        #region Variables
        public int StartEntry;
        public AIndexArray SubIndex;
        public int NextEntry;
        #endregion
        #region Constructor
        public AIndex()
        {
            StartEntry = 0;
            SubIndex = null;
            NextEntry = 0;
        }
        #endregion
    }
}
