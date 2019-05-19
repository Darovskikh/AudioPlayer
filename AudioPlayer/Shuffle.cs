using System;
using System.Collections.Generic;

namespace AudioPlayer
{
    static class Shuffle
    {
        public static List<Song> ShuffleSongs(this List<Song> songs)
        {
            Random random = new Random();
            List<Song> shuffledSongs = new List<Song>();
            List<int> numbers = new List<int>();
            int j = 0, i = 0, p = 0;
            foreach (Song song in songs)
            {
                while (p != 1)
                {
                    i = random.Next(0, songs.Count);
                    if (numbers.Contains(i))
                    {
                        p = 0;
                    }
                    else
                    {
                        numbers.Add(i);
                        p = 1;
                    }
                }

                shuffledSongs.Add(songs[i]);
                p = 0;
            }
            return shuffledSongs;
        }
    }
}
