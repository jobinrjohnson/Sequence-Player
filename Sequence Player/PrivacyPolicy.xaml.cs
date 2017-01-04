using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Sequence_Player {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class PrivacyPolicy : Page {
		public PrivacyPolicy() {
			this.InitializeComponent();
		}

		protected override async void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);
			var html = await Windows.Storage.PathIO.ReadTextAsync("ms-appx:///Assets/html/privacypolicy.html");
			web.NavigateToString(html);
		}

		private void StackPanel_Unloaded(object sender, TappedRoutedEventArgs e) {
			Frame.Navigate(typeof(MainPage));
		}

		private void HamburgerButton_Click(object sender, RoutedEventArgs e) {
			MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
		}

		private void StackPanel_Unloaded_1(object sender, TappedRoutedEventArgs e) {
			Frame.Navigate(typeof(HelpAsk));
		}
	}
}
