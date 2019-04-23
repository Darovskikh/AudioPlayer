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
        public string nickname { get; private set; }
        public string counry { get; private set; }
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
