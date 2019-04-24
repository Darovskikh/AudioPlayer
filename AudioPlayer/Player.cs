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
using Microsoft.SqlServer.Server;

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
                if (value > _maxVolume)
                {
                    _volume = _maxVolume;
                }
                else if (value < 0)
                {
                    _volume = 0;
                }
                else { _volume = value; }
            }
        }
        public static void Play(List<Song> songs, bool loop)
        {
            foreach (Song song in songs)
            {
                if (loop == true)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        song.Playing = true;
                        Player.WriteSongsList(songs);
                    }
                }
                else
                {
                    song.Playing = true;
                    Player.WriteSongsList(songs);
                    song.Playing = false;
                    System.Threading.Thread.Sleep(song.Duration + 1500);
                }

            }

        }
        public static void WriteLyrics(Song song)
        {
            string sentence = song.Title;
            if (sentence.Length > 13)
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

        
        public void VolumeUP()
        {
            Volume += 5;
            Console.WriteLine($"Volume is: {Volume}");
        }

        public void VolumeDown()
        {
            Volume -= 5;
            Console.WriteLine($"Volume is: {Volume}");
        }

        public static void Add(params Song[] songs)
        {
            foreach (Song song in songs)
            {
                Songs.Add(song);
            }

        }

       

        public static void WriteSongsList(List<Song> songs)
        {
            foreach (Song song in songs)
            {
                if (song.Playing == true)
                {
                    WriteSongData(song, ConsoleColor.Blue);
                }
                else
                {
                    if (song.LikeStatus.HasValue == true)
                    {
                        if (song.LikeStatus == true)
                        {
                            WriteSongData(song, ConsoleColor.Red);
                        }
                        else
                        {
                            WriteSongData(song, ConsoleColor.Green);
                        }
                    }
                    else WriteSongData(song, ConsoleColor.White);
                }
            }
        }
        public static void WriteSongData(Song song, ConsoleColor color)
        {
            //var songData = GetSongData(song);
            //Console.ForegroundColor = color;
            //Console.WriteLine($"Title - {songData.title.CutStringSymbols()}");
            //Console.ResetColor();
            //Console.WriteLine($"Duration - {songData.minutes}.{songData.seconds}");
            //Console.WriteLine($"Artist - {songData.artistName}");
            //Console.WriteLine($"Album - {songData.album}");
            //Console.WriteLine($"Year - {songData.year}");
            //Console.WriteLine();
            var (title, minutes, seconds, artistName, album, year ) = song;
            Console.ForegroundColor = color;
            Console.WriteLine($"Title - {title.CutStringSymbols()}");
            Console.ResetColor();
            Console.WriteLine($"Duration - {minutes}.{seconds}");
            Console.WriteLine($"Artist - {artistName}");
            Console.WriteLine($"Album - {album}");
            Console.WriteLine($"Year - {year}");
            Console.WriteLine();
        }
        public static (string title, int minutes, int seconds, string artistName, string album, int year, bool playing) GetSongData(Song song)
        {
            return (song.Title, song.Duration / 60, song.Duration % 60, song.Artist.Name, song.Album.Name,
                song.Album.Year, song.Playing);
        }

        public static List<Song> FilterByGrenre(List<Song> songs, Genre genre)
        {
            List<Song> filteredSongs = new List<Song>();
            foreach (Song song in songs)
            {
                if (song.Genre == genre)
                {
                    filteredSongs.Add(song);
                }
            }
            return filteredSongs;
        }

    }


}
