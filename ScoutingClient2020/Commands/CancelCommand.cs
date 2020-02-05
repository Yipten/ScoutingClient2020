using ScoutingClient2020.ViewModels;
using System;
using System.Windows.Input;

namespace ScoutingClient2020.Commands {
	class CancelCommand : ICommand {
		private DataManagementViewModel _viewModel;

		public CancelCommand(DataManagementViewModel viewModel) {
			_viewModel = viewModel;
		}

		public event EventHandler CanExecuteChanged {
			add {
				CommandManager.RequerySuggested += value;
			}
			remove {
				CommandManager.RequerySuggested -= value;
			}
		}

		public bool CanExecute(object parameter) {
			return _viewModel.Bluetooth.IsReceiving;
		}

		public void Execute(object parameter) {
			_viewModel.Cancel();
			_viewModel.Bluetooth.Yep();
		}
	}
}
