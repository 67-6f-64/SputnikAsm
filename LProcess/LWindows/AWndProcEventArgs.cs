using System;

namespace SputnikAsm.LProcess.LWindows
{
    public class AWndProcEventArgs : EventArgs
    {
        public AWndProcEventArgs(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            Hwnd = hwnd;
            Msg = msg;
            WParam = wParam;
            LParam = lParam;
        }

        public IntPtr Hwnd { get; }

        public int Msg { get; }

        public IntPtr WParam { get; }

        public IntPtr LParam { get; }
    }
}