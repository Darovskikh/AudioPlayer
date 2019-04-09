using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Player
    {
        private int _volume;
        public bool IsLock;
        private const int _maxVolume = 100;
        public Song[] Songs;
        public int Volume
        {
            get
            {
                return _volume;
            }
            private set  // считывается только в рамках класса            
            {
                if (value> _maxVolume )
                {
                    _volume = _maxVolume;
                }
                else if (value <0)
                {
                    _volume = 0;
                }
                else { _volume = value; }
            }
        }
        public void Play()
        {
            for (int i = 0; i < Songs.Length; i++)
            {
                Console.WriteLine(Songs[i].title);
                System.Threading.Thread.Sleep(2000);
            }          
        }

        public void VolumeUP()
        {
            Volume += 5;
            Console.WriteLine($"Volume is: {Volume}"); 
        }

        public void VolumeDown()
        {
            Volume  -= 5;
            Console.WriteLine($"Volume is: {Volume}");
        }

        public void Load()
        {
            Console.WriteLine("Loading music");
        }

        public void Lock()
        {
            Console.WriteLine("The player is locked");
        }

        public void Save()
        {
            Console.WriteLine("Saving music");
        }
    }
}
