using CustomAlgoritmen.Sorting;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;
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
        public void Sort_NullOrSmall_ShouldNotCrash()
        {
            int[] a = null;
            MergeSorter<int>.Sort(a);
            InsertionSorter<int>.Sort(a);

            int[] b = Array.Empty<int>();
            MergeSorter<int>.Sort(b);
            InsertionSorter<int>.Sort(b);
            Assert.Empty(b);

            int[] c = { 42 };
            MergeSorter<int>.Sort(c);
            InsertionSorter<int>.Sort(c);
            Assert.Equal(new[] { 42 }, c);
        }

        [Fact]
        public void Sort_AlreadySorted_ShouldStaySorted()
        {
            int[] data = { 1, 2, 3, 4, 5 };
            var m = (int[])data.Clone();
            var i = (int[])data.Clone();

            MergeSorter<int>.Sort(m);
            InsertionSorter<int>.Sort(i);

            Assert.Equal(data, m);
            Assert.Equal(data, i);
        }

        [Fact]
        public void Sort_ReverseSorted_ShouldSortCorrectly()
        {
            int[] data = { 5, 4, 3, 2, 1 };
            var m = (int[])data.Clone();
            var i = (int[])data.Clone();

            MergeSorter<int>.Sort(m);
            InsertionSorter<int>.Sort(i);

            Assert.True(IsSorted(m));
            Assert.True(IsSorted(i));
        }

        [Fact]
        public void Sort_WithDuplicates_ShouldSortCorrectly()
        {
            int[] data = { 3, 1, 2, 3, 1, 2 };
            var m = (int[])data.Clone();
            var i = (int[])data.Clone();

            MergeSorter<int>.Sort(m);
            InsertionSorter<int>.Sort(i);

            Assert.True(IsSorted(m));
            Assert.True(IsSorted(i));
        }

        [Fact]
        public void Compare_Sorting_Performance()
        {
            int n = 20000; // insertion sort is O(n^2), so keep n reasonable
            var rnd = new Random(123); // fixed seed for repeatability

            int[] original = Enumerable.Range(0, n).Select(_ => rnd.Next(0, n)).ToArray();
            int[] mergeData = (int[])original.Clone();
            int[] insertionData = (int[])original.Clone();

            var sw = Stopwatch.StartNew();
            MergeSorter<int>.Sort(mergeData);
            sw.Stop();
            long mergeTime = sw.ElapsedMilliseconds;

            sw.Restart();
            InsertionSorter<int>.Sort(insertionData);
            sw.Stop();
            long insertionTime = sw.ElapsedMilliseconds;

            _output.WriteLine($"N = {n}");
            _output.WriteLine($"Merge Sort: {mergeTime}ms");
            _output.WriteLine($"Insertion Sort: {insertionTime}ms");

            Assert.True(IsSorted(mergeData));
            Assert.True(IsSorted(insertionData));
        }

        private static bool IsSorted<T>(T[] array) where T : IComparable<T>
        {
            for (int i = 0; i < array.Length - 1; i++)
                if (array[i].CompareTo(array[i + 1]) > 0) return false;
            return true;
        }
    }
}
