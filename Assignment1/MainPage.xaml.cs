using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Assignment1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MusicCrawler musicCrawler = new MusicCrawler();
        public MainPage()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);   

        }

        private void Albums_ItemClick(object sender, ItemClickEventArgs e)
        {
            //get the clicked item (ie is album) and convert that into item of type Album
            //album is the instance of type album that was clicked on in a gridview
            var album = (Album)e.ClickedItem;

            this.Frame.Navigate(typeof(DisPlaySongs), album);
        }

        private void NvSample_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {

        }

        private void NvSample_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async void NvSample_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem item = args.SelectedItem as NavigationViewItem;

            switch (item.Tag.ToString())
            {
                case "song":
                    if (musicCrawler.IsMusicScanned == false)
                    {
                        await musicCrawler.EnumerateAndGetAlbumsAsync();
                    }
                    var songs = await musicCrawler.GetSongsAsync();
                    contentFrame.Navigate(typeof(DisPlaySongs),songs);
                    nvSample.Header = "Songs";
                    break;

                case "albums":
                    var album = await musicCrawler.EnumerateAndGetAlbumsAsync();
                    contentFrame.Navigate(typeof(DisplayAlbums), album);
                    nvSample.Header = "Albums";
                    break;

                case "playList":
                    contentFrame.Navigate(typeof(PlayLists), musicCrawler);
                    nvSample.Header = "Playlist";
                    break;

                default:
                    break;
            }
        }

        //private async void Songs_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    var song = e.ClickedItem as Song;
        //    StorageFile file = await StorageFile.GetFileFromPathAsync(song.FilePath);

        //    mediaPlayer.Source = MediaSource.CreateFromStorageFile(file);
        //    //mediaPlayer.AutoPlay = true;
        //    mediaPlayer.MediaPlayer.Dispose();
        //}
    }
}
