using ScoutingClient2020.Static;
using System;

namespace ScoutingClient2020 {
	class Stat {
		private double? _value = null;
		private readonly string
			_query,
			_label,
			_suffix;

		/// <summary>
		/// Initializes an instance of the Stat class.
		/// </summary>
		/// <param name="query">SQL query to get data.</param>
		/// <param name="label">Text to display.</param>
		/// <param name="suffix">Suffix of number (unit, percent, item name, etc.).</param>
		public Stat(string query, string label = "", string suffix = "") {
			_query = query;
			_label = label;
			_suffix = suffix;
		}

		/// <summary>
		/// Updates value by executing query on the database.
		/// </summary>
		/// <param name="team">Team to get data for.</param>
		public void Update(int team) {
			try {
				_value = Math.Round(DBClient.ExecuteQuery(string.Format(_query, team), true)[0], 2);
			} catch (ArgumentOutOfRangeException) {
				_value = null;
			}
		}

		/// <summary>
		/// Converts Stat.Value to its string representation.
		/// </summary>
		/// <returns>Stat.Value as a string.</returns>
		public override string ToString() {
			return _label + ": " + (_value.HasValue ? _value + _suffix : "null");
		}
	}
}
