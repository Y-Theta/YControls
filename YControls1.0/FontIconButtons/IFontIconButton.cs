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
        /// 鼠标提示可见性
        /// </summary>
        Visibility ToolTipVisibility { get; set; }
        /// <summary>
        /// 鼠标提示内容
        /// </summary>
        string ToolTipString { get; set; }
        #endregion
    }
}
