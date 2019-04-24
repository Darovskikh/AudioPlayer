using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Song
    {
        public bool Playing { get; set; }
        public bool? LikeStatus { get; set; }
        public int Duration { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string Lyrics { get; set; }
        public Genre Genre { get; set; }
        public Artist Artist { get; set; }
        public Album Album { get; set; }
        public static void Like(Song song)
        {
            song.LikeStatus = true;
        }

        public static void Dislike(Song song)
        {
            song.LikeStatus = false;
        }
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
    public enum Genre
    {
        Pop,
        Rock,
        Rap,
        HipHop,
        RussianRock,
        RussianRap
    }
}
