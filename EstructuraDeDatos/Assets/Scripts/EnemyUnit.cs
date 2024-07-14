using System.Collections;
using System.Collections.Generic;
using Data;
using DefaultNamespace;
using TDAs;
using TDAs.Graphs;
using Unity.Mathematics;
using UnityEngine;

public class EnemyUnit : Enemy
{
    [SerializeField] private UnitData unitData;
    [SerializeField] float currentHealth;
    public float damage;
    [SerializeField] private float speed;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private SpriteRenderer renderer;

    private void Start()
    {
        InitUnit();
    }

    private void InitUnit()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = unitData.sprite;
        currentHealth = unitData.health;
        damage = unitData.atkDamage;
        speed = unitData.speed;
    }
        
    private void InitUnit(UnitData unitData)
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = unitData.sprite;
        currentHealth = unitData.health;
        damage = unitData.atkDamage;
        speed = unitData.speed;
    }

    public override void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            Instantiate(explosionPrefab, transform.position, quaternion.identity);
            Destroy(gameObject);
        }
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

                
                if (renderer == null) yield return null;
                if (startPosition.x < endPosition.x)
                {
                    renderer.flipX = false;
                }
                else
                {
                    renderer.flipX = true;
                }

                // Actualiza el texto del tiempo restante
                yield return null;
            }
                
            OnDestinationReached();
        }
    }

    private void OnDestinationReached()
    {
        //throw new System.NotImplementedException();
    }
    
    public void StopCurrentMovement()
    {
        StopAllCoroutines();  // Detiene todas las corutinas, asegurando que no haya movimientos pendientes
    }
}