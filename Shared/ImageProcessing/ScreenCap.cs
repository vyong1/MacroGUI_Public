using Shared.FFXIV;
using Shared.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Shared.ImageProcessing
{
    // TODO [Low Priority] - Handle non-windows OS more gracefully

    public static class ScreenCap
    {
        // Most of this code was taken straight from Stackoverflow, don't ask me how it works

        public static Bitmap Capture(Rectangle rect)
        {
            if (!OperatingSystem.IsWindows())
            {
                return null;
            }

            Bitmap bmpScreenCapture = null;

            if (rect == Rectangle.Empty)//capture the whole screen
            {
                rect = Screen.PrimaryScreen.Bounds;
            }

            bmpScreenCapture = new Bitmap(rect.Width, rect.Height);

            Graphics p = Graphics.FromImage(bmpScreenCapture);


            p.CopyFromScreen(rect.X,
                     rect.Y,
                     0, 0,
                     rect.Size,
                     CopyPixelOperation.SourceCopy);


            p.Dispose();

            return bmpScreenCapture;
        }

        public static Bitmap CaptureFromApp(string appName)
        {
            var procs = Process.GetProcessesByName(appName);
            if (!procs.Any())
            {
                throw new Exception("No processes found with the name" +
                    $" {appName}");
            }

            if (procs.Count() > 1)
            {
                throw new Exception("Multiple processes found " +
                    $"with the name {appName}");
            }

            return CaptureFromApp(procs.First().MainWindowHandle);
        }

        public static Bitmap CaptureFromApp(IntPtr windowHandle, bool focusWindowBeforeCapture = false)
        {
            if (!OperatingSystem.IsWindows())
            {
                return null;
            }

            if (focusWindowBeforeCapture)
            {
                XivGame.FocusGameWindow();
                Win32.SetForegroundWindow(windowHandle);
                Win32.ShowWindow(windowHandle, Win32.SW_RESTORE);
                Thread.Sleep(200);
            }

            var rect = new Rect();
            Win32.GetWindowRect(windowHandle, ref rect);
            rect.Trim(30);

            var bmp = new Bitmap(rect.width, rect.height, PixelFormat.Format64bppArgb);
            Graphics.FromImage(bmp)
                .CopyFromScreen(rect.left, rect.top,0, 0, new Size(rect.width, rect.height), CopyPixelOperation.SourceCopy);
            return bmp;
        }

        public static void ShowImage(Image img)
        {
            if (!OperatingSystem.IsWindows())
            {
                return;
            }

            Form form = new Form();
            form.Text = "Image Viewer";
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = img;
            pictureBox.Dock = DockStyle.Fill;
            form.Controls.Add(pictureBox);
            form.Width = img.Width + 100;
            form.Height = img.Height + 100;
            Application.Run(form);
        }
    }
}
