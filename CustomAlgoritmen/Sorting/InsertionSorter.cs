using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAlgoritmen.Sorting
{
    public static class InsertionSorter<T> where T : IComparable<T>
    {
        public static void Sort(T[] array)
        {
            if (array == null || array.Length <= 1) return;

            int n = array.Length;
            for (int i = 1; i < n; ++i)
            {
                T key = array[i];
                int j = i - 1;

                // Verplaats elementen die groter zijn dan de key naar één positie verder
                while (j >= 0 && array[j].CompareTo(key) > 0)
                {
                    array[j + 1] = array[j];
                    j = j - 1;
                }
                array[j + 1] = key;
            }
        }
    }
}
