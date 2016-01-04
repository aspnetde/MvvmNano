using System;
using System.Windows.Input;

namespace MvvmNano
{
    public class MvvmNanoCommand : MvvmNanoCommand<object>
    {
        public MvvmNanoCommand(Action execute) 
            : base(p => execute(), null)
        {
        }

        public MvvmNanoCommand(Action execute, Func<bool> canExecute)
            : base(p => execute(), p => canExecute())
        {
        }
    }

    public class MvvmNanoCommand<T> : ICommand
    {
        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _execute;

        public event EventHandler CanExecuteChanged;

        public MvvmNanoCommand(Action<T> execute) : this(execute, null)
        {
        }

        public MvvmNanoCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}

