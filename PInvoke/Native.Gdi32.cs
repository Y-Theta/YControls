using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PInvoke
{
    public partial class Native
    {
        #region Methods

#if NET45_OR_GREATER
        [DllImport(Native.GDI32)]
        public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, CopyPixelOperation dwRop);
#endif

        [DllImport(Native.GDI32)]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [DllImport(Native.GDI32)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport(Native.GDI32)]
        public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

        [DllImport(Native.GDI32)]
        public static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nReghtRect, int nBottomRect);

        [DllImport(Native.GDI32)]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nReghtRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        [DllImport(Native.GDI32)]
        public static extern bool DeleteDC(IntPtr hDC);

        [DllImport(Native.GDI32)]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport(Native.GDI32)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport(Native.GDI32)]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport(Native.GDI32)]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

#endregion

    }
}
