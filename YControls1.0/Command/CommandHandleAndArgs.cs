using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YControls.Command {

    /// <summary>
    /// 命令使能回调
    /// </summary>
    /// <param name="para"></param>
    /// <returns></returns>
    public delegate bool EnableAction(object para = null);

    /// <summary>
    /// 命令执行回调,用于绑定Command
    /// </summary>
    /// <param name="para">命令参数</param>
    public delegate void CommandAction(object para = null);

    /// <summary>
    /// 命令使能回调
    /// </summary>
    public delegate bool EnableEventHandler(object para = null);

    /// <summary>
    /// 命令执行回调,用于绑定Command
    /// </summary>
    public delegate void CommandEventHandler(object para = null);

    /// <summary>
    /// 命令行为回调
    /// </summary>
    /// <param name="sender">发送者</param>
    /// <param name="args">回调参数</param>
    public delegate void CommandActionEventHandler(object sender, CommandArgs args);

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

}
