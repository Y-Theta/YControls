using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using YControls.Command;

namespace YControls.Dialogs {
    /// <summary>
    /// 自定义对话框类型
    /// </summary>
    public class YT_DialogBase : Window {
        #region Properties
        /// <summary>
        /// 控件布局方式
        /// </summary>
        public DialogBasicStyle ControlLayout {
            get { return (DialogBasicStyle)GetValue(ControlLayoutProperty); }
            set { SetValue(ControlLayoutProperty, value); }
        }
        public static readonly DependencyProperty ControlLayoutProperty =
            DependencyProperty.Register("ControlLayout", typeof(DialogBasicStyle),
                typeof(YT_DialogBase), new PropertyMetadata(DialogBasicStyle.ButtonBottomRight));

        #region ButtonUI

        public Visibility YesButtonVisibility {
            get { return (Visibility)GetValue(YesButtonVisibilityProperty); }
            set { SetValue(YesButtonVisibilityProperty, value); }
        }
        public static readonly DependencyProperty YesButtonVisibilityProperty =
            DependencyProperty.Register("YesButtonVisibility", typeof(Visibility),
                typeof(YT_DialogBase), new FrameworkPropertyMetadata(Visibility.Visible,
                    FrameworkPropertyMetadataOptions.Inherits));

        public Visibility NoButtonVisibility {
            get { return (Visibility)GetValue(NoButtonVisibilityProperty); }
            set { SetValue(NoButtonVisibilityProperty, value); }
        }
        public static readonly DependencyProperty NoButtonVisibilityProperty =
            DependencyProperty.Register("NoButtonVisibility", typeof(Visibility),
                typeof(YT_DialogBase), new FrameworkPropertyMetadata(Visibility.Visible,
                    FrameworkPropertyMetadataOptions.Inherits));

        public Visibility CancelButtonVisibility {
            get { return (Visibility)GetValue(CancelButtonVisibilityProperty); }
            set { SetValue(CancelButtonVisibilityProperty, value); }
        }
        public static readonly DependencyProperty CancelButtonVisibilityProperty =
            DependencyProperty.Register("CancelButtonVisibility", typeof(Visibility),
                typeof(YT_DialogBase), new FrameworkPropertyMetadata(Visibility.Visible,
                    FrameworkPropertyMetadataOptions.Inherits));

        public string YesButtonIcon {
            get { return (string)GetValue(YesButtonIconProperty); }
            set { SetValue(YesButtonIconProperty, value); }
        }
        public static readonly DependencyProperty YesButtonIconProperty =
            DependencyProperty.Register("YesButtonIcon", typeof(string), 
                typeof(YT_DialogBase), new FrameworkPropertyMetadata("", 
                    FrameworkPropertyMetadataOptions.Inherits));

        public string YesButtonLabel {
            get { return (string)GetValue(YesButtonLabelProperty); }
            set { SetValue(YesButtonLabelProperty, value); }
        }
        public static readonly DependencyProperty YesButtonLabelProperty =
            DependencyProperty.Register("YesButtonLabel", typeof(string), 
                typeof(YT_DialogBase), new FrameworkPropertyMetadata("YES",
                    FrameworkPropertyMetadataOptions.Inherits));

        public string NoButtonIcon {
            get { return (string)GetValue(NoButtonIconProperty); }
            set { SetValue(NoButtonIconProperty, value); }
        }
        public static readonly DependencyProperty NoButtonIconProperty =
            DependencyProperty.Register("NoButtonIcon", typeof(string),
                typeof(YT_DialogBase), new FrameworkPropertyMetadata("",
                    FrameworkPropertyMetadataOptions.Inherits));

        public string NoButtonLabel {
            get { return (string)GetValue(NoButtonLabelProperty); }
            set { SetValue(NoButtonLabelProperty, value); }
        }
        public static readonly DependencyProperty NoButtonLabelProperty =
            DependencyProperty.Register("NoButtonLabel", typeof(string),
                typeof(YT_DialogBase), new FrameworkPropertyMetadata("No",
                    FrameworkPropertyMetadataOptions.Inherits));

        public Style BottomButtonStyle {
            get { return (Style)GetValue(BottomButtonStyleProperty); }
            set { SetValue(BottomButtonStyleProperty, value); }
        }
        public static readonly DependencyProperty BottomButtonStyleProperty =
            DependencyProperty.Register("BottomButtonStyle", typeof(Style),
                typeof(YT_DialogBase), new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.Inherits));

        public Style CancelButtonStyle {
            get { return (Style)GetValue(CancelButtonStyleProperty); }
            set { SetValue(CancelButtonStyleProperty, value); }
        }
        public static readonly DependencyProperty CancelButtonStyleProperty =
            DependencyProperty.Register("CancelButtonStyle", typeof(Style),
                typeof(YT_DialogBase), new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        public CommandBase CancelCommand { get; set; }

        public CommandBase YesCommand { get; set; }

        public CommandBase NoCommand { get; set; }

        private event CommandAction _yesAction;
        public event CommandAction YesAction {
            add { _yesAction = value; }
            remove { _yesAction -= value; }
        }

        private event CommandAction _noAction;
        public event CommandAction NoAction {
            add { _noAction = value; }
            remove { _noAction -= value; }
        }

        private event CommandAction _cancelAction;
        public event CommandAction CancelAction {
            add { _cancelAction = value; }
            remove { _cancelAction -= value; }
        }
        #endregion

        #region Methods
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            DragMove();
            base.OnMouseLeftButtonDown(e);
        }

        private void InitCommands() {
            CancelCommand = new CommandBase();
            CancelCommand.Execution += CancelCommand_Commandaction;
            YesCommand = new CommandBase();
            YesCommand.Execution += YesCommand_Commandaction;
            NoCommand = new CommandBase();
            NoCommand.Execution += NoCommand_Commandaction;
        }

        protected virtual void NoCommand_Commandaction(object para) {
            DialogResult = false;
            _noAction?.Invoke(para);
            Close();
        }

        protected virtual void YesCommand_Commandaction(object para) {
            DialogResult = true;
            _yesAction?.Invoke(para);
            Close();
        }

        protected virtual void CancelCommand_Commandaction(object para) {
            DialogResult = false;
            _cancelAction?.Invoke(para);
            Close();
        }
        #endregion

        #region Override

        public virtual void ShowDialog(Window Holder) {
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Holder;
            ShowDialog();
        }
        #endregion

        #region Constructors
        public YT_DialogBase() : base() {
            InitCommands();
        }

        static YT_DialogBase() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_DialogBase), new FrameworkPropertyMetadata(typeof(YT_DialogBase), FrameworkPropertyMetadataOptions.Inherits));
        }
        #endregion
    }

}
