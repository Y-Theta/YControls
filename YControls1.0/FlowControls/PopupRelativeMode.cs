using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YControls.FlowControls {
    /// <summary>
    /// Popup的弹出原点相对于目标的位置
    /// </summary>
    public enum PopupRelativeMode {
        /// <summary>
        /// 原点在目标左上角
        /// </summary>
        LeftTop,
        /// <summary>
        /// 原点在目标左下角
        /// </summary>
        LeftBottom,
        /// <summary>
        /// 原点在目标右上角
        /// </summary>
        RightTop,
        /// <summary>
        /// 原点在目标右下角
        /// </summary>
        RightBottom,
        /// <summary>
        /// 放置在目标的居中位置
        /// </summary>
        CenterTarget,
        /// <summary>
        /// 放置在目标的下方居中
        /// </summary>
        BottomTarget,
        /// <summary>
        /// 放置在目标的上方居中
        /// </summary>
        TopTarget,
    }

}
