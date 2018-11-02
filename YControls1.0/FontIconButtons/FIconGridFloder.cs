using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace YControls.FontIconButtons {
    ///// <summary>
    ///// Grid折叠控件
    ///// </summary>
    public sealed class FIconGridFloder : FIconToggleButton {
        #region Properties
        private GridLength _originGridSize { get; set; }
        /// <summary>
        /// 目标的Grid
        /// </summary>
        public Grid AttachedGrid {
            get { return (Grid)GetValue(AttachedGridProperty); }
            set { SetValue(AttachedGridProperty, value); }
        }
        public static readonly DependencyProperty AttachedGridProperty =
            DependencyProperty.Register("AttachedGrid", typeof(Grid),
                typeof(FIconGridFloder), new PropertyMetadata(null));

        /// <summary>
        /// 关联的表格类型
        /// </summary>
        public bool AttachRow {
            get { return (bool)GetValue(AttachRowProperty); }
            set { SetValue(AttachRowProperty, value); }
        }
        public static readonly DependencyProperty AttachRowProperty =
            DependencyProperty.Register("AttachRow", typeof(bool),
                typeof(FIconGridFloder), new PropertyMetadata(true));

        /// <summary>
        /// 目标Grid的目标行或列
        /// </summary>
        public int AttachedLorC {
            get { return (int)GetValue(AttachedLorCProperty); }
            set { SetValue(AttachedLorCProperty, value); }
        }
        public static readonly DependencyProperty AttachedLorCProperty =
            DependencyProperty.Register("AttachedLorC", typeof(int),
                typeof(FIconGridFloder), new PropertyMetadata(0));
        #endregion

        #region Methods

        /// <summary>
        /// 显示选择的网格
        /// </summary>
        private void GridShow() {
            if (AttachRow)
                AttachedGrid.RowDefinitions[AttachedLorC].Height = _originGridSize;
            else
                AttachedGrid.ColumnDefinitions[AttachedLorC].Width = _originGridSize;
        }

        /// <summary>
        /// 隐藏选择的网格
        /// </summary>
        private void GridHide() {
            if (AttachRow) {
                _originGridSize = AttachedGrid.RowDefinitions[AttachedLorC].Height;
                AttachedGrid.RowDefinitions[AttachedLorC].Height = new GridLength(0, GridUnitType.Pixel);
            }
            else {
                _originGridSize = AttachedGrid.ColumnDefinitions[AttachedLorC].Width;
                AttachedGrid.ColumnDefinitions[AttachedLorC].Width = new GridLength(0, GridUnitType.Pixel);
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonUp(e);
            if ((bool)IsChecked)
                GridHide();
            else
                GridShow();
        }

        #endregion

        #region Constructors
        #endregion
    }

}
