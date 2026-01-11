using CustomAlgoritmen.Searching;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace TestProject
{
    public class BinarySearchTests
    {

        private readonly ITestOutputHelper _output;
        public BinarySearchTests(ITestOutputHelper output) { _output = output; }

        [Fact]
        public void Search_ShouldFindElement()
        {
            int[] data = { 1, 3, 5, 7, 9, 11 }; // Al gesorteerd
            int index = BinarySearch<int>.Search(data, 7);
            Assert.Equal(3, index);
        }

        [Fact]
        public void Compare_Search_Performance()
        {
            int n = 1_000_000; // 1 miljoen items
            int[] data = Enumerable.Range(0, n).ToArray(); // Reeds gesorteerd
            int target = n - 1; // We zoeken het allerlaatste item

            // 1. Lineair Zoeken (O(n))
            var sw = Stopwatch.StartNew();
            int linearIndex = -1;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == target) { linearIndex = i; break; }
            }
            sw.Stop();
            long linearTime = sw.ElapsedTicks; // Ticks gebruiken omdat ms te grof is voor Binary Search

            // 2. Binary Search (O(log n))
            sw.Restart();
            int binaryIndex = BinarySearch<int>.Search(data, target);
            sw.Stop();
            long binaryTime = sw.ElapsedTicks;

            _output.WriteLine($"Zoeken in {n} items:");
            _output.WriteLine($"Lineair zoeken: {linearTime} ticks");
            _output.WriteLine($"Binary Search: {binaryTime} ticks");

            Assert.Equal(linearIndex, binaryIndex);
            Assert.True(binaryTime < linearTime);
        }
    }
}
