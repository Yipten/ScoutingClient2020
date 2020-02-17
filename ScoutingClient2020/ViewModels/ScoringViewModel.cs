using ScoutingClient2020.Commands;
using ScoutingClient2020.Models;
using System.Windows.Input;

namespace ScoutingClient2020.ViewModels {
	class ScoringViewModel {
		public Scorer Scorer { get; set; }

		public ICommand ResetScoreCommand { get; private set; }

		public ScoringViewModel() {
			Scorer = new Scorer();

			ResetScoreCommand = new ResetScoreCommand(this);
		}

		public void UpdateScore() {
			Scorer.UpdateScore();
		}

		public void ResetScore() {
			Scorer.ResetScore();
		}
	}
}
