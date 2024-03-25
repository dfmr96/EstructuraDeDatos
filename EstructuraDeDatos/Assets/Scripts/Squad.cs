using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
    [SerializeField] private List<Units> units;

    
    public void AddUnits(UnitData unitToAdd, int amount)
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (unitToAdd == units[i].unitData)
            {
                Debug.Log($"Se han encontrado {units[i].amount}{units[i].unitData.name}");
                Units tempUnits = units[i];
                tempUnits.amount += amount;
                Debug.Log($"Se han agregado {amount}{unitToAdd.name}");
                units[i] = tempUnits;
                Debug.Log($"Ahora hay {units[i].amount}{units[i].unitData.name}");
                return;
            }
        }
        
        units.Add(new Units(unitToAdd,amount));
    }
}
[Serializable]
public struct Units
{
    public int amount;
    public UnitData unitData;

    public Units(UnitData unitData, int amount)
    {
        this.unitData = unitData;
        this.amount = amount;
    }

}
