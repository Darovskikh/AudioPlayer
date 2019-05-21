using System;
using System.Xml.Serialization;

namespace AudioPlayer
{
    [Serializable]
    public class Song
    {
        private Genre _genre;
        [XmlIgnore]
        private bool _playing;

        public bool Playing
        {
            get => _playing;
            set => _playing = value;
        }
        public static bool Loop { get; set; }
        public bool? LikeStatus { get; set; }
        public int Duration { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string Lyrics { get; set; }
        public string Genre
        {
            get { return _genre.ToString(); }
            set
            {
                try
                {
                    if (value != null)
                    {
                        _genre = (Genre)Enum.Parse(typeof(Genre), value);
                    }
                    else
                    {
                        value = "Other";
                        _genre = (Genre)Enum.Parse(typeof(Genre), value);
                    }
                }
                catch (Exception e)
                {
                    value = "Other";
                    _genre = (Genre)Enum.Parse(typeof(Genre), value);
                }
               
                
            }
        }
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
        RussianRap,
        Alternative,
        Blues,
        Other
    }
}
