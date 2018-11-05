using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace YControls.FlowControls {
    public class YT_ContextMenu : ContextMenu {
        #region Properties
        #endregion

        #region Methods
        #endregion

        #region Constructors
        static YT_ContextMenu() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_ContextMenu), new FrameworkPropertyMetadata(typeof(YT_ContextMenu)));
        }
        #endregion
    }

}
