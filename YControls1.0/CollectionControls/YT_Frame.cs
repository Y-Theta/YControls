using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using YControls.Command;

namespace YControls.CollectionControls {
    /// <summary>
    /// 自定义Frame 用于将frame的操作以命令的形式封装
    /// </summary>
    public class YT_Frame : Frame {
        #region Properties
        /// <summary>
        /// 导航命令
        /// </summary>
        public CommandBase NavigateCommand {
            get { return (CommandBase)GetValue(NavigateCommandProperty); }
            set { SetValue(NavigateCommandProperty, value); }
        }
        public static readonly DependencyProperty NavigateCommandProperty =
            DependencyProperty.Register("NavigateCommand", typeof(CommandBase),
                typeof(YT_Frame), new FrameworkPropertyMetadata(null, OnNavigateCommandChanged));
        private static void OnNavigateCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((YT_Frame)d).HockEvents();
        }

        /// <summary>
        /// 回退命令
        /// </summary>
        public CommandBase BackCommand {
            get { return (CommandBase)GetValue(BackCommandProperty); }
            set { SetValue(BackCommandProperty, value); }
        }
        public static readonly DependencyProperty BackCommandProperty =
            DependencyProperty.Register("BackCommand", typeof(CommandBase), 
                typeof(YT_Frame), new PropertyMetadata(new CommandBase()));

        /// <summary>
        /// 前进命令
        /// </summary>
        public CommandBase ForeCommand {
            get { return (CommandBase)GetValue(ForeCommandProperty); }
            set { SetValue(ForeCommandProperty, value); }
        }
        public static readonly DependencyProperty ForeCommandProperty =
            DependencyProperty.Register("ForeCommand", typeof(CommandBase),
                typeof(YT_Frame), new PropertyMetadata(new CommandBase()));
        #endregion

        #region Methods

        private void HockEvents() {
            if (NavigateCommand != null) {
                NavigateCommand.Execution += obj => {
                    if (obj is NavigateArgs arg) {
                        if (arg is null)
                            return;
                        if (arg.UseUri)
                            Navigate(arg.ContentUri, arg.ExtraData);
                        else
                            Navigate(arg.Content, arg.ExtraData);
                    }
                };
            }
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            BackCommand.Execution += (obj) => {
                if (CanGoBack)
                    GoBack();
            };
            ForeCommand.Execution += (obj) => {
                if (CanGoForward)
                    GoForward();
            };
        }
        #endregion

        #region Constructors
        static YT_Frame() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_Frame), new FrameworkPropertyMetadata(typeof(YT_Frame)));
        }
        #endregion
    }

}
