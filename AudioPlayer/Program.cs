using System;
using System.Runtime.CompilerServices;
using System.Threading;
using TagLib.Id3v2;
using static System.Console;

namespace AudioPlayer
{
    class Program
    {
        private static Player _player;
        private static Skin _skin;
        //[STAThread]
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
            _skin = new ClassicSkin();
            _skin.Render("Выберете стиль текста");
            _skin.Render("1. Классический стиль");
            _skin.Render("2. Цветной стиль");
            _skin.Render("3. Рандомный цвет стиль");
            WriteLine();
            switch (Console.ReadLine())
            {
                case "1":
                {
                     _player = new Player(new ClassicSkin());
                     _skin = Player.Skin;
                    break;
                }

                case "2":
                {
                    WriteLine();
                     _player = new Player(new ColorSkin(ChooseColor()));
                     _skin = Player.Skin;
                    break;
                }

                case "3":
                {
                    _player = new Player(new RandomSkin());
                    _skin = Player.Skin;
                    break;
                }
            }
            
            _player.Load();
            _player.ShowSongsList();
            WriteLine();
            _skin.Render("Введите номер песни для воспроизведения");
            _skin.Render("Для воспроизведения всех песен введите 0");
            _skin.Render("Для выхода нажмите ESC");
            int p = 0;
            while (p!=1)
            {
                int number = int.Parse(ReadLine());
                if (number == 0)
                {
                    _player.Play(Player.Songs);
                }
                else
                {
                    _player.Play(Player.Songs[number - 1]);
                }
                if (ReadKey(true).Key==ConsoleKey.Escape)
                {
                    p = 1;
                }
            }
            _player.Dispose();
            //while (true)
            //{
            //    var key = ReadKey();
            //    switch (key.Key)
            //    {
            //        case ConsoleKey.Escape:
            //            return;
                        
                    
                   
            //    }
            //}
            //player.SaveAsPlaylist();
            //player.LoadPlayList();
            //Player.Play(Player.Songs);
        }

        private static string ChooseColor()
        {
            _skin.Render("Выберете цвет из списка");
            for (int i = 0; i < 15; i++)
            {
                _skin.Render($"{i + 1}. {Enum.Parse(typeof(ConsoleColor), i.ToString())}");
            }
            WriteLine();
            int number = int.Parse(ReadLine());
            number--;
            return number.ToString();
        }



    }
}
