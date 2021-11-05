using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static PInvoke.Native;

namespace PInvoke {


    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

    public delegate bool EnumMonitorsDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

    public partial class Native {

        #region Methods

        [DllImport(Native.USER32)]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport(Native.USER32)]
        public static extern IntPtr CopyIcon(IntPtr hIcon);

        [DllImport(Native.USER32)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, UIntPtr wParam, IntPtr lParam);

        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumThreadWindows(uint dwThreadId, EnumWindowsProc lpfn, IntPtr lParam);

        [DllImport(Native.USER32, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport(Native.USER32)]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport(Native.USER32)]
        public static extern bool DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);

        [DllImport(Native.USER32)]
        public static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);

        [DllImport(Native.USER32)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport(Native.USER32)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport(Native.USER32)]
        public static extern IntPtr FindWindowEx(IntPtr parentHwnd, IntPtr childAfterHwnd, IntPtr className, string windowText);

        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [DllImport(Native.USER32)]
        public static extern IntPtr GetActiveWindow();

        [DllImport(Native.USER32)]
        public static extern uint GetClassLong(IntPtr hWnd, int nIndex);

        [DllImport(Native.USER32)]
        public static extern IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex);

        [DllImport(Native.USER32)]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport(Native.USER32)]
        public static extern bool GetCursorInfo(out CursorInfo pci);

        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport(Native.USER32)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport(Native.USER32)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport(Native.USER32)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport(Native.USER32)]
        public static extern bool GetIconInfo(IntPtr hIcon, out IconInfo piconinfo);

        [DllImport(Native.USER32)]
        public static extern IntPtr CreateIconIndirect([In] ref IconInfo piconinfo);

        [DllImport(Native.USER32, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        /// <summary>
        /// The GetNextWindow function retrieves a handle to the next or previous window in the Z-Order.
        /// The next window is below the specified window; the previous window is above.
        /// If the specified window is a topmost window, the function retrieves a handle to the next (or previous) topmost window.
        /// If the specified window is a top-level window, the function retrieves a handle to the next (or previous) top-level window.
        /// If the specified window is a child window, the function searches for a handle to the next (or previous) child window.
        /// </summary>
        [DllImport(Native.USER32)]
        public static extern IntPtr GetWindow(IntPtr hWnd, GetWindowConstants wCmd);

        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetScrollInfo(IntPtr hwnd, int fnBar, ref SCROLLINFO lpsi);

        [DllImport(Native.USER32)]
        public static extern int GetSystemMetrics(int smIndex);

        [DllImport(Native.USER32)]
        public static extern int GetSystemMetrics(SystemMetric smIndex);

        [DllImport(Native.USER32)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport(Native.USER32)]
        public static extern ulong GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport(Native.USER32, SetLastError = true)]
        private static extern Int32 SetWindowLong(IntPtr hWnd, int nIndex, Int32 dwNewLong);

        [DllImport(Native.USER32, SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport(Native.USER32)]
        public static extern int GetWindowRgn(IntPtr hWnd, IntPtr hRgn);

        [DllImport(Native.USER32, CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);

        [DllImport(Native.USER32, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport(Native.USER32)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        /// <summary>Determines the visibility state of the specified window.</summary>
        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        /// <summary>Determines whether the specified window is minimized (iconic).</summary>
        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        /// <summary>Determines whether a window is maximized.</summary>
        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsZoomed(IntPtr hWnd);

        [DllImport(Native.USER32)]
        public static extern IntPtr LoadCursor(IntPtr hInstance, int iconId);

        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        [DllImport(Native.USER32)]
        public static extern bool ReleaseCapture();

        [DllImport(Native.USER32)]
        public static extern bool ReleaseCapture(IntPtr hwnd);

        [DllImport(Native.USER32)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport(Native.USER32)]
        public static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [DllImport(Native.USER32)]
        public static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);

        [DllImport(Native.USER32, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport(Native.USER32, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport(Native.USER32)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [DllImport(Native.USER32, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam, SendMessageTimeoutFlags fuFlags, uint uTimeout, out UIntPtr lpdwResult);

        [DllImport(Native.USER32, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, int Msg, int wParam, int lParam, SendMessageTimeoutFlags fuFlags, uint uTimeout, out IntPtr lpdwResult);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(Native.USER32, CharSet = CharSet.Auto)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport(Native.USER32)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport(Native.USER32)]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport(Native.USER32)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);

        [DllImport(Native.USER32)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport(Native.USER32, ExactSpelling = true)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pptSrc, uint crKey, [In] ref BLENDFUNCTION pblend, uint dwFlags);

        /// <summary> The RegisterHotKey function defines a system-wide hot key </summary>
        /// <param name="hWnd">Handle to the window that will receive WM_HOTKEY messages generated by the hot key.</param>
        /// <param name="id">Specifies the identifier of the hot key.</param>
        /// <param name="fsModifiers">Specifies keys that must be pressed in combination with the key
        /// specified by the 'vk' parameter in order to generate the WM_HOTKEY message.</param>
        /// <param name="vk">Specifies the virtual-key code of the hot key</param>
        /// <returns><c>true</c> if the function succeeds, otherwise <c>false</c></returns>
        /// <seealso cref="http://msdn.microsoft.com/en-us/library/ms646309(VS.85).aspx"/>
        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        [DllImport(Native.USER32)]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport(Native.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport(Native.USER32)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport(Native.USER32)]
        internal static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, EnumMonitorsDelegate lpfnEnum, IntPtr dwData);

        [DllImport(Native.USER32, CharSet = CharSet.Auto)]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFOEX lpmi);

        #endregion
    }
}
