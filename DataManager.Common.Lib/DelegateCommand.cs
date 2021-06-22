using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PomodoroTimer.Common.Lib
{
    public class DelegateCommand : ICommand
    {

        private readonly Func<bool> canExecute;
        private readonly Action execute;

        public DelegateCommand(Action execute) : this(execute, null)
        {

        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (this.canExecute == null)
            {
                return true;
            }

            return this.canExecute();
        }

        public void Execute(object parameter)
        {
            this.execute();
        }

        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// parameter 있는 Command
    /// </summary>
    /// <typeparam name="T">인자값</typeparam>
    public class DelegateCommand<T> : ICommand
    {

        private readonly Func<T, bool> canExecute;
        private readonly Action<T> execute;

        public DelegateCommand(Action<T> execute) : this(execute, null)
        {

        }

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (this.canExecute == null)
            {
                return true;
            }

            T param = (T)parameter;

            return this.canExecute(param);
        }

        public void Execute(object parameter)
        {
            T param = (T)parameter;
            this.execute(param);
        }

        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
