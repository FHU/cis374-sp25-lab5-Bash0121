using Lab5;

namespace UnitTests;

[TestClass]
public class UnitTests
{
    [TestMethod]
    public void Graph1IsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph1.txt");

        Assert.IsTrue(undirectedGraph.IsReachable("a", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("e", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("d", "e"));
        Assert.IsTrue(undirectedGraph.IsReachable("d", "c"));
    }

    [TestMethod]
    public void Graph1ConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph1.txt");

        Assert.AreEqual(1, undirectedGraph.ConnectedComponents);
    }


    [TestMethod]
    public void Graph2IsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph2.txt");

        Assert.IsFalse(undirectedGraph.IsReachable("a", "c"));
        Assert.IsFalse(undirectedGraph.IsReachable("e", "c"));
        Assert.IsFalse(undirectedGraph.IsReachable("d", "e"));
        Assert.IsFalse(undirectedGraph.IsReachable("b", "a"));
        Assert.IsFalse(undirectedGraph.IsReachable("d", "b"));

    }

    [TestMethod]
    public void Graph2ConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph2.txt");

        Assert.AreEqual(5, undirectedGraph.ConnectedComponents);
    }


    [TestMethod]
    public void Graph3IsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph3.txt");

        Assert.IsTrue(undirectedGraph.IsReachable("a", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("e", "d"));
        Assert.IsTrue(undirectedGraph.IsReachable("h", "g"));

        Assert.IsFalse(undirectedGraph.IsReachable("a", "h"));
        Assert.IsFalse(undirectedGraph.IsReachable("c", "i"));
        Assert.IsFalse(undirectedGraph.IsReachable("g", "b"));

    }

    [TestMethod]
    public void Graph3ConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph3.txt");

        Assert.AreEqual(3, undirectedGraph.ConnectedComponents);
    }

    [TestMethod]
    public void Graph4IsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph4.txt");

        Assert.IsTrue(undirectedGraph.IsReachable("a", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("e", "i"));
        Assert.IsTrue(undirectedGraph.IsReachable("g", "b"));
        Assert.IsTrue(undirectedGraph.IsReachable("c", "f"));
        Assert.IsTrue(undirectedGraph.IsReachable("a", "d"));
        Assert.IsTrue(undirectedGraph.IsReachable("b", "i"));

    }

    [TestMethod]
    public void Graph4ConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph4.txt");

        Assert.AreEqual(1, undirectedGraph.ConnectedComponents);
    }

    [TestMethod]
    public void SavannahIsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/Savannah.txt");

        Assert.IsTrue(undirectedGraph.IsReachable("a", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("e", "i"));
        Assert.IsTrue(undirectedGraph.IsReachable("g", "b"));
        Assert.IsTrue(undirectedGraph.IsReachable("c", "f"));
        Assert.IsTrue(undirectedGraph.IsReachable("a", "j"));
        Assert.IsTrue(undirectedGraph.IsReachable("b", "i"));


        Assert.IsFalse(undirectedGraph.IsReachable("a", "d"));
        Assert.IsFalse(undirectedGraph.IsReachable("d", "j"));

    }

    [TestMethod]
    public void SavannahConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/Savannah.txt");

        Assert.AreEqual(2, undirectedGraph.ConnectedComponents);
    }

    [TestMethod]
    public void TestDFSPathBetween()
    {
        UndirectedWeightedGraph weightedGraph = new UndirectedWeightedGraph("../../../graphs/graph1-weighted.txt");
        List<Node> pathList;
        int cost1 = weightedGraph.DFSPathBetween("a", "c", out pathList);
        Assert.IsTrue(cost1 > 0);
        Assert.AreEqual("a", pathList.First().Name);
        Assert.AreEqual("c", pathList.Last().Name);
        int cost2 = weightedGraph.DFSPathBetween("b", "e", out pathList);
        Assert.IsTrue(cost2 > 0);
        Assert.AreEqual("b", pathList.First().Name);
        Assert.AreEqual("e", pathList.Last().Name);
    }

    [TestMethod]
    public void TestBFSPathBetween()
    {
        UndirectedWeightedGraph weightedGraph = new UndirectedWeightedGraph("../../../graphs/graph1-weighted.txt");
        List<Node> pathList;
        int cost1 = weightedGraph.BFSPathBetween("a", "c", out pathList);
        Assert.IsTrue(cost1 > 0);
        Assert.AreEqual("a", pathList.First().Name);
        Assert.AreEqual("c", pathList.Last().Name);
        int cost2 = weightedGraph.BFSPathBetween("b", "e", out pathList);
        Assert.IsTrue(cost2 > 0);
        Assert.AreEqual("b", pathList.First().Name);
        Assert.AreEqual("e", pathList.Last().Name);
    }

    [TestMethod]
    public void TestDijkstraAlgorithm()
    {
        UndirectedWeightedGraph weightedGraph = new UndirectedWeightedGraph("../../../graphs/graph1-weighted.txt");
        var startNode = weightedGraph.Nodes.FirstOrDefault(n => n.Name == "a");
        if (startNode == null)
        {
            throw new Exception();
        }
        var dijkstraResults = weightedGraph.Dijkstra(startNode);
        Assert.AreEqual(weightedGraph.Nodes.Count, dijkstraResults.Count);
        Assert.AreEqual(0, dijkstraResults[startNode].cost);
        Assert.IsNull(dijkstraResults[startNode].pred);
        foreach (var node in weightedGraph.Nodes)
        {
            var (pred, cost) = dijkstraResults[node];
            if (node == startNode) continue;
            Assert.IsTrue(cost >= 0);
            if (cost == int.MaxValue)
            {
                Assert.IsNull(pred);
            }
            else
            {
                Assert.IsNotNull(pred);
                Assert.IsTrue(weightedGraph.Nodes.Contains(pred));
            }
        }
    }

    [TestMethod]
    public void TestDijkstraPathBetween()
    {
        UndirectedWeightedGraph weightedGraph = new UndirectedWeightedGraph("../../../graphs/graph1-weighted.txt");
        List<Node> pathList;
        int cost1 = weightedGraph.DijkstraPathBetween("a", "c", out pathList);
        Assert.IsTrue(cost1 > 0);
        Assert.AreEqual("a", pathList.First().Name);
        Assert.AreEqual("c", pathList.Last().Name);
        int cost2 = weightedGraph.DijkstraPathBetween("b", "e", out pathList);
        Assert.IsTrue(cost2 > 0);
        Assert.AreEqual("b", pathList.First().Name);
        Assert.AreEqual("e", pathList.Last().Name);
    }

}