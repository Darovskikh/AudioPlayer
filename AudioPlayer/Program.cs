using System;
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
            Player player = new Player(new ClassicSkin());
            player.Load();
            //player.SaveAsPlaylist();
            //player.LoadPlayList();
            Player.Play(Player.Songs,false);
            Console.ReadKey();
        }

     


       
    }
}
