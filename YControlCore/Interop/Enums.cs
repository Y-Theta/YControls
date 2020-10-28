using System;
using System.Collections.Generic;
using System.Text;

namespace YControlCore.Interop {

    #region  Enum

    public enum GetWindowType : uint {
        /// <summary>
        /// The retrieved handle identifies the window of the same type that is highest in the Z order.
        /// <para/>
        /// If the specified window is a topmost window, the handle identifies a topmost window.
        /// If the specified window is a top-level window, the handle identifies a top-level window.
        /// If the specified window is a child window, the handle identifies a sibling window.
        /// </summary>
        GW_HWNDFIRST = 0,
        /// <summary>
        /// The retrieved handle identifies the window of the same type that is lowest in the Z order.
        /// <para />
        /// If the specified window is a topmost window, the handle identifies a topmost window.
        /// If the specified window is a top-level window, the handle identifies a top-level window.
        /// If the specified window is a child window, the handle identifies a sibling window.
        /// </summary>
        GW_HWNDLAST = 1,
        /// <summary>
        /// The retrieved handle identifies the window below the specified window in the Z order.
        /// <para />
        /// If the specified window is a topmost window, the handle identifies a topmost window.
        /// If the specified window is a top-level window, the handle identifies a top-level window.
        /// If the specified window is a child window, the handle identifies a sibling window.
        /// </summary>
        GW_HWNDNEXT = 2,
        /// <summary>
        /// The retrieved handle identifies the window above the specified window in the Z order.
        /// <para />
        /// If the specified window is a topmost window, the handle identifies a topmost window.
        /// If the specified window is a top-level window, the handle identifies a top-level window.
        /// If the specified window is a child window, the handle identifies a sibling window.
        /// </summary>
        GW_HWNDPREV = 3,
        /// <summary>
        /// The retrieved handle identifies the specified window's owner window, if any.
        /// </summary>
        GW_OWNER = 4,
        /// <summary>
        /// The retrieved handle identifies the child window at the top of the Z order,
        /// if the specified window is a parent window; otherwise, the retrieved handle is NULL.
        /// The function examines only child windows of the specified window. It does not examine descendant windows.
        /// </summary>
        GW_CHILD = 5,
        /// <summary>
        /// The retrieved handle identifies the enabled popup window owned by the specified window (the
        /// search uses the first such window found using GW_HWNDNEXT); otherwise, if there are no enabled
        /// popup windows, the retrieved handle is that of the specified window.
        /// </summary>
        GW_ENABLEDPOPUP = 6
    }

    /// <summary>
    /// 挂钩类型
    /// </summary>
    public enum _WH : int {

        /// Installs a hook procedure that monitors messages before the system sends them to the destination window 
        /// procedure. For more information, see the CallWndProc hook procedure
        WH_CALLWNDPROC = 4,

        /// Installs a hook procedure that monitors messages after they have been processed by the destination window 
        /// procedure. For more information, see the CallWndRetProc hook procedure
        WH_CALLWNDPROCRET = 12,

        /// Installs a hook procedure that receives notifications useful to a CBT application. For more information, 
        /// see the CBTProc hook procedure
        WH_CBT = 5,

        /// Installs a hook procedure useful for debugging other hook procedures. For more information, see the DebugProc 
        /// hook procedure
        WH_DEBUG = 9,

        /// Installs a hook procedure that will be called when the application's foreground thread is about to become 
        /// idle. This hook is useful for performing low priority tasks during idle time. For more information, see 
        /// the ForegroundIdleProc hook procedure
        WH_FOREGROUNDIDLE = 11,

        /// Installs a hook procedure that monitors messages posted to a message queue. For more information, see 
        /// the GetMsgProc hook procedure
        WH_GETMESSAGE = 3,

        /// Installs a hook procedure that posts messages previously recorded by a WH_JOURNALRECORD hook procedure. 
        /// For more information, see the JournalPlaybackProc hook procedure
        WH_JOURNALPLAYBACK = 1,

