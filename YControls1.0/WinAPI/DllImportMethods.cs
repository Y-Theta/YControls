using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace YControls.WinAPI {
    public static class DllImportMethods {
        #region Properties

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 获得窗口的裁剪矩形
        /// </summary>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        

        /// <summary>
        /// 设置窗口位置
        /// </summary>
        [DllImport("user32", EntryPoint = "SetWindowPos")]
        internal static extern int SetWindowPos(IntPtr hWnd, int hwndInsertAfter, int x, int y, int cx, int cy, int wFlags);  

        #endregion

        #region Constructors
        #endregion
    }

}
