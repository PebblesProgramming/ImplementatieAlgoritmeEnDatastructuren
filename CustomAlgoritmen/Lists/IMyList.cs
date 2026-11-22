using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAlgoritmen.Lists
{
    public interface IMyList<T>
    {
        void Add(T element);
        void RemoveAt(int index);
        T Get(int index);
        //T this[int index] { get; set; } // linkedlist does not need random access
        void Set(int index, T element);
        int Count { get; }
        bool isEmpty();
    }
}
