using System;
using System.Collections.Generic;
using System.Linq;
using SputnikAsm.LProcess.Utilities;

namespace SputnikAsm.LProcess.LWindows
{
    /// <summary>
    ///     Class providing tools for manipulating windows.
    /// </summary>
    public class AWindowFactory : IAWindowFactory
    {
        private readonly AProcessSharp _process;
        private IAWindow _mainWindow;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AWindowFactory" /> class.
        /// </summary>
        public AWindowFactory(AProcessSharp process)
        {
            // Save the parameter
            _process = process;
        }

        /// <summary>
        ///     Gets all the child window handles that belong to the application.
        /// </summary>
        internal IEnumerable<IntPtr> ChildWindowHandles
            => AWindowHelper.EnumChildWindows(_process.Native.MainWindowHandle);

        /// <summary>
        ///     Gets all the window handles that belong to the application.
        /// </summary>
        internal IEnumerable<IntPtr> WindowHandles => new List<IntPtr>(ChildWindowHandles) {MainWindowHandle};

        /// <summary>
        ///     Gets the main window handle of the application.
        /// </summary>
        public IntPtr MainWindowHandle => _process.Native.MainWindowHandle;

        /// <summary>
        ///     Gets all the child windows that belong to the application.
        /// </summary>
        public IEnumerable<IAWindow> ChildWindows
        {
            get { return ChildWindowHandles.Select(handle => new ARemoteWindow(_process, handle)); }
        }

        /// <summary>
        ///     Gets the main window of the application. 
        /// </summary>
        /// <remarks>
        /// If your applications main window has child windows as well, this will not always work.In this case, 
        /// you can set the main window property and it will use that value instead from that point
        /// on. You can set the value to null if you would like the return return value to go back to default. If your applications main window has child windows as well, this will not
        /// always work. In this case, you can set the main window property and it will use that value instead from that point
        ///  on. You can set the value to null if you would like the return return value to go back to default.
        /// </remarks>
        public IAWindow MainWindow
        {
            get
            {
                return _mainWindow ?? new ARemoteWindow(_process, MainWindowHandle);
            }
            set { _mainWindow = value; }
        }

        /// <summary>
        ///     Gets all the windows that have the same specified title.
        /// </summary>
        /// <param name="windowTitle">The window title string.</param>
        /// <returns>A collection of <see cref="ARemoteWindow" />.</returns>
        public IEnumerable<IAWindow> this[string windowTitle] => GetWindowsByTitle(windowTitle);

        /// <summary>
        ///     Gets all the windows that belong to the application.
        /// </summary>
        public IEnumerable<IAWindow> Windows
        {
            get { return WindowHandles.Select(handle => new ARemoteWindow(_process, handle)); }
        }

        /// <summary>
        ///     Gets all the windows that have the specified class name.
        /// </summary>
        /// <param name="className">The class name string.</param>
        /// <returns>A collection of <see cref="ARemoteWindow" />.</returns>
        public IEnumerable<IAWindow> GetWindowsByClassName(string className)
        {
            return WindowHandles
                .Where(handle => AWindowHelper.GetClassName(handle) == className)
                .Select(handle => new ARemoteWindow(_process, handle));
        }

        /// <summary>
        ///     Gets all the windows that have the same specified title.
        /// </summary>
        /// <param name="windowTitle">The window title string.</param>
        /// <returns>A collection of <see cref="ARemoteWindow" />.</returns>
        public IEnumerable<IAWindow> GetWindowsByTitle(string windowTitle)
        {
            return WindowHandles
                .Where(handle => AWindowHelper.GetWindowText(handle) == windowTitle)
                .Select(handle => new ARemoteWindow(_process, handle));
        }

        /// <summary>
        ///     Gets all the windows that contain the specified title.
        /// </summary>
        /// <param name="windowTitle">A part a window title string.</param>
        /// <returns>A collection of <see cref="ARemoteWindow" />.</returns>
        public IEnumerable<IAWindow> GetWindowsByTitleContains(string windowTitle)
        {
            return WindowHandles
                .Where(handle => AWindowHelper.GetWindowText(handle).Contains(windowTitle))
                .Select(handle => new ARemoteWindow(_process, handle));
        }

        /// <summary>
        ///     Releases all resources used by the <see cref="AWindowFactory" /> object.
        /// </summary>
        public void Dispose()
        {
            // Nothing to dispose... yet
        }
    }
}