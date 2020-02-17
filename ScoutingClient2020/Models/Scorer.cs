using System.ComponentModel;

namespace ScoutingClient2020.Models {
	class Scorer : INotifyPropertyChanged {
		public bool InitLine { get => _initLine; set { _initLine = value; OnPropertyChanged(nameof(InitLine)); UpdateScore(); } }
		public int AutoLower { get => _AutoLower; set { _AutoLower = value; OnPropertyChanged(nameof(AutoLower)); UpdateScore(); } }
		public int AutoOuter { get => _autoOuter; set { _autoOuter = value; OnPropertyChanged(nameof(AutoOuter)); UpdateScore(); } }
		public int AutoInner { get => _autoInner; set { _autoInner = value; OnPropertyChanged(nameof(AutoInner)); UpdateScore(); } }
		public int TeleLower { get => _TeleLower; set { _TeleLower = value; OnPropertyChanged(nameof(TeleLower)); UpdateScore(); } }
		public int TeleOuter { get => _teleOuter; set { _teleOuter = value; OnPropertyChanged(nameof(TeleOuter)); UpdateScore(); } }
		public int TeleInner { get => _teleInner; set { _teleInner = value; OnPropertyChanged(nameof(TeleInner)); UpdateScore(); } }
		public bool RotationControl { get => _rotationControl; set { _rotationControl = value; OnPropertyChanged(nameof(RotationControl)); UpdateScore(); } }
		public bool PositionControl { get => _positionControl; set { _positionControl = value; OnPropertyChanged(nameof(PositionControl)); UpdateScore(); } }
		public bool Parked { get => _parked; set { _parked = value; OnPropertyChanged(nameof(Parked)); UpdateScore(); } }
		public bool Climbed { get => _climbed; set { _climbed = value; OnPropertyChanged(nameof(Climbed)); UpdateScore(); } }
		public bool Balanced { get => _balanced; set { _balanced = value; OnPropertyChanged(nameof(Balanced)); UpdateScore(); } }
		public string ScoreStr { get => _scoreStr; set { _scoreStr = value; OnPropertyChanged(nameof(ScoreStr)); } }

		private bool _initLine;
		private int _AutoLower;
		private int _autoOuter;
		private int _autoInner;
		private int _TeleLower;
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
			AutoLower = 0;
			AutoOuter = 0;
			AutoInner = 0;
			TeleLower = 0;
			TeleOuter = 0;
			TeleInner = 0;
			RotationControl = false;
			PositionControl = false;
			Parked = false;
			Climbed = false;
			Balanced = false;
		}

		/// <summary>
		/// 
		/// </summary>
		public void UpdateScore() {
			_score =
				(InitLine ? 5 : 0) +
				AutoLower * 2 +
				AutoOuter * 4 +
				AutoInner * 6 +
				TeleLower * 1 +
				TeleOuter * 2 +
				TeleInner * 3 +
				(RotationControl ? 10 : 0) +
				(PositionControl ? 20 : 0) +
				(Parked ? 5 : 0) +
				(Climbed ? 25 : 0) +
				(Balanced ? 15 : 0);
			ScoreStr = "Score: " + _score.ToString();
		}

		public void ResetScore() {
			InitLine = false;
			AutoLower = 0;
			AutoOuter = 0;
			AutoInner = 0;
			TeleLower = 0;
			TeleOuter = 0;
			TeleInner = 0;
			RotationControl = false;
			PositionControl = false;
			Parked = false;
			Climbed = false;
			Balanced = false;
			UpdateScore();
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}
