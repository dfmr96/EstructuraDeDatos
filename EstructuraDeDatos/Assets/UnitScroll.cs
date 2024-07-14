using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
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
    public GameObject unit;

    public void UpdateInfo(Sprite unitSprite,int maxHealth, int health, string unitName, GameObject unit)
    {
        this.unitSprite.sprite = unitSprite;
        this.unit = unit;
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

    public void SelectUnit()
    {
        UnitManager.instance.SelectUnit(unit.GetComponent<MovableUnit>());
        Debug.Log(unit.name);
    }
}
