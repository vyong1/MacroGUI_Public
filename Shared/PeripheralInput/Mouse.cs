using Shared.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.PeripheralInput
{
    public class Mouse
    {
        public static void LeftClickAt(int x, int y)
        {
            SetCursorPos(x, y);
            LeftClick();
        }

        public static void LeftClick(int holdms = 50)
        {
            Win32.mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
            Thread.Sleep(holdms);
            Win32.mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
        }

        public static void RightClickAt(int x, int y)
        {
            SetCursorPos(x, y);
            RightClick();
        }

        public static void RightClick(int holdms = 50)
        {
            Win32.mouse_event(MouseEventFlag.RightDown, 0, 0, 0, UIntPtr.Zero);
            Thread.Sleep(holdms);
            Win32.mouse_event(MouseEventFlag.RightUp, 0, 0, 0, UIntPtr.Zero);
        }

        public static long SetCursorPos(int x, int y)
        {
            return Win32.SetCursorPos(x, y);
        }

        public static MousePoint GetCursorPos()
        {
            Win32.GetCursorPos(out MousePoint p);
            return p;
        }
    }
}
