using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace YControls.SlideControls {
    /// <summary>
    /// 进度条基类
    /// </summary>
    [TemplatePart(Name = "PB_Indicator")]
    [TemplatePart(Name = "PB_Track")]
    [TemplatePart(Name = "PB_GlowRect")]
    [TemplateVisualState(GroupName = "PB_IndeterminateState", Name = "Fixed")]
    [TemplateVisualState(GroupName = "PB_IndeterminateState", Name = "UnKnown")]
    public class YT_ProgressBarBase : RangeBase {
        #region Properties

        /// <summary>
        /// 判断是否已知进度
        /// <para>已知：　数值显示</para>
        /// <para>未知：　加载动画</para>
        /// </summary>
        public bool IsIndeterminate {
            get { return (bool)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }
        public static readonly DependencyProperty IsIndeterminateProperty =
            DependencyProperty.Register("IsIndeterminate", typeof(bool),
                typeof(YT_ProgressBarBase), new FrameworkPropertyMetadata(false,
                    FrameworkPropertyMetadataOptions.Inherits, OnIsIndeterminateChanged));
        private static void OnIsIndeterminateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((YT_ProgressBarBase)d).UpdateVisualState();
        }

        /// <summary>
        /// 百分比属性，0-1
        /// </summary>
        public double ValuePercent {
            get { return (double)GetValue(ValuePercentProperty); }
            set { SetValue(ValuePercentProperty, value); }
        }
        public static readonly DependencyProperty ValuePercentProperty =
            DependencyProperty.Register("ValuePercent", typeof(double),
                typeof(YT_ProgressBarBase), new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.Inherits, OnValuePercentChanged));
        private static void OnValuePercentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            YT_ProgressBarBase ypb = (YT_ProgressBarBase)d;
            ypb.Value = ypb.Maximum * (double)e.NewValue;
        }

        #endregion

        #region Methods

        protected override void OnValueChanged(double oldValue, double newValue) {
            base.OnValueChanged(oldValue, newValue);
            ValuePercent = Maximum == 0 ? 1 : newValue / Maximum;
        }

        private void YT_ProgressBarBase_Loaded(object sender, RoutedEventArgs e) {
            UpdateVisualState();
        }

        protected virtual void UpdateVisualState() {
            if (IsIndeterminate)
                VisualStateManager.GoToState(this, "UnKnown", true);
            else
                VisualStateManager.GoToState(this, "Fixed", true);
        }
        #endregion

        #region Constructors
        public YT_ProgressBarBase() {
            Loaded += YT_ProgressBarBase_Loaded;
        }

        static YT_ProgressBarBase() {
            MaximumProperty.OverrideMetadata(typeof(YT_ProgressBarBase), new FrameworkPropertyMetadata(100.0));
        }
        #endregion
    }
}
