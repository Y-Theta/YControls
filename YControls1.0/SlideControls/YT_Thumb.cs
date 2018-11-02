using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using YControls.FontIconButtons;
using YControls.IFontIcon;

namespace YControls.SlideControls {
    public class YT_Thumb : Thumb, IFontIconExtension {

        #region Properties
        /// <summary>
        /// 图标模式
        /// </summary>
        public ThumbIconType IconMode {
            get { return (ThumbIconType)GetValue(IconModeProperty); }
            set { SetValue(IconModeProperty, value); }
        }
        public static readonly DependencyProperty IconModeProperty =
            DependencyProperty.Register("IconMode", typeof(ThumbIconType),
                typeof(YT_Thumb), new FrameworkPropertyMetadata(ThumbIconType.Rect, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 在图标模式设置为FontIcon时有效，
        /// 为Icon使用的图标字族
        /// </summary>
        public FontFamily IconFont {
            get { return (FontFamily)GetValue(IconFontProperty); }
            set { SetValue(IconFontProperty, value); }
        }
        public static readonly DependencyProperty IconFontProperty =
            DependencyProperty.Register("IconFont", typeof(FontFamily),
                typeof(YT_Thumb), new FrameworkPropertyMetadata(new FontFamily(), FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 在图标模式设置为FontIcon、Path时有效
        /// Path 模式时 默认画布大小为 20 * 20 在Scrollbar下 此区域将被拉伸
        /// </summary>
        public String ThumbIcon {
            get { return (String)GetValue(ThumbIconProperty); }
            set { SetValue(ThumbIconProperty, value); }
        }
        public static readonly DependencyProperty ThumbIconProperty =
            DependencyProperty.Register("ThumbIcon", typeof(String),
                typeof(YT_Thumb), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 使用路径模式图标时路径的左端或上端形状 默认画布大小为 20 * 20 
        /// </summary>
        public string PathCapLoT {
            get { return (string)GetValue(PathCapLoTProperty); }
            set { SetValue(PathCapLoTProperty, value); }
        }
        public static readonly DependencyProperty PathCapLoTProperty =
            DependencyProperty.Register("PathCapLoT", typeof(string), 
                typeof(YT_Thumb), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 使用路径模式图标时路径的右端或下端形状 默认画布大小为 20 * 20 
        /// </summary>
        public string PathCapRoB {
            get { return (string)GetValue(PathCapRoBProperty); }
            set { SetValue(PathCapRoBProperty, value); }
        }
        public static readonly DependencyProperty PathCapRoBProperty =
            DependencyProperty.Register("PathCapRoB", typeof(string), 
                typeof(YT_Thumb), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 方向，默认竖直
        /// </summary>
        public Orientation Orientation {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), 
                typeof(YT_Thumb), new PropertyMetadata(Orientation.Vertical));

        #region Icon
        public Visibility IconVisibility {
            get { return (Visibility)GetValue(IconVisibilityProperty); }
            set { SetValue(IconVisibilityProperty, value); }
        }
        public static readonly DependencyProperty IconVisibilityProperty =
            DependencyProperty.Register("IconVisibility", typeof(Visibility),
                typeof(YT_Thumb), new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.Inherits));

        #region Alignment
        public Thickness IconMargin {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }
        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register("IconMargin", typeof(Thickness),
                typeof(YT_Thumb), new FrameworkPropertyMetadata(new Thickness(0), FrameworkPropertyMetadataOptions.Inherits));

        public HorizontalAlignment IconHorizontalAlignment {
            get { return (HorizontalAlignment)GetValue(IconHorizontalAlignmentProperty); }
            set { SetValue(IconHorizontalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty IconHorizontalAlignmentProperty =
            DependencyProperty.Register("IconHorizontalAlignment", typeof(HorizontalAlignment),
                typeof(YT_Thumb), new FrameworkPropertyMetadata(HorizontalAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        public VerticalAlignment IconVerticalAlignment {
            get { return (VerticalAlignment)GetValue(IconVerticalAlignmentProperty); }
            set { SetValue(IconVerticalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty IconVerticalAlignmentProperty =
            DependencyProperty.Register("IconVerticalAlignment", typeof(VerticalAlignment),
                typeof(YT_Thumb), new FrameworkPropertyMetadata(VerticalAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        public TextAlignment IconTextAlignment {
            get { return (TextAlignment)GetValue(IconTextAlignmentProperty); }
            set { SetValue(IconTextAlignmentProperty, value); }
        }
        public static readonly DependencyProperty IconTextAlignmentProperty =
            DependencyProperty.Register("IconTextAlignment", typeof(TextAlignment),
                typeof(YT_Thumb), new FrameworkPropertyMetadata(TextAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #region Color

        public Brush IconFgNormal {
            get { return (Brush)GetValue(IconFgNormalProperty); }
            set { SetValue(IconFgNormalProperty, value); }
        }
        public static readonly DependencyProperty IconFgNormalProperty =
            DependencyProperty.Register("IconFgNormal", typeof(Brush),
                typeof(YT_Thumb), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconFgOver {
            get { return (Brush)GetValue(IconFgOverProperty); }
            set { SetValue(IconFgOverProperty, value); }
        }
        public static readonly DependencyProperty IconFgOverProperty =
            DependencyProperty.Register("IconFgOver", typeof(Brush),
                typeof(YT_Thumb), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconFgPressed {
            get { return (Brush)GetValue(IconFgPressedProperty); }
            set { SetValue(IconFgPressedProperty, value); }
        }
        public static readonly DependencyProperty IconFgPressedProperty =
            DependencyProperty.Register("IconFgPressed", typeof(Brush),
                typeof(YT_Thumb), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgNormal {
            get { return (Brush)GetValue(IconBgNormalProperty); }
            set { SetValue(IconBgNormalProperty, value); }
        }
        public static readonly DependencyProperty IconBgNormalProperty =
            DependencyProperty.Register("IconBgNormal", typeof(Brush),
                typeof(YT_Thumb), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgOver {
            get { return (Brush)GetValue(IconBgOverProperty); }
            set { SetValue(IconBgOverProperty, value); }
        }
        public static readonly DependencyProperty IconBgOverProperty =
            DependencyProperty.Register("IconBgOver", typeof(Brush),
                typeof(YT_Thumb), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgPressed {
            get { return (Brush)GetValue(IconBgPressedProperty); }
            set { SetValue(IconBgPressedProperty, value); }
        }
        public static readonly DependencyProperty IconBgPressedProperty =
            DependencyProperty.Register("IconBgPressed", typeof(Brush),
                typeof(YT_Thumb), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)), FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #endregion

        #endregion

        #region Methods

        #endregion

        #region Constructors
        static YT_Thumb() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_Thumb), new FrameworkPropertyMetadata(typeof(YT_Thumb), FrameworkPropertyMetadataOptions.Inherits));
        }

        #endregion
    }

}
