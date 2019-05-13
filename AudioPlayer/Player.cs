using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.SqlServer.Server;
using TagLib;
using File = TagLib.File;

namespace AudioPlayer
{
    class Player
    {
        public List< Playlist> Playlists { get; private set; } = new List<Playlist>();
        private static Skin Skin { get; set; }
        public static bool Loop { get; set; }
        private int _volume;
        public bool IsLock { get; private set; }
        private const int _maxVolume = 100;
        public static List<Song> Songs { get; set; } = new List<Song>();
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

        public Player(Skin skin)
        {
            Skin = skin;
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
                    System.Threading.Thread.Sleep(song.Duration + 2000);
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
            //Console.WriteLine(sentence);
            Skin.Render(sentence);
            string[] p = song.Lyrics.Split(';');
            foreach (string str in p)
            {
                //Console.WriteLine(str);
                Skin.Render(str);
            }
        }
        public void VolumeUP()
        {
            Volume += 5;
            //Console.WriteLine($"Volume is: {Volume}");
            Skin.Render($"Volume is: {Volume}");
        }

        public void VolumeDown()
        {
            Volume -= 5;
            //Console.WriteLine($"Volume is: {Volume}");
            Skin.Render($"Volume is: {Volume}");
        }

        public void Clear()
        {
            Songs.Clear();
            Skin.Render("Cписок песен очищен");
        }

        public void Load()
        {
            List<FileInfo> fileInfos = new List<FileInfo>();
            Skin.Render("Укажите путь к папке с музыкой");
            string path = Console.ReadLine();
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            foreach (var file in directoryInfo.GetFiles("*.wav"))
            {
                fileInfos.Add(file);
            }
            
            foreach (var file in fileInfos)
            {
                var audio = File.Create(file.FullName);
                Songs.Add( new Song(){Album = new Album(audio?.Tag.Album,(int)audio.Tag?.Year),Artist = new Artist(audio.Tag?.FirstPerformer),Duration = (int)audio.Properties.Duration.TotalSeconds,Genre = audio.Tag?.FirstGenre,Lyrics = audio.Tag?.Lyrics,Title = audio.Tag?.Title,Path = audio.Name});
            }
        }

        public void SaveAsPlaylist()
        {
            Skin.Render("Введите название плейлиста");
            Playlist playlist = new Playlist(Console.ReadLine(),Songs);
            Playlists.Add(playlist);
            XmlSerializer xmlSerializer = new XmlSerializer(Playlists.GetType());
            using (FileStream fs = new FileStream("Playlists.xml", FileMode.OpenOrCreate))
            {
                playlist.Path = fs.Name;
                xmlSerializer.Serialize(fs,Playlists);
            }
           
        }

        public void LoadPlayList()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(Playlists.GetType());
            using (FileStream fs = new FileStream("Playlists.xml", FileMode.Open))
            {
                Playlists = (List<Playlist>) xmlSerializer.Deserialize(fs);
            }
            Skin.Render("Введите название плейлиста");
            string name = Console.ReadLine();
            foreach (var playlist in Playlists)
            {
                if (name == playlist.Title)
                {
                    Songs = playlist.Songs;
                }
                else
                {
                    Skin.Render("Not found");
                }
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
            var (title, minutes, seconds, artistName, album, year) = song;
            //Console.WriteLine($"Title - {title.CutStringSymbols()}");
            Skin.Render($"Title - {title.CutStringSymbols()}", color);
            Console.ResetColor();
            //Console.WriteLine($"Duration - {minutes}.{seconds}");
            Skin.Render($"Duration - {minutes}.{seconds}");
            //Console.WriteLine($"Artist - {artistName}");
            Skin.Render($"Artist - {artistName}");
            //Console.WriteLine($"Album - {album}");
            Skin.Render($"Album - {album}");
            //Console.WriteLine($"Year - {year}");
            Skin.Render($"Year - {year}");
            Console.WriteLine();
        }
        public static (string title, int minutes, int seconds, string artistName, string album, int year, bool playing) GetSongData(Song song)
        {
            return (song.Title, song.Duration / 60, song.Duration % 60, song.Artist.Name, song.Album.Name,
                song.Album.Year, song.Playing);
        }

        public static List<Song> FilterByGrenre(List<Song> songs, string genre)
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
