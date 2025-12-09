using System.Collections.Generic;
using CustomAlgoritmen;
using CustomAlgoritmen.Lists;

namespace Implementation
{

    class Program
    {
        static void Main(string[] args)
        {
            var normalList = new List<int>();
            normalList.Add(10);
            normalList.Add(20);
            normalList.Add(30);

            Console.WriteLine($"found: {normalList.Find(x => x == 10)}");
            Console.WriteLine($"found: {normalList.Find(x => x == 11)}");

            for(int i = 0; i < normalList.Count(); i++)
            {
                Console.WriteLine(normalList[i]);
            }
                
            var test = new CustomList<int>();
            test.Add(1);
            test.Add(2);
            test.Add(10);


            for (int i = 0; i < test.Count; i++)
            {
                Console.WriteLine(test[i]);
            }


            var test2 = new CustomLinkedList<int>();
        }

    }
}