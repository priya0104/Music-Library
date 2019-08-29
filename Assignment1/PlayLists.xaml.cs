using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Assignment1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayLists : Page
    {
        private MusicCrawler _instance;
        public IList<PlayList> playList = null;
        public PlayLists()
        {
            this.InitializeComponent();
            playList = new List<PlayList>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _instance = e.Parameter as MusicCrawler;

        }

        #region NotNeeded methods
        private async void AddPlayLists_Click(object sender, RoutedEventArgs e)
        {
           // playList.Add(await PickPlaylistAsync());
        }

        public async Task<PlayList> PickPlaylistAsync()
        {
            FileOpenPicker picker = new FileOpenPicker();
            IList<Song> songs = new List<Song>();

            picker.SuggestedStartLocation = PickerLocationId.MusicLibrary;

            IReadOnlyList<StorageFile> files = await picker.PickMultipleFilesAsync();

            if (files.Count > 0)
            {
                foreach (var file in files)
                {
                    var song = _instance.FindSongFromPath(file.Path);
                    songs.Add(song);
                }
            }
            PlayList play = new PlayList()
            {
                Name = Guid.NewGuid().ToString(),
                AlbumsList = null
            };

            return play;
        }
        private  void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
        }
        #endregion

        private void AddToPlayList_Click(object sender, RoutedEventArgs e)
        {
            PlayList newPlay = new PlayList() {Name = playListName.Text };
            _instance.AddPlayList(newPlay);
            this.DataContext = _instance.GetPlayList;
            ;
        }
    }
}
