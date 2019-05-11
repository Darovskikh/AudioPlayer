using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    static class Sorting
    {
        public static List<T> SortByTitle<T>(this List<T> items) where T: PlayingItem<T>
        {
            List<string> titles = new List<string>();
            List<T> sortedItems = new List<T>();
            foreach (T item in items)
            {
                titles.Add(item.Title);
            }

            titles.Sort();
            foreach (string title in titles)
            {
                foreach (T item in items)
                {
                    if (title == item.Title)
                    {
                        sortedItems.Add(item);
                    }
                }
            }
            return sortedItems;
        }
    }
}
