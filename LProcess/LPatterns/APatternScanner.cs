using System;
using System.Linq;
using SputnikAsm.LProcess.LModules;

namespace SputnikAsm.LProcess.LPatterns
{
    public class APatternScanner : IAPatternScanner
    {
        private readonly AProcessSharpModule _module;
        private readonly int _offsetFromBaseAddress;

        private static readonly APatternScanResult EmptyPatternScanResult = new APatternScanResult
        {
            BaseAddress = IntPtr.Zero,
            ReadAddress = IntPtr.Zero,
            Offset = 0,
            Found = false
        };

        public APatternScanner(AProcessSharpModule module) : this(module, 0) { }
        public APatternScanner(AProcessSharpModule module, int offsetFromBaseAddress)
        {
            _module = module;
            _offsetFromBaseAddress = offsetFromBaseAddress;
            Data = module.Read(_offsetFromBaseAddress, _module.Size - _offsetFromBaseAddress);
        }

        public byte[] Data { get; }

        public APatternScanResult Find(IAMemoryPattern pattern)
        {
            switch(pattern.PatternType)
            {
                case AMemoryPatternType.Function:
                    return FindFunctionPattern(pattern);
                case AMemoryPatternType.Data:
                    return FindDataPattern(pattern);
            }
            throw new NotImplementedException("PatternScanner encountered an unknown MemoryPatternType: " + pattern.PatternType + ".");
        }

        

        private int GetOffset(IAMemoryPattern pattern)
        {
            switch(pattern.Algorithm)
            {
                case APatternScannerAlgorithm.BoyerMooreHorspool:
                    return Utilities.ABoyerMooreHorspool.IndexOf(Data, pattern.GetBytes().ToArray());
                case APatternScannerAlgorithm.Naive:
                    return Utilities.ANaive.GetIndexOf(pattern, Data, _module);
            }
            throw new NotImplementedException("GetOffset encountered an unknown PatternScannerAlgorithm: " + pattern.Algorithm + ".");
        }

        private APatternScanResult FindFunctionPattern(IAMemoryPattern pattern)
        {
            var offset = GetOffset(pattern);
            if (offset != -1)
            {
                return new APatternScanResult
                {
                    BaseAddress = _module.BaseAddress + offset + _offsetFromBaseAddress,
                    ReadAddress = _module.BaseAddress + offset + _offsetFromBaseAddress,
                    Offset = offset + _offsetFromBaseAddress,
                    Found = true
                };
            }
            return EmptyPatternScanResult;
        }

        private APatternScanResult FindDataPattern(IAMemoryPattern pattern)
        {
            var result = new APatternScanResult();
            var offset = GetOffset(pattern);

            if ( offset != -1)
            {
                // If this area is reached, the pattern has been found.
                result.Found = true;
                result.ReadAddress = _module.Read<IntPtr>(offset + pattern.Offset);
                result.BaseAddress = new IntPtr(result.ReadAddress.ToInt64() - _module.BaseAddress.ToInt64());
                result.Offset = offset;
                return result;
            }
            // If this is reached, the pattern was not found.
            return EmptyPatternScanResult;
        }
    }
}