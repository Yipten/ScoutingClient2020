using System.Collections.Generic;
using System.ComponentModel;

namespace ScoutingClient2020.Models {
	class TeamList : INotifyPropertyChanged {
		public List<int> Teams { get => _teams; set { _teams = value; OnPropertyChanged(nameof(Teams)); } }

		private List<int> _teams;

		/// <summary>
		/// Initializes a new instance of the TeamList class.
		/// </summary>
		public TeamList() {
			Teams = new List<int>();
			Update();
		}

		/// <summary>
		/// Updates the list of teams.
		/// </summary>
		public void Update() {
			Teams = DBClient.GetTeams();
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}
