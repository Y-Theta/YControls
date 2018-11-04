using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace YControls.IFontIcon {
    /// <summary>
    /// 为FontIcon的ToggleButton和RadioButton添加颜色选项
    /// </summary>
    interface IToggleBrush
    {
        #region Icon
        string IconSelect { get; set; }

        Brush IconSelectFgNormal { get; set; }
        Brush IconSelectFgOver { get; set; }
        Brush IconSelectFgPressed { get; set; }
        Brush IconSelectFgDisabled { get; set; }
        Brush IconSelectBgNormal { get; set; }
        Brush IconSelectBgOver { get; set; }
        Brush IconSelectBgPressed { get; set; }
        Brush IconSelectBgDisabled { get; set; }

        #endregion

        #region Label
        string LabelSelect { get; set; }

        Brush LabelSelectFgNormal { get; set; }
        Brush LabelSelectFgOver { get; set; }
        Brush LabelSelectFgPressed { get; set; }
        Brush LabelSelectFgDisabled { get; set; }
        Brush LabelSelectBgNormal { get; set; }
        Brush LabelSelectBgOver { get; set; }
        Brush LabelSelectBgPressed { get; set; }
        Brush LabelSelectBgDisabled { get; set; }

        #endregion
    }
}
