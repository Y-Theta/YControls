using System;
using System.Windows;
using System.Windows.Data;

namespace YControls.TextControls {
    /// <summary>
    /// 用于方便创建TextBox的绑定
    /// </summary>
    public class TextBinding : Binding {
        private SubmitMode _submitMode;
        /// <summary>
        /// 文本提交方式,决定双向绑定时如何刷新绑定源
        /// </summary>
        public SubmitMode SubmitMode {
            get => _submitMode;
            set {
                _submitMode = value;
                if (_submitMode == SubmitMode.SpecialKey) {
                    Mode = BindingMode.TwoWay;
                    UpdateSourceTrigger = UpdateSourceTrigger.LostFocus;
                }
                else if (_submitMode == SubmitMode.WordChanged) {
                    Mode = BindingMode.TwoWay;
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                }
            }
        }

        #region Constructors
        /// <summary>
        /// 用于方便创建TextBox的绑定
        /// </summary>
        public TextBinding(string path) {
            if (path != null) {
                if (System.Windows.Threading.Dispatcher.CurrentDispatcher == null)
                    throw new InvalidOperationException();

                Path = new PropertyPath(path, (object[])null);
            }
            SubmitMode = SubmitMode.None;
        }
        #endregion
    }

}
