using System;

namespace TDAs.Graphs
{
    //[Serializable]
    public class GraphEdge<T>
    {
        public int weight;
        public GraphNode<T> destiny;
        public GraphEdge<T> next;

        public GraphEdge(int weight, GraphNode<T> destiny)
        {
            this.weight = weight;
            this.destiny = destiny;
            next = null;
        }
    }
}