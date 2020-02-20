using Microsoft.Win32;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows;

namespace ScoutingClient2020.Models {
	static class DBClient {
		private static readonly SQLiteConnection _connection;
		private static readonly string _filePath;
		private static readonly string _fileName;

		/// <summary>
		/// Static constructor for the DBClient class.
		/// </summary>
		static DBClient() {
			_filePath = "C:/Users/Andrew/Documents/Team 20/2019-20/Scouting/Data/";
			_fileName = "2020_test_master.sqlite";
			// if the file doesn't exist...
			while (!File.Exists(_filePath + _fileName)) {
				MessageBox.Show("Database file at location \"" + _filePath + _fileName + "\" does not exist.\n\nPlease manually locate the file.", "File not found", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				OpenFileDialog openFileDialog = new OpenFileDialog {
					InitialDirectory = "C:\\",
					Filter = "SQLite database files (*.db; *.db3; *.sqlite; *.sqlite3)|*.db; *.db3; *.sqlite; *.sqlite3 | All files (*.*)|*.*",
					FilterIndex = 1
				};
				if (openFileDialog.ShowDialog() == true) {
					_fileName = openFileDialog.SafeFileName;
					string tempFilePath = openFileDialog.FileName;
					_filePath = tempFilePath.Substring(0, tempFilePath.IndexOf(_fileName));
				}
			}
			// connect to database
			_connection = new SQLiteConnection("Data Source=" + _filePath + _fileName + "; Version=3;");
		}

		/// <summary>
		/// Merges data from tablets into database on computer.
		/// </summary>
		/// <param name="databases">Array of database file names to merge from.</param>
		public static void Merge(string fileName) {
			string[] databases = {
				fileName + "_blue_1",
				fileName + "_blue_2",
				fileName + "_blue_3",
				fileName + "_red_1",
				fileName + "_red_2",
				fileName + "_red_3",
			};
			for (int i = 0; i < databases.Length; i++) {
				string pathTemp = _filePath + databases[i] + ".sqlite";
				// skip file if it doesn't exist
				if (!File.Exists(pathTemp))
					continue;
				// query to merge data into database on computer
				//ExecuteQuery(
				//	// attach database to merge data from
				//	"ATTACH DATABASE '" + pathTemp + "' AS db" + i + ";" +
				//	// insert data into master database
				//	"INSERT INTO RawData(" +
				//		"ScoutName, " +
				//		"MatchNumber, " +
				//		"ReplayMatch, " +
				//		"TeamNumber, " +
				//		"AllianceColor, " +
				//		"StartPosition, " +
				//		"Preloaded, " +
				//		"InitLine, " +
				//		"AutoLower, " +
				//		"AutoOuter, " +
				//		"AutoInner, " +
				//		"AutoMissed, " +
				//		"AutoDropped, " +
				//		"AutoCollected, " +
				//		"TeleLower, " +
				//		"TeleOuter, " +
				//		"TeleInner, " +
				//		"TeleMissed, " +
				//		"TeleDropped, " +
				//		"TeleCollected, " +
				//		"RotationControl, " +
				//		"PositionControl, " +
				//		"Zone, " +
				//		"Park, " +
				//		"ClimbAttempt, " +
				//		"ClimbSuccess, " +
				//		"ClimbBalanced, " +
				//		"HadAssistance, " +
				//		"AssistedOthers, " +
				//		"DefensePlay, " +
				//		"DefensePlayStrength, " +
				//		"DefenseAgainst, " +
				//		"DefenseAgainstStrength, " +
				//		"Fouls, " +
				//		"Role, " +
				//		"Breakdown, " +
				//		"Comments" +
				//	") " +
				//	"SELECT " +
				//		"ScoutName, " +
				//		"MatchNumber, " +
				//		"ReplayMatch, " +
				//		"TeamNumber, " +
				//		"AllianceColor, " +
				//		"StartPosition, " +
				//		"Preloaded, " +
				//		"InitLine, " +
				//		"AutoLower, " +
				//		"AutoOuter, " +
				//		"AutoInner, " +
				//		"AutoMissed, " +
				//		"AutoDropped, " +
				//		"AutoCollected, " +
				//		"TeleLower, " +
				//		"TeleOuter, " +
				//		"TeleInner, " +
				//		"TeleMissed, " +
				//		"TeleDropped, " +
				//		"TeleCollected, " +
				//		"RotationControl, " +
				//		"PositionControl, " +
				//		"Zone, " +
				//		"Park, " +
				//		"ClimbAttempt, " +
				//		"ClimbSuccess, " +
				//		"ClimbBalanced, " +
				//		"HadAssistance, " +
				//		"AssistedOthers, " +
				//		"DefensePlay, " +
				//		"DefensePlayStrength, " +
				//		"DefenseAgainst, " +
				//		"DefenseAgainstStrength, " +
				//		"Fouls, " +
				//		"Role, " +
				//		"Breakdown, " +
				//		"Comments " +
				//	"FROM db" + i + ".RawData;" +
				//	// detach database when done
				//	"DETACH DATABASE db" + i + ";",
				//	false
				//);
				ExecuteQuery(
					// attach database to merge data from
					"ATTACH DATABASE '" + pathTemp + "' AS db" + i + ";" +
					// insert data into master database
					"INSERT INTO RawData SELECT * FROM db" + i + ".RawData WHERE NOT EXISTS(SELECT * FROM RawData WHERE RawData.ID = db" + i + ".RawData.ID);" +
					// detach database when done
					"DETACH DATABASE db" + i + ";",
					false
				);
				// delete database file after it has been merged from
				bool isDeleted = false;
				while (!isDeleted)
					try {
						File.Delete(pathTemp);
						isDeleted = true;
					} catch (IOException) {
						MessageBox.Show("The file at \"" + pathTemp + "\" is currently open in another program. Please close it and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					}
			}
		}

		/// <summary>
		/// Executes a query on the connected SQLite database.
		/// </summary>
		/// <param name="query">Query to execute.</param>
		/// <param name="read">True if results are desired. False if not.</param>
		/// <returns>List of data retrieved from the database.</returns>
		public static List<double> ExecuteQuery(string query, bool read) {
			if (_connection == null)
				return new List<double>();
			_connection.Open();
			SQLiteCommand command = new SQLiteCommand(query, _connection);
			// comma-separated form of data
			string dataCSV = "";
			try {
				if (read) {
					SQLiteDataReader reader = command.ExecuteReader();
					while (reader.Read())
						for (int i = 0; i < reader.FieldCount; i++)
							dataCSV += reader[i] + ",";
				} else
					dataCSV = command.ExecuteNonQuery().ToString();
			} catch (SQLiteException e) {
				MessageBox.Show("SQLiteException thrown\n\n" + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			} finally {
				command.Dispose();
			}
			_connection.Close();
			// string form of data separated by commas
			List<string> dataString = dataCSV.Split(',').ToList();
			// numerical form of data
			List<double> data = new List<double>();
			foreach (string s in dataString)
				if (!string.IsNullOrWhiteSpace(s))
					data.Add(double.Parse(s));
			return data;
		}

		/// <summary>
		/// Gets all teams that are recorded in the database.
		/// </summary>
		/// <returns>List of team numbers.</returns>
		public static List<int> GetTeams() {
			List<int> teams = new List<int>();
			List<double> doubleTeams = ExecuteQuery("SELECT DISTINCT TeamNumber FROM RawData ORDER BY TeamNumber ASC;", true);
			foreach (double team in doubleTeams)
				teams.Add((int)team);
			return teams;
		}
	}
}
