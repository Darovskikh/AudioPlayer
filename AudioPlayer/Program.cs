﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
            
            SongPlayer player = new SongPlayer(new ColorSkin("yellow"));
            var song = CreateSong("Paparazzi", 250, GenreSong.Pop, "bla bla", " la la", new Artist("Lady Gaga"), new Album("International EP", 2009));
            var song1 = CreateSong("Скользкие улицы", 200, GenreSong.RussianRock, "bla bla", " la la", new Artist("Би-2"), new Album("Иномарки", 2004));
            var song2 = CreateSong("Alors on dance", 230, GenreSong.Rap, "bla bla", " la la", new Artist("Stormae"), new Album("Cheese", 2010)); ;
            var song3 = CreateSong("Я роняю Запад", 180, GenreSong.RussianRap, "bla bla", " la la", new Artist("Face"), new Album("Single", 2017));
            SongPlayer.Add(song, song1, song2, song3);
            
            
            //SongPlayer.Items=SongPlayer.Items.SortByTitle();
            SongPlayer.Items = SongPlayer.Items.ShuffleSongs();

            
            Song.Like(SongPlayer.Items[1]);
            Song.Dislike(SongPlayer.Items[3]);
            //SongPlayer.Items = player.FilterByGenre(SongPlayer.Items, GenreSong.Rap);
            player.Play(SongPlayer.Items, false);

            //VideoPlayer player = new VideoPlayer(new ColorSkin());

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

        private static Song CreateSong(string title, int duration, GenreSong genre, string lyrics, string path, Artist artist, Album album)
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
    }
}
