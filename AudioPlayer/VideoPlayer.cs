using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class VideoPlayer : GenericPlayer< Video,GenreVideo>
    {
        public VideoPlayer(Skin skin) : base(skin)
        {
        }
        public override void Play(List<Video> items, bool loop)
        {
            foreach (Video video in items)
            {
                if (loop)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        video.Playing = true;
                        WriteItemList(items);
                    }
                }
                else
                {
                    video.Playing = true;
                    WriteItemList(items);
                    video.Playing = false;
                    System.Threading.Thread.Sleep(video.Duration + 2000);
                }
            }
        }
        public override void WriteItemList(List<Video> items)
        {
            foreach (Video video  in items)
            {
                if (video.Playing)
                {
                    WriteItemData(video, ConsoleColor.Blue);
                }
                else
                {
                    if (video.LikeStatus.HasValue)
                    {
                        if (video.LikeStatus == true)
                        {
                            WriteItemData(video, ConsoleColor.Red);
                        }
                        else
                        {
                            WriteItemData(video, ConsoleColor.Green);
                        }
                    }
                    else WriteItemData(video, ConsoleColor.White);
                }
            }
        }
        public override List<Video> FilterByGenre(List<Video> items, GenreVideo genre)
        {
            List<Video> filteredSongs = new List<Video>();
            foreach (Video video in items)
            {
                if (video.Genre == genre)
                {
                    filteredSongs.Add(video);
                }
            }
            return filteredSongs;
        }

        public override void WriteItemData(Video item, ConsoleColor color)
        {
            //var videoData = GetvideoData(video);
            //Console.ForegroundColor = color;
            //Console.WriteLine($"Title - {videoData.title.CutStringSymbols()}");
            //Console.ResetColor();
            //Console.WriteLine($"Duration - {videoData.minutes}.{videoData.seconds}");
            //Console.WriteLine($"Artist - {videoData.artistName}");
            //Console.WriteLine($"Album - {videoData.album}");
            //Console.WriteLine($"Year - {videoData.year}");
            //Console.WriteLine();
            var (title, minutes, seconds, producerName, year) = item;
            //Console.WriteLine($"Title - {title.CutStringSymbols()}");
            Skin.Render($"Title - {title.CutStringSymbols()}", color);
            Console.ResetColor();
            //Console.WriteLine($"Duration - {minutes}.{seconds}");
            Skin.Render($"Duration - {minutes}.{seconds}");
            //Console.WriteLine($"Artist - {artistName}");
            Skin.Render($"Artist - {producerName}");
            //Console.WriteLine($"Album - {album}");
            //Console.WriteLine($"Year - {year}");
            Skin.Render($"Year - {year}");
            Console.WriteLine();
        }
    }
}
