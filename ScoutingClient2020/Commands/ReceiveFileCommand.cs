using ScoutingClient2020.ViewModels;
using System;
using System.Windows.Input;

namespace ScoutingClient2020.Commands {
	class ReceiveFileCommand : ICommand {
		private DataManagementViewModel _viewModel;

		public ReceiveFileCommand(DataManagementViewModel viewModel) {
			_viewModel = viewModel;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter) {
			return !_viewModel.IsReceiving;
		}

		public void Execute(object parameter) {
			_viewModel.ReceiveFile();
		}
	}
}
