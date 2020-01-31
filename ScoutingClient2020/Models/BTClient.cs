using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ScoutingClient2020.Static {
	static class BTClient {
		private static readonly string _filePath;

		private static CancellationTokenSource _cancellationTokenSource;

		/// <summary>
		/// Static constructor for the BTClient class.
		/// </summary>
		static BTClient() {
			_filePath = "C:/Users/Andrew/Documents/Team 20/2019-20/Scouting/Data/";
		}

		/// <summary>
		/// Receives a file via bluetooth.
		/// </summary>
		/// <returns>True if successful.</returns>
		public static async void ReceiveFile() {
			if (!BluetoothRadio.IsSupported) {
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
			}
		}

		/// <summary>
		/// Cancels the pending bluetooth transfer
		/// </summary>
		public static void Cancel() {
			_cancellationTokenSource.Cancel();
		}
	}
}
