using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Album
    {
        //Album Properties like Name,file type, size, artist and allow the user to store Cover image  
        public string Title { get; set; }
        public string Name { get; set; }
        public string ArtistName { get; set; }
        public string ThumbNailPath { get; set; }
        //public ulong Size { get; set; }
        public string FolderPath { get; set; }
        //public string ImagePath { get; set; }
        //property to store cover image

        public Album()
        {
            songCollection = new List<Song>();
        }
        public void AddSong(Song song)
        {
            songCollection.Add(song);
        }
        public List<Song> SongsCollection
        {
            get {
                return songCollection;
            }
        }
        private List<Song> songCollection;
    }
}
