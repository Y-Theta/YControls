using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using YControls.SlideControls;

namespace YControls.WinXStyleControls {

    /// <summary>
    /// Win10 风格进度条
    /// 因为WPF不支持在模板中绑定动画属性
    /// 所以直线型进度条要在后台绑定 需要一系列后台属性
    /// </summary>
    [TemplatePart(Name = "ValuePanel", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "BackPanel", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "E(x)Frame(x)", Type = typeof(IKeyFrame))]
    [TemplateVisualState(GroupName = "PB_IndeterminateState", Name = "Stop")]
    public sealed class YT_WinXProgressBar : YT_ProgressBarBase {
        #region Properties

        #region AnimationAbout
        private SplineDoubleKeyFrame _e1frame1;
        private EasingDoubleKeyFrame _e1frame2;
        private SplineDoubleKeyFrame _e1frame3;

        private SplineDoubleKeyFrame _e2frame1;
        private EasingDoubleKeyFrame _e2frame2;
        private SplineDoubleKeyFrame _e2frame3;

        private SplineDoubleKeyFrame _e3frame1;
        private EasingDoubleKeyFrame _e3frame2;
        private SplineDoubleKeyFrame _e3frame3;

        private SplineDoubleKeyFrame _e4frame1;
        private EasingDoubleKeyFrame _e4frame2;
        private SplineDoubleKeyFrame _e4frame3;

        private SplineDoubleKeyFrame _e5frame1;
        private EasingDoubleKeyFrame _e5frame2;
        private SplineDoubleKeyFrame _e5frame3;

        private DoubleAnimation _panelmove;

        private double _wellPosition;

        private double _endPosition;
        #endregion  
        /// <summary>
        /// 百分比路径
        /// </summary>
        private FrameworkElement _valuePanel { get; set; }

        /// <summary>
        /// 背景路径
        /// </summary>
        private FrameworkElement _backPanel { get; set; }

        /// <summary>
        /// 圆形进度条的点半径
        /// </summary>
        public double DotSize {
            get { return (double)GetValue(DotSizeProperty); }
            set { SetValue(DotSizeProperty, value); }
        }
        public static readonly DependencyProperty DotSizeProperty =
            DependencyProperty.Register("DotSize", typeof(double),
                typeof(YT_WinXProgressBar), new PropertyMetadata(6.0));

        /// <summary>
        /// 圆形进度条的尺寸
        /// </summary>
        public Size ViewPortSize {
            get { return (Size)GetValue(ViewPortSizeProperty); }
            set { SetValue(ViewPortSizeProperty, value); }
        }
        public static readonly DependencyProperty ViewPortSizeProperty =
            DependencyProperty.Register("ViewPortSize", typeof(Size),
                typeof(YT_WinXProgressBar), new PropertyMetadata(new Size(80, 80)));

        /// <summary>
        /// 条形进度条点间距
        /// </summary>
        public double RectDotSpan {
            get { return (double)GetValue(RectDotSpanProperty); }
            set { SetValue(RectDotSpanProperty, value); }
        }
        public static readonly DependencyProperty RectDotSpanProperty =
            DependencyProperty.Register("RectDotSpan", typeof(double),
                typeof(YT_WinXProgressBar), new PropertyMetadata(6.0));

        /// <summary>
        /// 圆角风格
        /// </summary>
        public double RoundCap {
            get { return (double)GetValue(RoundCapProperty); }
            set { SetValue(RoundCapProperty, value); }
        }
        public static readonly DependencyProperty RoundCapProperty =
            DependencyProperty.Register("RoundCap", typeof(double),
                typeof(YT_WinXProgressBar), new PropertyMetadata(0.0, OnRectFeatureChanged));

