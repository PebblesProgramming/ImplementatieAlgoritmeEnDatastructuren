using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAlgoritmen.Lists
{
    public class DynamicArray<T>
    {
        private T[] _items;
        private int _count;
        public int Count => _count;
        public int Capacity => _items.Length;

        public DynamicArray(int capacity = 4)
        {
            _items = new T[capacity];
        }

        // O(1) - directe geheugenberekening: startadres + (index × elementgrootte)
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count) throw new IndexOutOfRangeException();
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _count) throw new IndexOutOfRangeException();
                _items[index] = value;
            }
        }

        // O(1) amortized - direct plaatsen op _items[_count]
        // Worst case O(n) bij resize: alle elementen kopiëren naar nieuwe array
        public void Add(T item)
        {
            if (_count == _items.Length) Resize();
            _items[_count++] = item;
        }

        // O(n) - alle elementen na index moeten opschuiven
        public void Insert(int index, T item)
        {
            if (index < 0 || index > _count) throw new IndexOutOfRangeException();
            if (_count == _items.Length) Resize();

            for (int i = _count; i > index; i--)
                _items[i] = _items[i - 1];

            _items[index] = item;
            _count++;
        }

        // O(n) - alle elementen na index moeten opschuiven
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count) throw new IndexOutOfRangeException();
            for (int i = index; i < _count - 1; i++)
                _items[i] = _items[i + 1];
            _count--;
            _items[_count] = default;
        }

        // O(n) - alle elementen kopiëren naar nieuwe array van dubbele grootte
        private void Resize()
        {
            T[] newArray = new T[_items.Length * 2];
            Array.Copy(_items, 0, newArray, 0, _count);
            _items = newArray;
        }
    }
}
