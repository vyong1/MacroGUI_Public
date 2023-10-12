using Shared.Math;
using Shared.Macros;
using Shared.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.FFXIV
{
    public class XivGame
    {
        public static IntPtr WindowHandle => GetProcess().MainWindowHandle;
        public static bool IsRunning => Process.GetProcessesByName(GAME_PROC_NAME).Any();
        private const string GAME_PROC_NAME = "ffxiv_dx11";

        public static void Restart()
        {
            if (IsRunning)
            {
                GetProcess().Kill();
                Thread.Sleep(3000);
            }
            
            Process.Start(@"C:\Users\vyong\AppData\Local\XIVLauncher\XIVLauncher.exe");
        }

        public static void RestartAndLogin()
        {
            Restart();
            Thread.Sleep(RNG.Range(20 * 1000, 25 * 1000));
            LogIn();
        }

        public static void LogIn()
        {
            FocusGameWindow();
            new LogInMacro().Execute();
        }
        
        public static void FocusGameWindow()
        {
            if (!IsRunning) { return; }

            var windowHandle = GetProcess().MainWindowHandle;
            Win32.SetForegroundWindow(windowHandle);
            Win32.ShowWindow(windowHandle, Win32.SW_RESTORE);
        }

        private static Process GetProcess()
        {
            var procs = Process.GetProcessesByName(GAME_PROC_NAME);
            if (!procs.Any())
            {
                throw new Exception($"No processes found with the name {GAME_PROC_NAME}");
            }

            return procs.First();
        }
    }
}
