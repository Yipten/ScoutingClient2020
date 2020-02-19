using ScoutingClient2020.Commands;
using ScoutingClient2020.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace ScoutingClient2020.ViewModels {
	class TeamStatsViewModel {
		public List<int> Teams { get; set; }
		public int SelectedTeam { get => _selectedTeam; set { _selectedTeam = value; UpdateStats(); } }

		public Stat AvgAllTotal { get; set; }
		public Stat AvgAllBottom { get; set; }
		public Stat AvgAllOuter { get; set; }
		public Stat AvgAllInner { get; set; }
		public Stat AvgAllDrop { get; set; }
		public Stat AvgAllPoints { get; set; }
		public Stat AvgFouls { get; set; }

		public Stat AvgAutoTotal { get; set; }
		public Stat AvgAutoLower { get; set; }
		public Stat AvgAutoOuter { get; set; }
		public Stat AvgAutoInner { get; set; }
		public Stat AvgAutoDropped { get; set; }
		public Stat AvgAutoPoints { get; set; }

		public Stat AvgTeleTotal { get; set; }
		public Stat AvgTeleLower { get; set; }
		public Stat AvgTeleOuter { get; set; }
		public Stat AvgTeleInner { get; set; }
		public Stat AvgTeleDropped { get; set; }
		public Stat AvgTelePoints { get; set; }

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
			Teams = DBClient.GetTeams();

			AvgAllTotal = new Stat("SELECT AVG(AutoLower + AutoOuter + AutoInner + TeleLower + TeleOuter + TeleInner) FROM RawData WHERE TeamNumber = {0};", "Total");
			AvgAllBottom = new Stat("SELECT AVG(AutoLower + TeleLower) FROM RawData WHERE TeamNumber = {0};", "Bottom Port");
			AvgAllOuter = new Stat("SELECT AVG(AutoOuter + TeleOuter) FROM RawData WHERE TeamNumber = {0};", "Outer Port");
			AvgAllInner = new Stat("SELECT AVG(AutoInner + TeleOuter) FROM RawData WHERE TeamNumber = {0};", "Inner Port");
			AvgAllDrop = new Stat("SELECT AVG(AutoDropped) FROM RawData WHERE TeamNumber = {0};", "Drop");
			AvgAllPoints = new Stat("SELECT AVG(InitLine * 5 + AutoLower * 2 + AutoOuter * 4 + AutoInner * 6 + TeleLower * 1 + TeleOuter * 2 + TeleInner * 3 + RotationControl * 10 + PositionControl * 20 + ClimbSuccess * 25 + Park * 5 + ClimbBalanced * 15) FROM RawData WHERE TeamNumber = {0};", "Points");
			AvgFouls = new Stat("SELECT AVG(Fouls) FROM RawData WHERE TeamNumber = {0};", "Fouls");

			AvgAutoTotal = new Stat("SELECT AVG(AutoLower + AutoOuter + AutoInner) FROM RawData WHERE TeamNumber = {0};", "Total");
			AvgAutoLower = new Stat("SELECT AVG(AutoLower) FROM RawData WHERE TeamNumber = {0};", "Bottom Port");
			AvgAutoOuter = new Stat("SELECT AVG(AutoOuter) FROM RawData WHERE TeamNumber = {0};", "Outer Port");
			AvgAutoInner = new Stat("SELECT AVG(AutoInner) FROM RawData WHERE TeamNumber = {0};", "Inner Port");
			AvgAutoDropped = new Stat("SELECT AVG(AutoDropped) FROM RawData WHERE TeamNumber = {0};", "Dropped");
			AvgAutoPoints = new Stat("SELECT AVG(InitLine * 5 + AutoLower * 2 + AutoOuter * 4 + AutoInner * 6) FROM RawData WHERE TeamNumber = {0};", "Points");

			AvgTeleTotal = new Stat("SELECT AVG(TeleLower + TeleOuter + TeleInner) FROM RawData WHERE TeamNumber = {0};", "Total");
			AvgTeleLower = new Stat("SELECT AVG(TeleLower) FROM RawData WHERE TeamNumber = {0};", "Bottom Port");
			AvgTeleOuter = new Stat("SELECT AVG(TeleOuter) FROM RawData WHERE TeamNumber = {0};", "Outer Port");
			AvgTeleInner = new Stat("SELECT AVG(TeleInner) FROM RawData WHERE TeamNumber = {0};", "Inner Port");
			AvgTeleDropped = new Stat("SELECT AVG(TeleDropped) FROM RawData WHERE TeamNumber = {0};", "Dropped");
			AvgTelePoints = new Stat("SELECT AVG(TeleLower * 1 + TeleOuter * 2 + TeleInner * 3 + RotationControl * 10 + PositionControl * 20 + ClimbSuccess * 25 + Park * 5 + ClimbBalanced * 15) FROM RawData WHERE TeamNumber = {0};", "Points");

			PercentInitLine = new Stat("SELECT 100.0 * SUM(InitLine) / COUNT() FROM RawData WHERE TeamNumber = {0};", "Leave Init Line", "%");
			PercentClimb = new Stat("SELECT 100.0 * SUM(ClimbSuccess) / COUNT() FROM RawData WHERE TeamNumber = {0} AND ClimbAttempt = 1;", "Climb Success", "%");

			MaxAll = new Stat("SELECT MAX(AutoLower + AutoOuter + AutoInner + TeleLower + TeleOuter + TeleInner) FROM RawData WHERE TeamNumber = {0};", "Overall");
			MaxAllPoints = new Stat("SELECT MAX(InitLine * 5 + AutoLower * 2 + AutoOuter * 4 + AutoInner * 6 + TeleLower * 1 + TeleOuter * 2 + TeleInner * 3 + RotationControl * 10 + PositionControl * 20 + ClimbSuccess * 25 + Park * 5 + ClimbBalanced * 15) FROM RawData WHERE TeamNumber = {0};", "Overall Points");
			MaxAuto = new Stat("SELECT MAX(AutoLower + AutoOuter + AutoInner) FROM RawData WHERE TeamNumber = {0};", "Autonomous");
			MaxAutoPoints = new Stat("SELECT MAX(InitLine * 5 + AutoLower * 2 + AutoOuter * 4 + AutoInner * 6) FROM RawData WHERE TeamNumber = {0};", "Autonomous Points");
			MaxTele = new Stat("SELECT MAX(TeleLower + TeleOuter + TeleInner) FROM RawData WHERE TeamNumber = {0};", "Teleop");
			MaxTelePoints = new Stat("SELECT MAX(TeleLower * 1 + TeleOuter * 2 + TeleInner * 3 + RotationControl * 10 + PositionControl * 20 + ClimbSuccess * 25 + Park * 5 + ClimbBalanced * 15) FROM RawData WHERE TeamNumber = {0};", "Teleop Points");

			_stats = new Stat[] {
				AvgAllTotal,
				AvgAllBottom,
				AvgAllOuter,
				AvgAllInner,
				AvgAllDrop,
				AvgAllPoints,
				AvgAutoTotal,
				AvgAutoLower,
				AvgAutoOuter,
				AvgAutoInner,
				AvgAutoDropped,
				AvgAutoPoints,
				AvgTeleTotal,
				AvgTeleLower,
				AvgTeleOuter,
				AvgTeleInner,
				AvgTeleDropped,
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

			Update();
		}

		/// <summary>
		/// Updates all data fields on the page.
		/// </summary>
		public void Update() {
			foreach (Stat stat in _stats)
				stat.Update(_selectedTeam);
		}

		/// <summary>
		/// Updates list of teams.
		/// </summary>
		public void UpdateTeamList() {
			Teams = DBClient.GetTeams();
		}
	}
}
