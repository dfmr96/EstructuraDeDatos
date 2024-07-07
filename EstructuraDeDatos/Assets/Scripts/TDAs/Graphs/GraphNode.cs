using System;

namespace TDAs.Graphs
{
    //[Serializable]
    public class GraphNode<T>
    {
        public T value;
        public GraphEdge<T> GraphEdge;
        public GraphNode<T> nextNode;

        public GraphNode(T value)
        {
            this.value = value;
            GraphEdge = null;
            nextNode = null;
        }
    }
}