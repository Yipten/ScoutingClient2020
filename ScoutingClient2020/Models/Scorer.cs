using System.ComponentModel;

namespace ScoutingClient2020.Models {
	class Scorer : INotifyPropertyChanged {
		public bool InitLine { get; set; }
		public int AutoBottom { get; set; }
		public int AutoOuter { get; set; }
		public int AutoInner { get; set; }
		public int TeleBottom { get; set; }
		public int TeleOuter { get; set; }
		public int TeleInner { get; set; }
		public bool RotationControl { get; set; }
		public bool PositionControl { get; set; }
		public bool Park { get; set; }
		public bool Climb { get; set; }
		public bool Balanced { get; set; }
		public string ScoreStr { get => _scoreStr; set { _scoreStr = value; OnPropertyChanged(nameof(ScoreStr)); } }

		private int _score;
		private string _scoreStr;

		public Scorer() {
			InitLine = false;
			AutoBottom = 0;
			AutoOuter = 0;
			AutoInner = 0;
			TeleBottom = 0;
			TeleOuter = 0;
			TeleInner = 0;
			RotationControl = false;
			PositionControl = false;
			Park = false;
			Climb = false;
			Balanced = false;
		}

		public void UpdateScore() {
			_score =
				(InitLine ? 5 : 0) +
				AutoBottom * 2 +
				AutoOuter * 4 +
				AutoInner * 6 +
				TeleBottom * 1 +
				TeleOuter * 2 +
				TeleInner * 3 +
				(RotationControl ? 10 : 0) +
				(PositionControl ? 20 : 0) +
				(Park ? 5 : 0) +
				(Climb ? 25 : 0) +
				(Balanced ? 15 : 0);
			ScoreStr = "Score: " + _score.ToString();
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}
