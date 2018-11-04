using System.Windows;
using YControls.IFontIcon;

namespace YControls.FontIconButtons {
    internal interface IFontIconButton : IFontLabelExtension , IFontIconExtension {
        /// <summary>
        /// 设置图标与标签如何排列
        /// </summary>
        LocateMode IconLayout { get; set; }

        #region ToolTip
        /// <summary>
        /// 鼠标提示样式
        /// </summary>
        Style ToolTipStyle { get; set; }

        /// <summary>
        /// 鼠标提示中默认文本控件的样式
        /// </summary>
        Style ToolTipTextStyle { get; set; }

        /// <summary>
        /// 鼠标提示内容
        /// </summary>
        string ToolTipString { get; set; }
        #endregion
    }
}
