using ScoutingClient2020.ViewModels;
using System;
using System.Windows.Input;

namespace ScoutingClient2020.Commands {
	class UpdateTeamTrendsListCommand : ICommand {
		private TeamTrendsViewModel _viewModel;

		public UpdateTeamTrendsListCommand(TeamTrendsViewModel viewModel) {
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
			return true;
		}

		public void Execute(object parameter) {
			_viewModel.UpdateTeamList();
		}
	}
}
