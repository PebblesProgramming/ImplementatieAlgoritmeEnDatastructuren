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
        public void EnqueueDequeue_ShouldReturnInCorrectOrder()
        {
            var pq = new PriorityQueue<int>();

            // Voeg getallen in willekeurige volgorde toe
            pq.Enqueue(100);
            pq.Enqueue(10);
            pq.Enqueue(50);
            pq.Enqueue(1);

            // Controleer of ze van klein naar groot verschijnen (Min-Heap)
            Assert.Equal(1, pq.Dequeue());
            Assert.Equal(10, pq.Dequeue());
            Assert.Equal(50, pq.Dequeue());
            Assert.Equal(100, pq.Dequeue());
        }

        [Fact]
        public void Count_ShouldBeCorrect()
        {
            var pq = new PriorityQueue<string>();
            pq.Enqueue("A");
            pq.Enqueue("B");

            Assert.Equal(2, pq.Count);
            pq.Dequeue();
            Assert.Equal(1, pq.Count);
        }
    }
}