        /// Installs a hook procedure that records input messages posted to the system message queue. This hook is 
        /// useful for recording macros. For more information, see the JournalRecordProc hook procedure
        WH_JOURNALRECORD = 0,

        /// Installs a hook procedure that monitors keystroke messages. For more information, see the KeyboardProc 
        /// hook procedure
        WH_KEYBOARD = 2,

        /// Installs a hook procedure that monitors low-level keyboard input events. For more information, see the 
        /// LowLevelKeyboardProc hook procedure
        WH_KEYBOARD_LL = 13,

        /// Installs a hook procedure that monitors mouse messages. For more information, see the MouseProc hook 
        /// procedure
        WH_MOUSE = 7,

        /// Installs a hook procedure that monitors low-level mouse input events. For more information, see the LowLevelMouseProc 
        /// hook procedure
        WH_MOUSE_LL = 14,

        /// Installs a hook procedure that monitors messages generated as a result of an input event in a dialog 
        /// box, message box, menu, or scroll bar. For more information, see the MessageProc hook procedure
        WH_MSGFILTER = -1,

        /// Installs a hook procedure that receives notifications useful to shell applications. For more information, 
        /// see the ShellProc hook procedure
        WH_SHELL = 10,

        /// Installs a hook procedure that monitors messages generated as a result of an input event in a dialog 
        /// box, message box, menu, or scroll bar. The hook procedure monitors these messages for all applications 
        /// in the same desktop as the calling thread. For more information, see the SysMsgProc hook procedure
        /// 
        WH_SYSMSGFILTER = 6,

    }

    [Flags]
    public enum _WS : long {

        ///  WS_BORDER 0x00800000L  The window has a thin-line border.
        WS_BORDER = 0x00800000L,

        ///  WS_CAPTION 0x00C00000L  The window has a title bar (includes the WS_BORDER style).
        WS_CAPTION = 0x00C00000L,

        ///  WS_CHILD 0x40000000L  The window is a child window. A window with this style cannot have a menu bar. 
        /// This style cannot be used with the WS_POPUP style
        WS_CHILD = 0x40000000L,

        ///  WS_CHILDWINDOW 0x40000000L  Same as the WS_CHILD style.
        WS_CHILDWINDOW = 0x40000000L,

        ///  WS_CLIPCHILDREN 0x02000000L  Excludes the area occupied by child windows when drawing occurs within 
        /// the parent window. This style is used when creating the parent window
        WS_CLIPCHILDREN = 0x02000000L,

        ///  WS_CLIPSIBLINGS 0x04000000L  Clips child windows relative to each other; that is, when a particular 
        /// child window receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other overlapping child 
        /// windows out of the region of the child window to be updated. If WS_CLIPSIBLINGS is not specified and 
        /// child windows overlap, it is possible, when drawing within the client area of a child window, to draw 
        /// within the client area of a neighboring child window
        WS_CLIPSIBLINGS = 0x04000000L,

        ///  WS_DISABLED 0x08000000L  The window is initially disabled. A disabled window cannot receive input from 
        /// the user. To change this after a window has been created, use the EnableWindow function
        WS_DISABLED = 0x08000000L,

        ///  WS_DLGFRAME 0x00400000L  The window has a border of a style typically used with dialog boxes. A window 
        /// with this style cannot have a title bar
        WS_DLGFRAME = 0x00400000L,

        ///  WS_GROUP 0x00020000L  The window is the first control of a group of controls. The group consists of 
        /// this first control and all controls defined after it, up to the next control with the WS_GROUP style. 
        /// The first control in each group usually has the WS_TABSTOP style so that the user can move from group 
        /// to group. The user can subsequently change the keyboard focus from one control in the group to the next 
        /// control in the group by using the direction keys. You can turn this style on and off to change dialog 
        /// box navigation. To change this style after a window has been created, use the SetWindowLong function
        /// 
        WS_GROUP = 0x00020000L,

