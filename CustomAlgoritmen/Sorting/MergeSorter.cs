using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAlgoritmen.Sorting
{
    public static class MergeSorter<T> where T : IComparable<T>
    {
        // Time complexity: Altijd O(n log n) - best, average en worst case
        // Space complexity: O(n) - hulparray van dezelfde grootte als input
        public static void Sort(T[] array)
        {
            if (array == null || array.Length <= 1) return;
            T[] aux = new T[array.Length];           // Extra geheugen O(n)
            InternalSort(array, aux, 0, array.Length - 1);
        }

        // Splitst array recursief in twee helften tot subarrays van 1 element
        // Diepte van recursie: log₂(n) niveaus
        private static void InternalSort(T[] array, T[] aux, int low, int high)
        {
            if (high <= low) return;
            int mid = low + (high - low) / 2;

            InternalSort(array, aux, low, mid);       // Links splitsen
            InternalSort(array, aux, mid + 1, high);  // Rechts splitsen
            Merge(array, aux, low, mid, high);        // Samenvoegen
        }

        // Voegt twee gesorteerde helften samen - O(n) per niveau
        private static void Merge(T[] array, T[] aux, int low, int mid, int high)
        {
            // Kopieer naar hulp-array
            for (int k = low; k <= high; k++) aux[k] = array[k];

            int i = low, j = mid + 1;
            for (int k = low; k <= high; k++)
            {
                if (i > mid) array[k] = aux[j++];                          // Links is leeg
                else if (j > high) array[k] = aux[i++];                    // Rechts is leeg
                else if (aux[j].CompareTo(aux[i]) < 0) array[k] = aux[j++]; // Rechts is kleiner
                else array[k] = aux[i++];                                   // Links is kleiner of gelijk
            }
        }
    }
}
