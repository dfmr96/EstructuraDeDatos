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
        public MovableUnit selectedUnit; // Unidad seleccionada actualmente
        public GameObject unitPrefab;

        public UnitData infantryData;
        public UnitData tankData;
        public UnitData artilleryData;

        public GameObject unitListContent;

        public UnitScrollList UnitScrollList;
        
        
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        
        
        
        public void UpdateSelectedUnit(MovableUnit unit)
        {
            selectedUnit = unit;
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
            cityGraph = new DynamicGraph<City>();

            City cityA = CreateCity("City A", cityPositions[0].position);
            City cityB = CreateCity("City B", cityPositions[1].position);
            City cityC = CreateCity("City C", cityPositions[2].position);
            City cityD = CreateCity("City D", cityPositions[3].position);
            City cityE = CreateCity("City E", cityPositions[4].position);
            City cityF = CreateCity("City F", cityPositions[5].position);
            City cityG = CreateCity("City G", cityPositions[6].position);
            City cityH = CreateCity("City H", cityPositions[7].position);
            City cityI = CreateCity("City I", cityPositions[8].position);
            
            cityGraph.AddNode(cityA);
            cityGraph.AddNode(cityB);
            cityGraph.AddNode(cityC);
            cityGraph.AddNode(cityD);
            cityGraph.AddNode(cityE);
            cityGraph.AddNode(cityF);
            cityGraph.AddNode(cityG);
            cityGraph.AddNode(cityH);
            cityGraph.AddNode(cityI);
            
            
            AddEdge(cityA, cityB, GetCityDistance(cityA, cityB));
            AddEdge(cityA, cityC, GetCityDistance(cityA,cityC));
            AddEdge(cityA, cityD, GetCityDistance(cityA,cityD));
            AddEdge(cityA, cityE, GetCityDistance(cityA,cityE));
            AddEdge(cityA, cityF, GetCityDistance(cityA,cityF));
            
            
            AddEdge(cityB, cityD, GetCityDistance(cityB,cityD));
            AddEdge(cityB, cityG, GetCityDistance(cityB,cityG));
            
            AddEdge(cityC, cityF, GetCityDistance(cityC,cityF));
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
            
            //MoveUnitBetweenCities(cityA, cityE);
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
        
        private void DrawFastestPath(List<GraphNode<City>> path)
        {
            fastestPathRenderer.positionCount = path.Count;
            for (int i = 0; i < path.Count; i++)
            {
                fastestPathRenderer.SetPosition(i, path[i].value.transform.position);
            }
        }

        private IEnumerator MoveUnitAlongPath(List<GraphNode<City>> path)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                City startCity = path[i].value;
                City endCity = path[i + 1].value;
                float travelTime = CalculateTravelTimeBetweenCities(startCity, endCity);
                float elapsedTime = 0;

                // Actualizar el tiempo restante mientras se mueve
                while (elapsedTime < travelTime)
                {
                    float timeRemaining = travelTime - elapsedTime;
                    timeRemainingText.text = $"Tiempo Restante: {timeRemaining:F1} segundos";

                    // Calcular el porcentaje de avance entre las ciudades
                    float t = elapsedTime / travelTime;

                    // Interpolar la posición entre la ciudad actual y la siguiente
                    Vector3 startPosition = startCity.transform.position;
                    Vector3 endPosition = endCity.transform.position;
                    unit.transform.position = Vector3.Lerp(startPosition, endPosition, t);

                    elapsedTime += Time.deltaTime;

                    yield return null; // Esperar un frame
                }

                // Asegurar que la unidad llegue exactamente a la posición final
                unit.transform.position = endCity.transform.position;

                // Lógica opcional al llegar a cada ciudad
                Debug.Log($"Llegó a {endCity.cityName}");

                // Opcional: Pausa antes de continuar al siguiente tramo
                yield return new WaitForSeconds(1f);
            }

            Debug.Log("Recorrido del camino completo");
        }
        
        private void UpdateTimeRemainingTextPosition(Vector3 unitPosition)
        {
            // Ajustar la posición del texto al lado de la unidad
            Vector3 offset = new Vector3(0, 1, 0); // Ajusta según la posición visual deseada
            timeRemainingText.transform.position = unitPosition + offset;
        }
        
        
        
        /*private float CalculateTotalTravelTime(List<GraphNode<City>> path)
        {
            float totalTime = 0;
            for (int i = 0; i < path.Count; i++)
            {
                totalTime += CalculateTravelTimeBetweenCities(path, i);
            }
            return totalTime;
        }*/
        
        private float CalculateTravelTimeBetweenCities(City fromCity, City toCity)
        {
            float distance = Vector3.Distance(fromCity.transform.position, toCity.transform.position);
            float travelSpeed = unit.speed; // Velocidad de la unidad (ajustar según tu juego)
            float travelTime = distance / travelSpeed;
            return travelTime;
        }
        
        private City GetCurrentCity(Unit unit)
        {
            // Implementar lógica para determinar la ciudad actual de la unidad
            // Puede ser basado en la posición o alguna lógica específica de tu juego
            return null; // Debes implementar esta función según la lógica de tu juego
        }
        
        public void ChangeUnitDestination(City newDestination)
        {
            if (selectedUnit != null && newDestination != null)
            {
                Debug.Log($"Changing destination for {selectedUnit.name} to {newDestination.name}");
                MoveUnitBetweenCities(selectedUnit, newDestination);
            }
            else
            {
                Debug.Log("Either no unit is selected or the new destination is null.");
            }
        }
        
    }
}