using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

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
            var song = CreateSong("some name");
            var song1 = song;
            Song[] songs = new Song[5];
            Player.Add(song);
            Player.Add(song, song1);
            songs[0] = song;
            songs[1] = song1;
            Player.Add(songs);
            var artist = AddArtist();
            var album = addAlbum(year:1992,name:"bla bla");


            Console.ReadKey();
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

        private static Song CreateSong()
        {
            Song song = new Song();
            Random random = new Random();
            song.Artist = new Artist("some name");
            song.duration = random.Next(200);
            song.genre = random.Next(100).ToString();
            song.lyrics = random.Next(350).ToString();
            song.path = random.Next(455).ToString();
            return song;
        }

        private static Song CreateSong(string name)
        {
            var song = CreateSong();
            song.title = name;
            return song;
        }

        private static Song CreateSong(string title, int duration, string genre, string lyrics, string path, Artist artist)
        {
            Song song = new Song();
            song.Artist = artist;
            song.duration = duration;
            song.title = title;
            song.genre = genre;
            song.lyrics = lyrics;
            song.path = path;
            return song;
        }


        private static Artist AddArtist (string name ="Unknown Artist")
        {
            Artist artist = new Artist(name);
            return artist;
        }

        private static Album addAlbum(string name = "Unknown album", int year = 0)
        {
            Album album = new Album(name,year);
            return album;
        }
    }
}
