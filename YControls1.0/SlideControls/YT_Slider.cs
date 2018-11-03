using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace YControls.SlideControls {
    public class YT_Slider : Slider {
        #region Properties
        #endregion

        #region Methods
        #endregion

        #region Constructors
        static YT_Slider() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_Slider), new FrameworkPropertyMetadata(typeof(YT_Slider)));
        }
        #endregion
    }

}
