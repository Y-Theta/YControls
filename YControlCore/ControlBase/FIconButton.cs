///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using YControlCore.ContentControl;

namespace YControlCore.ControlBase {

    /// <summary>
    /// IFontIconExtension 的按钮的图标布局方式
    /// </summary>
    public enum LocateMode {
        /// <summary>
        /// 图标在左
        /// </summary>
        IconLeft,
        /// <summary>
        /// 标签在左
        /// </summary>
        LabelLeft,
        /// <summary>
        /// 图标在上
        /// </summary>
        IconTop,
        /// <summary>
        /// 标签在上
        /// </summary>
        LabelTop
    }

    /// <summary>
    /// 这个接口用于扩展WPF Button控件中的颜色属性用于自定义模板
    /// </summary>
    internal interface IFontIconExtension {
        #region Icon
        /// <summary>
        /// 图标的可见性
        /// </summary>
        Visibility IconVisibility { get; set; }


        #region Alignment
        Thickness IconMargin { get; set; }
        HorizontalAlignment IconHorizontalAlignment { get; set; }
        VerticalAlignment IconVerticalAlignment { get; set; }
        TextAlignment IconTextAlignment { get; set; }
        #endregion

        #region Color
        /// <summary>
        /// 图标的颜色和背景
        /// </summary>
        Brush IconFgNormal { get; set; }
        Brush IconFgOver { get; set; }
        Brush IconFgPressed { get; set; }
        Brush IconFgDisabled { get; set; }
        Brush IconBgNormal { get; set; }
        Brush IconBgOver { get; set; }
        Brush IconBgPressed { get; set; }
        Brush IconBgDisabled { get; set; }
        #endregion

        #endregion
    }

    internal interface IFontLabelExtension {
        #region Label
        /// <summary>
        /// 标签的可见性
        /// </summary>
        Visibility LabelVisibility { get; set; }

        /// <summary>
        /// 标签内容
        /// </summary>
        String LabelString { get; set; }

        /// <summary>
        /// 标签的字体大小
        /// </summary>
        Double LabelFontSize { get; set; }

        /// <summary>
        /// 标签字重
        /// </summary>
        FontWeight LabelFontWeight { get; set; }

        /// <summary>
        /// 标签的字体
        /// </summary>
        FontFamily LabelFontFamily { get; set; }


        #region Alignment
        Thickness LabelMargin { get; set; }
        HorizontalAlignment LabelHorizontalAlignment { get; set; }
        VerticalAlignment LabelVerticalAlignment { get; set; }
        TextAlignment LabelTextAlignment { get; set; }
        #endregion

        #region Color
        /// <summary>
        /// 标签的前景和背景
        /// </summary>
        Brush LabelFgNormal { get; set; }
        Brush LabelFgOver { get; set; }
        Brush LabelFgPressed { get; set; }
        Brush LabelFgDisabled { get; set; }
        Brush LabelBgNormal { get; set; }
        Brush LabelBgOver { get; set; }
        Brush LabelBgPressed { get; set; }
        Brush LabelBgDisabled { get; set; }
        #endregion

        #endregion
    }

    internal interface IFontIconButton : IFontLabelExtension, IFontIconExtension {
        /// <summary>
        /// 设置图标与标签如何排列
        /// </summary>
        LocateMode IconLayout { get; set; }

        #region ToolTip
        /// <summary>
        /// 鼠标提示样式
        /// </summary>
        Style ToolTipStyle { get; set; }

        /// <summary>
        /// 鼠标提示中默认文本控件的样式
        /// </summary>
        Style ToolTipTextStyle { get; set; }

        /// <summary>
        /// 鼠标提示内容
        /// </summary>
        string ToolTipString { get; set; }
        #endregion
    }

    /// <summary>
    /// 使用字体图标的按钮
    /// </summary>
    public class FIconButton : Button, IFontIconButton {

        #region Properties
        protected virtual bool _initpopupmenu { get; set; }

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
                typeof(FIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)),
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
                typeof(FIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
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
                typeof(FIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)),
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
                typeof(FIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)),
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

        /// <summary>
        /// 左键弹出菜单
        /// </summary>
        public Popup PopupMenu {
            get { return (Popup)GetValue(PopupMenuProperty); }
            set { SetValue(PopupMenuProperty, value); }
        }

        public static readonly DependencyProperty PopupMenuProperty =
            DependencyProperty.Register("PopupMenu", typeof(Popup),
                typeof(FIconButton), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, OnPopupMenuChnaged));
        private static void OnPopupMenuChnaged(DependencyObject sender, DependencyPropertyChangedEventArgs args) {
            if (args.NewValue != null)
                ((FIconButton)sender).InitPopupMenu();
        }
        #endregion

        #region Method
        /// <summary>
        /// 初始化PopupMenu
        /// </summary>
        protected virtual void InitPopupMenu() {
            if (IsInitialized)
                PopupMenuBinding();
            else
                _initpopupmenu = true;
        }

        public override void EndInit() {
            base.EndInit();
            if (_initpopupmenu)
                PopupMenuBinding();
        }

        /// <summary>
        /// 将PopupMenu的属性与按钮关联
        /// </summary>
        protected virtual void PopupMenuBinding() {
            PopupMenu.PlacementTarget = this;
        }

        /// <summary>
        /// 打开与之关联的ContextMenu
        /// </summary>
        protected virtual void ShowPopupMenu() {
            if (PopupMenu != null) {
                if (PopupMenu.IsOpen)
                    return;
                PopupMenu.IsOpen = true;
            }
        }

        protected override void OnClick() {
            ShowPopupMenu();
            base.OnClick();
        }
        #endregion

        #region Constructor
        static FIconButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FIconButton), new FrameworkPropertyMetadata(typeof(FIconButton), FrameworkPropertyMetadataOptions.Inherits));
        }
        #endregion
    }

}
