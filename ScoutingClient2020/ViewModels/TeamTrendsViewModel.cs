using ScoutingClient2020.Commands;
using ScoutingClient2020.Models;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;

namespace ScoutingClient2020.ViewModels {
	class TeamTrendsViewModel {
		public TeamList TeamList { get; set; }
		public int SelectedTeam { get => _selectedTeam; set { _selectedTeam = value; Update(); } }

		public LineGraph AllTotal { get; set; }
		public LineGraph AllLower { get; set; }
		public LineGraph AllOuter { get; set; }
		public LineGraph AllInner { get; set; }
		public LineGraph AllDrop { get; set; }
		public LineGraph AllPoints { get; set; }
		public LineGraph Fouls { get; set; }

		public LineGraph AutoTotal { get; set; }
		public LineGraph AutoLower { get; set; }
		public LineGraph AutoOuter { get; set; }
		public LineGraph AutoInner { get; set; }
		public LineGraph AutoDropped { get; set; }
		public LineGraph AutoPoints { get; set; }

		public LineGraph TeleTotal { get; set; }
		public LineGraph TeleLower { get; set; }
		public LineGraph TeleOuter { get; set; }
		public LineGraph TeleInner { get; set; }
		public LineGraph TeleDropped { get; set; }
		public LineGraph TelePoints { get; set; }

		public ICommand UpdateTeamTrendsListCommand { get; private set; }

		private readonly LineGraph[] _lineGraphs;
		private int _selectedTeam;

		public TeamTrendsViewModel() {
			AllTotal = new LineGraph("SELECT AutoLower + AutoOuter + AutoInner + TeleLower + TeleOuter + TeleInner FROM RawData WHERE TeamNumber = {0};", Brushes.Red, false);
			AllLower = new LineGraph("SELECT AutoLower + TeleLower FROM RawData WHERE TeamNumber = {0};", Brushes.Green, true);
			
			TeamList = new TeamList();

			UpdateTeamTrendsListCommand = new UpdateTeamTrendsListCommand(this);

			_lineGraphs = new LineGraph[] {
				AllTotal,
				AllLower,
				AllOuter,
				AllInner,
				AllDrop,
				AllPoints,
				Fouls,
				AutoTotal,
				AutoLower,
				AutoOuter,
				AutoInner,
				AutoDropped,
				AutoPoints,
				TeleTotal,
				TeleLower,
				TeleOuter,
				TeleInner,
				TeleDropped,
				TelePoints
			};

			TeamList.Update();
			Update();
		}

		/// <summary>
		/// Updates all data fields on the page.
		/// </summary>
		public void Update() {
			foreach (LineGraph lineGraph in _lineGraphs)
				lineGraph?.Update(_selectedTeam);
		}

		/// <summary>
		/// Updates list of teams.
		/// </summary>
		public void UpdateTeamList() {
			TeamList.Update();
		}
	}
}
