using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomAlgoritmen.Graph
{
    public class Graph
    {
        private readonly List<Node> _nodes = new List<Node>();

        public void AddNode(Node node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (!_nodes.Contains(node))
                _nodes.Add(node);
        }

        public void AddEdge(Node source, Node target, double weight)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (weight < 0) throw new ArgumentOutOfRangeException(nameof(weight), "Dijkstra vereist niet-negatieve gewichten.");

            // Zorg dat nodes in de graaf zitten
            AddNode(source);
            AddNode(target);

            source.Neighbors.Add(new Edge { Target = target, Weight = weight });
        }

        public Node GetNode(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return _nodes.FirstOrDefault(n => n.Name == name);
        }

        public IReadOnlyList<Node> AllNodes => _nodes;
    }
}

