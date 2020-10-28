///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

using YControlCore.ControlBase;

namespace YControlCore.Primitives {


    /// <summary>
    /// 自定义Thumb支持通过属性定义一部分自定义样式
    /// </summary>
    public class Y_Thumb : Thumb , IFontIconExtension {

        #region Properties

        public Visibility IconVisibility {
            get { return (Visibility)GetValue(IconVisibilityProperty); }
            set { SetValue(IconVisibilityProperty, value); }
        }
        public static readonly DependencyProperty IconVisibilityProperty =
            DependencyProperty.Register("IconVisibility", typeof(Visibility),
                typeof(Y_Thumb), new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.Inherits));

        #region Alignment
        public Thickness IconMargin {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }
        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register("IconMargin", typeof(Thickness),
                typeof(Y_Thumb), new FrameworkPropertyMetadata(new Thickness(0), FrameworkPropertyMetadataOptions.Inherits));

        public HorizontalAlignment IconHorizontalAlignment {
            get { return (HorizontalAlignment)GetValue(IconHorizontalAlignmentProperty); }
            set { SetValue(IconHorizontalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty IconHorizontalAlignmentProperty =
            DependencyProperty.Register("IconHorizontalAlignment", typeof(HorizontalAlignment),
                typeof(Y_Thumb), new FrameworkPropertyMetadata(HorizontalAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        public VerticalAlignment IconVerticalAlignment {
            get { return (VerticalAlignment)GetValue(IconVerticalAlignmentProperty); }
            set { SetValue(IconVerticalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty IconVerticalAlignmentProperty =
            DependencyProperty.Register("IconVerticalAlignment", typeof(VerticalAlignment),
                typeof(Y_Thumb), new FrameworkPropertyMetadata(VerticalAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        public TextAlignment IconTextAlignment {
            get { return (TextAlignment)GetValue(IconTextAlignmentProperty); }
            set { SetValue(IconTextAlignmentProperty, value); }
        }
        public static readonly DependencyProperty IconTextAlignmentProperty =
            DependencyProperty.Register("IconTextAlignment", typeof(TextAlignment),
                typeof(Y_Thumb), new FrameworkPropertyMetadata(TextAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #region Color

        public Brush IconFgNormal {
            get { return (Brush)GetValue(IconFgNormalProperty); }
            set { SetValue(IconFgNormalProperty, value); }
        }
        public static readonly DependencyProperty IconFgNormalProperty =
            DependencyProperty.Register("IconFgNormal", typeof(Brush),
                typeof(Y_Thumb), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconFgOver {
            get { return (Brush)GetValue(IconFgOverProperty); }
            set { SetValue(IconFgOverProperty, value); }
        }
        public static readonly DependencyProperty IconFgOverProperty =
            DependencyProperty.Register("IconFgOver", typeof(Brush),
                typeof(Y_Thumb), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconFgPressed {
            get { return (Brush)GetValue(IconFgPressedProperty); }
            set { SetValue(IconFgPressedProperty, value); }
        }
        public static readonly DependencyProperty IconFgPressedProperty =
            DependencyProperty.Register("IconFgPressed", typeof(Brush),
                typeof(Y_Thumb), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconFgDisabled {
            get { return (Brush)GetValue(IconFgDisabledProperty); }
            set { SetValue(IconFgDisabledProperty, value); }
        }
        public static readonly DependencyProperty IconFgDisabledProperty =
            DependencyProperty.Register("IconFgDisabled", typeof(Brush),
                typeof(Y_Thumb), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 32, 32, 32)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgNormal {
            get { return (Brush)GetValue(IconBgNormalProperty); }
            set { SetValue(IconBgNormalProperty, value); }
        }
        public static readonly DependencyProperty IconBgNormalProperty =
            DependencyProperty.Register("IconBgNormal", typeof(Brush),
                typeof(Y_Thumb), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgOver {
            get { return (Brush)GetValue(IconBgOverProperty); }
            set { SetValue(IconBgOverProperty, value); }
        }
        public static readonly DependencyProperty IconBgOverProperty =
            DependencyProperty.Register("IconBgOver", typeof(Brush),
                typeof(Y_Thumb), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgPressed {
            get { return (Brush)GetValue(IconBgPressedProperty); }
            set { SetValue(IconBgPressedProperty, value); }
        }
        public static readonly DependencyProperty IconBgPressedProperty =
            DependencyProperty.Register("IconBgPressed", typeof(Brush),
                typeof(Y_Thumb), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgDisabled {
            get { return (Brush)GetValue(IconBgDisabledProperty); }
            set { SetValue(IconBgDisabledProperty, value); }
        }
        public static readonly DependencyProperty IconBgDisabledProperty =
            DependencyProperty.Register("IconBgDisabled", typeof(Brush),
                typeof(Y_Thumb), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)), FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #endregion

        #region Methods
        #endregion

        #region Constructors
        static Y_Thumb() {
        }

        #endregion
    }
}
