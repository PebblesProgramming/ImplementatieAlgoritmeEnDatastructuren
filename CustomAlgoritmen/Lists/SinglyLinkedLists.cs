using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAlgoritmen.Lists
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }
        public Node(T value) { Value = value; }
    }

    public class SinglyLinkedList<T>
    {
        private Node<T> _head;
        private int _count;
        public int Count => _count;

        public void AddFirst(T item) // O(1)
        {
            var newNode = new Node<T>(item);
            newNode.Next = _head;
            _head = newNode;
            _count++;
        }

        public void AddLast(T item) // O(n) zonder tail pointer
        {
            var newNode = new Node<T>(item);
            if (_head == null) { _head = newNode; }
            else
            {
                var current = _head;
                while (current.Next != null) current = current.Next;
                current.Next = newNode;
            }
            _count++;
        }

        public bool Remove(T item) // O(n)
        {
            if (_head == null) return false;
            if (_head.Value.Equals(item))
            {
                _head = _head.Next;
                _count--;
                return true;
            }

            var current = _head;
            while (current.Next != null && !current.Next.Value.Equals(item))
                current = current.Next;

            if (current.Next != null)
            {
                current.Next = current.Next.Next;
                _count--;
                return true;
            }
            return false;
        }
    }
}
