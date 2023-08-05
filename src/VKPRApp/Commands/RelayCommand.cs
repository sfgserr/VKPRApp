﻿using System.Windows.Input;

namespace VKPRApp.Commands
{
    class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action<object> _execute;

        public RelayCommand(Action<object> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
