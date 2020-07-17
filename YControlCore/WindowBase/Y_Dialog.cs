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
    [Flags]
    public enum DialogButton {
        /// <summary>
        /// 只有取消按钮
        /// </summary>
        Cancle,
        /// <summary>
        /// 只有确认按钮
        /// </summary>
        Yes,
        /// <summary>
        /// 只有否认按钮
        /// </summary>
        No
    }

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
                new FrameworkPropertyMetadata((typeof(Y_Dialog), FrameworkPropertyMetadataOptions.Inherits)));
        }
        #endregion
    }
}
