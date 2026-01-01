using CustomAlgoritmen;
using CustomAlgoritmen.Lists;
using System.Collections.Generic;
using System.Diagnostics;

namespace Implementation
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ADP Algoritmen Performance Demo ===\n");

            int n = 50000;

            // Demo DynamicArray
            var da = new DynamicArray<int>();
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < n; i++) da.Insert(0, i);
            sw.Stop();
            Console.WriteLine($"DynamicArray Insert(0) x {n}: {sw.ElapsedMilliseconds}ms");

            // Demo LinkedList
            var ll = new SinglyLinkedList<int>();
            sw.Restart();
            for (int i = 0; i < n; i++) ll.AddFirst(i);
            sw.Stop();
            Console.WriteLine($"LinkedList AddFirst x {n}: {sw.ElapsedMilliseconds}ms");
        }

    }
}