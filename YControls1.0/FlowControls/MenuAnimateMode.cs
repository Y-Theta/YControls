using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YControls.FlowControls {
    /// <summary>
    /// 定义YT_InlineMenuPanel的出现动画
    /// </summary>
    public enum MenuAnimateMode {
        /// <summary>
        /// 无效果
        /// </summary>
        None,
        /// <summary>
        /// 由左侧滑入
        /// </summary>
        Left_Slide,
        /// <summary>
        /// 由右侧滑入
        /// </summary>
        Right_Slide,
        /// <summary>
        /// 由顶部滑入
        /// </summary>
        Top_Slide,
        /// <summary>
        /// 由底部滑入
        /// </summary>
        Bottom_Slide,
        /// <summary>
        /// 淡入淡出
        /// </summary>
        Fade,
    }

}
