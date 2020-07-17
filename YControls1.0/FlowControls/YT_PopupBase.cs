using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using YControls.AreaIconWindow;
using YControls.Command;
using YControls.InterAction;
using YControls.WinAPI;

namespace YControls.FlowControls {
    public class YT_PopupBase : Popup {
        #region Properties
        /// <summary>
        /// 子控件窗口句柄
        /// </summary>
        private IntPtr _childhwnd;
        private const int WM_ACTIVATE = 0x0006;
        private const int WM_WINDOWPOSCHANGED = 0x0047;

        private bool _topmostparent;
        private bool _passiveActive;

        /// <summary>
        /// 自动淡出计时器
        /// </summary>
        private Timer _autohide { get; set; }

        /// <summary>
        /// popup所在窗口，为了窗口移动时能够无闪烁移动此popup
        /// </summary>
        private Window _holder { get; set; }

        /// <summary>
        /// 指示popup是需要刷新位置
        /// </summary>
        private bool _update { get; set; }

        /// <summary>
        /// 初始化时设置Blur效果需在控件可视后设置
        /// </summary>
        private Action _firstEnableBlur;

        /// <summary>
        /// 关闭popup
        /// </summary>
        public CommandBase CloseCommand { get; set; }

        /// <summary>
        /// 所有的消息类YT_PopupBase实例
        /// </summary>
        private static List<YT_PopupBase> _msgPopups { get; set; }

        /// <summary>
        /// 使用Popup相对于桌面时的偏移矩形
        /// </summary>
        [TypeConverter(typeof(PointConverter))]
        public Point RelativeRect {
            get { return (Point)GetValue(RelativeRectProperty); }
            set { SetValue(RelativeRectProperty, value); }
        }
        public static readonly DependencyProperty RelativeRectProperty =
            DependencyProperty.Register("RelativeRect", typeof(Point),
                typeof(YT_PopupBase), new FrameworkPropertyMetadata(new Point(0, 0),
                    FrameworkPropertyMetadataOptions.Inherits,
                    OnRelativeRectChanged));

