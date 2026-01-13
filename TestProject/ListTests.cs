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

        [Fact]
        public void DynamicArray_Add_ResizesAndKeepsOrder()
        {
            var da = new DynamicArray<int>(capacity: 2);

            da.Add(1);
            da.Add(2);
            // Next add should trigger resize
            da.Add(3);

            Assert.Equal(3, da.Count);
            Assert.True(da.Capacity >= 3);
            Assert.Equal(1, da[0]);
            Assert.Equal(2, da[1]);
            Assert.Equal(3, da[2]);
        }

        [Fact]
        public void DynamicArray_Insert_Middle_ShiftsCorrectly()
        {
            var da = new DynamicArray<int>();
            da.Add(10);
            da.Add(30);

            da.Insert(1, 20);

            Assert.Equal(3, da.Count);
            Assert.Equal(10, da[0]);
            Assert.Equal(20, da[1]);
            Assert.Equal(30, da[2]);
        }

        [Fact]
        public void DynamicArray_RemoveAt_ShiftsAndClearsLast()
        {
            var da = new DynamicArray<int>();
            da.Add(10);
            da.Add(20);
            da.Add(30);
            da.Add(40);

            da.RemoveAt(1); // remove 20

            Assert.Equal(3, da.Count);
            Assert.Equal(10, da[0]);
            Assert.Equal(30, da[1]);
            Assert.Equal(40, da[2]);
        }

        [Fact]
        public void DynamicArray_Indexer_ThrowsOnOutOfRange()
        {
            var da = new DynamicArray<int>();
            da.Add(1);

            Assert.Throws<IndexOutOfRangeException>(() => { var _ = da[-1]; });
            Assert.Throws<IndexOutOfRangeException>(() => { var _ = da[1]; });
            Assert.Throws<IndexOutOfRangeException>(() => da[1] = 5);
        }

        [Fact]
        public void DynamicArray_Insert_ThrowsOnOutOfRange()
        {
            var da = new DynamicArray<int>();
            da.Add(1);

            Assert.Throws<IndexOutOfRangeException>(() => da.Insert(-1, 99));
            Assert.Throws<IndexOutOfRangeException>(() => da.Insert(2, 99)); // > Count
            // index == Count is allowed (insert at end)
            da.Insert(1, 2);
            Assert.Equal(2, da.Count);
            Assert.Equal(2, da[1]);
        }

        [Fact]
        public void DynamicArray_RemoveAt_ThrowsOnOutOfRange()
        {
            var da = new DynamicArray<int>();
            Assert.Throws<IndexOutOfRangeException>(() => da.RemoveAt(0));

            da.Add(1);
            Assert.Throws<IndexOutOfRangeException>(() => da.RemoveAt(-1));
            Assert.Throws<IndexOutOfRangeException>(() => da.RemoveAt(1));
        }

        [Fact]
        public void SinglyLinkedList_AddFirst_RemovesHeadCorrectly()
        {
            var ll = new SinglyLinkedList<int>();
            ll.AddFirst(10);
            ll.AddFirst(20); // list: 20 -> 10

            Assert.True(ll.Remove(20)); // remove head
            Assert.Equal(1, ll.Count);

            Assert.True(ll.Remove(10));
            Assert.Equal(0, ll.Count);
        }

        [Fact]
        public void SinglyLinkedList_AddLast_ThenRemoveMiddle()
        {
            var ll = new SinglyLinkedList<int>();
            ll.AddLast(10);
            ll.AddLast(20);
            ll.AddLast(30);

            Assert.True(ll.Remove(20)); // remove middle
            Assert.Equal(2, ll.Count);

            // Remaining should be 10 and 30; we can't index, so verify by removing them
            Assert.True(ll.Remove(10));
            Assert.True(ll.Remove(30));
            Assert.Equal(0, ll.Count);
        }

        [Fact]
        public void SinglyLinkedList_Remove_ReturnsFalseWhenNotFound()
        {
            var ll = new SinglyLinkedList<int>();
            Assert.False(ll.Remove(123)); // empty list

            ll.AddFirst(1);
            ll.AddLast(2);
            Assert.False(ll.Remove(999)); // not in list
            Assert.Equal(2, ll.Count);    // count unchanged
        }
    }
}