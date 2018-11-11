using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using YControls.InterAction;
using YControls.WinAPI;
using static YControls.WinAPI.DllImportMethods;

namespace YControls.AreaIconWindow {
    public class WindowBlur {

        private Window _window;

        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(WindowBlur),
            new PropertyMetadata(false, OnIsEnabledChanged));

        public static void SetIsEnabled(DependencyObject element, bool value) {
            element.SetValue(IsEnabledProperty, value);
        }

        public static bool GetIsEnabled(DependencyObject element) {
            return (bool)element.GetValue(IsEnabledProperty);
        }

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is Window window) {
                if ((bool)e.NewValue) {
                    var blur = new WindowBlur();
                    blur.Attach(window);
                    window.SetValue(WindowBlurProperty, blur);
                }
                else {
                    GetWindowBlur(window)?.Detach();
                    window.ClearValue(WindowBlurProperty);
                }
            }
        }

        public static readonly DependencyProperty WindowBlurProperty = DependencyProperty.RegisterAttached(
            "WindowBlur", typeof(WindowBlur), typeof(WindowBlur),
            new PropertyMetadata(null, OnWindowBlurChanged));

        public static void SetWindowBlur(DependencyObject element, WindowBlur value) {
            element.SetValue(WindowBlurProperty, value);
        }

        public static WindowBlur GetWindowBlur(DependencyObject element) {
            return (WindowBlur)element.GetValue(WindowBlurProperty);
        }


        private static void OnWindowBlurChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is Window window) {
                (e.OldValue as WindowBlur)?.Detach();
                (e.NewValue as WindowBlur)?.Attach(window);
            }
        }

        private void Attach(Window window) {
            _window = window;
            var source = (HwndSource)PresentationSource.FromVisual(window);
            if (source == null) {
                window.SourceInitialized += OnSourceInitialized;
            }
            else {
                AttachCore();
            }
        }

        /// <summary>
        /// 取消事件关联
        /// </summary>
        private void Detach() {
            try {
                DetachCore();
            }
            finally {
                _window = null;
            }
        }

        private void OnSourceInitialized(object sender, EventArgs e) {
            ((Window)sender).SourceInitialized -= OnSourceInitialized;
            AttachCore();
        }

        private void AttachCore() {
            var windowHelper = HandleHelper.GetVisualHandle(_window);
            EnableBlur(windowHelper);
        }

        private void DetachCore() {
            _window.SourceInitialized += OnSourceInitialized;
        }

        internal static void EnableBlur(IntPtr hwnd) {

            var accent = new AccentPolicy {
                AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND
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
