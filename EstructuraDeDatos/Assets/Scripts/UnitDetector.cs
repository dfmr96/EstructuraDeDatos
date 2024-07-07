using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class UnitDetector : MonoBehaviour
{
    public Enemy target;
    public Action<Enemy> OnEnemyDetected;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            target = enemy;
            OnEnemyDetected?.Invoke(enemy);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            target = enemy;
            OnEnemyDetected?.Invoke(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (enemy != target) return;
            target = null;
            OnEnemyDetected?.Invoke(null);
            //state = UnitState.Moving;
        }
    }
}
