using ScoutingClient2020.Models;
using System.Collections.Generic;

namespace ScoutingClient2020.ViewModels {
	class TeamStatsViewModel {
		public List<int> Teams { get; set; }
		public int SelectedTeam { get => _selectedTeam; set { _selectedTeam = value; Update(); } }
		public Stat AvgAll { get; set; }
		public Stat AvgAllBottom { get; set; }
		public Stat AvgAllOuter { get; set; }
		public Stat AvgAllInner { get; set; }
		public Stat AvgAllDrop { get; set; }
		public Stat AvgAllPoints { get; set; }
		public Stat AvgAuto { get; set; }
		public Stat AvgAutoBottom { get; set; }
		public Stat AvgAutoOuter { get; set; }
		public Stat AvgAutoInner { get; set; }
		public Stat AvgAutoDrop { get; set; }
		public Stat AvgAutoPoints { get; set; }
		public Stat AvgTele { get; set; }
		public Stat AvgTeleBottom { get; set; }
		public Stat AvgTeleOuter { get; set; }
		public Stat AvgTeleInner { get; set; }
		public Stat AvgTeleDrop { get; set; }
		public Stat AvgTelePoints { get; set; }
		public Stat AvgFouls { get; set; }
		public Stat PercentInitLine { get; set; }
		public Stat PercentClimb { get; set; }
		public Stat MaxAll { get; set; }
		public Stat MaxAllPoints { get; set; }
		public Stat MaxAuto { get; set; }
		public Stat MaxAutoPoints { get; set; }
		public Stat MaxTele { get; set; }
		public Stat MaxTelePoints { get; set; }

		private readonly Stat[] _stats;
		private int _selectedTeam;

