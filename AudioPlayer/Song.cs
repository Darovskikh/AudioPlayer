using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Song : PlayingItem<Song>
    {
        public string Lyrics { get; set; }
        public Artist Artist { get; set; }
        public Album Album { get; set; }
        public GenreSong Genre { get; set; }

        public void Deconstruct(out string title, out int minutes, out int seconds, out string artistName,
            out string album, out int year)
        {
            title = Title;
            minutes = Duration / 60;
            seconds = Duration % 60;
            artistName = Artist.Name;
            album = Album.Name;
            year = Album.Year;
        }
    }
    public enum GenreSong
    {
        Pop,
        Rock,
        Rap,
        HipHop,
        RussianRock,
        RussianRap
    }
}
