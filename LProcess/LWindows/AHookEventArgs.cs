using System;
using SputnikAsm.LProcess.LWindows.LMouse;

namespace SputnikAsm.LProcess.LWindows
{
    public abstract class AHookEventArgs : EventArgs
    {
        protected HookEventType EventType { get; set; }
    }
}