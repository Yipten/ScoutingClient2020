using ScoutingClient2020.Commands;
using ScoutingClient2020.Static;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace ScoutingClient2020.ViewModels {
	class TeamStatsViewModel : INotifyPropertyChanged {
		public List<int> Teams { get; set; }
		public int SelectedTeam { get => _selectedTeam; set { _selectedTeam = value; Update(); } }
		public Stat TestStat { get; set; }

		public ICommand UpdateCommand { get; set; }

		private readonly Stat[] _stats;
		private int _selectedTeam;

		public TeamStatsViewModel() {
			Teams = new List<int>();
			TestStat = new Stat("SELECT 100.0 * SUM(CrossHabLine) / COUNT() FROM RawData WHERE TeamNumber = {0} AND StartPosition BETWEEN 3 AND 5;", "Test Value", "units");
			_stats = new Stat[] { TestStat };

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
				Teams.Add((int)team);
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
