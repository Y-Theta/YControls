using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace YControls.FlowControls {
    public class YT_CommandBar : ContentControl {
        #region Properties
        #endregion

        #region Methods
        #endregion

        #region Constructors
        static YT_CommandBar() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_CommandBar), new FrameworkPropertyMetadata(typeof(YT_CommandBar), FrameworkPropertyMetadataOptions.Inherits));
        }
        #endregion
    }

}
