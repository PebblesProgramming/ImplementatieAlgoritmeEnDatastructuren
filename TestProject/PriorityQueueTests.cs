using CustomAlgoritmen.PriorityQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class PriorityQueueTests
    {
        [Fact]
        public void Dequeue_OnEmpty_ShouldThrow()
        {
            var pq = new PriorityQueue<int>();
            Assert.Throws<InvalidOperationException>(() => pq.Dequeue());
        }

        [Fact]
        public void Count_ShouldGoBackToZero()
        {
            var pq = new PriorityQueue<int>();
            pq.Enqueue(2);
            pq.Enqueue(1);

            Assert.Equal(2, pq.Count);
            pq.Dequeue();
            pq.Dequeue();
            Assert.Equal(0, pq.Count);
        }

        [Fact]
        public void Duplicates_ShouldAllComeOut()
        {
            var pq = new PriorityQueue<int>();
            pq.Enqueue(5);
            pq.Enqueue(5);
            pq.Enqueue(5);

            Assert.Equal(5, pq.Dequeue());
            Assert.Equal(5, pq.Dequeue());
            Assert.Equal(5, pq.Dequeue());
        }

        [Fact]
        public void ManyItems_ShouldComeOutSorted()
        {
            var pq = new PriorityQueue<int>();
            int n = 1000;

            for (int i = n; i >= 1; i--) pq.Enqueue(i);

            for (int expected = 1; expected <= n; expected++)
                Assert.Equal(expected, pq.Dequeue());
        }

        [Fact]
        public void InterleavedOperations_ShouldWork()
        {
            var pq = new PriorityQueue<int>();
            pq.Enqueue(10);
            pq.Enqueue(3);

            Assert.Equal(3, pq.Dequeue());

            pq.Enqueue(7);
            pq.Enqueue(1);

            Assert.Equal(1, pq.Dequeue());
            Assert.Equal(7, pq.Dequeue());
            Assert.Equal(10, pq.Dequeue());
        }
    }
}

