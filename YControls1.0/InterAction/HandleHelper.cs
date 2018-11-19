using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace YControls.InterAction {
    /// <summary>
    /// 获得控件句柄以支持互操作
    /// </summary>
    public class HandleHelper {
      /// <summary>
      /// 获得控件句柄
      /// </summary>
        public static IntPtr GetVisualHandle(Visual visual) {
            return ((HwndSource)PresentationSource.FromVisual(visual)).Handle;
        }

    }

}
