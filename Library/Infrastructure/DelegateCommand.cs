using System;
using System.Windows.Input;

namespace Library.Helpers
{
    public class DelegateCommand : ICommand
    {
        #region Fields
        readonly Action<object> execute;
        readonly Predicate<object> canExecute;
        #endregion

        #region Constructors
        public DelegateCommand(Action<object> execute) : this(execute, null) { }

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }
        #endregion

        #region ICommand_numbers
        public bool CanExecute(object parameter)
        {
            return canExecute?.Invoke(parameter) ?? true;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
        #endregion
    }
}
