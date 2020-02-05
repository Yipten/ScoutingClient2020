using ScoutingClient2020.Models;
using System.Collections.Generic;

namespace ScoutingClient2020.ViewModels {
	class TeamTrendsViewModel {
		public List<int> Teams { get; set; }
		public int SelectedTeam { get => _selectedTeam; set { _selectedTeam = value; UpdateLineGraphs(); } }

		public LineGraph AllTotal { get; set; }
		public LineGraph AllBottom { get; set; }
		public LineGraph AllOuter { get; set; }
		public LineGraph AllInner { get; set; }
		public LineGraph AllDrop { get; set; }
		public LineGraph AllPoints { get; set; }
		public LineGraph Fouls { get; set; }

		public LineGraph AutoTotal { get; set; }
		public LineGraph AutoBottom { get; set; }
		public LineGraph AutoOuter { get; set; }
		public LineGraph AutoInner { get; set; }
		public LineGraph AutoDrop { get; set; }
		public LineGraph AutoPoints { get; set; }

		public LineGraph TeleTotal { get; set; }
		public LineGraph TeleBottom { get; set; }
		public LineGraph TeleOuter { get; set; }
		public LineGraph TeleInner { get; set; }
		public LineGraph TeleDrop { get; set; }
		public LineGraph TelePoints { get; set; }

		private readonly LineGraph[] _lineGraphs;
		private int _selectedTeam;

		public TeamTrendsViewModel() {
			Teams = DBClient.GetTeams();

			// TODO: initialize line graphs here

			_lineGraphs = new LineGraph[] {
				AllTotal,
				AllBottom,
				AllOuter,
				AllInner,
				AllDrop,
				AllPoints,
				Fouls,
				AutoTotal,
				AutoBottom,
				AutoOuter,
				AutoInner,
				AutoDrop,
				AutoPoints,
				TeleTotal,
				TeleBottom,
				TeleOuter,
				TeleInner,
				TeleDrop,
				TelePoints
			};

			UpdateLineGraphs();
		}

		public void UpdateLineGraphs() {
			foreach (LineGraph lineGraph in _lineGraphs)
				lineGraph?.Update(_selectedTeam);
		}
	}
}
