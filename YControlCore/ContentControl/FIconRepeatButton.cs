///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using YControlCore.ControlBase;

namespace YControlCore.ContentControl {

    /// <summary>
    /// 
    /// </summary>
    public class FIconRepeatButton : FIconButton {
        #region Properties
        /// <summary>
        /// 用于计时的Timer
        /// </summary>
        private DispatcherTimer _timer;

        [Bindable(true), Category("Behavior")]
        public int Delay {
            get { return (int)GetValue(DelayProperty); }
            set { SetValue(DelayProperty, value); }
        }
        public static readonly DependencyProperty DelayProperty =
            DependencyProperty.Register("Delay", typeof(int),
                typeof(FIconRepeatButton),
                new FrameworkPropertyMetadata(GetKeyboardDelay()),
                (d) => { return ((int)d) >= 0; });


        [Bindable(true), Category("Behavior")]
        public int Interval {
            get { return (int)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(int),
                typeof(FIconRepeatButton),
                new FrameworkPropertyMetadata(GetKeyboardSpeed()),
                (d) => { return ((int)d) >= 0; });


        #endregion

        #region Methods

        /// <summary>
        /// 开始计时
        /// </summary>
        private void StartTimer() {
            if (_timer == null) {
                _timer = new DispatcherTimer();
                _timer.Tick += OnTimeout;
            } else if (_timer.IsEnabled)
                return;

            _timer.Interval = TimeSpan.FromMilliseconds(Delay);
            _timer.Start();
        }

        /// <summary>
        /// 停止计时
        /// </summary>
        private void StopTimer() {
            if (_timer != null) {
                _timer.Stop();
            }
        }

        /// <summary>
        /// 计时器触发
        /// </summary>
        private void OnTimeout(object sender, EventArgs e) {
            TimeSpan interval = TimeSpan.FromMilliseconds(Interval);
            if (_timer.Interval != interval)
                _timer.Interval = interval;
            if (IsPressed) {
                OnClick();
            }
        }

        /// <summary>
        /// 对键盘行为的触发速率
        /// </summary>
        internal static int GetKeyboardSpeed() {
            int speed = SystemParameters.KeyboardSpeed;
            if (speed < 0 || speed > 31)
                speed = 31;
            return (31 - speed) * (400 - 1000 / 30) / 31 + 1000 / 30;
        }

        /// <summary>
        /// 对键盘行为的响应延迟
        /// </summary>
        internal static int GetKeyboardDelay() {
            int delay = SystemParameters.KeyboardDelay;
            if (delay < 0 || delay > 3)
                delay = 0;
            return (delay + 1) * 250;
        }

        /// <summary>
        /// 启动计时器自动触发click
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonDown(e);
            if (IsPressed && (ClickMode != System.Windows.Controls.ClickMode.Hover)) {
                StartTimer();
            }
        }

        /// <summary>
        /// 关闭计时器
        /// </summary>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonUp(e);
            if (ClickMode != System.Windows.Controls.ClickMode.Hover) {
                StopTimer();
            }
        }

        /// <summary>
        /// 失去鼠标时停止自动触发
        /// </summary>
        protected override void OnLostMouseCapture(MouseEventArgs e) {
            base.OnLostMouseCapture(e);
            StopTimer();
        }

        /// <summary>
        /// 判断鼠标是否以按下方式进入控件，即鼠标焦点在控件上，并在移出控件后保持按下状态，再次进入控件时触发
        /// </summary>
        protected override void OnMouseEnter(MouseEventArgs e) {
            base.OnMouseEnter(e);
            if (HandleIsMouseOverChanged()) {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 鼠标一处控件时停止触发
        /// </summary>
        protected override void OnMouseLeave(MouseEventArgs e) {
            base.OnMouseLeave(e);
            if (HandleIsMouseOverChanged()) {
                e.Handled = true;
            }
        }

        private bool HandleIsMouseOverChanged() {
            if (ClickMode == System.Windows.Controls.ClickMode.Hover) {
                if (IsMouseOver) {
                    StartTimer();
                } else {
                    StopTimer();
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// This is the method that responds to the KeyDown event.
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);
            if ((e.Key == Key.Space) && (ClickMode != System.Windows.Controls.ClickMode.Hover)) {
                StartTimer();
            }
        }

        /// <summary>
        /// This is the method that responds to the KeyUp event.
        /// </summary>
        protected override void OnKeyUp(KeyEventArgs e) {
            if ((e.Key == Key.Space) && (ClickMode != System.Windows.Controls.ClickMode.Hover)) {
                StopTimer();
            }
            base.OnKeyUp(e);
        }
        #endregion

        #region Constructors
        static FIconRepeatButton() {
            //  DefaultStyleKeyProperty.OverrideMetadata(typeof(FIconRepeatButton), new FrameworkPropertyMetadata(typeof(FIconRepeatButton)));
            ClickModeProperty.OverrideMetadata(typeof(FIconRepeatButton), new FrameworkPropertyMetadata(System.Windows.Controls.ClickMode.Press));
        }
        #endregion
    }

}
