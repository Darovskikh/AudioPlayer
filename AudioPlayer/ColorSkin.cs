using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class ColorSkin : Skin
    {
        public string Color { get; private set; }
        private ConsoleColor _color;
        public ColorSkin()
        {

        }
        public ColorSkin(string color )
        {
            string fl = Convert.ToString(color[0]);
            fl = fl.ToUpper();
            Color = color.Replace(color[0], Convert.ToChar(fl));
            GetColor(Color);
        }
        public override void Clear()
        {
            Console.Clear();
        }

        public override void Render(string text)
        {
            Console.ForegroundColor = _color;
            Console.WriteLine(text);
            Console.ResetColor();

        }
        public override void Render(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        protected void GetColor(string clr)
        {
            _color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), clr);
        }
    }
}
