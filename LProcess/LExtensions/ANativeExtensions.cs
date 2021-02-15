namespace SputnikAsm.LProcess.LExtensions
{
    public static class ANativeExtensions
    {
        public static int LowWord(this int i)
        {
            return i & 0xFFFF;
        }
        public static int HighWord(this int i)
        {
            return (i >> 16) & 0xFFFF;
        }
    }
}