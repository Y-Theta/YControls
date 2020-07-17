///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace YControlCore.ControlBase {

    /// <summary>
    /// 二态控件
    /// </summary>
    public class ToggleStateControl :Control{
        #region Properties
        /// <summary>
        /// 指示当前状态
        /// </summary>
        public bool IsExpand {
            get { return (bool)GetValue(IsExpandProperty); }
            set { SetValue(IsExpandProperty, value); }
        }
        public static readonly DependencyProperty IsExpandProperty =
            DependencyProperty.Register("IsExpand", typeof(bool), typeof(ToggleStateControl), 
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));


        /// <summary>
        /// 是否使用动画过渡
        /// </summary>
        public bool UseAnimate {
            get { return (bool)GetValue(UseAnimateProperty); }
            set { SetValue(UseAnimateProperty, value); }
        }
        public static readonly DependencyProperty UseAnimateProperty =
            DependencyProperty.Register("UseAnimate", typeof(bool),typeof(ToggleStateControl),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #region Methods
        #endregion

        #region Constructors
        #endregion
    }
}
