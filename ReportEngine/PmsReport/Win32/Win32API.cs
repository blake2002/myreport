using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows;

namespace PMS.Libraries.ToolControls.Report.Win32
{
    public class Win32API
    {
        [DllImport("User32.dll", EntryPoint = "SetLayeredWindowAttributes",CharSet=CharSet.Auto)]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd,int crKey,  byte bAlpha, int dwFlags);

        [DllImport("User32.dll", EntryPoint = "SetWindowLong")]
        public static extern long SetWindowLong(IntPtr hwnd, int nIndex, long dwNewLong);

        [DllImport("User32.dll", EntryPoint = "GetWindowLong")]
        public static extern long GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("Gdi32.dll", EntryPoint = "BitBlt")]
        public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc,  int nXSrc,   int nYSrc,   int dwRop);

        [DllImport("Msimg32.dll", EntryPoint = "TransparentBlt")]
        public static extern bool TransparentBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int hHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, int crTransparent);

        [DllImport("User32.dll", EntryPoint = "EnumChildWindows")]
        public static extern bool EnumChildWindows(IntPtr hWndParent, PMS.Libraries.ToolControls.Report.Win32.FunctionPointer.EnumChildProc enumFunc, ref IntPtr lParam);

        [DllImport("User32.dll", EntryPoint = "GetClientRect")]
        public static extern bool GetClientRect(IntPtr hWnd, ref Rect rect);

        [DllImport("Gdi32.dll", EntryPoint = "SetWindowExtEx", CharSet = CharSet.Auto)]
        public static extern bool SetWindowExtEx(IntPtr hdc, int nXExtent, int nYExtent, ref Size lpSize);

        [DllImport("Gdi32.dll", EntryPoint = "SetViewportExtEx", CharSet = CharSet.Auto)]
        public static extern bool SetViewportExtEx(IntPtr hdc, int nXExtent, int nYExtent, ref Size lpSize);

        [DllImport("Gdi32.dll", EntryPoint = "SetMapMode", CharSet = CharSet.Auto)]
        public static extern int SetMapMode(IntPtr hdc, int fnMapMode);

        [DllImport("User32.dll", EntryPoint = "GetDC", CharSet = CharSet.Auto)]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll", EntryPoint = "ReleaseDC", CharSet = CharSet.Auto)]
        public static extern IntPtr ReleaseDC(IntPtr hdc);

        [DllImport("Gdi32.dll", EntryPoint = "GetDeviceCaps", CharSet = CharSet.Auto)]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        // The SetWindowOrgEx function specifies which window point maps to the viewport origin (0,0). 
        [DllImport("Gdi32.dll", EntryPoint = "SetWindowOrgEx", CharSet = CharSet.Auto)]
        public static extern bool SetWindowOrgEx(IntPtr hdc, int x, int y, ref System.Drawing.Point lpPoint);

        //The SetViewportOrgEx function specifies which device point maps to the window origin (0,0). 
        [DllImport("Gdi32.dll", EntryPoint = "SetViewportOrgEx", CharSet = CharSet.Auto)]
        public static extern bool SetViewportOrgEx(IntPtr hdc, int x, int y, ref System.Drawing.Point lpPoint);

        [DllImport("User32.dll", EntryPoint = "SetWindowsHookEx",CharSet=CharSet.Auto)]
        public static extern IntPtr SetWindowsHookEx(int idHook, PMS.Libraries.ToolControls.Report.Event.Win32Delagate.HookProc hookProc, IntPtr hModule, Int32 dwThreadId);

        [DllImport("User32.dll", EntryPoint = "SetWindowsHookEx", CharSet = CharSet.Auto)]
        public static extern int CallNextHookEx(int hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", EntryPoint = "UnhookWindowsHookEx", CharSet = CharSet.Auto)]
        public static extern int UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("Kernel32.dll", EntryPoint = "GetCurrentThreadId", CharSet = CharSet.Auto)]
        public static extern int GetCurrentThreadId();

        [DllImport("User32.dll", EntryPoint = "ClientToScreen",CharSet=CharSet.Auto)]
        public static extern bool ClientToScreen(IntPtr hWnd, ref System.Drawing.Point pt);

        [DllImport("User32.dll", EntryPoint = "ScreenToClient", CharSet = CharSet.Auto)]
        public static extern bool ScreenToClient(IntPtr hWnd, ref System.Drawing.Point pt);

        public static int LOWORD(int word)
        {
            return (int)(word & 0xffff);
        }

        public static int HIWORD(int word)
        {
            return (int)(word >> 16);
        }
    }

    public class FunctionPointer
    {
        //枚举窗口回调函数
        public delegate bool EnumChildProc(IntPtr hWnd, ref IntPtr lParam);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEHOOKSTRUCT
    {
        //屏幕坐标XY
        public System.Drawing.Point pt;
        //接受到消息的窗口句柄
        public IntPtr hwnd;
        public UInt32 wHitTestCode;
        //指定与本消息联系的额外消息
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    { 
           public  Int32 left;
           public Int32 top;
           public Int32 right;
           public Int32 bottom; 
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct POINTS
    {
        public short X;
        public short Y;
    }
}
