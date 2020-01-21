using System.Collections.Generic;

namespace ScoutingClient2020.ViewModels {
	class TeamStatsViewModel {
		public List<int> Teams { get; set; }

		public TeamStatsViewModel() {
			Teams = new List<int>();
			Teams.Add(1);
			Teams.Add(2);
		}
	}
}
