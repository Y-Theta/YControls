///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace YControlCore.Base {
    public static class FunctionExtension {
        #region Properties
        #endregion

        #region Methods

        /// <summary>
        /// 注册组件命令
        /// </summary>
        /// <param name="command">需要注册的命令</param>
        /// <param name="controlType">组件类型</param>
        /// <param name="executedRoutedEventHandler">命令处理事件</param>
        /// <param name="canExecuteRoutedEventHandler">命令是否能够处理事件</param>
        /// <param name="inputGestures">命令快捷键绑定</param>
        public static void RegisterHandler(this RoutedCommand command, Type controlType,  ExecutedRoutedEventHandler executedRoutedEventHandler,
                                                   CanExecuteRoutedEventHandler canExecuteRoutedEventHandler, params InputGesture[] inputGestures) {
            // Validate parameters
            Debug.Assert(controlType != null);
            Debug.Assert(command != null);
            Debug.Assert(executedRoutedEventHandler != null);
            // All other parameters may be null

            // Create command link for this command
            CommandManager.RegisterClassCommandBinding(controlType, new CommandBinding(command, executedRoutedEventHandler, canExecuteRoutedEventHandler));

            // Create additional input binding for this command
            if (inputGestures != null) {
                for (int i = 0; i < inputGestures.Length; i++) {
                    CommandManager.RegisterClassInputBinding(controlType, new InputBinding(command, inputGestures[i]));
                }
            }

        }
        #endregion

        #region Constructors
        #endregion
    }
}
