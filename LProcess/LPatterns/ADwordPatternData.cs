using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SputnikAsm.LProcess.LPatterns
{
    public class ADwordPatternData : IAMemoryPattern
    {
        private readonly byte[] _bytes;
        private readonly string _mask;

        public readonly string PatternText;

        public ADwordPatternData(string pattern) : this(pattern, 0, APatternScannerAlgorithm.Naive) { }
        public ADwordPatternData(string pattern, int offset) : this(pattern, offset, APatternScannerAlgorithm.Naive) { }
        public ADwordPatternData(string pattern, APatternScannerAlgorithm algorithm) : this(pattern, 0, algorithm) { }
        public ADwordPatternData(string pattern, int offset, APatternScannerAlgorithm algorithm)
        {
            PatternText = pattern;
            PatternType = AMemoryPatternType.Data;
            Offset = offset;
            Algorithm = algorithm;
            _bytes = GetBytesFromDwordPattern(pattern);
            _mask = GetMaskFromDwordPattern(pattern);
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