using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Artist
    {      
        public string name;
        public string nickname;
        public string counry;
        public Artist()
        {
            this.name = "Unknown artist";
        }
        public Artist(string name)
        {
            this.name = name;
        }
    }
}
