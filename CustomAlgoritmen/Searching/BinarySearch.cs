using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAlgoritmen.Searching
{
    public static class BinarySearch<T> where T : IComparable<T>
    {
        public static int Search(T[] array, T target)
        {
            int low = 0;
            int high = array.Length - 1;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                int comparison = array[mid].CompareTo(target);

                if (comparison == 0)
                {
                    return mid; // Gevonden!
                }
                if (comparison < 0)
                {
                    low = mid + 1; // Zoek in de rechterhelft
                }
                else
                {
                    high = mid - 1; // Zoek in de linkerhelft
                }
            }

            return -1; // Niet gevonden
        }
    }
}
