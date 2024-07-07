using UnityEngine;

namespace TDAs.Graphs
{
    public class DijkstraProgram : MonoBehaviour
    {
        /*private StaticGraph<int> graph = new StaticGraph<int>();
        private int size = 10;

        [SerializeField] private int[] vertex;
        [SerializeField] private int[] originEdges;
        [SerializeField] private int[] destinyEdges;
        [SerializeField] private int[] weightEdges;
        private void Start()
        {
            graph.Init(size);

            for (int i = 0; i < vertex.Length; i++)
            {
                graph.AddNode(vertex[i]);
            }

            for (int i = 0; i < weightEdges.Length; i++)
            {
                graph.AddEdge(originEdges[i],destinyEdges[i], weightEdges[i]);
            }
            
            Debug.Log("\n Listado de etiquetas de nodos");

            for (int i = 0; i < graph.Tags.Length; i++)
            {
                if (graph.Tags[i] != 0)
                {
                    Debug.Log("Nodo" + graph.Tags[i]);
                }
            }
            
            Debug.Log("\n Listado de etiquetas de nodos");

            for (int i = 0; i < graph.NodeCount; i++)
            {
                for (int j = 0; j < graph.NodeCount; j++)
                {
                    if (graph.Matrix[i, j] != 0)
                    {
                        int headNode = graph.Tags[i];

                        int lastNode = graph.Tags[j];
                        
                        Debug.Log(headNode + " , " + lastNode + " , " + graph.Matrix[i,j]);
                    }
                }
            }
            
            Debug.Log("Dijkstra Algorithm");

            DijkstraAlgorithm dijsktra = new DijkstraAlgorithm();
            dijsktra.Dijkstra<int>(graph,3);

            ShowDijkstraResults(dijsktra.distance, graph.NodeCount, graph.Tags, dijsktra.nodes);
        }

        private void ShowDijkstraResults(int[] distance, int vertexCount, int[] tags, string[] paths)
        {
            string distanceText = "";
            
            Debug.Log("Vertice    Distance   desde      origen     Nodos");

            for (int i = 0; i < vertexCount; i++)
            {
                if (distance[i] == int.MaxValue)
                {
                    distanceText = "---";
                }
                else
                {
                    distanceText = distance[i].ToString();
                }
                
                Debug.Log($"{tags[i]}\t   {distance}\t\t\t {paths}");
            }
        }*/
    }
}