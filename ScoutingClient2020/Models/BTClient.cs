using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ScoutingClient2020.Models {
	class BTClient : INotifyPropertyChanged {
		public bool IsBluetoothSupported => BluetoothRadio.IsSupported;
		public bool IsReceiving { get => _isReceiving; set { _isReceiving = value; OnPropertyChanged(nameof(IsReceiving)); } }

		private readonly string _filePath;
		private CancellationTokenSource _cancellationTokenSource;
		private bool _isReceiving;
		private int _count;
		private int _times;

		/// <summary>
		/// Initializes a new instance of the BTClient class.
		/// </summary>
		public BTClient(string filePath) {
			_filePath = filePath;
			_isReceiving = false;
			_count = 0;
			_times = 0;

			Nope();
		}

		/// <summary>
		/// Receives a file via bluetooth.
		/// </summary>
		public async void ReceiveFile() {
			_isReceiving = true;
			// create new CancellationToken for this file transfer
			_cancellationTokenSource = new CancellationTokenSource();
			CancellationToken cancellationToken = _cancellationTokenSource.Token;
			// allow radio to be connected to by other devices
			BluetoothRadio.PrimaryRadio.Mode = RadioMode.Connectable;
			// create bluetooth file listener
			ObexListener listener = new ObexListener(ObexTransport.Bluetooth);
			// allow for files to be received
			listener.Start();
			Task t = new Task(() => {
				// wait for file to be received
				ObexListenerContext context = listener.GetContext();
				if (context != null) {
					// get file information
					ObexListenerRequest request = context.Request;
					// keep original file name
					string[] splitPath = request.RawUrl.Split('/');
					string fileName = splitPath[splitPath.Length - 1];
					// save file to disk
					request.WriteFile(_filePath + fileName);
				}
			});
			// run task to start pending file transfer
			t.Start();
			// wait until transfer is either finished or cancelled.
			await Task.Run(() => {
				try {
					t.Wait(cancellationToken);
				} catch (OperationCanceledException) { }
			});
			// release resources
			listener.Stop();
			listener.Close();
			_cancellationTokenSource.Dispose();
			_isReceiving = false;
		}

		/// <summary>
		/// Cancels the pending bluetooth transfer.
		/// </summary>
		public void Cancel() {
			if (_isReceiving) {
				_cancellationTokenSource.Cancel();
				_isReceiving = false;
			}
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion

		public void Yep() {
			if (_count++ >= 5)
				TellTheUserToStopBeingSoIndecisive(_count * _times);
		}

		public void Nope() {
			_count = 0;
			_times++;
		}

		public void TellTheUserToStopBeingSoIndecisive(int seeed) {
			Random random = new Random(seeed);
			string message;
			switch (random.Next(0, 11)) {
				case 0:
					message = "stop being so indecisive";
					break;
				case 1:
					message = "ouch, stop clicking, it hurts";
					break;
				case 2:
					message = "quit it";
					break;
				case 3:
					message = "water game";
					break;
				case 4:
					message = "aoeu";
					break;
				case 5:
					message = "how many times have you clicked that button?";
					break;
				case 6:
					message = "zhhzzzhzhhzhhzhhhh";
					break;
				case 7:
					message = "<Button Content=\"Cancel\" Command=\"{Binding CancelCommand}\" />";
					break;
				case 8:
					message = "you sure like that cancel button, don't you?";
					break;
				case 9:
					message = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
					break;
				default:
					message = "";
					break;
			}
			MessageBox.Show(message, "why", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
			Nope();
		}
	}
}
