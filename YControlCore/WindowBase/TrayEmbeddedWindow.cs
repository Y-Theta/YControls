///------------------------------------------------------------------------------
/// @ Y_Theta
/// 
/// Window Embedded in TrayNotification Area
/// need native dll to hook and subclass the 'TrayNotifyWnd' class
/// process its WM_USER + 100 and WM_NOTIFY to leave the space for 
/// you own window
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using YControlCore.Interop;
using System.Windows.Interop;
using System.Windows.Media;

using static YControlCore.Interop.Core;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Forms;
using Orientation = System.Windows.Controls.Orientation;
using System.Windows.Controls.Primitives;

namespace YControlCore.WindowBase {

    /// <summary>
    /// Class Embedded in Tray (not notification icon and width can be customed)
    /// </summary>
    public class TrayEmbeddedWindow : Window, IDisposable {
        #region Properties
        public const int WM_TRAY    = (int)_WM.WM_USER + 100;
        public const int WMC_INIT   = (int)_WM.WM_USER + 1;
        public const int WMC_SIZE   = (int)_WM.WM_USER + 2;
        public const int WMC_NEW    = (int)_WM.WM_USER + 3;

        private Popup _contentholder;

        /// <summary>
        /// the hook dll ptr
        /// </summary>
        private IntPtr _plibrary = IntPtr.Zero;
        /// <summary>
        /// window handle
        /// </summary>
        private IntPtr _pHandle = IntPtr.Zero;
        private IntPtr _pCHandle = IntPtr.Zero;
        protected IntPtr _pTrayHandle = IntPtr.Zero;
        private IntPtr _taskbar = IntPtr.Zero;

        /// <summary>
        /// native method ptr
        /// </summary>
        private IntPtr _hook = IntPtr.Zero;
        private IntPtr _unhook = IntPtr.Zero;
        private IntPtr _exit = IntPtr.Zero;
        private _RECT? _lastpos;
        private delegate void funch(IntPtr hwnd);

        private funch _fhook = null;
        private Action _funhook = null;
        private Action _fexit = null;

        private bool disposedValue;
        private bool _inited = false;

        private string _nativeHookPath = @"hsystrayembed.dll";
        /// <summary>
        /// The hook dll path ,if relative will find in the main process directory
        /// </summary>
        public string NativeHookPath {
            get => _nativeHookPath;
            set => _nativeHookPath = value;
        }

        /// <summary>
        /// taskbar orientation
        /// </summary>
        private Orientation _orientation;
        public Orientation Orientation {
            get => _orientation;
            private set => _orientation = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public Size ContentSize {
            get { return (Size)GetValue(ContentSizeProperty); }
            set { SetValue(ContentSizeProperty, value); }
        }
        public static readonly DependencyProperty ContentSizeProperty =
            DependencyProperty.Register("ContentSize", typeof(Size),
                typeof(TrayEmbeddedWindow), new PropertyMetadata(new Size(32, 32), OnWorHChanged));

        #endregion

        #region Override

        #endregion

        #region Methods

        public void TrayShow() {
            //_contentholder.StaysOpen = true;
            //_contentholder.IsOpen = true;
            //_pCHandle = ((HwndSource)PresentationSource.FromVisual(_contentholder.Child)).Handle;
            Show();
        }

        /// <summary>
        /// when window width or hight change call the hook proc to refrash tray layout
        /// </summary>
        private static void OnWorHChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            TrayEmbeddedWindow t = (TrayEmbeddedWindow)d;
            //t._contentholder.Child.Measure(t.ContentSize);
            if (t._pTrayHandle != IntPtr.Zero) {
                try {
                    int size = ((int)t.Width << 16) + (int)t.Height;
                    int cll = (int)SendMessage(t._pTrayHandle, WMC_SIZE, IntPtr.Zero, (IntPtr)size);
                    Debug.WriteLine(cll);
                } catch {

                }
            }
        }

        /// <summary>
        /// Loadlibrary and install hook
        /// </summary>
        private void _LoadLib() {
            try {
                _plibrary = LoadLibrary(_nativeHookPath);
                if (_plibrary != IntPtr.Zero) {
                    _hook = GetProcAddress(_plibrary, "Hook");
                    _fhook = (funch)Marshal.GetDelegateForFunctionPointer(_hook, typeof(funch));
                    _unhook = GetProcAddress(_plibrary, "UnHook");
                    _funhook = (Action)Marshal.GetDelegateForFunctionPointer(_unhook, typeof(Action));
                    _exit = GetProcAddress(_plibrary, "Exit");
                    _fexit = (Action)Marshal.GetDelegateForFunctionPointer(_exit, typeof(Action));
                    if (_fhook == null || _fexit == null || _funhook == null)
                        throw new EntryPointNotFoundException();
                } else {
                    throw new DllNotFoundException();
                }
            } catch (Exception ex) {
                int error = Marshal.GetLastWin32Error();
                throw new Exception($"Native call error {error}", ex);
            }
        }

