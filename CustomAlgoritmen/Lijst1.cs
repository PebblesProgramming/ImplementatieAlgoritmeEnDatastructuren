using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAlgoritmen
{
    public class Lijst1 <T> 
    {
        private T[] data;
        private int _defaultSize = 10;
        private int _count = 0;
        
        // Does a list sort the data automatically?
        // add(), get(), set(), find()
        // Arraylist, LinkedList, Doubly LinkedList, Stack   // check welke van deze allemaal valid zijn
        // List is an interface? what does that mean? Should it be an interface then?
        // Moet ik comparables toevoegen aan mijn generics? of heeft C# dat ingebouwd? Hebben we nodig voor sorting
        public Lijst1()
        {
            this.data = new T[_defaultSize];
        }
        public void add(T element)
        {
            if(_count == _defaultSize)
            {
                resize();
            }
            data[_count++] = element;
        }

        //public T find(T input, T element)
        //{
            
        //    for(int i = 0; i < _count; i++)
        //    {
        //        if(input === element)
        //        {
        //            return data[i];
        //        }
        //    }
        //    return Console.WriteLine(result);
        //}

        private void resize()
        {
            // make a new array that replaces the current one
            T[] temp = new T[_defaultSize * 2];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = data[i];
            }
            data = temp; 
        }

        public T get(int index)
        {
            return data[index];
        }

        public T set(int index)
        {
            return data[index];
        }

        public int size()
        {
            return _count;
        }
    }
}
