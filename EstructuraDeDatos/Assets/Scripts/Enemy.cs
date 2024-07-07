using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Enemy : MonoBehaviour
    {
        public float health;
        public abstract void TakeDamage(float damageTaken);
    }
}