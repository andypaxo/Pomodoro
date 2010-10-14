using System.Windows.Input;
using System;

namespace Pomodoro
{
    public abstract class Command : ICommand
    {
        private bool iCanExecute = true;
        protected bool ICanExecute
        {
            get { return iCanExecute; }
            set
            {
                iCanExecute = value;
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            return ICanExecute;
        }

        public event EventHandler CanExecuteChanged;

        public abstract void Execute(object parameter);
    }
}
