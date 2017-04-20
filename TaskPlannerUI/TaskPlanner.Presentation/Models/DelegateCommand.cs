using System;
using System.Windows.Input;

namespace TaskPlanner.Presentation.Models
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> executeAction;

        public DelegateCommand(Action<object> executeAction)
        {
            this.executeAction = executeAction;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}
