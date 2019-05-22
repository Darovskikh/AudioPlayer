using System;
using System.Diagnostics.Eventing.Reader;
using static System.Console;

namespace AudioPlayer
{
    class Program
    {
        private static Player _player;
        private static Skin _skin;
        static void Main(string[] args)
        {
            _skin = new ClassicSkin();
            _skin.Render("Выберете стиль текста");
            _skin.Render("1. Классический стиль");
            _skin.Render("2. Цветной стиль");
            _skin.Render("3. Рандомный цвет стиль");
            WriteLine();
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    {
                        _player = new Player(new ClassicSkin());
                        _skin = Player.Skin;
                        break;
                    }
                case ConsoleKey.NumPad1:
                    {
                        _player = new Player(new ClassicSkin());
                        _skin = Player.Skin;
                        break;
                    }

                case ConsoleKey.D2:
                    {
                        WriteLine();
                        _player = new Player(new ColorSkin(ChooseColor()));
                        _skin = Player.Skin;
                        break;
                    }
                case ConsoleKey.NumPad2:
                    {
                        WriteLine();
                        _player = new Player(new ColorSkin(ChooseColor()));
                        _skin = Player.Skin;
                        break;
                    }
                case ConsoleKey.D3:
                    {
                        _player = new Player(new RandomSkin());
                        _skin = Player.Skin;
                        break;
                    }
                case ConsoleKey.NumPad3:
                    {
                        _player = new Player(new RandomSkin());
                        _skin = Player.Skin;
                        break;
                    }
            }
            _player.PlayerStartedEvent += (sender, e) => { _skin.Render(e.Message); };
            _player.PlayerStoppedEvent += (sender, e) => { _skin.Render(e.Message); };
            _player.SongStarted += (sender, e) => { _skin.Render(e.Message); };
            _player.SongListChanged += (sender, e) => { _skin.Render(e.Message); };
            _player.VolumeStatus += (sender, e) => { _skin.Render(e.Message); };
            _player.LockOffEvent += (sender, e) => { _skin.Render(e.Message); };
            _player.LockOnEvent += (sender, e) => { _skin.Render(e.Message); };
            _player.Load();
            int p = 0;
            while (p != 1)
            {
                ShowSongsList();
                if (!_player.IsLock)
                {
                    switch (ReadKey(true).Key)
                    {
                        case ConsoleKey.A:
                            {
                                _player.Load();
                                break;
                            }

                        case ConsoleKey.D0:
                            {
                                _player.Play(_player.Songs);
                                break;
                            }
                        case ConsoleKey.NumPad0:
                            {
                                _player.Play(_player.Songs);
                                break;
                            }

                        case ConsoleKey.C:
                            {
                                _player.Clear();
                                break;
                            }

                        case ConsoleKey.Escape:
                            {
                                p = 1;
                                break;
                            }

                        case ConsoleKey.P:
                            {
                                try
                                {
                                    _skin.Render("Введите номер песни для воспроизведения");
                                    int number = int.Parse(ReadLine());
                                    _player.Play(_player.Songs[number - 1]);
                                }
                                catch (Exception e)
                                {
                                    _skin.Render(e.Message);
                                }

                                break;
                            }

                        case ConsoleKey.U:
                            {
                                _player.VolumeUP();
                                break;
                            }

                        case ConsoleKey.D:
                            {
                                _player.VolumeDown();
                                break;
                            }

                        case ConsoleKey.L:
                            {
                                _player.LockOn();
                                break;
                            }
                    }
                    
                }

                switch (ReadKey(true).Key)
                {
                    case ConsoleKey.N:
                        {
                            _player.LockOff();
                            break;
                        }
                }


            }
            _player.Dispose();
        }

        private static void _player_LockOnEvent(Player sender, PlayerEventArgs e)
        {
            throw new NotImplementedException();
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

        private static void ShowSongsList()
        {
            int i = 0;
            WriteLine();
            _skin.Render("Текущий плейлист:");
            foreach (var song in _player.Songs)
            {
                if (song.Artist.Name == null)
                {
                    song.Artist.Name = "Unknwon artist";
                }
                if (song.Title == null)
                {
                    song.Title = "No title";
                }
                i++;
                if (song.Playing)
                {
                    _skin.Render($"{i}. {song.Artist.Name} - {song.Title}", ConsoleColor.White);
                }
                else if (song.LikeStatus.HasValue)
                {
                    if (song.LikeStatus == true)
                    {
                        _skin.Render($"{i}. {song.Artist.Name} - {song.Title}", ConsoleColor.Green);
                    }
                    else
                    {
                        _skin.Render($"{i}. {song.Artist.Name} - {song.Title}", ConsoleColor.Red);
                    }
                }
                else
                {
                    _skin.Render($"{i}. {song.Artist.Name} - {song.Title}");
                }
            }
        }


    }
}
