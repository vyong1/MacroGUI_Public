using Shared.PeripheralInput;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Shared.Windows
{
    public delegate bool EnumWindowsCallback(IntPtr hwnd, IntPtr lParam);

    public class Win32
    {
        public const int SW_RESTORE = 9;

        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, UIntPtr dwExtraInfo);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hWnd, EnumWindowsCallback callback, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out MousePoint lpPoint);
    }

    public static class MouseEventFlag // Pseudo-enum
    {
        public const uint LeftDown = 0x02;
        public const uint LeftUp = 0x04;
        public const uint RightDown = 0x08;
        public const uint RightUp = 0x10;
        public const uint MiddleDown = 0x20;
        public const uint MiddleUp = 0x40;
        public const uint Absolute = 0x8000;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
        public int width => right - left;
        public int height => bottom - top;

        /// <summary>
        /// Custom-implemented function - Trims the borders of the image
        /// </summary>
        public void Trim(int marginToTrim)
        {
            left = left + marginToTrim;
            right = right - marginToTrim;
            top = top + marginToTrim;
            bottom = bottom - marginToTrim;
        }
    }
}
