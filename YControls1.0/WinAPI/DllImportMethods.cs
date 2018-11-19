using System;
using System.Runtime.InteropServices;
using System.Windows;
using YControls.AreaIconWindow;

namespace YControls.WinAPI {
    public class DllImportMethods {
        #region Properties

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        /// <summary>
        /// 控件特效
        /// </summary>
        public enum AccentState {
            /// <summary>
            /// 不启用
            /// </summary>
            ACCENT_DISABLED,
            /// <summary>
            /// 不明
            /// </summary>
            ACCENT_ENABLE_GRADIENT,
            ACCENT_ENABLE_TRANSPARENTGRADIENT,
            /// <summary>
            /// 毛玻璃
            /// </summary>
            ACCENT_ENABLE_BLURBEHIND,
            ACCENT_INVALID_STATE,
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowCompositionAttributeData {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        internal enum WindowCompositionAttribute {
            // 省略其他未使用的字段
            WCA_ACCENT_POLICY = 19,
            // 省略其他未使用的字段
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

        /// <summary>
        /// 发送窗口消息
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 毛玻璃
        /// </summary>
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        #endregion

    }

}
