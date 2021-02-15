using System.Threading;
using SputnikAsm.LProcess.LNative;
using SputnikAsm.LProcess.LNative.LTypes;
using SputnikAsm.LProcess.Utilities;

namespace SputnikAsm.LProcess.LWindows.LMouse
{
    /// <summary>
    ///     Class defining a virtual mouse using the API SendInput.
    /// </summary>
    public class ASendInputMouse : IAMouse
    {
        /// <summary>
        ///     Initializes a new instance of a child of the <see cref="ASendInputMouse" /> class.
        /// </summary>
        /// <param name="window">The reference of the <see cref="IAWindow" /> object.</param>
        public ASendInputMouse(IAWindow window)
        {
            Window = window;
        }

        protected IAWindow Window { get; set; }

        /// <summary>
        ///     Clicks the left button of the mouse at the current cursor position.
        /// </summary>
        public void ClickLeft()
        {
            PressLeft();
            ReleaseLeft();
        }

        /// <summary>
        ///     Clicks the middle button of the mouse at the current cursor position.
        /// </summary>
        public void ClickMiddle()
        {
            PressMiddle();
            ReleaseMiddle();
        }

        /// <summary>
        ///     Clicks the right button of the mouse at the current cursor position.
        /// </summary>
        public void ClickRight()
        {
            PressRight();
            ReleaseRight();
        }

        /// <summary>
        ///     Double clicks the left button of the mouse at the current cursor position.
        /// </summary>
        public void DoubleClickLeft()
        {
            ClickLeft();
            Thread.Sleep(10);
            ClickLeft();
        }

        /// <summary>
        ///     Moves the cursor at the specified coordinate from the position of the window.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        public void MoveTo(int x, int y)
        {
            MoveToAbsolute(Window.X + x, Window.Y + y);
        }

        /// <summary>
        ///     Presses the left button of the mouse at the current cursor position.
        /// </summary>
        public void PressLeft()
        {
            var input = CreateInput();
            input.Mouse.Flags = MouseFlags.LeftDown;
            AWindowHelper.SendInput(input);
        }

        /// <summary>
        ///     Presses the middle button of the mouse at the current cursor position.
        /// </summary>
        public void PressMiddle()
        {
            var input = CreateInput();
            input.Mouse.Flags = MouseFlags.MiddleDown;
            AWindowHelper.SendInput(input);
        }

        /// <summary>
        ///     Presses the right button of the mouse at the current cursor position.
        /// </summary>
        public void PressRight()
        {
            var input = CreateInput();
            input.Mouse.Flags = MouseFlags.RightDown;
            AWindowHelper.SendInput(input);
        }

        /// <summary>
        ///     Releases the left button of the mouse at the current cursor position.
        /// </summary>
        public void ReleaseLeft()
        {
            var input = CreateInput();
            input.Mouse.Flags = MouseFlags.LeftUp;
            AWindowHelper.SendInput(input);
        }

        /// <summary>
        ///     Releases the middle button of the mouse at the current cursor position.
        /// </summary>
        public void ReleaseMiddle()
        {
            var input = CreateInput();
            input.Mouse.Flags = MouseFlags.MiddleUp;
            AWindowHelper.SendInput(input);
        }

        /// <summary>
        ///     Releases the right button of the mouse at the current cursor position.
        /// </summary>
        public void ReleaseRight()
        {
            var input = CreateInput();
            input.Mouse.Flags = MouseFlags.RightUp;
            AWindowHelper.SendInput(input);
        }

        /// <summary>
        ///     Scrolls horizontally using the wheel of the mouse at the current cursor position.
        /// </summary>
        /// <param name="delta">The amount of wheel movement.</param>
        public void ScrollHorizontally(int delta = 120)
        {
            var input = CreateInput();
            input.Mouse.Flags = MouseFlags.HWheel;
            input.Mouse.MouseData = delta;
            AWindowHelper.SendInput(input);
        }

        /// <summary>
        ///     Scrolls vertically using the wheel of the mouse at the current cursor position.
        /// </summary>
        /// <param name="delta">The amount of wheel movement.</param>
        public void ScrollVertically(int delta = 120)
        {
            var input = CreateInput();
            input.Mouse.Flags = MouseFlags.Wheel;
            input.Mouse.MouseData = delta;
            AWindowHelper.SendInput(input);
        }

        /// <summary>
        ///     Moves the cursor at the specified coordinate.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        protected void MoveToAbsolute(int x, int y)
        {
            var input = CreateInput();
            input.Mouse.DeltaX = CalculateAbsoluteCoordinateX(x);
            input.Mouse.DeltaY = CalculateAbsoluteCoordinateY(y);
            input.Mouse.Flags = MouseFlags.Move | MouseFlags.Absolute;
            input.Mouse.MouseData = 0;
            AWindowHelper.SendInput(input);
        }

        /// <summary>
        ///     Calculates the x-coordinate with the system metric.
        /// </summary>
        private static int CalculateAbsoluteCoordinateX(int x)
        {
            return x*65536/User32.GetSystemMetrics(SystemMetrics.CxScreen);
        }

        /// <summary>
        ///     Calculates the y-coordinate with the system metric.
        /// </summary>
        private static int CalculateAbsoluteCoordinateY(int y)
        {
            return y*65536/User32.GetSystemMetrics(SystemMetrics.CyScreen);
        }

        /// <summary>
        ///     Create an <see cref="Input" /> structure for mouse event.
        /// </summary>
        private static Input CreateInput()
        {
            return new Input(InputTypes.Mouse);
        }
    }
}