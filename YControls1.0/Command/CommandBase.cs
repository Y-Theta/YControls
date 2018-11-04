using System;
using System.Windows.Input;

namespace YControls.Command {
    public class CommandBase : ICommand {
        #region Properties
        public event EventHandler CanExecuteChanged;

        private event EnableEventHandler _enable;
        public event EnableEventHandler Enable {
            add => _enable = value;
            remove => _enable -= value;
        }

        private event CommandEventHandler _execution;
        public event CommandEventHandler Execution {
            add => _execution = value;
            remove => _execution -= value;
        }
        #endregion

        #region Methods
        public bool CanExecute(object parameter) {
            return _enable is null ? true : _enable.Invoke(parameter);
        }

        public void Execute(object parameter) {
            _execution?.Invoke(parameter);
        }
        #endregion

        #region Constructors
        public CommandBase(CommandEventHandler action) {
            _execution += action;
        }
        public CommandBase() { }
        #endregion
    }

}
