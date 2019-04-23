using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Album
    {
        public string Name { get; private set; }
        public string Path { get; private set; }
        public int Year { get; private set; }

        public Album(string name, int year)
        {
            Name = name;
            Year = year;
        }
    }
}
