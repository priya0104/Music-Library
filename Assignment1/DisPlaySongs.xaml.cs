using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Assignment1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DisPlaySongs : Page
    {
        public DisPlaySongs()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                ResetPageCache();
            }
        }

        private void ResetPageCache()
        {
            var cacheSize = ((Frame)Parent).CacheSize;
            ((Frame)Parent).CacheSize = 0;
            ((Frame)Parent).CacheSize = cacheSize;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            
            //var frame = this.Parent as Frame;
            //frame.SetNavigationState(String.Empty);

            //navigate to this page based on buttons (button1 and button2) click 
            

            var songsList = e.Parameter as IList<Song>;
            this.DataContext = songsList;
        }




        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var song = e.ClickedItem as Song;
            StorageFile file = await StorageFile.GetFileFromPathAsync(song.FilePath);
            var frame = this.Parent as Frame;
            var creator = frame.Parent as NavigationView;
            var grid = creator.Parent as Grid;
            var player = grid.FindName("mediaPlayer") as MediaPlayerElement;
            player.Source = MediaSource.CreateFromStorageFile(file);
        }
    }
}
