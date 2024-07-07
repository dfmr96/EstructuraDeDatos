using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using TDAs;
using TDAs.Graphs;

public class GraphTest
{
    private DynamicGraph<string> graph;

        [SetUp]
        public void Setup()
        {
            graph = new DynamicGraph<string>();
            graph.AddNode("City A");
            graph.AddNode("City B");
            graph.AddNode("City C");
            graph.AddNode("City D");

            graph.AddEdge("City A", "City B", 10);
            graph.AddEdge("City A", "City C", 15);
            graph.AddEdge("City B", "City D", 12);
            graph.AddEdge("City C", "City D", 10);
        }

        [Test]
        public void TestShortestPathCityAToCityD()
        {
            List<GraphNode<string>> path = graph.FindShortestPath("City A", "City D");
            List<string> expectedPath = new List<string> { "City A", "City B", "City D" };

            Assert.IsNotNull(path, "Path should not be null");
            Assert.AreEqual(expectedPath.Count, path.Count, "Path length should be equal to expected path length");

            for (int i = 0; i < expectedPath.Count; i++)
            {
                Assert.AreEqual(expectedPath[i], path[i].value, $"Path at index {i} should be {expectedPath[i]}");
            }
        }

        [Test]
        public void TestShortestPathCityBToCityD()
        {
            List<GraphNode<string>> path = graph.FindShortestPath("City B", "City D");
            List<string> expectedPath = new List<string> { "City B", "City D" };

            Assert.IsNotNull(path, "Path should not be null");
            Assert.AreEqual(expectedPath.Count, path.Count, "Path length should be equal to expected path length");

            for (int i = 0; i < expectedPath.Count; i++)
            {
                Assert.AreEqual(expectedPath[i], path[i].value, $"Path at index {i} should be {expectedPath[i]}");
            }
        }

        [Test]
        public void TestNoPath()
        {
            graph.RemoveNode("City B");
            graph.RemoveNode("City C");

            List<GraphNode<string>> path = graph.FindShortestPath("City A", "City D");
            Assert.IsNull(path, "Path should be null");
        }
    }

