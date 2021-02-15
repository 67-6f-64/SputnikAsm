using System;

namespace SputnikAsm.LProcess.LMarshaling
{
    /// <summary>
    ///     Defines an IDisposable interface with a known state.
    /// </summary>
    public interface IADisposableState : IDisposable
    {
        /// <summary>
        ///     Gets a value indicating whether the element is disposed.
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        ///     Gets a value indicating whether the element must be disposed when the Garbage Collector collects the object.
        /// </summary>
        bool MustBeDisposed { get; }
    }
}