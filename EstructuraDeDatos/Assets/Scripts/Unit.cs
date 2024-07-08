using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using DefaultNamespace;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;

public enum UnitState
{
    Attacking,
    Moving
}
public class Unit : Player
{
    private UnitState state;
    [field: SerializeField] public UnitData UnitData { get; private set; }
    [SerializeField] private float currentHealth;
    private float damage;
    private float speed;
    [SerializeField] private CircleCollider2D col;
    private float atkTimer;
    private float atkRate;

    [SerializeField] private Enemy target;

    [SerializeField] private UnitDetector _unitDetector;
    
    private void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        //col = GetComponent<CircleCollider2D>();
        renderer.sprite = UnitData.sprite;
        currentHealth = UnitData.health;
        damage = UnitData.atkDamage;
        speed = UnitData.speed;
        col.radius = UnitData.range;
        state = UnitState.Moving;
        atkRate = UnitData.atkRate;
        atkTimer = 0;

        _unitDetector.OnEnemyDetected += TargetEnemy;
    }

    private void OnDisable()
    {
        _unitDetector.OnEnemyDetected -= TargetEnemy;
    }

    private void Update()
    {
        switch (state)
        {
            case UnitState.Attacking:
                atkTimer += Time.deltaTime;

                if (atkTimer >= atkRate)
                {
                    AttackEnemy();
                    atkTimer = 0;
                }
                break;
            case UnitState.Moving:
                transform.Translate(Vector3.right * (speed * Time.deltaTime));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TargetEnemy(Enemy enemy)
    {
        if (enemy == null)
        {
            target = null;
            state = UnitState.Moving;
            return;
        }
        target = enemy;
        state = UnitState.Attacking;
    }

    public void AttackEnemy()
    {
        target.TakeDamage(damage);
    }
}
