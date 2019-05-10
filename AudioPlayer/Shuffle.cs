using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    static class Shuffle 
    {
        public static List<T> ShuffleSongs<T>(this List<T> items)
        {
            Random random = new Random();
            List<T> shuffledItems = new List<T>();
            List<int> numbers = new List<int>();
            int j = 0, i = 0, p = 0;
            foreach (T item in items)
            {
                while (p != 1)
                {
                    i = random.Next(0, items.Count);
                    if (numbers.Contains(i))
                    {
                        p = 0;
                    }
                    else
                    {
                        numbers.Add(i);
                        p = 1;
                    }
                }

                shuffledItems.Add(items[i]);
                p = 0;
            }
            return shuffledItems;
        }
    }
}
