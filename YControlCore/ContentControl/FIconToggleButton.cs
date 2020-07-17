///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using YControlCore.ControlBase;

namespace YControlCore.ContentControl {

    /// <summary>
    /// 为FontIcon的ToggleButton和RadioButton添加颜色选项
    /// </summary>
    interface IToggleBrush {
        #region Icon
        string IconSelect { get; set; }

        Brush IconSelectFgNormal { get; set; }
        Brush IconSelectFgOver { get; set; }
        Brush IconSelectFgPressed { get; set; }
        Brush IconSelectFgDisabled { get; set; }
        Brush IconSelectBgNormal { get; set; }
        Brush IconSelectBgOver { get; set; }
        Brush IconSelectBgPressed { get; set; }
        Brush IconSelectBgDisabled { get; set; }

        #endregion

        #region Label
        string LabelSelect { get; set; }

        Brush LabelSelectFgNormal { get; set; }
        Brush LabelSelectFgOver { get; set; }
        Brush LabelSelectFgPressed { get; set; }
        Brush LabelSelectFgDisabled { get; set; }
        Brush LabelSelectBgNormal { get; set; }
        Brush LabelSelectBgOver { get; set; }
        Brush LabelSelectBgPressed { get; set; }
        Brush LabelSelectBgDisabled { get; set; }

        #endregion
    }

    [DefaultEvent("Checked")]
    public class FIconToggleButton : FIconButton, IToggleBrush {

        #region Properties

