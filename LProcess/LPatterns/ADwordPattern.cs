using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SputnikAsm.LProcess.LPatterns
{
    public class ADwordPattern : IAMemoryPattern
    {
        private readonly byte[] _bytes;
        private readonly string _mask;

        public readonly string PatternText;

        public ADwordPattern(string dwordPattern) : this(dwordPattern, APatternScannerAlgorithm.Naive) { }
        public ADwordPattern(string dwordPattern, APatternScannerAlgorithm algorithm)
        {
            PatternText = dwordPattern;
            PatternType = AMemoryPatternType.Function;
            Offset = 0;
            Algorithm = algorithm;
            _bytes = GetBytesFromDwordPattern(dwordPattern);
            _mask = GetMaskFromDwordPattern(dwordPattern);
        }

        public IList<byte> GetBytes()
        {
            return _bytes;
        }

        public string GetMask()
        {
            return _mask;
        }

        public int Offset { get; }
        public AMemoryPatternType PatternType { get; }
        public APatternScannerAlgorithm Algorithm { get; }

        private static string GetMaskFromDwordPattern(string pattern)
        {
            var mask = pattern.Split(' ').Select(s => s.Contains('?') ? "?" : "x");

            return string.Concat(mask);
        }

        private static byte[] GetBytesFromDwordPattern(string pattern)
        {
            return
                pattern.Split(' ')
                    .Select(s => s.Contains('?') ? (byte) 0 : byte.Parse(s, NumberStyles.HexNumber))
                    .ToArray();
        }

        public override string ToString()
        {
            return PatternText;
        }
    }
}