        ///  WS_HSCROLL 0x00100000L  The window has a horizontal scroll bar.
        WS_HSCROLL = 0x00100000L,

        ///  WS_ICONIC 0x20000000L  The window is initially minimized. Same as the WS_MINIMIZE style.
        WS_ICONIC = 0x20000000L,

        ///  WS_MAXIMIZE 0x01000000L  The window is initially maximized.
        WS_MAXIMIZE = 0x01000000L,

        ///  WS_MAXIMIZEBOX 0x00010000L  The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP 
        /// style. The WS_SYSMENU style must also be specified
        WS_MAXIMIZEBOX = 0x00010000L,

        ///  WS_MINIMIZE 0x20000000L  The window is initially minimized. Same as the WS_ICONIC style.
        WS_MINIMIZE = 0x20000000L,

        ///  WS_MINIMIZEBOX 0x00020000L  The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP 
        /// style. The WS_SYSMENU style must also be specified
        WS_MINIMIZEBOX = 0x00020000L,

        ///  WS_OVERLAPPED 0x00000000L  The window is an overlapped window. An overlapped window has a title bar 
        /// and a border. Same as the WS_TILED style
        WS_OVERLAPPED = 0x00000000L,

        ///  WS_OVERLAPPEDWINDOW (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX) 
        /// The window is an overlapped window. Same as the WS_TILEDWINDOW style
        WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX),

        ///  WS_POPUP 0x80000000L  The window is a pop-up window. This style cannot be used with the WS_CHILD st
        /// 
        WS_POPUP = 0x80000000L,

        ///  WS_POPUPWINDOW (WS_POPUP | WS_BORDER | WS_SYSMENU)  The window is a pop-up window. The WS_CAPTION and 
        /// WS_POPUPWINDOW styles must be combined to make the window menu visible
        WS_POPUPWINDOW = (WS_POPUP | WS_BORDER | WS_SYSMENU),

        ///  WS_SIZEBOX 0x00040000L  The window has a sizing border. Same as the WS_THICKFRAME style.
        WS_SIZEBOX = 0x00040000L,

        ///  WS_SYSMENU 0x00080000L  The window has a window menu on its title bar. The WS_CAPTION style must also 
        /// be specified
        WS_SYSMENU = 0x00080000L,

        ///  WS_TABSTOP 0x00010000L  The window is a control that can receive the keyboard focus when the user presses 
        /// the TAB key. Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP 
        /// style. You can turn this style on and off to change dialog box navigation. To change this style after 
        /// a window has been created, use the SetWindowLong function. For user-created windows and modeless dialogs 
        /// to work with tab stops, alter the message loop to call the IsDialogMessage function
        WS_TABSTOP = 0x00010000L,

        ///  WS_THICKFRAME 0x00040000L  The window has a sizing border. Same as the WS_SIZEBOX style.
        WS_THICKFRAME = 0x00040000L,

        ///  WS_TILED 0x00000000L  The window is an overlapped window. An overlapped window has a title bar and a 
        /// border. Same as the WS_OVERLAPPED style
        WS_TILED = 0x00000000L,

