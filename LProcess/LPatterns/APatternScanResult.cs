using System;

namespace SputnikAsm.LProcess.LPatterns
{
    public struct APatternScanResult
    {
        public IntPtr ReadAddress { get; set; }
        public IntPtr BaseAddress { get; set; }
        public int Offset { get; set; }
        public bool Found { get; set; }
    }
}