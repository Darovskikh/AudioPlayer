using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Artist
    {      
        public string Name { get; private set; }
        public string Nickname { get; private set; }
        public string Counry { get; private set; }
        public Artist()
        {
            Name = "Unknown artist";
        }
        public Artist(string name)
        {
            Name = name;
        }
    }
}
