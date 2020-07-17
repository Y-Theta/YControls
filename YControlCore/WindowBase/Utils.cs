///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using YControlCore.Interop;

namespace YControlCore.WindowBase {
    public class Utils {


        #region Properties
        #endregion

        #region Methods

        private static FieldInfo? windowField = typeof(NotifyIcon).GetField("window", BindingFlags.NonPublic | BindingFlags.Instance);

        private static FieldInfo? idField = typeof(NotifyIcon).GetField("id", BindingFlags.NonPublic | BindingFlags.Instance);

        public static Rectangle GetIconRect(NotifyIcon icon) {
            _RECT rect = new _RECT();
            NOTIFYICONIDENTIFIER notifyIcon = new NOTIFYICONIDENTIFIER();

            notifyIcon.cbSize = Marshal.SizeOf(notifyIcon);
            //use hWnd and id of NotifyIcon instead of guid is needed
            notifyIcon.hWnd = GetHandle(icon);
            notifyIcon.uID = GetId(icon);

            int hresult = Core.Shell_NotifyIconGetRect(ref notifyIcon, out rect);
            //rect now has the position and size of icon

            return new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
        }

        private static IntPtr GetHandle(NotifyIcon icon) {
            if (windowField == null) throw new InvalidOperationException("windowField prop not find !");
            NativeWindow? window = windowField.GetValue(icon) as NativeWindow;

            if (window == null) throw new InvalidOperationException("windowField prop is null!");  // should not happen?
            return window.Handle;
        }

        private static int GetId(NotifyIcon icon) {
            if (idField == null) throw new InvalidOperationException("[Useful error message]");
            int? id = (int?)idField.GetValue(icon);
            if (!id.HasValue)
                throw new InvalidOperationException("[Useful error message]");
            return id.Value;
        }
        #endregion

        #region Constructors
        #endregion
    }
}
