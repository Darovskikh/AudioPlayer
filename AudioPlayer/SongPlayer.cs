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
    class SongPlayer : GenericPlayer <Song,GenreSong>
    {
        public SongPlayer(Skin skin) : base(skin)
        {
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
        public override void WriteItemData(Song item, ConsoleColor color)
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
            var (title, minutes, seconds, artistName, album, year) = item;
            //Console.WriteLine($"Title - {title.CutStringSymbols()}");
            Skin.Render($"Title - {title.CutStringSymbols()}", color);
            //Console.ResetColor();
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
        public override void Play(List<Song> items, bool loop)
        {
            foreach (Song song in items)
            {
                if (loop)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        song.Playing = true;
                        WriteItemList(items);
                    }
                }
                else
                {
                    song.Playing = true;
                    WriteItemList(items);
                    song.Playing = false;
                    System.Threading.Thread.Sleep(song.Duration + 2000);
                }
            }
        }
        public override void WriteItemList(List<Song> items)
        {
            foreach (Song song in items)
            {
                if (song.Playing)
                {
                    WriteItemData(song, ConsoleColor.Blue);
                }
                else
                {
                    if (song.LikeStatus.HasValue)
                    {
                        if (song.LikeStatus == true)
                        {
                            WriteItemData(song, ConsoleColor.Red);
                        }
                        else
                        {
                            WriteItemData(song, ConsoleColor.Green);
                        }
                    }
                    else WriteItemData(song, ConsoleColor.White);
                }
            }
        }
        public override List<Song> FilterByGenre(List<Song> items,GenreSong genre)
        {
            List<Song> filteredSongs = new List<Song>();
            foreach (Song song in items)
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
