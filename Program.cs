using DiscordTouchpadBugWorkaround.Properties;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DiscordTouchpadBugWorkaround
{
    internal class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        // https://stackoverflow.com/a/6569555
        static string GetActiveProcessFileName()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            try
            {
                return p.MainModule.FileName;
            }
            catch
            {
                return null;
            }

        }

        [StructLayout(LayoutKind.Sequential)]
        struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        static void Main()
        {
            Console.Write("hello world");
            string filename;
            string newFilename;
            while (true)
            {
                filename = GetActiveProcessFileName();
                Thread.Sleep(1000);
                if (filename != null && filename.Contains("Discord.exe"))
                {
                    IntPtr hwnd = GetForegroundWindow();
                    RECT newRect;
                    GetWindowRect(hwnd, out newRect);
                    MoveWindow(hwnd, newRect.Left, newRect.Top, newRect.Right - newRect.Left + 1, newRect.Bottom - newRect.Top, false);
                    // it doesnt work if you dont wait between resizes
                    Thread.Sleep(1000);
                    GetWindowRect(hwnd, out newRect);
                    MoveWindow(hwnd, newRect.Left, newRect.Top, newRect.Right - newRect.Left - 1, newRect.Bottom - newRect.Top, false);
                    Console.WriteLine("resized discord");
                    // wait until its unfocused?
                    while (true)
                    {
                        newFilename = GetActiveProcessFileName();
                        if (newFilename != null && !newFilename.Contains("Discord.exe"))
                        {
                            Console.WriteLine("left loop");
                            break;
                        }
                        // only check this every second
                        Thread.Sleep(1000);
                    }

                }
            }
        }
    }

}

