using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Playlist
    {
        public string Path { get; private set; }
        public string Title { get; set; }
        public List <Song> Songs { get; set; }
    }
}
