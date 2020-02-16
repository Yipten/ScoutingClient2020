using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace ScoutingClient2020.Models {
	class LineGraph : INotifyPropertyChanged {
		public PointCollection Points { get => _points; set { _points = value; OnPropertyChanged(nameof(Points)); } }
		public Brush Color { get; set; }
		public DoubleCollection StrokeDashArray { get; set; }

		public const int GlobalWidth = 200;
		public const int GlobalHeight = 150;

		private readonly string _query;

		private PointCollection _points;

		/// <summary>
		/// Initializes a new instance of the LineGraph class.
		/// </summary>
		/// <param name="query">SQL query to get data.</param>
		/// <param name="color">Color of line.</param>
		/// <param name="dashed">Whether the line is dashed.</param>
		public LineGraph(string query, Brush color, bool dashed) {
			_query = query;
			Color = color;
			StrokeDashArray = dashed ? new DoubleCollection(new double[] { 4 }) : null;
			_points = new PointCollection();
		}

		/// <summary>
		/// Updates all line plots by executing a query on the database.
		/// </summary>
		/// <param name="team">Team to get data for.</param>
		public void Update(int team) {
			try {
				List<double> data = DBClient.ExecuteQuery(string.Format(_query, team), true);
				double max = double.MinValue;
				foreach (double d in data)
					if (d > max)
						max = d;
				for (int i = 0; i < data.Count; i++)
					_points.Add(new Point(i * (GlobalHeight / (data.Count - 1)), GlobalHeight - data[i] * (GlobalHeight / max)));
			} catch {
				_points.Clear();
			}
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}