        /// <summary>
        /// value在track中的间距
        /// </summary>
        public double ValuePadding {
            get { return (double)GetValue(ValuePaddingProperty); }
            set { SetValue(ValuePaddingProperty, value); }
        }
        public static readonly DependencyProperty ValuePaddingProperty =
            DependencyProperty.Register("ValuePadding", typeof(double),
                typeof(YT_WinXProgressBar), new PropertyMetadata(0.0, OnRectFeatureChanged));
        private static void OnRectFeatureChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((YT_WinXProgressBar)d).DrawValue();
        }

        #endregion

        #region Methods
        /// <summary>
        /// 值改变时重新绘制
        /// </summary>
        protected override void OnValueChanged(double oldValue, double newValue) {
            base.OnValueChanged(oldValue, newValue);
            DrawValue();
        }

        /// <summary>
        /// 绘制进度
        /// </summary>
        private void DrawValue() {
            if (_valuePanel is Path)
                DrawRing();
            else if (_valuePanel is Rectangle)
                DrawRect();
        }

        /// <summary>
        /// 绘制方形进度条进度
        /// </summary>
        private void DrawRect() {
            ((Rectangle)_valuePanel).Width = ActualWidth * ValuePercent - 2 * ValuePadding > 0 ? ActualWidth * ValuePercent - 2 * ValuePadding : 0;
            ((Rectangle)_valuePanel).Height = ActualHeight - 2 * ValuePadding > 0 ? ActualHeight - 2 * ValuePadding : 0;
            ((Rectangle)_valuePanel).Margin = new Thickness(ValuePadding, ValuePadding, 0, 0);
            ((Rectangle)_valuePanel).RadiusX = RoundCap;
            ((Rectangle)_valuePanel).RadiusY = RoundCap;
            ((Rectangle)_backPanel).Width = ActualWidth;
            ((Rectangle)_backPanel).Height = ActualHeight;
            ((Rectangle)_backPanel).RadiusX = RoundCap;
            ((Rectangle)_backPanel).RadiusY = RoundCap;
        }

        /// <summary>
        /// 绘制环形进度条进度
        /// </summary>
        private void DrawRing() {
            int vs = (int)(ViewPortSize.Width > ViewPortSize.Height ? ViewPortSize.Height : ViewPortSize.Width);
            int r = (int)((vs - DotSize) / 1.414);
            ((Path)_valuePanel).StrokeThickness = ((Path)_backPanel).StrokeThickness - ValuePadding;
            ((Path)_valuePanel).Data = Geometry.Parse(PercentToCircle(ValuePercent, vs, r));
            ((Path)_backPanel).Data = Geometry.Parse(PercentToCircle(1, vs, r));
        }

        /// <summary>
        /// 将百分比转换为圆形路径
        /// </summary>
        /// <param name="a">百分比 小数表示</param>
        /// <param name="cs">区域大小</param>
        /// <param name="r">半径</param>
        /// <returns></returns>
        public static string PercentToCircle(double a, int cs, int r) {
            string R = r.ToString();
            double center = cs / 2;
            string Center = center.ToString();
            string Gap = ((int)((cs - 2 * r) / 2)).ToString();
            var A = a * Math.PI * 2;
            var x = r * Math.Sin(A);
            var y = r * Math.Cos(A);
            x = center + x;
            y = center - y;
            if (a < 0.50)
                return "M " + Center + "," + Gap + " A " + R + "," + R + ",0,0,1," + x.ToString() + "," + y.ToString();
            else if (a == 1)
                return "M " + Center + "," + Gap + " A " + R + "," + R + ",0,1,1," + string.Format("{0:F2}", center - 0.1) + "," + y.ToString();
            else
                return "M " + Center + "," + Gap + " A " + R + "," + R + ",0,1,1," + x.ToString() + "," + y.ToString();
        }

        /// <summary>
        /// 在状态改变时即时绘制百分比
        /// </summary>
        protected override void UpdateVisualState() {
            if (_e1frame1 != null)
                SetRectIndeterminAnimate();
            base.UpdateVisualState();
            if (!IsIndeterminate)
                DrawValue();
        }

