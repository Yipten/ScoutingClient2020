﻿using System;
using System.ComponentModel;

namespace ScoutingClient2020.Models {
	class Stat : INotifyPropertyChanged {
		public string Display { get => _display; set { _display = value; OnPropertyChanged(nameof(Display)); } }

		private readonly string _query;
		private readonly string _label;
		private readonly string _suffix;
		
		private double? _value;
		private string _display;

		/// <summary>
		/// Initializes an instance of the Stat class.
		/// </summary>
		/// <param name="query">SQL query to get data.</param>
		/// <param name="label">Text to display.</param>
		/// <param name="suffix">Suffix of number (unit, percent, item name, etc.).</param>
		public Stat(string query, string label = "", string suffix = "") {
			_value = null;
			_query = query;
			_label = label;
			_suffix = suffix;
		}

		/// <summary>
		/// Updates value by executing a query on the database.
		/// </summary>
		/// <param name="team">Team to get data for.</param>
		public void Update(int team) {
			try {
				_value = Math.Round(DBClient.ExecuteQuery(string.Format(_query, team), true)[0], 2);
			} catch (ArgumentOutOfRangeException) {
				_value = null;
			}
			Display = ToString();
		}

		/// <summary>
		/// Converts Stat.Value to its string representation.
		/// </summary>
		/// <returns>Stat.Value as a string.</returns>
		public override string ToString() {
			return (string.IsNullOrWhiteSpace(_label) ? null : _label + ": ") + (_value.HasValue ? _value + _suffix : "null");
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}
