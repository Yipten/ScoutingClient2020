using ScoutingClient2020.Static;
using System.Collections.Generic;

namespace ScoutingClient2020.ViewModels {
	class TeamStatsViewModel {
		public List<int> Teams { get; private set; }

		public TeamStatsViewModel() {
			Teams = new List<int>();
			UpdateTeamsList();
		}

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
	}
}
