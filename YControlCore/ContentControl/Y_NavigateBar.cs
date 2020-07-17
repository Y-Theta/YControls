///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using YControlCore.ControlBase;

namespace YControlCore.ContentControl {

    /// <summary>
    /// which direction will the panel size making a change
    /// </summary>
    public enum ExpandDirection {
        Left,
        Top,
        Bottom,
        Right
    }

    /// <summary>
    /// 导航栏
    /// <para> 定义可 自动隐藏/最小化 的导航栏容器 </para>
    /// </summary>
    [ContentProperty(nameof(MiniContent))]
    public class Y_NavigateBar : ToggleStateControl {

        #region Properties

        #region animate
        private RectangleGeometry ClipMask;

        private DoubleAnimationUsingKeyFrames Expand_Trans_A1;
        private DoubleAnimationUsingKeyFrames Expand_Trans_A2;
        private DoubleAnimationUsingKeyFrames Collapsed_Trans_A1;
        private DoubleAnimationUsingKeyFrames Collapsed_Trans_A2;

        //private DoubleAnimationUsingKeyFrames Expand_A1;
        private DoubleAnimationUsingKeyFrames Expand_A2;
        //private DoubleAnimationUsingKeyFrames Collapsed_A1;
        private DoubleAnimationUsingKeyFrames Collapsed_A2;

        private SplineDoubleKeyFrame Expand_Trans_F1;
        private SplineDoubleKeyFrame Collapsed_Trans_F1;

        private DiscreteDoubleKeyFrame Expand_Trans_F2;
        private DiscreteDoubleKeyFrame Expand_Trans_F3;
        private DiscreteDoubleKeyFrame Collapsed_Trans_F2;
        private DiscreteDoubleKeyFrame Collapsed_Trans_F3;
        //private DiscreteDoubleKeyFrame Expand_F1;
        private DiscreteDoubleKeyFrame Expand_F2;
        //private DiscreteDoubleKeyFrame Collapsed_F1;
        private DiscreteDoubleKeyFrame Collapsed_F2;

        private bool _transtition = false;
        private VisualStateGroup _visualstates;
        #endregion

        /// <summary>
        /// 导航栏的布局方式
        /// </summary>
        public ExpandDirection Direction {
            get { return (ExpandDirection)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register("Direction", typeof(ExpandDirection),
                typeof(Y_NavigateBar), new PropertyMetadata(ExpandDirection.Bottom, OnDirectionChanged));
        private static void OnDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            Y_NavigateBar instance = (Y_NavigateBar)d;
            instance.calcanimeparams();
            instance.resetmaskclip();
        }

        /// <summary>
        /// 导航栏的状态
        /// </summary>
        private static void OnStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            Y_NavigateBar instance = (Y_NavigateBar)d;
            instance.switchstatus((bool)e.NewValue);
        }

