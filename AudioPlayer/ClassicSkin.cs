using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class ClassicSkin : Skin
    {
        public override void Clear()
        {
            Console.Clear();
        }

        public override void Render(string text)
        {
            Console.WriteLine(text);
        }

        public override void Render(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
