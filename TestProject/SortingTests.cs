using CustomAlgoritmen.Sorting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace TestProject
{
    public class SorterTests
    {
        private readonly ITestOutputHelper _output;
        public SorterTests(ITestOutputHelper output) { _output = output; }

        [Fact]
        public void MergeSort_ShouldSortCorrectly()
        {
            int[] data = { 5, 2, 9, 1, 5, 6 };
            MergeSorter<int>.Sort(data);
            Assert.Equal(new[] { 1, 2, 5, 5, 6, 9 }, data);
        }

        [Fact]
        public void InsertionSort_ShouldSortCorrectly()
        {
            string[] data = { "banaan", "appel", "citroen" };
            InsertionSorter<string>.Sort(data);
            Assert.Equal(new[] { "appel", "banaan", "citroen" }, data);
        }

        [Fact]
        public void Compare_Sorting_Performance()
        {
            // Gebruik een kleinere N voor Insertion Sort omdat O(n^2) traag is
            int n = 20000;
            Random rnd = new Random();
            int[] original = Enumerable.Range(0, n).Select(_ => rnd.Next(0, n)).ToArray();

            int[] mergeData = (int[])original.Clone();
            int[] insertionData = (int[])original.Clone();

            // Measure Merge Sort
            var sw = Stopwatch.StartNew();
            MergeSorter<int>.Sort(mergeData);
            sw.Stop();
            long mergeTime = sw.ElapsedMilliseconds;

            // Measure Insertion Sort
            sw.Restart();
            InsertionSorter<int>.Sort(insertionData);
            sw.Stop();
            long insertionTime = sw.ElapsedMilliseconds;

            _output.WriteLine($"N = {n}");
            _output.WriteLine($"Merge Sort: {mergeTime}ms");
            _output.WriteLine($"Insertion Sort: {insertionTime}ms");

            // Check of ze echt gesorteerd zijn
            Assert.True(IsSorted(mergeData));
            Assert.True(IsSorted(insertionData));
        }

        private bool IsSorted<T>(T[] array) where T : IComparable<T>
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i].CompareTo(array[i + 1]) > 0) return false;
            }
            return true;
        }
    }
}
