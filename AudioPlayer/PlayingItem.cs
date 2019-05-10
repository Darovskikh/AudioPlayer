using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public abstract class PlayingItem<T>
    {
        public bool Playing { get; set; }
        public bool? LikeStatus { get; set; }
        public int Duration { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public Genre Genre { get; set; }
        public static void Like(PlayingItem<T> item)
        {
            item.LikeStatus = true;
        }
        public static void Dislike(PlayingItem<T> item)
        {
            item.LikeStatus = false;
        }

    }
    
}

