using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAlgoritmen.Graph
{
    public class Graph
    {
        // Een lijst van alle nodes in de graaf
        private readonly List<Node> _nodes = new List<Node>();

        public void AddNode(Node node)
        {
            if (!_nodes.Contains(node))
            {
                _nodes.Add(node);
            }
        }

        public void AddEdge(Node source, Node target, double weight)
        {
            // Voeg een gerichte verbinding toe
            source.Neighbors.Add(new Edge { Target = target, Weight = weight });

            // Optioneel: voor een ongerichte graaf voeg je ook de omgekeerde verbinding toe:
            // target.Neighbors.Add(new Edge { Target = source, Weight = weight });
        }

        public Node GetNode(string name)
        {
            return _nodes.FirstOrDefault(n => n.Name == name);
        }

        public List<Node> AllNodes => _nodes;
    }
}
