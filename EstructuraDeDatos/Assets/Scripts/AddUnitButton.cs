using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class AddUnitButton : MonoBehaviour
{
    public UnitData unitData;
    public int amount;
    public Squad squad;

    public void AddUnits()
    {
        squad.AddUnits(unitData,amount);
    }
}
