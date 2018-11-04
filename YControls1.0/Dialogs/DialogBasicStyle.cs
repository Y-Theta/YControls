namespace YControls.Dialogs {
    /// <summary>
    /// YT_DialogBase的一些基础样式可以通过此枚举选择
    /// </summary>
    public enum DialogBasicStyle {
        /// <summary>
        /// 通知样式
        /// 只有取消按钮，内容在按钮之下占满整个对话框
        /// </summary>
        MessageStyle,
        /// <summary>
        /// 对话框内容只占据中间位置
        /// 确认/否认按钮在右下方
        /// </summary>
        ButtonBottomRight,
        /// <summary>
        /// 对话框内容只占据中间位置
        /// 确认/否认按钮在左下方平均占据整个空间
        /// </summary>
        ButtonBottomSeparate,
    }

}
