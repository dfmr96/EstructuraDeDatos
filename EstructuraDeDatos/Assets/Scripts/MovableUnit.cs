using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using TDAs;
using TDAs.Graphs;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public class MovableUnit : MonoBehaviour
    {
        public UnitData unitData;
        public float currentHealth;
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

        public GameObject explosionPrefab;

        public GameObject unitUIPrefab;

        public UnitScroll UnitScrollInfo;

        private UnitScrollList unitScrollList;

        public void InitUnit(UnitData unitData, GameObject unitListContent, UnitScrollList unitScrollList)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = unitData.sprite;
            currentHealth = unitData.health;
            damage = unitData.atkDamage;
            speed = unitData.speed;

            this.unitScrollList = unitScrollList;
            GenerateUnitListItem(unitData, unitListContent);
            unitScrollList.UpdateList();

        }

        private void GenerateUnitListItem(UnitData unitData, GameObject unitListContent)
        {
            GameObject unitUI = Instantiate(unitUIPrefab, unitListContent.transform);
            if (unitUI.TryGetComponent(out UnitScroll unitScroll))
            {
                UnitScrollInfo = unitScroll;
                unitScroll.UpdateInfo(unitData.bigSprite, unitData.health, (int)currentHealth, unitData.name, gameObject);
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

        public void MoveUnitAlongPath(List<GraphNode<City>> path)
        {
            StartCoroutine(MoveAlongPath(path));
        }

        private IEnumerator  MoveAlongPath(List<GraphNode<City>> path)
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
                
                OnDestinationReached();
            }
        }

        private void OnDestinationReached()
        {
            // Logic to handle what happens when the unit reaches its destination
            Debug.Log("Destination reached");
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
            if (collision.gameObject.TryGetComponent(out EnemyUnit enemy))
            {
                enemy.TakeDamage(damage);
                TakeDamage(enemy.damage);
            }

            if (collision.gameObject.TryGetComponent(out EnemyCastle castle))
            {
                castle.TakeDamage(damage);
                DestroyUnit();
            }
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            UnitScrollInfo.UpdateHealth( (int)currentHealth);
            unitScrollList.UpdateList();

            if (currentHealth <= 0)
            {
                DestroyUnit();
            }
        }
        
        private void DestroyUnit()
        {
            Destroy(UnitScrollInfo.gameObject);
            unitScrollList.UpdateList();
            Instantiate(explosionPrefab, transform.position, quaternion.identity);
            Destroy(gameObject);
        }
    }
}