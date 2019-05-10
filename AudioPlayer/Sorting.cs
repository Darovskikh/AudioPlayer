using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    static class Sorting
    {
        public static List<PlayingItem<T>> SortByTitle<T>(this List<PlayingItem<T>> items)
        {
            List<string> titles = new List<string>();
            List<PlayingItem<T>> sortedItems = new List<PlayingItem<T>>();
            foreach (PlayingItem<T> item in items)
            {
                titles.Add(item.Title);
            }

            titles.Sort();
            foreach (string title in titles)
            {
                foreach (PlayingItem<T> item in items)
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
