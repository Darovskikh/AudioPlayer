using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using File = TagLib.File;
using WMPLib;
using System.Windows.Forms;
using System.Threading;

namespace AudioPlayer
{
    class Player : IDisposable
    {
        private bool disposed = false;
        private static string _path;
        private static WindowsMediaPlayer _player = new WindowsMediaPlayer();
        public List<Playlist> Playlists { get; private set; } = new List<Playlist>();
        public static Skin Skin { get; set; }
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

        public void Play(List<Song> songs)
        {

            
            foreach (var song in songs)
            {
                _player.URL = song.Path;
                _player.controls.play();
            }


        }
        public void Play(Song song)
        {
            _player.URL = song.Path;
            Skin.Render($"Сейчас играет - {song.Artist.Name} - {song.Title}");
            Console.WriteLine();
            _player.controls.play();
        }

        public static void LoopOn<T>() where T : Song
        {
            var song = _player.currentMedia.GetType();
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


        private static void TakePath()
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    _path = fbd.SelectedPath;
                }
            }
        }
        public void Load()
        {
            List<FileInfo> fileInfos = new List<FileInfo>();
            Skin.Render("Укажите путь к папке с музыкой");
            Thread thread = new Thread(TakePath);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            DirectoryInfo directoryInfo = new DirectoryInfo(_path);
            foreach (var file in directoryInfo.GetFiles("*.mp3"))
            {
                fileInfos.Add(file);
            }
            foreach (var file in fileInfos)
            {
                var audio = File.Create(file.FullName);
                Songs.Add(new Song() { Album = new Album(audio?.Tag.Album, (int)audio.Tag?.Year), Artist = new Artist(audio.Tag?.FirstPerformer), Duration = (int)audio.Properties.Duration.TotalSeconds, Genre = audio.Tag?.FirstGenre, Lyrics = audio.Tag?.Lyrics, Title = audio.Tag?.Title, Path = audio.Name });
            }
        }

        public void SaveAsPlaylist()
        {
            Skin.Render("Введите название плейлиста");
            Playlist playlist = new Playlist(Console.ReadLine(), Songs);
            Playlists.Add(playlist);
            XmlSerializer xmlSerializer = new XmlSerializer(Playlists.GetType());
            using (FileStream fs = new FileStream("Playlists.xml", FileMode.OpenOrCreate))
            {
                playlist.Path = fs.Name;
                xmlSerializer.Serialize(fs, Playlists);
            }

        }

        public void LoadPlayList()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(Playlists.GetType());
            using (FileStream fs = new FileStream("Playlists.xml", FileMode.Open))
            {
                Playlists = (List<Playlist>)xmlSerializer.Deserialize(fs);
            }
            Skin.Render("Введите номер плейлиста для воспроизведения");
            Skin.Render("");
            int i = 1;
            foreach (var playlist in Playlists)
            {
                Skin.Render($"{i}. {playlist.Title}");
                i++;
            }
            Skin.Render("");
            int number = int.Parse(Console.ReadLine());
            Songs = Playlists[number - 1].Songs;
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

        public void ShowSongsList()
        {
            int i = 0;
            foreach (var song in Songs)
            {
                if (song.Artist.Name == null)
                {
                    song.Artist.Name = "Unknwon artist";
                }
                if (song.Title == null)
                {
                    song.Title = "No title";
                }
                i++;
                Skin.Render($"{i}. {song.Artist.Name} - {song.Title}");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    return;
                }

                {
                    _player.close();
                    _player = null;
                    disposed = true;
                }
            }
        }

        ~Player()
        {
            Dispose(false);
        }
        
    }


}
