using ScoutingClient2020.ViewModels;
using System;
using System.Windows.Input;

namespace ScoutingClient2020.Commands {
	class UpdateTeamStatsCommand : ICommand {
		private TeamStatsViewModel _viewModel;

		public UpdateTeamStatsCommand(TeamStatsViewModel viewModel) {
			_viewModel = viewModel;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter) {
			return _viewModel.CanUpdate();
		}

		public void Execute(object parameter) {
			_viewModel.Update();
		}
	}
}
