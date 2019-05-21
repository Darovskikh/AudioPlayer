using System;

namespace AudioPlayer
{
    [Serializable]
    public class Artist
    {      
        public string Name { get;  set; }
        public string Nickname { get; set; }
        public string Counry { get;  set; }
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
