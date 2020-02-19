using ScoutingClient2020.Commands;
using ScoutingClient2020.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace ScoutingClient2020.ViewModels {
	class TeamStatsViewModel {
		public List<int> Teams { get; set; }
		public int SelectedTeam { get => _selectedTeam; set { _selectedTeam = value; Update(); } }

		public Stat LowerAvgAuto { get; set; }
		public Stat LowerAvgTele { get; set; }

		public Stat OuterAvgAuto { get; set; }
		public Stat OuterAvgTele { get; set; }

		public Stat InnerAvgAuto { get; set; }
		public Stat InnerAvgTele { get; set; }

		public Stat TotalAvgAuto { get; set; }
		public Stat TotalAvgTele { get; set; }
		public Stat TotalPercentAuto { get; set; }
		public Stat TotalPercentTele { get; set; }
		public Stat TotalMaxAuto { get; set; }
		public Stat TotalMaxTele { get; set; }

		public Stat MissedAvgAuto { get; set; }
		public Stat MissedAvgTele { get; set; }

		public Stat DroppedAvgAuto { get; set; }
		public Stat DroppedAvgTele { get; set; }

		public Stat PointsAvgAuto { get; set; }
		public Stat PointsAvgTele { get; set; }
		public Stat PointsMaxAuto { get; set; }
		public Stat PointsMaxTele { get; set; }

		public Stat FoulsAvg { get; set; }

		public Stat InitLinePercent { get; set; }
		public Stat ClimbPercent { get; set; }

		public ICommand UpdateTeamStatsListCommand { get; private set; }

		private readonly Stat[] _stats;
		private int _selectedTeam;

		/// <summary>
		/// Initializes a new instance of the TeamStatsViewModel class.
		/// </summary>
		public TeamStatsViewModel() {
			Teams = DBClient.GetTeams();

			LowerAvgAuto = new Stat("SELECT AVG(AutoLower) FROM RawData WHERE TeamNumber = {0};", "Auto");
			LowerAvgTele = new Stat("SELECT AVG(TeleLower) FROM RawData WHERE TeamNumber = {0};", "Teleop");

			OuterAvgAuto = new Stat("SELECT AVG(AutoOuter) FROM RawData WHERE TeamNumber = {0};", "Auto");
			OuterAvgTele = new Stat("SELECT AVG(TeleOuter) FROM RawData WHERE TeamNumber = {0};", "Teleop");

			InnerAvgAuto = new Stat("SELECT AVG(AutoInner) FROM RawData WHERE TeamNumber = {0};", "Auto");
			InnerAvgTele = new Stat("SELECT AVG(TeleInner) FROM RawData WHERE TeamNumber = {0};", "Teleop");

			TotalAvgAuto = new Stat("SELECT AVG(AutoLower + AutoOuter + AutoInner) FROM RawData WHERE TeamNumber = {0};", "Auto");
			TotalAvgTele = new Stat("SELECT AVG(TeleLower + TeleOuter + TeleInner) FROM RawData WHERE TeamNumber = {0};", "Teleop");
			TotalPercentAuto = new Stat("SELECT 100.0 * (SUM(AutoLower + AutoOuter + AutoInner) - SUM(AutoMissed)) / SUM(AutoLower + AutoOuter + AutoInner) FROM RawData WHERE TeamNumber = {0};", "Auto Accuracy", "%");
			TotalPercentTele = new Stat("SELECT 100.0 * (SUM(TeleLower + TeleOuter + TeleInner) - SUM(TeleMissed)) / SUM(TeleLower + TeleOuter + TeleInner) FROM RawData WHERE TeamNumber = {0};", "Teleop Accuracy", "%");
			TotalMaxAuto = new Stat("SELECT MAX(AutoLower + AutoOuter + AutoInner) FROM RawData WHERE TeamNumber = {0};", "Auto Max");
			TotalMaxTele = new Stat("SELECT MAX(TeleLower + TeleOuter + TeleInner) FROM RawData WHERE TeamNumber = {0};", "Teleop Max");

			MissedAvgAuto = new Stat("SELECT AVG(AutoMissed) FROM RawData WHERE TeamNumber = {0};", "Auto");
			MissedAvgTele = new Stat("SELECT AVG(TeleMissed) FROM RawData WHERE TeamNumber = {0};", "Teleop");

			DroppedAvgAuto = new Stat("SELECT AVG(AutoDropped) FROM RawData WHERE TeamNumber = {0};", "Auto");
			DroppedAvgTele = new Stat("SELECT AVG(TeleDropped) FROM RawData WHERE TeamNumber = {0};", "Teleop");

			PointsAvgAuto = new Stat("SELECT AVG(InitLine * 5 + AutoLower * 2 + AutoOuter * 4 + AutoInner * 6) FROM RawData WHERE TeamNumber = {0};", "Auto Avg");
			PointsAvgTele = new Stat("SELECT AVG(TeleLower * 1 + TeleOuter * 2 + TeleInner * 3 + RotationControl * 10 + PositionControl * 20 + ClimbSuccess * 25 + Park * 5 + ClimbBalanced * 15) FROM RawData WHERE TeamNumber = {0};", "Teleop Avg");
			PointsMaxAuto = new Stat("SELECT MAX(InitLine * 5 + AutoLower * 2 + AutoOuter * 4 + AutoInner * 6) FROM RawData WHERE TeamNumber = {0};", "Auto Max");
			PointsMaxTele = new Stat("SELECT MAX(TeleLower * 1 + TeleOuter * 2 + TeleInner * 3 + RotationControl * 10 + PositionControl * 20 + ClimbSuccess * 25 + Park * 5 + ClimbBalanced * 15) FROM RawData WHERE TeamNumber = {0};", "Teleop Max");

			FoulsAvg = new Stat("SELECT AVG(Fouls) FROM RawData WHERE TeamNumber = {0};", "Fouls");

			InitLinePercent = new Stat("SELECT 100.0 * SUM(InitLine) / COUNT() FROM RawData WHERE TeamNumber = {0};", "Leave Init Line", "%");
			ClimbPercent = new Stat("SELECT 100.0 * SUM(ClimbSuccess) / COUNT() FROM RawData WHERE TeamNumber = {0} AND ClimbAttempt = 1;", "Climb Success", "%");

			UpdateTeamStatsListCommand = new UpdateTeamStatsListCommand(this);

			_stats = new Stat[] {
				LowerAvgAuto,
				LowerAvgTele,
				OuterAvgAuto,
				OuterAvgTele,
				InnerAvgAuto,
				InnerAvgTele,
				TotalAvgAuto,
				TotalAvgTele,
				TotalPercentAuto,
				TotalPercentTele,
				TotalMaxAuto,
				TotalMaxTele,
				MissedAvgAuto,
				MissedAvgTele,
				DroppedAvgAuto,
				DroppedAvgTele,
				PointsAvgAuto,
				PointsAvgTele,
				PointsMaxAuto,
				PointsMaxTele,
				FoulsAvg,
				InitLinePercent,
				ClimbPercent
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
