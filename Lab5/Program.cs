namespace Lab5;

class Program
{
    static void Main(string[] args)
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph1.txt");

        List<Node> nodes = new List<Node>();

        undirectedGraph.DFS(undirectedGraph.Nodes[0]);
    }
}

