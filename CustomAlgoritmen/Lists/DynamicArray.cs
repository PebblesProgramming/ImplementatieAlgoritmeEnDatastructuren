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

        public void Add(T item) // O(1) amortized
        {
            if (_count == _items.Length) Resize();
            _items[_count++] = item;
        }

        public void Insert(int index, T item) // O(n) worst case
        {
            if (index < 0 || index > _count) throw new IndexOutOfRangeException();
            if (_count == _items.Length) Resize();

            for (int i = _count; i > index; i--)
                _items[i] = _items[i - 1];

            _items[index] = item;
            _count++;
        }

        public void RemoveAt(int index) // O(n)
        {
            if (index < 0 || index >= _count) throw new IndexOutOfRangeException();
            for (int i = index; i < _count - 1; i++)
                _items[i] = _items[i + 1];
            _count--;
            _items[_count] = default;
        }

        private void Resize()
        {
            T[] newArray = new T[_items.Length * 2];
            Array.Copy(_items, 0, newArray, 0, _count);
            _items = newArray;
        }
    }
}
