using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class EnemyCastle : Enemy
    {
        public float currentHealth;

        [SerializeField] private VictoryPanel _victoryPanel;
        private void Start()
        {
            currentHealth = health;
        }

        public override void TakeDamage(float damageTaken)
        {
            currentHealth -= damageTaken;

            if (currentHealth <= 0)
            {
                Time.timeScale = 0;
                GameManager.Instance.Victory();
            }
        }
    }
}