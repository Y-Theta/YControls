using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using YControls.FlowControls;
using ContextMenu = System.Windows.Controls.ContextMenu;
using Control = System.Windows.Controls.Control;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
using MouseEventHandler = System.Windows.Forms.MouseEventHandler;

namespace YControls.AreaIconWindow {

    /// <summary>
    /// 将托盘图标包装成xaml的控件
    /// </summary>
    public class YT_AreaIcon : Control {

        #region Properties

        //NormalProperties
        private NotifyIcon _flowicon;

        private IntPtr _ico = IntPtr.Zero;

        /// <summary>
        /// 将显示在托盘区的图标
        /// </summary>
        public Icon Areaicon {
            get => _flowicon.Icon;
            set => _flowicon.Icon = value;
        }

        #region AttachedWindow
        /// <summary>
        /// 与图标关联的窗体
        /// </summary>
        public object AttachedWindow {
            get { return (object)GetValue(AttachedWindowProperty); }
            set { SetValue(AttachedWindowProperty, value); }
        }
        public static readonly DependencyProperty AttachedWindowProperty =
            DependencyProperty.Register("AttachedWindow", typeof(object),
                typeof(YT_AreaIcon), new PropertyMetadata(null));
        #endregion

        #region HolderName
        public String HolderName {
            get { return (String)GetValue(HolderNameProperty); }
            set { SetValue(HolderNameProperty, value); }
        }
        public static readonly DependencyProperty HolderNameProperty =
            DependencyProperty.Register("HolderName", typeof(String),
                typeof(YT_AreaIcon), new PropertyMetadata(null));
        #endregion

        #region AreaVisibility
        /// <summary>
        /// 图标的可见性
        /// </summary>
        public bool AreaVisibility {
            get { return (bool)GetValue(AreaVisibilityProperty); }
            set { SetValue(AreaVisibilityProperty, value); }
        }
        public static readonly DependencyProperty AreaVisibilityProperty =
            DependencyProperty.Register("AreaVisibility", typeof(bool), typeof(YT_AreaIcon),
                new PropertyMetadata(false, new PropertyChangedCallback(OnAreaVisibilityChanged)));
        private static void OnAreaVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            YT_AreaIcon dai = (YT_AreaIcon)d;
            dai._flowicon.Visible = (bool)e.NewValue;
        }
        #endregion

        #region CheckPop
        /// <summary>
        /// 与图标左键关联的弹出浮窗
        /// </summary>
        public YT_PopupBase CheckPop {
            get { return (YT_PopupBase)GetValue(CheckPopProperty); }
            set { SetValue(CheckPopProperty, value); }
        }
        public static readonly DependencyProperty CheckPopProperty =
            DependencyProperty.Register("CheckPop", typeof(YT_PopupBase),
                typeof(YT_AreaIcon), new PropertyMetadata(null));
        #endregion

        #region Ccontextmenu
        /// <summary>
        /// 与图标关联的右键菜单
        /// </summary>
        public ContextMenu DContextmenu {
            get { return (ContextMenu)GetValue(DContextmenuProperty); }
            set { SetValue(DContextmenuProperty, value); }
        }
        public static readonly DependencyProperty DContextmenuProperty =
            DependencyProperty.Register("DContextmenu", typeof(ContextMenu),
                typeof(YT_AreaIcon), new PropertyMetadata(null));
        #endregion

        /// <summary>
        /// 托盘图标鼠标移动事件
        /// </summary>
        public event MouseEventHandler IconMouseMove;
        /// <summary>
        /// 托盘图标鼠标单击事件
        /// </summary>
        public event MouseEventHandler IconMouseClick;
        /// <summary>
        /// 托盘图标鼠标双击事件
        /// </summary>
        public event MouseEventHandler IconMouseDoubleClick;
        #endregion

        #region Init
        private void InitNotifyIcon() {
            _flowicon = new NotifyIcon {
                Visible = true,
                ContextMenuStrip = new ContextMenuStrip(),
            };
            _flowicon.MouseClick += _flowicon_MouseClick;
            _flowicon.MouseMove += _flowicon_MouseMove;
            _flowicon.MouseDoubleClick += _flowicon_MouseDoubleClick;
        }
        #endregion

        #region Method
        protected virtual void _flowicon_MouseClick(object sender, MouseEventArgs e) {
            switch (e.Button) {
                case MouseButtons.Right:
                    if (DContextmenu is null)
                        return;
                    if (DContextmenu.IsOpen)
                        return;
                    DContextmenu.IsOpen = true;
                    break;
                case MouseButtons.Left:
                    if (CheckPop is null)
                        return;
                    if (CheckPop.IsOpen)
                        return;
                    CheckPop.IsOpen = true;
                    break;
            }
            IconMouseClick?.Invoke(new AreaIconSender(sender, HolderName), e);
        }

        protected virtual void _flowicon_MouseDoubleClick(object sender, MouseEventArgs e) {
            IconMouseDoubleClick?.Invoke(new AreaIconSender(sender, HolderName), e);
        }

        protected virtual void _flowicon_MouseMove(object sender, MouseEventArgs e) {
            IconMouseMove?.Invoke(new AreaIconSender(sender, HolderName), e);
        }
        #endregion

        #region Constructor
        public YT_AreaIcon() {
            InitNotifyIcon();
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class AreaIconSender {

        public object OriSender { set; get; }

        public string Holder { set; get; }

        public AreaIconSender(object sender, String holder) {
            OriSender = sender;
            Holder = holder;
        }
    }
}
