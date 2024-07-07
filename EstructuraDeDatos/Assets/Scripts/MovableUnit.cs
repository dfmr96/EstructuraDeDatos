using System.Collections;
using System.Collections.Generic;
using TDAs;
using TDAs.Graphs;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class MovableUnit : MonoBehaviour
    {
        public float speed = 1f; // Speed of the unit in units per second
        private Coroutine moveCoroutine; // Referencia a la corrutina de movimiento actual
        public bool isSelected = false; // Estado de selección de la unidad
        [SerializeField] private TMP_Text timeRemainingText;
        public bool isEnemy; // Define si la unidad es enemiga


        private void Start()
        {
            
            var clickable = GetComponent<ClickableObject>();
            if (clickable != null)
            {
                clickable.OnLeftClick.AddListener(ToggleSelection);
            }
        }
        
        private void ToggleSelection()
        {
            isSelected = !isSelected;
            Debug.Log($"Unidad {(isSelected ? "seleccionada" : "deseleccionada")}");
            CityManager.instance.UpdateSelectedUnit(isSelected ? this : null);
        }
        public void MoveToCity(Vector3 destination)
        {
            if (!gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);
            }
            StartCoroutine(MoveToDestination(destination));
        }

        private IEnumerator MoveToDestination(Vector3 destination)
        {
            while (Vector3.Distance(transform.position, destination) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                yield return null;
            }

            // Optional: Call a method or send a message when the destination is reached
            OnDestinationReached();
        }
        
        public void MoveAlongPath(List<GraphNode<City>> path)
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine); // Detener la corrutina anterior si existe
            }
            moveCoroutine = StartCoroutine(MoveCoroutine(path));
        }

        private void OnDestinationReached()
        {
            // Logic to handle what happens when the unit reaches its destination
            Debug.Log("Destination reached");
        }
        
        public void MoveToCity(City targetCity)
        {
            CityManager.instance.MoveUnitBetweenCities(this, targetCity);
        }
        
        private IEnumerator MoveCoroutine(List<GraphNode<City>> path)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                City startCity = path[i].value;
                City endCity = path[i + 1].value;
                float travelTime = CalculateTravelTime(startCity, endCity);
                float elapsedTime = 0;

                while (elapsedTime < travelTime)
                {
                    elapsedTime += Time.deltaTime;
                    float timeRemaining = travelTime - elapsedTime;
                    UpdateTimeRemainingText(timeRemaining);  // Actualizar el UI con el tiempo restante

                    float t = elapsedTime / travelTime;
                    transform.position = Vector3.Lerp(startCity.transform.position, endCity.transform.position, t);
                    yield return null;
                }
                transform.position = endCity.transform.position;
            }
            Debug.Log("Llegó al destino final.");
        }

        private float CalculateTravelTime(City startCity, City endCity)
        {
            float distance = Vector3.Distance(startCity.transform.position, endCity.transform.position);
            return distance / speed;
        }
        
        private void UpdateTimeRemainingText(float timeRemaining)
        {
            if (timeRemainingText != null)
            {
                timeRemainingText.text = $"Tiempo Restante: {timeRemaining:F1} segundos";
            }
        }
        
        void OnCollisionEnter2D(Collision2D collision)
        {
            MovableUnit otherUnit = collision.gameObject.GetComponent<MovableUnit>();
            if (otherUnit != null && otherUnit.isEnemy)
            {
                Debug.Log("Enemy unit destroyed!");
                Destroy(otherUnit.gameObject); // Destruye la unidad enemiga
            }
        }
        
    }
}