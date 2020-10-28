///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;


namespace YControlCore.WindowBase {

    /// <summary>
    /// 
    /// </summary>
    public class Y_Dialog : Window {
        #region Properties


        #endregion

        #region Methods
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            
        }
        #endregion

        #region Constructors
        public Y_Dialog() {

        }

        static Y_Dialog() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Y_Dialog),
                new FrameworkPropertyMetadata(typeof(Y_Dialog), FrameworkPropertyMetadataOptions.Inherits));
        }
        #endregion
    }
}
