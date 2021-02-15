using System.Linq;
using SputnikAsm.LProcess.LModules;
using SputnikAsm.LProcess.LPatterns;

namespace SputnikAsm.LProcess.Utilities
{
    public class ANaive
    {
        public static int GetIndexOf(IAMemoryPattern pattern, byte[] Data, AProcessSharpModule module)
        {
            var patternData = Data;
            var patternDataLength = patternData.Length;

            for (var offset = 0; offset < patternDataLength; offset++)
            {
                if (
                    pattern.GetMask()
                        .Where((m, b) => m == 'x' && pattern.GetBytes()[b] != patternData[b + offset])
                        .Any())
                    continue;

                return offset;
            }
            return -1;
        }
    }
}
