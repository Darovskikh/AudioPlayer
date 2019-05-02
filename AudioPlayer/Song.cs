using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Song
    {
        public int Duration { get;set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string Lyrics { get; set; }
        public string Genre { get;set; }
        public Artist Artist { get; set; }
    }
}
