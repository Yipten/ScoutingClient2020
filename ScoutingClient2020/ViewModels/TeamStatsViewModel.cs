using ScoutingClient2020.Models;
using System.Collections.Generic;

namespace ScoutingClient2020.ViewModels {
	class TeamStatsViewModel {
		public List<int> Teams { get; set; }
		public int SelectedTeam { get => _selectedTeam; set { _selectedTeam = value; Update(); } }
		public Stat TestStat { get; set; }

		private readonly Stat[] _stats;
		private int _selectedTeam;

		/// <summary>
		/// Initializes a new instance of the TeamStatsViewModel class.
		/// </summary>
		public TeamStatsViewModel() {
			Teams = new List<int>();
			TestStat = new Stat("SELECT 100.0 * SUM(CrossHabLine) / COUNT() FROM RawData WHERE TeamNumber = {0} AND StartPosition BETWEEN 3 AND 5;", "Test Value", "units");
			_stats = new Stat[] { TestStat };

			UpdateTeamsList();
		}

		/// <summary>
		/// Updates the list of selectable teams from database.
		/// </summary>
		private void UpdateTeamsList() {
			Teams.Clear();
			List<double> doubleTeams = DBClient.ExecuteQuery(
				"SELECT DISTINCT TeamNumber " +
				"FROM RawData " +
				"ORDER BY TeamNumber ASC;",
				true
			);
			foreach (double team in doubleTeams) {
				Teams.Add((int)team);
			}
		}

		/// <summary>
		/// Updates all data fields on the page.
		/// </summary>
		public void Update() {
			foreach (Stat stat in _stats)
				stat.Update(_selectedTeam);
		}
	}
}
