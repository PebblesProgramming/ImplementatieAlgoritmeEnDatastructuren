using System.Collections.Generic;
using CustomAlgoritmen;

namespace Implementation
{

    class Program
    {
        static void Main(string[] args)
        {
            var test = new Lijst1<int>();
            test.add(1);
            test.add(2);
            test.add(10);

            for (int i = 0; i < test.size(); i++)
            {
                Console.WriteLine(test.get(i));
            }

        }

    }
}