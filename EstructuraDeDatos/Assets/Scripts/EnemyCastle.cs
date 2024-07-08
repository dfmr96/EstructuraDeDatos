using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCastle : Enemy
{
    public float currentHealth;
    public Image healthBar;
    
    [SerializeField] private VictoryPanel _victoryPanel;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public override void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;
        healthBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            Time.timeScale = 0;
            Debug.Log("Victory!!!");
            GameManager.Instance.Victory();
        }
    }
}