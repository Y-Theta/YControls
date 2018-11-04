using System;
using System.Runtime.Remoting.Metadata;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace YControls.FontIconButtons {
    /// <summary>
    /// 使用字体图标的按钮
    /// </summary>
    public class FIconButton : Button, IFontIconButton {

        #region Properties
        public LocateMode IconLayout {
            get { return (LocateMode)GetValue(IconLayoutProperty); }
            set { SetValue(IconLayoutProperty, value); }
        }
        public static readonly DependencyProperty IconLayoutProperty =
            DependencyProperty.Register("IconLayout", typeof(LocateMode),
                typeof(FIconButton), new FrameworkPropertyMetadata(LocateMode.IconLeft,
                    FrameworkPropertyMetadataOptions.Inherits));

        #region Icon
        public Visibility IconVisibility {
            get { return (Visibility)GetValue(IconVisibilityProperty); }
            set { SetValue(IconVisibilityProperty, value); }
        }
        public static readonly DependencyProperty IconVisibilityProperty =
            DependencyProperty.Register("IconVisibility", typeof(Visibility),
                typeof(FIconButton), new FrameworkPropertyMetadata(Visibility.Visible, 
                    FrameworkPropertyMetadataOptions.Inherits));

        #region Alignment
        public Thickness IconMargin {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }
        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register("IconMargin", typeof(Thickness),
                typeof(FIconButton), new FrameworkPropertyMetadata(new Thickness(0), FrameworkPropertyMetadataOptions.Inherits));

        public HorizontalAlignment IconHorizontalAlignment {
            get { return (HorizontalAlignment)GetValue(IconHorizontalAlignmentProperty); }
            set { SetValue(IconHorizontalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty IconHorizontalAlignmentProperty =
            DependencyProperty.Register("IconHorizontalAlignment", typeof(HorizontalAlignment),
                typeof(FIconButton), new FrameworkPropertyMetadata(HorizontalAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        public VerticalAlignment IconVerticalAlignment {
            get { return (VerticalAlignment)GetValue(IconVerticalAlignmentProperty); }
            set { SetValue(IconVerticalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty IconVerticalAlignmentProperty =
            DependencyProperty.Register("IconVerticalAlignment", typeof(VerticalAlignment),
                typeof(FIconButton), new FrameworkPropertyMetadata(VerticalAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        public TextAlignment IconTextAlignment {
            get { return (TextAlignment)GetValue(IconTextAlignmentProperty); }
            set { SetValue(IconTextAlignmentProperty, value); }
        }
        public static readonly DependencyProperty IconTextAlignmentProperty =
            DependencyProperty.Register("IconTextAlignment", typeof(TextAlignment),
                typeof(FIconButton), new FrameworkPropertyMetadata(TextAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #region Color

        public Brush IconFgNormal {
            get { return (Brush)GetValue(IconFgNormalProperty); }
            set { SetValue(IconFgNormalProperty, value); }
        }
        public static readonly DependencyProperty IconFgNormalProperty =
            DependencyProperty.Register("IconFgNormal", typeof(Brush),
                typeof(FIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconFgOver {
            get { return (Brush)GetValue(IconFgOverProperty); }
            set { SetValue(IconFgOverProperty, value); }
        }
        public static readonly DependencyProperty IconFgOverProperty =
            DependencyProperty.Register("IconFgOver", typeof(Brush),
                typeof(FIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)), 
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconFgPressed {
            get { return (Brush)GetValue(IconFgPressedProperty); }
            set { SetValue(IconFgPressedProperty, value); }
        }
        public static readonly DependencyProperty IconFgPressedProperty =
            DependencyProperty.Register("IconFgPressed", typeof(Brush),
                typeof(FIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconFgDisabled {
            get { return (Brush)GetValue(IconFgDisabledProperty); }
            set { SetValue(IconFgDisabledProperty, value); }
        }
        public static readonly DependencyProperty IconFgDisabledProperty =
            DependencyProperty.Register("IconFgDisabled", typeof(Brush),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgNormal {
            get { return (Brush)GetValue(IconBgNormalProperty); }
            set { SetValue(IconBgNormalProperty, value); }
        }
        public static readonly DependencyProperty IconBgNormalProperty =
            DependencyProperty.Register("IconBgNormal", typeof(Brush),
                typeof(FIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)), 
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgOver {
            get { return (Brush)GetValue(IconBgOverProperty); }
            set { SetValue(IconBgOverProperty, value); }
        }
        public static readonly DependencyProperty IconBgOverProperty =
            DependencyProperty.Register("IconBgOver", typeof(Brush),
                typeof(FIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180,180)), 
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgPressed {
            get { return (Brush)GetValue(IconBgPressedProperty); }
            set { SetValue(IconBgPressedProperty, value); }
        }
        public static readonly DependencyProperty IconBgPressedProperty =
            DependencyProperty.Register("IconBgPressed", typeof(Brush),
                typeof(FIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)), 
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgDisabled {
            get { return (Brush)GetValue(IconBgDisabledProperty); }
            set { SetValue(IconBgDisabledProperty, value); }
        }
        public static readonly DependencyProperty IconBgDisabledProperty =
            DependencyProperty.Register("IconBgDisabled", typeof(Brush),
                typeof(FIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)), 
                    FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #endregion

        #region Label

        public Visibility LabelVisibility {
            get { return (Visibility)GetValue(LabelVisibilityProperty); }
            set { SetValue(LabelVisibilityProperty, value); }
        }
        public static readonly DependencyProperty LabelVisibilityProperty =
            DependencyProperty.Register("LabelVisibility", typeof(Visibility),
                typeof(FIconButton), new FrameworkPropertyMetadata(Visibility.Visible, 
                    FrameworkPropertyMetadataOptions.Inherits));

        public string LabelString {
            get { return (string)GetValue(LabelStringProperty); }
            set { SetValue(LabelStringProperty, value); }
        }
        public static readonly DependencyProperty LabelStringProperty =
            DependencyProperty.Register("LabelString", typeof(string),
                typeof(FIconButton), new FrameworkPropertyMetadata("This is a label", 
                    FrameworkPropertyMetadataOptions.Inherits));

        public double LabelFontSize {
            get { return (double)GetValue(LabelFontSizeProperty); }
            set { SetValue(LabelFontSizeProperty, value); }
        }
        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register("LabelFontSize", typeof(double),
                typeof(FIconButton), new FrameworkPropertyMetadata(0.0, 
                    FrameworkPropertyMetadataOptions.Inherits));

        public FontWeight LabelFontWeight {
            get { return (FontWeight)GetValue(LabelFontWeightProperty); }
            set { SetValue(LabelFontWeightProperty, value); }
        }
        public static readonly DependencyProperty LabelFontWeightProperty =
            DependencyProperty.Register("LabelFontWeight", typeof(FontWeight),
                typeof(FIconButton), new FrameworkPropertyMetadata(FontWeight.FromOpenTypeWeight(400), 
                    FrameworkPropertyMetadataOptions.Inherits));

        public FontFamily LabelFontFamily {
            get { return (FontFamily)GetValue(LabelFontFamilyProperty); }
            set { SetValue(LabelFontFamilyProperty, value); }
        }
        public static readonly DependencyProperty LabelFontFamilyProperty =
            DependencyProperty.Register("LabelFontFamily", typeof(FontFamily),
                typeof(FIconButton), new FrameworkPropertyMetadata(new FontFamily(), 
                    FrameworkPropertyMetadataOptions.Inherits));

        #region Alignment
        public Thickness LabelMargin {
            get { return (Thickness)GetValue(LabelMarginProperty); }
            set { SetValue(LabelMarginProperty, value); }
        }
        public static readonly DependencyProperty LabelMarginProperty =
            DependencyProperty.Register("LabelMargin", typeof(Thickness),
                typeof(FIconButton), new FrameworkPropertyMetadata(new Thickness(0), FrameworkPropertyMetadataOptions.Inherits));

        public HorizontalAlignment LabelHorizontalAlignment {
            get { return (HorizontalAlignment)GetValue(LabelHorizontalAlignmentProperty); }
            set { SetValue(LabelHorizontalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty LabelHorizontalAlignmentProperty =
            DependencyProperty.Register("LabelHorizontalAlignment", typeof(HorizontalAlignment),
                typeof(FIconButton), new FrameworkPropertyMetadata(HorizontalAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        public VerticalAlignment LabelVerticalAlignment {
            get { return (VerticalAlignment)GetValue(LabelVerticalAlignmentProperty); }
            set { SetValue(LabelVerticalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty LabelVerticalAlignmentProperty =
            DependencyProperty.Register("LabelVerticalAlignment", typeof(VerticalAlignment),
                typeof(FIconButton), new FrameworkPropertyMetadata(VerticalAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        public TextAlignment LabelTextAlignment {
            get { return (TextAlignment)GetValue(LabelTextAlignmentProperty); }
            set { SetValue(LabelTextAlignmentProperty, value); }
        }
        public static readonly DependencyProperty LabelTextAlignmentProperty =
            DependencyProperty.Register("LabelTextAlignment", typeof(TextAlignment),
                typeof(FIconButton), new FrameworkPropertyMetadata(TextAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #region Color

        public Brush LabelFgNormal {
            get { return (Brush)GetValue(LabelFgNormalProperty); }
            set { SetValue(LabelFgNormalProperty, value); }
        }
        public static readonly DependencyProperty LabelFgNormalProperty =
            DependencyProperty.Register("LabelFgNormal", typeof(Brush),
                typeof(FIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)), 
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelFgOver {
            get { return (Brush)GetValue(LabelFgOverProperty); }
            set { SetValue(LabelFgOverProperty, value); }
        }
        public static readonly DependencyProperty LabelFgOverProperty =
            DependencyProperty.Register("LabelFgOver", typeof(Brush),
                typeof(FIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)), 
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelFgPressed {
            get { return (Brush)GetValue(LabelFgPressedProperty); }
            set { SetValue(LabelFgPressedProperty, value); }
        }
        public static readonly DependencyProperty LabelFgPressedProperty =
            DependencyProperty.Register("LabelFgPressed", typeof(Brush),
                typeof(FIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)), 
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelFgDisabled {
            get { return (Brush)GetValue(LabelFgDisabledProperty); }
            set { SetValue(LabelFgDisabledProperty, value); }
        }
        public static readonly DependencyProperty LabelFgDisabledProperty =
            DependencyProperty.Register("LabelFgDisabled", typeof(Brush),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelBgNormal {
            get { return (Brush)GetValue(LabelBgNormalProperty); }
            set { SetValue(LabelBgNormalProperty, value); }
        }
        public static readonly DependencyProperty LabelBgNormalProperty =
            DependencyProperty.Register("LabelBgNormal", typeof(Brush),
                typeof(FIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)), 
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelBgOver {
            get { return (Brush)GetValue(LabelBgOverProperty); }
            set { SetValue(LabelBgOverProperty, value); }
        }
        public static readonly DependencyProperty LabelBgOverProperty =
            DependencyProperty.Register("LabelBgOver", typeof(Brush),
                typeof(FIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)), 
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelBgPressed {
            get { return (Brush)GetValue(LabelBgPressedProperty); }
            set { SetValue(LabelBgPressedProperty, value); }
        }
        public static readonly DependencyProperty LabelBgPressedProperty =
            DependencyProperty.Register("LabelBgPressed", typeof(Brush),
                typeof(FIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255,80,80,80)), 
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelBgDisabled {
            get { return (Brush)GetValue(LabelBgDisabledProperty); }
            set { SetValue(LabelBgDisabledProperty, value); }
        }
        public static readonly DependencyProperty LabelBgDisabledProperty =
            DependencyProperty.Register("LabelBgDisabled", typeof(Brush),
                typeof(FIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)), 
                    FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #endregion

        public Style ToolTipStyle {
            get { return (Style)GetValue(ToolTipStyleProperty); }
            set { SetValue(ToolTipStyleProperty, value); }
        }
        public static readonly DependencyProperty ToolTipStyleProperty =
            DependencyProperty.Register("ToolTipStyle", typeof(Style),
                typeof(FIconButton), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public Style ToolTipTextStyle {
            get { return (Style)GetValue(ToolTipTextStyleProperty); }
            set { SetValue(ToolTipTextStyleProperty, value); }
        }
        public static readonly DependencyProperty ToolTipTextStyleProperty =
            DependencyProperty.Register("ToolTipTextStyle", typeof(Style),
                typeof(FIconButton), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public string ToolTipString {
            get { return (string)GetValue(ToolTipStringProperty); }
            set { SetValue(ToolTipStringProperty, value); }
        }
        public static readonly DependencyProperty ToolTipStringProperty =
            DependencyProperty.Register("ToolTipString", typeof(string),
                typeof(FIconButton), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #region Constructors
        static FIconButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FIconButton), new FrameworkPropertyMetadata(typeof(FIconButton), FrameworkPropertyMetadataOptions.Inherits));
        }
        #endregion
    }

}
