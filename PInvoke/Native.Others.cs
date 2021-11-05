using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PInvoke {

    public partial class Native {

        [DllImport(Native.DWMAPI, PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        [DllImport(Native.DWMAPI)]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);

        [DllImport(Native.DWMAPI)]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out bool pvAttribute, int cbAttribute);

        [DllImport(Native.DWMAPI)]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out int pvAttribute, int cbAttribute);

        [DllImport(Native.KERNEL32, EntryPoint = "SetLastError")]
        static extern void SetLastError(int dwErrorCode);

        /// <summary>
        /// 系统版本 >= 8.1 {AB4C8EB9-3A17-4EB5-82AD-A7375D56FFCB}
        /// </summary>
        [DllImport("SHCORE")]
        internal static extern int GetDpiForMonitor(IntPtr hMonitor, MonitorDpiType dpiType, out uint dpiX, out uint dpiY);


        #region      WindowAbout
        public static bool IsDWMEnabled() {
            return OsExtend.IsWindowsVistaOrGreater() && DwmIsCompositionEnabled();
        }

        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong) {
            int error = 0;
            IntPtr result = IntPtr.Zero;
            // Win32 SetWindowLong doesn't clear error on success
            SetLastError(0);

            if (IntPtr.Size == 4) {
                // use SetWindowLong
                Int32 tempResult = SetWindowLong(hWnd, nIndex, unchecked((int)dwNewLong.ToInt64()));
                error = Marshal.GetLastWin32Error();
                result = new IntPtr(tempResult);
            } else {
                // use SetWindowLongPtr
                result = SetWindowLongPtr(hWnd, nIndex, dwNewLong);
                error = Marshal.GetLastWin32Error();
            }

            if ((result == IntPtr.Zero) && (error != 0)) {
                throw new System.ComponentModel.Win32Exception(error);
            }

            return result;
        }

        //TaskBar 
        public static bool SetTaskbarVisibilityIfIntersect(bool visible, Rectangle rect) {
            bool result = false;

            IntPtr taskbarHandle = FindWindow("Shell_TrayWnd", null);

            if (taskbarHandle != IntPtr.Zero) {
                Rectangle taskbarRect = GetWindowRect(taskbarHandle);

                if (rect.IntersectsWith(taskbarRect)) {
                    ShowWindow(taskbarHandle, visible ? (int)WindowShowStyle.Show : (int)WindowShowStyle.Hide);
                    result = true;
                }

                if (OsExtend.IsWindowsVista() || OsExtend.IsWindows7()) {
                    IntPtr startHandle = FindWindowEx(IntPtr.Zero, IntPtr.Zero, (IntPtr)0xC017, null);

                    if (startHandle != IntPtr.Zero) {
                        Rectangle startRect = GetWindowRect(startHandle);

                        if (rect.IntersectsWith(startRect)) {
                            ShowWindow(startHandle, visible ? (int)WindowShowStyle.Show : (int)WindowShowStyle.Hide);
                            result = true;
                        }
                    }
                }
            }

            return result;
        }

        //TaskBar 
        public static bool SetTaskbarVisibility(bool visible) {
            IntPtr taskbarHandle = FindWindow("Shell_TrayWnd", null);

            if (taskbarHandle != IntPtr.Zero) {
                ShowWindow(taskbarHandle, visible ? (int)WindowShowStyle.Show : (int)WindowShowStyle.Hide);

                if (OsExtend.IsWindowsVista() || OsExtend.IsWindows7()) {
                    IntPtr startHandle = FindWindowEx(IntPtr.Zero, IntPtr.Zero, (IntPtr)0xC017, null);

                    if (startHandle != IntPtr.Zero) {
                        ShowWindow(startHandle, visible ? (int)WindowShowStyle.Show : (int)WindowShowStyle.Hide);
                    }
                }

                return true;
            }

            return false;
        }

        public static Point GetCursorPosition() {
            if (GetCursorPos(out POINT point)) {
                return (Point)point;
            }

            return Point.Empty;
        }

        public static bool IsWindowCloaked(IntPtr handle) {
            if (IsDWMEnabled()) {
                int result = DwmGetWindowAttribute(handle, (int)DwmWindowAttribute.DWMWA_CLOAKED, out int cloaked, sizeof(int));
                return result == 0 && cloaked != 0;
            }

            return false;
        }

        public static string GetWindowText(IntPtr handle) {
            if (handle.ToInt32() > 0) {
                try {
                    int length = GetWindowTextLength(handle);

                    if (length > 0) {
                        StringBuilder sb = new StringBuilder(length + 1);

                        if (GetWindowText(handle, sb, sb.Capacity) > 0) {
                            return sb.ToString();
                        }
                    }
                } catch (Exception e) {
                    Debug.WriteLine(e);
                }
            }

            return null;
        }

        public static string GetClassName(IntPtr handle) {
            if (handle.ToInt32() > 0) {
                StringBuilder sb = new StringBuilder(256);

                if (GetClassName(handle, sb, sb.Capacity) > 0) {
                    return sb.ToString();
                }
            }

            return null;
        }

        public static bool GetBorderSize(IntPtr handle, out Size size) {
            WINDOWINFO wi = WINDOWINFO.Create();
            bool result = GetWindowInfo(handle, ref wi);

            if (result) {
                size = new Size((int)wi.cxWindowBorders, (int)wi.cyWindowBorders);
            } else {
                size = Size.Empty;
            }

            return result;
        }

        public static Rectangle MaximizedWindowFix(IntPtr handle, Rectangle windowRect) {
            if (GetBorderSize(handle, out Size size)) {
                windowRect = new Rectangle(windowRect.X + size.Width, windowRect.Y + size.Height, windowRect.Width - (size.Width * 2), windowRect.Height - (size.Height * 2));
            }

            return windowRect;
        }

        public static bool GetExtendedFrameBounds(IntPtr handle, out Rectangle rectangle) {
            int result = DwmGetWindowAttribute(handle, (int)DwmWindowAttribute.DWMWA_EXTENDED_FRAME_BOUNDS, out RECT rect, Marshal.SizeOf(typeof(RECT)));
            rectangle = rect;
            return result == 0;
        }

        public static Rectangle GetWindowRect(IntPtr handle) {
            GetWindowRect(handle, out RECT rect);
            return rect;
        }

        public static Rectangle GetWindowRectangle(IntPtr handle) {
            Rectangle rect = Rectangle.Empty;

            if (Native.IsDWMEnabled() && Native.GetExtendedFrameBounds(handle, out Rectangle tempRect)) {
                rect = tempRect;
            }

            if (rect.IsEmpty) {
                rect = Native.GetWindowRect(handle);
            }

            if (!OsExtend.IsWindows10OrGreater() && Native.IsZoomed(handle)) {
                rect = Native.MaximizedWindowFix(handle, rect);
            }

            return rect;
        }

        public static Rectangle GetClientRect(IntPtr handle) {
            GetClientRect(handle, out RECT rect);
            Point position = rect.Location;
            ClientToScreen(handle, ref position);
            return new Rectangle(position, rect.Size);
        }

        public static Point ScreenToClient(Point p) {
            int screenX = GetSystemMetrics(SystemMetric.SM_XVIRTUALSCREEN);
            int screenY = GetSystemMetrics(SystemMetric.SM_YVIRTUALSCREEN);
            return new Point(p.X - screenX, p.Y - screenY);
        }

        public static IntPtr GetClassLongPtrSafe(IntPtr hWnd, int nIndex) {
            if (IntPtr.Size > 4) {
                return GetClassLongPtr(hWnd, nIndex);
            }

            return new IntPtr(GetClassLong(hWnd, nIndex));
        }

        private static Icon GetSmallApplicationIcon(IntPtr handle) {
            SendMessageTimeout(handle, (int)WindowsMessages.GETICON, Native.ICON_SMALL2, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 1000, out IntPtr iconHandle);

            if (iconHandle == IntPtr.Zero) {
                SendMessageTimeout(handle, (int)WindowsMessages.GETICON, Native.ICON_SMALL, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 1000, out iconHandle);

                if (iconHandle == IntPtr.Zero) {
                    iconHandle = GetClassLongPtrSafe(handle, Native.GCL_HICONSM);

                    if (iconHandle == IntPtr.Zero) {
                        SendMessageTimeout(handle, (int)WindowsMessages.QUERYDRAGICON, 0, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 1000, out iconHandle);
                    }
                }
            }

            if (iconHandle != IntPtr.Zero) {
                return Icon.FromHandle(iconHandle);
            }

            return null;
        }

        private static Icon GetBigApplicationIcon(IntPtr handle) {
            SendMessageTimeout(handle, (int)WindowsMessages.GETICON, Native.ICON_BIG, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 1000, out IntPtr iconHandle);

            if (iconHandle == IntPtr.Zero) {
                iconHandle = GetClassLongPtrSafe(handle, Native.GCL_HICON);
            }

            if (iconHandle != IntPtr.Zero) {
                return Icon.FromHandle(iconHandle);
            }

            return null;
        }

        public static Icon GetApplicationIcon(IntPtr handle) {
            return GetSmallApplicationIcon(handle) ?? GetBigApplicationIcon(handle);
        }

        #endregion

    }
}
