using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using TDAs;
using TDAs.Graphs;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class MovableUnit : MonoBehaviour
    {
        public UnitData unitData;
        public int currentHealth;
        public int damage;
        
        public float speed = 1f; // Speed of the unit in units per second
        private Coroutine moveCoroutine; // Referencia a la corrutina de movimiento actual
        public bool isSelected = false; // Estado de selección de la unidad
        [SerializeField] private TMP_Text timeRemainingText;
        public bool isEnemy; // Define si la unidad es enemiga
        public Vector3 destination;
        private Coroutine currentMoveCoroutine;
        public Color selectedColor = Color.red; // Color cuando la unidad está seleccionada
        public Color defaultColor = Color.white;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            //InitUnit();
        }

        public void InitUnit(UnitData unitData)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = unitData.sprite;
            currentHealth = unitData.health;
            damage = unitData.atkDamage;
            speed = unitData.speed;
        }

        private void Start()
        {
            //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            //col = GetComponent<CircleCollider2D>();
            
            
            var clickable = GetComponent<ClickableObject>();
            if (clickable != null)
            {
                clickable.OnLeftClick.AddListener(ToggleSelection);
            }
        }
        void OnMouseDown()
        {
            UnitManager.instance.SelectUnit(this);
        }
        
        public void Select()
        {
            spriteRenderer.color = selectedColor;  // Cambia el color a seleccionado
        }

        public void Deselect()
        {
            spriteRenderer.color = defaultColor;  // Vuelve al color original
        }
        
        private void ToggleSelection()
        {
            isSelected = !isSelected;
            //spriteRenderer.color = isSelected ? selectedColor : defaultColor;
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
        
        public void MoveUnitAlongPath(List<GraphNode<City>> path)
        {
            StartCoroutine(MoveAlongPath(path));
        }
        
        public IEnumerator  MoveAlongPath(List<GraphNode<City>> path)
        {
            foreach (var node in path)
            {
                Vector3 startPosition = transform.position;
                Vector3 endPosition = node.value.transform.position;
                float journeyLength = Vector3.Distance(startPosition, endPosition);
                float journeyTravelled = 0f;

                while (journeyTravelled < journeyLength)
                {
                    journeyTravelled += speed * Time.deltaTime;
                    float fractionOfJourney = journeyTravelled / journeyLength;
                    transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);

                    if (startPosition.x < endPosition.x)
                    {
                        spriteRenderer.flipX = false;
                    }
                    else
                    {
                        spriteRenderer.flipX = true;
                    }

                    // Actualiza el texto del tiempo restante
                    float remainingTime = (journeyLength - journeyTravelled) / speed;
                    UpdateTimeRemainingText(remainingTime);

                    yield return null;
                }
            }
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
        
        public void StopCurrentMovement()
        {
            StopAllCoroutines();  // Detiene todas las corutinas, asegurando que no haya movimientos pendientes
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
        
        public void MoveTo(Vector3 newDestination)
        {
            destination = newDestination;
            if (currentMoveCoroutine != null)
            {
                StopCoroutine(currentMoveCoroutine);
            }
            currentMoveCoroutine = StartCoroutine(MoveTowardsDestination());
        }
        
        private IEnumerator MoveTowardsDestination()
        {
            while (Vector3.Distance(transform.position, destination) > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
                yield return null;
            }
        }
        
        public void ChangeDestination(Vector3 newDestination)
        {
            MoveTo(newDestination);
        }
        
        
    }
}