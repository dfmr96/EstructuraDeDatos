using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerUnitDetector : MonoBehaviour
    {
        public Player target;
        public Action<Player> OnPlayerDetected;
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                target = player;
                OnPlayerDetected?.Invoke(player);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                if (player != target) return;
                target = null;
                OnPlayerDetected?.Invoke(null);
                //state = UnitState.Moving;
            }
        }
    }
}