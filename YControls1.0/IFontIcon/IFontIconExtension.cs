using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using YControls.FontIconButtons;

namespace YControls.IFontIcon {
    /// <summary>
    /// 这个接口用于扩展WPF Button控件中的颜色属性用于自定义模板
    /// </summary>
    internal interface IFontIconExtension
    {
        #region Icon
        /// <summary>
        /// 图标的可见性
        /// </summary>
        Visibility IconVisibility { get; set; }


        #region Alignment
        Thickness IconMargin { get; set; }
        HorizontalAlignment IconHorizontalAlignment { get; set; }
        VerticalAlignment IconVerticalAlignment { get; set; }
        TextAlignment IconTextAlignment { get; set; }
        #endregion

        #region Color
        /// <summary>
        /// 图标的颜色和背景
        /// </summary>
        Brush IconFgNormal { get; set; }
        Brush IconFgOver { get; set; }
        Brush IconFgPressed { get; set; }
        Brush IconBgNormal { get; set; }
        Brush IconBgOver { get; set; }
        Brush IconBgPressed { get; set; }
        #endregion

        #endregion
    }
}
