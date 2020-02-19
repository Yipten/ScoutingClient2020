namespace ScoutingClient2020.ViewModels {
	class MatchHistoryViewModel {
		public int SelectedMatch { get => _selectedTeam; set { _selectedTeam = value; Update(); } }

		private int _selectedTeam;

		/// <summary>
		/// Initializes a new instance of the MatchHistoryViewModel class.
		/// </summary>
		public MatchHistoryViewModel() {

		}

		/// <summary>
		/// Updates all data fields on the page.
		/// </summary>
		public void Update() {

		}
	}
}