		/// <summary>
		/// Initializes a new instance of the TeamStatsViewModel class.
		/// </summary>
		public TeamStatsViewModel() {
			Teams = new List<int>();
			
			AvgAll = new Stat("SELECT AVG(AutoBottom + AutoOuter + AutoInner + TeleBottom + TeleOuter + TeleInner) FROM RawData WHERE TeamNumber = {0};", "Total");
			AvgAllBottom = new Stat("SELECT AVG(AutoBottom + TeleBottom) FROM RawData WHERE TeamNumber = {0};", "Bottom Port");
			AvgAllOuter = new Stat("SELECT AVG(AutoOuter + TeleOuter) FROM RawData WHERE TeamNumber = {0};", "Outer Port");
			AvgAllInner = new Stat("SELECT AVG(AutoInner + TeleOuter) FROM RawData WHERE TeamNumber = {0};", "Inner Port");
			AvgAllDrop = new Stat("SELECT AVG(AutoDrop) FROM RawData WHERE TeamNumber = {0};", "Drop");
			AvgAllPoints = new Stat("SELECT AVG(InitLine * 5 + AutoBottom * 2 + AutoOuter * 4 + AutoInner * 6 + TeleBottom * 1 + TeleOuter * 2 + TeleInner * 3 + RotationControl * 10 + PositionControl * 20 + ClimbSuccess * 25 + Park * 5 + ClimbBalanced * 15) FROM RawData WHERE TeamNumber = {0};", "Points");
			
			AvgAuto = new Stat("SELECT AVG(AutoBottom + AutoOuter + AutoInner) FROM RawData WHERE TeamNumber = {0};", "Total");
			AvgAutoBottom = new Stat("SELECT AVG(AutoBottom) FROM RawData WHERE TeamNumber = {0};", "Bottom Port");
			AvgAutoOuter = new Stat("SELECT AVG(AutoOuter) FROM RawData WHERE TeamNumber = {0};", "Outer Port");
			AvgAutoInner = new Stat("SELECT AVG(AutoInner) FROM RawData WHERE TeamNumber = {0};", "Inner Port");
			AvgAutoDrop = new Stat("SELECT AVG(AutoDrop) FROM RawData WHERE TeamNumber = {0};", "Dropped");
			AvgAutoPoints = new Stat("SELECT AVG(InitLine * 5 + AutoBottom * 2 + AutoOuter * 4 + AutoInner * 6) FROM RawData WHERE TeamNumber = {0};", "Points");
			
			AvgTele = new Stat("SELECT AVG(TeleBottom + TeleOuter + TeleInner) FROM RawData WHERE TeamNumber = {0};", "Total");
			AvgTeleBottom = new Stat("SELECT AVG(TeleBottom) FROM RawData WHERE TeamNumber = {0};", "Bottom Port");
			AvgTeleOuter = new Stat("SELECT AVG(TeleOuter) FROM RawData WHERE TeamNumber = {0};", "Outer Port");
			AvgTeleInner = new Stat("SELECT AVG(TeleInner) FROM RawData WHERE TeamNumber = {0};", "Inner Port");
			AvgTeleDrop = new Stat("SELECT AVG(TeleDrop) FROM RawData WHERE TeamNumber = {0};", "Dropped");
			AvgTelePoints = new Stat("SELECT AVG(TeleBottom * 1 + TeleOuter * 2 + TeleInner * 3 + RotationControl * 10 + PositionControl * 20 + ClimbSuccess * 25 + Park * 5 + ClimbBalanced * 15) FROM RawData WHERE TeamNumber = {0};", "Points");
			
			PercentInitLine = new Stat("SELECT 100.0 * SUM(InitLine) / COUNT() FROM RawData WHERE TeamNumber = {0};", "Leave Init Line", "%");
			PercentClimb = new Stat("SELECT 100.0 * SUM(ClimbSuccess) / COUNT() FROM RawData WHERE TeamNumber = {0} AND ClimbAttempt = 1;", "Climb Success", "%");
			
			MaxAll = new Stat("SELECT MAX(AutoBottom + AutoOuter + AutoInner + TeleBottom + TeleOuter + TeleInner) FROM RawData WHERE TeamNumber = {0};", "Overall");
			MaxAllPoints = new Stat("SELECT MAX(InitLine * 5 + AutoBottom * 2 + AutoOuter * 4 + AutoInner * 6 + TeleBottom * 1 + TeleOuter * 2 + TeleInner * 3 + RotationControl * 10 + PositionControl * 20 + ClimbSuccess * 25 + Park * 5 + ClimbBalanced * 15) FROM RawData WHERE TeamNumber = {0};", "Overall Points");
			MaxAuto = new Stat("SELECT MAX(AutoBottom + AutoOuter + AutoInner) FROM RawData WHERE TeamNumber = {0};", "Autonomous");
			MaxAutoPoints = new Stat("SELECT MAX(InitLine * 5 + AutoBottom * 2 + AutoOuter * 4 + AutoInner * 6) FROM RawData WHERE TeamNumber = {0};", "Autonomous Points");
			MaxTele = new Stat("SELECT MAX(TeleBottom + TeleOuter + TeleInner) FROM RawData WHERE TeamNumber = {0};", "Teleop");
			MaxTelePoints = new Stat("SELECT MAX(TeleBottom * 1 + TeleOuter * 2 + TeleInner * 3 + RotationControl * 10 + PositionControl * 20 + ClimbSuccess * 25 + Park * 5 + ClimbBalanced * 15) FROM RawData WHERE TeamNumber = {0};", "Teleop Points");

			_stats = new Stat[] {
				AvgAll,
				AvgAllBottom,
				AvgAllOuter,
				AvgAllInner,
				AvgAllDrop,
				AvgAllPoints,
				AvgAuto,
				AvgAutoBottom,
				AvgAutoOuter,
				AvgAutoInner,
				AvgAutoDrop,
				AvgAutoPoints,
				AvgTele,
				AvgTeleBottom,
				AvgTeleOuter,
				AvgTeleInner,
				AvgTeleDrop,
				AvgTelePoints,
				AvgFouls,
				PercentInitLine,
				PercentClimb,
				MaxAll,
				MaxAllPoints,
				MaxAuto,
				MaxAutoPoints,
				MaxTele,
				MaxTelePoints
			};

			UpdateTeamsList();
			Update();
		}

		/// <summary>
		/// Updates the list of selectable teams from database.
		/// </summary>
		private void UpdateTeamsList() {
			Teams.Clear();
			List<double> doubleTeams = DBClient.ExecuteQuery("SELECT DISTINCT TeamNumber FROM RawData ORDER BY TeamNumber ASC;", true);
			foreach (double team in doubleTeams) {
				Teams.Add((int)team);
			}
		}

		/// <summary>
		/// Updates all data fields on the page.
		/// </summary>
		public void Update() {
			foreach (Stat stat in _stats)
				stat?.Update(_selectedTeam);    // TODO: remove null check
		}
	}
}
