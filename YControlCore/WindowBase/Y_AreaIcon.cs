///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

using Control = System.Windows.Controls.Control;
using ContextMenu = System.Windows.Controls.ContextMenu;
using static YControlCore.WindowBase.Utils;
using System.ComponentModel;

namespace YControlCore.WindowBase {

    public class Y_AreaIcon : Control ,IDisposable{

        #region Properties
        private NotifyIcon _holder;

        private IntPtr _ico = IntPtr.Zero;


        /// <summary>
        /// 将显示在托盘区的图标
        /// </summary>
        public Icon Icon {
            get => _holder.Icon;
            set => _holder.Icon = value;
        }


        /// <summary>
        /// 将显示在托盘区的通知提示
        /// </summary>
        public string Text {
            get => _holder.Text;
            set => _holder.Text = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool EnableDefaultMouseEvent = true;

        /// <summary>
        /// 托盘图标的裁剪矩形
        /// </summary>
        private Rectangle _flowiconloc;


        #region depProperties
        /// <summary>
        /// 此托盘图标的标识
        /// </summary>
        public string HolderID {
            get { return (string)GetValue(HolderIDProperty); }
            set { SetValue(HolderIDProperty, value); }
        }
        public static readonly DependencyProperty HolderIDProperty =
            DependencyProperty.Register("HolderID", typeof(string),
                typeof(Y_AreaIcon), new PropertyMetadata(null));


        /// <summary>
        /// 图标的可见性
        /// </summary>
        public new bool Visibility {
            get { return (bool)GetValue(VisibilityProperty); }
            set { SetValue(VisibilityProperty, value); }
        }
        public static new readonly DependencyProperty VisibilityProperty =
            DependencyProperty.Register("Visibility", typeof(bool), typeof(Y_AreaIcon),
                new PropertyMetadata(false, new PropertyChangedCallback(OnAreaVisibilityChanged)));
        private static void OnAreaVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            Y_AreaIcon dai = (Y_AreaIcon)d;
            dai._holder.Visible = (bool)e.NewValue;
        }

        /// <summary>
        /// 与图标左键关联的弹出浮窗
        /// </summary>
        public Popup CheckPop {
            get { return (Popup)GetValue(CheckPopProperty); }
            set { SetValue(CheckPopProperty, value); }
        }
        public static readonly DependencyProperty CheckPopProperty =
            DependencyProperty.Register("CheckPop", typeof(Popup),
                typeof(Y_AreaIcon), new PropertyMetadata(null));

        /// <summary>
        /// 与图标关联的右键菜单
        /// </summary>
        public new ContextMenu ContextMenu {
            get { return (ContextMenu)GetValue(ContextMenuProperty); }
            set { SetValue(ContextMenuProperty, value); }
        }
        public new static readonly DependencyProperty ContextMenuProperty =
         DependencyProperty.Register("Contextmenu", typeof(ContextMenu),
             typeof(Y_AreaIcon), new PropertyMetadata(null, new PropertyChangedCallback(ContextMenuChanged)));
        private static void ContextMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            Y_AreaIcon aic = (Y_AreaIcon)d;
            if (e.NewValue != null && e.NewValue is ContextMenu menu) {
                if (menu.Placement != PlacementMode.Mouse && menu.Placement != PlacementMode.MousePoint) {
                    aic._flowiconloc = GetIconRect(aic._holder);
                    menu.HorizontalOffset = aic._flowiconloc.X;
                    menu.VerticalOffset = aic._flowiconloc.Y;
                }
            }
        }

        /// <summary>
        /// 托盘图标鼠标移动事件
        /// </summary>
        public event MouseEventHandler IconMouseMove {
            add => _holder.MouseMove += value;
            remove => _holder.MouseMove -= value;
        }

        /// <summary>
        /// 托盘图标鼠标单击事件
        /// </summary>
        public event MouseEventHandler IconMouseClick {
            add => _holder.MouseMove += value;
            remove => _holder.MouseMove -= value;
        }

        /// <summary>
        /// 托盘图标鼠标双击事件
        /// </summary>
        public event MouseEventHandler IconMouseDoubleClick {
            add => _holder.MouseMove += value;
            remove => _holder.MouseMove -= value;
        }
        #endregion

        #endregion

        #region Methods
        /// <summary>
        /// 弹出提示
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="tiptitle"></param>
        /// <param name="tiptext"></param>
        /// <param name="icon"></param>
        public void ShowBalloonTip(int timeout, string tiptitle, string tiptext, ToolTipIcon icon) {
            _holder.ShowBalloonTip(timeout, tiptitle, tiptext, icon);
        }

        private void Y_AreaIcon_Loaded(object sender, RoutedEventArgs e) {
            _holder = new NotifyIcon {
                Visible = true,
                ContextMenuStrip = new ContextMenuStrip(),
            };
            IconMouseClick += Y_AreaIcon_IconMouseClick;
        }

        private void TestLocation() {
            if (ContextMenu.Placement != PlacementMode.Mouse && ContextMenu.Placement != PlacementMode.MousePoint) {
                Rectangle pos = GetIconRect(_holder);
                if (!pos.Equals(_flowiconloc)) {
                    _flowiconloc = pos;
                    ContextMenu.HorizontalOffset = _flowiconloc.X;
                    ContextMenu.VerticalOffset = _flowiconloc.Y;
                }
            }
        }

        private void Y_AreaIcon_IconMouseClick(object sender, MouseEventArgs e) {
            if (EnableDefaultMouseEvent)
                switch (e.Button) {
                    case MouseButtons.Right:
                        if (ContextMenu is null)
                            return;
                        if (ContextMenu.IsOpen)
                            return;
                        TestLocation();
                        ContextMenu.IsOpen = true;
                        break;
                    case MouseButtons.Left:
                        if (CheckPop is null)
                            return;
                        if (CheckPop.IsOpen)
                            return;
                        CheckPop.IsOpen = true;
                        break;
                }
        }

        public void Dispose() {
            ((IDisposable)_holder).Dispose();
        }
        #endregion

        #region Constructors

        ~Y_AreaIcon() {
            Dispose();
        }

        public Y_AreaIcon() {
            base.Visibility = System.Windows.Visibility.Collapsed;
            RenderSize = System.Windows.Size.Empty;
            Loaded += Y_AreaIcon_Loaded;
    
        }
        #endregion
    }
}
