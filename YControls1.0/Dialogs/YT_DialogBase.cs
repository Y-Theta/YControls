using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using YControls.Command;

namespace YControls.Dialogs {
    public class YT_DialogBase :Window {
        #region Properties
        public Visibility YseButtonVisibility {
            get { return (Visibility)GetValue(YseButtonVisibilityProperty); }
            set { SetValue(YseButtonVisibilityProperty, value); }
        }
        public static readonly DependencyProperty YseButtonVisibilityProperty =
            DependencyProperty.Register("YseButtonVisibility", typeof(Visibility),
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

        public CommandBase CancelCommand {
            get { return (CommandBase)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }
        public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register("CancelCommand", typeof(CommandBase),
                typeof(YT_DialogBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public CommandBase YesCommand {
            get { return (CommandBase)GetValue(YesCommandProperty); }
            set { SetValue(YesCommandProperty, value); }
        }
        public static readonly DependencyProperty YesCommandProperty =
            DependencyProperty.Register("YesCommand", typeof(CommandBase),
                typeof(YT_DialogBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public CommandBase NoCommand {
            get { return (CommandBase)GetValue(NoCommandProperty); }
            set { SetValue(NoCommandProperty, value); }
        }
        public static readonly DependencyProperty NoCommandProperty =
            DependencyProperty.Register("NoCommand", typeof(CommandBase),
                typeof(YT_DialogBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));


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
        public YT_DialogBase() {
            InitCommands();
        }

        static YT_DialogBase() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_DialogBase), new FrameworkPropertyMetadata(typeof(YT_DialogBase)));
        }
        #endregion
    }

}