        /// <summary>
        /// 使用Popup相对于控件时的偏移矩形
        /// </summary>
        [TypeConverter(typeof(PointConverter))]
        public Point RelativeRectToTarget {
            get { return (Point)GetValue(RelativeRectToTargetProperty); }
            set { SetValue(RelativeRectToTargetProperty, value); }
        }
        public static readonly DependencyProperty RelativeRectToTargetProperty =
            DependencyProperty.Register("RelativeRectToTarget", typeof(Point),
                typeof(YT_PopupBase), new FrameworkPropertyMetadata(new Point(0, 0),
                    FrameworkPropertyMetadataOptions.Inherits,
                    OnRelativeRectChanged));
        private static void OnRelativeRectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((YT_PopupBase)d).ComputeOffset();
        }
        /// <summary>
        /// popup相对于父元素的弹出位置
        /// </summary>
        public PopupRelativeMode RelativeMode {
            get { return (PopupRelativeMode)GetValue(RelativeModeProperty); }
            set { SetValue(RelativeModeProperty, value); }
        }
        public static readonly DependencyProperty RelativeModeProperty =
            DependencyProperty.Register("RelativeMode", typeof(PopupRelativeMode),
                typeof(YT_PopupBase), new FrameworkPropertyMetadata(PopupRelativeMode.LeftTop,
                    FrameworkPropertyMetadataOptions.Inherits,
                    OnRelativeModeChanged));
        private static void OnRelativeModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((YT_PopupBase)d).ComputeOffset();
        }

        /// <summary>
        /// 自动隐藏
        /// </summary>
        public bool AutoHide {
            get { return (bool)GetValue(AutoHideProperty); }
            set { SetValue(AutoHideProperty, value); }
        }
        public static readonly DependencyProperty AutoHideProperty =
            DependencyProperty.Register("AutoHide", typeof(bool),
                typeof(YT_PopupBase), new FrameworkPropertyMetadata(false,
                    FrameworkPropertyMetadataOptions.Inherits,
                    OnAutoHideChanged));
        private static void OnAutoHideChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if ((bool)e.NewValue)
                ((YT_PopupBase)d).InitAutoHide();
            else
                ((YT_PopupBase)d).DisposeAutoHide();
        }

        /// <summary>
        /// 自动隐藏延时 ms
        /// </summary>
        public int AutoHideDelay {
            get { return (int)GetValue(AutoHideDelayProperty); }
            set { SetValue(AutoHideDelayProperty, value); }
        }
        public static readonly DependencyProperty AutoHideDelayProperty =
            DependencyProperty.Register("AutoHideDelay", typeof(int),
                typeof(YT_PopupBase), new FrameworkPropertyMetadata(0,
                    FrameworkPropertyMetadataOptions.Inherits,
                    OnAutoHideDelayChanged));
        private static void OnAutoHideDelayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (((YT_PopupBase)d).AutoHide)
                ((YT_PopupBase)d)._autohide.Interval = (int)e.NewValue;
        }

        /// <summary>
        /// 是否至于最前显示
        /// </summary>
        public bool TopMost {
            get { return (bool)GetValue(TopMostProperty); }
            set { SetValue(TopMostProperty, value); }
        }
        public static readonly DependencyProperty TopMostProperty =
            DependencyProperty.Register("TopMost", typeof(bool),
                typeof(YT_PopupBase), new FrameworkPropertyMetadata(false,
                    FrameworkPropertyMetadataOptions.Inherits,
                    OnTopMostChanged));
        private static void OnTopMostChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((YT_PopupBase)d).UpdateZlayer();
        }

        /// <summary>
        /// 指示这个Popup是否需要在主界面隐藏时继续显示
        /// </summary>
        public bool Message {
            get { return (bool)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(bool),
                typeof(YT_PopupBase), new FrameworkPropertyMetadata(false,
                    FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 启用毛玻璃效果
        /// </summary>
        public bool EnableBlur {
            get { return (bool)GetValue(EnableBlurProperty); }
            set { SetValue(EnableBlurProperty, value); }
        }
        public static readonly DependencyProperty EnableBlurProperty =
            DependencyProperty.Register("EnableBlur", typeof(bool),
                typeof(YT_PopupBase), new FrameworkPropertyMetadata(false,
                    FrameworkPropertyMetadataOptions.Inherits, OnEnableBlurChanged));
        private static void OnEnableBlurChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((YT_PopupBase)d).SetBlur((bool)e.NewValue);
        }

        /// <summary>
        /// 依赖对象改变事件
        /// </summary>
        public new UIElement PlacementTarget {
            get => base.PlacementTarget;
            set {
                base.PlacementTarget = value;
                OnPlacementTargetChanged();
            }
        }

        #endregion

        #region Methods
        protected override void OnClosed(EventArgs e) {
            if (AutoHide)
                _autohide.Enabled = false;
            base.OnClosed(e);
        }

        protected override void OnOpened(EventArgs e) {
            if (_update)
                OnPlacementTargetChanged();
            base.OnOpened(e);
            UpdateZlayer();
            if (AutoHide)
                _autohide.Enabled = true;
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            if (AutoHide)
                _autohide.Enabled = false;
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e) {
            if (AutoHide)
                _autohide.Enabled = true;
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// 刷新叠放次序
        /// </summary>
        private void UpdateZlayer() {
            if (Child is null)
                return;
            _firstEnableBlur?.Invoke();
            //刷新排放位置
            _childhwnd = HandleHelper.GetVisualHandle(Child);
            SetTopMostParent(true);
            if (_holder != null && _holder.IsVisible && Message) {
                _holder.Activate();
            }
        }

        /// <summary>
        /// 设置毛玻璃
        /// </summary>
        private void SetBlur(bool enable) {
            if (Child is null) {
                if (enable)
                    _firstEnableBlur = FirstEnableBlur;
                return;
            }
            //是否启用Aero玻璃效果
            if (enable)
                BlurEffect.SetBlur(HandleHelper.GetVisualHandle(Child), DllImportMethods.AccentState.ACCENT_ENABLE_BLURBEHIND);
            else
                BlurEffect.SetBlur(HandleHelper.GetVisualHandle(Child), DllImportMethods.AccentState.ACCENT_DISABLED);
        }

        /// <summary>
        /// 初始化开启Blur
        /// </summary>
        private void FirstEnableBlur() {
            BlurEffect.SetBlur(HandleHelper.GetVisualHandle(Child), DllImportMethods.AccentState.ACCENT_ENABLE_BLURBEHIND);
            _firstEnableBlur = null;
        }

        /// <summary>
        /// 获取popup所属窗口
        /// </summary>
        private Window GetRootWindow() {
            DependencyObject root = PlacementTarget;
            while (!(root is Window)) {
                if (root is null)
                    return null;
                root = VisualTreeHelper.GetParent(root);
            }
            return root as Window;
        }

        /// <summary>
        /// 在父控件改变时重新设置弹出控件的弹出方式
        /// </summary>
        private void OnPlacementTargetChanged() {
            if (Child is null)
                return;
            //当popup的直接对象不是窗体时,则当对象可见时才会显示Popup
            if (PlacementTarget != null && !(PlacementTarget is Window) && !PlacementTarget.IsVisible)
                return;
            _holder = GetRootWindow();
            if (_holder is null) {
                RootWorkArea();
            }
            else {
                if (!(PlacementTarget is null)) {
                    Placement = PlacementMode.RelativePoint;
                    //当Popup的直接对象是窗口时，若窗口隐藏，则使用全局布局
                    if (PlacementTarget is Window wnd && !wnd.IsVisible) {
                        Placement = PlacementMode.AbsolutePoint;
                    }
                    ComputeOffset();
                }
                RemoveAttachWindowHook();
                AddAttachWindowHook();
            }
        }

        /// <summary>
        /// 关联窗体事件
        /// </summary>
        private void AddAttachWindowHook() {
            _holder.Activated += YT_PopupBase_Activated;
            _holder.Deactivated += YT_PopupBase_Deactivated;
            _holder.LocationChanged += (s, e) => UpdateLocation();
            _holder.SizeChanged += YT_PopupBase_SizeChanged;
            _holder.IsVisibleChanged += _holder_IsVisibleChanged;
            ((FrameworkElement)Child).SizeChanged += YT_PopupBase_SizeChanged;
            PlacementTarget.IsVisibleChanged += PlacementTarget_IsVisibleChanged;
        }

        /// <summary>
        /// 取消关联窗体事件
        /// </summary>
        private void RemoveAttachWindowHook() {
            _holder.Activated -= YT_PopupBase_Activated;
            _holder.Deactivated -= YT_PopupBase_Deactivated;
            _holder.LocationChanged -= (s, e) => UpdateLocation();
            _holder.SizeChanged -= YT_PopupBase_SizeChanged;
            _holder.IsVisibleChanged -= _holder_IsVisibleChanged;
            ((FrameworkElement)Child).SizeChanged -= YT_PopupBase_SizeChanged;
            PlacementTarget.IsVisibleChanged -= PlacementTarget_IsVisibleChanged;
        }

        private void YT_PopupBase_Deactivated(object sender, EventArgs e) {
            if (_topmostparent) {
                if (_passiveActive) {
                    _passiveActive = false;
                    return;
                }
                SetTopMostParent(false);
            }
        }

        private void YT_PopupBase_Activated(object sender, EventArgs e) {
            if (!_topmostparent && !TopMost) {
                _passiveActive = true;
                SetTopMostParent(true);
            }
        }

        /// <summary>
        /// 设置置顶于父窗体
        /// </summary>
        private void SetTopMostParent(bool topmost) {
            if (DllImportMethods.GetWindowRect(_childhwnd, out DllImportMethods.RECT rect)) {
                _topmostparent = topmost;
                if (topmost) {
                    DllImportMethods.SetWindowPos(_childhwnd, -1, rect.Left, rect.Top, (int)Width, (int)Height, 0);
                }
                else {
                    DllImportMethods.SetWindowPos(_childhwnd, 1, rect.Left, rect.Top, (int)Width, (int)Height, 0);
                    DllImportMethods.SetWindowPos(_childhwnd, 0, rect.Left, rect.Top, (int)Width, (int)Height, 0);
                    DllImportMethods.SetWindowPos(_childhwnd, -2, rect.Left, rect.Top, (int)Width, (int)Height, 0);
                }
            }
        }

        /// <summary>
        /// 无闪烁跟随窗口移动
        /// </summary>
        private void UpdateLocation() {
            if (IsOpen) {
                typeof(Popup).GetMethod("UpdatePosition",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(this, null);
            }
        }

        /// <summary>
        /// 目标窗体的可视性发生改变时
        /// 重新安排消息弹窗位置
        /// </summary>
        private void _holder_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            if (!Message)
                return;
            if ((bool)e.NewValue)
                Placement = PlacementMode.RelativePoint;
            else
                Placement = PlacementMode.AbsolutePoint;
            _update = true;
            ComputeOffset();
        }

        /// <summary>
        /// <para>在以下因素大小变化时重新计算位置并刷新</para>
        /// <para>目标窗体 <see cref="Popup.PlacementTarget"/></para>
        /// <para>内容控件 <see cref="Popup.Child"/></para>
        /// </summary>
        private void YT_PopupBase_SizeChanged(object sender, SizeChangedEventArgs e) {
            _update = true;
        }

        /// <summary>
        /// 目标消失时（换页/控件刷新）跟随消失
        /// </summary>
        private void PlacementTarget_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            if (!(bool)e.NewValue && !Message)
                IsOpen = false;
        }

        /// <summary>
        /// popup在windows上弹出
        /// </summary>
        private void RootWorkArea() {
            Placement = PlacementMode.AbsolutePoint;
            ComputeOffset();
        }

        /// <summary>
        /// 计算popup的实际弹出位置
        /// </summary>
        private void ComputeOffset() {
            if (Child != null && IsOpen) {
                Size infinity = new Size(double.PositiveInfinity, double.PositiveInfinity);
                Size finalsize = new Size();
                if (!Child.IsMeasureValid)
                    Child.Measure(infinity);
                finalsize.Width = Child.DesiredSize.Width;
                finalsize.Height = Child.DesiredSize.Height;
                if (Child.DesiredSize.Width == 0 || !Width.Equals(double.NaN)) {
                    finalsize.Width = Width;
                }
                if (Child.DesiredSize.Height == 0 || !Height.Equals(double.NaN)) {
                    finalsize.Height = Height;
                }
                ArrangePopup(finalsize);
                _update = false;
            }
        }

        /// <summary>
        /// 放置Popup
        /// </summary>
        private void ArrangePopup(Size contentsize) {
            if (Placement == PlacementMode.AbsolutePoint) {
                //使自带的参数无效
                HorizontalOffset = 0;
                VerticalOffset = 0;
                PlacementRectangle = new Rect(SystemParameters.WorkArea.Width - contentsize.Width - RelativeRect.X,
                    SystemParameters.WorkArea.Height - contentsize.Height - RelativeRect.Y, 0, 0);
            }
            else if (Placement == PlacementMode.RelativePoint) {
                if (PlacementTarget != null) {
                    double x = 0, y = 0;
                    switch (RelativeMode) {
                        case PopupRelativeMode.LeftTop:
                            PlacementRectangle = new Rect(RelativeRectToTarget.X, RelativeRectToTarget.Y, 0, 0);
                            break;
                        case PopupRelativeMode.LeftBottom:
                            y = PlacementTarget.RenderSize.Height - contentsize.Height;
                            PlacementRectangle = new Rect(RelativeRectToTarget.X, y - RelativeRectToTarget.Y, 0, 0);
                            break;
                        case PopupRelativeMode.RightTop:
                            x = PlacementTarget.RenderSize.Width - contentsize.Width;
                            PlacementRectangle = new Rect(x - RelativeRectToTarget.X, RelativeRectToTarget.Y, 0, 0);
                            break;
                        case PopupRelativeMode.RightBottom:
                            x = PlacementTarget.RenderSize.Width - contentsize.Width;
                            y = PlacementTarget.RenderSize.Height - contentsize.Height;
                            PlacementRectangle = new Rect(x - RelativeRectToTarget.X, y - RelativeRectToTarget.Y, 0, 0);
                            break;
                        case PopupRelativeMode.CenterTarget:
                            x = (PlacementTarget.RenderSize.Width - contentsize.Width) / 2;
                            y = (PlacementTarget.RenderSize.Height - contentsize.Height) / 2;
                            PlacementRectangle = new Rect(x + RelativeRectToTarget.X, y + RelativeRectToTarget.Y, 0, 0);
                            break;
                        case PopupRelativeMode.TopTarget:
                            x = (PlacementTarget.RenderSize.Width - contentsize.Width) / 2;
                            PlacementRectangle = new Rect(x + RelativeRectToTarget.X, RelativeRectToTarget.Y, 0, 0);
                            break;
                        case PopupRelativeMode.BottomTarget:
                            x = (PlacementTarget.RenderSize.Width - contentsize.Width) / 2;
                            y = PlacementTarget.RenderSize.Height - contentsize.Height;
                            PlacementRectangle = new Rect(x + RelativeRectToTarget.X, y + RelativeRectToTarget.Y, 0, 0);
                            break;
                    }
                }
            }
            if (IsOpen) {
                UpdateLocation();
                UpdateZlayer();
            }
        }

        /// <summary>
        /// 自定义偏移位置回调函数
        /// </summary>
        private CustomPopupPlacement[] Location(Size popupSize, Size targetSize, Point offset) {
            CustomPopupPlacement placement1 =
                new CustomPopupPlacement(new Point(RelativeRect.X, targetSize.Height + RelativeRect.Y), PopupPrimaryAxis.Vertical);
            CustomPopupPlacement placement2 =
                new CustomPopupPlacement(new Point(RelativeRectToTarget.X, targetSize.Height + RelativeRectToTarget.Y), PopupPrimaryAxis.Horizontal);
            CustomPopupPlacement[] ttplaces =
                new CustomPopupPlacement[] { placement1, placement2 };
            return ttplaces;
        }

        /// <summary>
        /// 自动隐藏
        /// </summary>
        private void _autohide_Elapsed(object sender, ElapsedEventArgs e) {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => {
                IsOpen = false;
                _autohide.Enabled = false;
            }));
        }

        private void InitAutoHide() {
            _autohide = new Timer { Interval = 3000 };
            _autohide.Elapsed += _autohide_Elapsed;
        }

        private void DisposeAutoHide() {
            _autohide = null;
        }

        private void InitRes() {
            _update = true;
            CustomPopupPlacementCallback += Location;
            CloseCommand = new CommandBase(obj => { IsOpen = false; });
            if (AutoHide)
                InitAutoHide();
        }
        #endregion

        #region Constructors
        public YT_PopupBase() {
            InitRes();
        }

        static YT_PopupBase() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_PopupBase), new FrameworkPropertyMetadata(typeof(YT_PopupBase),
                FrameworkPropertyMetadataOptions.Inherits));
        }
        #endregion
    }
}
