using System;
using System.Collections.Generic;

namespace SputnikAsm.LProcess.LWindows
{
    public interface IAWindowFactory : IDisposable
    {
        IEnumerable<IAWindow> this[string windowTitle] { get; }

        IEnumerable<IAWindow> ChildWindows { get; }
        IAWindow MainWindow { get; set; }
        IEnumerable<IAWindow> Windows { get; }

        IEnumerable<IAWindow> GetWindowsByClassName(string className);
        IEnumerable<IAWindow> GetWindowsByTitle(string windowTitle);
        IEnumerable<IAWindow> GetWindowsByTitleContains(string windowTitle);
    }
}