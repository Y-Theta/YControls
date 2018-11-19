﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using YControls.WinAPI;

namespace YControls.AreaIconWindow {
    public class YT_SizeBorder : Border {
        private int resDirection;
        public int ResDirection {
            get { return resDirection; }
            set { resDirection = value; }
        }

        #region DirectionDictionary
        private Dictionary<int, Cursor> cursors = new Dictionary<int, Cursor>
        {
            {4,Cursors.SizeNWSE},//"LeftTop"
            {8,Cursors.SizeNWSE},//"RightBottom"
            {3,Cursors.SizeNS },//"Top"
            {6,Cursors.SizeNS },//"Bottom"
            {5,Cursors.SizeNESW },//"RightTop"
            {7,Cursors.SizeNESW },//"LeftBottom"
            {1,Cursors.SizeWE },//"Left"
            {2,Cursors.SizeWE }//"Right"
        };
        #endregion

        public Window AttachedWindow {
            get { return (Window)GetValue(AttachedWindowProperty); }
            set { SetValue(AttachedWindowProperty, value); }
        }
        public static readonly DependencyProperty AttachedWindowProperty =
            DependencyProperty.Register("AttachedWindow", typeof(Window),
                typeof(YT_SizeBorder), new FrameworkPropertyMetadata(null));

        private void GetRootWindow() {
            if (AttachedWindow is null) {
                DependencyObject root = this;
                while (!(root is Window)) {
                    if (root is null)
                        break;
                    root = VisualTreeHelper.GetParent(root);
                }
                AttachedWindow = root as Window;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            GetRootWindow();
            if (AttachedWindow.ResizeMode.Equals(ResizeMode.CanResize)) {
                base.OnMouseMove(e);
                Cursor = Cursors.Arrow;
                double TK = this.BorderThickness.Bottom;
                if (TK < 3) TK = 3;
                double Hig = this.ActualHeight;
                double Wid = this.ActualWidth;
                Point p = Mouse.GetPosition(e.Source as FrameworkElement);

                resDirection = p.X <= TK && p.Y <= TK ? 4 :
                                p.X <= TK && p.Y <= Hig - TK ? 1 :
                                p.X <= TK && p.Y >= Hig - TK ? 7 :
                                p.X >= Wid - TK && p.Y <= TK ? 5 :
                                p.X >= Wid - TK && p.Y <= Hig - TK ? 2 :
                                p.X >= Wid - TK && p.Y >= Hig - TK ? 8 :
                                p.X <= Wid - TK && p.X >= TK && p.Y <= TK ? 3 :
                                p.X <= Wid - TK && p.X >= TK && p.Y >= Hig - TK ? 6 :0;

                if (AttachedWindow.WindowState != WindowState.Maximized)
                    Cursor = cursors[ResDirection];
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            GetRootWindow();
            if (AttachedWindow.ResizeMode.Equals(ResizeMode.CanResize))
                DllImportMethods.SendMessage(new WindowInteropHelper(AttachedWindow as Window).Handle, 0x112, (IntPtr)(61440 + resDirection), IntPtr.Zero);
        }

        public YT_SizeBorder() {

        }

        static YT_SizeBorder() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_SizeBorder), new FrameworkPropertyMetadata(typeof(YT_SizeBorder)));
        }
    }


}
