using System;
using System.Collections.Generic;
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
            int min,  max, total=0;
            Player player =new  Player();
            var songs = CreateSongs(out min, out max, ref total);
            player.Songs = songs;
            Console.WriteLine($"min - {min}, max - {max}, total - {total}"); 
            while (true)
            {
                switch (ReadLine())     
                {
                    case "up":
                        player.VolumeUP();
                        break;
                    case "dn":
                        player.VolumeDown();
                        break;
                    case "p":
                        player.Play();
                        break;
                }
            }

            Console.ReadKey();
        }

        private static Song[] CreateSongs(out int min, out int max, ref int total)
        {
            Song[] songs = new Song[5];
            int minDuration=int.MaxValue, maxDuration=int.MinValue, totalDuration=0;            
            Random random = new Random();
            for (int i = 0; i < songs.Length; i++)
            {
            var song1 = new Song();
            song1.title = "Song "+i;
            song1.duration = random .Next(3000);
            song1.Artist = new Artist ();
            songs[i] = song1;
            totalDuration += song1.duration; 
            if (song1.duration < minDuration )
                {
                    minDuration = song1.duration;
                }
            if (song1.duration > maxDuration )
                {
                    maxDuration = song1.duration;
                }
            }
            total = totalDuration;
            max = maxDuration;
            min = minDuration;
            return songs;
     
        }
    }
}
