using CustomAlgoritmen;
using CustomAlgoritmen.Lists;
using System.Diagnostics;
using Xunit.Abstractions;

namespace TestProject
{
    public class ListTests
    {
        private readonly ITestOutputHelper _output;
        public ListTests(ITestOutputHelper output) { _output = output; }

        [Fact]
        public void Correctness_Test()
        {
            var da = new DynamicArray<int>();
            da.Add(1); da.Add(2);
            da.Insert(0, 0);
            Assert.Equal(0, da[0]);
            Assert.Equal(3, da.Count);
            
            var ll = new SinglyLinkedList<int>();
            ll.AddFirst(10);
            ll.AddLast(20);
            Assert.True(ll.Remove(10));
            Assert.Equal(1, ll.Count);
        }

        [Fact]
        public void Performance_Comparison_AddAtStart()
        {
            int n = 50000;

            // Test DynamicArray
            var sw = Stopwatch.StartNew();
            var da = new DynamicArray<int>();
            for (int i = 0; i < n; i++) da.Insert(0, i);
            sw.Stop();
            var daTime = sw.ElapsedMilliseconds;

            // Test LinkedList
            sw.Restart();
            var ll = new SinglyLinkedList<int>();
            for (int i = 0; i < n; i++) ll.AddFirst(i);
            sw.Stop();
            var llTime = sw.ElapsedMilliseconds;

            _output.WriteLine($"Add {n} items at START:");
            _output.WriteLine($"DynamicArray: {daTime}ms (O(n^2) totaal)");
            _output.WriteLine($"LinkedList: {llTime}ms (O(n) totaal)");

            Assert.True(llTime < daTime);
        }
    }
}