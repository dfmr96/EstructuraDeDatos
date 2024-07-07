using System;
using System.Collections.Generic;
using System.Linq;
using TDAs.Set;

namespace TDAs.Graphs
{
    //[Serializable]
    public class DynamicGraph<T> : IGraph<T>
    {
        private GraphNode<T> head;
        private int vertexCount;
        
        private List<GraphNode<T>> nodes = new List<GraphNode<T>>();

        public IEnumerable<GraphNode<T>> Nodes
        {
            get { return nodes; }
        }
        
        public void Init(int size)
        {
            head = null;
            vertexCount = 0;
        }

        public void AddNode(int node)
        {
            throw new System.NotImplementedException();
        }

        public void AddNode(T value)
        {
            GraphNode<T> newNode = new GraphNode<T>(value);
            newNode.nextNode = head;
            head = newNode;
            vertexCount++;
            nodes.Add(newNode);
        }
        public void RemoveNode(T value)
        {
            if (head != null && head.value.Equals(value))
            {
                head = head.nextNode;
                vertexCount--;
            }
            else
            {
                GraphNode<T> current = head;

                while (current != null && current.nextNode != null)
                {
                    if (current.nextNode.value.Equals(value))
                    {
                        current.nextNode = current.nextNode.nextNode;
                        vertexCount--;
                        break;
                    }
                    current = current.nextNode;
                }
            }

            GraphNode<T> node = head;
            while (node != null)
            {
                DeleteEdgeNode(node,value);
                node = node.nextNode;
            }
        }

        private void DeleteEdgeNode(GraphNode<T> node, T value)
        {
            GraphEdge<T> currentEdge = node.GraphEdge;
            if (currentEdge != null && currentEdge.destiny.value.Equals(value))
            {
                node.GraphEdge = currentEdge.next;
            }
            else
            {
                while (currentEdge != null && currentEdge.next != null)
                {
                    if (currentEdge.next.destiny.value.Equals(value))
                    {
                        currentEdge.next = currentEdge.next.next;
                        break;
                    }

                    currentEdge = currentEdge.next;
                }
            }
        }


        public StaticSetTDA<T> Vertex()
        {
            StaticSetTDA<T> newSet = new StaticSetTDA<T>();
            newSet.Init(vertexCount);
            GraphNode<T> current = head;

            while (current != null)
            {
                newSet.Add(current.value);
                current = current.nextNode;
            }

            return newSet;
        }

        public void AddEdge(T from, T to, int weight)
        {
            GraphNode<T> fromNode = FindNode(from);
            GraphNode<T> toNode = FindNode(to);

            if (fromNode != null && toNode != null)
            {
                GraphEdge<T> newEdge = new GraphEdge<T>(weight, toNode);
                newEdge.next = fromNode.GraphEdge;
                fromNode.GraphEdge = newEdge;
            }
        }


        public void RemoveEdge(T from, T to)
        {
            GraphNode<T> fromNode = FindNode(from);

            if (fromNode != null)
            {
                DeleteEdgeNode(fromNode,to);
            }
        }

        public bool CheckEdge(T from, T to)
        {
            GraphNode<T> fromNode = FindNode(from);
            if (fromNode != null)
            {
                GraphEdge<T> currentEdge = fromNode.GraphEdge;
                while (currentEdge != null)
                {
                    if (currentEdge.destiny.value.Equals(to))
                    {
                        return true;
                    }
                    currentEdge = currentEdge.next;
                }
            }
            return false;
        }

        public int EdgeWeight(T from, T to)
        {
            GraphNode<T> fromNode = FindNode(from);

            if (fromNode != null)
            {
                GraphEdge<T> currentEdge = fromNode.GraphEdge;

                while (currentEdge != null)
                {
                    if (currentEdge.destiny.value.Equals(to))
                    {
                        return currentEdge.weight;
                    }

                    currentEdge = currentEdge.next;
                }
            }

            return int.MaxValue;
        }
        private GraphNode<T> FindNode(T value)
        {
            GraphNode<T> current = head;
            while (current != null)
            {
                if (current.value.Equals(value))
                {
                    return current;
                }
                current = current.nextNode;
            }
            return null;
        }

        public List<GraphNode<T>> FindShortestPath(T startValue, T endValue)
        {
            var startNode = FindNode(startValue);
            var endNode = FindNode(endValue);

            if (startNode == null || endNode == null)
            {
                return null;
            }

            var distances = new Dictionary<GraphNode<T>, int>();
            var previousNodes = new Dictionary<GraphNode<T>, GraphNode<T>>();
            var unvisitedNodes = new List<GraphNode<T>>();

            GraphNode<T> current = head;

            while (current != null)
            {
                if (current.Equals(startNode))
                {
                    distances[current] = 0;
                }
                else
                {
                    distances[current] = int.MaxValue;
                }
                unvisitedNodes.Add(current);
                current = current.nextNode;
            }

            while (unvisitedNodes.Count > 0)
            {
                var currentNode = unvisitedNodes.OrderBy(node => distances[node]).First();
                unvisitedNodes.Remove(currentNode);

                if (currentNode == endNode)
                {
                    var path = new List<GraphNode<T>>();
                    while (previousNodes.ContainsKey(currentNode))
                    {
                        path.Add(currentNode);
                        currentNode = previousNodes[currentNode];
                    }
                    path.Add(startNode);
                    path.Reverse();
                    return path;
                }

                var edge = currentNode.GraphEdge;
                while (edge != null)
                {
                    var neighbor = edge.destiny;
                    if (unvisitedNodes.Contains(neighbor))
                    {
                        var newDist = distances[currentNode] + edge.weight;
                        if (newDist < distances[neighbor])
                        {
                            distances[neighbor] = newDist;
                            previousNodes[neighbor] = currentNode;
                        }
                    }
                    edge = edge.next;
                }
            }
            return null; // No path found
        }
    }
}
