namespace YControls.AreaIconWindow {
    /// <summary>
    /// 拖动方式
    /// </summary>
    public enum DragMode {
        /// <summary>
        /// 仅能通过标题栏拖动
        /// </summary>
        TitleBar,
        /// <summary>
        /// 窗口任意地方可以拖动
        /// </summary>
        FullWindow
    }

    /// <summary>
    /// 窗体标题栏模式
    /// </summary>
    public enum TitleBarMode {
        /// <summary>
        /// 正常
        /// </summary>
        Normal,
        /// <summary>
        /// 自动隐藏
        /// </summary>
        AutoHide,
        /// <summary>
        /// 可最小化
        /// </summary>
        Mini,
    }
}
