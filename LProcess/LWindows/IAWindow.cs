﻿using System;
using System.Collections.Generic;
using SputnikAsm.LProcess.LNative;
using SputnikAsm.LProcess.LNative.LTypes;
using SputnikAsm.LProcess.LThreads;
using SputnikAsm.LProcess.LWindows.LKeyboard;
using SputnikAsm.LProcess.LWindows.LMouse;

namespace SputnikAsm.LProcess.LWindows
{
    /// <summary>
    ///     An interface that defines operations and values r
    /// </summary>
    public interface IAWindow : IDisposable
    {
        /// <summary>
        ///     Gets all the child windows of this window.
        /// </summary>
        IEnumerable<IAWindow> Children { get; }

        /// <summary>
        ///     Gets the class name of the window.
        /// </summary>
        string ClassName { get; }

        /// <summary>
        ///     The handle of the window.
        /// </summary>
        /// <remarks>
        ///     The type here is not <see cref="ASafeMemoryHandle" /> because a window cannot be closed by calling
        ///     <see cref="Kernel32.CloseHandle" />.
        ///     For more information, see:
        ///     http://stackoverflow.com/questions/8507307/why-cant-i-close-the-window-handle-in-my-code.
        /// </remarks>
        IntPtr Handle { get; }

        /// <summary>
        ///     Gets or sets the height of the element.
        /// </summary>
        int Height { get; set; }

        /// <summary>
        ///     Gets if the window is currently activated.
        /// </summary>
        bool IsActivated { get; }

        /// <summary>
        ///     Gets if this is the main window.
        /// </summary>
        bool IsMainWindow { get; }

        /// <summary>
        ///     Tools for managing a virtual keyboard in the window.
        /// </summary>
        IAKeyboard Keyboard { get; set; }

        /// <summary>
        ///     Tools for managing a virtual mouse in the window.
        /// </summary>
        IAMouse Mouse { get; set; }

        /// <summary>
        ///     Gets or sets the placement of the window.
        /// </summary>
        WindowPlacement Placement { get; set; }

        /// <summary>
        ///     Gets or sets the specified window's show state.
        /// </summary>
        WindowStates State { get; set; }

        /// <summary>
        ///     Gets the thread of the window.
        /// </summary>
        IARemoteThread Thread { get; }

        /// <summary>
        ///     Gets or sets the title of the window.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        ///     Gets or sets the width of the element.
        /// </summary>
        int Width { get; set; }

        /// <summary>
        ///     Gets or sets the x-coordinate of the window.
        /// </summary>
        int X { get; set; }

        /// <summary>
        ///     Gets or sets the y-coordinate of the window.
        /// </summary>
        int Y { get; set; }

        /// <summary>
        ///     Activates the window.
        /// </summary>
        void Activate();

        /// <summary>
        ///     Closes the window.
        /// </summary>
        void Close();

        /// <summary>
        ///     Flashes the window, typically one time. It does not change the active state of the window.
        /// </summary>
        void Flash();

        /// <summary>
        ///     Flashes the window. It does not change the active state of the window.
        /// </summary>
        /// <param name="count">The number of times to flash the window.</param>
        /// <param name="timeout">The rate at which the window is to be flashed.</param>
        /// <param name="flags">The flash status.</param>
        void Flash(int count, TimeSpan timeout, FlashWindowFlags flags = FlashWindowFlags.All);

        /// <summary>
        ///     Places (posts) a message in the message queue associated with the thread that created the window and returns
        ///     without waiting for the thread to process the message.
        /// </summary>
        /// <param name="message">The message to be posted.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        void PostMessage(WindowsMessages message, IntPtr wParam, IntPtr lParam);

        /// <summary>
        ///     Places (posts) a message in the message queue associated with the thread that created the window and returns
        ///     without waiting for the thread to process the message.
        /// </summary>
        /// <param name="message">The message to be posted.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        void PostMessage(int message, IntPtr wParam, IntPtr lParam);

        /// <summary>
        ///     Sends the specified message to a window or windows.
        ///     The SendMessage function calls the window procedure for the specified window and does not return until the window
        ///     procedure has processed the message.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
        IntPtr SendMessage(WindowsMessages message, IntPtr wParam, IntPtr lParam);

        /// <summary>
        ///     Sends the specified message to a window or windows.
        ///     The SendMessage function calls the window procedure for the specified window and does not return until the window
        ///     procedure has processed the message.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
        IntPtr SendMessage(int message, IntPtr wParam, IntPtr lParam);
    }
}