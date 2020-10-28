namespace YControlCore.Interop {
    ///-----------------------------------------------------------------------------
    /// @ Y_Theta
    /// 
    /// Codes for Platform-Invoke ,for more infomation https://www.pinvoke.net/
    /// 
    /// Managed Type Reference :
    /// 
    /// C++                                  C#
    /// ----------------------------------------------------------------------------    
    /// void                                 <see cref="System.Void"/>
    /// bool                                 <see cref="System.Boolean"/>
    /// signed char                          <see cref="System.SByte"/>
    /// unsigned char                        <see cref="System.Byte"/>
    /// wchar_t                              <see cref="System.Char"/>
    /// short / signed short                 <see cref="System.Int16"/>
    /// unsigned short                       <see cref="System.UInt16"/>
    /// int / signed int                  
    /// long / signed long                   <see cref="System.Int32"/>
    /// unsigned int                      
    /// unsigned long                        <see cref="System.UInt32"/>
    /// __int64                           
    /// signed __int64                       <see cref="System.Int64"/>
    /// unsigned __int64                     <see cref="System.UInt64"/>
    /// float                                <see cref="System.Single"/>
    /// double / long double                 <see cref="System.Double"/>
    /// 
    /// 
    /// 
    /// 
    ///-----------------------------------------------------------------------------

    #region Using
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Interop;
    using System.Threading.Tasks;
    using System.Threading;
#if NETCOREAPP3_1
    using System.Drawing.Imaging;
    using System.Reflection;
#endif
    #endregion


    public class Core {

        #region Method

        #region User32

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out _RECT lpRect);

        [DllImport("user32.dll")]
        public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);
        /// <summary>
        ///     Retrieves a handle to the top-level window whose class name and window name match the specified strings. This
        ///     function does not search child windows. This function does not perform a case-sensitive search. To search child
        ///     windows, beginning with a specified child window, use the
        ///     <see cref="!:https://msdn.microsoft.com/en-us/library/windows/desktop/ms633500%28v=vs.85%29.aspx">FindWindowEx</see>
        ///     function.
        ///     <para>
        ///     Go to https://msdn.microsoft.com/en-us/library/windows/desktop/ms633499%28v=vs.85%29.aspx for FindWindow
        ///     information or https://msdn.microsoft.com/en-us/library/windows/desktop/ms633500%28v=vs.85%29.aspx for
        ///     FindWindowEx
        ///     </para>
        /// </summary>
        /// <param name="lpClassName">
        ///     C++ ( lpClassName [in, optional]. Type: LPCTSTR )<br />The class name or a class atom created by a previous call to
        ///     the RegisterClass or RegisterClassEx function. The atom must be in the low-order word of lpClassName; the
        ///     high-order word must be zero.
        ///     <para>
        ///     If lpClassName points to a string, it specifies the window class name. The class name can be any name
        ///     registered with RegisterClass or RegisterClassEx, or any of the predefined control-class names.
        ///     </para>
        ///     <para>If lpClassName is NULL, it finds any window whose title matches the lpWindowName parameter.</para>
        /// </param>
        /// <param name="lpWindowName">
        ///     C++ ( lpWindowName [in, optional]. Type: LPCTSTR )<br />The window name (the window's
        ///     title). If this parameter is NULL, all window names match.
        /// </param>
        /// <returns>
        ///     C++ ( Type: HWND )<br />If the function succeeds, the return value is a handle to the window that has the
        ///     specified class name and window name. If the function fails, the return value is NULL.
        ///     <para>To get extended error information, call GetLastError.</para>
        /// </returns>
        /// <remarks>
        ///     If the lpWindowName parameter is not NULL, FindWindow calls the <see cref="M:GetWindowText" /> function to
        ///     retrieve the window name for comparison. For a description of a potential problem that can arise, see the Remarks
        ///     for <see cref="M:GetWindowText" />.
        /// </remarks>
        // For Windows Mobile, replace user32.dll with coredll.dll
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// Retrieves a handle to a window that has the specified relationship (Z-Order or owner) to the specified window.
        /// </summary>
        /// <remarks>The EnumChildWindows function is more reliable than calling GetWindow in a loop. An application that
        /// calls GetWindow to perform this task risks being caught in an infinite loop or referencing a handle to a window
        /// that has been destroyed.</remarks>
        /// <param name="hWnd">A handle to a window. The window handle retrieved is relative to this window, based on the
        /// value of the uCmd parameter.</param>
        /// <param name="uCmd">The relationship between the specified window and the window whose handle is to be
        /// retrieved.</param>
        /// <returns>
        /// If the function succeeds, the return value is a window handle. If no window exists with the specified relationship
        /// to the specified window, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, GetWindowType uCmd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetParent(IntPtr hWnd);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="pwi"></param>
        /// <returns></returns>
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        /// <summary>
        ///     The MoveWindow function changes the position and dimensions of the specified window. For a top-level window, the
        ///     position and dimensions are relative to the upper-left corner of the screen. For a child window, they are relative
        ///     to the upper-left corner of the parent window's client area.
        ///     <para>
        ///     Go to https://msdn.microsoft.com/en-us/library/windows/desktop/ms633534%28v=vs.85%29.aspx for more
        ///     information
        ///     </para>
        /// </summary>
        /// <param name="hWnd">C++ ( hWnd [in]. Type: HWND )<br /> Handle to the window.</param>
        /// <param name="X">C++ ( X [in]. Type: int )<br />Specifies the new position of the left side of the window.</param>
        /// <param name="Y">C++ ( Y [in]. Type: int )<br /> Specifies the new position of the top of the window.</param>
        /// <param name="nWidth">C++ ( nWidth [in]. Type: int )<br />Specifies the new width of the window.</param>
        /// <param name="nHeight">C++ ( nHeight [in]. Type: int )<br />Specifies the new height of the window.</param>
        /// <param name="bRepaint">
        ///     C++ ( bRepaint [in]. Type: bool )<br />Specifies whether the window is to be repainted. If this
        ///     parameter is TRUE, the window receives a message. If the parameter is FALSE, no repainting of any kind occurs. This
        ///     applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the
        ///     parent window uncovered as a result of moving a child window.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.<br /> If the function fails, the return value is zero.
        ///     <br />To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, _SWP uFlags);

        [DllImport("user32.dll")]
        public static extern bool UpdateWindow(IntPtr hWnd);

        /// <summary>
        /// 挂钩函数
        /// <para>需要保存回调委托（以成员变量），若不保持则会被垃圾回收</para>
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(_WH idHook, HookProc lpfn, IntPtr hInstance, uint threadId);

        /// <summary>
        /// 脱钩函数
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        /// <summary>
        /// 串接钩子
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);

        /// <summary>
        /// 获取当前坐标点对应的窗体句柄
        /// </summary>
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT Point);

        /// <summary>
        /// 获取鼠标坐标点
        /// </summary>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT lpPoint);

        /// <summary>
        /// 释放鼠标捕获
        /// </summary>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReleaseCapture();

        /// <summary>
        /// 发送Windows消息
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 
        /// </summary>
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 
        /// </summary>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint RegisterWindowMessage(string lpString);

        /// <summary>
        /// 
        /// </summary>
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool ChangeWindowMessageFilter(uint message, int dwFlag);

        /// <summary>
        /// 获取按键名称
        /// </summary>
        [DllImport("user32", EntryPoint = "GetKeyNameText")]
        private static extern int GetKeyNameText(int IParam, StringBuilder lpBuffer, int nSize);

        /// <summary>
        /// 获取按键状态
        /// </summary>
        [DllImport("user32", EntryPoint = "GetKeyboardState")]
        private static extern int GetKeyboardState(byte[] pbKeyState);

        /// <summary>
        /// 获得窗体线程ID
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpdwProcessId"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // This static method is required because legacy OSes do not support
        // SetWindowLongPtr
        public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong) {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
               #endregion User32

        #region Shell32

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern int Shell_NotifyIconGetRect([In] ref NOTIFYICONIDENTIFIER identifier, [Out] out _RECT iconLocation);

        #endregion Shell32

        #region GDI32

        /// <summary>
        /// 释放指针对象
        /// </summary>
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        #endregion GDI32

        #region kernel32

        /// <summary>
        /// 使用WINDOWS API函数代替获取当前实例的函数,防止钩子失效
        /// </summary>
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);

        /// <summary>
        /// 加载非托管动态链接库
        /// </summary>
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

        /// <summary>
        /// 释放非托管动态链接库
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr hModule);

        /// <summary>
        /// 获得非托管导出方法
        /// </summary>
        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        #endregion kernel32

        /// <summary>
        /// 获取控件的句柄
        /// </summary>
        /// <param name="visual"></param>
        /// <returns></returns>
        public static IntPtr GetVisualHandle(Visual visual) {
            IntPtr hwnd = IntPtr.Zero;
            var source = PresentationSource.FromVisual(visual);
            if (source != null)
                hwnd = ((HwndSource)source).Handle;
            return hwnd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visual"></param>
        /// <returns></returns>
        public static IntPtr GetVisualHandle(DependencyObject visual) {
            IntPtr hwnd = IntPtr.Zero;
            var source = PresentationSource.FromDependencyObject(visual);
            if (source != null)
                hwnd = ((HwndSource)source).Handle;
            return hwnd;
        }

        /// <summary>
        /// 将位图转换为ImageSource
        /// </summary>
        public static ImageSource ToImageSource(Bitmap bitmap) {

            //#if NET45
            IntPtr hBitmap = bitmap.GetHbitmap();

            ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            DeleteObject(hBitmap);
            return wpfBitmap;

            //#elif NETCOREAPP3_1

            //if (bitmap == null)
            //    throw new ArgumentNullException("bitmap");

            //var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            //var bitmapData = bitmap.LockBits(
            //    rect,
            //    ImageLockMode.ReadWrite,
            //    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //try {
            //    var size = (rect.Width * rect.Height) * 4;

            //    return BitmapSource.Create(
            //        bitmap.Width,
            //        bitmap.Height,
            //        bitmap.HorizontalResolution,
            //        bitmap.VerticalResolution,
            //        PixelFormats.Bgra32,
            //        null,
            //        bitmapData.Scan0,
            //        size,
            //        bitmapData.Stride);
            //} finally {
            //    bitmap.UnlockBits(bitmapData);
            //}

            //#endif
        }

        #endregion

    }

    public static class ExtendStruct {
        public static string ToString(this _RECT obj,bool t) {
            return $" < {obj.left} {obj.top} {obj.right} {obj.bottom} > ";
        }
    }

    public class Extend {

        /// <summary>
        /// 为当前进程创建全局鼠标钩子
        /// </summary>
        /// <param name="id">钩子id 用于脱钩</param>
        /// <param name="callback">若调用成功</param>
        public static void HookGlobalMouseEvent(ref int id, HookProc callback) {
            if (id == 0) {
                id = Core.SetWindowsHookEx(
                    _WH.WH_MOUSE_LL,
                    callback,
                    //Core.GetModuleHandle(Assembly.GetExecutingAssembly().GetName().Name),
                    Marshal.GetHINSTANCE(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0]),
                    0);
                if (id == 0) {
                    throw new Exception("SetWindowsHookEx failed.");
                }
            }
        }

        public static void UnHookGlobalMouseEvent(ref int id) {
            if (id != 0) {
                if (Core.UnhookWindowsHookEx(id)) {
                    id = 0;
                } else {
                    throw new Exception("UnhookWindowsHookEx failed.");
                }
            }

        }


    }

    #region Deleget
    /// <summary>
    /// 钩子回调
    /// </summary>
    public delegate int HookProc(
        int nCode, 
        Int32 wParam, 
        IntPtr lParam
        );

    public delegate void MouseProc(int nCode, _WM wParam, IntPtr lParam);
    #endregion

    #region Struct

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

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NCCALCSIZE_PARAMS {
        public _RECT rcNewWindow;
        public _RECT rcOldWindow;
        public _RECT rcClient;
        IntPtr lppos;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct _RECT {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct _LRECT {
        [MarshalAs(UnmanagedType.U8)]
        public long left;
        [MarshalAs(UnmanagedType.U8)]
        public long top;
        [MarshalAs(UnmanagedType.U8)]
        public long right;
        [MarshalAs(UnmanagedType.U8)]
        public long bottom;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWINFO {
        public uint cbSize;
        public _RECT rcWindow;
        public _RECT rcClient;
        public uint dwStyle;
        public uint dwExStyle;
        public uint dwWindowStatus;
        public uint cxWindowBorders;
        public uint cyWindowBorders;
        public ushort atomWindowType;
        public ushort wCreatorVersion;

        public WINDOWINFO(Boolean? filler)
            : this()   // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
        {
            cbSize = (UInt32)(Marshal.SizeOf(typeof(WINDOWINFO)));
        }

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NOTIFYICONIDENTIFIER {
        public Int32 cbSize;
        public IntPtr hWnd;
        public Int32 uID;
        public Guid guidItem;
    }

    /// <summary>
    /// 鼠标信息结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MSLLHOOKSTRUCT {
        public POINT pt;
        public int mouseData; // be careful, this must be ints, not uints (was wrong before I changed it...). regards, cmew.
        public int flags;
        public int time;
        public UIntPtr dwExtraInfo;
    }

    #endregion
}

