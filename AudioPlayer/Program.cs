using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var song2 = new AudioPlayer.Song();
            song2.title = "Anaconda";
            song2.duration = 270;
            song2.Artist = new AudioPlayer.Artist { name = "Nicki Minaj" };
            var player = new Player();
            player.Songs = new[] { song1, song2 };


        }
    }
}
