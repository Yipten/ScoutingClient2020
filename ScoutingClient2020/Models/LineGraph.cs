using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace ScoutingClient2020.Models {
	class LineGraph : INotifyPropertyChanged {
		public PointCollection Points { get => _points; set { _points = value; OnPropertyChanged(nameof(Points)); } }
		public Brush Color { get; set; }
		public DoubleCollection StrokeDashArray { get; set; }

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
				for (int i = 0; i < data.Count; i++)
					_points.Add(new Point(i * 10, data[i]));
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
