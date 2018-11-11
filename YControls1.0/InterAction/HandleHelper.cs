using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace YControls.InterAction {
    public class HandleHelper {
      /// <summary>
      /// 获得控件句柄
      /// </summary>
        public static IntPtr GetVisualHandle(Visual visual) {
            return ((HwndSource)PresentationSource.FromVisual(visual)).Handle;
        }

    }

}
