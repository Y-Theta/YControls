///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace YControlCore.Base {


    /// <summary>
    /// 命令行为回调
    /// </summary>
    /// <param name="sender">发送者</param>
    /// <param name="args">回调参数</param>
    public delegate void CommandActionEventHandler(object sender, CommandArgs args);

    /// <summary>
    /// 
    /// </summary>
    public class CommandArgs : EventArgs {
        #region Properties
        /// <summary>
        /// 命令参数
        /// </summary>
        public object Parameter { get; set; }

        /// <summary>
        /// 命令标识
        /// </summary>
        public string Command { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// 命令回调参数
        /// </summary>
        /// <param name="para">命令的参数</param>
        /// <param name="str">命令标识</param>
        public CommandArgs(object para, string str = null) {
            Parameter = para;
            Command = str;
        }
        #endregion
    }

    /// <summary>
    /// 用于将一些通用设置
    /// </summary>
    public class CommandBase : ICommand {
        #region Properties
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 
        /// </summary>
        private bool _async;
        public bool Async {
            get => _async;
        }

        /// <summary>
        /// Command执行回调
        /// </summary>
        private Func<object, bool> _preparation;
        /// <summary>
        /// Command 准备回调
        /// </summary>
        /// <exception cref="ArgumentNullException" > command 初始化时出现问题 </exception>
        public event Func<object, bool> Preparation {
            add {
                _preparation += value;
            }
            remove => _preparation -= value;
        }

        /// <summary>
        /// Command执行回调
        /// </summary>
        private Action<object> _execution;
        private Action<IAsyncResult> _executecomplete;
        /// <summary>
        /// Command执行回调
        /// </summary>
        public event Action<object> Execution {
            add => _execution += value;
            remove => _execution -= value;
        }

        #endregion

        #region Methods
        protected void InvokeCanExecuteChange() {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter) {
            bool? ret = _preparation?.Invoke(parameter);
            return ret != false;
        }

        public void Execute(object parameter) {
            if (_async) {
                _execution.BeginInvoke(parameter, new AsyncCallback(_executecomplete), null);
            } else {
                _execution.Invoke(parameter);
            }
        }
        #endregion

        #region Constructors

        protected CommandBase(Action<object> handler, Func<object, bool> check = null, Action<IAsyncResult> callback = null, bool async = false) {
            _execution = handler;
            _preparation = check;
            _executecomplete = callback;
            _async = async;

        }

        public static CommandBase NewAsync(Action<object> handler, Action<IAsyncResult> callback = null, Func<object, bool> check = null)
            => new CommandBase(handler, check, callback, true);


        public static CommandBase New(Action<object> handler, Func<object, bool> check = null)
            => new CommandBase(handler, check, null, false);
        #endregion
    }
}
