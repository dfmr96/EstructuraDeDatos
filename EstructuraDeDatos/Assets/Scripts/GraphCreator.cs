using System;
using TDAs;
using TDAs.Graphs;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class GraphCreator: MonoBehaviour
    {
        private StaticGraph<int> _newStaticGraph = new();
        [SerializeField] private int graphSize;
        [SerializeField] private int[] nodes;
        [SerializeField] private int[] edgesOrigin;
        [SerializeField] private int[] edgesDest;
        [SerializeField] private int[] edgesWeight;
        private void Start()
        {
            _newStaticGraph.Init(graphSize);

            for (int i = 0; i < nodes.Length; i++)
            {
                _newStaticGraph.AddNode(nodes[i]);
            }

            for (int i = 0; i < _newStaticGraph.Tags.Length; i++)
            {
                _newStaticGraph.AddEdge(edgesOrigin[i], edgesDest[i], edgesWeight[i]);
            }
            Debug.Log("\n Listado de etiquetas de nodos");

            for (int i = 0; i < _newStaticGraph.NodeCount; i++)
            {
                if (_newStaticGraph.Tags[i] != 0)
                {
                    Debug.Log("Nodo:" + _newStaticGraph.Tags[i]);
                }
            }
            
            Debug.Log("\n Listado de Aristas (inicio,fin,peso");
            
            for (int i = 0; i < _newStaticGraph.NodeCount; i++)
            {
                for (int j = 0; j < _newStaticGraph.NodeCount; j++)
                {
                    if (_newStaticGraph.Matrix[i, j] != 0)
                    {
                        // obtengo la etiqueta del nodo origen, que está en las filas (i)
                        int nodoIni = _newStaticGraph.Tags[i];
                        // obtengo la etiqueta del nodo destino, que está en las columnas (j)
                        int nodoFin = _newStaticGraph.Tags[j];
                        Debug.Log(nodoIni + ", " + nodoFin + ", " + _newStaticGraph.Matrix[i, j]);
                    }
                }
            }
        }
    }
}