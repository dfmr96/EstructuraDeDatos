using TDAs.Set;

namespace TDAs.Graphs
{
    public class StaticGraph<T> : IGraph<T>
    {
        public int[,] Matrix { get; private set; }
        public int[] Tags { get; private set; }
        public int NodeCount { get; private set; }

        public void Init(int size)
        {
            Matrix = new int[size, size];
            Tags = new int[size];
            NodeCount = 0;
        }

        public void AddNode(int node)
        {
            Tags[NodeCount] = node;
            for (int i = 0; i < NodeCount; i++)
            {
                Matrix[NodeCount, i] = 0;
                Matrix[i, NodeCount] = 0;
            }

            NodeCount++;
        }

        public void AddNode(T value)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveNode(T value)
        {
            int index = Vertex2Index(value);

            for (int i = 0; i < NodeCount; i++)
            {
                Matrix[i, index] = Matrix[i, NodeCount - 1];
            }
            
            for (int i = 0; i < NodeCount; i++)
            {
                Matrix[index, i] = Matrix[NodeCount - 1, i];
            }

            Tags[index] = Tags[NodeCount - 1];
            NodeCount--;
        }


        public int Vertex2Index(T node)
        {
            for (int i = 0; i < NodeCount; i++)
            {
                if (Tags[i].Equals(node))
                {
                    return i;
                }
            }

            return 0;
        }

        public StaticSetTDA<T> Vertex()
        {
            StaticSetTDA<T> edges = new StaticSetTDA<T>();
            edges.Init(NodeCount);

            for (int i = 0; i < NodeCount; i++)
            {
                edges.Add(Tags[i]);
            }

            return edges;
        }

        public void AddEdge(T from, T to, int weight)
        {
            int node1Index = Vertex2Index(from);
            int node2Index = Vertex2Index(to);

            Matrix[node1Index, node2Index] = weight;
        }

        public void RemoveEdge(T from, T to)
        {
            int node1Index = Vertex2Index(from);
            int node2Index = Vertex2Index(to);

            Matrix[node1Index, node2Index] = 0;
        }

        public bool CheckEdge(T from, T to)
        {
            int node1Index = Vertex2Index(from);
            int node2Index = Vertex2Index(to);

            return Matrix[node1Index, node2Index] != 0;
        }

        public int EdgeWeight(T from, T to)
        {
            int node1Index = Vertex2Index(from);
            int node2Index = Vertex2Index(to);

            return Matrix[node1Index, node2Index];
        }
    }
}