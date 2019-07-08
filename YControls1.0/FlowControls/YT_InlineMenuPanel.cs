using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace YControls.FlowControls {
    /// <summary>
    /// 在视觉树中的菜单控件
    /// </summary>
    public class YT_InlineMenuPanel : ContentControl {
        #region Properties

        #region 
        private TranslateTransform _part_MaskTranslate { get; set; }

        private DoubleAnimationUsingKeyFrames _toNormal { get; set; }

        private DoubleAnimationUsingKeyFrames _toHide { get; set; }

        private SplineDoubleKeyFrame _normalkey { get; set; }

        private SplineDoubleKeyFrame _hidekey { get; set; }

        private Storyboard _hidestory { get; set; }

        private Storyboard _showstory { get; set; }
        #endregion

        /// <summary>
        /// 动画延迟 /ms
        /// </summary>
        public int AnimationDelay {
            get { return (int)GetValue(AnimationDelayProperty); }
            set { SetValue(AnimationDelayProperty, value); }
        }
        public static readonly DependencyProperty AnimationDelayProperty =
            DependencyProperty.Register("AnimationDelay", typeof(int),
                typeof(YT_InlineMenuPanel), new FrameworkPropertyMetadata(400, OnAnimationDelayChanged));
        private static void OnAnimationDelayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((YT_InlineMenuPanel)d).SetDelay((int)e.NewValue);
        }

        /// <summary>
        /// 出现与消失动画方式
        /// </summary>
        public MenuAnimateMode AnimationMode {
            get { return (MenuAnimateMode)GetValue(AnimationModeProperty); }
            set { SetValue(AnimationModeProperty, value); }
        }
        public static readonly DependencyProperty AnimationModeProperty =
            DependencyProperty.Register("AnimationMode", typeof(MenuAnimateMode),
                 typeof(YT_InlineMenuPanel), new FrameworkPropertyMetadata(MenuAnimateMode.None, OnAnimationModeChanged));
        private static void OnAnimationModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((YT_InlineMenuPanel)d).ModeChanged((MenuAnimateMode)e.NewValue);
        }

        /// <summary>
        /// 设置菜单是否打开
        /// </summary>
        public bool IsOpen {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool),
              typeof(YT_InlineMenuPanel), new FrameworkPropertyMetadata(false, OnIsOpenChanged));
        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((YT_InlineMenuPanel)d).ChangeState((bool)e.NewValue);
        }

        #endregion

        #region Methods
        /// <summary>
        /// 在控件大小改变时改变动画属性
        /// </summary>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
            if (AnimationMode != MenuAnimateMode.None && _normalkey != null) {
                switch (AnimationMode) {
                    case MenuAnimateMode.Left_Slide:
                        _hidekey.Value = -sizeInfo.NewSize.Width;
                        break;
                    case MenuAnimateMode.Right_Slide:
                        _hidekey.Value = sizeInfo.NewSize.Width;
                        break;
                    case MenuAnimateMode.Top_Slide:
                        _hidekey.Value = -sizeInfo.NewSize.Height;
                        break;
                    case MenuAnimateMode.Bottom_Slide:
                        _hidekey.Value = sizeInfo.NewSize.Height;
                        break;
                }
            }
            base.OnRenderSizeChanged(sizeInfo);
        }

        /// <summary>
        /// 动画模式改变
        /// </summary>
        protected void ModeChanged(MenuAnimateMode mode) {
            if (_normalkey != null) {
                switch (mode) {
                    case MenuAnimateMode.Left_Slide:
                        _hidekey.Value = -ActualWidth;
                        break;
                    case MenuAnimateMode.Right_Slide:
                        _hidekey.Value = ActualWidth;
                        break;
                    case MenuAnimateMode.Top_Slide:
                        _hidekey.Value = -ActualHeight;
                        break;
                    case MenuAnimateMode.Bottom_Slide:
                        _hidekey.Value = ActualHeight;
                        break;
                }
                ChangeState(IsOpen);
            }
        }

        /// <summary>
        /// 在菜单打开关闭时播放动画
        /// </summary>
        protected void ChangeState(bool open) {
            if (open)
                Visibility = Visibility.Visible;
            if (AnimationMode == MenuAnimateMode.Left_Slide || AnimationMode == MenuAnimateMode.Right_Slide) {
                Storyboard.SetTargetProperty(_toNormal, new PropertyPath(TranslateTransform.XProperty));
                Storyboard.SetTargetProperty(_toHide, new PropertyPath(TranslateTransform.XProperty));
                if (open)
                    _showstory.Begin(this);
                else
                    _hidestory.Begin(this);
            }else if(AnimationMode == MenuAnimateMode.Top_Slide || AnimationMode == MenuAnimateMode.Bottom_Slide) {
                Storyboard.SetTargetProperty(_toNormal, new PropertyPath(TranslateTransform.YProperty));
                Storyboard.SetTargetProperty(_toHide, new PropertyPath(TranslateTransform.YProperty));
                if (open)
                    _showstory.Begin(this);
                else
                    _hidestory.Begin(this);
            }
        }

        /// <summary>
        /// 设置动画延迟
        /// </summary>
        private void SetDelay(int value) {
            if (_normalkey != null) {
                _normalkey.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(AnimationDelay));
                _hidekey.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(AnimationDelay));
            }
        }

        /// <summary>
        /// 初始化动画
        /// </summary>
        private void InitAnimate() {
            //弹出动画
            _toNormal = new DoubleAnimationUsingKeyFrames();
            _normalkey = new SplineDoubleKeyFrame {
                KeySpline = new KeySpline(0, 0.5, 0.5, 1),
                Value = 0,
            };
            _toNormal.KeyFrames.Add(_normalkey);

            _showstory = new Storyboard();
            Storyboard.SetTargetName(_toNormal, "PART_MaskTranslate");
            _showstory.Children.Add(_toNormal);

            //隐藏动画
            _toHide = new DoubleAnimationUsingKeyFrames();
            _hidekey = new SplineDoubleKeyFrame {
                KeySpline = new KeySpline(0, 0.5, 0.5, 1),
            };
            _toHide.KeyFrames.Add(_hidekey);

            _hidestory = new Storyboard();
            Storyboard.SetTargetName(_toHide, "PART_MaskTranslate");
            _hidestory.Children.Add(_toHide);
            _hidestory.Completed += _toHide_Completed;

            SetDelay(AnimationDelay);
        }

        /// <summary>
        /// 隐藏动画结束时将控件隐藏
        /// </summary>
        private void _toHide_Completed(object sender, EventArgs e) {
            Visibility = Visibility.Collapsed;
        }

  

        public override void OnApplyTemplate() {
            _part_MaskTranslate = GetTemplateChild("PART_MaskTranslate") as TranslateTransform;
            RegisterName("PART_MaskTranslate", _part_MaskTranslate);
            if (AnimationMode != MenuAnimateMode.None) {
                InitAnimate();
            }
            base.OnApplyTemplate();
        }

        /// <summary>
        /// 接收继承自Button的左键单击事件
        /// </summary>
        private void LeftButtonUp(object sender, MouseButtonEventArgs e) {
            IsOpen = false;
        }

        private void YT_InlineMenuPanel_Loaded(object sender, RoutedEventArgs e) {
            Visibility = Visibility.Collapsed;
            ChangeState(IsOpen);
        }
        #endregion

        #region Constructors

        public YT_InlineMenuPanel() {
            Loaded += YT_InlineMenuPanel_Loaded;
            AddHandler(Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(LeftButtonUp), true);
        }

        static YT_InlineMenuPanel() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_InlineMenuPanel),
                new FrameworkPropertyMetadata(typeof(YT_InlineMenuPanel), FrameworkPropertyMetadataOptions.Inherits));
        }

        #endregion
    }

}
