using CustomAlgoritmen.PriorityQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAlgoritmen.Graph
{
    public class Dijkstra
    {
        public void Calculate(Graph graph, Node startNode)
        {
            // Reset alle afstanden in de graaf naar 'oneindig'
            foreach (var node in graph.AllNodes)
            {
                node.Distance = double.MaxValue;
            }

            startNode.Distance = 0;
            var pq = new PriorityQueue<Node>();
            pq.Enqueue(startNode);

            while (pq.Count > 0)
            {
                var current = pq.Dequeue();

                foreach (var edge in current.Neighbors)
                {
                    double newDist = current.Distance + edge.Weight;
                    if (newDist < edge.Target.Distance)
                    {
                        edge.Target.Distance = newDist;
                        pq.Enqueue(edge.Target);
                    }
                }
            }
        }
    }
}
