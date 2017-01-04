using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Sequence_Player
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

		private async void button_Tapped(object sender, TappedRoutedEventArgs e) {
			var picker = new Windows.Storage.Pickers.FileOpenPicker();
			picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
			picker.SuggestedStartLocation =
				Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
			picker.FileTypeFilter.Add(".mp3");
			picker.FileTypeFilter.Add(".wav");

			Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
			if (file != null) {
				// Application now has read/write access to the picked file
				//this.textBlock.Text = "Picked photo: " + file.Name;
				var stream = await file.OpenAsync(FileAccessMode.Read);

				TextBlock tb = new TextBlock();
				tb.Text = file.Name;

				MediaElement elem = new MediaElement();
				elem.Stretch = Stretch.Fill;
				elem.Height = 100;
				elem.AreTransportControlsEnabled = true;
				elem.AutoPlay = false;
				elem.SetSource(stream,file.FileType);

				container.Children.Add(tb);
				container.Children.Add(elem);

			}
			else {
				//this.textBlock.Text = "Operation cancelled.";
			}


		}

		private void HamburgerButton_Click(object sender, RoutedEventArgs e) {
			MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
		}
	}
}
