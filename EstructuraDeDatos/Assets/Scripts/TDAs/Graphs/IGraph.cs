using TDAs.Set;

namespace TDAs.Graphs
{
    public interface IGraph<T>
    {
        void Init(int size);
        void AddNode(int node);
        void AddNode(T value);
        void RemoveNode(T value);
        StaticSetTDA<T> Vertex();
        void AddEdge(T from, T to, int weight);
        void RemoveEdge(T from, T to);
        bool CheckEdge(T from, T to);
        int EdgeWeight(T from, T to);

    }
}