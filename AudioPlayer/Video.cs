using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Video : PlayingItem< Video>
    {
        public string Subtitels { get; set; }
        public Artist Producer { get; set; }
        public Album Album { get; set; }
        public GenreVideo Genre { get; set; }

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
    public enum GenreVideo
    {
        Pop,
        Rock,
        Rap,
        HipHop,
        RussianRock,
        RussianRap
    }
}

