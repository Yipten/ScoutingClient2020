using ScoutingClient2020.Commands;
using ScoutingClient2020.Models;
using System.Windows.Input;

namespace ScoutingClient2020.ViewModels {
	class TeamStatsViewModel {
		public TeamList TeamList { get; set; }
		public int SelectedTeam { get => _selectedTeam; set { _selectedTeam = value; Update(); } }

		public Stat LowAvgAuto { get; set; }
		public Stat LowAvgTele { get; set; }

		public Stat HighAvgAuto { get; set; }
		public Stat HighAvgTele { get; set; }

		public Stat TotalAvgAuto { get; set; }
		public Stat TotalAvgTele { get; set; }
		public Stat TotalPercentAuto { get; set; }
		public Stat TotalPercentTele { get; set; }
		public Stat TotalMaxAuto { get; set; }
		public Stat TotalMaxTele { get; set; }

		public Stat DroppedAvgAuto { get; set; }
		public Stat DroppedAvgTele { get; set; }

		//public Stat PointsAvgAuto { get; set; }
		//public Stat PointsAvgTele { get; set; }
		//public Stat PointsMaxAuto { get; set; }
		//public Stat PointsMaxTele { get; set; }

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
			LowAvgAuto = new Stat("SELECT AVG(AutoLow) FROM RawData WHERE TeamNumber = {0};", "Auto");
			LowAvgTele = new Stat("SELECT AVG(TeleLow) FROM RawData WHERE TeamNumber = {0};", "Teleop");

			HighAvgAuto = new Stat("SELECT AVG(AutoHigh) FROM RawData WHERE TeamNumber = {0};", "Auto");
			HighAvgTele = new Stat("SELECT AVG(TeleHigh) FROM RawData WHERE TeamNumber = {0};", "Teleop");

			TotalAvgAuto = new Stat("SELECT AVG(AutoLow + AutoHigh) FROM RawData WHERE TeamNumber = {0};", "Auto");
			TotalAvgTele = new Stat("SELECT AVG(TeleLow + TeleHigh) FROM RawData WHERE TeamNumber = {0};", "Teleop");
			TotalPercentAuto = new Stat("SELECT 100.0 * SUM(AutoLow + AutoHigh) / (SUM(AutoLow + AutoHigh) + SUM(AutoDropped)) FROM RawData WHERE TeamNumber = {0};", "Auto Accuracy", "%");
			TotalPercentTele = new Stat("SELECT 100.0 * SUM(TeleLow + TeleHigh) / (SUM(TeleLow + TeleHigh) + SUM(TeleDropped)) FROM RawData WHERE TeamNumber = {0};", "Teleop Accuracy", "%");
			TotalMaxAuto = new Stat("SELECT MAX(AutoLow + AutoHigh) FROM RawData WHERE TeamNumber = {0};", "Auto Max");
			TotalMaxTele = new Stat("SELECT MAX(TeleLow + TeleHigh) FROM RawData WHERE TeamNumber = {0};", "Teleop Max");

			DroppedAvgAuto = new Stat("SELECT AVG(AutoDropped) FROM RawData WHERE TeamNumber = {0};", "Auto");
			DroppedAvgTele = new Stat("SELECT AVG(TeleDropped) FROM RawData WHERE TeamNumber = {0};", "Teleop");

			//PointsAvgAuto = new Stat("SELECT AVG(InitLine * 5 + AutoLow * 2 + AutoOuter * 4 + AutoInner * 6) FROM RawData WHERE TeamNumber = {0};", "Auto Avg");
			//PointsAvgTele = new Stat("SELECT AVG(TeleLow * 1 + TeleOuter * 2 + TeleInner * 3 + RotationControl * 10 + PositionControl * 20 + ClimbSuccess * 25 + Park * 5 + ClimbBalanced * 15) FROM RawData WHERE TeamNumber = {0};", "Teleop Avg");
			//PointsMaxAuto = new Stat("SELECT MAX(InitLine * 5 + AutoLow * 2 + AutoOuter * 4 + AutoInner * 6) FROM RawData WHERE TeamNumber = {0};", "Auto Max");
			//PointsMaxTele = new Stat("SELECT MAX(TeleLow * 1 + TeleOuter * 2 + TeleInner * 3 + RotationControl * 10 + PositionControl * 20 + ClimbSuccess * 25 + Park * 5 + ClimbBalanced * 15) FROM RawData WHERE TeamNumber = {0};", "Teleop Max");

			FoulsAvg = new Stat("SELECT AVG(Fouls) FROM RawData WHERE TeamNumber = {0};", "Fouls");

			InitLinePercent = new Stat("SELECT 100.0 * SUM(InitLine) / COUNT() FROM RawData WHERE TeamNumber = {0};", "Leave Init Line", "%");
			ClimbPercent = new Stat("SELECT 100.0 * SUM(ClimbSuccess) / COUNT() FROM RawData WHERE TeamNumber = {0} AND ClimbAttempt = 1;", "Climb Success", "%");

			TeamList = new TeamList();

			UpdateTeamStatsListCommand = new UpdateTeamStatsListCommand(this);

			_stats = new Stat[] {
				LowAvgAuto,
				LowAvgTele,
				HighAvgAuto,
				HighAvgTele,
				TotalAvgAuto,
				TotalAvgTele,
				TotalPercentAuto,
				TotalPercentTele,
				TotalMaxAuto,
				TotalMaxTele,
				DroppedAvgAuto,
				DroppedAvgTele,
				//PointsAvgAuto,
				//PointsAvgTele,
				//PointsMaxAuto,
				//PointsMaxTele,
				FoulsAvg,
				InitLinePercent,
				ClimbPercent
			};

			TeamList.Update();
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
			TeamList.Update();
		}
	}
}
