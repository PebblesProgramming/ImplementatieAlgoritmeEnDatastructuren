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
            var pq = new PriorityQueue<string, int>();
            Assert.Throws<InvalidOperationException>(() => pq.Dequeue());
        }

        [Fact]
        public void Count_ShouldGoBackToZero()
        {
            var pq = new PriorityQueue<string, int>();
            pq.Enqueue("taak A", 2);
            pq.Enqueue("taak B", 1);

            Assert.Equal(2, pq.Count);
            pq.Dequeue();
            pq.Dequeue();
            Assert.Equal(0, pq.Count);
        }

        [Fact]
        public void Duplicates_ShouldAllComeOut()
        {
            var pq = new PriorityQueue<string, int>();
            pq.Enqueue("taak A", 5);
            pq.Enqueue("taak B", 5);
            pq.Enqueue("taak C", 5);

            // Alle drie moeten eruit komen (volgorde niet gegarandeerd bij gelijke prioriteit)
            var results = new List<string> { pq.Dequeue(), pq.Dequeue(), pq.Dequeue() };
            Assert.Contains("taak A", results);
            Assert.Contains("taak B", results);
            Assert.Contains("taak C", results);
        }

        [Fact]
        public void ManyItems_ShouldComeOutByPriority()
        {
            var pq = new PriorityQueue<int, int>();
            int n = 1000;

            // Voeg elementen toe met prioriteit gelijk aan de waarde
            for (int i = n; i >= 1; i--)
                pq.Enqueue(i, i);  // element=i, priority=i

            // Laagste prioriteit (1) komt eerst
            for (int expected = 1; expected <= n; expected++)
                Assert.Equal(expected, pq.Dequeue());
        }

        [Fact]
        public void InterleavedOperations_ShouldWork()
        {
            var pq = new PriorityQueue<string, int>();
            pq.Enqueue("laag", 10);
            pq.Enqueue("hoog", 3);

            Assert.Equal("hoog", pq.Dequeue());  // prioriteit 3 komt eerst

            pq.Enqueue("middel", 7);
            pq.Enqueue("hoogst", 1);

            Assert.Equal("hoogst", pq.Dequeue()); // prioriteit 1
            Assert.Equal("middel", pq.Dequeue()); // prioriteit 7
            Assert.Equal("laag", pq.Dequeue());   // prioriteit 10
        }

        [Fact]
        public void DifferentTypes_ShouldWork()
        {
            // Element = string, Priority = int
            var pq = new PriorityQueue<string, int>();
            pq.Enqueue("Brand blussen", 1);
            pq.Enqueue("Email beantwoorden", 3);
            pq.Enqueue("Koffie halen", 5);

            Assert.Equal("Brand blussen", pq.Dequeue());      // prioriteit 1
            Assert.Equal("Email beantwoorden", pq.Dequeue()); // prioriteit 3
            Assert.Equal("Koffie halen", pq.Dequeue());       // prioriteit 5
        }
    }
}