        #region Icon
        public new string Content {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public new static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public string IconSelect {
            get { return (string)GetValue(IconSelectProperty); }
            set { SetValue(IconSelectProperty, value); }
        }
        public static readonly DependencyProperty IconSelectProperty =
            DependencyProperty.Register("IconSelect", typeof(string),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        #region Color
        public Brush IconSelectFgNormal {
            get { return (Brush)GetValue(IconSelectFgNormalProperty); }
            set { SetValue(IconSelectFgNormalProperty, value); }
        }
        public static readonly DependencyProperty IconSelectFgNormalProperty =
            DependencyProperty.Register("IconSelectFgNormal", typeof(Brush),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectFgOver {
            get { return (Brush)GetValue(IconSelectFgOverProperty); }
            set { SetValue(IconSelectFgOverProperty, value); }
        }
        public static readonly DependencyProperty IconSelectFgOverProperty =
            DependencyProperty.Register("IconSelectFgOver", typeof(Brush),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectFgPressed {
            get { return (Brush)GetValue(IconSelectFgPressedProperty); }
            set { SetValue(IconSelectFgPressedProperty, value); }
        }
        public static readonly DependencyProperty IconSelectFgPressedProperty =
            DependencyProperty.Register("IconSelectFgPressed", typeof(Brush),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectFgDisabled {
            get { return (Brush)GetValue(IconSelectFgDisabledProperty); }
            set { SetValue(IconSelectFgDisabledProperty, value); }
        }
        public static readonly DependencyProperty IconSelectFgDisabledProperty =
            DependencyProperty.Register("IconSelectFgDisabled", typeof(Brush),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectBgNormal {
            get { return (Brush)GetValue(IconSelectBgNormalProperty); }
            set { SetValue(IconSelectBgNormalProperty, value); }
        }
        public static readonly DependencyProperty IconSelectBgNormalProperty =
            DependencyProperty.Register("IconSelectBgNormal", typeof(Brush),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectBgOver {
            get { return (Brush)GetValue(IconSelectBgOverProperty); }
            set { SetValue(IconSelectBgOverProperty, value); }
        }
        public static readonly DependencyProperty IconSelectBgOverProperty =
            DependencyProperty.Register("IconSelectBgOver", typeof(Brush),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 32, 32, 32)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectBgPressed {
            get { return (Brush)GetValue(IconSelectBgPressedProperty); }
            set { SetValue(IconSelectBgPressedProperty, value); }
        }
        public static readonly DependencyProperty IconSelectBgPressedProperty =
            DependencyProperty.Register("IconSelectBgPressed", typeof(Brush),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 16, 16, 16)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectBgDisabled {
            get { return (Brush)GetValue(IconSelectBgDisabledProperty); }
            set { SetValue(IconSelectBgDisabledProperty, value); }
        }
        public static readonly DependencyProperty IconSelectBgDisabledProperty =
            DependencyProperty.Register("IconSelectBgDisabled", typeof(Brush),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)),
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
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        #region Color
        public Brush LabelSelectFgNormal {
            get { return (Brush)GetValue(LabelSelectFgNormalProperty); }
            set { SetValue(LabelSelectFgNormalProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectFgNormalProperty =
            DependencyProperty.Register("LabelSelectFgNormal", typeof(Brush),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectFgOver {
            get { return (Brush)GetValue(LabelSelectFgOverProperty); }
            set { SetValue(LabelSelectFgOverProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectFgOverProperty =
            DependencyProperty.Register("LabelSelectFgOver", typeof(Brush),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectFgPressed {
            get { return (Brush)GetValue(LabelSelectFgPressedProperty); }
            set { SetValue(LabelSelectFgPressedProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectFgPressedProperty =
            DependencyProperty.Register("LabelSelectFgPressed", typeof(Brush),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectFgDisabled {
            get { return (Brush)GetValue(LabelSelectFgDisabledProperty); }
            set { SetValue(LabelSelectFgDisabledProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectFgDisabledProperty =
            DependencyProperty.Register("LabelSelectFgDisabled", typeof(Brush),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectBgNormal {
            get { return (Brush)GetValue(LabelSelectBgNormalProperty); }
            set { SetValue(LabelSelectBgNormalProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectBgNormalProperty =
            DependencyProperty.Register("LabelSelectBgNormal", typeof(Brush),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectBgOver {
            get { return (Brush)GetValue(LabelSelectBgOverProperty); }
            set { SetValue(LabelSelectBgOverProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectBgOverProperty =
            DependencyProperty.Register("LabelSelectBgOver", typeof(Brush),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 32, 32, 32)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectBgPressed {
            get { return (Brush)GetValue(LabelSelectBgPressedProperty); }
            set { SetValue(LabelSelectBgPressedProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectBgPressedProperty =
            DependencyProperty.Register("LabelSelectBgPressed", typeof(Brush),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 16, 16, 16)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectBgDisabled {
            get { return (Brush)GetValue(LabelSelectBgDisabledProperty); }
            set { SetValue(LabelSelectBgDisabledProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectBgDisabledProperty =
            DependencyProperty.Register("LabelSelectBgDisabled", typeof(Brush),
                typeof(FIconToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)),
                    FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #endregion

        [Category("Appearance")]
        [TypeConverter(typeof(NullableBoolConverter))]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public bool? IsChecked {
            get {
                object value = GetValue(IsCheckedProperty);
                if (value == null)
                    return new bool?();
                else
                    return new bool?((bool)value);
            }
            set { SetValue(IsCheckedProperty, value.HasValue ? value : null); }
        }
        public static readonly DependencyProperty IsCheckedProperty =
                DependencyProperty.Register(
                        "IsChecked",
                        typeof(bool?),
                        typeof(FIconToggleButton),
                        new FrameworkPropertyMetadata(
                                false,
                                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                                new PropertyChangedCallback(OnIsCheckedChanged)));

        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            FIconToggleButton button = (FIconToggleButton)d;
            bool? oldValue = (bool?)e.OldValue;
            bool? newValue = (bool?)e.NewValue;

            if (newValue == true) {
                button.OnChecked(new RoutedEventArgs(CheckedEvent));
            } else if (newValue == false) {
                button.OnUnchecked(new RoutedEventArgs(UncheckedEvent));
            } else {
                button.OnIndeterminate(new RoutedEventArgs(IndeterminateEvent));
            }

            button.UpdateVisualState();
        }

        /// <summary>
        /// 设置按钮是否为三态
        /// </summary>
        public static readonly DependencyProperty IsThreeStateProperty =
                DependencyProperty.Register(
                        "IsThreeState",
                        typeof(bool),
                        typeof(FIconToggleButton),
                        new FrameworkPropertyMetadata(false));
        [Bindable(true), Category("Behavior")]
        public bool IsThreeState {
            get { return (bool)GetValue(IsThreeStateProperty); }
            set { SetValue(IsThreeStateProperty, value); }
        }
        #endregion

        #region Events
        /// <summary>
        ///     Checked event
        /// </summary>
        public static readonly RoutedEvent CheckedEvent = EventManager.RegisterRoutedEvent("Checked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FIconToggleButton));

        /// <summary>
        ///     Unchecked event
        /// </summary>
        public static readonly RoutedEvent UncheckedEvent = EventManager.RegisterRoutedEvent("Unchecked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FIconToggleButton));

        /// <summary>
        ///     Indeterminate event
        /// </summary>
        public static readonly RoutedEvent IndeterminateEvent = EventManager.RegisterRoutedEvent("Indeterminate", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FIconToggleButton));


        /// <summary>
        ///     Add / Remove Checked handler
        /// </summary>
        [Category("Behavior")]
        public event RoutedEventHandler Checked {
            add { AddHandler(CheckedEvent, value); }
            remove { RemoveHandler(CheckedEvent, value); }
        }

        /// <summary>
        ///     Add / Remove Unchecked handler
        /// </summary>
        [Category("Behavior")]
        public event RoutedEventHandler Unchecked {
            add { AddHandler(UncheckedEvent, value); }
            remove { RemoveHandler(UncheckedEvent, value); }
        }

        /// <summary>
        ///     Add / Remove Indeterminate handler
        /// </summary>
        [Category("Behavior")]
        public event RoutedEventHandler Indeterminate {
            add { AddHandler(IndeterminateEvent, value); }
            remove { RemoveHandler(IndeterminateEvent, value); }
        }
        #endregion

        #region Method
        /// <summary>
        /// 更新视觉效果
        /// </summary>
        private void UpdateVisualState() {
            var update = typeof(Control).GetMethod("UpdateVisualState", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { }, null);
            update.Invoke(this, null);
        }

        protected override void PopupMenuBinding() {
            base.PopupMenuBinding();
            Binding binding = new Binding {
                Source = this,
                Path = new PropertyPath(IsCheckedProperty),
                Mode = BindingMode.TwoWay,
            };
            PopupMenu.SetBinding(ContextMenu.IsOpenProperty, binding);
        }

        /// <summary>
        /// 重写OnClick，定义新的表现形式
        /// </summary>
        protected override void OnClick() {
            OnToggle();
            base.OnClick();
        }

        /// <summary>
        /// 将button的操作置空，将判断移至toggel处
        /// </summary>
        protected override void ShowPopupMenu() { }

        /// <summary>
        /// 设置ToggleButton的状态
        /// </summary>
        protected virtual internal void OnToggle() {
            bool? isChecked;
            if (IsChecked == true)
                isChecked = IsThreeState ? (bool?)null : (bool?)false;
            else
                isChecked = IsChecked.HasValue;
            SetValue(IsCheckedProperty, isChecked);
        }

        /// <summary>
        /// OnChecked事件
        /// </summary>
        protected virtual void OnChecked(RoutedEventArgs e) {
            RaiseEvent(e);
        }

        /// <summary>
        /// OnUnchecked事件
        /// </summary>
        protected virtual void OnUnchecked(RoutedEventArgs e) {
            RaiseEvent(e);
        }

        /// <summary>
        /// OnIndeterminate 事件
        /// </summary>
        protected virtual void OnIndeterminate(RoutedEventArgs e) {
            RaiseEvent(e);
        }


        #endregion

        #region Constructors
        static FIconToggleButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FIconToggleButton), new FrameworkPropertyMetadata(typeof(FIconToggleButton), FrameworkPropertyMetadataOptions.Inherits));
        }
        #endregion
    }
}
