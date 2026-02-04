using System;
using System.Collections.Generic;
using CustomAlgoritmen.PriorityQueue;

namespace CustomAlgoritmen.Graph
{
    public class Dijkstra
    {
        public void Calculate(Graph graph, Node start)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            if (start == null) throw new ArgumentNullException(nameof(start));

            // Reset
            foreach (var node in graph.AllNodes)
            {
                node.Distance = double.MaxValue;
                node.Previous = null;
            }

            start.Distance = 0;

            // PriorityQueue met Node als element en double (Distance) als prioriteit
            var pq = new PriorityQueue<Node, double>();
            pq.Enqueue(start, start.Distance);

            var visited = new HashSet<Node>();

            while (pq.Count > 0)
            {
                var current = pq.Dequeue();

                // Als we hem al definitief hebben gehad: skip
                if (visited.Contains(current))
                    continue;

                visited.Add(current);

                foreach (var edge in current.Neighbors)
                {
                    if (edge.Weight < 0)
                        throw new InvalidOperationException("Dijkstra vereist niet-negatieve gewichten.");

                    var neighbor = edge.Target;
                    double newDist = current.Distance + edge.Weight;

                    if (newDist < neighbor.Distance)
                    {
                        neighbor.Distance = newDist;
                        neighbor.Previous = current;

                        // Geen decrease-key -> opnieuw in queue stoppen met nieuwe prioriteit
                        pq.Enqueue(neighbor, newDist);
                    }
                }
            }
        }
    }
}
