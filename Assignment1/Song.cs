using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Song
    {
        //Song Properties like Name,file type, size, artist and allow the user to store Cover image  
        public string Title { get; set; }
        public string Artist { get; set; }
        public ulong Size { get; set; }
        public string FilePath { get; set; }
        public string ImagePath { get; set; }
        public string AlbumName { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
