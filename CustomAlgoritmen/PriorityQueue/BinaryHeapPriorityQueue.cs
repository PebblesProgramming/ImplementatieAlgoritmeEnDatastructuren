using CustomAlgoritmen.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAlgoritmen.PriorityQueue
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> _data = new List<T>();

        public int Count => _data.Count;

        public void Enqueue(T item) 
        {
            _data.Add(item);
            int childIndex = _data.Count - 1;
            while (childIndex > 0)
            {
                int parentIndex = (childIndex - 1) / 2;
                if (_data[childIndex].CompareTo(_data[parentIndex]) >= 0) break;
                Swap(childIndex, parentIndex);
                childIndex = parentIndex;
            }
        }

        public T Peek() 
        {
            if (_data.Count == 0)
                throw new InvalidOperationException("PriorityQueue is empty");
            return _data[0];
        }

        public T Dequeue() 
        {
            if (_data.Count == 0)
                throw new InvalidOperationException("PriorityQueue is empty");

            int lastIndex = _data.Count - 1;
            T frontItem = _data[0];
            _data[0] = _data[lastIndex];
            _data.RemoveAt(lastIndex);

            lastIndex--;
            int parentIndex = 0;
            while (true)
            {
                int leftChildIndex = parentIndex * 2 + 1;
                if (leftChildIndex > lastIndex) break;

                int rightChildIndex = leftChildIndex + 1;
                int targetChildIndex = leftChildIndex;

                if (rightChildIndex <= lastIndex &&
                    _data[rightChildIndex].CompareTo(_data[leftChildIndex]) < 0)
                    targetChildIndex = rightChildIndex;

                if (_data[parentIndex].CompareTo(_data[targetChildIndex]) <= 0)
                    break;

                Swap(parentIndex, targetChildIndex);
                parentIndex = targetChildIndex;
            }

            return frontItem;
        }

        private void Swap(int a, int b)
        {
            T tmp = _data[a];
            _data[a] = _data[b];
            _data[b] = tmp;
        }
    }

}
