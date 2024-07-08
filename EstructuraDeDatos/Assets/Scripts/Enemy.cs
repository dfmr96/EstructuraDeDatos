using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public abstract class Enemy : MonoBehaviour
    {
        public float maxHealth;
        public abstract void TakeDamage(float damageTaken);
    }
}