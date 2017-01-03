using System;
using System.Windows.Input;

namespace MvvmNano
{
    /// <summary>
    /// A bindable Command, implementing ICommand.
    /// </summary>
    public class MvvmNanoCommand : MvvmNanoCommand<object>
    {
        /// <summary>
        /// Initializes a new instance of MvvmNanoCommand.
        /// </summary>
        /// <param name="execute">The callback which should be executed when the command is run.</param>
        public MvvmNanoCommand(Action execute) 
            : base(p => execute(), null)
        {
        }

        /// <summary>
        /// Initializes a new instance of MvvmNanoCommand.
        /// </summary>
        /// <param name="execute">The callback which should be executed when the command is run..</param>
        /// <param name="canExecute">The callback which determines if the command can be executed.</param>
        public MvvmNanoCommand(Action execute, Func<bool> canExecute)
            : base(p => execute(), p => canExecute())
        {
        }
    }

    /// <summary>
    /// A bindable Command, implementing ICommand.
    /// </summary>
    public class MvvmNanoCommand<T> : ICommand
    {
        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _execute;

        /// <summary>
        /// An event you can subscribe to be notified if the CancExecute() state changes.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Initializes a new instance of MvvmNanoCommand<T>.
        /// </summary>
        /// <param name="execute">The callback which should be executed when the command is run.</param>
        public MvvmNanoCommand(Action<T> execute) : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of MvvmNanoCommand<T>.
        /// </summary>
        /// <param name="execute">The callback which should be executed when the command is run.</param>
        /// <param name="canExecute">The callback which determines if the command can be executed.</param>
        public MvvmNanoCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Determines whether this commmand can be executed or not.
        /// </summary>
        /// <returns><c>true</c> if this commmand can be executed; otherwise, <c>false</c>.</returns>
        /// <param name="parameter">The Parameter wich should be passed.</param>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The Parameter wich should be passed.</param>
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        /// <summary>
        /// Raises the CanExecuteChanged event of this command.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

