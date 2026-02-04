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

        // O(1) - alleen head pointer aanpassen, geen iteratie nodig
        public void AddFirst(T item)
        {
            var newNode = new Node<T>(item);
            newNode.Next = _head;
            _head = newNode;
            _count++;
        }

        // O(n) - moet door hele lijst itereren om einde te vinden
        // Optimalisatie mogelijk: tail pointer bijhouden → O(1)
        public void AddLast(T item)
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

        // O(n) - moet element zoeken door te itereren vanaf head
        public bool Remove(T item) 
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
