using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public abstract class GenericPlayer <T,TV>
    {
        protected static Skin Skin { get; set; }
        public static bool Loop { get; set; }
        private int _volume;
        public bool IsLock { get; private set; }
        private const int _maxVolume = 100;
        public static List<T> Items { get; set; } = new List<T>();
        public int Volume
        {
            get
            {
                return _volume;
            }
            private set           
            {
                if (value > _maxVolume)
                {
                    _volume = _maxVolume;
                }
                else if (value < 0)
                {
                    _volume = 0;
                }
                else { _volume = value; }
            }
        }
        public GenericPlayer(Skin skin)
        {
            Skin = skin;
        }
        public abstract void Play(List<T> items, bool loop);
        
        public void VolumeUP()
        {
            Volume += 5;
            //Console.WriteLine($"Volume is: {Volume}");
            Skin.Render($"Volume is: {Volume}");
        }

        public void VolumeDown()
        {
            Volume -= 5;
            //Console.WriteLine($"Volume is: {Volume}");
            Skin.Render($"Volume is: {Volume}");
        }

        public static void Add(params T[] items)
        {
            foreach (T item in items)
            {
                Items.Add(item);
            }
        }
        public abstract void WriteItemList(List<T> items);
        public abstract List<T> FilterByGenre(List<T> items,TV genre);
        public abstract void WriteItemData(T item, ConsoleColor color);
    }
}
    

