using ScoutingClient2020.Models;

namespace ScoutingClient2020.ViewModels {
	class ScoringViewModel {
		public Scorer Scorer { get; set; }

		public ScoringViewModel() {
			Scorer = new Scorer();
		}

		public void UpdateScore() {
			Scorer.UpdateScore();
		}
	}
}
