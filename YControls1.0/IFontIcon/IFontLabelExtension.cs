using System;
using System.Windows;
using System.Windows.Media;

namespace YControls.IFontIcon {
    internal interface IFontLabelExtension {
        #region Label
        /// <summary>
        /// 标签的可见性
        /// </summary>
        Visibility LabelVisibility { get; set; }

        /// <summary>
        /// 标签内容
        /// </summary>
        String LabelString { get; set; }

        /// <summary>
        /// 标签的字体大小
        /// </summary>
        Double LabelFontSize { get; set; }

        /// <summary>
        /// 标签字重
        /// </summary>
        FontWeight LabelFontWeight { get; set; }

        /// <summary>
        /// 标签的字体
        /// </summary>
        FontFamily LabelFontFamily { get; set; }


        #region Alignment
        Thickness LabelMargin { get; set; }
        HorizontalAlignment LabelHorizontalAlignment { get; set; }
        VerticalAlignment LabelVerticalAlignment { get; set; }
        TextAlignment LabelTextAlignment { get; set; }
        #endregion

        #region Color
        /// <summary>
        /// 标签的前景和背景
        /// </summary>
        Brush LabelFgNormal { get; set; }
        Brush LabelFgOver { get; set; }
        Brush LabelFgPressed { get; set; }
        Brush LabelFgDisabled { get; set; }
        Brush LabelBgNormal { get; set; }
        Brush LabelBgOver { get; set; }
        Brush LabelBgPressed { get; set; }
        Brush LabelBgDisabled { get; set; }
        #endregion

        #endregion
    }

}
