using Calculator_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator_WPF.Commands
{
    class ButtonPressedCommand : ICommand
    {
        private MainViewModel _viewModel;

        public MainViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            ViewModel.GetPressedButton(parameter.ToString());
        }

        public ButtonPressedCommand(MainViewModel mainViewModel)
        {
            ViewModel = mainViewModel;
        }

    }
}
