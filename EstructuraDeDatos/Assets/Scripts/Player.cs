using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Player : MonoBehaviour
    {
        public float health;
        public abstract void TakeDamage(float damageTaken);
    }
}