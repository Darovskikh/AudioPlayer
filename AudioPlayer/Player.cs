using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Xml.Serialization;
using File = TagLib.File;
using WMPLib;
using System.Windows.Forms;
using System.Threading;

namespace AudioPlayer
{
    class Player : IDisposable
    {
        public delegate void ShowMessageHandler(Player sender, PlayerEventArgs e);
        public event ShowMessageHandler PlayerStartedEvent;
        public event ShowMessageHandler PlayerStoppedEvent;
        public event ShowMessageHandler SongStarted;
        public event ShowMessageHandler SongListChanged;
        public event ShowMessageHandler VolumeStatus;
        public event ShowMessageHandler LockOnEvent;
        public event ShowMessageHandler LockOffEvent;
        private bool disposed = false;
        private static string _path;
        private static WindowsMediaPlayer _player = new WindowsMediaPlayer();
        public List<Playlist> Playlists { get; private set; } = new List<Playlist>();
        public static Skin Skin { get; set; }
        private int _volume = 30;
        public bool IsLock { get; private set; }
        public List<Song> Songs { get; set; } = new List<Song>();

        public int Volume
        {
            get { return _volume; }
            private set
            {
                if (value > 100)
                    _volume = 100;
                else if (value < 0)
                    _volume = 0;
                else
                {
                    _volume = value;
                }
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
                _player.settings.volume = 30;
                _player.URL = song.Path;
                song.Playing = true;
                SongStarted(this, new PlayerEventArgs() { Message = ($"Сейчас играет - {song.Artist.Name} - {song.Title}") });
                _player.controls.play();
            }
        }
        public void Play(Song song)
        {
            _player.settings.volume = 30;
            _player.URL = song.Path;
            song.Playing = true;
            SongStarted(this, new PlayerEventArgs() { Message = ($"Сейчас играет - {song.Artist.Name} - {song.Title}") });
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
            _player.settings.volume = Volume;
            VolumeStatus?.Invoke(this, new PlayerEventArgs() { Message = $"Volume is: {Volume}" });
        }

        public void VolumeDown()
        {
            Volume -= 5;
            _player.settings.volume = Volume;
            VolumeStatus?.Invoke(this, new PlayerEventArgs() { Message = $"Volume is: {Volume}" });
        }

        public void Clear()
        {
            Songs.Clear();
            SongListChanged(this, new PlayerEventArgs() { Message = ("Cписок песен очищен") });
            SongListChanged(this, new PlayerEventArgs() { Message = ("Для загрузки песен нажмите L") });
            //Skin.Render("Cписок песен очищен");
            //Skin.Render("Для загрузки песен нажмите L");
            Console.WriteLine();
        }

        public void LockOn()
        {
            IsLock = true;
            LockOnEvent?.Invoke(this, new PlayerEventArgs() { Message = "Плеер заблокирован для разблокировки нажмите N" });
        }

        public void LockOff()
        {
            IsLock = false;
            LockOffEvent?.Invoke(this, new PlayerEventArgs() { Message = "Плеер разблокирован" });
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
            Console.WriteLine();
            PlayerStartedEvent?.Invoke(this, new PlayerEventArgs { Message = "Укажите путь к папке с музыкой" });
            List<FileInfo> fileInfos = new List<FileInfo>();
            //Skin.Render("Укажите путь к папке с музыкой");
            Thread thread = new Thread(TakePath);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(_path);
                foreach (var file in directoryInfo.GetFiles("*.wav"))
                {
                    fileInfos.Add(file);
                }
                foreach (var file in fileInfos)
                {
                    var audio = File.Create(file.FullName);
                    Songs.Add(new Song() { Album = new Album(audio?.Tag.Album, (int)audio.Tag?.Year), Artist = new Artist(audio.Tag?.FirstPerformer), Duration = (int)audio.Properties.Duration.TotalSeconds, Genre = audio.Tag?.FirstGenre, Lyrics = audio.Tag?.Lyrics, Title = audio.Tag?.Title, Path = audio.Name });
                }
                ShowMessage();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }

        public void SaveAsPlaylist()
        {
            Console.WriteLine();
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


        public void Dispose()
        {
            PlayerStoppedEvent?.Invoke(this, new PlayerEventArgs() { Message = "Выключение..." });
            Thread.Sleep(2000);
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _player.close();
                    _player = null;
                    Playlists = null;
                    Skin = null;
                    Songs = null;
                }
                else
                {
                    _player?.close();
                    _player = null;
                    disposed = true;
                }
            }
        }

        private static void ShowMessage()
        {
            Console.WriteLine();
            Skin.Render("Для воспроизведения песни по номеру нажмите P");
            Skin.Render("Для воспроизведения всех песен введите 0");
            Skin.Render("Для загрузки песен нажмите A");
            Skin.Render("Для очистки списка песен нажмите C");
            Skin.Render("Для увеличения громкости нажмите U");
            Skin.Render("Для уменьшения громкости нажмите D");
            Skin.Render("Чтобы заблокировать плеер нажмите L");
            Skin.Render("Чтобы разблокировать плеер нажмите N");
            Skin.Render("Для выхода нажмите ESC");
            Console.WriteLine();
        }
        ~Player()
        {
            Dispose(false);
        }

    }


}
