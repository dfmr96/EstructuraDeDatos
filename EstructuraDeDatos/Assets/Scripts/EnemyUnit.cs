using System;
using Data;
using UnityEngine;

namespace DefaultNamespace
{
    public enum UnitState
    {
        Attacking,
        Moving
    }
    public class EnemyUnit : Enemy
    {
        [SerializeField] private UnitState state;
        [SerializeField] private UnitData unit;
        [SerializeField] float currentHealth;
        private float damage;
        [SerializeField] private float speed;
        [SerializeField] private CircleCollider2D col;
        private float atkTimer;
        private float atkRate;

        [SerializeField] private Player target;

        [SerializeField] private PlayerUnitDetector _unitDetector;
    
        private void Start()
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = unit.sprite;
            currentHealth = unit.health;
            damage = unit.atkDamage;
            speed = unit.speed;
            col.radius = unit.range;
            state = UnitState.Moving;
            atkRate = unit.atkRate;
            atkTimer = 0;

            _unitDetector.OnPlayerDetected += TargetPlayer;
        }
        
        private void OnDisable()
        {
            _unitDetector.OnPlayerDetected -= TargetPlayer;
        }

        private void Update()
        {
            switch (state)
            {
                case UnitState.Attacking:
                    atkTimer += Time.deltaTime;

                    if (atkTimer >= atkRate)
                    {
                        AttackPlayer();
                        atkTimer = 0;
                    }
                    break;
                case UnitState.Moving:
                    transform.Translate(-transform.right * (speed * Time.deltaTime));
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

        public void TargetPlayer(Player player)
        {
            if (player == null)
            {
                target = null;
                state = UnitState.Moving;
                return;
            }
            target = player;
            state = UnitState.Attacking;
        }

        public void AttackPlayer()
        {
            target.TakeDamage(damage);
        }
    }
}