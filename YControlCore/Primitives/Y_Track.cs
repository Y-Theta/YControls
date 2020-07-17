///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace YControlCore.Primitives {


    public class Y_Track :Track{

        #region Properties


        public Style ThumbStyle {
            get { return (Style)GetValue(ThumbStyleProperty); }
            set { SetValue(ThumbStyleProperty, value); }
        }
        public static readonly DependencyProperty ThumbStyleProperty =
            DependencyProperty.Register("ThumbStyle", typeof(Style), 
                typeof(Y_Track), new PropertyMetadata(null));


        #endregion

        #region Methods
        #endregion

        #region Constructors
        static Y_Track() {

        }
        #endregion
    }
}
