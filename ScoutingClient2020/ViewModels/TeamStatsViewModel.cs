using ScoutingClient2020.Commands;
using ScoutingClient2020.Static;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace ScoutingClient2020.ViewModels {
	class TeamStatsViewModel : INotifyPropertyChanged {
		public List<int> Teams {
			get {
				return _teams;
			}
			set {
				_teams = value;
				OnPropertyChanged(nameof(Teams));
			}
		}
		public int SelectedTeam {
			get {
				return _selectedTeam;
			}
			set {
				_selectedTeam = value;
				OnPropertyChanged(nameof(SelectedTeam));
			}
		}
		public Stat TestStat {
			get {
				return _testStat;
			}
			set {
				_testStat = value;
				OnPropertyChanged(nameof(TestStat));
			}
		}

		public ICommand UpdateCommand { get; set; }

		private List<int> _teams;
		private int _selectedTeam;
		private Stat _testStat;
		private readonly Stat[] _stats;

		public TeamStatsViewModel() {
			_teams= new List<int>();
			_testStat = new Stat("SELECT 100.0 * SUM(CrossHabLine) / COUNT() FROM RawData WHERE TeamNumber = {0} AND StartPosition BETWEEN 3 AND 5;", "Test Value", "units");
			_stats = new Stat[] { _testStat };

			UpdateCommand = new UpdateTeamStatsCommand(this);

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
				_teams.Add((int)team);
			}
		}

		public bool CanUpdate() {
			return true;
		}

		public void Update() {
			foreach (Stat stat in _stats)
				stat.Update(_selectedTeam);
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}
