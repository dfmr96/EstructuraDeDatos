using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Units/UnitData", fileName = "New UnitData")]
public class UnitData : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite sprite;
    public int atkDamage;
    public int defDamage;
    public int health;
    public int speed;
}
