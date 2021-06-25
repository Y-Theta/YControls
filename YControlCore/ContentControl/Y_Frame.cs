using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YControlCore.ContentControl
{
    /// <summary>
    /// Frame with no navigate buttons
    /// </summary>
    public class Y_Frame : Frame
    {


        public Y_Frame()
        {
        }


        static Y_Frame()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Y_Frame), new FrameworkPropertyMetadata(typeof(Y_Frame)));
        }
    }
}
