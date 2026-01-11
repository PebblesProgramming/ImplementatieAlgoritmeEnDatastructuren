using CustomAlgoritmen.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class DijkstraTests
    {
        [Fact]
        public void Dijkstra_ShouldFindShortestPath()
        {
            // 1. Setup Nodes
            var nodeA = new Node("A");
            var nodeB = new Node("B");
            var nodeC = new Node("C");
            var nodeD = new Node("D");

            // 2. Setup Edges (Gewichten)
            // Route A -> B -> D (Totaal gewicht: 10 + 10 = 20)
            nodeA.Neighbors.Add(new Edge { Target = nodeB, Weight = 10 });
            nodeB.Neighbors.Add(new Edge { Target = nodeD, Weight = 10 });

            // Route A -> C -> D (Totaal gewicht: 2 + 3 = 5)
            nodeA.Neighbors.Add(new Edge { Target = nodeC, Weight = 2 });
            nodeC.Neighbors.Add(new Edge { Target = nodeD, Weight = 3 });

            // 3. Run Algorithm
            var solver = new Dijkstra();
            solver.Calculate(nodeA);

            // 4. Assert
            // De afstand naar D moet 5 zijn (via C), niet 20 (via B)
            Assert.Equal(5, nodeD.Distance);
            Assert.Equal(0, nodeA.Distance);
        }
    }
}
