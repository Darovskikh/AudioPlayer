using System.Collections.Generic;

namespace AudioPlayer
{
    static class Sorting
    {
        public static List<Song> SortByTitle(this List<Song> songs)
        {
            List<string> titles = new List<string>();
            List<Song> sortedSongs = new List<Song>();
            foreach (Song song in songs)
            {
                titles.Add(song.Title);
            }

            titles.Sort();
            foreach (string title in titles)
            {
                foreach (Song song in songs)
                {
                    if (title == song.Title)
                    {
                        sortedSongs.Add(song);
                    }
                }
            }
            return sortedSongs;
        }
    }
}
