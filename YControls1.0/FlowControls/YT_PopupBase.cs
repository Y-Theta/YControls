using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using YControls.AreaIconWindow;
using YControls.Command;
using YControls.InterAction;
using YControls.WinAPI;

namespace YControls.FlowControls {
    public class YT_PopupBase : Popup {
        #region Properties

        /// <summary>
        /// 自动淡出计时器
        /// </summary>
        private Timer _autohide { get; set; }

        /// <summary>
        /// popup所在窗口，为了窗口移动时能够无闪烁移动此popup
        /// </summary>
        private Window _holder { get; set; }

        /// <summary>
        /// 指示popup是否获取默认的弹出方式
        /// </summary>
        private bool _update { get; set; }

        /// <summary>
        /// 关闭popup
        /// </summary>
        public CommandBase CloseCommand { get; set; }

        /// <summary>
        /// 自定义位置时的偏移矩形
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
        /// 
        /// </summary>
        public bool EnableBlur {
            get { return (bool)GetValue(EnableBlurProperty); }
            set { SetValue(EnableBlurProperty, value); }
        }
        public static readonly DependencyProperty EnableBlurProperty =
            DependencyProperty.Register("EnableBlur", typeof(bool),
                typeof(YT_PopupBase), new FrameworkPropertyMetadata(false,
                    FrameworkPropertyMetadataOptions.Inherits));

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

        /// <summary>
        /// 重写属性加入事件触发
        /// </summary>
        public new bool IsOpen {
            get => base.IsOpen;
            set {
                if (value)
                    OnOpen();
                else
                    OnClosed();
            }
        }
        #endregion

        #region Methods
        protected virtual void OnClosed() {
            if (AutoHide)
                _autohide.Enabled = false;
            base.IsOpen = false;
        }

        protected virtual void OnOpen() {
            //若此弹出框还存在，则不再弹出
            if (base.IsOpen)
                return;
            if (_update)
                OnPlacementTargetChanged();
            if (AutoHide)
                _autohide.Enabled = true;
            base.IsOpen = true;
        }

        protected override void OnOpened(EventArgs e) {
            base.OnOpened(e);
            UpdateZlayer();
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
        /// 无闪烁跟随窗口移动
        /// </summary>
        private void UpdateLocation() {
            if (IsOpen) {
                typeof(Popup).GetMethod("UpdatePosition",
                  System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(this, null);
            }
        }

        /// <summary>
        /// 刷新叠放次序
        /// </summary>
        private void UpdateZlayer() {
            if (EnableBlur)
                BlurEffect.SetBlur(HandleHelper.GetVisualHandle(Child), DllImportMethods.AccentState.ACCENT_ENABLE_BLURBEHIND);
            else
                BlurEffect.SetBlur(HandleHelper.GetVisualHandle(Child), DllImportMethods.AccentState.ACCENT_DISABLED);
            var hwnd = HandleHelper.GetVisualHandle(Child);
            if (DllImportMethods.GetWindowRect(hwnd, out DllImportMethods.RECT rect)) {
                DllImportMethods.SetWindowPos(hwnd, TopMost ? -1 : -2, rect.Left, rect.Top, (int)Width, (int)Height, 0);
            }
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
            _holder = GetRootWindow();
            if (_holder is null)
                RootWorkArea();
            else {
                if (!(PlacementTarget is null)) {
                    Placement = PlacementMode.RelativePoint;
                    ComputeOffset();
                }
                _holder.LocationChanged += (s, e) => UpdateLocation();
                _holder.SizeChanged += (s, e) => UpdateLocation();
                _holder.IsVisibleChanged += _holder_IsVisibleChanged;
            }
            _update = false;
        }

        /// <summary>
        /// popup在windows上弹出
        /// </summary>
        private void RootWorkArea() {
            Placement = PlacementMode.AbsolutePoint;
            ComputeOffset();
        }

        /// <summary>
        /// 
        /// </summary>
        private void _holder_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            if ((bool)e.NewValue)
                Placement = PlacementMode.RelativePoint;
            else
                Placement = PlacementMode.AbsolutePoint;
            ComputeOffset();
        }

        /// <summary>
        /// 计算popup的实际弹出位置
        /// </summary>
        private void ComputeOffset() {
            if (Child != null) {
                if (!Child.IsMeasureValid)
                    Child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                //计算popup内容的尺寸
                if (Placement == PlacementMode.AbsolutePoint) {
                    HorizontalOffset = 0;
                    VerticalOffset = 0;
                    PlacementRectangle = new Rect(SystemParameters.WorkArea.Width - Child.DesiredSize.Width - RelativeRect.X,
                        SystemParameters.WorkArea.Height - Child.DesiredSize.Height - RelativeRect.Y, 0, 0);
                }
                else if (Placement == PlacementMode.RelativePoint) {
                    if (PlacementTarget != null)
                        switch (RelativeMode) {
                            case PopupRelativeMode.LeftTop:
                                PlacementRectangle = new Rect(RelativeRect.X, RelativeRect.Y, 0, 0);
                                break;
                            case PopupRelativeMode.LeftBottom:
                                PlacementRectangle = new Rect(RelativeRect.X, PlacementTarget.RenderSize.Height - Child.DesiredSize.Height - RelativeRect.Y, 0, 0);
                                break;
                            case PopupRelativeMode.RightTop:
                                PlacementRectangle = new Rect(PlacementTarget.RenderSize.Width - Child.DesiredSize.Width - RelativeRect.X, RelativeRect.Y, 0, 0);
                                break;
                            case PopupRelativeMode.RightBottom:
                                PlacementRectangle = new Rect(PlacementTarget.RenderSize.Width - Child.DesiredSize.Width - RelativeRect.X,
                                    PlacementTarget.RenderSize.Height - Child.DesiredSize.Height - RelativeRect.Y, 0, 0);
                                break;
                        }
                }
                //若popup正在呈现，则更新其位置
                if (IsOpen)
                    UpdateLocation();
            }
        }

        /// <summary>
        /// 自定义偏移位置回调函数
        /// </summary>
        private CustomPopupPlacement[] Location(Size popupSize, Size targetSize, Point offset) {
            CustomPopupPlacement placement1 =
                new CustomPopupPlacement(new Point(RelativeRect.X, targetSize.Height + RelativeRect.Y), PopupPrimaryAxis.Vertical);
            CustomPopupPlacement placement2 =
                new CustomPopupPlacement(new Point(0, 0), PopupPrimaryAxis.Horizontal);
            CustomPopupPlacement[] ttplaces =
                new CustomPopupPlacement[] { placement1, placement2 };
            return ttplaces;
        }

        /// <summary>
        /// 
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
