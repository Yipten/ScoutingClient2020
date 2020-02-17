using ScoutingClient2020.Commands;
using ScoutingClient2020.Models;
using System.Windows;
using System.Windows.Input;

namespace ScoutingClient2020.ViewModels {
	class DataManagementViewModel {
		public BTClient Bluetooth { get; set; }

		public ICommand ReceiveFileCommand { get; private set; }
		public ICommand CancelCommand { get; private set; }
		public ICommand MergeCommand { get; private set; }

		/// <summary>
		/// Initializes a new instance of the DataManagementViewModel class.
		/// </summary>
		public DataManagementViewModel() {
			Bluetooth = new BTClient("C:/Users/Andrew/Documents/Team 20/2019-20/Scouting/Data/");

			ReceiveFileCommand = new ReceiveFileCommand(this);
			CancelCommand = new CancelCommand(this);
			MergeCommand = new MergeCommand(this);
		}

		/// <summary>
		/// Receivies a file via bluetooth.
		/// </summary>
		public void ReceiveFile() {
			if (Bluetooth.IsBluetoothSupported) {
				Bluetooth.ReceiveFile();
			} else
				MessageBox.Show("Bluetooth must be enabled on your device in order to transfer files", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}

		/// <summary>
		/// Cancels the pending bluetooth transfer.
		/// </summary>
		public void Cancel() {
			Bluetooth.Cancel();
		}

		/// <summary>
		/// Merges data from tablets into database on computer.
		/// </summary>
		public void Merge() {
			DBClient.Merge("2020_test");
		}
	}
}
