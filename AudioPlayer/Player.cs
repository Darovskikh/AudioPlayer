using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Player
    {
        public static bool Loop { get; set; }
        private int _volume;    
        public bool IsLock { get; private set; }
        private const int _maxVolume = 100;
        private static List<Song> _songs = new List<Song>();
        public static List<Song> Songs
        {
            get { return _songs; }
            set { _songs = value; }
        }
        public int Volume
        {
            get
            {
                return _volume;
            }
            private set  // считывается только в рамках класса            
            {
                if (value> _maxVolume )
                {
                    _volume = _maxVolume;
                }
                else if (value <0)
                {
                    _volume = 0;
                }
                else { _volume = value; }
            }
        }
        public static void Play(bool loop)
        {
            if (loop == true)
            {
                for (int i = 0; i < 2; i++)
                {
                    Console.WriteLine(Songs[i].Title + " " + Songs[i].Artist.Name + " " + Songs[i].Duration);
                    System.Threading.Thread.Sleep(Songs[i].Duration);
                }
            }
            else
            {
                for (int i = 0; i < Songs.Count; i++)
                {
                    Console.WriteLine(Songs[i].Title + " " + Songs[i].Artist.Name + " " + Songs[i].Duration);
                    System.Threading.Thread.Sleep(Songs[i].Duration);
                }
            }
                   
        }

        public static  void WriteLyrics(Song song)
        {
            string sentence = song.Title;
            if (sentence.Length  > 13)
            {
                sentence = song.Title.Remove(13);
                sentence = sentence + "...";
            }
            Console.WriteLine(sentence);
            string[] p = song.Lyrics.Split(';');
            foreach (string str in p)
            {
                Console.WriteLine(str);
            }            
        }

        public static void SortByTitle(List<Song> songs)
        {
            List<string> titles = new List<string>();
            List< Song > sortedSongs = new List<Song>();
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

            Songs = sortedSongs;
                       
           
        }
        public void VolumeUP()
        {
            Volume += 5;
            Console.WriteLine($"Volume is: {Volume}"); 
        }

        public void VolumeDown()
        {
            Volume  -= 5;
            Console.WriteLine($"Volume is: {Volume}");
        }

        public static void Add(params Song[] songs)
        {
            foreach (Song song in songs)
            {
                Songs.Add(song);
            }
            
        }

        public static void Shuffle(List<Song> songs)
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

            Songs = shuffledSongs;
        }

        
    }
}
