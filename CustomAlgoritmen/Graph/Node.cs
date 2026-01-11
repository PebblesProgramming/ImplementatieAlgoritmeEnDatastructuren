using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAlgoritmen.Graph
{
    public class Node : IComparable<Node>
    {
        public string Name { get; set; }
        public List<Edge> Neighbors { get; set; } = new List<Edge>();
        public double Distance { get; set; } = double.MaxValue;

        public Node(string name) => Name = name;

        public int CompareTo(Node other) => Distance.CompareTo(other.Distance);
    }
}