        private void _TaskbarQ() {
            Size size = new Size(Screen.PrimaryScreen.Bounds.Width - SystemInformation.WorkingArea.Width,
                 Screen.PrimaryScreen.Bounds.Height - SystemInformation.WorkingArea.Height);
            _orientation = size.Width > size.Height ? Orientation.Vertical : Orientation.Horizontal;
        }

        private void _Arrange() {
            _TaskbarQ();
            GetWindowRect(_pTrayHandle, out _RECT bounds);
            bool changed = true;
            if (_lastpos.HasValue) {
                changed = !bounds.Equals(_lastpos);
            }
            _lastpos = bounds;
            //Debug.WriteLine($"{bounds.ToString(true)}");
            if (changed && _pTrayHandle != IntPtr.Zero) {
                if (_orientation.Equals(Orientation.Horizontal)) {
                    this.Height = bounds.bottom - bounds.top;
                    this.Width = ContentSize.Width;
                    //SetWindowPos(_pHandle, (IntPtr)(-1), bounds.right, bounds.top, 0, 0,
                    //    _SWP.SWP_NOSIZE);
                } else {
                    this.Width = bounds.right - bounds.left;
                    this.Height = ContentSize.Height;
                    //SetWindowPos(_pHandle, (IntPtr)(-1), bounds.left, bounds.top, 0, 0,
                    //    _SWP.SWP_NOSIZE);
                }
                _inited = true;
            }
        }

        private void TrayEmbeddedWindow_SourceInitialized(object? sender, EventArgs e) {
            HwndSource source = (HwndSource)HwndSource.FromVisual((Visual)sender);

            _pHandle = source.Handle;
            _taskbar = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Shell_TrayWnd", null);
            var interop = new WindowInteropHelper(this);
            interop.Owner = _taskbar;
            //SetWindowLongPtr(_pHandle, -16, (IntPtr)(_WS.WS_CHILD | _WS.WS_POPUP));
            //start wndproc
            source.AddHook(new HwndSourceHook(WndProc));
        }

        private void TrayEmbeddedWindow_Loaded(object sender, RoutedEventArgs e) {
            //load hook lib
            _LoadLib();
            //install hook
            _fhook(_pHandle);
            _TaskbarQ();
        }

        /// <summary>
        /// IPC with native hook dll
        /// </summary>
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {
            switch (msg) {
                case WM_TRAY:
                    //Debug.WriteLine("WM_TRAY");
                    break;
                case WMC_SIZE:
                    //Debug.WriteLine("WMC_SIZE");
                    _Arrange();
                    //SetWindowPos(_pHandle, (IntPtr)(-1), 0, 0, 0, 0,
                    //  _SWP.SWP_NOSIZE |_SWP.SWP_NOMOVE | _SWP.SWP_NOACTIVATE);
                    break;
                case (int)_WM.WM_WINDOWPOSCHANGED:
                    //Debug.WriteLine("WM_WINDOWPOSCHANGED");
                    PostMessage(_taskbar, (uint)_WM.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
                    break;
                case (int)_WM.WM_DESTROY:
                    _fexit?.Invoke();
                    Dispose();
                    break;
                case WMC_INIT:
                    _pTrayHandle = lParam;
                    if (_taskbar == IntPtr.Zero)
                        _taskbar = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Shell_TrayWnd", null);
                    _funhook?.Invoke();
                    OnWorHChanged(this, new DependencyPropertyChangedEventArgs());
                    break;
            }
            return IntPtr.Zero;
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                _fexit?.Invoke();
                if (disposing) {
                    // TODO: 释放托管状态(托管对象)
                    _funhook = null;
                    _fhook = null;
                    _fexit = null;
                    //_contentholder.IsOpen = false;
                }

                if (_plibrary != IntPtr.Zero) {
                    FreeLibrary(_plibrary);
                }
                disposedValue = true;
            }
        }

        public void Dispose() {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            //GC.SuppressFinalize(this);
        }
        #endregion

        #region Constructors
        public TrayEmbeddedWindow() {
            AllowsTransparency = true;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            ShowInTaskbar = false;
            this.Height = this.Width = 32;
            ContentSize = new Size(Width, Height);
            SourceInitialized += TrayEmbeddedWindow_SourceInitialized;
            Loaded += TrayEmbeddedWindow_Loaded;
        }

        ~TrayEmbeddedWindow() {
            Dispose(false);
        }

        static TrayEmbeddedWindow() {

            DefaultStyleKeyProperty.OverrideMetadata(typeof(TrayEmbeddedWindow), 
                new FrameworkPropertyMetadata(typeof(TrayEmbeddedWindow)));


        }
        #endregion
    }
}
