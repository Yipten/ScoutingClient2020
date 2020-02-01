using ScoutingClient2020.Commands;
using ScoutingClient2020.Models;
using System.Windows.Input;

namespace ScoutingClient2020.ViewModels {
	class ScoringViewModel {
		public Scorer Scorer { get; set; }

		public ICommand UpdateScoreCommand { get; private set; }

		public ScoringViewModel() {
			Scorer = new Scorer();

			UpdateScoreCommand = new UpdateScoreCommand(this);

			UpdateScore();
		}

		public void UpdateScore() {
			Scorer.UpdateScore();
		}
	}
}
