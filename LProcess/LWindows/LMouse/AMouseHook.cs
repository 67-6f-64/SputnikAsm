using System;
using System.Runtime.InteropServices;
using SputnikAsm.LProcess.LApplied;
using SputnikAsm.LProcess.LNative;
using SputnikAsm.LProcess.LNative.LTypes;

namespace SputnikAsm.LProcess.LWindows.LMouse
{
    public enum HookEventType
    {
        Keyboard,
        Mouse
    }

    public sealed class AMouseHook : IAApplied
    {
        private readonly LowLevelProc _callback;

        private IntPtr _hookId;

        public AMouseHook(string identifier)
        {
            Identifier = identifier;
            _callback = MouseHookCallback;
        }

        public bool IsEnabled { get; private set; }

        public bool IsDisposed { get; set; }
        public bool MustBeDisposed { get; set; } = true;
        public string Identifier { get; }

        public void Enable()
        {
            _hookId = User32.SetWindowsHook(HookType.WH_MOUSE_LL, _callback);
            IsEnabled = true;
        }

        public void Disable()
        {
            if (!IsEnabled) return;
            User32.UnhookWindowsHookEx(_hookId);
            IsEnabled = false;
        }

        /// <summary>
        ///     This is the callback method that is called whenever a low level mouse event is triggered.
        ///     We use it to call our individual custom events.
        /// </summary>
        private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var lParamStruct = (MSLLHOOKSTRUCT) Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                var e = new AMouseHookEventArgs(lParamStruct);
                switch ((MouseMessages) wParam)
                {
                    case MouseMessages.WmMouseMove:
                        TriggerMouseEvent(e, MouseEventNames.MouseMove, OnMove);
                        break;
                    case MouseMessages.WmLButtonDown:
                        TriggerMouseEvent(e, MouseEventNames.LeftButtonDown, OnLeftButtonDown);
                        break;
                    case MouseMessages.WmLButtonUp:
                        TriggerMouseEvent(e, MouseEventNames.LeftButtonUp, OnLeftButtonUp);
                        break;
                    case MouseMessages.WmRButtonDown:
                        TriggerMouseEvent(e, MouseEventNames.RightButtonDown, OnRightButtonDown);
                        break;
                    case MouseMessages.WmRButtonUp:
                        TriggerMouseEvent(e, MouseEventNames.RightButtonUp, OnRightButtonUp);
                        break;
                    case MouseMessages.WmMButtonDown:
                        TriggerMouseEvent(e, MouseEventNames.MiddleButtonDown, OnMiddleButtonDown);
                        break;
                    case MouseMessages.WmMButtonUp:
                        TriggerMouseEvent(e, MouseEventNames.MouseMove, OnMove);
                        e.MouseEventName = MouseEventNames.MiddleButtonUp;
                        OnMiddleButtonUp(e);
                        break;
                    case MouseMessages.WmMouseWheel:
                        TriggerMouseEvent(e, MouseEventNames.MouseMove, OnMove);
                        e.MouseEventName = MouseEventNames.MouseWheel;
                        OnWheel(e);
                        break;
                }
            }
            return (IntPtr) User32.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        private static void TriggerMouseEvent(AMouseHookEventArgs e, MouseEventNames name,
            Action<AMouseHookEventArgs> method)
        {
            e.MouseEventName = name;
            method(e);
        }

        #region Custom Events

        public event EventHandler<AMouseHookEventArgs> Move;

        private void OnMove(AMouseHookEventArgs e)
        {
            Move?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<AMouseHookEventArgs> LeftButtonDown;

        private void OnLeftButtonDown(AMouseHookEventArgs e)
        {
            LeftButtonDown?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<AMouseHookEventArgs> LeftButtonUp;

        private void OnLeftButtonUp(AMouseHookEventArgs e)
        {
            LeftButtonUp?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<AMouseHookEventArgs> RightButtonDown;

        private void OnRightButtonDown(AMouseHookEventArgs e)
        {
            RightButtonDown?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<AMouseHookEventArgs> RightButtonUp;

        private void OnRightButtonUp(AMouseHookEventArgs e)
        {
            RightButtonUp?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<AMouseHookEventArgs> MiddleButtonDown;

        private void OnMiddleButtonDown(AMouseHookEventArgs e)
        {
            MiddleButtonDown?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<AMouseHookEventArgs> MiddleButtonUp;

        private void OnMiddleButtonUp(AMouseHookEventArgs e)
        {
            MiddleButtonUp?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<AMouseHookEventArgs> Wheel;

        private void OnWheel(AMouseHookEventArgs e)
        {
            Wheel?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<AMouseHookEventArgs> MouseEvent;

        private void OnMouseEvent(AMouseHookEventArgs e)
        {
            MouseEvent?.Invoke(this, e);
        }

        #endregion

        #region IDisposable Members / Finalizer

        /// <summary>
        ///     Call this method to unhook the Mouse Hook, and to release resources allocated to it.
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed)
                return;
            IsDisposed = true;
            if (IsEnabled)
                Disable();
            GC.SuppressFinalize(this);
        }

        ~AMouseHook()
        {
            if (MustBeDisposed)
                Disable();
        }

        #endregion
    }
}