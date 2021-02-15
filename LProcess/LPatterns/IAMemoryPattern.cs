using System.Collections.Generic;

namespace SputnikAsm.LProcess.LPatterns
{
    public interface IAMemoryPattern
    {
        int Offset { get; }
        AMemoryPatternType PatternType { get; }
        IList<byte> GetBytes();
        string GetMask();
        APatternScannerAlgorithm Algorithm { get; }
    }
}