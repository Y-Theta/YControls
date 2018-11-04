using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YControls.TextControls {
    /// <summary>
    /// 输入框的文本提交方式
    /// </summary>
    public enum SubmitMode {
        /// <summary>
        /// 通过特殊的按键提交 如回车键
        /// </summary>
        SpecialKey,
        /// <summary>
        /// 当输入框内容变化时提交
        /// </summary>
        WordChanged,
        /// <summary>
        /// 不提交文本 单向绑定
        /// </summary>
        None
    }

}