        ///  WS_TILEDWINDOW (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX) 
        /// The window is an overlapped window. Same as the WS_OVERLAPPEDWINDOW style
        WS_TILEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX),

        ///  WS_VISIBLE 0x10000000L  The window is initially visible. This style can be turned on and off by using 
        /// the ShowWindow or SetWindowPos function
        WS_VISIBLE = 0x10000000L,

        ///  WS_VSCROLL 0x00200000L  The window has a vertical scroll bar.
        WS_VSCROLL = 0x00200000L,

    }

    [Flags]
    public enum _SWP : uint {

        /// If the calling thread and the thread that owns the window are attached to different input queues, the 
        /// system posts the request to the thread that owns the window. This prevents the calling thread from blocking 
        /// its execution while other threads process the request
        SWP_ASYNCWINDOWPOS = 0x4000,

        /// Prevents generation of the WM_SYNCPAINT message.
        SWP_DEFERERASE = 0x2000,

        /// Draws a frame (defined in the window's class description) around the window.
        SWP_DRAWFRAME = 0x0020,

        /// Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, 
        /// even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent 
        /// only when the window's size is being changed
        SWP_FRAMECHANGED = 0x0020,

        /// Hides the window.
        SWP_HIDEWINDOW = 0x0080,

        /// Does not activate the window. If this flag is not set, the window is activated and moved to the top of 
        /// either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter)
        /// 
        SWP_NOACTIVATE = 0x0010,

        /// Discards the entire contents of the client area. If this flag is not specified, the valid contents of 
        /// the client area are saved and copied back into the client area after the window is sized or repositi
        /// 
        SWP_NOCOPYBITS = 0x0100,

        /// Retains the current position (ignores X and Y parameters).
        SWP_NOMOVE = 0x0002,

        /// Does not change the owner window's position in the Z order.
        SWP_NOOWNERZORDER = 0x0200,

        /// Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client 
        /// area, the nonclient area (including the title bar and scroll bars), and any part of the parent window 
        /// uncovered as a result of the window being moved. When this flag is set, the application must explicitly 
        /// invalidate or redraw any parts of the window and parent window that need redrawing
        SWP_NOREDRAW = 0x0008,

        /// Same as the SWP_NOOWNERZORDER flag.
        SWP_NOREPOSITION = 0x0200,

        /// Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
        SWP_NOSENDCHANGING = 0x0400,

        /// Retains the current size (ignores the cx and cy parameters).
        SWP_NOSIZE = 0x0001,

        /// Retains the current Z order (ignores the hWndInsertAfter parameter).
        SWP_NOZORDER = 0x0004,

        /// Displays the window.
        SWP_SHOWWINDOW = 0x0040,

    }

    [Flags]
    public enum _MK {

        /// The CTRL key is down.
        MK_CONTROL = 0x0008,

        /// The middle mouse button is down.
        MK_MBUTTON = 0x0010,

        /// The right mouse button is down.
        MK_RBUTTON = 0x0002,

        /// The SHIFT key is down.
        MK_SHIFT = 0x0004,

        /// The first X button is down.
        MK_XBUTTON1 = 0x0020,

        /// The second X button is down.
        MK_XBUTTON2 = 0x0040,

    }

    /// <summary>
    /// The return value of the DefWindowProc function is one of the following values, indicating the position of the cursor hot spot.
    /// see https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-nchittest
    /// </summary>
    public enum _NCHITTEST {

        /// In the border of a window that does not have a sizing border.
        HTBORDER = 18,

        /// In the lower-horizontal border of a resizable window (the user can click the mouse to resize the window 
        /// vertically)
        HTBOTTOM = 15,

        /// In the lower-left corner of a border of a resizable window (the user can click the mouse to resize the 
        /// window diagonally)
        HTBOTTOMLEFT = 16,

        /// In the lower-right corner of a border of a resizable window (the user can click the mouse to resize the 
        /// window diagonally)
        HTBOTTOMRIGHT = 17,

        /// In a title bar.
        HTCAPTION = 2,

        /// In a client area.
        HTCLIENT = 1,

        /// In a Close button.
        HTCLOSE = 20,

        /// On the screen background or on a dividing line between windows (same as HTNOWHERE, except that the DefWindowProc 
        /// function produces a system beep to indicate an error)
        HTERROR = -2,

        /// In a size box (same as HTSIZE).
        HTGROWBOX = 4,

        /// In a Help button.
        HTHELP = 21,

        /// In a horizontal scroll bar.
        HTHSCROLL = 6,

        /// In the left border of a resizable window (the user can click the mouse to resize the window horizont
        /// 
        HTLEFT = 10,

        /// In a menu.
        HTMENU = 5,

        /// In a Maximize button.
        HTMAXBUTTON = 9,

        /// In a Minimize button.
        HTMINBUTTON = 8,

        /// On the screen background or on a dividing line between windows.
        HTNOWHERE = 0,

        /// In a Minimize button.
        HTREDUCE = 8,

        /// In the right border of a resizable window (the user can click the mouse to resize the window horizon
        /// 
        HTRIGHT = 11,

        /// In a size box (same as HTGROWBOX).
        HTSIZE = 4,

        /// In a window menu or in a Close button in a child window.
        HTSYSMENU = 3,

        /// In the upper-horizontal border of a window.
        HTTOP = 12,

        /// In the upper-left corner of a window border.
        HTTOPLEFT = 13,

        /// In the upper-right corner of a window border.
        HTTOPRIGHT = 14,

        /// In a window currently covered by another window in the same thread (the message will be sent to underlying 
        /// windows in the same thread until one of them returns a code that is not HTTRANSPARENT)
        HTTRANSPARENT = -1,

        /// In the vertical scroll bar.
        HTVSCROLL = 7,

        /// In a Maximize button.
        HTZOOM = 9,

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

    internal enum WindowCompositionAttribute {
        // 省略其他未使用的字段
        WCA_ACCENT_POLICY = 19,
    }

    public enum _WM : int {

        WM_NULL = 0x00,

        WM_CREATE = 0x01,

        WM_DESTROY = 0x02,

        WM_MOVE = 0x03,

        WM_SIZE = 0x05,

        WM_ACTIVATE = 0x06,

        WM_SETFOCUS = 0x07,

        WM_KILLFOCUS = 0x08,

        WM_ENABLE = 0x0A,

        WM_SETREDRAW = 0x0B,

        WM_SETTEXT = 0x0C,

        WM_GETTEXT = 0x0D,

        WM_GETTEXTLENGTH = 0x0E,

        WM_PAINT = 0x0F,

        WM_CLOSE = 0x10,

        WM_QUERYENDSESSION = 0x11,

        WM_QUIT = 0x12,

        WM_QUERYOPEN = 0x13,

        WM_ERASEBKGND = 0x14,

        WM_SYSCOLORCHANGE = 0x15,

        WM_ENDSESSION = 0x16,

        WM_SYSTEMERROR = 0x17,

        WM_SHOWWINDOW = 0x18,

        WM_CTLCOLOR = 0x19,

        WM_WININICHANGE = 0x1A,

        WM_SETTINGCHANGE = 0x1A,

        WM_DEVMODECHANGE = 0x1B,

        WM_ACTIVATEAPP = 0x1C,

        WM_FONTCHANGE = 0x1D,

        WM_TIMECHANGE = 0x1E,

        WM_CANCELMODE = 0x1F,

        WM_SETCURSOR = 0x20,

        WM_MOUSEACTIVATE = 0x21,

        WM_CHILDACTIVATE = 0x22,

        WM_QUEUESYNC = 0x23,

        WM_GETMINMAXINFO = 0x24,

        WM_PAINTICON = 0x26,

        WM_ICONERASEBKGND = 0x27,

        WM_NEXTDLGCTL = 0x28,

        WM_SPOOLERSTATUS = 0x2A,

        WM_DRAWITEM = 0x2B,

        WM_MEASUREITEM = 0x2C,

        WM_DELETEITEM = 0x2D,

        WM_VKEYTOITEM = 0x2E,

        WM_CHARTOITEM = 0x2F,

        WM_SETFONT = 0x30,

        WM_GETFONT = 0x31,

        WM_SETHOTKEY = 0x32,

        WM_GETHOTKEY = 0x33,

        WM_QUERYDRAGICON = 0x37,

        WM_COMPAREITEM = 0x39,

        WM_COMPACTING = 0x41,

        WM_WINDOWPOSCHANGING = 0x46,

        WM_WINDOWPOSCHANGED = 0x47,

        WM_POWER = 0x48,

        WM_COPYDATA = 0x4A,

        WM_CANCELJOURNAL = 0x4B,

        WM_NOTIFY = 0x4E,

        WM_INPUTLANGCHANGEREQUEST = 0x50,

        WM_INPUTLANGCHANGE = 0x51,

        WM_TCARD = 0x52,

        WM_HELP = 0x53,

        WM_USERCHANGED = 0x54,

        WM_NOTIFYFORMAT = 0x55,

        WM_CONTEXTMENU = 0x7B,

        WM_STYLECHANGING = 0x7C,

        WM_STYLECHANGED = 0x7D,

        WM_DISPLAYCHANGE = 0x7E,

        WM_GETICON = 0x7F,

        WM_SETICON = 0x80,

        WM_NCCREATE = 0x81,

        WM_NCDESTROY = 0x82,

        WM_NCCALCSIZE = 0x83,

        WM_NCHITTEST = 0x84,

        WM_NCPAINT = 0x85,

        WM_NCACTIVATE = 0x86,

        WM_GETDLGCODE = 0x87,

        WM_NCMOUSEMOVE = 0xA0,

        WM_NCLBUTTONDOWN = 0xA1,

        WM_NCLBUTTONUP = 0xA2,

        WM_NCLBUTTONDBLCLK = 0xA3,

        WM_NCRBUTTONDOWN = 0xA4,

        WM_NCRBUTTONUP = 0xA5,

        WM_NCRBUTTONDBLCLK = 0xA6,

        WM_NCMBUTTONDOWN = 0xA7,

        WM_NCMBUTTONUP = 0xA8,

        WM_NCMBUTTONDBLCLK = 0xA9,

        WM_KEYFIRST = 0x100,

        WM_KEYDOWN = 0x100,

        WM_KEYUP = 0x101,

        WM_CHAR = 0x102,

        WM_DEADCHAR = 0x103,

        WM_SYSKEYDOWN = 0x104,

        WM_SYSKEYUP = 0x105,

        WM_SYSCHAR = 0x106,

        WM_SYSDEADCHAR = 0x107,

        WM_KEYLAST = 0x108,

        WM_IME_STARTCOMPOSITION = 0x10D,

        WM_IME_ENDCOMPOSITION = 0x10E,

        WM_IME_COMPOSITION = 0x10F,

        WM_IME_KEYLAST = 0x10F,

        WM_INITDIALOG = 0x110,

        WM_COMMAND = 0x111,

        WM_SYSCOMMAND = 0x112,

        WM_TIMER = 0x113,

        WM_HSCROLL = 0x114,

        WM_VSCROLL = 0x115,

        WM_INITMENU = 0x116,

        WM_INITMENUPOPUP = 0x117,

        WM_MENUSELECT = 0x11F,

        WM_MENUCHAR = 0x120,

        WM_ENTERIDLE = 0x121,

        WM_CTLCOLORMSGBOX = 0x132,

        WM_CTLCOLOREDIT = 0x133,

        WM_CTLCOLORLISTBOX = 0x134,

        WM_CTLCOLORBTN = 0x135,

        WM_CTLCOLORDLG = 0x136,

        WM_CTLCOLORSCROLLBAR = 0x137,

        WM_CTLCOLORSTATIC = 0x138,

        WM_MOUSEFIRST = 0x200,

        WM_MOUSEMOVE = 0x200,

        WM_LBUTTONDOWN = 0x201,

        WM_LBUTTONUP = 0x202,

        WM_LBUTTONDBLCLK = 0x203,

        WM_RBUTTONDOWN = 0x204,

        WM_RBUTTONUP = 0x205,

        WM_RBUTTONDBLCLK = 0x206,

        WM_MBUTTONDOWN = 0x207,

        WM_MBUTTONUP = 0x208,

        WM_MBUTTONDBLCLK = 0x209,

        WM_MOUSEWHEEL = 0x20A,

        WM_MOUSEHWHEEL = 0x20E,

        WM_PARENTNOTIFY = 0x210,

        WM_ENTERMENULOOP = 0x211,

        WM_EXITMENULOOP = 0x212,

        WM_NEXTMENU = 0x213,

        WM_SIZING = 0x214,

        WM_CAPTURECHANGED = 0x215,

        WM_MOVING = 0x216,

        WM_POWERBROADCAST = 0x218,

        WM_DEVICECHANGE = 0x219,

        WM_MDICREATE = 0x220,

        WM_MDIDESTROY = 0x221,

        WM_MDIACTIVATE = 0x222,

        WM_MDIRESTORE = 0x223,

        WM_MDINEXT = 0x224,

        WM_MDIMAXIMIZE = 0x225,

        WM_MDITILE = 0x226,

        WM_MDICASCADE = 0x227,

        WM_MDIICONARRANGE = 0x228,

        WM_MDIGETACTIVE = 0x229,

        WM_MDISETMENU = 0x230,

        WM_ENTERSIZEMOVE = 0x231,

        WM_EXITSIZEMOVE = 0x232,

        WM_DROPFILES = 0x233,

        WM_MDIREFRESHMENU = 0x234,

        WM_IME_SETCONTEXT = 0x281,

        WM_IME_NOTIFY = 0x282,

        WM_IME_CONTROL = 0x283,

        WM_IME_COMPOSITIONFULL = 0x284,

        WM_IME_SELECT = 0x285,

        WM_IME_CHAR = 0x286,

        WM_IME_KEYDOWN = 0x290,

        WM_IME_KEYUP = 0x291,

        WM_MOUSEHOVER = 0x2A1,

        WM_NCMOUSELEAVE = 0x2A2,

        WM_MOUSELEAVE = 0x2A3,

        WM_CUT = 0x300,

        WM_COPY = 0x301,

        WM_PASTE = 0x302,

        WM_CLEAR = 0x303,

        WM_UNDO = 0x304,

        WM_RENDERFORMAT = 0x305,

        WM_RENDERALLFORMATS = 0x306,

        WM_DESTROYCLIPBOARD = 0x307,

        WM_DRAWCLIPBOARD = 0x308,

        WM_PAINTCLIPBOARD = 0x309,

        WM_VSCROLLCLIPBOARD = 0x30A,

        WM_SIZECLIPBOARD = 0x30B,

        WM_ASKCBFORMATNAME = 0x30C,

        WM_CHANGECBCHAIN = 0x30D,

        WM_HSCROLLCLIPBOARD = 0x30E,

        WM_QUERYNEWPALETTE = 0x30F,

        WM_PALETTEISCHANGING = 0x310,

        WM_PALETTECHANGED = 0x311,

        WM_HOTKEY = 0x312,

        WM_PRINT = 0x317,

        WM_PRINTCLIENT = 0x318,

        WM_HANDHELDFIRST = 0x358,

        WM_HANDHELDLAST = 0x35F,

        WM_PENWINFIRST = 0x380,

        WM_PENWINLAST = 0x38F,

        WM_COALESCE_FIRST = 0x390,

        WM_COALESCE_LAST = 0x39F,

        WM_DDE_FIRST = 0x3E0,

        WM_DDE_INITIATE = 0x3E0,

        WM_DDE_TERMINATE = 0x3E1,

        WM_DDE_ADVISE = 0x3E2,

        WM_DDE_UNADVISE = 0x3E3,

        WM_DDE_ACK = 0x3E4,

        WM_DDE_DATA = 0x3E5,

        WM_DDE_REQUEST = 0x3E6,

        WM_DDE_POKE = 0x3E7,

        WM_DDE_EXECUTE = 0x3E8,

        WM_DDE_LAST = 0x3E8,

        WM_USER = 0x400,

        WM_APP = 0x8000
    }

    #endregion
}
