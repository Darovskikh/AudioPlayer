using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    static class CutString
    {
        public static string CutStringSymbols(this string sentence)
        {            
            if (sentence.Length > 10)
            {
                sentence = sentence.Remove(10);
                sentence = sentence + "...";
            }
            return sentence;
        }
    }
}
