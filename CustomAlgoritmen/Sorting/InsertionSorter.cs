using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAlgoritmen.Sorting
{
    public static class InsertionSorter<T> where T : IComparable<T>
    {
        // Time complexity: Best O(n), Worst O(n²)
        // Space complexity: O(1) - sorteert in-place, geen extra geheugen nodig
        public static void Sort(T[] array)
        {
            if (array == null || array.Length <= 1) return;

            int n = array.Length;
            for (int i = 1; i < n; ++i)
            {
                T key = array[i];
                int j = i - 1;

                // Schuift elementen die groter zijn dan key op naar rechts
                // Best case: array al gesorteerd → 0 verschuivingen per iteratie
                // Worst case: array omgekeerd → i verschuivingen per iteratie
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
