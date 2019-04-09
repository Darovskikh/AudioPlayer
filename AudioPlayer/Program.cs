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
            var song1 = new AudioPlayer.Song();
            song1.title = "Дым сигарет с ментолом";
            song1.duration = 300;
            song1.Artist = new AudioPlayer.Artist { name = "Нэнси" };
            song1.path = "somePath";
            song1.lyrics = "someLyrics";
            var song2 = new AudioPlayer.Song();
            song2.title = "Anaconda";
            song2.duration = 270;
            song2.Artist = new AudioPlayer.Artist { name = "Nicki Minaj" };
            var player = new Player();
            player.Songs = new[] { song1, song2 };

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
    }
}
