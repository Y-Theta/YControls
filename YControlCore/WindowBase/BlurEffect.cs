///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Media;

using YControlCore.Interop;

using static YControlCore.Interop.Core;

namespace YControlCore.WindowBase {
    /// <summary>
    /// 启用毛玻璃效果
    /// </summary>
    public class BlurEffect {

        /// <summary>
        /// 设置控件效果
        /// </summary>
        //public static bool SetBlur(IntPtr hwnd, AccentState state) {
        //    try {
        //        EnableBlur(hwnd, state);
        //        return true;
        //    } catch {
        //        return false;
        //    }
        //}

        public static readonly DependencyProperty BlurProperty = DependencyProperty.RegisterAttached(
            "Blur", typeof(AccentState), typeof(BlurEffect), new PropertyMetadata(AccentState.ACCENT_DISABLED, OnBlurChanged));

        public static void SetBlur(FrameworkElement element, AccentState value) {
            //由于某些原因在我的vs和net版本下 如果此方法的名称为  SetBlurProperty 则可以起作用，
            //但在xaml中对应的属性名称就会变成"BlurProperty"，否则在Xaml下设置Blur属性值时此方法不会
            //触发，因此只能在OnBlurChanged中更改此方法
            try {
                IntPtr hwnd = IntPtr.Zero;
                if (element.IsInitialized) {
                    hwnd = GetVisualHandle(element);
                    EnableBlur(hwnd, value);
                } else {
                    element.Loaded += (s, e) => {
                        hwnd = GetVisualHandle(element);
                        EnableBlur(hwnd, value);
                    };
                }
            } catch {
            }
        }

        private static void OnBlurChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            FrameworkElement element = (FrameworkElement)d;
            SetBlur(element, (AccentState)e.NewValue);
        }

        //public static void SetBlur(FrameworkElement element, bool flag) {
        //    if (element is null) throw new ArgumentException("element");
        //    else {
        //        IntPtr hwnd = IntPtr.Zero;
        //        hwnd = GetVisualHandle(element);
        //        EnableBlur(hwnd, flag ? AccentState.ACCENT_ENABLE_BLURBEHIND : AccentState.ACCENT_DISABLED);
        //        //try {
        //        //    hwnd = GetVisualHandle(element as Visual);
        //        //    SetBlur(hwnd, flag ? AccentState.ACCENT_ENABLE_BLURBEHIND : AccentState.ACCENT_DISABLED);
        //        //} catch {
        //        //    ((FrameworkElement)element).Loaded += new RoutedEventHandler((sender,e)=> {
        //        //        hwnd = GetVisualHandle(sender as Visual);
        //        //        SetBlur(hwnd, flag ? AccentState.ACCENT_ENABLE_BLURBEHIND : AccentState.ACCENT_DISABLED);
        //        //    });
        //        //}
        //        element.SetValue(BlurProperty, flag);
        //    }
        //}

        //public static bool GetBlur(FrameworkElement element) {
        //    if (element is null || !(element is Visual)) throw new ArgumentException("element");
        //    return (bool)element.GetValue(BlurProperty);
        //}

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
