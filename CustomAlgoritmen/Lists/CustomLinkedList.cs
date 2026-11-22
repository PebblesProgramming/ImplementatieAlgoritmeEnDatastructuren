using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAlgoritmen
{
    public class CustomLinkedList <T>
    {
        public T[] data;
        private int _defaultSize = 0;

        public CustomLinkedList()
        {
            this.data = new T[_defaultSize];
        }
    }
}
