using System;
using Data;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class EnemyUnit : Enemy
    {
        [SerializeField] private UnitData unitData;
        [SerializeField] float currentHealth;
        public float damage;
        [SerializeField] private float speed;
        [SerializeField] private GameObject explosionPrefab;

        private void Start()
        {
            InitUnit();
        }

        private void InitUnit()
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = unitData.sprite;
            currentHealth = unitData.health;
            damage = unitData.atkDamage;
            speed = unitData.speed;
        }
        
        private void InitUnit(UnitData unitData)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
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
    }
}