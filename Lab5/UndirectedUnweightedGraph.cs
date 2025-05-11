using System.Text.RegularExpressions;

namespace Lab5
{
    public class UndirectedUnweightedGraph
    {

        public List<Node> Nodes { get; set; }

        public UndirectedUnweightedGraph()
        {
            Nodes = new List<Node>();
        }

        public UndirectedUnweightedGraph(string path)
        {
            Nodes = new List<Node>();

            List<string> lines = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line == "")
                        {
                            continue;
                        }
                        if (line[0] == '#')
                        {
                            continue;
                        }

                        lines.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            // process the lines
            if (lines.Count < 1)
            {
                // empty file
                Console.WriteLine("Graph file was empty");
                return;
            }

            // Add nodes
            string[] nodeNames = Regex.Split(lines[0], @"\W+");

            foreach (var name in nodeNames)
            {
                Nodes.Add(new Node(name));
            }

            // Add edges
            for (int i = 1; i < lines.Count; i++)
            {
                // extract node names
                nodeNames = Regex.Split(lines[i], @"\W+");
                if (nodeNames.Length < 2)
                {
                    throw new Exception("Two nodes are required for each edge.");
                }

                // add edge between those nodes
                AddEdge(nodeNames[0], nodeNames[1]);
            }
        }

        public void AddEdge(string node1Name, string node2Name)
        {
            Node node1 = GetNodeByName(node1Name);
            Node node2 = GetNodeByName(node2Name);

            if (node1 == null || node2 == null)
            {
                throw new Exception("Invalid node name");
            }

            node1.Neighbors.Add( new Neighbor() { Node=node2, Weight=1 } );
            node2.Neighbors.Add( new Neighbor() { Node=node1, Weight=1 } );
        }

        private Node GetNodeByName(string nodeName)
        {
            var node = Nodes.Find(node => node.Name == nodeName);

            return node;
        }

        public int ConnectedComponents
        {
           
            get 
            {
                 Dictionary<Node, Node> pred = new Dictionary<Node, Node>();
                int CCcount = 0;

                foreach (var node in Nodes)
                {
                    if (node.Color==Color.White)
                    {
                        DFSVisit(node, pred);
                        CCcount++;
                    }
                }
                return CCcount;
            }
        }


        public bool IsReachable(string node1name, string node2name)
        {
            Node node1 = GetNodeByName(node1name);
            Node node2 = GetNodeByName(node2name);

            if (node1 == null || node2 == null)
            {
                throw new Exception($"{node1name} or {node2name} does not exist.)");
            }

            // Do a DFS
            var pred = DFS(node1);

            // Was a pred for node2 found?
            return pred[node2] != null;
        }


        /// <summary>
        /// Searches the graph in a depth-first manner, creating a
        /// dictionary of the Node to Predessor Node links discovered by starting at the given node.
        /// Neighbors are visited in alphabetical order. 
        /// </summary>
        /// <param name="startingNode">The starting node of the depth first search</param>
        /// <returns>A dictionary of the Node to Predecessor Node 
        /// for each node in the graph reachable from the starting node
        /// as discovered by a DFS.</returns>
        public Dictionary<Node, Node> DFS(Node startingNode, bool reset = true)
        {
            Dictionary<Node, Node> pred = new Dictionary<Node, Node>();

            if (reset)
            {
                // setup DFS
                foreach (Node node in Nodes)
                {
                    pred[node] = null;
                    node.Color = Color.White;
                }
            }

            // call the recursive method
            DFSVisit(startingNode, pred);

            return pred;
        }

        private void DFSVisit(Node node, Dictionary<Node, Node> pred)
        {
            // color node gray
            node.Color = Color.Gray;

            // sort the neighbors so that we visit them in alpha order
            node.Neighbors.Sort();

            // visit every neighbor 
            foreach (var neighbor in node.Neighbors)
            {
                if (neighbor.Node.Color == Color.White)
                {
                    pred[neighbor.Node] = node;
                    DFSVisit(neighbor.Node, pred);
                }
            }

            // color the node black
            node.Color = Color.Black;
        }

        // TODO
        /// <summary>
        /// Searches the graph in a breadth-first manner, creating a
        /// dictionary of the Node to Predecessor and Distance discovered by starting at the given node.
        /// Neighbors are visited in alphabetical order. 
        /// </summary>
        /// <param name="startingNode"></param>
        /// <returns>A dictionary of the Node to Predecessor Node and Distance 
        /// for each node in the graph reachable from the starting node
        /// as discovered by a BFS.</returns>
        public Dictionary<Node, (Node pred, int dist)> BFS(Node startingNode)
        {
            var resultsDictionary = new Dictionary<Node, (Node pred, int dist)>();

            // initialize the dictionary 

            foreach (var node in Nodes)
            {
                node.Color = Color.White;
                resultsDictionary[node] = (null, int.MaxValue);
            }

            // setting up the starting node
            startingNode.Color = Color.Gray;
            resultsDictionary[startingNode] = (null, 0);

            // create a queue
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(startingNode);

            // iteratively process the graph (neighbors of the nodes in the queue)
            while (queue.Count > 0)
            {

                // get the front of queue 
                var node = queue.Peek();

                foreach (var neighbor in node.Neighbors)
                {
                    int distance = resultsDictionary[node].dist;
                    resultsDictionary[neighbor.Node] = (node, distance + 1);
                    queue.Enqueue(neighbor.Node);
                }

                queue.Dequeue();
                node.Color = Color.Black;

            }



            return resultsDictionary;
        }

    }
}