        /// <summary>
        /// 内容控件区域（在Expand下显示）
        /// </summary>
        public FrameworkElement Content {
            get { return (FrameworkElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(FrameworkElement),
                typeof(Y_NavigateBar), new PropertyMetadata(null));

        /// <summary>
        /// 迷你内容控件区域（在Collapsed下显示）
        /// </summary>
        public FrameworkElement MiniContent {
            get { return (FrameworkElement)GetValue(MiniContentProperty); }
            set { SetValue(MiniContentProperty, value); }
        }
        public static readonly DependencyProperty MiniContentProperty =
            DependencyProperty.Register("MiniContent", typeof(FrameworkElement),
                typeof(Y_NavigateBar), new PropertyMetadata(null));

        /// <summary>
        /// 面板展开时的大小 横向时表示宽度 纵向时表示高度
        /// </summary>
        public double ExpandSize {
            get { return (double)GetValue(ExpandSizeProperty); }
            set { SetValue(ExpandSizeProperty, value); }
        }
        public static readonly DependencyProperty ExpandSizeProperty =
            DependencyProperty.Register("ExpandSize", typeof(double),
                typeof(Y_NavigateBar), new PropertyMetadata(YControl.NavigateBarExpandSize, OnPanelSizeChanged));

        /// <summary>
        /// 面板折叠时的大小 横向时表示宽度 纵向时表示高度
        /// </summary>
        public double CollapsedSize {
            get { return (double)GetValue(CollapsedSizeProperty); }
            set { SetValue(CollapsedSizeProperty, value); }
        }
        public static readonly DependencyProperty CollapsedSizeProperty =
            DependencyProperty.Register("CollapsedSize", typeof(double),
                typeof(Y_NavigateBar), new PropertyMetadata(YControl.NavigateBarCollapsedSize, OnPanelSizeChanged));

        private static void OnPanelSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            Y_NavigateBar instance = (Y_NavigateBar)d;
            if (instance.ExpandSize <= instance.CollapsedSize)
                throw new ArgumentException(" ExpandSize > CollapsedSize is required");
            instance.calcanimeparams();
            instance.resetmaskclip();
        }
        #endregion

        #region Methods
        private void switchstatus(bool flag) {
            VisualStateManager.GoToState(this, flag ? "Expand" : "Collapsed", UseAnimate);
        }

        private void resetmaskclip() {
            if (Expand_Trans_F1 != null) {
                if (Direction.Equals(ExpandDirection.Bottom)) {
                    ClipMask.Rect = new Rect(0, 0, RenderSize.Width, ExpandSize);
                } else if (Direction.Equals(ExpandDirection.Top)) {
                    ClipMask.Rect = new Rect(0, 0, RenderSize.Width, ExpandSize);

                } else {
                    ClipMask.Rect = new Rect(0, 0, ExpandSize, RenderSize.Height);
                }
            }
        }

        private void calcanimeparams() {
            if (Expand_Trans_F1 != null) {

                if (Direction.Equals(ExpandDirection.Bottom) || Direction.Equals(ExpandDirection.Top)) {
                    Storyboard.SetTargetProperty(Expand_Trans_A1, new PropertyPath(TranslateTransform.YProperty));
                    Storyboard.SetTargetProperty(Collapsed_Trans_A1, new PropertyPath(TranslateTransform.YProperty));
                    Storyboard.SetTargetProperty(Expand_Trans_A2, new PropertyPath(Grid.HeightProperty));
                    Storyboard.SetTargetProperty(Collapsed_Trans_A2, new PropertyPath(Grid.HeightProperty));
                    //Storyboard.SetTargetProperty(Expand_A1, new PropertyPath(TranslateTransform.YProperty));
                    //Storyboard.SetTargetProperty(Collapsed_A1, new PropertyPath(TranslateTransform.YProperty));
                    Storyboard.SetTargetProperty(Expand_A2, new PropertyPath(Grid.HeightProperty));
                    Storyboard.SetTargetProperty(Collapsed_A2, new PropertyPath(Grid.HeightProperty));
                } else {
                    Storyboard.SetTargetProperty(Expand_Trans_A1, new PropertyPath(TranslateTransform.XProperty));
                    Storyboard.SetTargetProperty(Collapsed_Trans_A1, new PropertyPath(TranslateTransform.XProperty));
                    Storyboard.SetTargetProperty(Expand_Trans_A2, new PropertyPath(Grid.WidthProperty));
                    Storyboard.SetTargetProperty(Collapsed_Trans_A2, new PropertyPath(Grid.WidthProperty));
                    //Storyboard.SetTargetProperty(Expand_A1, new PropertyPath(TranslateTransform.XProperty));
                    //Storyboard.SetTargetProperty(Collapsed_A1, new PropertyPath(TranslateTransform.XProperty));
                    Storyboard.SetTargetProperty(Expand_A2, new PropertyPath(Grid.WidthProperty));
                    Storyboard.SetTargetProperty(Collapsed_A2, new PropertyPath(Grid.WidthProperty));
                }

                //Expand_Trans_F1.Value = Expand_F1.Value = Collapsed_F1.Value = 0;
                Expand_Trans_F1.Value = 0;
                Collapsed_Trans_F3.Value = 0;

                switch (Direction) {
                    case ExpandDirection.Right:
                    case ExpandDirection.Bottom:
                        Collapsed_Trans_F1.Value = Expand_Trans_F3.Value = CollapsedSize - ExpandSize;
                        break;
                    case ExpandDirection.Top:
                    case ExpandDirection.Left:
                        Collapsed_Trans_F1.Value = Expand_Trans_F3.Value = ExpandSize - CollapsedSize;
                        break;

                }
                Expand_Trans_F2.Value = Expand_F2.Value = ExpandSize;
                Collapsed_Trans_F2.Value = Collapsed_F2.Value = CollapsedSize;
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
            base.OnRenderSizeChanged(sizeInfo);
            if (_transtition) {
                _transtition = false;
            } else {
                resetmaskclip();
                calcanimeparams();
            }
        }

        public override void OnApplyTemplate() {
            Expand_Trans_A1 = GetTemplateChild("Expand_Trans_A1") as DoubleAnimationUsingKeyFrames;
            Expand_Trans_A2 = GetTemplateChild("Expand_Trans_A2") as DoubleAnimationUsingKeyFrames;
            Collapsed_Trans_A1 = GetTemplateChild("Collapsed_Trans_A1") as DoubleAnimationUsingKeyFrames;
            Collapsed_Trans_A2 = GetTemplateChild("Collapsed_Trans_A2") as DoubleAnimationUsingKeyFrames;

            //Expand_A1 = GetTemplateChild("Expand_A1") as DoubleAnimationUsingKeyFrames;
            Expand_A2 = GetTemplateChild("Expand_A2") as DoubleAnimationUsingKeyFrames;
            //Collapsed_A1 = GetTemplateChild("Collapsed_A1") as DoubleAnimationUsingKeyFrames;
            Collapsed_A2 = GetTemplateChild("Collapsed_A2") as DoubleAnimationUsingKeyFrames;

            Expand_Trans_F1 = GetTemplateChild("Expand_Trans_F1") as SplineDoubleKeyFrame;
            Expand_Trans_F3 = GetTemplateChild("Expand_Trans_F3") as DiscreteDoubleKeyFrame;
            Collapsed_Trans_F1 = GetTemplateChild("Collapsed_Trans_F1") as SplineDoubleKeyFrame;
            Collapsed_Trans_F3 = GetTemplateChild("Collapsed_Trans_F3") as DiscreteDoubleKeyFrame;
            Expand_Trans_F2 = GetTemplateChild("Expand_Trans_F2") as DiscreteDoubleKeyFrame;
            Collapsed_Trans_F2 = GetTemplateChild("Collapsed_Trans_F2") as DiscreteDoubleKeyFrame;
            //Expand_F1 = GetTemplateChild("Expand_F1") as DiscreteDoubleKeyFrame;
            Expand_F2 = GetTemplateChild("Expand_F2") as DiscreteDoubleKeyFrame;
            //Collapsed_F1 = GetTemplateChild("Collapsed_F1") as DiscreteDoubleKeyFrame;
            Collapsed_F2 = GetTemplateChild("Collapsed_F2") as DiscreteDoubleKeyFrame;


            ClipMask = GetTemplateChild("ClipMask") as RectangleGeometry;
            base.OnApplyTemplate();

            var root = GetTemplateChild("rootborder") as FrameworkElement;
            _visualstates = VisualStateManager.GetVisualStateGroups(root)[0] as VisualStateGroup;
        }


        #endregion

        #region Constructors
        static Y_NavigateBar() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Y_NavigateBar), new FrameworkPropertyMetadata(typeof(Y_NavigateBar)));

            IsExpandProperty.OverrideMetadata(typeof(Y_NavigateBar), new FrameworkPropertyMetadata(false, OnStatusChanged));
        }
        #endregion
    }
}
