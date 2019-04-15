﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Player
    {
        private int _volume;    
        public bool IsLock { get; private set; }
        private const int _maxVolume = 100;
        public static Song[] Songs { get; set; }
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
                Console.WriteLine(Songs[i].title+" " + Songs [i].Artist.Name+" "+Songs[i].duration  );
                System.Threading.Thread.Sleep(Songs [i].duration );
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

        public static void Add(params Song[] songs)
        {
            Songs = songs;
        }
    }
}
