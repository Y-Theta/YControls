using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using YControls.FontIconButtons;
using YControls.IFontIcon;

namespace YControls.FlowControls {

    public class YT_MenuItem : MenuItem, IFontIconExtension, IFontLabelExtension ,IToggleBrush {
        #region Properties

        #region Icon
        public Visibility IconVisibility {
            get { return (Visibility)GetValue(IconVisibilityProperty); }
            set { SetValue(IconVisibilityProperty, value); }
        }
        public static readonly DependencyProperty IconVisibilityProperty =
            DependencyProperty.Register("IconVisibility", typeof(Visibility),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(Visibility.Visible,
                    FrameworkPropertyMetadataOptions.Inherits));

        public string IconSelect {
            get { return (string)GetValue(IconSelectProperty); }
            set { SetValue(IconSelectProperty, value); }
        }
        public static readonly DependencyProperty IconSelectProperty =
            DependencyProperty.Register("IconSelect", typeof(string),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        #region Alignment
        public Thickness IconMargin {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }
        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register("IconMargin", typeof(Thickness),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new Thickness(0), FrameworkPropertyMetadataOptions.Inherits));

        public HorizontalAlignment IconHorizontalAlignment {
            get { return (HorizontalAlignment)GetValue(IconHorizontalAlignmentProperty); }
            set { SetValue(IconHorizontalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty IconHorizontalAlignmentProperty =
            DependencyProperty.Register("IconHorizontalAlignment", typeof(HorizontalAlignment),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(HorizontalAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        public VerticalAlignment IconVerticalAlignment {
            get { return (VerticalAlignment)GetValue(IconVerticalAlignmentProperty); }
            set { SetValue(IconVerticalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty IconVerticalAlignmentProperty =
            DependencyProperty.Register("IconVerticalAlignment", typeof(VerticalAlignment),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(VerticalAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        public TextAlignment IconTextAlignment {
            get { return (TextAlignment)GetValue(IconTextAlignmentProperty); }
            set { SetValue(IconTextAlignmentProperty, value); }
        }
        public static readonly DependencyProperty IconTextAlignmentProperty =
            DependencyProperty.Register("IconTextAlignment", typeof(TextAlignment),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(TextAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #region Color

        public Brush IconFgNormal {
            get { return (Brush)GetValue(IconFgNormalProperty); }
            set { SetValue(IconFgNormalProperty, value); }
        }
        public static readonly DependencyProperty IconFgNormalProperty =
            DependencyProperty.Register("IconFgNormal", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconFgOver {
            get { return (Brush)GetValue(IconFgOverProperty); }
            set { SetValue(IconFgOverProperty, value); }
        }
        public static readonly DependencyProperty IconFgOverProperty =
            DependencyProperty.Register("IconFgOver", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconFgPressed {
            get { return (Brush)GetValue(IconFgPressedProperty); }
            set { SetValue(IconFgPressedProperty, value); }
        }
        public static readonly DependencyProperty IconFgPressedProperty =
            DependencyProperty.Register("IconFgPressed", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconFgDisabled {
            get { return (Brush)GetValue(IconFgDisabledProperty); }
            set { SetValue(IconFgDisabledProperty, value); }
        }
        public static readonly DependencyProperty IconFgDisabledProperty =
            DependencyProperty.Register("IconFgDisabled", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgNormal {
            get { return (Brush)GetValue(IconBgNormalProperty); }
            set { SetValue(IconBgNormalProperty, value); }
        }
        public static readonly DependencyProperty IconBgNormalProperty =
            DependencyProperty.Register("IconBgNormal", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgOver {
            get { return (Brush)GetValue(IconBgOverProperty); }
            set { SetValue(IconBgOverProperty, value); }
        }
        public static readonly DependencyProperty IconBgOverProperty =
            DependencyProperty.Register("IconBgOver", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgPressed {
            get { return (Brush)GetValue(IconBgPressedProperty); }
            set { SetValue(IconBgPressedProperty, value); }
        }
        public static readonly DependencyProperty IconBgPressedProperty =
            DependencyProperty.Register("IconBgPressed", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgDisabled {
            get { return (Brush)GetValue(IconBgDisabledProperty); }
            set { SetValue(IconBgDisabledProperty, value); }
        }
        public static readonly DependencyProperty IconBgDisabledProperty =
            DependencyProperty.Register("IconBgDisabled", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectFgNormal {
            get { return (Brush)GetValue(IconSelectFgNormalProperty); }
            set { SetValue(IconSelectFgNormalProperty, value); }
        }
        public static readonly DependencyProperty IconSelectFgNormalProperty =
            DependencyProperty.Register("IconSelectFgNormal", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectFgOver {
            get { return (Brush)GetValue(IconSelectFgOverProperty); }
            set { SetValue(IconSelectFgOverProperty, value); }
        }
        public static readonly DependencyProperty IconSelectFgOverProperty =
            DependencyProperty.Register("IconSelectFgOver", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectFgPressed {
            get { return (Brush)GetValue(IconSelectFgPressedProperty); }
            set { SetValue(IconSelectFgPressedProperty, value); }
        }
        public static readonly DependencyProperty IconSelectFgPressedProperty =
            DependencyProperty.Register("IconSelectFgPressed", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectFgDisabled {
            get { return (Brush)GetValue(IconSelectFgDisabledProperty); }
            set { SetValue(IconSelectFgDisabledProperty, value); }
        }
        public static readonly DependencyProperty IconSelectFgDisabledProperty =
            DependencyProperty.Register("IconSelectFgDisabled", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectBgNormal {
            get { return (Brush)GetValue(IconSelectBgNormalProperty); }
            set { SetValue(IconSelectBgNormalProperty, value); }
        }
        public static readonly DependencyProperty IconSelectBgNormalProperty =
            DependencyProperty.Register("IconSelectBgNormal", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectBgOver {
            get { return (Brush)GetValue(IconSelectBgOverProperty); }
            set { SetValue(IconSelectBgOverProperty, value); }
        }
        public static readonly DependencyProperty IconSelectBgOverProperty =
            DependencyProperty.Register("IconSelectBgOver", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 32, 32, 32)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectBgPressed {
            get { return (Brush)GetValue(IconSelectBgPressedProperty); }
            set { SetValue(IconSelectBgPressedProperty, value); }
        }
        public static readonly DependencyProperty IconSelectBgPressedProperty =
            DependencyProperty.Register("IconSelectBgPressed", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 16, 16, 16)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectBgDisabled {
            get { return (Brush)GetValue(IconSelectBgDisabledProperty); }
            set { SetValue(IconSelectBgDisabledProperty, value); }
        }
        public static readonly DependencyProperty IconSelectBgDisabledProperty =
            DependencyProperty.Register("IconSelectBgDisabled", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)),
                    FrameworkPropertyMetadataOptions.Inherits));



        #endregion

        #endregion

        #region Label
        public string LabelSelect {
            get { return (string)GetValue(LabelSelectProperty); }
            set { SetValue(LabelSelectProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectProperty =
            DependencyProperty.Register("LabelSelect", typeof(string),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public Visibility LabelVisibility {
            get { return (Visibility)GetValue(LabelVisibilityProperty); }
            set { SetValue(LabelVisibilityProperty, value); }
        }
        public static readonly DependencyProperty LabelVisibilityProperty =
            DependencyProperty.Register("LabelVisibility", typeof(Visibility),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(Visibility.Visible,
                    FrameworkPropertyMetadataOptions.Inherits));

        public string LabelString {
            get { return (string)GetValue(LabelStringProperty); }
            set { SetValue(LabelStringProperty, value); }
        }
        public static readonly DependencyProperty LabelStringProperty =
            DependencyProperty.Register("LabelString", typeof(string),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata("This is a label",
                    FrameworkPropertyMetadataOptions.Inherits));

        public double LabelFontSize {
            get { return (double)GetValue(LabelFontSizeProperty); }
            set { SetValue(LabelFontSizeProperty, value); }
        }
        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register("LabelFontSize", typeof(double),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.Inherits));

        public FontWeight LabelFontWeight {
            get { return (FontWeight)GetValue(LabelFontWeightProperty); }
            set { SetValue(LabelFontWeightProperty, value); }
        }
        public static readonly DependencyProperty LabelFontWeightProperty =
            DependencyProperty.Register("LabelFontWeight", typeof(FontWeight),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(FontWeight.FromOpenTypeWeight(400),
                    FrameworkPropertyMetadataOptions.Inherits));

        public FontFamily LabelFontFamily {
            get { return (FontFamily)GetValue(LabelFontFamilyProperty); }
            set { SetValue(LabelFontFamilyProperty, value); }
        }
        public static readonly DependencyProperty LabelFontFamilyProperty =
            DependencyProperty.Register("LabelFontFamily", typeof(FontFamily),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new FontFamily(),
                    FrameworkPropertyMetadataOptions.Inherits));

        #region Alignment
        public Thickness LabelMargin {
            get { return (Thickness)GetValue(LabelMarginProperty); }
            set { SetValue(LabelMarginProperty, value); }
        }
        public static readonly DependencyProperty LabelMarginProperty =
            DependencyProperty.Register("LabelMargin", typeof(Thickness),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new Thickness(0), FrameworkPropertyMetadataOptions.Inherits));

        public HorizontalAlignment LabelHorizontalAlignment {
            get { return (HorizontalAlignment)GetValue(LabelHorizontalAlignmentProperty); }
            set { SetValue(LabelHorizontalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty LabelHorizontalAlignmentProperty =
            DependencyProperty.Register("LabelHorizontalAlignment", typeof(HorizontalAlignment),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(HorizontalAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        public VerticalAlignment LabelVerticalAlignment {
            get { return (VerticalAlignment)GetValue(LabelVerticalAlignmentProperty); }
            set { SetValue(LabelVerticalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty LabelVerticalAlignmentProperty =
            DependencyProperty.Register("LabelVerticalAlignment", typeof(VerticalAlignment),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(VerticalAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        public TextAlignment LabelTextAlignment {
            get { return (TextAlignment)GetValue(LabelTextAlignmentProperty); }
            set { SetValue(LabelTextAlignmentProperty, value); }
        }
        public static readonly DependencyProperty LabelTextAlignmentProperty =
            DependencyProperty.Register("LabelTextAlignment", typeof(TextAlignment),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(TextAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #region Color
        public Brush LabelSelectFgNormal {
            get { return (Brush)GetValue(LabelSelectFgNormalProperty); }
            set { SetValue(LabelSelectFgNormalProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectFgNormalProperty =
            DependencyProperty.Register("LabelSelectFgNormal", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectFgOver {
            get { return (Brush)GetValue(LabelSelectFgOverProperty); }
            set { SetValue(LabelSelectFgOverProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectFgOverProperty =
            DependencyProperty.Register("LabelSelectFgOver", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectFgPressed {
            get { return (Brush)GetValue(LabelSelectFgPressedProperty); }
            set { SetValue(LabelSelectFgPressedProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectFgPressedProperty =
            DependencyProperty.Register("LabelSelectFgPressed", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectFgDisabled {
            get { return (Brush)GetValue(LabelSelectFgDisabledProperty); }
            set { SetValue(LabelSelectFgDisabledProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectFgDisabledProperty =
            DependencyProperty.Register("LabelSelectFgDisabled", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectBgNormal {
            get { return (Brush)GetValue(LabelSelectBgNormalProperty); }
            set { SetValue(LabelSelectBgNormalProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectBgNormalProperty =
            DependencyProperty.Register("LabelSelectBgNormal", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectBgOver {
            get { return (Brush)GetValue(LabelSelectBgOverProperty); }
            set { SetValue(LabelSelectBgOverProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectBgOverProperty =
            DependencyProperty.Register("LabelSelectBgOver", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 32, 32, 32)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectBgPressed {
            get { return (Brush)GetValue(LabelSelectBgPressedProperty); }
            set { SetValue(LabelSelectBgPressedProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectBgPressedProperty =
            DependencyProperty.Register("LabelSelectBgPressed", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 16, 16, 16)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectBgDisabled {
            get { return (Brush)GetValue(LabelSelectBgDisabledProperty); }
            set { SetValue(LabelSelectBgDisabledProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectBgDisabledProperty =
            DependencyProperty.Register("LabelSelectBgDisabled", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelFgNormal {
            get { return (Brush)GetValue(LabelFgNormalProperty); }
            set { SetValue(LabelFgNormalProperty, value); }
        }
        public static readonly DependencyProperty LabelFgNormalProperty =
            DependencyProperty.Register("LabelFgNormal", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelFgOver {
            get { return (Brush)GetValue(LabelFgOverProperty); }
            set { SetValue(LabelFgOverProperty, value); }
        }
        public static readonly DependencyProperty LabelFgOverProperty =
            DependencyProperty.Register("LabelFgOver", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelFgPressed {
            get { return (Brush)GetValue(LabelFgPressedProperty); }
            set { SetValue(LabelFgPressedProperty, value); }
        }
        public static readonly DependencyProperty LabelFgPressedProperty =
            DependencyProperty.Register("LabelFgPressed", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelFgDisabled {
            get { return (Brush)GetValue(LabelFgDisabledProperty); }
            set { SetValue(LabelFgDisabledProperty, value); }
        }
        public static readonly DependencyProperty LabelFgDisabledProperty =
            DependencyProperty.Register("LabelFgDisabled", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelBgNormal {
            get { return (Brush)GetValue(LabelBgNormalProperty); }
            set { SetValue(LabelBgNormalProperty, value); }
        }
        public static readonly DependencyProperty LabelBgNormalProperty =
            DependencyProperty.Register("LabelBgNormal", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelBgOver {
            get { return (Brush)GetValue(LabelBgOverProperty); }
            set { SetValue(LabelBgOverProperty, value); }
        }
        public static readonly DependencyProperty LabelBgOverProperty =
            DependencyProperty.Register("LabelBgOver", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelBgPressed {
            get { return (Brush)GetValue(LabelBgPressedProperty); }
            set { SetValue(LabelBgPressedProperty, value); }
        }
        public static readonly DependencyProperty LabelBgPressedProperty =
            DependencyProperty.Register("LabelBgPressed", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelBgDisabled {
            get { return (Brush)GetValue(LabelBgDisabledProperty); }
            set { SetValue(LabelBgDisabledProperty, value); }
        }
        public static readonly DependencyProperty LabelBgDisabledProperty =
            DependencyProperty.Register("LabelBgDisabled", typeof(Brush),
                typeof(YT_MenuItem), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)),
                    FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #endregion

        #endregion

        #region Methods


        #endregion

        #region Constructors
        static YT_MenuItem() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_MenuItem), new FrameworkPropertyMetadata(typeof(YT_MenuItem), FrameworkPropertyMetadataOptions.Inherits));
        }
        #endregion
    }

}
