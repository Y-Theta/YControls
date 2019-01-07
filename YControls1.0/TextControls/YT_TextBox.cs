using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace YControls.TextControls {
    /// <summary>
    /// 自定义文本输入控件
    /// </summary>
    public class YT_TextBox : TextBox {
        #region Properties

        private TextBlock _placeHolder;

        /// <summary>
        /// 输入这个按键来更新绑定
        /// 同时使文本框失去焦点
        /// </summary>
        public Key SubmitKey {
            get { return (Key)GetValue(SubmitKeyProperty); }
            set { SetValue(SubmitKeyProperty, value); }
        }
        public static readonly DependencyProperty SubmitKeyProperty =
            DependencyProperty.Register("SubmitKey", typeof(Key),
                typeof(YT_TextBox), new FrameworkPropertyMetadata(Key.Enter, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 占位文本,在文本框内容为空时显示
        /// </summary>
        public string PlaceHolderText {
            get { return (string)GetValue(PlaceHolderTextProperty); }
            set { SetValue(PlaceHolderTextProperty, value); }
        }
        public static readonly DependencyProperty PlaceHolderTextProperty =
            DependencyProperty.Register("PlaceHolderText", typeof(string),
                typeof(YT_TextBox), new FrameworkPropertyMetadata("this is a place_holder text", FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 占位文本的颜色
        /// </summary>
        public Brush PlaceHolderTextForeground {
            get { return (Brush)GetValue(PlaceHolderTextForegroundProperty); }
            set { SetValue(PlaceHolderTextForegroundProperty, value); }
        }
        public static readonly DependencyProperty PlaceHolderTextForegroundProperty =
            DependencyProperty.Register("PlaceHolderTextForeground", typeof(Brush),
                typeof(YT_TextBox), new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 文字相对于控件的布局
        /// 用于在不同字体时微调使布局合理
        /// </summary>
        public Thickness TextMargin {
            get { return (Thickness)GetValue(TextMarginProperty); }
            set { SetValue(TextMarginProperty, value); }
        }
        public static readonly DependencyProperty TextMarginProperty =
            DependencyProperty.Register("TextMargin", typeof(Thickness),
                typeof(YT_TextBox), new FrameworkPropertyMetadata(new Thickness(0), FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 占位文本相对于控件的布局
        /// 用于在不同字体时微调使布局合理
        /// </summary>
        public Thickness PlaceHolderMargin {
            get { return (Thickness)GetValue(PlaceHolderMarginProperty); }
            set { SetValue(PlaceHolderMarginProperty, value); }
        }
        public static readonly DependencyProperty PlaceHolderMarginProperty =
            DependencyProperty.Register("PlaceHolderMargin", typeof(Thickness),
                typeof(YT_TextBox), new FrameworkPropertyMetadata(new Thickness(0), FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #region Methods
        protected override void OnKeyDown(KeyEventArgs e) {
            if (e.Key == SubmitKey) {
                if (!GiveupFocus()) {
                    Keyboard.ClearFocus();
                }
            }
            base.OnKeyDown(e);
        }

        /// <summary>
        /// 使焦点转移到父对象
        /// </summary>
        private bool GiveupFocus() {
            FrameworkElement parent = (FrameworkElement)Parent;
            while (parent != null && parent is IInputElement && !((IInputElement)parent).Focusable) {
                parent = (FrameworkElement)parent.Parent;
            }

            DependencyObject scope = FocusManager.GetFocusScope(this);
            FocusManager.SetFocusedElement(scope, parent as IInputElement);

            return !(parent is null);
        }

        /// <summary>
        /// 在输入变化时决定是否显示占位文本
        /// </summary>
        protected override void OnTextChanged(TextChangedEventArgs e) {
            CalculatePlaceHolderVisibility();
            base.OnTextChanged(e);
        }

        /// <summary>
        /// 在失去焦点时决定是否显示占位文本
        /// </summary>
        protected override void OnLostFocus(RoutedEventArgs e) {
            if (string.IsNullOrEmpty(Text))
                _placeHolder.Visibility = Visibility.Visible;
            base.OnLostFocus(e);
        }

        /// <summary>
        /// 获得占位文本控件
        /// </summary>
        public override void OnApplyTemplate() {
            _placeHolder = GetTemplateChild("YT_PlaceHolder") as TextBlock;
            CalculatePlaceHolderVisibility();

            base.OnApplyTemplate();
        }

        private void CalculatePlaceHolderVisibility() {
            if (_placeHolder != null) {
                if (string.IsNullOrEmpty(Text))
                    _placeHolder.Visibility = Visibility.Visible;
                else
                    _placeHolder.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        #region Constructors
        static YT_TextBox() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_TextBox), new FrameworkPropertyMetadata(typeof(YT_TextBox), FrameworkPropertyMetadataOptions.Inherits));
        }
        #endregion
    }

}
