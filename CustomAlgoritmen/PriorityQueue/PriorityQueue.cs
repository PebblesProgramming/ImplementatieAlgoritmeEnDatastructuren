using System;
using System.Collections.Generic;

namespace CustomAlgoritmen.PriorityQueue
{
    // Priority Queue geïmplementeerd met een ongesorteerde lijst
    // TElement: het type van de elementen in de queue
    // TPriority: het type van de prioriteit (moet IComparable zijn)
    // Laagste prioriteitswaarde = hoogste prioriteit (1 komt voor 2)
    public class PriorityQueue<TElement, TPriority> where TPriority : IComparable<TPriority>
    {
        private List<(TElement Element, TPriority Priority)> _data = new List<(TElement, TPriority)>();

        public int Count => _data.Count;

        // O(1) - voegt element met prioriteit toe aan einde van de lijst
        public void Enqueue(TElement element, TPriority priority)
        {
            _data.Add((element, priority));
        }

        // O(n) - moet door hele lijst zoeken naar element met laagste prioriteit
        public TElement Peek()
        {
            if (_data.Count == 0)
                throw new InvalidOperationException("PriorityQueue is empty");

            int minIndex = FindMinIndex();
            return _data[minIndex].Element;
        }

        // O(n) - moet door hele lijst zoeken naar element met laagste prioriteit
        // Best case O(n): element met laagste prioriteit staat vooraan (volledige scan nodig)
        // Worst case O(n): element met laagste prioriteit staat achteraan
        public TElement Dequeue()
        {
            if (_data.Count == 0)
                throw new InvalidOperationException("PriorityQueue is empty");

            // Zoek index van element met laagste prioriteit O(n)
            int minIndex = FindMinIndex();

            // Haal element op
            TElement minElement = _data[minIndex].Element;

            // Verwijder door laatste element naar minIndex te verplaatsen O(1)
            int lastIndex = _data.Count - 1;
            _data[minIndex] = _data[lastIndex];
            _data.RemoveAt(lastIndex);

            return minElement;
        }

        // O(n) - doorloopt hele lijst om element met laagste prioriteit te vinden
        private int FindMinIndex()
        {
            int minIndex = 0;
            for (int i = 1; i < _data.Count; i++)
            {
                if (_data[i].Priority.CompareTo(_data[minIndex].Priority) < 0)
                    minIndex = i;
            }
            return minIndex;
        }
    }
}
