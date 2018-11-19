using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using YControls.InterAction;
using YControls.WinAPI;
using static YControls.WinAPI.DllImportMethods;

namespace YControls.AreaIconWindow {
    /// <summary>
    /// 启用毛玻璃效果
    /// </summary>
    public class BlurEffect {

        /// <summary>
        /// 设置控件效果
        /// </summary>
        public static bool SetBlur(IntPtr hwnd, AccentState state) {
            try {
                EnableBlur(hwnd, state);
                return true;
            }
            catch {
                return false;
            }

        }

        /// <summary>
        /// 给句柄指定的控件设置毛玻璃效果
        /// </summary>
        internal static void EnableBlur(IntPtr hwnd, AccentState state) {

            var accent = new AccentPolicy {
                AccentState = state
            };

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };

            SetWindowCompositionAttribute(hwnd, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }
    }

}
