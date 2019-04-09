using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Song
    {
        public int duration;
        public string title;
        public string path;
        public string lyrics;
        public string genre;
        public Artist Artist;
        public Playlist[] Playlists;
    }
}
