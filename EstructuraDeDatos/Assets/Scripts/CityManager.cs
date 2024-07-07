using System.Collections;
using System.Collections.Generic;
using TDAs;
using TDAs.Graphs;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class CityManager : MonoBehaviour
    {
        public GameObject cityPrefab;
        public GameObject linePrefab;
        public GameObject distanceLabelPrefab;
        public DynamicGraph<City> cityGraph;
        public MovableUnit unit;
        public LineRenderer fastestPathRenderer;
        public TMP_Text timeRemainingText;

        private void Start()
        {
            cityGraph = new DynamicGraph<City>();

            City cityA = CreateCity("City A", new Vector3(0, 0, 0));
            City cityB = CreateCity("City B", new Vector3(5, 0, 0));
            City cityC = CreateCity("City C", new Vector3(10, 0, 0));
            City cityD = CreateCity("City D", new Vector3(5, 5, 0));
            City cityE = CreateCity("City E", new Vector3(10, 5, 0));
            
            cityGraph.AddNode(cityA);
            cityGraph.AddNode(cityB);
            cityGraph.AddNode(cityC);
            cityGraph.AddNode(cityD);
            cityGraph.AddNode(cityE);
            
            AddEdge(cityA, cityB, 5);
            AddEdge(cityB, cityC, 5);
            AddEdge(cityA, cityD, 10);
            AddEdge(cityD, cityE, 5);
            AddEdge(cityE, cityC, 5);
            AddEdge(cityB, cityE, 7);
            
            MoveUnitBetweenCities(cityA, cityE);
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

        public void MoveUnitBetweenCities(City fromCity, City toCity)
        {
            List<GraphNode<City>> path = cityGraph.FindShortestPath(fromCity, toCity);
            if (path != null)
            {
                DrawFastestPath(path);
                StartCoroutine(MoveUnitAlongPath(path));
            }
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
    }
}