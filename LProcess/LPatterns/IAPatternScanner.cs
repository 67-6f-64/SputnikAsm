namespace SputnikAsm.LProcess.LPatterns
{
    public interface IAPatternScanner
    {
        APatternScanResult Find(IAMemoryPattern pattern);
    }
}