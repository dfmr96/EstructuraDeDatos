using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitScroll : MonoBehaviour
{
    public Image unitSprite;
    public Image healthBar;
    public TMP_Text unitName;
    private float maxHealth;
    public float currentHealth;

    public void UpdateInfo(Sprite unitSprite,int maxHealth, int health, string unitName)
    {
        this.unitSprite.sprite = unitSprite;
        this.maxHealth = maxHealth;
        healthBar.fillAmount = health / (float)maxHealth;
        this.unitName.SetText(unitName);
    }

    public void UpdateHealth(int health)
    {
        healthBar.fillAmount = health / maxHealth;
        currentHealth = health;
        Debug.Log(healthBar.fillAmount);
        Debug.Log(maxHealth);
        Debug.Log(health);
    }
}
