using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Sequence_Player {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class HelpAsk : Page {
		public HelpAsk() {
			this.InitializeComponent();
		}

		private void StackPanel_Unloaded(object sender, TappedRoutedEventArgs e) {
			Frame.Navigate(typeof(MainPage));
		}

		private void HamburgerButton_Click(object sender, RoutedEventArgs e) {
			MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
		}

		private void StackPanel_Unloaded_1(object sender, TappedRoutedEventArgs e) {
			Frame.Navigate(typeof(PrivacyPolicy));
		}

		public async void getBoard() {
			String myname = Namee.Text.Trim();
			String myemail = email.Text.Trim();
			String mysubject ="Sequence Player | "+ subject.Text.Trim();
			String mymsg = Message.Text.Trim();

			String messbox = null;
			if (myname.Length < 1)
				messbox = "Enter a valid name";
			else if (myemail.Length > 1 && !new EmailAddressAttribute().IsValid(myemail))
				messbox = "Enter a valid email";
			else if (mysubject.Length < 1)
				messbox = "Enter a valid subject";
			else if (mymsg.Length < 1)
				messbox = "Enter a valid message text";

			if (messbox != null) {
				await new MessageDialog(messbox).ShowAsync();
				return;
			}

			if (!NetworkInterface.GetIsNetworkAvailable()) {
				await new MessageDialog("You are not connected to internet").ShowAsync();
				return;
			}

			var client = new HttpClient();
			var pairs = new List<KeyValuePair<string, string>>{
				new KeyValuePair<string, string>("name",myname),
				new KeyValuePair<string, string>("email",myemail),
				new KeyValuePair<string, string>("subject",mysubject),
				new KeyValuePair<string, string>("msg",mymsg)
			};
			var content = new FormUrlEncodedContent(pairs);
			try {
				var response = client.PostAsync("http://www.crazykeys.in/api/feedback", content).Result;

				if (response.IsSuccessStatusCode) {
					var contents = await response.Content.ReadAsStringAsync();
					JsonObject jo = JsonObject.Parse(contents).GetObject();
					if (jo.GetNamedNumber("status") == 1) {
						await new MessageDialog(jo.GetNamedString("msg")).ShowAsync();
					}
					else {
						await new MessageDialog(jo.GetNamedString("msg")).ShowAsync();
					}

				}
			}
			catch {
				await new MessageDialog("Unable to send").ShowAsync();
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			getBoard();
		}
	}
}
