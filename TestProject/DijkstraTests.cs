using CustomAlgoritmen.Graph;
using Xunit;

namespace TestProject
{
    public class DijkstraTests
    {
        [Fact]
        public void Dijkstra_ShouldFindShortestPath()
        {
            var nodeA = new Node("A");
            var nodeB = new Node("B");
            var nodeC = new Node("C");
            var nodeD = new Node("D");

            var graph = new Graph();
            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            graph.AddNode(nodeC);
            graph.AddNode(nodeD);

            graph.AddEdge(nodeA, nodeB, 10);
            graph.AddEdge(nodeB, nodeD, 10);

            graph.AddEdge(nodeA, nodeC, 2);
            graph.AddEdge(nodeC, nodeD, 3);

            var solver = new Dijkstra();
            solver.Calculate(graph, nodeA);

            Assert.Equal(0, nodeA.Distance);
            Assert.Equal(10, nodeB.Distance);
            Assert.Equal(2, nodeC.Distance);
            Assert.Equal(5, nodeD.Distance);
        }

        [Fact]
        public void Dijkstra_ShouldThrow_OnNegativeWeight()
        {
            var a = new Node("A");
            var b = new Node("B");

            var graph = new Graph();
            graph.AddNode(a);
            graph.AddNode(b);

            // Negatieve edge is niet toegestaan voor Dijkstra
            Assert.Throws<ArgumentOutOfRangeException>(() => graph.AddEdge(a, b, -1));
        }
    }
}
