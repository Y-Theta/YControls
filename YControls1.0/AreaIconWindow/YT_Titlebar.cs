using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace YControls.AreaIconWindow {

    /// <summary>
    /// 拖动边框
    /// </summary>
    public class YT_TitleBar : ContentControl {

        #region Properties
        private Window attachedWindow;
        public Window AttachedWindow {
            get => attachedWindow;
            set { attachedWindow = value; }
        }

        #region AnimationAbout
        public bool TitleBarVisible { get; set; }

        private string _tname;

        private RectangleGeometry _clipMask;

        private TranslateTransform _cliptransform;

        private DoubleAnimationUsingKeyFrames _fadeinanimation;

        private SplineDoubleKeyFrame _keyin1;

        private Storyboard _fadein;

        private DoubleAnimationUsingKeyFrames _fadeoutanimation;

        private SplineDoubleKeyFrame _keyout1;

        private Storyboard _fadeout;

        #endregion

        #region AutoHide
        /// <summary>
        /// 标题栏是否自动隐藏
        /// </summary>
        public bool AutoHide {
            get { return (bool)GetValue(AutoHideProperty); }
            set { SetValue(AutoHideProperty, value); }
        }
        public static readonly DependencyProperty AutoHideProperty =
            DependencyProperty.Register("AutoHide", typeof(bool),
                typeof(YT_TitleBar), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits,
                    AutoHideTitleBarChanged));
        private static void AutoHideTitleBarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if ((bool)e.NewValue)
                ((YT_TitleBar)d).InitStoryboard();
            else
                ((YT_TitleBar)d).ClearRes();
        }
        #endregion

        #endregion

        #region Methods Overrides
        private void YT_TitleBar_SizeChanged(object sender, SizeChangedEventArgs e) {
            _clipMask.Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height);                
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            AttachedWindow.DragMove();
            base.OnMouseLeftButtonDown(e);
        }

        public override void OnApplyTemplate() {
            if (AutoHide)
                InitStoryboard();
            DependencyObject RootElement = this;
            while (!(RootElement is Window)) { RootElement = VisualTreeHelper.GetParent(RootElement); }
            AttachedWindow = RootElement as Window;
            base.OnApplyTemplate();
        }

        private void Holder_MouseEnter(object sender, MouseEventArgs e) {
            ShowTitleBar();
            base.OnMouseEnter(e);
        }

        private void Holder_MouseLeave(object sender, MouseEventArgs e) {
            HideTitleBar();
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// 显示标题栏动画
        /// </summary>
        public void ShowTitleBar() {
            //_cliptransform.BeginAnimation(TranslateTransform.YProperty, _fadeoutanimation);
            if (!TitleBarVisible)
                _fadeout.Begin(this);

            TitleBarVisible = true;
        }

        /// <summary>
        /// 隐藏标题栏动画
        /// </summary>
        public void HideTitleBar() {
            //_cliptransform.BeginAnimation(TranslateTransform.YProperty, _fadeinanimation);
            if (TitleBarVisible || _clipMask.Rect.Height != -_keyin1.Value) {
                _keyin1.Value = -_clipMask.Rect.Height;
                _fadein.Begin(this);
            }

            TitleBarVisible = false;
        }

        /// <summary>
        /// 初始化动画
        /// </summary>
        private void InitStoryboard() {
            var holder = GetTemplateChild("TitleHolder") as Grid;

            if (holder is null)
                return;
            //通过设置autohide属性引发动画时，不用重新创建一次动画资源
            if (_cliptransform is null) {
                holder.MouseEnter += Holder_MouseEnter;
                holder.MouseLeave += Holder_MouseLeave;

                _clipMask = GetTemplateChild("Mask") as RectangleGeometry;
                _cliptransform = GetTemplateChild("MaskTanslate") as TranslateTransform;
                _tname = "MaskTanslate";
                RegisterName(_tname, _cliptransform);

                _fadein = new Storyboard();
                _fadeinanimation = new DoubleAnimationUsingKeyFrames();
                _keyin1 = new SplineDoubleKeyFrame {
                    KeySpline = new KeySpline(0, 0.4, 0.6, 1),
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(320)),
                    Value = -_clipMask.Rect.Height,
                };
                _fadeinanimation.KeyFrames.Add(_keyin1);
                Storyboard.SetTargetName(_fadeinanimation, _tname);
                Storyboard.SetTargetProperty(_fadeinanimation, new PropertyPath(TranslateTransform.YProperty));
                _fadein.Children.Add(_fadeinanimation);

                _fadeout = new Storyboard();
                _fadeoutanimation = new DoubleAnimationUsingKeyFrames();
                _keyout1 = new SplineDoubleKeyFrame {
                    KeySpline = new KeySpline(0, 0.4, 0.6, 1),
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(320)),
                    Value = 0,
                };
                _fadeoutanimation.KeyFrames.Add(_keyout1);
                Storyboard.SetTargetName(_fadeoutanimation, _tname);
                Storyboard.SetTargetProperty(_fadeoutanimation, new PropertyPath(TranslateTransform.YProperty));
                _fadeout.Children.Add(_fadeoutanimation);
            }

            if (TitleBarVisible)
                HideTitleBar();
        }

        /// <summary>
        ///释放动画资源
        /// <summary>
        private void ClearRes() {
            if (!TitleBarVisible)
                ShowTitleBar();

            _cliptransform = null;
            _fadeinanimation = null;
            _fadeoutanimation = null;
            _keyin1 = null;
            _keyout1 = null;
            _fadein = null;
            _fadeout = null;
            var holder = GetTemplateChild("TitleHolder") as Grid;
            holder.MouseEnter -= Holder_MouseEnter;
            holder.MouseLeave -= Holder_MouseLeave;
        }

        #endregion

        #region Constructors
        public YT_TitleBar() {
            TitleBarVisible = true;
            SizeChanged += YT_TitleBar_SizeChanged;
        }

        static YT_TitleBar() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_TitleBar), new FrameworkPropertyMetadata(typeof(YT_TitleBar)));
        }
        #endregion
    }

}
