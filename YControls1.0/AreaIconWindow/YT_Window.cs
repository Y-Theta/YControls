using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using YControls.InterAction;
using AccentState = YControls.WinAPI.DllImportMethods.AccentState;

namespace YControls.AreaIconWindow {

    /// <summary>
    /// 带托盘图标与窗体消息循环的窗体
    /// </summary>
    [TemplatePart(Name = "YT_TitleBar")]
    public class YT_Window : Window {
        /// <summary>
        /// 与窗体关联的标题栏
        /// </summary>
        private YT_TitleBar _titlebar { get; set; }

        /// <summary>
        /// 窗口所拥有的托盘图标
        /// </summary>
        public Dictionary<string, YT_AreaIcon> AreaIcons { get; private set; }

        /// <summary>
        /// 开启毛玻璃
        /// </summary>
        public bool EnableAeroGlass {
            get { return (bool)GetValue(EnableAeroGlassProperty); }
            set { SetValue(EnableAeroGlassProperty, value); }
        }
        public static readonly DependencyProperty EnableAeroGlassProperty =
            DependencyProperty.Register("EnableAeroGlass", typeof(bool),
                typeof(YT_Window), new PropertyMetadata(false, OnEnableAeroGlassChanged));
        private static void OnEnableAeroGlassChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((YT_Window)d).ActiveBlur((bool)e.NewValue);
        }

        #region ExtendToTitleBar
        /// <summary>
        /// 是否将窗口内容扩充至标题栏
        /// </summary>
        public bool ExtendToTitleBar {
            get { return (bool)GetValue(ExtendToTitleBarProperty); }
            set { SetValue(ExtendToTitleBarProperty, value); }
        }
        public static readonly DependencyProperty ExtendToTitleBarProperty =
            DependencyProperty.Register("ExtendToTitleBar", typeof(bool),
                typeof(YT_Window), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #region TitleArea
        /// <summary>
        /// 标题区域
        /// </summary>
        public object TitleArea {
            get { return (object)GetValue(TitleAreaProperty); }
            set { SetValue(TitleAreaProperty, value); }
        }
        public static readonly DependencyProperty TitleAreaProperty =
            DependencyProperty.Register("TitleArea", typeof(object),
                typeof(YT_Window), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #region AutoHideTitleBar
        /// <summary>
        /// 标题栏是否自动隐藏
        /// </summary>
        public bool AutoHideTitleBar {
            get { return (bool)GetValue(AutoHideTitleBarProperty); }
            set { SetValue(AutoHideTitleBarProperty, value); }
        }
        public static readonly DependencyProperty AutoHideTitleBarProperty =
            DependencyProperty.Register("AutoHideTitleBar", typeof(bool),
                typeof(YT_Window), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #region TitleArea
        public double TitleHeight {
            get { return (double)GetValue(TitleHeightProperty); }
            set { SetValue(TitleHeightProperty, value); }
        }
        public static readonly DependencyProperty TitleHeightProperty =
            DependencyProperty.Register("TitleHeight", typeof(double),
                typeof(YT_Window), new FrameworkPropertyMetadata(24.0, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #region DragMode
        public DragMode DragingMode {
            get { return (DragMode)GetValue(DragingModeProperty); }
            set { SetValue(DragingModeProperty, value); }
        }
        public static readonly DependencyProperty DragingModeProperty =
            DependencyProperty.Register("DragingMode", typeof(DragMode),
                typeof(YT_Window), new FrameworkPropertyMetadata(DragMode.TitleBar, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #region AllowAreaIcon
        /// <summary>
        /// 是否允许托盘图标
        /// </summary>
        public bool AllowAreaIcon {
            get { return (bool)GetValue(AllowAreaIconProperty); }
            set { SetValue(AllowAreaIconProperty, value); }
        }
        public static readonly DependencyProperty AllowAreaIconProperty =
            DependencyProperty.Register("AllowAreaIcon", typeof(bool),
                typeof(YT_Window), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits,
                    AllowAreaIconChanged));
        private static void AllowAreaIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if ((bool)e.NewValue)
                ((YT_Window)d).AreaIcons = new Dictionary<string, YT_AreaIcon>();
            else
                ((YT_Window)d).AreaIcons = null;
        }
        #endregion

        #region override
        protected override void OnLocationChanged(EventArgs e) {

        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {

        }

        protected override void OnInitialized(EventArgs e) {
            SourceInitialized += (sender, args) => {
                (PresentationSource.FromVisual((Visual)sender) as HwndSource).AddHook(new HwndSourceHook(WndProc));
            };
            base.OnInitialized(e);
            //if (EnableAeroGlass)
            //    BlurEffect.EnableBlur(HandleHelper.GetVisualHandle(this), AccentState.ACCENT_ENABLE_BLURBEHIND);
        }

        public override void OnApplyTemplate() {
            _titlebar = GetTemplateChild("YT_TitleBar") as YT_TitleBar;
            base.OnApplyTemplate();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            if (e.LeftButton.Equals(MouseButtonState.Pressed) && DragingMode.Equals(DragMode.FullWindow)) {
                DragMove();
            }

            base.OnMouseLeftButtonDown(e);
        }

        private void YT_Window_Loaded(object sender, RoutedEventArgs e) {
            if (AutoHideTitleBar)
                _titlebar.HideTitleBar();
        }
        #endregion

        #region Method
        /// <summary>
        /// 申请托盘图标，通过键值访问
        /// </summary>
        /// <param name="holder">键值</param>
        public void RegisterAreaIcon(string holder) {
            if (AllowAreaIcon && !AreaIcons.ContainsKey(holder)) {
                AreaIcons.Add(holder, new YT_AreaIcon());
                AreaIcons[holder].AttachedWindow = this;
                AreaIcons[holder].HolderName = holder;
            }
            else
                throw new ArgumentException("Icon not allowed");
        }

        /// <summary>
        /// 注销键值所对应的托盘图标
        /// </summary>
        /// <param name="holder"></param>
        public void DisposAreaIcon(string holder) {
            if (AllowAreaIcon && AreaIcons.ContainsKey(holder)) {
                AreaIcons[holder].Areaicon = null;
                AreaIcons[holder] = null;
                AreaIcons.Remove(holder);
            }
            else
                throw new ArgumentException("No Such Icon");
        }

        /// <summary>
        /// 启用毛玻璃
        /// </summary>
        protected void ActiveBlur(bool flag) {
            if (flag) {
                if (!IsInitialized)
                    SourceInitialized += YT_Window_SourceInitialized;
                else
                    YT_Window_SourceInitialized(null, null);
            }
            else
                BlurEffect.EnableBlur(HandleHelper.GetVisualHandle(this), AccentState.ACCENT_DISABLED);
        }

        private void YT_Window_SourceInitialized(object sender, EventArgs e) {
            BlurEffect.EnableBlur(HandleHelper.GetVisualHandle(this), AccentState.ACCENT_ENABLE_BLURBEHIND);
        }

        /// <summary>
        /// windows消息循环
        /// </summary>
        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {
            return IntPtr.Zero;
        }
        #endregion

        #region Constructor
        public YT_Window() : base() {
            Loaded += YT_Window_Loaded;
        }

        static YT_Window() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_Window), new FrameworkPropertyMetadata(typeof(YT_Window)));
        }
        #endregion
    }
}