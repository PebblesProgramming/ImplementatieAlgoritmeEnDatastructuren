using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CustomAlgoritmen.Lists
{
    public class CustomList <T> : IMyList<T> 
    {
        private T[] data;
        private int _defaultSize = 10;
        private int _count = 0;
        
        // Does a list sort the data automatically?
        // add(), get(), set(), find()
        // Arraylist, LinkedList, Doubly LinkedList, Stack   // check welke van deze allemaal valid zijn
        // List is an interface? what does that mean? Should it be an interface then?
        // Moet ik comparables toevoegen aan mijn generics? of heeft C# dat ingebouwd? Hebben we nodig voor sorting
        public CustomList()
        {
            this.data = new T[_defaultSize];
        }

        public int Count => _count;

        public void Add(T element)
        {
            throw new NotImplementedException();
        }

        public T Get(int index)
        {
            throw new NotImplementedException();
        }

        public T this[int i]
        {
            get => data[i];
            set => data[i] = value;
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Set(int index, T element)
        {
            throw new NotImplementedException();
        }

        public bool isEmpty()
        {
            if (_count == 0) return true;
            else
            {
                return false;
            }
        }
    }
}
