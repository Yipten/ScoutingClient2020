using System.ComponentModel;

namespace ScoutingClient2020.Models {
	class Scorer : INotifyPropertyChanged {
		public bool InitLine { get => _initLine; set { _initLine = value; UpdateScore(); } }
		public int AutoBottom { get => _autoBottom; set { _autoBottom = value; UpdateScore(); } }
		public int AutoOuter { get => _autoOuter; set { _autoOuter = value; UpdateScore(); } }
		public int AutoInner { get => _autoInner; set { _autoInner = value; UpdateScore(); } }
		public int TeleBottom { get => _teleBottom; set { _teleBottom = value; UpdateScore(); } }
		public int TeleOuter { get => _teleOuter; set { _teleOuter = value; UpdateScore(); } }
		public int TeleInner { get => _teleInner; set { _teleInner = value; UpdateScore(); } }
		public bool RotationControl { get => _rotationControl; set { _rotationControl = value; UpdateScore(); } }
		public bool PositionControl { get => _positionControl; set { _positionControl = value; UpdateScore(); } }
		public bool Parked { get => _parked; set { _parked = value; UpdateScore(); } }
		public bool Climbed { get => _climbed; set { _climbed = value; UpdateScore(); } }
		public bool Balanced { get => _balanced; set { _balanced = value; UpdateScore(); } }
		public string ScoreStr { get => _scoreStr; set { _scoreStr = value; OnPropertyChanged(nameof(ScoreStr)); } }

		private bool _initLine;
		private int _autoBottom;
		private int _autoOuter;
		private int _autoInner;
		private int _teleBottom;
		private int _teleOuter;
		private int _teleInner;
		private bool _rotationControl;
		private bool _positionControl;
		private bool _parked;
		private bool _climbed;
		private bool _balanced;
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
			Parked = false;
			Climbed = false;
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
				(Parked ? 5 : 0) +
				(Climbed ? 25 : 0) +
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
