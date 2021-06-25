using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using YControlCore.UIContract;

namespace YControlCore.WindowBase {
    /// <summary>
    /// new class
    /// </summary>
    public class FrameControl  {

        #region     Props & Fields

        public static readonly DependencyProperty ControllerProperty =
            DependencyProperty.RegisterAttached("Controller", typeof(IFrameController), typeof(FrameControl),
                new PropertyMetadata(null, OnControllerChanged));

        #endregion

        #region     Methods

        private static void OnControllerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if(d is Frame frame && e.NewValue is IFrameController controller) {
                controller.Holder = frame;
            }
        }

        public static void SetController(Frame holder, IFrameController controller) {
            if (controller == null) return;
            controller.Holder = holder;
        }
        #endregion

        #region     Constructors

        #endregion

        #region     Interface Impl
        #endregion
    }
}
