using ScoutingClient2020.Models;
using ScoutingClient2020.ViewModels;
using System;
using System.Windows.Input;

namespace ScoutingClient2020.Commands {
	class MergeCommand : ICommand {
		private DataManagementViewModel _viewModel;

		public MergeCommand(DataManagementViewModel viewModel) {
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
			return !_viewModel.Bluetooth.IsReceiving;
		}

		public void Execute(object parameter) {
			DBClient.Merge("2020_test");
		}
	}
}
