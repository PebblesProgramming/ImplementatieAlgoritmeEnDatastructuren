using CustomAlgoritmen;
using CustomAlgoritmen.Graph;
using CustomAlgoritmen.HashTable;
using CustomAlgoritmen.Lists;
using CustomAlgoritmen.Searching;
using CustomAlgoritmen.Sorting;
using System.Collections.Generic;
using System.Diagnostics;

namespace Implementation
{

        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("================================================");
                Console.WriteLine("   ADP Algoritmen & Datastructuren Portfolio    ");
                Console.WriteLine("================================================\n");

                RunLijstenDemo();
                RunSortingDemo();
                RunSearchingDemo();
                RunHashTableDemo();
                RunDijkstraDemo();

                Console.WriteLine("\nAlle demonstraties voltooid. Zie Unit Tests voor correctheidsbewijzen.");
                Console.ReadLine();
            }

            static void RunLijstenDemo()
            {
                Console.WriteLine("--- 1. LIJSTEN & PERFORMANCE ---");
                int n = 20000;

                // DynamicArray
                var da = new DynamicArray<int>();
                var sw = Stopwatch.StartNew();
                for (int i = 0; i < n; i++) da.Insert(0, i); // O(n) per insert
                sw.Stop();
                Console.WriteLine($"DynamicArray: Insert(0) {n}x duurde: {sw.ElapsedMilliseconds}ms (O(n^2))");

                // SinglyLinkedList
                var ll = new SinglyLinkedList<int>();
                sw.Restart();
                for (int i = 0; i < n; i++) ll.AddFirst(i); // O(1) per insert
                sw.Stop();
                Console.WriteLine($"LinkedList: AddFirst {n}x duurde: {sw.ElapsedMilliseconds}ms (O(n))\n");
            }

            static void RunSortingDemo()
            {
                Console.WriteLine("--- 2. SORTEERALGORITMES ---");
                int n = 10000;
                Random rnd = new Random();
                int[] data = new int[n];
                for (int i = 0; i < n; i++) data[i] = rnd.Next(0, n);

                int[] mergeData = (int[])data.Clone();
                int[] insertionData = (int[])data.Clone();

                var sw = Stopwatch.StartNew();
                MergeSorter<int>.Sort(mergeData);
                sw.Stop();
                Console.WriteLine($"MergeSort (O(n log n)): {sw.ElapsedMilliseconds}ms");

                sw.Restart();
                InsertionSorter<int>.Sort(insertionData);
                sw.Stop();
                Console.WriteLine($"InsertionSort (O(n^2)): {sw.ElapsedMilliseconds}ms\n");
            }

            static void RunSearchingDemo()
            {
                Console.WriteLine("--- 3. BINARY SEARCH ---");
                int n = 1000000;
                int[] sortedData = new int[n];
                for (int i = 0; i < n; i++) sortedData[i] = i;

                var sw = Stopwatch.StartNew();
                int index = BinarySearch<int>.Search(sortedData, n - 1);
                sw.Stop();
                Console.WriteLine($"Binary Search in {n} items: {sw.ElapsedTicks} ticks (Gevonden op index: {index})\n");
            }

            static void RunHashTableDemo()
            {
                Console.WriteLine("--- 4. HASH TABLE (CHAINING) ---");
                var table = new HashTable<string, string>(100);
                table.Put("StudentNummer", "S123456");
                table.Put("Naam", "Jouw Naam");

                Console.WriteLine($"Opgehaald uit Hash Table: {table.Get("Naam")} ({table.Get("StudentNummer")})\n");
            }

            static void RunDijkstraDemo()
            {
                Console.WriteLine("--- 5. DIJKSTRA & GRAPH ---");
                var graph = new Graph();
                var a = new Node("A");
                var b = new Node("B");
                var c = new Node("C");
                var d = new Node("D");

                graph.AddNode(a); graph.AddNode(b); graph.AddNode(c); graph.AddNode(d);
                graph.AddEdge(a, b, 10);
                graph.AddEdge(b, d, 10);
                graph.AddEdge(a, c, 2);
                graph.AddEdge(c, d, 3);

                var solver = new Dijkstra();
                solver.Calculate(graph, a);

                Console.WriteLine($"Kortste pad van A naar D: {d.Distance} (via C is 2+3=5)");
            }
        }
}