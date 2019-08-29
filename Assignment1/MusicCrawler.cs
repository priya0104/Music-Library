using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Foundation.Collections;
using Windows.Storage.Search;
using Windows.Storage.FileProperties;

namespace Assignment1
{
    public class MusicCrawler
    {
        private Dictionary<string, Album> albumCollection;// = new Dictionary<string, Album>();
        private Dictionary<string, Song> songsCollection;
        private IList<PlayList> playListCollection;
   
        // Consturctor 
        public MusicCrawler()
        {
            albumCollection = new Dictionary<string, Album>();
            songsCollection = new Dictionary<string, Song>();
            playListCollection = new List<PlayList>();
            //songList = new List<Song>();
        }

        public IList<PlayList> GetPlayList
        {
            get {
                return playListCollection;
            }
        }

        public bool IsMusicScanned
        {
            get; set;
        }

        public IList<Album> Albums
        {
            get
            {
                //await EnumerateAndGetAlbumsAsync();
                return albumCollection.Values.ToList<Album>();
            }
        }

        public void AddPlayList(PlayList playList)
        {
            if (playList !=null)
            {
                playListCollection.Add(playList);
            }
        }
        public Song FindSongFromPath(string filepath)
        {
            if (filepath != null && System.IO.File.Exists(filepath))
            {
                return songsCollection[filepath];
            }
            return null;
        }

        //access the default music folder
        public async Task<Dictionary<string, object>> GetAlbumsAsync()
        {
            //To get a reference to the user's Music, Pictures, or Video library
            var myMusic = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Music);

            //get the List of folders in a library
            var myMusicFolders = myMusic.Folders;

            // dictionary takes Tkey = folder name, Tvalue = album object
            Dictionary<string, object> folderCollection = new Dictionary<string, object>();

            //add the albums/folders to a dictionary
            foreach (var album in myMusicFolders)
            {
                folderCollection.Add(album.Name, album.Properties);
            }
            //get the default folder where new files will be saved
            //var defaultFolder = myMusic.SaveFolder;

            return folderCollection;


        }

        //to get a collection of music files
        public async Task<IList<Album>> EnumerateAndGetAlbumsAsync()
        {
            string[] fileType = { ".mp3", ".mp4", ".wma" };

            QueryOptions queryOptions = new QueryOptions(CommonFileQuery.OrderByTitle, fileType);

            queryOptions.FolderDepth = FolderDepth.Deep;

            var files = await KnownFolders.MusicLibrary.CreateFileQueryWithOptions(queryOptions).GetFilesAsync();

            //get thumbnail
            const uint requestThumbNailSize = 190;
            const ThumbnailMode thumbnailMode = ThumbnailMode.MusicView;
            const ThumbnailOptions thumbnailOptions = ThumbnailOptions.UseCurrentScale;

            foreach (var file in files)
            {

                MusicProperties musicProperties = await file.Properties.GetMusicPropertiesAsync();
                BasicProperties basicProperties = await file.GetBasicPropertiesAsync();
                var thumbnail = await file.GetThumbnailAsync(thumbnailMode, requestThumbNailSize, thumbnailOptions);

                //Get ThumbNail

                // Create a new Song object, add it to the album object created above.
                Song song = new Song()
                {
                    Title = musicProperties.Title,
                    Artist = musicProperties.Artist,
                    Size = basicProperties.Size,
                    FilePath = file.Path,
                    AlbumName = musicProperties.Album,
                    Duration = musicProperties.Duration,
                    //ImagePath = musicProperties.
                };

                //Album does not exist in the colleciotn 
                if (!albumCollection.ContainsKey(musicProperties.Album.ToLower()))
                {
                    // Create a new Alubum - new Album()
                    Album album = new Album()
                    {
                        Name = musicProperties.Album,
                        ArtistName = musicProperties.AlbumArtist,
                        //FolderPath = 
                    };

                    //Add song object to Album
                    album.AddSong(song);

                    //Add song to the songs collection
                    if (!songsCollection.ContainsKey(song.FilePath))
                    {
                        songsCollection.Add(song.FilePath, song);
                    }
                    // Then, add album to the colleciton
                    albumCollection.Add(album.Name.ToLower(), album);

                }
                else  // Album exists in the collection
                {
                    // Get a alumbum object from the collection using album name as key
                    var album = albumCollection[musicProperties.Album.ToLower()];
                    // Create a new Song object, add it to the alubum object obtained above.
                    album.AddSong(song);

                    //Add song to the songs collection
                    if (!songsCollection.ContainsKey(song.FilePath))
                    {
                        songsCollection.Add(song.FilePath, song);
                    }
                }

            }
            IsMusicScanned = true;
            return albumCollection.Values.ToList<Album>();
        }

        //access the default music folder
        public async Task<IList<Song>> GetSongsAsync()
        {
            var songsList = new List<Song>();
            if (albumCollection == null)
            {
                await EnumerateAndGetAlbumsAsync();
            }

            foreach (var album in albumCollection.Values)
            {
                foreach (var song in album.SongsCollection)
                {
                    songsList.Add(song);
                }
            }

            return songsList;
        }
    }
}
