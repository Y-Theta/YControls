using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace YControls.CollectionControls {
    public class YT_ComboBox : ComboBox {
        #region Properties
        #endregion

        #region Methods
        #endregion

        #region Constructors
        static YT_ComboBox() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_ComboBox), new FrameworkPropertyMetadata(typeof(YT_ComboBox), FrameworkPropertyMetadataOptions.Inherits));
        }
        #endregion
    }

}
