using System;
using System.Collections.Generic;

namespace CustomAlgoritmen.Graph
{
    public class Node : IComparable<Node>
    {
        public string Name { get; set; }
        public List<Edge> Neighbors { get; set; } = new List<Edge>();

        public double Distance { get; set; } = double.MaxValue;
        public Node Previous { get; set; } = null; 

        public Node(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public int CompareTo(Node other)
        {
            if (other == null) return 1;
            return Distance.CompareTo(other.Distance);
        }
    }
}
