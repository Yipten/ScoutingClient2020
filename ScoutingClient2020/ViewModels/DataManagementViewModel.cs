using ScoutingClient2020.Commands;
using ScoutingClient2020.Static;
using System.Windows.Input;

namespace ScoutingClient2020.ViewModels {
	class DataManagementViewModel {
		public bool IsReceiving { get; set; }		// TODO: move this to BTClient class and make it OO (not static)

		public ICommand ReceiveFileCommand { get; private set; }
		public ICommand CancelCommand { get; private set; }

		/// <summary>
		/// Initializes a new instance of the DataManagementViewModel class.
		/// </summary>
		public DataManagementViewModel() {
			IsReceiving = false;

			ReceiveFileCommand = new ReceiveFileCommand(this);
			CancelCommand = new CancelCommand(this);
		}

		public void ReceiveFile() {
			BTClient.ReceiveFile();
			IsReceiving = true;
		}

		public void Cancel() {
			BTClient.Cancel();
			IsReceiving = false;
		}
	}
}
