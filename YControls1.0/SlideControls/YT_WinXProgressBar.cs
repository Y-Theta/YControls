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

namespace YControls.SlideControls {

    /// <summary>
    /// Win10 风格进度条
    /// </summary>
    [TemplatePart(Name = "ValuePanel", Type = typeof(Path))]
    [TemplatePart(Name = "BackPanel", Type = typeof(Path))]
    public class YT_WinXProgressBar : YT_ProgressBarBase {
        #region Properties
        /// <summary>
        /// 百分比路径
        /// </summary>
        private Path _valuePanel { get; set; }

        /// <summary>
        /// 背景路径
        /// </summary>
        private Path _backPanel { get; set; }

        /// <summary>
        /// 圆形进度条的点半径
        /// </summary>
        public double DotSize {
            get { return (double)GetValue(DotSizeProperty); }
            set { SetValue(DotSizeProperty, value); }
        }
        public static readonly DependencyProperty DotSizeProperty =
            DependencyProperty.Register("DotSize", typeof(double),
                typeof(YT_WinXProgressBar), new PropertyMetadata(8.0));

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
        /// 直线进度条动画的1/3位置
        /// </summary>
        public double EllipseAnimationWellPosition {
            get { return (double)GetValue(EllipseAnimationWellPositionProperty); }
            set { SetValue(EllipseAnimationWellPositionProperty, value); }
        }
        public static readonly DependencyProperty EllipseAnimationWellPositionProperty =
            DependencyProperty.Register("EllipseAnimationWellPosition", typeof(double),
                 typeof(YT_ProgressBarBase), new PropertyMetadata(0.0));



        public double EllipseAnimationEndPosition {
            get { return (double)GetValue(EllipseAnimationEndPositionProperty); }
            set { SetValue(EllipseAnimationEndPositionProperty, value); }
        }
        public static readonly DependencyProperty EllipseAnimationEndPositionProperty =
            DependencyProperty.Register("EllipseAnimationEndPosition", typeof(double), 
                typeof(YT_ProgressBarBase), new PropertyMetadata(0.0));


        #endregion

        #region Methods
        /// <summary>
        /// 值改变时重新绘制
        /// </summary>
        protected override void OnValueChanged(double oldValue, double newValue) {
            base.OnValueChanged(oldValue, newValue);
            DrawValue();
        }

        private void DrawValue() {
            if (_valuePanel is Path)
                DrawRing();
            else
                return;
        }

        /// <summary>
        /// 
        /// </summary>
        private void DrawRect() {

        }

        /// <summary>
        /// 绘制百分比样式
        /// </summary>
        private void DrawRing() {
            int vs = (int)(ViewPortSize.Width > ViewPortSize.Height ? ViewPortSize.Height : ViewPortSize.Width);
            int r = (int)((vs - DotSize) / 1.414);
            _valuePanel.Data = Geometry.Parse(PercentToCircle(ValuePercent, vs, r));
            _backPanel.Data = Geometry.Parse(PercentToCircle(1, vs, r));
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
            base.UpdateVisualState();
            if (!IsIndeterminate)
                DrawValue();
        }

        /// <summary>
        /// 尺寸改变时同步改变
        /// </summary>
        private void YT_ProgressBarBase_SizeChanged(object sender, SizeChangedEventArgs e) {
            EllipseAnimationWellPosition = e.NewSize.Width / 3;
            EllipseAnimationEndPosition = 2 * EllipseAnimationWellPosition;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            _valuePanel = GetTemplateChild("ValuePanel") as Path;
            _backPanel = GetTemplateChild("BackPanel") as Path;
            if(_valuePanel is null) {
                ;
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