        /// <summary>
        /// 尺寸改变时同步改变
        /// </summary>
        private void YT_ProgressBarBase_SizeChanged(object sender, SizeChangedEventArgs e) {
            _wellPosition = e.NewSize.Width / 3;
            _endPosition = 2 * _wellPosition;
            DrawValue();
            if (IsIndeterminate && _valuePanel is Rectangle)
                SetRectIndeterminAnimate();
        }

        /// <summary>
        /// 具体设置条形进度条的动画属性
        /// </summary>
        private void SetRectIndeterminAnimate() {

            VisualStateManager.GoToState(this, "Stop", true);

            _e1frame1.Value = _wellPosition;
            _e1frame2.Value = _wellPosition;
            _e1frame3.Value = _endPosition;

            _e2frame1.Value = _wellPosition;
            _e2frame2.Value = _wellPosition;
            _e2frame3.Value = _endPosition;

            _e3frame1.Value = _wellPosition;
            _e3frame2.Value = _wellPosition;
            _e3frame3.Value = _endPosition;

            _e4frame1.Value = _wellPosition;
            _e4frame2.Value = _wellPosition;
            _e4frame3.Value = _endPosition;

            _e5frame1.Value = _wellPosition;
            _e5frame2.Value = _wellPosition;
            _e5frame3.Value = _endPosition;

            _panelmove.From = -_wellPosition / 2;
            _panelmove.To = _endPosition / 1.5;

            VisualStateManager.GoToState(this, "UnKnown", true);
        }

        /// <summary>
        /// 获取模板控件
        /// </summary>
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            _valuePanel = GetTemplateChild("ValuePanel") as Path;
            _backPanel = GetTemplateChild("BackPanel") as Path;
            if (_valuePanel is null) {
                _valuePanel = GetTemplateChild("ValuePanel") as Rectangle;
                _backPanel = GetTemplateChild("BackPanel") as Rectangle;

                _e1frame1 = GetTemplateChild("E1Frame1") as SplineDoubleKeyFrame;
                _e1frame2 = GetTemplateChild("E1Frame2") as EasingDoubleKeyFrame;
                _e1frame3 = GetTemplateChild("E1Frame3") as SplineDoubleKeyFrame;

                _e2frame1 = GetTemplateChild("E2Frame1") as SplineDoubleKeyFrame;
                _e2frame2 = GetTemplateChild("E2Frame2") as EasingDoubleKeyFrame;
                _e2frame3 = GetTemplateChild("E2Frame3") as SplineDoubleKeyFrame;

                _e3frame1 = GetTemplateChild("E3Frame1") as SplineDoubleKeyFrame;
                _e3frame2 = GetTemplateChild("E3Frame2") as EasingDoubleKeyFrame;
                _e3frame3 = GetTemplateChild("E3Frame3") as SplineDoubleKeyFrame;

                _e4frame1 = GetTemplateChild("E4Frame1") as SplineDoubleKeyFrame;
                _e4frame2 = GetTemplateChild("E4Frame2") as EasingDoubleKeyFrame;
                _e4frame3 = GetTemplateChild("E4Frame3") as SplineDoubleKeyFrame;

                _e5frame1 = GetTemplateChild("E5Frame1") as SplineDoubleKeyFrame;
                _e5frame2 = GetTemplateChild("E5Frame2") as EasingDoubleKeyFrame;
                _e5frame3 = GetTemplateChild("E5Frame3") as SplineDoubleKeyFrame;

                _panelmove = GetTemplateChild("PanelMove") as DoubleAnimation;
            }
        }
        #endregion

        #region Constructors
        public YT_WinXProgressBar() {
            SizeChanged += YT_ProgressBarBase_SizeChanged;
        }

        static YT_WinXProgressBar() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_WinXProgressBar), new FrameworkPropertyMetadata(typeof(YT_WinXProgressBar)));
        }
        #endregion
    }

}
