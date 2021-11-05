using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using YControlCore.Base;
using static YControlCore.Interop.Core;
using YControlCore.Interop;
using System.Windows.Controls;
using YControlCore.ControlBase;
using YControlCore.ContentControl;

namespace YControlCore.WindowBase {

    /// <summary>
    /// 自定义窗体:
    /// <para> - 暴露出windows消息循环 </para>
    /// <para> - 无边框可拖拽 </para>
    /// <para> - 默认样式中带Titlebar </para>
    /// <para> - 带托盘图标 </para>
    /// </summary>
    [TemplatePart(Name = "PART_TitleBar", Type = typeof(Y_CollapseControl))]
    public class WindowBaseEx : Window {
        #region Properties

        /// <summary>
        /// 与窗体关联的标题栏
        /// </summary>
        protected Y_CollapseControl _titlebar;

        /// <summary>
        /// 窗口句柄
        /// </summary>
        public IntPtr Handle { get; private set; } = IntPtr.Zero;

        /// <summary>
        /// 句柄是否创建
        /// </summary>
        public bool IsHandleCreated { get; private set; } = false;

        /// <summary>
        /// 窗口所拥有的托盘图标
        /// </summary>
        public Dictionary<string, Y_AreaIcon> AreaIcons { get; private set; }

        /// <summary>
        /// 开启毛玻璃
        /// </summary>
        public bool EnableAeroGlass {
            get { return (bool)GetValue(EnableAeroGlassProperty); }
            set { SetValue(EnableAeroGlassProperty, value); }
        }
        public static readonly DependencyProperty EnableAeroGlassProperty =
            DependencyProperty.Register("EnableAeroGlass", typeof(bool),
                typeof(WindowBaseEx), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, OnEnableAeroGlassChanged));
        private static void OnEnableAeroGlassChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((WindowBaseEx)d).ActiveBlur((bool)e.NewValue);
        }

        /// <summary>
        /// 毛玻璃下的背景颜色
        /// </summary>
        public Brush AeroModeBackground {
            get { return (Brush)GetValue(AeroModeBackgroundProperty); }
            set { SetValue(AeroModeBackgroundProperty, value); }
        }
        public static readonly DependencyProperty AeroModeBackgroundProperty =
            DependencyProperty.Register("AeroModeBackground", typeof(Brush),
                typeof(WindowBaseEx), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 毛玻璃下的边框颜色
        /// </summary>
        public Brush AeroModeBorderBrush {
            get { return (Brush)GetValue(AeroModeBorderBrushProperty); }
            set { SetValue(AeroModeBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty AeroModeBorderBrushProperty =
            DependencyProperty.Register("AeroModeBorderBrush", typeof(Brush),
                typeof(WindowBaseEx), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 可拖拽缩放边框的宽度
        /// </summary>
        public Thickness SizeBorderThickness {
            get { return (Thickness)GetValue(SizeBorderThicknessProperty); }
            set { SetValue(SizeBorderThicknessProperty, value); }
        }
        public static readonly DependencyProperty SizeBorderThicknessProperty =
            DependencyProperty.Register("SizeBorderThickness", typeof(Thickness),
                typeof(WindowBaseEx), new FrameworkPropertyMetadata(new Thickness(4), FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 是否将窗口内容扩充至标题栏
        /// </summary>
        public bool ExtendToTitleBar {
            get { return (bool)GetValue(ExtendToTitleBarProperty); }
            set { SetValue(ExtendToTitleBarProperty, value); }
        }
        public static readonly DependencyProperty ExtendToTitleBarProperty =
            DependencyProperty.Register("ExtendToTitleBar", typeof(bool),
                typeof(WindowBaseEx), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 标题区域
        /// </summary>
        public FrameworkElement TitleArea {
            get { return (FrameworkElement)GetValue(TitleAreaProperty); }
            set { SetValue(TitleAreaProperty, value); }
        }
        public static readonly DependencyProperty TitleAreaProperty =
            DependencyProperty.Register("TitleArea", typeof(FrameworkElement),
                typeof(WindowBaseEx), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 标题栏是否自动隐藏
        /// </summary>
        public Float TitleBarMode {
            get { return (Float)GetValue(TitleBarModeProperty); }
            set { SetValue(TitleBarModeProperty, value); }
        }
        public static readonly DependencyProperty TitleBarModeProperty =
            DependencyProperty.Register("TitleBarMode", typeof(Float),
                typeof(WindowBaseEx), new FrameworkPropertyMetadata(Float.Dock, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 标题栏高度 默认24
        /// </summary>
        public double TitleHeight {
            get { return (double)GetValue(TitleHeightProperty); }
            set { SetValue(TitleHeightProperty, value); }
        }
        public static readonly DependencyProperty TitleHeightProperty =
            DependencyProperty.Register("TitleHeight", typeof(double),
                typeof(WindowBaseEx), new FrameworkPropertyMetadata(24.0, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 拖拽模式 默认只能通过标题栏拖动窗口
        /// </summary>
        //public DragMode DragingMode {
        //    get { return (DragMode)GetValue(DragingModeProperty); }
        //    set { SetValue(DragingModeProperty, value); }
        //}
        //public static readonly DependencyProperty DragingModeProperty =
        //    DependencyProperty.Register("DragingMode", typeof(DragMode),
        //        typeof(Y_Window), new FrameworkPropertyMetadata(DragMode.TitleBar, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 是否允许托盘图标
        /// </summary>
        public bool AllowAreaIcon {
            get { return (bool)GetValue(AllowAreaIconProperty); }
            set { SetValue(AllowAreaIconProperty, value); }
        }
        public static readonly DependencyProperty AllowAreaIconProperty =
            DependencyProperty.Register("AllowAreaIcon", typeof(bool),
                typeof(WindowBaseEx), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits,
                    AllowAreaIconChanged));
        private static void AllowAreaIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if ((bool)e.NewValue)
                ((WindowBaseEx)d).AreaIcons = new Dictionary<string, Y_AreaIcon>();
            else
                ((WindowBaseEx)d).AreaIcons = null;
        }

        /// <summary>
        /// 标题栏是否可见
        /// </summary>
        public bool IsTitleVisible {
            get { return (bool)GetValue(IsTitleVisibleProperty); }
            set { SetValue(IsTitleVisibleProperty, value); }
        }
        public static readonly DependencyProperty IsTitleVisibleProperty =
            DependencyProperty.Register("IsTitleVisible", typeof(bool), 
                typeof(WindowBaseEx), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 标题栏是否启用动画
        /// </summary>
        public bool IsTitleAnimate {
            get { return (bool)GetValue(IsTitleAnimateProperty); }
            set { SetValue(IsTitleAnimateProperty, value); }
        }
        public static readonly DependencyProperty IsTitleAnimateProperty =
            DependencyProperty.Register("IsTitleAnimate", typeof(bool), 
                typeof(WindowBaseEx), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 最小化
        /// </summary>
        public static readonly RoutedCommand MiniCommand = new RoutedCommand("Minimize", typeof(WindowBaseEx));

        /// <summary>
        /// 正常
        /// </summary>
        public static readonly RoutedCommand NormalCommand = new RoutedCommand("Normal", typeof(WindowBaseEx));

        /// <summary>
        /// 最大化
        /// </summary>
        public static readonly RoutedCommand MaxCommand = new RoutedCommand("Maxmize", typeof(WindowBaseEx));

        #endregion

        #region override
        protected override void OnInitialized(EventArgs e) {
     
            base.OnInitialized(e);
        }

        //public override void OnApplyTemplate() {
        //    _titlebar = GetTemplateChild("YT_TitleBar") as YT_TitleBar;
        //    base.OnApplyTemplate();
        //}

        //protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
        //    if (e.LeftButton.Equals(MouseButtonState.Pressed) && DragingMode.Equals(DragMode.FullWindow)) {
        //        DragMove();
        //    }

        //    base.OnMouseLeftButtonDown(e);
        //}

        private void Y_Window_Loaded(object sender, RoutedEventArgs e) {
            //if (TitleBarMode == TitleBarMode.AutoHide)
            //    _titlebar.HideTitleBar();

        }
        #endregion

        #region Method

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            _titlebar = GetTemplateChild("PART_TitleBar") as Y_CollapseControl;
        }

        /// <summary>
        /// 申请托盘图标，通过键值访问
        /// </summary>
        /// <param name="holder">键值</param>
        public void RegisterAreaIcon(string holder) {
            if (AllowAreaIcon && !AreaIcons.ContainsKey(holder)) {

            } else
                throw new ArgumentException("Icon not allowed");
        }

        /// <summary>
        /// 注销键值所对应的托盘图标
        /// </summary>
        /// <param name="holder"></param>
        public void DisposAreaIcon(string holder) {
            if (AllowAreaIcon && AreaIcons.ContainsKey(holder)) {

            } else
                throw new ArgumentException("No Such Icon");
        }

        /// <summary>
        /// 启用毛玻璃
        /// </summary>
        protected void ActiveBlur(bool flag) {
            if (flag) {
                if (!IsInitialized)
                    SourceInitialized += Y_Window_SourceInitialized;
                else
                    Y_Window_SourceInitialized(null, null);
            } else
                BlurEffect.EnableBlur(GetVisualHandle(this), AccentState.ACCENT_DISABLED);
        }

        private void Y_Window_SourceInitialized(object sender, EventArgs e) {
            BlurEffect.EnableBlur(GetVisualHandle(this), AccentState.ACCENT_ENABLE_BLURBEHIND);
        }

        private void Y_Window_TryInitSource(object? sender, EventArgs e)
        {
            HwndSource? source = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            if (source != null)
            {
                this.IsHandleCreated = true;
                this.Handle = source.Handle;
                source.AddHook(new HwndSourceHook(WndProc));
            }
        }

        /// <summary>
        /// windows消息循环
        /// </summary>
        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {
            return IntPtr.Zero;
        }

        #region Command process

        private static void OnWindowCommand(object sender, ExecutedRoutedEventArgs e) {
            WindowBaseEx window = (WindowBaseEx)sender;

            if (e.Command == WindowBaseEx.MaxCommand) {
                window.WindowState = WindowState.Maximized;
            } else if (e.Command == NormalCommand) {
                window.WindowState = WindowState.Normal;
            } else {
                window.WindowState = WindowState.Minimized;
            }
        }

        private static void OnQueryWindowCommand(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        #endregion

        #endregion

        #region Constructor


        static WindowBaseEx() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowBaseEx), new FrameworkPropertyMetadata(typeof(WindowBaseEx)));

            MiniCommand.RegisterHandler(typeof(WindowBaseEx), OnWindowCommand, OnQueryWindowCommand);
        }


        public WindowBaseEx() : base() {
            Loaded += Y_Window_Loaded;
            SourceInitialized += Y_Window_TryInitSource;
        }

        #endregion
    }
}
