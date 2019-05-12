using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Console;
using System.Xml.Serialization;

namespace AudioPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            //    int min,  max, total=0;
            //    Player player =new  Player();
            //    var songs = CreateSongs(out min, out max, ref total);
            //    player.Songs = songs;
            //    Console.WriteLine($"min - {min}, max - {max}, total - {total}"); 
            //    while (true)
            //    {
            //        switch (ReadLine())     
            //        {
            //            case "up":
            //                player.VolumeUP();
            //                break;
            //            case "dn":
            //                player.VolumeDown();
            //                break;
            //            case "p":
            //                player.Play();
            //                break;
            //        }
            //    }

            Player player = new Player(new RandomSkin());
            var song = CreateSong("Paparazzi", 250, Genre.Pop, "bla bla", " la la", new Artist("Lady Gaga"),
                new Album("International EP", 2009));
            var song1 = CreateSong("Скользкие улицы", 200, Genre.RussianRock, "bla bla", " la la", new Artist("Би-2"),
                new Album("Иномарки", 2004));
            var song2 = CreateSong("Alors on dance", 230, Genre.Rap, "bla bla", " la la", new Artist("Stormae"),
                new Album("Cheese", 2010));
            ;
            var song3 = CreateSong("Я роняю Запад", 180, Genre.RussianRap, "bla bla", " la la", new Artist("Face"),
                new Album("Single", 2017));
            Player.Add(song, song1, song2, song3);
            Song[] songs = {song, song1, song2, song3};
            //Player.Songs = Player.Songs.SortByTitle();
            //Player.Songs = Player.Songs.ShuffleSongs();

            //Player.Shuffle(Player.Songs);
            //Song.Like(Player.Songs[1]);
            //Song.Dislike(Player.Songs[3]);
            //List<Song> songs = Player.FilterByGrenre(Player.Songs, Genre.Pop);
            //Player.Play(Player.Songs, false);
            //ConsoleSerlz();
            FileSeriz();
            ReadKey();
        }

        //private static Song[] CreateSongs(out int min, out int max, ref int total)
        //{
        //    Song[] songs = new Song[5];
        //    int minDuration=int.MaxValue, maxDuration=int.MinValue, totalDuration=0;            
        //    Random random = new Random();
        //    for (int i = 0; i < songs.Length; i++)
        //    {
        //    var song1 = new Song();
        //    song1.title = "Song "+i;
        //    song1.duration = random .Next(3000);
        //    song1.Artist = new Artist ();
        //    songs[i] = song1;
        //    totalDuration += song1.duration; 
        //    if (song1.duration < minDuration )
        //        {
        //            minDuration = song1.duration;
        //        }
        //    if (song1.duration > maxDuration )
        //        {
        //            maxDuration = song1.duration;
        //        }
        //    }
        //    total = totalDuration;
        //    max = maxDuration;
        //    min = minDuration;
        //    return songs;     
        //}

        //private static Song CreateSong()
        //{
        //    Song song = new Song();
        //    Random random = new Random();
        //    song.Artist = new Artist("some name");
        //    song.Duration = random.Next(200);            
        //    song.Lyrics = random.Next(350).ToString();
        //    song.Path = random.Next(455).ToString();
        //    return song;
        //}

        //private static Song CreateSong(string name)
        //{
        //    var song = CreateSong();
        //    song.Title = name;
        //    return song;
        //}

        private static Song CreateSong(string title, int duration, Genre genre, string lyrics, string path,
            Artist artist, Album album)
        {
            Song song = new Song();
            song.Album = album;
            song.Artist = artist;
            song.Duration = duration;
            song.Title = title;
            song.Genre = genre;
            song.Lyrics = lyrics;
            song.Path = path;
            return song;
        }

        public static void ConsoleSerlz()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(Player.Songs.GetType());
            string result;
            using (StringWriter sw = new StringWriter())
            {
                xmlSerializer.Serialize(sw, Player.Songs);
                WriteLine(sw);

                result = sw.ToString();
                Player.Songs.Clear();
                WriteLine();
                WriteLine($"Количество песен после сериализации - {Player.Songs.Count}");
            }

            using (StringReader sr = new StringReader(result))
            {
                Player.Songs = (List<Song>) xmlSerializer.Deserialize(sr);
            }

            WriteLine();
            WriteLine($"Количество песен после десериализации - {Player.Songs.Count}");
            ReadKey();
        }

        public static void FileSeriz()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(Player.Songs.GetType());
            using (FileStream fs = new FileStream("Songs.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs,Player.Songs);
                Player.Songs.Clear();
                WriteLine($"Количество песен после сериализации - {Player.Songs.Count}");
               
            }
            using (FileStream fs = new FileStream("Songs.xml", FileMode.OpenOrCreate))
            {
                Player.Songs = (List<Song>)xmlSerializer.Deserialize(fs);
                WriteLine($"Количество песен после десериализации - {Player.Songs.Count}");
            }
            
        }

    }
}
