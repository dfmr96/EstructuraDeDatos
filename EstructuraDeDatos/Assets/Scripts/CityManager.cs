using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using TDAs;
using TDAs.Graphs;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class CityManager : MonoBehaviour
    {
        public static CityManager instance; // Singleton para acceder desde otros scripts
        public List<Transform> cityPositions;
        public GameObject cityPrefab;
        public GameObject linePrefab;
        public GameObject distanceLabelPrefab;
        public DynamicGraph<City> cityGraph;
        public MovableUnit unit;
        public LineRenderer fastestPathRenderer;
        public TMP_Text timeRemainingText;
        public GameObject unitPrefab;

        public UnitData infantryData;
        public UnitData tankData;
        public UnitData artilleryData;

        public GameObject unitListContent;

        public UnitScrollList UnitScrollList;
        
        public Dictionary<int, List<GraphNode<City>>[]> wavePathDic = new Dictionary<int, List<GraphNode<City>>[]>();
        
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GameObject newUnit = Instantiate(unitPrefab, cityPositions[0].position, Quaternion.identity);
                newUnit.GetComponent<MovableUnit>().InitUnit(infantryData, unitListContent,UnitScrollList);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                GameObject newUnit = Instantiate(unitPrefab, cityPositions[0].position, Quaternion.identity);
                newUnit.GetComponent<MovableUnit>().InitUnit(tankData, unitListContent,UnitScrollList);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                GameObject newUnit = Instantiate(unitPrefab, cityPositions[0].position, Quaternion.identity);
                newUnit.GetComponent<MovableUnit>().InitUnit(artilleryData, unitListContent, UnitScrollList);
            }
        }

        private void Start()
        {
            Time.timeScale = 1;
            
            cityGraph = new DynamicGraph<City>();

            City cityA = CreateCity("City A", cityPositions[0].position);
            City cityB = CreateCity("City B", cityPositions[1].position);
            City cityC = CreateCity("City C", cityPositions[2].position);
            City enemyPoint1 = CreateCity("EP1", cityPositions[3].position);
            City enemyPoint2 = CreateCity("EP2", cityPositions[4].position);
            City enemyPoint3 = CreateCity("EP3", cityPositions[5].position);
            City enemyPoint4 = CreateCity("EP4", cityPositions[6].position);
            City cityD = CreateCity("City D", cityPositions[7].position);
            City cityE = CreateCity("City E", cityPositions[8].position);
            City cityF = CreateCity("City F", cityPositions[9].position);
            City cityG = CreateCity("City G", cityPositions[10].position);
            City cityH = CreateCity("City H", cityPositions[11].position);
            City cityI = CreateCity("City I", cityPositions[12].position);
            
            cityGraph.AddNode(cityA);
            cityGraph.AddNode(cityB);
            cityGraph.AddNode(cityC);
            cityGraph.AddNode(enemyPoint1);
            cityGraph.AddNode(enemyPoint2);
            cityGraph.AddNode(enemyPoint3);
            cityGraph.AddNode(enemyPoint4);
            cityGraph.AddNode(cityD);
            cityGraph.AddNode(cityE);
            cityGraph.AddNode(cityF);
            cityGraph.AddNode(cityG);
            cityGraph.AddNode(cityH);
            cityGraph.AddNode(cityI);
            
            
            AddEdge(cityA, cityB, GetCityDistance(cityA, cityB));
            AddEdge(cityA, cityC, GetCityDistance(cityA,cityC));
            AddEdge(cityA, enemyPoint2, GetCityDistance(cityA,enemyPoint2));
            AddEdge(cityA, cityE, GetCityDistance(cityA,cityE));
            AddEdge(cityA, enemyPoint3, GetCityDistance(cityA,enemyPoint3));
            
            
            AddEdge(cityB, enemyPoint1, GetCityDistance(cityB,enemyPoint1));
            AddEdge(cityB, cityG, GetCityDistance(cityB,cityG));
            
            AddEdge(cityC, enemyPoint4, GetCityDistance(cityC,enemyPoint4));
            AddEdge(cityC, cityH, GetCityDistance(cityC,cityH));
            
            AddEdge(cityD, cityG, GetCityDistance(cityD,cityG));
            AddEdge(cityD, cityE, GetCityDistance(cityD,cityE));
            AddEdge(cityD, cityI, GetCityDistance(cityD,cityI));
            
            AddEdge(cityE, cityI, GetCityDistance(cityE,cityI));
            AddEdge(cityE, cityF, GetCityDistance(cityE,cityF));
            
            AddEdge(cityF, cityH, GetCityDistance(cityF,cityH));
            AddEdge(cityF, cityI, GetCityDistance(cityF,cityI));
            
            AddEdge(cityG, cityI, GetCityDistance(cityG,cityI));
            
            AddEdge(cityH, cityI, GetCityDistance(cityH,cityI));
            
            AddEdge(enemyPoint1,cityD, GetCityDistance(enemyPoint1,cityD));
            AddEdge(enemyPoint2,cityD, GetCityDistance(enemyPoint2,cityD));
            AddEdge(enemyPoint3,cityF, GetCityDistance(enemyPoint3,cityF));
            AddEdge(enemyPoint4,cityF, GetCityDistance(enemyPoint4,cityF));
            
            //Guardar caminos de unidades de Wave 1
            List<GraphNode<City>>[] wave1UnitPaths = new List<GraphNode<City>>[7];
            wave1UnitPaths[0] = cityGraph.FindShortestPath(cityI, cityB);
            wave1UnitPaths[1] = cityGraph.FindShortestPath(cityI, enemyPoint1);
            wave1UnitPaths[2] = cityGraph.FindShortestPath(cityI, enemyPoint2);
            wave1UnitPaths[3] = cityGraph.FindShortestPath(cityI, cityE);
            wave1UnitPaths[4] = cityGraph.FindShortestPath(cityI, enemyPoint3);
            wave1UnitPaths[5] = cityGraph.FindShortestPath(cityI, enemyPoint4);
            wave1UnitPaths[6] = cityGraph.FindShortestPath(cityI, cityC);
            wavePathDic.Add(0,wave1UnitPaths);
            
            List<GraphNode<City>>[] wave2UnitPaths = new List<GraphNode<City>>[3];
            wave2UnitPaths[0] = cityGraph.FindShortestPath(cityI, cityA);
            wave2UnitPaths[1] = cityGraph.FindShortestPath(cityI, cityA);
            wave2UnitPaths[2] = cityGraph.FindShortestPath(cityI, cityA);
            wavePathDic.Add(1,wave2UnitPaths);
            
            List<GraphNode<City>>[] wave3UnitPaths = new List<GraphNode<City>>[7];
            wave3UnitPaths[0] = cityGraph.FindShortestPath(cityI, cityB);
            wave3UnitPaths[1] = cityGraph.FindShortestPath(cityI, cityG);
            wave3UnitPaths[2] = cityGraph.FindShortestPath(cityI, cityA);
            wave3UnitPaths[3] = cityGraph.FindShortestPath(cityI, cityE);
            wave3UnitPaths[4] = cityGraph.FindShortestPath(cityI, cityC);
            wave3UnitPaths[5] = cityGraph.FindShortestPath(cityI, enemyPoint4);
            wave3UnitPaths[6] = cityGraph.FindShortestPath(cityI, cityH);
            wavePathDic.Add(2,wave3UnitPaths);
        }

        public int GetCityDistance(City cityA, City cityB)
        {
            float distance = (cityB.transform.position - cityA.transform.position).magnitude;
            return (int)distance;
        }
        
        private City CreateCity(string cityName, Vector3 position)
        {
            GameObject cityObject = Instantiate(cityPrefab, position, Quaternion.identity);
            City city = cityObject.GetComponent<City>();
            city.SetCityName(cityName);
            return city;
        }
        
        private void AddEdge(City fromCity, City toCity, int weight)
        {
            cityGraph.AddEdge(fromCity, toCity, weight);
            cityGraph.AddEdge(toCity, fromCity, weight);
            DrawConnection(fromCity, toCity, weight);
        }
        
        private void DrawConnection(City fromCity, City toCity, int weight)
        {
            // Draw the line
            GameObject lineObject = Instantiate(linePrefab);
            LineRenderer lineRenderer = lineObject.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, fromCity.transform.position);
            lineRenderer.SetPosition(1, toCity.transform.position);

            // Draw the distance label
            Vector3 midPoint = (fromCity.transform.position + toCity.transform.position) / 2;
            GameObject distanceLabelObject = Instantiate(distanceLabelPrefab, midPoint, Quaternion.identity);
            TextMesh textMesh = distanceLabelObject.GetComponent<TextMesh>();
            textMesh.text = weight.ToString();
        }

        public void MoveUnitBetweenCities(MovableUnit unit, City targetCity)
        {
            City currentCity = GetCurrentCity(unit);
            if (currentCity != null)
            {
                List<GraphNode<City>> path = cityGraph.FindShortestPath(currentCity, targetCity);
                if (path != null && path.Count > 0)
                {
                    Debug.Log("Path found, moving unit.");
                    unit.StopCurrentMovement();  // Asegúrate de detener el movimiento actual
                    unit.MoveUnitAlongPath(path);
                }
                else
                {
                    Debug.Log("No valid path found.");
                }
            }
            else
            {
                Debug.Log("Could not determine the current city of the unit.");
            }
        }
        private City GetCurrentCity(MovableUnit unit)
        {
            City closestCity = null;
            float minDistance = float.MaxValue;

            foreach (GraphNode<City> node in cityGraph.Nodes) // Asumiendo que tienes una lista o algún método para acceder a los nodos
            {
                City city = node.value;
                float distance = Vector3.Distance(unit.transform.position, city.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestCity = city;
                }
            }

            return closestCity;
        }

        private float CalculateTravelTimeBetweenCities(City fromCity, City toCity)
        {
            float distance = Vector3.Distance(fromCity.transform.position, toCity.transform.position);
            float travelSpeed = unit.speed; // Velocidad de la unidad (ajustar según tu juego)
            float travelTime = distance / travelSpeed;
            return travelTime;
        }

    }
}