using System;
using System.Collections.Generic;

namespace TDAs.Graphs
{
    /*public class DijkstraAlgorithm
    {
        public int[] distance;
        public string[] nodes;

        private int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int vertexCount)
        {
            int min = int.MaxValue;
            int minIndex = 0;

            for (int i = 0; i < vertexCount; ++i)
            {
                if (shortestPathTreeSet[i] == false && distance[i] <= min)
                {
                    min = distance[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }

        public void Dijkstra<T>(StaticGraph<int> graph, int source)
        {
            int[,] newGraph = graph.Matrix;
            int vertexCount = graph.NodeCount;

            source = graph.Vertex2Index(source);

            distance = new int[vertexCount];

            bool[] shortestPathTreeSet = new bool[vertexCount];

            int[] nodes1 = new int[vertexCount];
            int[] nodes2 = new int[vertexCount];

            for (int i = 0; i < vertexCount; ++i)
            {
                distance[i] = int.MaxValue;

                shortestPathTreeSet[i] = false;

                nodes1[i] = nodes2[i] = -1;
            }

            distance[source] = 0;

            nodes1[source] = nodes2[source] = graph.Tags[source];

            for (int count = 0; count < vertexCount - 1; ++count)
            {
                int u = MinimumDistance(distance, shortestPathTreeSet, vertexCount);

                shortestPathTreeSet[u] = true;

                for (int v = 0; v < vertexCount; ++v)
                {
                    if (!shortestPathTreeSet[v] && Convert.ToBoolean(newGraph[u,v]) && distance[u] != int.MaxValue &&
                        distance[u] + newGraph[u, v] < distance[v])
                    {
                        distance[v] = distance[u] + newGraph[u, v];

                        nodes1[v] = graph.Tags[u];
                        nodes2[v] = graph.Tags[v];
                    }
                }
            }

            nodes = new string[vertexCount];
            int nodeOrigin = graph.Tags[source];

            for (int i = 0; i < vertexCount; i++)
            {
                List<int> l1 = new List<int>();
                l1.Add(nodes1[i]);
                l1.Add(nodes2[i]);

                while (l1[0] != nodeOrigin)
                {
                    for (int j = 0; j < vertexCount; j++)
                    {
                        if (j != source && l1[0] == nodes2[j])
                        {
                            l1.Insert(0,nodes1[j]);
                            break;
                        }
                    }
                }

                for (int j = 0; j < l1.Count; j++)
                {
                    if (j == 0)
                    {
                        nodes[i] = l1[j].ToString();
                    }
                    else
                    {
                        nodes[i] += "," + l1[j].ToString();
                    }
                }
            }
        }
    }*/
}