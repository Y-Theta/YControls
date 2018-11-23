using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace YControls.WinXStyleControls {
    /// <summary>
    /// Win10风格的开关按钮
    /// </summary>
    [TemplatePart(Name = "ToggelBack", Type = typeof(Shape))]
    [TemplatePart(Name = "ToggelThumb", Type = typeof(Shape))]
    [TemplatePart(Name = "ToggelPanel", Type = typeof(Grid))]
    public class YT_WinXToggelSwitch : ToggleButton {
        #region Properties

        private Rectangle _toggelback;
        private Ellipse _toggelthumb;
        private Grid _toggelpanel;

        private TranslateTransform _toggeltranslate;
        private double _mousepoint;
        private double _oripoint;

        private DoubleAnimationUsingKeyFrames _thumboff;
        private DoubleAnimationUsingKeyFrames _thumbon;

        private SplineDoubleKeyFrame _offkey1;
        private SplineDoubleKeyFrame _onkey1;

        private bool _passivecheck;

        /// <summary>
        /// 标签在右边
        /// </summary>
        public bool LabelRight {
            get { return (bool)GetValue(LabelRightProperty); }
            set { SetValue(LabelRightProperty, value); }
        }
        public static readonly DependencyProperty LabelRightProperty =
            DependencyProperty.Register("LabelRight", typeof(bool),
                typeof(YT_WinXToggelSwitch), new FrameworkPropertyMetadata(true));

        /// <summary>
        /// 关闭时的提示
        /// </summary>
        public string OffString {
            get { return (string)GetValue(OffStringProperty); }
            set { SetValue(OffStringProperty, value); }
        }
        public static readonly DependencyProperty OffStringProperty =
            DependencyProperty.Register("OffString", typeof(string),
                typeof(YT_WinXToggelSwitch), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// 打开时的提示
        /// </summary>
        public string OnString {
            get { return (string)GetValue(OnStringProperty); }
            set { SetValue(OnStringProperty, value); }
        }
        public static readonly DependencyProperty OnStringProperty =
            DependencyProperty.Register("OnString", typeof(string),
                typeof(YT_WinXToggelSwitch), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// 按下时前景色
        /// </summary>
        public Brush ForegroundPressed {
            get { return (Brush)GetValue(ForegroundPressedProperty); }
            set { SetValue(ForegroundPressedProperty, value); }
        }
        public static readonly DependencyProperty ForegroundPressedProperty =
            DependencyProperty.Register("ForegroundPressed", typeof(Brush),
                typeof(YT_WinXToggelSwitch), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// 按下时背景色
        /// </summary>
        public Brush BackgroundPressed {
            get { return (Brush)GetValue(BackgroundPressedProperty); }
            set { SetValue(BackgroundPressedProperty, value); }
        }
        public static readonly DependencyProperty BackgroundPressedProperty =
            DependencyProperty.Register("BackgroundPressed", typeof(Brush),
                typeof(YT_WinXToggelSwitch), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// 按钮与边框间隙
        /// </summary>
        public double ThumbPadding {
            get { return (double)GetValue(ThumbPaddingProperty); }
            set { SetValue(ThumbPaddingProperty, value); }
        }
        public static readonly DependencyProperty ThumbPaddingProperty =
            DependencyProperty.Register("ThumbPadding", typeof(double),
                typeof(YT_WinXToggelSwitch), new FrameworkPropertyMetadata(2.0, OnToggelSizeChanged));

        /// <summary>
        /// 文字间距
        /// </summary>
        public Thickness LabelMargin {
            get { return (Thickness)GetValue(LabelMarginProperty); }
            set { SetValue(LabelMarginProperty, value); }
        }
        public static readonly DependencyProperty LabelMarginProperty =
            DependencyProperty.Register("LabelMargin", typeof(Thickness),
                typeof(YT_WinXToggelSwitch), new FrameworkPropertyMetadata(new Thickness(0)));

        /// <summary>
        /// 标签间距
        /// </summary>
        public double LabelWidth {
            get { return (double)GetValue(LabelWidthProperty); }
            set { SetValue(LabelWidthProperty, value); }
        }
        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.Register("LabelWidth", typeof(double),
                typeof(YT_WinXToggelSwitch), new FrameworkPropertyMetadata(24.0));

        /// <summary>
        /// 按钮边框宽度
        /// </summary>
        public double ToggelThickness {
            get { return (double)GetValue(ToggelThicknessProperty); }
            set { SetValue(ToggelThicknessProperty, value); }
        }
        public static readonly DependencyProperty ToggelThicknessProperty =
            DependencyProperty.Register("ToggelThickness", typeof(double),
                typeof(YT_WinXToggelSwitch), new FrameworkPropertyMetadata(2.0, OnToggelSizeChanged));

        /// <summary>
        /// 滑动按钮的宽度
        /// </summary>
        public double ToggelWidth {
            get { return (double)GetValue(ToggelWidthProperty); }
            set { SetValue(ToggelWidthProperty, value); }
        }
        public static readonly DependencyProperty ToggelWidthProperty =
            DependencyProperty.Register("ToggelWidth", typeof(double),
                typeof(YT_WinXToggelSwitch), new FrameworkPropertyMetadata(32.0, OnToggelSizeChanged));

        /// <summary>
        /// 滑动按钮的高度
        /// </summary>
        public double ToggelHeight {
            get { return (double)GetValue(ToggelHeightProperty); }
            set { SetValue(ToggelHeightProperty, value); }
        }
        public static readonly DependencyProperty ToggelHeightProperty =
            DependencyProperty.Register("ToggelHeight", typeof(double),
                typeof(YT_WinXToggelSwitch), new FrameworkPropertyMetadata(16.0, OnToggelSizeChanged));
        #endregion

        #region Methods
        private static void OnToggelSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((YT_WinXToggelSwitch)d).MeasurePartSize();
        }

        protected override void OnChecked(RoutedEventArgs e) {
            base.OnChecked(e);
            if (_passivecheck) {
                _toggeltranslate.BeginAnimation(TranslateTransform.XProperty, _thumbon);
            }
        }

        protected override void OnUnchecked(RoutedEventArgs e) {
            base.OnUnchecked(e);
            if (_passivecheck) {
                _toggeltranslate.BeginAnimation(TranslateTransform.XProperty, _thumboff);
            }
        }

        protected override void OnToggle() {
            //空置,将判断移至鼠标抬起时
        }

        protected override void OnMouseLeave(MouseEventArgs e) {
            base.OnMouseLeave(e);
            _passivecheck = true;
        }

        protected override void OnMouseEnter(MouseEventArgs e) {
            base.OnMouseEnter(e);
            _passivecheck = false;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonDown(e);
            _oripoint = e.GetPosition(_toggelpanel).X;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonUp(e);
            bool? isChecked = true;

            if (_oripoint != _mousepoint) {//若进行过滑动
                if (_toggeltranslate.X > (_toggelpanel.ActualWidth - _toggelthumb.ActualWidth) / 2) {
                    _toggeltranslate.BeginAnimation(TranslateTransform.XProperty, _thumbon);
                    isChecked = true;
                }
                else {
                    _toggeltranslate.BeginAnimation(TranslateTransform.XProperty, _thumboff);
                    isChecked = false;
                }
            }
            else {//仅点击
                if ((bool)IsChecked) {
                    _toggeltranslate.BeginAnimation(TranslateTransform.XProperty, _thumboff);
                    isChecked = false;
                }
                else {
                    _toggeltranslate.BeginAnimation(TranslateTransform.XProperty, _thumbon);
                    isChecked = true;
                }
            }
            SetValue(IsCheckedProperty, isChecked);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);

            if (e.LeftButton == MouseButtonState.Pressed) {
                double offset = e.GetPosition(_toggelpanel).X - _mousepoint;

                if (_toggeltranslate.X + offset >= _toggelback.StrokeThickness + ThumbPadding
                    && _toggeltranslate.X + offset <= _toggelpanel.ActualWidth - _toggelback.StrokeThickness - ThumbPadding - _toggelthumb.Width) {
                    //设置位置
                    _toggeltranslate.X += e.GetPosition(_toggelpanel).X - _mousepoint;
                }
            }
            _mousepoint = e.GetPosition(_toggelpanel).X;
        }

        public override void OnApplyTemplate() {
            _toggelback = GetTemplateChild("ToggelBack") as Rectangle;
            _toggelthumb = GetTemplateChild("ToggelThumb") as Ellipse;
            _toggelpanel = GetTemplateChild("ToggelPanel") as Grid;
            _toggelpanel.SizeChanged += _toggelpanel_SizeChanged;
            _toggeltranslate = GetTemplateChild("ToggelThumbTranslate") as TranslateTransform;
            _thumbon = new DoubleAnimationUsingKeyFrames();
            _onkey1 = new SplineDoubleKeyFrame {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200)),
                KeySpline = new KeySpline(0.2, 0, 1, 0.2),
            };
            _thumbon.KeyFrames.Add(_onkey1);
            _thumbon.Completed += _thumbon_Completed;
            _thumboff = new DoubleAnimationUsingKeyFrames();
            _offkey1 = new SplineDoubleKeyFrame {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200)),
                KeySpline = new KeySpline(0.2, 0, 1, 0.2),
            };
            _thumboff.KeyFrames.Add(_offkey1);
            _thumboff.Completed += _thumboff_Completed;
            _passivecheck = true;
            base.OnApplyTemplate();
        }

        private void _toggelpanel_SizeChanged(object sender, SizeChangedEventArgs e) {
            if (e.NewSize.Width == e.PreviousSize.Width)
                return;
            MeasurePartSize();
        }

        private void MeasurePartSize() {
            if (_toggelback != null) {
                _toggelback.RadiusX = _toggelback.RadiusY = ToggelHeight / 2.0;
                _toggelback.Height = ToggelHeight;
                _toggelthumb.Height = _toggelthumb.Width = ToggelHeight - 2 * _toggelback.StrokeThickness - 2 * ThumbPadding;
                _onkey1.Value = _toggelpanel.ActualWidth - _toggelback.StrokeThickness - ThumbPadding - _toggelthumb.Width;
                _offkey1.Value = _toggelback.StrokeThickness + ThumbPadding;
                _toggeltranslate.X = (bool)IsChecked
                    ? _onkey1.Value
                    : _offkey1.Value;
            }
        }

        private void _thumboff_Completed(object sender, EventArgs e) {
            _toggeltranslate.BeginAnimation(TranslateTransform.XProperty, null);
            _toggeltranslate.X = _toggelback.StrokeThickness + ThumbPadding;
        }

        private void _thumbon_Completed(object sender, EventArgs e) {
            _toggeltranslate.BeginAnimation(TranslateTransform.XProperty, null);
            _toggeltranslate.X = _toggelpanel.ActualWidth - _toggelback.StrokeThickness - ThumbPadding - _toggelthumb.Width;
        }
        #endregion

        #region Constructors
        static YT_WinXToggelSwitch() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_WinXToggelSwitch),
                new FrameworkPropertyMetadata(typeof(YT_WinXToggelSwitch), FrameworkPropertyMetadataOptions.Inherits));
        }
        #endregion
    }

}
